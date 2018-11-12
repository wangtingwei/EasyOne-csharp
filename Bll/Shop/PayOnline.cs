namespace EasyOne.Shop
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Enumerations;
    using EasyOne.Model.Accessories;
    using EasyOne.Model.Shop;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Web;

    public class PayOnline
    {
        private int m_ClientID;
        private string m_Message;
        private decimal m_MoneyPayout;
        private decimal m_MoneyReceipt;
        private OrderInfo m_OrderInfo;
        private int m_PaymentLogId;
        private int m_PlatformId;
        private int m_Point;
        private int m_UserId;
        private string m_UserName;

        public static PayOnlineState AddPayment(int orderId, int payPlatformId, decimal money, string userName, string paymentNum)
        {
            if (orderId > 0)
            {
                OrderInfo orderById = Order.GetOrderById(orderId);
                if (orderById.IsNull)
                {
                    return PayOnlineState.OrderNotFound;
                }
                if (orderById.MoneyTotal <= orderById.MoneyReceipt)
                {
                    return PayOnlineState.AccountPaid;
                }
            }
            PayPlatformInfo payPlatformById = PayPlatform.GetPayPlatformById(payPlatformId);
            if (payPlatformById.IsNull)
            {
                return PayOnlineState.PayPlatFormNotFound;
            }
            if (payPlatformById.IsDisabled)
            {
                return PayOnlineState.PayPlatFormDisabled;
            }
            money = Math.Abs(money);
            if (money <= 0.01M)
            {
                return PayOnlineState.TooLittleMoney;
            }
            if (string.IsNullOrEmpty(paymentNum))
            {
                return PayOnlineState.NoPaymentNumber;
            }
            decimal num = money + ((money * DataConverter.CDecimal(payPlatformById.Rate)) / 100M);
            PaymentLogInfo paymentLogInfo = new PaymentLogInfo();
            paymentLogInfo.UserName = userName;
            paymentLogInfo.OrderId = orderId;
            paymentLogInfo.PaymentNum = paymentNum;
            paymentLogInfo.PlatformId = payPlatformById.PayPlatformId;
            paymentLogInfo.MoneyPay = money;
            paymentLogInfo.MoneyTrue = num;
            paymentLogInfo.PayTime = new DateTime?(DateTime.Now);
            paymentLogInfo.Status = 1;
            paymentLogInfo.PlatformInfo = "";
            paymentLogInfo.Remark = "";
            paymentLogInfo.SuccessTime = null;
            if (PaymentLog.Add(paymentLogInfo))
            {
                return PayOnlineState.Ok;
            }
            return PayOnlineState.Fail;
        }

        public static string AppendParam(string returnStr, string paramId, string paramValue)
        {
            if (!string.IsNullOrEmpty(returnStr))
            {
                if (!string.IsNullOrEmpty(paramValue))
                {
                    string str = returnStr;
                    returnStr = str + "&" + paramId + "=" + paramValue;
                }
                return returnStr;
            }
            if (!string.IsNullOrEmpty(paramValue))
            {
                returnStr = paramId + "=" + paramValue;
            }
            return returnStr;
        }

        public static string GetApplicationName()
        {
            return GetApplicationName(HttpContext.Current);
        }

        public static string GetApplicationName(HttpContext context)
        {
            if (context == null)
            {
                return string.Empty;
            }
            string host = context.Request.Url.Host;
            string applicationPath = context.Request.ApplicationPath;
            return (host + applicationPath);
        }

        public static string GetStateDescription(PayOnlineState state)
        {
            string str = string.Empty;
            switch (state)
            {
                case PayOnlineState.None:
                    return "";

                case PayOnlineState.Ok:
                    return "在线支付成功！";

                case PayOnlineState.OrderNotFound:
                    return "找不到指定的订单！";

                case PayOnlineState.AccountPaid:
                    return "指定的订单已经付清，不用再付款！";

                case PayOnlineState.NoMoney:
                    return "请输入划款金额！";

                case PayOnlineState.TooLittleMoney:
                    return "每次划款金额不能低于0.01元！";

                case PayOnlineState.PayPlatFormNotFound:
                    return "找不到指定的支付平台！";

                case PayOnlineState.PayPlatFormDisabled:
                    return "指定的支付平台未启动！";

                case PayOnlineState.NoPaymentNumber:
                    return "没有支付编号！";

                case PayOnlineState.PaymentLogNotFound:
                    return "找不到指定的支付单！";

                case PayOnlineState.RemittanceWrong:
                    return "支付金额不对！";

                case PayOnlineState.Fail:
                    return "在线支付失败！";
            }
            return str;
        }

        public static bool IsFabrication(int orderId)
        {
            bool flag = false;
            if (orderId == 0)
            {
                return true;
            }
            foreach (OrderItemInfo info in OrderItem.GetInfoListByOrderId(orderId))
            {
                ProductInfo productById = Product.GetProductById(info.ProductId, info.TableName);
                if (!productById.IsNull)
                {
                    if (Product.CharacterIsExists(productById.ProductCharacter, ProductCharacter.Card))
                    {
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return flag;
        }

        private static void Log(string logTitle, string logMessage, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine("{0} {1} {2}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString(), logTitle);
            w.WriteLine("{0}", logMessage);
            w.WriteLine("-------------------------------");
            w.Flush();
        }

        private static bool ProcessCards(StringBuilder payOnlineMessage, bool doUpdate, OrderItemInfo itemInfo, ref bool isFirstStockItem, ref int stockId)
        {
            bool flag = false;
            IList<CardInfo> list = Cards.GetCardList(itemInfo.TableName, itemInfo.ProductId, itemInfo.ItemId);
            if (list.Count == 0)
            {
                if (doUpdate)
                {
                    IList<CardInfo> list2 = Cards.GetUnsoldCard(itemInfo.TableName, itemInfo.ProductId, itemInfo.Amount);
                    string str = string.Empty;
                    if (list2.Count >= itemInfo.Amount)
                    {
                        foreach (CardInfo info in list2)
                        {
                            string str2 = str;
                            str = str2 + "<br>卡号：" + info.CardNum + "&nbsp;&nbsp;&nbsp;&nbsp;密码：" + StringHelper.Base64StringDecode(info.Password);
                            info.OrderItemId = itemInfo.ItemId;
                            Cards.Update(info);
                        }
                        if (isFirstStockItem)
                        {
                            stockId = StockManage.GetMaxId() + 1;
                            isFirstStockItem = false;
                        }
                        Order.AddStockItemBySendCard(stockId, itemInfo);
                    }
                    else
                    {
                        flag = true;
                    }
                    payOnlineMessage.Append("<br /><br />您购买的充值卡的信息如下，请您尽快使用，以防充值卡被他人使用！<br />");
                    payOnlineMessage.Append(str);
                    payOnlineMessage.Append("<br><br><a href='../User/Info/Recharge.aspx'>使用充值卡充值</a>&nbsp;&nbsp;&nbsp;&nbsp;");
                }
                return flag;
            }
            payOnlineMessage.Append("<br /><br />您购买的充值卡的信息如下，请您尽快使用，以防充值卡被他人使用！<br />");
            foreach (CardInfo info2 in list)
            {
                payOnlineMessage.Append("<br />卡号：" + info2.CardNum + "&nbsp;&nbsp;&nbsp;&nbsp;密码：" + StringHelper.Base64StringDecode(info2.Password));
                payOnlineMessage.Append("<br /><br /><a href='../User/Info/Recharge.aspx'>使用充值卡充值</a>&nbsp;&nbsp;&nbsp;&nbsp;");
            }
            return flag;
        }

        private bool ShowCardInfo(StringBuilder payOnlineMessage, bool doUpdate, bool isok)
        {
            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            bool flag4 = false;
            bool flag5 = false;
            IList<OrderItemInfo> infoListByOrderId = OrderItem.GetInfoListByOrderId(this.m_OrderInfo.OrderId);
            int stockId = 0;
            bool isFirstStockItem = true;
            foreach (OrderItemInfo info in infoListByOrderId)
            {
                if (Product.CharacterIsExists(info.ProductCharacter, ProductCharacter.Card))
                {
                    flag5 = ProcessCards(payOnlineMessage, doUpdate, info, ref isFirstStockItem, ref stockId);
                    flag4 = true;
                }
                if (Product.CharacterIsExists(info.ProductCharacter, ProductCharacter.Practicality))
                {
                    flag = true;
                }
                if (Product.CharacterIsExists(info.ProductCharacter, ProductCharacter.Service))
                {
                    flag2 = true;
                }
                if (Product.CharacterIsExists(info.ProductCharacter, ProductCharacter.Download))
                {
                    flag3 = true;
                }
            }
            if (!isFirstStockItem)
            {
                StockInfo stockInfo = new StockInfo();
                stockInfo.Inputer = PEContext.Current.Admin.AdminName;
                stockInfo.InputTime = DateTime.Now;
                stockInfo.Remark = "交付点卡";
                stockInfo.StockId = StockManage.GetMaxId() + 1;
                stockInfo.StockNum = StockItem.GetShipmentNum();
                stockInfo.StockType = StockType.Shipment;
                StockManage.Add(stockInfo);
            }
            if (doUpdate && !flag)
            {
                if ((flag3 && !flag4) && !flag2)
                {
                    Order.Recieve(this.m_OrderInfo.OrderId);
                    return isok;
                }
                if (!flag5)
                {
                    this.m_OrderInfo.DeliverStatus = DeliverStatus.Consignment;
                    isok = Order.Update(this.m_OrderInfo);
                }
            }
            return isok;
        }

        public static void TestLog(bool isLog, string title, string payState, string signMsg, string md5Msg)
        {
            if (isLog)
            {
                string str = HttpContext.Current.Request.QueryString.ToString();
                string str2 = HttpContext.Current.Request.Form.ToString();
                string path = HttpContext.Current.Request.PhysicalApplicationPath + @"PayOnline\log.txt";
                StringBuilder builder = new StringBuilder();
                builder.Append("payState=");
                builder.Append(payState);
                builder.Append("\r\n");
                builder.Append("signMsg=");
                builder.Append(signMsg);
                builder.Append("\r\n");
                builder.Append("md5Msg =");
                builder.Append(md5Msg);
                builder.Append("\r\n");
                if (signMsg == md5Msg)
                {
                    builder.Append("加密签名串验证通过");
                    builder.Append("\r\n");
                }
                else
                {
                    builder.Append("加密签名串验证失败");
                    builder.Append("\r\n");
                }
                builder.Append("QueryString=");
                builder.Append(str);
                builder.Append("\r\n");
                builder.Append("FormString=");
                builder.Append(str2);
                try
                {
                    StreamWriter w = File.AppendText(path);
                    Log(title, builder.ToString(), w);
                    w.Close();
                }
                catch (UnauthorizedAccessException)
                {
                }
            }
        }

        public PayOnlineState UpdateOrder(string paymentNum, decimal amount, string eBankInfo, int status, string remark, bool updateDeliverStatus, bool updateOrderStatus)
        {
            if (Convert.ToString(PEContext.Current.Context.Session["PaymentNum"]) != paymentNum)
            {
                PEContext.Current.Context.Session["PaymentNum"] = paymentNum;
                StringBuilder payOnlineMessage = new StringBuilder(0x40);
                paymentNum = DataSecurity.FilterBadChar(paymentNum);
                eBankInfo = DataSecurity.FilterBadChar(eBankInfo);
                remark = DataSecurity.FilterBadChar(remark);
                PayOnlineState state = this.UpdatePaymentLog(paymentNum, amount, eBankInfo, status, remark);
                if (state != PayOnlineState.Ok)
                {
                    PEContext.Current.Context.Session["PaymentNum"] = "";
                    return state;
                }
                if (!updateDeliverStatus)
                {
                    PEContext.Current.Context.Session["PaymentNum"] = "";
                    return PayOnlineState.Ok;
                }
                bool doUpdate = !BankrollItem.ExistsPaymentLog(this.m_PaymentLogId);
                if (!doUpdate)
                {
                    PEContext.Current.Context.Session["PaymentNum"] = "";
                    return PayOnlineState.AccountPaid;
                }
                bool isok = false;
                IEncourageStrategy<decimal> strategy = new UserMoney();
                if (updateOrderStatus)
                {
                    if (this.m_UserId > 0)
                    {
                        strategy.IncreaseForUsers(this.m_UserId.ToString(), this.m_MoneyReceipt, "", false, "");
                    }
                    BankrollItemInfo bankrollItemInfo = new BankrollItemInfo();
                    bankrollItemInfo.UserName = this.m_UserName;
                    bankrollItemInfo.ClientId = this.m_ClientID;
                    bankrollItemInfo.Money = this.m_MoneyReceipt;
                    bankrollItemInfo.MoneyType = 3;
                    bankrollItemInfo.EBankId = this.m_PlatformId;
                    bankrollItemInfo.OrderId = this.m_OrderInfo.OrderId;
                    bankrollItemInfo.PaymentId = this.m_PaymentLogId;
                    bankrollItemInfo.Remark = "在线支付单号：" + paymentNum;
                    bankrollItemInfo.DateAndTime = new DateTime?(DateTime.Now);
                    bankrollItemInfo.CurrencyType = 1;
                    isok = BankrollItem.Add(bankrollItemInfo);
                }
                if (this.m_OrderInfo.IsNull)
                {
                    if (this.m_Point > 0)
                    {
                        IEncourageStrategy<int> strategy2 = new UserPoint();
                        strategy2.IncreaseForUsers(this.m_UserId.ToString(), this.m_Point, "购买" + SiteConfig.UserConfig.PointName, true, "");
                        BankrollItemInfo info2 = new BankrollItemInfo();
                        info2.UserName = this.m_UserName;
                        info2.ClientId = this.m_ClientID;
                        info2.Money = (this.m_MoneyReceipt > 0M) ? (-1M * this.m_MoneyReceipt) : this.m_MoneyReceipt;
                        info2.MoneyType = 4;
                        info2.EBankId = 0;
                        info2.OrderId = this.m_OrderInfo.OrderId;
                        info2.PaymentId = 0;
                        info2.Remark = "购买" + SiteConfig.UserConfig.PointName + "，购买数：" + this.m_Point.ToString() + SiteConfig.UserConfig.PointUnit;
                        info2.DateAndTime = new DateTime?(DateTime.Now);
                        info2.CurrencyType = 1;
                        isok = BankrollItem.Add(info2);
                        if (this.m_UserId > 0)
                        {
                            isok = strategy.IncreaseForUsers(this.m_UserId.ToString(), -(this.m_MoneyReceipt), "", false, "");//将decimal.op_UnaryNegation 
                        }
                    }
                    PEContext.Current.Context.Session["PaymentNum"] = "";
                    if (!isok)
                    {
                        return PayOnlineState.Fail;
                    }
                    return PayOnlineState.Ok;
                }
                if (updateDeliverStatus && ((this.m_OrderInfo.MoneyTotal - this.m_OrderInfo.MoneyReceipt) <= this.m_MoneyReceipt))
                {
                    this.m_OrderInfo.EnableDownload = true;
                    isok = Order.Update(this.m_OrderInfo);
                }
                if ((this.m_OrderInfo.MoneyReceipt < this.m_OrderInfo.MoneyTotal) && updateOrderStatus)
                {
                    bool flag3 = false;
                    if ((this.m_OrderInfo.MoneyTotal - this.m_OrderInfo.MoneyReceipt) <= this.m_MoneyReceipt)
                    {
                        this.m_MoneyPayout = this.m_OrderInfo.MoneyTotal - this.m_OrderInfo.MoneyReceipt;
                        this.m_OrderInfo.MoneyReceipt = this.m_OrderInfo.MoneyTotal;
                        flag3 = true;
                    }
                    else if (SiteConfig.ShopConfig.EnablePartPay)
                    {
                        this.m_MoneyPayout = this.m_MoneyReceipt;
                        this.m_OrderInfo.MoneyReceipt += this.m_MoneyReceipt;
                        flag3 = true;
                    }
                    if (flag3)
                    {
                        if (this.m_OrderInfo.Status <= OrderStatus.WaitForConfirm)
                        {
                            this.m_OrderInfo.Status = OrderStatus.Confirmed;
                        }
                        isok = Order.Update(this.m_OrderInfo);
                        BankrollItemInfo info3 = new BankrollItemInfo();
                        info3.UserName = this.m_UserName;
                        info3.ClientId = this.m_ClientID;
                        info3.Money = (this.m_MoneyPayout > 0M) ? (-1M * this.m_MoneyPayout) : this.m_MoneyPayout;
                        info3.MoneyType = 4;
                        info3.EBankId = 0;
                        info3.OrderId = this.m_OrderInfo.OrderId;
                        info3.PaymentId = 0;
                        info3.Remark = "支付订单费用，订单号：" + this.m_OrderInfo.OrderNum;
                        info3.DateAndTime = new DateTime?(DateTime.Now);
                        info3.CurrencyType = 1;
                        isok = BankrollItem.Add(info3);
                        if (this.m_UserId > 0)
                        {
                            isok = strategy.IncreaseForUsers(this.m_UserId.ToString(), -(this.m_MoneyPayout), "", false, "");//将decimal.op_UnaryNegation改为了-
                        }
                        payOnlineMessage.Append("同时已经为您的订单编号为 " + this.m_OrderInfo.OrderNum + " 的订单支付了 " + this.m_MoneyPayout.ToString("N2") + "元。<br />");
                    }
                    else
                    {
                        payOnlineMessage.Append("您的支付金额小于订单金额，不能对订单进行支付，资金已经打入您的帐户中做为预付款。<br />");
                        updateDeliverStatus = false;
                    }
                }
                if (updateDeliverStatus)
                {
                    this.ShowCardInfo(payOnlineMessage, doUpdate, isok);
                }
                payOnlineMessage.Append("<a href='../User/Shop/ShowOrder.aspx?OrderId=" + this.m_OrderInfo.OrderId.ToString() + "'>点此查看订单信息</a>");
                this.m_Message = payOnlineMessage.ToString();
                PEContext.Current.Context.Session["PaymentNum"] = "";
                if (!isok)
                {
                    return PayOnlineState.Fail;
                }
            }
            return PayOnlineState.Ok;
        }

        private PayOnlineState UpdatePaymentLog(string paymentNum, decimal amount, string eBankInfo, int status, string remark)
        {
            PaymentLogInfo infoByPaymentNum = PaymentLog.GetInfoByPaymentNum(paymentNum);
            PayOnlineState ok = PayOnlineState.Ok;
            if (infoByPaymentNum.IsNull)
            {
                return PayOnlineState.PaymentLogNotFound;
            }
            if (infoByPaymentNum.MoneyTrue != amount)
            {
                return PayOnlineState.RemittanceWrong;
            }
            this.m_OrderInfo = Order.GetOrderById(infoByPaymentNum.OrderId);
            this.m_Point = infoByPaymentNum.Point;
            this.m_PlatformId = infoByPaymentNum.PlatformId;
            this.m_PaymentLogId = infoByPaymentNum.PaymentLogId;
            this.m_UserName = infoByPaymentNum.UserName;
            this.m_MoneyReceipt = infoByPaymentNum.MoneyPay;
            infoByPaymentNum.PlatformInfo = eBankInfo;
            infoByPaymentNum.Remark = remark;
            infoByPaymentNum.Status = status;
            PaymentLog.Update(infoByPaymentNum);
            UserInfo usersByUserName = Users.GetUsersByUserName(this.m_UserName);
            if (!usersByUserName.IsNull)
            {
                this.m_UserId = usersByUserName.UserId;
                this.m_ClientID = usersByUserName.ClientId;
            }
            return ok;
        }

        public string Message
        {
            get
            {
                return this.m_Message;
            }
            set
            {
                this.m_Message = value;
            }
        }
    }
}

