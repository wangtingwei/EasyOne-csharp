namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Model.WorkFlow;
    using EasyOne.Web.UI;
    using EasyOne.WorkFlows;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI.WebControls;
    using EasyOne.Model.UserManage;

    public partial class FlowProcessUI : AdminPage
    {

        protected void DropStatusCodeDataBind(ListControl dropName, int listType)
        {
            IList<StatusInfo> statusList = Status.GetStatusList(listType);
            if (statusList.Count > 0)
            {
                dropName.Items.Clear();
                dropName.DataSource = statusList;
                dropName.DataBind();
            }
            else
            {
                dropName.Items.Clear();
            }
        }

        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                if (this.DropPassActionStatus.SelectedValue == this.DropRejectActionStatus.SelectedValue)
                {
                    AdminPage.WriteErrMsg("<li>“通过后的状态”与“打回后的状态”必须不同！</li>");
                }
                FlowProcessInfo flowProcessInfo = new FlowProcessInfo();
                if (this.HdnAction.Value == "Modify")
                {
                    flowProcessInfo.ProcessId = BasePage.RequestInt32("ProcessID");
                    flowProcessInfo.FlowId = BasePage.RequestInt32("FlowID");
                }
                flowProcessInfo.FlowId = DataConverter.CLng(this.HdnFlowId.Value);
                flowProcessInfo.ProcessName = this.TxtProcessName.Text;
                flowProcessInfo.Description = this.TxtDescription.Text;
                flowProcessInfo.PassActionName = this.TxtPassActionName.Text;
                flowProcessInfo.PassActionStatus = DataConverter.CLng(this.DropPassActionStatus.SelectedValue);
                flowProcessInfo.RejectActionName = this.TxtRejectActionName.Text;
                flowProcessInfo.RejectActionStatus = DataConverter.CLng(this.DropRejectActionStatus.SelectedValue);
                StringBuilder listControl = this.GetListControl(this.ListProcessStatusCode, "请选择可以执行操作的状态码！");
                string roleIds = this.EChklProcessGroup.SelectList();
                bool flag = false;
                if (this.HdnAction.Value == "Modify")
                {
                    flag = FlowProcess.Update(flowProcessInfo, listControl.ToString(), roleIds);
                }
                else if (!FlowProcess.ExistFlowProcess(flowProcessInfo.FlowId, flowProcessInfo.ProcessName))
                {
                    flag = FlowProcess.Add(flowProcessInfo, listControl.ToString(), roleIds);
                }
                else
                {
                    AdminPage.WriteErrMsg("<li>存在相同的步骤名！</li>");
                }
                if (flag)
                {
                    AdminPage.WriteSuccessMsg("<li>保存流程数据成功！</li>", "FlowProcessManage.aspx?FlowId=" + BasePage.RequestString("FlowId") + "&ProcessID=" + BasePage.RequestString("FlowId"));
                }
                else
                {
                    AdminPage.WriteErrMsg("<li>保存流程数据失败！</li>");
                }
            }
        }

        private StringBuilder GetListControl(ListControl listControl, string errInfo)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < listControl.Items.Count; i++)
            {
                if (listControl.Items[i].Selected)
                {
                    StringHelper.AppendString(sb, listControl.Items[i].Value);
                }
            }
            if (sb.Length < 1)
            {
                AdminPage.WriteErrMsg("<li>" + errInfo + "</li>");
            }
            return sb;
        }

        private void GetProcessStatusCodeList(int flowId, int processId)
        {
            IList<StatusInfo> processStatusCodeList = FlowProcess.GetProcessStatusCodeList(flowId, processId);
            for (int i = 0; i < processStatusCodeList.Count; i++)
            {
                foreach (ListItem item in this.ListProcessStatusCode.Items)
                {
                    if (item.Value == processStatusCodeList[i].StatusCode.ToString())
                    {
                        item.Selected = true;
                        break;
                    }
                }
            }
        }

        protected void ListStatusCodeDataBind(int listType)
        {
            IList<StatusInfo> statusList = Status.GetStatusList(listType);
            if (statusList.Count > 0)
            {
                this.ListProcessStatusCode.Items.Clear();
                this.ListProcessStatusCode.DataSource = statusList;
                this.ListProcessStatusCode.DataBind();
            }
            else
            {
                this.ListProcessStatusCode.Items.Clear();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                string action = BasePage.RequestString("Action", "Add");
                int workFlowsId = BasePage.RequestInt32("FlowId");
                int processId = BasePage.RequestInt32("ProcessID");
                this.HdnFlowId.Value = workFlowsId.ToString();
                string flowName = WorkFlow.GetWorkFlowsById(workFlowsId).FlowName;
                this.ListStatusCodeDataBind(1);
                this.DropStatusCodeDataBind(this.DropPassActionStatus, 2);
                this.DropStatusCodeDataBind(this.DropRejectActionStatus, 3);
                this.RolesList(action, workFlowsId, processId);
                this.SmpNavigator.CurrentNode = " " + flowName;
                this.LblWorkFlows.Text = flowName;
                if (action == "Modify")
                {
                    FlowProcessInfo flowProcessById = FlowProcess.GetFlowProcessById(workFlowsId, processId);
                    if (!flowProcessById.IsNull)
                    {
                        this.TxtProcessName.Text = flowProcessById.ProcessName;
                        this.TxtDescription.Text = flowProcessById.Description;
                    }
                    this.GetProcessStatusCodeList(workFlowsId, processId);
                    this.EChklProcessGroup.SetSelectValue(FlowProcess.GetGroupIdByProcessIdAndFlowId(flowProcessById.FlowId, flowProcessById.ProcessId));
                    this.TxtPassActionName.Text = flowProcessById.PassActionName;
                    this.DropPassActionStatus.SelectedValue = flowProcessById.PassActionStatus.ToString();
                    this.TxtRejectActionName.Text = flowProcessById.RejectActionName;
                    this.DropRejectActionStatus.Text = flowProcessById.RejectActionStatus.ToString();
                    this.HdnAction.Value = action;
                    this.HdnProcessId.Value = flowProcessById.ProcessId.ToString();
                }
            }
        }

        protected void RolesList(string action, int flowId, int processId)
        {
            IList<RoleInfo> roleListByFlowIdAndProcessId;
            if (action == "Modify")
            {
                roleListByFlowIdAndProcessId = RolePermissions.GetRoleListByFlowIdAndProcessId(flowId, processId);
            }
            else
            {
                roleListByFlowIdAndProcessId = RolePermissions.GetRoleListByFlowId(flowId);
            }
            if (roleListByFlowIdAndProcessId.Count == 0)
            {
                AdminPage.WriteErrMsg("<li>不能创建该流程步骤，因为可操作的角色已经被其他步骤占用或系统没有定义角色，请添加新的角色！</li>");
            }
            if (roleListByFlowIdAndProcessId.Count > 0)
            {
                this.EChklProcessGroup.Items.Clear();
                this.EChklProcessGroup.DataSource = roleListByFlowIdAndProcessId;
                this.EChklProcessGroup.DataTextField = "RoleName";
                this.EChklProcessGroup.DataValueField = "RoleId";
                this.EChklProcessGroup.DataBind();
            }
        }
    }
}

