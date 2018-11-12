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

    public class StockItem : IStockItem
    {
        private int m_TotalOfStockItem;

        public bool Add(StockItemInfo stockItemInfo)
        {
            return DBHelper.ExecuteSql("INSERT INTO PE_StockItem(ItemID, StockID, ProductID, [TableName], [Property], Amount, Price) VALUES (@ItemID, @StockID, @ProductID, @TableName, @Property, @Amount, @Price)", GetParameters(stockItemInfo));
        }

        public bool Delete(int productId, string tableName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ProductID", DbType.Int32, productId);
            cmdParams.AddInParameter("@TableName", DbType.String, tableName);
            return DBHelper.ExecuteSql("DELETE FROM PE_StockItem WHERE ProductID = @ProductID AND TableName = @TableName", cmdParams);
        }

        public bool DeleteByStockIds(string stockIds)
        {
            return DBHelper.ExecuteSql("DELETE FROM PE_StockItem WHERE StockID IN (" + DBHelper.ToValidId(stockIds) + ")");
        }

        public IList<StockItemInfo> GetListByStockId(int stockId)
        {
            IList<StockItemInfo> list = new List<StockItemInfo>();
            string strSql = "SELECT P.ProductNum, P.ProductName, P.Unit, S.* FROM PE_StockItem s INNER JOIN PE_CommonProduct p ON s.productId = p.productId AND s.TableName = P.TableName WHERE StockId = @StockId ; SELECT P.PresentNum AS ProductNum, P.PresentName AS ProductName, P.Unit, S.* FROM PE_StockItem s INNER JOIN PE_Present P ON s.productId = P.PresentID AND ISNULL(s.TableName,'')= '' WHERE s.StockId = @StockId";
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, new Parameters("@StockId", DbType.Int32, stockId)))
            {
                while (reader.Read())
                {
                    list.Add(StockItemFromrdr(reader));
                }
                if (!reader.NextResult())
                {
                    return list;
                }
                while (reader.Read())
                {
                    list.Add(StockItemFromrdr(reader));
                }
            }
            return list;
        }

        public IList<StockItemInfo> GetListByStockId(int startRowIndexId, int maxNumberRows, int stockId)
        {
            Database database = DatabaseFactory.CreateDatabase();
            IList<StockItemInfo> list = new List<StockItemInfo>();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "ItemID");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "P.ProductNum, P.ProductName, P.Unit, S.* ");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, "S.StockID = " + stockId.ToString());
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_StockItem S INNER JOIN PE_CommonProduct P ON (S.ProductID = P.ProductID)");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(StockItemFromrdr(reader));
                }
            }
            this.m_TotalOfStockItem = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public int GetMaxId()
        {
            return DBHelper.GetMaxId("PE_StockItem", "ItemID");
        }

        private static Parameters GetParameters(StockItemInfo stockItemInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@ItemID", DbType.Int32, stockItemInfo.ItemId);
            parameters.AddInParameter("@StockID", DbType.Int32, stockItemInfo.StockId);
            parameters.AddInParameter("@ProductID", DbType.Int32, stockItemInfo.ProductId);
            parameters.AddInParameter("@TableName", DbType.String, stockItemInfo.TableName);
            parameters.AddInParameter("@Property", DbType.String, stockItemInfo.Property);
            parameters.AddInParameter("@Amount", DbType.Int32, stockItemInfo.Amount);
            parameters.AddInParameter("@Price", DbType.Currency, stockItemInfo.Price);
            return parameters;
        }

        public int GetTotalOfStockItem()
        {
            return this.m_TotalOfStockItem;
        }

        private static StockItemInfo StockItemFromrdr(NullableDataReader rdr)
        {
            StockItemInfo info = new StockItemInfo();
            info.ItemId = rdr.GetInt32("ItemID");
            info.StockId = rdr.GetInt32("StockID");
            info.ProductId = rdr.GetInt32("ProductID");
            info.TableName = rdr.GetString("TableName");
            info.Property = rdr.GetString("Property");
            info.Amount = rdr.GetInt32("Amount");
            info.Price = rdr.GetDecimal("Price");
            info.ProductName = rdr.GetString("ProductName");
            info.ProductNum = rdr.GetString("ProductNum");
            info.Unit = rdr.GetString("Unit");
            return info;
        }
    }
}

