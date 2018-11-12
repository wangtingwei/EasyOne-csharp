namespace EasyOne.SqlServerDal.Shop
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.Common;
    using EasyOne.Enumerations;
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Contents;
    using EasyOne.Model.Shop;
    using EasyOne.SqlServerDal;
    using EasyOne.SqlServerDal.Contents;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Runtime.InteropServices;
    using System.Text;

    public class Product : IProduct
    {
        private int m_TotalOfAllProducts;
        private int m_TotalOfProducts;

        public bool Add(string tableName, ProductInfo info)
        {
            DataRow[] dataRows = Query.GetDataRows(info.Fields, "FieldLevel = 1");
            string filedSting = Query.GetFiledSting(dataRows);
            filedSting = filedSting + (string.IsNullOrEmpty(filedSting) ? "ID" : ", ID");
            string parametersString = Query.GetParametersString(dataRows);
            parametersString = parametersString + (string.IsNullOrEmpty(parametersString) ? "@ID" : ", @ID");
            string strSql = Query.GetInsertTableSql(tableName, filedSting, parametersString);
            Parameters cmdParams = GetParameters(info);
            cmdParams.AddInParameter("@ID", DbType.Int32, info.ProductId);
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool Delete(string generalIdList)
        {
            string[] strArray = generalIdList.Split(new char[] { ',' });
            ContentManage manage = new ContentManage();
            ProductCommon common = new ProductCommon();
            ProductData data = new ProductData();
            ProductPrice price = new ProductPrice();
            for (int i = 0; i < strArray.Length; i++)
            {
                CommonModelInfo commonModelInfoById = manage.GetCommonModelInfoById(DataConverter.CLng(strArray[i]));
                if (!commonModelInfoById.IsNull && (DataConverter.CLng(commonModelInfoById.LinkType) == 0))
                {
                    manage.DeleteVirtualContent(DataConverter.CLng(strArray[i]));
                    DBHelper.ExecuteSql("DELETE FROM " + DBHelper.FilterBadChar(commonModelInfoById.TableName) + " WHERE ID = " + commonModelInfoById.ItemId.ToString());
                    common.DeleteById(commonModelInfoById.ItemId, commonModelInfoById.TableName);
                    data.DeleteByProduct(commonModelInfoById.ItemId, commonModelInfoById.TableName);
                    price.Delete(commonModelInfoById.ItemId, commonModelInfoById.TableName);
                }
            }
            return DBHelper.ExecuteSql("DELETE FROM PE_CommonModel WHERE GeneralId IN(" + DBHelper.ToValidId(generalIdList) + ")");
        }

        public int GetGeneralId(string tableName, int productId)
        {
            string strSql = "SELECT TOP 1 generalid FROM PE_CommonModel WHERE tableName = @TableName AND itemid = @ProductId AND linktype = 0";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@TableName", DbType.String, tableName);
            cmdParams.AddInParameter("@ProductId", DbType.Int32, productId);
            return DataConverter.CLng(DBHelper.ExecuteScalarSql(strSql, cmdParams));
        }

        public IDictionary<int, string> GetListByNodeIdAndTrademark(int nodeId, string trademarkName)
        {
            string sql = "SELECT DISTINCT M.GeneralId, P.ProductName FROM PE_CommonModel M INNER JOIN PE_CommonProduct\r\nP ON M.ItemId = P.ProductId WHERE M.status = 99 AND P.EnableSale = 1 AND P.Stocks > 0 AND M.NodeID=@NodeID ";
            Parameters parms = new Parameters();
            parms.AddInParameter("@NodeID", DbType.Int32, nodeId);
            if (!string.IsNullOrEmpty(trademarkName))
            {
                sql = sql + "AND TrademarkName=@TrademarkName";
                parms.AddInParameter("@TrademarkName", DbType.String, trademarkName);
            }
            return GetProductDictionary(sql, parms);
        }

        public int GetNewProductId()
        {
            return (DBHelper.GetMaxId("PE_CommonModel", "GeneralId") + 1);
        }

        private static Parameters GetParameters(ProductInfo productInfo)
        {
            Parameters parameters = new Parameters();
            foreach (Parameter parameter in Query.GetParameters(productInfo.Fields, "FieldLevel = 1").Entries)
            {
                parameters.AddInParameter(parameter.Name, parameter.DBType, parameter.Value);
            }
            return parameters;
        }

        public ProductInfo GetProductById(int id)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ID", DbType.Int32, id);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * From PE_CommonProduct WHERE ProductID = @ID", cmdParams))
            {
                if (reader.Read())
                {
                    ProductInfo productInfo = new ProductInfo();
                    ProductFromrdr<ProductInfo>(reader, productInfo);
                    return productInfo;
                }
                return new ProductInfo(true);
            }
        }

        public ProductInfo GetProductById(int productId, string tableName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@TableName", DbType.String, tableName);
            cmdParams.AddInParameter("@ProductId", DbType.Int32, productId);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * From PE_CommonProduct WHERE TableName = @TableName AND ProductId = @ProductId", cmdParams))
            {
                if (reader.Read())
                {
                    ProductInfo productInfo = new ProductInfo();
                    ProductFromrdr<ProductInfo>(reader, productInfo);
                    return productInfo;
                }
                return new ProductInfo(true);
            }
        }

        public ProductInfo GetProductById(int generalId, int productId, string tableName)
        {
            ProductInfo productById = this.GetProductById(productId, tableName);
            if (!productById.IsNull)
            {
                DataTable table = DBHelper.ExecuteDataSetSql("SELECT C.*, T.*, C.ItemId AS InfoId, C.ItemId AS SpecialId FROM PE_CommonModel C INNER JOIN " + DBHelper.FilterBadChar(tableName) + " T ON C.ItemID = T.ID WHERE GeneralId = @GeneralId", new Parameters("@GeneralId", DbType.Int32, generalId)).Tables[0];
                productById.Fields = table;
            }
            return productById;
        }

        public ProductDetailInfo GetProductDetailInfoById(int id)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ID", DbType.Int32, id);
            string strSql = "SELECT N.NodeName, M.EliteLevel, M.CreateTime, M.UpdateTime, M.NodeId, M.GeneralId, M.ModelId, M.LinkType, P.* \r\n                           FROM PE_CommonProduct P INNER JOIN (PE_CommonModel M RIGHT JOIN PE_Nodes N ON M.NodeID= N.NodeID) \r\n                                ON P.ProductID = M.ItemID WHERE M.GeneralID=@ID";
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, cmdParams))
            {
                if (reader.Read())
                {
                    ProductDetailInfo productInfo = new ProductDetailInfo();
                    ProductFromrdr<ProductDetailInfo>(reader, productInfo);
                    productInfo.EliteLevel = reader.GetInt32("EliteLevel");
                    productInfo.UpdateTime = new DateTime?(reader.GetDateTime("UpdateTime"));
                    productInfo.CreateTime = reader.GetNullableDateTime("CreateTime");
                    productInfo.NodeName = reader.GetString("NodeName");
                    productInfo.NodeId = reader.GetInt32("NodeId");
                    productInfo.GeneralId = reader.GetInt32("GeneralId");
                    productInfo.ModelId = reader.GetInt32("ModelId");
                    productInfo.LinkType = reader.GetInt32("LinkType");
                    return productInfo;
                }
                return new ProductDetailInfo();
            }
        }

        private static IDictionary<int, string> GetProductDictionary(string sql, Parameters parms)
        {
            IDictionary<int, string> dictionary = new Dictionary<int, string>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(sql, parms))
            {
                while (reader.Read())
                {
                    dictionary.Add(reader.GetInt32(0), reader.GetString(1));
                }
            }
            return dictionary;
        }

        public IList<ProductInfo> GetProductInfoList(int startRowIndexId, int maxNumberRows, string tableName, string searchProductName, string productType)
        {
            IList<ProductInfo> list = new List<ProductInfo>();
            Database database = DatabaseFactory.CreateDatabase();
            StringBuilder builder = new StringBuilder(" 1 = 1 ");
            if (!string.IsNullOrEmpty(tableName))
            {
                builder.Append(" AND P.TableName = '" + DBHelper.FilterBadChar(tableName) + "' ");
            }
            builder.Append(" AND P.EnableSale = 1 AND P.ProductType IN (" + DBHelper.ToValidId(productType) + ") ");
            if (!string.IsNullOrEmpty(searchProductName))
            {
                builder.Append(" AND P.ProductName LIKE '%" + DBHelper.FilterBadChar(searchProductName) + "%'");
            }
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "P.ProductID");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "P.*");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_CommonProduct P INNER JOIN PE_CommonModel M ON P.TableName = M.TableName AND P.ProductID = M.ItemID AND M.LinkType = 0 AND Status = 99");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, builder.ToString());
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    ProductInfo productInfo = new ProductInfo();
                    ProductFromrdr<ProductInfo>(reader, productInfo);
                    list.Add(productInfo);
                }
            }
            this.m_TotalOfProducts = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public IDictionary<int, string> GetProductList(int modelId)
        {
            string sql = "SELECT P.ProductId, P.ProductName FROM PE_CommonModel M INNER JOIN PE_CommonProduct P ON M.ItemId = P.ProductId WHERE M.ModelId = @ModelId AND M.status = 99 AND M.linkType = 0 AND P.EnableSale = 1";
            return GetProductDictionary(sql, new Parameters("@ModelId", DbType.Int32, modelId));
        }

        public IDictionary<int, string> GetProductList(int nodeId, int modelId)
        {
            string sql = "SELECT P.ProductId, P.ProductName FROM PE_CommonModel M INNER JOIN PE_CommonProduct P ON M.ItemId = P.ProductId WHERE M.NodeId = @NodeId AND M.ModelId = @ModelId AND M.status = 99 AND M.linkType = 0 AND P.EnableSale = 1";
            Parameters parms = new Parameters();
            parms.AddInParameter("@NodeId", DbType.Int32, nodeId);
            parms.AddInParameter("@ModelId", DbType.Int32, modelId);
            return GetProductDictionary(sql, parms);
        }

        public IDictionary<int, string> GetProductList(int modelId, string productIdList)
        {
            return GetProductDictionary(string.Format("SELECT itemId, Title FROM PE_CommonModel WHERE ModelId = {0} AND LinkType = 0 AND Status<>-3 AND itemId IN ( {1} )", modelId.ToString(), DBHelper.ToValidId(productIdList)), null);
        }

        public IDictionary<int, string> GetProductList(string producerName, string trademarkName)
        {
            string sql = "SELECT P.ProductId, P.ProductName FROM PE_CommonModel M INNER JOIN PE_CommonProduct P ON M.ItemId = P.ProductId WHERE M.status = 99 AND M.linkType = 0 AND P.EnableSale = 1 AND ProducerName=@ProducerName AND TrademarkName=@TrademarkName ";
            Parameters parms = new Parameters();
            parms.AddInParameter("@ProducerName", DbType.String, producerName);
            parms.AddInParameter("@TrademarkName", DbType.String, trademarkName);
            return GetProductDictionary(sql, parms);
        }

        public IDictionary<int, string> GetProductList(int modelId, int searchType, string keyword, string keyword2)
        {
            StringBuilder builder = new StringBuilder("SELECT M.GeneralId, P.ProductName FROM PE_CommonProduct P INNER JOIN PE_CommonModel M ON M.ItemID = P.ProductID WHERE M.LinkType = 0 AND M.Status<>-3 AND M.ModelId = @ModelId");
            Parameters parms = new Parameters();
            parms.AddInParameter("@ModelId", DbType.Int32, modelId);
            if (!string.IsNullOrEmpty(keyword))
            {
                switch (searchType)
                {
                    case 1:
                        builder.Append(" AND P.ProductName LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'");
                        break;

                    case 2:
                        builder.Append(" AND P.Price >= @Keyword");
                        parms.AddInParameter("@Keyword", DbType.Decimal, DataConverter.CDecimal(keyword));
                        if (!string.IsNullOrEmpty(keyword2))
                        {
                            builder.Append(" AND P.Price <= @Keyword2");
                            parms.AddInParameter("@Keyword2", DbType.Decimal, DataConverter.CDecimal(keyword2));
                        }
                        break;

                    case 3:
                        builder.Append(" AND P.ProductID >= @Keyword");
                        parms.AddInParameter("@Keyword", DbType.Decimal, DataConverter.CDecimal(keyword));
                        if (!string.IsNullOrEmpty(keyword2))
                        {
                            builder.Append(" AND P.ProductID <= @Keyword2");
                            parms.AddInParameter("@Keyword2", DbType.Decimal, DataConverter.CDecimal(keyword2));
                        }
                        break;

                    case 4:
                        builder.Append(" AND P.ProdutType = @Keyword");
                        parms.AddInParameter("@Keyword", DbType.Int32, DataConverter.CLng(keyword));
                        break;
                }
            }
            return GetProductDictionary(builder.ToString(), parms);
        }

        public string GetProductName(int productId, string tableName)
        {
            string str = "";
            string strSql = "SELECT Title FROM PE_CommonModel WHERE TableName = @TableName AND LinkType = 0 AND itemId = @ProductId";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ProductId", DbType.String, productId);
            cmdParams.AddInParameter("@TableName", DbType.String, tableName);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, cmdParams))
            {
                if (reader.Read())
                {
                    str = reader.GetString("Title");
                }
            }
            return str;
        }

        public IList<ProductDetailInfo> GetProductsList(int startRowIndexId, int maxNumberRows, int searchType, string field, string keyword, int modelId)
        {
            string str;
            if (searchType > 0)
            {
                str = "SpeedSearch";
                keyword = searchType.ToString();
            }
            else
            {
                str = field;
            }
            return this.GetProductsList(startRowIndexId, maxNumberRows, str, keyword, string.Empty, 0, 0x63, modelId, true);
        }

        public IList<ProductDetailInfo> GetProductsList(int startRowIndexId, int maxNumberRows, string searchType, string keyword, string nodeIds, int listType, int status)
        {
            return this.GetProductsList(startRowIndexId, maxNumberRows, searchType, keyword, nodeIds, listType, status, 0, false);
        }

        private IList<ProductDetailInfo> GetProductsList(int startRowIndexId, int maxNumberRows, string searchType, string keyword, string nodeIds, int listType, int status, int modelId, bool wholesale)
        {
            string str;
            string str2;
            Database database = DatabaseFactory.CreateDatabase();
            GetSortParameter(listType, out str, out str2);
            string str3 = "";
            string temp = " AND ";
            if (!string.IsNullOrEmpty(nodeIds))
            {
                str3 = " M.NodeId IN (" + DBHelper.ToValidId(nodeIds) + ") ";
            }
            else
            {
                str3 = " M.TableName IS NOT null ";
            }
            if ((status < 100) && (status > -4))
            {
                str3 = string.Concat(new object[] { str3, temp, "M.Status = ", status, " " });
            }
            if (status == 100)
            {
                str3 = str3 + temp + "M.Status <= 99 AND M.Status >= 0 ";
            }
            if (status == 0x65)
            {
                str3 = str3 + temp + "M.Status < 99 AND M.Status >0 ";
            }
            if (modelId > 0)
            {
                str3 = string.Concat(new object[] { str3, temp, "M.ModelID =", modelId });
            }
            if (wholesale)
            {
                str3 = str3 + temp + "P.EnableWholesale = 1";
            }
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, str);
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "N.NodeName, M.EliteLevel, M.CreateTime, M.UpdateTime, M.NodeId, M.GeneralId, M.ModelId, M.LinkType, P.*");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, str2);
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_CommonProduct P INNER JOIN (PE_CommonModel M RIGHT JOIN PE_Nodes N ON M.NodeID= N.NodeID) ON P.ProductID = M.ItemID AND P.TableName = M.TableName AND M.Status<>-3");
            if (string.IsNullOrEmpty(searchType))
            {
                database.AddInParameter(storedProcCommand, "@Filter", DbType.String, str3);
            }
            else if (searchType == "SpeedSearch")
            {
                str3 = GetSpeedSearchFilter(keyword, str3, temp);
                database.AddInParameter(storedProcCommand, "@Filter", DbType.String, str3);
            }
            else
            {
                database.AddInParameter(storedProcCommand, "@Filter", DbType.String, str3 + temp + "P." + DBHelper.FilterBadChar(searchType) + " LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'");
            }
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            IList<ProductDetailInfo> list = new List<ProductDetailInfo>();
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    ProductDetailInfo productInfo = new ProductDetailInfo();
                    ProductFromrdr<ProductDetailInfo>(reader, productInfo);
                    productInfo.EliteLevel = reader.GetInt32("EliteLevel");
                    productInfo.UpdateTime = new DateTime?(reader.GetDateTime("UpdateTime"));
                    productInfo.CreateTime = reader.IsDBNull("CreateTime") ? productInfo.UpdateTime : new DateTime?(reader.GetDateTime("CreateTime"));
                    productInfo.NodeName = reader.GetString("NodeName");
                    productInfo.NodeId = reader.GetInt32("NodeId");
                    productInfo.GeneralId = reader.GetInt32("GeneralId");
                    productInfo.ModelId = reader.GetInt32("ModelId");
                    productInfo.LinkType = reader.GetInt32("LinkType");
                    list.Add(productInfo);
                }
            }
            this.m_TotalOfAllProducts = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public IList<ProductDetailInfo> GetProductsListByUserName(int startRowIndexId, int maxNumberRows, string userName)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "M.GeneralId");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "N.NodeName, M.EliteLevel, M.CreateTime, M.UpdateTime, M.NodeId, M.GeneralId, M.ModelId, M.LinkType, P.*");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_CommonProduct P INNER JOIN (PE_CommonModel M RIGHT JOIN PE_Nodes N ON M.NodeID= N.NodeID) ON P.ProductID = M.ItemID AND P.TableName = M.TableName AND M.Status<>-3");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, "M.Inputer = '" + DBHelper.FilterBadChar(userName) + "'");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            IList<ProductDetailInfo> list = new List<ProductDetailInfo>();
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    ProductDetailInfo productInfo = new ProductDetailInfo();
                    ProductFromrdr<ProductDetailInfo>(reader, productInfo);
                    productInfo.EliteLevel = reader.GetInt32("EliteLevel");
                    productInfo.UpdateTime = new DateTime?(reader.GetDateTime("UpdateTime"));
                    productInfo.CreateTime = reader.IsDBNull("CreateTime") ? productInfo.UpdateTime : new DateTime?(reader.GetDateTime("CreateTime"));
                    productInfo.NodeName = reader.GetString("NodeName");
                    productInfo.NodeId = reader.GetInt32("NodeId");
                    productInfo.GeneralId = reader.GetInt32("GeneralId");
                    productInfo.ModelId = reader.GetInt32("ModelId");
                    productInfo.LinkType = reader.GetInt32("LinkType");
                    list.Add(productInfo);
                }
            }
            this.m_TotalOfProducts = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public IList<string> GetShopTableNames()
        {
            IList<string> list = new List<string>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT TableName FROM PE_Model WHERE isEshop = 1 AND Disabled = 0"))
            {
                while (reader.Read())
                {
                    list.Add(reader.GetString(0));
                }
            }
            return list;
        }

        private static void GetSortParameter(int listType, out string sortColumn, out string sorts)
        {
            sortColumn = "M.GeneralId";
            sorts = "DESC";
            switch (listType)
            {
                case -2:
                    sortColumn = "M.GeneralId";
                    sorts = "ASC";
                    return;

                case -1:
                case 0:
                    break;

                case 1:
                    sortColumn = "M.EliteLevel";
                    sorts = "DESC";
                    return;

                case 2:
                    sortColumn = "M.EliteLevel";
                    sorts = "ASC";
                    return;

                case 3:
                    sortColumn = "M.Priority";
                    sorts = "DESC";
                    return;

                case 4:
                    sortColumn = "M.Priority";
                    sorts = "ASC";
                    return;

                case 5:
                    sortColumn = "M.DayHits";
                    sorts = "DESC";
                    return;

                case 6:
                    sortColumn = "M.DayHits";
                    sorts = "ASC";
                    return;

                case 7:
                    sortColumn = "M.WeekHits";
                    sorts = "DESC";
                    return;

                case 8:
                    sortColumn = "M.WeekHits";
                    sorts = "ASC";
                    return;

                case 9:
                    sortColumn = "M.MonthHits";
                    sorts = "DESC";
                    return;

                case 10:
                    sortColumn = "M.MonthHits";
                    sorts = "ASC";
                    return;

                case 11:
                    sortColumn = "M.Hits";
                    sorts = "DESC";
                    return;

                case 12:
                    sortColumn = "M.Hits";
                    sorts = "ASC";
                    break;

                default:
                    return;
            }
        }

        private static string GetSpeedSearchFilter(string keyword, string filter, string temp)
        {
            switch (DataConverter.CLng(keyword))
            {
                case 20:
                    return filter;

                case 0x15:
                    filter = filter + temp + "P.EnableSale = 1 ";
                    return filter;

                case 0x16:
                    filter = filter + temp + "P.EnableSale = 0 ";
                    return filter;

                case 0x17:
                    filter = filter + temp + "P.ProductType = 0 ";
                    return filter;

                case 0x18:
                    filter = filter + temp + "P.ProductType = 3 ";
                    return filter;

                case 0x19:
                    filter = filter + temp + "P.IsHot = 1 ";
                    return filter;

                case 0x1a:
                    filter = filter + temp + "P.IsNew = 1 ";
                    return filter;

                case 0x1b:
                    filter = filter + temp + "P.IsBest = 1 ";
                    return filter;

                case 0x1c:
                    filter = filter + temp + "P.SalePromotionType > 0 ";
                    return filter;

                case 0x1d:
                    filter = filter + temp + "P.Stocks <= P.AlarmNum ";
                    return filter;

                case 30:
                    filter = filter + temp + "P.Stocks <= P.AlarmNum + P.OrderNum ";
                    return filter;

                case 0x1f:
                    filter = filter + temp + "P.Stocks <= 0 ";
                    return filter;

                case 0x20:
                    filter = filter + temp + "P.EnableWholesale = 1 ";
                    return filter;
            }
            return filter;
        }

        public int GetStockAlarmCount(int type)
        {
            string strSql = " SELECT count(P.ProductID) FROM PE_CommonProduct P INNER JOIN PE_CommonModel M ON M.ItemID = P.ProductID AND M.Status<>-3 AND M.Linktype = 0 WHERE P.EnableSale = 1 ";
            if (type == 0)
            {
                strSql = strSql + "AND P.Stocks <= P.AlarmNum";
            }
            else
            {
                strSql = strSql + "AND P.Stocks <= P.AlarmNum + P.OrderNum";
            }
            return DataConverter.CLng(DBHelper.ExecuteScalarSql(strSql));
        }

        public int GetTotalOfAllProducts()
        {
            return this.m_TotalOfAllProducts;
        }

        public int GetTotalOfProducts()
        {
            return this.m_TotalOfProducts;
        }

        public IList<string> GetTrademarkListByNodeId(int nodeId)
        {
            string strSql = "SELECT DISTINCT P.TrademarkName FROM PE_CommonModel M INNER JOIN PE_CommonProduct P \r\n             ON M.ItemID = P.productID where NodeID=@NodeID AND M.LinkType=0 AND P.EnableSale = 1 AND M.Status=99 AND P.TrademarkName <>''";
            IList<string> list = new List<string>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, new Parameters("@NodeID", DbType.Int32, nodeId)))
            {
                while (reader.Read())
                {
                    list.Add(reader.GetString("TrademarkName"));
                }
            }
            return list;
        }

        private static void ProductFromrdr<T>(NullableDataReader rdr, T productInfo) where T: ProductInfo
        {
            productInfo.ProductId = rdr.GetInt32("ProductId");
            productInfo.TableName = rdr.GetString("TableName");
            productInfo.ProductName = rdr.GetString("ProductName");
            productInfo.ProductType = (ProductType) rdr.GetInt32("ProductType");
            productInfo.ProductPic = rdr.GetString("ProductPic");
            productInfo.ProductThumb = rdr.GetString("ProductThumb");
            productInfo.Unit = rdr.GetString("Unit");
            productInfo.ProductNum = rdr.GetString("ProductNum");
            productInfo.ServiceTermUnit = (ServiceTermUnit) rdr.GetInt32("ServiceTermUnit");
            productInfo.ServiceTerm = rdr.GetInt32("ServiceTerm");
            productInfo.PriceInfo.Price = rdr.GetDecimal("Price");
            productInfo.PriceMarket = rdr.GetDecimal("Price_Market");
            productInfo.PriceInfo.PriceMember = rdr.GetDecimal("Price_Member");
            productInfo.PriceInfo.PriceAgent = rdr.GetDecimal("Price_Agent");
            productInfo.EnableWholesale = rdr.GetBoolean("EnableWholesale");
            productInfo.PriceInfo.PriceWholesale1 = rdr.GetDecimal("Price_Wholesale1");
            productInfo.PriceInfo.PriceWholesale2 = rdr.GetDecimal("Price_Wholesale2");
            productInfo.PriceInfo.PriceWholesale3 = rdr.GetDecimal("Price_Wholesale3");
            productInfo.PriceInfo.NumberWholesale1 = rdr.GetInt32("Number_Wholesale1");
            productInfo.PriceInfo.NumberWholesale2 = rdr.GetInt32("Number_Wholesale2");
            productInfo.PriceInfo.NumberWholesale3 = rdr.GetInt32("Number_Wholesale3");
            productInfo.PresentId = rdr.GetInt32("PresentID");
            productInfo.PresentNumber = rdr.GetInt32("PresentNumber");
            productInfo.PresentPoint = rdr.GetInt32("PresentPoint");
            productInfo.PresentExp = rdr.GetInt32("PresentExp");
            productInfo.PresentMoney = rdr.GetDecimal("PresentMoney");
            productInfo.StocksProject = (StocksProject) rdr.GetInt32("StocksProject");
            productInfo.SalePromotionType = rdr.GetInt32("SalePromotionType");
            productInfo.MinNumber = rdr.GetInt32("MinNumber");
            productInfo.Discount = rdr.GetDouble("Discount");
            productInfo.IncludeTax = (TaxRateType) rdr.GetInt32("IncludeTax");
            productInfo.TaxRate = rdr.GetDouble("TaxRate");
            productInfo.Properties = rdr.GetString("Properties");
            productInfo.Weight = rdr.GetDouble("Weight");
            productInfo.LimitNum = rdr.GetInt32("LimitNum");
            productInfo.EnableSingleSell = rdr.GetBoolean("EnableSingleSell");
            productInfo.DependentProducts = rdr.GetString("DependentProducts");
            productInfo.ProductKind = (ProductKind) rdr.GetInt32("ProductKind");
            productInfo.OrderNum = rdr.GetInt32("OrderNum");
            productInfo.Stocks = rdr.GetInt32("Stocks");
            productInfo.EnableSale = rdr.GetBoolean("EnableSale");
            productInfo.EnableBuyWhenOutofstock = rdr.GetBoolean("EnableBuyWhenOutofstock");
            productInfo.BarCode = rdr.GetString("BarCode");
            productInfo.ProductExplain = rdr.GetString("ProductExplain");
            productInfo.ProducerName = rdr.GetString("ProducerName");
            productInfo.TrademarkName = rdr.GetString("TrademarkName");
            productInfo.Keyword = rdr.GetString("Keyword");
            productInfo.Stars = rdr.GetInt32("Stars");
            productInfo.ProductIntro = rdr.GetString("ProductIntro");
            productInfo.IsNew = rdr.GetBoolean("IsNew");
            productInfo.IsHot = rdr.GetBoolean("IsHot");
            productInfo.IsBest = rdr.GetBoolean("IsBest");
            productInfo.ProductCharacter = (ProductCharacter) rdr.GetInt32("ProductCharacter");
            productInfo.DownloadUrl = rdr.GetString("DownloadUrl");
            productInfo.Remark = rdr.GetString("Remark");
            productInfo.AlarmNum = rdr.GetInt32("AlarmNum");
            productInfo.BuyTimes = rdr.GetInt32("BuyTimes");
            productInfo.Minimum = rdr.GetInt32("Minimum");
        }

        public bool SetBest(string generalIdList, bool enableBest)
        {
            string strSql = "UPDATE PE_CommonProduct SET IsBest = @enableBest WHERE ProductID IN (" + DBHelper.ToValidId(generalIdList) + ")";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@enableBest", DbType.Boolean, enableBest);
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool SetElite(string generalIdList, int eliteLevel)
        {
            string strSql = "UPDATE PE_CommonModel SET EliteLevel = @EliteLevel WHERE GeneralId IN(" + DBHelper.ToValidId(generalIdList) + ")";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@EliteLevel", DbType.Int32, eliteLevel);
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool SetHot(string generalIdList, bool enableHot)
        {
            string strSql = "UPDATE PE_CommonProduct SET IsHot = @enableHot WHERE ProductID IN (" + DBHelper.ToValidId(generalIdList) + ")";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@enableHot", DbType.Boolean, enableHot);
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool SetNew(string generalIdList, bool enableNew)
        {
            string strSql = "UPDATE PE_CommonProduct SET IsNew = @enableNew WHERE ProductID IN (" + DBHelper.ToValidId(generalIdList) + ")";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@enableNew", DbType.Boolean, enableNew);
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool SetSale(string generalIdList, bool enableSale)
        {
            string strSql = "UPDATE PE_CommonProduct SET EnableSale = @EnableSale WHERE ProductID IN (" + DBHelper.ToValidId(generalIdList) + ")";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@EnableSale", DbType.Boolean, enableSale);
            int num = 0x63;
            if (!enableSale)
            {
                num = 90;
            }
            if (DBHelper.ExecuteSql(strSql, cmdParams))
            {
                DBHelper.ExecuteSql("UPDATE PE_CommonModel SET Status = @Status WHERE GeneralID IN (" + DBHelper.ToValidId(generalIdList) + ")", new Parameters("@Status", DbType.Int32, num));
            }
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool Update(ProductInfo info, string tableName)
        {
            StringBuilder builder = new StringBuilder(0x80);
            builder.Append("UPDATE ");
            builder.Append(DBHelper.FilterBadChar(tableName));
            builder.Append(" SET ");
            DataRow[] dataRows = Query.GetDataRows(info.Fields, "FieldLevel = 1");
            if (dataRows.Length > 0)
            {
                builder.Append(Query.GetUpdateFieldList(dataRows));
                builder.Append(" WHERE ID = " + info.ProductId.ToString());
                return DBHelper.ExecuteSql(builder.ToString(), GetParameters(info));
            }
            return false;
        }
    }
}

