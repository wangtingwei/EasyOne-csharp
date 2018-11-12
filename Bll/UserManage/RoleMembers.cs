namespace EasyOne.UserManage
{
    using EasyOne.Common;
    using EasyOne.IDal.UserManage;
    using System;
    using System.Collections.Generic;
    using EasyOne.Model.UserManage;
    using EasyOne.DalFactory;

    public sealed class RoleMembers
    {
        private static readonly IRoleMembers dal = DataAccess.CreateRoleMembers();

        private RoleMembers()
        {
        }

        public static void AddMembersToRole(string admins, int roleId)
        {
            dal.RemoveAdminFromRolesByRoleId(roleId);
            if (!string.IsNullOrEmpty(admins))
            {
                foreach (string str in admins.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    dal.AddMemberToRole(DataConverter.CLng(str), roleId);
                }
            }
        }

        public static void AddMemberToRoles(int adminId, string roles)
        {
            dal.RemoveMemberFromAllRoles(adminId);
            foreach (string str in roles.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                dal.AddMemberToRole(adminId, DataConverter.CLng(str));
            }
        }

        public static IList<AdministratorInfo> GetMemberListByRoleId(int roleId)
        {
            return dal.GetMemberListByRoleId(roleId);
        }

        public static IList<AdministratorInfo> GetMemberListNotInRole(int roleId)
        {
            return dal.GetMemberListNotInRole(roleId);
        }

        public static string GetRoleIdListByAdminId(int adminId)
        {
            return dal.GetRoleIdListByAdminId(adminId);
        }

        public static void RemoveMemberFromAllRoles(int adminId)
        {
            dal.RemoveAdminFromRolesByRoleId(adminId);
        }
    }
}

