namespace EasyOne.SqlServerDal.Shop
{
    using EasyOne.Common;
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Shop;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class ProductData : IProductData
    {
        public bool Add(int productId, string tableName, ProductDataInfo dataInfo)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ProductID", DbType.Int32, productId);
            cmdParams.AddInParameter("@Stocks", DbType.Int32, dataInfo.Stocks);
            cmdParams.AddInParameter("@AlarmNum", DbType.Int32, dataInfo.AlarmNum);
            cmdParams.AddInParameter("@TableName", DbType.String, tableName);
            cmdParams.AddInParameter("@PropertyValue", DbType.String, dataInfo.PropertyValue);
            cmdParams.AddInParameter("@Price", DbType.Decimal, dataInfo.PriceInfo.Price);
            cmdParams.AddInParameter("@PriceMember", DbType.Decimal, dataInfo.PriceInfo.PriceMember);
            cmdParams.AddInParameter("@PriceAgent", DbType.Decimal, dataInfo.PriceInfo.PriceAgent);
            cmdParams.AddInParameter("@PriceWholesale1", DbType.Decimal, dataInfo.PriceInfo.PriceWholesale1);
            cmdParams.AddInParameter("@PriceWholesale2", DbType.Decimal, dataInfo.PriceInfo.PriceWholesale2);
            cmdParams.AddInParameter("@PriceWholesale3", DbType.Decimal, dataInfo.PriceInfo.PriceWholesale3);
            cmdParams.AddInParameter("@NumberWholesale1", DbType.Int32, dataInfo.PriceInfo.NumberWholesale1);
            cmdParams.AddInParameter("@NumberWholesale2", DbType.Int32, dataInfo.PriceInfo.NumberWholesale2);
            cmdParams.AddInParameter("@NumberWholesale3", DbType.Int32, dataInfo.PriceInfo.NumberWholesale3);
            cmdParams.AddInParameter("@IsValid", DbType.Boolean, dataInfo.IsValid);
            return DBHelper.ExecuteSql("INSERT INTO PE_ProductData(ProductID, TableName, Stocks, AlarmNum, PropertyValue, Price, Price_Member, Price_Agent, Price_Wholesale1, Price_Wholesale2, Price_Wholesale3, Number_Wholesale1, Number_Wholesale2, Number_Wholesale3, IsValid) VALUES(@ProductID, @TableName, @Stocks, @AlarmNum, @PropertyValue, @Price, @PriceMember, @PriceAgent, @PriceWholesale1, @PriceWholesale2, @PriceWholesale3, @NumberWholesale1, @NumberWholesale2, @NumberWholesale3, @IsValid)", cmdParams);
        }

        public bool Add(int productId, string tableName, IList<string> attrList)
        {
            bool flag = true;
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ProductID", DbType.Int32, productId);
            cmdParams.AddInParameter("@TableName", DbType.String, tableName);
            cmdParams.AddInParameter("@PropertyValue", DbType.String, string.Empty);
            foreach (string str in attrList)
            {
                cmdParams.Entries[2].Value = str;
                flag = DBHelper.ExecuteSql("INSERT INTO PE_ProductData (ProductID, TableName, PropertyValue) VALUES (@ProductID, @TableName, @PropertyValue)", cmdParams);
                if (!flag)
                {
                    this.DeleteByProduct(productId, tableName);
                    return flag;
                }
            }
            return flag;
        }

        public bool AddOrderNum(int productId, string tableName, string property, int quantity)
        {
            string strSql = "UPDATE PE_ProductData SET OrderNum = OrderNum + @Quantity WHERE productId = @ProductId AND tableName = @TableName";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@Quantity", DbType.Int32, quantity);
            cmdParams.AddInParameter("@ProductId", DbType.Int32, productId);
            cmdParams.AddInParameter("@TableName", DbType.String, tableName);
            if (!string.IsNullOrEmpty(property))
            {
                strSql = strSql + " AND PropertyValue = @PropertyValue";
                cmdParams.AddInParameter("@PropertyValue", DbType.String, property);
            }
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool AddStocks(int productId, int quantity, string propertyValue)
        {
            string strSql = "UPDATE PE_ProductData SET Stocks= ISNULL(Stocks, 0)+@Quantity WHERE productId = @ProductId";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@Quantity", DbType.Int32, quantity);
            cmdParams.AddInParameter("@ProductId", DbType.Int32, productId);
            if (!string.IsNullOrEmpty(propertyValue))
            {
                strSql = strSql + " AND PropertyValue = @PropertyValue";
                cmdParams.AddInParameter("@PropertyValue", DbType.String, propertyValue);
            }
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool DeleteById(int id)
        {
            return DBHelper.ExecuteSql("DELETE FROM PE_ProductData WHERE ID = @ID", new Parameters("@ID", DbType.Int32, id));
        }

        public bool DeleteByProduct(int productId, string tableName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ProductID", DbType.Int32, productId);
            cmdParams.AddInParameter("@TableName", DbType.String, tableName);
            return DBHelper.ExecuteSql("DELETE FROM PE_ProductData WHERE ProductID = @ProductID AND TableName = @TableName", cmdParams);
        }

        public IList<ProductDataInfo> GetListByProduct(int productId, string tableName)
        {
            IList<ProductDataInfo> list = new List<ProductDataInfo>();
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ProductID", DbType.Int32, productId);
            cmdParams.AddInParameter("@TableName", DbType.String, tableName);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_ProductData WHERE ProductID = @ProductID AND TableName = @TableName", cmdParams))
            {
                while (reader.Read())
                {
                    list.Add(ProductDataFromrdr(reader));
                }
            }
            return list;
        }

        public ProductDataInfo GetProductDataByPropertyValue(int productId, string tableName, string propertyValue)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ProductID", DbType.Int32, productId);
            cmdParams.AddInParameter("@TableName", DbType.String, tableName);
            cmdParams.AddInParameter("@PropertyValue", DbType.String, propertyValue);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_ProductData WHERE ProductID = @ProductID AND TableName = @TableName AND PropertyValue = @PropertyValue", cmdParams))
            {
                if (reader.Read())
                {
                    return ProductDataFromrdr(reader);
                }
                return new ProductDataInfo(true);
            }
        }

        public int GetStockByProperty(int productid, string propertyValue)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ProductID", DbType.Int32, productid);
            cmdParams.AddInParameter("@PropertyValue", DbType.String, propertyValue);
            return DataConverter.CLng(DBHelper.ExecuteScalarSql("SELECT TOP 1 Stocks FROM PE_ProductData WHERE ProductID = @ProductID AND PropertyValue = @PropertyValue", cmdParams));
        }

