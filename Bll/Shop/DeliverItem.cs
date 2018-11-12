namespace EasyOne.Shop
{
    using EasyOne.Common;
    using EasyOne.Enumerations;
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Shop;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using EasyOne.DalFactory;

    public sealed class DeliverItem
    {
        private static readonly IDeliverItem dal = DataAccess.CreateDeliverItem();

        private DeliverItem()
        {
        }

        public static bool Add(DeliverItemInfo deliverItemInfo)
        {
            bool flag = false;
            IList<OrderItemInfo> infoListByOrderId = OrderItem.GetInfoListByOrderId(deliverItemInfo.OrderId);
            DeliverStatus preparative = DeliverStatus.Preparative;
            int num = 0;
            IList<StockItemInfo> infoList = new List<StockItemInfo>();
            bool flag2 = true;
            foreach (OrderItemInfo info in infoListByOrderId)
            {
                bool flag3 = false;
                if (Product.CharacterIsExists(info.ProductCharacter, ProductCharacter.Practicality))
                {
                    flag3 = true;
                }
                else if ((deliverItemInfo.DeliverDirection != 1) && Product.CharacterIsExists(info.ProductCharacter, ProductCharacter.Card))
                {
                    flag3 = true;
                }
                if (flag3)
                {
                    if (flag2)
                    {
                        num = StockManage.GetMaxId() + 1;
                        flag2 = false;
                    }
                    int amount = info.Amount;
                    if (deliverItemInfo.DeliverDirection == 1)
                    {
                        amount = -amount;
                        Product.AddBuyTimes(info.ProductId, info.TableName);
                    }
                    if (string.IsNullOrEmpty(info.TableName))
                    {
                        Present.AddStocks(info.ProductId, amount);
                    }
                    else
                    {
                        Product.AddStocks(info.ProductId, amount, info.Property);
                    }
                    StockItemInfo item = new StockItemInfo();
                    item.Amount = Math.Abs(amount);
                    item.ItemId = StockItem.GetMaxId() + 1;
                    item.Price = info.TruePrice;
                    item.ProductId = info.ProductId;
                    item.TableName = info.TableName;
                    item.Property = info.Property;
                    item.ProductName = info.ProductName;
                    item.ProductNum = "";
                    item.StockId = num;
                    item.Unit = info.Unit;
                    infoList.Add(item);
                    Product.AddOrderNum(info.ProductId, info.TableName, info.Property, amount);
                }
            }
            if (!flag2)
            {
                StockInfo stockInfo = new StockInfo();
                stockInfo.Inputer = deliverItemInfo.HandlerName;
                stockInfo.InputTime = deliverItemInfo.DeliverDate;
                stockInfo.Remark = "退货";
                stockInfo.StockId = num;
                if (deliverItemInfo.DeliverDirection == 1)
                {
                    stockInfo.StockNum = StockItem.GetShipmentNum();
                    stockInfo.StockType = StockType.Shipment;
                    stockInfo.Remark = "订单" + deliverItemInfo.OrderNum + "发货";
                    preparative = DeliverStatus.Consignment;
                }
                else
                {
                    stockInfo.StockNum = StockItem.GetInStockNum();
                    stockInfo.StockType = StockType.InStock;
                    stockInfo.Remark = "订单" + deliverItemInfo.OrderNum + "退货";
                }
                if (StockManage.Add(stockInfo))
                {
                    StockItem.Add(infoList, stockInfo.StockId);
                }
            }
            if (Order.UpdateDeliverStatus(deliverItemInfo.OrderId, preparative))
            {
                flag = dal.Add(deliverItemInfo);
            }
            return flag;
        }

        public static DeliverItemInfo GetDeliverItemById(int deliverItemId)
        {
            return dal.GetDeliverItemById(deliverItemId);
        }

        public static DeliverItemInfo GetDeliverItemByOrderId(int orderId)
        {
            return dal.GetDeliverItemByOrderId(orderId);
        }

        public static DeliverItemInfo GetDeliverItemByOrderId(int orderId, int deliverDirection)
        {
            return dal.GetDeliverItemByOrderId(orderId, deliverDirection);
        }

        public static ArrayList GetExpressCompannyList()
        {
            return dal.GetExpressCompannyList();
        }

        public static IList<DeliverItemInfo> GetList(int startRowIndexId, int maxNumberRows, int searchType, string keyword, int quickSearch)
        {
            if (searchType == 7)
            {
                keyword = Convert.ToString(DataConverter.CDate(keyword));
            }
            else
            {
                keyword = DataSecurity.FilterBadChar(keyword);
            }
            return dal.GetList(startRowIndexId, maxNumberRows, searchType, keyword, quickSearch);
        }

        public static string GetOutOfStockProduct(int orderId)
        {
            IList<OrderItemInfo> infoListByOrderId = OrderItem.GetInfoListByOrderId(orderId);
            StringBuilder sb = new StringBuilder();
            foreach (OrderItemInfo info in infoListByOrderId)
            {
                if (string.IsNullOrEmpty(info.TableName))
                {
                    PresentInfo presentById = Present.GetPresentById(info.ProductId);
                    if (presentById.Stocks < info.Amount)
                    {
                        StringHelper.AppendString(sb, presentById.PresentName);
                    }
                }
                else
                {
                    ProductInfo productById = Product.GetProductById(info.ProductId, info.TableName);
                    if (string.IsNullOrEmpty(info.Property))
                    {
                        if (productById.Stocks < info.Amount)
                        {
                            StringHelper.AppendString(sb, productById.ProductName);
                        }
                        continue;
                    }
                    ProductDataInfo info4 = ProductData.GetProductDataByPropertyValue(productById.ProductId, productById.TableName, info.Property);
                    if (!info4.IsNull && (info4.Stocks < info.Amount))
                    {
                        StringHelper.AppendString(sb, productById.ProductName + "（" + info.Property + ")");
                    }
                }
            }
            return sb.ToString();
        }

        public static string GetSearchTypeName(int searchType)
        {
            switch (searchType)
            {
                case 1:
                    return "客户名称";

                case 2:
                    return "收货人姓名";

                case 3:
                    return "用户名";

                case 4:
                    return "快递公司";

                case 5:
                    return "快递单号";

                case 6:
                    return "经手人";

                case 7:
                    return "发退货日期";
            }
            return "";
        }

        public static int GetTotalOfDeliverItem(string searchType, string keyword, string quickSearch)
        {
            return dal.GetTotalOfDeliverItem();
        }

        public static void UpdateReceive(int orderId)
        {
            dal.UpdateReceive(orderId);
        }
    }
}

