namespace EasyOne.IDal.Analytics
{
    using EasyOne.Model.Analytics;
    using System;
    using System.Collections.Generic;

    public interface IOtherReport
    {
        int GetCurrentOnlineCount();
        StatOnlineInfo GetOnlineByIP(string ip);
        StatInfoListInfo GetStatInfoListInfo();
        IList<StatOnlineInfo> GetStatOnlineList(int startRowIndexId, int maxiNumRows);
        StatVisitorInfo GetStatVisitorById(int id);
        IList<StatVisitorInfo> GetStatVisitorList(int startRowIndexId, int maxNumberRows);
        int GetTotalOfStatVisitor();
        int GetTotalStatOnline();
        int[] GetVisitList();
    }
}

