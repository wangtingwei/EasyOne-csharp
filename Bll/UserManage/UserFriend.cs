namespace EasyOne.UserManage
{
    using EasyOne.Common;
    using EasyOne.IDal.UserManage;
    using EasyOne.Model.UserManage;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using EasyOne.DalFactory;

    public sealed class UserFriend
    {
        private static readonly IUserFriend dal = DataAccess.CreateUserFriend();

        private UserFriend()
        {
        }

        public static bool Add(UserFriendInfo userFriendInfo)
        {
            return dal.Add(userFriendInfo);
        }

        public static bool CheckBlackFriend(string friendName, string userName)
        {
            return dal.CheckBlackFriend(friendName, userName);
        }

        public static bool Delete(string friendId)
        {
            return (DataValidator.IsValidId(friendId) && dal.Delete(friendId));
        }

        public static bool Delete(string userName, int friendGroupId)
        {
            return dal.Delete(userName, friendGroupId);
        }

        public static bool Exists(string friendName, string userName)
        {
            return dal.Exists(friendName, userName);
        }

        public static int GetFriendCount(int friendGroupId, string userName)
        {
            return dal.GetFriendCount(friendGroupId, userName);
        }

        public static IList<string> GetFriendNameList(string userName)
        {
            return dal.GetFriendNameList(userName);
        }

        public static DataTable GetList(int startRowIndexId, int maxNumberRows, string userName, int groupId)
        {
            return dal.GetList(startRowIndexId, maxNumberRows, DataSecurity.FilterBadChar(userName), groupId);
        }

        public static int GetTotalOfFriend(string userName, int groupId)
        {
            return dal.GetTotalOfFriend();
        }

        public static bool MoveByGroupId(string friendId, int groupId)
        {
            return (DataValidator.IsValidId(friendId) && dal.MoveByGroupId(friendId, groupId));
        }
    }
}