        private static ProductDataInfo ProductDataFromrdr(NullableDataReader rdr)
        {
            ProductDataInfo info = new ProductDataInfo();
            info.Id = rdr.GetInt32("ID");
            info.ProductId = rdr.GetInt32("ProductId");
            info.TableName = rdr.GetString("TableName");
            info.PropertyValue = rdr.GetString("PropertyValue");
            info.Stocks = rdr.GetInt32("Stocks");
            info.OrderNum = rdr.GetInt32("OrderNum");
            info.AlarmNum = rdr.GetInt32("AlarmNum");
            info.BuyTimes = rdr.GetInt32("BuyTimes");
            info.PriceInfo.NumberWholesale1 = rdr.GetInt32("Number_Wholesale1");
            info.PriceInfo.NumberWholesale2 = rdr.GetInt32("Number_Wholesale2");
            info.PriceInfo.NumberWholesale3 = rdr.GetInt32("Number_Wholesale3");
            info.PriceInfo.Price = rdr.GetDecimal("Price");
            info.PriceInfo.PriceAgent = rdr.GetDecimal("Price_Agent");
            info.PriceInfo.PriceMember = rdr.GetDecimal("Price_Member");
            info.PriceInfo.PriceWholesale1 = rdr.GetDecimal("Price_Wholesale1");
            info.PriceInfo.PriceWholesale2 = rdr.GetDecimal("Price_Wholesale2");
            info.PriceInfo.PriceWholesale3 = rdr.GetDecimal("Price_Wholesale3");
            info.IsValid = rdr.GetBoolean("IsValid");
            return info;
        }

        public bool Update(int productId, string tableName, ProductDataInfo info)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ProductID", DbType.Int32, productId);
            cmdParams.AddInParameter("@TableName", DbType.String, tableName);
            cmdParams.AddInParameter("@Stocks", DbType.Int32, info.Stocks);
            cmdParams.AddInParameter("@AlarmNum", DbType.Int32, info.AlarmNum);
            if (string.IsNullOrEmpty(info.PropertyValue))
            {
                return DBHelper.ExecuteSql("UPDATE PE_ProductData SET Stocks = @Stocks, AlarmNum = @AlarmNum WHERE ProductId = @ProductId AND TableName = @TableName", cmdParams);
            }
            cmdParams.AddInParameter("@PropertyValue", DbType.String, info.PropertyValue);
            cmdParams.AddInParameter("@PriceMember", DbType.Decimal, info.PriceInfo.PriceMember);
            cmdParams.AddInParameter("@PriceAgent", DbType.Decimal, info.PriceInfo.PriceAgent);
            cmdParams.AddInParameter("@PriceWholesale1", DbType.Decimal, info.PriceInfo.PriceWholesale1);
            cmdParams.AddInParameter("@PriceWholesale2", DbType.Decimal, info.PriceInfo.PriceWholesale2);
            cmdParams.AddInParameter("@PriceWholesale3", DbType.Decimal, info.PriceInfo.PriceWholesale3);
            cmdParams.AddInParameter("@NumberWholesale1", DbType.Int32, info.PriceInfo.NumberWholesale1);
            cmdParams.AddInParameter("@NumberWholesale2", DbType.Int32, info.PriceInfo.NumberWholesale2);
            cmdParams.AddInParameter("@NumberWholesale3", DbType.Int32, info.PriceInfo.NumberWholesale3);
            cmdParams.AddInParameter("@IsValid", DbType.Boolean, info.IsValid);
            return DBHelper.ExecuteSql("UPDATE PE_ProductData SET Stocks = @Stocks, AlarmNum = @AlarmNum, PropertyValue = @PropertyValue, Price_Member = @PriceMember, Price_Agent = @PriceAgent, Price_Wholesale1 = @PriceWholesale1, Price_Wholesale2 = @PriceWholesale2, Price_Wholesale3 = @PriceWholesale3, Number_Wholesale1 = @NumberWholesale1, Number_Wholesale2 = @NumberWholesale2, Number_Wholesale3 = @NumberWholesale3, IsValid = @IsValid WHERE ProductId = @ProductId AND TableName = @TableName AND PropertyValue = @PropertyValue", cmdParams);
        }
    }
}

