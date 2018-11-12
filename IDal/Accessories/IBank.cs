namespace EasyOne.IDal.Accessories
{
    using EasyOne.Model.Accessories;
    using System;
    using System.Collections.Generic;

    public interface IBank
    {
        bool Add(BankInfo bankInfo);
        int Count();
        bool Delete(int bankId);
        bool ExistBankShortName(string bankShortName);
        BankInfo GetBankById(int bankId);
        IList<BankInfo> GetList();
        IList<BankInfo> GetList(int startRowIndexId, int maxiNumRows);
        IList<BankInfo> GetListByEnabled();
        int GetMaxId();
        bool SetDefault(int bankId);
        bool SetDisabled(int bankId, bool isDisabled);
        bool SetOrderId(int bankId, int orderId);
        bool Update(BankInfo bankInfo);
    }
}

