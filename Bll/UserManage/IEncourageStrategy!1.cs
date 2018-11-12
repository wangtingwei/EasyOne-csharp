namespace EasyOne.UserManage
{
    using System;

    public interface IEncourageStrategy<T>
    {
        bool IncreaseForAll(T howMany, string reason, bool isRecord, string memo);
        bool IncreaseForGroup(string groups, T howMany, string reason, bool isRecord, string memo);
        bool IncreaseForUsers(string toUser, T howMany, string reason, bool isRecord, string memo);
    }
}

