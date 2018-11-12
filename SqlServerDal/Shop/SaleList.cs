namespace EasyOne.SqlServerDal.Shop
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.Common;
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Shop;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    public class SaleList : ISaleList
    {
        private const string s_FromSql = " PE_CommonProduct P INNER JOIN (PE_OrderItem I INNER JOIN (PE_Orders O LEFT JOIN PE_Client C ON O.ClientID = C.ClientID) ON I.OrderID = O.OrderID) ON P.ProductID = I.ProductID AND P.TableName = I.TableName ";
        private const string s_Querysql = " (O.MoneyReceipt >= O.MoneyTotal OR O.MoneyReceipt > 0) ";
        private int totalOfSaleList;

        private static string ComplexSearch(string keyword, string filterString)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                string[] strArray = keyword.Split(new char[] { '|' });
                string str = strArray[0];
                string str2 = strArray[1];
                string str3 = strArray[2];
                string str4 = strArray[3];
                string str5 = strArray[4];
                string str6 = strArray[5];
                string str7 = strArray[6];
                string str8 = strArray[7];
                if ((!string.IsNullOrEmpty(str) && !string.IsNullOrEmpty(str2)) && ((str != "0") && (str2 != "0")))
                {
                    string str9 = filterString;
                    filterString = str9 + " AND (I.ItemID Between " + DBHelper.ToNumber(str) + " AND " + DBHelper.ToNumber(str2) + ") ";
                }
                if (!string.IsNullOrEmpty(str3) && !string.IsNullOrEmpty(str4))
                {
                    string str10 = filterString;
                    filterString = str10 + " AND (O.InputTime Between '" + str3.Replace("'", "") + "' AND '" + str4.Replace("'", "") + "') ";
                }
                if (!string.IsNullOrEmpty(str5))
                {
                    filterString = filterString + " AND O.OrderNum = '" + DBHelper.FilterBadChar(str5) + "' ";
                }
                if (!string.IsNullOrEmpty(str6))
                {
                    filterString = filterString + " AND C.ShortedForm LIKE '%" + DBHelper.FilterBadChar(str6) + "%' ";
                }
                if (!string.IsNullOrEmpty(str7))
                {
                    filterString = filterString + " AND O.UserName LIKE '%" + DBHelper.FilterBadChar(str7) + "%' ";
                }
                if (!string.IsNullOrEmpty(str8))
                {
                    filterString = filterString + " AND P.ProductName LIKE '%" + DBHelper.FilterBadChar(str8) + "%' ";
                }
            }
            return filterString;
        }

        private static SaleListInfo CreateSaleListInfo(NullableDataReader rdr)
        {
            SaleListInfo info = new SaleListInfo();
            info.SaleType = rdr.GetInt32("SaleType");
            info.ProductId = rdr.GetInt32("ProductID");
            info.OrderId = rdr.GetInt32("OrderID");
            info.ClientId = rdr.GetInt32("ClientID");
            info.ItemId = rdr.GetInt32("ItemID");
            info.ProductName = rdr.GetString("ProductName");
            info.UserName = rdr.GetString("UserName");
            info.ClientName = rdr.GetString("ClientName");
            info.Unit = rdr.GetString("Unit");
            info.PresentExp = rdr.GetInt32("PresentExp");
            info.Price = rdr.GetDecimal("Price");
            info.TruePrice = rdr.GetDecimal("TruePrice");
            info.Amount = rdr.GetInt32("Amount");
            info.OrderNum = rdr.GetString("OrderNum");
            info.InputTime = rdr.GetDateTime("InputTime");
            info.TableName = rdr.GetString("TableName");
            info.Property = rdr.GetString("Property");
            info.SubTotal = info.TruePrice * DataConverter.CDecimal(info.Amount);
            return info;
        }

        public IList<SaleListInfo> GetSaleList(int startRowIndexId, int maxNumberRows, int searchType, int field, string keyword)
        {
            IList<SaleListInfo> list = new List<SaleListInfo>();
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            string filterString = " (O.MoneyReceipt >= O.MoneyTotal OR O.MoneyReceipt > 0) ";
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, " I.ItemID ");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, " P.ProductID, P.TableName, P.ProductName, P.Unit, ISNULL(I.[Property], '') AS [Property] , I.ItemID, I.SaleType, I.PresentExp, I.Price, I.TruePrice, I.Amount, O.OrderID, O.OrderNum, O.InputTime, O.UserName, O.ClientID, ISNULL(C.ShortedForm, '') AS ClientName ");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, " DESC ");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, "");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, " PE_CommonProduct P INNER JOIN (PE_OrderItem I INNER JOIN (PE_Orders O LEFT JOIN PE_Client C ON O.ClientID = C.ClientID) ON I.OrderID = O.OrderID) ON P.ProductID = I.ProductID AND P.TableName = I.TableName ");
            switch (searchType)
            {
                case 1:
                    filterString = filterString + " AND DATEDIFF(dd, O.InputTime, GETDATE() ) < 1 ";
                    break;

                case 2:
                    filterString = filterString + " AND DATEDIFF(ww, O.InputTime, GETDATE() ) < 1 ";
                    break;

                case 3:
                    filterString = filterString + " AND DATEDIFF(m, O.InputTime, GETDATE() ) < 1 ";
                    break;

                case 4:
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        string[] strArray = keyword.Split(new char[] { '|' });
                        string str2 = filterString;
                        filterString = str2 + " AND I.ProductID = " + DBHelper.ToNumber(strArray[0]) + "AND I.TableName = '" + DBHelper.FilterBadChar(strArray[1]) + "'";
                    }
                    break;

                case 10:
                    filterString = HighLevelSearch(field, keyword, filterString);
                    break;

                case 11:
                    filterString = ComplexSearch(keyword, filterString);
                    break;
            }
            database.SetParameterValue(storedProcCommand, "@Filter", filterString);
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(CreateSaleListInfo(reader));
                }
            }
            this.totalOfSaleList = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public ArrayList GetSumSubTotalAndExp()
        {
            string strSql = "SELECT SUM(I.SubTotal) AS SubTotal, SUM(I.PresentExp) AS Exp FROM  PE_CommonProduct P INNER JOIN (PE_OrderItem I INNER JOIN (PE_Orders O LEFT JOIN PE_Client C ON O.ClientID = C.ClientID) ON I.OrderID = O.OrderID) ON P.ProductID = I.ProductID AND P.TableName = I.TableName  WHERE  (O.MoneyReceipt >= O.MoneyTotal OR O.MoneyReceipt > 0) ";
            ArrayList list = new ArrayList();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql))
            {
                while (reader.Read())
                {
                    list.Add(reader["SubTotal"]);
                    list.Add(reader["Exp"]);
                }
            }
            return list;
        }

        public int GetTotalOfSaleList()
        {
            return this.totalOfSaleList;
        }

        private static string HighLevelSearch(int field, string keyword, string filterString)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                switch (field)
                {
                    case 0:
                        filterString = filterString + " AND C.ShortedForm LIKE '%" + DBHelper.FilterBadChar(keyword) + "%' ";
                        return filterString;

                    case 1:
                        filterString = filterString + " AND O.UserName LIKE '%" + DBHelper.FilterBadChar(keyword) + "%' ";
                        return filterString;

                    case 2:
                        filterString = filterString + " AND P.ProductName LIKE '%" + DBHelper.FilterBadChar(keyword) + "%' ";
                        return filterString;

                    case 3:
                        filterString = filterString + " AND DATEDIFF(dd, O.InputTime, '" + keyword + "') = 0 ";
                        return filterString;
                }
            }
            return filterString;
        }
    }
}

