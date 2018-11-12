namespace EasyOne.WebSite.Controls.FieldControl
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Model.WorkFlow;
    using EasyOne.Web.UI;
    using EasyOne.WorkFlows;
    using System;
    using System.Web;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class StatusType : BaseFieldControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                int nodeId = DataConverter.CLng(HttpContext.Current.Request["NodeId"]);
                if (nodeId == 0)
                {
                    nodeId = ContentManage.GetContentNodeId(DataConverter.CLng(HttpContext.Current.Request["GeneralId"]));
                }
                if (base.IsAdminManage)
                {
                    int nodeWorkFlowId = Nodes.GetNodeWorkFlowId(nodeId);
                    string roles = PEContext.Current.Admin.Roles;
                    FlowProcessInfo flowProcessByRoles = FlowProcess.GetFlowProcessByRoles(nodeWorkFlowId, roles);
                    if (BaseUserControl.RequestStringToLower("Action") == "modify")
                    {
                        ListItem item = new ListItem("草稿", "-1");
                        ListItem item2 = new ListItem("不改变当前状态", this.FieldValue);
                        item2.Selected = true;
                        if (PEContext.Current.Admin.IsSuperAdmin)
                        {
                            ListItem item3 = new ListItem("退稿", "-2");
                            this.RadlStatus.Items.Add(item);
                            this.RadlStatus.Items.Add(item3);
                            if (this.FieldValue == "99")
                            {
                                item2.Text = "终审通过";
                                this.RadlStatus.Items.Add(item2);
                            }
                            else
                            {
                                this.RadlStatus.Items.Add(item2);
                                ListItem item4 = new ListItem("终审通过", "99");
                                this.RadlStatus.Items.Add(item4);
                            }
                        }
                        else if (!flowProcessByRoles.IsNull)
                        {
                            ListItem item5 = new ListItem(flowProcessByRoles.RejectActionName, flowProcessByRoles.RejectActionStatus.ToString());
                            ListItem item6 = new ListItem(flowProcessByRoles.PassActionName, flowProcessByRoles.PassActionStatus.ToString());
                            this.RadlStatus.Items.Add(item5);
                            if (this.FieldValue == flowProcessByRoles.PassActionStatus.ToString())
                            {
                                item2.Text = flowProcessByRoles.PassActionName;
                                this.RadlStatus.Items.Add(item2);
                            }
                            else
                            {
                                this.RadlStatus.Items.Add(item2);
                                this.RadlStatus.Items.Add(item6);
                            }
                        }
                        else
                        {
                            this.RadlStatus.Items.Add(item2);
                        }
                    }
                    else
                    {
                        ListItem item7 = new ListItem("草稿", "-1");
                        ListItem item8 = new ListItem("待审核", "0");
                        this.RadlStatus.Items.Add(item7);
                        this.RadlStatus.Items.Add(item8);
                        item8.Selected = true;
                        if (PEContext.Current.Admin.IsSuperAdmin)
                        {
                            ListItem item9 = new ListItem("终审通过", "99");
                            item9.Selected = true;
                            this.RadlStatus.Items.Add(item9);
                        }
                        else if (!flowProcessByRoles.IsNull)
                        {
                            ListItem item10 = new ListItem(flowProcessByRoles.PassActionName, flowProcessByRoles.PassActionStatus.ToString());
                            item10.Selected = true;
                            this.RadlStatus.Items.Add(item10);
                        }
                    }
                }
                else if (PEContext.Current.User.Identity.IsAuthenticated)
                {
                    if (HttpContext.Current.Request["Action"] == "Modify")
                    {
                        ListItem item11 = new ListItem("草稿", "-1");
                        ListItem item12 = new ListItem("不改变当前状态", this.FieldValue);
                        if (this.FieldValue == "-1")
                        {
                            item11.Selected = true;
                            item12.Text = "投稿";
                            item12.Value = "0";
                        }
                        else
                        {
                            item12.Selected = true;
                        }
                        this.RadlStatus.Items.Add(item11);
                        this.RadlStatus.Items.Add(item12);
                    }
                    else
                    {
                        ListItem item13 = new ListItem("草稿", "-1");
                        ListItem item14 = new ListItem("投稿", "0");
                        this.RadlStatus.Items.Add(item13);
                        this.RadlStatus.Items.Add(item14);
                        item14.Selected = true;
                    }
                }
                else
                {
                    ListItem item15 = new ListItem("投稿", "0");
                    this.RadlStatus.Items.Add(item15);
                    item15.Selected = true;
                }
                this.RadlStatus.RepeatLayout = RepeatLayout.Flow;
                this.RadlStatus.RepeatDirection = RepeatDirection.Horizontal;
            }
            else
            {
                this.FieldValue = this.RadlStatus.SelectedValue;
            }
        }
    }
}

