namespace EasyOne.IDal.Shop
{
    using EasyOne.Enumerations;
    using EasyOne.Model.Shop;
    using System;
    using System.Collections.Generic;

    public interface IProductCommon
    {
        bool Add(ProductInfo info, string tableName);
        bool AddBuyTimes(int productId, string tableName);
        bool AddOrderNum(int id, int quantity);
        bool AddOrderNum(int productId, string tableName, int quantity);
        bool AddStocks(int productId, int quantity);
        bool DeleteById(int id, string tableName);
        bool ExistsPresent(int presentId);
        string GetGeneralIdList(string nodeIdList, string modelIdList);
        IList<ProductInfo> GetProductCommonListByCharacter(ProductCharacter productCharacter);
        ProductInfo GetProductInfoByType(int productId, string tableName, ProductType productType);
        string GetProductName(int id);
        IList<string> GetUnitList();
        bool IsExistSameProductNum(string productNum);
        bool Update(ProductInfo info, string tableName);
    }
}

