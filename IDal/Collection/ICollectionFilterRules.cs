namespace EasyOne.IDal.Collection
{
    using EasyOne.Model.Collection;
    using System;
    using System.Collections.Generic;

    public interface ICollectionFilterRules
    {
        bool Add(CollectionFilterRuleInfo collectionFilterRuleInfo);
        bool Delete(int id);
        bool Delete(string id);
        bool Exists(string filterName);
        int GetCountNumber();
        CollectionFilterRuleInfo GetInfoById(int id);
        IList<CollectionFilterRuleInfo> GetList(int startRowIndexId, int maxNumberRows);
        bool Update(CollectionFilterRuleInfo collectionFilterRuleInfo);
    }
}

