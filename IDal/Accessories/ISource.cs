namespace EasyOne.IDal.Accessories
{
    using EasyOne.Model.Accessories;
    using System;
    using System.Collections.Generic;

    public interface ISource
    {
        bool Add(SourceInfo sourceInfo);
        bool Delete(string strId);
        bool Exists(string sname);
        bool ExistsPassedSource(string sourceName);
        SourceInfo GetSourceInfoById(int id);
        IList<SourceInfo> GetSourceList(int startRowIndexId, int maxNumberRows, int searchType, string keyword, string sourceType, bool isShowDisable);
        IList<SourceInfo> GetSourceListByType(string type);
        IList<SourceInfo> GetSourceTypeList();
        int GetTotalOfSource();
        bool Update(SourceInfo sourceInfo);
    }
}

