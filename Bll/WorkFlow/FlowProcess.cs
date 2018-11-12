namespace EasyOne.WorkFlows
{
    using EasyOne.Common;
    using EasyOne.IDal.WorkFlow;
    using EasyOne.Model.WorkFlow;
    using System;
    using System.Collections.Generic;
    using EasyOne.Model.UserManage;
    using EasyOne.DalFactory;

    public sealed class FlowProcess
    {
        private static readonly IFlowProcess dal = DataAccess.CreateFlowProcess();

        private FlowProcess()
        {
        }

        public static bool Add(FlowProcessInfo flowProcessInfo)
        {
            bool flag = false;
            if (flowProcessInfo.FlowId > 0)
            {
                flag = dal.Add(flowProcessInfo);
            }
            return flag;
        }

        public static bool Add(FlowProcessInfo flowProcessInfo, string statusCodes, string roleIds)
        {
            bool[] flagArray = new bool[] { true, true, true };
            bool flag = false;
            if (Add(flowProcessInfo))
            {
                if (!string.IsNullOrEmpty(statusCodes))
                {
                    flagArray[1] = AddStatusCodeToProcessStatusCode(flowProcessInfo.FlowId, flowProcessInfo.ProcessId, statusCodes);
                }
                if (!string.IsNullOrEmpty(roleIds))
                {
                    flagArray[2] = AddGroupToProcessGroup(flowProcessInfo.FlowId, flowProcessInfo.ProcessId, roleIds);
                }
            }
            else
            {
                flagArray[0] = false;
            }
            if ((flagArray[0] && flagArray[1]) && flagArray[2])
            {
                flag = true;
            }
            return flag;
        }

        public static bool AddGroupToProcessGroup(int flowId, int processId, string groupIds)
        {
            bool flag = false;
            if (!DataValidator.IsValidId(groupIds))
            {
                return false;
            }
            foreach (string str in groupIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                flag = dal.AddRoleToProcessRoles(flowId, processId, DataConverter.CLng(str));
                if (!flag)
                {
                    DeleteGroupInProcess(flowId, processId);
                    return flag;
                }
            }
            return flag;
        }

        public static bool AddStatusCodeToProcessStatusCode(int flowId, int processId, string stateCodesId)
        {
            if (!DataValidator.IsValidId(stateCodesId))
            {
                return false;
            }
            foreach (string str in stateCodesId.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (!dal.AddStatusCodeToProcessStatusCode(flowId, processId, DataConverter.CLng(str)))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool Delete(int flowId)
        {
            bool flag = false;
            bool[] flagArray = new bool[] { true, true, true };
            if (ExistWorkFlowInProcessRole(flowId))
            {
                flagArray[0] = DeleteWorkFlowInProcessRole(flowId);
            }
            if (ExistWorkFlowInProcessStatusCode(flowId))
            {
                flagArray[1] = DeleteWorkFlowInProcessStatusCode(flowId);
            }
            flagArray[2] = dal.Delete(flowId);
            if ((flagArray[0] && flagArray[1]) && flagArray[2])
            {
                flag = true;
            }
            return flag;
        }

        public static bool Delete(int flowId, int processId)
        {
            bool flag = false;
            bool[] flagArray = new bool[] { true, true, true };
            if (ExistRoleInProcessRole(flowId, processId))
            {
                flagArray[0] = DeleteGroupInProcess(flowId, processId);
            }
            if (ExistStatusCodeInProcessStatusCode(flowId, processId))
            {
                flagArray[1] = DeleteStatusCodeInProcessStatusCode(flowId, processId);
            }
            flagArray[2] = dal.Delete(flowId, processId);
            if ((flagArray[0] && flagArray[1]) && flagArray[2])
            {
                flag = true;
            }
            return flag;
        }

        public static bool DeleteGroupInProcess(int flowId, int processId)
        {
            return dal.DeleteRoleInProcessRole(flowId, processId);
        }

        public static bool DeleteStatusCodeInProcessStatusCode(int statusCode)
        {
            return dal.DeleteStatusCodeInProcessStatusCode(statusCode);
        }

        public static bool DeleteStatusCodeInProcessStatusCode(int flowId, int processId)
        {
            return dal.DeleteStatusCodeInProcessStatusCode(flowId, processId);
        }

        public static bool DeleteWorkFlowInProcessRole(int flowId)
        {
            return dal.DeleteWorkFlowInProcessRole(flowId);
        }

        public static bool DeleteWorkFlowInProcessStatusCode(int flowId)
        {
            return dal.DeleteWorkFlowInProcessStatusCode(flowId);
        }

        public static bool ExistFlowProcess(int flowId, string processName)
        {
            return dal.ExistFlowProcess(flowId, processName);
        }

        public static bool ExistRoleInProcessRole(int roleId)
        {
            return dal.ExistRoleInProcessRole(roleId);
        }

        public static bool ExistRoleInProcessRole(int flowId, int processId)
        {
            return dal.ExistRoleInProcessRole(flowId, processId);
        }

        public static bool ExistStatusCodeInProcessStatusCode(int statusCode)
        {
            return dal.ExistStatusCodeInProcessStatusCode(statusCode);
        }

        public static bool ExistStatusCodeInProcessStatusCode(int flowId, int processId)
        {
            return dal.ExistStatusCodeInProcessStatusCode(flowId, processId);
        }

        public static bool ExistWorkFlowInFlowProcess(int flowId)
        {
            return dal.ExistWorkFlowInFlowProcess(flowId);
        }

        public static bool ExistWorkFlowInProcessRole(int flowId)
        {
            return dal.ExistWorkFlowInProcessRole(flowId);
        }

        public static bool ExistWorkFlowInProcessStatusCode(int flowId)
        {
            return dal.ExistWorkFlowInProcessStatusCode(flowId);
        }

        public static FlowProcessInfo GetFlowProcessById(int flowId, int processId)
        {
            return dal.GetFlowProcessById(flowId, processId);
        }

        public static FlowProcessInfo GetFlowProcessByRoles(int flowId, int roleId)
        {
            return dal.GetFlowProcessByRoles(flowId, roleId);
        }

        public static FlowProcessInfo GetFlowProcessByRoles(int flowId, string roleIdArr)
        {
            if (!DataValidator.IsValidId(roleIdArr))
            {
                return new FlowProcessInfo(true);
            }
            return dal.GetFlowProcessByRoles(flowId, roleIdArr);
        }

        public static IList<FlowProcessInfo> GetFlowProcessList(int flowId)
        {
            return dal.GetFlowProcessList(flowId);
        }

        public static string GetGroupIdByProcessIdAndFlowId(int flowId, int processId)
        {
            return dal.GetGroupIdByProcessIdAndFlowId(flowId, processId);
        }

        public static IList<RoleInfo> GetProcessRoleList(int flowId, int processId)
        {
            return dal.GetProcessRoleList(flowId, processId);
        }

        public static IList<StatusInfo> GetProcessStatusCodeList(int flowId, int processId)
        {
            return dal.GetProcessStatusCodeList(flowId, processId);
        }

        public static string GetStatusCodeToProcessStatusCode(int flowId, string rolesId)
        {
            if (!DataValidator.IsValidId(rolesId))
            {
                return string.Empty;
            }
            return dal.GetStatusCodeToProcessStatusCode(flowId, rolesId);
        }

        public static bool Update(FlowProcessInfo flowProcessInfo)
        {
            return dal.Update(flowProcessInfo);
        }

        public static bool Update(FlowProcessInfo flowProcessInfo, string statusCodes, string roleIds)
        {
            bool[] flagArray = new bool[] { true, true, true };
            bool flag = false;
            if (flowProcessInfo != null)
            {
                flagArray[0] = Update(flowProcessInfo);
            }
            if ((flowProcessInfo != null) && !string.IsNullOrEmpty(statusCodes))
            {
                flagArray[1] = UpdateStatusCodeInProcessStatusCode(flowProcessInfo.FlowId, flowProcessInfo.ProcessId, statusCodes);
            }
            if ((flowProcessInfo != null) && !string.IsNullOrEmpty(roleIds))
            {
                flagArray[2] = UpdateRoleInProcessRole(flowProcessInfo.FlowId, flowProcessInfo.ProcessId, roleIds);
            }
            if ((flagArray[0] && flagArray[1]) && flagArray[2])
            {
                flag = true;
            }
            return flag;
        }

        public static bool UpdateRoleInProcessRole(int flowId, int processId, string roleIds)
        {
            if (!DataValidator.IsValidId(roleIds))
            {
                return false;
            }
            DeleteGroupInProcess(flowId, processId);
            return AddGroupToProcessGroup(flowId, processId, roleIds);
        }

        public static bool UpdateStatusCodeInProcessStatusCode(int flowId, int processId, string stateCodesId)
        {
            if (!DataValidator.IsValidId(stateCodesId))
            {
                return false;
            }
            DeleteStatusCodeInProcessStatusCode(flowId, processId);
            return AddStatusCodeToProcessStatusCode(flowId, processId, stateCodesId);
        }
    }
}

