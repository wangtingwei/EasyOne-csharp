namespace EasyOne.WebSite.User
{
    using EasyOne.Accessories;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.Accessories;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class MessageRead : DynamicPage
    {


        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            if ((base.Request.QueryString["MessageID"] != null) && (base.Request.QueryString["ManageType"] != null))
            {
                int num = BasePage.RequestInt32("ManageType");
                if (EasyOne.Accessories.Message.Clear((MessageManageType) num, PEContext.Current.User.UserName, BasePage.RequestInt32("MessageID").ToString()))
                {
                    DynamicPage.WriteSuccessMsg("删除短信息成功。", "MessageManager.aspx?ManageType=" + num.ToString());
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

        protected void BtnReturn_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("MessageManager.aspx");
        }

        protected void BtnWrite_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("Message.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
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
                        if (string.Compare(userName, messageById.Incept, StringComparison.Ordinal) == 0)
                        {
                            EasyOne.Accessories.Message.UpdateState(messageId);
                        }
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

