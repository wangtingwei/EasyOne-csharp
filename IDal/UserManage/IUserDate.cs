namespace EasyOne.IDal.UserManage
{
    using System;

    public interface IUserDate
    {
        bool AddDateForAll(int date);
        bool AddDateForGroups(string toGroups, int date);
        bool AddDateForUsers(string toUsers, int date);
    }
}

