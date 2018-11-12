namespace EasyOne.SqlServerDal.UserManage
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.IDal.UserManage;
    using EasyOne.Model.UserManage;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    public class UserValidLog : IUserValidLog
    {
        private int m_CountNumber;

        public bool Add(EasyOne.Model.UserManage.UserValidLogInfo userValidLogInfo)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@LogID", DbType.Int32, userValidLogInfo.LogId);
            cmdParams.AddInParameter("@UserName", DbType.String, userValidLogInfo.UserName);
            cmdParams.AddInParameter("@ValidNum", DbType.Int32, userValidLogInfo.ValidNum);
            cmdParams.AddInParameter("@IncomePayout", DbType.Int32, userValidLogInfo.IncomePayout);
            cmdParams.AddInParameter("@LogTime", DbType.DateTime, userValidLogInfo.LogTime);
            cmdParams.AddInParameter("@Remark", DbType.String, userValidLogInfo.Remark);
            cmdParams.AddInParameter("@IP", DbType.String, userValidLogInfo.IP);
            cmdParams.AddInParameter("@Inputer", DbType.String, userValidLogInfo.Inputer);
            cmdParams.AddInParameter("@Memo", DbType.String, userValidLogInfo.Memo);
            return DBHelper.ExecuteProc("PR_UserManage_UserValidLog_Add", cmdParams);
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
                    str = str + " AND UserName LIKE '%" + DBHelper.FilterBadChar(str6) + "%' ";
                }
                if (!string.IsNullOrEmpty(str7))
                {
                    str = str + " AND DATEDIFF(dd, LogTime, '" + str7 + "') = 0 ";
                }
                if (!string.IsNullOrEmpty(str8))
                {
                    str = str + " AND Inputer LIKE '%" + DBHelper.FilterBadChar(str8) + "%' ";
                }
                if (!string.IsNullOrEmpty(str9))
                {
                    str = str + " AND IP LIKE '%" + str9.Replace("'", "") + "%' ";
                }
            }
            return str;
        }

        public bool Delete(DateTime tempDate)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@Date", DbType.DateTime, tempDate);
            return DBHelper.ExecuteProc("PR_UserManage_UserValidLog_Delete", cmdParams);
        }

        public bool Delete(string userName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@UserName", DbType.String, userName);
            return DBHelper.ExecuteProc("PR_UserManage_UserValidLog_DeleteUser", cmdParams);
        }

        public bool Exists(string userName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@UserName", DbType.String, userName);
            return DBHelper.ExistsProc("PR_UserManage_UserValidLog_Exists", cmdParams);
        }

        public int GetCountNumber()
        {
            return this.m_CountNumber;
        }

        public IList<EasyOne.Model.UserManage.UserValidLogInfo> GetValidList(int startRowIndexId, int maxNumberRows, int scopesType, int field, string keyword)
        {
            Database database = DatabaseFactory.CreateDatabase();
            IList<EasyOne.Model.UserManage.UserValidLogInfo> list = new List<EasyOne.Model.UserManage.UserValidLogInfo>();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "LogID");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "LogID, UserName, ValidNum, IncomePayout, LogTime, Remark, IP, Inputer");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, "");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_ValidLog");
            switch (scopesType)
            {
                case 0:
                    database.SetParameterValue(storedProcCommand, "@Filter", " DATEDIFF(dd, LogTime, GETDATE()) < 10 ");
                    break;

                case 1:
                    database.SetParameterValue(storedProcCommand, "@Filter", " DATEDIFF(dd, LogTime, GETDATE()) < " + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
                    break;

                case 2:
                    database.SetParameterValue(storedProcCommand, "@Filter", " IncomePayout = 1 ");
                    break;

                case 3:
                    database.SetParameterValue(storedProcCommand, "@Filter", " IncomePayout = 2 ");
                    break;

                case 10:
                    if (field != 1)
                    {
                        database.SetParameterValue(storedProcCommand, "@Filter", " DATEDIFF(dd, LogTime, '" + DBHelper.FilterBadChar(keyword) + "') = 0 ");
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
                    list.Add(UserValidLogInfo(reader));
                }
            }
            this.m_CountNumber = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public EasyOne.Model.UserManage.UserValidLogInfo GetValidLogById(int logId)
        {
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_ValidLog WHERE LogID = @LogID", new Parameters("@LogID", DbType.String, logId)))
            {
                if (reader.Read())
                {
                    EasyOne.Model.UserManage.UserValidLogInfo info = UserValidLogInfo(reader);
                    info.Memo = reader.GetString("Memo");
                    return info;
                }
                return new EasyOne.Model.UserManage.UserValidLogInfo(true);
            }
        }

        public EasyOne.Model.UserManage.UserValidLogInfo GetValidLogByIdAndUserName(int logId, string userName)
        {
            string strSql = "SELECT * FROM PE_ValidLog WHERE LogID = @LogID AND UserName = @UserName";
            Parameters cmdParams = new Parameters("@LogID", DbType.Int32, logId);
            cmdParams.AddInParameter("@UserName", DbType.String, userName);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, cmdParams))
            {
                if (reader.Read())
                {
                    EasyOne.Model.UserManage.UserValidLogInfo info = UserValidLogInfo(reader);
                    info.Memo = reader.GetString("Memo");
                    return info;
                }
                return new EasyOne.Model.UserManage.UserValidLogInfo(true);
            }
        }

        private static EasyOne.Model.UserManage.UserValidLogInfo UserValidLogInfo(NullableDataReader rdr)
        {
            EasyOne.Model.UserManage.UserValidLogInfo info = new EasyOne.Model.UserManage.UserValidLogInfo();
            info.LogId = rdr.GetInt32("LogID");
            info.UserName = rdr.GetString("UserName");
            info.ValidNum = rdr.GetInt32("ValidNum");
            info.LogTime = rdr.GetDateTime("LogTime");
            info.IncomePayout = rdr.GetInt32("IncomePayout");
            info.Remark = rdr.GetString("Remark");
            info.IP = rdr.GetString("IP");
            info.Inputer = rdr.GetString("Inputer");
            return info;
        }
    }
}

