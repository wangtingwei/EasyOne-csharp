namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Model.WorkFlow;
    using EasyOne.Web.UI;
    using EasyOne.WorkFlows;
    using System;
    using System.Web.UI.WebControls;

    public partial class StatusUI : AdminPage
    {

        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                StatusInfo statusInfo = new StatusInfo();
                int statusId = DataConverter.CLng(this.DropStatusCode.SelectedValue);
                bool flag = false;
                if (this.HdnAction.Value == "Modify")
                {
                    statusInfo.StatusId = BasePage.RequestInt32("StatusID");
                }
                statusInfo.StatusCode = statusId;
                statusInfo.StatusName = this.TxtStatusName.Text;
                statusInfo.StatusType = 1;
                bool flag2 = false;
                if ((this.HdnAction.Value == "Modify") && (statusId == DataConverter.CLng(this.HdnStatusCode.Value)))
                {
                    flag = false;
                }
                else if (Status.Exists(statusId))
                {
                    AdminPage.WriteErrMsg("<li>已经添加过此状态码了，请返回重新选择状态码！</li>");
                }
                if (!flag)
                {
                    if (this.HdnAction.Value == "Modify")
                    {
                        flag2 = Status.Update(statusInfo);
                    }
                    else
                    {
                        flag2 = Status.Add(statusInfo);
                    }
                    if (flag2)
                    {
                        AdminPage.WriteSuccessMsg("<li>保存稿件状态码成功！</li>", "StatusManage.aspx");
                    }
                    else
                    {
                        AdminPage.WriteErrMsg("<li>保存稿件状态码失败！</li>");
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                string str = BasePage.RequestString("Action");
                int num = 0;
                if (str == "Modify")
                {
                    StatusInfo statusById = Status.GetStatusById(BasePage.RequestInt32("StatusID"));
                    if (statusById.StatusId > 0)
                    {
                        for (int i = num; i < 0x63; i++)
                        {
                            this.DropStatusCode.Items.Add(i.ToString());
                        }
                        BasePage.SetSelectedIndexByValue(this.DropStatusCode, statusById.StatusCode.ToString());
                        this.DropStatusCode.Enabled = false;
                        this.TxtStatusName.Text = statusById.StatusName;
                        this.HdnAction.Value = str;
                        this.HdnStatusCode.Value = statusById.StatusCode.ToString();
                    }
                    else
                    {
                        AdminPage.WriteErrMsg("<li>没有找到 StatusID，请返回！</li>");
                    }
                }
                else
                {
                    num = Status.GetStatusById(Status.GetMaxId()).StatusCode + 1;
                    if (num > 0x63)
                    {
                        num = 1;
                    }
                    for (int j = num; j < 0x63; j++)
                    {
                        this.DropStatusCode.Items.Add(j.ToString());
                    }
                }
            }
        }
    }
}

