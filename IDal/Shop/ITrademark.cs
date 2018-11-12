namespace EasyOne.IDal.Shop
{
    using EasyOne.Model.Shop;
    using System;
    using System.Collections.Generic;

    public interface ITrademark
    {
        bool Add(TrademarkInfo trademarkInfo);
        bool Delete(string trademarkId);
        IList<TrademarkInfo> GetList(int producerId);
        IList<TrademarkInfo> GetList(int startRowIndexId, int maxNumberRows, string searchType, string keyword, int trademarkType, bool isPassed);
        int GetTotalOfTrademark();
        TrademarkInfo GetTrademarkById(int trademarkId);
        bool SetElite(int trademarkId, bool value);
        bool SetOnTop(int trademarkId, bool value);
        bool SetPassed(int trademarkId, bool value);
        bool TrademarkNameExists(string trademarkName);
        bool Update(TrademarkInfo trademarkInfo);
    }
}

