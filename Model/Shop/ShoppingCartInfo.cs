namespace EasyOne.Model.Shop
{
    using EasyOne.Model;
    using System;

    [Serializable]
    public class ShoppingCartInfo : EasyOne.Model.Nullable
    {
        private string m_CartId;
        private int m_CartItemId;
        private int m_InformResult;
        private bool m_IsPresent;
        private int m_ProductId;
        private ProductInfo m_ProductInfo;
        private string m_Property;
        private int m_Quantity;
        private string m_TableName;
        private decimal m_TotalMoney;
        private DateTime m_UpdateTime;
        private string m_UserName;

        public ShoppingCartInfo()
        {
        }

        public ShoppingCartInfo(bool value)
        {
            base.IsNull = value;
        }

        public string CartId
        {
            get
            {
                return this.m_CartId;
            }
            set
            {
                this.m_CartId = value;
            }
        }

        public int CartItemId
        {
            get
            {
                return this.m_CartItemId;
            }
            set
            {
                this.m_CartItemId = value;
            }
        }

        public int InformResult
        {
            get
            {
                return this.m_InformResult;
            }
            set
            {
                this.m_InformResult = value;
            }
        }

        public bool IsPresent
        {
            get
            {
                return this.m_IsPresent;
            }
            set
            {
                this.m_IsPresent = value;
            }
        }

        public int ProductId
        {
            get
            {
                return this.m_ProductId;
            }
            set
            {
                this.m_ProductId = value;
            }
        }

        public ProductInfo ProductInfomation
        {
            get
            {
                if (this.m_ProductInfo == null)
                {
                    this.m_ProductInfo = new ProductInfo();
                }
                return this.m_ProductInfo;
            }
            set
            {
                this.m_ProductInfo = value;
            }
        }

        public string Property
        {
            get
            {
                return this.m_Property;
            }
            set
            {
                this.m_Property = value;
            }
        }

        public int Quantity
        {
            get
            {
                return this.m_Quantity;
            }
            set
            {
                this.m_Quantity = value;
            }
        }

        public string TableName
        {
            get
            {
                return this.m_TableName;
            }
            set
            {
                this.m_TableName = value;
            }
        }

        public decimal TotalMoney
        {
            get
            {
                return this.m_TotalMoney;
            }
            set
            {
                this.m_TotalMoney = value;
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

        public string UserName
        {
            get
            {
                return this.m_UserName;
            }
            set
            {
                this.m_UserName = value;
            }
        }
    }
}

