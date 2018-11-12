namespace EasyOne.Shop
{
    using EasyOne.IDal.Shop;
    using System;
    using System.Collections.Generic;
    using EasyOne.Model.Shop;
    using EasyOne.DalFactory;

    public sealed class MemberExpenditure
    {
        private static readonly IStatistics dal = DataAccess.CreateStatistics();

        private MemberExpenditure()
        {
        }

        public static IList<MemberExpenditureInfo> GetMemberExpenditureList(int startRowIndexId, int maxNumberRows)
        {
            return dal.GetMemberExpenditureList(startRowIndexId, maxNumberRows);
        }

        public static int GetTotalOfMemberExpenditure()
        {
            return dal.GetTotalOfMemberExpenditure();
        }
    }
}

