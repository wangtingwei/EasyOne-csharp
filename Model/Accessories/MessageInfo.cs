namespace EasyOne.Model.Accessories
{
    using EasyOne.Model;
    using System;

    [Serializable]
    public class MessageInfo : EasyOne.Model.Nullable
    {
        private string m_Content;
        private string m_Incept;
        private int m_IsDelInbox;
        private int m_IsDelSendbox;
        private int m_IsRead;
        private int m_IsSend;
        private int m_MessageId;
        private string m_Sender;
        private DateTime m_SendTime;
        private string m_Title;

        public MessageInfo()
        {
        }

        public MessageInfo(bool value)
        {
            base.IsNull = value;
        }

        public string Content
        {
            get
            {
                return this.m_Content;
            }
            set
            {
                this.m_Content = value;
            }
        }

        public string Incept
        {
            get
            {
                return this.m_Incept;
            }
            set
            {
                this.m_Incept = value;
            }
        }

        public int IsDelInbox
        {
            get
            {
                return this.m_IsDelInbox;
            }
            set
            {
                this.m_IsDelInbox = value;
            }
        }

        public int IsDelSendbox
        {
            get
            {
                return this.m_IsDelSendbox;
            }
            set
            {
                this.m_IsDelSendbox = value;
            }
        }

        public int IsRead
        {
            get
            {
                return this.m_IsRead;
            }
            set
            {
                this.m_IsRead = value;
            }
        }

        public int IsSend
        {
            get
            {
                return this.m_IsSend;
            }
            set
            {
                this.m_IsSend = value;
            }
        }

        public int MessageId
        {
            get
            {
                return this.m_MessageId;
            }
            set
            {
                this.m_MessageId = value;
            }
        }

        public string Sender
        {
            get
            {
                return this.m_Sender;
            }
            set
            {
                this.m_Sender = value;
            }
        }

        public DateTime SendTime
        {
            get
            {
                return this.m_SendTime;
            }
            set
            {
                this.m_SendTime = value;
            }
        }

        public string Title
        {
            get
            {
                return this.m_Title;
            }
            set
            {
                this.m_Title = value;
            }
        }
    }
}

