namespace EasyOne.SqlServerDal.Accessories
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.IDal.Accessories;
    using EasyOne.Model.Accessories;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Text;

    public sealed class Keywords : IKeywords
    {
        private int m_TotalOfkeyword;

        public bool Add(KeywordInfo keywordInfo)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@KeywordText", DbType.StringFixedLength, keywordInfo.KeywordText);
            cmdParams.AddInParameter("@KeywordType", DbType.Int32, keywordInfo.KeywordType);
            cmdParams.AddInParameter("@Priority", DbType.Int32, keywordInfo.Priority);
            cmdParams.AddInParameter("@ArrayGeneralId", DbType.String, keywordInfo.ArrayGeneralId);
            cmdParams.AddInParameter("@QuoteTimes", DbType.Int32, keywordInfo.QuoteTimes);
            return DBHelper.ExecuteProc("PR_Accessories_Keywords_Add", cmdParams);
        }

        public bool Delete(string id)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@KeywordID", DbType.String, id);
            return DBHelper.ExecuteProc("PR_Accessories_Keywords_Delete", cmdParams);
        }

        public bool Exists(string keywordText)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@KeywordText", DbType.String, keywordText);
            return DBHelper.ExistsProc("PR_Accessories_Keywords_Exists", cmdParams);
        }

        public KeywordInfo GetKeywordById(int id)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@KeywordID", DbType.Int32, id);
            using (NullableDataReader reader = DBHelper.ExecuteReaderProc("PR_Accessories_Keywords_GetById", cmdParams))
            {
                if (reader.Read())
                {
                    return KeywordsFromrdr(reader);
                }
                return new KeywordInfo(true);
            }
        }

        public KeywordInfo GetKeywordByKeywordName(string keyword)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@KeywordText", DbType.String, keyword);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_Keywords WHERE KeywordText = @KeywordText", cmdParams))
            {
                if (reader.Read())
                {
                    return KeywordsFromrdr(reader);
                }
                return new KeywordInfo(true);
            }
        }

        public ArrayList GetKeywords(int number)
        {
            string strSql = "SELECT TOP " + number.ToString() + " * FROM PE_Keywords ORDER BY LastUseTime DESC";
            ArrayList list = new ArrayList();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql))
            {
                while (reader.Read())
                {
                    list.Add(reader.GetString("KeywordText"));
                }
            }
            return list;
        }

        public IList<KeywordInfo> GetList(int startRowIndexId, int maxNumberRows, int searchType, string keyword, int listType)
        {
            Database database = DatabaseFactory.CreateDatabase();
            IList<KeywordInfo> list = new List<KeywordInfo>();
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
            database.SetParameterValue(storedProcCommand, "@SortColumn", "KeywordID");
            database.SetParameterValue(storedProcCommand, "@StrColumn", "KeywordID, KeywordText, KeywordType, Priority, Hits, LastUseTime, arrGeneralID, QuoteTimes");
            database.SetParameterValue(storedProcCommand, "@Sorts", "DESC");
            database.SetParameterValue(storedProcCommand, "@TableName", "PE_Keywords");
            switch (listType)
            {
                case 0:
                    database.SetParameterValue(storedProcCommand, "@Filter", "");
                    break;

                case 1:
                    database.SetParameterValue(storedProcCommand, "@Filter", "KeywordType = " + searchType);
                    break;

                case 2:
                    database.SetParameterValue(storedProcCommand, "@Filter", "KeywordText LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'");
                    break;

                case 3:
                    database.SetParameterValue(storedProcCommand, "@Filter", string.Concat(new object[] { "KeywordType =", searchType, " AND KeywordText LIKE '%", DBHelper.FilterBadChar(keyword), "%'" }));
                    break;
            }
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(KeywordsFromrdr(reader));
                }
            }
            this.m_TotalOfkeyword = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public string GetStrArrayKeywords(int number)
        {
            string strSql = "SELECT TOP " + number.ToString() + " * FROM PE_Keywords ORDER BY LastUseTime DESC";
            StringBuilder builder = new StringBuilder();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql))
            {
                while (reader.Read())
                {
                    if (builder.Length > 0)
                    {
                        builder.Append("|");
                    }
                    builder.Append(reader.GetString("KeywordText"));
                }
            }
            return builder.ToString();
        }

        public int GetTotalOfKeyword()
        {
            return this.m_TotalOfkeyword;
        }

        private static KeywordInfo KeywordsFromrdr(NullableDataReader rdr)
        {
            KeywordInfo info = new KeywordInfo();
            info.KeywordId = rdr.GetInt32("KeywordID");
            info.KeywordText = rdr.GetString("KeywordText");
            info.KeywordType = rdr.GetInt32("KeywordType");
            info.Priority = rdr.GetInt32("Priority");
            info.Hits = rdr.GetInt32("Hits");
            info.LastUseTime = rdr.GetDateTime("LastUseTime");
            info.ArrayGeneralId = rdr.GetString("arrGeneralID");
            info.QuoteTimes = rdr.GetInt32("QuoteTimes");
            return info;
        }

        public bool QueryKeyword(string keyword)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@KeywordText", DbType.StringFixedLength, keyword);
            return DBHelper.ExecuteProc("PR_Accessories_Keywords_SearchKeyword", cmdParams);
        }

        public bool Update(KeywordInfo keywordInfo)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@KeywordID", DbType.Int32, keywordInfo.KeywordId);
            cmdParams.AddInParameter("@KeywordText", DbType.StringFixedLength, keywordInfo.KeywordText);
            cmdParams.AddInParameter("@KeywordType", DbType.Int32, keywordInfo.KeywordType);
            cmdParams.AddInParameter("@Priority", DbType.Int32, keywordInfo.Priority);
            cmdParams.AddInParameter("@LastUseTime", DbType.DateTime, DateTime.Now);
            cmdParams.AddInParameter("@ArrayGeneralId", DbType.String, keywordInfo.ArrayGeneralId);
            cmdParams.AddInParameter("@QuoteTimes", DbType.Int32, keywordInfo.QuoteTimes);
            return DBHelper.ExecuteProc("PR_Accessories_Keywords_Update", cmdParams);
        }

        public bool UpdateHitsByKeywordName(string keywordname)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@KeywordText", DbType.String, keywordname);
            return DBHelper.ExecuteSql("UPDATE PE_Keywords SET Hits = Hits + 1 WHERE KeywordText = @KeywordText", cmdParams);
        }
    }
}

