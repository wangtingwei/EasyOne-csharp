namespace EasyOne.Shop
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Accessories;
    using EasyOne.Model.Shop;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class Agent
    {
        private static readonly IAgent dal = DataAccess.CreateAgentPayment();

        private Agent()
        {
        }

        private static void AddBankroll(AgentInfo agentInfo, int incomePayOut, string inputer, string remark)
        {
            BankrollItemInfo bankrollItemInfo = new BankrollItemInfo();
            bankrollItemInfo.ClientId = agentInfo.ClientId;
            bankrollItemInfo.UserName = agentInfo.AgentName;
            bankrollItemInfo.CurrencyType = 1;
            bankrollItemInfo.Inputer = inputer;
            bankrollItemInfo.LogTime = new DateTime?(DateTime.Now);
            bankrollItemInfo.DateAndTime = new DateTime?(DateTime.Now);
            decimal money = agentInfo.Money;
            if (incomePayOut == 2)
            {
                money = (money > 0M) ? (-1M * money) : money;
            }
            bankrollItemInfo.Money = money;
            bankrollItemInfo.MoneyType = 4;
            bankrollItemInfo.OrderId = agentInfo.OrderId;
            bankrollItemInfo.Remark = remark;
            BankrollItem.Add(bankrollItemInfo);
        }

        public static IList<string> GetAgentNameList(int startRowIndexId, int maxiNumRows, string keyword)
        {
            return dal.GetAgentNameList(startRowIndexId, maxiNumRows, DataSecurity.FilterBadChar(keyword));
        }

        public static string GetAgentPaymentState(int state)
        {
            switch (state)
            {
                case 0:
                    return "支付不成功";

                case 1:
                    return "找不到指定的订单";

                case 2:
                    return "此订单已支付";

                case 3:
                    return "找不到指定的代理商";

                case 4:
                    return "代理商资金余额小于支出金额";

                case 0x63:
                    return "支付成功";
            }
            return "";
        }

        private static decimal GetMargin(OrderInfo orderInfo, UserInfo userInfo)
        {
            IList<OrderItemInfo> infoListByOrderId = OrderItem.GetInfoListByOrderId(orderInfo.OrderId);
            decimal num = 0M;
            decimal totalMoney = 0M;
            double goodsWeight = 0.0;
            decimal num4 = 0M;
            UserPurviewInfo userPurview = userInfo.UserPurview;
            bool haveWholesalePurview = false;
            if (userPurview != null)
            {
                haveWholesalePurview = userPurview.Enablepm;
            }
            foreach (OrderItemInfo info2 in infoListByOrderId)
            {
                if (string.IsNullOrEmpty(info2.TableName))
                {
                    PresentInfo presentById = Present.GetPresentById(info2.ProductId);
                    goodsWeight += presentById.Weight * info2.Amount;
                    totalMoney += info2.SubTotal;
                }
                else
                {
                    ProductInfo productById = Product.GetProductById(info2.ProductId, info2.TableName);
                    if (!productById.IsNull)
                    {
                        AbstractItemInfo info5 = new ConcreteProductInfo(info2.Amount, info2.Property, productById, userInfo, orderInfo.NeedInvoice, true, haveWholesalePurview);
                        info5.GetItemInfo();
                        totalMoney += info5.SubTotal;
                        goodsWeight += info5.TotalWeight;
                    }
                }
            }
            PackageInfo packageByGoodsWeight = Package.GetPackageByGoodsWeight(goodsWeight);
            if (!packageByGoodsWeight.IsNull)
            {
                goodsWeight += packageByGoodsWeight.PackageWeight;
            }
            num4 = DeliverCharge.GetDeliverCharge(orderInfo.DeliverType, goodsWeight, orderInfo.ZipCode, totalMoney, orderInfo.NeedInvoice);
            int couponId = orderInfo.CouponId;
            if (couponId > 0)
            {
                CouponInfo couponInfoById = Coupon.GetCouponInfoById(couponId);
                if (!couponInfoById.IsNull)
                {
                    totalMoney -= couponInfoById.Money;
                    if (totalMoney < 0M)
                    {
                        totalMoney = 0M;
                    }
                }
            }
            totalMoney += num4;
            num = orderInfo.MoneyTotal - totalMoney;
            if (num < 0M)
            {
                num = 0M;
            }
            return num;
        }

        public static int GetTotal()
        {
            return dal.GetTotal();
        }

        public static int Payment(int orderId, string inputer)
        {
            orderId = DataConverter.CLng(orderId);
            inputer = DataSecurity.FilterBadChar(inputer);
            decimal margin = 0M;
            decimal num2 = 0M;
            bool flag = false;
            AgentInfo agentInfo = new AgentInfo();
            int num3 = 0;
            OrderInfo orderById = Order.GetOrderById(orderId);
            if (orderById.IsNull)
            {
                num3 = 1;
            }
            else if (orderById.MoneyTotal == orderById.MoneyReceipt)
            {
                num3 = 2;
            }
            else
            {
                UserInfo usersByUserName = Users.GetUsersByUserName(orderById.AgentName);
                if (usersByUserName.IsNull)
                {
                    num3 = 3;
                }
                else
                {
                    if (orderById.AgentName != orderById.UserName)
                    {
                        margin = GetMargin(orderById, usersByUserName);
                    }
                    num2 = orderById.MoneyTotal - orderById.MoneyReceipt;
                    if ((usersByUserName.Balance + DataConverter.CDecimal(usersByUserName.UserPurview.Overdraft)) < (num2 - margin))
                    {
                        num3 = 4;
                    }
                    else
                    {
                        agentInfo.OrderId = orderById.OrderId;
                        agentInfo.AgentName = orderById.AgentName;
                        agentInfo.Margin = margin;
                        agentInfo.Money = num2;
                        agentInfo.UserName = usersByUserName.UserName;
                        agentInfo.ClientId = usersByUserName.ClientId;
                        flag = dal.Payment(agentInfo);
                    }
                }
            }
            if (!flag)
            {
                return num3;
            }
            AddBankroll(agentInfo, 2, inputer, "支付订单费用，订单号：" + orderById.OrderNum);
            if (margin != 0M)
            {
                agentInfo.Money = margin;
                AddBankroll(agentInfo, 1, inputer, "返还代理订单差额，订单号：" + orderById.OrderNum);
            }
            return 0x63;
        }
    }
}

