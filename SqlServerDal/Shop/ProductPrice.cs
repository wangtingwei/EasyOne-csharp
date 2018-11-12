namespace EasyOne.SqlServerDal.Shop
{
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Shop;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class ProductPrice : IProductPrice
    {
        public bool Add(int productId, string tableName, ProductPriceInfo priceInfo)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@TableName", DbType.String, tableName);
            cmdParams.AddInParameter("@ProductID", DbType.Int32, productId);
            cmdParams.AddInParameter("@GroupID", DbType.Int32, priceInfo.GroupId);
            cmdParams.AddInParameter("@Price", DbType.Currency, priceInfo.Price);
            cmdParams.AddInParameter("@PropertyValue", DbType.String, priceInfo.PropertyValue);
            return DBHelper.ExecuteSql("INSERT INTO PE_ProductPrice(TableName, ProductID, GroupID, Price, PropertyValue) VALUES (@TableName, @ProductID, @GroupID, @Price, @PropertyValue)", cmdParams);
        }

        public bool Delete(int id)
        {
            return DBHelper.ExecuteSql("DELETE FROM PE_ProductPrice WHERE ID = @ID", new Parameters("@ID", DbType.Int32, id));
        }

        public bool Delete(int productId, string tableName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@TableName", DbType.String, tableName);
            cmdParams.AddInParameter("@ProductId", DbType.Int32, productId);
            return DBHelper.ExecuteSql("DELETE FROM PE_ProductPrice WHERE TableName = @TableName AND ProductId = @ProductId", cmdParams);
        }

        public IList<ProductPriceInfo> GetProductPrice(int productId, string tableName, string property)
        {
            IList<ProductPriceInfo> list = new List<ProductPriceInfo>();
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@TableName", DbType.String, tableName);
            cmdParams.AddInParameter("@ProductId", DbType.Int32, productId);
            cmdParams.AddInParameter("@Property", DbType.String, property);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT ID, GroupID, Price, PropertyValue FROM PE_ProductPrice WHERE TableName = @TableName AND ProductId = @ProductId AND PropertyValue = @Property", cmdParams))
            {
                while (reader.Read())
                {
                    list.Add(ProductPriceFromrdr(reader));
                }
            }
            return list;
        }

        public IList<ProductPriceInfo> GetProductPriceById(int productId, string tableName)
        {
            IList<ProductPriceInfo> list = new List<ProductPriceInfo>();
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@TableName", DbType.String, tableName);
            cmdParams.AddInParameter("@ProductId", DbType.Int32, productId);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT ID, GroupID, Price, PropertyValue FROM PE_ProductPrice WHERE TableName = @TableName AND ProductId = @ProductId", cmdParams))
            {
                while (reader.Read())
                {
                    list.Add(ProductPriceFromrdr(reader));
                }
            }
            return list;
        }

        public ProductPriceInfo GetProductPriceInfo(int productId, string tableName, int groupId, string property)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@TableName", DbType.String, tableName);
            cmdParams.AddInParameter("@ProductId", DbType.Int32, productId);
            cmdParams.AddInParameter("@GroupId", DbType.Int32, groupId);
            string str = "SELECT TOP 1 * FROM PE_ProductPrice WHERE tablename = @TableName AND productId = @ProductId";
            if (!string.IsNullOrEmpty(property))
            {
                cmdParams.AddInParameter("@Property", DbType.String, property);
                str = str + " AND PropertyValue = @Property";
            }
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(str + " AND groupId = @GroupId", cmdParams))
            {
                if (reader.Read())
                {
                    return ProductPriceFromrdr(reader);
                }
                return new ProductPriceInfo(true);
            }
        }

        private static ProductPriceInfo ProductPriceFromrdr(NullableDataReader rdr)
        {
            ProductPriceInfo info = new ProductPriceInfo();
            info.Id = rdr.GetInt32("ID");
            info.GroupId = rdr.GetInt32("GroupID");
            info.Price = rdr.GetDecimal("Price");
            info.PropertyValue = rdr.GetString("PropertyValue");
            return info;
        }

        public bool Update(int productId, string tableName, ProductPriceInfo productPriceInfo)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@TableName", DbType.String, tableName);
            cmdParams.AddInParameter("@ProductID", DbType.Int32, productId);
            cmdParams.AddInParameter("@GroupID", DbType.Int32, productPriceInfo.GroupId);
            cmdParams.AddInParameter("@Price", DbType.Currency, productPriceInfo.Price);
            cmdParams.AddInParameter("@PropertyValue", DbType.String, productPriceInfo.PropertyValue);
            return DBHelper.ExecuteProc("PR_Shop_ProductPrice_Update", cmdParams);
        }
    }
}

