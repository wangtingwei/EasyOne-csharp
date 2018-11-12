namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Model.Contents;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class ContentMessage : AdminPage
    {
        private int m_NodeId;
        private int m_State;


        private static string ChangeStateSendMessageToUser(string itemIDList, GridView gv, int state, string sendContent)
        {
            StringBuilder builder = new StringBuilder();
            string[] strArray = itemIDList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            if (strArray.Length == gv.Rows.Count)
            {
                for (int i = 0; i < gv.Rows.Count; i++)
                {
                    Label label = (Label) gv.Rows[i].FindControl("LTitle");
                    builder.Append(Users.ChangeStateSendMessageToUser(gv.Rows[i].Cells[3].Text, sendContent, label.Text, state.ToString()));
                }
            }
            else
            {
                foreach (string str in strArray)
                {
                    for (int j = 0; j < gv.Rows.Count; j++)
                    {
                        if (str == gv.Rows[j].Cells[1].Text)
                        {
                            Label label2 = (Label) gv.Rows[j].FindControl("LTitle");
                            builder.Append(Users.ChangeStateSendMessageToUser(gv.Rows[j].Cells[3].Text, sendContent, label2.Text, state.ToString()));
                            break;
                        }
                    }
                }
            }
            return builder.ToString();
        }

        protected void EBtnCancelPass_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("ContentManage.aspx?NodeID=" + this.m_NodeId);
        }

        protected void EBtnPass_Click(object sender, EventArgs e)
        {
            StringBuilder selectList = this.EgvContent.SelectList;
            string changeStateMessage = SiteConfig.SmsConfig.ChangeStateMessage;
            if (selectList.Length == 0)
            {
                AdminPage.WriteErrMsg("<li>对不起，您还没选择要发送短信的项目！</li>");
            }
            else if (string.IsNullOrEmpty(changeStateMessage))
            {
                AdminPage.WriteErrMsg("<li>对不起，您没有配置回馈信息！</li>", "ContentManage.aspx?NodeID=" + BasePage.RequestInt32("NodeID"));
            }
            else if ((!changeStateMessage.Contains("{$UserName}") || !changeStateMessage.Contains("{$Title}")) || !changeStateMessage.Contains("{$State}"))
            {
                AdminPage.WriteErrMsg("<li>对不起，您没有配置好回馈信息！</li>", "ContentManage.aspx?NodeID=" + BasePage.RequestInt32("NodeID"));
            }
            else
            {
                AdminPage.WriteSuccessMsg(ChangeStateSendMessageToUser(selectList.ToString(), this.EgvContent, this.m_State, changeStateMessage), "ContentManage.aspx?NodeID=" + BasePage.RequestInt32("NodeID"));
            }
        }

        protected void EgvContent_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CommonModelInfo dataItem = (CommonModelInfo) e.Row.DataItem;
                Label label = e.Row.FindControl("LTitle") as Label;
                label.Text = StringHelper.SubString(dataItem.Title, 40, "...");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.m_NodeId = BasePage.RequestInt32("NodeID");
                this.m_State = BasePage.RequestInt32("State");
                this.HdnIDList.Value = BasePage.RequestStringToLower("ItemIDList");
            }
        }
    }
}

