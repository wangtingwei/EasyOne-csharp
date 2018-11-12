namespace EasyOne.IDal.Collection
{
    using EasyOne.Model.Collection;
    using System;
    using System.Collections.Generic;

    public interface ICollectionPagingRules
    {
        bool Add(CollectionPagingRuleInfo collectionPagingRuleInfo);
        bool Delete(int id, int ruleType);
        bool Exists(int id, int ruleType);
        IList<CollectionPagingRuleInfo> GetCollectionPagingRuleList(int itemId);
        CollectionPagingRuleInfo GetInfoById(int id, int ruleType);
        bool Update(CollectionPagingRuleInfo collectionPagingRuleInfo);
    }
}

