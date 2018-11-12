namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class ContentModelManage : AdminPage
    {

        protected void EgvModel_RowCommand(object sender, CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Disabled":
                    ModelManager.Disable(DataConverter.CLng(e.CommandArgument), true);
                    break;

                case "Enabled":
                    ModelManager.Disable(DataConverter.CLng(e.CommandArgument), false);
                    break;

                case "DisabledCharge":
                    ModelManager.EnableCharge(DataConverter.CLng(e.CommandArgument), true);
                    break;

                case "EnableCharge":
                    ModelManager.EnableCharge(DataConverter.CLng(e.CommandArgument), false);
                    break;

                case "DisabledSignin":
                    ModelManager.EnableSignIn(DataConverter.CLng(e.CommandArgument), true);
                    break;

                case "EnabledSignin":
                    ModelManager.EnableSignIn(DataConverter.CLng(e.CommandArgument), false);
                    break;
            }
            this.EgvModel.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                string str;
                int modelId = BasePage.RequestInt32("ModelID");
                if (((str = BasePage.RequestString("Action")) != null) && (str == "Delete"))
                {
                    if (ModelManager.Delete(modelId))
                    {
                        base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
                        AdminPage.WriteSuccessMsg("<li>删除指定的模型成功！</li>", "ModelManage.aspx");
                    }
                    else
                    {
                        AdminPage.WriteErrMsg("<li>删除指定的模型失败！</li>");
                    }
                }
            }
        }
    }
}

