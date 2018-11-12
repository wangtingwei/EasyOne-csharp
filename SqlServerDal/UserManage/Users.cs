namespace EasyOne.SqlServerDal.UserManage
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.Common;
    using EasyOne.Enumerations;
    using EasyOne.IDal.UserManage;
    using EasyOne.Model.UserManage;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.OleDb;
    using System.Text;

    public class Users : IUsers
    {
        private int m_NumUsers;
        private static Serialize<UserPurviewInfo> ser = new Serialize<UserPurviewInfo>();

        public bool Add(UserInfo usersInfo)
        {
            usersInfo.UserId = GetNewId();
            Parameters userParameters = GetUserParameters(usersInfo);
            userParameters.AddInParameter("@UserFriendGroup", DbType.String, "黑名单$我的好友");
            return DBHelper.ExecuteProc("PR_UserManage_Users_Add", userParameters);
        }

        public bool AddPoint(int infoPoint, string userName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@UserPoint", DbType.Int32, infoPoint);
            cmdParams.AddInParameter("@UserName", DbType.String, userName);
            string strSql = "UPDATE PE_Users SET UserPoint = UserPoint + @UserPoint WHERE UserName = @UserName";
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool AddToAdminCompany(string userName)
        {
            return DBHelper.ExecuteSql("UPDATE PE_Users SET UserType = 2 WHERE UserType > 2 AND UserName = @UserName", new Parameters("@UserName", DbType.String, userName));
        }

        public bool AgreeJoinCompany(string userName, int companyClientId)
        {
            string str;
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@UserName", DbType.String, userName);
            if (companyClientId > 0)
            {
                str = "UPDATE PE_Users SET UserType = 3, ClientID = @CompanyClientID WHERE UserType = 4 AND UserName = @UserName";
                cmdParams.AddInParameter("@CompanyClientID", DbType.Int32, companyClientId);
            }
            else
            {
                str = "UPDATE PE_Users SET UserType = 3 WHERE UserType = 4 AND UserName = @UserName";
            }
            return DBHelper.ExecuteSql(str, cmdParams);
        }

        public void BatchAuditing(string userId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@Status", DbType.Int32, UserStatus.None);
            DBHelper.ExecuteSql("UPDATE PE_Users SET [Status]=@Status WHERE UserId in (" + DBHelper.ToValidId(userId) + ") AND Status >= 2 ", cmdParams);
        }

        public bool BatchLock(string userId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@Status", DbType.Int32, UserStatus.Locked);
            return DBHelper.ExecuteSql("UPDATE PE_Users SET [Status]=@Status,LastLockoutTime=GETDATE() WHERE UserId in (" + DBHelper.ToValidId(userId) + ")", cmdParams);
        }

        public void BatchUnlock(string userId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@Status", DbType.Int32, UserStatus.None);
            DBHelper.ExecuteSql("UPDATE PE_Users SET [Status]=@Status WHERE UserId in (" + DBHelper.ToValidId(userId) + ") AND Status = 1", cmdParams);
        }

        public bool BatchUpdateUserStatus(string userId, UserStatus userStatus)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@Status", DbType.Int32, userStatus);
            return DBHelper.ExecuteSql("UPDATE PE_Users SET [Status]=@Status WHERE UserId in (" + DBHelper.ToValidId(userId) + ")", cmdParams);
        }

        public bool Delete(int userId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@UserID", DbType.Int32, userId);
            string strSql = "DELETE FROM PE_Users WHERE UserID = @UserID";
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool DeleteCompany(int companyId)
        {
            DBHelper.ExecuteSql("UPDATE PE_Users SET UserType = 0, CompanyID = 0, ClientID = 0 WHERE CompanyID = @CompanyID", new Parameters("@CompanyID", DbType.Int32, companyId));
            return DBHelper.ExecuteSql("DELETE FROM PE_Company WHERE CompanyID = @CompanyID", new Parameters("@CompanyID", DbType.Int32, companyId));
        }

        public bool Exists(string userName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@UserName", DbType.String, userName);
            return DBHelper.ExistsProc("PR_UserManage_User_Exists", cmdParams);
        }

        public bool ExistsUserByClientId(int clientId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ClientID", DbType.Int32, clientId);
            return DBHelper.ExistsSql("SELECT UserID FROM PE_Users WHERE ClientID = @ClientID", cmdParams);
        }

        public int ExportDataToAccess(string databaseName, int groupId)
        {
            string connectionString = "Provider = Microsoft.Jet.OLEDB.4.0;Data Source = " + databaseName;
            int num = 0;
            IList<string> userMailByGroupId = this.GetUserMailByGroupId(groupId);
            if (userMailByGroupId.Count > 0)
            {
                OleDbConnection connection = new OleDbConnection(connectionString);
                OleDbCommand command = new OleDbCommand("INSERT INTO [user] (useremail) VALUES (@Email)", connection);
                OleDbParameter parameter = new OleDbParameter();
                parameter.ParameterName = "@Email";
                command.Parameters.Add(parameter);
                try
                {
                    connection.Open();
                    foreach (string str2 in userMailByGroupId)
                    {
                        command.Parameters["@Email"].Value = str2;
                        if (command.ExecuteNonQuery() > 0)
                        {
                            num++;
                        }
                    }
                    return num;
                }
                catch
                {
                    num = 0;
                    throw;
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }
            return num;
        }

        public IList<UserInfo> GetAllUsers(int startRowIndexId, int maxNumberRows, int groupId, string keyword, int listType)
        {
            IList<UserInfo> list = new List<UserInfo>();
            string storedProcedureName = "PR_Common_GetList";
            string str2 = "UserID";
            string str3 = "*";
            string str4 = "DESC";
            string str5 = "";
            switch (listType)
            {
                case 1:
                    str3 = "top 100 *";
                    str2 = "PostItems";
                    str4 = "DESC";
                    break;

                case 2:
                    str3 = "top 100 *";
                    str2 = "PostItems";
                    str4 = "ASC";
                    break;

                case 3:
                    str4 = "DESC";
                    str5 = "DATEDIFF(hh, LastLoginTime, GETDATE()) < 25 ";
                    break;

                case 4:
                    str4 = "DESC";
                    str5 = "DATEDIFF(hh, RegTime, GETDATE()) < 25";
                    break;

                case 5:
                    str5 = "Status = " + 1;
                    break;

                case 6:
                    str5 = "UserPoint > 0";
                    break;

                case 7:
                    str5 = "UserExp > 0";
                    break;

                case 8:
                    str5 = "Balance > 0";
                    break;

                case 9:
                    str5 = "Balance <= 0";
                    break;

                case 10:
                    str5 = "GroupID = " + groupId;
                    break;

                case 11:
                    str5 = "UserID= " + DBHelper.ToNumber(keyword);
                    break;

                case 12:
                    str5 = "UserName LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'";
                    break;

                case 13:
                    str5 = "Email LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'";
                    break;

                case 14:
                    str5 = string.Concat(new object[] { "Status = ", 2, " OR Status= ", 6 });
                    break;

                case 15:
                    str5 = string.Concat(new object[] { "Status = ", 4, " OR Status= ", 6 });
                    break;

                case 0x10:
                    str3 = "top 100 *";
                    str2 = "UserId";
                    str4 = "DESC";
                    break;

                case 0x11:
                    str3 = "top 100 *";
                    str2 = "UserPoint";
                    str4 = "DESC";
                    break;

                case 0x12:
                    str3 = "top 100 *";
                    str2 = "UserExp";
                    str4 = "DESC";
                    break;

                case 0x13:
                    str4 = "DESC";
                    str5 = "DATEDIFF(dd, GETDATE(), EndTime) <= 5 AND DATEDIFF(dd, GETDATE(), EndTime) >= 0";
                    break;

                case 20:
                    str5 = " UserName IN (SELECT UserName FROM PE_Contacter WHERE Homepage LIKE '%" + DBHelper.FilterBadChar(keyword) + "%')";
                    break;

                case 0x15:
                    str5 = " UserName IN (SELECT UserName FROM PE_Contacter WHERE TrueName LIKE '%" + DBHelper.FilterBadChar(keyword) + "%')";
                    break;

                case 0x16:
                    str5 = " UserName IN (SELECT UserName FROM PE_Contacter WHERE IDCard LIKE '%" + DBHelper.FilterBadChar(keyword) + "%')";
                    break;

                case 0x17:
                    str5 = " UserName IN (SELECT UserName FROM PE_Contacter WHERE Company LIKE '%" + DBHelper.FilterBadChar(keyword) + "%')";
                    break;

                case 0x18:
                    str5 = " UserName IN (SELECT UserName FROM PE_Contacter WHERE Address LIKE '%" + DBHelper.FilterBadChar(keyword) + "%')";
                    break;

                case 0x19:
                    str5 = " UserName IN (SELECT UserName FROM PE_Contacter WHERE Mobile LIKE '%" + DBHelper.FilterBadChar(keyword) + "%')";
                    break;

                case 0x1a:
                    str5 = " UserName IN (SELECT UserName FROM PE_Contacter WHERE OfficePhone LIKE '%" + DBHelper.FilterBadChar(keyword) + "%')";
                    break;

                case 0x1b:
                    str5 = " UserName IN (SELECT UserName FROM PE_Contacter WHERE HomePhone LIKE '%" + DBHelper.FilterBadChar(keyword) + "%')";
                    break;

                case 0x1c:
                    str5 = " UserName IN (SELECT UserName FROM PE_Contacter WHERE PHS LIKE '%" + DBHelper.FilterBadChar(keyword) + "%')";
                    break;

                case 0x1d:
                    str5 = " UserName IN (SELECT UserName FROM PE_Contacter WHERE Fax LIKE '%" + DBHelper.FilterBadChar(keyword) + "%')";
                    break;

                case 30:
                    str5 = " UserName IN (SELECT UserName FROM PE_Contacter WHERE QQ LIKE '%" + DBHelper.FilterBadChar(keyword) + "%')";
                    break;

                case 0x1f:
                    str5 = " UserName IN (SELECT UserName FROM PE_Contacter WHERE ICQ LIKE '%" + DBHelper.FilterBadChar(keyword) + "%')";
                    break;

                case 0x20:
                    str5 = " UserName IN (SELECT UserName FROM PE_Contacter WHERE MSN LIKE '%" + DBHelper.FilterBadChar(keyword) + "%')";
                    break;

                case 0x21:
                    str5 = " UserName IN (SELECT UserName FROM PE_Contacter WHERE Yahoo LIKE '%" + DBHelper.FilterBadChar(keyword) + "%')";
                    break;

                case 0x22:
                    str5 = " UserName IN (SELECT UserName FROM PE_Contacter WHERE UC LIKE '%" + DBHelper.FilterBadChar(keyword) + "%')";
                    break;

                case 0x23:
                    str5 = " UserName IN (SELECT UserName FROM PE_Contacter WHERE Aim LIKE '%" + DBHelper.FilterBadChar(keyword) + "%')";
                    break;

                case 0x24:
                    str5 = "IsInheritGroupRole = 0";
                    break;

                case 50:
                    str5 = " ClientID= " + DBHelper.ToNumber(keyword);
                    break;

                default:
                    str5 = "";
                    break;
            }
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand(storedProcedureName);
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String);
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String);
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String);
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String);
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String);
            database.SetParameterValue(storedProcCommand, "@StartRows", startRowIndexId);
            database.SetParameterValue(storedProcCommand, "@PageSize", maxNumberRows);
            database.SetParameterValue(storedProcCommand, "@SortColumn", str2);
            database.SetParameterValue(storedProcCommand, "@StrColumn", str3);
            database.SetParameterValue(storedProcCommand, "@Sorts", str4);
            database.SetParameterValue(storedProcCommand, "@TableName", "PE_Users");
            database.SetParameterValue(storedProcCommand, "@Filter", str5);
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(UsersFromrdr(reader));
                }
            }
            this.m_NumUsers = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public int GetAuditingCompanyMemberCount(int companyId)
        {
            return DBHelper.ObjectToInt32(DBHelper.ExecuteScalarSql("SELECT COUNT(*) FROM PE_Users WHERE UserType = 4 AND CompanyID = @CompanyID", new Parameters("@CompanyID", DbType.Int32, companyId)));
        }

        public IList<UserInfo> GetListByCompanyId(int companyId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@CompanyID", DbType.Int32, companyId);
            IList<UserInfo> list = new List<UserInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_Users WHERE CompanyID = @CompanyID ORDER BY UserType ASC", cmdParams))
            {
                while (reader.Read())
                {
                    list.Add(UsersFromrdr(reader));
                }
                return list;
            }
        }

        private static int GetMaxId()
        {
            return DBHelper.GetMaxId("PE_Users", "UserID");
        }

        private static int GetNewId()
        {
            return (GetMaxId() + 1);
        }

        public int GetNumberOfUsers()
        {
            return this.m_NumUsers;
        }

        public UserInfo GetUserById(int userId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@UserID", DbType.Int32, userId);
            using (NullableDataReader reader = DBHelper.ExecuteReaderProc("PR_UserManage_Users_GetById", cmdParams))
            {
                if (reader.Read())
                {
                    return UsersFromrdr(reader);
                }
                return new UserInfo(true);
            }
        }

        public IList<UserInfo> GetUserByPost()
        {
            IList<UserInfo> list = new List<UserInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_Users WHERE PostItems > 0"))
            {
                while (reader.Read())
                {
                    list.Add(UsersFromrdr(reader));
                }
            }
            return list;
        }

        public IList<string> GetUserMailByGroupId(int groupId)
        {
            IList<string> list = new List<string>();
            string strSql = string.Empty;
            Parameters cmdParams = new Parameters();
            if (groupId == 0)
            {
                strSql = "SELECT Email FROM PE_Users WHERE Email LIKE '%@%'";
                cmdParams = null;
            }
            else
            {
                strSql = "SELECT Email FROM PE_Users WHERE Email LIKE '%@%' AND GroupID = @GroupId";
                cmdParams.AddInParameter("@GroupId", DbType.Int32, groupId);
            }
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, cmdParams))
            {
                while (reader.Read())
                {
                    list.Add(reader.GetString("Email"));
                }
            }
            return list;
        }

        public IList<string[]> GetUserNameAndEmailList(int type, string value)
        {
            StringBuilder builder = new StringBuilder("");
            string strSql = "SELECT UserName, Email FROM PE_Users ";
            IList<string[]> list = new List<string[]>();
            Parameters cmdParams = new Parameters();
            string[] strArray = value.Split(new char[] { ',' });
            switch (type)
            {
                case 0:
                    cmdParams = null;
                    break;

                case 1:
                    builder.Append(" WHERE GroupID IN (");
                    for (int i = 0; i < strArray.Length; i++)
                    {
                        cmdParams.AddInParameter("@value" + i.ToString(), DbType.Int32, Convert.ToInt32(strArray[i]));
                        builder.Append("@value" + i.ToString() + ",");
                    }
                    strSql = strSql + builder.ToString().TrimEnd(new char[] { ',' }) + ")";
                    break;

                case 2:
                    builder.Append(" WHERE UserName IN (");
                    for (int j = 0; j < strArray.Length; j++)
                    {
                        cmdParams.AddInParameter("@value" + j.ToString(), DbType.String, DBHelper.FilterBadChar(strArray[j]));
                        builder.Append("@value" + j.ToString() + ",");
                    }
                    strSql = strSql + builder.ToString().TrimEnd(new char[] { ',' }) + ")";
                    break;

                case 3:
                    builder.Append(" WHERE Email IN (");
                    for (int k = 0; k < strArray.Length; k++)
                    {
                        cmdParams.AddInParameter("@value" + k.ToString(), DbType.String, DBHelper.FilterBadChar(strArray[k]));
                        builder.Append("@value" + k.ToString() + ",");
                    }
                    strSql = strSql + builder.ToString().TrimEnd(new char[] { ',' }) + ")";
                    break;

                default:
                    cmdParams = null;
                    break;
            }
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, cmdParams))
            {
                while (reader.Read())
                {
                    list.Add(new string[] { reader.GetString("UserName"), reader.GetString("Email") });
                }
            }
            return list;
        }

        public string GetUserNameByClientId(int clientId)
        {
            object objA = DBHelper.ExecuteScalarSql("SELECT TOP 1 UserName FROM PE_Users WHERE ClientID = @ClientID", new Parameters("@ClientID", DbType.Int32, clientId));
            if (!object.Equals(objA, null) && !object.Equals(objA, DBNull.Value))
            {
                return objA.ToString();
            }
            return string.Empty;
        }

        public IList<string> GetUserNameList(int startRowIndexId, int maxiNumRows, int searchType, string keyword)
        {
            IList<string> list = new List<string>();
            CommonListParameters cmdParams = new CommonListParameters(startRowIndexId, maxiNumRows);
            cmdParams.TableName = "PE_Users";
            cmdParams.StrColumn = "UserName";
            cmdParams.SortColumn = "UserId";
            cmdParams.Sorts = Sorts.Asc;
            switch (searchType)
            {
                case 0:
                    cmdParams.Filter = " GroupID = " + DBHelper.ToNumber(keyword);
                    break;

                case 1:
                    cmdParams.Filter = " UserName LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'";
                    break;
            }
            cmdParams.CreateParameter();
            using (NullableDataReader reader = DBHelper.ExecuteReaderProc("PR_Common_GetList", cmdParams))
            {
                while (reader.Read())
                {
                    list.Add(reader.GetString("UserName"));
                }
            }
            return list;
        }

        public int GetUserNameListTotal(int searchType, string keyword)
        {
            string strSql = "SELECT COUNT(*) FROM PE_Users ";
            Parameters cmdParams = new Parameters();
            if (searchType == 1)
            {
                strSql = strSql + " WHERE UserName LIKE @UserName";
                cmdParams.AddInParameter("@UserName", DbType.String, "%" + DBHelper.FilterBadChar(keyword) + "%");
            }
            else
            {
                strSql = strSql + "WHERE GroupId = @GroupId";
                cmdParams.AddInParameter("@GroupId", DbType.Int32, Convert.ToInt32(keyword));
            }
            object objA = DBHelper.ExecuteScalarSql(strSql, cmdParams);
            if (!object.Equals(objA, null))
            {
                return Convert.ToInt32(objA);
            }
            return 0;
        }

        private static Parameters GetUserParameters(UserInfo usersInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@UserID", DbType.Int32, usersInfo.UserId);
            parameters.AddInParameter("@GroupID", DbType.Int32, usersInfo.GroupId);
            parameters.AddInParameter("@CompanyID", DbType.Int32, usersInfo.CompanyId);
            parameters.AddInParameter("@ClientID", DbType.Int32, usersInfo.ClientId);
            parameters.AddInParameter("@UserType", DbType.Int32, usersInfo.UserType);
            parameters.AddInParameter("@UserName", DbType.String, usersInfo.UserName);
            parameters.AddInParameter("@UserPassword", DbType.String, usersInfo.UserPassword);
            parameters.AddInParameter("@LastPassword", DbType.String, usersInfo.LastPassword);
            parameters.AddInParameter("@PayPassword", DbType.String, usersInfo.PayPassword);
            parameters.AddInParameter("@Question", DbType.String, usersInfo.Question);
            parameters.AddInParameter("@Answer", DbType.String, usersInfo.Answer);
            parameters.AddInParameter("@Email", DbType.String, usersInfo.Email);
            parameters.AddInParameter("@Sex", DbType.Int32, usersInfo.Sex);
            parameters.AddInParameter("@RegTime", DbType.DateTime, usersInfo.RegTime);
            parameters.AddInParameter("@JoinTime", DbType.DateTime, usersInfo.JoinTime);
            parameters.AddInParameter("@LoginTimes", DbType.Int32, usersInfo.LogOnTimes);
            parameters.AddInParameter("@LastLoginTime", DbType.DateTime, usersInfo.LastLogOnTime);
            parameters.AddInParameter("@LastPresentTime", DbType.DateTime, usersInfo.LastPresentTime);
            parameters.AddInParameter("@LastLoginIP", DbType.String, usersInfo.LastLogOnIP);
            parameters.AddInParameter("@LastPasswordChangedTime", DbType.DateTime, usersInfo.LastPasswordChangedTime);
            parameters.AddInParameter("@LastLockoutTime", DbType.DateTime, usersInfo.LastLockoutTime);
            parameters.AddInParameter("@FailedPasswordAttemptCount", DbType.Int32, usersInfo.FailedPasswordAttemptCount);
            parameters.AddInParameter("@FirstFailedPasswordAttempTime", DbType.DateTime, usersInfo.FirstFailedPasswordAttempTime);
            parameters.AddInParameter("@FailedPasswordAnswerAttempCount", DbType.Int32, usersInfo.FailedPasswordAnswerAttempCount);
            parameters.AddInParameter("@FirstFailedPasswordAnswerAttempTime", DbType.DateTime, usersInfo.FirstFailedPasswordAnswerAttempTime);
            parameters.AddInParameter("@Status", DbType.Int32, usersInfo.Status);
            parameters.AddInParameter("@CheckNum", DbType.AnsiStringFixedLength, usersInfo.CheckNum);
            parameters.AddInParameter("@EnableResetPassword", DbType.Boolean, usersInfo.EnableResetPassword);
            parameters.AddInParameter("@UserFace", DbType.String, usersInfo.UserFace);
            parameters.AddInParameter("@FaceWidth", DbType.Int32, usersInfo.FaceWidth);
            parameters.AddInParameter("@FaceHeight", DbType.Int32, usersInfo.FaceHeight);
            parameters.AddInParameter("@Sign", DbType.String, usersInfo.Sign);
            parameters.AddInParameter("@PrivacySetting", DbType.Int32, usersInfo.PrivacySetting);
            parameters.AddInParameter("@Balance", DbType.Currency, usersInfo.Balance);
            parameters.AddInParameter("@UserPoint", DbType.Int32, usersInfo.UserPoint);
            parameters.AddInParameter("@UserExp", DbType.Int32, usersInfo.UserExp);
            parameters.AddInParameter("@ConsumeMoney", DbType.Int32, usersInfo.ConsumeMoney);
            parameters.AddInParameter("@ConsumePoint", DbType.Int32, usersInfo.ConsumePoint);
            parameters.AddInParameter("@ConsumeExp", DbType.Int32, usersInfo.ConsumeExp);
            parameters.AddInParameter("@PostItems", DbType.Int32, usersInfo.PostItems);
            parameters.AddInParameter("@PassedItems", DbType.Int32, usersInfo.PassedItems);
            parameters.AddInParameter("@RejectItems", DbType.Int32, usersInfo.RejectItems);
            parameters.AddInParameter("@DelItems", DbType.Int32, usersInfo.DelItems);
            parameters.AddInParameter("@EndTime", DbType.DateTime, usersInfo.EndTime);
            parameters.AddInParameter("@IsInheritGroupRole", DbType.Boolean, usersInfo.IsInheritGroupRole);
            parameters.AddInParameter("@TrueName", DbType.String, usersInfo.UserTrueName);
            return parameters;
        }

        public UserInfo GetUsersByEmail(string email)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@Email", DbType.String, email);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM [PE_Users] WHERE Email = @Email", cmdParams))
            {
                if (reader.Read())
                {
                    return UsersFromrdr(reader);
                }
                return new UserInfo(true);
            }
        }

        public IList<UserInfo> GetUsersByGroupId(string groupId)
        {
            IList<UserInfo> list = new List<UserInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_Users WHERE GroupId IN (" + DBHelper.ToValidId(groupId) + ")"))
            {
                while (reader.Read())
                {
                    list.Add(UsersFromrdr(reader));
                }
            }
            return list;
        }

        public IList<UserInfo> GetUsersByUserId(string userId)
        {
            IList<UserInfo> list = new List<UserInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_Users WHERE UserId IN (" + DBHelper.ToValidId(userId) + ")"))
            {
                while (reader.Read())
                {
                    list.Add(UsersFromrdr(reader));
                }
            }
            return list;
        }

        public UserInfo GetUsersByUserName(string userName)
        {
            Parameters cmdParams = new Parameters("@UserName", DbType.String, userName);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM [PE_Users] WHERE UserName = @UserName", cmdParams))
            {
                if (reader.Read())
                {
                    return UsersFromrdr(reader);
                }
                return new UserInfo(true);
            }
        }

        public bool LockUser(int userId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@Status", DbType.Int32, UserStatus.Locked);
            cmdParams.AddInParameter("@UserID", DbType.Int32, userId);
            string strSql = "UPDATE PE_Users SET [Status]=@Status,LastLockoutTime=GETDATE() WHERE UserID = @UserID";
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool MinusPoint(int infoPoint, string userName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@UserPoint", DbType.Int32, infoPoint);
            cmdParams.AddInParameter("@UserName", DbType.String, userName);
            string strSql = "UPDATE PE_Users SET UserPoint = UserPoint - @UserPoint WHERE UserName = @UserName";
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool MoveBetweenUserId(int startUserId, int endUserId, int groupId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@UserType", DbType.Int32, 3);
            cmdParams.AddInParameter("@StartUserId", DbType.Int32, startUserId);
            cmdParams.AddInParameter("@EndUserId", DbType.Int32, endUserId);
            cmdParams.AddInParameter("@GroupId", DbType.Int32, groupId);
            return DBHelper.ExecuteProc("PR_UserManage_Users_BatchMove", cmdParams);
        }

        public bool MoveByGroups(string groupId, int targetGroupId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@UserType", DbType.Int32, 4);
            cmdParams.AddInParameter("@BatchUserGroupId", DbType.String, groupId);
            cmdParams.AddInParameter("@GroupId", DbType.Int32, targetGroupId);
            return DBHelper.ExecuteProc("PR_UserManage_Users_BatchMove", cmdParams);
        }

        public bool MoveByUserName(string userName, int groupId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@UserType", DbType.Int32, 2);
            cmdParams.AddInParameter("@UserName", DbType.String, userName);
            cmdParams.AddInParameter("@GroupId", DbType.Int32, groupId);
            return DBHelper.ExecuteProc("PR_UserManage_Users_BatchMove", cmdParams);
        }

        public bool MoveByUsers(string userId, int groupId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@UserId", DbType.String, userId);
            cmdParams.AddInParameter("@GroupId", DbType.Int32, groupId);
            return DBHelper.ExecuteProc("PR_UserManage_Users_BatchMove", cmdParams);
        }

        public bool RemoveFromAdminCompany(string userName)
        {
            return DBHelper.ExecuteSql("UPDATE PE_Users SET UserType = 3 WHERE UserType = 2 AND UserName = @UserName", new Parameters("@UserName", DbType.String, userName));
        }

        public bool RemoveFromCompany(string userName)
        {
            return DBHelper.ExecuteSql("UPDATE PE_Users SET UserType = 0, CompanyID = 0, ClientID = 0 WHERE UserName = @UserName", new Parameters("@UserName", DbType.String, userName));
        }

        public bool SaveUserPurview(UserPurviewInfo userPurviewInfo, int userId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@UserId", DbType.Int32, userId);
            cmdParams.AddInParameter("@UserSetting", DbType.String, ser.SerializeField(userPurviewInfo));
            return DBHelper.ExecuteSql("UPDATE PE_Users SET UserSetting = @UserSetting WHERE UserId = @UserId", cmdParams);
        }

        public bool SaveUserPurview(bool inheritGroupRole, int userId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@UserId", DbType.Int32, userId);
            cmdParams.AddInParameter("@IsInheritGroupRole", DbType.Boolean, inheritGroupRole);
            return DBHelper.ExecuteSql("UPDATE PE_Users SET IsInheritGroupRole = @IsInheritGroupRole WHERE UserId = @UserId", cmdParams);
        }

        public bool Update(UserInfo usersInfo)
        {
            return DBHelper.ExecuteProc("PR_UserManage_Users_Update", GetUserParameters(usersInfo));
        }

        public bool Update(int userId, string fieldName, string fieldValue)
        {
            return DBHelper.ExecuteSql(string.Format("UPDATE PE_Users SET {0}='{1}' WHERE [UserID]={2}", DBHelper.FilterBadChar(fieldName), DBHelper.FilterBadChar(fieldValue), userId.ToString()));
        }

        public bool UpdateForCompany(int companyId, string userName, UserType userType, int companyClientId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@CompanyID", DbType.Int32, companyId);
            cmdParams.AddInParameter("@UserName", DbType.String, userName);
            cmdParams.AddInParameter("@UserType", DbType.String, (int) userType);
            if (companyClientId > 0)
            {
                cmdParams.AddInParameter("@ClientID", DbType.Int32, companyClientId);
                return DBHelper.ExecuteSql("UPDATE PE_Users SET UserType = @UserType, CompanyID = @CompanyID, ClientID = @ClientID WHERE UserName = @UserName", cmdParams);
            }
            return DBHelper.ExecuteSql("UPDATE PE_Users SET UserType = @UserType, CompanyID = @CompanyID WHERE UserName = @UserName", cmdParams);
        }

        public bool UpdateUserFriendGroup(string userName, string userFriendGroup)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@UserName", DbType.String, userName);
            cmdParams.AddInParameter("@UserFriendGroup", DbType.String, userFriendGroup);
            return DBHelper.ExecuteSql("UPDATE PE_Users SET UserFriendGroup = @UserFriendGroup WHERE UserName = @UserName", cmdParams);
        }

        public bool UpdateUserStatus(int userId, UserStatus userStatus)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@UserID", DbType.Int32, userId);
            cmdParams.AddInParameter("@Status", DbType.Int32, userStatus);
            string strSql = "UPDATE PE_Users SET [Status]=@Status WHERE UserID=@UserID";
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        private static UserInfo UsersFromrdr(NullableDataReader rdr)
        {
            UserInfo info = new UserInfo();
            info.UserId = rdr.GetInt32("UserID");
            info.GroupId = rdr.GetInt32("GroupID");
            info.CompanyId = rdr.GetInt32("CompanyID");
            info.ClientId = rdr.GetInt32("ClientID");
            info.UserType = (UserType) rdr.GetInt32("UserType");
            info.UserName = rdr.GetString("UserName");
            info.UserPassword = rdr.GetString("UserPassword");
            info.LastPassword = rdr.GetString("LastPassword");
            info.PayPassword = rdr.GetString("PayPassword");
            info.Question = rdr.GetString("Question");
            info.Answer = rdr.GetString("Answer");
            info.Email = rdr.GetString("Email");
            info.Sex = (UserSexType) rdr.GetInt32("Sex");
            info.RegTime = rdr.GetDateTime("RegTime");
            info.JoinTime = rdr.GetDateTime("JoinTime");
            info.LogOnTimes = rdr.GetInt32("LoginTimes");
            info.LastLogOnTime = rdr.GetNullableDateTime("LastLoginTime");
            info.LastPresentTime = rdr.GetNullableDateTime("LastPresentTime");
            info.LastLogOnIP = rdr.GetString("LastLoginIP");
            info.LastPasswordChangedTime = rdr.GetNullableDateTime("LastPasswordChangedTime");
            info.LastLockoutTime = rdr.GetNullableDateTime("LastLockoutTime");
            info.FailedPasswordAttemptCount = rdr.GetInt32("FailedPasswordAttemptCount");
            info.FirstFailedPasswordAttempTime = rdr.GetNullableDateTime("FirstFailedPasswordAttempTime");
            info.FailedPasswordAnswerAttempCount = rdr.GetInt32("FailedPasswordAnswerAttempCount");
            info.FirstFailedPasswordAnswerAttempTime = rdr.GetNullableDateTime("FirstFailedPasswordAnswerAttempTime");
            info.Status = (UserStatus) rdr.GetInt32("Status");
            info.CheckNum = rdr.GetString("CheckNum");
            info.EnableResetPassword = rdr.GetBoolean("EnableResetPassword");
            info.UserFace = rdr.GetString("UserFace");
            info.FaceWidth = rdr.GetInt32("FaceWidth");
            info.FaceHeight = rdr.GetInt32("FaceHeight");
            info.Sign = rdr.GetString("Sign");
            info.PrivacySetting = rdr.GetInt32("PrivacySetting");
            info.Balance = rdr.GetDecimal("Balance");
            info.UserPoint = rdr.GetInt32("UserPoint");
            info.UserExp = rdr.GetInt32("UserExp");
            info.ConsumeMoney = rdr.GetInt32("ConsumeMoney");
            info.ConsumePoint = rdr.GetInt32("ConsumePoint");
            info.ConsumeExp = rdr.GetInt32("ConsumeExp");
            info.PostItems = rdr.GetInt32("PostItems");
            info.PassedItems = rdr.GetInt32("PassedItems");
            info.RejectItems = rdr.GetInt32("RejectItems");
            info.DelItems = rdr.GetInt32("DelItems");
            info.EndTime = rdr.GetNullableDateTime("EndTime");
            info.IsInheritGroupRole = rdr.GetBoolean("IsInheritGroupRole");
            info.UserSetting = rdr.GetString("UserSetting");
            info.UserFriendGroup = rdr.GetString("UserFriendGroup");
            info.UserTrueName = rdr.GetString("TrueName");
            return info;
        }

        public int ValidateUser(string userName, string password)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@UserName", DbType.String, userName);
            cmdParams.AddInParameter("@Password", DbType.String, password);
            int num = -1;
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT UserId FROM [PE_Users] WHERE UserName = @UserName AND UserPassword = @Password", cmdParams))
            {
                if (reader.Read())
                {
                    num = reader.GetInt32("UserId");
                }
            }
            return num;
        }
    }
}

