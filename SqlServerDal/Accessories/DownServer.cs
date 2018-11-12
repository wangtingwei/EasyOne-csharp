namespace EasyOne.SqlServerDal.Accessories
{
    using EasyOne.Components;
    using EasyOne.IDal.Accessories;
    using EasyOne.Model.Accessories;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;

    public class DownServer : IDownServer
    {
        public bool Add(DownServerInfo downServerInfo)
        {
            Parameters parms = new Parameters();
            downServerInfo.ServerId = DBHelper.GetMaxId("PE_DownServer", "ServerID") + 1;
            downServerInfo.OrderId = DBHelper.GetMaxId("PE_DownServer", "OrderId") + 1;
            GetDownServerParameters(downServerInfo, parms);
            return DBHelper.ExecuteProc("PR_Accessories_DownServer_Add", parms);
        }

        public bool Delete(int serverId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ServerID", DbType.Int32, serverId);
            return DBHelper.ExecuteProc("PR_Accessories_DownServer_Delete", cmdParams);
        }

        private static DownServerInfo DownServerInfoFromrdr(NullableDataReader rdr)
        {
            DownServerInfo info = new DownServerInfo();
            info.ServerId = rdr.GetInt32("ServerID");
            info.ServerName = rdr.GetString("ServerName");
            info.ServerUrl = new Uri(rdr.GetString("ServerUrl"));
            info.ServerLogo = rdr.GetString("ServerLogo");
            info.OrderId = rdr.GetInt32("OrderID");
            info.ShowType = rdr.GetInt32("ShowType");
            return info;
        }

        public Uri GetDownloadUrlByServerId(int serverId, string downloadName)
        {
            string uriString = "";
            if (SiteConfig.SiteInfo.SiteUrl.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
            {
                uriString = SiteConfig.SiteInfo.SiteUrl;
            }
            else
            {
                uriString = "http://www.EasyOne.net/";
            }
            Uri uri = new Uri(uriString);
            StringBuilder builder = new StringBuilder();
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ServerID", DbType.Int32, serverId);
            using (NullableDataReader reader = DBHelper.ExecuteReaderProc("PR_Accessories_DownServer_GetServerUrlByServerId", cmdParams))
            {
                if (reader.Read())
                {
                    builder.Append(reader.GetString("ServerUrl"));
                    builder.Append(downloadName);
                }
            }
            if (builder.ToString().Length != 0)
            {
                uri = new Uri(builder.ToString());
            }
            return uri;
        }

        public DownServerInfo GetDownServerById(int serverId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ServerID", DbType.Int32, serverId);
            using (NullableDataReader reader = DBHelper.ExecuteReaderProc("PR_Accessories_DownServer_GetById", cmdParams))
            {
                if (reader.Read())
                {
                    return DownServerInfoFromrdr(reader);
                }
                return new DownServerInfo(true);
            }
        }

        public IList<DownServerInfo> GetDownServerList()
        {
            IList<DownServerInfo> list = new List<DownServerInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderProc("PR_Accessories_DownServer_GetList"))
            {
                while (reader.Read())
                {
                    list.Add(DownServerInfoFromrdr(reader));
                }
            }
            return list;
        }

        private static void GetDownServerParameters(DownServerInfo downServerInfo, Parameters parms)
        {
            parms.AddInParameter("@ServerID", DbType.Int32, downServerInfo.ServerId);
            parms.AddInParameter("@ServerName", DbType.String, downServerInfo.ServerName);
            parms.AddInParameter("@ServerUrl", DbType.String, downServerInfo.ServerUrl.OriginalString);
            parms.AddInParameter("@ServerLogo", DbType.String, downServerInfo.ServerLogo);
            parms.AddInParameter("@OrderID", DbType.Int32, downServerInfo.OrderId);
            parms.AddInParameter("@ShowType", DbType.Int32, downServerInfo.ShowType);
        }

        public int GetMaxOrderId()
        {
            return DBHelper.GetMaxId("PE_DownServer", "OrderId");
        }

        public bool SetShowType(string serverId, int showType)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ShowType", DbType.Int32, showType);
            return DBHelper.ExecuteSql("UPDATE PE_DownServer SET ShowType=@ShowType WHERE ServerId IN(" + serverId + ")", cmdParams);
        }

        public bool Update(DownServerInfo downServerInfo)
        {
            Parameters parms = new Parameters();
            GetDownServerParameters(downServerInfo, parms);
            return DBHelper.ExecuteProc("PR_Accessories_DownServer_Update", parms);
        }
    }
}

