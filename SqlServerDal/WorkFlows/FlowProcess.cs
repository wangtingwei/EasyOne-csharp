namespace EasyOne.SqlServerDal.WorkFlows
{
    using EasyOne.Common;
    using EasyOne.IDal.WorkFlow;
    using EasyOne.Model.UserManage;
    using EasyOne.Model.WorkFlow;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;

    public sealed class FlowProcess : IFlowProcess
    {
        public bool Add(FlowProcessInfo flowProcessInfo)
        {
            Parameters parms = new Parameters();
            flowProcessInfo.ProcessId = GetMaxProcessId() + 1;
            GetParameters(flowProcessInfo, parms);
            string strSql = "INSERT INTO PE_FlowProcess(ProcessID, FlowID, ProcessName, Description, PassActionName, PassActionStatus, RejectActionName, RejectActionStatus) VALUES (@ProcessId, @FlowId, @ProcessName, @Description, @PassActionName, @PassActionStatus, @RejectActionName, @RejectActionStatus)";
            return DBHelper.ExecuteSql(strSql, parms);
        }

        public bool AddRoleToProcessRoles(int flowId, int processId, int roleId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@FlowID", DbType.Int32, flowId);
            cmdParams.AddInParameter("@ProcessID", DbType.Int32, processId);
            cmdParams.AddInParameter("@RoleId", DbType.Int32, roleId);
            string strSql = "INSERT INTO PE_Process_Roles(FlowID, ProcessID, RoleId) VALUES (@FlowID, @ProcessID, @RoleId)";
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool AddStatusCodeToProcessStatusCode(int flowId, int processId, int statusCode)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@FlowID", DbType.Int32, flowId);
            cmdParams.AddInParameter("@ProcessID", DbType.Int32, processId);
            cmdParams.AddInParameter("@StatusCode", DbType.Int32, statusCode);
            string strSql = "INSERT INTO PE_ProcessStatusCode(FlowID, ProcessID, StatusCode) VALUES (@FlowID, @ProcessID, @StatusCode)";
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool Delete(int flowId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@FlowID", DbType.Int32, flowId);
            string strSql = "DELETE FROM PE_FlowProcess WHERE FlowID = @FlowID";
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool Delete(int flowId, int processId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@FlowID", DbType.Int32, flowId);
            cmdParams.AddInParameter("@ProcessID", DbType.Int32, processId);
            string strSql = "DELETE FROM PE_FlowProcess WHERE FlowID = @FlowID AND ProcessID = @ProcessID";
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool DeleteRoleInProcessRole(int groupId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@RoleId", DbType.Int32, groupId);
            string strSql = "DELETE FROM PE_Process_Roles WHERE RoleId = @RoleId";
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool DeleteRoleInProcessRole(int flowId, int processId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@FlowID", DbType.Int32, flowId);
            cmdParams.AddInParameter("@ProcessID", DbType.Int32, processId);
            string strSql = "DELETE FROM PE_Process_Roles WHERE FlowID = @FlowID AND ProcessID = @ProcessID";
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool DeleteStatusCodeInProcessStatusCode(int statusCode)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@StatusCode", DbType.Int32, statusCode);
            string strSql = "DELETE FROM PE_ProcessStatusCode WHERE StatusCode = @StatusCode";
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool DeleteStatusCodeInProcessStatusCode(int flowId, int processId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@FlowID", DbType.Int32, flowId);
            cmdParams.AddInParameter("@ProcessID", DbType.Int32, processId);
            string strSql = "DELETE FROM PE_ProcessStatusCode WHERE FlowID = @FlowID AND ProcessID = @ProcessID";
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool DeleteWorkFlowInProcessRole(int flowId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@FlowID", DbType.Int32, flowId);
            string strSql = "DELETE FROM PE_Process_Roles WHERE FlowID = @FlowID";
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool DeleteWorkFlowInProcessStatusCode(int flowId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@FlowID", DbType.Int32, flowId);
            string strSql = "DELETE FROM PE_ProcessStatusCode WHERE FlowID = @FlowID";
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool ExistFlowProcess(int flowId, string processName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@FlowId", DbType.Int32, flowId);
            cmdParams.AddInParameter("@ProcessName", DbType.String, processName);
            string strSql = "SELECT COUNT(*) FROM PE_FlowProcess WHERE FlowID = @FlowID AND ProcessName = @ProcessName";
            return DBHelper.ExistsSql(strSql, cmdParams);
        }

        public bool ExistRoleInProcessRole(int groupId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@RoleId", DbType.Int32, groupId);
            string strSql = "SELECT COUNT(*) FROM PE_Process_Roles WHERE RoleId = @RoleId";
            return DBHelper.ExistsSql(strSql, cmdParams);
        }

        public bool ExistRoleInProcessRole(int flowId, int processId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@FlowID", DbType.Int32, flowId);
            cmdParams.AddInParameter("@ProcessID", DbType.Int32, processId);
            string strSql = "SELECT COUNT(*) FROM PE_Process_Roles WHERE FlowID = @FlowID AND ProcessID = @ProcessID";
            return DBHelper.ExistsSql(strSql, cmdParams);
        }

        public bool ExistStatusCodeInProcessStatusCode(int statusCode)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@StatusCode", DbType.Int32, statusCode);
            string strSql = "SELECT COUNT(*) FROM PE_ProcessStatusCode WHERE StatusCode = @StatusCode";
            return DBHelper.ExistsSql(strSql, cmdParams);
        }

        public bool ExistStatusCodeInProcessStatusCode(int flowId, int processId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@FlowID", DbType.Int32, flowId);
            cmdParams.AddInParameter("@ProcessID", DbType.Int32, processId);
            string strSql = "SELECT COUNT(*) FROM PE_ProcessStatusCode WHERE FlowID = @FlowID AND ProcessID = @ProcessID";
            return DBHelper.ExistsSql(strSql, cmdParams);
        }

        public bool ExistWorkFlowInFlowProcess(int flowId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@FlowID", DbType.Int32, flowId);
            string strSql = "SELECT COUNT(*) FROM PE_FlowProcess WHERE FlowID = @FlowID";
            return DBHelper.ExistsSql(strSql, cmdParams);
        }

        public bool ExistWorkFlowInProcessRole(int flowId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@FlowID", DbType.Int32, flowId);
            string strSql = "SELECT COUNT(*) FROM PE_Process_Roles WHERE FlowID = @FlowID";
            return DBHelper.ExistsSql(strSql, cmdParams);
        }

        public bool ExistWorkFlowInProcessStatusCode(int flowId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@FlowID", DbType.Int32, flowId);
            string strSql = "SELECT COUNT(*) FROM PE_ProcessStatusCode WHERE FlowID = @FlowID";
            return DBHelper.ExistsSql(strSql, cmdParams);
        }

        private static void FlowProcessFromrdr(FlowProcessInfo flowProcessInfo, NullableDataReader rdr)
        {
            flowProcessInfo.ProcessId = rdr.GetInt32("ProcessID");
            flowProcessInfo.FlowId = rdr.GetInt32("FlowID");
            flowProcessInfo.ProcessName = rdr.GetString("ProcessName");
            flowProcessInfo.Description = rdr.GetString("Description");
            flowProcessInfo.PassActionName = rdr.GetString("PassActionName");
            flowProcessInfo.PassActionStatus = rdr.GetInt32("PassActionStatus");
            flowProcessInfo.RejectActionName = rdr.GetString("RejectActionName");
            flowProcessInfo.RejectActionStatus = rdr.GetInt32("RejectActionStatus");
        }

        private static FlowProcessInfo FlowProcessInfoFromrdataReader(NullableDataReader rdr)
        {
            FlowProcessInfo info = new FlowProcessInfo();
            info.ProcessId = rdr.GetInt32("ProcessId");
            info.FlowId = rdr.GetInt32("FlowId");
            info.ProcessName = rdr.GetString("ProcessName");
            info.Description = rdr.GetString("Description");
            info.PassActionName = rdr.GetString("PassActionName");
            info.PassActionStatus = rdr.GetInt32("PassActionStatus");
            info.RejectActionName = rdr.GetString("RejectActionName");
            info.RejectActionStatus = rdr.GetInt32("RejectActionStatus");
            return info;
        }

        public FlowProcessInfo GetFlowProcessById(int flowId, int processId)
        {
            FlowProcessInfo flowProcessInfo = new FlowProcessInfo();
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@FlowId", DbType.Int32, flowId);
            cmdParams.AddInParameter("@ProcessId", DbType.Int32, processId);
            string strCommand = "SELECT * FROM PE_FlowProcess WHERE FlowID = @FlowId AND ProcessID = @ProcessId";
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, cmdParams))
            {
                if (reader.Read())
                {
                    FlowProcessFromrdr(flowProcessInfo, reader);
                    return flowProcessInfo;
                }
                return new FlowProcessInfo(true);
            }
        }

        public FlowProcessInfo GetFlowProcessByRoles(int flowId, int roleId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@FlowId", DbType.Int32, flowId);
            cmdParams.AddInParameter("@RoleId", DbType.Int32, roleId);
            string strCommand = "SELECT * FROM PE_FlowProcess WHERE FlowID = @FlowId AND ProcessID = (SELECT ProcessID FROM PE_Process_Roles WHERE FlowID = @FlowID AND RoleId = @RoleId)";
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, cmdParams))
            {
                if (reader.Read())
                {
                    FlowProcessInfo flowProcessInfo = new FlowProcessInfo();
                    FlowProcessFromrdr(flowProcessInfo, reader);
                    return flowProcessInfo;
                }
                return new FlowProcessInfo(true);
            }
        }

        public FlowProcessInfo GetFlowProcessByRoles(int flowId, string roleIdArr)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@FlowId", DbType.Int32, flowId);
            string strCommand = "SELECT TOP 1 * FROM PE_FlowProcess WHERE FlowID = @FlowId AND ProcessID IN (SELECT ProcessID FROM PE_Process_Roles WHERE FlowID = @FlowID AND RoleId IN (" + roleIdArr + ")) ORDER BY PassActionStatus DESC";
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, cmdParams))
            {
                if (reader.Read())
                {
                    FlowProcessInfo flowProcessInfo = new FlowProcessInfo();
                    FlowProcessFromrdr(flowProcessInfo, reader);
                    return flowProcessInfo;
                }
                return new FlowProcessInfo(true);
            }
        }

        public IList<FlowProcessInfo> GetFlowProcessList(int flowId)
        {
            IList<FlowProcessInfo> list = new List<FlowProcessInfo>();
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@FlowID", DbType.Int32, flowId);
            string strCommand = "SELECT ProcessId, FlowId, ProcessName, Description, PassActionName, PassActionStatus, RejectActionName, RejectActionStatus FROM PE_FlowProcess";
            if (flowId > 0)
            {
                strCommand = strCommand + " WHERE FlowID = @FlowID";
            }
            strCommand = strCommand + " ORDER BY ProcessID DESC";
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, cmdParams))
            {
                while (reader.Read())
                {
                    list.Add(FlowProcessInfoFromrdataReader(reader));
                }
            }
            return list;
        }

        public string GetGroupIdByProcessIdAndFlowId(int flowId, int processId)
        {
            string strSql = "SELECT RoleId FROM PE_Process_Roles WHERE FlowId = @FlowId AND ProcessId = @ProcessId";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@FlowId", DbType.Int32, flowId);
            cmdParams.AddInParameter("@ProcessId", DbType.Int32, processId);
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

        private static int GetMaxProcessId()
        {
            string strSql = "SELECT MAX(ProcessID) FROM PE_FlowProcess";
            return DataConverter.CLng(DBHelper.ExecuteScalarSql(strSql));
        }

        private static void GetParameters(FlowProcessInfo flowProcessInfo, Parameters parms)
        {
            parms.AddInParameter("@ProcessId", DbType.Int32, flowProcessInfo.ProcessId);
            parms.AddInParameter("@FlowId", DbType.Int32, flowProcessInfo.FlowId);
            parms.AddInParameter("@ProcessName", DbType.String, flowProcessInfo.ProcessName);
            parms.AddInParameter("@Description", DbType.String, flowProcessInfo.Description);
            parms.AddInParameter("@PassActionName", DbType.String, flowProcessInfo.PassActionName);
            parms.AddInParameter("@PassActionStatus", DbType.Int32, flowProcessInfo.PassActionStatus);
            parms.AddInParameter("@RejectActionName", DbType.String, flowProcessInfo.RejectActionName);
            parms.AddInParameter("@RejectActionStatus", DbType.Int32, flowProcessInfo.RejectActionStatus);
        }

        public IList<RoleInfo> GetProcessRoleList(int flowId, int processId)
        {
            IList<RoleInfo> list = new List<RoleInfo>();
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@FlowID", DbType.Int32, flowId);
            cmdParams.AddInParameter("@ProcessID", DbType.Int32, processId);
            string strCommand = "SELECT FlowID, ProcessID, RoleId FROM PE_Process_Roles";
            if (processId > 0)
            {
                strCommand = strCommand + " WHERE ProcessID = @ProcessID AND FlowID = @FlowID";
            }
            strCommand = strCommand + " ORDER BY ProcessID DESC";
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, cmdParams))
            {
                while (reader.Read())
                {
                    RoleInfo item = new RoleInfo();
                    item.RoleId = reader.GetInt32("RoleId");
                    list.Add(item);
                }
            }
            return list;
        }

        public IList<StatusInfo> GetProcessStatusCodeList(int flowId, int processId)
        {
            IList<StatusInfo> list = new List<StatusInfo>();
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@FlowID", DbType.Int32, flowId);
            cmdParams.AddInParameter("@ProcessID", DbType.Int32, processId);
            string strCommand = "SELECT FlowID, ProcessID, StatusCode FROM PE_ProcessStatusCode";
            if (processId > 0)
            {
                strCommand = strCommand + " WHERE ProcessID = @ProcessID AND FlowID = @FlowID";
            }
            strCommand = strCommand + " ORDER BY ProcessID DESC";
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, cmdParams))
            {
                while (reader.Read())
                {
                    StatusInfo item = new StatusInfo();
                    item.StatusCode = reader.GetInt32("StatusCode");
                    list.Add(item);
                }
            }
            return list;
        }

        public string GetStatusCodeToProcessStatusCode(int flowId, string rolesId)
        {
            StringBuilder sb = new StringBuilder();
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@FlowID", DbType.Int32, flowId);
            string strCommand = "SELECT StatusCode FROM PE_ProcessStatusCode WHERE FlowID = @FlowID AND ProcessID IN (SELECT ProcessID FROM PE_Process_Roles WHERE FlowID = @FlowID AND RoleID IN(" + rolesId + "))";
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, cmdParams))
            {
                while (reader.Read())
                {
                    StringHelper.AppendString(sb, reader.GetInt32("StatusCode").ToString());
                }
            }
            return sb.ToString();
        }

        public bool Update(FlowProcessInfo flowProcessInfo)
        {
            Parameters parms = new Parameters();
            GetParameters(flowProcessInfo, parms);
            string strSql = "UPDATE PE_FlowProcess SET ProcessID = @ProcessId, FlowID = @FlowId, ProcessName = @ProcessName, Description = @Description, PassActionName = @PassActionName, PassActionStatus = @PassActionStatus, RejectActionName = @RejectActionName, RejectActionStatus = @RejectActionStatus WHERE ProcessID = @ProcessId AND FlowID = @FlowID";
            return DBHelper.ExecuteSql(strSql, parms);
        }
    }
}

