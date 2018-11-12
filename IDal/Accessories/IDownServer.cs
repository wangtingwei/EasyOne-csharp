namespace EasyOne.IDal.Accessories
{
    using EasyOne.Model.Accessories;
    using System;
    using System.Collections.Generic;

    public interface IDownServer
    {
        bool Add(DownServerInfo downServerInfo);
        bool Delete(int serverId);
        Uri GetDownloadUrlByServerId(int serverId, string downloadName);
        DownServerInfo GetDownServerById(int serverId);
        IList<DownServerInfo> GetDownServerList();
        int GetMaxOrderId();
        bool SetShowType(string serverId, int showType);
        bool Update(DownServerInfo downServerInfo);
    }
}

