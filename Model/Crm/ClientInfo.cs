namespace EasyOne.Model.Crm
{
    using EasyOne.Model;
    using System;

    public class ClientInfo : EasyOne.Model.Nullable
    {
        private int m_Area;
        private decimal m_Balance;
        private int m_ClientField;
        private int m_ClientId;
        private string m_ClientName;
        private string m_ClientNum;
        private int m_ClientType;
        private int m_ComplainTimes;
        private int m_ConnectionLevel;
        private DateTime m_CreateTime;
        private int m_CreditLevel;
        private int m_GroupId;
        private int m_Importance;
        private DateTime m_LastComplainTime;
        private DateTime m_LastServiceTime;
        private DateTime m_LastVisitTime;
        private string m_Owner;
        private int m_ParentId;
        private int m_PhaseType;
        private string m_Remark;
        private int m_ServiceTimes;
        private string m_ShortedForm;
        private int m_SourceType;
        private DateTime m_UpdateTime;
        private int m_ValueLevel;
        private int m_VisitTimes;

        public ClientInfo()
        {
        }

        public ClientInfo(bool value)
        {
            base.IsNull = value;
        }

        public int Area
        {
            get
            {
                return this.m_Area;
            }
            set
            {
                this.m_Area = value;
            }
        }

        public decimal Balance
        {
            get
            {
                return this.m_Balance;
            }
            set
            {
                this.m_Balance = value;
            }
        }

        public int ClientField
        {
            get
            {
                return this.m_ClientField;
            }
            set
            {
                this.m_ClientField = value;
            }
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

        public string ClientNum
        {
            get
            {
                return this.m_ClientNum;
            }
            set
            {
                this.m_ClientNum = value;
            }
        }

        public int ClientType
        {
            get
            {
                return this.m_ClientType;
            }
            set
            {
                this.m_ClientType = value;
            }
        }

        public int ComplainTimes
        {
            get
            {
                return this.m_ComplainTimes;
            }
            set
            {
                this.m_ComplainTimes = value;
            }
        }

        public int ConnectionLevel
        {
            get
            {
                return this.m_ConnectionLevel;
            }
            set
            {
                this.m_ConnectionLevel = value;
            }
        }

        public DateTime CreateTime
        {
            get
            {
                return this.m_CreateTime;
            }
            set
            {
                this.m_CreateTime = value;
            }
        }

        public int CreditLevel
        {
            get
            {
                return this.m_CreditLevel;
            }
            set
            {
                this.m_CreditLevel = value;
            }
        }

        public int GroupId
        {
            get
            {
                return this.m_GroupId;
            }
            set
            {
                this.m_GroupId = value;
            }
        }

        public int Importance
        {
            get
            {
                return this.m_Importance;
            }
            set
            {
                this.m_Importance = value;
            }
        }

        public DateTime LastComplainTime
        {
            get
            {
                return this.m_LastComplainTime;
            }
            set
            {
                this.m_LastComplainTime = value;
            }
        }

        public DateTime LastServiceTime
        {
            get
            {
                return this.m_LastServiceTime;
            }
            set
            {
                this.m_LastServiceTime = value;
            }
        }

        public DateTime LastVisitTime
        {
            get
            {
                return this.m_LastVisitTime;
            }
            set
            {
                this.m_LastVisitTime = value;
            }
        }

        public string Owner
        {
            get
            {
                return this.m_Owner;
            }
            set
            {
                this.m_Owner = value;
            }
        }

        public int ParentId
        {
            get
            {
                return this.m_ParentId;
            }
            set
            {
                this.m_ParentId = value;
            }
        }

        public int PhaseType
        {
            get
            {
                return this.m_PhaseType;
            }
            set
            {
                this.m_PhaseType = value;
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

        public int ServiceTimes
        {
            get
            {
                return this.m_ServiceTimes;
            }
            set
            {
                this.m_ServiceTimes = value;
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

        public int SourceType
        {
            get
            {
                return this.m_SourceType;
            }
            set
            {
                this.m_SourceType = value;
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

        public int ValueLevel
        {
            get
            {
                return this.m_ValueLevel;
            }
            set
            {
                this.m_ValueLevel = value;
            }
        }

        public int VisitTimes
        {
            get
            {
                return this.m_VisitTimes;
            }
            set
            {
                this.m_VisitTimes = value;
            }
        }
    }
}

