namespace EasyOne.Shop
{
    using EasyOne.IDal.Shop;
    using System;
    using System.Collections.Generic;
    using EasyOne.Model.Shop;
    using EasyOne.DalFactory;

    public sealed class MemberOrders
    {
        private static readonly IStatistics dal = DataAccess.CreateStatistics();

        private MemberOrders()
        {
        }

        public static IList<MemberOrdersInfo> GetMemberOrdersList(int startRowIndexId, int maxNumberRows)
        {
            return dal.GetMemberOrdersList(startRowIndexId, maxNumberRows);
        }

        public static int GetTotalOfMemberOrders()
        {
            return dal.GetTotalOfMemberOrders();
        }
    }
}

