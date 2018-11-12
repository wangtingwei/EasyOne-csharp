namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.Contents;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class SpecialManage : AdminPage
    {

        private void ClearSpecialInfos()
        {
            if (Special.DeleteSpecialInfoBySpecialId(BasePage.RequestInt32("SpecialID")))
            {
                AdminPage.WriteSuccessMsg("已经成功清空该专题的所有内容！", "SpecialManage.aspx");
            }
            else
            {
                AdminPage.WriteErrMsg("清空该专题的所有内容失败！", "SpecialManage.aspx");
            }
        }

        private void DeleteSpecial()
        {
            if (Special.DeleteSpecialById(BasePage.RequestInt32("SpecialID")))
            {
                IncludeFile.CreateIncludeFileByAssociateType(AssociateType.Special);
                base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
                AdminPage.WriteSuccessMsg("<li>专题删除成功！</li>", "SpecialManage.aspx");
            }
            else
            {
                AdminPage.WriteErrMsg("<li>专题删除失败！</li>");
            }
        }

        protected void DropSelectedIndex_Changed(object sender, EventArgs e)
        {
            this.HdnListType.Value = this.DropRescentQuery.SelectedValue;
            this.EgvSpecial.PageIndex = 0;
        }

        protected void EgvSpecial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int specialCategoryId = BasePage.RequestInt32("SpecialCategoryID");
                SpecialInfo dataItem = e.Row.DataItem as SpecialInfo;
                if (dataItem != null)
                {
                    HyperLink link = e.Row.FindControl("HypTitle") as HyperLink;
                    if (link != null)
                    {
                        link.Text = StringHelper.SubString(dataItem.SpecialName, 20, "...");
                        link.NavigateUrl = "Special.aspx?Action=Modify&SpecialID=" + dataItem.SpecialId;
                    }
                    if (specialCategoryId == 0)
                    {
                        specialCategoryId = dataItem.SpecialCategoryId;
                        SpecialCategoryInfo specialCategoryInfoById = Special.GetSpecialCategoryInfoById(specialCategoryId);
                        ExtendedHyperLink link2 = e.Row.FindControl("LblSpecialCategoryLink") as ExtendedHyperLink;
                        link2.BeginTag = "<strong>[";
                        link2.Text = specialCategoryInfoById.SpecialCategoryName;
                        link2.EndTag = "]</strong>";
                        link2.NavigateUrl = "SpecialManage.aspx?SpecialCategoryID=" + specialCategoryId.ToString();
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RolePermissions.BusinessAccessCheck(OperateCode.SpecialManage);
            if (!this.Page.IsPostBack)
            {
                switch (BasePage.RequestStringToLower("Action", ""))
                {
                    case "clear":
                        this.ClearSpecialInfos();
                        break;

                    case "delete":
                        this.DeleteSpecial();
                        break;
                }
                this.DropRescentQuery.SelectedValue = BasePage.RequestStringToLower("ListType");
                this.HdnListType.Value = BasePage.RequestStringToLower("ListType");
                if (BasePage.RequestInt32("SpecialCategoryID") > 0)
                {
                    this.LblOrderType.Visible = false;
                    this.DropRescentQuery.Visible = false;
                }
            }
        }
    }
}

