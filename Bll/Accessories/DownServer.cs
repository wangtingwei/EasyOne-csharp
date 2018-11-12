namespace EasyOne.Accessories
{
    using EasyOne.Common;
    using EasyOne.IDal.Accessories;
    using EasyOne.Model.Accessories;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class DownServer
    {
        private static readonly IDownServer dal = DataAccess.CreateDownServer();

        private DownServer()
        {
        }

        public static bool Add(DownServerInfo downServerInfo)
        {
            return dal.Add(downServerInfo);
        }

        public static bool Delete(int serverId)
        {
            return dal.Delete(serverId);
        }

        public static Uri GetDownloadUrlByServerId(int serverId, string downloadName)
        {
            return dal.GetDownloadUrlByServerId(serverId, downloadName);
        }

        public static DownServerInfo GetDownServerById(int serverId)
        {
            return dal.GetDownServerById(serverId);
        }

        public static IList<DownServerInfo> GetDownServerList()
        {
            return dal.GetDownServerList();
        }

        public static int GetMaxOrderId()
        {
            return dal.GetMaxOrderId();
        }

        public static void OrderDownServer(IList<DownServerInfo> list)
        {
            List<DownServerInfo> list2 = (List<DownServerInfo>) list;
            list2.Sort();
            foreach (DownServerInfo info in list2)
            {
                Update(info);
            }
        }

        public static bool SetShowType(string serverId, int showType)
        {
            if (!DataValidator.IsValidId(serverId))
            {
                return false;
            }
            return dal.SetShowType(serverId, showType);
        }

        public static bool Update(DownServerInfo downServerInfo)
        {
            return dal.Update(downServerInfo);
        }
    }
}

