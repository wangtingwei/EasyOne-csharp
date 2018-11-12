namespace EasyOne.Collection
{
    using EasyOne.Common;
    using EasyOne.IDal.Collection;
    using EasyOne.Model.Collection;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public static class CollectionExclosion
    {
        private static readonly ICollectionExclosion dal = DataAccess.CreateCollectionExclosion();

        public static bool Add(CollectionExclosionInfo collectionExclosionInfo)
        {
            return dal.Add(collectionExclosionInfo);
        }

        public static bool Delete(int id)
        {
            bool flag = dal.Delete(id);
            if (flag)
            {
                CollectionFieldRules.UpdateExclosionId(id);
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
                    flag = Delete(DataConverter.CLng(str));
                    if (!flag)
                    {
                        return flag;
                    }
                }
                return flag;
            }
            return Delete(DataConverter.CLng(id));
        }

        public static bool Exists(string exclosionName)
        {
            return dal.Exists(DataSecurity.FilterBadChar(exclosionName));
        }

        public static int GetCountNumber()
        {
            return dal.GetCountNumber();
        }

        public static CollectionExclosionInfo GetInfoById(int id)
        {
            return dal.GetInfoById(id);
        }

        public static IList<CollectionExclosionInfo> GetList(int exclosionType)
        {
            return dal.GetList(exclosionType);
        }

        public static IList<CollectionExclosionInfo> GetList(int startRowIndexId, int maxNumberRows)
        {
            return dal.GetList(startRowIndexId, maxNumberRows);
        }

        public static bool IsExclosion(CollectionExclosionInfo collectionExclosionInfo, string testContent)
        {
            if (collectionExclosionInfo.IsNull || string.IsNullOrEmpty(testContent))
            {
                return false;
            }
            switch (collectionExclosionInfo.ExclosionType)
            {
                case 1:
                    return IsExclosionString(collectionExclosionInfo, testContent);

                case 2:
                    return IsExclosionDateTime(collectionExclosionInfo, testContent);

                case 3:
                    return IsExclosionNumber(collectionExclosionInfo, testContent);
            }
            return false;
        }

        private static bool IsExclosionDateTime(CollectionExclosionInfo collectionExclosionInfo, string testContent)
        {
            DateTime? nullable = new DateTime?(DataConverter.CDate(testContent));
            if (nullable.HasValue)
            {
                if (collectionExclosionInfo.IsExclosionDesignatedDateTime && (nullable == collectionExclosionInfo.ExclosionDesignatedDateTime))
                {
                    return true;
                }
                if (collectionExclosionInfo.IsExclosionMaxDateTime)
                {
                    DateTime? nullable4 = nullable;
                    DateTime? exclosionMaxDateTime = collectionExclosionInfo.ExclosionMaxDateTime;
                    if ((nullable4.HasValue & exclosionMaxDateTime.HasValue) ? (nullable4.GetValueOrDefault() > exclosionMaxDateTime.GetValueOrDefault()) : false)
                    {
                        return true;
                    }
                }
                if (collectionExclosionInfo.IsExclosionMinDateTime)
                {
                    DateTime? nullable6 = nullable;
                    DateTime? exclosionMinDateTime = collectionExclosionInfo.ExclosionMinDateTime;
                    if ((nullable6.HasValue & exclosionMinDateTime.HasValue) ? (nullable6.GetValueOrDefault() < exclosionMinDateTime.GetValueOrDefault()) : false)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static bool IsExclosionNumber(CollectionExclosionInfo collectionExclosionInfo, string testContent)
        {
            int num = DataConverter.CLng(testContent);
            return ((collectionExclosionInfo.IsExclosionDesignatedNumber && (num == collectionExclosionInfo.ExclosionDesignatedNumber)) || ((collectionExclosionInfo.IsExclosionMaxNumber && (num > collectionExclosionInfo.ExclosionMaxNumber)) || (collectionExclosionInfo.IsExclosionMinNumber && (num < collectionExclosionInfo.ExclosionMinNumber))));
        }

        private static bool IsExclosionString(CollectionExclosionInfo collectionExclosionInfo, string testContent)
        {
            if (collectionExclosionInfo.ExclosionStringType == 1)
            {
                return IsExclosionStringType(collectionExclosionInfo, testContent);
            }
            return !IsExclosionStringType(collectionExclosionInfo, testContent);
        }

        private static bool IsExclosionStringType(CollectionExclosionInfo collectionExclosionInfo, string testContent)
        {
            bool flag = false;
            if (collectionExclosionInfo.ExclosionString.IndexOf("\r\n", StringComparison.Ordinal) > 0)
            {
                foreach (string str in collectionExclosionInfo.ExclosionString.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (!string.IsNullOrEmpty(str) && (testContent.IndexOf(str, StringComparison.Ordinal) != -1))
                    {
                        return true;
                    }
                }
                return flag;
            }
            if (testContent.IndexOf(collectionExclosionInfo.ExclosionString, StringComparison.Ordinal) != 0)
            {
                flag = true;
            }
            return flag;
        }

        public static bool Update(CollectionExclosionInfo collectionExclosionInfo)
        {
            return dal.Update(collectionExclosionInfo);
        }
    }
}

