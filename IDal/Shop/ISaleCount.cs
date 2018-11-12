namespace EasyOne.IDal.Shop
{
    using System;
    using System.Collections.Generic;
    using EasyOne.Model.Shop;

    public interface ISaleCount
    {
        IList<SaleCountInfo> GetSaleCountList(int startRowIndexId, int maxNumberRows, string infoType, string searchType, string keyword, int orderType);
        int GetTotalOfSaleCount();
    }
}

