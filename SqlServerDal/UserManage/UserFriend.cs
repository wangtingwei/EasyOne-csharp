namespace EasyOne.SqlServerDal.UserManage
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.IDal.UserManage;
    using EasyOne.Model.UserManage;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    public class UserFriend : IUserFriend
    {
        private int m_TotalOfFriend;

        public bool Add(UserFriendInfo userFriendInfo)
        {
            Parameters cmdParams = GetParameters(userFriendInfo);
            cmdParams.AddInParameter("@AddTime", DbType.DateTime, DateTime.Now);
            return DBHelper.ExecuteSql("INSERT INTO PE_Friend (FriendName, UserName, AddTime, GroupID) VALUES (@FriendName, @UserName, @AddTime, @GroupID)", cmdParams);
        }

        public bool CheckBlackFriend(string friendName, string userName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@FriendName", DbType.String, friendName);
            cmdParams.AddInParameter("@UserName", DbType.String, userName);
            return DBHelper.ExistsSql("SELECT * FROM PE_Friend WHERE ((FriendName = @FriendName AND UserName = @UserName) OR (UserName = @FriendName AND FriendName = @UserName)) AND GroupID = 0", cmdParams);
        }

        public bool Delete(string friendId)
        {
            return DBHelper.ExecuteSql("DELETE FROM PE_Friend WHERE ID IN (" + DBHelper.ToValidId(friendId) + ")");
        }

        public bool Delete(string userName, int friendGroupId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@UserName", DbType.String, userName);
            cmdParams.AddInParameter("@GroupID", DbType.Int32, friendGroupId);
            return DBHelper.ExecuteSql("DELETE FROM PE_Friend WHERE UserName = @UserName AND GroupID = @GroupID", cmdParams);
        }

        public bool Exists(string friendName, string userName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@FriendName", DbType.String, friendName);
            cmdParams.AddInParameter("@UserName", DbType.String, userName);
            return DBHelper.ExistsSql("SELECT * FROM PE_Friend WHERE FriendName = @FriendName AND UserName = @UserName", cmdParams);
        }

        public int GetFriendCount(int friendGroupId, string userName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@GroupID", DbType.Int32, friendGroupId);
            cmdParams.AddInParameter("@UserName", DbType.String, userName);
            return DBHelper.ObjectToInt32(DBHelper.ExecuteScalarSql("SELECT COUNT(ID) FROM PE_Friend WHERE UserName = @UserName AND GroupID = @GroupID", cmdParams));
        }

        public IList<string> GetFriendNameList(string userName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@UserName", DbType.String, userName);
            IList<string> list = new List<string>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT TOP 20 FriendName FROM PE_Friend WHERE UserName = @UserName AND GroupID <> 0 ORDER BY AddTime DESC", cmdParams))
            {
                while (reader.Read())
                {
                    list.Add(reader.GetString("FriendName"));
                }
            }
            return list;
        }

        public DataTable GetList(int startRowIndexId, int maxNumberRows, string userName, int groupId)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String);
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String);
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String);
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String);
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String);
            database.SetParameterValue(storedProcCommand, "@StartRows", startRowIndexId);
            database.SetParameterValue(storedProcCommand, "@PageSize", maxNumberRows);
            database.SetParameterValue(storedProcCommand, "@SortColumn", "F.ID");
            database.SetParameterValue(storedProcCommand, "@StrColumn", "F.ID, F.FriendName, F.GroupID, U.Email, C.QQ, C.Homepage");
            database.SetParameterValue(storedProcCommand, "@Sorts", "DESC");
            database.SetParameterValue(storedProcCommand, "@TableName", "PE_Friend F LEFT JOIN (PE_Users U LEFT JOIN PE_Contacter C ON U.UserName = C.UserName ) ON F.FriendName = U.UserName");
            if (groupId >= 0)
            {
                database.SetParameterValue(storedProcCommand, "@Filter", string.Concat(new object[] { "F.UserName = '", DBHelper.FilterBadChar(userName), "' AND F.GroupID = ", groupId }));
            }
            else
            {
                database.SetParameterValue(storedProcCommand, "@Filter", "F.UserName = '" + DBHelper.FilterBadChar(userName) + "'");
            }
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, 10);
            DataTable table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("FriendName");
            table.Columns.Add("GroupID");
            table.Columns.Add("Email");
            table.Columns.Add("QQ");
            table.Columns.Add("Homepage");
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    DataRow row = table.NewRow();
                    row["ID"] = reader.GetInt32("ID");
                    row["FriendName"] = reader.GetString("FriendName");
                    row["GroupID"] = reader.GetInt32("GroupID");
                    row["Email"] = reader.GetString("Email");
                    row["QQ"] = reader.GetString("QQ");
                    row["Homepage"] = reader.GetString("Homepage");
                    table.Rows.Add(row);
                }
            }
            this.m_TotalOfFriend = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return table;
        }

        private static Parameters GetParameters(UserFriendInfo userFriendInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@FriendName", DbType.String, userFriendInfo.FriendName);
            parameters.AddInParameter("@UserName", DbType.String, userFriendInfo.UserName);
            parameters.AddInParameter("@GroupID", DbType.Int32, userFriendInfo.GroupId);
            return parameters;
        }

        public int GetTotalOfFriend()
        {
            return this.m_TotalOfFriend;
        }

        public bool MoveByGroupId(string friendId, int groupId)
        {
            return DBHelper.ExecuteSql("UPDATE PE_Friend SET GroupID = @GroupID WHERE ID IN (" + DBHelper.ToValidId(friendId) + ")", new Parameters("@GroupID", DbType.Int32, groupId));
        }
    }
}

