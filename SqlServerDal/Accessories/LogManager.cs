namespace EasyOne.SqlServerDal.Accessories
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.Logging;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    public class LogManager : ILogManager
    {
        private int m_TotalOfLog;

        public void Add(LogInfo logInfo)
        {
            DBHelper.ExecuteProc("PR_Accessories_Log_Add", GetParameters(logInfo));
        }

        public bool Delete(DateTime time)
        {
            return DBHelper.ExecuteSql("DELETE FROM PE_Log WHERE DATEDIFF(dd, [timestamp], @time) > 0", new Parameters("@time", DbType.DateTime, time));
        }

        public bool Delete(string id)
        {
            return DBHelper.ExecuteSql("DELETE FROM PE_Log WHERE LogID IN ( " + DBHelper.ToValidId(id) + " )");
        }

        public IList<LogInfo> GetList(int startRowIndexId, int maxNumberRows)
        {
            return this.GetList(startRowIndexId, maxNumberRows, (string) null);
        }

        public IList<LogInfo> GetList(int startRowIndexId, int maxNumberRows, LogCategory category)
        {
            return this.GetList(startRowIndexId, maxNumberRows, string.Format("Category = {0}", (int) category));
        }

        private IList<LogInfo> GetList(int startRowIndexId, int maxNumberRows, string filter)
        {
            Database database = DatabaseFactory.CreateDatabase();
            IList<LogInfo> list = new List<LogInfo>();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "LogID");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "*");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, string.IsNullOrEmpty(filter) ? "" : filter);
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_Log");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(LogFromrdr(reader));
                }
            }
            this.m_TotalOfLog = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public IList<LogInfo> GetList(int startRowIndexId, int maxNumberRows, string searchType, string keyword)
        {
            string filter = string.Empty;
            string str2 = searchType;
            if (str2 != null)
            {
                if (!(str2 == "UserName"))
                {
                    if (str2 == "Title")
                    {
                        filter = "Title LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'";
                    }
                    else if (str2 == "UserIP")
                    {
                        filter = "UserIP LIKE '" + DBHelper.FilterBadChar(keyword) + "'";
                    }
                }
                else
                {
                    filter = "UserName LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'";
                }
            }
            return this.GetList(startRowIndexId, maxNumberRows, filter);
        }

        public LogInfo GetLogById(int id)
        {
            LogInfo info = null;
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_Log WHERE LogID = @LogID", new Parameters("@LogID", DbType.Int32, id)))
            {
                if (reader.Read())
                {
                    info = LogFromrdr(reader);
                }
            }
            return info;
        }

        private static Parameters GetParameters(LogInfo logInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@Category", DbType.Int32, (int) logInfo.Category);
            parameters.AddInParameter("@Priority", DbType.Int32, logInfo.Priority);
            parameters.AddInParameter("@Title", DbType.String, logInfo.Title);
            parameters.AddInParameter("@Message", DbType.AnsiString, logInfo.Message);
            parameters.AddInParameter("@Timestamp", DbType.DateTime, logInfo.Timestamp);
            parameters.AddInParameter("@UserName", DbType.String, logInfo.UserName);
            parameters.AddInParameter("@UserIP", DbType.String, logInfo.UserIP);
            parameters.AddInParameter("@Source", DbType.AnsiString, logInfo.Source);
            parameters.AddInParameter("@ScriptName", DbType.String, logInfo.ScriptName);
            parameters.AddInParameter("@PostString", DbType.AnsiString, logInfo.PostString);
            return parameters;
        }

        public int GetTotalOfLog()
        {
            return this.m_TotalOfLog;
        }

        private static LogInfo LogFromrdr(NullableDataReader rdr)
        {
            LogInfo info = new LogInfo();
            info.LogId = rdr.GetInt32("LogID");
            info.Category = (LogCategory) rdr.GetInt32("Category");
            info.Priority = (LogPriority) rdr.GetInt32("Priority");
            info.Title = rdr.GetString("Title");
            info.Message = rdr.GetString("Message");
            info.Timestamp = rdr.GetDateTime("Timestamp");
            info.UserName = rdr.GetString("UserName");
            info.UserIP = rdr.GetString("UserIP");
            info.Source = rdr.GetString("Source");
            info.ScriptName = rdr.GetString("ScriptName");
            info.PostString = rdr.GetString("PostString");
            return info;
        }

        public bool Update(LogInfo logInfo)
        {
            Parameters cmdParams = GetParameters(logInfo);
            cmdParams.AddInParameter("@LogID", DbType.Int32, logInfo.LogId);
            return DBHelper.ExecuteProc("PR_Accessories_Log_Update", cmdParams);
        }
    }
}

