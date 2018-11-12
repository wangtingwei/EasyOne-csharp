namespace EasyOne.Collection
{
    using EasyOne.Common;
    using EasyOne.IDal.Collection;
    using EasyOne.Model.Collection;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public static class CollectionFilterRules
    {
        private static readonly ICollectionFilterRules dal = DataAccess.CreateCollectionFilterRules();

        public static bool Add(CollectionFilterRuleInfo collectionFilterRuleInfo)
        {
            return dal.Add(collectionFilterRuleInfo);
        }

        public static bool Delete(int id)
        {
            return dal.Delete(id);
        }

        public static bool Delete(string id)
        {
            return dal.Delete(DataSecurity.FilterBadChar(id));
        }

        public static bool Exists(string filterName)
        {
            return dal.Exists(DataSecurity.FilterBadChar(filterName));
        }

        public static int GetCountNumber()
        {
            return dal.GetCountNumber();
        }

        public static CollectionFilterRuleInfo GetInfoById(int id)
        {
            return dal.GetInfoById(id);
        }

        public static IList<CollectionFilterRuleInfo> GetList(int startRowIndexId, int maxNumberRows)
        {
            return dal.GetList(startRowIndexId, maxNumberRows);
        }

        public static bool Update(CollectionFilterRuleInfo collectionFilterRuleInfo)
        {
            return dal.Update(collectionFilterRuleInfo);
        }
    }
}

