namespace EasyOne.IDal.Contents
{
    using EasyOne.Model.Contents;
    using System;
    using System.Collections.Generic;

    public interface ISpecial
    {
        bool AddSpecial(SpecialInfo specialInfo);
        bool AddSpecialCategory(SpecialCategoryInfo specialCategoryInfo);
        bool AddToSpecialInfos(int specialId, int generalId);
        bool DeleteSpecial(int specialId);
        bool DeleteSpecialCategoryById(int specialCategoryId);
        bool DeleteSpecialCategoryIdInSpecials(int specialCategoryId);
        bool DeleteSpecialIdInSpecialInfos(string specialIds);
        bool DeleteSpecialInfoById(string specialInfoIds);
        bool DeleteSpecialInfoById(string specialInfoIds, int specialId);
        bool DeleteSpecialInfoBySpecialId(int specialId);
        bool DeleteSpecialInfos(int generalId);
        bool ExistInSpecialInfos(int generalId);
        bool ExistInSpecialInfos(int specialId, int generalId);
        bool ExistsSpecialCategoryIdInSpecials(int specialCategoryId);
        bool ExistsSpecialCategoryName(string specialCategoryName);
        bool ExistsSpecialDir(string specialDir);
        bool ExistsSpecialName(string specialName);
        int GetCountSpecial(int specialCategoryId);
        string GetGeneralIdBySpecialId(string specialId);
        string GetGeneralIdBySpecialInfoId(string specialInfoId);
        int GetMaxSpecialId();
        SpecialCategoryInfo GetSpecialCategoryInfoById(int specialCategoryId);
        IList<SpecialCategoryInfo> GetSpecialCategoryList();
        IList<SpecialCategoryInfo> GetSpecialCategoryList(string specialCategoryId);
        SpecialInfo GetSpecialInfoById(int specialId);
        string GetSpecialInfoIds(int generalId);
        IList<SpecialInfo> GetSpecialList();
        IList<SpecialInfo> GetSpecialList(int specialCategoryId);
        IList<SpecialInfo> GetSpecialList(string specialId);
        IList<SpecialInfo> GetSpecialList(int startRowIndexId, int maxNumberRows, int specialCategoryId, int listType);
        int GetTotalOfSpecial();
        void MoveSpecialInfoBySpecialId(string sourceSpecialId, int targetSpecialId);
        void ReplaceTemplateDir(string oldDir, string newDir);
        void ReplaceTemplateFileName(string replaceFormer, string replaceAfter);
        bool SpecialBatchSet(SpecialInfo specialInfo, string specialIds, Dictionary<string, bool> checkItem);
        bool UpdateNeedCreateHtml(string arrSpecialId, bool needCreateHtml);
        bool UpdateSpecial(SpecialInfo specialInfo);
        bool UpdateSpecialCategory(SpecialCategoryInfo specialCategoryInfo);
        bool UpdateSpecialCategoryNeedCreateHtml(string arrSpecialCategoryId, bool needCreateHtml);
        void UpdateSpecialIdByGeneralId(int specialId, int sourceSpecialId, string specialInfoId);
    }
}

