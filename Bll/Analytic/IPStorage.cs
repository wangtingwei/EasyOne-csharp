namespace EasyOne.Analytics
{
    using EasyOne.Common;
    using EasyOne.IDal.Analytics;
    using EasyOne.Model.Analytics;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class IPStorage
    {
        private static readonly IIPStorage dal = DataAccess.CreateIPStorage();

        private IPStorage()
        {
        }

        public static bool Add(StatIPInfo info)
        {
            return dal.Add(info);
        }

        public static bool Delete(StatIPInfo info)
        {
            return dal.Delete(info);
        }

        public static string GetAddressByIP(double ip)
        {
            return GetAddressByIP(ip);
        }

        public static IList<StatIPInfo> GetList(int startRowIndexId, int maxiNumRows, string searchIP, string searchAddress)
        {
            double num = DataValidator.IsIP(searchIP) ? StringHelper.EncodeIP(searchIP) : 0.0;
            string str = string.IsNullOrEmpty(searchAddress) ? string.Empty : DataSecurity.FilterBadChar(searchAddress.Trim());
            return dal.GetList(startRowIndexId, maxiNumRows, num, str);
        }

        public static int GetTotal()
        {
            return dal.GetTotal();
        }

        public static int GetTotal(string searchIP, string searchAddress)
        {
            return GetTotal();
        }

        public static bool Update(StatIPInfo newInfo, StatIPInfo oldInfo)
        {
            return dal.Update(newInfo, oldInfo);
        }
    }
}

