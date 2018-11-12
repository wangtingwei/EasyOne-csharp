namespace EasyOne.Accessories
{
    using EasyOne.Common;
    using EasyOne.IDal.Accessories;
    using EasyOne.Model.Accessories;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class Keywords
    {
        private static readonly IKeywords dal = DataAccess.CreateKeywords();

        private Keywords()
        {
        }

        public static bool Add(KeywordInfo keywordInfo)
        {
            return dal.Add(keywordInfo);
        }

        public static bool Delete(string id)
        {
            return (DataValidator.IsValidId(id) && dal.Delete(id));
        }

        public static bool Exists(string keywordText)
        {
            return dal.Exists(keywordText);
        }

        public static KeywordInfo GetKeywordById(int id)
        {
            return dal.GetKeywordById(id);
        }

        public static KeywordInfo GetKeywordByKeywordName(string keyword)
        {
            return dal.GetKeywordByKeywordName(keyword);
        }

        public static ArrayList GetKeywords(int number)
        {
            return dal.GetKeywords(number);
        }

        public static IList<KeywordInfo> GetList(int startRowIndexId, int maxNumberRows, int searchType, string keyword, int listType)
        {
            return dal.GetList(startRowIndexId, maxNumberRows, searchType, DataSecurity.FilterBadChar(keyword), listType);
        }

        public static string GetStrArrayKeywords(int number)
        {
            return dal.GetStrArrayKeywords(number);
        }

        public static int GetTotalOfKeyword(int searchType, string keyword, int listType)
        {
            return dal.GetTotalOfKeyword();
        }

        public static bool QueryKeyword(string keyword)
        {
            return dal.QueryKeyword(keyword);
        }

        public static bool Update(KeywordInfo keywordInfo)
        {
            return dal.Update(keywordInfo);
        }

        public static bool UpdateHitsByKeywordName(string keywordname)
        {
            return dal.UpdateHitsByKeywordName(DataSecurity.FilterBadChar(keywordname));
        }
    }
}

