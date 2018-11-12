namespace EasyOne.SqlServerDal.Shop
{
    using EasyOne.Enumerations;
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Shop;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class OrderItem : IOrderItem
    {
        public bool Add(OrderItemInfo orderItemInfo)
        {
            if (orderItemInfo.ItemId <= 0)
            {
                orderItemInfo.ItemId = DBHelper.GetMaxId("PE_OrderItem", "ItemId") + 1;
            }
            return DBHelper.ExecuteProc("PR_Shop_OrderItem_Add", GetParms(orderItemInfo));
        }

        public bool Delete(int orderId)
        {
            return DBHelper.ExecuteSql("DELETE FROM PE_OrderItem WHERE OrderId = @OrderID", new Parameters("@OrderID", DbType.Int32, orderId));
        }

        public bool ExistsPresent(int presentId)
        {
            return DBHelper.ExistsSql("SELECT TOP 1 ItemID FROM PE_OrderItem WHERE ProductID = @PresentID AND TableName is null", new Parameters("@PresentID", DbType.Int32, presentId));
        }

        public bool ExistsProduct(string tableName)
        {
            return this.ExistsProduct(tableName, 0);
        }

        public bool ExistsProduct(string tableName, int productId)
        {
            string strSql = "SELECT TOP 1 itemId FROM PE_OrderItem WHERE TableName = @TableName ";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@TableName", DbType.String, tableName);
            if (productId != 0)
            {
                cmdParams.AddInParameter("@ProductID", DbType.Int32, productId);
                strSql = strSql + "AND ProductID = @ProductID";
            }
            return (DBHelper.ExecuteScalarSql(strSql, cmdParams) != null);
        }

        public OrderItemInfo GetInfoByItemId(int itemId)
        {
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_OrderItem WHERE ItemID = @ItemID", new Parameters("@ItemID", DbType.Int32, itemId)))
            {
                if (reader.Read())
                {
                    return OrderItemFromrdr(reader);
                }
                return new OrderItemInfo(true);
            }
        }

        public IList<OrderItemInfo> GetInfoListByOrderId(int orderId)
        {
            IList<OrderItemInfo> list = new List<OrderItemInfo>();
            string strSql = "SELECT * FROM PE_OrderItem WHERE OrderID = @OrderID";
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, new Parameters("@OrderID", DbType.Int32, orderId)))
            {
                while (reader.Read())
                {
                    list.Add(OrderItemFromrdr(reader));
                }
            }
            return list;
        }

        public int GetMaxId()
        {
            return DBHelper.GetMaxId("PE_OrderItem", "ItemId");
        }

        private static Parameters GetParms(OrderItemInfo orderItemInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@ItemID", DbType.Int32, orderItemInfo.ItemId);
            parameters.AddInParameter("@OrderID", DbType.Int32, orderItemInfo.OrderId);
            parameters.AddInParameter("@ProductID", DbType.Int32, orderItemInfo.ProductId);
            parameters.AddInParameter("@TableName", DbType.String, orderItemInfo.TableName);
            parameters.AddInParameter("@Property", DbType.String, orderItemInfo.Property);
            parameters.AddInParameter("@SaleType", DbType.Int32, orderItemInfo.SaleType);
            parameters.AddInParameter("@Price_Market", DbType.Currency, orderItemInfo.PriceMarket);
            parameters.AddInParameter("@Price", DbType.Currency, orderItemInfo.Price);
            parameters.AddInParameter("@TruePrice", DbType.Currency, orderItemInfo.TruePrice);
            parameters.AddInParameter("@Amount", DbType.Int32, orderItemInfo.Amount);
            parameters.AddInParameter("@SubTotal", DbType.Currency, orderItemInfo.SubTotal);
            parameters.AddInParameter("@BeginDate", DbType.DateTime, orderItemInfo.BeginDate);
            parameters.AddInParameter("@ServiceTerm", DbType.Int32, orderItemInfo.ServiceTerm);
            parameters.AddInParameter("@Remark", DbType.String, orderItemInfo.Remark);
            parameters.AddInParameter("@PresentMoney", DbType.Currency, orderItemInfo.PresentMoney);
            parameters.AddInParameter("@PresentPoint", DbType.Int32, orderItemInfo.PresentPoint);
            parameters.AddInParameter("@PresentExp", DbType.Int32, orderItemInfo.PresentExp);
            parameters.AddInParameter("@ServiceTermUnit", DbType.Int32, orderItemInfo.ServiceTermUnit);
            parameters.AddInParameter("@ProductCharacter", DbType.Int32, orderItemInfo.ProductCharacter);
            parameters.AddInParameter("@ProductName", DbType.String, orderItemInfo.ProductName);
            parameters.AddInParameter("@Unit", DbType.String, orderItemInfo.Unit);
            parameters.AddInParameter("@Weight", DbType.Double, orderItemInfo.Weight);
            return parameters;
        }

        public IList<string> GetProductNameList(int orderId)
        {
            string strSql = "SELECT P.ProductName FROM PE_OrderItem O INNER JOIN PE_CommonProduct P ON O.ProductID=P.ProductID WHERE OrderID=@OrderID";
            IList<string> list = new List<string>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, new Parameters("@OrderID", DbType.Int32, orderId)))
            {
                while (reader.Read())
                {
                    list.Add(reader.GetString(0));
                }
            }
            return list;
        }

        private static OrderItemInfo OrderItemFromrdr(NullableDataReader rdr)
        {
            OrderItemInfo info = new OrderItemInfo();
            info.ItemId = rdr.GetInt32("ItemID");
            info.OrderId = rdr.GetInt32("OrderID");
            info.ProductId = rdr.GetInt32("ProductID");
            info.TableName = rdr.GetString("TableName");
            info.Property = rdr.GetString("Property");
            info.SaleType = rdr.GetInt32("SaleType");
            info.PriceMarket = rdr.GetDecimal("Price_Market");
            info.Price = rdr.GetDecimal("Price");
            info.TruePrice = rdr.GetDecimal("TruePrice");
            info.Amount = rdr.GetInt32("Amount");
            info.SubTotal = rdr.GetDecimal("SubTotal");
            info.BeginDate = rdr.GetDateTime("BeginDate");
            info.ServiceTerm = rdr.GetInt32("ServiceTerm");
            info.Remark = rdr.GetString("Remark");
            info.PresentMoney = rdr.GetDecimal("PresentMoney");
            info.PresentPoint = rdr.GetInt32("PresentPoint");
            info.PresentExp = rdr.GetInt32("PresentExp");
            info.ServiceTermUnit = (ServiceTermUnit) rdr.GetInt32("ServiceTermUnit");
            info.ProductCharacter = (ProductCharacter) rdr.GetInt32("ProductCharacter");
            info.ProductName = rdr.GetString("ProductName");
            info.Unit = rdr.GetString("Unit");
            info.Weight = rdr.GetDouble("Weight");
            return info;
        }

        public bool Update(OrderItemInfo orderItemInfo)
        {
            return DBHelper.ExecuteProc("PR_Shop_OrderItem_Update", GetParms(orderItemInfo));
        }
    }
}

