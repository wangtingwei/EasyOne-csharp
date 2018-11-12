namespace EasyOne.Shop
{
    using EasyOne.Common;
    using EasyOne.Contents;
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Contents;
    using EasyOne.Model.Shop;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class StockItem
    {
        private static readonly IStockItem dal = DataAccess.CreateStockItem();

        private StockItem()
        {
        }

        public static bool Add(StockItemInfo info, int stockId)
        {
            info.ItemId = GetMaxId() + 1;
            info.StockId = stockId;
            return dal.Add(info);
        }

        public static void Add(IList<StockItemInfo> infoList, int stockId)
        {
            foreach (StockItemInfo info in infoList)
            {
                Add(info, stockId);
            }
        }

        public static bool Delete(int generalId)
        {
            CommonModelInfo commonModelInfoById = ContentManage.GetCommonModelInfoById(generalId);
            return (((!commonModelInfoById.IsNull && commonModelInfoById.IsEshop) && (commonModelInfoById.LinkType == 0)) && dal.Delete(commonModelInfoById.ItemId, commonModelInfoById.TableName));
        }

        public static bool Delete(int productId, string tableName)
        {
            return dal.Delete(productId, tableName);
        }

        public static bool DeleteByStockIds(string stockIds)
        {
            return (DataValidator.IsValidId(stockIds) && dal.DeleteByStockIds(stockIds));
        }

        public static string GetInStockNum()
        {
            return ("RK" + DateTime.Now.ToString("yyyyMMddHHmmssff"));
        }

        public static IList<StockItemInfo> GetListByStockId(int stockId)
        {
            return dal.GetListByStockId(stockId);
        }

        public static IList<StockItemInfo> GetListByStockId(int startRowIndexId, int maxNumberRows, int stockId)
        {
            return dal.GetListByStockId(startRowIndexId, maxNumberRows, stockId);
        }

        public static int GetMaxId()
        {
            return dal.GetMaxId();
        }

        public static string GetShipmentNum()
        {
            return ("CK" + DateTime.Now.ToString("yyyyMMddHHmmssff"));
        }

        public static int GetTotalOfStockItem(int startRowIndexId, int maxNumberRows, int stockId)
        {
            return dal.GetTotalOfStockItem();
        }

        public static void Update(IList<StockItemInfo> infoList, int stockId)
        {
            if (DeleteByStockIds(stockId.ToString()))
            {
                Add(infoList, stockId);
            }
        }
    }
}

