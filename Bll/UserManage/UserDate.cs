namespace EasyOne.UserManage
{
    using EasyOne.Common;
    using EasyOne.IDal.UserManage;
    using EasyOne.Model.UserManage;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;
    using EasyOne.Components;

    public class UserDate : IEncourageStrategy<int>
    {
        private string m_FromUser = PEContext.Current.Admin.AdminName;
        private string m_FromUserIp = PEContext.Current.UserHostAddress;
        private IList<UserInfo> m_UserList;
        private static readonly IUserDate userDate = DataAccess.CreateUserDateCreate();
        private static readonly IUsers users = DataAccess.CreateUsers();

        private void AddUserDateLog(int days, IList<UserInfo> userList, string reason, string memo)
        {
            foreach (UserInfo info in userList)
            {
                UserValidLogInfo userValidLogInfo = new UserValidLogInfo();
                userValidLogInfo.Inputer = string.IsNullOrEmpty(this.m_FromUser) ? "System" : this.m_FromUser;
                userValidLogInfo.IP = this.m_FromUserIp;
                userValidLogInfo.UserName = info.UserName;
                userValidLogInfo.LogTime = DateTime.Now;
                userValidLogInfo.Memo = memo;
                userValidLogInfo.Remark = reason;
                if (days > 0)
                {
                    userValidLogInfo.IncomePayout = 1;
                }
                else
                {
                    userValidLogInfo.IncomePayout = 2;
                }
                if (days == -9999)
                {
                    days = 0;
                }
                userValidLogInfo.ValidNum = days;
                UserValidLog.Add(userValidLogInfo);
            }
        }

        public bool IncreaseForAll(int howMany, string reason, bool isRecord, string memo)
        {
            bool flag = userDate.AddDateForAll(howMany);
            if (isRecord)
            {
                this.m_UserList = users.GetAllUsers(0, 0, -1, null, -1);
                this.AddUserDateLog(howMany, this.m_UserList, reason, memo);
            }
            return flag;
        }

        public bool IncreaseForGroup(string groups, int howMany, string reason, bool isRecord, string memo)
        {
            if (!DataValidator.IsValidId(groups))
            {
                return false;
            }
            bool flag = userDate.AddDateForGroups(groups, howMany);
            if (isRecord)
            {
                this.m_UserList = users.GetUsersByGroupId(groups);
                this.AddUserDateLog(howMany, this.m_UserList, reason, memo);
            }
            return flag;
        }

        public bool IncreaseForUsers(string toUser, int howMany, string reason, bool isRecord, string memo)
        {
            if (!DataValidator.IsValidId(toUser))
            {
                return false;
            }
            bool flag = userDate.AddDateForUsers(toUser, howMany);
            if (isRecord)
            {
                this.m_UserList = users.GetUsersByUserId(toUser);
                this.AddUserDateLog(howMany, this.m_UserList, reason, memo);
            }
            return flag;
        }
    }
}

