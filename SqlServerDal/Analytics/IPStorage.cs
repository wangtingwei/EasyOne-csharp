namespace EasyOne.SqlServerDal.Analytics
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.IDal.Analytics;
    using EasyOne.Model.Analytics;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    public class IPStorage : IIPStorage
    {
        private int m_IPTotal;

        public bool Add(StatIPInfo info)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@StartIp", DbType.Double, info.StartIP);
            cmdParams.AddInParameter("@EndIp", DbType.Double, info.EndIP);
            cmdParams.AddInParameter("@Address", DbType.String, info.Address);
            return DBHelper.ExecuteProc("PR_Analytics_StatIPInfo_Add", cmdParams);
        }

        public bool Delete(StatIPInfo info)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@StartIp", DbType.Double, info.StartIP);
            cmdParams.AddInParameter("@EndIp", DbType.Double, info.EndIP);
            return DBHelper.ExecuteProc("PR_Analytics_StatIPInfo_Delete", cmdParams);
        }

        public string GetAddressByIP(double ip)
        {
            new Parameters().AddInParameter("@ip", DbType.Double, ip);
            object objA = DBHelper.ExecuteScalarSql("SELECT TOP 1 Address From PE_StatIpInfo WHERE StartIp <= @ip AND EndIp >= @ip ORDER BY EndIp - StartIp ASC");
            if (!object.Equals(objA, null))
            {
                return objA.ToString();
            }
            return null;
        }

        public IList<StatIPInfo> GetList(int startRowIndexId, int maxiNumRows, double searchIP, string searchAddress)
        {
            string str = "1 = 1";
            Database database = DatabaseFactory.CreateDatabase();
            IList<StatIPInfo> list = new List<StatIPInfo>();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxiNumRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "ID");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "*");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_StatIpInfo");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxiNumRows);
            if (searchIP != 0.0)
            {
                str = str + string.Format(" AND StartIp<={0} AND EndIp >={0}", searchIP.ToString());
            }
            if (!string.IsNullOrEmpty(searchAddress))
            {
                str = str + " AND Address LIKE '%" + DBHelper.FilterBadChar(searchAddress) + "%' ";
            }
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, str);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(StatIPInfoListFromrdr(reader));
                }
            }
            this.m_IPTotal = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public int GetTotal()
        {
            return this.m_IPTotal;
        }

        private static StatIPInfo StatIPInfoListFromrdr(NullableDataReader rdr)
        {
            StatIPInfo info = new StatIPInfo();
            info.StartIP = rdr.GetDouble("StartIp");
            info.EndIP = rdr.GetDouble("EndIp");
            info.Address = rdr.GetString("Address");
            return info;
        }

        public bool Update(StatIPInfo newInfo, StatIPInfo oldInfo)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@StartIp", DbType.Double, newInfo.StartIP);
            cmdParams.AddInParameter("@EndIp", DbType.Double, newInfo.EndIP);
            cmdParams.AddInParameter("@Address", DbType.String, newInfo.Address);
            cmdParams.AddInParameter("@oldStartIp", DbType.Double, oldInfo.StartIP);
            cmdParams.AddInParameter("@oldEndIp", DbType.Double, oldInfo.EndIP);
            return (DBHelper.ExecuteNonQueryProc("PR_Analytics_StatIPInfo_Update", cmdParams) > 0);
        }
    }
}

