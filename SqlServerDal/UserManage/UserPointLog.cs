namespace EasyOne.SqlServerDal.UserManage
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.Common;
    using EasyOne.IDal.UserManage;
    using EasyOne.Model.UserManage;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    public class UserPointLog : IUserPointLog
    {
        private int m_CountNumber;

        public bool Add(EasyOne.Model.UserManage.UserPointLogInfo userPointLogInfo)
        {
            return DBHelper.ExecuteProc("PR_UserManage_UserPointLog_Add", GetProcdbComm(userPointLogInfo));
        }

        private static string ComplexSearch(string keyword)
        {
            string str = "1 = 1";
            if (!string.IsNullOrEmpty(keyword))
            {
                string[] strArray = keyword.Split(new char[] { '|' });
                string str2 = strArray[0];
                string str3 = strArray[1];
                string str4 = strArray[2];
                string str5 = strArray[3];
                string str6 = strArray[4];
                string str7 = strArray[5];
                string str8 = strArray[6];
                string str9 = strArray[7];
                if (!string.IsNullOrEmpty(str2) && !string.IsNullOrEmpty(str3))
                {
                    string str10 = str;
                    str = str10 + " AND (LogID Between " + DBHelper.ToNumber(str2) + " AND " + DBHelper.ToNumber(str3) + ") ";
                }
                if (!string.IsNullOrEmpty(str4) && !string.IsNullOrEmpty(str5))
                {
                    string str11 = str;
                    str = str11 + " AND (LogTime Between '" + str4.Replace("'", "") + "' AND '" + str5.Replace("'", "") + "') ";
                }
                if (!string.IsNullOrEmpty(str6))
                {
                    str = str + " AND UserName = '" + DBHelper.FilterBadChar(str6) + "' ";
                }
                if (!string.IsNullOrEmpty(str7))
                {
                    str = str + " AND DATEDIFF(dd, LogTime, '" + str7.Replace("'", "") + "') = 0 ";
                }
                if (!string.IsNullOrEmpty(str8))
                {
                    str = str + " AND Inputer LIKE '%" + DBHelper.FilterBadChar(str8) + "%' ";
                }
                if (!string.IsNullOrEmpty(str9))
                {
                    str = str + " AND IP LIKE '%" + DBHelper.FilterBadChar(str9) + "%' ";
                }
            }
            return str;
        }

        public bool Delete(DateTime tempDate)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@Date", DbType.DateTime, tempDate);
            return DBHelper.ExecuteProc("PR_UserManage_UserPointLog_Delete", cmdParams);
        }

        public bool Delete(string userName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@UserName", DbType.String, userName);
            return DBHelper.ExecuteProc("PR_UserManage_UserPointLog_DeleteUser", cmdParams);
        }

        public bool Exists(string userName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@UserName", DbType.String, userName);
            return DBHelper.ExistsSql("SELECT COUNT(UserName) FROM PE_PointLog WHERE UserName = @UserName", cmdParams);
        }

        public int GetCountNumber()
        {
            return this.m_CountNumber;
        }

        private static ArrayList GetIncomeAndPayout(string sql)
        {
            ArrayList list = new ArrayList();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(sql))
            {
                if (reader.Read())
                {
                    list.Add(reader[0]);
                }
                if (reader.NextResult() && reader.Read())
                {
                    list.Add(reader[0]);
                }
            }
            return list;
        }

        public IList<EasyOne.Model.UserManage.UserPointLogInfo> GetPointList(int startRowIndexId, int maxNumberRows, int scopesType, int field, string keyword)
        {
            Database database = DatabaseFactory.CreateDatabase();
            IList<EasyOne.Model.UserManage.UserPointLogInfo> list = new List<EasyOne.Model.UserManage.UserPointLogInfo>();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "LogID");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "LogID, UserName, ModuleType, InfoID, Point, LogTime, Times, IncomePayOut, Remark, IP, Inputer");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, "");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_PointLog");
            switch (scopesType)
            {
                case 0:
                    database.SetParameterValue(storedProcCommand, "@Filter", " DATEDIFF(dd, LogTime, GETDATE()) < 10 ");
                    break;

                case 1:
                    database.SetParameterValue(storedProcCommand, "@Filter", " DATEDIFF(dd, LogTime, GETDATE()) < " + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) + " ");
                    break;

                case 2:
                    database.SetParameterValue(storedProcCommand, "@Filter", " IncomePayout = 1 ");
                    break;

                case 3:
                    database.SetParameterValue(storedProcCommand, "@Filter", " IncomePayout = 2 ");
                    break;

                case 4:
                    database.SetParameterValue(storedProcCommand, "@Filter", " Income_Payout <= 2 ");
                    break;

                case 10:
                    if (field != 1)
                    {
                        database.SetParameterValue(storedProcCommand, "@Filter", " DATEDIFF(dd, LogTime, '" + keyword + "') = 0 ");
                        break;
                    }
                    database.SetParameterValue(storedProcCommand, "@Filter", " UserName = '" + DBHelper.FilterBadChar(keyword) + "' ");
                    break;

                case 11:
                    database.SetParameterValue(storedProcCommand, "@Filter", ComplexSearch(keyword));
                    break;
            }
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(UserPointLogInfo(reader));
                }
            }
            this.m_CountNumber = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public EasyOne.Model.UserManage.UserPointLogInfo GetPointLogById(int logId)
        {
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_PointLog WHERE LogID = @LogID", new Parameters("@LogID", DbType.String, logId)))
            {
                if (reader.Read())
                {
                    EasyOne.Model.UserManage.UserPointLogInfo info = UserPointLogInfo(reader);
                    info.Memo = reader.GetString("Memo");
                    return info;
                }
                return new EasyOne.Model.UserManage.UserPointLogInfo(true);
            }
        }

        public EasyOne.Model.UserManage.UserPointLogInfo GetPointLogByIdAndUserName(int logId, string userName)
        {
            string strSql = "SELECT * FROM PE_PointLog WHERE LogID = @LogID AND UserName = @UserName";
            Parameters cmdParams = new Parameters("@LogID", DbType.Int32, logId);
            cmdParams.AddInParameter("@UserName", DbType.String, userName);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, cmdParams))
            {
                if (reader.Read())
                {
                    EasyOne.Model.UserManage.UserPointLogInfo info = UserPointLogInfo(reader);
                    info.Memo = reader.GetString("Memo");
                    return info;
                }
                return new EasyOne.Model.UserManage.UserPointLogInfo(true);
            }
        }

        private static Parameters GetProcdbComm(EasyOne.Model.UserManage.UserPointLogInfo userPointLogInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@LogID", DbType.Int32, userPointLogInfo.LogId);
            parameters.AddInParameter("@UserName", DbType.String, userPointLogInfo.UserName);
            parameters.AddInParameter("@ModuleType", DbType.Int32, userPointLogInfo.ModuleType);
            parameters.AddInParameter("@InfoID", DbType.Int32, userPointLogInfo.InfoId);
            parameters.AddInParameter("@Point", DbType.Int32, userPointLogInfo.Point);
            parameters.AddInParameter("@LogTime", DbType.DateTime, userPointLogInfo.LogTime);
            parameters.AddInParameter("@Times", DbType.Int32, userPointLogInfo.Times);
            parameters.AddInParameter("@IncomePayOut", DbType.Int32, userPointLogInfo.IncomePayOut);
            parameters.AddInParameter("@Remark", DbType.String, userPointLogInfo.Remark);
            parameters.AddInParameter("@IP", DbType.String, userPointLogInfo.IP);
            parameters.AddInParameter("@Inputer", DbType.String, userPointLogInfo.Inputer);
            parameters.AddInParameter("@Memo", DbType.String, userPointLogInfo.Memo);
            return parameters;
        }

        public ArrayList GetTotalInComeAndPayOutAll()
        {
            return GetIncomeAndPayout("SELECT ISNULL(SUM(Point), 0) FROM PE_PointLog WHERE [Point]>0 AND IncomePayout = 1 ; SELECT ISNULL(SUM(Point), 0) FROM PE_PointLog WHERE Point > 0 AND IncomePayout = 2");
        }

        public ArrayList GetTotalInComeAndPayOutAll(string userName)
        {
            return GetIncomeAndPayout("SELECT ISNULL(SUM(Point), 0) FROM PE_PointLog WHERE [Point]>0 AND IncomePayout = 1 AND UserName = '" + DBHelper.FilterBadChar(userName) + "'; SELECT ISNULL(SUM(Point), 0) FROM PE_PointLog WHERE Point > 0 AND IncomePayout = 2 AND UserName = '" + DBHelper.FilterBadChar(userName) + "'");
        }

        public int GetValidPointLogId(string userName, int moduleType, int infoId, int chargeType, int pitchTime, int readTimes)
        {
            Parameters cmdParams = new Parameters();
            string strSql = "SELECT TOP 1 LogID FROM PE_PointLog";
            string str2 = string.Concat(new object[] { " WHERE UserName = '", DBHelper.FilterBadChar(userName), "' AND ModuleType = ", moduleType, " AND InfoID = ", infoId, " AND IncomePayout = 2 " });
            switch (chargeType)
            {
                case 0:
                    strSql = strSql + str2;
                    break;

                case 1:
                    strSql = strSql + str2 + " AND DateDiff(hh, LogTime, GETDATE())<" + pitchTime.ToString();
                    break;

                case 2:
                    strSql = strSql + str2 + " AND Times< " + readTimes.ToString();
                    break;

                case 3:
                {
                    string str3 = strSql;
                    strSql = str3 + str2 + " AND DateDiff(hh, LogTime, GETDATE())<" + pitchTime.ToString() + " OR Times<" + readTimes.ToString();
                    break;
                }
                case 4:
                {
                    string str4 = strSql;
                    strSql = str4 + str2 + " AND DateDiff(hh, LogTime, GETDATE())<" + pitchTime.ToString() + " AND Times<" + readTimes.ToString();
                    break;
                }
                case 5:
                    strSql = strSql + " WHERE 1 = 0 ";
                    break;
            }
            return DataConverter.CLng(DBHelper.ExecuteScalarSql(strSql, cmdParams));
        }

        public int PointSum(int sumType)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@SumType", DbType.String, sumType);
            try
            {
                return DataConverter.CLng(DBHelper.ExecuteScalarProc("PR_UserManage_UserPointLog_PointSum", cmdParams));
            }
            catch
            {
                return 0;
            }
        }

        public int PointSum(int sumType, string userName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@SumType", DbType.String, sumType);
            cmdParams.AddInParameter("@UserName", DbType.String, userName);
            try
            {
                return DataConverter.CLng(DBHelper.ExecuteScalarProc("PR_UserManage_UserPointLog_PointSum", cmdParams));
            }
            catch
            {
                return 0;
            }
        }

        public bool UpdateTimes(int id, string ip)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@Id", DbType.String, id);
            cmdParams.AddInParameter("@Ip", DbType.String, ip);
            return DBHelper.ExecuteProc("PR_UserManage_UserPointLog_UpdateTimes", cmdParams);
        }

        public void UpdateTimes(string userTrueIP, int logId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@userTrueIP", DbType.String, userTrueIP);
            cmdParams.AddInParameter("@LogId", DbType.Int32, logId);
            string strSql = "UPDATE PE_PointLog SET Times = Times + 1, IP = @userTrueIP WHERE LogID = @LogId";
            DBHelper.ExecuteSql(strSql, cmdParams);
        }

        private static EasyOne.Model.UserManage.UserPointLogInfo UserPointLogInfo(NullableDataReader rdr)
        {
            EasyOne.Model.UserManage.UserPointLogInfo info = new EasyOne.Model.UserManage.UserPointLogInfo();
            info.LogId = rdr.GetInt32("LogID");
            info.UserName = rdr.GetString("UserName");
            info.ModuleType = rdr.GetInt32("ModuleType");
            info.InfoId = rdr.GetInt32("InfoID");
            info.Point = rdr.GetInt32("Point");
            info.LogTime = rdr.GetDateTime("LogTime");
            info.Times = rdr.GetInt32("Times");
            info.IncomePayOut = rdr.GetInt32("IncomePayOut");
            info.Remark = rdr.GetString("Remark");
            info.IP = rdr.GetString("IP");
            info.Inputer = rdr.GetString("Inputer");
            return info;
        }

        public int ViewInfosOneDay(string userName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@UserName", DbType.String, userName);
            string strSql = "SELECT COUNT(0) FROM PE_PointLog WHERE UserName = @UserName AND IncomePayout = 2 AND InfoID > 0 AND DateDiff(d, LogTime, GETDATE()) < 1";
            return DataConverter.CLng(DBHelper.ExecuteScalarSql(strSql, cmdParams));
        }

        public int ViewTotalInfos(string userName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@UserName", DbType.String, userName);
            string strSql = "SELECT COUNT(0) FROM PE_PointLog WHERE UserName = @UserName AND IncomePayout = 2 AND InfoID > 0";
            return DataConverter.CLng(DBHelper.ExecuteScalarSql(strSql, cmdParams));
        }
    }
}

