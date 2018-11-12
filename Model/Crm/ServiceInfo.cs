namespace EasyOne.Model.Crm
{
    using EasyOne.Model;
    using System;

    [Serializable]
    public class ServiceInfo : EasyOne.Model.Nullable
    {
        private int m_ClientId;
        private string m_ClientName;
        private string m_ConfirmCaller;
        private string m_ConfirmFeedback;
        private int m_ConfirmScore;
        private DateTime? m_ConfirmTime;
        private int m_ContacterId;
        private string m_Feedback;
        private string m_Inputer;
        private int m_ItemId;
        private int m_OrderId;
        private string m_Processor;
        private string m_Remark;
        private string m_ServiceContent;
        private string m_ServiceMode;
        private int m_ServiceResult;
        private DateTime m_ServiceTime;
        private string m_ServiceTitle;
        private string m_ServiceType;
        private string m_ShortedForm;
        private int m_TakeTime;

        public ServiceInfo()
        {
        }

        public ServiceInfo(bool value)
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

        public string ClientName
        {
            get
            {
                return this.m_ClientName;
            }
            set
            {
                this.m_ClientName = value;
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

        public string Inputer
        {
            get
            {
                return this.m_Inputer;
            }
            set
            {
                this.m_Inputer = value;
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

        public string ServiceContent
        {
            get
            {
                return this.m_ServiceContent;
            }
            set
            {
                this.m_ServiceContent = value;
            }
        }

        public string ServiceMode
        {
            get
            {
                return this.m_ServiceMode;
            }
            set
            {
                this.m_ServiceMode = value;
            }
        }

        public int ServiceResult
        {
            get
            {
                return this.m_ServiceResult;
            }
            set
            {
                this.m_ServiceResult = value;
            }
        }

        public DateTime ServiceTime
        {
            get
            {
                return this.m_ServiceTime;
            }
            set
            {
                this.m_ServiceTime = value;
            }
        }

        public string ServiceTitle
        {
            get
            {
                return this.m_ServiceTitle;
            }
            set
            {
                this.m_ServiceTitle = value;
            }
        }

        public string ServiceType
        {
            get
            {
                return this.m_ServiceType;
            }
            set
            {
                this.m_ServiceType = value;
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

        public int TakeTime
        {
            get
            {
                return this.m_TakeTime;
            }
            set
            {
                this.m_TakeTime = value;
            }
        }
    }
}

