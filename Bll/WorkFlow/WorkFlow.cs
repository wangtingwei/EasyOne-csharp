namespace EasyOne.WorkFlows
{
    using EasyOne.Common;
    using EasyOne.IDal.WorkFlow;
    using EasyOne.Model.WorkFlow;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using EasyOne.DalFactory;
    using EasyOne.Model.UserManage;

    public sealed class WorkFlow
    {
        private static readonly IWorkFlows dal = DataAccess.CreateWorkFlows();

        private WorkFlow()
        {
        }

        public static bool Add(WorkFlowsInfo workFlowsInfo)
        {
            return dal.Add(workFlowsInfo);
        }

        public static bool Copy(int flowId)
        {
            bool flag = false;
            WorkFlowsInfo workFlowsById = new WorkFlowsInfo();
            WorkFlowsInfo workFlowsInfo = new WorkFlowsInfo();
            workFlowsById = GetWorkFlowsById(flowId);
            workFlowsInfo.FlowId = GetMaxId() + 1;
            workFlowsInfo.FlowName = workFlowsById.FlowName + "复制";
            workFlowsInfo.Description = workFlowsById.Description;
            if (!Add(workFlowsInfo))
            {
                return flag;
            }
            string str = "";
            IList<FlowProcessInfo> flowProcessList = FlowProcess.GetFlowProcessList(flowId);
            for (int i = 0; i < flowProcessList.Count; i++)
            {
                FlowProcessInfo flowProcessInfo = new FlowProcessInfo();
                flowProcessInfo.FlowId = workFlowsInfo.FlowId;
                flowProcessInfo.Description = flowProcessList[i].Description;
                flowProcessInfo.PassActionName = flowProcessList[i].PassActionName;
                flowProcessInfo.PassActionStatus = flowProcessList[i].PassActionStatus;
                flowProcessInfo.ProcessId = flowProcessList[i].ProcessId;
                flowProcessInfo.ProcessName = flowProcessList[i].ProcessName;
                flowProcessInfo.RejectActionName = flowProcessList[i].RejectActionName;
                flowProcessInfo.RejectActionStatus = flowProcessList[i].RejectActionStatus;
                IList<StatusInfo> processStatusCodeList = FlowProcess.GetProcessStatusCodeList(flowProcessList[i].FlowId, flowProcessList[i].ProcessId);
                StringBuilder sb = new StringBuilder();
                for (int j = 0; j < processStatusCodeList.Count; j++)
                {
                    StringHelper.AppendString(sb, processStatusCodeList[j].StatusCode.ToString());
                }
                IList<RoleInfo> processRoleList = FlowProcess.GetProcessRoleList(flowProcessList[i].FlowId, flowProcessList[i].ProcessId);
                for (int k = 0; k < processRoleList.Count; k++)
                {
                    if (string.IsNullOrEmpty(str))
                    {
                        str = processRoleList[k].RoleId.ToString();
                    }
                    else
                    {
                        str = str + "," + processRoleList[k].RoleId;
                    }
                }
                if (!FlowProcess.Add(flowProcessInfo, sb.ToString(), str))
                {
                    break;
                }
            }
            return true;
        }

        public static bool Delete(int workFlowsId)
        {
            bool flag = false;
            bool[] flagArray = new bool[] { true, true };
            if (FlowProcess.ExistWorkFlowInFlowProcess(workFlowsId))
            {
                flagArray[0] = FlowProcess.Delete(workFlowsId);
            }
            flagArray[1] = dal.Delete(workFlowsId);
            if (flagArray[0] && flagArray[1])
            {
                flag = true;
            }
            return flag;
        }

        public static bool Exists(string flowName)
        {
            return dal.Exists(flowName);
        }

        public static int GetMaxId()
        {
            return dal.GetMaxId();
        }

        public static WorkFlowsInfo GetWorkFlowsById(int workFlowsId)
        {
            return dal.GetWorkFlowsById(workFlowsId);
        }

        public static IList<WorkFlowsInfo> GetWorkFlowsList()
        {
            return dal.GetWorkFlowsList();
        }

        public static bool Update(WorkFlowsInfo workFlowsInfo)
        {
            return dal.Update(workFlowsInfo);
        }
    }
}

