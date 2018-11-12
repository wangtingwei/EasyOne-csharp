namespace EasyOne.SqlServerDal.UserManage
{
    using EasyOne.Common;
    using EasyOne.IDal.UserManage;
    using EasyOne.Model.UserManage;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;

    public class RoleMembers : IRoleMembers
    {
        public bool AddMemberToRole(int adminId, int roleId)
        {
            string strSql = "INSERT INTO PE_Admin_Roles(AdminId, RoleId) VALUES (@AdminId, @RoleId)";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@AdminId", DbType.Int32, adminId);
            cmdParams.AddInParameter("@RoleId", DbType.Int32, roleId);
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        private static AdministratorInfo GetAdminInfoFromrdr(NullableDataReader rdr)
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
            return info;
        }

        public IList<AdministratorInfo> GetMemberListByRoleId(int roleId)
        {
            IList<AdministratorInfo> list = new List<AdministratorInfo>();
            string strSql = "SELECT * FROM PE_Admin WHERE (AdminID IN (SELECT AdminID FROM PE_Admin_Roles WHERE (RoleID = @RoleID)))";
            Parameters cmdParams = new Parameters("@RoleID", DbType.Int32, roleId);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, cmdParams))
            {
                while (reader.Read())
                {
                    AdministratorInfo adminInfoFromrdr = GetAdminInfoFromrdr(reader);
                    list.Add(adminInfoFromrdr);
                }
            }
            return list;
        }

        public IList<AdministratorInfo> GetMemberListNotInRole(int roleId)
        {
            IList<AdministratorInfo> list = new List<AdministratorInfo>();
            string strSql = "SELECT * FROM PE_Admin WHERE (AdminID NOT IN (SELECT AdminID FROM PE_Admin_Roles WHERE (RoleID = @RoleID)))";
            Parameters cmdParams = new Parameters("@RoleID", DbType.Int32, roleId);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, cmdParams))
            {
                while (reader.Read())
                {
                    AdministratorInfo adminInfoFromrdr = GetAdminInfoFromrdr(reader);
                    list.Add(adminInfoFromrdr);
                }
            }
            return list;
        }

        public string GetRoleIdListByAdminId(int adminId)
        {
            string strSql = "SELECT RoleId FROM PE_Admin_Roles WHERE AdminId = @AdminId";
            Parameters cmdParams = new Parameters("@AdminId", DbType.Int32, adminId);
            StringBuilder sb = new StringBuilder();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, cmdParams))
            {
                while (reader.Read())
                {
                    StringHelper.AppendString(sb, reader.GetInt32("RoleId").ToString());
                }
            }
            return sb.ToString();
        }

        public void RemoveAdminFromRolesByRoleId(int roleId)
        {
            string strSql = "DELETE FROM PE_Admin_Roles WHERE RoleID = @RoleID";
            Parameters cmdParams = new Parameters("@RoleID", DbType.Int32, roleId);
            DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public void RemoveMemberFromAllRoles(int adminId)
        {
            string strSql = "DELETE FROM PE_Admin_Roles WHERE AdminId = @AdminId";
            Parameters cmdParams = new Parameters("@AdminId", DbType.Int32, adminId);
            DBHelper.ExecuteSql(strSql, cmdParams);
        }
    }
}

