namespace EasyOne.IDal.Shop
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using EasyOne.Model.Shop;

    public interface ISaleList
    {
        IList<SaleListInfo> GetSaleList(int startRowIndexId, int maxNumberRows, int searchType, int field, string keyword);
        ArrayList GetSumSubTotalAndExp();
        int GetTotalOfSaleList();
    }
}

