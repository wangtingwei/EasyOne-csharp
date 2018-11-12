namespace EasyOne.IDal.Analytics
{
    using EasyOne.Model.Analytics;
    using System;
    using System.Collections.Generic;

    public interface IIPStorage
    {
        bool Add(StatIPInfo info);
        bool Delete(StatIPInfo info);
        string GetAddressByIP(double ip);
        IList<StatIPInfo> GetList(int startRowIndexId, int maxiNumRows, double searchIP, string searchAddress);
        int GetTotal();
        bool Update(StatIPInfo newInfo, StatIPInfo oldInfo);
    }
}

