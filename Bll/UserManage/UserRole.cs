namespace EasyOne.UserManage
{
    using EasyOne.IDal.UserManage;
    using EasyOne.Model.UserManage;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class UserRole
    {
        private static readonly IUserRole dal = DataAccess.CreateRoles();

        private UserRole()
        {
        }

        public static bool Add(RoleInfo roleInfo)
        {
            return dal.Add(roleInfo);
        }

        public static bool Delete(int roleId)
        {
            if (roleId <= 0)
            {
                return false;
            }
            DeleteRoleRelation(roleId);
            return dal.Delete(roleId);
        }

        private static void DeleteRoleRelation(int roleId)
        {
            dal.DeleteFieldPermissionFromRoles(roleId, 0, null);
            dal.DeleteNodePermissionFromRoles(roleId, -2);
            dal.DeletePermissionFromRoles(roleId);
            dal.DeleteSpecialPermissionFromRoles(roleId, 0);
        }

        public static RoleInfo GetRoleInfoByRoleId(int roleId)
        {
            return dal.GetRoleInfoById(roleId);
        }

        public static IList<RoleInfo> GetRoleList()
        {
            return GetRoleListFromDAL(0, 0);
        }

        public static IList<RoleInfo> GetRoleList(int startRowIndexId, int maxNumberRows)
        {
            IList<RoleInfo> roleListFromDAL = GetRoleListFromDAL(startRowIndexId, maxNumberRows);
            RoleInfo item = new RoleInfo();
            item.RoleId = 0;
            item.RoleName = "超级管理员";
            item.Description = "超级管理员";
            roleListFromDAL.Add(item);
            return roleListFromDAL;
        }

        public static IList<RoleInfo> GetRoleListByRoleId(int adminId)
        {
            return dal.GetRoleListByRoleId(adminId);
        }

        private static IList<RoleInfo> GetRoleListFromDAL(int startRowIndexId, int maxNumberRows)
        {
            return dal.GetRoleList(startRowIndexId, maxNumberRows);
        }

        public static IList<RoleInfo> GetRoleListNotInRole(int adminId)
        {
            return dal.GetRoleListNotInRole(adminId);
        }

        public static int GetTotalOfRoles()
        {
            return (dal.GetTotalOfRoles() + 1);
        }

        public static bool IsExist(string roleName)
        {
            return dal.IsExist(roleName);
        }

        public static bool Update(RoleInfo roleInfo)
        {
            return dal.Update(roleInfo);
        }
    }
}

