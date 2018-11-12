namespace EasyOne.Accessories
{
    using EasyOne.Common;
    using EasyOne.IDal.Accessories;
    using EasyOne.Model.Accessories;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class Bank
    {
        private static readonly IBank dal = DataAccess.CreateBank();

        private Bank()
        {
        }

        public static bool Add(BankInfo bankInfo)
        {
            bankInfo.BankId = GetMaxId() + 1;
            return dal.Add(bankInfo);
        }

        public static int Count()
        {
            return dal.Count();
        }

        public static bool Delete(int bankId)
        {
            return dal.Delete(bankId);
        }

        public static bool Exists(string bankShortName)
        {
            return dal.ExistBankShortName(DataSecurity.FilterBadChar(bankShortName));
        }

        public static BankInfo GetBankById(int bankId)
        {
            return dal.GetBankById(bankId);
        }

        public static IList<BankInfo> GetList()
        {
            return dal.GetList();
        }

        public static IList<BankInfo> GetList(int startRowIndexId, int maxNumberRows)
        {
            return dal.GetList(startRowIndexId, maxNumberRows);
        }

        public static IList<BankInfo> GetListByEnabled()
        {
            return dal.GetListByEnabled();
        }

        public static int GetMaxId()
        {
            return dal.GetMaxId();
        }

        public static bool SetDefault(int bankId)
        {
            return dal.SetDefault(bankId);
        }

        public static bool SetDisabled(int bankId, bool isDisabled)
        {
            return dal.SetDisabled(bankId, isDisabled);
        }

        public static bool SetOrderId(int bankId, int orderId)
        {
            return dal.SetOrderId(bankId, orderId);
        }

        public static bool Update(BankInfo bankInfo)
        {
            return dal.Update(bankInfo);
        }
    }
}

