namespace EasyOne.IDal.WorkFlow
{
    using EasyOne.Model.WorkFlow;
    using System;
    using System.Collections.Generic;

    public interface IWorkFlows
    {
        bool Add(WorkFlowsInfo workFlowsInfo);
        bool Delete(int flowId);
        bool Exists(string flowName);
        int GetMaxId();
        WorkFlowsInfo GetWorkFlowsById(int flowId);
        IList<WorkFlowsInfo> GetWorkFlowsList();
        bool Update(WorkFlowsInfo workFlowsInfo);
    }
}

