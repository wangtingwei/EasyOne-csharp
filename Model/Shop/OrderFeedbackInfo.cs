namespace EasyOne.Model.Shop
{
    using EasyOne.Model;
    using System;

    [Serializable]
    public class OrderFeedbackInfo : EasyOne.Model.Nullable
    {
        private string m_Content;
        private int m_id;
        private int m_OrderId;
        private string m_ReplyContent;
        private string m_ReplyName;
        private DateTime m_ReplyTime;
        private string m_UserName;
        private DateTime m_WriteTime;

        public OrderFeedbackInfo()
        {
        }

        public OrderFeedbackInfo(bool isNull)
        {
            base.IsNull = isNull;
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

        public int Id
        {
            get
            {
                return this.m_id;
            }
            set
            {
                this.m_id = value;
            }
        }

        public int OrderId
        {
            get
            {
                return this.m_OrderId;
            }
            set
            {
                this.m_OrderId = value;
            }
        }

        public string ReplyContent
        {
            get
            {
                return this.m_ReplyContent;
            }
            set
            {
                this.m_ReplyContent = value;
            }
        }

        public string ReplyName
        {
            get
            {
                return this.m_ReplyName;
            }
            set
            {
                this.m_ReplyName = value;
            }
        }

        public DateTime ReplyTime
        {
            get
            {
                return this.m_ReplyTime;
            }
            set
            {
                this.m_ReplyTime = value;
            }
        }

        public string UserName
        {
            get
            {
                return this.m_UserName;
            }
            set
            {
                this.m_UserName = value;
            }
        }

        public DateTime WriteTime
        {
            get
            {
                return this.m_WriteTime;
            }
            set
            {
                this.m_WriteTime = value;
            }
        }
    }
}

