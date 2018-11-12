namespace EasyOne.SqlServerDal.Accessories
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.IDal.Accessories;
    using EasyOne.Model.Accessories;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    public class Source : ISource
    {
        private int m_TotalOfSource;

        public bool Add(SourceInfo sourceInfo)
        {
            Parameters cmdParams = GetParameters(sourceInfo);
            return DBHelper.ExecuteProc("PR_Accessories_Source_Insert", cmdParams);
        }

        public bool Delete(string strId)
        {
            return DBHelper.ExecuteSql("DELETE FROM PE_Source WHERE ID IN (" + DBHelper.ToValidId(strId) + ")");
        }

        public bool Exists(string sname)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@Name", DbType.String, sname);
            return DBHelper.ExistsSql("SELECT COUNT(ID) FROM PE_Source WHERE Name = @Name", cmdParams);
        }

        public bool ExistsPassedSource(string sourceName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@Name", DbType.String, sourceName);
            return DBHelper.ExistsSql("SELECT COUNT(ID) FROM PE_Source WHERE Name = @Name AND Passed = 1", cmdParams);
        }

        private static Parameters GetParameters(SourceInfo sourceInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@Type", DbType.String, sourceInfo.Type);
            parameters.AddInParameter("@Name", DbType.String, sourceInfo.Name);
            parameters.AddInParameter("@Passed", DbType.Boolean, sourceInfo.Passed);
            parameters.AddInParameter("@onTop", DbType.Boolean, sourceInfo.OnTop);
            parameters.AddInParameter("@IsElite", DbType.Boolean, sourceInfo.Elite);
            parameters.AddInParameter("@Hits", DbType.Int32, sourceInfo.Hits);
            parameters.AddInParameter("@LastUseTime", DbType.DateTime, sourceInfo.LastUseTime);
            parameters.AddInParameter("@Photo", DbType.String, sourceInfo.Photo);
            parameters.AddInParameter("@Intro", DbType.String, sourceInfo.Intro);
            parameters.AddInParameter("@Address", DbType.String, sourceInfo.Address);
            parameters.AddInParameter("@Tel", DbType.String, sourceInfo.Tel);
            parameters.AddInParameter("@Fax", DbType.String, sourceInfo.Fax);
            parameters.AddInParameter("@Mail", DbType.String, sourceInfo.Mail);
            parameters.AddInParameter("@Email", DbType.String, sourceInfo.Email);
            parameters.AddInParameter("@ZipCode", DbType.Int32, sourceInfo.ZipCode);
            parameters.AddInParameter("@HomePage", DbType.String, sourceInfo.HomePage);
            parameters.AddInParameter("@Im", DbType.String, sourceInfo.Imeeting);
            parameters.AddInParameter("@ContacterName", DbType.String, sourceInfo.ContacterName);
            return parameters;
        }

        public SourceInfo GetSourceInfoById(int id)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ID", DbType.Int32, id);
            using (NullableDataReader reader = DBHelper.ExecuteReaderProc("PR_Accessories_Source_GetSourceInfoByID", cmdParams))
            {
                if (reader.Read())
                {
                    return SourceFromrdr(reader);
                }
                return new SourceInfo(true);
            }
        }

        public IList<SourceInfo> GetSourceList(int startRowIndexId, int maxNumberRows, int searchType, string keyword, string sourceType, bool isShowDisable)
        {
            string str;
            switch (searchType)
            {
                case 0:
                    str = "Name LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'";
                    break;

                case 1:
                    str = "Address LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'";
                    break;

                case 2:
                    str = "Tel LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'";
                    break;

                case 3:
                    str = "Intro LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'";
                    break;

                case 4:
                    str = "Contacter LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'";
                    break;

                case 5:
                    str = "Type = '" + DBHelper.FilterBadChar(sourceType) + "'";
                    break;

                default:
                    str = string.Empty;
                    break;
            }
            if (isShowDisable)
            {
                str = str + " AND Passed = 1";
            }
            IList<SourceInfo> list = new List<SourceInfo>();
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "ID");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "*");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_Source");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, str);
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(SourceFromrdr(reader));
                }
            }
            this.m_TotalOfSource = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public IList<SourceInfo> GetSourceListByType(string type)
        {
            IList<SourceInfo> list = new List<SourceInfo>();
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@Type", DbType.String, type);
            using (NullableDataReader reader = DBHelper.ExecuteReaderProc("PR_Accessories_Source_GetSourceInfoByType", cmdParams))
            {
                while (reader.Read())
                {
                    list.Add(SourceFromrdr(reader));
                }
            }
            return list;
        }

        public IList<SourceInfo> GetSourceTypeList()
        {
            string strSql = "SELECT Type FROM PE_Source GROUP BY Type";
            IList<SourceInfo> list = new List<SourceInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql))
            {
                while (reader.Read())
                {
                    if (!string.IsNullOrEmpty(reader.GetString(0)))
                    {
                        SourceInfo item = new SourceInfo(reader.GetString(0));
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public int GetTotalOfSource()
        {
            return this.m_TotalOfSource;
        }

        private static SourceInfo SourceFromrdr(NullableDataReader rdr)
        {
            SourceInfo info = new SourceInfo();
            info.Id = rdr.GetInt32("ID");
            info.Type = rdr.GetString("Type");
            info.Name = rdr.GetString("Name");
            info.Passed = rdr.GetBoolean("Passed");
            info.OnTop = rdr.GetBoolean("onTop");
            info.Elite = rdr.GetBoolean("IsElite");
            info.Hits = rdr.GetInt32("Hits");
            info.LastUseTime = rdr.GetDateTime("LastUseTime");
            info.Photo = rdr.GetString("Photo");
            info.Intro = rdr.GetString("Intro");
            info.Address = rdr.GetString("Address");
            info.Tel = rdr.GetString("Tel");
            info.Fax = rdr.GetString("Fax");
            info.Mail = rdr.GetString("Mail");
            info.Email = rdr.GetString("Email");
            info.ZipCode = rdr.GetInt32("ZipCode");
            info.HomePage = rdr.GetString("HomePage");
            info.Imeeting = rdr.GetString("Im");
            info.ContacterName = rdr.GetString("Contacter");
            return info;
        }

        public bool Update(SourceInfo sourceInfo)
        {
            Parameters cmdParams = GetParameters(sourceInfo);
            cmdParams.AddInParameter("@ID", DbType.Int32, sourceInfo.Id);
            return DBHelper.ExecuteProc("PR_Accessories_Source_Update", cmdParams);
        }
    }
}

