namespace EasyOne.Shop
{
    using EasyOne.Common;
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Shop;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class Invoice
    {
        private static readonly IInvoice dal = DataAccess.CreateInvoice();

        private Invoice()
        {
        }

        public static bool Add(InvoiceInfo invoiceInfo)
        {
            return dal.Add(invoiceInfo);
        }

        public static InvoiceInfo GetInvoiceInfoById(int invoiceId)
        {
            return dal.GetInvoiceInfoById(invoiceId);
        }

        public static InvoiceInfo GetInvoiceInfoByOrderId(int orderId)
        {
            return dal.GetInvoiceInfoByOrderId(orderId);
        }

        public static string GetInvoiceType(int invoiceType)
        {
            switch (invoiceType)
            {
                case 0:
                    return "地税普通发票";

                case 1:
                    return "国税普通发票";

                case 2:
                    return "增值税发票";
            }
            return "";
        }

        public static IList<InvoiceInfo> GetList(int startRowIndexId, int maxNumberRows, int searchType, string keyword, int quickSearch)
        {
            if (searchType == 3)
            {
                keyword = Convert.ToString(DataConverter.CDate(keyword));
            }
            else
            {
                keyword = DataSecurity.FilterBadChar(keyword);
            }
            return dal.GetList(startRowIndexId, maxNumberRows, searchType, keyword, quickSearch);
        }

        public static string GetSearchTypeName(int searchType)
        {
            switch (searchType)
            {
                case 1:
                    return "客户名称";

                case 2:
                    return "开票人";

                case 3:
                    return "发票日期";
            }
            return "";
        }

        public static int GetTotalOfInvoiceItem(string searchType, string keyword, string quickSearch)
        {
            return dal.GetTotalOfInvoiceItem();
        }
    }
}

