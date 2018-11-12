namespace EasyOne.Collection
{
    using EasyOne.Common;
    using EasyOne.IDal.Collection;
    using EasyOne.Model.Collection;
    using System;
    using System.Data;
    using EasyOne.DalFactory;

    public static class CollectionHistory
    {
        private static readonly ICollectionHistory dal = DataAccess.CreateCollectionHistory();

        public static bool Add(CollectionHistoryInfo collectionHistoryInfo)
        {
            return dal.Add(collectionHistoryInfo);
        }

        public static bool Delete()
        {
            return dal.Delete();
        }

        public static bool Delete(string id)
        {
            return dal.Delete(DataSecurity.FilterBadChar(id));
        }

        public static bool DeleteErr()
        {
            return dal.DeleteErr();
        }

        public static bool DeleteSuccess()
        {
            return dal.DeleteSuccess();
        }

        public static bool Exists(string title)
        {
            return dal.Exists(title);
        }

        public static int FailureRecord()
        {
            return dal.RecordCount(false, 0);
        }

        public static int FailureRecord(int itemd)
        {
            return dal.RecordCount(false, itemd);
        }

        public static DataTable GetCollectionHistory(int startRowIndexId, int maxNumberRows)
        {
            return dal.GetCollectionHistory(startRowIndexId, maxNumberRows);
        }

        public static int GetCountNumber()
        {
            return dal.GetCountNumber();
        }

        public static int GetMaxId()
        {
            return dal.GetMaxId();
        }

        public static int SuccessRecord()
        {
            return dal.RecordCount(true, 0);
        }

        public static int SuccessRecord(int itemd)
        {
            return dal.RecordCount(true, itemd);
        }
    }
}

