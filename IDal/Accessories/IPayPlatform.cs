namespace EasyOne.IDal.Accessories
{
    using EasyOne.Model.Accessories;
    using System;
    using System.Collections.Generic;

    public interface IPayPlatform
    {
        bool Add(PayPlatformInfo payPlatformInfo);
        bool CheckSameName(string payPlatformName);
        bool Delete(int payPlatformId);
        bool DisablePayPlatform(int payPlatformId, bool isDisabled);
        PayPlatformInfo GetInfoByName(string payPlatformName);
        IList<PayPlatformInfo> GetList();
        IList<PayPlatformInfo> GetListOfDisabled(bool isDisabled);
        PayPlatformInfo GetPayPlatformById(int payPlatformId);
        bool SetDefault(int payPlatformId);
        bool SetOrderId(int payPlatformId, int orderId);
        bool Update(PayPlatformInfo payPlatformInfo);
    }
}

