namespace EasyOne.IDal.Shop
{
    using EasyOne.Model.Shop;
    using System;
    using System.Collections.Generic;

    public interface IProductPrice
    {
        bool Add(int productId, string tableName, ProductPriceInfo priceInfo);
        bool Delete(int id);
        bool Delete(int productId, string tableName);
        IList<ProductPriceInfo> GetProductPrice(int productId, string tableName, string property);
        IList<ProductPriceInfo> GetProductPriceById(int productId, string tableName);
        ProductPriceInfo GetProductPriceInfo(int productId, string tableName, int groupId, string property);
        bool Update(int productId, string tableName, ProductPriceInfo productPriceInfo);
    }
}

