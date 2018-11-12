namespace EasyOne.Shop
{
    using EasyOne.Components;
    using EasyOne.Model.Shop;
    using System;

    public class OrderFlow : AbstractOperationOfOrder
    {
        public OrderFlow(OrderInfo orderInfo, string orderDetail)
        {
            base.OrderInfo = orderInfo;
            base.OrderDetail = orderDetail;
        }

        public override string GetBody()
        {
            string orderMessage = SiteConfig.SmsConfig.OrderMessage;
            return base.ReplaceOrderInfo(orderMessage);
        }

        public override string GetOperationMsg()
        {
            return "下的订单已经成功";
        }

        public override string GetTitle()
        {
            return string.Empty;
        }
    }
}

