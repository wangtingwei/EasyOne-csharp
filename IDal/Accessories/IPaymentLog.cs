namespace EasyOne.IDal.Accessories
{
    using EasyOne.Model.Accessories;
    using System;
    using System.Collections.Generic;

    public interface IPaymentLog
    {
        bool Add(PaymentLogInfo paymentLogInfo);
        bool Delete(DateTime tempDate);
        bool Delete(string paymentLogId);
        PaymentLogInfo GetInfoByPaymentNum(string paymentNum);
        IList<PaymentLogInfo> GetList(int startRowIndexId, int maxNumberRows, string searchType, string field, string keyword);
        IList<PaymentLogInfo> GetListByOrderId(int orderId);
        IList<PaymentLogInfo> GetListByUserName(int startRowIndexId, int maxNumberRows, string userName);
        PaymentLogInfo GetPaymentLogById(int paymentLogId);
        int GetTotalOfPaymentLog();
        bool Update(PaymentLogInfo info);
        int Update(int paymentLogId);
    }
}

