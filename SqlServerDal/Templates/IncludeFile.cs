namespace EasyOne.SqlServerDal.Templates
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.Enumerations;
    using EasyOne.IDal.Templates;
    using EasyOne.Model.Templates;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    public class IncludeFile : IIncludeFile
    {
        private int m_TotalOfIncludeFileInfo;

        public bool Add(IncludeFileInfo includeFileInfo)
        {
            Parameters parms = new Parameters();
            parms.AddInParameter("@ID", DbType.Int32, DBHelper.GetMaxId("PE_IncludeFile", "ID") + 1);
            InitParameters(includeFileInfo, parms);
            return DBHelper.ExecuteSql("INSERT INTO PE_IncludeFile (ID, Name, Description, IncludeType, AssociateType, FileName, Template) VALUES (@ID, @Name, @Description, @IncludeType, @AssociateType, @FileName, @Template)", parms);
        }

        public bool Delete(int id)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ID", DbType.Int32, id);
            return DBHelper.ExecuteSql("DELETE FROM PE_IncludeFile WHERE ID = @ID", cmdParams);
        }

        public bool ExistsFileName(string fileName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@FileName", DbType.String, fileName);
            return DBHelper.ExistsSql("SELECT * FROM PE_IncludeFile WHERE FileName = @FileName", cmdParams);
        }

        public bool ExistsName(string name)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@Name", DbType.String, name);
            return DBHelper.ExistsSql("SELECT * FROM PE_IncludeFile WHERE Name = @Name", cmdParams);
        }

        public IncludeFileInfo GetIncludeFileInfoById(int id)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ID", DbType.Int32, id);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_IncludeFile WHERE ID = @ID", cmdParams))
            {
                if (reader.Read())
                {
                    return GetIncludeFileInfoFromDataReader(reader);
                }
                return new IncludeFileInfo(true);
            }
        }

        private static IncludeFileInfo GetIncludeFileInfoFromDataReader(NullableDataReader dr)
        {
            IncludeFileInfo info = new IncludeFileInfo();
            info.Id = dr.GetInt32("ID");
            info.Name = dr.GetString("Name");
            info.Description = dr.GetString("Description");
            info.IncludeType = (IncludeType) dr.GetInt32("IncludeType");
            info.AssociateType = (AssociateType) dr.GetInt32("AssociateType");
            info.FileName = dr.GetString("FileName");
            info.Template = dr.GetString("Template");
            return info;
        }

        public IList<IncludeFileInfo> GetIncludeFileInfoList()
        {
            List<IncludeFileInfo> list = new List<IncludeFileInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_IncludeFile ORDER BY ID DESC"))
            {
                while (reader.Read())
                {
                    list.Add(GetIncludeFileInfoFromDataReader(reader));
                }
            }
            return list;
        }

        public IList<IncludeFileInfo> GetIncludeFileInfoList(int startRowIndexId, int maxNumberRows)
        {
            Database database = DatabaseFactory.CreateDatabase();
            List<IncludeFileInfo> list = new List<IncludeFileInfo>();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "ID");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "*");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_IncludeFile");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, "");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(GetIncludeFileInfoFromDataReader(reader));
                }
            }
            this.m_TotalOfIncludeFileInfo = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public IList<IncludeFileInfo> GetIncludeFileListByAssociateType(AssociateType associateType)
        {
            List<IncludeFileInfo> list = new List<IncludeFileInfo>();
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@AssociateType", DbType.Int32, associateType);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_IncludeFile WHERE AssociateType = @AssociateType ORDER BY ID DESC", cmdParams))
            {
                while (reader.Read())
                {
                    list.Add(GetIncludeFileInfoFromDataReader(reader));
                }
            }
            return list;
        }

        public int GetTotalOfIncludeFileInfo()
        {
            return this.m_TotalOfIncludeFileInfo;
        }

        private static void InitParameters(IncludeFileInfo includeFileInfo, Parameters parms)
        {
            parms.AddInParameter("@Name", DbType.String, includeFileInfo.Name);
            parms.AddInParameter("@Description", DbType.String, includeFileInfo.Description);
            parms.AddInParameter("@IncludeType", DbType.Int32, includeFileInfo.IncludeType);
            parms.AddInParameter("@FileName", DbType.String, includeFileInfo.FileName);
            parms.AddInParameter("@Template", DbType.String, includeFileInfo.Template);
            parms.AddInParameter("@AssociateType", DbType.Int32, includeFileInfo.AssociateType);
        }

        public bool Update(IncludeFileInfo includeFileInfo)
        {
            Parameters parms = new Parameters();
            parms.AddInParameter("@ID", DbType.Int32, includeFileInfo.Id);
            InitParameters(includeFileInfo, parms);
            return DBHelper.ExecuteSql("UPDATE PE_IncludeFile SET Name = @Name, Description = @Description, IncludeType = @IncludeType, AssociateType = @AssociateType, FileName = @FileName, Template = @Template WHERE ID = @ID", parms);
        }
    }
}

