namespace EasyOne.Common
{
    using System;
    using System.Net;
    using System.Net.Sockets;

    public static class Utility
    {
        public static string ConvertAbsolutePath(string virtualPath, string weburl)
        {
            if (string.IsNullOrEmpty(weburl))
            {
                return string.Empty;
            }
            if (!weburl.Contains("://") && !weburl.StartsWith("/", StringComparison.Ordinal))
            {
                return DataSecurity.UrlEncode(virtualPath + weburl);
            }
            return DataSecurity.UrlEncode(weburl);
        }

        public static string ConvertAbsolutePath(string virtualPath, string weburl, bool isSEncode)
        {
            if (!isSEncode)
            {
                return ConvertAbsolutePath(virtualPath, weburl);
            }
            if (string.IsNullOrEmpty(weburl))
            {
                return string.Empty;
            }
            if (!weburl.Contains("://") && !weburl.StartsWith("/", StringComparison.Ordinal))
            {
                return (virtualPath + DataSecurity.UrlEncode(weburl, true));
            }
            return DataSecurity.UrlEncode(weburl, true).Replace("%3a%2f%2f", "://").Replace("%2f", "/");
        }

        public static IPAddress GetHostIP()
        {
            return GetHostIP("");
        }

        public static IPAddress GetHostIP(string hostName)
        {
            IPAddress[] hostAddresses = Dns.GetHostAddresses(hostName);
            IPAddress none = IPAddress.None;
            foreach (IPAddress address2 in hostAddresses)
            {
                if (address2.AddressFamily == AddressFamily.InterNetwork)
                {
                    return address2;
                }
            }
            return none;
        }
    }
}

