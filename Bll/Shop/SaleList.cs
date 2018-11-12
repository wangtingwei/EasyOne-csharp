namespace EasyOne.Shop
{
    using EasyOne.Common;
    using EasyOne.IDal.Shop;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using EasyOne.Model.Shop;
    using EasyOne.DalFactory;

    public sealed class SaleList
    {
        private static readonly ISaleList dal = DataAccess.CreateSaleList();

        private SaleList()
        {
        }

        public static IList<SaleListInfo> GetSaleList(int startRowIndex, int maximumRows, int searchType, int field, string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                switch (searchType)
                {
                    case 4:
                        keyword = DataSecurity.FilterBadChar(keyword);
                        break;

                    case 10:
                        if (field == 3)
                        {
                            keyword = DataConverter.CDate(keyword).ToString("yyyy-MM-dd");
                        }
                        else
                        {
                            keyword = DataSecurity.FilterBadChar(keyword);
                        }
                        break;
                }
            }
            return dal.GetSaleList(startRowIndex, maximumRows, searchType, field, keyword);
        }

        public static string GetSaleTypeString(object saleType)
        {
            return Product.ShowProductType(DataConverter.CLng(saleType));
        }

        public static ArrayList GetSumSubTotalAndExp()
        {
            return dal.GetSumSubTotalAndExp();
        }

        public static int GetTotalOfSaleList(int searchType, int field, string keyword)
        {
            keyword = Convert.ToString(searchType + keyword + field);
            string.IsNullOrEmpty(keyword);
            return dal.GetTotalOfSaleList();
        }
    }
}

