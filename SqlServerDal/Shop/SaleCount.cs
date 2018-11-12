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

    public sealed class SaleCount : ISaleCount
    {
        private int m_TotalOfSaleCount;

        public IList<SaleCountInfo> GetSaleCountList(int startRowIndexId, int maxNumberRows, string infoType, string searchType, string keyword, int orderType)
        {
            string str;
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Shop_Statistics_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@ID", DbType.String, "P.ProductID");
            database.AddInParameter(storedProcCommand, "@Group", DbType.String, "Group by P.ProductID");
            if ((orderType == 0) || (orderType == 1))
            {
                database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "SUM(I.Amount)");
                if (orderType == 1)
                {
                    database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "ASC");
                }
                else
                {
                    database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
                }
            }
            else
            {
                database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "SUM(I.SubTotal)");
                if (orderType == 3)
                {
                    database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "ASC");
                }
                else
                {
                    database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
                }
            }
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_OrderItem I INNER JOIN PE_CommonProduct P ON I.ProductID = P.ProductID AND I.TableName = P.TableName");
            StringBuilder builder = new StringBuilder();
            if (string.IsNullOrEmpty(infoType) || (infoType == "All"))
            {
                database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, " Max(I.ProductID) AS ProductID, Max(I.TableName) AS TableName, Max(P.ProductName) AS ProductName, Max(P.Unit) AS Unit, SUM(I.Amount) AS tAmount, SUM(I.SubTotal) AS tSubTotal ");
                builder.Append("I.OrderID IN (SELECT OrderID FROM PE_Orders WHERE MoneyReceipt >= MoneyTotal OR MoneyReceipt > 0)");
            }
            else
            {
                database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, " Max(I.ProductID) AS ProductID, Max(I.TableName) AS TableName, Max(P.ProductName) AS ProductName, Max(P.Unit) AS Unit, SUM(I.Amount) AS NoDeliverAmount ");
                builder.Append("I.OrderID IN (SELECT OrderID FROM PE_Orders WHERE (MoneyReceipt>=MoneyTotal or MoneyReceipt>0) AND DeliverStatus<1)");
            }
            if (!string.IsNullOrEmpty(searchType) && ((str = searchType) != null))
            {
                if (!(str == "Day"))
                {
                    if (str == "Week")
                    {
                        builder.Append(" AND DATEDIFF(ww, I.BeginDate, GETDATE()) < 1");
                    }
                    else if (str == "Month")
                    {
                        builder.Append(" AND DATEDIFF(m, I.BeginDate, GETDATE()) < 1");
                    }
                    else if (str == "ProductName")
                    {
                        builder.Append(" AND P.ProductName LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'");
                    }
                    else if (str == "InputTime")
                    {
                        builder.Append(" AND I.BeginDate = '" + DBHelper.FilterBadChar(keyword) + "'");
                    }
                }
                else
                {
                    builder.Append(" AND DATEDIFF(dd, I.BeginDate, GETDATE()) < 1");
                }
            }
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, builder.ToString());
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            IList<SaleCountInfo> list = new List<SaleCountInfo>();
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(SaleCountInfoFromrdataReader(reader, infoType));
                }
            }
            this.m_TotalOfSaleCount = DataConverter.CLng(database.GetParameterValue(storedProcCommand, "@Total"));
            return list;
        }

        public int GetTotalOfSaleCount()
        {
            return this.m_TotalOfSaleCount;
        }

        private static SaleCountInfo SaleCountInfoFromrdataReader(NullableDataReader dataReader, string infoType)
        {
            SaleCountInfo info = new SaleCountInfo();
            info.ProductId = dataReader.GetInt32("ProductID");
            info.ProductName = dataReader.GetString("ProductName");
            info.Unit = dataReader.GetString("Unit");
            info.TableName = dataReader.GetString("TableName");
            if (infoType == "NoDeliver")
            {
                info.NoDeliverAmount = dataReader.GetInt32("NoDeliverAmount");
                return info;
            }
            info.TotalAmount = dataReader.GetInt32("tAmount");
            info.SubTotal = dataReader.GetDecimal("tSubTotal");
            return info;
        }
    }
}

