namespace EasyOne.Model.Shop
{
    using EasyOne.Model;
    using System;

    [Serializable]
    public class ProductPriceInfo : EasyOne.Model.Nullable
    {
        private int m_GroupId;
        private int m_Id;
        private decimal m_Price;
        private string m_PropertyValue;

        public ProductPriceInfo()
        {
        }

        public ProductPriceInfo(bool value)
        {
            base.IsNull = value;
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

        public string PropertyValue
        {
            get
            {
                return this.m_PropertyValue;
            }
            set
            {
                this.m_PropertyValue = value;
            }
        }
    }
}

