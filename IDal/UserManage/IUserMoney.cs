namespace EasyOne.IDal.UserManage
{
    using System;

    public interface IUserMoney
    {
        bool AddMoneyForAll(decimal balance);
        bool AddMoneyForGroups(string groupId, decimal balance);
        bool AddMoneyForUsers(string toUserList, decimal balance);
    }
}

