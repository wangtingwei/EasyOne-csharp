namespace EasyOne.Accessories
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.IDal.Accessories;
    using EasyOne.Model.Accessories;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using EasyOne.DalFactory;

    public sealed class PaymentLog
    {
        private static readonly IPaymentLog dal = DataAccess.CreatePaymentLog();

        private PaymentLog()
        {
        }

        public static bool Add(PaymentLogInfo paymentLogInfo)
        {
            return dal.Add(paymentLogInfo);
        }

        public static bool Delete(DateTime tempDate)
        {
            return dal.Delete(tempDate);
        }

        public static bool Delete(string paymentLogId)
        {
            return dal.Delete(paymentLogId);
        }

        public static PaymentLogInfo GetInfoByPaymentNum(string paymentNum)
        {
            return dal.GetInfoByPaymentNum(paymentNum);
        }

        public static IList<PaymentLogInfo> GetList(int startRowIndexId, int maxNumberRows, string searchType, string field, string keyword)
        {
            string str = searchType;
            if (str != null)
            {
                if (!(str == "6"))
                {
                    if (str == "10")
                    {
                        if (field == "PayTime")
                        {
                            keyword = DataConverter.CDate(keyword).ToString();
                        }
                        else
                        {
                            keyword = DataSecurity.FilterBadChar(keyword);
                        }
                    }
                }
                else if (string.IsNullOrEmpty(field))
                {
                    return new List<PaymentLogInfo>();
                }
            }
            return dal.GetList(startRowIndexId, maxNumberRows, searchType, field, keyword);
        }

        public static IList<PaymentLogInfo> GetListByOrderId(int orderId)
        {
            return dal.GetListByOrderId(orderId);
        }

        public static IList<PaymentLogInfo> GetListByUserName(int startRowIndexId, int maxNumberRows, string userName)
        {
            return dal.GetListByUserName(startRowIndexId, maxNumberRows, DataSecurity.FilterBadChar(userName));
        }

        public static PaymentLogInfo GetPaymentLogById(int paymentLogId)
        {
            return dal.GetPaymentLogById(paymentLogId);
        }

        public static string GetPaymentNum()
        {
            return (SiteConfig.ShopConfig.PrefixPaymentNum + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture));
        }

        public static string GetStatusDepict(int platformId, int status)
        {
            string str = string.Empty;
            if (platformId != 8)
            {
                switch (status)
                {
                    case 1:
                        return "未提交";

                    case 2:
                        return "已经提交，但未成功";

                    case 3:
                        return "支付成功";
                }
                return str;
            }
            switch (status)
            {
                case 1:
                    return "等待买家付款";

                case 2:
                    return "买家已付款，等待卖家发货";

                case 3:
                    return "交易成功";

                case 4:
                    return "卖家已发货，等待买家确认收货";
            }
            return str;
        }

        public static int GetTotalOfPaymentLog(string searchType, string field, string keyword)
        {
            return dal.GetTotalOfPaymentLog();
        }

        public static int GetTotalOfPaymentLogByUserName(string userName)
        {
            return dal.GetTotalOfPaymentLog();
        }

        public static bool Update(PaymentLogInfo info)
        {
            return dal.Update(info);
        }

        public static int Update(int paymentLogId)
        {
            return dal.Update(paymentLogId);
        }
    }
}

