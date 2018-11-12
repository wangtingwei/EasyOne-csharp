namespace EasyOne.IDal.Shop
{
    using EasyOne.Model.Shop;
    using System;
    using System.Collections.Generic;

    public interface IProductData
    {
        bool Add(int productId, string tableName, ProductDataInfo dataInfo);
        bool Add(int productId, string tableName, IList<string> attrList);
        bool AddOrderNum(int productId, string tableName, string property, int quantity);
        bool AddStocks(int productId, int quantity, string propertyValue);
        bool DeleteById(int id);
        bool DeleteByProduct(int productId, string tableName);
        IList<ProductDataInfo> GetListByProduct(int productId, string tableName);
        ProductDataInfo GetProductDataByPropertyValue(int productId, string tableName, string propertyValue);
        int GetStockByProperty(int productid, string propertyValue);
        bool Update(int productId, string tableName, ProductDataInfo info);
    }
}

