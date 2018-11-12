namespace EasyOne.Shop
{
    using EasyOne.Components;
    using EasyOne.Model.Shop;
    using System;

    public class OrderInvoiceAdd : AbstractOperationOfOrder
    {
        private string m_OrderDetail;

        public OrderInvoiceAdd(OrderInfo orderInfo, string orderDetail)
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
                content = SiteConfig.SmsConfig.InvoiceMessage;
            }
            else
            {
                content = SiteConfig.ShopConfig.EmailOfInvoice;
            }
            return base.ReplaceOrderInfo(content);
        }

        public override string GetOperationMsg()
        {
            return "已经开具发票";
        }

        public override string GetTitle()
        {
            return AbstractOperationOfOrder.GetGlobalString("MessageTitle_OrderInvoiceAdd", "开具发票通知");
        }
    }
}

