namespace EasyOne.Model.Accessories
{
    using EasyOne.Model;
    using System;
    using System.Collections.Generic;
    using System.Net.Mail;

    public class MailInfo : EasyOne.Model.Nullable
    {
        private string m_AttachmentFilePath;
        private string m_FromName;
        private bool m_IsBodyHtml;
        private string m_MailBody;
        private IList<MailAddress> m_MailCopyToAddressList;
        private IList<MailAddress> m_MailToAddressList;
        private string m_Msg;
        private MailPriority m_Priority;
        private MailAddress m_ReplyTo;
        private string m_Subject;

        public MailInfo()
        {
        }

        public MailInfo(bool value)
        {
            base.IsNull = value;
        }

        public string AttachmentFilePath
        {
            get
            {
                return this.m_AttachmentFilePath;
            }
            set
            {
                this.m_AttachmentFilePath = value;
            }
        }

        public string FromName
        {
            get
            {
                return this.m_FromName;
            }
            set
            {
                this.m_FromName = value;
            }
        }

        public bool IsBodyHtml
        {
            get
            {
                return this.m_IsBodyHtml;
            }
            set
            {
                this.m_IsBodyHtml = value;
            }
        }

        public string MailBody
        {
            get
            {
                return this.m_MailBody;
            }
            set
            {
                this.m_MailBody = value;
            }
        }

        public IList<MailAddress> MailCopyToAddressList
        {
            get
            {
                if (this.m_MailCopyToAddressList == null)
                {
                    this.m_MailCopyToAddressList = new List<MailAddress>();
                }
                return this.m_MailCopyToAddressList;
            }
            set
            {
                this.m_MailCopyToAddressList = value;
            }
        }

        public IList<MailAddress> MailToAddressList
        {
            get
            {
                if (this.m_MailToAddressList == null)
                {
                    this.m_MailToAddressList = new List<MailAddress>();
                }
                return this.m_MailToAddressList;
            }
            set
            {
                this.m_MailToAddressList = value;
            }
        }

        public string Msg
        {
            get
            {
                return this.m_Msg;
            }
            set
            {
                this.m_Msg = value;
            }
        }

        public MailPriority Priority
        {
            get
            {
                return this.m_Priority;
            }
            set
            {
                this.m_Priority = value;
            }
        }

        public MailAddress ReplyTo
        {
            get
            {
                return this.m_ReplyTo;
            }
            set
            {
                this.m_ReplyTo = value;
            }
        }

        public string Subject
        {
            get
            {
                return this.m_Subject;
            }
            set
            {
                this.m_Subject = value;
            }
        }
    }
}

