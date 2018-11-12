namespace EasyOne.Shop
{
    using EasyOne.Common;
    using EasyOne.IDal.Shop;
    using System;
    using System.Collections.Generic;
    using EasyOne.Model.Shop;
    using EasyOne.DalFactory;

    public sealed class SaleCount
    {
        private static readonly ISaleCount dal = DataAccess.CreateSaleCount();

        private SaleCount()
        {
        }

        public static IList<SaleCountInfo> GetSaleCountList(int startRowIndexId, int maxNumberRows, string infoType, string searchType, string keyword, int orderType)
        {
            return dal.GetSaleCountList(startRowIndexId, maxNumberRows, infoType, searchType, DataSecurity.FilterBadChar(keyword), orderType);
        }

        public static int GetTotalOfSaleCount(string infoType, string searchType, string keyword, int orderType)
        {
            return dal.GetTotalOfSaleCount();
        }
    }
}

