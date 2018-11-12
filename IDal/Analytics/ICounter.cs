namespace EasyOne.IDal.Analytics
{
    using EasyOne.Model.Analytics;
    using System;

    public interface ICounter
    {
        bool DoInit();
        int GetInterval();
        bool SaveConfig(StatInfoListInfo info);
        void StatInfoListAddView();
        void StatOnlineAdd(StatOnlineInfo info);
        void StatUpdate(StatUpdateInfo updateInfo, string address);
        bool StatVisitorAdd(StatVisitorInfo statVisitorInfo);
        bool VisitUpdate(int visitCount);
    }
}

