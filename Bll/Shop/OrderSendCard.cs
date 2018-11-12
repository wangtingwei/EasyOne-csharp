namespace EasyOne.Shop
{
    using EasyOne.Components;
    using EasyOne.Model.Shop;
    using System;

    public class OrderSendCard : AbstractOperationOfOrder
    {
        private string m_MessgeOrMailContentOfCard;
        private string m_OrderDetail;

        public OrderSendCard(OrderInfo orderInfo, string orderDetail, string messgeOrMailContentOfCard)
        {
            base.OrderInfo = orderInfo;
            base.OrderDetail = orderDetail;
            this.m_MessgeOrMailContentOfCard = messgeOrMailContentOfCard;
            this.m_OrderDetail = orderDetail;
        }

        public override string GetBody()
        {
            string content = "";
            if (string.IsNullOrEmpty(this.m_OrderDetail))
            {
                content = SiteConfig.SmsConfig.SendCardNumberMessage;
            }
            else
            {
                content = SiteConfig.ShopConfig.EmailOfSendCard;
            }
            content = content.Replace("{$CardInfo}", this.m_MessgeOrMailContentOfCard);
            return base.ReplaceOrderInfo(content);
        }

        public override string GetOperationMsg()
        {
            return "充值卡已交付";
        }

        public override string GetTitle()
        {
            return AbstractOperationOfOrder.GetGlobalString("MessageTitle_OrderSendCard", "充值卡交付通知");
        }
    }
}

