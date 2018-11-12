namespace EasyOne.SqlServerDal.Contents
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.Common;
    using EasyOne.Enumerations;
    using EasyOne.IDal.Contents;
    using EasyOne.Model.CommonModel;
    using EasyOne.Model.Contents;
    using EasyOne.SqlServerDal;
    using EasyOne.SqlServerDal.CommonModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Globalization;
    using System.Text;

    public class ContentManage : IContentManage
    {
        private int m_TotalOfCommonModelInfo;
        private int m_TotalOfCommonModelInfoBySpecialCategoryId;
        private int m_TotalOfCommonModelInfoBySpecialId;

        public bool Add(int modelId, DataTable contentData)
        {
            ModelInfo modelInfoById = new ModelDal().GetModelInfoById(modelId);
            if (modelInfoById.IsNull)
            {
                return false;
            }
            string tableName = modelInfoById.TableName;
            return (this.AddCommonModel(modelId, tableName, contentData) && AddContent(tableName, contentData));
        }

        public bool AddCommonModel(int modelId, string tableName, DataTable contentData)
        {
            int newGeneralId = GetNewGeneralId();
            return this.AddCommonModel(modelId, tableName, contentData, newGeneralId);
        }

        public bool AddCommonModel(int modelId, string tableName, DataTable contentData, int generalId)
        {
            string[] filedNames = new string[] { "generalId", "InputTime", "ModelId", "TableName", "ItemId" };
            string[] filedValues = new string[] { generalId.ToString(), DateTime.Now.ToString(), modelId.ToString(), tableName, generalId.ToString() };
            FieldType[] typeArray2 = new FieldType[5];
            typeArray2[1] = FieldType.DateTimeType;
            typeArray2[3] = FieldType.TextType;
            FieldType[] fieldType = typeArray2;
            GetCommonModelDataTable(filedNames, filedValues, fieldType, contentData);
            string filter = "FieldLevel=0 AND FieldName <> 'infoid' AND FieldName <> 'specialid'";
            string strSql = Query.GetInsertTableSql("PE_CommonModel", contentData, filter);
            Parameters cmdParams = Query.GetParameters(contentData, filter);
            if (DBHelper.ExecuteSql(strSql, cmdParams))
            {
                this.AddVirtualContent(generalId, contentData);
                return AddSpecialContent(generalId, contentData);
            }
            return false;
        }

        private static bool AddContent(string tableName, DataTable contentData)
        {
            string filter = "FieldName='generalId'";
            int num = DataConverter.CLng(Query.GetDataRows(contentData, filter)[0]["FieldValue"]);
            DataRow row = contentData.NewRow();
            row["FieldName"] = "ID";
            row["FieldValue"] = num;
            row["FieldType"] = FieldType.None;
            row["FieldLevel"] = 1;
            contentData.Rows.Add(row);
            filter = "FieldLevel=1";
            string strSql = Query.GetInsertTableSql(tableName, contentData, filter);
            Parameters cmdParams = Query.GetParameters(contentData, filter);
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        private static string AddFieldValue(string fieldName, string fieldValue, IList<FieldInfo> fieldList)
        {
            string str = "";
            string str2 = "";
            string str3 = "";
            foreach (FieldInfo info in fieldList)
            {
                if (string.Compare(fieldName, info.FieldName, StringComparison.CurrentCulture) != 0)
                {
                    continue;
                }
                if (info.FieldLevel == 0)
                {
                    str2 = "C.";
                }
                else
                {
                    str2 = "M.";
                }
                str3 = str2 + fieldName.Replace("'", "");
                switch (info.FieldType)
                {
                    case FieldType.NumberType:
                    case FieldType.MoneyType:
                    {
                        str = str3 + "=" + DataConverter.CLng(fieldValue).ToString();
                        continue;
                    }
                    case FieldType.DateTimeType:
                    {
                        str = str3 + " > '" + DataConverter.CDate(fieldValue).ToShortDateString() + "' AND " + str3 + " < '" + DataConverter.CDate(fieldValue).AddDays(1.0).ToShortDateString() + "'";
                        continue;
                    }
                    case FieldType.BoolType:
                    {
                        if (!DataConverter.CBoolean(fieldValue))
                        {
                            break;
                        }
                        str = str3 + "=1";
                        continue;
                    }
                    case FieldType.SpecialType:
                    {
                        str = "S." + fieldName.Replace("'", "") + "=" + DataConverter.CLng(fieldValue).ToString();
                        continue;
                    }
                    default:
                        goto Label_018D;
                }
                str = str3 + "=0";
                continue;
            Label_018D:
                str = str3 + " LIKE '%" + DBHelper.FilterBadChar(fieldValue) + "%'";
            }
            return str;
        }

        public static bool AddSpecialContent(int generalId, DataTable contentData)
        {
            bool flag2 = true;
            string filter = "FieldName='specialid'";
            DataRow[] dataRows = Query.GetDataRows(contentData, filter);
            string str2 = "";
            if (dataRows.Length > 0)
            {
                str2 = dataRows[0]["FieldValue"].ToString();
            }
            if (!string.IsNullOrEmpty(str2))
            {
                foreach (string str3 in str2.Split(new char[] { ',' }))
                {
                    if (!AddToSpecialInfo(DataConverter.CLng(str3), generalId))
                    {
                        flag2 = false;
                    }
                }
            }
            return flag2;
        }

        private static bool AddToSpecialInfo(int specialId, int generalId)
        {
            int num = DBHelper.GetMaxId("PE_SpecialInfos", "SpecialInfoID") + 1;
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@SpecialInfoId", DbType.Int32, num);
            cmdParams.AddInParameter("@SpecialId", DbType.Int32, specialId);
            cmdParams.AddInParameter("@GeneralId", DbType.Int32, generalId);
            string strSql = "INSERT INTO PE_SpecialInfos(SpecialInfoID, SpecialID, GeneralId) VALUES (@SpecialInfoId, @SpecialId, @GeneralId)";
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public void AddVirtualContent(int generalId, DataTable contentData)
        {
            string filter = "FieldName=''";
            int num = 1;
            filter = "FieldName='infoid'";
            DataRow[] dataRows = Query.GetDataRows(contentData, filter);
            string str2 = "";
            if (dataRows.Length > 0)
            {
                str2 = dataRows[0]["FieldValue"].ToString();
            }
            if (!string.IsNullOrEmpty(str2))
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("INSERT INTO PE_CommonModel SELECT @NewGeneralID AS GeneralId, @NodeId AS NodeId, ModelId,");
                builder.Append("@ItemId AS ItemId, TableName, Title, Inputer, Hits, DayHits, WeekHits,");
                builder.Append("MonthHits, @LinkType AS LinkType, UpdateTime, CreateTime, TemplateFile, Status,");
                builder.Append("EliteLevel, Priority, CommentAudited, CommentUnAudited, SigninType, InputTime, PassedTime, Editor, LastHitTime, DefaultPicUrl, UploadFiles, PinyinTitle FROM PE_CommonModel AS PE_CM WHERE PE_CM.GeneralId = @GeneralID");
                string[] strArray = str2.Split(new char[] { ',' });
                Database database = DatabaseFactory.CreateDatabase();
                DbCommand sqlStringCommand = database.GetSqlStringCommand(builder.ToString());
                database.AddInParameter(sqlStringCommand, "@NewGeneralID", DbType.Int32);
                database.AddInParameter(sqlStringCommand, "@GeneralID", DbType.Int32);
                database.AddInParameter(sqlStringCommand, "@LinkType", DbType.Int32);
                database.AddInParameter(sqlStringCommand, "@NodeId", DbType.Int32);
                database.AddInParameter(sqlStringCommand, "@ItemId", DbType.Int32);
                database.SetParameterValue(sqlStringCommand, "@GeneralID", generalId);
                database.SetParameterValue(sqlStringCommand, "@LinkType", num);
                database.SetParameterValue(sqlStringCommand, "@ItemId", generalId);
                foreach (string str3 in strArray)
                {
                    database.SetParameterValue(sqlStringCommand, "@NewGeneralID", GetNewGeneralId());
                    database.SetParameterValue(sqlStringCommand, "@NodeId", DataConverter.CLng(str3));
                    database.ExecuteNonQuery(sqlStringCommand);
                }
            }
        }

        public void AddVirtualContent(int generalId, int infoid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO PE_CommonModel SELECT @NewGeneralID AS GeneralId, @NodeId AS NodeId, ModelId,");
            builder.Append("@ItemId AS ItemId, TableName, Title, Inputer, Hits, DayHits, WeekHits,");
            builder.Append("MonthHits, @LinkType AS LinkType, UpdateTime, CreateTime, TemplateFile, Status,");
            builder.Append("EliteLevel, Priority, CommentAudited, CommentUnAudited, SigninType, InputTime, PassedTime, Editor, LastHitTime, DefaultPicUrl, UploadFiles, PinyinTitle FROM PE_CommonModel AS PE_CM WHERE PE_CM.GeneralId = @GeneralID AND PE_CM.LinkType =0");
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@NewGeneralID", DbType.Int32, GetNewGeneralId());
            cmdParams.AddInParameter("@GeneralID", DbType.Int32, generalId);
            cmdParams.AddInParameter("@LinkType", DbType.Int32, 1);
            cmdParams.AddInParameter("@NodeId", DbType.Int32, infoid);
            cmdParams.AddInParameter("@ItemId", DbType.Int32, generalId);
            DBHelper.ExecuteNonQuerySql(builder.ToString(), cmdParams);
        }

        public bool BatchMove(string generalIds, int nodeId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@NodeID", DbType.Int32, nodeId);
            cmdParams.AddInParameter("@CreateTime", DbType.DateTime, null);
            return DBHelper.ExecuteSql("UPDATE PE_CommonModel SET NodeID = @NodeID,CreateTime=@CreateTime WHERE GeneralID IN(" + DBHelper.ToValidId(generalIds) + ")", cmdParams);
        }

        public bool BatchUpdate(CommonModelInfo commonModelInfo, string itemId, Dictionary<string, bool> checkItem, int batchType)
        {
            StringBuilder builder = new StringBuilder();
            Parameters cmdParams = new Parameters();
            builder.Append("UPDATE PE_CommonModel SET ");
            if (checkItem["EliteLevel"])
            {
                builder.Append("EliteLevel = @EliteLevel,");
                cmdParams.AddInParameter("@EliteLevel", DbType.Int32, commonModelInfo.EliteLevel);
            }
            if (checkItem["Priority"])
            {
                builder.Append("Priority = @Priority,");
                cmdParams.AddInParameter("@Priority", DbType.Int32, commonModelInfo.Priority);
            }
            if (checkItem["Hits"])
            {
                builder.Append("Hits = @Hits,");
                cmdParams.AddInParameter("@Hits", DbType.Int32, commonModelInfo.Hits);
            }
            if (checkItem["DayHits"])
            {
                builder.Append("DayHits = @DayHits,");
                cmdParams.AddInParameter("@DayHits", DbType.Int32, commonModelInfo.DayHits);
            }
            if (checkItem["WeekHits"])
            {
                builder.Append("WeekHits = @WeekHits,");
                cmdParams.AddInParameter("@WeekHits", DbType.Int32, commonModelInfo.WeekHits);
            }
            if (checkItem["MonthHits"])
            {
                builder.Append("MonthHits = @MonthHits,");
                cmdParams.AddInParameter("MonthHits", DbType.Int32, commonModelInfo.MonthHits);
            }
            if (checkItem["UpdateTime"])
            {
                builder.Append("UpdateTime = @UpdateTime,");
                cmdParams.AddInParameter("@UpdateTime", DbType.DateTime, commonModelInfo.UpdateTime);
            }
            if (checkItem["TemplateFile"])
            {
                builder.Append("TemplateFile = @TemplateFile,");
                cmdParams.AddInParameter("@TemplateFile", DbType.String, commonModelInfo.TemplateFile);
            }
            if (builder.Length <= 0x1a)
            {
                return true;
            }
            builder.Remove(builder.Length - 1, 1);
            builder.Append(" WHERE ");
            if (batchType == 0)
            {
                builder.Append(" GeneralID IN ( ");
            }
            else
            {
                builder.Append(" NodeID IN ( ");
            }
            builder.Append(DBHelper.ToValidId(itemId));
            builder.Append(" )");
            return DBHelper.ExecuteSql(builder.ToString(), cmdParams);
        }

        private static CommonModelInfo CommonModelInfoFromDataReader(NullableDataReader rdr)
        {
            CommonModelInfo info = new CommonModelInfo();
            info.GeneralId = rdr.GetInt32("GeneralId");
            info.NodeId = rdr.GetInt32("NodeId");
            info.ModelId = rdr.GetInt32("ModelId");
            info.ItemId = rdr.GetInt32("ItemId");
            info.TableName = rdr.GetString("TableName");
            info.Title = rdr.GetString("Title");
            info.PinyinTitle = rdr.GetString("PinyinTitle");
            info.Inputer = rdr.GetString("Inputer");
            info.Hits = rdr.GetInt32("Hits");
            info.DayHits = rdr.GetInt32("DayHits");
            info.WeekHits = rdr.GetInt32("WeekHits");
            info.MonthHits = rdr.GetInt32("MonthHits");
            info.LinkType = rdr.GetInt32("LinkType");
            info.UpdateTime = rdr.GetDateTime("UpdateTime");
            info.CreateTime = rdr.GetNullableDateTime("CreateTime");
            info.TemplateFile = rdr.GetString("TemplateFile");
            info.Status = rdr.GetInt32("Status");
            info.EliteLevel = rdr.GetInt32("EliteLevel");
            info.Priority = rdr.GetInt32("Priority");
            info.CommentAudited = rdr.GetInt32("CommentAudited");
            info.CommentUNAudited = rdr.GetInt32("CommentUnAudited");
            info.InputTime = rdr.GetDateTime("InputTime");
            info.DefaultPicurl = rdr.GetString("DefaultPicurl");
            info.UploadFiles = rdr.GetString("UploadFiles");
            info.LastHitTime = rdr.GetNullableDateTime("LastHitTime");
            return info;
        }

        public IList<CommonModelInfo> CreateAll(string nodeIdArray, int startIndex, int pageNumber)
        {
            string filter = "M.Status = 99";
            filter = filter + GetNodeBound(nodeIdArray);
            return this.GetCommonList(filter, startIndex, pageNumber);
        }

        public IList<CommonModelInfo> CreateByNotCreate(string nodeIdArray, int startIndex, int pageNumber)
        {
            string filter = "M.Status = 99 ";
            filter = filter + GetNodeBound(nodeIdArray) + " AND (M.CreateTime IS NULL OR M.CreateTime <= M.UpdateTime) ";
            return this.GetCommonList(filter, startIndex, pageNumber);
        }

        public bool Delete(string generalId)
        {
            string[] strArray = generalId.Split(new char[] { ',' });
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@TableName", DbType.String, string.Empty);
            cmdParams.AddInParameter("@ProductID", DbType.Int32, 0);
            for (int i = 0; i < strArray.Length; i++)
            {
                CommonModelInfo commonModelInfoById = this.GetCommonModelInfoById(DataConverter.CLng(strArray[i]));
                if (!commonModelInfoById.IsNull && (commonModelInfoById.LinkType == 0))
                {
                    if (commonModelInfoById.IsEshop)
                    {
                        cmdParams.Entries[0].Value = commonModelInfoById.TableName;
                        cmdParams.Entries[1].Value = commonModelInfoById.GeneralId;
                        DBHelper.ExistsProc("PR_Shop_Product_DeleteAll", cmdParams);
                    }
                    this.DeleteVirtualContent(DataConverter.CLng(strArray[i]));
                    DBHelper.ExecuteSql(string.Concat(new object[] { "DELETE FROM ", commonModelInfoById.TableName, " WHERE ID = ", commonModelInfoById.GeneralId }));
                }
            }
            return DBHelper.ExecuteSql("DELETE FROM PE_CommonModel WHERE GeneralId IN(" + DBHelper.ToValidId(generalId) + ")");
        }

        public void DeleteByNodeId(string nodeIds, int status)
        {
            string strSql = "DELETE FROM PE_CommonModel WHERE NodeId IN(" + DBHelper.ToValidId(nodeIds) + ")";
            if (status >= -3)
            {
                strSql = strSql + "Status = " + status.ToString();
            }
            DBHelper.ExecuteNonQuerySql(strSql);
        }

        private static bool DeleteSpecialContent(int generalId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@GeneralId", DbType.Int32, generalId);
            return DBHelper.ExecuteSql("DELETE FROM PE_SpecialInfos WHERE GeneralId = @GeneralId", cmdParams);
        }

        public bool DeleteVirtualContent(int generalId)
        {
            CommonModelInfo commonModelInfoById = this.GetCommonModelInfoById(generalId);
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ItemId", DbType.Int32, commonModelInfoById.GeneralId);
            cmdParams.AddInParameter("@TableName", DbType.String, commonModelInfoById.TableName);
            return DBHelper.ExecuteSql("DELETE FROM PE_CommonModel WHERE ItemId = @ItemId AND TableName = @TableName AND LinkType = 1", cmdParams);
        }

        public void EmptyContentArchiving()
        {
            DBHelper.ExecuteSql("UPDATE PE_CommonModel SET Status = 99 WHERE Status = 100");
        }

        public bool Exists(int generalId, int nodeId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@GeneralId", DbType.Int32, generalId);
            cmdParams.AddInParameter("@NodeId", DbType.Int32, nodeId);
            string strSql = "SELECT COUNT(*) FROM PE_CommonModel WHERE NodeId = @NodeId AND GeneralId = @GeneralId";
            return DBHelper.ExistsSql(strSql, cmdParams);
        }

        public bool ExistSameTitle(int nodeId, string title)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@NodeID", DbType.Int32, nodeId);
            cmdParams.AddInParameter("@Title", DbType.String, title);
            return DBHelper.ExistsSql("SELECT COUNT(*) FROM PE_CommonModel WHERE NodeID = @NodeID AND Title = @Title", cmdParams);
        }

        public IList<CommonModelInfo> GetAdvancedSearchContentList(int startRowIndexId, int maxNumberRows, string nodeIds, ModelInfo modelInfo, IList<FieldInfo> fieldList, ContentSortType sortType, int status, string searchFieldName, string searchFieldValue)
        {
            Database database = DatabaseFactory.CreateDatabase();
            List<CommonModelInfo> list = new List<CommonModelInfo>();
            string sortColumn = "C.GeneralId";
            string str2 = "C.*";
            string sorts = "DESC";
            string str4 = "";
            string str5 = " AND ";
            str4 = " C.NodeId IN (" + nodeIds.ToString() + ") AND C.ModelID =" + modelInfo.ModelId.ToString();
            if (searchFieldName.IndexOf(',') > 0)
            {
                string[] strArray = searchFieldName.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string[] strArray2 = searchFieldValue.Split(new char[] { ',' }, StringSplitOptions.None);
                for (int i = 0; i < strArray.Length; i++)
                {
                    str4 = str4 + str5 + AddFieldValue(strArray[i], strArray2[i], fieldList);
                }
            }
            else
            {
                str4 = str4 + str5 + AddFieldValue(searchFieldName, searchFieldValue, fieldList);
            }
            if ((status < 100) && (status > -4))
            {
                str4 = string.Concat(new object[] { str4, str5, "C.Status = ", status, " " });
            }
            if (status == 100)
            {
                str4 = str4 + str5 + "C.Status <= 99 AND C.Status >= 0 ";
            }
            if (status == 0x65)
            {
                str4 = str4 + str5 + "C.Status < 99 AND C.Status >= 0 ";
            }
            if (status == 0x66)
            {
                str4 = str4 + str5 + "C.Status = 100 ";
            }
            InitContentSortColumnAndSort(sortType, ref sortColumn, ref sorts);
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetListBySortColumn");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@PrimaryColumn", DbType.String, "C.GeneralId");
            database.AddInParameter(storedProcCommand, "@SortColumnDbType", DbType.String, "int");
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, sortColumn);
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, str2);
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, sorts);
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_CommonModel C INNER JOIN " + modelInfo.TableName + " M ON C.GeneralID = M.ID LEFT JOIN PE_SpecialInfos S ON C.GeneralID = S.GeneralID");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, str4);
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    CommonModelInfo item = new CommonModelInfo();
                    item = CommonModelInfoFromDataReader(reader);
                    list.Add(item);
                }
            }
            this.m_TotalOfCommonModelInfo = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public IList<CommonModelInfo> GetCommentedCommonModelInfoListByNodeId(int startRowIndexId, int maxNumberRows, string nodeIds, int type)
        {
            Database database = DatabaseFactory.CreateDatabase();
            List<CommonModelInfo> list = new List<CommonModelInfo>();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            StringBuilder builder = new StringBuilder(" CommentAudited + CommentUnAudited > 0 ");
            switch (type)
            {
                case 1:
                    builder.Append(" AND CommentUnAudited >0");
                    break;

                case 2:
                    builder.Append(" AND CommentUnAudited = 0");
                    break;
            }
            if (!string.IsNullOrEmpty(nodeIds))
            {
                builder.Append(" AND NodeId IN (" + DBHelper.ToValidId(nodeIds.ToString()) + ") ");
            }
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "GeneralId");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "*");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_CommonModel");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, builder.ToString());
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    CommonModelInfo item = CommonModelInfoFromDataReader(reader);
                    list.Add(item);
                }
            }
            this.m_TotalOfCommonModelInfo = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public IList<CommonModelInfo> GetCommentedCommonModelInfoListByUserName(int startRowIndexId, int maxNumberRows, string nodeIds, int type, string userName)
        {
            Database database = DatabaseFactory.CreateDatabase();
            List<CommonModelInfo> list = new List<CommonModelInfo>();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            StringBuilder builder = new StringBuilder(" CommentAudited + CommentUnAudited > 0 ");
            switch (type)
            {
                case 1:
                    builder.Append(" AND CommentUnAudited >0");
                    break;

                case 2:
                    builder.Append(" AND CommentUnAudited = 0");
                    break;
            }
            if (!string.IsNullOrEmpty(nodeIds))
            {
                builder.Append(" AND NodeId IN (" + DBHelper.ToValidId(nodeIds.ToString()) + ") ");
            }
            if (!string.IsNullOrEmpty(userName))
            {
                builder.Append(" AND ((SELECT COUNT(*) FROM PE_Comment Comment WHERE Comment.UserName = '" + DBHelper.FilterBadChar(userName) + "' AND Comment.GeneralId = CM.GeneralId) > 0)");
            }
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "GeneralId");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "*");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_CommonModel CM");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, builder.ToString());
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    CommonModelInfo item = CommonModelInfoFromDataReader(reader);
                    list.Add(item);
                }
            }
            this.m_TotalOfCommonModelInfo = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public IList<CommonModelInfo> GetCommonInfoListByGeneralId(string generalId)
        {
            string strSqlText = "SELECT * FROM PE_CommonModel WHERE GeneralId IN(" + DBHelper.ToValidId(generalId) + ")";
            IList<CommonModelInfo> commonModelInfoList = new List<CommonModelInfo>();
            GetCommonModelInfo(commonModelInfoList, null, strSqlText);
            return commonModelInfoList;
        }

        private IList<CommonModelInfo> GetCommonList(string filter, int startIndex, int pageNumber)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startIndex);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, pageNumber);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "M.GeneralId");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "M.*");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, filter);
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_CommonModel M ");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, pageNumber);
            IList<CommonModelInfo> list = new List<CommonModelInfo>();
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    CommonModelInfo item = new CommonModelInfo();
                    item = CommonModelInfoFromDataReader(reader);
                    list.Add(item);
                }
            }
            this.m_TotalOfCommonModelInfo = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        private static void GetCommonModelDataTable(string[] filedNames, string[] filedValues, FieldType[] fieldType, DataTable contentData)
        {
            int length = filedNames.Length;
            for (int i = 0; i < length; i++)
            {
                DataRow[] rowArray = contentData.Select("FieldName = '" + filedNames[i] + "'");
                if (rowArray.Length == 0)
                {
                    DataRow row = contentData.NewRow();
                    row["FieldName"] = filedNames[i];
                    row["FieldValue"] = filedValues[i];
                    row["FieldType"] = fieldType[i];
                    row["FieldLevel"] = 0;
                    contentData.Rows.Add(row);
                }
                else
                {
                    rowArray[0][1] = filedValues[i];
                }
            }
        }

        public CommonModelInfo GetCommonModelInfo(int itemId, string tableName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ItemID", DbType.Int32, itemId);
            cmdParams.AddInParameter("@TableName", DbType.String, tableName);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_CommonModel WHERE ItemID = @ItemID AND TableName = @TableName", cmdParams))
            {
                if (reader.Read())
                {
                    return CommonModelInfoFromDataReader(reader);
                }
                return new CommonModelInfo(true);
            }
        }

        private static void GetCommonModelInfo(IList<CommonModelInfo> commonModelInfoList, Parameters parms, string strSqlText)
        {
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strSqlText, parms))
            {
                while (reader.Read())
                {
                    CommonModelInfo item = CommonModelInfoFromDataReader(reader);
                    commonModelInfoList.Add(item);
                }
            }
        }

        public CommonModelInfo GetCommonModelInfoById(int generalId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@GeneralId", DbType.Int32, generalId);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT C.IsEshop, M.* FROM PE_CommonModel M INNER JOIN PE_Model C ON C.ModelID = M.ModelID WHERE M.generalID = @GeneralId", cmdParams))
            {
                if (reader.Read())
                {
                    CommonModelInfo info = CommonModelInfoFromDataReader(reader);
                    info.IsEshop = reader.GetBoolean("IsEshop");
                    return info;
                }
                return new CommonModelInfo(true);
            }
        }

        public IList<CommonModelInfo> GetCommonModelInfoList()
        {
            IList<CommonModelInfo> commonModelInfoList = new List<CommonModelInfo>();
            Parameters parms = new Parameters();
            string strSqlText = "SELECT * FROM PE_CommonModel ORDER BY GeneralId DESC";
            GetCommonModelInfo(commonModelInfoList, parms, strSqlText);
            return commonModelInfoList;
        }

        public IList<CommonModelInfo> GetCommonModelInfoList(string nodeIdArray, int number)
        {
            IList<CommonModelInfo> commonModelInfoList = new List<CommonModelInfo>();
            Parameters parms = new Parameters();
            string strSql = "SELECT TOP " + number + " M.* FROM PE_CommonModel M WHERE M.Status = 99 ";
            strSql = GetNodeBound(nodeIdArray, parms, strSql) + " ORDER BY M.GeneralId DESC";
            GetCommonModelInfo(commonModelInfoList, parms, strSql);
            return commonModelInfoList;
        }

        public IList<CommonModelInfo> GetCommonModelInfoList(string nodeIdArray, string generalIdArray)
        {
            string filter = "M.Status = 99 ";
            filter = filter + GetNodeBound(nodeIdArray);
            if (generalIdArray.IndexOf(',') > 0)
            {
                filter = filter + " AND M.GeneralId IN (" + DBHelper.ToValidId(generalIdArray) + ") ";
            }
            else
            {
                filter = filter + " AND M.GeneralId =" + DBHelper.ToNumber(generalIdArray);
            }
            return this.GetCommonList(filter, 0, 0);
        }

        public IList<CommonModelInfo> GetCommonModelInfoList(string nodeIdArray, DateTime beginTime, DateTime endTime, int startIndex, int pageNumber)
        {
            string filter = "M.Status = 99 ";
            object obj2 = filter + GetNodeBound(nodeIdArray);
            filter = string.Concat(new object[] { obj2, " AND M.UpdateTime BETWEEN '", beginTime, "' AND '", endTime, "'" });
            return this.GetCommonList(filter, startIndex, pageNumber);
        }

        public IList<CommonModelInfo> GetCommonModelInfoList(string nodeIdArray, int minId, int maxId, int startIndex, int pageNumber)
        {
            string filter = "M.Status = 99 ";
            object obj2 = filter + GetNodeBound(nodeIdArray);
            filter = string.Concat(new object[] { obj2, " AND M.GeneralId BETWEEN ", minId, " AND ", maxId });
            return this.GetCommonList(filter, startIndex, pageNumber);
        }

        public IList<CommonModelInfo> GetCommonModelInfoList(int startRowIndexId, int maxNumberRows, string nodeIds, ContentSortType sortType, int status, string roleIds)
        {
            return this.GetCommonModelInfoList(startRowIndexId, maxNumberRows, nodeIds, sortType, status, roleIds, false);
        }

        private IList<CommonModelInfo> GetCommonModelInfoList(int startRowIndexId, int maxNumberRows, string nodeIds, ContentSortType sortType, int status, string roleIds, bool isEshop)
        {
            Database database = DatabaseFactory.CreateDatabase();
            List<CommonModelInfo> list = new List<CommonModelInfo>();
            string sortColumn = "C.GeneralId";
            string str2 = "C.*, M.IsEshop";
            string sorts = "DESC";
            string str4 = "";
            string str5 = "";
            if (!string.IsNullOrEmpty(nodeIds))
            {
                str4 = " C.NodeId IN (" + nodeIds.ToString() + ") ";
                str5 = " AND ";
            }
            if ((status < 100) && (status > -4))
            {
                str4 = string.Concat(new object[] { str4, str5, "C.Status = ", status, " " });
            }
            if (status == 100)
            {
                str4 = str4 + str5 + "C.Status <= 99 AND C.Status >= 0 ";
            }
            if (status == 0x65)
            {
                if (!string.IsNullOrEmpty(roleIds))
                {
                    string str6 = "SELECT StatusCode\r\n                     FROM PE_ProcessStatusCode AS psc\r\n                     WHERE   (FlowID =\r\n                                         (SELECT WorkFlowID\r\n                                          FROM PE_Nodes\r\n                                          WHERE   (NodeID = C.NodeID))) AND (ProcessID IN\r\n                                         (SELECT ProcessID\r\n                                          FROM PE_Process_Roles\r\n                                          WHERE   (FlowID = psc.FlowID) AND (RoleID IN (" + roleIds + "))))";
                    str4 = str4 + str5 + "C.Status < 99 AND C.Status >= 0 AND C.Status IN(" + str6 + ") ";
                }
                else
                {
                    str4 = str4 + str5 + "C.Status < 99 AND C.Status >= 0 ";
                }
            }
            if (status == 0x66)
            {
                str4 = str4 + str5 + "C.Status = 100 ";
            }
            InitContentSortColumnAndSort(sortType, ref sortColumn, ref sorts);
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetListBySortColumn");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@PrimaryColumn", DbType.String, "C.GeneralId");
            database.AddInParameter(storedProcCommand, "@SortColumnDbType", DbType.String, "int");
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, sortColumn);
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, str2);
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, sorts);
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_CommonModel C INNER JOIN PE_Model M ON C.ModelID = M.ModelID AND M.IsEshop = " + (isEshop ? "1" : "0"));
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, str4);
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    CommonModelInfo item = new CommonModelInfo();
                    item = CommonModelInfoFromDataReader(reader);
                    item.IsEshop = reader.GetBoolean("IsEshop");
                    list.Add(item);
                }
            }
            this.m_TotalOfCommonModelInfo = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public IList<CommonModelInfo> GetCommonModelInfoListByShop(int startRowIndexId, int maxNumberRows, string nodeIds, ContentSortType sortType, int status, string roleIds)
        {
            return this.GetCommonModelInfoList(startRowIndexId, maxNumberRows, nodeIds, sortType, status, roleIds, true);
        }

        public DataTable GetCommonModelInfoListBySignInLog(int startRowIndexId, int maxNumberRows, string userName)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DataTable table = new DataTable();
            table.Columns.Add("GeneralId");
            table.Columns.Add("NodeId");
            table.Columns.Add("Title");
            table.Columns.Add("LinkType");
            table.Columns.Add("Inputer");
            table.Columns.Add("Hits");
            table.Columns.Add("EliteLevel");
            table.Columns.Add("Priority");
            table.Columns.Add("IsSignin");
            table.Columns.Add("ModelId");
            string str = "C.GeneralId";
            string str2 = "C.*, M.GeneralId, M.UserName, M.IsSignin, M.SigninTime, M.IP";
            string str3 = "DESC";
            string str4 = "M.UserName = '" + userName.Replace("'", "") + "'";
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, str);
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, str2);
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, str3);
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, str4);
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_CommonModel C INNER JOIN PE_SigninLog M ON C.GeneralId = M.GeneralId ");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    DataRow row = table.NewRow();
                    row["GeneralId"] = reader.GetInt32("GeneralId");
                    row["NodeId"] = reader.GetInt32("NodeId");
                    row["Title"] = reader.GetString("Title");
                    row["LinkType"] = reader.GetInt32("LinkType");
                    row["Inputer"] = reader.GetString("Inputer");
                    row["Hits"] = reader.GetInt32("Hits");
                    row["EliteLevel"] = reader.GetInt32("EliteLevel");
                    row["Priority"] = reader.GetInt32("Priority");
                    row["IsSignin"] = reader.GetBoolean("IsSignin");
                    row["ModelId"] = reader.GetInt32("ModelID");
                    table.Rows.Add(row);
                }
            }
            this.m_TotalOfCommonModelInfo = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return table;
        }

        public IList<CommonModelInfo> GetCommonModelInfoListBySignInStatus(int startRowIndexId, int maxNumberRows, string userName, int signIn)
        {
            Database database = DatabaseFactory.CreateDatabase();
            List<CommonModelInfo> list = new List<CommonModelInfo>();
            string str = "C.GeneralId";
            string str2 = "C.*, M.IsEshop";
            string str3 = "DESC";
            string str4 = "";
            if (!string.IsNullOrEmpty(userName))
            {
                string str5 = "";
                switch (signIn)
                {
                    case 0:
                        str5 = "";
                        break;

                    case 1:
                        str5 = " AND IsSignin = 1";
                        break;

                    case 2:
                        str5 = " AND IsSignin = 0";
                        break;
                }
                str4 = " C.GeneralId IN (SELECT GeneralId FROM PE_SigninLog WHERE UserName = '" + userName.Replace("'", "") + "' " + str5 + " ) ";
                DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
                database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
                database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
                database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, str);
                database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, str2);
                database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, str3);
                database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_CommonModel C INNER JOIN PE_Model M ON C.ModelID = M.ModelID");
                database.AddInParameter(storedProcCommand, "@Filter", DbType.String, str4);
                database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
                using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
                {
                    while (reader.Read())
                    {
                        CommonModelInfo item = new CommonModelInfo();
                        item = CommonModelInfoFromDataReader(reader);
                        list.Add(item);
                    }
                }
                this.m_TotalOfCommonModelInfo = (int) database.GetParameterValue(storedProcCommand, "@Total");
            }
            return list;
        }

        public IList<CommonModelInfo> GetCommonModelInfoListBySignInType(int startRowIndexId, int maxNumberRows, string nodeIds, int signInType, ContentSortType sortType, int status, string searchType, string keyword)
        {
            Database database;
            string str = "";
            keyword = DataSecurity.FilterBadChar(keyword);
            if (!string.IsNullOrEmpty(keyword))
            {
                string str8 = searchType;
                if (str8 == null)
                {
                    goto Label_0065;
                }
                if (!(str8 == "Title"))
                {
                    if (str8 == "Inputer")
                    {
                        str = "C.Inputer LIKE '%" + keyword + "%'";
                        goto Label_0077;
                    }
                    goto Label_0065;
                }
                str = "C.Title LIKE '%" + keyword + "%'";
            }
            goto Label_0077;
        Label_0065:
            str = "C.Title LIKE '%" + keyword + "%'";
        Label_0077:
            database = DatabaseFactory.CreateDatabase();
            List<CommonModelInfo> list = new List<CommonModelInfo>();
            string sortColumn = "C.GeneralId";
            string str3 = "C.*";
            string sorts = "DESC";
            string str5 = "";
            string str6 = "";
            if (!string.IsNullOrEmpty(nodeIds))
            {
                str5 = " C.NodeId IN (" + DBHelper.ToValidId(nodeIds.ToString()) + ") ";
                str6 = " AND ";
            }
            if ((status < 100) && (status > -4))
            {
                str5 = string.Concat(new object[] { str5, str6, "C.Status = ", status, " " });
                str6 = " AND ";
            }
            if (status == 100)
            {
                str5 = str5 + str6 + "C.Status <= 99 AND C.Status >= 0 ";
                str6 = " AND ";
            }
            if (status == 0x65)
            {
                str5 = str5 + str6 + "C.Status < 99 AND C.Status >= 0 ";
                str6 = " AND ";
            }
            if (status == 0x66)
            {
                str5 = str5 + str6 + "C.Status = 100 ";
                str6 = " AND ";
            }
            string str7 = "";
            if ((signInType <= 0) || (signInType > 2))
            {
                str7 = "C.SigninType > 0";
            }
            else
            {
                str7 = "C.SigninType =" + signInType.ToString();
            }
            str5 = str5 + str6 + str7;
            if (!string.IsNullOrEmpty(str))
            {
                if (str5.Length > 0)
                {
                    str5 = str5 + " AND " + str;
                }
                else
                {
                    str5 = str;
                }
            }
            InitContentSortColumnAndSort(sortType, ref sortColumn, ref sorts);
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetListBySortColumn");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@PrimaryColumn", DbType.String, "C.GeneralId");
            database.AddInParameter(storedProcCommand, "@SortColumnDbType", DbType.String, "int");
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, sortColumn);
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, str3);
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, sorts);
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_CommonModel C");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, str5);
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(CommonModelInfoFromDataReader(reader));
                }
            }
            this.m_TotalOfCommonModelInfo = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public IList<SpecialCommonModelInfo> GetCommonModelInfoListBySpecialCategoryId(int startRowIndexId, int maxNumberRows, int specialCategoryId, ContentSortType sortType, int status, string roleIds, bool showProductsOnly)
        {
            Database database = DatabaseFactory.CreateDatabase();
            List<SpecialCommonModelInfo> list = new List<SpecialCommonModelInfo>();
            string sortColumn = "S.SpecialInfoID";
            string str2 = "C.*, S.SpecialInfoID, S.SpecialID";
            string sorts = "DESC";
            string str4 = "";
            string str5 = "";
            if (specialCategoryId > 0)
            {
                str4 = "S.SpecialID IN(SELECT SpecialID FROM PE_Specials WHERE SpecialCategoryID = " + specialCategoryId + ") ";
                str5 = " AND ";
            }
            if ((status < 100) && (status > -4))
            {
                str4 = string.Concat(new object[] { str4, str5, "C.Status = ", status, " " });
                str5 = " AND ";
            }
            if (status == 0x65)
            {
                if (!string.IsNullOrEmpty(roleIds))
                {
                    string str6 = "SELECT StatusCode\r\n                     FROM PE_ProcessStatusCode AS psc\r\n                     WHERE   (FlowID =\r\n                                         (SELECT WorkFlowID\r\n                                          FROM PE_Nodes\r\n                                          WHERE   (NodeID = C.NodeID))) AND (ProcessID IN\r\n                                         (SELECT ProcessID\r\n                                          FROM PE_Process_Roles\r\n                                          WHERE   (FlowID = psc.FlowID) AND (RoleID IN (" + DBHelper.ToValidId(roleIds) + "))))";
                    str4 = str4 + str5 + "C.Status < 99 AND C.Status >= 0 AND C.Status IN(" + str6 + ") ";
                }
                else
                {
                    str4 = str4 + str5 + "C.Status < 99 AND C.Status >0 ";
                }
                str5 = " AND ";
            }
            if (status == 0x66)
            {
                str4 = str4 + str5 + "C.Status = 100 ";
                str5 = " AND ";
            }
            InitSpecialContentSortColumnAndSort(sortType, ref sortColumn, ref sorts);
            string str7 = "PE_SpecialInfos S INNER JOIN PE_CommonModel C ON S.GeneralId = C.GeneralId";
            if (showProductsOnly)
            {
                str7 = str7 + " INNER JOIN PE_CommonProduct P ON S.GeneralId = P.ProductID ";
                str2 = str2 + ", P.Unit, P.Price, P.Stocks, P.EnableSale";
            }
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetListBySortColumn");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@PrimaryColumn", DbType.String, "S.SpecialInfoID");
            database.AddInParameter(storedProcCommand, "@SortColumnDbType", DbType.String, "int");
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, sortColumn);
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, str2);
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, sorts);
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, str7);
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, str4);
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    SpecialCommonModelInfo item = new SpecialCommonModelInfo();
                    item = SpecialCommonModelInfoFromDataReader(reader);
                    if (showProductsOnly)
                    {
                        item.Unit = reader.GetString("Unit");
                        item.Price = reader.GetDecimal("Price");
                        item.Stocks = reader.GetInt32("Stocks");
                        item.EnableSale = reader.GetBoolean("EnableSale");
                    }
                    list.Add(item);
                }
            }
            this.m_TotalOfCommonModelInfoBySpecialCategoryId = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public IList<SpecialCommonModelInfo> GetCommonModelInfoListBySpecialId(int startRowIndexId, int maxNumberRows, int specialId, ContentSortType sortType, int status, string roleIds, bool showProductsOnly)
        {
            Database database = DatabaseFactory.CreateDatabase();
            List<SpecialCommonModelInfo> list = new List<SpecialCommonModelInfo>();
            string sortColumn = "S.SpecialInfoID";
            string str2 = "C.*, S.SpecialInfoID, S.SpecialID";
            string sorts = "DESC";
            string str4 = "";
            string str5 = "";
            if (specialId > 0)
            {
                str4 = "S.SpecialID = " + specialId;
                str5 = " AND ";
            }
            if ((status < 100) && (status > -4))
            {
                str4 = string.Concat(new object[] { str4, str5, "C.Status = ", status, " " });
                str5 = " AND ";
            }
            if (status == 0x65)
            {
                if (!string.IsNullOrEmpty(roleIds))
                {
                    string str6 = "SELECT StatusCode\r\n                     FROM PE_ProcessStatusCode AS psc\r\n                     WHERE   (FlowID =\r\n                                         (SELECT WorkFlowID\r\n                                          FROM PE_Nodes\r\n                                          WHERE   (NodeID = C.NodeID))) AND (ProcessID IN\r\n                                         (SELECT ProcessID\r\n                                          FROM PE_Process_Roles\r\n                                          WHERE   (FlowID = psc.FlowID) AND (RoleID IN (" + DBHelper.ToValidId(roleIds) + "))))";
                    str4 = str4 + str5 + "C.Status < 99 AND C.Status >= 0 AND C.Status IN(" + str6 + ") ";
                }
                else
                {
                    str4 = str4 + str5 + "C.Status < 99 AND C.Status >0 ";
                }
                str5 = " AND ";
            }
            if (status == 0x66)
            {
                str4 = str4 + str5 + "C.Status = 100 ";
                str5 = " AND ";
            }
            str4 = str4 + str5 + "S.SpecialInfoID IS NOT NULL";
            InitSpecialContentSortColumnAndSort(sortType, ref sortColumn, ref sorts);
            string str7 = "PE_CommonModel C LEFT JOIN PE_SpecialInfos S ON C.GeneralId = S.GeneralId";
            if (showProductsOnly)
            {
                str7 = str7 + " INNER JOIN PE_CommonProduct P ON S.GeneralId = P.ProductID ";
                str2 = str2 + ", P.Unit, P.Price, P.Stocks, P.EnableSale";
            }
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetListBySortColumn");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@PrimaryColumn", DbType.String, "S.SpecialInfoID");
            database.AddInParameter(storedProcCommand, "@SortColumnDbType", DbType.String, "int");
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, sortColumn);
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, str2);
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, sorts);
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, str7);
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, str4);
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    SpecialCommonModelInfo item = new SpecialCommonModelInfo();
                    item = SpecialCommonModelInfoFromDataReader(reader);
                    if (showProductsOnly)
                    {
                        item.Unit = reader.GetString("Unit");
                        item.Price = reader.GetDecimal("Price");
                        item.Stocks = reader.GetInt32("Stocks");
                        item.EnableSale = reader.GetBoolean("EnableSale");
                    }
                    list.Add(item);
                }
            }
            this.m_TotalOfCommonModelInfoBySpecialId = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public IList<CommonModelInfo> GetCommonModelInfoListByUserName(int startRowIndexId, int maxNumberRows, string userName, string nodeIds, ContentSortType sortType, int status, string title)
        {
            Database database = DatabaseFactory.CreateDatabase();
            List<CommonModelInfo> list = new List<CommonModelInfo>();
            string sortColumn = "C.GeneralId";
            string str2 = "C.*, M.IsEshop";
            string sorts = "DESC";
            StringBuilder builder = new StringBuilder();
            builder.Append("C.Inputer = '" + DBHelper.FilterBadChar(userName) + "'");
            string str4 = " AND ";
            if (!string.IsNullOrEmpty(nodeIds))
            {
                builder.Append(str4 + "C.NodeId IN (" + DBHelper.ToValidId(nodeIds.ToString()) + ") ");
            }
            if ((status < 100) && (status > -4))
            {
                builder.Append(string.Concat(new object[] { str4, "C.Status = ", status, " " }));
            }
            if (status == 100)
            {
                builder.Append(str4 + "C.Status <= 99 AND C.Status >= -2 ");
            }
            if (status == 0x65)
            {
                builder.Append(str4 + "C.Status < 99 AND C.Status >= 0 ");
            }
            if (!string.IsNullOrEmpty(title))
            {
                builder.Append(str4 + "C.Title LIKE '%" + title.Replace("'", "''") + "%'");
            }
            InitContentSortColumnAndSort(sortType, ref sortColumn, ref sorts);
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetListBySortColumn");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@PrimaryColumn", DbType.String, "C.GeneralId");
            database.AddInParameter(storedProcCommand, "@SortColumnDbType", DbType.String, "int");
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, sortColumn);
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, str2);
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, sorts);
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_CommonModel C INNER JOIN PE_Model M ON C.ModelID = M.ModelID");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, builder.ToString());
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    CommonModelInfo item = new CommonModelInfo();
                    item = CommonModelInfoFromDataReader(reader);
                    item.IsEshop = reader.GetBoolean("IsEshop");
                    list.Add(item);
                }
            }
            this.m_TotalOfCommonModelInfo = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public IList<CommonModelInfo> GetCommonModelListByGeneralID(string itemIDList)
        {
            if (string.IsNullOrEmpty(itemIDList))
            {
                return null;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM PE_CommonModel WHERE ");
            foreach (string str in itemIDList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
            {
                builder.Append(" GeneralID = " + DBHelper.ToNumber(str) + " or");
            }
            builder.Remove(builder.Length - 2, 2);
            List<CommonModelInfo> list = new List<CommonModelInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(builder.ToString()))
            {
                while (reader.Read())
                {
                    CommonModelInfo item = new CommonModelInfo();
                    item = CommonModelInfoFromDataReader(reader);
                    list.Add(item);
                }
            }
            return list;
        }

        public DataTable GetContentDataById(int generalId)
        {
            CommonModelInfo commonModelInfoById = this.GetCommonModelInfoById(generalId);
            if (commonModelInfoById.IsNull)
            {
                DataTable table = new DataTable();
                table.Columns.Add("FieldName");
                table.Columns.Add("FieldValue");
                table.Columns.Add("FieldType");
                table.Columns.Add("FieldLevel");
                return table;
            }
            string tableName = commonModelInfoById.TableName;
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@GeneralId", DbType.Int32, generalId);
            return DBHelper.ExecuteDataSetSql("SELECT C.*, C.ItemId AS InfoId, C.ItemId AS SpecialId, T.* FROM PE_CommonModel C INNER JOIN " + DBHelper.FilterBadChar(tableName) + " T ON C.ItemID = T.ID WHERE GeneralId = @GeneralId", cmdParams).Tables[0];
        }

        public DataSet GetContentList(int modelId, string nodeIds, int startIndex, int pageSize)
        {
            string str = "";
            if (!string.IsNullOrEmpty(nodeIds))
            {
                str = " AND C.NodeID IN (" + nodeIds + ")";
            }
            ModelInfo modelInfoById = new ModelDal().GetModelInfoById(modelId);
            string storedProcedureName = "PR_Common_GetList";
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand(storedProcedureName);
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startIndex);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, pageSize);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "C.GeneralId");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "*");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, "C.ModelId = " + modelId.ToString() + str);
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_CommonModel C INNER JOIN " + modelInfoById.TableName + " T ON C.GeneralId = T.Id");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, pageSize);
            DataSet set = database.ExecuteDataSet(storedProcCommand);
            this.m_TotalOfCommonModelInfo = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return set;
        }

        public int GetContentNodeId(int generalId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@GeneralId", DbType.Int32, generalId);
            object obj2 = DBHelper.ExecuteScalarSql("SELECT NodeID FROM PE_CommonModel WHERE GeneralId = @GeneralId", cmdParams);
            if (obj2 != null)
            {
                return (int) obj2;
            }
            return 0;
        }

        public string GetContentTemplate(int infoid)
        {
            string str = string.Empty;
            if (infoid > 0)
            {
                int num = 0;
                int num2 = 0;
                string strSql = "SELECT TOP 1 GeneralID, NodeId, ModelId, TemplateFile FROM PE_CommonModel WHERE GeneralID = @GeneralId";
                Parameters cmdParams = new Parameters();
                cmdParams.AddInParameter("@GeneralId", DbType.Int32, infoid);
                using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, cmdParams))
                {
                    if (reader.Read())
                    {
                        str = reader.GetString("TemplateFile");
                        num = reader.GetInt32("NodeId");
                        num2 = reader.GetInt32("ModelId");
                    }
                }
                if (!string.IsNullOrEmpty(str))
                {
                    return str;
                }
                strSql = "SELECT TOP 1 NodeID, ModelID, DefaultTemplateFile FROM PE_Nodes_Model_Template WHERE NodeID = @NodeID AND ModelID = @ModelID";
                cmdParams = new Parameters();
                cmdParams.AddInParameter("@NodeID", DbType.Int32, num);
                cmdParams.AddInParameter("@ModelID", DbType.Int32, num2);
                using (NullableDataReader reader2 = DBHelper.ExecuteReaderSql(strSql, cmdParams))
                {
                    if (reader2.Read())
                    {
                        str = reader2.GetString("DefaultTemplateFile");
                    }
                }
            }
            return str;
        }

        public DataTable GetCountByEditorAndMonth(int nodeId, string editor, DateTime beginDate, DateTime endDate)
        {
            string format = "SELECT Editor, CONVERT(char(4), year(PassedTime))+'-'+CONVERT(char(2), month(PassedTime)) AS Date, COUNT(*) AS count\r\n                              FROM PE_CommonModel\r\n                              WHERE (PassedTime >= @BeginDate AND PassedTime< = @EndDate) AND Status = 99 {0}\r\n                              GROUP BY Editor, CONVERT(char(4), year(PassedTime))+'-'+CONVERT(char(2), month(PassedTime))";
            Parameters cmdParams = new Parameters();
            string str2 = string.Empty;
            if (!string.IsNullOrEmpty(editor))
            {
                str2 = str2 + " AND Editor = @Editor";
                cmdParams.AddInParameter("@Editor", DbType.String, editor);
            }
            if (nodeId > 0)
            {
                str2 = str2 + " AND NodeID = @NodeId ";
                cmdParams.AddInParameter("@NodeId", DbType.Int32, nodeId);
            }
            format = string.Format(format, str2);
            cmdParams.AddInParameter("@BeginDate", DbType.DateTime, beginDate);
            cmdParams.AddInParameter("@EndDate", DbType.DateTime, endDate);
            DataTable table = new DataTable();
            DataColumn column = new DataColumn("Editor", typeof(string));
            table.Columns.Add(column);
            column = new DataColumn("InputTime", typeof(string));
            table.Columns.Add(column);
            column = new DataColumn("Count", typeof(int));
            table.Columns.Add(column);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(format, cmdParams))
            {
                while (reader.Read())
                {
                    DataRow row = table.NewRow();
                    row["Editor"] = reader.GetString("Editor");
                    row["InputTime"] = reader.GetString("Date");
                    row["Count"] = reader.GetInt32("Count");
                    table.Rows.Add(row);
                }
            }
            return table;
        }

        public DataTable GetCountByInputerMonth(int nodeId, string userName, DateTime beginDate, DateTime endDate)
        {
            string format = "SELECT Inputer, CONVERT(char(4), year(InputTime))+'-'+CONVERT(char(2), month(InputTime)) AS Date, COUNT(*) AS count\r\n                              FROM PE_CommonModel\r\n                              WHERE    (InputTime >= @BeginDate AND InputTime< = @EndDate) AND Status = 99 {0}\r\n                              GROUP BY Inputer, CONVERT(char(4), year(InputTime))+'-'+CONVERT(char(2), month(InputTime))";
            Parameters cmdParams = new Parameters();
            string str2 = string.Empty;
            if (!string.IsNullOrEmpty(userName))
            {
                str2 = str2 + " AND Inputer = @UserName";
                cmdParams.AddInParameter("@UserName", DbType.String, userName);
            }
            if (nodeId > 0)
            {
                str2 = str2 + " AND NodeID = @NodeId ";
                cmdParams.AddInParameter("@NodeId", DbType.Int32, nodeId);
            }
            format = string.Format(format, str2);
            cmdParams.AddInParameter("@BeginDate", DbType.DateTime, beginDate);
            cmdParams.AddInParameter("@EndDate", DbType.DateTime, endDate);
            DataTable table = new DataTable();
            DataColumn column = new DataColumn("Inputer", typeof(string));
            table.Columns.Add(column);
            column = new DataColumn("InputTime", typeof(string));
            table.Columns.Add(column);
            column = new DataColumn("Count", typeof(int));
            table.Columns.Add(column);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(format, cmdParams))
            {
                while (reader.Read())
                {
                    DataRow row = table.NewRow();
                    row["Inputer"] = reader.GetString("Inputer");
                    row["InputTime"] = reader.GetString("Date");
                    row["Count"] = reader.GetInt32("Count");
                    table.Rows.Add(row);
                }
            }
            return table;
        }

        public DataTable GetCountByNodeAndEditor(int nodeId, string editor, DateTime beginDate, DateTime endDate)
        {
            string format = "SELECT Editor, NodeName, COUNT(*) AS count\r\n                              FROM PE_CommonModel M INNER JOIN PE_Nodes C ON M.NodeId = C.NodeId\r\n                              WHERE (PassedTime >= @BeginDate AND PassedTime< = @EndDate) AND Status = 99 {0}\r\n                              GROUP BY Editor, NodeName";
            Parameters cmdParams = new Parameters();
            string str2 = string.Empty;
            if (!string.IsNullOrEmpty(editor))
            {
                str2 = str2 + " AND M.Editor = @Editor";
                cmdParams.AddInParameter("@Editor", DbType.String, editor);
            }
            if (nodeId > 0)
            {
                str2 = str2 + " AND C.NodeId = @NodeId";
                cmdParams.AddInParameter("@NodeId", DbType.Int32, nodeId);
            }
            cmdParams.AddInParameter("@BeginDate", DbType.DateTime, beginDate);
            cmdParams.AddInParameter("@EndDate", DbType.DateTime, endDate);
            format = string.Format(format, str2);
            DataTable table = new DataTable();
            DataColumn column = new DataColumn("Editor", typeof(string));
            table.Columns.Add(column);
            column = new DataColumn("NodeName", typeof(string));
            table.Columns.Add(column);
            column = new DataColumn("Count", typeof(int));
            table.Columns.Add(column);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(format, cmdParams))
            {
                while (reader.Read())
                {
                    DataRow row = table.NewRow();
                    row["Editor"] = reader.GetString("Editor");
                    row["NodeName"] = reader.GetString("NodeName");
                    row["Count"] = reader.GetInt32("Count");
                    table.Rows.Add(row);
                }
            }
            return table;
        }

        public DataTable GetCountByNodeAndInputer(int nodeId, string userName, DateTime beginDate, DateTime endDate)
        {
            string format = "SELECT Inputer, NodeName, COUNT(*) AS count\r\n                              FROM PE_CommonModel M INNER JOIN PE_Nodes C ON M.NodeId = C.NodeId\r\n                              WHERE (InputTime >= @BeginDate AND InputTime< = @EndDate) AND Status = 99 {0}\r\n                              GROUP BY Inputer, NodeName";
            Parameters cmdParams = new Parameters();
            string str2 = string.Empty;
            if (!string.IsNullOrEmpty(userName))
            {
                str2 = str2 + " AND M.Inputer = @UserName";
                cmdParams.AddInParameter("@UserName", DbType.String, userName);
            }
            if (nodeId > 0)
            {
                str2 = str2 + " AND C.NodeId = @NodeId";
                cmdParams.AddInParameter("@NodeId", DbType.Int32, nodeId);
            }
            cmdParams.AddInParameter("@BeginDate", DbType.DateTime, beginDate);
            cmdParams.AddInParameter("@EndDate", DbType.DateTime, endDate);
            format = string.Format(format, str2);
            DataTable table = new DataTable();
            DataColumn column = new DataColumn("UserName", typeof(string));
            table.Columns.Add(column);
            column = new DataColumn("NodeName", typeof(string));
            table.Columns.Add(column);
            column = new DataColumn("Count", typeof(int));
            table.Columns.Add(column);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(format, cmdParams))
            {
                while (reader.Read())
                {
                    DataRow row = table.NewRow();
                    row["UserName"] = reader.GetString("Inputer");
                    row["NodeName"] = reader.GetString("NodeName");
                    row["Count"] = reader.GetInt32("Count");
                    table.Rows.Add(row);
                }
            }
            return table;
        }

        public DataTable GetCountByNodeAndMonth(int nodeId, DateTime beginDate, DateTime endDate)
        {
            string format = "SELECT NodeName, CONVERT(char(4), year(InputTime))+'-'+CONVERT(char(2), month(InputTime)) AS Date, COUNT(*) AS count\r\n                              FROM PE_CommonModel M INNER JOIN PE_Nodes C ON C.NodeId = M.NodeId\r\n                              WHERE (InputTime >= @BeginDate AND InputTime< = @EndDate) AND Status = 99 {0}\r\n                              GROUP BY NodeName, CONVERT(char(4), year(InputTime))+'-'+CONVERT(char(2), month(InputTime))";
            Parameters cmdParams = new Parameters();
            string str2 = string.Empty;
            if (nodeId > 0)
            {
                str2 = str2 + " AND M.NodeId = @NodeId";
                cmdParams.AddInParameter("@NodeId", DbType.Int32, nodeId);
            }
            cmdParams.AddInParameter("@BeginDate", DbType.DateTime, beginDate);
            cmdParams.AddInParameter("@EndDate", DbType.DateTime, endDate);
            format = string.Format(format, str2);
            DataTable table = new DataTable();
            DataColumn column = new DataColumn("NodeName", typeof(string));
            table.Columns.Add(column);
            column = new DataColumn("InputTime", typeof(string));
            table.Columns.Add(column);
            column = new DataColumn("Count", typeof(int));
            table.Columns.Add(column);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(format, cmdParams))
            {
                while (reader.Read())
                {
                    DataRow row = table.NewRow();
                    row["NodeName"] = reader.GetString("NodeName");
                    row["InputTime"] = reader.GetString("Date");
                    row["Count"] = reader.GetInt32("Count");
                    table.Rows.Add(row);
                }
            }
            return table;
        }

        public int GetCountBySignIn(string userName, bool isSignIn)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@UserName", DbType.String, userName);
            cmdParams.AddInParameter("@IsSignin", DbType.Boolean, isSignIn);
            return (int) DBHelper.ExecuteScalarSql("SELECT COUNT(*) FROM [dbo].[PE_SigninLog] WHERE UserName = @UserName AND IsSignin = @IsSignin", cmdParams);
        }

        public int GetCountByStatus(int status)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@Status", DbType.Int32, status);
            return (int) DBHelper.ExecuteScalarSql("SELECT COUNT(*) FROM [dbo].[PE_CommonModel] WHERE Status = @Status", cmdParams);
        }

        public IList<CommonModelInfo> GetCreateHtmlCommonModelInfoList(int startRowIndexId, int maxNumberRows, string nodeIds, int created, ContentSortType sortType, bool isEshop)
        {
            Database database = DatabaseFactory.CreateDatabase();
            List<CommonModelInfo> list = new List<CommonModelInfo>();
            string sortColumn = "C.GeneralId";
            string str2 = "C.*, M.IsEshop";
            string sorts = "DESC";
            string str4 = "";
            string str5 = "";
            if (!string.IsNullOrEmpty(nodeIds))
            {
                str4 = " C.NodeId IN (" + nodeIds.ToString() + ") ";
                str5 = " AND ";
            }
            str4 = str4 + str5 + "C.Status = 99 AND N.IsCreateContentPage = 1";
            if (isEshop)
            {
                str4 = str4 + " AND M.IsEshop = 1";
            }
            else
            {
                str4 = str4 + " AND M.IsEshop = 0";
            }
            switch (created)
            {
                case 1:
                    str4 = str4 + " AND C.CreateTime > C.UpdateTime";
                    break;

                case 2:
                    str4 = str4 + " AND (C.CreateTime < = C.UpdateTime OR C.CreateTime IS NULL)";
                    break;
            }
            InitContentSortColumnAndSort(sortType, ref sortColumn, ref sorts);
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetListBySortColumn");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@PrimaryColumn", DbType.String, "C.GeneralId");
            database.AddInParameter(storedProcCommand, "@SortColumnDbType", DbType.String, "int");
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, sortColumn);
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, str2);
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, sorts);
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "(PE_CommonModel C INNER JOIN PE_Model M ON C.ModelID = M.ModelID) INNER JOIN PE_Nodes N ON C.NodeID = N.NodeID");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, str4);
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    CommonModelInfo item = new CommonModelInfo();
                    item = CommonModelInfoFromDataReader(reader);
                    item.IsEshop = reader.GetBoolean("IsEshop");
                    list.Add(item);
                }
            }
            this.m_TotalOfCommonModelInfo = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public string GetGeneralIdArrByNodeId(string nodeIds, int status)
        {
            string strSql = "SELECT GeneralID FROM PE_CommonModel WHERE 1 = 1 ";
            if (!string.IsNullOrEmpty(nodeIds))
            {
                strSql = strSql + "AND NodeID IN(" + DBHelper.ToValidId(nodeIds) + ")";
            }
            if (status >= -3)
            {
                strSql = strSql + "AND Status = " + status.ToString();
            }
            StringBuilder sb = new StringBuilder();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql))
            {
                while (reader.Read())
                {
                    StringHelper.AppendString(sb, reader.GetInt32("GeneralID").ToString());
                }
            }
            return sb.ToString();
        }

        public string GetGeneralIdsByItemId(int generalId)
        {
            string strSql = "SELECT GeneralId FROM PE_CommonModel WHERE ItemId = @GeneralId";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@GeneralId", DbType.Int32, generalId);
            StringBuilder sb = new StringBuilder();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, cmdParams))
            {
                while (reader.Read())
                {
                    StringHelper.AppendString(sb, reader.GetInt32("GeneralId").ToString());
                }
            }
            return sb.ToString();
        }

        public DataTable GetHitsDataById(int generalId)
        {
            string strSql = "SELECT Hits, DayHits, WeekHits, MonthHits, LastHitTime FROM PE_CommonModel WHERE Status = 99 AND GeneralID = @GeneralId";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@GeneralId", DbType.Int32, generalId);
            DataTable table = new DataTable();
            DataColumn column = new DataColumn("Hits", typeof(int));
            table.Columns.Add(column);
            column = new DataColumn("DayHits", typeof(int));
            table.Columns.Add(column);
            column = new DataColumn("WeekHits", typeof(int));
            table.Columns.Add(column);
            column = new DataColumn("MonthHits", typeof(int));
            table.Columns.Add(column);
            column = new DataColumn("LastHitTime", typeof(DateTime));
            table.Columns.Add(column);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, cmdParams))
            {
                while (reader.Read())
                {
                    DataRow row = table.NewRow();
                    row["Hits"] = reader.GetInt32("Hits");
                    row["DayHits"] = reader.GetInt32("DayHits");
                    row["WeekHits"] = reader.GetInt32("WeekHits");
                    row["MonthHits"] = reader.GetInt32("MonthHits");
                    row["LastHitTime"] = reader.GetDateTime("LastHitTime");
                    table.Rows.Add(row);
                }
            }
            return table;
        }

        public IList<CommonModelInfo> GetInfoList(int generalId)
        {
            IList<CommonModelInfo> list = new List<CommonModelInfo>();
            CommonModelInfo commonModelInfoById = this.GetCommonModelInfoById(generalId);
            if (!commonModelInfoById.IsNull)
            {
                string strSql = string.Concat(new object[] { "SELECT * FROM PE_CommonModel WHERE LinkType = 1 AND ItemId = ", commonModelInfoById.GeneralId, " AND TableName = '", commonModelInfoById.TableName, "'" });
                Parameters cmdParams = new Parameters();
                using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, cmdParams))
                {
                    while (reader.Read())
                    {
                        list.Add(CommonModelInfoFromDataReader(reader));
                    }
                }
            }
            return list;
        }

        private static int GetNewGeneralId()
        {
            return (DBHelper.GetMaxId("PE_CommonModel", "GeneralId") + 1);
        }

        public CommonModelInfo GetNextInfo(int nodeId, int generalId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@NodeID", DbType.Int32, nodeId);
            cmdParams.AddInParameter("@GeneralID", DbType.Int32, generalId);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT TOP 1 * FROM PE_CommonModel WHERE NodeID = @NodeID AND GeneralID > @GeneralID AND Status > -3 ORDER BY GeneralID ASC", cmdParams))
            {
                if (reader.Read())
                {
                    return CommonModelInfoFromDataReader(reader);
                }
                return new CommonModelInfo(true);
            }
        }

        private static string GetNodeBound(string nodeIdArray)
        {
            string str = string.Empty;
            if (string.IsNullOrEmpty(nodeIdArray))
            {
                return str;
            }
            if (nodeIdArray.IndexOf(',') > 0)
            {
                return (str + " AND M.NodeId IN (" + DBHelper.ToValidId(nodeIdArray) + ")");
            }
            return (str + " AND M.NodeId =" + DBHelper.ToNumber(nodeIdArray));
        }

        private static string GetNodeBound(string nodeIdArray, Parameters parms, string strSql)
        {
            if (!string.IsNullOrEmpty(nodeIdArray))
            {
                if (nodeIdArray.IndexOf(',') > 0)
                {
                    strSql = strSql + " AND M.NodeId IN (" + DBHelper.ToValidId(nodeIdArray) + ")";
                    return strSql;
                }
                strSql = strSql + " AND M.NodeId = @NodeId ";
                parms.AddInParameter("@NodeId", DbType.Int32, nodeIdArray);
            }
            return strSql;
        }

        public CommonModelInfo GetPrevInfo(int nodeId, int generalId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@NodeID", DbType.Int32, nodeId);
            cmdParams.AddInParameter("@GeneralID", DbType.Int32, generalId);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT TOP 1 * FROM PE_CommonModel WHERE NodeID = @NodeID AND GeneralID < @GeneralID AND Status > -3 ORDER BY GeneralID DESC", cmdParams))
            {
                if (reader.Read())
                {
                    return CommonModelInfoFromDataReader(reader);
                }
                return new CommonModelInfo(true);
            }
        }

        public IList<CommonModelInfo> GetSearchContentList(int startRowIndexId, int maxNumberRows, string nodeIds, string modelIds, ContentSortType sortType, int status, string roleIds, string searchType, string keyword, int nodeId, IList<FieldInfo> fieldList)
        {
            string str = "";
            keyword = DataSecurity.FilterBadChar(keyword);
            searchType = DataSecurity.FilterBadChar(searchType);
            if (!string.IsNullOrEmpty(keyword))
            {
                if (searchType == "ID")
                {
                    str = "C.GeneralID =" + DataConverter.CLng(keyword);
                }
                else if (searchType == "Title")
                {
                    str = "C.Title LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'";
                }
                else if (searchType == "Inputer")
                {
                    str = "C.Inputer LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'";
                }
                else if (fieldList != null)
                {
                    foreach (FieldInfo info in fieldList)
                    {
                        if (!(info.FieldName == searchType))
                        {
                            continue;
                        }
                        if (info.FieldLevel == 1)
                        {
                            string tableNameByNodeID = this.GetTableNameByNodeID(nodeId);
                            if (info.FieldType == FieldType.DateTimeType)
                            {
                                str = "C.ItemID IN (SELECT ID FROM " + tableNameByNodeID + " WHERE ";
                                object obj2 = str;
                                str = string.Concat(new object[] { obj2, searchType, " > '", DataConverter.CDate(keyword), "'" });
                                keyword = DataConverter.CDate(keyword).AddDays(1.0).ToString(CultureInfo.CurrentCulture);
                                string str9 = str;
                                str = str9 + "AND " + searchType + " < '" + keyword + "')";
                            }
                            else if (info.FieldType == FieldType.BoolType)
                            {
                                str = "C.ItemID IN (SELECT ID FROM " + tableNameByNodeID + " WHERE " + searchType + " = '" + DBHelper.FilterBadChar(keyword) + "')";
                            }
                            else
                            {
                                str = "C.ItemID IN (SELECT ID FROM " + tableNameByNodeID + " WHERE " + searchType + " LIKE '%" + DBHelper.FilterBadChar(keyword) + "%')";
                            }
                        }
                        else if (searchType == "UpdateTime")
                        {
                            str = "C.UpdateTime > '" + DataConverter.CDate(keyword) + "'";
                            keyword = DataConverter.CDate(keyword).AddDays(1.0).ToString(CultureInfo.CurrentCulture);
                            object obj3 = str;
                            str = string.Concat(new object[] { obj3, " AND C.UpdateTime < '", DataConverter.CDate(keyword), "'" });
                        }
                        else if ((searchType == "NodeId") || (searchType == "InfoId"))
                        {
                            str = "C.NodeID IN (SELECT NodeID FROM PE_Nodes WHERE NodeName LIKE '%" + DBHelper.FilterBadChar(keyword) + "%')";
                            nodeIds = null;
                        }
                        else if (searchType == "SpecialId")
                        {
                            str = "C.GeneralID IN (SELECT GeneralID FROM PE_SpecialInfos WHERE SpecialID IN (SELECT SpecialID FROM PE_Specials WHERE SpecialName LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'))";
                        }
                        else
                        {
                            str = "C." + searchType + " LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'";
                        }
                        break;
                    }
                }
            }
            Database database = DatabaseFactory.CreateDatabase();
            List<CommonModelInfo> list = new List<CommonModelInfo>();
            string sortColumn = "C.GeneralId";
            string str4 = "C.*";
            string sorts = "DESC";
            string str6 = "";
            string str7 = "";
            if (!string.IsNullOrEmpty(nodeIds))
            {
                str6 = " C.NodeId IN (" + nodeIds.ToString() + ") ";
                str7 = " AND ";
            }
            if (!string.IsNullOrEmpty(modelIds))
            {
                str6 = str6 + str7 + " C.ModelID IN (" + modelIds + ") ";
                str7 = " AND ";
            }
            else
            {
                str6 = str6 + str7 + " C.ModelID = 0 ";
                str7 = " AND ";
            }
            if ((status < 100) && (status > -4))
            {
                str6 = string.Concat(new object[] { str6, str7, "C.Status = ", status, " " });
            }
            if (status == 100)
            {
                str6 = str6 + str7 + "C.Status <= 99 AND C.Status >= 0 ";
            }
            if (status == 0x65)
            {
                if (!string.IsNullOrEmpty(roleIds))
                {
                    string str8 = "SELECT StatusCode\r\n                     FROM PE_ProcessStatusCode AS psc\r\n                     WHERE   (FlowID =\r\n                                         (SELECT WorkFlowID\r\n                                          FROM PE_Nodes\r\n                                          WHERE (NodeID = C.NodeID))) AND (ProcessID IN\r\n                                         (SELECT ProcessID\r\n                                          FROM PE_Process_Roles\r\n                                          WHERE   (FlowID = psc.FlowID) AND (RoleID IN (" + roleIds + "))))";
                    str6 = str6 + str7 + "C.Status < 99 AND C.Status >= 0 AND C.Status IN(" + str8 + ") ";
                }
                else
                {
                    str6 = str6 + str7 + "C.Status < 99 AND C.Status >= 0 ";
                }
            }
            if (status == 0x66)
            {
                str6 = str6 + str7 + "C.Status = 100 ";
            }
            if (!string.IsNullOrEmpty(str))
            {
                if (str6.Length > 0)
                {
                    str6 = str6 + " AND " + str;
                }
                else
                {
                    str6 = str;
                }
            }
            InitContentSortColumnAndSort(sortType, ref sortColumn, ref sorts);
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetListBySortColumn");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@PrimaryColumn", DbType.String, "C.GeneralId");
            database.AddInParameter(storedProcCommand, "@SortColumnDbType", DbType.String, "int");
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, sortColumn);
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, str4);
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, sorts);
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_CommonModel C ");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, str6);
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    CommonModelInfo item = new CommonModelInfo();
                    item = CommonModelInfoFromDataReader(reader);
                    list.Add(item);
                }
            }
            this.m_TotalOfCommonModelInfo = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public string GetTableNameByNodeID(int nodeId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@NodeID", DbType.Int32, nodeId);
            object obj2 = DBHelper.ExecuteScalarSql("SELECT tablename FROM PE_Model WHERE ModelID = (SELECT TOP 1 ModelID FROM PE_Nodes_Model_Template WHERE NodeID = @NodeID)", cmdParams);
            if (obj2 == null)
            {
                return "";
            }
            return (string) obj2;
        }

        public int GetTodayPublicInfoCountByUserName(string userName)
        {
            string strSql = "SELECT COUNT(*) FROM PE_CommonModel WHERE Inputer = @UserName AND InputTime>=DATEADD(dd, DATEDIFF(dd, 0, GETDATE()), 0) AND InputTime<DATEADD(dd, DATEDIFF(dd, 0, GETDATE()), 1) AND Status >= 0";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@UserName", DbType.String, userName);
            return (int) DBHelper.ExecuteScalarSql(strSql, cmdParams);
        }

        public int GetTotalOfCommonModelInfo()
        {
            return this.m_TotalOfCommonModelInfo;
        }

        public int GetTotalOfCommonModelInfoBySpecialCategoryId()
        {
            return this.m_TotalOfCommonModelInfoBySpecialCategoryId;
        }

        public int GetTotalOfCommonModelInfoBySpecialId()
        {
            return this.m_TotalOfCommonModelInfoBySpecialId;
        }

        public DataTable GetUserContentDataById(int generalId, string userName)
        {
            string tableName = this.GetCommonModelInfoById(generalId).TableName;
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@GeneralId", DbType.Int32, generalId);
            cmdParams.AddInParameter("@UserName", DbType.String, userName);
            return DBHelper.ExecuteDataSetSql("SELECT C.*, C.ItemId AS InfoId, C.ItemId AS SpecialId, T.* FROM PE_CommonModel C INNER JOIN " + DBHelper.FilterBadChar(tableName) + " T ON C.ItemID = T.ID WHERE GeneralId = @GeneralId AND C.Inputer = @UserName", cmdParams).Tables[0];
        }

        private static void InitContentSortColumnAndSort(ContentSortType sortType, ref string sortColumn, ref string sorts)
        {
            switch (sortType)
            {
                case ContentSortType.IdAsc:
                    sortColumn = "C.GeneralId";
                    sorts = "ASC";
                    return;

                case ContentSortType.EliteLevelDesc:
                    sortColumn = "C.EliteLevel";
                    sorts = "DESC";
                    return;

                case ContentSortType.EliteLevelAsc:
                    sortColumn = "C.EliteLevel";
                    sorts = "ASC";
                    return;

                case ContentSortType.PriorityDesc:
                    sortColumn = "C.Priority";
                    sorts = "DESC";
                    return;

                case ContentSortType.PriorityAsc:
                    sortColumn = "C.Priority";
                    sorts = "ASC";
                    return;

                case ContentSortType.DayHitsDesc:
                    sortColumn = "C.DayHits";
                    sorts = "DESC";
                    return;

                case ContentSortType.DayHitsAsc:
                    sortColumn = "C.DayHits";
                    sorts = "ASC";
                    return;

                case ContentSortType.WeekHitsDesc:
                    sortColumn = "C.WeekHits";
                    sorts = "DESC";
                    return;

                case ContentSortType.WeekHitsAsc:
                    sortColumn = "C.WeekHits";
                    sorts = "ASC";
                    return;

                case ContentSortType.MonthHitsDesc:
                    sortColumn = "C.MonthHits";
                    sorts = "DESC";
                    return;

                case ContentSortType.MonthHitsAsc:
                    sortColumn = "C.MonthHits";
                    sorts = "ASC";
                    return;

                case ContentSortType.HitsDesc:
                    sortColumn = "C.Hits";
                    sorts = "DESC";
                    return;

                case ContentSortType.HitsAsc:
                    sortColumn = "C.Hits";
                    sorts = "ASC";
                    return;
            }
            sortColumn = "C.GeneralId";
            sorts = "DESC";
        }

        private static void InitSpecialContentSortColumnAndSort(ContentSortType sortType, ref string sortColumn, ref string sorts)
        {
            switch (sortType)
            {
                case ContentSortType.IdAsc:
                    sortColumn = "S.SpecialInfoID";
                    sorts = "ASC";
                    return;

                case ContentSortType.EliteLevelDesc:
                    sortColumn = "C.EliteLevel";
                    sorts = "DESC";
                    return;

                case ContentSortType.EliteLevelAsc:
                    sortColumn = "C.EliteLevel";
                    sorts = "ASC";
                    return;

                case ContentSortType.PriorityDesc:
                    sortColumn = "C.Priority";
                    sorts = "DESC";
                    return;

                case ContentSortType.PriorityAsc:
                    sortColumn = "C.Priority";
                    sorts = "ASC";
                    return;

                case ContentSortType.DayHitsDesc:
                    sortColumn = "C.DayHits";
                    sorts = "DESC";
                    return;

                case ContentSortType.DayHitsAsc:
                    sortColumn = "C.DayHits";
                    sorts = "ASC";
                    return;

                case ContentSortType.WeekHitsDesc:
                    sortColumn = "C.WeekHits";
                    sorts = "DESC";
                    return;

                case ContentSortType.WeekHitsAsc:
                    sortColumn = "C.WeekHits";
                    sorts = "ASC";
                    return;

                case ContentSortType.MonthHitsDesc:
                    sortColumn = "C.MonthHits";
                    sorts = "DESC";
                    return;

                case ContentSortType.MonthHitsAsc:
                    sortColumn = "C.MonthHits";
                    sorts = "ASC";
                    return;

                case ContentSortType.HitsDesc:
                    sortColumn = "C.Hits";
                    sorts = "DESC";
                    return;

                case ContentSortType.HitsAsc:
                    sortColumn = "C.Hits";
                    sorts = "ASC";
                    return;
            }
            sortColumn = "S.SpecialInfoID";
            sorts = "DESC";
        }

        public bool RecycleAll(string nodeIds)
        {
            if (string.IsNullOrEmpty(nodeIds))
            {
                return false;
            }
            string str = "";
            if (nodeIds != "0")
            {
                str = " AND NodeID IN (" + DBHelper.ToValidId(nodeIds) + ")";
            }
            return DBHelper.ExecuteSql("UPDATE PE_CommonModel SET Status = 0 WHERE Status=-3 " + str);
        }

        public void ReplaceTemplateDir(string oldDir, string newDir)
        {
            oldDir = oldDir.Replace("'", "''");
            newDir = newDir.Replace("'", "''");
            string str = "'" + oldDir + "%'";
            DBHelper.ExecuteSql("UPDATE PE_CommonModel SET TemplateFile = replace(cast(TemplateFile AS nvarchar(4000)), '" + oldDir + "', '" + newDir + "') WHERE TemplateFile LIKE " + str);
        }

        public void ReplaceTemplateFileName(string replaceFormer, string replaceAfter)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ReplaceFormer", DbType.String, replaceFormer);
            cmdParams.AddInParameter("@ReplaceAfter", DbType.String, replaceAfter);
            DBHelper.ExecuteSql("UPDATE PE_CommonModel SET TemplateFile = @ReplaceAfter WHERE TemplateFile = @ReplaceFormer", cmdParams);
        }

        private static SpecialCommonModelInfo SpecialCommonModelInfoFromDataReader(NullableDataReader rdr)
        {
            SpecialCommonModelInfo info = new SpecialCommonModelInfo();
            info.SpecialInfoId = rdr.GetInt32("SpecialInfoID");
            info.SpecialId = rdr.GetInt32("SpecialID");
            info.GeneralId = rdr.GetInt32("GeneralId");
            info.NodeId = rdr.GetInt32("NodeId");
            info.ModelId = rdr.GetInt32("ModelId");
            info.ItemId = rdr.GetInt32("ItemId");
            info.TableName = rdr.GetString("TableName");
            info.Title = rdr.GetString("Title");
            info.PinyinTitle = rdr.GetString("PinyinTitle");
            info.Inputer = rdr.GetString("Inputer");
            info.Hits = rdr.GetInt32("Hits");
            info.DayHits = rdr.GetInt32("DayHits");
            info.WeekHits = rdr.GetInt32("WeekHits");
            info.MonthHits = rdr.GetInt32("MonthHits");
            info.LinkType = rdr.GetInt32("LinkType");
            info.UpdateTime = rdr.GetDateTime("UpdateTime");
            info.CreateTime = rdr.GetNullableDateTime("CreateTime");
            info.InputTime = rdr.GetDateTime("InputTime");
            info.TemplateFile = rdr.GetString("TemplateFile");
            info.Status = rdr.GetInt32("Status");
            info.EliteLevel = rdr.GetInt32("EliteLevel");
            info.Priority = rdr.GetInt32("Priority");
            return info;
        }

        public bool Update(int generalId, DataTable contentData)
        {
            bool flag = false;
            CommonModelInfo commonModelInfoById = this.GetCommonModelInfoById(generalId);
            if (commonModelInfoById.IsNull)
            {
                return false;
            }
            string tableName = commonModelInfoById.TableName;
            if (commonModelInfoById.LinkType == 1)
            {
                if (this.UpdateCommonModel(generalId, contentData))
                {
                    flag = true;
                }
                return flag;
            }
            if (UpdateContent(generalId, tableName, contentData))
            {
                flag = this.UpdateCommonModel(generalId, contentData);
            }
            return flag;
        }

        public int UpdateBrowseTimes(int generalId)
        {
            string tableName = this.GetCommonModelInfoById(generalId).TableName;
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@GeneralId", DbType.Int32, generalId);
            object input = DBHelper.ExecuteScalarSql("SELECT BrowseTimes FROM " + tableName + " WHERE ID = @GeneralId", cmdParams);
            if (input == null)
            {
                DBHelper.ExecuteSql("UPDATE " + tableName + " SET BrowseTimes = 1 WHERE ID = @GeneralId", cmdParams);
                return 1;
            }
            DBHelper.ExecuteSql("UPDATE " + tableName + " SET BrowseTimes = BrowseTimes + 1 WHERE ID = @GeneralId", cmdParams);
            return (DataConverter.CLng(input) + 1);
        }

        public bool UpdateByUser(int generalId, DataTable contentData, string userName)
        {
            bool flag = false;
            CommonModelInfo commonModelInfoById = this.GetCommonModelInfoById(generalId);
            if (commonModelInfoById.IsNull || (commonModelInfoById.Inputer != userName))
            {
                return false;
            }
            string tableName = commonModelInfoById.TableName;
            if (commonModelInfoById.LinkType == 1)
            {
                if (this.UpdateCommonModel(generalId, contentData))
                {
                    flag = true;
                }
                return flag;
            }
            if (UpdateContent(generalId, tableName, contentData))
            {
                flag = this.UpdateCommonModel(generalId, contentData);
            }
            return flag;
        }

        public bool UpdateCommentAuditedAndUnaudited(int generalId)
        {
            string strSql = "UPDATE PE_CommonModel SET CommentAudited = (SELECT COUNT(*) FROM PE_Comment WHERE GeneralId = @GeneralId AND Status = 1),CommentUnAudited = (SELECT COUNT(*) FROM PE_Comment WHERE GeneralId = @GeneralId AND Status = 0) WHERE GeneralId = @GeneralId";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@GeneralId", DbType.Int32, generalId);
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool UpdateCommonModel(int generalId, DataTable contentData)
        {
            CommonModelInfo commonModelInfoById = this.GetCommonModelInfoById(generalId);
            if (commonModelInfoById.IsNull)
            {
                return false;
            }
            int modelId = commonModelInfoById.ModelId;
            int itemId = commonModelInfoById.ItemId;
            string tableName = commonModelInfoById.TableName;
            string[] filedNames = new string[] { "generalId", "CreateTime", "ModelId", "TableName", "ItemId" };
            string[] strArray4 = new string[5];
            strArray4[0] = generalId.ToString();
            strArray4[2] = modelId.ToString();
            strArray4[3] = tableName;
            strArray4[4] = itemId.ToString();
            string[] filedValues = strArray4;
            FieldType[] typeArray2 = new FieldType[5];
            typeArray2[1] = FieldType.DateTimeType;
            typeArray2[3] = FieldType.TextType;
            FieldType[] fieldType = typeArray2;
            GetCommonModelDataTable(filedNames, filedValues, fieldType, contentData);
            string filter = "FieldLevel = 0 AND FieldName <> 'infoid' AND FieldName <> 'specialid'";
            string strSql = Query.GetUpdataSql("PE_CommonModel", contentData, filter, " GeneralId = " + generalId.ToString());
            Parameters cmdParams = Query.GetParameters(contentData, filter);
            if (!DBHelper.ExecuteSql(strSql, cmdParams))
            {
                return false;
            }
            if (commonModelInfoById.LinkType == 0)
            {
                this.DeleteVirtualContent(generalId);
                this.AddVirtualContent(generalId, contentData);
                DeleteSpecialContent(generalId);
                AddSpecialContent(generalId, contentData);
            }
            return true;
        }

        public static bool UpdateContent(int generalId, string tableName, DataTable contentData)
        {
            DataRow row = contentData.NewRow();
            row["FieldName"] = "Id";
            row["FieldValue"] = generalId;
            row["FieldType"] = 15;
            row["FieldLevel"] = 1;
            contentData.Rows.Add(row);
            string filter = "FieldLevel = 1";
            string strSql = Query.GetUpdataSql(tableName, contentData, filter, "Id = " + generalId);
            Parameters cmdParams = Query.GetParameters(contentData, filter);
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool UpdateCreateTime(int generalId, DateTime? createTime)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@CreateTime", DbType.DateTime, createTime);
            cmdParams.AddInParameter("@GeneralId", DbType.Int32, generalId);
            return DBHelper.ExecuteSql("UPDATE PE_CommonModel SET CreateTime = @CreateTime WHERE GeneralId = @GeneralId", cmdParams);
        }

        public bool UpdateField(int id, string table, string fieldName, DataTable content)
        {
            Parameters cmdParams = new Parameters();
            StringBuilder builder = new StringBuilder();
            string[] strArray = new string[0];
            if (fieldName.IndexOf('$') > 0)
            {
                strArray = fieldName.Split(new string[] { "$" }, StringSplitOptions.None);
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(", ");
                    }
                    builder.Append(strArray[i] + " = @" + strArray[i]);
                    cmdParams.AddInParameter("@" + strArray[i], DbType.String, content.Rows[0][strArray[i]].ToString());
                }
            }
            else
            {
                builder.Append(fieldName + " = @" + fieldName);
                cmdParams.AddInParameter("@" + fieldName, DbType.String, content.Rows[0][fieldName].ToString());
            }
            cmdParams.AddInParameter("@ID", DbType.Int32, id);
            cmdParams.AddInParameter("@DefaultPicUrl", DbType.String, content.Rows[0]["DefaultPicUrl"].ToString());
            cmdParams.AddInParameter("@UploadFiles", DbType.String, content.Rows[0]["UploadFiles"].ToString());
            return (DBHelper.ExecuteSql("UPDATE [" + table + "] SET " + builder.ToString() + " WHERE ID = @ID", cmdParams) && DBHelper.ExecuteSql("UPDATE [PE_CommonModel] SET DefaultPicUrl = @DefaultPicUrl, UploadFiles = @UploadFiles WHERE GeneralID = @ID", cmdParams));
        }

        public bool UpdateHits(int generalId, int hits, int dayhits, int weekhits, int monthhits, DateTime lasthittime)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@GeneralId", DbType.Int32, generalId);
            cmdParams.AddInParameter("@Hits", DbType.Int32, hits);
            cmdParams.AddInParameter("@DayHits", DbType.Int32, dayhits);
            cmdParams.AddInParameter("@WeekHits", DbType.Int32, weekhits);
            cmdParams.AddInParameter("@MonthHits", DbType.Int32, monthhits);
            cmdParams.AddInParameter("@LastHitTime", DbType.DateTime, lasthittime);
            return DBHelper.ExecuteSql("UPDATE PE_CommonModel SET Hits = @Hits, DayHits = @DayHits, WeekHits = @WeekHits, MonthHits = @MonthHits, LastHitTime = @LastHitTime WHERE GeneralID = @GeneralId", cmdParams);
        }

        public void UpdateNodeId(int nodeId, string sourceNodeIds)
        {
            string strSql = "UPDATE PE_CommonModel SET NodeId = @NodeId WHERE NodeId IN(" + DBHelper.ToValidId(sourceNodeIds) + ")";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@NodeId", DbType.Int32, nodeId);
            DBHelper.ExecuteNonQuerySql(strSql, cmdParams);
        }

        public bool UpdateStatus(int generalId, int status, string editor)
        {
            DateTime? nullable = null;
            if (status == 0x63)
            {
                nullable = new DateTime?(DateTime.Now);
            }
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@Status", DbType.Int32, status);
            cmdParams.AddInParameter("@GeneralId", DbType.Int32, generalId);
            cmdParams.AddInParameter("@PassedTime", DbType.DateTime, nullable);
            cmdParams.AddInParameter("@Editor", DbType.String, editor);
            cmdParams.AddInParameter("@CreateTime", DbType.String, DBNull.Value);
            return DBHelper.ExecuteSql("UPDATE PE_CommonModel SET Status = @Status, Editor = @Editor, PassedTime = @PassedTime,CreateTime=@CreateTime WHERE GeneralId = @GeneralId", cmdParams);
        }

        public bool UpdateStatus(string generalIds, int status, string editor)
        {
            DateTime? nullable = null;
            if (status == 0x63)
            {
                nullable = new DateTime?(DateTime.Now);
            }
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@Status", DbType.Int32, status);
            cmdParams.AddInParameter("@PassedTime", DbType.DateTime, nullable);
            cmdParams.AddInParameter("@Editor", DbType.String, editor);
            cmdParams.AddInParameter("@CreateTime", DbType.String, DBNull.Value);
            return DBHelper.ExecuteSql("UPDATE PE_CommonModel SET Status = @Status, Editor = @Editor, PassedTime = @PassedTime,CreateTime=@CreateTime WHERE GeneralId IN(" + DBHelper.ToValidId(generalIds) + ")", cmdParams);
        }

        public bool UpdateStatusByUser(string generalIds, int status, string editor, string userName)
        {
            DateTime? nullable = null;
            if (status == 0x63)
            {
                nullable = new DateTime?(DateTime.Now);
            }
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@Status", DbType.Int32, status);
            cmdParams.AddInParameter("@PassedTime", DbType.DateTime, nullable);
            cmdParams.AddInParameter("@Editor", DbType.String, editor);
            cmdParams.AddInParameter("@Inputer", DbType.String, userName);
            return DBHelper.ExecuteSql("UPDATE PE_CommonModel SET Status = @Status, Editor = @Editor, PassedTime = @PassedTime WHERE Inputer = @Inputer AND GeneralId IN(" + DBHelper.ToValidId(generalIds) + ")", cmdParams);
        }

        public bool UpdateTemplateFile(int generalId, string templateFile)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@GeneralId", DbType.Int32, generalId);
            cmdParams.AddInParameter("@TemplateFile", DbType.String, templateFile);
            return DBHelper.ExecuteSql("UPDATE PE_CommonModel SET TemplateFile = @TemplateFile WHERE GeneralId = @GeneralId", cmdParams);
        }
    }
}

