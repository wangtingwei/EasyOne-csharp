namespace EasyOne.Shop
{
    using EasyOne.IDal.Shop;
    using System;
    using System.Collections.Generic;
    using EasyOne.Model.Shop;
    using EasyOne.DalFactory;

    public sealed class CompareHitsOrderNumber
    {
        private static readonly IStatistics dal = DataAccess.CreateStatistics();

        private CompareHitsOrderNumber()
        {
        }

        public static IList<CompareHitsOrderNumberInfo> GetCompareHitsOrderNumberList(int startRowIndexId, int maxNumberRows, int orderType)
        {
            return dal.GetCompareHitsOrderNumberList(startRowIndexId, maxNumberRows, orderType);
        }

        public static int GetTotalOfCompareHitsOrderNumber(int orderType)
        {
            return dal.GetTotalOfCompareHitsOrderNumber();
        }
    }
}

