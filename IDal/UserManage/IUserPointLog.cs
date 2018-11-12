namespace EasyOne.IDal.UserManage
{
    using EasyOne.Model.UserManage;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public interface IUserPointLog
    {
        bool Add(UserPointLogInfo userPointLogInfo);
        bool Delete(DateTime tempDate);
        bool Delete(string userName);
        bool Exists(string userName);
        int GetCountNumber();
        IList<UserPointLogInfo> GetPointList(int startRowIndexId, int maxNumberRows, int scopesType, int field, string keyword);
        UserPointLogInfo GetPointLogById(int logId);
        UserPointLogInfo GetPointLogByIdAndUserName(int logId, string userName);
        ArrayList GetTotalInComeAndPayOutAll();
        ArrayList GetTotalInComeAndPayOutAll(string userName);
        int GetValidPointLogId(string userName, int moduleType, int infoId, int chargeType, int pitchTime, int readTimes);
        int PointSum(int sumType);
        int PointSum(int sumType, string userName);
        bool UpdateTimes(int id, string ip);
        void UpdateTimes(string userTrueIP, int logId);
        int ViewInfosOneDay(string userName);
        int ViewTotalInfos(string userName);
    }
}

