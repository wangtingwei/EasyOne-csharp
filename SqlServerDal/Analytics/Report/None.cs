namespace EasyOne.SqlServerDal.Analytics.Report
{
    using EasyOne.IDal.Analytics;
    using System;
    using System.Collections.Generic;

    public class None : IUserDataReport, ITimeReport
    {
        public int[] GetAllList()
        {
            return new int[1];
        }

        public int[] GetList(string value)
        {
            return new int[1];
        }

        public Dictionary<string, int> GetList(int startRowIndexId, int maxiNumRows)
        {
            return new Dictionary<string, int>();
        }

        public int Count
        {
            get
            {
                return 0;
            }
        }

        public string MaxValue
        {
            get
            {
                return "无";
            }
        }

        public int Sum
        {
            get
            {
                return 0;
            }
        }
    }
}

