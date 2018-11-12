namespace EasyOne.Model.Shop
{
    using EasyOne.Enumerations;
    using EasyOne.Model;
    using System;

    [Serializable]
    public class UserOrderCommonInfo : EasyOne.Model.Nullable
    {
        private OrderItemInfo m_OrderItemInfo;
        private ProductInfo m_ProductInfo;

        public UserOrderCommonInfo()
        {
            this.m_OrderItemInfo = new OrderItemInfo();
            this.m_ProductInfo = new ProductInfo();
        }

        public UserOrderCommonInfo(bool value) : this()
        {
            base.IsNull = value;
        }

        public int Amount
        {
            get
            {
                return this.m_OrderItemInfo.Amount;
            }
            set
            {
                this.m_OrderItemInfo.Amount = value;
            }
        }

        public DateTime BeginDate
        {
            get
            {
                return this.m_OrderItemInfo.BeginDate;
            }
            set
            {
                this.m_OrderItemInfo.BeginDate = value;
            }
        }

        public string DownloadUrl
        {
            get
            {
                return this.m_ProductInfo.DownloadUrl;
            }
            set
            {
                this.m_ProductInfo.DownloadUrl = value;
            }
        }

        public int OrderItemId
        {
            get
            {
                return this.m_OrderItemInfo.ItemId;
            }
            set
            {
                this.m_OrderItemInfo.ItemId = value;
            }
        }

        public decimal Price
        {
            get
            {
                return this.m_OrderItemInfo.Price;
            }
            set
            {
                this.m_OrderItemInfo.Price = value;
            }
        }

        public int ProductId
        {
            get
            {
                return this.m_ProductInfo.ProductId;
            }
            set
            {
                this.m_ProductInfo.ProductId = value;
            }
        }

        public string ProductName
        {
            get
            {
                return this.m_ProductInfo.ProductName;
            }
            set
            {
                this.m_ProductInfo.ProductName = value;
            }
        }

        public string Remark
        {
            get
            {
                return this.m_ProductInfo.Remark;
            }
            set
            {
                this.m_ProductInfo.Remark = value;
            }
        }

        public int ServiceTerm
        {
            get
            {
                return this.m_OrderItemInfo.ServiceTerm;
            }
            set
            {
                this.m_OrderItemInfo.ServiceTerm = value;
            }
        }

        public EasyOne.Enumerations.ServiceTermUnit ServiceTermUnit
        {
            get
            {
                return this.m_OrderItemInfo.ServiceTermUnit;
            }
            set
            {
                this.m_OrderItemInfo.ServiceTermUnit = value;
            }
        }

        public string TableName
        {
            get
            {
                return this.m_ProductInfo.TableName;
            }
            set
            {
                this.m_ProductInfo.TableName = value;
            }
        }

        public decimal TruePrice
        {
            get
            {
                return this.m_OrderItemInfo.TruePrice;
            }
            set
            {
                this.m_OrderItemInfo.TruePrice = value;
            }
        }

        public string Unit
        {
            get
            {
                return this.m_ProductInfo.Unit;
            }
            set
            {
                this.m_ProductInfo.Unit = value;
            }
        }
    }
}

