namespace EasyOne.Accessories
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.IDal.Accessories;
    using EasyOne.Model.Accessories;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class DownloadError
    {
        private static readonly IDownloadError dal = DataAccess.CreateDownloadError();

        private DownloadError()
        {
        }

        public static bool Add(DownloadErrorInfo downloadErrorInfo)
        {
            bool flag = false;
            if (downloadErrorInfo.IsNull)
            {
                return flag;
            }
            if (Exists(downloadErrorInfo.InfoId, downloadErrorInfo.ErrorUrl))
            {
                return UpdateErrorTimes(downloadErrorInfo.InfoId, downloadErrorInfo.ErrorUrl);
            }
            return dal.Add(downloadErrorInfo);
        }

        public static bool Clear()
        {
            return dal.Clear();
        }

        public static bool Delete(string errorId)
        {
            if (!DataValidator.IsValidId(errorId))
            {
                return false;
            }
            return dal.Delete(errorId);
        }

        public static bool Exists(int infoId, string errorInformation)
        {
            return dal.Exists(infoId, errorInformation);
        }

        public static IList<DownloadErrorInfo> GetDownloadErrorList(int startRowIndexId, int maxiNumRows, string searchType, string keyword)
        {
            string str = DataSecurity.FilterBadChar(searchType);
            string str2 = DataSecurity.FilterBadChar(keyword);
            return dal.GetDownloadErrorList(startRowIndexId, maxiNumRows, str, str2);
        }

        public static string GetDownloadurlById(string downloadUrls, int urlId, int serverId)
        {
            if ((downloadUrls.Length == 0) || (urlId <= 0))
            {
                return "";
            }
            string str2 = "";
            if (!string.IsNullOrEmpty(downloadUrls))
            {
                int num = 0;
                foreach (string str3 in downloadUrls.Split(new string[] { "$$$" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    string[] strArray2 = str3.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                    if (num == urlId)
                    {
                        str2 = strArray2[1];
                    }
                    num++;
                }
            }
            DownServerInfo downServerById = DownServer.GetDownServerById(serverId);
            if (downServerById.IsNull)
            {
                if (str2.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
                {
                    return str2;
                }
                return (SiteConfig.SiteInfo.VirtualPath + SiteConfig.SiteOption.UploadDir + "/" + str2);
            }
            string str4 = downServerById.ServerUrl.ToString();
            if (!str4.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
            {
                str4 = "http://" + str4;
            }
            if (!str4.EndsWith("/", StringComparison.OrdinalIgnoreCase))
            {
                str4 = str4 + "/";
            }
            if (str2.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
            {
                return str2;
            }
            return (str4 + str2);
        }

        public static int GetTotalOfDownloadError(string searchType, string keyword)
        {
            if (string.IsNullOrEmpty(searchType) || string.IsNullOrEmpty(keyword))
            {
                return dal.GetTotalOfDownloadError();
            }
            return dal.GetTotalOfDownloadError();
        }

        public static bool UpdateErrorTimes(int infoId, string errorInformation)
        {
            return dal.UpdateErrorTimes(infoId, errorInformation);
        }
    }
}

