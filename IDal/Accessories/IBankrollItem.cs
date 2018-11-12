namespace EasyOne.IDal.Accessories
{
    using EasyOne.Enumerations;
    using EasyOne.Model.Accessories;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;

    public interface IBankrollItem
    {
        bool Add(BankrollItemInfo bankrollItemInfo);
        bool Confirm(int itemId, BankrollItemStatus status);
        bool Delete(int itemId);
        bool ExistsConfirmRemittance(int orderId);
        bool ExistsPaymentLog(int paymentId);
        BankrollItemInfo GetBankrollItemById(int itemId);
        DataTable GetBillOfAgent(int startRowIndexId, int maxNumberRows, string userName);
        IList<BankrollItemInfo> GetList(int startRowIndexId, int maxNumberRows, int searchType, int field, string keyword);
        int GetMaxItemId();
        ArrayList GetTotalInComeAndPayOutAll();
        ArrayList GetTotalInComeAndPayOutAll(int clientId);
        ArrayList GetTotalInComeAndPayOutAll(string userName);
        int GetTotalOfBankrollItem();
        int GetTotalOfBill();
        bool Update(BankrollItemInfo bankrollItemInfo);
    }
}

