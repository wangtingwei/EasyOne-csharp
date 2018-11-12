namespace EasyOne.Analytics
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.IDal.Analytics;
    using EasyOne.Model.Analytics;
    using System;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using EasyOne.DalFactory;

    public sealed class Counter
    {
        private static readonly ICounter dal = DataAccess.CreateCounter();
        private static readonly string[][] s_Enginers = new string[][] { 
            new string[] { "google", "utf8", "q" }, new string[] { "baidu", "gb2312", "wd" }, new string[] { "cn.yahoo", "gb2312", "p" }, new string[] { "yahoo.cn", "gb2312", "p" }, new string[] { "yahoo", "utf8", "p" }, new string[] { "yisou", "utf8", "search" }, new string[] { "live", "utf8", "q" }, new string[] { "tom", "utf8", "w" }, new string[] { "yodao", "gb2312", "q" }, new string[] { "163", "gb2312", "q" }, new string[] { "iask", "gb2312", "k" }, new string[] { "soso", "gb2312", "w" }, new string[] { "sogou", "gb2312", "query" }, new string[] { "zhongsou", "gb2312", "w" }, new string[] { "3721", "gb2312", "p" }, new string[] { "openfind", "utf8", "q" }, 
            new string[] { "alltheweb", "utf8", "q" }, new string[] { "lycos", "utf8", "query" }, new string[] { "onseek", "utf8", "query" }
         };

        private Counter()
        {
        }

        public static bool DoInit()
        {
            return dal.DoInit();
        }

        public static string FindKeyword(string input)
        {
            string str = string.Empty;
            string str2 = string.Empty;
            string str3 = string.Empty;
            string pattern = string.Empty;
            foreach (string[] strArray in s_Enginers)
            {
                if (input.Contains(strArray[0]))
                {
                    str = strArray[0];
                    str2 = strArray[1];
                    if ((str == "google") && input.Contains("ie=gb"))
                    {
                        str2 = "gb2312";
                    }
                    str3 = strArray[2];
                    pattern = "(" + str + @"\.*.*[?/&]" + str3 + "[=:])(?<key>[^&]*)";
                    break;
                }
            }
            if (!string.IsNullOrEmpty(str))
            {
                input = Regex.Match(input, pattern, RegexOptions.None).Groups["key"].Value;//将1改为None
                input = input.Replace("+", " ");
                if (str2 == "gb2312")
                {
                    input = GetUTF8String(input);
                    return input;
                }
                try
                {
                    input = Uri.UnescapeDataString(input);
                    if (!(str == "alltheweb") && !(str == "onseek"))
                    {
                        return input;
                    }
                    input = HttpContext.Current.Server.HtmlDecode(input);
                }
                catch (UriFormatException)
                {
                    input = string.Empty;
                }
                return input;
            }
            input = string.Empty;
            return input;
        }

        private static string GB2312ToUTF8(string input)
        {
            string[] strArray = input.Split(new char[] { '%' });
            byte[] bytes = new byte[] { Convert.ToByte(strArray[1], 0x10), Convert.ToByte(strArray[2], 0x10) };
            Encoding srcEncoding = Encoding.GetEncoding("GB2312");
            Encoding dstEncoding = Encoding.UTF8;
            bytes = Encoding.Convert(srcEncoding, dstEncoding, bytes);
            char[] chars = new char[dstEncoding.GetCharCount(bytes, 0, bytes.Length)];
            dstEncoding.GetChars(bytes, 0, bytes.Length, chars, 0);
            return new string(chars);
        }

        public static string GetColor(string refColor)
        {
            switch (refColor)
            {
                case "4":
                    return "16 色";

                case "8":
                    return "256 色";

                case "16":
                    return "增强色（16位）";

                case "24":
                    return "真彩色（24位）";

                case "32":
                    return "真彩色（32位）";
            }
            return "其它";
        }

        public static int GetInterval()
        {
            return dal.GetInterval();
        }

        public static string GetIP(HttpRequest request)
        {
            string userHostAddress = string.Empty;
            if (request != null)
            {
                if (request.ServerVariables["HTTP_VIA"] != null)
                {
                    userHostAddress = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                }
                else
                {
                    userHostAddress = request.ServerVariables["REMOTE_ADDR"];
                }
                if (string.IsNullOrEmpty(userHostAddress))
                {
                    userHostAddress = request.UserHostAddress;
                }
            }
            return userHostAddress;
        }

        public static string GetItemName(string regField)
        {
            string str = string.Empty;
            switch (regField)
            {
                case "FVisit":
                    return "访问次数";

                case "FIP":
                    return "IP 地 址";

                case "FAddress":
                    return "地址分析";

                case "FTimezone":
                    return "时区分析";

                case "FKeyword":
                    return "关 键 词";

                case "FWeburl":
                    return "来访网站";

                case "FRefer":
                    return "链接页面";

                case "FSystem":
                    return "操作系统";

                case "FBrowser":
                    return "浏 览 器";

                case "FMozilla":
                    return "字串分析";

                case "FScreen":
                    return "屏幕大小";

                case "FColor":
                    return "屏幕色深";

                case "IsCountOnline":
                    return "在线用户";
            }
            return str;
        }

        public static string GetScreen(string width, string height)
        {
            if ((DataConverter.CLng(height) == 0) || (DataConverter.CLng(width) == 0))
            {
                return "其它";
            }
            return (width + "x" + height);
        }

        public static string GetSystemType(HttpRequest request)
        {
            string str = "其它";
            if (string.IsNullOrEmpty(request.Browser.Platform))
            {
                return str;
            }
            if (request.UserAgent.Contains("NT 6.0"))
            {
                return "Windows Vista";
            }
            if (request.UserAgent.Contains("NT 5.2"))
            {
                return "Windows 2003";
            }
            if (request.UserAgent.Contains("NT 5.1"))
            {
                return "Windows XP";
            }
            if (request.UserAgent.Contains("NT 5"))
            {
                return "Windows 2000";
            }
            if (request.UserAgent.Contains("NT 4.9"))
            {
                return "Windows ME";
            }
            if (request.UserAgent.Contains("NT 4"))
            {
                return "Windows NT4";
            }
            if (request.UserAgent.Contains("NT 98"))
            {
                return "Windows 98";
            }
            if (request.UserAgent.Contains("NT 95"))
            {
                return "Windows 95";
            }
            return request.Browser.Platform.Replace("Win", "Windows").Replace("dowsdows", "dows");
        }

        private static string GetUTF8String(string input)
        {
            MatchCollection matchs = Regex.Matches(input, "(?<key>%..%..)", RegexOptions.IgnoreCase);
            int num = 0;
            int count = matchs.Count;
            while (num < count)
            {
                string oldValue = matchs[num].Groups["key"].Value.ToString();
                input = input.Replace(oldValue, GB2312ToUTF8(oldValue));
                num++;
            }
            return input;
        }

        public static bool SaveConfig(StatInfoListInfo info)
        {
            return dal.SaveConfig(info);
        }

        public static void StatInfoListAddView()
        {
            dal.StatInfoListAddView();
        }

        public static void StatOnlineAdd(StatOnlineInfo info)
        {
            dal.StatOnlineAdd(info);
        }

        public static void StatUpdate(StatUpdateInfo updateInfo)
        {
            dal.StatUpdate(updateInfo, IPScanner.IPLocation(updateInfo.IP));
        }

        public static bool StatVisitorAdd()
        {
            HttpRequest request = HttpContext.Current.Request;
            StatVisitorInfo statVisitorInfo = new StatVisitorInfo();
            statVisitorInfo.VTime = DateTime.Now;
            statVisitorInfo.IP = PEContext.Current.UserHostAddress;
            statVisitorInfo.Referer = request.ServerVariables["HTTP_REFERER"];
            statVisitorInfo.Browser = request.Browser.Browser + request.Browser.Version;
            statVisitorInfo.System = string.IsNullOrEmpty(request.Browser.Platform) ? "其它" : request.Browser.Platform;
            statVisitorInfo.Timezone = 8;
            statVisitorInfo.Screen = "";
            statVisitorInfo.Address = IPScanner.IPLocation(PEContext.Current.UserHostAddress);
            statVisitorInfo.Color = "";
            return dal.StatVisitorAdd(statVisitorInfo);
        }

        public static bool VisitUpdate(int visitCount)
        {
            return dal.VisitUpdate(visitCount);
        }
    }
}

