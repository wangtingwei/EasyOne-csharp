namespace EasyOne.SqlServerDal.Analytics.Report
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.Common;
    using EasyOne.IDal.Analytics;
    using EasyOne.Model.Analytics;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    public class OtherReport : IOtherReport
    {
        private int m_Total;

        public int GetCurrentOnlineCount()
        {
            return DataConverter.CLng(DBHelper.ExecuteScalarSql("SELECT COUNT(*) FROM PE_StatOnline WHERE LastTime > DATEADD(ss,-(SELECT TOP 1 ISNULL(onlineTime, 0) FROM PE_StatInfoList), GETDATE())"));
        }

        public StatOnlineInfo GetOnlineByIP(string ip)
        {
            using (NullableDataReader reader = DBHelper.ExecuteReaderProc("PR_Analytics_GetOnlineByIP", new Parameters("@IP", DbType.String, ip)))
            {
                if (reader.Read())
                {
                    return StatOnlineInfoFromrdr(reader);
                }
                return new StatOnlineInfo(true);
            }
        }

        public StatInfoListInfo GetStatInfoListInfo()
        {
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT TOP 1 * FROM PE_StatInfoList"))
            {
                if (reader.Read())
                {
                    return StatInfoListFromrdr(reader);
                }
                return new StatInfoListInfo(true);
            }
        }

        public IList<StatOnlineInfo> GetStatOnlineList(int startRowIndexId, int maxiNumRows)
        {
            Database database = DatabaseFactory.CreateDatabase();
            IList<StatOnlineInfo> list = new List<StatOnlineInfo>();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxiNumRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "ID");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "*");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, " LastTime > DATEADD(ss,-(SELECT TOP 1 ISNULL(onlineTime, 0) FROM PE_StatInfoList), getdate()) ");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_StatOnline");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxiNumRows);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(StatOnlineInfoFromrdr(reader));
                }
            }
            this.m_Total = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public StatVisitorInfo GetStatVisitorById(int id)
        {
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_StatVisitor WHERE [Id] = @id ", new Parameters("@id", DbType.Int32, id)))
            {
                if (reader.Read())
                {
                    return StatVisitorFromrdr(reader);
                }
                return new StatVisitorInfo(true);
            }
        }

        public IList<StatVisitorInfo> GetStatVisitorList(int startRowIndexId, int maxNumberRows)
        {
            Database database = DatabaseFactory.CreateDatabase();
            IList<StatVisitorInfo> list = new List<StatVisitorInfo>();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "[Id]");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "*");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, "");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_StatVisitor");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(StatVisitorFromrdr(reader));
                }
            }
            this.m_Total = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public int GetTotalOfStatVisitor()
        {
            return this.m_Total;
        }

        public int GetTotalStatOnline()
        {
            return this.m_Total;
        }

        public int[] GetVisitList()
        {
            int[] numArray = new int[10];
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT TOP 1 [1], [2], [3], [4], [5], [6], [7], [8], [9], [10] FROM PE_StatVisit"))
            {
                if (reader.Read())
                {
                    for (int i = 0; i < 10; i++)
                    {
                        numArray[i] = reader.GetInt32(i);
                    }
                    return numArray;
                }
                return null;
            }
        }

        private static StatInfoListInfo StatInfoListFromrdr(NullableDataReader rdr)
        {
            StatInfoListInfo info = new StatInfoListInfo();
            info.StartDate = rdr.GetString("StartDate");
            info.TotalNum = rdr.GetInt32("TotalNum");
            info.TotalView = rdr.GetInt32("TotalView");
            info.MonthNum = rdr.GetInt32("MonthNum");
            info.MonthMaxNum = rdr.GetInt32("MonthMaxNum");
            info.OldMonth = rdr.GetString("OldMonth");
            info.MonthMaxDate = rdr.GetString("MonthMaxDate");
            info.DayNum = rdr.GetInt32("DayNum");
            info.DayMaxNum = rdr.GetInt32("DayMaxNum");
            info.OldDay = rdr.GetString("OldDay");
            info.DayMaxDate = rdr.GetString("DayMaxDate");
            info.HourNum = rdr.GetInt32("HourNum");
            info.HourMaxNum = rdr.GetInt32("HourMaxNum");
            info.OldHour = rdr.GetString("OldHour");
            info.HourMaxTime = rdr.GetString("HourMaxTime");
            info.ChinaNum = rdr.GetInt32("ChinaNum");
            info.OtherNum = rdr.GetInt32("OtherNum");
            info.MasterTimeZone = rdr.GetInt32("MasterTimeZone");
            info.Interval = rdr.GetInt32("Interval");
            info.IntervalNum = rdr.GetInt32("IntervalNum");
            info.OnlineTime = rdr.GetInt32("OnlineTime");
            info.VisitRecord = rdr.GetInt32("VisitRecord");
            info.KillRefresh = rdr.GetInt32("KillRefresh");
            info.RegFieldsFill = rdr.GetString("RegFields_Fill");
            info.OldTotalNum = rdr.GetInt32("OldTotalNum");
            info.OldTotalView = rdr.GetInt32("OldTotalView");
            return info;
        }

        private static StatOnlineInfo StatOnlineInfoFromrdr(NullableDataReader rdr)
        {
            StatOnlineInfo info = new StatOnlineInfo();
            info.Id = rdr.GetInt32("id");
            info.UserIP = rdr.GetString("UserIP");
            info.UserPage = rdr.GetString("UserPage");
            info.UserAgent = rdr.GetString("UserAgent");
            info.OnTime = rdr.GetDateTime("OnTime");
            info.LastTime = rdr.GetDateTime("LastTime");
            return info;
        }

        private static StatVisitorInfo StatVisitorFromrdr(NullableDataReader rdr)
        {
            StatVisitorInfo info = new StatVisitorInfo();
            info.Id = rdr.GetInt32("Id");
            info.VTime = rdr.GetDateTime("VTime");
            info.IP = rdr.GetString("Ip");
            info.Address = rdr.GetString("Address");
            info.System = rdr.GetString("System");
            info.Browser = rdr.GetString("Browser");
            info.Screen = rdr.GetString("Screen");
            info.Color = rdr.GetString("Color");
            info.Referer = rdr.GetString("Referer");
            info.Timezone = rdr.GetInt32("Timezone");
            return info;
        }
    }
}

