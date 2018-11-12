namespace EasyOne.UserManage
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Enumerations;
    using EasyOne.IDal.UserManage;
    using EasyOne.Model.UserManage;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class UserGroups
    {
        private static readonly IUserGroups dal = DataAccess.CreateUserGroups();
        private static Serialize<UserPurviewInfo> ser = new Serialize<UserPurviewInfo>();

        private UserGroups()
        {
        }

        public static DataActionState Add(UserGroupsInfo userGroupsInfo)
        {
            if (!RolePermissions.AccessCheck(OperateCode.UserGroupManage))
            {
                throw new CustomException(PEExceptionType.ExceedAuthority);
            }
            DataActionState unknown = DataActionState.Unknown;
            if (GroupNameIsExist(userGroupsInfo.GroupName))
            {
                return DataActionState.Exist;
            }
            if (dal.Add(userGroupsInfo))
            {
                unknown = DataActionState.Successed;
            }
            return unknown;
        }

        private static void CountUserNumber(IList<UserGroupsInfo> userGroupsList)
        {
            for (int i = 0; i < userGroupsList.Count; i++)
            {
                userGroupsList[i].UserInGroupNumber = GetUserInGroupNumber(userGroupsList[i].GroupId);
            }
        }

        public static bool Delete(int id)
        {
            RolePermissions.BusinessAccessCheck(OperateCode.UserGroupManage);
            if (dal.Delete(id))
            {
                DeleteUserGroupRelation(id);
            }
            foreach (UserInfo info in Users.GetUsersByGroupId(id.ToString()))
            {
                info.GroupId = 1;
                Users.Update(info);
            }
            return true;
        }

        private static void DeleteUserGroupRelation(int id)
        {
            UserPermissions.DeleteFieldPermissions(id, 1);
            UserPermissions.DeleteNodePermissions(id, -2);
            UserPermissions.DeleteSpecialPermissions(id);
        }

        public static string GetGroupName(int groupId)
        {
            return GetUserGroupById(groupId).GroupName;
        }

        public static UserPurviewInfo GetGroupSetting(string groupSetting)
        {
            return ser.DeserializeField(groupSetting);
        }

        public static IList<UserGroupsInfo> GetGroupTable(GroupType groupType)
        {
            return dal.GetUserGroupList(groupType);
        }

        public static int GetNumberUserGroups()
        {
            return dal.GetNumberUserGroups();
        }

        public static UserGroupsInfo GetUserGroupById(int id)
        {
            return dal.GetUserGroupById(id);
        }

        public static IList<UserGroupsInfo> GetUserGroupList(int startRowIndexId, int maxNumberRows)
        {
            return GetUserGroupList(startRowIndexId, maxNumberRows, false);
        }

        public static IList<UserGroupsInfo> GetUserGroupList(int startRowIndexId, int maxNumberRows, bool isNeedUserNumber)
        {
            IList<UserGroupsInfo> userGroupList = dal.GetUserGroupList(startRowIndexId, maxNumberRows);
            if (isNeedUserNumber)
            {
                CountUserNumber(userGroupList);
            }
            return userGroupList;
        }

        private static int GetUserInGroupNumber(int groupId)
        {
            return dal.GetUserInGroupNumber(groupId);
        }

        public static bool GroupNameIsExist(string groupName)
        {
            return dal.UserGroupIsExist(groupName);
        }

        public static DataActionState Update(UserGroupsInfo userGroupsInfo)
        {
            DataActionState unknown = DataActionState.Unknown;
            if (!RolePermissions.AccessCheck(OperateCode.UserGroupManage))
            {
                throw new CustomException(PEExceptionType.ExceedAuthority);
            }
            if (dal.Update(userGroupsInfo))
            {
                unknown = DataActionState.Successed;
            }
            return unknown;
        }
    }
}

