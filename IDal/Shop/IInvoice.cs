namespace EasyOne.IDal.Shop
{
    using EasyOne.Model.Shop;
    using System;
    using System.Collections.Generic;

    public interface IInvoice
    {
        bool Add(InvoiceInfo invoiceInfo);
        InvoiceInfo GetInvoiceInfoById(int invoiceId);
        InvoiceInfo GetInvoiceInfoByOrderId(int orderId);
        IList<InvoiceInfo> GetList(int startRowIndexId, int maxNumberRows, int searchType, string keyword, int quickSearch);
        int GetTotalOfInvoiceItem();
    }
}

