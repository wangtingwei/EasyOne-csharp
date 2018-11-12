namespace EasyOne.IDal.UserManage
{
    using EasyOne.Enumerations;
    using EasyOne.Model.UserManage;
    using System;
    using System.Collections.Generic;

    public interface IUsers
    {
        bool Add(UserInfo usersInfo);
        bool AddPoint(int infoPoint, string userName);
        bool AddToAdminCompany(string userName);
        bool AgreeJoinCompany(string userName, int companyClientId);
        void BatchAuditing(string userId);
        bool BatchLock(string userId);
        void BatchUnlock(string userId);
        bool BatchUpdateUserStatus(string userId, UserStatus userStatus);
        bool Delete(int userId);
        bool DeleteCompany(int companyId);
        bool Exists(string userName);
        bool ExistsUserByClientId(int clientId);
        int ExportDataToAccess(string databaseName, int groupId);
        IList<UserInfo> GetAllUsers(int startRowIndexId, int maxNumberRows, int groupId, string keyword, int listType);
        int GetAuditingCompanyMemberCount(int companyId);
        IList<UserInfo> GetListByCompanyId(int companyId);
        int GetNumberOfUsers();
        UserInfo GetUserById(int userId);
        IList<UserInfo> GetUserByPost();
        IList<string> GetUserMailByGroupId(int groupId);
        IList<string[]> GetUserNameAndEmailList(int type, string value);
        string GetUserNameByClientId(int clientId);
        IList<string> GetUserNameList(int startRowIndexId, int maxiNumRows, int searchType, string keyword);
        int GetUserNameListTotal(int searchType, string keyword);
        UserInfo GetUsersByEmail(string email);
        IList<UserInfo> GetUsersByGroupId(string groupId);
        IList<UserInfo> GetUsersByUserId(string userId);
        UserInfo GetUsersByUserName(string userName);
        bool LockUser(int userId);
        bool MinusPoint(int infoPoint, string userName);
        bool MoveBetweenUserId(int startUserId, int endUserId, int groupId);
        bool MoveByGroups(string groupId, int targetGroupId);
        bool MoveByUserName(string userName, int groupId);
        bool MoveByUsers(string userId, int groupId);
        bool RemoveFromAdminCompany(string userName);
        bool RemoveFromCompany(string userName);
        bool SaveUserPurview(UserPurviewInfo userPurviewInfo, int userId);
        bool SaveUserPurview(bool inheritGroupRole, int userId);
        bool Update(UserInfo usersInfo);
        bool Update(int userId, string fieldName, string fieldValue);
        bool UpdateForCompany(int companyId, string userName, UserType userType, int companyClientId);
        bool UpdateUserFriendGroup(string userName, string userFriendGroup);
        bool UpdateUserStatus(int userId, UserStatus userStatus);
        int ValidateUser(string userName, string password);
    }
}

