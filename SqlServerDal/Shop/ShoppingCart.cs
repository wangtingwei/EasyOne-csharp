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

    public class ShoppingCart : IShoppingCart
    {
        private int m_TotalOfShoppingCart;

        public bool Add(ShoppingCartInfo shoppingcartinfo)
        {
            Parameters cmdParams = GetParameters(shoppingcartinfo);
            return DBHelper.ExecuteProc("PR_Shop_ShoppingCarts_Add", cmdParams);
        }

        public bool Delete(DateTime datePart)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@DatePart", DbType.DateTime, datePart);
            return DBHelper.ExecuteSql("DELETE FROM PE_ShoppingCarts WHERE UpdateTime < @DatePart", cmdParams);
        }

        public bool Delete(string cartId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@CartId", DbType.String, cartId);
            return DBHelper.ExecuteSql("DELETE FROM PE_ShoppingCarts WHERE CartID = @CartId", cmdParams);
        }

        public IList<ShoppingCartInfo> GetInfoByCart(string cartId, bool isPresent)
        {
            IList<ShoppingCartInfo> list = new List<ShoppingCartInfo>();
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@CartId", DbType.String, cartId);
            cmdParams.AddInParameter("@IsPresent", DbType.Boolean, isPresent);
            string strSql = "SELECT * FROM PE_ShoppingCarts WHERE CartID = @CartId AND IsPresent = @IsPresent ORDER BY CartItemID DESC";
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, cmdParams))
            {
                while (reader.Read())
                {
                    list.Add(ShoppingCartFromrdr(reader, false));
                }
            }
            return list;
        }

        public IList<ShoppingCartInfo> GetList(int startRowIndexId, int maxNumberRows, int searchType, string keyword)
        {
            Database database = DatabaseFactory.CreateDatabase();
            IList<ShoppingCartInfo> list = new List<ShoppingCartInfo>();
            DbCommand storedProcCommand = null;
            if (searchType == 4)
            {
                storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
                database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "CartItemID");
                database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "C.*, P.*");
            }
            else
            {
                storedProcCommand = database.GetStoredProcCommand("PR_Shop_ShoppingCart_GetList");
                database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "CartID");
                database.AddInParameter(storedProcCommand, "@SortColumnDbType", DbType.String, "varchar(100)");
                database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "CartID, SUM(C.Quantity * P.Price) AS TotalMoney, SUM(Quantity) AS Quantity, Max(UserName) AS UserName, Max(UpdateTime) AS UpdateTime, Max(C.InformResult) AS InformResult");
            }
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String);
            switch (searchType)
            {
                case 0:
                    database.SetParameterValue(storedProcCommand, "@Filter", "");
                    break;

                case 1:
                    database.SetParameterValue(storedProcCommand, "@Filter", "DATEDIFF(dd, UpdateTime, GETDATE()) < 1");
                    break;

                case 2:
                    database.SetParameterValue(storedProcCommand, "@Filter", "DATEDIFF(ww, UpdateTime, GETDATE()) < 1");
                    break;

                case 3:
                    database.SetParameterValue(storedProcCommand, "@Filter", "DATEDIFF(m, UpdateTime, GETDATE()) < 1");
                    break;

                case 4:
                    database.SetParameterValue(storedProcCommand, "@Filter", "CartID = '" + DBHelper.FilterBadChar(keyword) + "' AND C.IsPresent = 0");
                    break;

                case 10:
                    database.SetParameterValue(storedProcCommand, "@Filter", "UserName IS NOT NULL AND UserName!=''");
                    break;
            }
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_ShoppingCarts C INNER JOIN PE_CommonProduct P ON C.ProductID = P.ProductID AND C.TableName = P.TableName");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    if (searchType != 4)
                    {
                        ShoppingCartInfo item = new ShoppingCartInfo();
                        item.CartId = reader.GetString("CartId");
                        item.UserName = reader.GetString("UserName");
                        item.Quantity = reader.GetInt32("Quantity");
                        item.UpdateTime = reader.GetDateTime("UpdateTime");
                        item.TotalMoney = reader.GetDecimal("TotalMoney");
                        item.InformResult = reader.GetInt32("InformResult");
                        list.Add(item);
                    }
                    else
                    {
                        list.Add(ShoppingCartFromrdr(reader, true));
                    }
                }
            }
            this.m_TotalOfShoppingCart = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        private static Parameters GetParameters(ShoppingCartInfo shoppingCartInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@UserName", DbType.String, shoppingCartInfo.UserName);
            parameters.AddInParameter("@CartID", DbType.String, shoppingCartInfo.CartId);
            parameters.AddInParameter("@ProductID", DbType.Int32, shoppingCartInfo.ProductId);
            parameters.AddInParameter("@Quantity", DbType.Int32, shoppingCartInfo.Quantity);
            parameters.AddInParameter("@IsPresent", DbType.Boolean, shoppingCartInfo.IsPresent);
            parameters.AddInParameter("@UpdateTime", DbType.DateTime, shoppingCartInfo.UpdateTime);
            parameters.AddInParameter("@TableName", DbType.String, shoppingCartInfo.TableName);
            parameters.AddInParameter("@Property", DbType.String, shoppingCartInfo.Property);
            return parameters;
        }

        public int GetTotalOfShoppingCart()
        {
            return this.m_TotalOfShoppingCart;
        }

        private static ShoppingCartInfo ShoppingCartFromrdr(NullableDataReader rdr, bool isDetail)
        {
            ShoppingCartInfo info = new ShoppingCartInfo();
            info.UserName = rdr.GetString("UserName");
            info.CartId = rdr.GetString("CartID");
            info.ProductId = rdr.GetInt32("ProductID");
            info.Quantity = rdr.GetInt32("Quantity");
            info.IsPresent = rdr.GetBoolean("IsPresent");
            info.UpdateTime = rdr.GetDateTime("UpdateTime");
            info.TableName = rdr.GetString("TableName");
            info.Property = rdr.GetString("Property");
            info.InformResult = rdr.GetInt32("InformResult");
            if (isDetail)
            {
                info.ProductInfomation.ProductName = rdr.GetString("ProductName");
                info.ProductInfomation.ProductType = (ProductType) rdr.GetInt32("ProductType");
                info.ProductInfomation.ProductPic = rdr.GetString("ProductPic");
                info.ProductInfomation.ProductThumb = rdr.GetString("ProductThumb");
                info.ProductInfomation.Unit = rdr.GetString("Unit");
                info.ProductInfomation.ServiceTermUnit = (ServiceTermUnit) rdr.GetInt32("ServiceTermUnit");
                info.ProductInfomation.ServiceTerm = rdr.GetInt32("ServiceTerm");
                info.ProductInfomation.PriceInfo.Price = rdr.GetDecimal("Price");
                info.ProductInfomation.PriceMarket = rdr.GetDecimal("Price_Market");
                info.ProductInfomation.PriceInfo.PriceMember = rdr.GetDecimal("Price_Member");
                info.ProductInfomation.PriceInfo.PriceAgent = rdr.GetDecimal("Price_Agent");
                info.ProductInfomation.EnableWholesale = rdr.GetBoolean("EnableWholesale");
                info.ProductInfomation.PriceInfo.PriceWholesale1 = rdr.GetDecimal("Price_Wholesale1");
                info.ProductInfomation.PriceInfo.PriceWholesale2 = rdr.GetDecimal("Price_Wholesale2");
                info.ProductInfomation.PriceInfo.PriceWholesale3 = rdr.GetDecimal("Price_Wholesale3");
                info.ProductInfomation.PriceInfo.NumberWholesale1 = rdr.GetInt32("Number_Wholesale1");
                info.ProductInfomation.PriceInfo.NumberWholesale2 = rdr.GetInt32("Number_Wholesale2");
                info.ProductInfomation.PriceInfo.NumberWholesale3 = rdr.GetInt32("Number_Wholesale3");
                info.ProductInfomation.PresentId = rdr.GetInt32("PresentID");
                info.ProductInfomation.PresentNumber = rdr.GetInt32("PresentNumber");
                info.ProductInfomation.PresentPoint = rdr.GetInt32("PresentPoint");
                info.ProductInfomation.PresentExp = rdr.GetInt32("PresentExp");
                info.ProductInfomation.PresentMoney = rdr.GetDecimal("PresentMoney");
                info.ProductInfomation.StocksProject = (StocksProject) rdr.GetInt32("StocksProject");
                info.ProductInfomation.SalePromotionType = rdr.GetInt32("SalePromotionType");
                info.ProductInfomation.MinNumber = rdr.GetInt32("MinNumber");
                info.ProductInfomation.Discount = rdr.GetDouble("Discount");
                info.ProductInfomation.IncludeTax = (TaxRateType) rdr.GetInt32("IncludeTax");
                info.ProductInfomation.TaxRate = rdr.GetDouble("TaxRate");
                info.ProductInfomation.Properties = rdr.GetString("Properties");
                info.ProductInfomation.Weight = rdr.GetDouble("Weight");
                info.ProductInfomation.LimitNum = rdr.GetInt32("LimitNum");
                info.ProductInfomation.EnableSingleSell = rdr.GetBoolean("EnableSingleSell");
                info.ProductInfomation.DependentProducts = rdr.GetString("DependentProducts");
                info.ProductInfomation.ProductKind = (ProductKind) rdr.GetInt32("ProductKind");
                info.ProductInfomation.TableName = rdr.GetString("TableName");
                info.ProductInfomation.ProductId = rdr.GetInt32("ProductID");
                info.ProductInfomation.Stocks = rdr.GetInt32("Stocks");
                info.ProductInfomation.OrderNum = rdr.GetInt32("OrderNum");
            }
            return info;
        }

        public void UpdateInformState(string cartId, int state)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@CartID", DbType.String, cartId);
            cmdParams.AddInParameter("@State", DbType.Int32, state);
            DBHelper.ExecuteSql("UPDATE PE_ShoppingCarts SET InformResult = @State WHERE CartID = @CartID", cmdParams);
        }

        public void UpdateUserName(string cartId, string userName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@UserName", DbType.String, userName);
            cmdParams.AddInParameter("@CartID", DbType.String, cartId);
            DBHelper.ExecuteSql("UPDATE PE_ShoppingCarts SET UserName = @UserName WHERE CartID = @CartId", cmdParams);
        }
    }
}

