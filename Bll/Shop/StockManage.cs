namespace EasyOne.Shop
{
    using EasyOne.Common;
    using EasyOne.Enumerations;
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Shop;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class StockManage
    {
        private static readonly IStockManage dal = DataAccess.CreateStockManage();

        private StockManage()
        {
        }

        public static bool Add(StockInfo stockInfo)
        {
            if (stockInfo.StockId <= 0)
            {
                stockInfo.StockId = GetMaxId() + 1;
            }
            return dal.Add(stockInfo);
        }

        public static bool Delete(string stockId)
        {
            if (!DataValidator.IsValidId(stockId))
            {
                return false;
            }
            foreach (string str in stockId.Split(new char[] { ',' }))
            {
                DeleteAndRevertProductStock(DataConverter.CLng(str));
            }
            return (dal.Delete(stockId) && StockItem.DeleteByStockIds(stockId));
        }

        private static void DeleteAndRevertProductStock(int stockId)
        {
            int num = (GetStockInfoById(stockId).StockType == StockType.InStock) ? -1 : 1;
            foreach (StockItemInfo info2 in StockItem.GetListByStockId(stockId))
            {
                Product.AddStocks(info2.ProductId, num * info2.Amount, info2.Property);
            }
        }

        public static IList<StockInfo> GetList(int startRowIndexId, int maxNumberRows, int searchType, string keyword)
        {
            if (searchType == 5)
            {
                DateTime time;
                if (!DateTime.TryParse(keyword, out time))
                {
                    return new List<StockInfo>();
                }
                keyword = time.ToShortDateString();
            }
            return dal.GetList(startRowIndexId, maxNumberRows, searchType, DataSecurity.FilterBadChar(keyword));
        }

        public static int GetMaxId()
        {
            return dal.GetMaxId();
        }

        public static StockInfo GetStockInfoById(int id)
        {
            return dal.GetStockInfoById(id);
        }

        public static int GetTotalOfStock(int startRowIndexId, int maxNumberRows, int searchType, string keyword)
        {
            return dal.GetTotalOfStock();
        }

        public static bool Update(StockInfo stockInfo)
        {
            return dal.Update(stockInfo);
        }
    }
}

