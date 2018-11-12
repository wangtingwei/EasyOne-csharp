namespace EasyOne.IDal.UserManage
{
    using System;

    public interface IUserPoint
    {
        bool AddPointForAll(int userPoint);
        bool AddPointForGroups(string groupId, int userPoint);
        bool AddPointForUsers(string toUserList, int userPoint);
    }
}

