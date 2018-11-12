namespace EasyOne.Analytics
{
    using EasyOne.IDal.Analytics;
    using EasyOne.Model.Analytics;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class OtherReport
    {
        private static readonly IOtherReport dal = DataAccess.CreateOtherReport();

        private OtherReport()
        {
        }

        public static int GetCurrentOnlineCount()
        {
            return dal.GetCurrentOnlineCount();
        }

        public static StatOnlineInfo GetOnlineByIP(string ip)
        {
            return dal.GetOnlineByIP(ip);
        }

        public static StatInfoListInfo GetStatInfoListInfo()
        {
            return dal.GetStatInfoListInfo();
        }

        public static IList<StatOnlineInfo> GetStatOnlineList(int startRowIndexId, int maxiNumRows)
        {
            return dal.GetStatOnlineList(startRowIndexId, maxiNumRows);
        }

        public static StatVisitorInfo GetStatVisitorById(int id)
        {
            return dal.GetStatVisitorById(id);
        }

        public static IList<StatVisitorInfo> GetStatVisitorList(int startRowIndexId, int maxNumberRows)
        {
            return dal.GetStatVisitorList(startRowIndexId, maxNumberRows);
        }

        public static int GetTotalOfStatVisitor()
        {
            return dal.GetTotalOfStatVisitor();
        }

        public static int GetTotalStatOnline()
        {
            return dal.GetTotalStatOnline();
        }

        public static int GetTotalStatOnline(int startRowIndexId, int maxiNumRows)
        {
            return GetTotalStatOnline();
        }

        public static int[] GetVisitList()
        {
            return dal.GetVisitList();
        }

        public static int GetVisitorCount()
        {
            int num = 0;
            int[] visitList = GetVisitList();
            if (visitList != null)
            {
                foreach (int num2 in visitList)
                {
                    num += num2;
                }
            }
            return num;
        }
    }
}

