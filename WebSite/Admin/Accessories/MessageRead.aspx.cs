namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Accessories;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.Accessories;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class MessageRead : AdminPage
    {


        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            if (base.Request.QueryString["MessageID"] != null)
            {
                if (Message.Delete(MessageDelType.Id, BasePage.RequestInt32("MessageID").ToString()))
                {
                    AdminPage.WriteSuccessMsg("删除短信息成功。", "MessageManage.aspx");
                }
                else
                {
                    AdminPage.WriteErrMsg("删除短信息失败！");
                }
            }
        }

        protected void BtnForward_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("MessageSend.aspx?Action=Forward&MessageID=" + BasePage.RequestInt32("MessageID").ToString());
        }

        protected void BtnReply_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("MessageSend.aspx?Action=Reply&MessageID=" + BasePage.RequestInt32("MessageID").ToString());
        }

        protected void BtnReturn_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("MessageManage.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                string adminName = PEContext.Current.Admin.AdminName;
                int messageId = BasePage.RequestInt32("MessageID");
                MessageInfo messageById = Message.GetMessageById(messageId);
                if (!messageById.IsNull)
                {
                    this.LblSender.Text = messageById.Sender;
                    this.LblIncept.Text = messageById.Incept;
                    this.LblSendTime.Text = messageById.SendTime.ToString();
                    this.LblTitle.Text = messageById.Title;
                    this.LblContent.Text = messageById.Content;
                    if (adminName.Equals(messageById.Incept))
                    {
                        Message.UpdateState(messageId);
                    }
                    if (adminName.Equals(messageById.Sender) || adminName.Equals(messageById.Incept))
                    {
                        this.BtnDelete.Visible = true;
                        this.BtnForward.Visible = true;
                        this.BtnReply.Visible = true;
                    }
                }
            }
        }
    }
}

