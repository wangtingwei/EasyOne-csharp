namespace EasyOne.Shop
{
    using EasyOne.Components;
    using EasyOne.Model.Shop;
    using System;

    public class ReceiptMoney : AbstractOperationOfOrder
    {
        private string m_BankName;
        private string m_Money;
        private string m_OrderDetail;

        public ReceiptMoney(OrderInfo orderInfo, string orderDetail, string bankName, string money)
        {
            base.OrderInfo = orderInfo;
            base.OrderDetail = orderDetail;
            this.m_OrderDetail = orderDetail;
            this.m_BankName = bankName;
            this.m_Money = money;
        }

        public override string GetBody()
        {
            string content = "";
            if (string.IsNullOrEmpty(this.m_OrderDetail))
            {
                content = SiteConfig.SmsConfig.RemitMessage;
            }
            else
            {
                content = SiteConfig.ShopConfig.EmailOfReceiptMoney;
            }
            content = content.Replace("{$BankName}", this.m_BankName).Replace("{$Money}", this.m_Money);
            return base.ReplaceOrderInfo(content);
        }

        public override string GetOperationMsg()
        {
            return "支付订单成功";
        }

        public override string GetTitle()
        {
            return AbstractOperationOfOrder.GetGlobalString("MessageTitle_ReceiptMoney", "银行支付通知");
        }
    }
}

