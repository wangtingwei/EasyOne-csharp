namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using EasyOne.WorkFlows;
    using System;
    using System.Web.UI.WebControls;

    public partial class WorkFlowsManage : AdminPage
    {

        protected void EgvWorkFlows_RowCommand(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "DeleteWorkFlows")
            {
                if (WorkFlow.Delete(DataConverter.CLng(e.CommandArgument.ToString())))
                {
                    base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
                    AdminPage.WriteSuccessMsg("<li>流程删除成功！</li>", "WorkFlowsManage.aspx");
                }
                else
                {
                    AdminPage.WriteErrMsg("<li>流程删除失败！</li>");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str2;
            if ((!this.Page.IsPostBack && ((str2 = BasePage.RequestString("Action")) != null)) && (str2 == "Copy"))
            {
                if (WorkFlow.Copy(BasePage.RequestInt32("FlowID")))
                {
                    base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
                    AdminPage.WriteSuccessMsg("<li>流程复制成功！</li>", "WorkFlowsManage.aspx");
                }
                else
                {
                    AdminPage.WriteErrMsg("<li>流程复制失败！</li>");
                }
            }
        }
    }
}

