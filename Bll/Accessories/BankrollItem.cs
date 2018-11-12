namespace EasyOne.Accessories
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Enumerations;
    using EasyOne.IDal.Accessories;
    using EasyOne.Model.Accessories;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using EasyOne.DalFactory;

    public sealed class BankrollItem
    {
        private static readonly string[] CurrencyType = new string[] { "", "人民币", "美元" };
        private static readonly IBankrollItem dal = DataAccess.CreateBankrollItem();
        private static readonly string[] FieldType = new string[] { "客户姓名中含有“ {0} ”的资金明细记录", "用户名中含有“{0} ”的资金明细记录", "{0} 的资金明细记录", "交易时间为 {0} 的资金明细记录" };
        private static readonly string[] MoneyType = new string[] { "", "现金", "银行汇款", "在线支付", "虚拟货币" };
        private static readonly string[] SearchType = new string[] { "所有资金明细记录", "最近10天内的新资金明细记录", "最近一月内的新资金明细记录", "所有收入记录", "所有支出记录" };

        private BankrollItem()
        {
        }

        public static bool Add(BankrollItemInfo bankrollItemInfo)
        {
            if (bankrollItemInfo == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(bankrollItemInfo.IP))
            {
                bankrollItemInfo.IP = PEContext.Current.UserHostAddress;
            }
            if (bankrollItemInfo.OrderId == 0)
            {
                bankrollItemInfo.OrderId = -RandomManage.GetFormatedNumeric(1, 0x7fffffff);
            }
            return dal.Add(bankrollItemInfo);
        }

        public static bool Confirm(int itemId, BankrollItemStatus status)
        {
            return dal.Confirm(itemId, status);
        }

        public static bool Delete(int itemId)
        {
            return dal.Delete(itemId);
        }

        public static bool ExistsConfirmRemittance(int orderId)
        {
            return dal.ExistsConfirmRemittance(orderId);
        }

        public static bool ExistsPaymentLog(int paymentId)
        {
            return dal.ExistsPaymentLog(paymentId);
        }

        public static BankrollItemInfo GetBankrollItemById(int itemId)
        {
            return dal.GetBankrollItemById(itemId);
        }

        public static DataTable GetBillOfAgent(int startRowIndex, int maximumRows)
        {
            string userName = PEContext.Current.User.UserName;
            return GetBillOfAgent(startRowIndex, maximumRows, userName);
        }

        public static DataTable GetBillOfAgent(int startRowIndex, int maximumRows, string userName)
        {
            return dal.GetBillOfAgent(startRowIndex, maximumRows, DataSecurity.FilterBadChar(userName));
        }

        public static string GetCurrencyType(object type)
        {
            int index = DataConverter.CLng(type);
            if (index < CurrencyType.Length)
            {
                return CurrencyType[index];
            }
            return "其它";
        }

        public static string GetCurrentNode(int searchType, int field, string keyword)
        {
            string str = SearchType[0];
            switch (searchType)
            {
                case 5:
                    return "所有已确认的记录";

                case 6:
                    return "所有未确认的记录";

                case 10:
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        str = string.Format(CultureInfo.CurrentCulture, FieldType[field], new object[] { "<span style='color:#f00'>" + keyword + "</span>" });
                    }
                    return str;

                case 11:
                    return "资金明细复杂查询结果";
            }
            if (searchType < SearchType.Length)
            {
                str = SearchType[searchType];
            }
            return str;
        }

        public static IList<BankrollItemInfo> GetList(int startRowIndex, int maximumRows, int searchType, int field, string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                switch (searchType)
                {
                    case 10:
                        switch (DataConverter.CLng(field))
                        {
                            case 3:
                                keyword = DataConverter.CDate(keyword).ToString("yyyy-MM-dd", CultureInfo.CurrentCulture);
                                goto Label_0192;

                            case 4:
                            case 5:
                                goto Label_0089;

                            case 6:
                            case 7:
                            case 8:
                                if (!string.IsNullOrEmpty(keyword))
                                {
                                    goto Label_0192;
                                }
                                return new List<BankrollItemInfo>();
                        }
                        goto Label_0089;

                    case 11:
                    {
                        string[] strArray = keyword.Split(new char[] { '|' });
                        if (strArray.Length != 7)
                        {
                            searchType = 0;
                        }
                        else
                        {
                            string str = DataConverter.CLng(strArray[0]).ToString(CultureInfo.CurrentCulture);
                            string str2 = DataConverter.CLng(strArray[1]).ToString(CultureInfo.CurrentCulture);
                            string str3 = string.IsNullOrEmpty(strArray[2]) ? "" : DataConverter.CDate(strArray[2]).ToShortDateString();
                            string str4 = string.IsNullOrEmpty(strArray[3]) ? "" : DataConverter.CDate(strArray[3]).ToShortDateString();
                            string str5 = DataSecurity.FilterBadChar(strArray[4]);
                            string str6 = DataSecurity.FilterBadChar(strArray[5]);
                            string str7 = DataSecurity.FilterBadChar(strArray[6]);
                            keyword = string.Format(CultureInfo.CurrentCulture, "{0}|{1}|{2}|{3}|{4}|{5}|{6}", new object[] { str, str2, str3, str4, str5, str6, str7 });
                        }
                        goto Label_0192;
                    }
                }
            }
            goto Label_0192;
        Label_0089:
            keyword = DataSecurity.FilterBadChar(keyword);
        Label_0192:
            return dal.GetList(startRowIndex, maximumRows, searchType, field, keyword);
        }

        public static int GetMaxItemId()
        {
            return dal.GetMaxItemId();
        }

        public static string GetMoneyType(object type)
        {
            int index = DataConverter.CLng(type);
            if (index < MoneyType.Length)
            {
                return MoneyType[index];
            }
            return "";
        }

        public static ArrayList GetTotalInComeAndPayOutAll()
        {
            return dal.GetTotalInComeAndPayOutAll();
        }

        public static ArrayList GetTotalInComeAndPayOutAll(int clientId)
        {
            if (clientId == 0)
            {
                ArrayList list = new ArrayList();
                list.Add(0);
                list.Add(0);
                return list;
            }
            return dal.GetTotalInComeAndPayOutAll(clientId);
        }

        public static ArrayList GetTotalInComeAndPayOutAll(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                ArrayList list = new ArrayList();
                list.Add(0);
                list.Add(0);
                return list;
            }
            return dal.GetTotalInComeAndPayOutAll(userName);
        }

        public static int GetTotalOfBankrollItem(int searchType, int field, string keyword)
        {
            return GetTotalOfBankrollItem(0, 0, searchType, field, keyword);
        }

        public static int GetTotalOfBankrollItem(int startRowIndex, int maximumRows, int searchType, int field, string keyword)
        {
            return dal.GetTotalOfBankrollItem();
        }

        public static int GetTotalOfBill(int startRowIndex, int maximumRows)
        {
            return dal.GetTotalOfBill();
        }

        public static int GetTotalOfBill(int startRowIndex, int maximumRows, string userName)
        {
            return dal.GetTotalOfBill();
        }

        public static bool Update(BankrollItemInfo bankrollItemInfo)
        {
            if (bankrollItemInfo == null)
            {
                return false;
            }
            if (bankrollItemInfo.OrderId == 0)
            {
                bankrollItemInfo.OrderId = -RandomManage.GetFormatedNumeric(1, 0x7fffffff);
            }
            return dal.Update(bankrollItemInfo);
        }
    }
}

