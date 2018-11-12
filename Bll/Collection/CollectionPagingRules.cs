namespace EasyOne.Collection
{
    using EasyOne.IDal.Collection;
    using EasyOne.Model.Collection;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public static class CollectionPagingRules
    {
        private static readonly ICollectionPagingRules dal = DataAccess.CreateCollectionPagingRules();

        public static bool Add(CollectionPagingRuleInfo collectionPagingRuleInfo)
        {
            return dal.Add(collectionPagingRuleInfo);
        }

        public static bool Delete(int id, int ruleType)
        {
            return dal.Delete(id, ruleType);
        }

        public static bool Exists(int id, int ruleType)
        {
            return dal.Exists(id, ruleType);
        }

        public static IList<CollectionPagingRuleInfo> GetCollectionPagingRuleList(int itemId)
        {
            return dal.GetCollectionPagingRuleList(itemId);
        }

        public static CollectionPagingRuleInfo GetInfoById(int id, int ruleType)
        {
            return dal.GetInfoById(id, ruleType);
        }

        public static bool Update(CollectionPagingRuleInfo collectionPagingRuleInfo)
        {
            return dal.Update(collectionPagingRuleInfo);
        }
    }
}

