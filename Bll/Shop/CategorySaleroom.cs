namespace EasyOne.Shop
{
    using EasyOne.Common;
    using EasyOne.IDal.Shop;
    using System;
    using System.Collections.Generic;
    using EasyOne.Model.Shop;
    using EasyOne.DalFactory;

    public sealed class CategorySaleroom
    {
        private static readonly IStatistics dal = DataAccess.CreateStatistics();

        private CategorySaleroom()
        {
        }

        public static IList<CategorySaleroomInfo> GetCategorySaleroomList(int startRowIndexId, int maxNumberRows, int orderType, int year, int month, bool isAll)
        {
            DateTime today = DateTime.Today;
            if ((year == 0) || (month == 0))
            {
                isAll = true;
            }
            else
            {
                string str = month.ToString();
                if (str.Length == 1)
                {
                    str = "0" + str;
                }
                today = DataConverter.CDate(year.ToString() + "-" + str + "-01");
            }
            return dal.GetCategorySaleroomList(startRowIndexId, maxNumberRows, orderType, today, isAll);
        }

        public static int GetTotalOfCategorySaleroom(int orderType, int year, int month, bool isAll)
        {
            return dal.GetTotalOfCategorySaleroom();
        }
    }
}

