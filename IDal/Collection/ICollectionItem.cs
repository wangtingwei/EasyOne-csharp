namespace EasyOne.IDal.Collection
{
    using EasyOne.Model.Collection;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public interface ICollectionItem
    {
        bool Add(CollectionItemInfo collectionItemInfo);
        bool Delete(int id);
        bool Disabled(int itemId);
        bool Exists(string itemName);
        bool ExistsCreateHtml(string itemIds);
        IList<CollectionItemInfo> GetCollectionList(string itemId);
        DataTable GetCollectionList(int startRowIndexId, int maxNumberRows);
        int GetCountNumber();
        CollectionItemInfo GetInfoById(int id);
        DataTable GetList(int startRowIndexId, int maxNumberRows);
        int GetMaxId();
        bool Update(CollectionItemInfo collectionItemInfo);
        bool UpdateCollecDate(int id);
    }
}

