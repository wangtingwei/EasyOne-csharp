namespace EasyOne.IDal.Collection
{
    using EasyOne.Model.Collection;
    using System;
    using System.Data;

    public interface ICollectionHistory
    {
        bool Add(CollectionHistoryInfo collectionHistoryInfo);
        bool Delete();
        bool Delete(string id);
        bool DeleteErr();
        bool DeleteSuccess();
        bool Exists(string title);
        DataTable GetCollectionHistory(int startRowIndexId, int maxNumberRows);
        int GetCountNumber();
        int GetMaxId();
        int RecordCount(bool countType, int itemId);
    }
}

