namespace EasyOne.Shop
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Crm;
    using EasyOne.Enumerations;
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Accessories;
    using EasyOne.Model.Crm;
    using EasyOne.Model.Shop;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Text;
    using EasyOne.DalFactory;

    public sealed class Order
    {
        private static readonly IOrder dal = DataAccess.CreateOrder();
        private static string s_MessgeOrMailContentOfCard;

        private Order()
        {
        }

        public static bool Add(OrderInfo orderInfo)
        {
            if (SiteConfig.ConfigInfo().ShopConfig.IsSetFunctionary)
            {
                string str = string.Empty;
                foreach (AdministratorInfo info in Administrators.GetAdminListByOperateCode(0, Administrators.GetTotalOfAdmin(), 0x614719c))
                {
                    str = str + info.AdminName + ",";
                }
                orderInfo.Functionary = AllotFunctionary(str.TrimEnd(new char[] { ',' }));
            }
            return dal.Add(orderInfo);
        }

        public static bool Add(OrderInfo orderInfo, OrderFlowInfo orderFlowInfo, bool processUserInfo)
        {
            if (processUserInfo && !string.IsNullOrEmpty(orderInfo.UserName))
            {
                UserInfo usersByUserName = Users.GetUsersByUserName(orderInfo.UserName);
                orderFlowInfo.ConsigneeName = orderInfo.ContacterName;
                orderFlowInfo.ZipCode = orderInfo.ZipCode;
                orderFlowInfo.Email = orderInfo.Email;
                orderFlowInfo.HomePhone = orderInfo.Phone;
                orderFlowInfo.Mobile = orderInfo.Mobile;
                ProcessSubscriber(orderFlowInfo, usersByUserName);
                orderInfo.ClientId = usersByUserName.ClientId;
            }
            return Add(orderInfo);
        }

        public static int Add(OrderFlowInfo orderFlowInfo, UserInfo userInfo, string cartId, int couponId, decimal trueTotalMoney)
        {
            decimal num5;
            int num6;
            int num7;
            if (string.IsNullOrEmpty(cartId))
            {
                return 1;
            }
            IList<ShoppingCartInfo> infoByCart = ShoppingCart.GetInfoByCart(cartId, false);
            if (infoByCart.Count <= 0)
            {
                return 2;
            }
            IList<ShoppingCartInfo> shoppingCartPresentInfoList = ShoppingCart.GetInfoByCart(cartId, true);
            OrderInfo orderInfo = new OrderInfo();
            double totalWeight = 0.0;
            decimal totalMoney = 0M;
            double discount = PaymentType.GetPaymentTypeById(orderFlowInfo.PaymentType).Discount;
            string userName = "";
            int clientId = 0;
            if (!userInfo.IsNull)
            {
                ProcessSubscriber(orderFlowInfo, userInfo);
                userName = userInfo.UserName;
                clientId = userInfo.ClientId;
            }
            AddOrder(orderFlowInfo, orderInfo, discount, userName, clientId);
            AddOrderItems(orderFlowInfo, infoByCart, shoppingCartPresentInfoList, orderInfo, ref totalWeight, ref totalMoney, userInfo);
            CheckPresentProject(orderFlowInfo, orderInfo, ref totalWeight, ref totalMoney, out num5, out num6, out num7);
            decimal num8 = totalMoney;
            PackageInfo packageByGoodsWeight = Package.GetPackageByGoodsWeight(totalWeight);
            if (!packageByGoodsWeight.IsNull)
            {
                totalWeight += packageByGoodsWeight.PackageWeight;
            }
            decimal num9 = DeliverCharge.GetDeliverCharge(orderFlowInfo.DeliverType, totalWeight, orderFlowInfo.ZipCode, totalMoney, orderFlowInfo.NeedInvoice);
            totalMoney += num9;
            orderInfo.MoneyGoods = num8;
            orderInfo.ChargeDeliver = num9;
            if (couponId > 0)
            {
                orderInfo.MoneyTotal = trueTotalMoney;
                orderInfo.CouponId = couponId;
            }
            else
            {
                orderInfo.MoneyTotal = totalMoney;
            }
            orderInfo.PresentMoney = num5;
            orderInfo.PresentExp = num6;
            orderInfo.PresentPoint = num7;
            Update(orderInfo);
            ShoppingCart.Delete(cartId);
            SmsConfig smsConfig = SiteConfig.SmsConfig;
            if ((smsConfig.IsAutoSendMessage && !string.IsNullOrEmpty(smsConfig.AdminPhoneNumber)) && !string.IsNullOrEmpty(smsConfig.OrderMessage))
            {
                AbstractMessageOfOrder order = new SmsOfOrder("", SendType.SendToAdmin, smsConfig.AdminPhoneNumber);
                order.Implementor = new OrderFlow(orderInfo, "");
                order.Send();
            }
            return 0x63;
        }

        private static void AddOrder(OrderFlowInfo orderFlowInfo, OrderInfo orderInfo, double paymentDiscount, string userName, int clientId)
        {
            if (orderFlowInfo.OrderId == 0)
            {
                orderInfo.OrderId = GetMaxId() + 1;
            }
            else
            {
                orderInfo.OrderId = orderFlowInfo.OrderId;
            }
            orderInfo.OrderNum = GetOrderNum();
            orderInfo.UserName = userName;
            orderInfo.ClientId = clientId;
            orderInfo.ContacterName = orderFlowInfo.ConsigneeName;
            if (orderFlowInfo.Country != "中华人民共和国")
            {
                orderInfo.Address = orderFlowInfo.Country;
            }
            string address = orderInfo.Address;
            orderInfo.Address = address + orderFlowInfo.Province + orderFlowInfo.City + orderFlowInfo.Area + orderFlowInfo.Address;
            orderInfo.ZipCode = orderFlowInfo.ZipCode;
            orderInfo.Mobile = orderFlowInfo.Mobile;
            orderInfo.Phone = orderFlowInfo.HomePhone;
            orderInfo.Email = orderFlowInfo.Email;
            orderInfo.PaymentType = orderFlowInfo.PaymentType;
            orderInfo.DeliverType = orderFlowInfo.DeliverType;
            orderInfo.NeedInvoice = orderFlowInfo.NeedInvoice;
            orderInfo.InvoiceContent = orderFlowInfo.InvoiceContent;
            orderInfo.Invoiced = false;
            orderInfo.Remark = orderFlowInfo.Remark;
            orderInfo.BeginDate = orderFlowInfo.BeginDate;
            orderInfo.InputTime = DateTime.Now;
            orderInfo.Status = OrderStatus.WaitForConfirm;
            orderInfo.DeliverStatus = DeliverStatus.Preparative;
            orderInfo.EnableDownload = false;
            orderInfo.DiscountPayment = paymentDiscount;
            orderInfo.ChargeDeliver = 0M;
            orderInfo.AgentName = orderFlowInfo.AgentName;
            orderInfo.OutOfStockProject = orderFlowInfo.OutOfStockProject;
            orderInfo.DeliveryTime = orderFlowInfo.DeliveryTime;
            Add(orderInfo);
        }

        private static void AddOrderItems(OrderFlowInfo orderFlowInfo, IList<ShoppingCartInfo> shoppingCartProductInfoList, IList<ShoppingCartInfo> shoppingCartPresentInfoList, OrderInfo orderInfo, ref double totalWeight, ref decimal totalMoney, UserInfo userInfo)
        {
            bool haveWholesalePurview = false;
            if (!userInfo.UserPurview.IsNull)
            {
                haveWholesalePurview = userInfo.UserPurview.Enablepm;
            }
            foreach (ShoppingCartInfo info in shoppingCartProductInfoList)
            {
                ProductInfo productById = Product.GetProductById(info.ProductId, info.TableName);
                if (!productById.IsNull)
                {
                    AbstractItemInfo info3 = new ConcreteProductInfo(info.Quantity, info.Property, productById, userInfo, orderFlowInfo.NeedInvoice, true, haveWholesalePurview);
                    info3.GetItemInfo();
                    OrderItem.Add(info3.GetOrderItemInfo(orderInfo.OrderId));
                    totalMoney += info3.SubTotal;
                    totalWeight += info3.TotalWeight;
                    AddPressent(info.Quantity, shoppingCartPresentInfoList, ref totalWeight, ref totalMoney, productById, orderInfo.OrderId);
                }
            }
        }

        private static void AddPressent(int amount, IList<ShoppingCartInfo> shoppingCartPresentInfoList, ref double totalWeight, ref decimal totalMoney, ProductInfo productInfo, int orderId)
        {
            if ((productInfo.SalePromotionType > 0) && (amount >= productInfo.MinNumber))
            {
                AbstractItemInfo info = new ConcreteSalePromotionType(amount, productInfo, true, shoppingCartPresentInfoList);
                info.GetItemInfo();
                if (!info.IsNull)
                {
                    totalWeight += info.TotalWeight;
                    totalMoney += info.SubTotal;
                    if (OrderItem.Add(info.GetOrderItemInfo(orderId)))
                    {
                        Product.AddOrderNum(info.ProductId, info.TableName, info.Amount);
                    }
                }
            }
        }

        public static void AddStockItemBySendCard(int stockId, OrderItemInfo orderItemInfo)
        {
            string productNum = string.Empty;
            if (!string.IsNullOrEmpty(orderItemInfo.TableName))
            {
                ProductInfo productById = Product.GetProductById(orderItemInfo.ProductId);
                productById.Stocks -= orderItemInfo.Amount;
                productById.OrderNum -= orderItemInfo.Amount;
                ProductCommon.Update(productById, productById.TableName);
                productNum = productById.ProductNum;
            }
            else
            {
                PresentInfo presentById = Present.GetPresentById(orderItemInfo.ProductId);
                presentById.Stocks -= orderItemInfo.Amount;
                presentById.OrderNum -= orderItemInfo.Amount;
                Present.UpdatePressent(presentById);
                productNum = presentById.PresentNum;
            }
            StockItemInfo info3 = new StockItemInfo();
            info3.Amount = orderItemInfo.Amount;
            info3.Price = orderItemInfo.TruePrice;
            info3.ProductId = orderItemInfo.ProductId;
            info3.TableName = orderItemInfo.TableName;
            info3.ProductName = orderItemInfo.ProductName;
            info3.ProductNum = productNum;
            info3.Property = orderItemInfo.Property;
            info3.StockId = stockId;
            info3.Unit = orderItemInfo.Unit;
            StockItem.Add(info3, stockId);
        }

        private static string AllotFunctionary(string shopConfigFunctionary)
        {
            string[] strArray = shopConfigFunctionary.Split(new char[] { ',' });
            if (strArray.Length == 0)
            {
                return string.Empty;
            }
            string str = string.Empty;
            string lastFunctionary = GetLastFunctionary();
            bool flag = false;
            foreach (string str3 in strArray)
            {
                if (flag)
                {
                    str = str3;
                    break;
                }
                if (str3 == lastFunctionary)
                {
                    flag = true;
                }
            }
            if (string.IsNullOrEmpty(str))
            {
                str = strArray[0];
            }
            return str;
        }

        public static bool CancelConfirm(int orderId)
        {
            return dal.CancelConfirm(orderId);
        }

        private static void CheckPresentProject(OrderFlowInfo orderFlowInfo, OrderInfo orderInfo, ref double totalWeight, ref decimal totalMoney, out decimal presentMoney, out int presentExp, out int presentPoint)
        {
            presentMoney = 0M;
            presentExp = 0;
            presentPoint = 0;
            PresentProjectInfo presentProjectByTotalMoney = PresentProject.GetPresentProjectByTotalMoney(totalMoney);
            if (!presentProjectByTotalMoney.IsNull)
            {
                if ((presentProjectByTotalMoney.PresentContent.Contains("1") && (orderFlowInfo.PresentId > 0)) && !Present.GetPresentById(orderFlowInfo.PresentId).IsNull)
                {
                    AbstractItemInfo info3 = new ConcretePresentProject(orderFlowInfo.PresentId, presentProjectByTotalMoney);
                    info3.GetItemInfo();
                    if (!info3.IsNull)
                    {
                        totalMoney += info3.SubTotal;
                        totalWeight += info3.TotalWeight;
                        OrderItem.Add(info3.GetOrderItemInfo(orderInfo.OrderId));
                        Product.AddOrderNum(info3.ProductId, info3.TableName, 1);
                    }
                }
                if (presentProjectByTotalMoney.PresentContent.Contains("2"))
                {
                    presentMoney = presentProjectByTotalMoney.Cash;
                }
                if (presentProjectByTotalMoney.PresentContent.Contains("3"))
                {
                    presentExp = presentProjectByTotalMoney.PresentExp;
                }
                if (presentProjectByTotalMoney.PresentContent.Contains("4"))
                {
                    presentPoint = presentProjectByTotalMoney.PresentPoint;
                }
            }
        }

        public static int Confirm(int orderId)
        {
            return dal.Confirm(orderId);
        }

        public static int CountBuyNum(string userName, int productId)
        {
            return dal.CountBuyNum(userName, productId);
        }

        public static int CountByNoConsignment()
        {
            return dal.CountByNoConsignment();
        }

        public static int CountByOrderStatus(OrderStatus status)
        {
            return dal.CountByOrderStatus(status);
        }

        public static string Delete(string orderId)
        {
            return dal.Delete(DataSecurity.FilterBadChar(orderId));
        }

        public static bool DoDownload(int orderId, bool enableDownload)
        {
            bool flag = dal.DoDownload(orderId, enableDownload);
            if (enableDownload && flag)
            {
                bool flag2 = false;
                bool flag3 = false;
                bool flag4 = false;
                foreach (OrderItemInfo info in OrderItem.GetInfoListByOrderId(orderId))
                {
                    if (Product.CharacterIsExists(info.ProductCharacter, ProductCharacter.Practicality))
                    {
                        flag2 = true;
                        break;
                    }
                    if (Product.CharacterIsExists(info.ProductCharacter, ProductCharacter.Card))
                    {
                        flag3 = true;
                        break;
                    }
                    if (Product.CharacterIsExists(info.ProductCharacter, ProductCharacter.Service))
                    {
                        flag4 = true;
                        break;
                    }
                }
                if ((!flag2 && !flag3) && !flag4)
                {
                    Recieve(orderId);
                }
            }
            return flag;
        }

        public static int EndOrder(int orderId)
        {
            int num = 0;
            int totalPresentExp = 0;
            int totalPresentMoney = 0;
            int totalPresentPoint = 0;
            OrderInfo orderById = GetOrderById(orderId);
            if (orderById.Status <= OrderStatus.WaitForConfirm)
            {
                num = 1;
            }
            if (orderById.MoneyReceipt < orderById.MoneyTotal)
            {
                num = 2;
            }
            if (orderById.DeliverStatus <= DeliverStatus.Preparative)
            {
                num = 3;
            }
            if (num == 0)
            {
                orderById.Status = OrderStatus.End;
                Update(orderById);
                foreach (OrderItemInfo info2 in OrderItem.GetInfoListByOrderId(orderId))
                {
                    if (info2.PresentExp > 0)
                    {
                        totalPresentExp += info2.PresentExp * info2.Amount;
                    }
                    if (info2.PresentPoint > 0)
                    {
                        totalPresentPoint += info2.PresentPoint * info2.Amount;
                    }
                    if (info2.PresentMoney > 0M)
                    {
                        totalPresentMoney += Convert.ToInt32(info2.PresentMoney) * info2.Amount;
                    }
                }
                UpdateUserInfo(orderById, totalPresentExp, totalPresentPoint, totalPresentMoney);
                Coupon.Create(orderById);
            }
            return num;
        }

        public static OrderInfo GetAnonymousOrderInfo(string orderNo, string contactName)
        {
            return dal.GetAnonymousOrderInfo(orderNo, contactName);
        }

        public static IList<UserOrderCommonInfo> GetCardList(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return new List<UserOrderCommonInfo>();
            }
            return dal.GetCardList(userName);
        }

        public static IList<UserOrderCommonInfo> GetDownList(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return new List<UserOrderCommonInfo>();
            }
            return dal.GetDownList(userName);
        }

        public static UserOrderCommonInfo GetDownloadInfo(string userName, int orderItemId)
        {
            UserOrderCommonInfo downloadInfo = dal.GetDownloadInfo(userName, orderItemId);
            if (string.IsNullOrEmpty(downloadInfo.TableName))
            {
                PresentInfo presentById = Present.GetPresentById(downloadInfo.ProductId);
                downloadInfo.DownloadUrl = presentById.DownloadUrl;
                downloadInfo.Remark = presentById.Remark;
                return downloadInfo;
            }
            ProductInfo productById = Product.GetProductById(downloadInfo.ProductId);
            downloadInfo.DownloadUrl = productById.DownloadUrl;
            downloadInfo.Remark = productById.Remark;
            return downloadInfo;
        }

        public static string GetLastFunctionary()
        {
            return dal.GetLastFunctionary();
        }

        public static IList<OrderInfo> GetList(int startRowIndexId, int maxNumberRows, string searchType, string field, string keyword, string action)
        {
            string str27;
            if (!string.IsNullOrEmpty(keyword) && ((str27 = searchType) != null))
            {
                if (!(str27 == "10"))
                {
                    if (str27 == "20")
                    {
                        string[] strArray = keyword.Split(new char[] { '|' });
                        if (strArray.Length == 0x1a)
                        {
                            string str = DataConverter.CLng(strArray[0]).ToString();
                            string str2 = DataConverter.CLng(strArray[1]).ToString();
                            string str3 = string.IsNullOrEmpty(strArray[2]) ? "" : DataConverter.CDate(strArray[2]).ToString("yyyy-MM-dd");
                            string str4 = string.IsNullOrEmpty(strArray[3]) ? "" : DataConverter.CDate(strArray[3]).ToString("yyyy-MM-dd");
                            string str5 = DataConverter.CDecimal(strArray[4]).ToString();
                            string str6 = DataConverter.CDecimal(strArray[5]).ToString();
                            string str7 = DataConverter.CLng(strArray[6]).ToString();
                            string str8 = DataConverter.CLng(strArray[7]).ToString();
                            string str9 = DataConverter.CLng(strArray[8]).ToString();
                            string str10 = DataSecurity.FilterBadChar(strArray[9]);
                            string str11 = DataSecurity.FilterBadChar(strArray[10]);
                            string str12 = DataSecurity.FilterBadChar(strArray[11]);
                            string str13 = DataSecurity.FilterBadChar(strArray[12]);
                            string str14 = DataSecurity.FilterBadChar(strArray[13]);
                            string str15 = DataSecurity.FilterBadChar(strArray[14]);
                            string str16 = DataSecurity.FilterBadChar(strArray[15]);
                            string str17 = DataSecurity.FilterBadChar(strArray[0x10]);
                            string str18 = DataSecurity.FilterBadChar(strArray[0x11]);
                            string str19 = DataSecurity.FilterBadChar(strArray[0x12]);
                            string str20 = DataConverter.CLng(strArray[0x13]).ToString();
                            string str21 = DataSecurity.FilterBadChar(strArray[20]);
                            string str22 = DataSecurity.FilterBadChar(strArray[0x15]);
                            string str23 = DataSecurity.FilterBadChar(strArray[0x16]);
                            string str24 = DataSecurity.FilterBadChar(strArray[0x17]);
                            string str25 = DataConverter.CLng(strArray[0x18]).ToString();
                            string str26 = DataConverter.CLng(strArray[0x19]).ToString();
                            keyword = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}|{13}|{14}|{15}|{16}|{17}|{18}|{19}|{20}|{21}|{22}|{23}|{24}|{25}", new object[] { 
                                str, str2, str3, str4, str5, str6, str7, str8, str9, str10, str11, str12, str13, str14, str15, str16, 
                                str17, str18, str19, str20, str21, str22, str23, str24, str25, str26
                             });
                        }
                        else
                        {
                            searchType = "0";
                        }
                    }
                }
                else
                {
                    string str28;
                    if (((str28 = field) != null) && (str28 == "DateAndTime"))
                    {
                        keyword = DataConverter.CDate(keyword).ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        keyword = DataSecurity.FilterBadChar(keyword);
                    }
                }
            }
            return dal.GetList(startRowIndexId, maxNumberRows, searchType, field, keyword, action);
        }

        public static IDictionary<int, string> GetListByUserName(string userName)
        {
            return dal.GetListByUserName(userName);
        }

        public static int GetMaxId()
        {
            return dal.GetMaxId();
        }

        private static StringBuilder GetMessageOrMailofCard(string content)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<table width='80%' class='border' align='center' cellspacing='1' cellpadding='2'><tr class='title' align='center'><td>商品名称</td><td>充值卡类型</td><td>充值卡卡号</td><td>充值卡密码</td><td>充值卡面值</td><td>充值卡点数</td><td>充值截止日期</td></tr>");
            builder.Append(content);
            builder.Append("</table>");
            return builder;
        }

        private static void GetMessageOrMailofCard(StringBuilder content, CardInfo cartInfo, string productName)
        {
            content.Append("<tr class='tdbg' align='center'><td>" + productName + "</td>");
            content.Append("<td>");
            if (cartInfo.CardType == 0)
            {
                content.Append("本站充值卡");
            }
            else
            {
                content.Append("<font color='blue'>其他公司卡</font>");
            }
            content.Append("</td>");
            content.Append("<td>" + cartInfo.CardNum + "</td>");
            content.Append("<td>" + StringHelper.Base64StringDecode(cartInfo.Password) + "</td>");
            content.Append("<td>" + cartInfo.Money.ToString() + "</td>");
            content.Append(string.Concat(new object[] { "<td>", cartInfo.ValidNum, cartInfo.ValidUnit, "</td>" }));
            content.Append("<td>" + cartInfo.EndDate.ToString() + "</td>");
            content.Append("</tr>");
        }

        public static OrderInfo GetMyOrderById(int orderId, string userName)
        {
            if ((orderId > 0) && !string.IsNullOrEmpty(userName))
            {
                return dal.GetMyOrderById(orderId, userName);
            }
            return new OrderInfo(true);
        }

        public static OrderInfo GetOrderById(int orderId)
        {
            return dal.GetOrderById(orderId);
        }

        public static OrderInfo GetOrderByOrderNum(string orderNum)
        {
            return dal.GetOrderByOrderNum(orderNum);
        }

        public static string GetOrderNum()
        {
            return (SiteConfig.ShopConfig.PrefixOrderFormNum + Client.GetClientNum());
        }

        public static PayStatus GetPayStatus(OrderInfo info)
        {
            if (info.IsNull)
            {
                return PayStatus.WaitForPay;
            }
            if (info.MoneyTotal > info.MoneyReceipt)
            {
                if (info.MoneyReceipt > 0M)
                {
                    return PayStatus.ReceivedEarnest;
                }
                return PayStatus.WaitForPay;
            }
            return PayStatus.Payoff;
        }

        public static ArrayList GetTotalofMoneyAndReceipt()
        {
            return dal.GetTotalofMoneyAndReceipt();
        }

        public static ArrayList GetTotalofMoneyAndReceiptByAgentName(string agentName)
        {
            return dal.GetTotalofMoneyAndReceiptByAgentName(agentName);
        }

        public static ArrayList GetTotalofMoneyAndReceiptByUserName(string userName)
        {
            return dal.GetTotalofMoneyAndReceiptByUserName(userName);
        }

        public static int GetTotalOfOrder(string searchType, string field, string keyword, string action)
        {
            return dal.GetTotalOfOrder();
        }

        public static ArrayList GetTotalofthisMoneyAndReceipt(string field)
        {
            return dal.GetTotalofthisMoneyAndReceipt(field);
        }

        public static bool GoPause(int orderId)
        {
            return dal.GoPause(orderId);
        }

        public static bool GoRubbish(int orderId)
        {
            return dal.GoRubbish(orderId);
        }

        private static void ProcessSubscriber(OrderFlowInfo orderFlowInfo, UserInfo userInfo)
        {
            if (!userInfo.IsNull)
            {
                ClientInfo clientInfo = new ClientInfo();
                CompanyInfo companyInfo = new CompanyInfo();
                ContacterInfo contacterInfo = new ContacterInfo();
                if ((userInfo.UserId > 0) && (userInfo.ClientId == 0))
                {
                    if (userInfo.UserType > UserType.Persional)
                    {
                        companyInfo = Company.GetCompayById(userInfo.CompanyId);
                        if (!companyInfo.IsNull)
                        {
                            string companyName = companyInfo.CompanyName;
                            companyName = string.IsNullOrEmpty(companyName) ? string.Empty : companyName;
                            clientInfo.ClientName = companyInfo.CompanyName;
                            clientInfo.ShortedForm = companyInfo.CompanyName.Substring(0, 6);
                            clientInfo.ClientType = 0;
                        }
                    }
                    else
                    {
                        clientInfo.ClientName = orderFlowInfo.ConsigneeName;
                        clientInfo.ShortedForm = orderFlowInfo.ConsigneeName;
                        clientInfo.ClientType = 1;
                    }
                    clientInfo.ClientId = companyInfo.ClientId = userInfo.ClientId = Client.GetMaxId() + 1;
                    clientInfo.ClientNum = Client.GetClientNum();
                    clientInfo.Area = -1;
                    clientInfo.ClientField = -1;
                    clientInfo.ValueLevel = -1;
                    clientInfo.CreditLevel = -1;
                    clientInfo.Importance = -1;
                    clientInfo.ConnectionLevel = -1;
                    clientInfo.SourceType = -1;
                    clientInfo.PhaseType = -1;
                    clientInfo.UpdateTime = clientInfo.CreateTime = DateTime.Now;
                    Client.Add(clientInfo);
                    userInfo.ClientId = clientInfo.ClientId;
                    Users.Update(userInfo);
                    Company.Update(companyInfo);
                    Contacter.UpdateClientForSameCompany(clientInfo.ClientId, userInfo.CompanyId);
                }
                if (userInfo.UserId > 0)
                {
                    StringBuilder builder = new StringBuilder();
                    if (orderFlowInfo.Country != "中华人民共和国")
                    {
                        builder.Append(orderFlowInfo.Country);
                    }
                    builder.Append(orderFlowInfo.Province);
                    builder.Append(orderFlowInfo.City);
                    builder.Append(orderFlowInfo.Area);
                    builder.Append(orderFlowInfo.Address);
                    if (!Contacter.Exists(userInfo.UserName))
                    {
                        contacterInfo.ContacterId = Contacter.GetMaxId() + 1;
                        contacterInfo.UserName = userInfo.UserName;
                        contacterInfo.ClientId = userInfo.ClientId;
                        contacterInfo.CreateTime = contacterInfo.UpdateTime = DateTime.Now;
                        contacterInfo.TrueName = orderFlowInfo.ConsigneeName;
                        contacterInfo.ZipCode = orderFlowInfo.ZipCode;
                        contacterInfo.Address = builder.ToString();
                        contacterInfo.Mobile = orderFlowInfo.Mobile;
                        contacterInfo.OfficePhone = contacterInfo.HomePhone = orderFlowInfo.HomePhone;
                        contacterInfo.Email = orderFlowInfo.Email;
                        contacterInfo.Education = -1;
                        contacterInfo.Income = -1;
                        contacterInfo.Sex = UserSexType.Secrecy;
                        contacterInfo.Marriage = UserMarriageType.Secrecy;
                        contacterInfo.Country = orderFlowInfo.Country;
                        contacterInfo.Province = orderFlowInfo.Province;
                        contacterInfo.City = orderFlowInfo.City;
                        Contacter.Add(contacterInfo);
                    }
                    else
                    {
                        contacterInfo = Contacter.GetContacterByUserName(userInfo.UserName);
                        if (!contacterInfo.IsNull)
                        {
                            if (contacterInfo.ClientId <= 0)
                            {
                                contacterInfo.ClientId = userInfo.ClientId;
                            }
                            if (string.IsNullOrEmpty(contacterInfo.TrueName))
                            {
                                contacterInfo.TrueName = orderFlowInfo.ConsigneeName;
                            }
                            if (string.IsNullOrEmpty(contacterInfo.ZipCode))
                            {
                                contacterInfo.ZipCode = orderFlowInfo.ZipCode;
                            }
                            if (string.IsNullOrEmpty(contacterInfo.Address))
                            {
                                contacterInfo.Address = builder.ToString();
                            }
                            if (string.IsNullOrEmpty(contacterInfo.Mobile))
                            {
                                contacterInfo.Mobile = orderFlowInfo.Mobile;
                            }
                            if (string.IsNullOrEmpty(contacterInfo.HomePhone))
                            {
                                contacterInfo.HomePhone = orderFlowInfo.HomePhone;
                            }
                            if (string.IsNullOrEmpty(contacterInfo.Email))
                            {
                                contacterInfo.Email = orderFlowInfo.Email;
                            }
                            if (string.IsNullOrEmpty(contacterInfo.Country))
                            {
                                contacterInfo.Country = orderFlowInfo.Country;
                            }
                            if (string.IsNullOrEmpty(contacterInfo.Province))
                            {
                                contacterInfo.Province = orderFlowInfo.Province;
                            }
                            if (string.IsNullOrEmpty(contacterInfo.City))
                            {
                                contacterInfo.City = orderFlowInfo.City;
                            }
                            Contacter.Update(contacterInfo);
                        }
                    }
                    AddressInfo defaultAddressByUserName = Address.GetDefaultAddressByUserName(userInfo.UserName);
                    if (defaultAddressByUserName.IsNull)
                    {
                        defaultAddressByUserName.UserName = userInfo.UserName;
                        defaultAddressByUserName.Address = orderFlowInfo.Address;
                        defaultAddressByUserName.Area = orderFlowInfo.Area;
                        defaultAddressByUserName.City = orderFlowInfo.City;
                        defaultAddressByUserName.ConsigneeName = orderFlowInfo.ConsigneeName;
                        defaultAddressByUserName.Country = orderFlowInfo.Country;
                        defaultAddressByUserName.Province = orderFlowInfo.Province;
                        defaultAddressByUserName.HomePhone = orderFlowInfo.HomePhone;
                        defaultAddressByUserName.Mobile = orderFlowInfo.Mobile;
                        defaultAddressByUserName.ZipCode = orderFlowInfo.ZipCode;
                        defaultAddressByUserName.IsDefault = true;
                        Address.Add(defaultAddressByUserName);
                    }
                }
            }
        }

        public static bool Recieve(int orderId)
        {
            DeliverItem.UpdateReceive(orderId);
            return dal.Recieve(orderId);
        }

        public static bool SendCard(int orderId)
        {
            bool flag = true;
            bool flag2 = false;
            StringBuilder content = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            IList<OrderItemInfo> infoListByOrderId = OrderItem.GetInfoListByOrderId(orderId);
            int num = 0;
            StockInfo stockInfo = new StockInfo();
            stockInfo.Inputer = PEContext.Current.Admin.AdminName;
            stockInfo.InputTime = DateTime.Now;
            stockInfo.Remark = "交付点卡";
            stockInfo.StockId = StockManage.GetMaxId() + 1;
            stockInfo.StockNum = StockItem.GetShipmentNum();
            stockInfo.StockType = StockType.Shipment;
            foreach (OrderItemInfo info2 in infoListByOrderId)
            {
                if (Product.CharacterIsExists(info2.ProductCharacter, ProductCharacter.Card) && Cards.GetCardByOrderItemId(info2.ProductId, info2.TableName, info2.ItemId).IsNull)
                {
                    IList<CardInfo> list2 = Cards.GetUnsoldCard(info2.TableName, info2.ProductId, info2.Amount);
                    if (list2.Count < info2.Amount)
                    {
                        builder2.Append("<li>" + info2.ProductName + "的充值卡数量已经不够交付！请先去充值卡管理中添加有关充值卡！</li>");
                        flag = false;
                    }
                    else
                    {
                        foreach (CardInfo info4 in list2)
                        {
                            info4.OrderItemId = info2.ItemId;
                            if (!Cards.Update(info4))
                            {
                                return false;
                            }
                            GetMessageOrMailofCard(content, info4, info2.ProductName);
                        }
                        AddStockItemBySendCard(stockInfo.StockId, info2);
                        num++;
                    }
                }
                if (!flag2 && Product.CharacterIsExists(info2.ProductCharacter, ProductCharacter.Practicality))
                {
                    flag2 = true;
                }
            }
            if (num > 0)
            {
                StockManage.Add(stockInfo);
            }
            if (flag)
            {
                if (!flag2)
                {
                    OrderInfo orderById = GetOrderById(orderId);
                    if (!orderById.IsNull)
                    {
                        orderById.DeliverStatus = DeliverStatus.Consignment;
                        Update(orderById);
                    }
                }
            }
            else
            {
                CustomException.ThrowBllException(builder2.ToString());
            }
            s_MessgeOrMailContentOfCard = GetMessageOrMailofCard(content.ToString()).ToString();
            return flag;
        }

        public static bool Transfer(int orderId, int clientId, string userName)
        {
            return dal.Transfer(orderId, clientId, userName);
        }

        public static bool Update(OrderInfo orderInfo)
        {
            return dal.Update(orderInfo);
        }

        public static bool UpdateDeliverStatus(int orderId, DeliverStatus statusValue)
        {
            return dal.UpdateDeliverStatus(orderId, statusValue);
        }

        public static void UpdateOrderInfo(OrderInfo orderInfo, IList<OrderItemInfo> orderItemInfoList)
        {
            decimal totalMoney = 0M;
            double goodsWeight = 0.0;
            decimal num3 = 0M;
            foreach (OrderItemInfo info in orderItemInfoList)
            {
                totalMoney += info.SubTotal;
                goodsWeight += DataConverter.CDouble(info.Weight) * info.Amount;
            }
            orderInfo.MoneyGoods = totalMoney;
            PackageInfo packageByGoodsWeight = Package.GetPackageByGoodsWeight(goodsWeight);
            if (!packageByGoodsWeight.IsNull)
            {
                goodsWeight += packageByGoodsWeight.PackageWeight;
            }
            num3 = DeliverCharge.GetDeliverCharge(orderInfo.DeliverType, goodsWeight, orderInfo.ZipCode, totalMoney, orderInfo.NeedInvoice);
            if (num3 > 0M)
            {
                totalMoney += num3;
            }
            orderInfo.MoneyTotal = totalMoney;
            orderInfo.ChargeDeliver = num3;
            Update(orderInfo);
        }

        private static void UpdateUserInfo(OrderInfo orderinfo, int TotalPresentExp, int TotalPresentPoint, int TotalPresentMoney)
        {
            if ((((orderinfo.PresentExp != 0) || (orderinfo.PresentMoney != 0M)) || ((orderinfo.PresentPoint != 0) || (TotalPresentExp != 0))) || ((TotalPresentMoney != 0) || (TotalPresentPoint != 0)))
            {
                UserInfo usersByUserName = Users.GetUsersByUserName(orderinfo.UserName);
                if ((orderinfo.PresentMoney > 0M) || (TotalPresentMoney > 0))
                {
                    usersByUserName.Balance = (usersByUserName.Balance + orderinfo.PresentMoney) + TotalPresentMoney;
                    BankrollItemInfo bankrollItemInfo = new BankrollItemInfo();
                    bankrollItemInfo.Inputer = "";
                    bankrollItemInfo.UserName = orderinfo.UserName;
                    bankrollItemInfo.ClientId = orderinfo.ClientId;
                    bankrollItemInfo.Money = orderinfo.PresentMoney + TotalPresentMoney;
                    bankrollItemInfo.MoneyType = 4;
                    bankrollItemInfo.Bank = "";
                    bankrollItemInfo.EBankId = 0;
                    bankrollItemInfo.OrderId = orderinfo.OrderId;
                    bankrollItemInfo.PaymentId = 0;
                    bankrollItemInfo.Remark = "购物返还现金券";
                    bankrollItemInfo.LogTime = new DateTime?(DateTime.Now);
                    bankrollItemInfo.DateAndTime = new DateTime?(DateTime.Now);
                    bankrollItemInfo.CurrencyType = 1;
                    BankrollItem.Add(bankrollItemInfo);
                }
                if ((orderinfo.PresentExp > 0) || (TotalPresentExp > 0))
                {
                    usersByUserName.UserExp = (usersByUserName.UserExp + orderinfo.PresentExp) + TotalPresentExp;
                }
                if ((orderinfo.PresentPoint > 0) || (TotalPresentPoint > 0))
                {
                    usersByUserName.UserPoint = (usersByUserName.UserPoint + orderinfo.PresentPoint) + TotalPresentPoint;
                    UserPointLogInfo userPointLogInfo = new UserPointLogInfo();
                    userPointLogInfo.UserName = usersByUserName.UserName;
                    userPointLogInfo.IncomePayOut = 1;
                    userPointLogInfo.LogTime = DateTime.Now;
                    userPointLogInfo.Point = orderinfo.PresentPoint + TotalPresentPoint;
                    userPointLogInfo.Remark = "购物返还" + SiteConfig.UserConfig.PointName;
                    UserPointLog.Add(userPointLogInfo);
                }
                Users.Update(usersByUserName);
            }
        }

        public static bool UserPayment(int orderId, decimal moneyReceipt, OrderStatus status)
        {
            return dal.UserPayment(orderId, moneyReceipt, status);
        }

        public static string MessgeOrMailContentOfCard
        {
            get
            {
                return s_MessgeOrMailContentOfCard;
            }
        }
    }
}

