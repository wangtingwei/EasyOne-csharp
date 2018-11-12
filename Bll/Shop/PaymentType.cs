namespace EasyOne.Shop
{
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Shop;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class PaymentType
    {
        private static readonly IPaymentType dal = DataAccess.CreatePaymentType();

        private PaymentType()
        {
        }

        public static bool Add(PaymentTypeInfo paymentTypeInfo)
        {
            return ((paymentTypeInfo != null) && dal.Add(paymentTypeInfo));
        }

        public static bool Delete(int typeId)
        {
            return dal.Delete(typeId);
        }

        public static string GetCategory(int index)
        {
            switch (index)
            {
                case 1:
                    return "在线支付";

                case 2:
                    return "余额支付";
            }
            return "其它";
        }

        public static PaymentTypeInfo GetPaymentTypeById(int typeId)
        {
            return dal.GetPaymentTypeById(typeId);
        }

        public static IList<PaymentTypeInfo> GetPaymentTypeList()
        {
            return dal.GetPaymentTypeList();
        }

        public static IList<PaymentTypeInfo> GetPaymentTypeList(int category)
        {
            return dal.GetPaymentTypeList(category);
        }

        public static IList<PaymentTypeInfo> GetPaymentTypeListByEnabled()
        {
            return dal.GetPaymentTypeListByEnabled();
        }

        public static bool Update(PaymentTypeInfo paymentTypeInfo)
        {
            return ((paymentTypeInfo != null) && dal.Update(paymentTypeInfo));
        }
    }
}

