namespace EasyOne.IDal.WorkFlow
{
    using EasyOne.Model.WorkFlow;
    using System;
    using System.Collections.Generic;
    using EasyOne.Model.UserManage;

    public interface IFlowProcess
    {
        bool Add(FlowProcessInfo flowProcessInfo);
        bool AddRoleToProcessRoles(int flowId, int processId, int roleId);
        bool AddStatusCodeToProcessStatusCode(int flowId, int processId, int statusCode);
        bool Delete(int flowId);
        bool Delete(int flowId, int processId);
        bool DeleteRoleInProcessRole(int groupId);
        bool DeleteRoleInProcessRole(int flowId, int processId);
        bool DeleteStatusCodeInProcessStatusCode(int statusCode);
        bool DeleteStatusCodeInProcessStatusCode(int flowId, int processId);
        bool DeleteWorkFlowInProcessRole(int flowId);
        bool DeleteWorkFlowInProcessStatusCode(int flowId);
        bool ExistFlowProcess(int flowId, string processName);
        bool ExistRoleInProcessRole(int groupId);
        bool ExistRoleInProcessRole(int flowId, int processId);
        bool ExistStatusCodeInProcessStatusCode(int statusCode);
        bool ExistStatusCodeInProcessStatusCode(int flowId, int processId);
        bool ExistWorkFlowInFlowProcess(int flowId);
        bool ExistWorkFlowInProcessRole(int flowId);
        bool ExistWorkFlowInProcessStatusCode(int flowId);
        FlowProcessInfo GetFlowProcessById(int flowId, int processId);
        FlowProcessInfo GetFlowProcessByRoles(int flowId, int roleId);
        FlowProcessInfo GetFlowProcessByRoles(int flowId, string roleIdArr);
        IList<FlowProcessInfo> GetFlowProcessList(int flowId);
        string GetGroupIdByProcessIdAndFlowId(int flowId, int processId);
        IList<RoleInfo> GetProcessRoleList(int flowId, int processId);
        IList<StatusInfo> GetProcessStatusCodeList(int flowId, int processId);
        string GetStatusCodeToProcessStatusCode(int flowId, string rolesId);
        bool Update(FlowProcessInfo flowProcessInfo);
    }
}

