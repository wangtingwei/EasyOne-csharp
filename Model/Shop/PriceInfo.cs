namespace EasyOne.Model.Shop
{
    using EasyOne.Model;
    using System;

    [Serializable]
    public class PriceInfo : EasyOne.Model.Nullable
    {
        private int m_NumberWholesale1;
        private int m_NumberWholesale2;
        private int m_NumberWholesale3;
        private decimal m_Price;
        private decimal m_PriceAgent;
        private decimal m_PriceMember;
        private decimal m_PriceWholesale1;
        private decimal m_PriceWholesale2;
        private decimal m_PriceWholesale3;

        public PriceInfo()
        {
        }

        public PriceInfo(bool value)
        {
            base.IsNull = value;
        }

        public int NumberWholesale1
        {
            get
            {
                return this.m_NumberWholesale1;
            }
            set
            {
                this.m_NumberWholesale1 = value;
            }
        }

        public int NumberWholesale2
        {
            get
            {
                return this.m_NumberWholesale2;
            }
            set
            {
                this.m_NumberWholesale2 = value;
            }
        }

        public int NumberWholesale3
        {
            get
            {
                return this.m_NumberWholesale3;
            }
            set
            {
                this.m_NumberWholesale3 = value;
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

        public decimal PriceAgent
        {
            get
            {
                return this.m_PriceAgent;
            }
            set
            {
                this.m_PriceAgent = value;
            }
        }

        public decimal PriceMember
        {
            get
            {
                return this.m_PriceMember;
            }
            set
            {
                this.m_PriceMember = value;
            }
        }

        public decimal PriceWholesale1
        {
            get
            {
                return this.m_PriceWholesale1;
            }
            set
            {
                this.m_PriceWholesale1 = value;
            }
        }

        public decimal PriceWholesale2
        {
            get
            {
                return this.m_PriceWholesale2;
            }
            set
            {
                this.m_PriceWholesale2 = value;
            }
        }

        public decimal PriceWholesale3
        {
            get
            {
                return this.m_PriceWholesale3;
            }
            set
            {
                this.m_PriceWholesale3 = value;
            }
        }
    }
}

