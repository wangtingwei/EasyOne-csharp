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
    using System.Text;

    public class Files : IFiles
    {
        private int m_NumFiles;

        public bool Add(FileInfo fileInfo)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ID", DbType.Int32, fileInfo.Id);
            cmdParams.AddInParameter("@Name", DbType.String, fileInfo.Name);
            cmdParams.AddInParameter("@Size", DbType.Int32, fileInfo.Size);
            cmdParams.AddInParameter("@Quote", DbType.Int32, fileInfo.Quote);
            cmdParams.AddInParameter("@Path", DbType.String, fileInfo.Path);
            cmdParams.AddInParameter("@IsThumb", DbType.Boolean, fileInfo.IsThumb);
            return DBHelper.ExecuteProc("PR_Accessories_Files_Add", cmdParams);
        }

        public int Count()
        {
            return this.m_NumFiles;
        }

        public bool Delete(int id)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ID", DbType.Int32, id);
            return DBHelper.ExecuteSql("DELETE FROM PE_Files WHERE ID = @ID", cmdParams);
        }

        private static FileInfo FileInfoFromrdr(NullableDataReader rdr)
        {
            FileInfo info = new FileInfo();
            info.Id = rdr.GetInt32("ID");
            info.Name = rdr.GetString("Name");
            info.Size = rdr.GetInt32("Size");
            info.Quote = rdr.GetInt32("Quote");
            info.Path = rdr.GetString("Path");
            info.IsThumb = rdr.GetBoolean("IsThumb");
            return info;
        }

        public IList<FileInfo> GetList()
        {
            List<FileInfo> list = new List<FileInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_Files"))
            {
                while (reader.Read())
                {
                    list.Add(FileInfoFromrdr(reader));
                }
            }
            return list;
        }

        public IList<FileInfo> GetList(int startRowIndexId, int maxNumberRows)
        {
            return this.GetSearchList(startRowIndexId, maxNumberRows, "");
        }

        public IList<FileInfo> GetList(int startRowIndexId, int maxNumberRows, string path)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" Path LIKE '");
            builder.Append(DBHelper.FilterBadChar(path));
            builder.Append("%'");
            return this.GetSearchList(startRowIndexId, maxNumberRows, builder.ToString());
        }

        public int GetMaxId()
        {
            return DBHelper.GetMaxId("PE_Files", "ID");
        }

        public FileInfo GetModel(int id)
        {
            FileInfo info = new FileInfo();
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ID", DbType.Int32, id);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_Files WHERE ID = @ID", cmdParams))
            {
                if (reader.Read())
                {
                    info = FileInfoFromrdr(reader);
                }
            }
            return info;
        }

        public FileInfo GetModel(string path)
        {
            FileInfo info = new FileInfo();
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@Path", DbType.String, path);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_Files WHERE Path = @Path", cmdParams))
            {
                if (reader.Read())
                {
                    info = FileInfoFromrdr(reader);
                }
            }
            return info;
        }

        private IList<FileInfo> GetSearchList(int startRowIndexId, int maxNumberRows, string filter)
        {
            List<FileInfo> list = new List<FileInfo>();
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "ID");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "*");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_Files");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, filter);
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(FileInfoFromrdr(reader));
                }
            }
            this.m_NumFiles = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }
    }
}

