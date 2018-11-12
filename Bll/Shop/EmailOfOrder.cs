namespace EasyOne.Shop
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Enumerations;
    using EasyOne.Model.Accessories;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using System;
    using System.Collections.Generic;
    using System.Net.Mail;

    public class EmailOfOrder : AbstractMessageOfOrder
    {
        private string m_Email;
        private SendType m_SendType;
        private string m_UserName;

        public EmailOfOrder(string userName) : this(userName, SendType.SendToUser, string.Empty)
        {
        }

        public EmailOfOrder(SendType sendType, string email) : this(string.Empty, sendType, email)
        {
        }

        public EmailOfOrder(string userName, SendType sendType, string email)
        {
            this.m_UserName = userName;
            this.m_SendType = sendType;
            this.m_Email = email;
        }

        public override void Send()
        {
            if ((this.m_SendType == SendType.SendToUser) && !string.IsNullOrEmpty(this.m_UserName))
            {
                UserInfo usersByUserName = new UserInfo();
                usersByUserName = Users.GetUsersByUserName(this.m_UserName);
                if (usersByUserName.IsNull)
                {
                    base.ErrorMsg.Append("<br>找不到订单对应的会员，向会员发送邮件失败！");
                }
                else
                {
                    this.SendEMail(usersByUserName.Email);
                }
            }
            if (this.m_SendType == SendType.SendToContacter)
            {
                this.SendEMail(this.m_Email);
            }
        }

        private void SendEMail(string email)
        {
            string str;
            IList<MailAddress> list = new List<MailAddress>();
            MailState none = MailState.None;
            MailInfo mailInfo = new MailInfo();
            mailInfo.MailBody = base.MessageBody;
            mailInfo.Subject = base.MessageTitle;
            mailInfo.IsBodyHtml = true;
            if (this.m_SendType == SendType.SendToContacter)
            {
                str = "收货人";
            }
            else
            {
                str = "会员";
            }
            if (!string.IsNullOrEmpty(email) && DataValidator.IsEmail(email))
            {
                list.Add(new MailAddress(email));
                mailInfo.MailToAddressList = list;
                none = SendMail.Send(mailInfo);
                if (none == MailState.Ok)
                {
                    base.SuccessMsg.Append("<br>已经向" + str + "发送了一封Email，通知他");
                    base.SuccessMsg.Append(base.OperationMsg);
                    base.SuccessMsg.Append("！");
                }
                else
                {
                    string mailStateInfo = SendMail.GetMailStateInfo(none);
                    base.ErrorMsg.Append("<br>");
                    base.ErrorMsg.Append(mailStateInfo);
                    base.ErrorMsg.Append("，向" + str + "发送邮件失败！");
                }
            }
            else
            {
                base.ErrorMsg.Append("<br>邮件地址为空或无效邮件地址，向" + str + "发送邮件失败！");
            }
        }
    }
}

