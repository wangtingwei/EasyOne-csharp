namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using EasyOne.WorkFlows;
    using System;
    using System.Web.UI.WebControls;

    public partial class FlowProcessManage : AdminPage
    {

        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("FlowProcess.aspx?Action=Add&FlowId=" + BasePage.RequestString("FlowId") + "&ProcessID=" + BasePage.RequestString("FlowId"));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                string str3;
                string str = BasePage.RequestString("Action");
                string flowName = WorkFlow.GetWorkFlowsById(BasePage.RequestInt32("FlowID")).FlowName;
                if (((str3 = str) != null) && (str3 == "Delete"))
                {
                    if (FlowProcess.Delete(BasePage.RequestInt32("FlowID"), BasePage.RequestInt32("ProcessID")))
                    {
                        AdminPage.WriteSuccessMsg("<li>流程步骤删除成功！</li>", "FlowProcessManage.aspx?FlowId=" + BasePage.RequestString("FlowId") + "&ProcessID=" + BasePage.RequestString("FlowId"));
                    }
                    else
                    {
                        AdminPage.WriteErrMsg("<li>流程步骤删除失败！</li>");
                    }
                }
                this.SmpNavigator.CurrentNode = " " + flowName;
            }
        }
    }
}

