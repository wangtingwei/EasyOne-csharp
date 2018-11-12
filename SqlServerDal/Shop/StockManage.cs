namespace EasyOne.SqlServerDal.Shop
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.Enumerations;
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Shop;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    public class StockManage : IStockManage
    {
        private int m_TotalOfStock;

        public bool Add(StockInfo stockInfo)
        {
            return DBHelper.ExecuteSql("INSERT INTO PE_Stock(StockID, StockNum, InputTime, Inputer, StockType, Remark) VALUES (@StockID, @StockNum, @InputTime, @Inputer, @StockType, @Remark)", GetParameters(stockInfo));
        }

        public bool Delete(string ids)
        {
            return DBHelper.ExecuteSql("DELETE FROM PE_Stock WHERE StockID IN (" + DBHelper.ToValidId(ids) + ")");
        }

        private static string GetFilter(int searchType, string keyword)
        {
            string str = "1 = 1 ";
            switch (searchType)
            {
                case 1:
                    return (str + "AND StockType = " + StockType.InStock.ToString("D"));

                case 2:
                    return (str + "AND StockType = " + StockType.Shipment.ToString("D"));

                case 3:
                    return (str + "AND StockNum LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'");

                case 4:
                    return (str + "AND Inputer LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'");

                case 5:
                    return (str + "AND DATEDIFF(dd, inputtime, '" + keyword + "') = 0");
            }
            return str;
        }

        public IList<StockInfo> GetList(int startRowIndexId, int maxNumberRows, int searchType, string keyword)
        {
            Database database = DatabaseFactory.CreateDatabase();
            IList<StockInfo> list = new List<StockInfo>();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "StockID");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "*");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, GetFilter(searchType, keyword));
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_Stock");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(StockFromrdr(reader));
                }
            }
            this.m_TotalOfStock = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public int GetMaxId()
        {
            return DBHelper.GetMaxId("PE_Stock", "StockID");
        }

        private static Parameters GetParameters(StockInfo stockInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@StockID", DbType.Int32, stockInfo.StockId);
            parameters.AddInParameter("@StockNum", DbType.String, stockInfo.StockNum);
            parameters.AddInParameter("@InputTime", DbType.DateTime, stockInfo.InputTime);
            parameters.AddInParameter("@Inputer", DbType.String, stockInfo.Inputer);
            parameters.AddInParameter("@StockType", DbType.Int32, (int) stockInfo.StockType);
            parameters.AddInParameter("@Remark", DbType.String, stockInfo.Remark);
            return parameters;
        }

        public StockInfo GetStockInfoById(int id)
        {
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_Stock WHERE StockID = @StockID", new Parameters("@StockID", DbType.Int32, id)))
            {
                if (reader.Read())
                {
                    return StockFromrdr(reader);
                }
                return new StockInfo(true);
            }
        }

        public int GetTotalOfStock()
        {
            return this.m_TotalOfStock;
        }

        private static StockInfo StockFromrdr(NullableDataReader rdr)
        {
            StockInfo info = new StockInfo();
            info.StockId = rdr.GetInt32("StockID");
            info.StockNum = rdr.GetString("StockNum");
            info.InputTime = rdr.GetDateTime("InputTime");
            info.Inputer = rdr.GetString("Inputer");
            info.StockType = (StockType) rdr.GetInt32("StockType");
            info.Remark = rdr.GetString("Remark");
            return info;
        }

        public bool Update(StockInfo stockInfo)
        {
            return DBHelper.ExecuteSql("UPDATE PE_Stock SET StockNum = @StockNum, InputTime = @InputTime, Inputer = @Inputer, StockType = @StockType, Remark = @Remark WHERE StockID = @StockID", GetParameters(stockInfo));
        }
    }
}

