namespace EasyOne.Shop
{
    using EasyOne.Components;
    using EasyOne.Model.Shop;
    using System;

    public class OrderConfirm : AbstractOperationOfOrder
    {
        private string m_OrderDetail;

        public OrderConfirm(OrderInfo orderInfo, string orderDetail)
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
                content = SiteConfig.SmsConfig.ConfirmOrderMessage;
            }
            else
            {
                content = SiteConfig.ShopConfig.EmailOfOrderConfirm;
            }
            return base.ReplaceOrderInfo(content);
        }

        public override string GetOperationMsg()
        {
            return "订单已经确认";
        }

        public override string GetTitle()
        {
            return AbstractOperationOfOrder.GetGlobalString("MessageTitle_OrderConfirm", "订单确认通知");
        }
    }
}

