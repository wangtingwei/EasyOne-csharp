namespace EasyOne.IDal.Shop
{
    using EasyOne.Model.Shop;
    using System;
    using System.Collections.Generic;

    public interface IStockItem
    {
        bool Add(StockItemInfo stockItemInfo);
        bool Delete(int productId, string tableName);
        bool DeleteByStockIds(string stockIds);
        IList<StockItemInfo> GetListByStockId(int stockId);
        IList<StockItemInfo> GetListByStockId(int startRowIndexId, int maxNumberRows, int stockId);
        int GetMaxId();
        int GetTotalOfStockItem();
    }
}

