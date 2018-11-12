namespace EasyOne.SqlServerDal.AccessManage
{
    using EasyOne.Enumerations;
    using EasyOne.IDal.AccessManage;
    using EasyOne.Model.AccessManage;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;

    public class UserPermissions : IUserPermissions
    {
        public bool AddFieldPermissions(int id, int modelId, string fieldName, OperateCode operateCode, int idType)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@GroupID", DbType.Int32, id);
            cmdParams.AddInParameter("@OperateCode", DbType.Int32, operateCode);
            cmdParams.AddInParameter("@ModelID", DbType.Int32, modelId);
            cmdParams.AddInParameter("@FieldName", DbType.String, fieldName);
            cmdParams.AddInParameter("@IdType", DbType.Int32, idType);
            string strSql = "INSERT INTO PE_GroupFieldPermissions(GroupID, OperateCode, ModelID, FieldName, IdType) VALUES (@GroupID, @OperateCode, @ModelID, @FieldName, @IdType)";
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool AddNodePermissions(int id, OperateCode operateCode, int nodeId, int idType)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@GroupId", DbType.Int32, id);
            cmdParams.AddInParameter("@OperateCode", DbType.Int32, operateCode);
            cmdParams.AddInParameter("@NodeID", DbType.Int32, nodeId);
            cmdParams.AddInParameter("@IdType", DbType.Int32, idType);
            string strSql = "INSERT INTO PE_GroupNodePermissions(GroupID, OperateCode, NodeID, IdType) VALUES (@GroupId, @OperateCode, @NodeID, @IdType)";
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool AddSpecialPermissions(int id, OperateCode operateCode, int specialId, int idType)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@GroupID", DbType.Int32, id);
            cmdParams.AddInParameter("@OperateCode", DbType.Int32, operateCode);
            cmdParams.AddInParameter("@SpecialID", DbType.Int32, specialId);
            cmdParams.AddInParameter("@IdType", DbType.Int32, idType);
            string strSql = "INSERT INTO PE_GroupSpecialPermissions(GroupID, OperateCode, SpecialID, IdType) VALUES (@GroupID, @OperateCode, @SpecialID, @IdType)";
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        private static IList<int> CreateRoleList(Parameters parms, string strSqlText)
        {
            IList<int> list = new List<int>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSqlText, parms))
            {
                while (reader.Read())
                {
                    list.Add(reader.GetInt32("GroupId"));
                }
            }
            return list;
        }

        public bool DeleteFieldPermissions(int id, int modelId, string fieldName, int idType)
        {
            Parameters cmdParams = new Parameters();
            string strSql = "DELETE FROM PE_GroupFieldPermissions WHERE 1 = 1";
            if (modelId > 0)
            {
                cmdParams.AddInParameter("@ModelId", DbType.Int32, modelId);
                strSql = strSql + " AND ModelID = @ModelId ";
            }
            if (!string.IsNullOrEmpty(fieldName))
            {
                strSql = strSql + " AND FieldName = @FieldName ";
                cmdParams.AddInParameter("@FieldName", DbType.String, fieldName);
            }
            if ((id > 0) || (id == -2))
            {
                strSql = strSql + " AND GroupID = @GroupID ";
                cmdParams.AddInParameter("@GroupID", DbType.Int32, id);
            }
            if (idType >= 0)
            {
                strSql = strSql + " AND IdType = @IdType ";
                cmdParams.AddInParameter("@IdType", DbType.Int32, idType);
            }
            try
            {
                DBHelper.ExecuteNonQuerySql(strSql, cmdParams);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteNodePermissions(int id, int nodeId, OperateCode operateCode, int idType)
        {
            Parameters cmdParams = new Parameters();
            string strSql = "DELETE FROM PE_GroupNodePermissions WHERE 1 = 1";
            if ((id > 0) || (id == -2))
            {
                strSql = strSql + " AND GroupID = @GroupID ";
                cmdParams.AddInParameter("@GroupID", DbType.Int32, id);
            }
            if (nodeId >= -2)
            {
                strSql = strSql + " AND NodeId = @NodeId ";
                cmdParams.AddInParameter("@NodeId", DbType.Int32, nodeId);
            }
            if (operateCode != OperateCode.None)
            {
                strSql = strSql + " AND OperateCode = @OperateCode ";
                cmdParams.AddInParameter("@OperateCode", DbType.Int32, operateCode);
            }
            if (idType >= 0)
            {
                strSql = strSql + " AND IdType = @IdType ";
                cmdParams.AddInParameter("@IdType", DbType.Int32, idType);
            }
            try
            {
                DBHelper.ExecuteNonQuerySql(strSql, cmdParams);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteNodePermissionsByNodeId(int nodeId, OperateCode operateCode)
        {
            return this.DeleteNodePermissions(0, nodeId, operateCode, -1);
        }

        public bool DeleteSpecialPermissions(int id, int specialId, OperateCode operateCode, int idType)
        {
            Parameters cmdParams = new Parameters();
            string strSql = "DELETE FROM PE_GroupSpecialPermissions WHERE 1 = 1";
            if ((id > 0) || (id == -2))
            {
                strSql = strSql + " AND GroupID = @GroupID ";
                cmdParams.AddInParameter("@GroupID", DbType.Int32, id);
            }
            if (specialId > 0)
            {
                strSql = strSql + " AND SpecialID = @SpecialID ";
                cmdParams.AddInParameter("@SpecialID", DbType.Int32, specialId);
            }
            if (operateCode != OperateCode.None)
            {
                strSql = strSql + " AND OperateCode = @OperateCode ";
                cmdParams.AddInParameter("@OperateCode", DbType.Int32, operateCode);
            }
            if (idType >= 0)
            {
                strSql = strSql + " AND IdType = @IdType ";
                cmdParams.AddInParameter("@IdType", DbType.Int32, idType);
            }
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool DeleteSpecialPermissionsBySpecialId(int specialId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@SpecialID", DbType.Int32, specialId);
            string strSql = "DELETE FROM PE_GroupSpecialPermissions WHERE SpecialID = @SpecialID";
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool DeleteSpecialPermissionsBySpecialId(int specialId, OperateCode operateCode)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@SpecialID", DbType.Int32, specialId);
            cmdParams.AddInParameter("@OperateCode", DbType.Int32, operateCode);
            string strSql = "DELETE FROM PE_GroupSpecialPermissions WHERE SpecialID = @SpecialID AND OperateCode = @OperateCode";
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public IList<RoleNodePermissionsInfo> GetAllNodePermissionsById(int id, int nodeId, int idType)
        {
            IList<RoleNodePermissionsInfo> list = new List<RoleNodePermissionsInfo>();
            Parameters cmdParams = new Parameters();
            string strCommand = "SELECT * FROM PE_GroupNodePermissions WHERE 1 = 1 ";
            if ((id > 0) || (id == -2))
            {
                strCommand = strCommand + " AND GroupID = @GroupID ";
                cmdParams.AddInParameter("@GroupID", DbType.Int32, id);
            }
            if (nodeId >= -1)
            {
                strCommand = strCommand + " AND (NodeID = @NodeID OR NodeID = -1) ";
                cmdParams.AddInParameter("@NodeID", DbType.Int32, nodeId);
            }
            if (idType >= 0)
            {
                strCommand = strCommand + " AND IdType = @IdType ";
                cmdParams.AddInParameter("@IdType", DbType.Int32, idType);
            }
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, cmdParams))
            {
                while (reader.Read())
                {
                    RoleNodePermissionsInfo item = new RoleNodePermissionsInfo();
                    item.GroupId = reader.GetInt32("GroupID");
                    item.OperateCode = (OperateCode) reader.GetInt32("OperateCode");
                    item.NodeId = reader.GetInt32("NodeID");
                    list.Add(item);
                }
            }
            return list;
        }

        public IList<RoleFieldPermissionsInfo> GetFieldPermissionsById(int id, int modelId, string fieldName, int idType)
        {
            IList<RoleFieldPermissionsInfo> list = new List<RoleFieldPermissionsInfo>();
            Parameters cmdParams = new Parameters();
            string strCommand = "SELECT * FROM PE_GroupFieldPermissions WHERE 1 = 1 ";
            if ((id > 0) || (id == -2))
            {
                strCommand = strCommand + " AND GroupID = @GroupID ";
                cmdParams.AddInParameter("@GroupID", DbType.Int32, id);
            }
            if (modelId > 0)
            {
                strCommand = strCommand + " AND ModelId = @ModelId";
                cmdParams.AddInParameter("@ModelId", DbType.Int32, modelId);
            }
            if (!string.IsNullOrEmpty(fieldName))
            {
                strCommand = strCommand + " AND FieldName = @FieldName";
                cmdParams.AddInParameter("@FieldName", DbType.String, fieldName);
            }
            if (idType >= 0)
            {
                strCommand = strCommand + " AND IdType = @IdType ";
                cmdParams.AddInParameter("@IdType", DbType.Int32, idType);
            }
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, cmdParams))
            {
                while (reader.Read())
                {
                    RoleFieldPermissionsInfo item = new RoleFieldPermissionsInfo();
                    item.RoleId = reader.GetInt32("GroupID");
                    item.OperateCode = (OperateCode) reader.GetInt32("OperateCode");
                    item.ModelId = reader.GetInt32("ModelID");
                    item.FieldName = reader.GetString("FieldName");
                    list.Add(item);
                }
            }
            return list;
        }

        public IList<int> GetGroupListByOperateCodeSpecialId(OperateCode operateCode, int specialId, int idType)
        {
            Parameters parms = new Parameters();
            parms.AddInParameter("@OperateCode", DbType.Int32, operateCode);
            parms.AddInParameter("@SpecialId", DbType.Int32, specialId);
            parms.AddInParameter("@IdType", DbType.Int32, idType);
            string strSqlText = "SELECT GroupID FROM PE_GroupSpecialPermissions WHERE OperateCode = @OperateCode AND IdType=@IdType AND SpecialId = @SpecialId";
            return CreateRoleList(parms, strSqlText);
        }

        public IList<RoleNodePermissionsInfo> GetNodePermissionsList(int id, int nodeId, OperateCode operateCode, int idType)
        {
            IList<RoleNodePermissionsInfo> list = new List<RoleNodePermissionsInfo>();
            Parameters cmdParams = new Parameters();
            string strCommand = "SELECT * FROM PE_GroupNodePermissions WHERE 1 = 1 ";
            if ((id > 0) || (id == -2))
            {
                strCommand = strCommand + " AND GroupID = @GroupID ";
                cmdParams.AddInParameter("@GroupID", DbType.Int32, id);
            }
            if (nodeId >= -1)
            {
                strCommand = strCommand + " AND NodeID = @NodeID ";
                cmdParams.AddInParameter("@NodeID", DbType.Int32, nodeId);
            }
            if (operateCode != OperateCode.None)
            {
                strCommand = strCommand + " AND OperateCode = @OperateCode ";
                cmdParams.AddInParameter("@OperateCode", DbType.Int32, operateCode);
            }
            if (idType >= 0)
            {
                strCommand = strCommand + " AND IdType = @IdType ";
                cmdParams.AddInParameter("@IdType", DbType.Int32, idType);
            }
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, cmdParams))
            {
                while (reader.Read())
                {
                    RoleNodePermissionsInfo item = new RoleNodePermissionsInfo();
                    item.GroupId = reader.GetInt32("GroupID");
                    item.OperateCode = (OperateCode) reader.GetInt32("OperateCode");
                    item.NodeId = reader.GetInt32("NodeID");
                    list.Add(item);
                }
            }
            return list;
        }

        public IList<int> GetRoleListByOperateCodeFieldName(OperateCode operateCode, int modelId, string fieldName, int idType)
        {
            Parameters parms = new Parameters();
            parms.AddInParameter("@OperateCode", DbType.Int32, operateCode);
            parms.AddInParameter("@ModelID", DbType.Int32, modelId);
            parms.AddInParameter("@FieldName", DbType.String, fieldName);
            parms.AddInParameter("@IdType", DbType.Int32, idType);
            string strSqlText = "SELECT GroupId FROM PE_GroupFieldPermissions WHERE OperateCode = @OperateCode AND ModelID = @ModelID AND IdType=@IdType AND FieldName = @FieldName";
            return CreateRoleList(parms, strSqlText);
        }

        public IList<int> GetRoleListByOperateCodeNode(OperateCode operateCode, int nodeId, int idType)
        {
            Parameters parms = new Parameters();
            parms.AddInParameter("@OperateCode", DbType.Int32, operateCode);
            parms.AddInParameter("@NodeID", DbType.Int32, nodeId);
            parms.AddInParameter("@IdType", DbType.Int32, idType);
            string strSqlText = "SELECT GroupID FROM PE_GroupNodePermissions WHERE OperateCode = @OperateCode AND IdType=@IdType AND NodeID = @NodeID";
            return CreateRoleList(parms, strSqlText);
        }

        public IList<int> GetRoleListByOperateCodeNode(OperateCode operateCode, string nodeId, int idType)
        {
            Parameters parms = new Parameters();
            parms.AddInParameter("@OperateCode", DbType.Int32, operateCode);
            parms.AddInParameter("@IdType", DbType.Int32, idType);
            string strSqlText = "SELECT GroupID FROM PE_GroupNodePermissions WHERE OperateCode = @OperateCode AND IdType=@IdType AND NodeID IN (" + DBHelper.ToValidId(nodeId) + ")";
            return CreateRoleList(parms, strSqlText);
        }

        public string GetRoleNodeId(int roleId, OperateCode operateCode, int idType)
        {
            string strCommand = "SELECT DISTINCT NodeID FROM PE_GroupNodePermissions WHERE GroupID = @GroupID AND OperateCode = @OperateCode AND IdType = @idType";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@GroupID", DbType.Int32, roleId);
            cmdParams.AddInParameter("@OperateCode", DbType.Int32, operateCode);
            cmdParams.AddInParameter("idType", DbType.Int32, idType);
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

        public IList<RoleSpecialPermissionsInfo> GetSpecialPermissionsList(int id, int specialId, OperateCode operateCode, int idType)
        {
            IList<RoleSpecialPermissionsInfo> list = new List<RoleSpecialPermissionsInfo>();
            Parameters cmdParams = new Parameters();
            string strCommand = "SELECT * FROM PE_GroupSpecialPermissions WHERE 1 = 1";
            if ((id > 0) || (id == -2))
            {
                strCommand = strCommand + " AND GroupID = @GroupID ";
                cmdParams.AddInParameter("@GroupID", DbType.Int32, id);
            }
            if ((specialId > 0) || (specialId == -1))
            {
                strCommand = strCommand + " AND SpecialID = @SpecialID ";
                cmdParams.AddInParameter("@SpecialID", DbType.Int32, specialId);
            }
            if (operateCode != OperateCode.None)
            {
                strCommand = strCommand + " AND OperateCode = @OperateCode ";
                cmdParams.AddInParameter("@OperateCode", DbType.Int32, operateCode);
            }
            if (idType >= 0)
            {
                strCommand = strCommand + " AND IdType = @IdType ";
                cmdParams.AddInParameter("@IdType", DbType.Int32, idType);
            }
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, cmdParams))
            {
                while (reader.Read())
                {
                    RoleSpecialPermissionsInfo item = new RoleSpecialPermissionsInfo();
                    item.GroupId = reader.GetInt32("GroupID");
                    item.OperateCode = (OperateCode) reader.GetInt32("OperateCode");
                    item.SpecialId = reader.GetInt32("SpecialID");
                    list.Add(item);
                }
            }
            return list;
        }
    }
}

