namespace EasyOne.IDal.Shop
{
    using EasyOne.Model.Shop;
    using System;
    using System.Collections.Generic;

    public interface ITransferLog
    {
        bool Add(TransferLogInfo info);
        IList<TransferLogInfo> GetList(int startRowIndexId, int maxNumberRows, string searchType, string field, string keyword);
        int GetTotalOfTransferLog();
        TransferLogInfo GetTransferLogById(int transferLogId);
    }
}

