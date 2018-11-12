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

    public sealed class WordReplace : IWordReplace
    {
        private int m_CountNumber;

        public bool Add(EasyOne.Model.Accessories.WordReplaceInfo wordReplaceInfo)
        {
            string strSql = "INSERT INTO PE_WordReplaceItem(ItemID, SourceWord, TargetWord, ReplaceType, Priority, ReplaceTimes, OpenType, IsEnabled, Title) VALUES (@ItemID, @SourceWord, @TargetWord, @ReplaceType, @Priority, @ReplaceTimes, @OpenType, @IsEnabled, @Title)";
            wordReplaceInfo.ItemId = this.GetMaxId() + 1;
            return DBHelper.ExecuteSql(strSql, GetParameters(wordReplaceInfo));
        }

        public bool Delete(string id)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ItemID", DbType.String, id);
            return DBHelper.ExecuteProc("PR_Accessories_WordReplace_Delete", cmdParams);
        }

        public bool Disabled(string id)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ItemID", DbType.String, id);
            cmdParams.AddInParameter("@IsEnabled", DbType.String, "0");
            return DBHelper.ExecuteProc("PR_Accessories_WordReplace_IsEnabled", cmdParams);
        }

        public bool Enabled(string id)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ItemID", DbType.String, id);
            cmdParams.AddInParameter("@IsEnabled", DbType.String, "1");
            return DBHelper.ExecuteProc("PR_Accessories_WordReplace_IsEnabled", cmdParams);
        }

        public bool Exists(string source, int type)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@SourceWord", DbType.String, source);
            cmdParams.AddInParameter("@ReplaceType", DbType.Int32, type);
            string strSql = "SELECT COUNT(*) FROM PE_WordReplaceItem WHERE SourceWord = @SourceWord AND ReplaceType = @ReplaceType";
            return DBHelper.ExistsSql(strSql, cmdParams);
        }

        public int GetCountNumber()
        {
            return this.m_CountNumber;
        }

        public EasyOne.Model.Accessories.WordReplaceInfo GetInfoById(int id)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ItemID", DbType.Int32, id);
            using (NullableDataReader reader = DBHelper.ExecuteReaderProc("PR_Accessories_WordReplace_GetById", cmdParams))
            {
                if (reader.Read())
                {
                    return WordReplaceInfo(reader);
                }
                return new EasyOne.Model.Accessories.WordReplaceInfo(true);
            }
        }

        public IList<EasyOne.Model.Accessories.WordReplaceInfo> GetInsideLink(int startRowIndexId, int maxNumberRows, string keyword, int listType)
        {
            Database database = DatabaseFactory.CreateDatabase();
            IList<EasyOne.Model.Accessories.WordReplaceInfo> list = new List<EasyOne.Model.Accessories.WordReplaceInfo>();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "ItemID");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "*");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, "");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_WordReplaceItem");
            switch (listType)
            {
                case 0:
                    database.SetParameterValue(storedProcCommand, "@Filter", " ReplaceType = 1");
                    break;

                case 1:
                    database.SetParameterValue(storedProcCommand, "@Filter", " ReplaceType = 1 AND SourceWord LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'");
                    break;

                case 2:
                    database.SetParameterValue(storedProcCommand, "@Filter", " ReplaceType = 1 AND IsEnabled = 1");
                    break;

                case 3:
                    database.SetParameterValue(storedProcCommand, "@Filter", " ReplaceType = 1 AND TargetWord LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'");
                    break;
            }
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(WordReplaceInfo(reader));
                }
            }
            this.m_CountNumber = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public int GetMaxId()
        {
            return DBHelper.GetMaxId("PE_WordReplaceItem", "ItemID");
        }

        private static Parameters GetParameters(EasyOne.Model.Accessories.WordReplaceInfo wordReplaceInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@ItemID", DbType.Int32, wordReplaceInfo.ItemId);
            parameters.AddInParameter("@SourceWord", DbType.String, wordReplaceInfo.SourceWord);
            parameters.AddInParameter("@TargetWord", DbType.String, wordReplaceInfo.TargetWord);
            parameters.AddInParameter("@ReplaceType", DbType.Int32, wordReplaceInfo.ReplaceType);
            parameters.AddInParameter("@Priority", DbType.Int32, wordReplaceInfo.Priority);
            parameters.AddInParameter("@ReplaceTimes", DbType.Int32, wordReplaceInfo.ReplaceTimes);
            parameters.AddInParameter("@OpenType", DbType.Boolean, wordReplaceInfo.OpenType);
            parameters.AddInParameter("@IsEnabled", DbType.Boolean, wordReplaceInfo.IsEnabled);
            parameters.AddInParameter("@Title", DbType.String, wordReplaceInfo.Title);
            return parameters;
        }

        public IList<EasyOne.Model.Accessories.WordReplaceInfo> GetWordFilterList()
        {
            IList<EasyOne.Model.Accessories.WordReplaceInfo> list = new List<EasyOne.Model.Accessories.WordReplaceInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_WordReplaceItem WHERE ReplaceType = 0 AND IsEnabled = 1 ORDER BY Priority DESC"))
            {
                while (reader.Read())
                {
                    list.Add(WordReplaceInfo(reader));
                }
            }
            return list;
        }

        public IList<EasyOne.Model.Accessories.WordReplaceInfo> GetWordFilterList(int startRowIndexId, int maxNumberRows, string keyword, int listType)
        {
            Database database = DatabaseFactory.CreateDatabase();
            IList<EasyOne.Model.Accessories.WordReplaceInfo> list = new List<EasyOne.Model.Accessories.WordReplaceInfo>();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "ItemID");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "*");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, "");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_WordReplaceItem");
            switch (listType)
            {
                case 0:
                    database.SetParameterValue(storedProcCommand, "@Filter", " ReplaceType = 0");
                    break;

                case 1:
                    database.SetParameterValue(storedProcCommand, "@Filter", " ReplaceType = 0 AND SourceWord LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'");
                    break;

                case 2:
                    database.SetParameterValue(storedProcCommand, "@Filter", " ReplaceType = 0 AND TargetWord LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'");
                    break;

                case 3:
                    database.SetParameterValue(storedProcCommand, "@Filter", " ReplaceType = 0 AND IsEnabled = 1");
                    break;
            }
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(WordReplaceInfo(reader));
                }
            }
            this.m_CountNumber = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public bool Update(EasyOne.Model.Accessories.WordReplaceInfo wordReplaceInfo)
        {
            string strSql = "UPDATE PE_WordReplaceItem SET SourceWord = @SourceWord, TargetWord = @TargetWord, ReplaceType = @ReplaceType, Priority = @Priority, ReplaceTimes = @ReplaceTimes, OpenType = @OpenType, IsEnabled = @IsEnabled, Title = @Title WHERE ItemID = @ItemID";
            return DBHelper.ExecuteSql(strSql, GetParameters(wordReplaceInfo));
        }

        private static EasyOne.Model.Accessories.WordReplaceInfo WordReplaceInfo(NullableDataReader rdr)
        {
            EasyOne.Model.Accessories.WordReplaceInfo info = new EasyOne.Model.Accessories.WordReplaceInfo();
            info.ItemId = rdr.GetInt32("ItemId");
            info.SourceWord = rdr.GetString("SourceWord");
            info.TargetWord = rdr.GetString("TargetWord");
            info.ReplaceType = rdr.GetInt32("ReplaceType");
            info.Priority = rdr.GetInt32("Priority");
            info.ReplaceTimes = rdr.GetInt32("ReplaceTimes");
            info.OpenType = rdr.GetBoolean("OpenType");
            info.IsEnabled = rdr.GetBoolean("IsEnabled");
            info.Title = rdr.GetString("Title");
            return info;
        }
    }
}

