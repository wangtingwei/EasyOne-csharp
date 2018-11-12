namespace EasyOne.Shop
{
    using EasyOne.Accessories;
    using EasyOne.Model.Accessories;
    using System;

    public class MessageOfOrder : AbstractMessageOfOrder
    {
        private string m_AdminName;
        private string m_UserName;

        public MessageOfOrder(string adminName, string userName)
        {
            this.m_AdminName = adminName;
            this.m_UserName = userName;
        }

        public override void Send()
        {
            if (string.IsNullOrEmpty(base.MessageBody))
            {
                base.ErrorMsg.Append("<br><br>发送的内容为空，所以发送失败！");
            }
            else
            {
                this.SendMessage();
            }
        }

        private void SendMessage()
        {
            MessageInfo messageInfo = new MessageInfo();
            messageInfo.Title = base.MessageTitle;
            messageInfo.Incept = this.m_UserName;
            messageInfo.Sender = this.m_AdminName;
            messageInfo.Content = base.MessageBody;
            messageInfo.SendTime = DateTime.Now;
            messageInfo.IsSend = 1;
            if (Message.Add(messageInfo))
            {
                base.SuccessMsg.Append("<br>已经向会员发送了一条站内短信，通知他");
                base.SuccessMsg.Append(base.OperationMsg);
                base.SuccessMsg.Append("！");
            }
            else
            {
                base.ErrorMsg.Append("<br>给会员发送站内短信失败！");
            }
        }
    }
}

