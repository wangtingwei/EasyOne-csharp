namespace EasyOne.IDal.UserManage
{
    using EasyOne.Model.UserManage;
    using System;
    using System.Collections.Generic;

    public interface IUserValidLog
    {
        bool Add(UserValidLogInfo userValidLogInfo);
        bool Delete(DateTime tempDate);
        bool Delete(string userName);
        bool Exists(string userName);
        int GetCountNumber();
        IList<UserValidLogInfo> GetValidList(int startRowIndexId, int maxNumberRows, int scopesType, int field, string keyword);
        UserValidLogInfo GetValidLogById(int logId);
        UserValidLogInfo GetValidLogByIdAndUserName(int logId, string userName);
    }
}

