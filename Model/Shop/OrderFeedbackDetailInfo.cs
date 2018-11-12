namespace EasyOne.Model.Shop
{
    using EasyOne.Model;
    using System;

    public class OrderFeedbackDetailInfo : EasyOne.Model.Nullable
    {
        private string m_ClientName;
        private EasyOne.Model.Shop.OrderFeedbackInfo m_OrderFeedbackInfo;
        private string m_OrderNum;

        public OrderFeedbackDetailInfo()
        {
        }

        public OrderFeedbackDetailInfo(bool isNull)
        {
            base.IsNull = isNull;
            if (isNull)
            {
                this.m_OrderFeedbackInfo = new EasyOne.Model.Shop.OrderFeedbackInfo(true);
            }
            else
            {
                this.m_OrderFeedbackInfo = new EasyOne.Model.Shop.OrderFeedbackInfo();
            }
        }

        public string ClientName
        {
            get
            {
                return this.m_ClientName;
            }
            set
            {
                this.m_ClientName = value;
            }
        }

        public EasyOne.Model.Shop.OrderFeedbackInfo OrderFeedbackInfo
        {
            get
            {
                return this.m_OrderFeedbackInfo;
            }
            set
            {
                this.m_OrderFeedbackInfo = value;
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
    }
}

