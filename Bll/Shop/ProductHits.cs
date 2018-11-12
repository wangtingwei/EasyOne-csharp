namespace EasyOne.Shop
{
    using EasyOne.IDal.Shop;
    using System;
    using System.Collections.Generic;
    using EasyOne.Model.Shop;
    using EasyOne.DalFactory;

    public sealed class ProductHits
    {
        private static readonly IStatistics dal = DataAccess.CreateStatistics();

        private ProductHits()
        {
        }

        public static IList<ProductHitsInfo> GetProductHitsList(int startRowIndexId, int maxNumberRows)
        {
            return dal.GetProductHitsList(startRowIndexId, maxNumberRows);
        }

        public static int GetTotalOfProductHits()
        {
            return dal.GetTotalOfProductHits();
        }
    }
}

