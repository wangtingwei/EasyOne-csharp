namespace EasyOne.IDal.Shop
{
    using EasyOne.Model.Shop;
    using System;
    using System.Collections.Generic;

    public interface IAgent
    {
        IList<string> GetAgentNameList(int startRowIndexId, int maxiNumRows, string keyword);
        int GetTotal();
        bool Payment(AgentInfo agentInfo);
    }
}

