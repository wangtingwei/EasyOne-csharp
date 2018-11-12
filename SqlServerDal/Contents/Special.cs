namespace EasyOne.SqlServerDal.Contents
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.Common;
    using EasyOne.Enumerations;
    using EasyOne.IDal.Contents;
    using EasyOne.Model.Contents;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Text;

    public class Special : ISpecial
    {
        private int m_TotalOfSpecial;

        public bool AddSpecial(SpecialInfo specialInfo)
        {
            Parameters parms = new Parameters();
            specialInfo.SpecialId = this.GetMaxSpecialId() + 1;
            specialInfo.OrderId = GetMaxSpecialOrderId(specialInfo.SpecialCategoryId) + 1;
            GetSpecialParameters(specialInfo, parms);
            string strSql = "INSERT INTO PE_Specials(SpecialID, SpecialName, SpecialCategoryID, SpecialDir, SpecialIdentifier, SpecialPic, SpecialTips, SpecialTemplatePath, SearchTemplatePath, IsElite, OrderId, OpenType, Description, IsCreateListPage, ListPageSavePathType, ListPagePostfix, Custom_Content, NeedCreateHtml) VALUES (@SpecialID, @SpecialName, @SpecialCategoryId, @SpecialDir, @SpecialIdentifier, @SpecialPic, @SpecialTips, @SpecialTemplatePath, @SearchTemplatePath, @IsElite, @OrderId, @OpenType, @Description, @IsCreateListPage, @ListPageSavePathType, @ListPagePostfix, @CustomContent, @NeedCreateHtml)";
            return DBHelper.ExecuteSql(strSql, parms);
        }

        public bool AddSpecialCategory(SpecialCategoryInfo specialCategoryInfo)
        {
            Parameters parms = new Parameters();
            specialCategoryInfo.SpecialCategoryId = GetMaxSpecialCategoryId() + 1;
            specialCategoryInfo.OrderId = GetMaxSpecialCategoryOrderId() + 1;
            GetSpecialCategoryParameters(specialCategoryInfo, parms);
            string strSql = "INSERT INTO PE_SpecialCategory(SpecialCategoryID, SpecialCategoryName, SpecialCategoryDir, SpecialTemplatePath, SearchTemplatePath, OrderId, OpenType, PagePostfix, IsCreateHtml, Description, NeedCreateHtml) VALUES (@SpecialCategoryID, @SpecialCategoryName, @SpecialCategoryDir, @SpecialTemplatePath, @SearchTemplatePath, @OrderId, @OpenType, @PagePostfix, @IsCreateHtml, @Description, @NeedCreateHtml)";
            return DBHelper.ExecuteSql(strSql, parms);
        }

        public bool AddToSpecialInfos(int specialId, int generalId)
        {
            int num = GetMaxSpecialInfoId() + 1;
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@SpecialInfoId", DbType.Int32, num);
            cmdParams.AddInParameter("@SpecialId", DbType.Int32, specialId);
            cmdParams.AddInParameter("@GeneralId", DbType.Int32, generalId);
            string strSql = "INSERT INTO PE_SpecialInfos(SpecialInfoID, SpecialID, GeneralId) VALUES (@SpecialInfoId, @SpecialId, @GeneralId)";
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool DeleteSpecial(int specialId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@SpecialId", DbType.Int32, specialId);
            string strSql = "DELETE FROM PE_Specials WHERE SpecialID = @SpecialId";
            try
            {
                DBHelper.ExecuteNonQuerySql(strSql, cmdParams);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteSpecialCategoryById(int specialCategoryId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@SpecialCategoryId", DbType.Int32, specialCategoryId);
            string strSql = "DELETE FROM PE_SpecialCategory WHERE SpecialCategoryID = @SpecialCategoryId";
            try
            {
                DBHelper.ExecuteNonQuerySql(strSql, cmdParams);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteSpecialCategoryIdInSpecials(int specialCategoryId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@SpecialCategoryId", DbType.Int32, specialCategoryId);
            string strSql = "DELETE FROM PE_Specials WHERE SpecialCategoryID = @SpecialCategoryId";
            try
            {
                DBHelper.ExecuteNonQuerySql(strSql, cmdParams);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteSpecialIdInSpecialInfos(string specialIds)
        {
            string strSql = "DELETE FROM PE_SpecialInfos WHERE SpecialID IN(" + DBHelper.ToValidId(specialIds) + ")";
            try
            {
                DBHelper.ExecuteNonQuerySql(strSql);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteSpecialInfoById(string specialInfoIds)
        {
            string strSql = "DELETE FROM PE_SpecialInfos WHERE SpecialInfoID IN(" + DBHelper.ToValidId(specialInfoIds) + ")";
            try
            {
                DBHelper.ExecuteNonQuerySql(strSql);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteSpecialInfoById(string specialInfoIds, int specialId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@SpecialId", DbType.Int32, specialId);
            string strSql = "DELETE FROM PE_SpecialInfos WHERE SpecialID!=@SpecialId AND SpecialInfoID IN(" + DBHelper.ToValidId(specialInfoIds) + ")";
            try
            {
                DBHelper.ExecuteNonQuerySql(strSql, cmdParams);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteSpecialInfoBySpecialId(int specialId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@SpecialId", DbType.Int32, specialId);
            string strSql = "DELETE FROM PE_SpecialInfos WHERE SpecialID = @SpecialId";
            try
            {
                DBHelper.ExecuteNonQuerySql(strSql, cmdParams);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteSpecialInfos(int generalId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@GeneralId", DbType.Int32, generalId);
            string strSql = "DELETE FROM PE_SpecialInfos WHERE GeneralId = @GeneralId";
            try
            {
                DBHelper.ExecuteNonQuerySql(strSql, cmdParams);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ExistInSpecialInfos(int generalId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@GeneralId", DbType.Int32, generalId);
            string strSql = "SELECT COUNT(*) FROM PE_SpecialInfos WHERE GeneralId = @GeneralId";
            return DBHelper.ExistsSql(strSql, cmdParams);
        }

        public bool ExistInSpecialInfos(int specialId, int generalId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@SpecialId", DbType.Int32, specialId);
            cmdParams.AddInParameter("@GeneralId", DbType.Int32, generalId);
            string strSql = "SELECT COUNT(*) FROM PE_SpecialInfos WHERE SpecialID = @SpecialId AND GeneralId = @GeneralId";
            return DBHelper.ExistsSql(strSql, cmdParams);
        }

        public bool ExistsSpecialCategoryIdInSpecials(int specialCategoryId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@SpecialCategoryId", DbType.Int32, specialCategoryId);
            string strSql = "SELECT COUNT(*) FROM PE_Specials WHERE SpecialCategoryID = @SpecialCategoryId";
            return DBHelper.ExistsSql(strSql, cmdParams);
        }

        public bool ExistsSpecialCategoryName(string specialCategoryName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@SpecialCategoryName", DbType.String, specialCategoryName);
            string strSql = "SELECT COUNT(*) FROM PE_SpecialCategory WHERE SpecialCategoryName = @SpecialCategoryName";
            return DBHelper.ExistsSql(strSql, cmdParams);
        }

        public bool ExistsSpecialDir(string specialDir)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@SpecialDir", DbType.String, specialDir);
            string strSql = "SELECT COUNT(*) FROM PE_Specials WHERE SpecialDir = @SpecialDir";
            return DBHelper.ExistsSql(strSql, cmdParams);
        }

        public bool ExistsSpecialName(string specialName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@SpecialName", DbType.String, specialName);
            string strSql = "SELECT COUNT(*) FROM PE_Specials WHERE SpecialName = @SpecialName";
            return DBHelper.ExistsSql(strSql, cmdParams);
        }

        public int GetCountSpecial(int specialCategoryId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@SpecialCategoryId", DbType.Int32, specialCategoryId);
            string strSql = "SELECT COUNT(*) FROM PE_Specials";
            if (specialCategoryId > 0)
            {
                strSql = strSql + " WHERE SpecialCategoryID = @SpecialCategoryId";
            }
            return DataConverter.CLng(DBHelper.ExecuteScalarSql(strSql, cmdParams));
        }

        public string GetGeneralIdBySpecialId(string specialId)
        {
            StringBuilder builder = new StringBuilder();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT GeneralId FROM PE_SpecialInfos WHERE SpecialID IN(" + DBHelper.ToValidId(specialId) + ")"))
            {
                while (reader.Read())
                {
                    builder.Append(reader.GetInt32("GeneralId"));
                    builder.Append(",");
                }
            }
            return builder.ToString().TrimEnd(new char[] { ',' });
        }

        public string GetGeneralIdBySpecialInfoId(string specialInfoId)
        {
            StringBuilder builder = new StringBuilder();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT GeneralId FROM PE_SpecialInfos WHERE SpecialInfoId IN(" + DBHelper.ToValidId(specialInfoId) + ")"))
            {
                while (reader.Read())
                {
                    builder.Append(reader.GetInt32("GeneralId"));
                    builder.Append(",");
                }
            }
            return builder.ToString().TrimEnd(new char[] { ',' });
        }

        private static int GetMaxSpecialCategoryId()
        {
            return DBHelper.GetMaxId("PE_SpecialCategory", "SpecialCategoryID");
        }

        private static int GetMaxSpecialCategoryOrderId()
        {
            return DBHelper.GetMaxId("PE_SpecialCategory", "OrderId");
        }

        public int GetMaxSpecialId()
        {
            return DBHelper.GetMaxId("PE_Specials", "SpecialID");
        }

        private static int GetMaxSpecialInfoId()
        {
            return DBHelper.GetMaxId("PE_SpecialInfos", "SpecialInfoID");
        }

        private static int GetMaxSpecialOrderId(int specialCategoryId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@SpecialCategoryId", DbType.Int32, specialCategoryId);
            string strSql = "SELECT MAX(OrderId) FROM PE_Specials WHERE SpecialCategoryID = @SpecialCategoryId";
            return DataConverter.CLng(DBHelper.ExecuteScalarSql(strSql, cmdParams));
        }

        public SpecialCategoryInfo GetSpecialCategoryInfoById(int specialCategoryId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@SpecialCategoryId", DbType.Int32, specialCategoryId);
            string strCommand = "SELECT * FROM PE_SpecialCategory WHERE SpecialCategoryID = @SpecialCategoryId";
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, cmdParams))
            {
                if (reader.Read())
                {
                    return SpecialCategoryInfoFromrdr(reader);
                }
                return new SpecialCategoryInfo(true);
            }
        }

        public IList<SpecialCategoryInfo> GetSpecialCategoryList()
        {
            IList<SpecialCategoryInfo> list = new List<SpecialCategoryInfo>();
            string strCommand = "SELECT * FROM PE_SpecialCategory ORDER BY OrderId, SpecialCategoryID DESC";
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, null))
            {
                while (reader.Read())
                {
                    list.Add(SpecialCategoryInfoFromrdr(reader));
                }
            }
            return list;
        }

        public IList<SpecialCategoryInfo> GetSpecialCategoryList(string specialCategoryId)
        {
            IList<SpecialCategoryInfo> list = new List<SpecialCategoryInfo>();
            string strCommand = "SELECT * FROM PE_SpecialCategory WHERE SpecialCategoryId IN (" + DBHelper.ToValidId(specialCategoryId) + ") ORDER BY OrderId, SpecialCategoryID DESC";
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, null))
            {
                while (reader.Read())
                {
                    list.Add(SpecialCategoryInfoFromrdr(reader));
                }
            }
            return list;
        }

        private static void GetSpecialCategoryParameters(SpecialCategoryInfo specialCategoryInfo, Parameters parms)
        {
            parms.AddInParameter("@SpecialCategoryId", DbType.Int32, specialCategoryInfo.SpecialCategoryId);
            parms.AddInParameter("@SpecialCategoryName", DbType.String, specialCategoryInfo.SpecialCategoryName);
            parms.AddInParameter("@SpecialCategoryDir", DbType.String, specialCategoryInfo.SpecialCategoryDir);
            parms.AddInParameter("@SpecialTemplatePath", DbType.String, specialCategoryInfo.SpecialTemplatePath);
            parms.AddInParameter("@SearchTemplatePath", DbType.String, specialCategoryInfo.SearchTemplatePath);
            parms.AddInParameter("@OrderId", DbType.Int32, specialCategoryInfo.OrderId);
            parms.AddInParameter("@OpenType", DbType.Boolean, specialCategoryInfo.OpenType);
            parms.AddInParameter("@Description", DbType.String, specialCategoryInfo.Description);
            parms.AddInParameter("@PagePostfix", DbType.String, specialCategoryInfo.PagePostfix);
            parms.AddInParameter("@IsCreateHtml", DbType.Boolean, specialCategoryInfo.IsCreateHtml);
            parms.AddInParameter("@NeedCreateHtml", DbType.Boolean, specialCategoryInfo.NeedCreateHtml);
        }

        public SpecialInfo GetSpecialInfoById(int specialId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@SpecialId", DbType.Int32, specialId);
            string strCommand = "SELECT * FROM PE_Specials WHERE SpecialID = @SpecialId";
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, cmdParams))
            {
                if (reader.Read())
                {
                    return SpecialInfoFromrdr(reader);
                }
                return new SpecialInfo(true);
            }
        }

        public string GetSpecialInfoIds(int generalId)
        {
            StringBuilder builder = new StringBuilder();
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@GeneralId", DbType.Int32, generalId);
            string strCommand = "SELECT SpecialID FROM PE_SpecialInfos WHERE GeneralId = @GeneralId";
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, cmdParams))
            {
                while (reader.Read())
                {
                    builder.Append(reader.GetInt32("SpecialID"));
                    builder.Append(",");
                }
            }
            return builder.ToString().TrimEnd(new char[] { ',' });
        }

        public IList<SpecialInfo> GetSpecialList()
        {
            IList<SpecialInfo> list = new List<SpecialInfo>();
            string strCommand = "SELECT * FROM PE_Specials ORDER BY OrderId, SpecialID DESC";
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, null))
            {
                while (reader.Read())
                {
                    list.Add(SpecialInfoFromrdr(reader));
                }
            }
            return list;
        }

        public IList<SpecialInfo> GetSpecialList(int specialCategoryId)
        {
            IList<SpecialInfo> list = new List<SpecialInfo>();
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@SpecialCategoryId", DbType.Int32, specialCategoryId);
            string strCommand = "SELECT * FROM PE_Specials";
            if (specialCategoryId > 0)
            {
                strCommand = strCommand + " WHERE SpecialCategoryID = @SpecialCategoryId";
            }
            strCommand = strCommand + " ORDER BY SpecialCategoryID DESC, OrderId ASC";
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, cmdParams))
            {
                while (reader.Read())
                {
                    list.Add(SpecialInfoFromrdr(reader));
                }
            }
            return list;
        }

        public IList<SpecialInfo> GetSpecialList(string specialId)
        {
            IList<SpecialInfo> list = new List<SpecialInfo>();
            string str = "SELECT * FROM PE_Specials";
            if (specialId.IndexOf(',') < 0)
            {
                str = str + " WHERE SpecialID = " + DBHelper.ToNumber(specialId);
            }
            else
            {
                str = str + " WHERE SpecialID IN (" + DBHelper.ToValidId(specialId) + ")";
            }
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(str + " ORDER BY SpecialCategoryID DESC, OrderId ASC"))
            {
                while (reader.Read())
                {
                    list.Add(SpecialInfoFromrdr(reader));
                }
            }
            return list;
        }

        public IList<SpecialInfo> GetSpecialList(int startRowIndexId, int maxNumberRows, int specialCategoryId, int listType)
        {
            Database database = DatabaseFactory.CreateDatabase();
            IList<SpecialInfo> list = new List<SpecialInfo>();
            string str = "SpecialID";
            string str2 = "DESC";
            switch (listType)
            {
                case -2:
                    str = "SpecialID";
                    str2 = "ASC";
                    break;

                case 1:
                    str = "SpecialID";
                    str2 = "DESC";
                    break;

                case 2:
                    str = "SpecialCategoryID";
                    str2 = "ASC";
                    break;

                case 3:
                    str = "SpecialCategoryID";
                    str2 = "DESC";
                    break;
            }
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, str);
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "*");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, str2);
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String);
            if (specialCategoryId > 0)
            {
                database.SetParameterValue(storedProcCommand, "@Filter", "SpecialCategoryID = " + specialCategoryId);
            }
            else
            {
                database.SetParameterValue(storedProcCommand, "@Filter", "");
            }
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_Specials");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(SpecialInfoFromrdr(reader));
                }
            }
            this.m_TotalOfSpecial = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        private static void GetSpecialParameters(SpecialInfo specialInfo, Parameters parms)
        {
            parms.AddInParameter("@SpecialId", DbType.Int32, specialInfo.SpecialId);
            parms.AddInParameter("@SpecialName", DbType.String, specialInfo.SpecialName);
            parms.AddInParameter("@SpecialCategoryId", DbType.Int32, specialInfo.SpecialCategoryId);
            parms.AddInParameter("@SpecialDir", DbType.String, specialInfo.SpecialDir);
            parms.AddInParameter("@SpecialIdentifier", DbType.String, specialInfo.SpecialIdentifier);
            parms.AddInParameter("@SpecialPic", DbType.String, specialInfo.SpecialPic);
            parms.AddInParameter("@SpecialTips", DbType.String, specialInfo.SpecialTips);
            parms.AddInParameter("@SpecialTemplatePath", DbType.String, specialInfo.SpecialTemplatePath);
            parms.AddInParameter("@SearchTemplatePath", DbType.String, specialInfo.SearchTemplatePath);
            parms.AddInParameter("@IsElite", DbType.Boolean, specialInfo.IsElite);
            parms.AddInParameter("@OrderId", DbType.Int32, specialInfo.OrderId);
            parms.AddInParameter("@OpenType", DbType.Int32, specialInfo.OpenType);
            parms.AddInParameter("@Description", DbType.String, specialInfo.Description);
            parms.AddInParameter("@IsCreateListPage", DbType.Boolean, specialInfo.IsCreateListPage);
            parms.AddInParameter("@ListPageSavePathType", DbType.Int32, specialInfo.ListPageSavePathType);
            parms.AddInParameter("@ListPagePostfix", DbType.String, specialInfo.ListPagePostfix);
            parms.AddInParameter("@CustomContent", DbType.String, specialInfo.CustomContent);
            parms.AddInParameter("@NeedCreateHtml", DbType.Boolean, specialInfo.NeedCreateHtml);
        }

        public int GetTotalOfSpecial()
        {
            return this.m_TotalOfSpecial;
        }

        public void MoveSpecialInfoBySpecialId(string sourceSpecialId, int targetSpecialId)
        {
            string strSql = "UPDATE PE_SpecialInfos SET SpecialId = @TargetSpecialId WHERE SpecialId IN(" + DBHelper.ToValidId(sourceSpecialId) + ")";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@TargetSpecialId", DbType.Int32, targetSpecialId);
            DBHelper.ExecuteNonQuerySql(strSql, cmdParams);
        }

        public void ReplaceTemplateDir(string oldDir, string newDir)
        {
            oldDir = oldDir.Replace("'", "''");
            newDir = newDir.Replace("'", "''");
            string str = "'" + oldDir + "%'";
            DBHelper.ExecuteSql("UPDATE PE_SpecialCategory SET SpecialTemplatePath = replace(cast(SpecialTemplatePath AS nvarchar(4000)), '" + oldDir + "', '" + newDir + "') WHERE SpecialTemplatePath LIKE " + str);
            DBHelper.ExecuteSql("UPDATE PE_Specials SET SpecialTemplatePath = replace(cast(SpecialTemplatePath AS nvarchar(4000)), '" + oldDir + "', '" + newDir + "') WHERE SpecialTemplatePath LIKE " + str);
        }

        public void ReplaceTemplateFileName(string replaceFormer, string replaceAfter)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ReplaceFormer", DbType.String, replaceFormer);
            cmdParams.AddInParameter("@ReplaceAfter", DbType.String, replaceAfter);
            DBHelper.ExecuteSql("UPDATE PE_SpecialCategory SET SpecialTemplatePath = @ReplaceAfter WHERE SpecialTemplatePath = @ReplaceFormer", cmdParams);
            DBHelper.ExecuteSql("UPDATE PE_Specials SET SpecialTemplatePath = @ReplaceAfter WHERE SpecialTemplatePath = @ReplaceFormer", cmdParams);
        }

        public bool SpecialBatchSet(SpecialInfo specialInfo, string specialIds, Dictionary<string, bool> checkItem)
        {
            StringBuilder builder = new StringBuilder();
            Parameters cmdParams = new Parameters();
            builder.Append("UPDATE PE_Specials SET ");
            if (checkItem["OpenType"])
            {
                builder.Append("OpenType = @OpenType,");
                cmdParams.AddInParameter("@OpenType", DbType.Int32, specialInfo.OpenType);
            }
            if (checkItem["IsElite"])
            {
                builder.Append("IsElite = @IsElite,");
                cmdParams.AddInParameter("@IsElite", DbType.Boolean, specialInfo.IsElite);
            }
            if (checkItem["SpecialTemplatePath"])
            {
                builder.Append("SpecialTemplatePath = @SpecialTemplatePath,");
                cmdParams.AddInParameter("@SpecialTemplatePath", DbType.String, specialInfo.SpecialTemplatePath);
            }
            builder.Remove(builder.Length - 1, 1);
            builder.Append(" WHERE SpecialID IN ( ");
            builder.Append(specialIds);
            builder.Append(" )");
            return DBHelper.ExecuteSql(builder.ToString(), cmdParams);
        }

        private static SpecialCategoryInfo SpecialCategoryInfoFromrdr(NullableDataReader rdr)
        {
            SpecialCategoryInfo info = new SpecialCategoryInfo();
            info.SpecialCategoryId = rdr.GetInt32("SpecialCategoryID");
            info.SpecialCategoryDir = rdr.GetString("SpecialCategoryDir");
            info.SpecialCategoryName = rdr.GetString("SpecialCategoryName");
            info.SpecialTemplatePath = rdr.GetString("SpecialTemplatePath");
            info.SearchTemplatePath = rdr.GetString("SearchTemplatePath");
            info.OrderId = rdr.GetInt32("OrderId");
            info.OpenType = rdr.GetBoolean("OpenType");
            info.Description = rdr.GetString("Description");
            info.IsCreateHtml = rdr.GetBoolean("IsCreateHtml");
            info.PagePostfix = rdr.GetString("PagePostfix");
            info.NeedCreateHtml = rdr.GetBoolean("NeedCreateHtml");
            return info;
        }

        private static SpecialInfo SpecialInfoFromrdr(NullableDataReader rdr)
        {
            SpecialInfo info = new SpecialInfo();
            info.SpecialId = rdr.GetInt32("SpecialID");
            info.SpecialName = rdr.GetString("SpecialName");
            info.SpecialCategoryId = rdr.GetInt32("SpecialCategoryID");
            info.SpecialDir = rdr.GetString("SpecialDir");
            info.SpecialIdentifier = rdr.GetString("SpecialIdentifier");
            info.SpecialPic = rdr.GetString("SpecialPic");
            info.SpecialTips = rdr.GetString("SpecialTips");
            info.SpecialTemplatePath = rdr.GetString("SpecialTemplatePath");
            info.SearchTemplatePath = rdr.GetString("SearchTemplatePath");
            info.IsElite = rdr.GetBoolean("IsElite");
            info.OrderId = rdr.GetInt32("OrderId");
            info.OpenType = rdr.GetInt32("OpenType");
            info.Description = rdr.GetString("Description");
            info.IsCreateListPage = rdr.GetBoolean("IsCreateListPage");
            info.ListPagePostfix = rdr.GetString("ListPagePostfix");
            info.ListPageSavePathType = (ListPagePathType) rdr.GetInt32("ListPageSavePathType");
            info.CustomContent = rdr.GetString("Custom_Content");
            info.NeedCreateHtml = rdr.GetBoolean("NeedCreateHtml");
            return info;
        }

        public bool UpdateNeedCreateHtml(string arrSpecialId, bool needCreateHtml)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@needCreateHtml", DbType.Boolean, needCreateHtml);
            return DBHelper.ExecuteSql("UPDATE PE_Specials SET NeedCreateHtml = @needCreateHtml WHERE SpecialID IN (" + DBHelper.ToValidId(arrSpecialId) + ")", cmdParams);
        }

        public bool UpdateSpecial(SpecialInfo specialInfo)
        {
            Parameters parms = new Parameters();
            GetSpecialParameters(specialInfo, parms);
            string strSql = "UPDATE PE_Specials SET SpecialName = @SpecialName, SpecialCategoryID = @SpecialCategoryId,SpecialDir = @SpecialDir, SpecialIdentifier = @SpecialIdentifier, SpecialPic = @SpecialPic,SpecialTips = @SpecialTips, SpecialTemplatePath = @SpecialTemplatePath, SearchTemplatePath = @SearchTemplatePath, IsElite = @IsElite,OrderId = @OrderId, OpenType = @OpenType, Description = @Description, IsCreateListPage = @IsCreateListPage,ListPageSavePathType = @ListPageSavePathType, ListPagePostfix = @ListPagePostfix, Custom_Content = @CustomContent, NeedCreateHtml = @NeedCreateHtml WHERE SpecialID = @SpecialId";
            return DBHelper.ExecuteSql(strSql, parms);
        }

        public bool UpdateSpecialCategory(SpecialCategoryInfo specialCategoryInfo)
        {
            Parameters parms = new Parameters();
            GetSpecialCategoryParameters(specialCategoryInfo, parms);
            string strSql = "UPDATE PE_SpecialCategory SET SpecialCategoryName = @SpecialCategoryName, SpecialCategoryDir = @SpecialCategoryDir, SpecialTemplatePath = @SpecialTemplatePath, SearchTemplatePath = @SearchTemplatePath, OrderId = @OrderId, OpenType = @OpenType, Description = @Description, IsCreateHtml = @IsCreateHtml, PagePostfix = @PagePostfix, NeedCreateHtml = @NeedCreateHtml WHERE SpecialCategoryID = @SpecialCategoryId";
            return DBHelper.ExecuteSql(strSql, parms);
        }

        public bool UpdateSpecialCategoryNeedCreateHtml(string arrSpecialCategoryId, bool needCreateHtml)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@needCreateHtml", DbType.Boolean, needCreateHtml);
            return DBHelper.ExecuteSql("UPDATE PE_SpecialCategory SET NeedCreateHtml = @needCreateHtml WHERE SpecialCategoryID IN (" + DBHelper.ToValidId(arrSpecialCategoryId) + ")", cmdParams);
        }

        public void UpdateSpecialIdByGeneralId(int specialId, int sourceSpecialId, string specialInfoId)
        {
            string strSql = "UPDATE PE_SpecialInfos SET SpecialId = @SpecialId WHERE SpecialID = @SourceSpecialId AND SpecialInfoId IN(" + DBHelper.ToValidId(specialInfoId) + ")";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@SpecialId", DbType.Int32, specialId);
            cmdParams.AddInParameter("@SourceSpecialId", DbType.Int32, sourceSpecialId);
            DBHelper.ExecuteNonQuerySql(strSql, cmdParams);
        }
    }
}

