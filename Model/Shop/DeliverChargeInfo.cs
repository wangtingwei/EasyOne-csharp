namespace EasyOne.Model.Shop
{
    using EasyOne.Model;
    using System;

    public class DeliverChargeInfo : EasyOne.Model.Nullable
    {
        private int m_AreaType;
        private string m_ArrArea;
        private decimal m_ChargeMax;
        private decimal m_ChargeMin;
        private decimal m_ChargePerUnit;
        private int m_DeliverTypeId;
        private int m_Id;
        private double m_WeightMin;
        private double m_WeightPerUnit;

        public DeliverChargeInfo()
        {
        }

        public DeliverChargeInfo(bool value)
        {
            base.IsNull = value;
        }

        public int AreaType
        {
            get
            {
                return this.m_AreaType;
            }
            set
            {
                this.m_AreaType = value;
            }
        }

        public string ArrArea
        {
            get
            {
                return this.m_ArrArea;
            }
            set
            {
                this.m_ArrArea = value;
            }
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

        public decimal ChargePerUnit
        {
            get
            {
                return this.m_ChargePerUnit;
            }
            set
            {
                this.m_ChargePerUnit = value;
            }
        }

        public int DeliverTypeId
        {
            get
            {
                return this.m_DeliverTypeId;
            }
            set
            {
                this.m_DeliverTypeId = value;
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

        public double WeightMin
        {
            get
            {
                return this.m_WeightMin;
            }
            set
            {
                this.m_WeightMin = value;
            }
        }

        public double WeightPerUnit
        {
            get
            {
                return this.m_WeightPerUnit;
            }
            set
            {
                this.m_WeightPerUnit = value;
            }
        }
    }
}

