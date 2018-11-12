namespace EasyOne.Model.Accessories
{
    using EasyOne.Model;
    using System;

    public class KeywordInfo : EasyOne.Model.Nullable
    {
        private string m_ArrayGeneralId;
        private int m_Hits;
        private int m_KeywordId;
        private string m_KeywordText;
        private int m_KeywordType;
        private DateTime m_LastUseTime;
        private int m_Priority;
        private int m_QuoteTimes;

        public KeywordInfo()
        {
        }

        public KeywordInfo(bool value)
        {
            base.IsNull = value;
        }

        public string ArrayGeneralId
        {
            get
            {
                return this.m_ArrayGeneralId;
            }
            set
            {
                this.m_ArrayGeneralId = value;
            }
        }

        public int Hits
        {
            get
            {
                return this.m_Hits;
            }
            set
            {
                this.m_Hits = value;
            }
        }

        public int KeywordId
        {
            get
            {
                return this.m_KeywordId;
            }
            set
            {
                this.m_KeywordId = value;
            }
        }

        public string KeywordText
        {
            get
            {
                return this.m_KeywordText;
            }
            set
            {
                this.m_KeywordText = value;
            }
        }

        public int KeywordType
        {
            get
            {
                return this.m_KeywordType;
            }
            set
            {
                this.m_KeywordType = value;
            }
        }

        public DateTime LastUseTime
        {
            get
            {
                return this.m_LastUseTime;
            }
            set
            {
                this.m_LastUseTime = value;
            }
        }

        public int Priority
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

        public int QuoteTimes
        {
            get
            {
                return this.m_QuoteTimes;
            }
            set
            {
                this.m_QuoteTimes = value;
            }
        }
    }
}

