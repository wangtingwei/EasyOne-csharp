namespace EasyOne.Model.Crm
{
    using EasyOne.Model;
    using System;

    [Serializable]
    public class QuestionInfo : EasyOne.Model.Nullable
    {
        private string m_AntiVirus;
        private string m_ErrorCode;
        private string m_ErrorText;
        private string m_FireWall;
        private int m_Id;
        private string m_IP;
        private bool m_IsPublic;
        private bool m_IsReply;
        private bool m_IsSolved;
        private DateTime m_LastUpdateTime;
        private string m_ProductDBType;
        private string m_ProductVersion;
        private string m_QuestionContent;
        private DateTime m_QuestionCreateTime;
        private string m_QuestionCreator;
        private string m_QuestionTitle;
        private string m_ReplyCreator;
        private DateTime? m_ReplyTime;
        private int m_Score;
        private string m_SystemType;
        private int m_TypeId;
        private string m_TypeName;
        private string m_Url;

        public QuestionInfo()
        {
        }

        public QuestionInfo(bool value)
        {
            base.IsNull = value;
        }

        public string AntiVirus
        {
            get
            {
                return this.m_AntiVirus;
            }
            set
            {
                this.m_AntiVirus = value;
            }
        }

        public string ErrorCode
        {
            get
            {
                return this.m_ErrorCode;
            }
            set
            {
                this.m_ErrorCode = value;
            }
        }

        public string ErrorText
        {
            get
            {
                return this.m_ErrorText;
            }
            set
            {
                this.m_ErrorText = value;
            }
        }

        public string FireWall
        {
            get
            {
                return this.m_FireWall;
            }
            set
            {
                this.m_FireWall = value;
            }
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

        public bool IsPublic
        {
            get
            {
                return this.m_IsPublic;
            }
            set
            {
                this.m_IsPublic = value;
            }
        }

        public bool IsReply
        {
            get
            {
                return this.m_IsReply;
            }
            set
            {
                this.m_IsReply = value;
            }
        }

        public bool IsSolved
        {
            get
            {
                return this.m_IsSolved;
            }
            set
            {
                this.m_IsSolved = value;
            }
        }

        public DateTime LastUpdateTime
        {
            get
            {
                return this.m_LastUpdateTime;
            }
            set
            {
                this.m_LastUpdateTime = value;
            }
        }

        public string ProductDBType
        {
            get
            {
                return this.m_ProductDBType;
            }
            set
            {
                this.m_ProductDBType = value;
            }
        }

        public string ProductVersion
        {
            get
            {
                return this.m_ProductVersion;
            }
            set
            {
                this.m_ProductVersion = value;
            }
        }

        public string QuestionContent
        {
            get
            {
                return this.m_QuestionContent;
            }
            set
            {
                this.m_QuestionContent = value;
            }
        }

        public DateTime QuestionCreateTime
        {
            get
            {
                return this.m_QuestionCreateTime;
            }
            set
            {
                this.m_QuestionCreateTime = value;
            }
        }

        public string QuestionCreator
        {
            get
            {
                return this.m_QuestionCreator;
            }
            set
            {
                this.m_QuestionCreator = value;
            }
        }

        public string QuestionTitle
        {
            get
            {
                return this.m_QuestionTitle;
            }
            set
            {
                this.m_QuestionTitle = value;
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

        public DateTime? ReplyTime
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

        public string SystemType
        {
            get
            {
                return this.m_SystemType;
            }
            set
            {
                this.m_SystemType = value;
            }
        }

        public int TypeId
        {
            get
            {
                return this.m_TypeId;
            }
            set
            {
                this.m_TypeId = value;
            }
        }

        public string TypeName
        {
            get
            {
                return this.m_TypeName;
            }
            set
            {
                this.m_TypeName = value;
            }
        }

        public string Url
        {
            get
            {
                return this.m_Url;
            }
            set
            {
                this.m_Url = value;
            }
        }
    }
}

