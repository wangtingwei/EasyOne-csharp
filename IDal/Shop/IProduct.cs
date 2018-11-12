namespace EasyOne.IDal.Shop
{
    using EasyOne.Model.Shop;
    using System;
    using System.Collections.Generic;

    public interface IProduct
    {
        bool Add(string tableName, ProductInfo info);
        bool Delete(string generalIdList);
        int GetGeneralId(string tableName, int productId);
        IDictionary<int, string> GetListByNodeIdAndTrademark(int nodeId, string trademarkName);
        int GetNewProductId();
        ProductInfo GetProductById(int id);
        ProductInfo GetProductById(int productId, string tableName);
        ProductInfo GetProductById(int generalId, int productId, string tableName);
        ProductDetailInfo GetProductDetailInfoById(int id);
        IList<ProductInfo> GetProductInfoList(int startRowIndexId, int maxNumberRows, string tableName, string searchProductName, string productType);
        IDictionary<int, string> GetProductList(int modelId);
        IDictionary<int, string> GetProductList(int nodeId, int modelId);
        IDictionary<int, string> GetProductList(int modelId, string productIdList);
        IDictionary<int, string> GetProductList(string producerName, string trademarkName);
        IDictionary<int, string> GetProductList(int modelId, int searchType, string keyword, string keyword2);
        string GetProductName(int productId, string tableName);
        IList<ProductDetailInfo> GetProductsList(int startRowIndexId, int maxNumberRows, int searchType, string field, string keyword, int modelId);
        IList<ProductDetailInfo> GetProductsList(int startRowIndexId, int maxNumberRows, string searchType, string keyword, string nodeIds, int listType, int status);
        IList<ProductDetailInfo> GetProductsListByUserName(int startRowIndexId, int maxNumberRows, string userName);
        IList<string> GetShopTableNames();
        int GetStockAlarmCount(int type);
        int GetTotalOfAllProducts();
        int GetTotalOfProducts();
        IList<string> GetTrademarkListByNodeId(int nodeId);
        bool SetBest(string generalIdList, bool enableBest);
        bool SetElite(string generalIdList, int eliteLevel);
        bool SetHot(string generalIdList, bool enableHot);
        bool SetNew(string generalIdList, bool enableNew);
        bool SetSale(string generalIdList, bool enableSale);
        bool Update(ProductInfo info, string tableName);
    }
}

