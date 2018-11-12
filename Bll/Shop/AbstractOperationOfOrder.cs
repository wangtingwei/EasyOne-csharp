namespace EasyOne.Shop
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Enumerations;
    using EasyOne.Model.Shop;
    using System;
    using System.Text;

    public abstract class AbstractOperationOfOrder
    {
        private string m_OrderDetail;
        private EasyOne.Model.Shop.OrderInfo m_OrderInfo;

        protected AbstractOperationOfOrder()
        {
        }

        public abstract string GetBody();
        private static string GetDeliverType(int deliverTypeId)
        {
            DeliverTypeInfo deliverTypeById = DeliverType.GetDeliverTypeById(deliverTypeId);
            if (!deliverTypeById.IsNull)
            {
                return deliverTypeById.TypeName;
            }
            return string.Empty;
        }

        protected static string GetGlobalString(string resourceKey, string defaultValue)
        {
            string globalString = PEContext.GetGlobalString("EnumResources", resourceKey);
            if (string.IsNullOrEmpty(globalString))
            {
                globalString = defaultValue;
            }
            return globalString;
        }

        public abstract string GetOperationMsg();
        private void GetOrderDetail()
        {
            StringBuilder sb = new StringBuilder();
            foreach (OrderItemInfo info in OrderItem.GetInfoListByOrderId(this.m_OrderInfo.OrderId))
            {
                StringHelper.AppendString(sb, string.Concat(new object[] { info.ProductName, info.Amount, info.Unit, ",价格", info.SubTotal, "元" }), "；");
            }
            this.m_OrderDetail = sb.ToString();
        }

        private static string GetPaymentType(int paymentTypeId)
        {
            PaymentTypeInfo paymentTypeById = PaymentType.GetPaymentTypeById(paymentTypeId);
            if (!paymentTypeById.IsNull)
            {
                return paymentTypeById.TypeName;
            }
            return string.Empty;
        }

        private static string GetPayStatus(decimal moneyTotal, decimal moneyReceipt)
        {
            if (moneyTotal > moneyReceipt)
            {
                if (moneyReceipt > 0M)
                {
                    return PEContext.EnumToHtml<PayStatus>(PayStatus.ReceivedEarnest);
                }
                return PEContext.EnumToHtml<PayStatus>(PayStatus.WaitForPay);
            }
            return PEContext.EnumToHtml<PayStatus>(PayStatus.Payoff);
        }

        public abstract string GetTitle();
        protected string ReplaceOrderInfo(string content)
        {
            content = content.Replace("{$OrderID}", this.m_OrderInfo.OrderId.ToString());
            content = content.Replace("{$OrderNum}", this.m_OrderInfo.OrderNum);
            content = content.Replace("{$ContacterName}", this.m_OrderInfo.ContacterName);
            content = content.Replace("{$MoneyTotal}", this.m_OrderInfo.MoneyTotal.ToString());
            content = content.Replace("{$MoneyReceipt}", this.m_OrderInfo.MoneyReceipt.ToString());
            content = content.Replace("{$MoneyNeedPay}", Convert.ToString((decimal) (this.m_OrderInfo.MoneyTotal - this.m_OrderInfo.MoneyReceipt)));
            content = content.Replace("{$InputTime}", this.m_OrderInfo.InputTime.ToString());
            content = content.Replace("{$UserName}", this.m_OrderInfo.UserName);
            content = content.Replace("{$Address}", this.m_OrderInfo.Address);
            content = content.Replace("{$ZipCode}", this.m_OrderInfo.ZipCode);
            content = content.Replace("{$Mobile}", this.m_OrderInfo.Mobile);
            content = content.Replace("{$Phone}", this.m_OrderInfo.Phone);
            content = content.Replace("{$Email}", this.m_OrderInfo.Email);
            content = content.Replace("{$Charge_Deliver}", this.m_OrderInfo.ChargeDeliver.ToString());
            content = content.Replace("{$PresentMoney}", this.m_OrderInfo.PresentMoney.ToString());
            content = content.Replace("{$PresentExp}", this.m_OrderInfo.PresentExp.ToString());
            content = content.Replace("{$PresentPoint}", this.m_OrderInfo.PresentPoint.ToString());
            content = content.Replace("{$Discount_Payment}", this.m_OrderInfo.DiscountPayment.ToString());
            content = content.Replace("{$PaymentType}", GetPaymentType(this.m_OrderInfo.PaymentType));
            content = content.Replace("{$DeliverType}", GetDeliverType(this.m_OrderInfo.DeliverType));
            content = content.Replace("{$OrderStatus}", PEContext.EnumToHtml<OrderStatus>(this.m_OrderInfo.Status));
            content = content.Replace("{$PayStatus}", GetPayStatus(this.m_OrderInfo.MoneyTotal, this.m_OrderInfo.MoneyReceipt));
            content = content.Replace("{$DeliverStatus}", PEContext.EnumToHtml<DeliverStatus>(this.m_OrderInfo.DeliverStatus));
            content = content.Replace("{$OrderInfo}", this.OrderDetail);
            return content;
        }

        public string OrderDetail
        {
            get
            {
                if (string.IsNullOrEmpty(this.m_OrderDetail))
                {
                    this.GetOrderDetail();
                }
                return this.m_OrderDetail;
            }
            set
            {
                this.m_OrderDetail = value;
            }
        }

        public EasyOne.Model.Shop.OrderInfo OrderInfo
        {
            get
            {
                return this.m_OrderInfo;
            }
            set
            {
                this.m_OrderInfo = value;
            }
        }
    }
}

