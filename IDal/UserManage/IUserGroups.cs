namespace EasyOne.IDal.UserManage
{
    using EasyOne.Enumerations;
    using EasyOne.Model.UserManage;
    using System;
    using System.Collections.Generic;

    public interface IUserGroups
    {
        bool Add(UserGroupsInfo userGroupsInfo);
        bool Delete(int id);
        int GetMaxId();
        int GetNumberUserGroups();
        UserGroupsInfo GetUserGroupById(int id);
        IList<UserGroupsInfo> GetUserGroupList(GroupType groupType);
        IList<UserGroupsInfo> GetUserGroupList(int startRowIndexId, int maxNumberRows);
        int GetUserInGroupNumber(int groupId);
        bool Update(UserGroupsInfo userGroupsInfo);
        bool UserGroupIsExist(string groupName);
    }
}

