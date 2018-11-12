namespace EasyOne.SqlServerDal.UserManage
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.Enumerations;
    using EasyOne.IDal.UserManage;
    using EasyOne.Model.UserManage;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    public class UserGroups : IUserGroups
    {
        private int m_NumUserGroups;

        public bool Add(UserGroupsInfo userGroupsInfo)
        {
            userGroupsInfo.GroupId = GetNewGroupId();
            Parameters parms = new Parameters();
            parms = GetProcdbComm(parms, userGroupsInfo);
            bool flag = false;
            try
            {
                if (DBHelper.ExecuteNonQueryProc("PR_UserManage_UserGroups_Add", parms) > 0)
                {
                    flag = true;
                }
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        public bool Delete(int id)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@GroupId", DbType.Int32, id);
            string strSql = "DELETE FROM PE_UserGroups WHERE GroupID = @GroupId";
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        private static IList<UserGroupsInfo> GetGroupInfoList(string strSql, Parameters parms)
        {
            IList<UserGroupsInfo> list = new List<UserGroupsInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, parms))
            {
                while (reader.Read())
                {
                    UserGroupsInfo userGroupsInfo = new UserGroupsInfo();
                    UserGroupsFromrdr(userGroupsInfo, reader);
                    list.Add(userGroupsInfo);
                }
            }
            return list;
        }

        public int GetMaxId()
        {
            return DBHelper.GetMaxId("PE_UserGroups", "GroupID");
        }

        private static int GetNewGroupId()
        {
            int maxId = DBHelper.GetMaxId("PE_UserGroups", "GroupID");
            if (maxId < 0)
            {
                maxId = 0;
            }
            return (maxId + 1);
        }

        public int GetNumberUserGroups()
        {
            return this.m_NumUserGroups;
        }

        private static Parameters GetProcdbComm(Parameters parms, UserGroupsInfo userGroupsInfo)
        {
            parms.AddInParameter("@GroupID", DbType.Int32, userGroupsInfo.GroupId);
            parms.AddInParameter("@GroupName", DbType.String, userGroupsInfo.GroupName);
            parms.AddInParameter("@Description", DbType.String, userGroupsInfo.Description);
            parms.AddInParameter("@Settings", DbType.String, userGroupsInfo.Settings);
            parms.AddInParameter("@GroupType", DbType.Int32, userGroupsInfo.GroupType);
            parms.AddInParameter("@GroupSetting", DbType.String, userGroupsInfo.GroupSetting);
            return parms;
        }

        public UserGroupsInfo GetUserGroupById(int id)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@GroupID", DbType.Int32, id);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_UserGroups WHERE GroupID = @GroupID", cmdParams))
            {
                if (reader.Read())
                {
                    UserGroupsInfo userGroupsInfo = new UserGroupsInfo();
                    UserGroupsFromrdr(userGroupsInfo, reader);
                    return userGroupsInfo;
                }
                return new UserGroupsInfo(true);
            }
        }

        public IList<UserGroupsInfo> GetUserGroupList(GroupType groupType)
        {
            string strSql = "SELECT * FROM PE_UserGroups WHERE GroupType = @GroupType ORDER BY GroupID ASC";
            Parameters parms = new Parameters();
            parms.AddInParameter("@GroupType", DbType.Int32, groupType);
            return GetGroupInfoList(strSql, parms);
        }

        public IList<UserGroupsInfo> GetUserGroupList(int startRowIndexId, int maxNumberRows)
        {
            string filter = string.Empty;
            return this.GetUserGroupsList(filter, startRowIndexId, maxNumberRows);
        }

        private IList<UserGroupsInfo> GetUserGroupsList(string filter, int startRowIndexId, int maxNumberRows)
        {
            IList<UserGroupsInfo> list = new List<UserGroupsInfo>();
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "GroupID");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "*");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "ASC");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, filter);
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_UserGroups");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    UserGroupsInfo userGroupsInfo = new UserGroupsInfo();
                    UserGroupsFromrdr(userGroupsInfo, reader);
                    list.Add(userGroupsInfo);
                }
            }
            this.m_NumUserGroups = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public int GetUserInGroupNumber(int groupId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@groupId", DbType.String, groupId);
            object obj2 = DBHelper.ExecuteScalarSql("SELECT COUNT(*) FROM PE_Users WHERE groupId = @groupId", cmdParams);
            if (obj2 == null)
            {
                return 0;
            }
            return Convert.ToInt32(obj2);
        }

        public bool Update(UserGroupsInfo userGroupsInfo)
        {
            Parameters parms = new Parameters();
            parms = GetProcdbComm(parms, userGroupsInfo);
            bool flag = false;
            if (DBHelper.ExecuteNonQueryProc("PR_UserManage_UserGroups_Update", parms) > 0)
            {
                flag = true;
            }
            return flag;
        }

        public bool UserGroupIsExist(string groupName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@GroupName", DbType.String, groupName);
            bool flag = false;
            if (((int) DBHelper.ExecuteScalarSql("SELECT COUNT(*) FROM PE_UserGroups WHERE GroupName = @GroupName", cmdParams)) > 0)
            {
                flag = true;
            }
            return flag;
        }

        private static void UserGroupsFromrdr(UserGroupsInfo userGroupsInfo, NullableDataReader rdr)
        {
            userGroupsInfo.GroupId = rdr.GetInt32("GroupID");
            userGroupsInfo.GroupName = rdr.GetString("GroupName");
            userGroupsInfo.Description = rdr.GetString("Description");
            userGroupsInfo.Settings = rdr.GetString("Settings");
            userGroupsInfo.GroupType = (GroupType) rdr.GetInt32("GroupType");
            userGroupsInfo.GroupSetting = rdr.GetString("GroupSetting");
        }
    }
}

