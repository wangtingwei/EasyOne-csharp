namespace EasyOne.Model.Contents
{
    using EasyOne.Model;
    using System;

    public class CommentPKZoneInfo : EasyOne.Model.Nullable
    {
        private int m_CommentId;
        private string m_Content;
        private string m_IP;
        private int m_PKId;
        private int m_Position;
        private string m_Title;
        private DateTime m_UpdateTime;
        private string m_UserName;

        public CommentPKZoneInfo()
        {
        }

        public CommentPKZoneInfo(bool value)
        {
            base.IsNull = value;
        }

        public int CommentId
        {
            get
            {
                return this.m_CommentId;
            }
            set
            {
                this.m_CommentId = value;
            }
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

        public string IP
        {
            get
            {
                return this.m_IP;
            }
            set
            {
                this.m_IP = value;
            }
        }

        public int PKId
        {
            get
            {
                return this.m_PKId;
            }
            set
            {
                this.m_PKId = value;
            }
        }

        public int Position
        {
            get
            {
                return this.m_Position;
            }
            set
            {
                this.m_Position = value;
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

        public DateTime UpdateTime
        {
            get
            {
                return this.m_UpdateTime;
            }
            set
            {
                this.m_UpdateTime = value;
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
    }
}

