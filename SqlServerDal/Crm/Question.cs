namespace EasyOne.SqlServerDal.Crm
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.IDal.Crm;
    using EasyOne.Model.Crm;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Text;

    public class Question : IQuestion
    {
        private int m_StatisticCount;
        private int m_TotalCount;
        private int m_TotalScore;
        private const string SEARCH_TABLENAME = "PE_Question q INNER JOIN PE_QuestionType t ON q.TypeID = t.TypeID";

        public bool Add(QuestionInfo info)
        {
            return DBHelper.ExecuteSql("INSERT INTO PE_Question(ID, ProductVersion, ProductDBType, SystemType, TypeId, QuestionTitle, QuestionContent, QuestionCreateTime, QuestionCreator, IsPublic, IP, ReplyCreator, ReplyTime, IsReply, AntiVirus, FireWall, ErrorCode, ErrorText, IsSolved, Url, Score, LastUpdateTime) VALUES (@ID, @ProductVersion, @ProductDBType, @SystemType, @TypeId, @QuestionTitle, @QuestionContent, @QuestionCreateTime, @QuestionCreator, @IsPublic, @IP, @ReplyCreator, @ReplyTime, @IsReply, @AntiVirus, @FireWall, @ErrorCode, @ErrorText, @IsSolved, @Url, @Score, @LastUpdateTime) ", GetParameters(info));
        }

        public void AddFavorite(int questionId)
        {
            string strSql = "IF NOT EXISTS (SELECT * FROM PE_QuestionFavorite WHERE QuestionID=@QuestionID AND AdminID=@AdminID) INSERT INTO PE_QuestionFavorite ([QuestionID],[AdminID]) VALUES (@QuestionID, @AdminID)";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@QuestionID", DbType.Int32, questionId);
            cmdParams.AddInParameter("@AdminID", DbType.Int32, PEContext.Current.Admin.AdministratorInfo.AdminId);
            DBHelper.ExecuteNonQuerySql(strSql, cmdParams);
        }

        public bool BatchSetSolved(string questionIdList)
        {
            return DBHelper.ExecuteSql("UPDATE PE_Question SET IsSolved = 1 WHERE ID IN (" + DBHelper.ToValidId(questionIdList) + ") ");
        }

        public bool BatchSetTypeId(string questionIdList, int typeId)
        {
            return DBHelper.ExecuteSql("UPDATE PE_Question SET TypeId = " + typeId.ToString() + " WHERE ID IN(" + DBHelper.ToValidId(questionIdList) + ")");
        }

        public bool Delete(int questionId)
        {
            return DBHelper.ExecuteSql("DELETE FROM PE_Question WHERE ID = @ID", new Parameters("@ID", DbType.Int32, questionId));
        }

        public bool Delete(string questionIdList)
        {
            return DBHelper.ExecuteSql("DELETE FROM PE_Question WHERE ID IN (" + DBHelper.ToValidId(questionIdList) + ")");
        }

        public int GetAdminReplyJustNow()
        {
            string strSql = "SELECT COUNT(ID) FROM PE_Question WHERE IsSolved=0 AND IsReply=1  AND QuestionCreator=@QuestionCreator AND ReplyCreator!=QuestionCreator";
            string userName = PEContext.Current.User.UserName;
            if (string.IsNullOrEmpty(userName))
            {
                return 0;
            }
            return DataConverter.CLng(DBHelper.ExecuteScalarSql(strSql, new Parameters("@QuestionCreator", DbType.String, userName)));
        }

        private static string GetFilterByComplexSearch(string keyword)
        {
            string[] strArray = keyword.Split(new char[] { '|' });
            if (strArray.Length == 0)
            {
                return string.Empty;
            }
            string complexSearchItem = strArray[0];
            string str2 = strArray[1];
            string str3 = strArray[2];
            string str4 = strArray[3];
            string str5 = strArray[4];
            string str6 = strArray[5];
            string str7 = strArray[6];
            string str8 = strArray[7];
            string str9 = strArray[8];
            string str10 = strArray[9];
            string str11 = strArray[10];
            string str12 = strArray[11];
            string str13 = strArray[12];
            string str14 = PEContext.Current.Admin.AdministratorInfo.AdminId.ToString();
            StringBuilder builder = new StringBuilder(0x80);
            builder.Append(HasValue(str2) ? (" AND q.Score = " + DBHelper.ToNumber(str2)) : string.Empty);
            builder.Append(HasValue(str3) ? (" AND q.IsReply = " + ((str3 == "1") ? str3 : "0")) : string.Empty);
            builder.Append(HasValue(str4) ? (" AND q.IsSolved = " + ((str4 == "1") ? str4 : "0")) : string.Empty);
            builder.Append(HasValue(str5) ? (" AND q.IsPublic = " + ((str5 == "1") ? str5 : "0")) : string.Empty);
            builder.Append(HasValue(str12) ? (" AND q.QuestionCreator = '" + DBHelper.FilterBadChar(str12) + "'") : string.Empty);
            if (HasValue(str8))
            {
                builder.Append(" AND q.QuestionCreateTime >= '" + str8 + "'");
            }
            if (HasValue(str9))
            {
                builder.Append(" AND q.QuestionCreateTime <= '" + str9 + "'");
            }
            if (HasValue(str7))
            {
                builder.Append(" AND q.QuestionTitle LIKE '%" + DBHelper.FilterBadChar(str7) + "%'");
            }
            if (HasValue(str6) || HasValue(complexSearchItem))
            {
                string str15 = string.Empty;
                if (HasValue(str6))
                {
                    str15 = (str6 == "1") ? "=" : "<>";
                }
                if (HasValue(str6) && HasValue(complexSearchItem))
                {
                    builder.Append(" AND (q.TypeId IN (" + DBHelper.ToValidId(complexSearchItem) + ") OR q.TypeId IN (SELECT TypeId FROM PE_QuestionType_Admin WHERE AdminID" + str15 + str14 + "))");
                }
                else if (HasValue(str6))
                {
                    builder.Append(" AND q.TypeID IN (SELECT TypeID FROM PE_QuestionType WHERE AdminID" + str15 + str14 + ")");
                }
                else
                {
                    builder.Append(" AND q.TypeId IN (" + DBHelper.ToValidId(complexSearchItem) + ")");
                }
            }
            if ((HasValue(str13) || HasValue(str10)) || HasValue(str11))
            {
                builder.Append(" AND q.ID IN (SELECT questionID FROM PE_Reply WHERE 1=1 ");
                if (HasValue(str13))
                {
                    builder.Append(" AND ReplyCreator = '").Append(DBHelper.FilterBadChar(str13)).Append("' AND ID IN(Select MIN(ID) from PE_Reply Group By QuestionID) ");
                }
                if (HasValue(str10))
                {
                    builder.Append(" AND replyTime >= '").Append(str10).Append("' ");
                }
                if (HasValue(str11))
                {
                    builder.Append(" AND replyTime <= '").Append(str11).Append("'");
                }
                builder.Append(")");
            }
            return builder.ToString();
        }

        private static QuestionInfo GetInfoByReader(NullableDataReader rdr, bool getAll)
        {
            QuestionInfo info = new QuestionInfo();
            info.Id = rdr.GetInt32("ID");
            info.TypeId = rdr.GetInt32("TypeID");
            info.TypeName = rdr.GetString("TypeName");
            info.QuestionTitle = rdr.GetString("QuestionTitle");
            info.QuestionCreateTime = rdr.GetDateTime("QuestionCreateTime");
            info.QuestionCreator = rdr.GetString("QuestionCreator");
            info.ReplyCreator = rdr.GetString("ReplyCreator");
            info.ReplyTime = rdr.GetNullableDateTime("ReplyTime");
            info.Score = rdr.GetInt32("Score");
            info.IsPublic = rdr.GetBoolean("IsPublic");
            info.IsReply = rdr.GetBoolean("IsReply");
            info.IsSolved = rdr.GetBoolean("IsSolved");
            if (getAll)
            {
                info.AntiVirus = rdr.GetString("AntiVirus");
                info.ErrorCode = rdr.GetString("ErrorCode");
                info.ErrorText = rdr.GetString("ErrorText");
                info.FireWall = rdr.GetString("FireWall");
                info.IP = rdr.GetString("IP");
                info.ProductDBType = rdr.GetString("ProductDBType");
                info.ProductVersion = rdr.GetString("ProductVersion");
                info.QuestionContent = rdr.GetString("QuestionContent");
                info.SystemType = rdr.GetString("SystemType");
                info.Url = rdr.GetString("Url");
                info.LastUpdateTime = rdr.GetDateTime("LastUpdateTime");
            }
            return info;
        }

        public int GetMaxId()
        {
            return DBHelper.GetMaxId("PE_Question", "ID");
        }

        private static Parameters GetParameters(QuestionInfo info)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@ID", DbType.Int32, info.Id);
            parameters.AddInParameter("@ProductVersion", DbType.String, info.ProductVersion);
            parameters.AddInParameter("@ProductDBType", DbType.String, info.ProductDBType);
            parameters.AddInParameter("@SystemType", DbType.String, info.SystemType);
            parameters.AddInParameter("@TypeId", DbType.Int32, info.TypeId);
            parameters.AddInParameter("@TypeName", DbType.String, info.TypeName);
            parameters.AddInParameter("@QuestionTitle", DbType.String, info.QuestionTitle);
            parameters.AddInParameter("@QuestionContent", DbType.String, info.QuestionContent);
            parameters.AddInParameter("@QuestionCreateTime", DbType.DateTime, info.QuestionCreateTime);
            parameters.AddInParameter("@QuestionCreator", DbType.String, info.QuestionCreator);
            parameters.AddInParameter("@AntiVirus", DbType.String, info.AntiVirus);
            parameters.AddInParameter("@FireWall", DbType.String, info.FireWall);
            parameters.AddInParameter("@ErrorCode", DbType.String, info.ErrorCode);
            parameters.AddInParameter("@ErrorText", DbType.String, info.ErrorText);
            parameters.AddInParameter("@IP", DbType.String, info.IP);
            parameters.AddInParameter("@ReplyCreator", DbType.String, info.ReplyCreator);
            parameters.AddInParameter("@ReplyTime", DbType.DateTime, info.ReplyTime);
            parameters.AddInParameter("@Url", DbType.String, info.Url);
            parameters.AddInParameter("@Score", DbType.Int32, info.Score);
            parameters.AddInParameter("@IsSolved", DbType.Boolean, info.IsSolved);
            parameters.AddInParameter("@IsPublic", DbType.Boolean, info.IsPublic);
            parameters.AddInParameter("@IsReply", DbType.Boolean, info.IsReply);
            parameters.AddInParameter("@LastUpdateTime", DbType.DateTime, info.LastUpdateTime);
            return parameters;
        }

        public IList<QuestionInfo> GetQuestion(int startRowIndex, int maximumRows, int searchType, string keyword)
        {
            StringBuilder builder = new StringBuilder(" 1 = 1 ");
            string sortCloumn = "q.LastUpdateTime";
            switch (searchType)
            {
                case 1:
                    builder.Append(" AND q.IsSolved = 1 ");
                    break;

                case 2:
                    builder.Append(" AND q.IsSolved = 0 ");
                    break;

                case 3:
                    builder.Append(" AND q.IsSolved = 1 AND q.ReplyCreator = '").Append(DBHelper.FilterBadChar(PEContext.Current.Admin.AdminName)).Append("' ");
                    break;

                case 4:
                    builder.Append(" AND q.IsSolved = 0 AND q.TypeId IN(SELECT TypeId FROM PE_QuestionType_Admin WHERE AdminID = ").Append(PEContext.Current.Admin.AdministratorInfo.AdminId).Append(") ");
                    break;

                case 5:
                    builder.Append(" AND q.IsReply = 0 AND q.IsSolved = 0 ");
                    break;

                case 6:
                    builder.Append(" AND ReplyCreator=QuestionCreator AND q.IsSolved = 0 AND q.IsReply = 1 ");
                    break;

                case 7:
                    builder.Append(" AND q.QuestionTitle LIKE '%").Append(DBHelper.FilterBadChar(keyword)).Append("%'");
                    break;

                case 8:
                    builder.Append(" AND q.QuestionCreator = '").Append(DBHelper.FilterBadChar(keyword)).Append("'");
                    break;

                case 9:
                    builder.Append(" AND q.ID IN (SELECT DISTINCT QuestionID FROM PE_Reply WHERE ReplyCreator = '").Append(DBHelper.FilterBadChar(keyword)).Append("')");
                    sortCloumn = "q.ReplyTime";
                    break;

                case 10:
                {
                    builder.Append(GetFilterByComplexSearch(keyword));
                    object input = DBHelper.ExecuteScalarSql("SELECT SUM(q.Score) FROM " + DBHelper.FilterBadChar("PE_Question q INNER JOIN PE_QuestionType t ON q.TypeID = t.TypeID") + " WHERE " + builder.ToString());
                    this.m_TotalScore = DataConverter.CLng(input);
                    break;
                }
                case 11:
                    builder.Append(" AND q.ID IN (SELECT DISTINCT QuestionID FROM PE_Reply WHERE ReplyCreator = '").Append(DBHelper.FilterBadChar(PEContext.Current.Admin.AdminName)).Append("')");
                    sortCloumn = "q.ReplyTime";
                    break;

                case 12:
                    builder.Append(" AND q.ID IN (SELECT QuestionID FROM PE_QuestionFavorite WHERE AdminID=").Append(PEContext.Current.Admin.AdministratorInfo.AdminId.ToString() + ")");
                    break;
            }
            return this.GetQuestionCommon(startRowIndex, maximumRows, builder.ToString(), sortCloumn);
        }

        public QuestionInfo GetQuestionById(int id)
        {
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT q.*, t.TypeName FROM PE_Question q INNER JOIN PE_QuestionType t On q.TypeID = t.TypeID WHERE q.ID = @ID", new Parameters("@ID", DbType.Int32, id)))
            {
                if (reader.Read())
                {
                    return GetInfoByReader(reader, true);
                }
                return new QuestionInfo(true);
            }
        }

        private IList<QuestionInfo> GetQuestionCommon(int startRowIndex, int maximumRows, string filter, string sortCloumn)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetListBySortColumn");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndex);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maximumRows);
            database.AddInParameter(storedProcCommand, "@PrimaryColumn", DbType.String, "q.ID");
            database.AddInParameter(storedProcCommand, "@SortColumnDbType", DbType.String, "DateTime");
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, sortCloumn);
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "q.ID, q.TypeID, t.TypeName, q.QuestionTitle, q.QuestionCreateTime, q.QuestionCreator, q.ReplyCreator, q.ReplyTime, q.Score, q.IsPublic, q.IsReply, q.IsSolved");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_Question q INNER JOIN PE_QuestionType t ON q.TypeID = t.TypeID");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, filter);
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maximumRows);
            IList<QuestionInfo> list = new List<QuestionInfo>();
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(GetInfoByReader(reader, false));
                }
            }
            this.m_TotalCount = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public int GetQuestionStatCount()
        {
            return this.m_StatisticCount;
        }

        public DataTable GetQuestionStatistic(int startRowIndex, int maximumRows)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetListBySortColumn");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndex);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maximumRows);
            database.AddInParameter(storedProcCommand, "@PrimaryColumn", DbType.String, "ID");
            database.AddInParameter(storedProcCommand, "@SortColumnDbType", DbType.String, "int");
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "QuestionCount");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "ID, QuestionCreator, QuestionCount, IsReply, IsSolved");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String);
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "View_QuestionStatistic");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maximumRows);
            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("QuestionCreator", typeof(string));
            table.Columns.Add("QuestionCount", typeof(int));
            table.Columns.Add("IsReply", typeof(int));
            table.Columns.Add("IsSolved", typeof(int));
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    DataRow row = table.NewRow();
                    row["ID"] = reader.GetInt32("ID");
                    row["QuestionCreator"] = reader.GetString("QuestionCreator");
                    row["QuestionCount"] = reader.GetInt32("QuestionCount");
                    row["IsReply"] = reader.GetInt32("IsReply");
                    row["IsSolved"] = reader.GetInt32("IsSolved");
                    table.Rows.Add(row);
                }
            }
            this.m_StatisticCount = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return table;
        }

        public int GetQuestionTotal()
        {
            return this.m_TotalCount;
        }

        public IList<QuestionInfo> GetQuestonsByUser(string userName, int startRowIndex, int maximumRows, int searchType, string keyword)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetListBySortColumn");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndex);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maximumRows);
            database.AddInParameter(storedProcCommand, "@PrimaryColumn", DbType.String, "ID");
            database.AddInParameter(storedProcCommand, "@SortColumnDbType", DbType.String, "DateTime");
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "LastUpdateTime");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "ID, QuestionTitle, LastUpdateTime, ReplyTime,ReplyCreator, IsReply, IsSolved");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_Question");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String);
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maximumRows);
            string str = " QuestionCreator = '" + DBHelper.FilterBadChar(userName) + "' ";
            switch (searchType)
            {
                case 1:
                    str = str + " AND IsReply = 1 AND IsSolved = 0 ";
                    storedProcCommand.Parameters["@SortColumn"].Value = "ReplyTime";
                    break;

                case 2:
                    str = str + " AND IsSolved = 1 ";
                    break;

                case 3:
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        str = str + " AND QuestionTitle LIKE '%" + DBHelper.FilterBadChar(keyword) + "%' ";
                    }
                    break;
            }
            storedProcCommand.Parameters["@Filter"].Value = str;
            IList<QuestionInfo> list = new List<QuestionInfo>();
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    QuestionInfo item = new QuestionInfo();
                    item.Id = reader.GetInt32("ID");
                    item.QuestionTitle = reader.GetString("QuestionTitle");
                    item.QuestionCreateTime = reader.GetDateTime("LastUpdateTime");
                    item.ReplyTime = reader.GetNullableDateTime("ReplyTime");
                    item.IsReply = reader.GetBoolean("IsReply");
                    item.IsSolved = reader.GetBoolean("IsSolved");
                    item.ReplyCreator = reader.GetString("ReplyCreator");
                    list.Add(item);
                }
            }
            this.m_TotalCount = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public IList<QuestionInfo> GetSolvedQuestions(int startRowIndex, int maximumRows, int searchType, string keyword)
        {
            string filter = " q.IsSolved = 1 AND q.IsPublic = 1 ";
            if (!string.IsNullOrEmpty(keyword))
            {
                switch (searchType)
                {
                    case 1:
                        filter = filter + " AND q.QuestionTitle LIKE '%" + DBHelper.FilterBadChar(keyword) + "%' ";
                        break;

                    case 2:
                        filter = filter + " AND t.TypeID = " + DBHelper.ToNumber(keyword);
                        break;
                }
            }
            return this.GetQuestionCommon(startRowIndex, maximumRows, filter, "q.ReplyTime");
        }

        public int GetTotalScore()
        {
            return this.m_TotalScore;
        }

        public int GetUserPointByUserName(string userName)
        {
            return DataConverter.CLng(DBHelper.ExecuteScalarSql("SELECT (UserPoint-(SELECT ISNULL(SUM(score), 0) FROM PE_Question WHERE QuestionCreator = @UserName AND IsSolved = 0)) AS UserPoint\r\nFROM PE_Users WHERE UserName = @UserName", new Parameters("@UserName", DbType.String, userName)));
        }

        public int GetUserReplyJustNow()
        {
            string strSql = "SELECT COUNT(ID) FROM PE_Question WHERE IsSolved=0 AND IsReply=1 AND ReplyCreator=QuestionCreator";
            return DataConverter.CLng(DBHelper.ExecuteScalarSql(strSql));
        }

        private static bool HasValue(string complexSearchItem)
        {
            return (!string.IsNullOrEmpty(complexSearchItem) && (complexSearchItem != "0"));
        }

        public void RemoveFavoriteById(string questionIdList)
        {
            DBHelper.ExecuteNonQuerySql("DELETE FROM PE_QuestionFavorite WHERE QuestionID IN(" + DBHelper.ToValidId(questionIdList) + ")");
        }

        public bool RemoveFavorites(string questionIdList)
        {
            return DBHelper.ExecuteSql("DELETE FROM PE_QuestionFavorite WHERE AdminID=" + PEContext.Current.Admin.AdministratorInfo.AdminId.ToString() + " AND QuestionID IN (" + DBHelper.ToValidId(questionIdList) + ")");
        }

        public bool SaveQuestionSetting(int questionId, int typeId, int score, bool isPublic, bool isSolved)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@IsPublic", DbType.Boolean, isPublic);
            cmdParams.AddInParameter("@IsSolved", DbType.Boolean, isSolved);
            cmdParams.AddInParameter("@Score", DbType.Int32, score);
            cmdParams.AddInParameter("@TypeID", DbType.Int32, typeId);
            cmdParams.AddInParameter("@ID", DbType.Int32, questionId);
            return DBHelper.ExecuteSql("UPDATE PE_Question SET TypeID = @TypeID, IsPublic = @IsPublic, IsSolved = @IsSolved, Score = @Score WHERE ID = @ID", cmdParams);
        }

        public bool Update(QuestionInfo info)
        {
            return DBHelper.ExecuteSql("UPDATE PE_Question SET ProductVersion = @ProductVersion, ProductDBType = @ProductDBType, SystemType = @SystemType, TypeId = @TypeId, QuestionTitle = @QuestionTitle, QuestionContent = @QuestionContent, LastUpdateTime = @LastUpdateTime, QuestionCreator = @QuestionCreator, IsPublic = @IsPublic, [IP]=@IP, ReplyCreator = @ReplyCreator, ReplyTime = @ReplyTime, IsReply = @IsReply, AntiVirus = @AntiVirus, FireWall = @FireWall, ErrorCode = @ErrorCode, ErrorText = @ErrorText, IsSolved = @IsSolved, Url = @Url, Score = @Score WHERE ID = @ID", GetParameters(info));
        }

        public bool Update(int questionId, string replyCreator, DateTime replyTime)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ReplyCreator", DbType.String, DBHelper.FilterBadChar(replyCreator));
            cmdParams.AddInParameter("@ReplyTime", DbType.DateTime, replyTime);
            cmdParams.AddInParameter("@ID", DbType.Int32, questionId);
            return DBHelper.ExecuteSql("UPDATE PE_Question SET ReplyCreator = @ReplyCreator, ReplyTime = @ReplyTime WHERE ID = @ID", cmdParams);
        }

        public bool Update(int questionId, string replyCreator, DateTime replyTime, bool isReply, bool isPublic, bool isSolved, int score)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@IsPublic", DbType.Boolean, isPublic);
            cmdParams.AddInParameter("@ReplyCreator", DbType.String, replyCreator);
            cmdParams.AddInParameter("@ReplyTime", DbType.DateTime, replyTime);
            cmdParams.AddInParameter("@IsReply", DbType.Boolean, isReply);
            cmdParams.AddInParameter("@IsSolved", DbType.Boolean, isSolved);
            cmdParams.AddInParameter("@Score", DbType.Int32, score);
            cmdParams.AddInParameter("@ID", DbType.Int32, questionId);
            return DBHelper.ExecuteSql("UPDATE PE_Question SET IsPublic = @IsPublic, ReplyCreator = @ReplyCreator, ReplyTime = @ReplyTime, IsReply = @IsReply, IsSolved = @IsSolved, Score = @Score WHERE ID = @ID", cmdParams);
        }

        public bool UpdateCreateTime(DateTime dt, int questionId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@LastUpdateTime", DbType.DateTime, dt);
            cmdParams.AddInParameter("@ID", DbType.Int32, questionId);
            return DBHelper.ExecuteSql("UPDATE PE_Question SET LastUpdateTime = @LastUpdateTime WHERE ID = @ID", cmdParams);
        }
    }
}

