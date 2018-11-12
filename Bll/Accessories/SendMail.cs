namespace EasyOne.Accessories
{
    using EasyOne.Components;
    using EasyOne.Enumerations;
    using EasyOne.Model.Accessories;
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Mail;
    using System.Net.Sockets;
    using System.Text;

    public sealed class SendMail
    {
        private SendMail()
        {
        }

        private static MailMessage GetMailMessage(MailInfo mailInfo, MailConfig mailSettings)
        {
            MailMessage message = new MailMessage();
            foreach (MailAddress address in mailInfo.MailToAddressList)
            {
                message.To.Add(address);
            }
            if (mailInfo.MailCopyToAddressList != null)
            {
                foreach (MailAddress address2 in mailInfo.MailCopyToAddressList)
                {
                    message.CC.Add(address2);
                }
            }
            if (mailInfo.ReplyTo != null)
            {
                message.ReplyTo = mailInfo.ReplyTo;
            }
            if (!string.IsNullOrEmpty(mailInfo.FromName))
            {
                message.From = new MailAddress(mailSettings.MailFrom, mailInfo.FromName);
            }
            else
            {
                message.From = new MailAddress(mailSettings.MailFrom);
            }
            message.Subject = mailInfo.Subject;
            message.SubjectEncoding = Encoding.UTF8;
            message.Body = mailInfo.MailBody;
            message.BodyEncoding = Encoding.UTF8;
            message.Priority = mailInfo.Priority;
            message.IsBodyHtml = mailInfo.IsBodyHtml;
            return message;
        }

        public static string GetMailStateInfo(MailState mailcode)
        {
            switch (mailcode)
            {
                case MailState.NoMailToAddress:
                    return "没有可发送的信箱地址";

                case MailState.NoSubject:
                    return "没有邮件题标";

                case MailState.FileNotFind:
                    return "找不到要上传的文件";

                case MailState.MailConfigIsNullOrEmpty:
                    return "邮箱配置错误";

                case MailState.SendFailure:
                    return "发送不成功";

                case MailState.ConfigFileIsWriteOnly:
                    return "配置文件只读";

                case MailState.SaveFailure:
                    return "保存不成功";

                case MailState.SmtpServerNotFind:
                    return "找不到指定的SMTP服务器";

                case MailState.UserNameOrPasswordError:
                    return "用户名或密码错误";

                case MailState.AttachmentSizeLimit:
                    return "附件容量受到限制";

                case MailState.MustIssueStartTlsFirst:
                    return "SMTP 服务器要求安全连接(SSL)或客户端未通过身份验证。";

                case MailState.NonsupportSsl:
                    return "服务器不支持安全连接";

                case MailState.PortError:
                    return "不能建立连接，或者SMTP端口设置有错误";

                case MailState.Ok:
                    return "邮件发送成功";
            }
            return "未知的错误";
        }

        public static MailState Send(MailInfo mailInfo)
        {
            MailConfig mailConfig = SiteConfig.MailConfig;
            MailState mailcode = ValidInfo(mailInfo, mailConfig);
            Attachment item = null;
            if (mailcode == MailState.None)
            {
                SmtpClient client = new SmtpClient();
                MailMessage mailMessage = GetMailMessage(mailInfo, mailConfig);
                try
                {
                    if (!string.IsNullOrEmpty(mailInfo.AttachmentFilePath))
                    {
                        item = new Attachment(mailInfo.AttachmentFilePath, "application/octet-stream");
                        mailMessage.Attachments.Add(item);
                    }
                    client.Host = mailConfig.MailServer;
                    client.Port = mailConfig.Port;
                    NetworkCredential credential = new NetworkCredential(mailConfig.MailServerUserName, mailConfig.MailServerPassWord);
                    string authenticationType = string.Empty;
                    switch (mailConfig.AuthenticationType)
                    {
                        case AuthenticationType.None:
                            client.UseDefaultCredentials = false;
                            break;

                        case AuthenticationType.Basic:
                            client.UseDefaultCredentials = true;
                            authenticationType = "Basic";
                            break;

                        case AuthenticationType.Ntlm:
                            authenticationType = "NTLM";
                            break;
                    }
                    client.EnableSsl = mailConfig.EnabledSsl;
                    client.Credentials = credential.GetCredential(mailConfig.MailServer, mailConfig.Port, authenticationType);
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Send(mailMessage);
                    mailcode = MailState.Ok;
                }
                catch (SmtpException exception)
                {
                    SmtpStatusCode statusCode = exception.StatusCode;
                    if (statusCode != SmtpStatusCode.GeneralFailure)
                    {
                        if (statusCode == SmtpStatusCode.MustIssueStartTlsFirst)
                        {
                            goto Label_0176;
                        }
                        if (statusCode == SmtpStatusCode.MailboxNameNotAllowed)
                        {
                            goto Label_0171;
                        }
                        goto Label_017B;
                    }
                    if (exception.InnerException is IOException)
                    {
                        mailcode = MailState.AttachmentSizeLimit;
                    }
                    else if (exception.InnerException is WebException)
                    {
                        if (exception.InnerException.InnerException == null)
                        {
                            mailcode = MailState.SmtpServerNotFind;
                        }
                        else if (exception.InnerException.InnerException is SocketException)
                        {
                            mailcode = MailState.PortError;
                        }
                    }
                    else
                    {
                        mailcode = MailState.NonsupportSsl;
                    }
                    goto Label_018B;
                Label_0171:
                    mailcode = MailState.UserNameOrPasswordError;
                    goto Label_018B;
                Label_0176:
                    mailcode = MailState.MustIssueStartTlsFirst;
                    goto Label_018B;
                Label_017B:
                    mailcode = MailState.SendFailure;
                }
                finally
                {
                    if (item != null)
                    {
                        item.Dispose();
                    }
                }
            }
        Label_018B:
            mailInfo.Msg = GetMailStateInfo(mailcode);
            return mailcode;
        }

        private static MailState ValidInfo(MailInfo mailInfo, MailConfig mailSettings)
        {
            MailState none = MailState.None;
            if (string.IsNullOrEmpty(mailSettings.MailFrom) || string.IsNullOrEmpty(mailSettings.MailServer))
            {
                return MailState.MailConfigIsNullOrEmpty;
            }
            if (mailInfo.MailToAddressList == null)
            {
                return MailState.NoMailToAddress;
            }
            if (string.IsNullOrEmpty(mailInfo.Subject))
            {
                return MailState.NoSubject;
            }
            if (!string.IsNullOrEmpty(mailInfo.AttachmentFilePath) && !System.IO.File.Exists(mailInfo.AttachmentFilePath))
            {
                none = MailState.FileNotFind;
            }
            return none;
        }
    }
}

