namespace EasyOne.Shop
{
    using EasyOne.Components;
    using EasyOne.Model.Shop;
    using System;

    public class Deliver : AbstractOperationOfOrder
    {
        private string m_ExpressCompany;
        private string m_ExpressNumber;
        private string m_OrderDetail;

        public Deliver(OrderInfo orderInfo, string orderDetail, string expressCompany, string expressNumber)
        {
            base.OrderInfo = orderInfo;
            base.OrderDetail = orderDetail;
            this.m_ExpressCompany = expressCompany;
            this.m_ExpressNumber = expressNumber;
            this.m_OrderDetail = orderDetail;
        }

        public override string GetBody()
        {
            string content = "";
            if (string.IsNullOrEmpty(this.m_OrderDetail))
            {
                content = SiteConfig.SmsConfig.ConsignmentMessage;
            }
            else
            {
                content = SiteConfig.ShopConfig.EmailOfDeliver;
            }
            content = content.Replace("{$ExpressCompany}", this.m_ExpressCompany).Replace("{$ExpressNumber}", this.m_ExpressNumber);
            return base.ReplaceOrderInfo(content);
        }

        public override string GetOperationMsg()
        {
            return "已经发货";
        }

        public override string GetTitle()
        {
            return AbstractOperationOfOrder.GetGlobalString("MessageTitle_Deliver", "发货通知");
        }
    }
}

