namespace EasyOne.Shop
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Enumerations;
    using EasyOne.IDal.Contents;
    using EasyOne.IDal.Shop;
    using EasyOne.Model.CommonModel;
    using EasyOne.Model.Contents;
    using EasyOne.Model.Shop;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.OleDb;
    using System.Text;
    using EasyOne.DalFactory;

    public class Product
    {
        private static readonly IProduct dal = DataAccess.CreateProduct();
        private static readonly IContentManage dalContentManage = DataAccess.CreateContentManage();
        private EasyOne.Model.Contents.CommonModelInfo m_CommonModelInfo;
        private ProductInfo m_ProductInfoData;
        private static string s_ErrMsg;
        private static string s_MessgeOfBatchImport;

        public Product()
        {
        }

        public Product(int generalId)
        {
            this.GetProductAllDataById(generalId);
        }

        private static bool Add(int modelId, string tableName, ProductInfo info)
        {
            return ((dalContentManage.AddCommonModel(modelId, tableName, info.Fields, info.ProductId) && ProductCommon.Add(info, tableName)) && dal.Add(tableName, info));
        }

        public static bool Add(int modelId, ProductInfo productInfo, IList<ProductDataInfo> productDataInfoList, IList<ProductPriceInfo> priceInfoList)
        {
            ModelInfo modelInfoById = ModelManager.GetModelInfoById(modelId);
            if (modelInfoById.IsNull)
            {
                return false;
            }
            string tableName = modelInfoById.TableName;
            int newProductId = GetNewProductId();
            productInfo.ProductId = newProductId;
            bool flag = true;
            int num2 = 0;
            int alarmNum = 0;
            foreach (ProductDataInfo info2 in productDataInfoList)
            {
                num2 += info2.Stocks;
                alarmNum = info2.AlarmNum;
            }
            productInfo.Stocks = num2;
            productInfo.AlarmNum = alarmNum;
            flag = Add(modelId, tableName, productInfo);
            StockInfo stockInfo = new StockInfo();
            stockInfo.StockId = StockManage.GetMaxId() + 1;
            stockInfo.StockNum = StockItem.GetInStockNum();
            DataRow[] rowArray = productInfo.Fields.Select("FieldName='Inputer'");
            if (rowArray.Length > 0)
            {
                stockInfo.Inputer = rowArray[0]["FieldValue"].ToString();
            }
            stockInfo.InputTime = DateTime.Now;
            stockInfo.StockType = StockType.InStock;
            stockInfo.Remark = "商品库存初始";
            StockManage.Add(stockInfo);
            if (flag)
            {
                IList<StockItemInfo> infoList = new List<StockItemInfo>();
                if (!string.IsNullOrEmpty(productInfo.Properties))
                {
                    flag = ProductData.Add(newProductId, tableName, productDataInfoList);
                    foreach (ProductDataInfo info4 in productDataInfoList)
                    {
                        StockItemInfo item = new StockItemInfo();
                        item.ItemId = StockItem.GetMaxId() + 1;
                        item.Amount = info4.Stocks;
                        item.Price = info4.PriceInfo.Price;
                        item.ProductId = newProductId;
                        item.TableName = tableName;
                        item.Property = info4.PropertyValue;
                        item.StockId = stockInfo.StockId;
                        item.ProductNum = productInfo.ProductNum;
                        item.Unit = productInfo.Unit;
                        item.ProductName = productInfo.ProductName;
                        infoList.Add(item);
                    }
                }
                else
                {
                    StockItemInfo info6 = new StockItemInfo();
                    info6.Amount = productInfo.Stocks;
                    info6.Price = productInfo.PriceInfo.Price;
                    info6.ProductId = newProductId;
                    info6.TableName = tableName;
                    info6.Property = string.Empty;
                    info6.ProductNum = productInfo.ProductNum;
                    info6.Unit = productInfo.Unit;
                    info6.ProductName = productInfo.ProductName;
                    infoList.Add(info6);
                }
                StockItem.Add(infoList, stockInfo.StockId);
                if (((productInfo.PriceInfo.PriceMember == -1M) || (productInfo.PriceInfo.PriceAgent == -1M)) && (priceInfoList != null))
                {
                    flag = ProductPrice.Add(newProductId, tableName, priceInfoList);
                }
            }
            return flag;
        }

        public static bool AddBuyTimes(int productId, string tableName)
        {
            return ProductCommon.AddBuyTimes(productId, tableName);
        }

        private static void AddNewRows(DataTable dataTable, string fieldName, string fieldValue, FieldType fieldType, int fieldLevel)
        {
            DataRow row = dataTable.NewRow();
            row["FieldName"] = fieldName;
            row["FieldValue"] = fieldValue;
            row["FieldType"] = fieldType;
            row["FieldLevel"] = fieldLevel;
            dataTable.Rows.Add(row);
        }

        public static bool AddOrderNum(int id, int quantity)
        {
            return ProductCommon.AddOrderNum(id, quantity);
        }

        public static bool AddOrderNum(int productId, string tableName, int quantity)
        {
            return ProductCommon.AddOrderNum(productId, tableName, quantity);
        }

        public static bool AddOrderNum(int productId, string tableName, string property, int quantity)
        {
            return (ProductCommon.AddOrderNum(productId, tableName, quantity) && ProductData.AddOrderNum(productId, tableName, property, quantity));
        }

        public static bool AddStocks(int productId, int quantity)
        {
            return AddStocks(productId, quantity, null);
        }

        public static bool AddStocks(int productId, int quantity, string propertyValue)
        {
            if (string.IsNullOrEmpty(propertyValue))
            {
                return ProductCommon.AddStocks(productId, quantity);
            }
            return (ProductData.AddStocks(productId, quantity, propertyValue) && ProductCommon.AddStocks(productId, quantity));
        }

        public static int BatchImport(DataTable dtField, string filePath, string specialId, int nodeId, int modelId)
        {
            ModelInfo modelInfoById = ModelManager.GetModelInfoById(modelId);
            if (modelInfoById.IsNull)
            {
                return 0;
            }
            string tableName = modelInfoById.TableName;
            ProductInfo info = new ProductInfo();
            info.EnableWholesale = false;
            info.EnableSingleSell = true;
            info.SalePromotionType = 0;
            info.ProductCharacter = modelInfoById.Character;
            DataTable dataTable = GetDataTableFromModel(modelId, specialId, nodeId);
            info.Fields = ContentManage.GetNewContentData(dataTable);
            OleDbConnection selectConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"");
            string selectCommandText = "select * from [sheet1$]";
            OleDbDataAdapter adapter = new OleDbDataAdapter(selectCommandText, selectConnection);
            DataSet dataSet = new DataSet();
            try
            {
                adapter.Fill(dataSet, "ss");
            }
            catch (OleDbException)
            {
            }
            StringBuilder builder = new StringBuilder();
            int num2 = 0;
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                int newProductId = GetNewProductId();
                info.ProductId = newProductId;
                DataRow rows = dataSet.Tables[0].Rows[i];
                info.ProductName = GetExcelFieldValue(rows, dtField, "ProductName", string.Empty);
                info.ProductNum = GetExcelFieldValue(rows, dtField, "ProductNum", string.Empty);
                if (string.IsNullOrEmpty(info.ProductNum))
                {
                    Random random = new Random();
                    info.ProductNum = DateTime.Now.ToString("yyyyMMddHHmmss") + random.Next().ToString();
                }
                else if (IsExistSameProductNum(info.ProductNum))
                {
                    builder.Append("<li>商品：" + info.ProductName + "的编号已存在，未导入！</li>");
                    continue;
                }
                info.Unit = GetExcelFieldValue(rows, dtField, "Unit", "个");
                info.PriceInfo.Price = DataConverter.CDecimal(GetExcelFieldValue(rows, dtField, "Price", string.Empty));
                info.PriceMarket = DataConverter.CDecimal(GetExcelFieldValue(rows, dtField, "Price_Market", string.Empty));
                info.Weight = DataConverter.CDouble(GetExcelFieldValue(rows, dtField, "Weight", string.Empty));
                info.Stocks = DataConverter.CLng(GetExcelFieldValue(rows, dtField, "Stocks", string.Empty));
                info.AlarmNum = DataConverter.CLng(GetExcelFieldValue(rows, dtField, "AlarmNum", string.Empty));
                info.ProducerName = GetExcelFieldValue(rows, dtField, "ProducerName", string.Empty);
                info.TrademarkName = GetExcelFieldValue(rows, dtField, "TrademarkName", string.Empty);
                info.BarCode = GetExcelFieldValue(rows, dtField, "BarCode", string.Empty);
                info.ProductIntro = GetExcelFieldValue(rows, dtField, "ProductIntro", string.Empty);
                info.ProductExplain = GetExcelFieldValue(rows, dtField, "ProductExplain", string.Empty);
                info.Stars = DataConverter.CLng(GetExcelFieldValue(rows, dtField, "Stars", "3"));
                info.PriceInfo.PriceMember = DataConverter.CDecimal(GetExcelFieldValue(rows, dtField, "PriceMember", string.Empty));
                info.PriceInfo.PriceAgent = DataConverter.CDecimal(GetExcelFieldValue(rows, dtField, "PriceAgent", string.Empty));
                DataRow[] rowArray = info.Fields.Select("FieldName='Title'");
                if (rowArray.Length == 0)
                {
                    AddNewRows(dataTable, "Title", info.ProductName, FieldType.TitleType, 0);
                }
                else
                {
                    rowArray[0][1] = info.ProductName;
                }
                foreach (DataRow row2 in info.Fields.Select("FieldLevel=1"))
                {
                    string str5 = GetExcelFieldValue(rows, dtField, row2["FieldName"].ToString() + "_F", string.Empty);
                    if (string.IsNullOrEmpty(str5))
                    {
                        goto Label_04AA;
                    }
                    FieldType fieldtype = (FieldType) Enum.Parse(typeof(FieldType), row2["FieldType"].ToString());
                    string name = Field.GetFieldDataType(fieldtype).Name;
                    if (name != null)
                    {
                        if (!(name == "Double"))
                        {
                            if (name == "Decimal")
                            {
                                goto Label_0448;
                            }
                            if (name == "DateTime")
                            {
                                goto Label_045C;
                            }
                            if (name == "Boolean")
                            {
                                goto Label_0476;
                            }
                            if (name == "Int32")
                            {
                                goto Label_048A;
                            }
                        }
                        else
                        {
                            str5 = DataConverter.CDouble(str5).ToString();
                        }
                    }
                    goto Label_049C;
                Label_0448:
                    str5 = DataConverter.CDecimal(str5).ToString();
                    goto Label_049C;
                Label_045C:
                    str5 = DataConverter.CDate(str5).ToString();
                    goto Label_049C;
                Label_0476:
                    str5 = DataConverter.CBoolean(str5).ToString();
                    goto Label_049C;
                Label_048A:
                    str5 = DataConverter.CLng(str5).ToString();
                Label_049C:
                    row2["FieldValue"] = str5;
                Label_04AA:;
                }
                bool flag = Add(modelId, tableName, info);
                StockInfo stockInfo = new StockInfo();
                stockInfo.StockId = StockManage.GetMaxId() + 1;
                stockInfo.StockNum = StockItem.GetInStockNum();
                DataRow[] rowArray3 = info.Fields.Select("FieldName='Inputer'");
                if (rowArray3.Length > 0)
                {
                    stockInfo.Inputer = rowArray3[0]["FieldValue"].ToString();
                }
                stockInfo.InputTime = DateTime.Now;
                stockInfo.StockType = StockType.InStock;
                stockInfo.Remark = "商品库存初始";
                StockManage.Add(stockInfo);
                if (flag)
                {
                    IList<StockItemInfo> infoList = new List<StockItemInfo>();
                    StockItemInfo item = new StockItemInfo();
                    item.Amount = info.Stocks;
                    item.Price = info.PriceInfo.Price;
                    item.ProductId = newProductId;
                    item.TableName = tableName;
                    item.Property = string.Empty;
                    item.ProductNum = info.ProductNum;
                    item.Unit = info.Unit;
                    item.ProductName = info.ProductName;
                    infoList.Add(item);
                    StockItem.Add(infoList, stockInfo.StockId);
                }
                else
                {
                    builder.Append("<li>商品：" + info.ProductName + "导入失败！</li>");
                    continue;
                }
                num2++;
            }
            s_MessgeOfBatchImport = builder.ToString();
            dataSet.Dispose();
            adapter.Dispose();
            selectConnection.Dispose();
            return num2;
        }

        public static bool CharacterIsExists(ProductCharacter target, ProductCharacter source)
        {
            if (source == ProductCharacter.None)
            {
                return false;
            }
            return ((target & source) == source);
        }

        private static string CheckEnableSingleSell(string cartId, ProductInfo info)
        {
            string str = "";
            if (!info.EnableSingleSell)
            {
                str = CheckEnableSingleSell(ShoppingCart.GetProductIdAndTableNameInCart(cartId, false), info);
            }
            return str;
        }

        private static string CheckEnableSingleSell(StringBuilder productIdInCart, ProductInfo info)
        {
            string str = "";
            if (!info.EnableSingleSell)
            {
                bool flag = true;
                string[] strArray = info.DependentProducts.Split(new char[] { ',' });
                ProductInfo productById = new ProductInfo();
                foreach (string str2 in strArray)
                {
                    productById = GetProductById(DataConverter.CLng(str2));
                    if (!productById.IsNull && !StringHelper.FoundCharInArr(productIdInCart.ToString(), productById.ProductId.ToString() + "|" + productById.TableName))
                    {
                        flag = false;
                        break;
                    }
                }
                if (!flag)
                {
                    str = "<li>" + info.ProductName + "不能直接购买！请先购买此商品的从属商品" + productById.ProductName + "后再购买。</li>";
                }
            }
            return str;
        }

        public static string CheckStocks(ProductInfo productInfo, string propertyValue, int amount, StringBuilder productIdInCart)
        {
            int stocks = amount;
            string str = string.Empty;
            str = CheckEnableSingleSell(productIdInCart, productInfo);
            if (string.IsNullOrEmpty(str))
            {
                if (!GetProductById(productInfo.ProductId).EnableBuyWhenOutofstock)
                {
                    if (productInfo.StocksProject == StocksProject.ActualStock)
                    {
                        if (string.IsNullOrEmpty(propertyValue))
                        {
                            if (productInfo.Stocks < amount)
                            {
                                stocks = productInfo.Stocks;
                            }
                        }
                        else
                        {
                            ProductDataInfo info = ProductData.GetProductDataByPropertyValue(productInfo.ProductId, productInfo.TableName, propertyValue);
                            if (!info.IsNull && (info.Stocks < amount))
                            {
                                stocks = info.Stocks;
                            }
                        }
                    }
                    else if (string.IsNullOrEmpty(propertyValue))
                    {
                        if ((productInfo.Stocks - productInfo.OrderNum) < amount)
                        {
                            stocks = productInfo.Stocks - productInfo.OrderNum;
                        }
                    }
                    else
                    {
                        ProductDataInfo info2 = ProductData.GetProductDataByPropertyValue(productInfo.ProductId, productInfo.TableName, propertyValue);
                        if (!info2.IsNull && ((info2.Stocks - info2.OrderNum) < amount))
                        {
                            stocks = info2.Stocks - info2.OrderNum;
                        }
                    }
                    if (stocks != amount)
                    {
                        str = string.Concat(new object[] { "您订购了", amount, productInfo.Unit, productInfo.ProductName, "，而此商品目前只有", stocks, productInfo.Unit, "，请重新调整您的购物车！" });
                    }
                }
                int num2 = Order.CountBuyNum(PEContext.Current.User.UserName, productInfo.ProductId);
                if (((productInfo.LimitNum > 0) && ((num2 + amount) > productInfo.LimitNum)) || ((productInfo.LimitNum > 0) && (amount > productInfo.LimitNum)))
                {
                    str = string.Concat(new object[] { "您订购了", amount, productInfo.Unit, productInfo.ProductName, "，曾经购买了", num2, productInfo.Unit, "，而此商品每人最多限购", productInfo.LimitNum, productInfo.Unit, "，请重新调整您的购物车！" });
                }
                ProductInfo productById = GetProductById(productInfo.ProductId);
                if ((productById.Minimum > 0) && (amount < productById.Minimum))
                {
                    str = string.Concat(new object[] { "您订购了", amount, productInfo.Unit, productInfo.ProductName, "，曾经购买了", num2, productInfo.Unit, "，而此商品每人最低购买量为", productById.Minimum, productInfo.Unit, "，请重新调整您的购物车！" });
                }
            }
            return str;
        }

        public static bool Delete(string generalIdList)
        {
            return (DataValidator.IsValidId(generalIdList) && dal.Delete(generalIdList));
        }

        public static bool ExistsPresent(int presentId)
        {
            return ProductCommon.ExistsPresent(presentId);
        }

        private static DataTable GetDataTableFromModel(int modelId, string specialId, int nodeId)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("FieldName");
            dataTable.Columns.Add("FieldValue");
            dataTable.Columns.Add("FieldType");
            dataTable.Columns.Add("FieldLevel");
            foreach (FieldInfo info in Field.GetFieldList(modelId, false))
            {
                if (info.FieldType != FieldType.Property)
                {
                    AddNewRows(dataTable, info.FieldName, string.IsNullOrEmpty(info.DefaultValue) ? "" : info.DefaultValue, info.FieldType, info.FieldLevel);
                }
            }
            AddNewRows(dataTable, "DayHits", "0", FieldType.NumberType, 0);
            AddNewRows(dataTable, "EliteLevel", "0", FieldType.NumberType, 0);
            AddNewRows(dataTable, "Hits", "0", FieldType.NumberType, 0);
            AddNewRows(dataTable, "Priority", "0", FieldType.NumberType, 0);
            AddNewRows(dataTable, "WeekHits", "0", FieldType.NumberType, 0);
            AddNewRows(dataTable, "MonthHits", "0", FieldType.NumberType, 0);
            AddNewRows(dataTable, "UpdateTime", DateTime.Now.ToString(), FieldType.DateTimeType, 0);
            AddNewRows(dataTable, "TemplateFile", "", FieldType.TemplateType, 0);
            AddNewRows(dataTable, "SpecialId", specialId, FieldType.SpecialType, 0);
            AddNewRows(dataTable, "InfoId", "", FieldType.InfoType, 0);
            AddNewRows(dataTable, "NodeId", nodeId.ToString(), FieldType.NodeType, 0);
            AddNewRows(dataTable, "Inputer", PEContext.Current.Admin.AdminName, FieldType.TextType, 0);
            AddNewRows(dataTable, "Status", "90", FieldType.StatusType, 0);
            return dataTable;
        }

        public static ArrayList GetExcelFields(string excelSource)
        {
            ArrayList list = new ArrayList();
            list.Add("不导入此项");
            OleDbConnection selectConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + excelSource + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"");
            OleDbDataAdapter adapter = new OleDbDataAdapter("select * from [sheet1$]", selectConnection);
            DataSet dataSet = new DataSet();
            try
            {
                adapter.Fill(dataSet, "ss");
                for (int i = 0; i < dataSet.Tables[0].Columns.Count; i++)
                {
                    list.Add(dataSet.Tables[0].Columns[i].ColumnName);
                }
            }
            catch (OleDbException)
            {
            }
            finally
            {
                dataSet.Dispose();
                adapter.Dispose();
                selectConnection.Dispose();
            }
            return list;
        }

        private static string GetExcelFieldValue(DataRow rows, DataTable dtField, string fieldName, string defaultValue)
        {
            string input = defaultValue;
            string strB = "不导入此项";
            DataRow[] rowArray = dtField.Select("FieldName='" + fieldName + "'");
            if (rowArray.Length != 0)
            {
                strB = rowArray[0][0].ToString();
            }
            if (string.Compare("不导入此项", strB, StringComparison.CurrentCulture) != 0)
            {
                input = rows[strB].ToString();
                if (string.Compare(fieldName, "stars", true) == 0)
                {
                    int num = DataConverter.CLng(input);
                    if ((num < 1) || (num > 5))
                    {
                        input = defaultValue;
                    }
                }
            }
            if (string.IsNullOrEmpty(input))
            {
                input = defaultValue;
            }
            return input;
        }

        public static IDictionary<int, string> GetListByNodeIdAndTrademark(int nodeId, string trademarkName)
        {
            return dal.GetListByNodeIdAndTrademark(nodeId, trademarkName);
        }

        private static int GetNewProductId()
        {
            return dal.GetNewProductId();
        }

        private static string GetNodeArrChildId(int nodeId)
        {
            string arrChildId = string.Empty;
            if (nodeId > 0)
            {
                NodeInfo cacheNodeById = EasyOne.Contents.Nodes.GetCacheNodeById(nodeId);
                if (!cacheNodeById.IsNull)
                {
                    arrChildId = cacheNodeById.ArrChildId;
                }
            }
            return arrChildId;
        }

        public void GetProductAllDataById(int generalId)
        {
            EasyOne.Model.Contents.CommonModelInfo commonModelInfoById = ContentManage.GetCommonModelInfoById(generalId);
            if (!commonModelInfoById.IsNull)
            {
                this.m_CommonModelInfo = commonModelInfoById;
                this.m_ProductInfoData = GetProductById(generalId, commonModelInfoById.ItemId, commonModelInfoById.TableName);
            }
        }

        public void GetProductAllDataById(int productId, string tableName)
        {
            if ((productId > 0) && !string.IsNullOrEmpty(tableName))
            {
                tableName = DataSecurity.FilterBadChar(tableName);
                this.m_ProductInfoData = GetProductById(productId, tableName);
            }
        }

        public static ProductInfo GetProductById(int id)
        {
            return dal.GetProductById(id);
        }

        public static ProductInfo GetProductById(int productId, string tableName)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                return new ProductInfo(true);
            }
            tableName = DataSecurity.FilterBadChar(tableName);
            return dal.GetProductById(productId, tableName);
        }

        private static ProductInfo GetProductById(int generalId, int productId, string tableName)
        {
            return dal.GetProductById(generalId, productId, tableName);
        }

        public static IList<ProductInfo> GetProductCommonListByCharacter(ProductCharacter productCharacter)
        {
            return ProductCommon.GetProductCommonListByCharacter(productCharacter);
        }

        public static ProductDetailInfo GetProductDetailInfoById(int id)
        {
            return dal.GetProductDetailInfoById(id);
        }

        public ProductInfo GetProductInfo()
        {
            if (this.m_ProductInfoData == null)
            {
                this.m_ProductInfoData = new ProductInfo(true);
            }
            return this.m_ProductInfoData;
        }

        public static ProductInfo GetProductInfo(int productId, string tableName, ProductType productType)
        {
            return ProductCommon.GetProductInfoByType(productId, tableName, productType);
        }

        public static IList<ProductInfo> GetProductInfoList(int startRowIndexId, int maxNumberRows, string searchProductName, string productTypeList)
        {
            return GetProductInfoList(0, startRowIndexId, maxNumberRows, searchProductName, productTypeList);
        }

        public static IList<ProductInfo> GetProductInfoList(int modelId, int startRowIndexId, int maxNumberRows, string searchProductName, string productTypeList)
        {
            IList<ProductInfo> list = new List<ProductInfo>();
            string tableName = string.Empty;
            if (modelId > 0)
            {
                ModelInfo modelInfoById = ModelManager.GetModelInfoById(modelId);
                if (modelInfoById.IsNull)
                {
                    return list;
                }
                if (string.IsNullOrEmpty(modelInfoById.TableName))
                {
                    return list;
                }
                tableName = modelInfoById.TableName;
            }
            if (!DataValidator.IsValidId(productTypeList))
            {
                return list;
            }
            return dal.GetProductInfoList(startRowIndexId, maxNumberRows, tableName, DataSecurity.FilterBadChar(searchProductName), productTypeList);
        }

        public static IDictionary<int, string> GetProductList(int modelId)
        {
            return dal.GetProductList(modelId);
        }

        public static DataTable GetProductList(string productIdList)
        {
            string[] strArray = productIdList.Split(new char[] { ',' });
            DataTable table = new DataTable();
            table.Columns.Add("ProductId");
            table.Columns.Add("ProductName");
            foreach (string str in strArray)
            {
                string[] strArray2 = str.Split(new char[] { '|' });
                if (strArray2.Length > 0)
                {
                    int productId = DataConverter.CLng(strArray2[0]);
                    string tableName = DataSecurity.FilterBadChar(strArray2[1]);
                    string productName = dal.GetProductName(productId, tableName);
                    if (productName != null)
                    {
                        DataRow row = table.NewRow();
                        row["ProductId"] = productId + "|" + tableName;
                        row["ProductName"] = productName;
                        table.Rows.Add(row);
                    }
                }
            }
            return table;
        }

        public static IDictionary<int, string> GetProductList(int nodeId, int modelId)
        {
            return dal.GetProductList(nodeId, modelId);
        }

        public static IDictionary<int, string> GetProductList(int modelId, string productIdList)
        {
            if (!DataValidator.IsValidId(productIdList))
            {
                return null;
            }
            return dal.GetProductList(modelId, productIdList);
        }

        public static IDictionary<int, string> GetProductList(string producerName, string trademarkName)
        {
            if (string.IsNullOrEmpty(producerName) && string.IsNullOrEmpty(trademarkName))
            {
                return new Dictionary<int, string>();
            }
            return dal.GetProductList(DataSecurity.FilterBadChar(producerName), DataSecurity.FilterBadChar(trademarkName));
        }

        public static IDictionary<int, string> GetProductList(int modelId, int searchType, string keyword, string keyword2)
        {
            if (searchType == 1)
            {
                DataSecurity.FilterBadChar(keyword);
            }
            return dal.GetProductList(modelId, searchType, keyword, keyword2);
        }

        public static IList<string> GetProductPropertiesList(IList<string> attrList)
        {
            return GetProductPropertiesList(attrList, null);
        }

        public static IList<string> GetProductPropertiesList(string properties)
        {
            return GetProductPropertiesList(properties, null);
        }

        public static IList<string> GetProductPropertiesList(IList<string> attrList, char? splitor)
        {
            char? nullable = splitor;
            int? nullable3 = nullable.HasValue ? new int?(nullable.GetValueOrDefault()) : null;
            if (!nullable3.HasValue)
            {
                splitor = '|';
            }
            IList<string> list = new List<string>(attrList[0].Split(new char[] { '|' }));
            for (int i = 1; i < attrList.Count; i++)
            {
                list = JoinPart(list, attrList[i].Split(new char[] { '|' }), splitor.Value);
            }
            return list;
        }

        public static IList<string> GetProductPropertiesList(string properties, char? splitor)
        {
            if (string.IsNullOrEmpty(properties))
            {
                return new List<string>();
            }
            IList<string> attrList = new List<string>();
            foreach (string str in properties.Split(new char[] { '\n' }))
            {
                attrList.Add(str.Substring(str.LastIndexOf('$') + 1));
            }
            return GetProductPropertiesList(attrList, splitor);
        }

        public static IList<ProductDetailInfo> GetProductsList(int startRowIndexId, int maxNumberRows, int searchType, string field, string keyword, int modelId)
        {
            return dal.GetProductsList(startRowIndexId, maxNumberRows, searchType, DataSecurity.FilterBadChar(field), DataSecurity.FilterBadChar(keyword), modelId);
        }

        public static IList<ProductDetailInfo> GetProductsList(int startRowIndexId, int maxNumberRows, string searchType, string keyword, int nodeId, int listType, int status)
        {
            string nodeIds = string.Empty;
            GetSearchNodeIds(nodeId, ref nodeIds);
            return dal.GetProductsList(startRowIndexId, maxNumberRows, DataSecurity.FilterBadChar(searchType), DataSecurity.FilterBadChar(keyword), nodeIds, listType, status);
        }

        public static IList<ProductDetailInfo> GetProductsListByUserName(int startRowIndexId, int maxNumberRows, string userName)
        {
            return dal.GetProductsListByUserName(startRowIndexId, maxNumberRows, DataSecurity.FilterBadChar(userName));
        }

        private static void GetSearchNodeIds(int nodeId, ref string nodeIds)
        {
            nodeIds = GetNodeArrChildId(nodeId);
            if (string.IsNullOrEmpty(nodeIds) && !PEContext.Current.Admin.IsSuperAdmin)
            {
                nodeIds = RolePermissions.GetRoleAllNodeId(PEContext.Current.Admin.Roles);
            }
        }

        public static int GetStockAlarmCount(int type)
        {
            if (type == 2)
            {
                return (dal.GetStockAlarmCount(0) + dal.GetStockAlarmCount(1));
            }
            return dal.GetStockAlarmCount(type);
        }

        public static int GetStockByProperty(int productid, string propertyValue)
        {
            return ProductData.GetStockByProperty(productid, propertyValue);
        }

        public static int GetTotalOfAllProducts(string searchType, string keyword, int nodeId, int listType, int status)
        {
            return dal.GetTotalOfAllProducts();
        }

        public static int GetTotalOfProducts()
        {
            return dal.GetTotalOfProducts();
        }

        public static int GetTotalProductsOfUser(string userName)
        {
            return dal.GetTotalOfProducts();
        }

        public static IList<string> GetTrademarkListByNodeId(int nodeId)
        {
            if (nodeId == 0)
            {
                return new List<string>();
            }
            return dal.GetTrademarkListByNodeId(nodeId);
        }

        public static IList<string> GetUnitList()
        {
            return ProductCommon.GetUnitList();
        }

        public static int GetWholesaleProductTotal(int searchType, string field, string keyword, int modelId)
        {
            return dal.GetTotalOfAllProducts();
        }

        public static bool IsEnableSale(int productId, string tableName, string propertyValue, string cartId)
        {
            s_ErrMsg = "";
            ProductInfo productById = GetProductById(productId, tableName);
            if (productById.IsNull)
            {
                s_ErrMsg = "<li>找不到指定的商品！</li>";
                return false;
            }
            if (productById.ProductId == 0)
            {
                s_ErrMsg = "<li>找不到指定的商品！</li>";
                return false;
            }
            if (!productById.EnableSale)
            {
                s_ErrMsg = "<li>商品已经停止销售！</li>";
                return false;
            }
            s_ErrMsg = CheckEnableSingleSell(cartId, productById);
            if (string.IsNullOrEmpty(s_ErrMsg))
            {
                if (string.IsNullOrEmpty(propertyValue))
                {
                    if (!productById.EnableBuyWhenOutofstock && !IsEnoughStock(productById.StocksProject, productById.Stocks, productById.OrderNum))
                    {
                        return false;
                    }
                    goto Label_00D3;
                }
                ProductDataInfo info2 = ProductData.GetProductDataByPropertyValue(productId, tableName, propertyValue);
                if (!info2.IsNull)
                {
                    if (!productById.EnableBuyWhenOutofstock && !IsEnoughStock(productById.StocksProject, info2.Stocks, info2.OrderNum))
                    {
                        return false;
                    }
                    goto Label_00D3;
                }
                s_ErrMsg = "<li>对不起，没有该商品！</li>";
            }
            return false;
        Label_00D3:
            if (productById.ProductType == ProductType.Promotion)
            {
                s_ErrMsg = "<li>促销商品不能直接购买！</li>";
                return false;
            }
            return true;
        }

        private static bool IsEnoughStock(StocksProject stocksProject, int stocks, int orderNum)
        {
            int num;
            bool flag = true;
            if (stocksProject == StocksProject.ActualStock)
            {
                num = stocks;
            }
            else
            {
                num = stocks - orderNum;
            }
            if (num <= 0)
            {
                s_ErrMsg = "<li>实在对不起，此商品已经暂时售罄！</li>";
                flag = false;
            }
            return flag;
        }

        public static bool IsExistSameProductNum(string productNum)
        {
            return ProductCommon.IsExistSameProductNum(productNum);
        }

        private static IList<string> JoinPart(IList<string> part1, string[] part2, char splitor)
        {
            IList<string> list = new List<string>();
            foreach (string str in part1)
            {
                foreach (string str2 in part2)
                {
                    list.Add(str.Trim() + splitor + str2.Trim());
                }
            }
            return list;
        }

        public static bool SetBest(string generalIdList, bool enableBest)
        {
            if (!DataValidator.IsValidId(generalIdList))
            {
                return false;
            }
            return dal.SetBest(generalIdList, enableBest);
        }

        public static bool SetElite(string generalIdList, int eliteLevel)
        {
            if (!DataValidator.IsValidId(generalIdList))
            {
                return false;
            }
            return dal.SetElite(generalIdList, eliteLevel);
        }

        public static bool SetHot(string generalIdList, bool enableHot)
        {
            if (!DataValidator.IsValidId(generalIdList))
            {
                return false;
            }
            return dal.SetHot(generalIdList, enableHot);
        }

        public static bool SetNew(string generalIdList, bool enableNew)
        {
            if (!DataValidator.IsValidId(generalIdList))
            {
                return false;
            }
            return dal.SetNew(generalIdList, enableNew);
        }

        public static bool SetSale(string generalIdList, bool enableSale)
        {
            if (!DataValidator.IsValidId(generalIdList))
            {
                return false;
            }
            return dal.SetSale(generalIdList, enableSale);
        }

        public static string ShowProductType(int saleType)
        {
            switch (saleType)
            {
                case 1:
                    return "";

                case 2:
                    return " （换购）";

                case 3:
                    return " （赠送）";

                case 4:
                    return " （批发）";
            }
            return "";
        }

        public static bool Update(int generalId, ProductInfo productInfo, IList<ProductDataInfo> dataInfoList, IList<ProductPriceInfo> priceInfoList)
        {
            EasyOne.Model.Contents.CommonModelInfo commonModelInfoById = ContentManage.GetCommonModelInfoById(generalId);
            if (commonModelInfoById.IsNull)
            {
                return false;
            }
            productInfo.ProductId = commonModelInfoById.ItemId;
            string tableName = commonModelInfoById.TableName;
            if (commonModelInfoById.LinkType == 1)
            {
                return dalContentManage.UpdateCommonModel(generalId, productInfo.Fields);
            }
            int num = 0;
            int num2 = 0;
            foreach (ProductDataInfo info2 in dataInfoList)
            {
                num += info2.Stocks;
                num2 += info2.AlarmNum;
            }
            if (num > 0)
            {
                productInfo.Stocks = num;
            }
            if (num2 > 0)
            {
                productInfo.AlarmNum = num2;
            }
            if (num2 > num)
            {
                num2 = num;
            }
            if (!dalContentManage.UpdateCommonModel(generalId, productInfo.Fields) || !ProductCommon.Update(productInfo, tableName))
            {
                return false;
            }
            dal.Update(productInfo, tableName);
            if (!string.IsNullOrEmpty(productInfo.Properties) && !ProductData.Update(productInfo.ProductId, tableName, dataInfoList))
            {
                return false;
            }
            return ((priceInfoList == null) || ProductPrice.Update(productInfo.ProductId, tableName, priceInfoList));
        }

        public EasyOne.Model.Contents.CommonModelInfo CommonModelInfo
        {
            get
            {
                return this.m_CommonModelInfo;
            }
        }

        public static string ErrMsgOfEnableSale
        {
            get
            {
                return s_ErrMsg;
            }
        }

        public static string MessgeOfBatchImport
        {
            get
            {
                return s_MessgeOfBatchImport;
            }
        }

        public IList<ProductDataInfo> ProductDataInfoList
        {
            get
            {
                if (this.m_ProductInfoData == null)
                {
                    return null;
                }
                return ProductData.GetListByProduct(this.m_ProductInfoData.ProductId, this.m_ProductInfoData.TableName);
            }
        }

        public ProductInfo ProductInfoData
        {
            get
            {
                return this.m_ProductInfoData;
            }
        }

        public IList<ProductPriceInfo> ProductPriceInfoList
        {
            get
            {
                if (this.m_ProductInfoData == null)
                {
                    return null;
                }
                return ProductPrice.GetProductPriceById(this.m_ProductInfoData.ProductId, this.m_ProductInfoData.TableName);
            }
        }
    }
}

