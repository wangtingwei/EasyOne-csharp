namespace EasyOne.IDal.UserManage
{
    using EasyOne.Model.UserManage;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public interface IUserFriend
    {
        bool Add(UserFriendInfo userFriendInfo);
        bool CheckBlackFriend(string friendName, string userName);
        bool Delete(string friendId);
        bool Delete(string userName, int friendGroupId);
        bool Exists(string friendName, string userName);
        int GetFriendCount(int friendGroupId, string userName);
        IList<string> GetFriendNameList(string userName);
        DataTable GetList(int startRowIndexId, int maxNumberRows, string userName, int groupId);
        int GetTotalOfFriend();
        bool MoveByGroupId(string friendId, int groupId);
    }
}

