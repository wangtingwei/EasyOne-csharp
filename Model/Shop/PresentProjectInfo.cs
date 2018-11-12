namespace EasyOne.Model.Shop
{
    using EasyOne.Model;
    using System;

    public class PresentProjectInfo : EasyOne.Model.Nullable
    {
        private DateTime m_BeginDate;
        private decimal m_Cash;
        private bool m_Disabled;
        private DateTime m_EndDate;
        private decimal m_MaxMoney;
        private decimal m_MinMoney;
        private string m_PresentContent;
        private int m_PresentExp;
        private string m_PresentId;
        private int m_PresentPoint;
        private decimal m_Price;
        private int m_ProjectId;
        private string m_ProjectName;

        public PresentProjectInfo()
        {
        }

        public PresentProjectInfo(bool value)
        {
            base.IsNull = value;
        }

        public DateTime BeginDate
        {
            get
            {
                return this.m_BeginDate;
            }
            set
            {
                this.m_BeginDate = value;
            }
        }

        public decimal Cash
        {
            get
            {
                return this.m_Cash;
            }
            set
            {
                this.m_Cash = value;
            }
        }

        public bool Disabled
        {
            get
            {
                return this.m_Disabled;
            }
            set
            {
                this.m_Disabled = value;
            }
        }

        public DateTime EndDate
        {
            get
            {
                return this.m_EndDate;
            }
            set
            {
                this.m_EndDate = value;
            }
        }

        public decimal MaxMoney
        {
            get
            {
                return this.m_MaxMoney;
            }
            set
            {
                this.m_MaxMoney = value;
            }
        }

        public decimal MinMoney
        {
            get
            {
                return this.m_MinMoney;
            }
            set
            {
                this.m_MinMoney = value;
            }
        }

        public string PresentContent
        {
            get
            {
                return this.m_PresentContent;
            }
            set
            {
                this.m_PresentContent = value;
            }
        }

        public int PresentExp
        {
            get
            {
                return this.m_PresentExp;
            }
            set
            {
                this.m_PresentExp = value;
            }
        }

        public string PresentId
        {
            get
            {
                return this.m_PresentId;
            }
            set
            {
                this.m_PresentId = value;
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

        public decimal Price
        {
            get
            {
                return this.m_Price;
            }
            set
            {
                this.m_Price = value;
            }
        }

        public int ProjectId
        {
            get
            {
                return this.m_ProjectId;
            }
            set
            {
                this.m_ProjectId = value;
            }
        }

        public string ProjectName
        {
            get
            {
                return this.m_ProjectName;
            }
            set
            {
                this.m_ProjectName = value;
            }
        }
    }
}

