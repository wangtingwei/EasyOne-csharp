namespace EasyOne.IDal.Shop
{
    using EasyOne.Enumerations;
    using EasyOne.Model.Shop;
    using System;
    using System.Collections.Generic;

    public interface IPresent
    {
        bool AddPresent(PresentInfo info);
        bool AddStocks(int id, int quantity);
        bool DeletePresents(string idList);
        IList<PresentInfo> GetPresentByCharacter(ProductCharacter productCharacter);
        PresentInfo GetPresentById(int id);
        PresentInfo GetPresentByPresentNum(string presentNum);
        IList<PresentInfo> GetPresentList(int startRowIndexId, int maxNumberRows);
        IList<PresentInfo> GetPresentList(int startRowIndexId, int maxNumberRows, int searchType, string keyword);
        string GetPresentNameById(int id);
        int GetPresentTotal();
        IList<string> GetUnitList();
        bool UpdatePressent(PresentInfo info);
    }
}

