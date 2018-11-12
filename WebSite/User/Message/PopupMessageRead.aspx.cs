namespace EasyOne.WebSite.User
{
    using EasyOne.Accessories;
    using EasyOne.Components;
    using EasyOne.Enumerations;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.Accessories;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class PopupMessageRead : DynamicPage
    {


        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            if (base.Request.QueryString["MessageID"] != null)
            {
                if (EasyOne.Accessories.Message.Clear(MessageManageType.Inbox, PEContext.Current.User.UserName, BasePage.RequestInt32("MessageID").ToString()))
                {
                    if (string.IsNullOrEmpty(this.LbtnNextMessage.Text))
                    {
                        DynamicPage.WriteSuccessMsg("删除短信息成功。", "~/User/Default.aspx");
                    }
                    else
                    {
                        DynamicPage.WriteSuccessMsg("删除短信息成功。", "PopupMessageRead.aspx?Unread=1&MessageID=" + EasyOne.Accessories.Message.GetUnreadMessageFirstId(PEContext.Current.User.UserName).ToString());
                    }
                }
                else
                {
                    DynamicPage.WriteErrMsg("删除短信息失败！");
                }
            }
        }

        protected void BtnForward_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("Message.aspx?Action=Forward&MessageID=" + BasePage.RequestInt32("MessageID").ToString());
        }

        protected void BtnReply_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("Message.aspx?Action=Reply&MessageID=" + BasePage.RequestInt32("MessageID").ToString());
        }

        protected void BtnWrite_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("Message.aspx");
        }

        protected void LbtnNextMessage_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("PopupMessageRead.aspx?Unread=1&MessageID=" + EasyOne.Accessories.Message.GetUnreadMessageFirstId(PEContext.Current.User.UserName).ToString());
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                if ((BasePage.RequestInt32("Unread") == 1) && (EasyOne.Accessories.Message.UnreadMessageCount(PEContext.Current.User.UserName) > 1))
                {
                    this.LbtnNextMessage.Text = "(下一条)";
                }
                int messageId = BasePage.RequestInt32("MessageID");
                string userName = PEContext.Current.User.UserName;
                if (messageId > 0)
                {
                    MessageInfo messageById = EasyOne.Accessories.Message.GetMessageById(messageId);
                    if (messageById == null)
                    {
                        DynamicPage.WriteErrMsg("<li>指定的短消息ID错误！</li>");
                    }
                    else if ((userName != messageById.Sender) && (userName != messageById.Incept))
                    {
                        DynamicPage.WriteErrMsg("<li>找不到指定的短消息！</li>");
                    }
                    else
                    {
                        this.LblSender.Text = messageById.Sender;
                        this.LblIncept.Text = messageById.Incept;
                        this.LblSendTime.Text = messageById.SendTime.ToString();
                        this.LblTitle.Text = messageById.Title;
                        this.LblContent.Text = messageById.Content;
                        EasyOne.Accessories.Message.UpdateState(messageId);
                    }
                }
                else
                {
                    DynamicPage.WriteErrMsg("<li>请指定短消息ID！</li>");
                }
            }
        }
    }
}

