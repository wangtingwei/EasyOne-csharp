namespace EasyOne.Model.Shop
{
    using EasyOne.Enumerations;
    using EasyOne.Model;
    using System;

    public class DeliverTypeInfo : EasyOne.Model.Nullable
    {
        private int m_Charge_Percent;
        private decimal m_ChargeMax;
        private decimal m_ChargeMin;
        private int m_ChargeType;
        private TaxRateType m_IncludeTax;
        private string m_Intro;
        private bool m_IsDefault;
        private bool m_IsDisabled;
        private decimal m_MaxCharge;
        private decimal m_MinMoney1;
        private decimal m_MinMoney2;
        private decimal m_MinMoney3;
        private int m_OrderId;
        private decimal m_ReleaseCharge;
        private int m_ReleaseType;
        private double m_TaxRate;
        private int m_TypeId;
        private string m_TypeName;

        public DeliverTypeInfo()
        {
        }

        public DeliverTypeInfo(bool value)
        {
            base.IsNull = value;
        }

        public decimal ChargeMax
        {
            get
            {
                return this.m_ChargeMax;
            }
            set
            {
                this.m_ChargeMax = value;
            }
        }

        public decimal ChargeMin
        {
            get
            {
                return this.m_ChargeMin;
            }
            set
            {
                this.m_ChargeMin = value;
            }
        }

        public int ChargePercent
        {
            get
            {
                return this.m_Charge_Percent;
            }
            set
            {
                this.m_Charge_Percent = value;
            }
        }

        public int ChargeType
        {
            get
            {
                return this.m_ChargeType;
            }
            set
            {
                this.m_ChargeType = value;
            }
        }

        public TaxRateType IncludeTax
        {
            get
            {
                return this.m_IncludeTax;
            }
            set
            {
                this.m_IncludeTax = value;
            }
        }

        public string Intro
        {
            get
            {
                return this.m_Intro;
            }
            set
            {
                this.m_Intro = value;
            }
        }

        public bool IsDefault
        {
            get
            {
                return this.m_IsDefault;
            }
            set
            {
                this.m_IsDefault = value;
            }
        }

        public bool IsDisabled
        {
            get
            {
                return this.m_IsDisabled;
            }
            set
            {
                this.m_IsDisabled = value;
            }
        }

        public decimal MaxCharge
        {
            get
            {
                return this.m_MaxCharge;
            }
            set
            {
                this.m_MaxCharge = value;
            }
        }

        public decimal MinMoney1
        {
            get
            {
                return this.m_MinMoney1;
            }
            set
            {
                this.m_MinMoney1 = value;
            }
        }

        public decimal MinMoney2
        {
            get
            {
                return this.m_MinMoney2;
            }
            set
            {
                this.m_MinMoney2 = value;
            }
        }

        public decimal MinMoney3
        {
            get
            {
                return this.m_MinMoney3;
            }
            set
            {
                this.m_MinMoney3 = value;
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

        public decimal ReleaseCharge
        {
            get
            {
                return this.m_ReleaseCharge;
            }
            set
            {
                this.m_ReleaseCharge = value;
            }
        }

        public int ReleaseType
        {
            get
            {
                return this.m_ReleaseType;
            }
            set
            {
                this.m_ReleaseType = value;
            }
        }

        public double TaxRate
        {
            get
            {
                return this.m_TaxRate;
            }
            set
            {
                this.m_TaxRate = value;
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
    }
}

