namespace EasyOne.WebSite.Admin.CommonModel
{
    using EasyOne.CommonModel;
    using EasyOne.Controls;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.CommonModel;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class TemplateFieldManage : AdminPage
    {

        protected void EgvField_RowCommand(object sender, CommandEventArgs e)
        {
            int templateId = BasePage.RequestInt32("TemplateId");
            if (e.CommandName == "DeleteField")
            {
                if (EasyOne.CommonModel.TemplateField.Delete((string) e.CommandArgument, templateId))
                {
                    AdminPage.WriteSuccessMsg("删除字段成功。", "TemplateFieldManage.aspx?ModelType=" + BasePage.RequestInt32("ModelType").ToString() + "&TemplateId=" + templateId.ToString() + "&TemplateName=" + base.Server.HtmlEncode(BasePage.RequestString("TemplateName")));
                }
                else
                {
                    AdminPage.WriteErrMsg("删除字段失败！", "TemplateFieldManage.aspx?ModelType=" + BasePage.RequestInt32("ModelType").ToString() + "&TemplateId=" + templateId.ToString() + "&TemplateName=" + base.Server.HtmlEncode(BasePage.RequestString("TemplateName")));
                }
            }
        }

        protected void EgvField_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                FieldInfo dataItem = new FieldInfo();
                dataItem = (FieldInfo) e.Row.DataItem;
                ExtendedLinkButton button = (ExtendedLinkButton) e.Row.FindControl("ELbtnDelField");
                if (dataItem.FieldLevel == 0)
                {
                    button.Enabled = false;
                }
                else
                {
                    button.Enabled = true;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.LblTemplateName.Text = "当前模型模板：<a href='TemplateFieldManage.aspx?ModelType=" + BasePage.RequestInt32("ModelType").ToString() + "&TemplateId=" + BasePage.RequestString("TemplateId") + "&TemplateName=" + base.Server.HtmlEncode(BasePage.RequestString("TemplateName")) + "'>" + BasePage.RequestString("TemplateName") + "</a>";
        }
    }
}

