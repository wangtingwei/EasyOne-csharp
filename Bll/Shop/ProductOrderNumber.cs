namespace EasyOne.Shop
{
    using EasyOne.IDal.Shop;
    using System;
    using System.Collections.Generic;
    using EasyOne.Model.Shop;
    using EasyOne.DalFactory;

    public sealed class ProductOrderNumber
    {
        private static readonly IStatistics dal = DataAccess.CreateStatistics();

        private ProductOrderNumber()
        {
        }

        public static IList<ProductOrderNumberInfo> GetProductOrderNumberList(int startRowIndexId, int maxNumberRows)
        {
            return dal.GetProductOrderNumberList(startRowIndexId, maxNumberRows);
        }

        public static int GetTotalOfProductOrderNumber()
        {
            return dal.GetTotalOfProductOrderNumber();
        }
    }
}

