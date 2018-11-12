namespace EasyOne.Collection
{
    using EasyOne.IDal.Collection;
    using EasyOne.Model.Collection;
    using System;
    using EasyOne.DalFactory;

    public static class CollectionListRules
    {
        private static readonly ICollectionListRules dal = DataAccess.CreateCollectionListRules();

        public static bool Add(CollectionListRuleInfo collectionListRuleInfo)
        {
            return dal.Add(collectionListRuleInfo);
        }

        public static bool Delete(int id)
        {
            return dal.Delete(id);
        }

        public static bool Exists(int id)
        {
            return dal.Exists(id);
        }

        public static CollectionListRuleInfo GetInfoById(int id)
        {
            return dal.GetInfoById(id);
        }

        public static bool Update(CollectionListRuleInfo collectionListRuleInfo)
        {
            return dal.Update(collectionListRuleInfo);
        }
    }
}

