namespace EasyOne.IDal.Contents
{
    using EasyOne.Enumerations;
    using EasyOne.Model.CommonModel;
    using EasyOne.Model.Contents;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public interface IContentManage
    {
        bool Add(int modelId, DataTable contentData);
        bool AddCommonModel(int modelId, string tableName, DataTable contentData);
        bool AddCommonModel(int modelId, string tableName, DataTable contentData, int generalId);
        void AddVirtualContent(int generalId, DataTable contentData);
        void AddVirtualContent(int generalId, int infoid);
        bool BatchMove(string generalIds, int nodeId);
        bool BatchUpdate(CommonModelInfo commonModelInfo, string itemId, Dictionary<string, bool> checkItem, int batchType);
        IList<CommonModelInfo> CreateAll(string nodeIdArray, int startIndex, int pageNumber);
        IList<CommonModelInfo> CreateByNotCreate(string nodeIdArray, int startIndex, int pageNumber);
        bool Delete(string generalId);
        void DeleteByNodeId(string nodeIds, int status);
        void EmptyContentArchiving();
        bool Exists(int generalId, int nodeId);
        bool ExistSameTitle(int nodeId, string title);
        IList<CommonModelInfo> GetAdvancedSearchContentList(int startRowIndexId, int maxNumberRows, string nodeIds, ModelInfo modelInfo, IList<FieldInfo> fieldList, ContentSortType sortType, int status, string searchFieldName, string searchFieldValue);
        IList<CommonModelInfo> GetCommentedCommonModelInfoListByNodeId(int startRowIndexId, int maxNumberRows, string nodeIds, int type);
        IList<CommonModelInfo> GetCommentedCommonModelInfoListByUserName(int startRowIndexId, int maxNumberRows, string nodeIds, int type, string userName);
        IList<CommonModelInfo> GetCommonInfoListByGeneralId(string generalId);
        CommonModelInfo GetCommonModelInfo(int itemId, string tableName);
        CommonModelInfo GetCommonModelInfoById(int generalId);
        IList<CommonModelInfo> GetCommonModelInfoList();
        IList<CommonModelInfo> GetCommonModelInfoList(string nodeIdArray, int number);
        IList<CommonModelInfo> GetCommonModelInfoList(string nodeIdArray, string generalIdArray);
        IList<CommonModelInfo> GetCommonModelInfoList(string nodeIdArray, DateTime beginTime, DateTime endTime, int startIndex, int pageNumber);
        IList<CommonModelInfo> GetCommonModelInfoList(string nodeIdArray, int minId, int maxId, int startIndex, int pageNumber);
        IList<CommonModelInfo> GetCommonModelInfoList(int startRowIndexId, int maxNumberRows, string nodeIds, ContentSortType sortType, int status, string roleIds);
        IList<CommonModelInfo> GetCommonModelInfoListByShop(int startRowIndexId, int maxNumberRows, string nodeIds, ContentSortType sortType, int status, string roleIds);
        DataTable GetCommonModelInfoListBySignInLog(int startRowIndexId, int maxNumberRows, string userName);
        IList<CommonModelInfo> GetCommonModelInfoListBySignInStatus(int startRowIndexId, int maxNumberRows, string userName, int signIn);
        IList<CommonModelInfo> GetCommonModelInfoListBySignInType(int startRowIndexId, int maxNumberRows, string nodeIds, int signInType, ContentSortType sortType, int status, string searchType, string keyword);
        IList<SpecialCommonModelInfo> GetCommonModelInfoListBySpecialCategoryId(int startRowIndexId, int maxNumberRows, int specialCategoryId, ContentSortType sortType, int status, string roleIds, bool showProductsOnly);
        IList<SpecialCommonModelInfo> GetCommonModelInfoListBySpecialId(int startRowIndexId, int maxNumberRows, int specialId, ContentSortType sortType, int status, string roleIds, bool showProductsOnly);
        IList<CommonModelInfo> GetCommonModelInfoListByUserName(int startRowIndexId, int maxNumberRows, string userName, string nodeIds, ContentSortType sortType, int status, string title);
        IList<CommonModelInfo> GetCommonModelListByGeneralID(string itemIDList);
        DataTable GetContentDataById(int generalId);
        DataSet GetContentList(int modelId, string nodeIds, int startIndex, int pageSize);
        int GetContentNodeId(int generalId);
        string GetContentTemplate(int infoid);
        DataTable GetCountByEditorAndMonth(int nodeId, string editor, DateTime beginDate, DateTime endDate);
        DataTable GetCountByInputerMonth(int nodeId, string userName, DateTime beginDate, DateTime endDate);
        DataTable GetCountByNodeAndEditor(int nodeId, string editor, DateTime beginDate, DateTime endDate);
        DataTable GetCountByNodeAndInputer(int nodeId, string userName, DateTime beginDate, DateTime endDate);
        DataTable GetCountByNodeAndMonth(int nodeId, DateTime beginDate, DateTime endDate);
        int GetCountBySignIn(string userName, bool isSignIn);
        int GetCountByStatus(int status);
        IList<CommonModelInfo> GetCreateHtmlCommonModelInfoList(int startRowIndexId, int maxNumberRows, string nodeIds, int created, ContentSortType sortType, bool isEshop);
        string GetGeneralIdArrByNodeId(string nodeIds, int status);
        string GetGeneralIdsByItemId(int generalId);
        DataTable GetHitsDataById(int generalId);
        IList<CommonModelInfo> GetInfoList(int generalId);
        CommonModelInfo GetNextInfo(int nodeId, int generalId);
        CommonModelInfo GetPrevInfo(int nodeId, int generalId);
        IList<CommonModelInfo> GetSearchContentList(int startRowIndexId, int maxNumberRows, string nodeIds, string modelIds, ContentSortType sortType, int status, string roleIds, string searchType, string keyword, int nodeId, IList<FieldInfo> fieldList);
        string GetTableNameByNodeID(int nodeId);
        int GetTodayPublicInfoCountByUserName(string userName);
        int GetTotalOfCommonModelInfo();
        int GetTotalOfCommonModelInfoBySpecialCategoryId();
        int GetTotalOfCommonModelInfoBySpecialId();
        DataTable GetUserContentDataById(int generalId, string userName);
        bool RecycleAll(string nodeIds);
        void ReplaceTemplateDir(string oldDir, string newDir);
        void ReplaceTemplateFileName(string replaceFormer, string replaceAfter);
        bool Update(int generalId, DataTable contentData);
        int UpdateBrowseTimes(int generalId);
        bool UpdateByUser(int generalId, DataTable contentData, string userName);
        bool UpdateCommentAuditedAndUnaudited(int generalId);
        bool UpdateCommonModel(int generalId, DataTable contentData);
        bool UpdateCreateTime(int generalId, DateTime? createTime);
        bool UpdateField(int id, string table, string fieldName, DataTable content);
        bool UpdateHits(int generalId, int hits, int dayhits, int weekhits, int monthhits, DateTime lasthittime);
        void UpdateNodeId(int nodeId, string sourceNodeIds);
        bool UpdateStatus(int generalId, int status, string editor);
        bool UpdateStatus(string generalIds, int status, string editor);
        bool UpdateStatusByUser(string generalIds, int status, string editor, string userName);
        bool UpdateTemplateFile(int generalId, string templateFile);
    }
}

