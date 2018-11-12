namespace EasyOne.Model.Crm
{
    using EasyOne.Model;
    using System;

    [Serializable]
    public class ComplainItemInfo : EasyOne.Model.Nullable
    {
        private int m_ClientId;
        private int m_ComplainMode;
        private int m_ComplainType;
        private string m_ConfirmCaller;
        private string m_ConfirmFeedback;
        private int m_ConfirmScore;
        private DateTime? m_ConfirmTime;
        private int m_ContacterId;
        private string m_Content;
        private string m_CurrentOwner;
        private DateTime m_DateAndTime;
        private string m_Defendant;
        private DateTime? m_EndTime;
        private string m_Feedback;
        private string m_FirstReceiver;
        private int m_ItemId;
        private int m_MagnitudeOfExigence;
        private string m_Process;
        private string m_Processor;
        private string m_Remark;
        private string m_Result;
        private string m_ShortedForm;
        private int m_Status;
        private string m_Title;

        public ComplainItemInfo()
        {
        }

        public ComplainItemInfo(bool value)
        {
            base.IsNull = value;
        }

        public int ClientId
        {
            get
            {
                return this.m_ClientId;
            }
            set
            {
                this.m_ClientId = value;
            }
        }

        public int ComplainMode
        {
            get
            {
                return this.m_ComplainMode;
            }
            set
            {
                this.m_ComplainMode = value;
            }
        }

        public int ComplainType
        {
            get
            {
                return this.m_ComplainType;
            }
            set
            {
                this.m_ComplainType = value;
            }
        }

        public string ConfirmCaller
        {
            get
            {
                return this.m_ConfirmCaller;
            }
            set
            {
                this.m_ConfirmCaller = value;
            }
        }

        public string ConfirmFeedback
        {
            get
            {
                return this.m_ConfirmFeedback;
            }
            set
            {
                this.m_ConfirmFeedback = value;
            }
        }

        public int ConfirmScore
        {
            get
            {
                return this.m_ConfirmScore;
            }
            set
            {
                this.m_ConfirmScore = value;
            }
        }

        public DateTime? ConfirmTime
        {
            get
            {
                return this.m_ConfirmTime;
            }
            set
            {
                this.m_ConfirmTime = value;
            }
        }

        public int ContacterId
        {
            get
            {
                return this.m_ContacterId;
            }
            set
            {
                this.m_ContacterId = value;
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

        public string CurrentOwner
        {
            get
            {
                return this.m_CurrentOwner;
            }
            set
            {
                this.m_CurrentOwner = value;
            }
        }

        public DateTime DateAndTime
        {
            get
            {
                return this.m_DateAndTime;
            }
            set
            {
                this.m_DateAndTime = value;
            }
        }

        public string Defendant
        {
            get
            {
                return this.m_Defendant;
            }
            set
            {
                this.m_Defendant = value;
            }
        }

        public DateTime? EndTime
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

        public string Feedback
        {
            get
            {
                return this.m_Feedback;
            }
            set
            {
                this.m_Feedback = value;
            }
        }

        public string FirstReceiver
        {
            get
            {
                return this.m_FirstReceiver;
            }
            set
            {
                this.m_FirstReceiver = value;
            }
        }

        public int ItemId
        {
            get
            {
                return this.m_ItemId;
            }
            set
            {
                this.m_ItemId = value;
            }
        }

        public int MagnitudeOfExigence
        {
            get
            {
                return this.m_MagnitudeOfExigence;
            }
            set
            {
                this.m_MagnitudeOfExigence = value;
            }
        }

        public string Process
        {
            get
            {
                return this.m_Process;
            }
            set
            {
                this.m_Process = value;
            }
        }

        public string Processor
        {
            get
            {
                return this.m_Processor;
            }
            set
            {
                this.m_Processor = value;
            }
        }

        public string Remark
        {
            get
            {
                return this.m_Remark;
            }
            set
            {
                this.m_Remark = value;
            }
        }

        public string Result
        {
            get
            {
                return this.m_Result;
            }
            set
            {
                this.m_Result = value;
            }
        }

        public string ShortedForm
        {
            get
            {
                return this.m_ShortedForm;
            }
            set
            {
                this.m_ShortedForm = value;
            }
        }

        public int Status
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

