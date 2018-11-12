namespace EasyOne.Analytics
{
    using EasyOne.DalFactory;
    using EasyOne.Enumerations;
    using EasyOne.IDal.Analytics;
    using System;
    using System.Collections.Generic;

    public sealed class UserDataReport
    {
        private static Dictionary<StatName, IUserDataReport> s_Dic;

        private UserDataReport()
        {
        }

        public static int Count(StatName sn)
        {
            return GetInstance(sn).Count;
        }

        public static string[] GetDescription(StatName sn)
        {
            string[] strArray = new string[2];
            switch (sn)
            {
                case StatName.UserAddress:
                    strArray[0] = "地址";
                    strArray[1] = "访问者所在地址分析";
                    return strArray;

                case StatName.UserBrowser:
                    strArray[0] = "浏览器";
                    strArray[1] = "访问者所用浏览器分析";
                    return strArray;

                case StatName.UserColor:
                    strArray[0] = "屏幕显示颜色";
                    strArray[1] = "访问者屏幕显示颜色分析";
                    return strArray;

                case StatName.UserIP:
                    strArray[0] = "IP地址";
                    strArray[1] = "访问者IP地址分析";
                    return strArray;

                case StatName.UserKeyword:
                    strArray[0] = "关 键 词";
                    strArray[1] = "访问者搜索关键词分析";
                    return strArray;

                case StatName.UserMozilla:
                    strArray[0] = "USER_AGENT";
                    strArray[1] = "访问者HTTP_USER_AGENT字符串分析";
                    return strArray;

                case StatName.UserRefer:
                    strArray[0] = "链接页面";
                    strArray[1] = "访问者链接页面分析";
                    return strArray;

                case StatName.UserScreen:
                    strArray[0] = "屏幕大小";
                    strArray[1] = "访问者屏幕大小分析";
                    return strArray;

                case StatName.UserSystem:
                    strArray[0] = "操作系统";
                    strArray[1] = "访问者所用操作系统分析";
                    return strArray;

                case StatName.UserTimezone:
                    strArray[0] = "时区";
                    strArray[1] = "访问者所处时区分析";
                    return strArray;

                case StatName.UserWeburl:
                    strArray[0] = "来访网站";
                    strArray[1] = "访问者来访网站分析";
                    return strArray;
            }
            return strArray;
        }

        private static IUserDataReport GetInstance(StatName sn)
        {
            if (s_Dic == null)
            {
                s_Dic = new Dictionary<StatName, IUserDataReport>();
            }
            if (s_Dic.ContainsKey(sn))
            {
                return s_Dic[sn];
            }
            IUserDataReport report = DataAccess.CreateUserDataReport(sn.ToString());
            s_Dic.Add(sn, report);
            return report;
        }

        public static Dictionary<string, int> GetList(int startRowIndexId, int maxiNumRows, StatName sn)
        {
            return GetInstance(sn).GetList(startRowIndexId, maxiNumRows);
        }

        public static string MaxValue(StatName sn)
        {
            return GetInstance(sn).MaxValue;
        }

        public static int Sum(StatName sn)
        {
            return GetInstance(sn).Sum;
        }
    }
}

