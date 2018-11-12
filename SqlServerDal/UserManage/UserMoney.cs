namespace EasyOne.SqlServerDal.UserManage
{
    using EasyOne.IDal.UserManage;
    using EasyOne.SqlServerDal;
    using System;
    using System.Data;

    public class UserMoney : IUserMoney
    {
        private static bool AddMoney(string strSql, decimal balance)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@Balance", DbType.Currency, balance);
            return (DBHelper.ExecuteNonQuerySql(strSql, cmdParams) > 0);
        }

        public bool AddMoneyForAll(decimal balance)
        {
            string strSql = "UPDATE PE_Users SET Balance = Balance + @Balance";
            return AddMoney(strSql, balance);
        }

        public bool AddMoneyForGroups(string groupId, decimal balance)
        {
            return AddMoney(string.Format("UPDATE PE_Users SET Balance = isnull(Balance,0) + @Balance WHERE GroupId IN ({0})", DBHelper.ToValidId(groupId)), balance);
        }

        public bool AddMoneyForUsers(string toUserList, decimal balance)
        {
            return AddMoney(string.Format("UPDATE PE_Users SET Balance = isnull(Balance,0) + @Balance WHERE UserId IN ({0})", DBHelper.ToValidId(toUserList)), balance);
        }
    }
}

