namespace EasyOne.Model.Shop
{
    using EasyOne.Model;
    using System;

    public class MemberOrdersInfo : EasyOne.Model.Nullable
    {
        private int m_OrdersNum;
        private string m_UserName;

        public MemberOrdersInfo()
        {
        }

        public MemberOrdersInfo(bool isNull)
        {
            base.IsNull = isNull;
        }

        public int OrderNum
        {
            get
            {
                return this.m_OrdersNum;
            }
            set
            {
                this.m_OrdersNum = value;
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

