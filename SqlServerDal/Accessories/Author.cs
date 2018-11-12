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

    public class Author : IAuthor
    {
        private int m_TotalOfAuthor;

        public bool Add(AuthorInfo authorInfo)
        {
            Parameters cmdParams = GetParameters(authorInfo);
            return DBHelper.ExecuteProc("PR_Accessories_Author_Add", cmdParams);
        }

        private static AuthorInfo AuthorFromrdr(NullableDataReader rdr)
        {
            AuthorInfo info = new AuthorInfo();
            info.Id = rdr.GetInt32("ID");
            info.UserId = rdr.GetInt32("UserID");
            info.Type = rdr.GetString("Type");
            info.Name = rdr.GetString("Name");
            info.Passed = rdr.GetBoolean("Passed");
            info.OnTop = rdr.GetBoolean("onTop");
            info.Elite = rdr.GetBoolean("IsElite");
            info.Hits = rdr.GetInt32("Hits");
            info.LastUseTime = rdr.GetDateTime("LastUseTime");
            info.TemplateId = rdr.GetInt32("TemplateID");
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
            info.Sex = rdr.GetInt16("Sex");
            info.BirthDay = rdr.GetDateTime("BirthDay");
            info.Company = rdr.GetString("Company");
            info.Department = rdr.GetString("Department");
            return info;
        }

        public bool Delete(string strId)
        {
            return DBHelper.ExecuteSql("DELETE FROM PE_Author WHERE ID IN (" + DBHelper.ToValidId(strId) + ")");
        }

        public bool Exists(string authorname)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@Name", DbType.String, authorname);
            return DBHelper.ExistsSql("SELECT COUNT(*) FROM PE_Author WHERE Name = @Name", cmdParams);
        }

        public bool ExistsPassedAuthor(string authorName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@Name", DbType.String, authorName);
            return DBHelper.ExistsSql("SELECT COUNT(*) FROM PE_Author WHERE Name = @Name AND Passed = 1", cmdParams);
        }

        public AuthorInfo GetAuthorInfoById(int id)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ID", DbType.Int32, id);
            using (NullableDataReader reader = DBHelper.ExecuteReaderProc("PR_Accessories_Author_GetAuthorInfoByID", cmdParams))
            {
                if (reader.Read())
                {
                    return AuthorFromrdr(reader);
                }
                return new AuthorInfo(true);
            }
        }

        public AuthorInfo GetAuthorInfoByUserId(int userId)
        {
            string strSql = "SELECT * FROM PE_Author WHERE UserId = @UserId";
            Parameters cmdParams = new Parameters("@UserId", DbType.Int32, userId);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, cmdParams))
            {
                if (reader.Read())
                {
                    return AuthorFromrdr(reader);
                }
                return new AuthorInfo(true);
            }
        }

        public IList<AuthorInfo> GetAuthorList(int startRowIndexId, int maxNumberRows, int listType, string searchType, string keyword, bool isDisable)
        {
            string str;
            IList<AuthorInfo> list = new List<AuthorInfo>();
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.Replace("'", "''");
            }
            if (!string.IsNullOrEmpty(searchType))
            {
                searchType = searchType.Replace("'", "''");
            }
            switch (listType)
            {
                case 0:
                    str = "[Name] LIKE '%" + keyword + "%'";
                    break;

                case 1:
                    str = "[Address] LIKE '%" + keyword + "%'";
                    break;

                case 2:
                    str = "[Tel] LIKE '%" + keyword + "%'";
                    break;

                case 3:
                    str = "[Intro] LIKE '%" + keyword + "%'";
                    break;

                case 4:
                    str = "Type = '" + searchType + "'";
                    break;

                case 5:
                    str = "Type = '" + searchType + "' AND [Name] LIKE '%" + keyword + "%'";
                    break;

                default:
                    str = string.Empty;
                    break;
            }
            if (isDisable)
            {
                str = str + " AND Passed = 1";
            }
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String);
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String);
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String);
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String);
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String);
            database.SetParameterValue(storedProcCommand, "@StartRows", startRowIndexId);
            database.SetParameterValue(storedProcCommand, "@PageSize", maxNumberRows);
            database.SetParameterValue(storedProcCommand, "@SortColumn", "ID");
            database.SetParameterValue(storedProcCommand, "@StrColumn", "*");
            database.SetParameterValue(storedProcCommand, "@Sorts", "DESC");
            database.SetParameterValue(storedProcCommand, "@TableName", "PE_Author");
            database.SetParameterValue(storedProcCommand, "@Filter", str);
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(AuthorFromrdr(reader));
                }
            }
            this.m_TotalOfAuthor = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        private static Parameters GetParameters(AuthorInfo authorInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@UserId", DbType.Int32, authorInfo.UserId);
            parameters.AddInParameter("@Name", DbType.String, authorInfo.Name);
            parameters.AddInParameter("@Type", DbType.String, authorInfo.Type);
            parameters.AddInParameter("@Passed", DbType.Boolean, authorInfo.Passed);
            parameters.AddInParameter("@onTop", DbType.Boolean, authorInfo.OnTop);
            parameters.AddInParameter("@IsElite", DbType.Boolean, authorInfo.Elite);
            parameters.AddInParameter("@Hits", DbType.Int32, authorInfo.Hits);
            parameters.AddInParameter("@LastUseTime", DbType.DateTime, authorInfo.LastUseTime);
            parameters.AddInParameter("@TemplateID", DbType.Int32, authorInfo.TemplateId);
            parameters.AddInParameter("@Photo", DbType.String, authorInfo.Photo);
            parameters.AddInParameter("@Intro", DbType.String, authorInfo.Intro);
            parameters.AddInParameter("@Address", DbType.String, authorInfo.Address);
            parameters.AddInParameter("@Tel", DbType.String, authorInfo.Tel);
            parameters.AddInParameter("@Fax", DbType.String, authorInfo.Fax);
            parameters.AddInParameter("@Mail", DbType.String, authorInfo.Mail);
            parameters.AddInParameter("@Email", DbType.String, authorInfo.Email);
            parameters.AddInParameter("@ZipCode", DbType.Int32, authorInfo.ZipCode);
            parameters.AddInParameter("@HomePage", DbType.String, authorInfo.HomePage);
            parameters.AddInParameter("@Im", DbType.String, authorInfo.Imeeting);
            parameters.AddInParameter("@Sex", DbType.Int16, authorInfo.Sex);
            parameters.AddInParameter("@BirthDay", DbType.DateTime, authorInfo.BirthDay);
            parameters.AddInParameter("@Company", DbType.String, authorInfo.Company);
            parameters.AddInParameter("@Department", DbType.String, authorInfo.Department);
            return parameters;
        }

        public int GetTotalOfAuthor()
        {
            return this.m_TotalOfAuthor;
        }

        public bool Update(AuthorInfo authorInfo)
        {
            Parameters cmdParams = GetParameters(authorInfo);
            cmdParams.AddInParameter("@ID", DbType.Int32, authorInfo.Id);
            return DBHelper.ExecuteProc("PR_Accessories_Author_Update", cmdParams);
        }
    }
}

