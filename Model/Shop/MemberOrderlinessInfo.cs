namespace EasyOne.Model.Shop
{
    using EasyOne.Model;
    using System;

    public class MemberOrderlinessInfo : EasyOne.Model.Nullable
    {
        private int m_Amount;
        private string m_ProductName;
        private decimal m_SubTotal;

        public MemberOrderlinessInfo()
        {
        }

        public MemberOrderlinessInfo(bool isNull)
        {
            base.IsNull = isNull;
        }

        public int Amount
        {
            get
            {
                return this.m_Amount;
            }
            set
            {
                this.m_Amount = value;
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

        public decimal SubTotal
        {
            get
            {
                return this.m_SubTotal;
            }
            set
            {
                this.m_SubTotal = value;
            }
        }
    }
}

