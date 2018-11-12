namespace EasyOne.IDal.Crm
{
    using EasyOne.Model.Crm;
    using System;
    using System.Collections.Generic;

    public interface IComplain
    {
        bool Add(ComplainItemInfo info);
        bool Delete(string itemId);
        ComplainItemInfo GetComplainById(int itemId);
        IList<ComplainItemInfo> GetList(int startRowIndexId, int maxNumberRows, int searchType, int field, string keyword);
        IList<ComplainItemInfo> GetListByClientName(int startRowIndexId, int maxNumberRows, string clientName);
        int GetMaxId();
        int GetTotal();
        bool Update(ComplainItemInfo info);
    }
}

