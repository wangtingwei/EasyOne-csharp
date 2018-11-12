namespace EasyOne.UserManage
{
    using EasyOne.Common;
    using EasyOne.IDal.UserManage;
    using EasyOne.Model.UserManage;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;
    using EasyOne.Components;

    public class UserPoint : IEncourageStrategy<int>
    {
        private string m_FromUser = PEContext.Current.Admin.AdminName;
        private string m_FromUserIp = PEContext.Current.UserHostAddress;
        private IList<UserInfo> m_UserList;
        private static readonly IUserPoint userPoint = DataAccess.CreateUserPointCreate();
        private static readonly IUsers users = DataAccess.CreateUsers();

        private void AddPointLog(int point, IList<UserInfo> userList, string reason, string memo)
        {
            foreach (UserInfo info in userList)
            {
                UserPointLogInfo userPointLogInfo = new UserPointLogInfo();
                userPointLogInfo.Inputer = string.IsNullOrEmpty(this.m_FromUser) ? "System" : this.m_FromUser;
                userPointLogInfo.IP = this.m_FromUserIp;
                userPointLogInfo.UserName = info.UserName;
                userPointLogInfo.LogTime = DateTime.Now;
                userPointLogInfo.Remark = reason;
                userPointLogInfo.Memo = memo;
                if (point > 0)
                {
                    userPointLogInfo.IncomePayOut = 1;
                }
                else
                {
                    userPointLogInfo.IncomePayOut = 2;
                }
                userPointLogInfo.Point = Math.Abs(point);
                UserPointLog.Add(userPointLogInfo);
            }
        }

        public bool IncreaseForAll(int howMany, string reason, bool isRecord, string memo)
        {
            bool flag = userPoint.AddPointForAll(howMany);
            if (isRecord)
            {
                this.m_UserList = users.GetAllUsers(0, 0, -1, null, -1);
                this.AddPointLog(howMany, this.m_UserList, reason, memo);
            }
            return flag;
        }

        public bool IncreaseForGroup(string groups, int howMany, string reason, bool isRecord, string memo)
        {
            if (!DataValidator.IsValidId(groups))
            {
                return false;
            }
            bool flag = userPoint.AddPointForGroups(groups, howMany);
            if (isRecord)
            {
                this.m_UserList = users.GetUsersByGroupId(groups);
                this.AddPointLog(howMany, this.m_UserList, reason, memo);
            }
            return flag;
        }

        public bool IncreaseForUsers(string toUser, int howMany, string reason, bool isRecord, string memo)
        {
            if (!DataValidator.IsValidId(toUser))
            {
                return false;
            }
            bool flag = userPoint.AddPointForUsers(toUser, howMany);
            if (isRecord)
            {
                this.m_UserList = users.GetUsersByUserId(toUser);
                this.AddPointLog(howMany, this.m_UserList, reason, memo);
            }
            return flag;
        }
    }
}

