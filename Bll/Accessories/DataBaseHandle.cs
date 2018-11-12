namespace EasyOne.Accessories
{
    using EasyOne.Components;
    using EasyOne.IDal.Accessories;
    using EasyOne.Model.Accessories;
    using System;
    using System.Globalization;
    using EasyOne.DalFactory;

    public sealed class DataBaseHandle
    {
        private static readonly IDataBaseHandle dal = DataAccess.CreateDataBaseHandle();

        private DataBaseHandle()
        {
        }

        public static string CurrentVersion()
        {
            string str = "0.0.0.0";
            string key = "CK_System_DataBaseCurrentVersion";
            if (SiteCache.Get(key) == null)
            {
                DataBaseVersionInfo info = LastVersion();
                str = info.Major.ToString(CultureInfo.CurrentCulture) + "." + info.Minor.ToString(CultureInfo.CurrentCulture) + "." + info.Build.ToString(CultureInfo.CurrentCulture) + "." + info.Revision.ToString(CultureInfo.CurrentCulture);
                SiteCache.Insert(key, str, 0x4380);
                return str;
            }
            return SiteCache.Get(key).ToString();
        }

        public static DataBaseVersionInfo LastVersion()
        {
            return dal.LastVersion();
        }
    }
}

