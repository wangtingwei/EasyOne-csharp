namespace EasyOne.SqlServerDal.Shop
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.Common;
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Shop;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Text;

    public class Statistics : IStatistics
    {
        private int m_TotalOfCategorySaleroom;
        private int m_TotalOfCompareHitsOrderNumber;
        private int m_TotalOfMemberExpenditure;
        private int m_TotalOfMemberOrderliness;
        private int m_TotalOfMemberOrders;
        private int m_TotalOfProductHits;
        private int m_TotalOfProductOrderNumber;

        public IList<CategorySaleroomInfo> GetCategorySaleroomList(int startRowIndexId, int maxNumberRows, int orderType, DateTime time, bool isAll)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Shop_Statistics_GetList");
            if (orderType == 1)
            {
                database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "SUM(I.SubTotal)");
            }
            else
            {
                database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "SUM(I.Amount)");
            }
            if (!isAll)
            {
                database.AddInParameter(storedProcCommand, "@Filter", DbType.String, "DATEDIFF(m, O.InputTime, '" + time + "') = 0 AND O.MoneyReceipt >= O.MoneyTotal");
            }
            else
            {
                database.AddInParameter(storedProcCommand, "@Filter", DbType.String, "O.MoneyReceipt >= O.MoneyTotal");
            }
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "SUM(I.Amount) AS SalesVolumn, MAX(N.NodeName) AS NodeName, SUM(I.SubTotal) AS Saleroom, N.NodeID");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, " PE_Orders AS O INNER JOIN PE_OrderItem AS I INNER JOIN PE_CommonModel AS C INNER JOIN PE_Nodes AS N ON C.NodeID = N.NodeID ON C.ItemID = I.ProductID AND C.TableName = I.TableName ON O.OrderID = I.OrderID");
            database.AddInParameter(storedProcCommand, "@ID", DbType.String, "N.NodeID");
            database.AddInParameter(storedProcCommand, "@Group", DbType.String, "GROUP BY N.NodeID");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            IList<CategorySaleroomInfo> list = new List<CategorySaleroomInfo>();
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    CategorySaleroomInfo item = new CategorySaleroomInfo();
                    item.NodeName = reader.GetString("NodeName");
                    item.SalesVolumn = reader.GetInt32("SalesVolumn");
                    item.Saleroom = reader.GetDecimal("Saleroom");
                    list.Add(item);
                }
            }
            this.m_TotalOfCategorySaleroom = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public IList<CompareHitsOrderNumberInfo> GetCompareHitsOrderNumberList(int startRowIndexId, int maxNumberRows, int orderType)
        {
            string str;
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Shop_Statistics_GetList");
            switch (orderType)
            {
                case 1:
                    str = "SUM(M.Hits)";
                    break;

                case 2:
                    str = "SUM(P.BuyTimes)";
                    break;

                default:
                    str = "(SUM(P.BuyTimes)*1.0)/(SUM(M.Hits))";
                    break;
            }
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, str);
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "Max(P.ProductName) AS ProductName, SUM(M.Hits) AS Hits, SUM(P.BuyTimes) AS BuyTimes, CONVERT(float,(SUM(P.BuyTimes)*1.0)/(SUM(M.Hits))) AS CompareRate");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_CommonProduct P INNER JOIN PE_CommonModel M ON P.TableName = M.TableName AND P.ProductID = M.ItemID");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, "M.Hits > 0");
            database.AddInParameter(storedProcCommand, "@ID", DbType.String, "P.ProductID");
            database.AddInParameter(storedProcCommand, "@Group", DbType.String, "Group by P.ProductID");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            IList<CompareHitsOrderNumberInfo> list = new List<CompareHitsOrderNumberInfo>();
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    CompareHitsOrderNumberInfo item = new CompareHitsOrderNumberInfo();
                    item.ProductName = reader.GetString("ProductName");
                    item.Hits = reader.GetInt32("Hits");
                    item.OrderNumber = reader.GetInt32("BuyTimes");
                    item.CompareRate = reader.GetDouble("CompareRate");
                    list.Add(item);
                }
            }
            this.m_TotalOfCompareHitsOrderNumber = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public int GetHaveOrderOfMember()
        {
            return DBHelper.ObjectToInt32(DBHelper.ExecuteScalarSql("SELECT COUNT(DISTINCT U.UserID) FROM PE_Users AS U INNER JOIN PE_Orders AS O ON U.UserName = O.UserName AND O.MoneyReceipt >= O.MoneyTotal"));
        }

        public IList<MemberExpenditureInfo> GetMemberExpenditureList(int startRowIndexId, int maxNumberRows)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Shop_Statistics_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "SUM(O.MoneyReceipt)");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, " Max(U.UserName) AS UserName, SUM(O.MoneyReceipt) AS totalMoneyReceip");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_Users AS U INNER JOIN PE_Orders AS O ON O.UserName = U.UserName");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, "O.MoneyReceipt > 0");
            database.AddInParameter(storedProcCommand, "@ID", DbType.String, "U.UserID");
            database.AddInParameter(storedProcCommand, "@Group", DbType.String, "GROUP BY U.UserID");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            IList<MemberExpenditureInfo> list = new List<MemberExpenditureInfo>();
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    MemberExpenditureInfo item = new MemberExpenditureInfo();
                    item.UserName = reader.GetString("UserName");
                    item.MoneyReceipt = reader.GetDecimal("totalMoneyReceip");
                    list.Add(item);
                }
            }
            this.m_TotalOfMemberExpenditure = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public IList<MemberOrderlinessInfo> GetMemberOrderlinessList(int startRowIndexId, int maxNumberRows, string userName, DateTime time, bool isAll)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Shop_Statistics_GetList");
            StringBuilder builder = new StringBuilder();
            builder.Append("O.UserName = '" + DBHelper.FilterBadChar(userName) + "' AND O.MoneyReceipt >= O.MoneyTotal");
            if (!isAll)
            {
                builder.Append(" AND DATEDIFF(m, O.InputTime, '" + time + "') = 0");
            }
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "SUM(I.SubTotal)");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "MAX(P.ProductName) AS ProductName, SUM(I.Amount) AS Amount, SUM(I.SubTotal) AS SubTotal");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_Orders AS O INNER JOIN PE_OrderItem AS I INNER JOIN PE_CommonProduct AS P ON I.ProductID = P.ProductID AND I.TableName = P.TableName ON O.OrderID = I.OrderID");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, builder.ToString());
            database.AddInParameter(storedProcCommand, "@ID", DbType.String, " P.ProductID");
            database.AddInParameter(storedProcCommand, "@Group", DbType.String, "GROUP BY P.ProductID");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            IList<MemberOrderlinessInfo> list = new List<MemberOrderlinessInfo>();
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    MemberOrderlinessInfo item = new MemberOrderlinessInfo();
                    item.ProductName = reader.GetString("ProductName");
                    item.Amount = reader.GetInt32("Amount");
                    item.SubTotal = reader.GetDecimal("SubTotal");
                    list.Add(item);
                }
            }
            this.m_TotalOfMemberOrderliness = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public IList<MemberOrdersInfo> GetMemberOrdersList(int startRowIndexId, int maxNumberRows)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Shop_Statistics_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "COUNT(O.OrderID)");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "Max(U.UserName) AS UserName, COUNT(O.OrderID) AS OrderCount");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_Users AS U INNER JOIN PE_Orders AS O ON O.UserName = U.UserName");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, "(O.Status <> 3) AND O.MoneyReceipt >= O.MoneyTotal");
            database.AddInParameter(storedProcCommand, "@ID", DbType.String, "U.UserID");
            database.AddInParameter(storedProcCommand, "@Group", DbType.String, "GROUP BY U.UserID");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            IList<MemberOrdersInfo> list = new List<MemberOrdersInfo>();
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    MemberOrdersInfo item = new MemberOrdersInfo();
                    item.UserName = reader.GetString("UserName");
                    item.OrderNum = reader.GetInt32("OrderCount");
                    list.Add(item);
                }
            }
            this.m_TotalOfMemberOrders = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public IList<ProductHitsInfo> GetProductHitsList(int startRowIndexId, int maxNumberRows)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Shop_Statistics_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "M.Hits");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "P.ProductName, M.Hits");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_CommonProduct P INNER JOIN PE_CommonModel M ON P.TableName = M.TableName AND P.ProductID = M.ItemID");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, "P.ProductType <> 4 AND M.LinkType = 0 AND M.Hits <> 0");
            database.AddInParameter(storedProcCommand, "@ID", DbType.String, "P.ProductID");
            database.AddInParameter(storedProcCommand, "@Group", DbType.String, "");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            IList<ProductHitsInfo> list = new List<ProductHitsInfo>();
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    ProductHitsInfo item = new ProductHitsInfo();
                    item.ProductName = reader.GetString("ProductName");
                    item.Hits = reader.GetInt32("Hits");
                    list.Add(item);
                }
            }
            this.m_TotalOfProductHits = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public IList<ProductOrderNumberInfo> GetProductOrderNumberList(int startRowIndexId, int maxNumberRows)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Shop_Statistics_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "OrderNum");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "ProductName, OrderNum");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_CommonProduct");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, "OrderNum > 0");
            database.AddInParameter(storedProcCommand, "@ID", DbType.String, "ID");
            database.AddInParameter(storedProcCommand, "@Group", DbType.String, "");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            IList<ProductOrderNumberInfo> list = new List<ProductOrderNumberInfo>();
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    ProductOrderNumberInfo item = new ProductOrderNumberInfo();
                    item.ProductName = reader.GetString("ProductName");
                    item.OrderNumber = reader.GetInt32("OrderNum");
                    list.Add(item);
                }
            }
            this.m_TotalOfProductOrderNumber = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public decimal GetSaleOfDay(DateTime saleTime)
        {
            return DataConverter.CDecimal(DBHelper.ExecuteScalarSql("SELECT SUM(MoneyTotal) AS Sale FROM PE_Orders WHERE DATEDIFF(dd, InputTime, @SaleTime) = 0 AND MoneyReceipt >= MoneyTotal", new Parameters("@SaleTime", DbType.DateTime, saleTime)));
        }

        public decimal GetSaleOfMonth(DateTime saleTime)
        {
            return DataConverter.CDecimal(DBHelper.ExecuteScalarSql("SELECT SUM(MoneyTotal) AS Sale FROM PE_Orders WHERE DATEDIFF(m, InputTime, @SaleTime) = 0 AND MoneyReceipt >= MoneyTotal", new Parameters("@SaleTime", DbType.DateTime, saleTime)));
        }

        public decimal GetSaleOfYear(DateTime saleTime)
        {
            return DataConverter.CDecimal(DBHelper.ExecuteScalarSql("SELECT SUM(MoneyTotal) AS Sale FROM PE_Orders WHERE DATEDIFF(yy, InputTime, @SaleTime) = 0 AND MoneyReceipt >= MoneyTotal", new Parameters("@SaleTime", DbType.DateTime, saleTime)));
        }

        public int GetTotalHits()
        {
            return DBHelper.ObjectToInt32(DBHelper.ExecuteScalarSql("SELECT SUM(C.Hits) AS Hits FROM PE_CommonModel AS C INNER JOIN PE_CommonProduct AS P ON C.TableName = P.TableName AND C.ItemID = P.ProductID"));
        }

        public int GetTotalMember()
        {
            return DBHelper.ObjectToInt32(DBHelper.ExecuteScalarSql("SELECT COUNT(*) FROM PE_Users"));
        }

        public decimal GetTotalMoney(DateTime beginDate, DateTime endDate)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@BeginDate", DbType.DateTime, beginDate);
            cmdParams.AddInParameter("@EndDate", DbType.DateTime, endDate);
            return DataConverter.CDecimal(DBHelper.ExecuteScalarSql("SELECT SUM(MoneyTotal) AS MoneyTotal FROM PE_Orders WHERE InputTime > @BeginDate AND InputTime <= @EndDate AND MoneyReceipt >= MoneyTotal", cmdParams));
        }

        public int GetTotalOfCategorySaleroom()
        {
            return this.m_TotalOfCategorySaleroom;
        }

        public int GetTotalOfCompareHitsOrderNumber()
        {
            return this.m_TotalOfCompareHitsOrderNumber;
        }

        public int GetTotalOfMemberExpenditure()
        {
            return this.m_TotalOfMemberExpenditure;
        }

        public int GetTotalOfMemberOrderliness()
        {
            return this.m_TotalOfMemberOrderliness;
        }

        public int GetTotalOfMemberOrders()
        {
            return this.m_TotalOfMemberOrders;
        }

        public int GetTotalOfProductHits()
        {
            return this.m_TotalOfProductHits;
        }

        public int GetTotalOfProductOrderNumber()
        {
            return this.m_TotalOfProductOrderNumber;
        }

        public int GetTotalOrder(DateTime beginDate, DateTime endDate)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@BeginDate", DbType.DateTime, beginDate);
            cmdParams.AddInParameter("@EndDate", DbType.DateTime, endDate);
            return DBHelper.ObjectToInt32(DBHelper.ExecuteScalarSql("SELECT COUNT(*) FROM PE_Orders WHERE InputTime > @BeginDate AND InputTime <= @EndDate AND MoneyReceipt >= MoneyTotal", cmdParams));
        }
    }
}

