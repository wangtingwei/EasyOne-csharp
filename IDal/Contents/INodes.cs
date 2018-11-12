namespace EasyOne.IDal.Contents
{
    using EasyOne.Enumerations;
    using EasyOne.Model.Contents;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public interface INodes
    {
        bool Add(NodeInfo nodeInfo);
        bool BatchUpdateField(NodeInfo nodeInfo, string nodesId, Dictionary<string, bool> checkItem);
        bool DeleteInArrChild(string arrChildId);
        bool ExistNodeDir(int nodeId, string nodeDir);
        bool ExistsNodeIdentifier(int parentId, string nodeIdentifier);
        bool ExistsNodeName(int parentId, string nodeName);
        bool ExistsTargetNodeIdInArrChildId(int targetNodeId, string arrChildId);
        IList<NodeInfo> GetAnonymousNodeId(int groupId, OperateCode operateCode);
        int GetCountModelByNodeId(int nodeId);
        int GetCountNodesBySameNodeDir(int parentId, int nodeId, string nodeDir);
        bool GetDefaultTemplate(int nodeId, int templateId);
        int GetMaxNodeId();
        int GetMaxPurviewTypeInParentPath(string parentPath);
        int GetMaxRootId();
        int GetNextIdByDepth(int depth, string parentPath);
        NodeInfo GetNodeById(int nodeId);
        DataTable GetNodeNameByModelId(int modelId, NodeType nodeType);
        IList<NodeInfo> GetNodesList(NodeType nodeType);
        IList<NodeInfo> GetNodesList(string nodesId);
        IList<NodeInfo> GetNodesListByParentId(int parentId);
        IList<NodeInfo> GetNodesListByRootId(int rootId);
        IList<NodeInfo> GetNodesListInArrChildId(string arrChildId);
        IList<NodeInfo> GetNodesListInParentPath(string parentPath);
        int GetNodeWorkFlowId(int nodeId);
        NodeInfo GetParentNodeByNodeId(int parentId, int nodeId);
        Dictionary<int, string> GetParentPathArrChildId(string parentPath);
        int GetPrevId(int parentId);
        int GetPrevOrderId(string arrChildId);
        int GetRootPrevId(int maxRootId);
        IList<NodeInfo> GetShopNodeList();
        int GetTotalOfNodes();
        void ReplaceTemplateDir(string oldDir, string newDir);
        void ReplaceTemplateFileName(string replaceFormer, string replaceAfter);
        bool Update(NodeInfo nodeInfo);
        bool UpdateArrChildId(int nodeId, string arrChildId);
        bool UpdateChild(int parentId);
        bool UpdateChild(int nodeId, int child);
        bool UpdateChildPurview(string arrChildId, int purviewType);
        bool UpdateNeedCreateHtml(string arrNodeId, bool needCreateHtml);
        bool UpdateNextId(int nodeId, int nextId);
        bool UpdateNodePurviewType(int nodeId, int purviewType);
        bool UpdateOrderId(int nodeId, int orderId);
        bool UpdateOrderId(int rootId, int orderId, int addNum);
        void UpdateOrderIdByRootIdAndOrderd(int orderId, int rootId);
        bool UpdateRootId(string nodeId, int rootId);
        bool UpdateSettings(NodeSettingInfo settingsInfo, int nodeId);
    }
}

