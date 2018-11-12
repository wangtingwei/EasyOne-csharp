namespace EasyOne.Model.Shop
{
    using EasyOne.Model;
    using System;

    public class ProductOrderNumberInfo : EasyOne.Model.Nullable
    {
        private int m_OrderNumber;
        private string m_ProductName;

        public ProductOrderNumberInfo()
        {
        }

        public ProductOrderNumberInfo(bool value)
        {
            base.IsNull = value;
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

