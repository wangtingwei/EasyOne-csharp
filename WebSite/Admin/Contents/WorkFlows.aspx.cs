namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.Controls;
    using EasyOne.Model.WorkFlow;
    using EasyOne.Web.UI;
    using EasyOne.WorkFlows;
    using System;
    using System.Web.UI.WebControls;

    public partial class WorkFlows : AdminPage
    {

        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                bool flag = false;
                bool flag2 = false;
                WorkFlowsInfo workFlowsInfo = new WorkFlowsInfo();
                if (this.HdnAction.Value == "Modify")
                {
                    workFlowsInfo.FlowId = BasePage.RequestInt32("FlowID");
                }
                workFlowsInfo.FlowName = this.TxtFlowName.Text;
                workFlowsInfo.Description = this.TxtDescription.Text;
                string text = this.TxtFlowName.Text;
                if ((this.HdnAction.Value == "Modify") && (text == this.HdnFlowName.Value))
                {
                    flag = false;
                }
                else if (WorkFlow.Exists(text))
                {
                    AdminPage.WriteErrMsg("<li>系统已经有此流程名称，请返回重新填写流程名称！</li>");
                }
                if (!flag)
                {
                    if (this.HdnAction.Value == "Modify")
                    {
                        flag2 = WorkFlow.Update(workFlowsInfo);
                    }
                    else
                    {
                        flag2 = WorkFlow.Add(workFlowsInfo);
                    }
                    if (flag2)
                    {
                        base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
                        AdminPage.WriteSuccessMsg("<li>保存流程数据成功！</li>", "WorkFlowsManage.aspx");
                    }
                    else
                    {
                        AdminPage.WriteErrMsg("<li>保存流程数据失败！</li>");
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                string str = BasePage.RequestString("Action", "Add");
                if (str == "Modify")
                {
                    WorkFlowsInfo workFlowsById = WorkFlow.GetWorkFlowsById(BasePage.RequestInt32("FlowID"));
                    if (!workFlowsById.IsNull)
                    {
                        this.TxtFlowName.Text = workFlowsById.FlowName;
                        this.TxtDescription.Text = workFlowsById.Description;
                        this.HdnAction.Value = str;
                        this.HdnFlowName.Value = workFlowsById.FlowName.ToString();
                    }
                }
            }
        }
    }
}

