namespace EasyOne.Analytics
{
    using EasyOne.DalFactory;
    using EasyOne.Enumerations;
    using EasyOne.IDal.Analytics;
    using System;
    using System.Collections.Generic;

    public sealed class TimeReport
    {
        private static Dictionary<StatName, ITimeReport> s_Dic;

        private TimeReport()
        {
        }

        public static int[] GetAllList(StatName sn)
        {
            return GetInstance(sn).GetAllList();
        }

        private static ITimeReport GetInstance(StatName sn)
        {
            if (s_Dic == null)
            {
                s_Dic = new Dictionary<StatName, ITimeReport>();
            }
            if (s_Dic.ContainsKey(sn))
            {
                return s_Dic[sn];
            }
            ITimeReport report = DataAccess.CreateTimeReport(sn.ToString());
            s_Dic.Add(sn, report);
            return report;
        }

        public static int[] GetList(StatName sn, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                switch (sn)
                {
                    case StatName.Year:
                        value = DateTime.Today.Year.ToString();
                        break;

                    case StatName.Month:
                        value = DateTime.Today.ToString("yyyy-MM");
                        break;

                    case StatName.Day:
                        value = DateTime.Today.ToString("yyyy-MM-dd");
                        break;
                }
            }
            return GetInstance(sn).GetList(value);
        }
    }
}

