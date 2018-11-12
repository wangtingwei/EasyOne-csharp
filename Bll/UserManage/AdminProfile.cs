namespace EasyOne.UserManage
{
    using EasyOne.IDal.UserManage;
    using EasyOne.Model.UserManage;
    using System;
    using EasyOne.DalFactory;

    public sealed class AdminProfile
    {
        private static readonly IAdminProfile dal = DataAccess.CreateAdminProfile();

        private AdminProfile()
        {
        }

        public static void Add(AdminProfileInfo adminProileInfo)
        {
            dal.Add(adminProileInfo);
        }

        public static bool ExistsAdminName(string adminName)
        {
            return dal.ExistsAdminName(adminName);
        }

        public static AdminProfileInfo GetAdminProfile(string adminName)
        {
            return dal.GetAdminProfile(adminName);
        }

        public static void Update(AdminProfileInfo adminProileInfo)
        {
            dal.Update(adminProileInfo);
        }
    }
}

