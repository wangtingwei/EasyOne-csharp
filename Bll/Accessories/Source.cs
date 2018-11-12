namespace EasyOne.Accessories
{
    using EasyOne.Common;
    using EasyOne.IDal.Accessories;
    using EasyOne.Model.Accessories;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class Source
    {
        private static readonly ISource dal = DataAccess.CreateSourceInfo();

        private Source()
        {
        }

        public static bool Add(SourceInfo ainfo)
        {
            if (dal.Exists(ainfo.Name))
            {
                return false;
            }
            return dal.Add(ainfo);
        }

        public static bool Delete(string id)
        {
            return (DataValidator.IsValidId(id) && dal.Delete(id));
        }

        public static bool ExistsPassedSource(string sourceName)
        {
            bool flag = false;
            if (dal.ExistsPassedSource(DataSecurity.FilterBadChar(sourceName)))
            {
                flag = true;
            }
            return flag;
        }

        public static SourceInfo GetSourceInfoById(int id)
        {
            return dal.GetSourceInfoById(id);
        }

        public static IList<SourceInfo> GetSourceList(int startRowIndexId, int maxNumberRows, int searchType, string keyword, string sourceType)
        {
            return dal.GetSourceList(startRowIndexId, maxNumberRows, searchType, DataSecurity.FilterBadChar(keyword), DataSecurity.FilterBadChar(sourceType), false);
        }

        public static IList<SourceInfo> GetSourceList(int startRowIndexId, int maxNumberRows, int searchType, string keyword, string sourceType, bool isShowDisable)
        {
            return dal.GetSourceList(startRowIndexId, maxNumberRows, searchType, DataSecurity.FilterBadChar(keyword), DataSecurity.FilterBadChar(sourceType), isShowDisable);
        }

        public static IList<SourceInfo> GetSourceTypeList()
        {
            return dal.GetSourceTypeList();
        }

        public static int GetTotalOfSource(int searchType, string keyword, string sourceType)
        {
            return dal.GetTotalOfSource();
        }

        public static bool Update(SourceInfo ainfo)
        {
            if (string.IsNullOrEmpty(ainfo.Name))
            {
                return false;
            }
            return dal.Update(ainfo);
        }
    }
}

