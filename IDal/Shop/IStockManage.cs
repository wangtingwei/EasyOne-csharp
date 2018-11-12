namespace EasyOne.IDal.Shop
{
    using EasyOne.Model.Shop;
    using System;
    using System.Collections.Generic;

    public interface IStockManage
    {
        bool Add(StockInfo stockInfo);
        bool Delete(string ids);
        IList<StockInfo> GetList(int startRowIndexId, int maxNumberRows, int searchType, string keyword);
        int GetMaxId();
        StockInfo GetStockInfoById(int id);
        int GetTotalOfStock();
        bool Update(StockInfo stockInfo);
    }
}

