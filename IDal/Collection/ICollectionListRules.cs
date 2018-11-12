namespace EasyOne.IDal.Collection
{
    using EasyOne.Model.Collection;
    using System;

    public interface ICollectionListRules
    {
        bool Add(CollectionListRuleInfo collectionListRuleInfo);
        bool Delete(int id);
        bool Exists(int id);
        CollectionListRuleInfo GetInfoById(int id);
        bool Update(CollectionListRuleInfo collectionListRuleInfo);
    }
}

