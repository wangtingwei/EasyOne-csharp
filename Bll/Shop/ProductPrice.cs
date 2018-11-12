namespace EasyOne.Shop
{
    using EasyOne.Common;
    using EasyOne.Enumerations;
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Shop;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class ProductPrice
    {
        private static readonly IProductPrice dal = DataAccess.CreateProductPrice();

        private ProductPrice()
        {
        }

        public static bool Add(int productId, string tableName, IList<ProductPriceInfo> priceInfoList)
        {
            foreach (ProductPriceInfo info in priceInfoList)
            {
                if (!Add(productId, tableName, info))
                {
                    Delete(productId, tableName);
                    return false;
                }
            }
            return true;
        }

        public static bool Add(int productId, string tableName, ProductPriceInfo priceInfo)
        {
            return dal.Add(productId, tableName, priceInfo);
        }

        public static bool Delete(int productId, string tableName)
        {
            return dal.Delete(productId, tableName);
        }

        private static decimal GetGroupPrice(ProductInfo productInfo, UserInfo userInfo, decimal price, string property)
        {
            if (price == -1M)
            {
                return GetProductPriceInfo(productInfo.ProductId, productInfo.TableName, userInfo.GroupId, property).Price;
            }
            if (price == 0M)
            {
                double input = 100.0;
                UserPurviewInfo userPurview = userInfo.UserPurview;
                if (userPurview != null)
                {
                    input = userPurview.Discount;
                }
                return ((productInfo.PriceInfo.Price * DataConverter.CDecimal(input)) / 100M);
            }
            return price;
        }

        public static IList<ProductPriceInfo> GetProductPrice(int productId, string tableName, string property)
        {
            return dal.GetProductPrice(productId, tableName, property);
        }

        public static IList<ProductPriceInfo> GetProductPriceById(int productId, string tableName)
        {
            return dal.GetProductPriceById(productId, tableName);
        }

        public static ProductPriceInfo GetProductPriceInfo(int productId, string tableName, int groupId, string property)
        {
            return dal.GetProductPriceInfo(productId, tableName, groupId, property);
        }

        public static decimal GetTruePrice(ProductInfo productInfo, int productAmount, UserInfo userInfo, string property, bool haveWholesalePurview)
        {
            if (!string.IsNullOrEmpty(property))
            {
                ProductDataInfo info = ProductData.GetProductDataByPropertyValue(productInfo.ProductId, productInfo.TableName, property);
                productInfo.PriceInfo = info.PriceInfo;
            }
            if ((haveWholesalePurview && productInfo.EnableWholesale) && (productAmount >= productInfo.PriceInfo.NumberWholesale1))
            {
                if (productAmount < productInfo.PriceInfo.NumberWholesale2)
                {
                    return productInfo.PriceInfo.PriceWholesale1;
                }
                if (productAmount < productInfo.PriceInfo.NumberWholesale3)
                {
                    return productInfo.PriceInfo.PriceWholesale2;
                }
                return productInfo.PriceInfo.PriceWholesale3;
            }
            if (!userInfo.IsNull)
            {
                switch (UserGroups.GetUserGroupById(userInfo.GroupId).GroupType)
                {
                    case GroupType.Register:
                        return GetGroupPrice(productInfo, userInfo, productInfo.PriceInfo.PriceMember, property);

                    case GroupType.Agent:
                        return GetGroupPrice(productInfo, userInfo, productInfo.PriceInfo.PriceAgent, property);
                }
                return productInfo.PriceInfo.Price;
            }
            return productInfo.PriceInfo.Price;
        }

        public static bool Update(int productId, string tableName, IList<ProductPriceInfo> priceInfoList)
        {
            Delete(productId, tableName);
            return Add(productId, tableName, priceInfoList);
        }

        public static bool Update(int productId, string tableName, ProductPriceInfo productPriceInfo)
        {
            return dal.Update(productId, tableName, productPriceInfo);
        }
    }
}

