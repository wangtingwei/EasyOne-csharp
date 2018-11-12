namespace EasyOne.Model.Shop
{
    using EasyOne.Model;
    using System;

    public class MemberExpenditureInfo : EasyOne.Model.Nullable
    {
        private decimal m_MoneyReceipt;
        private string m_UserName;

        public MemberExpenditureInfo()
        {
        }

        public MemberExpenditureInfo(bool isNull)
        {
            base.IsNull = isNull;
        }

        public decimal MoneyReceipt
        {
            get
            {
                return this.m_MoneyReceipt;
            }
            set
            {
                this.m_MoneyReceipt = value;
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

