namespace EasyOne.IDal.Collection
{
    using EasyOne.Model.Collection;
    using System;
    using System.Collections.Generic;

    public interface ICollectionExclosion
    {
        bool Add(CollectionExclosionInfo collectionExclosionInfo);
        bool Delete(int id);
        bool Exists(string exclosionName);
        int GetCountNumber();
        CollectionExclosionInfo GetInfoById(int id);
        IList<CollectionExclosionInfo> GetList(int exclosionType);
        IList<CollectionExclosionInfo> GetList(int startRowIndexId, int maxNumberRows);
        bool Update(CollectionExclosionInfo collectionExclosionInfo);
    }
}

