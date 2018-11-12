namespace EasyOne.Model.Shop
{
    using EasyOne.Model;
    using System;

    public class CompareHitsOrderNumberInfo : EasyOne.Model.Nullable
    {
        private double m_CompareRate;
        private int m_Hits;
        private int m_OrderNumber;
        private string m_ProductName;

        public CompareHitsOrderNumberInfo()
        {
        }

        public CompareHitsOrderNumberInfo(bool value)
        {
            base.IsNull = value;
        }

        public double CompareRate
        {
            get
            {
                return this.m_CompareRate;
            }
            set
            {
                this.m_CompareRate = value;
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

        public int OrderNumber
        {
            get
            {
                return this.m_OrderNumber;
            }
            set
            {
                this.m_OrderNumber = value;
            }
        }

        public string ProductName
        {
            get
            {
                return this.m_ProductName;
            }
            set
            {
                this.m_ProductName = value;
            }
        }
    }
}

