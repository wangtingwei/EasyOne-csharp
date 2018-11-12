namespace EasyOne.Accessories
{
    using EasyOne.Common;
    using EasyOne.IDal.Accessories;
    using EasyOne.Model.Accessories;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class PayPlatform
    {
        private static readonly IPayPlatform dal = DataAccess.CreatePayPlatform();

        private PayPlatform()
        {
        }

        public static bool Add(PayPlatformInfo payPlatformInfo)
        {
            return dal.Add(payPlatformInfo);
        }

        public static bool Delete(int payPlatformId)
        {
            return dal.Delete(payPlatformId);
        }

        public static bool DisablePayPlatform(int payPlatformId, bool isDisabled)
        {
            return dal.DisablePayPlatform(payPlatformId, isDisabled);
        }

        public static bool Exists(string payPlatformName)
        {
            return dal.CheckSameName(DataSecurity.FilterBadChar(payPlatformName));
        }

        public static PayPlatformInfo GetInfoByName(string payPlatformName)
        {
            return dal.GetInfoByName(DataSecurity.FilterBadChar(payPlatformName));
        }

        public static IList<PayPlatformInfo> GetList()
        {
            return dal.GetList();
        }

        public static IList<PayPlatformInfo> GetListOfEnabled()
        {
            return dal.GetListOfDisabled(false);
        }

        public static PayPlatformInfo GetPayPlatformById(int payPlatformId)
        {
            return dal.GetPayPlatformById(payPlatformId);
        }

        public static bool SetDefault(int payPlatformId)
        {
            return dal.SetDefault(payPlatformId);
        }

        public static bool SetOrderId(int payPlatformId, int orderId)
        {
            return dal.SetOrderId(payPlatformId, orderId);
        }

        public static bool Update(PayPlatformInfo payPlatformInfo)
        {
            return dal.Update(payPlatformInfo);
        }
    }
}

