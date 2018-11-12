namespace EasyOne.UserManage
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.IDal.UserManage;
    using EasyOne.Model.UserManage;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class Administrators
    {
        private static readonly IAdministrator dal = DataAccess.CreateAdmin();

        private Administrators()
        {
        }

        public static bool Add(AdministratorInfo adminInfo)
        {
            return dal.Add(adminInfo);
        }

        public static IList<AdministratorInfo> AdminList(int startRowIndexId, int maxNumberRows)
        {
            return AdminList(startRowIndexId, maxNumberRows, string.Empty);
        }

        public static IList<AdministratorInfo> AdminList(int startRowIndexId, int maxNumberRows, string userName)
        {
            userName = DataSecurity.FilterBadChar(userName);
            return dal.AdminList(startRowIndexId, maxNumberRows, userName);
        }

        public static IList<AdministratorInfo> AdminList(int startRowIndexId, int maxNumberRows, int roleId, int listType)
        {
            return dal.AdminList(startRowIndexId, maxNumberRows, roleId, listType);
        }

        public static IList<AdministratorInfo> AdminList(int startRowIndexId, int maxNumberRows, int searchType, string keyword)
        {
            return dal.AdminList(startRowIndexId, maxNumberRows, searchType, DataSecurity.FilterBadChar(keyword));
        }

        public static AdministratorInfo AuthenticateAdmin(string adminName, string adminPassword)
        {
            AdministratorInfo administratorByAdminName = GetAdministratorByAdminName(adminName);
            if ((!administratorByAdminName.IsNull && !string.IsNullOrEmpty(adminName)) && !string.IsNullOrEmpty(adminPassword))
            {
                string str = StringHelper.MD5(adminPassword);
                if (!StringHelper.ValidateMD5(administratorByAdminName.AdminPassword, str))
                {
                    return new AdministratorInfo(true);
                }
                administratorByAdminName.LastLogOnIP = PEContext.Current.UserHostAddress;
                administratorByAdminName.LastLogOnTime = new DateTime?(DateTime.Now);
                administratorByAdminName.LogOnTimes++;
                administratorByAdminName.AdminPassword = str;
                administratorByAdminName.RndPassword = DataSecurity.MakeRandomString(0x10);
                Update(administratorByAdminName);
            }
            return administratorByAdminName;
        }

        public static bool Delete(int adminId)
        {
            if (PEContext.Current.Admin.AdministratorInfo.AdminId == adminId)
            {
                return false;
            }
            RoleMembers.RemoveMemberFromAllRoles(adminId);
            return dal.Delete(adminId);
        }

        private static AdministratorInfo GetAdministrator(int adminId, string adminName, string userName)
        {
            return dal.GetAdministrator(adminId, adminName, userName);
        }

        public static AdministratorInfo GetAdministratorByAdminId(int adminId)
        {
            if (adminId <= 0)
            {
                return new AdministratorInfo(true);
            }
            return GetAdministrator(adminId, null, null);
        }

        public static AdministratorInfo GetAdministratorByAdminName(string adminName)
        {
            if (string.IsNullOrEmpty(adminName))
            {
                return new AdministratorInfo(true);
            }
            return GetAdministrator(0, adminName, null);
        }

        public static AdministratorInfo GetAdministratorByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return new AdministratorInfo(true);
            }
            return GetAdministrator(0, null, userName);
        }

        public static IList<AdministratorInfo> GetAdminList(int startRowIndexId, int maxNumberRows, string adminName)
        {
            adminName = DataSecurity.FilterBadChar(adminName);
            return dal.GetAdminList(startRowIndexId, maxNumberRows, adminName);
        }

        public static IList<AdministratorInfo> GetAdminListByOperateCode(int startRowIndexId, int maxNumberRows, int operateCode)
        {
            return dal.GetAdminListByOperateCode(startRowIndexId, maxNumberRows, operateCode);
        }

        public static int GetTotalOfAdmin()
        {
            return dal.GetTotalOfAdmin();
        }

        public static int GetTotalOfAdmin(int roleId, int listType)
        {
            return dal.GetTotalOfAdmin();
        }

        public static bool IsExist(string adminName)
        {
            return dal.IsExist(adminName);
        }

        public static bool Update(AdministratorInfo adminInfo)
        {
            return dal.Update(adminInfo);
        }
    }
}

