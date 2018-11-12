namespace EasyOne.Model.Contents
{
    using System;

    public class SpecialCommonModelInfo : CommonModelInfo
    {
        private bool m_EnableSale;
        private decimal m_Price;
        private int m_SpecialId;
        private int m_SpecialInfoId;
        private int m_Stocks;
        private string m_Unit;

        public bool EnableSale
        {
            get
            {
                return this.m_EnableSale;
            }
            set
            {
                this.m_EnableSale = value;
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

        public int SpecialId
        {
            get
            {
                return this.m_SpecialId;
            }
            set
            {
                this.m_SpecialId = value;
            }
        }

        public int SpecialInfoId
        {
            get
            {
                return this.m_SpecialInfoId;
            }
            set
            {
                this.m_SpecialInfoId = value;
            }
        }

        public int Stocks
        {
            get
            {
                return this.m_Stocks;
            }
            set
            {
                this.m_Stocks = value;
            }
        }

        public string Unit
        {
            get
            {
                return this.m_Unit;
            }
            set
            {
                this.m_Unit = value;
            }
        }
    }
}

