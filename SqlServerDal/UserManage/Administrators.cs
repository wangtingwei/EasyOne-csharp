namespace EasyOne.SqlServerDal.UserManage
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.Common;
    using EasyOne.IDal.UserManage;
    using EasyOne.Model.UserManage;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Text;

    public class Administrators : IAdministrator
    {
        private int m_TotalOfAdmins;

        public bool Add(AdministratorInfo administratorInfo)
        {
            string strSql = "INSERT INTO PE_Admin (AdminId, AdminName, AdminPassword, UserName, EnableMultiLogin, RndPassword, LoginTimes, LastLoginIP, LastLoginTime, LastLogoutTime, LastModifyPasswordTime, IsLock, EnableModifyPassword) VALUES (@AdminId, @AdminName, @AdminPassword, @UserName, @EnableMultiLogin, @RndPassword, @LoginTimes, @LastLoginIp, @LastLoginTime, @LastLogoutTime, @LastModifyPasswordTime, @IsLock, @EnableModifyPassword)";
            administratorInfo.AdminId = GetNewId();
            Parameters cmdParams = GetParameters(administratorInfo);
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public IList<AdministratorInfo> AdminList(int startRowIndexId, int maxNumberRows, string userName)
        {
            return this.AdminList(startRowIndexId, maxNumberRows, 1, userName);
        }

        public IList<AdministratorInfo> AdminList(int startRowIndexId, int maxNumberRows, int roleId, int listType)
        {
            string filter = string.Empty;
            if (roleId == -1)
            {
                if (listType == 1)
                {
                    filter = "DATEDIFF(d, LastModifyPasswordTime, GETDATE()) > 30 OR LastModifyPasswordTime IS NULL ";
                }
            }
            else
            {
                filter = " AdminID IN (SELECT PEAR.AdminID FROM PE_Admin_Roles AS PEAR WHERE (PEAR.RoleID = " + roleId + "))";
            }
            return this.GetList(startRowIndexId, maxNumberRows, filter);
        }

        public IList<AdministratorInfo> AdminList(int startRowIndexId, int maxNumberRows, int searchType, string keyword)
        {
            string filter = string.Empty;
            switch (searchType)
            {
                case 0:
                    filter = filter + "AdminName LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'";
                    break;

                case 1:
                    filter = filter + "UserName LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'";
                    break;
            }
            return this.GetList(startRowIndexId, maxNumberRows, filter);
        }

        public bool Delete(int adminId)
        {
            string strSql = "DELETE FROM PE_Admin WHERE AdminId = @AdminId";
            Parameters cmdParams = new Parameters("@AdminId", DbType.Int32, adminId);
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public AdministratorInfo GetAdministrator(int adminId, string adminName, string userName)
        {
            string strSql = "SELECT * FROM PE_Admin WHERE 1 = 1 ";
            Parameters cmdParams = new Parameters();
            if (adminId > 0)
            {
                strSql = strSql + " AND AdminId = @AdminId ";
                cmdParams.AddInParameter("@AdminId", DbType.Int32, adminId);
            }
            if (!string.IsNullOrEmpty(adminName))
            {
                strSql = strSql + " AND AdminName = @AdminName ";
                cmdParams.AddInParameter("@AdminName", DbType.String, adminName);
            }
            if (!string.IsNullOrEmpty(userName))
            {
                strSql = strSql + " AND UserName = @UserName ";
                cmdParams.AddInParameter("@UserName", DbType.String, userName);
            }
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, cmdParams))
            {
                if (reader.Read())
                {
                    return GetRAdminInfoFromrdr(reader);
                }
                return new AdministratorInfo(true);
            }
        }

        public IList<AdministratorInfo> GetAdminList(int startRowIndexId, int maxNumberRows, string adminName)
        {
            string filter = string.Empty;
            if (!string.IsNullOrEmpty(adminName))
            {
                filter = "AdminName LIKE '%" + DBHelper.FilterBadChar(adminName) + "%'";
            }
            return this.GetList(startRowIndexId, maxNumberRows, filter);
        }

        public IList<AdministratorInfo> GetAdminListByOperateCode(int startRowIndexId, int maxNumberRows, int operateCode)
        {
            string filter = string.Empty;
            filter = " AdminID IN (SELECT DISTINCT(PEAR.AdminID) FROM PE_Admin_Roles AS PEAR WHERE PEAR.RoleID IN (SELECT RoleID FROM PE_Roles_Permissions WHERE OperateCode = " + operateCode + "))";
            return this.GetList(startRowIndexId, maxNumberRows, filter);
        }

        private IList<AdministratorInfo> GetList(int startRowIndexId, int maxNumberRows, string filter)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "AdminId");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "*");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, filter);
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_Admin");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            IList<AdministratorInfo> list = new List<AdministratorInfo>();
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    AdministratorInfo rAdminInfoFromrdr = GetRAdminInfoFromrdr(reader);
                    rAdminInfoFromrdr.RoleList = GetRoleNameListByAdminId(rAdminInfoFromrdr.AdminId);
                    list.Add(rAdminInfoFromrdr);
                }
            }
            this.m_TotalOfAdmins = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        private static int GetNewId()
        {
            return (DBHelper.GetMaxId("PE_Admin", "AdminId") + 1);
        }

        private static Parameters GetParameters(AdministratorInfo administratorInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@AdminId", DbType.Int32, administratorInfo.AdminId);
            parameters.AddInParameter("@AdminName", DbType.String, administratorInfo.AdminName);
            parameters.AddInParameter("@AdminPassword", DbType.String, administratorInfo.AdminPassword);
            parameters.AddInParameter("@UserName", DbType.String, administratorInfo.UserName);
            parameters.AddInParameter("@EnableMultiLogin", DbType.Boolean, administratorInfo.EnableMultiLogOn);
            parameters.AddInParameter("@RndPassword", DbType.String, administratorInfo.RndPassword);
            parameters.AddInParameter("@LoginTimes", DbType.Int32, administratorInfo.LogOnTimes);
            parameters.AddInParameter("@LastLoginIp", DbType.String, administratorInfo.LastLogOnIP);
            parameters.AddInParameter("@LastLoginTime", DbType.DateTime, administratorInfo.LastLogOnTime);
            parameters.AddInParameter("@LastLogoutTime", DbType.DateTime, administratorInfo.LastLogOffTime);
            parameters.AddInParameter("@LastModifyPasswordTime", DbType.DateTime, administratorInfo.LastModifyPasswordTime);
            parameters.AddInParameter("@IsLock", DbType.Boolean, administratorInfo.IsLock);
            parameters.AddInParameter("@EnableModifyPassword", DbType.Boolean, administratorInfo.EnableModifyPassword);
            return parameters;
        }

        private static AdministratorInfo GetRAdminInfoFromrdr(NullableDataReader rdr)
        {
            AdministratorInfo info = new AdministratorInfo();
            info.AdminId = rdr.GetInt32("AdminId");
            info.AdminName = rdr.GetString("AdminName");
            info.AdminPassword = rdr.GetString("AdminPassword");
            info.UserName = rdr.GetString("UserName");
            info.EnableMultiLogOn = rdr.GetBoolean("EnableMultilogin");
            info.RndPassword = rdr.GetString("RndPassword");
            info.LogOnTimes = rdr.GetInt32("LoginTimes");
            info.LastLogOnIP = rdr.GetString("LastLoginIp");
            info.LastLogOnTime = rdr.GetNullableDateTime("LastLoginTime");
            info.LastLogOffTime = rdr.GetNullableDateTime("LastLogoutTime");
            info.LastModifyPasswordTime = rdr.GetNullableDateTime("LastModifyPasswordTime");
            info.IsLock = rdr.GetBoolean("IsLock");
            info.EnableModifyPassword = rdr.GetBoolean("EnableModifyPassword");
            return info;
        }

        private static string GetRoleNameListByAdminId(int adminId)
        {
            string strSql = "SELECT RoleName FROM PE_Roles WHERE RoleId IN(SELECT RoleId FROM PE_Admin_Roles WHERE AdminId = @AdminId)";
            Parameters cmdParams = new Parameters("@AdminId", DbType.Int32, adminId);
            StringBuilder sb = new StringBuilder();
            if (DBHelper.ExistsSql("SELECT COUNT(*) FROM PE_Admin_Roles WHERE AdminId = @AdminId AND RoleId = 0", cmdParams))
            {
                sb.Append("超级管理员");
            }
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, cmdParams))
            {
                while (reader.Read())
                {
                    StringHelper.AppendString(sb, reader.GetString("RoleName"));
                }
            }
            return sb.ToString();
        }

        public int GetTotalOfAdmin()
        {
            return this.m_TotalOfAdmins;
        }

        public bool IsExist(string adminName)
        {
            string strSql = "SELECT COUNT(*) FROM PE_Admin WHERE AdminName = @AdminName";
            Parameters cmdParams = new Parameters("@AdminName", DbType.String, adminName);
            return DBHelper.ExistsSql(strSql, cmdParams);
        }

        public bool Update(AdministratorInfo administratorInfo)
        {
            string strSql = "UPDATE PE_Admin SET AdminName = @AdminName, AdminPassword = @AdminPassword, UserName = @UserName, EnableMultilogin = @EnableMultilogin, RndPassword = @RndPassword, LoginTimes = @LoginTimes, LastLoginIp = @LastLoginIp, LastLoginTime = @LastLoginTime, LastLogoutTime = @LastLogoutTime, LastModifyPasswordTime = @LastModifyPasswordTime, IsLock = @IsLock, EnableModifyPassword = @EnableModifyPassword WHERE AdminId = @AdminId";
            Parameters cmdParams = GetParameters(administratorInfo);
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }
    }
}

