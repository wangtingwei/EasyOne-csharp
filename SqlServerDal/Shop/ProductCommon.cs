namespace EasyOne.SqlServerDal.Shop
{
    using EasyOne.Common;
    using EasyOne.Enumerations;
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Shop;
    using EasyOne.SqlServerDal;
    using EasyOne.SqlServerDal.Contents;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;

    public class ProductCommon : IProductCommon
    {
        private const string s_FieldParameters = "@ProductID, @TableName, @ProductName, @ProductType, @ProductPic, @ProductThumb, @Unit, @ProductNum,@ServiceTermUnit, @ServiceTerm, @Price, @Price_Market, @Price_Member, @Price_Agent, @EnableWholesale, @Price_Wholesale1, @Price_Wholesale2, @Price_Wholesale3, @Number_Wholesale1, @Number_Wholesale2, @Number_Wholesale3, @PresentID, @PresentNumber, @PresentPoint, @PresentExp, @PresentMoney, @StocksProject, @SalePromotionType, @MinNumber, @Discount, @IncludeTax, @TaxRate, @Properties, @Weight, @LimitNum, @EnableSingleSell, @DependentProducts, @ProductKind, @ProductCharacter,@EnableBuyWhenOutofstock, @Keyword, @ProducerName, @TrademarkName, @BarCode, @ProductIntro, @ProductExplain, @IsNew, @IsHot, @IsBest,@Stars, @EnableSale, @Stocks, @DownloadUrl, @Remark, @AlarmNum, @OrderNum, @Minimum";
        private const string s_Fields = "ProductID, TableName, ProductName, ProductType, ProductPic, ProductThumb, Unit, ProductNum, ServiceTermUnit, ServiceTerm, Price, Price_Market, Price_Member, Price_Agent, EnableWholesale, Price_Wholesale1, Price_Wholesale2, Price_Wholesale3, Number_Wholesale1, Number_Wholesale2, Number_Wholesale3, PresentID, PresentNumber, PresentPoint, PresentExp, PresentMoney, StocksProject, SalePromotionType, MinNumber, Discount, IncludeTax, TaxRate, Properties, Weight, LimitNum, EnableSingleSell, DependentProducts, ProductKind, ProductCharacter,EnableBuyWhenOutofstock, Keyword, ProducerName, TrademarkName, BarCode, ProductIntro, ProductExplain, IsNew, IsHot,IsBest, Stars, EnableSale, Stocks, DownloadUrl, Remark, AlarmNum, OrderNum, Minimum";

        public bool Add(ProductInfo info, string tableName)
        {
            string strSql = Query.GetInsertTableSql("PE_CommonProduct", "ProductID, TableName, ProductName, ProductType, ProductPic, ProductThumb, Unit, ProductNum, ServiceTermUnit, ServiceTerm, Price, Price_Market, Price_Member, Price_Agent, EnableWholesale, Price_Wholesale1, Price_Wholesale2, Price_Wholesale3, Number_Wholesale1, Number_Wholesale2, Number_Wholesale3, PresentID, PresentNumber, PresentPoint, PresentExp, PresentMoney, StocksProject, SalePromotionType, MinNumber, Discount, IncludeTax, TaxRate, Properties, Weight, LimitNum, EnableSingleSell, DependentProducts, ProductKind, ProductCharacter,EnableBuyWhenOutofstock, Keyword, ProducerName, TrademarkName, BarCode, ProductIntro, ProductExplain, IsNew, IsHot,IsBest, Stars, EnableSale, Stocks, DownloadUrl, Remark, AlarmNum, OrderNum, Minimum", "@ProductID, @TableName, @ProductName, @ProductType, @ProductPic, @ProductThumb, @Unit, @ProductNum,@ServiceTermUnit, @ServiceTerm, @Price, @Price_Market, @Price_Member, @Price_Agent, @EnableWholesale, @Price_Wholesale1, @Price_Wholesale2, @Price_Wholesale3, @Number_Wholesale1, @Number_Wholesale2, @Number_Wholesale3, @PresentID, @PresentNumber, @PresentPoint, @PresentExp, @PresentMoney, @StocksProject, @SalePromotionType, @MinNumber, @Discount, @IncludeTax, @TaxRate, @Properties, @Weight, @LimitNum, @EnableSingleSell, @DependentProducts, @ProductKind, @ProductCharacter,@EnableBuyWhenOutofstock, @Keyword, @ProducerName, @TrademarkName, @BarCode, @ProductIntro, @ProductExplain, @IsNew, @IsHot, @IsBest,@Stars, @EnableSale, @Stocks, @DownloadUrl, @Remark, @AlarmNum, @OrderNum, @Minimum");
            Parameters cmdParams = GetParameters(info, tableName);
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool AddBuyTimes(int productId, string tableName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@TableName", DbType.String, tableName);
            cmdParams.AddInParameter("@ProductID", DbType.Int32, productId);
            return DBHelper.ExecuteSql("UPDATE dbo.PE_CommonProduct SET BuyTimes = ISNULL(BuyTimes, 0)+1 WHERE TableName = @TableName AND ProductID = @ProductID", cmdParams);
        }

        public bool AddOrderNum(int id, int quantity)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@Quantity", DbType.Int32, quantity);
            cmdParams.AddInParameter("@ID", DbType.Int32, id);
            return DBHelper.ExecuteSql("UPDATE PE_CommonProduct SET OrderNum = ISNULL(OrderNum, 0)+@Quantity WHERE ProductID = @ID", cmdParams);
        }

        public bool AddOrderNum(int productId, string tableName, int quantity)
        {
            string strSql = "UPDATE PE_CommonProduct SET OrderNum = ISNULL(OrderNum, 0)+@Quantity WHERE productId = @ProductId AND tableName = @TableName";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@Quantity", DbType.Int32, quantity);
            cmdParams.AddInParameter("@ProductId", DbType.Int32, productId);
            cmdParams.AddInParameter("@TableName", DbType.String, tableName);
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool AddStocks(int productId, int quantity)
        {
            string strSql = "UPDATE dbo.PE_CommonProduct SET Stocks = ISNULL(Stocks, 0)+@Quantity WHERE ProductID = @ProductID";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@Quantity", DbType.Int32, quantity);
            cmdParams.AddInParameter("@ProductID", DbType.Int32, productId);
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool DeleteById(int id, string tableName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@TableName", DbType.String, tableName);
            cmdParams.AddInParameter("@ProductId", DbType.Int32, id);
            return DBHelper.ExecuteSql("DELETE FROM PE_CommonProduct WHERE TableName = @TableName AND ProductId = @ProductId", cmdParams);
        }

        public bool ExistsPresent(int presentId)
        {
            return DBHelper.ExistsSql("SELECT TOP 1 ProductID FROM PE_CommonProduct WHERE PresentID = @PresentID", new Parameters("@PresentID", DbType.Int32, presentId));
        }

        public string GetGeneralIdList(string nodeIdList, string modelIdList)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder builder2 = new StringBuilder("SELECT C.GeneralID FROM PE_CommonProduct P INNER JOIN PE_CommonModel C ON C.ItemID = P.ProductID AND C.TableName = P.TableName WHERE NodeID IN (" + DBHelper.ToValidId(nodeIdList) + ")");
            if (!string.IsNullOrEmpty(modelIdList))
            {
                builder2.Append(" AND ModelID IN (" + DBHelper.ToValidId(modelIdList) + ")");
            }
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(builder2.ToString()))
            {
                while (reader.Read())
                {
                    StringHelper.AppendString(sb, reader.GetInt32("GeneralID").ToString());
                }
            }
            return sb.ToString();
        }

        private static Parameters GetParameters(ProductInfo productInfo, string tableName)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@ProductID", DbType.Int32, productInfo.ProductId);
            parameters.AddInParameter("@TableName", DbType.String, tableName);
            parameters.AddInParameter("@ProductName", DbType.String, productInfo.ProductName);
            parameters.AddInParameter("@ProductType", DbType.Int32, productInfo.ProductType);
            parameters.AddInParameter("@ProductPic", DbType.String, productInfo.ProductPic);
            parameters.AddInParameter("@ProductThumb", DbType.String, productInfo.ProductThumb);
            parameters.AddInParameter("@Unit", DbType.String, productInfo.Unit);
            parameters.AddInParameter("@ProductNum", DbType.String, productInfo.ProductNum);
            parameters.AddInParameter("@ServiceTermUnit", DbType.Int32, productInfo.ServiceTermUnit);
            parameters.AddInParameter("@ServiceTerm", DbType.Int32, productInfo.ServiceTerm);
            parameters.AddInParameter("@Price", DbType.Currency, productInfo.PriceInfo.Price);
            parameters.AddInParameter("@Price_Market", DbType.Currency, productInfo.PriceMarket);
            parameters.AddInParameter("@Price_Member", DbType.Currency, productInfo.PriceInfo.PriceMember);
            parameters.AddInParameter("@Price_Agent", DbType.Currency, productInfo.PriceInfo.PriceAgent);
            parameters.AddInParameter("@EnableWholesale", DbType.Boolean, productInfo.EnableWholesale);
            parameters.AddInParameter("@Price_Wholesale1", DbType.Currency, productInfo.PriceInfo.PriceWholesale1);
            parameters.AddInParameter("@Price_Wholesale2", DbType.Currency, productInfo.PriceInfo.PriceWholesale2);
            parameters.AddInParameter("@Price_Wholesale3", DbType.Currency, productInfo.PriceInfo.PriceWholesale3);
            parameters.AddInParameter("@Number_Wholesale1", DbType.Int32, productInfo.PriceInfo.NumberWholesale1);
            parameters.AddInParameter("@Number_Wholesale2", DbType.Int32, productInfo.PriceInfo.NumberWholesale2);
            parameters.AddInParameter("@Number_Wholesale3", DbType.Int32, productInfo.PriceInfo.NumberWholesale3);
            parameters.AddInParameter("@PresentID", DbType.String, productInfo.PresentId);
            parameters.AddInParameter("@PresentNumber", DbType.Int32, productInfo.PresentNumber);
            parameters.AddInParameter("@PresentPoint", DbType.Int32, productInfo.PresentPoint);
            parameters.AddInParameter("@PresentExp", DbType.Int32, productInfo.PresentExp);
            parameters.AddInParameter("@PresentMoney", DbType.Currency, productInfo.PresentMoney);
            parameters.AddInParameter("@StocksProject", DbType.Int32, (int) productInfo.StocksProject);
            parameters.AddInParameter("@SalePromotionType", DbType.Int32, productInfo.SalePromotionType);
            parameters.AddInParameter("@MinNumber", DbType.Int32, productInfo.MinNumber);
            parameters.AddInParameter("@Discount", DbType.Double, productInfo.Discount);
            parameters.AddInParameter("@IncludeTax", DbType.Int32, (int) productInfo.IncludeTax);
            parameters.AddInParameter("@TaxRate", DbType.Double, productInfo.TaxRate);
            parameters.AddInParameter("@Properties", DbType.String, productInfo.Properties);
            parameters.AddInParameter("@Weight", DbType.Double, productInfo.Weight);
            parameters.AddInParameter("@LimitNum", DbType.Int32, productInfo.LimitNum);
            parameters.AddInParameter("@EnableSingleSell", DbType.Boolean, productInfo.EnableSingleSell);
            parameters.AddInParameter("@DependentProducts", DbType.String, productInfo.DependentProducts);
            parameters.AddInParameter("@ProductKind", DbType.Int32, productInfo.ProductKind);
            parameters.AddInParameter("@ProductCharacter", DbType.Int32, (int) productInfo.ProductCharacter);
            parameters.AddInParameter("@EnableBuyWhenOutofstock", DbType.Boolean, productInfo.EnableBuyWhenOutofstock);
            parameters.AddInParameter("@Keyword", DbType.String, productInfo.Keyword);
            parameters.AddInParameter("@ProductIntro", DbType.String, productInfo.ProductIntro);
            parameters.AddInParameter("@ProducerName", DbType.String, productInfo.ProducerName);
            parameters.AddInParameter("@ProductExplain", DbType.String, productInfo.ProductExplain);
            parameters.AddInParameter("@TrademarkName", DbType.String, productInfo.TrademarkName);
            parameters.AddInParameter("@BarCode", DbType.String, productInfo.BarCode);
            parameters.AddInParameter("@Stars", DbType.String, productInfo.Stars);
            parameters.AddInParameter("@IsNew", DbType.Boolean, productInfo.IsNew);
            parameters.AddInParameter("@IsHot", DbType.Boolean, productInfo.IsHot);
            parameters.AddInParameter("@IsBest", DbType.Boolean, productInfo.IsBest);
            parameters.AddInParameter("@EnableSale", DbType.Boolean, productInfo.EnableSale);
            parameters.AddInParameter("@Stocks", DbType.Int32, productInfo.Stocks);
            parameters.AddInParameter("@DownloadUrl", DbType.String, productInfo.DownloadUrl);
            parameters.AddInParameter("@Remark", DbType.String, productInfo.Remark);
            parameters.AddInParameter("@AlarmNum", DbType.Int32, productInfo.AlarmNum);
            parameters.AddInParameter("@OrderNum", DbType.Int32, productInfo.OrderNum);
            parameters.AddInParameter("@Minimum", DbType.Int32, productInfo.Minimum);
            return parameters;
        }

        public IList<ProductInfo> GetProductCommonListByCharacter(ProductCharacter productCharacter)
        {
            IList<ProductInfo> list = new List<ProductInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT P.* FROM PE_CommonProduct P INNER JOIN PE_CommonModel M on P.TableName = M.TableName AND P.ProductID = M.ItemID AND M.LinkType = 0 AND Status<>-3 Where (P.ProductCharacter&@ProductCharacter) > 0 AND P.ProductType<>" + ProductType.Promotion.ToString("D"), new Parameters("@ProductCharacter", DbType.Int32, (int) productCharacter)))
            {
                while (reader.Read())
                {
                    list.Add(ProductFromrdr(reader));
                }
            }
            return list;
        }

        public ProductInfo GetProductInfoByType(int productId, string tableName, ProductType productType)
        {
            string strSql = string.Format("SELECT TOP 1 * FROM PE_CommonProduct WHERE ProductId = @ProductId AND TableName = @TableName AND ProductType = @ProductType", new object[0]);
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@ProductId", DbType.Int32, productId);
            parameters.AddInParameter("@TableName", DbType.String, tableName);
            parameters.AddInParameter("@ProductType", DbType.Int32, (int) productType);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql))
            {
                if (reader.Read())
                {
                    ProductInfo info = ProductFromrdr(reader);
                    info.TableName = tableName;
                    return info;
                }
                return new ProductInfo(true);
            }
        }

        public string GetProductName(int id)
        {
            string str = "";
            string strSql = "SELECT ProductName FROM PE_CommonProduct WHERE ProductID = @ID";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ID", DbType.String, id);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, cmdParams))
            {
                if (reader.Read())
                {
                    str = reader.GetString("ProductName");
                }
            }
            return str;
        }

        public IList<string> GetUnitList()
        {
            IList<string> list = new List<string>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT DISTINCT Unit FROM PE_CommonProduct"))
            {
                while (reader.Read())
                {
                    list.Add(reader.GetString("Unit"));
                }
            }
            return list;
        }

        public bool IsExistSameProductNum(string productNum)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ProductNum", DbType.String, productNum);
            return DBHelper.ExecuteSql("SELECT ProductID FROM PE_CommonProduct WHERE ProductNum = @ProductNum", cmdParams);
        }

        private static ProductInfo ProductFromrdr(NullableDataReader rdr)
        {
            ProductInfo info = new ProductInfo();
            info.ProductName = rdr.GetString("ProductName");
            info.ProductId = rdr.GetInt32("ProductID");
            info.ProductType = (ProductType) rdr.GetInt32("ProductType");
            info.ProductPic = rdr.GetString("ProductPic");
            info.ProductThumb = rdr.GetString("ProductThumb");
            info.Unit = rdr.GetString("Unit");
            info.ProductNum = rdr.GetString("ProductNum");
            info.ServiceTermUnit = (ServiceTermUnit) rdr.GetInt32("ServiceTermUnit");
            info.ServiceTerm = rdr.GetInt32("ServiceTerm");
            info.PriceInfo.Price = rdr.GetDecimal("Price");
            info.PriceMarket = rdr.GetDecimal("Price_Market");
            info.PriceInfo.PriceMember = rdr.GetDecimal("Price_Member");
            info.PriceInfo.PriceAgent = rdr.GetDecimal("Price_Agent");
            info.EnableWholesale = rdr.GetBoolean("EnableWholesale");
            info.PriceInfo.PriceWholesale1 = rdr.GetDecimal("Price_Wholesale1");
            info.PriceInfo.PriceWholesale2 = rdr.GetDecimal("Price_Wholesale2");
            info.PriceInfo.PriceWholesale3 = rdr.GetDecimal("Price_Wholesale3");
            info.PriceInfo.NumberWholesale1 = rdr.GetInt32("Number_Wholesale1");
            info.PriceInfo.NumberWholesale2 = rdr.GetInt32("Number_Wholesale2");
            info.PriceInfo.NumberWholesale3 = rdr.GetInt32("Number_Wholesale3");
            info.PresentId = rdr.GetInt32("PresentID");
            info.PresentNumber = rdr.GetInt32("PresentNumber");
            info.PresentPoint = rdr.GetInt32("PresentPoint");
            info.PresentExp = rdr.GetInt32("PresentExp");
            info.PresentMoney = rdr.GetDecimal("PresentMoney");
            info.StocksProject = (StocksProject) rdr.GetInt32("StocksProject");
            info.SalePromotionType = rdr.GetInt32("SalePromotionType");
            info.MinNumber = rdr.GetInt32("MinNumber");
            info.Discount = rdr.GetDouble("Discount");
            info.IncludeTax = (TaxRateType) rdr.GetInt32("IncludeTax");
            info.TaxRate = rdr.GetDouble("TaxRate");
            info.Properties = rdr.GetString("Properties");
            info.Weight = rdr.GetDouble("Weight");
            info.LimitNum = rdr.GetInt32("LimitNum");
            info.EnableSingleSell = rdr.GetBoolean("EnableSingleSell");
            info.DependentProducts = rdr.GetString("DependentProducts");
            info.ProductKind = (ProductKind) rdr.GetInt32("ProductKind");
            info.ProductCharacter = (ProductCharacter) rdr.GetInt32("ProductCharacter");
            info.EnableBuyWhenOutofstock = rdr.GetBoolean("EnableBuyWhenOutofstock");
            info.ProductExplain = rdr.GetString("ProductExplain");
            info.ProducerName = rdr.GetString("ProducerName");
            info.BarCode = rdr.GetString("BarCode");
            info.Stars = rdr.GetInt32("Stars");
            info.TrademarkName = rdr.GetString("TrademarkName");
            info.Keyword = rdr.GetString("Keyword");
            info.ProductIntro = rdr.GetString("ProductIntro");
            info.IsNew = rdr.GetBoolean("IsNew");
            info.IsHot = rdr.GetBoolean("IsHot");
            info.IsBest = rdr.GetBoolean("IsBest");
            info.EnableSale = rdr.GetBoolean("EnableSale");
            info.TableName = rdr.GetString("TableName");
            info.Stocks = rdr.GetInt32("Stocks");
            info.DownloadUrl = rdr.GetString("DownloadUrl");
            info.Remark = rdr.GetString("Remark");
            info.AlarmNum = rdr.GetInt32("AlarmNum");
            info.OrderNum = rdr.GetInt32("OrderNum");
            info.Minimum = rdr.GetInt32("Minimum");
            return info;
        }

        public bool Update(ProductInfo info, string tableName)
        {
            StringBuilder builder = new StringBuilder(0x80);
            builder.Append("UPDATE ");
            builder.Append("PE_CommonProduct");
            builder.Append(" SET ");
            string[] strArray = "ProductID, TableName, ProductName, ProductType, ProductPic, ProductThumb, Unit, ProductNum, ServiceTermUnit, ServiceTerm, Price, Price_Market, Price_Member, Price_Agent, EnableWholesale, Price_Wholesale1, Price_Wholesale2, Price_Wholesale3, Number_Wholesale1, Number_Wholesale2, Number_Wholesale3, PresentID, PresentNumber, PresentPoint, PresentExp, PresentMoney, StocksProject, SalePromotionType, MinNumber, Discount, IncludeTax, TaxRate, Properties, Weight, LimitNum, EnableSingleSell, DependentProducts, ProductKind, ProductCharacter,EnableBuyWhenOutofstock, Keyword, ProducerName, TrademarkName, BarCode, ProductIntro, ProductExplain, IsNew, IsHot,IsBest, Stars, EnableSale, Stocks, DownloadUrl, Remark, AlarmNum, OrderNum, Minimum".Split(new char[] { ',' });
            string[] strArray2 = "@ProductID, @TableName, @ProductName, @ProductType, @ProductPic, @ProductThumb, @Unit, @ProductNum,@ServiceTermUnit, @ServiceTerm, @Price, @Price_Market, @Price_Member, @Price_Agent, @EnableWholesale, @Price_Wholesale1, @Price_Wholesale2, @Price_Wholesale3, @Number_Wholesale1, @Number_Wholesale2, @Number_Wholesale3, @PresentID, @PresentNumber, @PresentPoint, @PresentExp, @PresentMoney, @StocksProject, @SalePromotionType, @MinNumber, @Discount, @IncludeTax, @TaxRate, @Properties, @Weight, @LimitNum, @EnableSingleSell, @DependentProducts, @ProductKind, @ProductCharacter,@EnableBuyWhenOutofstock, @Keyword, @ProducerName, @TrademarkName, @BarCode, @ProductIntro, @ProductExplain, @IsNew, @IsHot, @IsBest,@Stars, @EnableSale, @Stocks, @DownloadUrl, @Remark, @AlarmNum, @OrderNum, @Minimum".Split(new char[] { ',' });
            for (int i = 0; i < strArray.Length; i++)
            {
                builder.Append(strArray[i]);
                builder.Append("=");
                builder.Append(strArray2[i]);
                builder.Append(",");
            }
            builder.Remove(builder.Length - 1, 1);
            builder.Append(" WHERE ProductId = @ProductId AND TableName = @TableName");
            return DBHelper.ExecuteSql(builder.ToString(), GetParameters(info, tableName));
        }
    }
}

