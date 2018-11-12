namespace EasyOne.UserManage
{
    using EasyOne.Common;
    using EasyOne.Enumerations;
    using EasyOne.IDal.Accessories;
    using EasyOne.IDal.UserManage;
    using EasyOne.Model.Accessories;
    using EasyOne.Model.UserManage;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;
    using EasyOne.Components;

    public class UserMoney : IEncourageStrategy<decimal>
    {
        private string m_FromUser = PEContext.Current.Admin.AdminName;
        private string m_FromUserIp = PEContext.Current.UserHostAddress;
        private IList<UserInfo> m_UserList;
        private static readonly IBankrollItem s_Bank = DataAccess.CreateBankrollItem();
        private static readonly IUserMoney s_UserMoney = DataAccess.CreateUserMoneyCreate();
        private static readonly IUsers s_Users = DataAccess.CreateUsers();

        private void AddMoneyLog(decimal money, IList<UserInfo> userList, string reason, string memo)
        {
            foreach (UserInfo info in userList)
            {
                BankrollItemInfo bankrollItemInfo = new BankrollItemInfo();
                bankrollItemInfo.Inputer = this.m_FromUser;
                bankrollItemInfo.UserName = info.UserName;
                bankrollItemInfo.ClientId = info.ClientId;
                bankrollItemInfo.MoneyType = 4;
                bankrollItemInfo.CurrencyType = 3;
                bankrollItemInfo.DateAndTime = new DateTime?(DateTime.Now);
                bankrollItemInfo.Inputer = this.m_FromUser;
                bankrollItemInfo.Bank = "";
                bankrollItemInfo.ClientName = "";
                bankrollItemInfo.IP = this.m_FromUserIp;
                bankrollItemInfo.LogTime = new DateTime?(DateTime.Now);
                bankrollItemInfo.Money = money;
                bankrollItemInfo.Remark = reason;
                bankrollItemInfo.Status = BankrollItemStatus.Confirm;
                bankrollItemInfo.Memo = memo;
                s_Bank.Add(bankrollItemInfo);
            }
        }

        public bool IncreaseForAll(decimal howMany, string reason, bool isRecord, string memo)
        {
            bool flag = s_UserMoney.AddMoneyForAll(howMany);
            if (isRecord)
            {
                this.m_UserList = s_Users.GetAllUsers(0, 0, -1, null, -1);
                this.AddMoneyLog(howMany, this.m_UserList, reason, memo);
            }
            return flag;
        }

        public bool IncreaseForGroup(string groups, decimal howMany, string reason, bool isRecord, string memo)
        {
            if (!DataValidator.IsValidId(groups))
            {
                return false;
            }
            bool flag = s_UserMoney.AddMoneyForGroups(groups, howMany);
            if (isRecord)
            {
                this.m_UserList = s_Users.GetUsersByGroupId(groups);
                this.AddMoneyLog(howMany, this.m_UserList, reason, memo);
            }
            return flag;
        }

        public bool IncreaseForUsers(string toUser, decimal howMany, string reason, bool isRecord, string memo)
        {
            if (!DataValidator.IsValidId(toUser))
            {
                return false;
            }
            bool flag = s_UserMoney.AddMoneyForUsers(toUser, howMany);
            if (isRecord)
            {
                this.m_UserList = s_Users.GetUsersByUserId(toUser);
                this.AddMoneyLog(howMany, this.m_UserList, reason, memo);
            }
            return flag;
        }
    }
}

