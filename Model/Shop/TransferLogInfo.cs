namespace EasyOne.Model.Shop
{
    using EasyOne.Model;
    using System;

    public class TransferLogInfo : EasyOne.Model.Nullable
    {
        private string m_Inputer;
        private int m_OrderId;
        private string m_OrderNum;
        private string m_OwnerUserName;
        private string m_PayerUserName;
        private decimal m_Poundage;
        private string m_Remark;
        private string m_TargetUserName;
        private int m_TransferLogID;
        private DateTime? m_TransferTime;
        private string m_UserName;

        public TransferLogInfo()
        {
        }

        public TransferLogInfo(bool value)
        {
            base.IsNull = value;
        }

        public string Inputer
        {
            get
            {
                return this.m_Inputer;
            }
            set
            {
                this.m_Inputer = value;
            }
        }

        public int OrderId
        {
            get
            {
                return this.m_OrderId;
            }
            set
            {
                this.m_OrderId = value;
            }
        }

        public string OrderNum
        {
            get
            {
                return this.m_OrderNum;
            }
            set
            {
                this.m_OrderNum = value;
            }
        }

        public string OwnerUserName
        {
            get
            {
                return this.m_OwnerUserName;
            }
            set
            {
                this.m_OwnerUserName = value;
            }
        }

        public string PayerUserName
        {
            get
            {
                return this.m_PayerUserName;
            }
            set
            {
                this.m_PayerUserName = value;
            }
        }

        public decimal Poundage
        {
            get
            {
                return this.m_Poundage;
            }
            set
            {
                this.m_Poundage = value;
            }
        }

        public string Remark
        {
            get
            {
                return this.m_Remark;
            }
            set
            {
                this.m_Remark = value;
            }
        }

        public string TargetUserName
        {
            get
            {
                return this.m_TargetUserName;
            }
            set
            {
                this.m_TargetUserName = value;
            }
        }

        public int TransferLogId
        {
            get
            {
                return this.m_TransferLogID;
            }
            set
            {
                this.m_TransferLogID = value;
            }
        }

        public DateTime? TransferTime
        {
            get
            {
                return this.m_TransferTime;
            }
            set
            {
                this.m_TransferTime = value;
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

