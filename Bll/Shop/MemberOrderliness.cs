namespace EasyOne.Shop
{
    using EasyOne.Common;
    using EasyOne.IDal.Shop;
    using System;
    using System.Collections.Generic;
    using EasyOne.Model.Shop;
    using EasyOne.DalFactory;

    public sealed class MemberOrderliness
    {
        private static readonly IStatistics dal = DataAccess.CreateStatistics();

        private MemberOrderliness()
        {
        }

        public static IList<MemberOrderlinessInfo> GetMemberOrderlinessList(int startRowIndexId, int maxNumberRows, string userName, int year, int month, bool isAll)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return new List<MemberOrderlinessInfo>();
            }
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
            return dal.GetMemberOrderlinessList(startRowIndexId, maxNumberRows, DataSecurity.FilterBadChar(userName), today, isAll);
        }

        public static int GetTotalOfMemberOrderliness(string userName, int year, int month, bool isAll)
        {
            return dal.GetTotalOfMemberOrderliness();
        }
    }
}

