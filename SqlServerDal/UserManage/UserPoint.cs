namespace EasyOne.SqlServerDal.UserManage
{
    using EasyOne.IDal.UserManage;
    using EasyOne.SqlServerDal;
    using System;
    using System.Data;

    public class UserPoint : IUserPoint
    {
        private static bool AddPoint(string strSql, int userPoint)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@UserPoint", DbType.Int32, userPoint);
            return (DBHelper.ExecuteNonQuerySql(strSql, cmdParams) > 0);
        }

        public bool AddPointForAll(int userPoint)
        {
            string strSql = "UPDATE PE_Users SET UserPoint = isnull(UserPoint,0) + @UserPoint";
            return AddPoint(strSql, userPoint);
        }

        public bool AddPointForGroups(string groupId, int userPoint)
        {
            return AddPoint(string.Format("UPDATE PE_Users SET UserPoint = isnull(UserPoint,0) + @UserPoint WHERE GroupId IN ({0})", DBHelper.ToValidId(groupId)), userPoint);
        }

        public bool AddPointForUsers(string toUserList, int userPoint)
        {
            return AddPoint(string.Format("UPDATE PE_Users SET UserPoint = isnull(UserPoint,0) + @UserPoint WHERE UserId IN ({0})", DBHelper.ToValidId(toUserList)), userPoint);
        }
    }
}

