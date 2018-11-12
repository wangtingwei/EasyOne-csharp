namespace EasyOne.SqlServerDal.UserManage
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.Enumerations;
    using EasyOne.IDal.UserManage;
    using EasyOne.Model.AccessManage;
    using EasyOne.Model.UserManage;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Text;

    public class UserRole : IUserRole
    {
        private int m_TotalOfRoles;

        public bool AccessCheckNodePermissions(string nodeId, OperateCode operateCode)
        {
            string strSql = "SELECT nodeId PE_Role_Node_Permissions WHERE NodeId IN (" + DBHelper.ToValidId(nodeId) + ") AND OperateCode = @OperateCode";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@OperateCode", DbType.Int32, operateCode);
            return DBHelper.ExistsSql(strSql, cmdParams);
        }

        public bool Add(RoleInfo roleInfo)
        {
            string strSql = "INSERT INTO PE_Roles(RoleId, RoleName, Description) VALUES (@RoleId, @RoleName, @Description)";
            roleInfo.RoleId = GetNewId();
            Parameters cmdParams = GetParameters(roleInfo);
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool AddFieldPermissionToRoles(int roleId, int modelId, string fieldName, OperateCode operateCode)
        {
            string strSql = "INSERT INTO PE_Role_Field_Permissions(RoleId, ModelId, FieldName, OperateCode) VALUES (@RoleId, @ModelId, @FieldName, @OperateCode)";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@RoleId", DbType.Int32, roleId);
            cmdParams.AddInParameter("@ModelId", DbType.Int32, modelId);
            cmdParams.AddInParameter("@FieldName", DbType.String, fieldName);
            cmdParams.AddInParameter("@OperateCode", DbType.Int32, operateCode);
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public void AddNodePermissionToRoles(int roleId, int nodeId, OperateCode operateCode)
        {
            string strSql = "INSERT INTO PE_Role_Node_Permissions(RoleId, NodeId, OperateCode) VALUES (@RoleId, @NodeId, @OperateCode)";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@RoleId", DbType.Int32, roleId);
            cmdParams.AddInParameter("@NodeId", DbType.Int32, nodeId);
            cmdParams.AddInParameter("@OperateCode", DbType.Int32, operateCode);
            DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public void AddPermissionToRoles(int roleId, OperateCode operateCode)
        {
            string strSql = "INSERT INTO PE_Roles_Permissions(RoleId, OperateCode) VALUES (@RoleId, @OperateCode)";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@RoleId", DbType.Int32, roleId);
            cmdParams.AddInParameter("@OperateCode", DbType.Int32, operateCode);
            DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public void AddPermissionToRoles(int roleId, int operateCode)
        {
            string strSql = "INSERT INTO PE_Roles_Permissions(RoleId, OperateCode) VALUES (@RoleId, @OperateCode)";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@RoleId", DbType.Int32, roleId);
            cmdParams.AddInParameter("@OperateCode", DbType.Int32, operateCode);
            DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public void AddSepcialPermissionToRoles(int roleId, int specialId, OperateCode operateCode)
        {
            string strSql = "INSERT INTO PE_Role_Special_Permissions(RoleId, SpecialId, OperateCode) VALUES (@RoleId, @SpecialId, @OperateCode)";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@RoleId", DbType.Int32, roleId);
            cmdParams.AddInParameter("@SpecialId", DbType.Int32, specialId);
            cmdParams.AddInParameter("@OperateCode", DbType.Int32, operateCode);
            DBHelper.ExecuteSql(strSql, cmdParams);
        }
        /// <summary>
        /// 根据指定的参数据和SQL语句获取权限列表
        /// </summary>
        /// <param name="parms"></param>
        /// <param name="strSqlText"></param>
        /// <returns></returns>
        private static IList<string> CreateRoleList(Parameters parms, string strSqlText)
        {
            IList<string> list = new List<string>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSqlText, parms))
            {
                while (reader.Read())
                {
                    list.Add(reader.GetInt32("RoleId").ToString());
                }
            }
            return list;
        }

        public bool Delete(int roleId)
        {
            string strSql = "DELETE FROM PE_Roles WHERE RoleId = @RoleId";
            Parameters cmdParams = new Parameters("@RoleId", DbType.Int32, roleId);
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public void DeleteFieldPermissionFromRoles(int roleId, int modelId, string fieldName)
        {
            string strSql = "DELETE FROM PE_Role_Field_Permissions WHERE 1 = 1 ";
            Parameters cmdParams = new Parameters();
            if (roleId >= 0)
            {
                strSql = strSql + " AND RoleId = @RoleId ";
                cmdParams.AddInParameter("@RoleId", DbType.Int32, roleId);
            }
            if (modelId > 0)
            {
                strSql = strSql + " AND ModelId = @ModelId ";
                cmdParams.AddInParameter("@ModelId", DbType.Int32, modelId);
            }
            if (!string.IsNullOrEmpty(fieldName))
            {
                strSql = strSql + " AND FieldName = @FieldName ";
                cmdParams.AddInParameter("@FieldName", DbType.String, fieldName);
            }
            DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public void DeleteNodePermissionFromRoles(int roleId, int nodeId)
        {
            string strSql = "DELETE FROM PE_Role_Node_Permissions WHERE 1 = 1 ";
            Parameters cmdParams = new Parameters();
            if (nodeId >= -1)
            {
                strSql = strSql + " AND NodeId = @NodeId ";
                cmdParams.AddInParameter("@NodeId", DbType.Int32, nodeId);
            }
            if (roleId >= 0)
            {
                strSql = strSql + " AND RoleId = @RoleId ";
                cmdParams.AddInParameter("@RoleId", DbType.Int32, roleId);
            }
            DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public void DeleteNodePermissionFromRoles(int roleId, string nodeId)
        {
            string strSql = "DELETE FROM PE_Role_Node_Permissions WHERE RoleId = @RoleId AND NodeId IN (" + DBHelper.ToValidId(nodeId) + ")";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@RoleId", DbType.Int32, roleId);
            DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public void DeleteNodePermissionFromRoles(int roleId, int nodeId, OperateCode operateCode)
        {
            string strSql = "DELETE FROM PE_Role_Node_Permissions WHERE 1 = 1 ";
            Parameters cmdParams = new Parameters();
            if (nodeId >= -1)
            {
                strSql = strSql + " AND NodeId = @NodeId ";
                cmdParams.AddInParameter("@NodeId", DbType.Int32, nodeId);
            }
            if (roleId >= 0)
            {
                strSql = strSql + " AND RoleId = @RoleId ";
                cmdParams.AddInParameter("@RoleId", DbType.Int32, roleId);
            }
            strSql = strSql + " AND OperateCode = @OperateCode ";
            cmdParams.AddInParameter("@OperateCode", DbType.Int32, operateCode);
            DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public void DeletePermissionFromRoles(int roleId)
        {
            string strSql = "DELETE FROM PE_Roles_Permissions WHERE RoleId = @RoleId";
            Parameters cmdParams = new Parameters("@RoleId", DbType.Int32, roleId);
            DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public void DeleteSpecialPermissionFromRoles(int roleId, int specialId)
        {
            string strSql = "DELETE FROM PE_Role_Special_Permissions WHERE 1 = 1";
            Parameters cmdParams = new Parameters();
            if (roleId > 0)
            {
                strSql = strSql + " AND RoleId = @RoleId ";
                cmdParams.AddInParameter("@RoleId", DbType.Int32, roleId);
            }
            if (specialId > 0)
            {
                strSql = strSql + " AND SpecialId = @SpecialId ";
                cmdParams.AddInParameter("@SpecialId", DbType.Int32, specialId);
            }
            DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public IList<RoleNodePermissionsInfo> GetAllNodePermissionsById(int roleId, int nodeId)
        {
            IList<RoleNodePermissionsInfo> list = new List<RoleNodePermissionsInfo>();
            Parameters cmdParams = new Parameters();
            string strCommand = "SELECT * FROM PE_Role_Node_Permissions WHERE 1 = 1";
            if (roleId >= 0)
            {
                strCommand = strCommand + " AND RoleID = @RoleID ";
                cmdParams.AddInParameter("@RoleID", DbType.Int32, roleId);
            }
            if (nodeId >= 0)
            {
                strCommand = strCommand + " AND (NodeID = @NodeId OR NodeID=-1) ";
                cmdParams.AddInParameter("@NodeId", DbType.Int32, nodeId);
            }
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, cmdParams))
            {
                while (reader.Read())
                {
                    RoleNodePermissionsInfo item = new RoleNodePermissionsInfo();
                    item.GroupId = reader.GetInt32("RoleID");
                    item.OperateCode = (OperateCode) reader.GetInt32("OperateCode");
                    item.NodeId = reader.GetInt32("NodeID");
                    list.Add(item);
                }
            }
            return list;
        }

        public IList<RoleFieldPermissionsInfo> GetFieldPermissionsById(int roleId, int modelId, string fieldName, OperateCode operateCode)
        {
            IList<RoleFieldPermissionsInfo> list = new List<RoleFieldPermissionsInfo>();
            Parameters cmdParams = new Parameters();
            string strCommand = "SELECT * FROM PE_Role_Field_Permissions WHERE 1 = 1";
            if (roleId >= 0)
            {
                strCommand = strCommand + " AND RoleId = @RoleId ";
                cmdParams.AddInParameter("@RoleId", DbType.Int32, roleId);
            }
            if (modelId > 0)
            {
                strCommand = strCommand + " AND ModelId = @ModelId ";
                cmdParams.AddInParameter("@ModelId", DbType.Int32, modelId);
            }
            if (!string.IsNullOrEmpty(fieldName))
            {
                strCommand = strCommand + " AND FieldName = @FieldName ";
                cmdParams.AddInParameter("@FieldName", DbType.String, fieldName);
            }
            if (operateCode != OperateCode.None)
            {
                strCommand = strCommand + " AND OperateCode = @OperateCode ";
                cmdParams.AddInParameter("@OperateCode", DbType.Int32, operateCode);
            }
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, cmdParams))
            {
                while (reader.Read())
                {
                    RoleFieldPermissionsInfo item = new RoleFieldPermissionsInfo();
                    item.RoleId = reader.GetInt32("RoleID");
                    item.OperateCode = (OperateCode) reader.GetInt32("OperateCode");
                    item.ModelId = reader.GetInt32("ModelID");
                    item.FieldName = reader.GetString("FieldName");
                    list.Add(item);
                }
            }
            return list;
        }

        public IList<RoleModulePermissionsInfo> GetModelPermissionsById(int roleId)
        {
            IList<RoleModulePermissionsInfo> list = new List<RoleModulePermissionsInfo>();
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@RoleID", DbType.Int32, roleId);
            string strCommand = "SELECT * FROM PE_Roles_Permissions WHERE RoleID = @RoleID";
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, cmdParams))
            {
                while (reader.Read())
                {
                    RoleModulePermissionsInfo item = new RoleModulePermissionsInfo();
                    item.RoleId = reader.GetInt32("RoleID");
                    item.OperateCode = (OperateCode) reader.GetInt32("OperateCode");
                    list.Add(item);
                }
            }
            return list;
        }

        private static int GetNewId()
        {
            return (DBHelper.GetMaxId("PE_Roles", "RoleId") + 1);
        }

        public IList<RoleNodePermissionsInfo> GetNodePermissionsById(int roleId, int nodeId)
        {
            IList<RoleNodePermissionsInfo> list = new List<RoleNodePermissionsInfo>();
            Parameters cmdParams = new Parameters();
            string strCommand = "SELECT * FROM PE_Role_Node_Permissions WHERE 1 = 1";
            if (roleId >= 0)
            {
                strCommand = strCommand + " AND RoleID = @RoleID ";
                cmdParams.AddInParameter("@RoleID", DbType.Int32, roleId);
            }
            if (nodeId >= 0)
            {
                strCommand = strCommand + " AND NodeID = @NodeId ";
                cmdParams.AddInParameter("@NodeId", DbType.Int32, nodeId);
            }
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, cmdParams))
            {
                while (reader.Read())
                {
                    RoleNodePermissionsInfo item = new RoleNodePermissionsInfo();
                    item.GroupId = reader.GetInt32("RoleID");
                    item.OperateCode = (OperateCode) reader.GetInt32("OperateCode");
                    item.NodeId = reader.GetInt32("NodeID");
                    list.Add(item);
                }
            }
            return list;
        }

        public IList<RoleNodePermissionsInfo> GetNodePermissionsByNodeId(int nodeId)
        {
            IList<RoleNodePermissionsInfo> list = new List<RoleNodePermissionsInfo>();
            Parameters cmdParams = new Parameters();
            string strSql = "SELECT * FROM PE_Role_Node_Permissions WHERE NodeID = @NodeID";
            cmdParams.AddInParameter("@NodeID", DbType.Int32, nodeId);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, cmdParams))
            {
                while (reader.Read())
                {
                    RoleNodePermissionsInfo item = new RoleNodePermissionsInfo();
                    item.GroupId = reader.GetInt32("RoleID");
                    item.OperateCode = (OperateCode) reader.GetInt32("OperateCode");
                    item.NodeId = reader.GetInt32("NodeID");
                    list.Add(item);
                }
            }
            return list;
        }

        public IList<RoleModulePermissionsInfo> GetOtherModelPermissionsById(int roleId)
        {
            IList<RoleModulePermissionsInfo> list = new List<RoleModulePermissionsInfo>();
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@RoleID", DbType.Int32, roleId);
            string strCommand = " SELECT RoleID, OperateCode FROM PE_Roles_Permissions WHERE RoleID = @RoleID AND (OperateCode LIKE '9%')";
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, cmdParams))
            {
                while (reader.Read())
                {
                    RoleModulePermissionsInfo item = new RoleModulePermissionsInfo();
                    item.RoleId = reader.GetInt32("RoleID");
                    item.OperateCode = (OperateCode) reader.GetInt32("OperateCode");
                    list.Add(item);
                }
            }
            return list;
        }

        private static Parameters GetParameters(RoleInfo roleInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@RoleId", DbType.Int32, roleInfo.RoleId);
            parameters.AddInParameter("@RoleName", DbType.String, roleInfo.RoleName);
            parameters.AddInParameter("@Description", DbType.String, roleInfo.Description);
            return parameters;
        }

        public string GetRoleAllNodeId(string roleId)
        {
            string strCommand = "SELECT DISTINCT NodeID FROM PE_Role_Node_Permissions WHERE RoleID IN (" + DBHelper.ToValidId(roleId) + ")";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@RoleId", DbType.String, roleId);
            StringBuilder builder = new StringBuilder();
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, cmdParams))
            {
                while (reader.Read())
                {
                    if (builder.Length > 0)
                    {
                        builder.Append("," + reader.GetInt32("NodeID").ToString());
                    }
                    else
                    {
                        builder.Append(reader.GetInt32("NodeID").ToString());
                    }
                }
            }
            return builder.ToString();
        }

        public RoleInfo GetRoleInfoById(int roleId)
        {
            string strSql = "SELECT * FROM PE_Roles WHERE RoleId = @RoleId";
            Parameters cmdParams = new Parameters("@RoleId", DbType.Int32, roleId);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, cmdParams))
            {
                if (reader.Read())
                {
                    return GetRoleInfoFromrdr(reader);
                }
                return new RoleInfo(true);
            }
        }

        private static RoleInfo GetRoleInfoFromrdr(NullableDataReader rdr)
        {
            RoleInfo info = new RoleInfo();
            info.RoleId = rdr.GetInt32("RoleId");
            info.RoleName = rdr.GetString("RoleName");
            info.Description = rdr.GetString("Description");
            return info;
        }

        public IList<RoleInfo> GetRoleList(int startRowIndexId, int maxNumberRows)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "RoleId");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "*");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, "");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_Roles");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxNumberRows);
            IList<RoleInfo> list = new List<RoleInfo>();
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(GetRoleInfoFromrdr(reader));
                }
            }
            this.m_TotalOfRoles = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public IList<RoleInfo> GetRoleListByFlowId(int flowId)
        {
            string strSql = "SELECT * FROM PE_Roles AS U WHERE (NOT EXISTS(SELECT NULL FROM PE_Process_Roles AS P WHERE FlowId = @FlowId AND U.RoleId = P.RoleId))";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@FlowId", DbType.Int32, flowId);
            IList<RoleInfo> list = new List<RoleInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, cmdParams))
            {
                while (reader.Read())
                {
                    list.Add(GetRoleInfoFromrdr(reader));
                }
            }
            return list;
        }

        public IList<RoleInfo> GetRoleListByFlowIdAndProcessId(int flowId, int processId)
        {
            string strSql = "SELECT * FROM PE_Roles AS U WHERE (NOT EXISTS(SELECT NULL AS Exp FROM PE_Process_Roles AS P WHERE P.FlowId = @FlowId AND P.ProcessId<>@ProcessId AND U.RoleId = P.RoleId))";
            IList<RoleInfo> list = new List<RoleInfo>();
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@FlowId", DbType.Int32, flowId);
            cmdParams.AddInParameter("@ProcessId", DbType.Int32, processId);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, cmdParams))
            {
                while (reader.Read())
                {
                    list.Add(GetRoleInfoFromrdr(reader));
                }
            }
            return list;
        }

        public IList<string> GetRoleListByOperateCode(OperateCode operateCode)
        {
            Parameters parms = new Parameters();
            parms.AddInParameter("@OperateCode", DbType.Int32, operateCode);
            string strSqlText = "SELECT RoleId FROM PE_Roles_Permissions WHERE OperateCode = @OperateCode";
            return CreateRoleList(parms, strSqlText);
        }
        /// <summary>
        /// 根据页面操作码获取权限列表
        /// </summary>
        /// <param name="operateCode"></param>
        /// <returns></returns>
        public IList<string> GetRoleListByOperateCode(int operateCode)
        {
            Parameters parms = new Parameters();
            parms.AddInParameter("@OperateCode", DbType.Int32, operateCode);
            string strSqlText = "SELECT RoleId FROM PE_Roles_Permissions WHERE OperateCode = @OperateCode";
            return CreateRoleList(parms, strSqlText);
        }

        public IList<string> GetRoleListByOperateCodeFieldName(OperateCode operateCode, int modelId, string fieldName)
        {
            Parameters parms = new Parameters();
            parms.AddInParameter("@OperateCode", DbType.Int32, operateCode);
            parms.AddInParameter("@ModelID", DbType.Int32, modelId);
            parms.AddInParameter("@FieldName", DbType.String, fieldName);
            string strSqlText = "SELECT RoleId FROM PE_Role_Field_Permissions WHERE OperateCode = @OperateCode AND ModelID = @ModelID AND FieldName = @FieldName";
            return CreateRoleList(parms, strSqlText);
        }

        public IList<string> GetRoleListByOperateCodeNode(OperateCode operateCode, int nodeId)
        {
            Parameters parms = new Parameters();
            parms.AddInParameter("@OperateCode", DbType.Int32, operateCode);
            parms.AddInParameter("@NodeID", DbType.Int32, nodeId);
            string strSqlText = "SELECT RoleId FROM PE_Role_Node_Permissions WHERE OperateCode = @OperateCode AND NodeID = @NodeID";
            return CreateRoleList(parms, strSqlText);
        }

        public IList<string> GetRoleListByOperateCodeSpecialId(OperateCode operateCode, int specialId)
        {
            Parameters parms = new Parameters();
            parms.AddInParameter("@OperateCode", DbType.Int32, operateCode);
            parms.AddInParameter("@SpecialId", DbType.Int32, specialId);
            string strSqlText = "SELECT RoleId FROM PE_Role_Special_Permissions WHERE OperateCode = @OperateCode AND SpecialId = @SpecialId";
            return CreateRoleList(parms, strSqlText);
        }

        public IList<RoleInfo> GetRoleListByRoleId(int adminId)
        {
            IList<RoleInfo> list = new List<RoleInfo>();
            string strSql = "SELECT * FROM PE_Roles WHERE (RoleID IN (SELECT RoleID FROM PE_Admin_Roles WHERE (AdminID = @AdminID)))";
            Parameters cmdParams = new Parameters("@AdminID", DbType.Int32, adminId);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, cmdParams))
            {
                while (reader.Read())
                {
                    list.Add(GetRoleInfoFromrdr(reader));
                }
            }
            return list;
        }

        public IList<RoleInfo> GetRoleListNotInRole(int adminId)
        {
            IList<RoleInfo> list = new List<RoleInfo>();
            string strSql = "SELECT * FROM PE_Roles WHERE (RoleID NOT IN (SELECT RoleID FROM PE_Admin_Roles WHERE (AdminID = @AdminID)))";
            Parameters cmdParams = new Parameters("@AdminID", DbType.Int32, adminId);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, cmdParams))
            {
                while (reader.Read())
                {
                    list.Add(GetRoleInfoFromrdr(reader));
                }
            }
            return list;
        }

        public string GetRoleNodeId(string roleId, OperateCode[] arrOperateCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT DISTINCT NodeID FROM PE_Role_Node_Permissions WHERE RoleID IN (" + DBHelper.ToValidId(roleId) + ") ");
            foreach (OperateCode code in arrOperateCode)
            {
                builder.Append(" OR OperateCode = " + ((int) code));
            }
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@RoleId", DbType.String, roleId);
            StringBuilder builder2 = new StringBuilder();
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, builder.ToString(), cmdParams))
            {
                while (reader.Read())
                {
                    if (builder2.Length > 0)
                    {
                        builder2.Append("," + reader.GetInt32("NodeID").ToString());
                    }
                    else
                    {
                        builder2.Append(reader.GetInt32("NodeID").ToString());
                    }
                }
            }
            return builder2.ToString();
        }

        public string GetRoleNodeId(string roleId, OperateCode operateCode)
        {
            string strCommand = "SELECT DISTINCT NodeID FROM PE_Role_Node_Permissions WHERE RoleID IN (" + DBHelper.ToValidId(roleId) + ")  AND OperateCode = @OperateCode";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@RoleId", DbType.String, roleId);
            cmdParams.AddInParameter("@OperateCode", DbType.Int32, operateCode);
            StringBuilder builder = new StringBuilder();
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, cmdParams))
            {
                while (reader.Read())
                {
                    if (builder.Length > 0)
                    {
                        builder.Append("," + reader.GetInt32("NodeID").ToString());
                    }
                    else
                    {
                        builder.Append(reader.GetInt32("NodeID").ToString());
                    }
                }
            }
            return builder.ToString();
        }

        public IList<RoleSpecialPermissionsInfo> GetSpecialPermissionsByRoleId(int roleId, OperateCode operateCode)
        {
            IList<RoleSpecialPermissionsInfo> list = new List<RoleSpecialPermissionsInfo>();
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@RoleID", DbType.Int32, roleId);
            cmdParams.AddInParameter("@OperateCode", DbType.Int32, operateCode);
            string strCommand = "SELECT * FROM PE_Role_Special_Permissions WHERE RoleID = @RoleID AND OperateCode = @OperateCode";
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, cmdParams))
            {
                while (reader.Read())
                {
                    RoleSpecialPermissionsInfo item = new RoleSpecialPermissionsInfo();
                    item.GroupId = reader.GetInt32("RoleID");
                    item.OperateCode = (OperateCode) reader.GetInt32("OperateCode");
                    item.SpecialId = reader.GetInt32("SpecialID");
                    list.Add(item);
                }
            }
            return list;
        }

        public IList<RoleSpecialPermissionsInfo> GetSpecialPermissionsBySpecialId(int specialId)
        {
            IList<RoleSpecialPermissionsInfo> list = new List<RoleSpecialPermissionsInfo>();
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@SpecialID", DbType.Int32, specialId);
            string strCommand = "SELECT * FROM PE_Role_Special_Permissions WHERE SpecialID = @SpecialID";
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, cmdParams))
            {
                while (reader.Read())
                {
                    RoleSpecialPermissionsInfo item = new RoleSpecialPermissionsInfo();
                    item.GroupId = reader.GetInt32("RoleID");
                    item.OperateCode = (OperateCode) reader.GetInt32("OperateCode");
                    item.SpecialId = reader.GetInt32("SpecialID");
                    list.Add(item);
                }
            }
            return list;
        }

        public IList<RoleSpecialPermissionsInfo> GetSpecialPermssionList(int roleId, int specialId)
        {
            IList<RoleSpecialPermissionsInfo> list = new List<RoleSpecialPermissionsInfo>();
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@RoleID", DbType.Int32, roleId);
            cmdParams.AddInParameter("@SpecialId", DbType.Int32, specialId);
            string strCommand = "SELECT * FROM PE_Role_Special_Permissions WHERE RoleID = @RoleID AND SpecialId = @SpecialId";
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, cmdParams))
            {
                while (reader.Read())
                {
                    RoleSpecialPermissionsInfo item = new RoleSpecialPermissionsInfo();
                    item.GroupId = reader.GetInt32("RoleID");
                    item.OperateCode = (OperateCode) reader.GetInt32("OperateCode");
                    item.SpecialId = reader.GetInt32("SpecialID");
                    list.Add(item);
                }
            }
            return list;
        }

        public int GetTotalOfRoles()
        {
            return this.m_TotalOfRoles;
        }

        public bool IsExist(string roleName)
        {
            string strSql = "SELECT COUNT(*) FROM PE_Roles WHERE RoleName = @RoleName";
            Parameters cmdParams = new Parameters("@RoleName", DbType.String, roleName);
            return DBHelper.ExistsSql(strSql, cmdParams);
        }

        public bool Update(RoleInfo roleInfo)
        {
            string strSql = "UPDATE PE_Roles SET RoleName = @RoleName, Description = @Description WHERE RoleId = @RoleId";
            Parameters cmdParams = GetParameters(roleInfo);
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }
    }
}

