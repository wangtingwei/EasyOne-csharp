namespace EasyOne.IDal.Accessories
{
    using EasyOne.Model.Accessories;
    using System;
    using System.Collections.Generic;

    public interface IWordReplace
    {
        bool Add(WordReplaceInfo wordReplaceInfo);
        bool Delete(string id);
        bool Disabled(string id);
        bool Enabled(string id);
        bool Exists(string source, int type);
        int GetCountNumber();
        WordReplaceInfo GetInfoById(int id);
        IList<WordReplaceInfo> GetInsideLink(int startRowIndexId, int maxNumberRows, string keyword, int listType);
        int GetMaxId();
        IList<WordReplaceInfo> GetWordFilterList();
        IList<WordReplaceInfo> GetWordFilterList(int startRowIndexId, int maxNumberRows, string keyword, int listType);
        bool Update(WordReplaceInfo wordReplaceInfo);
    }
}

