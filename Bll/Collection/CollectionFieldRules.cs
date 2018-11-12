namespace EasyOne.Collection
{
    using EasyOne.Common;
    using EasyOne.IDal.Collection;
    using EasyOne.Model.Collection;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public static class CollectionFieldRules
    {
        private static readonly ICollectionFieldRules dal = DataAccess.CreateCollectionFieldRules();

        public static bool Add(CollectionFieldRuleInfo collectionFieldRuleInfo)
        {
            return dal.Add(collectionFieldRuleInfo);
        }

        public static bool Delete(int id)
        {
            return dal.Delete(id);
        }

        public static bool DeleteFieldName(int itemId, string fieldName)
        {
            return dal.DeleteFieldName(itemId, DataSecurity.FilterBadChar(fieldName));
        }

        public static bool DeleteItem(int itemId)
        {
            return dal.DeleteItem(itemId);
        }

        public static bool Exists(int itemId, string fieldName)
        {
            return dal.Exists(itemId, DataSecurity.FilterBadChar(fieldName));
        }

        public static CollectionFieldRuleInfo GetInfoById(int itemId, string fieldName)
        {
            return dal.GetInfoById(itemId, DataSecurity.FilterBadChar(fieldName));
        }

        public static IList<CollectionFieldRuleInfo> GetList(int itemId)
        {
            return dal.GetList(itemId);
        }

        public static bool Update(CollectionFieldRuleInfo collectionFieldRuleInfo)
        {
            return dal.Update(collectionFieldRuleInfo);
        }

        public static void UpdateExclosionId(int exclosionId)
        {
            dal.UpdateExclosionId(exclosionId);
        }
    }
}

