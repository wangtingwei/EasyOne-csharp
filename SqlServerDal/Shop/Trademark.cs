namespace EasyOne.SqlServerDal.Shop
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Shop;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Text;

    public sealed class Trademark : ITrademark
    {
        private int m_TotalOfTrademark;

        public bool Add(TrademarkInfo trademarkInfo)
        {
            Parameters cmdParams = GetParameters(trademarkInfo);
            return DBHelper.ExecuteProc("PR_Shop_Trademark_Add", cmdParams);
        }

        public bool Delete(string trademarkId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@TrademarkID", DbType.String, trademarkId);
            return DBHelper.ExecuteProc("PR_Shop_Trademark_Delete", cmdParams);
        }

        public IList<TrademarkInfo> GetList(int producerId)
        {
            IList<TrademarkInfo> list = new List<TrademarkInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_Trademark WHERE ProducerID=@ProducerID AND Passed=1", new Parameters("@ProducerID", DbType.Int32, producerId)))
            {
                while (reader.Read())
                {
                    list.Add(trademarkInfoFromrdataReader(reader));
                }
            }
            return list;
        }

        public IList<TrademarkInfo> GetList(int startRowIndexId, int maxNumberRows, string searchType, string keyword, int trademarkType, bool isPassed)
        {
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
            database.SetParameterValue(storedProcCommand, "@SortColumn", "TrademarkId");
            database.SetParameterValue(storedProcCommand, "@StrColumn", "*");
            database.SetParameterValue(storedProcCommand, "@Sorts", "DESC");
            database.SetParameterValue(storedProcCommand, "@TableName", "PE_Trademark");
            StringBuilder builder = new StringBuilder("1 = 1");
            if (isPassed)
            {
                builder.Append(" AND Passed = 1");
            }
            if (string.IsNullOrEmpty(searchType))
            {
                if (trademarkType >= 0)
                {
                    builder.Append(" AND TrademarkType = " + trademarkType);
                }
            }
            else
            {
                builder.Append(" AND " + DBHelper.FilterBadChar(searchType) + " LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'");
            }
            database.SetParameterValue(storedProcCommand, "@Filter", builder.ToString());
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, 10);
            IList<TrademarkInfo> list = new List<TrademarkInfo>();
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(trademarkInfoFromrdataReader(reader));
                }
            }
            this.m_TotalOfTrademark = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        private static Parameters GetParameters(TrademarkInfo trademarkInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@TrademarkName", DbType.String, trademarkInfo.TrademarkName);
            parameters.AddInParameter("@ProducerId", DbType.Int32, trademarkInfo.ProducerId);
            parameters.AddInParameter("@IsElite", DbType.Boolean, trademarkInfo.IsElite);
            parameters.AddInParameter("@TrademarkType", DbType.Int32, trademarkInfo.TrademarkType);
            parameters.AddInParameter("@TrademarkPhoto", DbType.String, trademarkInfo.TrademarkPhoto);
            parameters.AddInParameter("@TrademarkIntro", DbType.String, trademarkInfo.TrademarkIntro);
            return parameters;
        }

        public int GetTotalOfTrademark()
        {
            return this.m_TotalOfTrademark;
        }

        public TrademarkInfo GetTrademarkById(int trademarkId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@TrademarkID", DbType.Int32, trademarkId);
            using (NullableDataReader reader = new NullableDataReader(DBHelper.ExecuteReaderProc("PR_Shop_Trademark_GetById", cmdParams)))
            {
                if (reader.Read())
                {
                    return trademarkInfoFromrdataReader(reader);
                }
                return new TrademarkInfo(true);
            }
        }

        public bool SetElite(int trademarkId, bool value)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@TrademarkID", DbType.Int32, trademarkId);
            cmdParams.AddInParameter("@IsElite", DbType.Boolean, value);
            return DBHelper.ExecuteProc("PR_Shop_Trademark_SetElite", cmdParams);
        }

        public bool SetOnTop(int trademarkId, bool value)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@TrademarkID", DbType.Int32, trademarkId);
            cmdParams.AddInParameter("@OnTop", DbType.Boolean, value);
            return DBHelper.ExecuteProc("PR_Shop_Trademark_SetOnTop", cmdParams);
        }

        public bool SetPassed(int trademarkId, bool value)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@TrademarkID", DbType.Int32, trademarkId);
            cmdParams.AddInParameter("@Passed", DbType.Boolean, value);
            return DBHelper.ExecuteProc("PR_Shop_Trademark_SetPassed", cmdParams);
        }

        private static TrademarkInfo trademarkInfoFromrdataReader(NullableDataReader dataReader)
        {
            TrademarkInfo info = new TrademarkInfo();
            info.TrademarkId = dataReader.GetInt32("TrademarkID");
            info.TrademarkType = dataReader.GetInt32("TrademarkType");
            info.ProducerId = dataReader.GetInt32("ProducerID");
            info.TrademarkName = dataReader.GetString("TrademarkName");
            info.TrademarkIntro = dataReader.GetString("TrademarkIntro");
            info.Passed = dataReader.GetBoolean("Passed");
            info.OnTop = dataReader.GetBoolean("OnTop");
            info.IsElite = dataReader.GetBoolean("IsElite");
            info.TrademarkPhoto = dataReader.GetString("TrademarkPhoto");
            return info;
        }

        public bool TrademarkNameExists(string trademarkName)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Shop_Trademark_TrademarkNameExists");
            database.AddInParameter(storedProcCommand, "@TrademarkName", DbType.String, trademarkName);
            database.AddParameter(storedProcCommand, "@RETURN_VALUE", DbType.String, ParameterDirection.ReturnValue, "", DataRowVersion.Current, null);
            database.ExecuteNonQuery(storedProcCommand);
            int num = (int) storedProcCommand.Parameters["@RETURN_VALUE"].Value;
            return (num == 1);
        }

        public bool Update(TrademarkInfo trademarkInfo)
        {
            Parameters cmdParams = GetParameters(trademarkInfo);
            cmdParams.AddInParameter("@TrademarkID", DbType.Int32, trademarkInfo.TrademarkId);
            return DBHelper.ExecuteProc("PR_Shop_Trademark_Update", cmdParams);
        }
    }
}

