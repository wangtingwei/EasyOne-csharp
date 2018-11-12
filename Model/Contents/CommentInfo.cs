namespace EasyOne.Model.Contents
{
    using EasyOne.Model;
    using System;

    public class CommentInfo : EasyOne.Model.Nullable
    {
        private int m_Agree;
        private int m_CommentId;
        private string m_CommentTitle;
        private string m_Content;
        private string m_Email;
        private string m_Face;
        private int m_FaceHeight;
        private int m_FaceWidth;
        private int m_GeneralId;
        private string m_Ip;
        private bool m_IsElite;
        private bool m_IsPrivate;
        private int m_Neutral;
        private int m_NodeId;
        private int m_Oppose;
        private int m_PassedItems;
        private int m_Position;
        private string m_Reply;
        private string m_ReplyAdmin;
        private DateTime m_ReplyDateTime;
        private bool m_ReplyIsPrivate;
        private string m_ReplyUserName;
        private int m_Score;
        private bool m_Status;
        private int m_TopicId;
        private DateTime m_UpdateDateTime;
        private int m_UserExp;
        private string m_UserFace;
        private int m_UserId;
        private string m_UserName;
        private int m_UserPoint;
        private DateTime m_UserRegTime;

        public CommentInfo()
        {
        }

        public CommentInfo(bool value)
        {
            base.IsNull = value;
        }

        public int Agree
        {
            get
            {
                return this.m_Agree;
            }
            set
            {
                this.m_Agree = value;
            }
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

        public string CommentTitle
        {
            get
            {
                return this.m_CommentTitle;
            }
            set
            {
                this.m_CommentTitle = value;
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

        public string Email
        {
            get
            {
                return this.m_Email;
            }
            set
            {
                this.m_Email = value;
            }
        }

        public string Face
        {
            get
            {
                return this.m_Face;
            }
            set
            {
                this.m_Face = value;
            }
        }

        public int FaceHeight
        {
            get
            {
                return this.m_FaceHeight;
            }
            set
            {
                this.m_FaceHeight = value;
            }
        }

        public int FaceWidth
        {
            get
            {
                return this.m_FaceWidth;
            }
            set
            {
                this.m_FaceWidth = value;
            }
        }

        public int GeneralId
        {
            get
            {
                return this.m_GeneralId;
            }
            set
            {
                this.m_GeneralId = value;
            }
        }

        public string IP
        {
            get
            {
                return this.m_Ip;
            }
            set
            {
                this.m_Ip = value;
            }
        }

        public bool IsElite
        {
            get
            {
                return this.m_IsElite;
            }
            set
            {
                this.m_IsElite = value;
            }
        }

        public bool IsPrivate
        {
            get
            {
                return this.m_IsPrivate;
            }
            set
            {
                this.m_IsPrivate = value;
            }
        }

        public int Neutral
        {
            get
            {
                return this.m_Neutral;
            }
            set
            {
                this.m_Neutral = value;
            }
        }

        public int NodeId
        {
            get
            {
                return this.m_NodeId;
            }
            set
            {
                this.m_NodeId = value;
            }
        }

        public int Oppose
        {
            get
            {
                return this.m_Oppose;
            }
            set
            {
                this.m_Oppose = value;
            }
        }

        public int PassedItems
        {
            get
            {
                return this.m_PassedItems;
            }
            set
            {
                this.m_PassedItems = value;
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

        public string Reply
        {
            get
            {
                return this.m_Reply;
            }
            set
            {
                this.m_Reply = value;
            }
        }

        public string ReplyAdmin
        {
            get
            {
                return this.m_ReplyAdmin;
            }
            set
            {
                this.m_ReplyAdmin = value;
            }
        }

        public DateTime ReplyDateTime
        {
            get
            {
                return this.m_ReplyDateTime;
            }
            set
            {
                this.m_ReplyDateTime = value;
            }
        }

        public bool ReplyIsPrivate
        {
            get
            {
                return this.m_ReplyIsPrivate;
            }
            set
            {
                this.m_ReplyIsPrivate = value;
            }
        }

        public string ReplyUserName
        {
            get
            {
                return this.m_ReplyUserName;
            }
            set
            {
                this.m_ReplyUserName = value;
            }
        }

        public int Score
        {
            get
            {
                return this.m_Score;
            }
            set
            {
                this.m_Score = value;
            }
        }

        public bool Status
        {
            get
            {
                return this.m_Status;
            }
            set
            {
                this.m_Status = value;
            }
        }

        public int TopicId
        {
            get
            {
                return this.m_TopicId;
            }
            set
            {
                this.m_TopicId = value;
            }
        }

        public DateTime UpdateDateTime
        {
            get
            {
                return this.m_UpdateDateTime;
            }
            set
            {
                this.m_UpdateDateTime = value;
            }
        }

        public int UserExp
        {
            get
            {
                return this.m_UserExp;
            }
            set
            {
                this.m_UserExp = value;
            }
        }

        public string UserFace
        {
            get
            {
                return this.m_UserFace;
            }
            set
            {
                this.m_UserFace = value;
            }
        }

        public int UserId
        {
            get
            {
                return this.m_UserId;
            }
            set
            {
                this.m_UserId = value;
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

        public int UserPoint
        {
            get
            {
                return this.m_UserPoint;
            }
            set
            {
                this.m_UserPoint = value;
            }
        }

        public DateTime UserRegTime
        {
            get
            {
                return this.m_UserRegTime;
            }
            set
            {
                this.m_UserRegTime = value;
            }
        }
    }
}

