namespace EasyOne.Shop
{
    using EasyOne.Common;
    using EasyOne.Enumerations;
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Shop;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class ProductCommon
    {
        private static readonly IProductCommon dal = DataAccess.CreateProductCommon();

        private ProductCommon()
        {
        }

        public static bool Add(ProductInfo info, string tableName)
        {
            return dal.Add(info, tableName);
        }

        public static bool AddBuyTimes(int productId, string tableName)
        {
            return dal.AddBuyTimes(productId, tableName);
        }

        public static bool AddOrderNum(int id, int quantity)
        {
            return dal.AddOrderNum(id, quantity);
        }

        public static bool AddOrderNum(int productId, string tableName, int quantity)
        {
            return dal.AddOrderNum(productId, tableName, quantity);
        }

        public static bool AddStocks(int productId, int quantity)
        {
            return dal.AddStocks(productId, quantity);
        }

        public static bool ExistsPresent(int presentId)
        {
            return dal.ExistsPresent(presentId);
        }

        public static string GetGeneralIdList(string nodeIdList, string modelIdList)
        {
            if (!DataValidator.IsValidId(nodeIdList))
            {
                return string.Empty;
            }
            if (!string.IsNullOrEmpty(modelIdList) && !DataValidator.IsValidId(modelIdList))
            {
                return string.Empty;
            }
            return dal.GetGeneralIdList(nodeIdList, modelIdList);
        }

        public static IList<ProductInfo> GetProductCommonListByCharacter(ProductCharacter productCharacter)
        {
            return dal.GetProductCommonListByCharacter(productCharacter);
        }

        public static ProductInfo GetProductInfoByType(int productId, string tableName, ProductType productType)
        {
            return dal.GetProductInfoByType(productId, DataSecurity.FilterBadChar(tableName), productType);
        }

        public static Dictionary<int, string> GetProductList(string idList)
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            if (DataValidator.IsValidId(idList))
            {
                foreach (string str in idList.Split(new char[] { ',' }))
                {
                    int id = DataConverter.CLng(str);
                    string productName = dal.GetProductName(id);
                    if (!string.IsNullOrEmpty(productName))
                    {
                        dictionary.Add(id, productName);
                    }
                }
            }
            return dictionary;
        }

        public static string GetProductName(int productId)
        {
            return dal.GetProductName(productId);
        }

        public static IList<string> GetUnitList()
        {
            return dal.GetUnitList();
        }

        public static bool IsExistSameProductNum(string productNum)
        {
            return dal.IsExistSameProductNum(DataSecurity.FilterBadChar(productNum));
        }

        public static bool Update(ProductInfo info, string tableName)
        {
            return dal.Update(info, tableName);
        }
    }
}

