namespace EasyOne.IDal.Accessories
{
    using EasyOne.Model.Accessories;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public interface IKeywords
    {
        bool Add(KeywordInfo keywordInfo);
        bool Delete(string id);
        bool Exists(string keywordText);
        KeywordInfo GetKeywordById(int id);
        KeywordInfo GetKeywordByKeywordName(string keyword);
        ArrayList GetKeywords(int number);
        IList<KeywordInfo> GetList(int startRowIndexId, int maxNumberRows, int searchType, string keyword, int listType);
        string GetStrArrayKeywords(int number);
        int GetTotalOfKeyword();
        bool QueryKeyword(string keyword);
        bool Update(KeywordInfo keywordInfo);
        bool UpdateHitsByKeywordName(string keywordname);
    }
}

