namespace EasyOne.Model.Survey
{
    using EasyOne.Model;
    using System;
    using System.Data;

    public class SurveyRecordInfo : EasyOne.Model.Nullable
    {
        private DataTable m_Answer;
        private string m_IP;
        private int m_RecordId;
        private DateTime m_SubmitTime;
        private int m_SurveyId;
        private string m_UserName;

        public SurveyRecordInfo()
        {
        }

        public SurveyRecordInfo(bool value)
        {
            base.IsNull = value;
        }

        public DataTable Answer
        {
            get
            {
                return this.m_Answer;
            }
            set
            {
                this.m_Answer = value;
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

        public int RecordId
        {
            get
            {
                return this.m_RecordId;
            }
            set
            {
                this.m_RecordId = value;
            }
        }

        public DateTime SubmitTime
        {
            get
            {
                return this.m_SubmitTime;
            }
            set
            {
                this.m_SubmitTime = value;
            }
        }

        public int SurveyId
        {
            get
            {
                return this.m_SurveyId;
            }
            set
            {
                this.m_SurveyId = value;
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

