namespace EasyOne.SqlServerDal.AD
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.Enumerations;
    using EasyOne.IDal.AD;
    using EasyOne.Model.AD;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.OleDb;
    using System.Text;

    public sealed class ADZone : IADZone
    {
        private int m_NumADZones;

        public bool ActiveADZone(string id)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@strZoneId", DbType.String, id);
            return DBHelper.ExecuteProc("PR_AD_ADZone_Active", cmdParams);
        }

        public bool Add(ADZoneInfo adZoneInfo)
        {
            Parameters cmdParams = GetParameters(adZoneInfo);
            cmdParams.AddInParameter("@ZoneId", DbType.Int32, GetMaxADZoneId());
            return DBHelper.ExecuteProc("PR_Ad_ADZone_ADD", cmdParams);
        }

        private static string BuildQuery(ADZoneSearchType listType, string keywords)
        {
            switch (listType)
            {
                case ADZoneSearchType.ZoneName:
                    return ("ZoneName LIKE '%" + DBHelper.FilterBadChar(keywords) + "%'");

                case ADZoneSearchType.ZoneIntro:
                    return ("ZoneIntro LIKE '%" + DBHelper.FilterBadChar(keywords) + "%'");
            }
            return null;
        }

        public bool ClearADZone(int id)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ZoneId", DbType.Int32, id);
            return DBHelper.ExecuteSql("DELETE FROM PE_Zone_Advertisement WHERE ZoneId = @ZoneId", cmdParams);
        }

        public bool CopyADZone(int id)
        {
            int maxADZoneId = GetMaxADZoneId();
            string newJSName = this.GetNewJSName();
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@MaxId", DbType.Int32, maxADZoneId);
            cmdParams.AddInParameter("@ZoneJsName", DbType.String, newJSName);
            cmdParams.AddInParameter("@ZoneId", DbType.Int32, id);
            bool flag = false;
            try
            {
                if (DBHelper.ExecuteNonQuerySql("INSERT INTO PE_AdZone SELECT @MaxId AS ZoneID, '复制'+Convert(Varchar, @MaxId)+ZoneName AS ZoneName, @ZoneJsName AS ZoneJSName, ZoneIntro, ZoneType, DefaultSetting, ZoneSetting, ZoneWidth, ZoneHeight, Active, ShowType, UpdateTime FROM PE_AdZone WHERE ZoneID = @ZoneId", cmdParams) > 0)
                {
                    flag = true;
                }
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        public bool Delete(string id)
        {
            string strSql = "DELETE FROM PE_AdZone WHERE ZoneId IN(" + DBHelper.ToValidId(id) + ")";
            DBHelper.ExecuteSql("DELETE FROM PE_Zone_Advertisement WHERE ZoneId IN(" + DBHelper.ToValidId(id) + ")");
            return DBHelper.ExecuteSql(strSql);
        }

        public bool ExportData(string zoneId, string importDatabase, bool chkFormatConn)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Ad_ADZone_GetExportList");
            OleDbConnection connection = new OleDbConnection("Provider = Microsoft.Jet.OleDb.4.0;Data Source = " + importDatabase);
            if (chkFormatConn)
            {
                OleDbCommand command2 = new OleDbCommand("DELETE FROM [PE_AdZone]", connection);
                command2.CommandType = CommandType.Text;
                connection.Open();
                try
                {
                    command2.ExecuteNonQuery();
                }
                catch
                {
                    return false;
                }
                finally
                {
                    command2.Cancel();
                    connection.Close();
                }
            }
            database.AddInParameter(storedProcCommand, "@ZoneID", DbType.String, zoneId);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                connection.Open();
                object obj2 = new OleDbCommand("SELECT MAX(ZoneId) FROM PE_AdZone", connection).ExecuteScalar();
                connection.Close();
                int num = Convert.IsDBNull(obj2) ? 1 : (Convert.ToInt32(obj2) + 1);
                bool flag = false;
                while (reader.Read())
                {
                    if (!flag)
                    {
                        flag = true;
                    }
                    else
                    {
                        num++;
                    }
                    OleDbCommand command4 = new OleDbCommand("INSERT INTO PE_AdZone (ZoneID, ZoneName, ZoneJSName, ZoneIntro, ZoneType, DefaultSetting, ZoneSetting, ZoneWidth, ZoneHeight, ShowType, Active) VALUES (@ZoneID, @ZoneName, @ZoneJSName, @ZoneIntro, @ZoneType, @DefaultSetting, @ZoneSetting, @ZoneWidth, @ZoneHeight, @ShowType, @Active)", connection);
                    try
                    {
                        OleDbParameter parameter = new OleDbParameter("@ZoneId", num);
                        OleDbParameter parameter2 = new OleDbParameter("@ZoneName", reader.GetString("ZoneName"));
                        OleDbParameter parameter3 = new OleDbParameter("@ZoneJSName", reader.GetString("ZoneJSName"));
                        OleDbParameter parameter4 = new OleDbParameter("@ZoneIntro", reader.GetString("ZoneIntro"));
                        OleDbParameter parameter5 = new OleDbParameter("@ZoneType", reader.GetInt32("ZoneType"));
                        OleDbParameter parameter6 = new OleDbParameter("@DefaultSetting", reader.GetBoolean("DefaultSetting"));
                        OleDbParameter parameter7 = new OleDbParameter("@ZoneSetting", reader.GetString("ZoneSetting"));
                        OleDbParameter parameter8 = new OleDbParameter("@ZoneWidth", reader.GetInt32("ZoneWidth"));
                        OleDbParameter parameter9 = new OleDbParameter("@ZoneHeight", reader.GetInt32("ZoneHeight"));
                        OleDbParameter parameter10 = new OleDbParameter("@ShowType", reader.GetInt32("ShowType"));
                        OleDbParameter parameter11 = new OleDbParameter("@Active", reader.GetBoolean("Active"));
                        command4.Parameters.Add(parameter);
                        command4.Parameters.Add(parameter2);
                        command4.Parameters.Add(parameter3);
                        command4.Parameters.Add(parameter4);
                        command4.Parameters.Add(parameter5);
                        command4.Parameters.Add(parameter6);
                        command4.Parameters.Add(parameter7);
                        command4.Parameters.Add(parameter8);
                        command4.Parameters.Add(parameter9);
                        command4.Parameters.Add(parameter10);
                        command4.Parameters.Add(parameter11);
                        connection.Open();
                        command4.ExecuteNonQuery();
                        continue;
                    }
                    catch
                    {
                        return false;
                    }
                    finally
                    {
                        command4.Clone();
                        connection.Close();
                    }
                }
                return true;
            }
        }

        public ADZoneInfo GetADZoneById(int id)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ZoneId", DbType.Int32, id);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_AdZone WHERE ZoneId = @ZoneId", cmdParams))
            {
                if (reader.Read())
                {
                    return GetAdZoneInfoFromrdr(reader);
                }
                return new ADZoneInfo(true);
            }
        }

        private static ADZoneInfo GetAdZoneInfoFromrdr(NullableDataReader rdr)
        {
            ADZoneInfo info = new ADZoneInfo();
            info.ZoneId = rdr.GetInt32("ZoneId");
            info.ZoneName = rdr.GetString("ZoneName");
            info.ZoneJSName = rdr.GetString("ZoneJSName");
            info.ZoneIntro = rdr.GetString("ZoneIntro");
            info.ZoneType = (ADZoneType) rdr.GetInt32("ZoneType");
            info.DefaultSetting = rdr.GetBoolean("DefaultSetting");
            info.Setting = rdr.GetString("ZoneSetting");
            info.ZoneWidth = rdr.GetInt32("ZoneWidth");
            info.ZoneHeight = rdr.GetInt32("ZoneHeight");
            info.Active = rdr.GetBoolean("Active");
            info.ShowType = rdr.GetInt32("ShowType");
            info.UpdateTime = rdr.GetDateTime("UpdateTime");
            return info;
        }

        public IList<ADZoneInfo> GetAllADZone(int startRowIndexId, int maxNumberRows, ADZoneSearchType listType, string keywords)
        {
            Database database = DatabaseFactory.CreateDatabase();
            IList<ADZoneInfo> list = new List<ADZoneInfo>();
            string storedProcedureName = "PR_Common_GetList";
            string str2 = BuildQuery(listType, keywords);
            DbCommand storedProcCommand = database.GetStoredProcCommand(storedProcedureName);
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "ZoneID");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "*");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, str2);
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_AdZone");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(GetAdZoneInfoFromrdr(reader));
                }
            }
            this.m_NumADZones = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public IList<ADZoneInfo> GetImportList(string importDatabase)
        {
            OleDbConnection connection = new OleDbConnection("Provider = Microsoft.Jet.OleDb.4.0;Data Source = " + importDatabase);
            IList<ADZoneInfo> list = new List<ADZoneInfo>();
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(" SELECT ZoneID, ZoneName, ZoneJSName, ZoneIntro, ZoneType, DefaultSetting, ZoneSetting, ZoneWidth, ZoneHeight, ShowType, Active, UpdateTime FROM PE_AdZone ORDER BY ZoneId ASC", connection);
                NullableDataReader rdr = new NullableDataReader(command.ExecuteReader(CommandBehavior.CloseConnection));
                while (rdr.Read())
                {
                    list.Add(GetAdZoneInfoFromrdr(rdr));
                }
            }
            catch
            {
            }
            finally
            {
                connection.Close();
            }
            return list;
        }

        private static int GetMaxADZoneId()
        {
            int maxId = DBHelper.GetMaxId("PE_AdZone", "ZoneID");
            if (maxId < 1)
            {
                return 1;
            }
            return (maxId + 1);
        }

        public string GetNewJSName()
        {
            return (DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString().PadLeft(2, '0') + "/" + GetMaxADZoneId().ToString() + ".js");
        }

        private static Parameters GetParameters(ADZoneInfo adZoneInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@ZoneName", DbType.String, adZoneInfo.ZoneName);
            parameters.AddInParameter("@ZoneJSName", DbType.String, adZoneInfo.ZoneJSName);
            parameters.AddInParameter("@ZoneIntro", DbType.String, adZoneInfo.ZoneIntro);
            parameters.AddInParameter("@ZoneType", DbType.Int32, adZoneInfo.ZoneType);
            parameters.AddInParameter("@DefaultSetting", DbType.Boolean, adZoneInfo.DefaultSetting);
            parameters.AddInParameter("@ZoneSetting", DbType.String, adZoneInfo.Setting);
            parameters.AddInParameter("@ZoneWidth", DbType.Int32, adZoneInfo.ZoneWidth);
            parameters.AddInParameter("@ZoneHeight", DbType.Int32, adZoneInfo.ZoneHeight);
            parameters.AddInParameter("@Active", DbType.Boolean, adZoneInfo.Active);
            parameters.AddInParameter("@ShowType", DbType.Int32, adZoneInfo.ShowType);
            parameters.AddInParameter("@UpdateTime", DbType.DateTime, adZoneInfo.UpdateTime);
            return parameters;
        }

        private static DbCommand GetProcdbComm(Database db, string proName, ADZoneInfo adZoneInfo)
        {
            DbCommand storedProcCommand = db.GetStoredProcCommand(proName);
            db.AddInParameter(storedProcCommand, "@ZoneId", DbType.Int32, adZoneInfo.ZoneId);
            db.AddInParameter(storedProcCommand, "@ZoneName", DbType.String, adZoneInfo.ZoneName);
            db.AddInParameter(storedProcCommand, "@ZoneJSName", DbType.String, adZoneInfo.ZoneJSName);
            db.AddInParameter(storedProcCommand, "@ZoneIntro", DbType.String, adZoneInfo.ZoneIntro);
            db.AddInParameter(storedProcCommand, "@ZoneType", DbType.Int32, adZoneInfo.ZoneType);
            db.AddInParameter(storedProcCommand, "@DefaultSetting", DbType.Boolean, adZoneInfo.DefaultSetting);
            db.AddInParameter(storedProcCommand, "@ZoneSetting", DbType.String, adZoneInfo.Setting);
            db.AddInParameter(storedProcCommand, "@ZoneWidth", DbType.Int32, adZoneInfo.ZoneWidth);
            db.AddInParameter(storedProcCommand, "@ZoneHeight", DbType.Int32, adZoneInfo.ZoneHeight);
            db.AddInParameter(storedProcCommand, "@Active", DbType.Boolean, adZoneInfo.Active);
            db.AddInParameter(storedProcCommand, "@ShowType", DbType.Int32, adZoneInfo.ShowType);
            db.AddInParameter(storedProcCommand, "@UpdateTime", DbType.DateTime, adZoneInfo.UpdateTime);
            return storedProcCommand;
        }

        public int GetTotalOfADZone()
        {
            return this.m_NumADZones;
        }

        public bool ImportData(string zoneId, string importDatabase)
        {
            bool flag;
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder builder = new StringBuilder("");
            builder.Append(" SELECT ZoneID, ZoneName, ZoneJSName, ZoneIntro, ZoneType, DefaultSetting, ZoneSetting, ZoneWidth, ZoneHeight, ShowType, Active, UpdateTime FROM PE_AdZone ");
            OleDbConnection connection = new OleDbConnection("Provider = Microsoft.Jet.OleDb.4.0;Data Source = " + importDatabase);
            try
            {
                connection.Open();
                builder.Append("Where ZoneID IN (" + zoneId + ") ORDER BY ZoneID");
                OleDbCommand command = new OleDbCommand(builder.ToString(), connection);
                NullableDataReader reader = new NullableDataReader(command.ExecuteReader(CommandBehavior.CloseConnection));
                while (reader.Read())
                {
                    ADZoneInfo adZoneInfo = new ADZoneInfo();
                    adZoneInfo.ZoneId = GetMaxADZoneId();
                    adZoneInfo.ZoneName = reader.GetString("ZoneName");
                    adZoneInfo.ZoneJSName = reader.GetString("ZoneJSName");
                    adZoneInfo.ZoneIntro = reader.GetString("ZoneIntro");
                    adZoneInfo.ZoneType = (ADZoneType) reader.GetInt32("ZoneType");
                    adZoneInfo.DefaultSetting = reader.GetBoolean("DefaultSetting");
                    adZoneInfo.Setting = reader.GetString("ZoneSetting");
                    adZoneInfo.ZoneWidth = reader.GetInt32("ZoneWidth");
                    adZoneInfo.ZoneHeight = reader.GetInt32("ZoneHeight");
                    adZoneInfo.ShowType = reader.GetInt32("ShowType");
                    adZoneInfo.Active = reader.GetBoolean("Active");
                    adZoneInfo.UpdateTime = reader.GetDateTime("UpdateTime");
                    DbCommand command2 = GetProcdbComm(db, "PR_Ad_ADZone_ADD", adZoneInfo);
                    try
                    {
                        db.ExecuteNonQuery(command2);
                        continue;
                    }
                    catch
                    {
                        reader.Close();
                        return false;
                    }
                }
                flag = true;
            }
            catch
            {
                flag = false;
            }
            finally
            {
                connection.Close();
            }
            return flag;
        }

        public bool PauseADZone(string id)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@strZoneId", DbType.String, id);
            return DBHelper.ExecuteProc("PR_AD_ADZone_Pause", cmdParams);
        }

        public bool Update(ADZoneInfo adZoneInfo)
        {
            Parameters cmdParams = GetParameters(adZoneInfo);
            cmdParams.AddInParameter("@ZoneId", DbType.Int32, adZoneInfo.ZoneId);
            return DBHelper.ExecuteProc("PR_AD_ADZone_UPDATE", cmdParams);
        }
    }
}

