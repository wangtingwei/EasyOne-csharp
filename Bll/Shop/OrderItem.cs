namespace EasyOne.Shop
{
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Shop;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class OrderItem
    {
        private static readonly IOrderItem dal = DataAccess.CreateOrderItem();

        private OrderItem()
        {
        }

        public static bool Add(OrderItemInfo orderItemInfo)
        {
            bool flag = dal.Add(orderItemInfo);
            if (!flag || string.IsNullOrEmpty(orderItemInfo.TableName))
            {
                return flag;
            }
            if (string.IsNullOrEmpty(orderItemInfo.Property))
            {
                return Product.AddOrderNum(orderItemInfo.ProductId, orderItemInfo.TableName, orderItemInfo.Amount);
            }
            return Product.AddOrderNum(orderItemInfo.ProductId, orderItemInfo.TableName, orderItemInfo.Property, orderItemInfo.Amount);
        }

        public static bool Add(IList<OrderItemInfo> orderItemInfoList)
        {
            bool flag = false;
            foreach (OrderItemInfo info in orderItemInfoList)
            {
                flag = Add(info);
                if (!flag)
                {
                    return flag;
                }
            }
            return flag;
        }

        public static bool Delete(int orderId)
        {
            IList<OrderItemInfo> infoListByOrderId = GetInfoListByOrderId(orderId);
            bool flag = dal.Delete(orderId);
            if (flag)
            {
                foreach (OrderItemInfo info in infoListByOrderId)
                {
                    if (!string.IsNullOrEmpty(info.TableName))
                    {
                        Product.AddOrderNum(info.ProductId, -info.Amount);
                    }
                }
            }
            return flag;
        }

        public static bool ExistsPresent(int presentId)
        {
            return dal.ExistsPresent(presentId);
        }

        public static bool ExistsProduct(string tableName)
        {
            return dal.ExistsProduct(tableName);
        }

        public static bool ExistsProduct(string tableName, int productId)
        {
            return dal.ExistsProduct(tableName, productId);
        }

        public static OrderItemInfo GetInfoByItemId(int itemId)
        {
            return dal.GetInfoByItemId(itemId);
        }

        public static IList<OrderItemInfo> GetInfoListByOrderId(int orderId)
        {
            return dal.GetInfoListByOrderId(orderId);
        }

        public static int GetMaxId()
        {
            return dal.GetMaxId();
        }

        public static IList<string> GetProductNameList(int orderId)
        {
            return dal.GetProductNameList(orderId);
        }

        public static bool Update(IList<OrderItemInfo> orderItemInfoList)
        {
            bool flag = false;
            foreach (OrderItemInfo info in orderItemInfoList)
            {
                flag = Update(info);
                if (!flag)
                {
                    return flag;
                }
            }
            return flag;
        }

        public static bool Update(OrderItemInfo orderItemInfo)
        {
            OrderItemInfo infoByItemId = GetInfoByItemId(orderItemInfo.ItemId);
            bool flag = dal.Update(orderItemInfo);
            if (flag)
            {
                flag = Product.AddOrderNum(orderItemInfo.ProductId, orderItemInfo.TableName, orderItemInfo.Property, orderItemInfo.Amount - infoByItemId.Amount);
            }
            return flag;
        }
    }
}

