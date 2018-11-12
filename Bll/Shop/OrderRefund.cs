namespace EasyOne.Shop
{
    using EasyOne.Components;
    using EasyOne.Model.Shop;
    using System;

    public class OrderRefund : AbstractOperationOfOrder
    {
        private string m_OrderDetail;

        public OrderRefund(OrderInfo orderInfo, string orderDetail)
        {
            base.OrderInfo = orderInfo;
            base.OrderDetail = orderDetail;
            this.m_OrderDetail = orderDetail;
        }

        public override string GetBody()
        {
            string content = "";
            if (string.IsNullOrEmpty(this.m_OrderDetail))
            {
                content = SiteConfig.SmsConfig.RefundmentMessage;
            }
            else
            {
                content = SiteConfig.ShopConfig.EmailOfRefund;
            }
            return base.ReplaceOrderInfo(content);
        }

        public override string GetOperationMsg()
        {
            return "已经退款";
        }

        public override string GetTitle()
        {
            return AbstractOperationOfOrder.GetGlobalString("MessageTitle_OrderRefund", "退款通知");
        }
    }
}

