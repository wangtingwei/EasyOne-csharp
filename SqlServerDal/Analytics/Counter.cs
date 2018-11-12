namespace EasyOne.SqlServerDal.Analytics
{
    using EasyOne.Common;
    using EasyOne.IDal.Analytics;
    using EasyOne.Model.Analytics;
    using EasyOne.SqlServerDal;
    using System;
    using System.Data;

    public class Counter : ICounter
    {
        public bool DoInit()
        {
            return DBHelper.ExecuteProc("PR_Analytics_DoInit");
        }

        public int GetInterval()
        {
            return DataConverter.CLng(DBHelper.ExecuteScalarSql("SELECT TOP 1 interval FROM PE_StatinfoList"), 60);
        }

        private static Parameters GetStatVisitorParameters(StatVisitorInfo statVisitorInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@Ip", DbType.AnsiString, statVisitorInfo.IP);
            parameters.AddInParameter("@Address", DbType.String, statVisitorInfo.Address);
            parameters.AddInParameter("@System", DbType.AnsiString, statVisitorInfo.System);
            parameters.AddInParameter("@Browser", DbType.AnsiString, statVisitorInfo.Browser);
            parameters.AddInParameter("@Screen", DbType.AnsiString, statVisitorInfo.Screen);
            parameters.AddInParameter("@Color", DbType.AnsiString, statVisitorInfo.Color);
            parameters.AddInParameter("@Referer", DbType.AnsiString, statVisitorInfo.Referer);
            parameters.AddInParameter("@Timezone", DbType.Int32, statVisitorInfo.Timezone);
            return parameters;
        }

        public bool SaveConfig(StatInfoListInfo info)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@MasterTimeZone", DbType.Int32, info.MasterTimeZone);
            cmdParams.AddInParameter("@Interval", DbType.Int32, info.Interval);
            cmdParams.AddInParameter("@IntervalNum", DbType.Int32, info.IntervalNum);
            cmdParams.AddInParameter("@OnlineTime", DbType.Int32, info.OnlineTime);
            cmdParams.AddInParameter("@VisitRecord", DbType.Int32, info.VisitRecord);
            cmdParams.AddInParameter("@KillRefresh", DbType.Int32, info.KillRefresh);
            cmdParams.AddInParameter("@RegFields_Fill", DbType.String, info.RegFieldsFill);
            cmdParams.AddInParameter("@OldTotalNum", DbType.Int32, info.OldTotalNum);
            cmdParams.AddInParameter("@OldTotalView", DbType.Int32, info.OldTotalView);
            cmdParams.AddInParameter("@StartDate", DbType.String, info.StartDate);
            return (DBHelper.ExecuteNonQueryProc("PR_Analytics_SaveConfig", cmdParams) > 0);
        }

        public void StatInfoListAddView()
        {
            DBHelper.ExecuteNonQuerySql("UPDATE PE_StatInfoList SET TotalView = TotalView + 1");
        }

        public void StatOnlineAdd(StatOnlineInfo info)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@UserIP", DbType.String, info.UserIP);
            cmdParams.AddInParameter("@UserAgent", DbType.String, info.UserAgent);
            cmdParams.AddInParameter("@UserPage", DbType.String, info.UserPage);
            DBHelper.ExecuteNonQueryProc("PR_Analytics_StatOnline_Add", cmdParams);
        }

        public void StatUpdate(StatUpdateInfo updateInfo, string address)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@_visit", DbType.Int32, updateInfo.VisitNum);
            cmdParams.AddInParameter("@_IP", DbType.String, updateInfo.IP);
            cmdParams.AddInParameter("@_address", DbType.String, address);
            cmdParams.AddInParameter("@_system", DbType.String, updateInfo.System);
            cmdParams.AddInParameter("@_browser", DbType.String, updateInfo.Browser);
            cmdParams.AddInParameter("@_screen", DbType.String, updateInfo.Screen);
            cmdParams.AddInParameter("@_color", DbType.String, updateInfo.Color);
            cmdParams.AddInParameter("@_referer", DbType.String, updateInfo.Referer);
            cmdParams.AddInParameter("@_weburl", DbType.String, updateInfo.Weburl);
            cmdParams.AddInParameter("@_timezone", DbType.String, updateInfo.Timezone);
            cmdParams.AddInParameter("@_visitTimezone", DbType.Int32, updateInfo.VisitTimezone);
            cmdParams.AddInParameter("@_keyword", DbType.String, updateInfo.Keyword);
            cmdParams.AddInParameter("@_mozilla", DbType.String, updateInfo.Mozilla);
            DBHelper.ExecuteNonQueryProc("PR_Analytics_StatUpdate", cmdParams);
        }

        public bool StatVisitorAdd(StatVisitorInfo statVisitorInfo)
        {
            return DBHelper.ExecuteProc("PR_Analytics_StatVisitor_Add", GetStatVisitorParameters(statVisitorInfo));
        }

        public bool VisitUpdate(int visitCount)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@visit", DbType.Int32, visitCount);
            return (DBHelper.ExecuteNonQueryProc("PR_Analytics_StatVisit_Update", cmdParams) > 0);
        }
    }
}

