namespace EasyOne.Model.Survey
{
    using System;

    [Serializable]
    public class SurveyInfo
    {
        private DateTime m_CreateDate;
        private string m_Description;
        private DateTime m_EndTime;
        private string m_FileName;
        private int m_IPRepeat;
        private bool m_IsNull;
        private int m_IsOpen;
        private int m_LockIPType;
        private string m_LockUrl;
        private int m_NeedLogin;
        private int m_PresentPoint;
        private string m_QuestionField;
        private int m_QuestionMaxId;
        private string m_SetIPLock;
        private string m_SetPassword;
        private int m_SurveyId;
        private string m_SurveyName;
        private string m_Template;

        public SurveyInfo()
        {
        }

        public SurveyInfo(bool value)
        {
            this.m_IsNull = value;
        }

        public DateTime CreateDate
        {
            get
            {
                return this.m_CreateDate;
            }
            set
            {
                this.m_CreateDate = value;
            }
        }

        public string Description
        {
            get
            {
                return this.m_Description;
            }
            set
            {
                this.m_Description = value;
            }
        }

        public DateTime EndTime
        {
            get
            {
                return this.m_EndTime;
            }
            set
            {
                this.m_EndTime = value;
            }
        }

        public string FileName
        {
            get
            {
                return this.m_FileName;
            }
            set
            {
                this.m_FileName = value;
            }
        }

        public int IPRepeat
        {
            get
            {
                return this.m_IPRepeat;
            }
            set
            {
                this.m_IPRepeat = value;
            }
        }

        public bool IsNull
        {
            get
            {
                return this.m_IsNull;
            }
        }

        public int IsOpen
        {
            get
            {
                return this.m_IsOpen;
            }
            set
            {
                this.m_IsOpen = value;
            }
        }

        public int LockIPType
        {
            get
            {
                return this.m_LockIPType;
            }
            set
            {
                this.m_LockIPType = value;
            }
        }

        public string LockUrl
        {
            get
            {
                return this.m_LockUrl;
            }
            set
            {
                this.m_LockUrl = value;
            }
        }

        public int NeedLogin
        {
            get
            {
                return this.m_NeedLogin;
            }
            set
            {
                this.m_NeedLogin = value;
            }
        }

        public int PresentPoint
        {
            get
            {
                return this.m_PresentPoint;
            }
            set
            {
                this.m_PresentPoint = value;
            }
        }

        public string QuestionField
        {
            get
            {
                return this.m_QuestionField;
            }
            set
            {
                this.m_QuestionField = value;
            }
        }

        public int QuestionMaxId
        {
            get
            {
                return this.m_QuestionMaxId;
            }
            set
            {
                this.m_QuestionMaxId = value;
            }
        }

        public string SetIPLock
        {
            get
            {
                return this.m_SetIPLock;
            }
            set
            {
                this.m_SetIPLock = value;
            }
        }

        public string SetPassword
        {
            get
            {
                return this.m_SetPassword;
            }
            set
            {
                this.m_SetPassword = value;
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

        public string SurveyName
        {
            get
            {
                return this.m_SurveyName;
            }
            set
            {
                this.m_SurveyName = value;
            }
        }

        public string Template
        {
            get
            {
                return this.m_Template;
            }
            set
            {
                this.m_Template = value;
            }
        }
    }
}

