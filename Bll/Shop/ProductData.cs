namespace EasyOne.Shop
{
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Shop;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    internal sealed class ProductData
    {
        private static readonly IProductData dal = DataAccess.CreateProductData();

        private ProductData()
        {
        }

        public static bool Add(int productId, string tableName, IList<ProductDataInfo> productDataInfoList)
        {
            bool flag = true;
            foreach (ProductDataInfo info in productDataInfoList)
            {
                flag = Add(productId, tableName, info);
                if (!flag)
                {
                    dal.DeleteByProduct(productId, tableName);
                    return flag;
                }
            }
            return flag;
        }

        public static bool Add(int productId, string tableName, ProductDataInfo dataInfo)
        {
            return dal.Add(productId, tableName, dataInfo);
        }

        public static bool AddOrderNum(int productId, string tableName, string property, int quantity)
        {
            return dal.AddOrderNum(productId, tableName, property, quantity);
        }

        public static bool AddStocks(int productId, int quantity, string propertyValue)
        {
            return dal.AddStocks(productId, quantity, propertyValue);
        }

        public static IList<ProductDataInfo> GetListByProduct(int productId, string tableName)
        {
            return dal.GetListByProduct(productId, tableName);
        }

        public static ProductDataInfo GetProductDataByPropertyValue(int productId, string tableName, string propertyValue)
        {
            return dal.GetProductDataByPropertyValue(productId, tableName, propertyValue);
        }

        public static int GetStockByProperty(int productid, string propertyValue)
        {
            return dal.GetStockByProperty(productid, propertyValue);
        }

        public static bool Update(int productId, string tableName, IList<ProductDataInfo> infoList)
        {
            dal.DeleteByProduct(productId, tableName);
            return Add(productId, tableName, infoList);
        }
    }
}

