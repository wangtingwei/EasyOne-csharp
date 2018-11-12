namespace EasyOne.Model.Crm
{
    using EasyOne.Model;
    using System;

    [Serializable]
    public class ReplyInfo : EasyOne.Model.Nullable
    {
        private int m_Id;
        private int m_QuestionId;
        private string m_ReplyContent;
        private string m_ReplyCreator;
        private DateTime m_ReplyTime;

        public ReplyInfo()
        {
        }

        public ReplyInfo(bool value)
        {
            base.IsNull = value;
        }

        public ReplyInfo(int id, int questionId, string replyCreator, DateTime replyTime, string replyContent)
        {
            this.Id = id;
            this.QuestionId = questionId;
            this.ReplyCreator = replyCreator;
            this.ReplyTime = replyTime;
            this.ReplyContent = replyContent;
        }

        public int Id
        {
            get
            {
                return this.m_Id;
            }
            set
            {
                this.m_Id = value;
            }
        }

        public int QuestionId
        {
            get
            {
                return this.m_QuestionId;
            }
            set
            {
                this.m_QuestionId = value;
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

        public string ReplyCreator
        {
            get
            {
                return this.m_ReplyCreator;
            }
            set
            {
                this.m_ReplyCreator = value;
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
    }
}

