namespace EasyOne.IDal.UserManage
{
    using System;
    using System.Collections.Generic;
    using EasyOne.Model.UserManage;

    public interface IRoleMembers
    {
        bool AddMemberToRole(int adminId, int roleId);
        IList<AdministratorInfo> GetMemberListByRoleId(int roleId);
        IList<AdministratorInfo> GetMemberListNotInRole(int roleId);
        string GetRoleIdListByAdminId(int adminId);
        void RemoveAdminFromRolesByRoleId(int roleId);
        void RemoveMemberFromAllRoles(int adminId);
    }
}

