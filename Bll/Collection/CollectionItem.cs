namespace EasyOne.Collection
{
    using EasyOne.Common;
    using EasyOne.IDal.Collection;
    using EasyOne.Model.Collection;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using EasyOne.DalFactory;

    public static class CollectionItem
    {
        private static readonly ICollectionItem dal = DataAccess.CreateCollectionItem();

        public static bool Add(CollectionItemInfo collectionItemInfo)
        {
            return dal.Add(collectionItemInfo);
        }

        public static bool Copy(int id)
        {
            if (id <= 0)
            {
                return false;
            }
            CollectionItemInfo infoById = GetInfoById(id);
            if (infoById.IsNull)
            {
                return false;
            }
            bool flag = false;
            infoById.ItemName = infoById.ItemName + "_复制";
            if (!Add(infoById))
            {
                return flag;
            }
            CollectionListRuleInfo collectionListRuleInfo = CollectionListRules.GetInfoById(id);
            collectionListRuleInfo.ItemId = infoById.ItemId;
            if (!CollectionListRules.Add(collectionListRuleInfo))
            {
                return flag;
            }
            foreach (CollectionPagingRuleInfo info3 in CollectionPagingRules.GetCollectionPagingRuleList(id))
            {
                info3.ItemId = infoById.ItemId;
                CollectionPagingRules.Add(info3);
            }
            foreach (CollectionFieldRuleInfo info4 in CollectionFieldRules.GetList(id))
            {
                info4.ItemId = infoById.ItemId;
                CollectionFieldRules.Add(info4);
            }
            return true;
        }

        public static bool Delete(int id)
        {
            bool flag = false;
            if (dal.Delete(id))
            {
                flag = true;
            }
            return flag;
        }

        public static bool Delete(string id)
        {
            bool flag = false;
            if (!DataValidator.IsValidId(id))
            {
                return flag;
            }
            if (id.IndexOf(',') > 0)
            {
                foreach (string str in id.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    flag = dal.Delete(DataConverter.CLng(str));
                    if (!flag)
                    {
                        return flag;
                    }
                }
                return flag;
            }
            return dal.Delete(DataConverter.CLng(id));
        }

        public static bool Disabled(int itemId)
        {
            return dal.Disabled(itemId);
        }

        public static bool Exists(string itemName)
        {
            return dal.Exists(DataSecurity.FilterBadChar(itemName));
        }

        public static bool ExistsCreateHtml(string itemIds)
        {
            if (string.IsNullOrEmpty(itemIds))
            {
                return false;
            }
            return dal.ExistsCreateHtml(DataSecurity.FilterBadChar(itemIds));
        }

        public static IList<CollectionItemInfo> GetCollectionList(string itemId)
        {
            return dal.GetCollectionList(DataSecurity.FilterBadChar(itemId));
        }

        public static DataTable GetCollectionList(int startRowIndexId, int maxNumberRows)
        {
            return dal.GetCollectionList(startRowIndexId, maxNumberRows);
        }

        public static int GetCountNumber()
        {
            return dal.GetCountNumber();
        }

        public static CollectionItemInfo GetInfoById(int id)
        {
            return dal.GetInfoById(id);
        }

        public static DataTable GetList(int startRowIndexId, int maxNumberRows)
        {
            return dal.GetList(startRowIndexId, maxNumberRows);
        }

        public static int GetMaxId()
        {
            return dal.GetMaxId();
        }

        public static bool Update(CollectionItemInfo collectionItemInfo)
        {
            return dal.Update(collectionItemInfo);
        }

        public static bool UpdateCollecDate(int id)
        {
            return dal.UpdateCollecDate(id);
        }
    }
}

