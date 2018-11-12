namespace EasyOne.IDal.Collection
{
    using EasyOne.Model.Collection;
    using System;
    using System.Collections.Generic;

    public interface ICollectionFieldRules
    {
        bool Add(CollectionFieldRuleInfo collectionFieldRuleInfo);
        bool Delete(int id);
        bool DeleteFieldName(int itemId, string fieldName);
        bool DeleteItem(int itemId);
        bool Exists(int itemId, string fieldName);
        CollectionFieldRuleInfo GetInfoById(int itemId, string fieldName);
        IList<CollectionFieldRuleInfo> GetList(int itemId);
        bool Update(CollectionFieldRuleInfo collectionFieldRuleInfo);
        bool UpdateExclosionId(int exclosionId);
    }
}

