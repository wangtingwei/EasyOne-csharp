namespace EasyOne.Contents
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Components;
    using EasyOne.Enumerations;
    using EasyOne.IDal.Contents;
    using EasyOne.Model.CommonModel;
    using EasyOne.Model.Contents;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using EasyOne.DalFactory;

    public sealed class ContentManage
    {
        private static readonly IContentManage dal = DataAccess.CreateContentManage();

        private ContentManage()
        {
        }

        public static bool Add(int modelId, DataTable contentData)
        {
            return dal.Add(modelId, contentData);
        }

        public static void AddVirtualContent(int generalId, DataTable contentData)
        {
            dal.AddVirtualContent(generalId, contentData);
        }

        public static void AddVirtualContent(int generalId, int infoid)
        {
            if ((generalId > 0) && (infoid > 0))
            {
                dal.AddVirtualContent(generalId, infoid);
            }
        }

        public static bool BatchMove(string generalIds, int nodeId)
        {
            if (DataValidator.IsValidId(generalIds))
            {
                DeleteHtmlFile(generalIds);
                return dal.BatchMove(generalIds, nodeId);
            }
            return false;
        }

        public static bool BatchUpdate(CommonModelInfo commonModelInfo, string itemId, Dictionary<string, bool> checkItem, int batchType)
        {
            if (!DataValidator.IsValidId(itemId))
            {
                return false;
            }
            if (!checkItem.ContainsValue(true))
            {
                return false;
            }
            return dal.BatchUpdate(commonModelInfo, itemId, checkItem, batchType);
        }

        private static void CommonMethod(int nodeId, ref string nodeIds, ref string roles)
        {
            bool isSuperAdmin = PEContext.Current.Admin.IsSuperAdmin;
            if (!isSuperAdmin)
            {
                roles = PEContext.Current.Admin.Roles;
            }
            nodeIds = GetNodeArrChildId(nodeId);
            if (string.IsNullOrEmpty(nodeIds) && !isSuperAdmin)
            {
                nodeIds = RolePermissions.GetRoleAllNodeId(PEContext.Current.Admin.Roles);
            }
        }

        public static string ContentHtmlName(CommonModelInfo commonModelInfo, NodeInfo node, int pageIndex)
        {
            string contentPageHtmlRule = node.ContentPageHtmlRule;
            if (string.IsNullOrEmpty(contentPageHtmlRule))
            {
                return string.Empty;
            }
            int index = contentPageHtmlRule.IndexOf(".");
            if (pageIndex > 0)
            {
                contentPageHtmlRule = contentPageHtmlRule.Substring(0, index) + "_" + pageIndex.ToString() + contentPageHtmlRule.Substring(index, contentPageHtmlRule.Length - index);
            }
            return contentPageHtmlRule.ToLower().Replace("{$categorydir}", node.ParentDir + node.NodeDir).Replace("{$year}", commonModelInfo.InputTime.Year.ToString("0000")).Replace("{$month}", commonModelInfo.InputTime.Month.ToString("00")).Replace("{$day}", commonModelInfo.InputTime.Day.ToString("00")).Replace("{$pinyinoftitle}", commonModelInfo.PinyinTitle).Replace("{$time}", commonModelInfo.InputTime.Hour.ToString("00") + commonModelInfo.InputTime.Minute.ToString("00") + commonModelInfo.InputTime.Second.ToString("00")).Replace("{$infoid}", commonModelInfo.GeneralId.ToString());
        }

        public static IList<CommonModelInfo> CreateAll(string nodeIdArray, int startIndex, int pageNumber)
        {
            if (!DataValidator.IsValidId(nodeIdArray))
            {
                return dal.CreateAll(null, startIndex, pageNumber);
            }
            return dal.CreateAll(nodeIdArray, startIndex, pageNumber);
        }

        public static IList<CommonModelInfo> CreateByNotCreate(string nodeIdArray, int startIndex, int pageNumber)
        {
            if (!DataValidator.IsValidId(nodeIdArray))
            {
                return dal.CreateByNotCreate(null, startIndex, pageNumber);
            }
            return dal.CreateByNotCreate(nodeIdArray, startIndex, pageNumber);
        }

        public static bool Delete(string generalId)
        {
            DeleteHtmlFile(generalId);
            if (!DataValidator.IsValidId(generalId))
            {
                return false;
            }
            bool flag = dal.Delete(generalId);
            if (flag)
            {
                Votes.Delete(generalId);
            }
            foreach (string str in generalId.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
            {
                int num = DataConverter.CLng(str);
                Comment.DeleteByGeneralId(num);
                Special.DeleteSpecialInfos(num);
                PermissionContent.Delete(num);
                SignInContent.Delete(num);
                SignInLog.Delete(num);
            }
            return flag;
        }

        public static void DeleteByNodeId(string nodeIds, int status)
        {
            if (DataValidator.IsValidId(nodeIds))
            {
                Delete(GetGeneralIdArrByNodeId(nodeIds, status));
            }
        }

        private static void DeleteHtmlFile(string gengeralId)
        {
            IList<CommonModelInfo> commonModelListByGeneralID = GetCommonModelListByGeneralID(gengeralId);
            string file = HttpContext.Current.Server.MapPath("~/" + VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.CreateHtmlPath));
            foreach (CommonModelInfo info in commonModelListByGeneralID)
            {
                NodeInfo cacheNodeById = EasyOne.Contents.Nodes.GetCacheNodeById(info.NodeId);
                file = file + ContentHtmlName(info, cacheNodeById, 0);
                if (FileSystemObject.IsExist(file, FsoMethod.File))
                {
                    FileSystemObject.Delete(file, FsoMethod.File);
                }
            }
        }

        public static string DeleteUnusefualFile(string content, string defaultPicUrl, string uploadFiles)
        {
            string file = "";
            if (uploadFiles.IndexOf('|') > 1)
            {
                string[] strArray = uploadFiles.Split(new string[] { "|" }, StringSplitOptions.None);
                uploadFiles = "";
                foreach (string str2 in strArray)
                {
                    if ((content.IndexOf(str2, StringComparison.Ordinal) <= 0) && (str2 != defaultPicUrl))
                    {
                        file = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.UploadDir) + str2).Replace("/", @"\");
                        if (FileSystemObject.IsExist(file, FsoMethod.File))
                        {
                            FileSystemObject.Delete(file, FsoMethod.File);
                            HttpContext.Current.Response.Write("<li>" + str2 + "在项目中没有用到，也没有被设为首页图片，所以已经被删除！</li>");
                        }
                        else if (string.IsNullOrEmpty(uploadFiles))
                        {
                            uploadFiles = str2;
                        }
                        else
                        {
                            uploadFiles = uploadFiles + "|" + str2;
                        }
                    }
                }
            }
            else if ((content.IndexOf(uploadFiles, StringComparison.Ordinal) <= 0) && (uploadFiles != defaultPicUrl))
            {
                file = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.UploadDir) + uploadFiles).Replace("/", @"\");
                if (FileSystemObject.IsExist(file, FsoMethod.File))
                {
                    FileSystemObject.Delete(file, FsoMethod.File);
                    HttpContext.Current.Response.Write("<li>" + uploadFiles + "在项目中没有用到，也没有被设为首页图片，所以已经被删除！</li>");
                }
                uploadFiles = "";
            }
            if (string.IsNullOrEmpty(uploadFiles))
            {
                uploadFiles = defaultPicUrl;
            }
            return uploadFiles;
        }

        public static void EmptyContentArchiving()
        {
            dal.EmptyContentArchiving();
        }

        public static bool Exists(int generalId, int nodeId)
        {
            return dal.Exists(generalId, nodeId);
        }

        public static bool ExistSameTitle(int nodeId, string title)
        {
            return dal.ExistSameTitle(nodeId, title);
        }

        public static IList<CommonModelInfo> GetAdvancedSearchContentList(int startRowIndexId, int maxNumberRows, string nodeId, int modelId, ContentSortType sortType, int status)
        {
            if (((HttpContext.Current.Session["SearchFieldName"] == null) || string.IsNullOrEmpty(HttpContext.Current.Session["SearchFieldName"].ToString())) || (modelId == 0))
            {
                return new List<CommonModelInfo>();
            }
            ModelInfo cacheModelById = ModelManager.GetCacheModelById(modelId);
            if (cacheModelById.IsNull)
            {
                return new List<CommonModelInfo>();
            }
            IList<FieldInfo> fieldList = Field.GetFieldList(modelId);
            return dal.GetAdvancedSearchContentList(startRowIndexId, maxNumberRows, nodeId, cacheModelById, fieldList, sortType, status, HttpContext.Current.Session["SearchFieldName"].ToString(), HttpContext.Current.Session["SearchFieldValue"].ToString());
        }

        public static IList<CommonModelInfo> GetCommentedCommonModelInfoListByNodeId(int startRowIndexId, int maxNumberRows, int nodeId, int type)
        {
            string nodeArrChildId = GetNodeArrChildId(nodeId);
            return dal.GetCommentedCommonModelInfoListByNodeId(startRowIndexId, maxNumberRows, nodeArrChildId, type);
        }

        public static IList<CommonModelInfo> GetCommentedCommonModelInfoListByUserName(int startRowIndexId, int maxNumberRows, int nodeId, int type, string userName)
        {
            string nodeArrChildId = GetNodeArrChildId(nodeId);
            return dal.GetCommentedCommonModelInfoListByUserName(startRowIndexId, maxNumberRows, nodeArrChildId, type, userName);
        }

        public static CommonModelInfo GetCommonModelInfo(int itemId, string tableName)
        {
            return dal.GetCommonModelInfo(itemId, tableName);
        }

        public static CommonModelInfo GetCommonModelInfoById(int generalId)
        {
            return dal.GetCommonModelInfoById(generalId);
        }

        public static IList<CommonModelInfo> GetCommonModelInfoList()
        {
            return dal.GetCommonModelInfoList();
        }

        public static IList<CommonModelInfo> GetCommonModelInfoList(string nodeIds)
        {
            if (!DataValidator.IsValidId(nodeIds))
            {
                return new List<CommonModelInfo>();
            }
            return dal.GetCommonModelInfoList(0, 0, nodeIds, ContentSortType.None, -4, "");
        }

        public static IList<CommonModelInfo> GetCommonModelInfoList(string nodeIdArray, int number)
        {
            if (!DataValidator.IsValidId(nodeIdArray))
            {
                return dal.GetCommonModelInfoList(null, number);
            }
            return dal.GetCommonModelInfoList(nodeIdArray, number);
        }

        public static IList<CommonModelInfo> GetCommonModelInfoList(string nodeIdArray, string generalIdArray)
        {
            if (!DataValidator.IsValidId(nodeIdArray))
            {
                return dal.GetCommonModelInfoList(null, generalIdArray);
            }
            return dal.GetCommonModelInfoList(nodeIdArray, generalIdArray);
        }

        public static IList<CommonModelInfo> GetCommonModelInfoList(int startRowIndexId, int maxNumberRows, int nodeId, ContentSortType sortType, int status)
        {
            string nodeIds = "";
            string roles = "";
            CommonMethod(nodeId, ref nodeIds, ref roles);
            return dal.GetCommonModelInfoList(startRowIndexId, maxNumberRows, nodeIds, sortType, status, roles);
        }

        public static IList<CommonModelInfo> GetCommonModelInfoList(string nodeIdArray, DateTime beginTime, DateTime endTime, int startIndex, int pageNumber)
        {
            if (!DataValidator.IsValidId(nodeIdArray))
            {
                return dal.GetCommonModelInfoList(null, beginTime, endTime, startIndex, pageNumber);
            }
            return dal.GetCommonModelInfoList(nodeIdArray, beginTime, endTime, startIndex, pageNumber);
        }

        public static IList<CommonModelInfo> GetCommonModelInfoList(string nodeIdArray, int minId, int maxId, int startIndex, int pageNumber)
        {
            if (!DataValidator.IsValidId(nodeIdArray))
            {
                return dal.GetCommonModelInfoList(null, minId, maxId, startIndex, pageNumber);
            }
            return dal.GetCommonModelInfoList(nodeIdArray, minId, maxId, startIndex, pageNumber);
        }

        public static IList<CommonModelInfo> GetCommonModelInfoListByShop(int startRowIndexId, int maxNumberRows, int nodeId, ContentSortType sortType, int status)
        {
            string nodeIds = "";
            string roles = "";
            CommonMethod(nodeId, ref nodeIds, ref roles);
            return dal.GetCommonModelInfoListByShop(startRowIndexId, maxNumberRows, nodeIds, sortType, status, roles);
        }

        public static IList<SpecialCommonModelInfo> GetCommonModelInfoListByShop(int startRowIndexId, int maxNumberRows, int specialId, int specialCategoryId, ContentSortType sortType, int status)
        {
            string roleIds = "";
            if (!PEContext.Current.Admin.IsSuperAdmin)
            {
                roleIds = PEContext.Current.Admin.Roles;
            }
            if ((specialCategoryId > 0) && (specialId == 0))
            {
                return dal.GetCommonModelInfoListBySpecialCategoryId(startRowIndexId, maxNumberRows, specialCategoryId, sortType, status, roleIds, true);
            }
            return dal.GetCommonModelInfoListBySpecialId(startRowIndexId, maxNumberRows, specialId, sortType, status, roleIds, true);
        }

        public static DataTable GetCommonModelInfoListBySignInLog(int startRowIndexId, int maxNumberRows, string userName)
        {
            return dal.GetCommonModelInfoListBySignInLog(startRowIndexId, maxNumberRows, DataSecurity.FilterBadChar(userName));
        }

        public static IList<CommonModelInfo> GetCommonModelInfoListBySignInStatus(int startRowIndexId, int maxNumberRows, string userName, int signIn)
        {
            return dal.GetCommonModelInfoListBySignInStatus(startRowIndexId, maxNumberRows, DataSecurity.FilterBadChar(userName), signIn);
        }

        public static IList<CommonModelInfo> GetCommonModelInfoListBySignInType(int startRowIndexId, int maxNumberRows, int nodeId, int signInType, ContentSortType sortType, int status, string searchType, string keyword)
        {
            string nodeArrChildId = GetNodeArrChildId(nodeId);
            return dal.GetCommonModelInfoListBySignInType(startRowIndexId, maxNumberRows, nodeArrChildId, signInType, sortType, status, searchType, DataSecurity.FilterBadChar(keyword));
        }

        public static IList<SpecialCommonModelInfo> GetCommonModelInfoListBySpecialCategoryId(int startRowIndexId, int maxNumberRows, int specialCategoryId, ContentSortType sortType, int status)
        {
            string roleIds = "";
            if (!PEContext.Current.Admin.IsSuperAdmin)
            {
                roleIds = PEContext.Current.Admin.Roles;
            }
            return dal.GetCommonModelInfoListBySpecialCategoryId(startRowIndexId, maxNumberRows, specialCategoryId, sortType, status, roleIds, false);
        }

        public static IList<SpecialCommonModelInfo> GetCommonModelInfoListBySpecialId(int startRowIndexId, int maxNumberRows, int specialId, ContentSortType sortType, int status)
        {
            string roleIds = "";
            if (!PEContext.Current.Admin.IsSuperAdmin)
            {
                roleIds = PEContext.Current.Admin.Roles;
            }
            return dal.GetCommonModelInfoListBySpecialId(startRowIndexId, maxNumberRows, specialId, sortType, status, roleIds, false);
        }

        public static IList<SpecialCommonModelInfo> GetCommonModelInfoListBySpecialIdOrSpecialCategoryId(int startRowIndexId, int maxNumberRows, int specialId, int specialCategoryId, ContentSortType sortType, int status)
        {
            IList<SpecialCommonModelInfo> list = new List<SpecialCommonModelInfo>();
            if ((specialCategoryId > 0) && (specialId == 0))
            {
                return GetCommonModelInfoListBySpecialCategoryId(startRowIndexId, maxNumberRows, specialCategoryId, sortType, status);
            }
            return GetCommonModelInfoListBySpecialId(startRowIndexId, maxNumberRows, specialId, sortType, status);
        }

        public static IList<CommonModelInfo> GetCommonModelInfoListByUserName(int startRowIndexId, int maxNumberRows, string userName, int nodeId, ContentSortType sortType, int status, string title)
        {
            string nodeArrChildId = GetNodeArrChildId(nodeId);
            return dal.GetCommonModelInfoListByUserName(startRowIndexId, maxNumberRows, userName, nodeArrChildId, sortType, status, title);
        }

        public static IList<CommonModelInfo> GetCommonModelListByGeneralID(string itemIDList)
        {
            if (!string.IsNullOrEmpty(itemIDList))
            {
                return dal.GetCommonModelListByGeneralID(itemIDList);
            }
            return new List<CommonModelInfo>();
        }

        public static DataTable GetContentDataById(int generalId)
        {
            return dal.GetContentDataById(generalId);
        }

        public static DataSet GetContentList(int modelId, string nodeIds, int startIndex, int pageSize)
        {
            return dal.GetContentList(modelId, nodeIds, startIndex, pageSize);
        }

        public static int GetContentNodeId(int generalId)
        {
            return dal.GetContentNodeId(generalId);
        }

        public static DataTable GetCountByEditorAndMonth(int nodeId, string editor, DateTime beginDate, DateTime endDate)
        {
            return dal.GetCountByEditorAndMonth(nodeId, editor, beginDate, endDate);
        }

        public static DataTable GetCountByInputerAndMonth(int nodeId, string userName, DateTime beginDate, DateTime endDate)
        {
            return dal.GetCountByInputerMonth(nodeId, userName, beginDate, endDate);
        }

        public static DataTable GetCountByNodeAndEditor(int nodeId, string editor, DateTime beginDate, DateTime endDate)
        {
            return dal.GetCountByNodeAndEditor(nodeId, editor, beginDate, endDate);
        }

        public static DataTable GetCountByNodeAndInputer(int nodeId, string userName, DateTime beginDate, DateTime endDate)
        {
            return dal.GetCountByNodeAndInputer(nodeId, userName, beginDate, endDate);
        }

        public static DataTable GetCountByNodeAndMonth(int nodeId, DateTime beginDate, DateTime endDate)
        {
            return dal.GetCountByNodeAndMonth(nodeId, beginDate, endDate);
        }

        public static int GetCountBySignIn(string userName, bool isSignIn)
        {
            return dal.GetCountBySignIn(userName, isSignIn);
        }

        public static int GetCountByStatus(int status)
        {
            return dal.GetCountByStatus(status);
        }

        public static IList<CommonModelInfo> GetCreateHtmlCommonModelInfoList(int startRowIndexId, int maxNumberRows, int nodeId, int created, ContentSortType sortType, bool isEshop)
        {
            string nodeArrChildId = GetNodeArrChildId(nodeId);
            return dal.GetCreateHtmlCommonModelInfoList(startRowIndexId, maxNumberRows, nodeArrChildId, created, sortType, isEshop);
        }

        private static DataRow[] GetDataRow(DataTable dataTable, string filter)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                return dataTable.Select(filter);
            }
            return dataTable.Select();
        }

        private static FieldInfo GetFieldInfoByName(IList<FieldInfo> fieldList, string name)
        {
            foreach (FieldInfo info in fieldList)
            {
                if (string.Compare(info.FieldName, name, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    return info;
                }
            }
            return new FieldInfo(true);
        }

        public static string GetGeneralIdArrByNodeId(string nodeIds, int status)
        {
            if (!DataValidator.IsValidId(nodeIds))
            {
                return string.Empty;
            }
            return dal.GetGeneralIdArrByNodeId(nodeIds, status);
        }

        public static string GetGeneralIdsByItemId(int generalId)
        {
            if (generalId <= 0)
            {
                return string.Empty;
            }
            return dal.GetGeneralIdsByItemId(generalId);
        }

        public static DataTable GetHitsDataById(int generalId)
        {
            return dal.GetHitsDataById(generalId);
        }

        public static IList<CommonModelInfo> GetInfoList(int generalId)
        {
            return dal.GetInfoList(generalId);
        }

        public static DataTable GetNewContentData(DataTable dataTable)
        {
            DataRow[] dataRow = GetDataRow(dataTable, "FieldName = 'nodeid'");
            DataRow[] rowArray2 = GetDataRow(dataTable, "FieldName = 'infoid'");
            string str = dataRow[0]["FieldValue"].ToString();
            string str2 = rowArray2[0]["FieldValue"].ToString();
            StringBuilder sb = new StringBuilder("");
            string[] strArray = str2.Split(new char[] { ',' });
            for (int i = 0; i < strArray.Length; i++)
            {
                if (str != strArray[i])
                {
                    StringHelper.AppendString(sb, strArray[i]);
                }
            }
            if (sb.Length < 1)
            {
                rowArray2[0]["FieldValue"] = "";
                return dataTable;
            }
            rowArray2[0]["FieldValue"] = sb.ToString();
            return dataTable;
        }

        public static CommonModelInfo GetNextInfo(int nodeId, int generalId)
        {
            return dal.GetNextInfo(nodeId, generalId);
        }

        private static string GetNodeArrChildId(int nodeId)
        {
            string arrChildId = string.Empty;
            if (nodeId > 0)
            {
                arrChildId = EasyOne.Contents.Nodes.GetCacheNodeById(nodeId).ArrChildId;
            }
            return arrChildId;
        }

        public static CommonModelInfo GetPrevInfo(int nodeId, int generalId)
        {
            return dal.GetPrevInfo(nodeId, generalId);
        }

        public static IList<CommonModelInfo> GetSearchContentList(int startRowIndexId, int maxNumberRows, int nodeId, ContentSortType sortType, int status, string searchType, string keyword)
        {
            string nodeIds = "";
            string roles = "";
            CommonMethod(nodeId, ref nodeIds, ref roles);
            string cacheContentModelIdList = ModelManager.GetCacheContentModelIdList();
            if (!string.IsNullOrEmpty(keyword) && (nodeId > 0))
            {
                return dal.GetSearchContentList(startRowIndexId, maxNumberRows, nodeIds, cacheContentModelIdList, sortType, status, roles, searchType, keyword, nodeId, ModelManager.GetNodeFieldList(nodeId));
            }
            return dal.GetSearchContentList(startRowIndexId, maxNumberRows, nodeIds, cacheContentModelIdList, sortType, status, roles, searchType, keyword, nodeId, null);
        }

        public static string GetStatusShow(string status)
        {
            int num = DataConverter.CLng(status);
            switch (num)
            {
                case -3:
                    return "回收站中";

                case -2:
                    return "退稿";

                case -1:
                    return "草稿";

                case 0:
                    return "待审核";

                case 0x63:
                    return "终审通过";
            }
            return "审核中";
        }

        public static int GetTodayPublicInfoCountByUserName(string userName)
        {
            return dal.GetTodayPublicInfoCountByUserName(userName);
        }

        public static int GetTotalOfCommentedCommonModelInfo(int nodeId, int type)
        {
            return dal.GetTotalOfCommonModelInfo();
        }

        public static int GetTotalOfCommentedCommonModelInfoByUserName(int nodeId, int type, string userName)
        {
            return dal.GetTotalOfCommonModelInfo();
        }

        public static int GetTotalOfCommonModelInfo(int nodeId, ContentSortType sortType, int status)
        {
            return dal.GetTotalOfCommonModelInfo();
        }

        public static int GetTotalOfCommonModelInfo(string nodeId, int modelId, ContentSortType sortType, int status)
        {
            return dal.GetTotalOfCommonModelInfo();
        }

        public static int GetTotalOfCommonModelInfo(int nodeId, ContentSortType sortType, int status, string searchType, string keyword)
        {
            return dal.GetTotalOfCommonModelInfo();
        }

        public static int GetTotalOfCommonModelInfoBySignInLog(string userName)
        {
            return dal.GetTotalOfCommonModelInfo();
        }

        public static int GetTotalOfCommonModelInfoBySignInType(int nodeId, int signInType, ContentSortType sortType, int status, string searchType, string keyword)
        {
            return dal.GetTotalOfCommonModelInfo();
        }

        public static int GetTotalOfCommonModelInfoBySpecialCategoryId(int specialCategoryId, ContentSortType sortType, int status)
        {
            return dal.GetTotalOfCommonModelInfoBySpecialCategoryId();
        }

        public static int GetTotalOfCommonModelInfoBySpecialId(int specialId, ContentSortType sortType, int status)
        {
            return dal.GetTotalOfCommonModelInfoBySpecialId();
        }

        public static int GetTotalOfCommonModelInfoBySpecialIdOrSpecialCategoryId(int specialId, int specialCategoryId, ContentSortType sortType, int status)
        {
            if ((specialCategoryId > 0) && (specialId == 0))
            {
                return GetTotalOfCommonModelInfoBySpecialCategoryId(specialCategoryId, sortType, status);
            }
            return GetTotalOfCommonModelInfoBySpecialId(specialId, sortType, status);
        }

        public static int GetTotalOfCommonModelInfoByUserName(string userName, int nodeId, ContentSortType sortType, int status, string title)
        {
            return dal.GetTotalOfCommonModelInfo();
        }

        public static int GetTotalOfCreateHtmlCommonModelInfo(int nodeId, int created, ContentSortType sortType, bool isEshop)
        {
            return dal.GetTotalOfCommonModelInfo();
        }

        public static DataTable GetUserContentDataById(int generalId)
        {
            string userName = PEContext.Current.User.UserName;
            return dal.GetUserContentDataById(generalId, userName);
        }

        private static string PathReplaceLable(string content)
        {
            string input = content;
            string pattern = @"{PE\.SiteConfig\.ApplicationPath\/}";
            foreach (Match match in Regex.Matches(input, pattern, RegexOptions.IgnoreCase))
            {
                input = input.Replace(match.Value, SiteConfig.SiteInfo.VirtualPath);
            }
            pattern = @"{PE\.SiteConfig\.uploaddir\/}";
            foreach (Match match2 in Regex.Matches(input, pattern, RegexOptions.IgnoreCase))
            {
                input = input.Replace(match2.Value, SiteConfig.SiteOption.UploadDir);
            }
            return input;
        }

        private static void PresentExp(CommonModelInfo commonInfo, int status)
        {
            UserInfo usersByUserName = Users.GetUsersByUserName(commonInfo.Inputer);
            if (!usersByUserName.IsNull)
            {
                UserPurviewInfo userPurview = usersByUserName.UserPurview;
                if (userPurview.IsNull)
                {
                    userPurview.MaxPublicInfoOneDay = -1;
                    userPurview.GetExp = 1;
                }
                if (userPurview.GetExp == 0)
                {
                    userPurview.GetExp = 1;
                }
                int num = EasyOne.Contents.Nodes.GetCacheNodeById(commonInfo.NodeId).Settings.PresentExp * userPurview.GetExp;
                if ((status == 0x63) && (commonInfo.Status < 0x63))
                {
                    usersByUserName.UserExp += num;
                    usersByUserName.PassedItems++;
                }
                if ((commonInfo.Status == 0x63) && (status < 0x63))
                {
                    usersByUserName.UserExp -= num;
                    if (status == -2)
                    {
                        usersByUserName.RejectItems++;
                    }
                    if (status == -3)
                    {
                        usersByUserName.DelItems++;
                    }
                }
                Users.Update(usersByUserName);
            }
        }

        public static string RebuildArr(string idArray)
        {
            StringBuilder sb = new StringBuilder();
            string[] strArray = idArray.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < strArray.Length; i++)
            {
                bool flag = true;
                for (int j = 0; j < i; j++)
                {
                    if (strArray[i] == strArray[j])
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    StringHelper.AppendString(sb, strArray[i]);
                }
            }
            return sb.ToString();
        }

        public static bool RecycleAll(string nodeIds)
        {
            if (!DataValidator.IsValidId(nodeIds))
            {
                return false;
            }
            return dal.RecycleAll(nodeIds);
        }

        public static DataTable SavePemotePic(DataTable dtContent, NodeInfo nodeInfo, string content)
        {
            string str = VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.UploadDir);
            string str2 = dtContent.Rows[0][content].ToString();
            string str3 = dtContent.Rows[0]["DefaultPicUrl"].ToString();
            StringBuilder builder = new StringBuilder();
            builder.Append(dtContent.Rows[0]["UploadFiles"].ToString());
            str2 = PathReplaceLable(str2);
            WebClient client = new WebClient();
            MatchCollection matchs = new Regex(@"((http|https|ftp|rtsp|mms):(\/\/|\\\\){1}([\w\-]+[.]){1,}(net|com|cn|org|cc|tv|[0-9]{1,3})(([^.])+[.]{1}(gif|jpg|jpeg|jpe|bmp|png)))", RegexOptions.IgnoreCase).Matches(str2);
            if (matchs.Count > 0)
            {
                HttpContext.Current.Response.Write("字段" + content + "：总共有 <span style='color:blue'><b>" + matchs.Count.ToString() + " </b></span>张远程图片， 请等待不要刷新！<br/>");
            }
            int num = 0;
            foreach (Match match in matchs)
            {
                num++;
                string path = match.Groups[1].Value;
                try
                {
                    string str5 = DataSecurity.MakeFileRndName();
                    string str6 = Path.GetExtension(path).ToLower();
                    string str7 = EasyOne.Contents.Nodes.UploadPathParse(nodeInfo, str5 + str6).Replace("//", "/");
                    StringBuilder builder2 = new StringBuilder();
                    builder2.Append(FileSystemObject.CreateFileFolder(str + str7, HttpContext.Current));
                    builder2.Append(str5 + str6);
                    string str8 = str7 + str5 + str6;
                    client.DownloadFile(path, builder2.ToString());
                    str2 = str2.Replace(path, "{PE.SiteConfig.ApplicationPath/}{PE.SiteConfig.uploaddir/}/" + str8);
                    if (builder.Length > 0)
                    {
                        builder.Append("|" + str8);
                    }
                    else
                    {
                        builder.Append(str8);
                    }
                    if (string.IsNullOrEmpty(str3))
                    {
                        str3 = str8;
                    }
                    HttpContext.Current.Response.Write("保存远程图片第 <span style='color:blue'><b>" + num + " </b></span> 张成功。<br/>");
                }
                catch (WebException)
                {
                    HttpContext.Current.Response.Write("获取保存远程图片第 <b>" + num + " </b> 张失败。<br/>");
                }
                catch (Exception)
                {
                    HttpContext.Current.Response.Write("获取保存远程图片第 <b>" + num + " </b> 张失败。<br/>");
                }
                client.Dispose();
                HttpContext.Current.Response.Flush();
            }
            str2 = str2.Replace(SiteConfig.SiteOption.UploadDir, "{PE.SiteConfig.uploaddir/}");
            if (string.CompareOrdinal(SiteConfig.SiteInfo.VirtualPath, "/") != 0)
            {
                str2 = str2.Replace(SiteConfig.SiteInfo.VirtualPath, "{PE.SiteConfig.ApplicationPath/}");
            }
            dtContent.Rows[0][content] = str2;
            dtContent.Rows[0]["DefaultPicUrl"] = str3;
            dtContent.Rows[0]["UploadFiles"] = builder.ToString();
            return dtContent;
        }

        private static void SavePresentExp(string generalIds, int status)
        {
            foreach (CommonModelInfo info in dal.GetCommonInfoListByGeneralId(generalIds))
            {
                PresentExp(info, status);
            }
        }

        public static string ToFieldType(string fieldValue, FieldType fieldtype)
        {
            string str = "";
            Dictionary<FieldType, string> dictionary = SiteCache.Get("CK_Content_Dictionary_ConvertFieldType") as Dictionary<FieldType, string>;
            if (dictionary == null)
            {
                dictionary = new Dictionary<FieldType, string>();
                dictionary.Add(FieldType.TextType, "Original");
                dictionary.Add(FieldType.MultipleTextType, "Original");
                dictionary.Add(FieldType.MultipleHtmlTextType, "Original");
                dictionary.Add(FieldType.ListBoxType, "Original");
                dictionary.Add(FieldType.NumberType, "Original");
                dictionary.Add(FieldType.MoneyType, "Original");
                dictionary.Add(FieldType.DateTimeType, "Original");
                dictionary.Add(FieldType.LookType, "Original");
                dictionary.Add(FieldType.LinkType, "Original");
                dictionary.Add(FieldType.BoolType, "ConvertBool");
                dictionary.Add(FieldType.CountType, "Original");
                dictionary.Add(FieldType.PictureType, "Original");
                dictionary.Add(FieldType.FileType, "Original");
                dictionary.Add(FieldType.MultiplePhotoType, "Original");
                dictionary.Add(FieldType.ColorType, "Original");
                dictionary.Add(FieldType.NodeType, "Original");
                dictionary.Add(FieldType.TemplateType, "Original");
                dictionary.Add(FieldType.InfoType, "Original");
                dictionary.Add(FieldType.SpecialType, "Original");
                dictionary.Add(FieldType.AuthorType, "Original");
                dictionary.Add(FieldType.SourceType, "Original");
                dictionary.Add(FieldType.KeywordType, "Original");
                dictionary.Add(FieldType.OperatingType, "Original");
                dictionary.Add(FieldType.SkinType, "Original");
                dictionary.Add(FieldType.StatusType, "Original");
                dictionary.Add(FieldType.Producer, "Original");
                dictionary.Add(FieldType.Trademark, "Original");
                dictionary.Add(FieldType.ContentType, "Original");
                dictionary.Add(FieldType.TitleType, "Original");
                dictionary.Add(FieldType.DownServerType, "Original");
                SiteCache.Insert("CK_Content_Dictionary_ConvertFieldType", dictionary);
            }
            string str2 = dictionary[fieldtype];
            if (str2 == null)
            {
                return str;
            }
            if (!(str2 == "ConvertBool"))
            {
                if (str2 != "Original")
                {
                    return str;
                }
            }
            else
            {
                if (string.Compare(fieldValue, "true", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    return "1";
                }
                return "0";
            }
            return fieldValue;
        }

        public static bool Update(int generalId, DataTable contentData)
        {
            return dal.Update(generalId, contentData);
        }

        public static int UpdateBrowseTimes(int generalId)
        {
            if (generalId <= 0)
            {
                return 0;
            }
            return dal.UpdateBrowseTimes(generalId);
        }

        public static bool UpdateByUser(int generalId, DataTable contentData)
        {
            string userName = PEContext.Current.User.UserName;
            return dal.UpdateByUser(generalId, contentData, userName);
        }

        public static bool UpdateCommentAuditedAndUnaudited(int generalId)
        {
            return dal.UpdateCommentAuditedAndUnaudited(generalId);
        }

        public static bool UpdateCreateTime(int generalId, DateTime? createTime)
        {
            return dal.UpdateCreateTime(generalId, createTime);
        }

        public static void UpdateDataSet(DataSet ds, int modeId)
        {
            IList<FieldInfo> fieldListByModelId = ModelManager.GetFieldListByModelId(modeId);
            DataTable contentData = new DataTable();
            contentData.Columns.Add("FieldName");
            contentData.Columns.Add("FieldValue");
            contentData.Columns.Add("FieldType");
            contentData.Columns.Add("FieldLevel");
            foreach (DataRow row2 in ds.Tables[0].Rows)
            {
                int generalId = DataConverter.CLng(row2["GeneralID"]);
                foreach (DataColumn column in ds.Tables[0].Columns)
                {
                    if ((((((string.Compare(column.ColumnName, "GeneralID", StringComparison.OrdinalIgnoreCase) != 0) && (string.Compare(column.ColumnName, "ModelId", StringComparison.OrdinalIgnoreCase) != 0)) && ((string.Compare(column.ColumnName, "ItemId", StringComparison.OrdinalIgnoreCase) != 0) && (string.Compare(column.ColumnName, "TableName", StringComparison.OrdinalIgnoreCase) != 0))) && (((string.Compare(column.ColumnName, "Inputer", StringComparison.OrdinalIgnoreCase) != 0) && (string.Compare(column.ColumnName, "LinkType", StringComparison.OrdinalIgnoreCase) != 0)) && ((string.Compare(column.ColumnName, "CreateTime", StringComparison.OrdinalIgnoreCase) != 0) && (string.Compare(column.ColumnName, "CommentAudited", StringComparison.OrdinalIgnoreCase) != 0)))) && ((((string.Compare(column.ColumnName, "CommentUnAudited", StringComparison.OrdinalIgnoreCase) != 0) && (string.Compare(column.ColumnName, "SigninType", StringComparison.OrdinalIgnoreCase) != 0)) && ((string.Compare(column.ColumnName, "InputTime", StringComparison.OrdinalIgnoreCase) != 0) && (string.Compare(column.ColumnName, "PassedTime", StringComparison.OrdinalIgnoreCase) != 0))) && (((string.Compare(column.ColumnName, "Editor", StringComparison.OrdinalIgnoreCase) != 0) && (string.Compare(column.ColumnName, "LastHitTime", StringComparison.OrdinalIgnoreCase) != 0)) && ((string.Compare(column.ColumnName, "ID", StringComparison.OrdinalIgnoreCase) != 0) && (string.Compare(column.ColumnName, "DefaultPicUrl", StringComparison.OrdinalIgnoreCase) != 0))))) && ((string.Compare(column.ColumnName, "UploadFiles", StringComparison.OrdinalIgnoreCase) != 0) && (string.Compare(column.ColumnName, "PinyinTitle", StringComparison.OrdinalIgnoreCase) != 0)))
                    {
                        FieldInfo fieldInfoByName = GetFieldInfoByName(fieldListByModelId, column.ColumnName);
                        if (fieldInfoByName.IsNull)
                        {
                            throw new CustomException("ID为" + modeId.ToString() + "的模型中没有找到对应的字段" + column.ColumnName + "！");
                        }
                        DataRow row = contentData.NewRow();
                        row["FieldName"] = column.ColumnName;
                        row["FieldValue"] = row2[column.ColumnName].ToString();
                        row["FieldType"] = fieldInfoByName.FieldType;
                        row["FieldLevel"] = fieldInfoByName.FieldLevel;
                        contentData.Rows.Add(row);
                    }
                }
                if (generalId > 0)
                {
                    dal.Update(generalId, contentData);
                    contentData.Rows.Clear();
                }
            }
        }

        public static bool UpdateField(int id, string table, string fieldName, DataTable dtContent)
        {
            return dal.UpdateField(id, DataSecurity.FilterBadChar(table), DataSecurity.FilterBadChar(fieldName), dtContent);
        }

        public static bool UpdateHits(int generalId, int hits, int dayhits, int weekhits, int monthhits, DateTime lasthittime)
        {
            return dal.UpdateHits(generalId, hits, dayhits, weekhits, monthhits, lasthittime);
        }

        public static void UpdateNodeId(int nodeId, string sourceNodeIds)
        {
            dal.UpdateNodeId(nodeId, sourceNodeIds);
        }

        public static bool UpdateStatus(int generalId, int status)
        {
            string editor = string.Empty;
            if (status == 0x63)
            {
                editor = PEContext.Current.Admin.AdminName;
            }
            SavePresentExp(generalId.ToString(), status);
            return dal.UpdateStatus(generalId, status, editor);
        }

        public static bool UpdateStatus(string generalIds, int status)
        {
            if (!DataValidator.IsValidId(generalIds))
            {
                return false;
            }
            string editor = string.Empty;
            if (status == 0x63)
            {
                editor = PEContext.Current.Admin.AdminName;
            }
            else
            {
                DeleteHtmlFile(generalIds);
            }
            SavePresentExp(generalIds, status);
            return dal.UpdateStatus(generalIds, status, editor);
        }

        public static bool UpdateStatusByUserName(string generalIds, int status)
        {
            if (DataValidator.IsValidId(generalIds))
            {
                string editor = string.Empty;
                if (status == 0x63)
                {
                    editor = PEContext.Current.Admin.AdminName;
                }
                if (dal.UpdateStatusByUser(generalIds, status, editor, PEContext.Current.User.UserName))
                {
                    SavePresentExp(generalIds, status);
                    return true;
                }
            }
            return false;
        }
    }
}

