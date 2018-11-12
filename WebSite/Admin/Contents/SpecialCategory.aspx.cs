namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.Contents;
    using EasyOne.ModelControls;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class SpecialCategory : AdminPage
    {

        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                bool flag = true;
                SpecialCategoryInfo specialCategoryInfo = new SpecialCategoryInfo();
                specialCategoryInfo.NeedCreateHtml = false;
                if (this.HdnAction.Value == "Modify")
                {
                    specialCategoryInfo.SpecialCategoryId = BasePage.RequestInt32("SpecialCategoryID");
                    specialCategoryInfo.OrderId = DataConverter.CLng(this.HdnOrderId.Value);
                    specialCategoryInfo.NeedCreateHtml = true;
                }
                specialCategoryInfo.SpecialCategoryName = this.TxtSpecialCategoryName.Text;
                specialCategoryInfo.SpecialCategoryDir = this.TxtSpecialCategoryDir.Text;
                specialCategoryInfo.SpecialTemplatePath = this.FileCSpecialTemplatePath.Text;
                specialCategoryInfo.SearchTemplatePath = this.FileCSearchTemplatePath.Text;
                specialCategoryInfo.OpenType = DataConverter.CBoolean(this.RadOpenType.SelectedValue);
                specialCategoryInfo.Description = this.TxtDescription.Text;
                specialCategoryInfo.PagePostfix = this.PagePostfix.Value;
                specialCategoryInfo.IsCreateHtml = DataConverter.CBoolean(this.RadlCreatHtml.SelectedValue);
                string text = this.TxtSpecialCategoryName.Text;
                if (((this.HdnAction.Value == "Modify") && (text != this.HdnSpecialCategoryName.Value)) && Special.ExistsSpecialCategoryName(text))
                {
                    AdminPage.WriteErrMsg("<li>系统已经有此专题类别名称，请返回重新填写专题类别名称！</li>");
                }
                if (this.HdnAction.Value == "Modify")
                {
                    flag = Special.UpdateSpecialCategory(specialCategoryInfo);
                }
                else
                {
                    flag = Special.AddSpecialCategory(specialCategoryInfo);
                }
                if (flag)
                {
                    IncludeFile.CreateIncludeFileByAssociateType(AssociateType.Special);
                    base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
                    AdminPage.WriteSuccessMsg("<li>专题类别信息保存成功！</li>", "SpecialCategoryManage.aspx");
                }
                else
                {
                    AdminPage.WriteErrMsg("<li>专题类别信息保存失败！</li>", "SpecialCategoryManage.aspx");
                }
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            RolePermissions.BusinessAccessCheck(OperateCode.SpecialManage);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                string str = BasePage.RequestString("Action", "Add");
                if (str == "Modify")
                {
                    SpecialCategoryInfo specialCategoryInfoById = Special.GetSpecialCategoryInfoById(BasePage.RequestInt32("SpecialCategoryID"));
                    if (!specialCategoryInfoById.IsNull)
                    {
                        this.TxtSpecialCategoryName.Text = specialCategoryInfoById.SpecialCategoryName;
                        this.TxtSpecialCategoryDir.Enabled = false;
                        this.TxtSpecialCategoryDir.Text = specialCategoryInfoById.SpecialCategoryDir;
                        this.FileCSpecialTemplatePath.Text = specialCategoryInfoById.SpecialTemplatePath;
                        this.FileCSearchTemplatePath.Text = specialCategoryInfoById.SearchTemplatePath;
                        this.TxtDescription.Text = specialCategoryInfoById.Description;
                        this.PagePostfix.Value = specialCategoryInfoById.PagePostfix;
                        BasePage.SetSelectedIndexByValue(this.RadlCreatHtml, specialCategoryInfoById.IsCreateHtml.ToString());
                        BasePage.SetSelectedIndexByValue(this.RadOpenType, specialCategoryInfoById.OpenType.ToString());
                        this.HdnAction.Value = str;
                        this.HdnSpecialCategoryName.Value = specialCategoryInfoById.SpecialCategoryName;
                        this.HdnOrderId.Value = specialCategoryInfoById.OrderId.ToString();
                    }
                }
            }
        }
    }
}

