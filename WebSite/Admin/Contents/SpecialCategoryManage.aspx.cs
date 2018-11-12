namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.AccessManage;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.Contents;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class SpecialCategoryManage : AdminPage
    {

        private void DeleteSpecialCategory()
        {
            if (Special.DeleteSpecialCategoryById(BasePage.RequestInt32("SpecialCategoryID")))
            {
                IncludeFile.CreateIncludeFileByAssociateType(AssociateType.Special);
                base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
                AdminPage.WriteSuccessMsg("<li>专题类别删除成功！</li>", "SpecialCategoryManage.aspx");
            }
            else
            {
                AdminPage.WriteErrMsg("<li>专题类别删除失败！</li>");
            }
        }

        protected void EgvSpecialCategory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                SpecialCategoryInfo dataItem = (SpecialCategoryInfo) e.Row.DataItem;
                HyperLink link = (HyperLink) e.Row.FindControl("HypSpecialAdd");
                HyperLink link2 = (HyperLink) e.Row.FindControl("HypSpecialBathAdd");
                HyperLink link3 = (HyperLink) e.Row.FindControl("HypSpecialOrder");
                link2.Text = "批量添加专题";
                link.Text = "添加专题";
                link3.Text = "专题排序";
                link.NavigateUrl = "Special.aspx?SpecialCategoryID=" + dataItem.SpecialCategoryId.ToString();
                link2.NavigateUrl = "Special.aspx?SpecialCategoryID=" + dataItem.SpecialCategoryId.ToString() + "&Action=add&AddType=BatchSpecial";
                link3.NavigateUrl = "SpecialOrder.aspx?SpecialCategoryID=" + dataItem.SpecialCategoryId.ToString();
                link.Enabled = RolePermissions.AccessCheck(OperateCode.SpecialManage);
                link3.Enabled = RolePermissions.AccessCheck(OperateCode.SpecialManage);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            RolePermissions.BusinessAccessCheck(OperateCode.SpecialManage);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (BasePage.RequestStringToLower("Action", "") == "delete")
            {
                this.DeleteSpecialCategory();
            }
        }
    }
}

