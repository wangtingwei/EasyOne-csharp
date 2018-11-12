namespace EasyOne.IDal.Shop
{
    using EasyOne.Model.Shop;
    using System;
    using System.Collections.Generic;

    public interface IPaymentType
    {
        bool Add(PaymentTypeInfo paymentTypeInfo);
        bool Delete(int typeId);
        PaymentTypeInfo GetPaymentTypeById(int typeId);
        IList<PaymentTypeInfo> GetPaymentTypeList();
        IList<PaymentTypeInfo> GetPaymentTypeList(int category);
        IList<PaymentTypeInfo> GetPaymentTypeListByEnabled();
        bool Update(PaymentTypeInfo paymentTypeInfo);
    }
}

