namespace EasyOne.Shop
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Accessories;
    using EasyOne.Model.Shop;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Text;
    using EasyOne.DalFactory;

    public sealed class TransferLog
    {
        private static readonly ITransferLog dal = DataAccess.CreateTransferLog();
        private static string s_ErrMsg;
        private static string s_SuccessMsg;

        private TransferLog()
        {
        }

        public static bool Add(TransferLogInfo info)
        {
            bool flag = false;
            OrderInfo orderById = Order.GetOrderById(info.OrderId);
            StringBuilder builder = new StringBuilder();
            if (orderById.IsNull)
            {
                builder.Append("<li>找不到指定的订单！</li>");
                flag = false;
            }
            else
            {
                bool flag2;
                string str;
                UserInfo info3;
                CheckValidation(info, orderById, out flag2, out str, out info3);
                if (flag2)
                {
                    builder.Append(str);
                    flag = false;
                }
                else
                {
                    int num;
                    GetTransferLogInfo(ref info, out num, orderById, info3);
                    if (info.Poundage > 0M)
                    {
                        BankrollItemInfo info4;
                        GetBankrollItemInfo(info, orderById, info3, num, out info4);
                        if (!BankrollItem.Add(info4))
                        {
                            builder.Append("<li>资金明细记录添加失败！</li>");
                        }
                    }
                    if (dal.Add(info))
                    {
                        if (!Order.Transfer(info.OrderId, info3.ClientId, info3.UserName))
                        {
                            flag = false;
                        }
                        else
                        {
                            s_SuccessMsg = "已经成功将编号为：<font color='red'>" + orderById.OrderNum + "</font> 的订单（原所有者：<font color='red'>" + orderById.UserName + "</font>）过户给：<font color='red'>" + info3.UserName + "</font>！";
                            s_SuccessMsg = string.Concat(new object[] { s_SuccessMsg, "<br><p align='center'><a href='OrderManage.aspx?OrderID=", info.OrderId, "'>点此查看订单信息</a></p>" });
                            flag = true;
                        }
                    }
                }
            }
            s_ErrMsg = builder.ToString();
            return flag;
        }

        private static void CheckValidation(TransferLogInfo info, OrderInfo orderInfo, out bool blStale, out string errMsg, out UserInfo usersInfo)
        {
            blStale = false;
            errMsg = "";
            StringBuilder builder = new StringBuilder();
            if (orderInfo.UserName == info.TargetUserName)
            {
                blStale = true;
                builder.Append("<li>接收人与订单当前所有者为同一人，无需过户！</li>");
            }
            usersInfo = Users.GetUsersByUserName(info.TargetUserName);
            if (usersInfo.IsNull)
            {
                blStale = true;
                builder.Append("<li>找不到指定的接收人！</li>");
            }
            else if ((usersInfo.Balance < info.Poundage) && (info.PayerUserName == "2"))
            {
                blStale = true;
                builder.Append("<li>过户对象的资金余额不足支付手续费！</li>");
            }
            if (((info.PayerUserName == "1") && !string.IsNullOrEmpty(orderInfo.UserName)) && (Users.GetUsersByUserName(orderInfo.UserName).Balance < info.Poundage))
            {
                blStale = true;
                builder.Append("<li>订单当前所有者，资金余额不足支付手续费！</li>");
            }
            errMsg = builder.ToString();
        }

        private static void GetBankrollItemInfo(TransferLogInfo info, OrderInfo orderInfo, UserInfo usersInfo, int payerClientID, out BankrollItemInfo bankrollItemInfo)
        {
            bankrollItemInfo = new BankrollItemInfo();
            bankrollItemInfo.UserName = info.PayerUserName;
            bankrollItemInfo.ClientId = payerClientID;
            bankrollItemInfo.Money = (info.Poundage > 0M) ? (-1M * info.Poundage) : info.Poundage;
            bankrollItemInfo.MoneyType = 4;
            bankrollItemInfo.CurrencyType = 1;
            bankrollItemInfo.Bank = "";
            bankrollItemInfo.EBankId = 0;
            bankrollItemInfo.OrderId = info.OrderId;
            bankrollItemInfo.PaymentId = 0;
            bankrollItemInfo.Remark = "支付订单过户费用，订单号：" + orderInfo.OrderNum + "。由" + orderInfo.UserName + "过户给" + usersInfo.UserName;
            bankrollItemInfo.LogTime = new DateTime?(DateTime.Now);
            bankrollItemInfo.Inputer = info.Inputer;
            bankrollItemInfo.IP = PEContext.Current.UserHostAddress;
            bankrollItemInfo.DateAndTime = new DateTime?(DateTime.Now);
        }

        public static IList<TransferLogInfo> GetList(int startRowIndexId, int maxNumberRows, string searchType, string field, string keyword)
        {
            string str;
            if ((!string.IsNullOrEmpty(keyword) && ((str = searchType) != null)) && (str == "10"))
            {
                string str2;
                if (((str2 = field) != null) && (str2 == "TransferTime"))
                {
                    keyword = DataConverter.CDate(keyword).ToString("yyyy-MM-dd");
                }
                else
                {
                    keyword = DataSecurity.FilterBadChar(keyword);
                }
            }
            return dal.GetList(startRowIndexId, maxNumberRows, searchType, field, keyword);
        }

        public static int GetTotalOfTransferLog(string searchType, string field, string keyword)
        {
            return dal.GetTotalOfTransferLog();
        }

        public static TransferLogInfo GetTransferLogById(int transferLogId)
        {
            return dal.GetTransferLogById(transferLogId);
        }

        private static void GetTransferLogInfo(ref TransferLogInfo info, out int payerClientID, OrderInfo orderInfo, UserInfo usersInfo)
        {
            if (info.PayerUserName == "1")
            {
                payerClientID = DataConverter.CLng(orderInfo.ClientId);
                info.PayerUserName = orderInfo.UserName;
            }
            else
            {
                payerClientID = DataConverter.CLng(usersInfo.ClientId);
                info.PayerUserName = usersInfo.UserName;
            }
            info.TargetUserName = usersInfo.UserName;
            info.OwnerUserName = orderInfo.UserName;
            info.TransferTime = new DateTime?(DateTime.Now);
        }

        public static string ErrMsgOfAddTransferLog
        {
            get
            {
                return s_ErrMsg;
            }
        }

        public static string SuccessMsgOfAddTransferLog
        {
            get
            {
                return s_SuccessMsg;
            }
        }
    }
}

