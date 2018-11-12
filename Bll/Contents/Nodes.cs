namespace EasyOne.Contents
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Components;
    using EasyOne.Enumerations;
    using EasyOne.IDal.Contents;
    using EasyOne.Model.AccessManage;
    using EasyOne.Model.Contents;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Text;
    using System.Web;
    using EasyOne.DalFactory;

    public sealed class Nodes
    {
        private static readonly INodes dal = DataAccess.CreateNodes();
        private static int orderId;
        private static int rootId;

        private Nodes()
        {
        }

        public static int Add(NodeInfo nodeInfo)
        {
            int maxRootId = GetMaxRootId();
            nodeInfo.NodeId = GetMaxNodeId() + 1;
            nodeInfo.ArrChildId = nodeInfo.NodeId.ToString();
            nodeInfo.Child = 0;
            if (ExistsNodeName(nodeInfo.ParentId, nodeInfo.NodeName))
            {
                return 2;
            }
            if (ExistsNodeIdentifier(nodeInfo.ParentId, nodeInfo.NodeIdentifier))
            {
                return 3;
            }
            if (nodeInfo.NodeType < NodeType.Special)
            {
                nodeInfo.ParentDir = "/" + nodeInfo.ParentDir;
                if (ExistsNodeDir(nodeInfo.ParentId, nodeInfo.NodeDir))
                {
                    return 4;
                }
            }
            if (nodeInfo.ParentId > 0)
            {
                NodeInfo cacheNodeById = GetCacheNodeById(nodeInfo.ParentId);
                if (cacheNodeById.IsNull)
                {
                    return 6;
                }
                nodeInfo.ParentPath = cacheNodeById.ParentPath + "," + cacheNodeById.NodeId.ToString();
                nodeInfo.RootId = cacheNodeById.RootId;
                nodeInfo.Depth = cacheNodeById.Depth + 1;
                nodeInfo.ParentDir = cacheNodeById.ParentDir + cacheNodeById.NodeDir + "/";
                if ((cacheNodeById.NodeType == NodeType.Single) || (cacheNodeById.NodeType == NodeType.Link))
                {
                    return 5;
                }
                UpdateChild(nodeInfo.ParentId);
                foreach (KeyValuePair<int, string> pair in GetParentPathArrChildId(nodeInfo.ParentPath))
                {
                    int key = pair.Key;
                    string arrChildId = pair.Value + "," + nodeInfo.NodeId.ToString();
                    UpdateArrChildId(key, arrChildId);
                }
                if (cacheNodeById.Child > 0)
                {
                    nodeInfo.OrderId = GetPrevOrderId(cacheNodeById.ArrChildId);
                    nodeInfo.PrevId = GetPrevId(nodeInfo.ParentId);
                }
                else
                {
                    nodeInfo.OrderId = cacheNodeById.OrderId;
                    nodeInfo.PrevId = 0;
                }
            }
            else
            {
                nodeInfo.ParentDir = "/";
                if (maxRootId > 0)
                {
                    nodeInfo.PrevId = dal.GetRootPrevId(maxRootId);
                }
                else
                {
                    nodeInfo.PrevId = 0;
                }
                nodeInfo.RootId = maxRootId + 1;
                nodeInfo.ParentPath = "0";
                nodeInfo.OrderId = 0;
                nodeInfo.Depth = 0;
            }
            UpdateNextId(nodeInfo.PrevId, nodeInfo.NodeId);
            bool flag = dal.Add(nodeInfo);
            UpdateOrderId(nodeInfo.RootId, nodeInfo.OrderId, 1);
            if (nodeInfo.ParentId > 0)
            {
                UpdateOrderId(nodeInfo.NodeId, nodeInfo.OrderId + 1);
            }
            RemoveCacheAllNodeInfo();
            if (!flag)
            {
                return 0;
            }
            return 1;
        }

        public static int Add(NodeInfo nodeInfo, IList<NodesModelTemplateRelationShipInfo> infoList)
        {
            int num = Add(nodeInfo);
            if (num == 1)
            {
                foreach (NodesModelTemplateRelationShipInfo info in infoList)
                {
                    info.NodeId = nodeInfo.NodeId;
                    if (!ModelManager.ExistsNodesModelTemplateRelationShip(info))
                    {
                        ModelManager.AddNodesModelTemplateRelationShip(info);
                    }
                }
            }
            return num;
        }

        public static bool BatchUpdate(NodeInfo nodeInfo, string nodesId, Dictionary<string, bool> checkItem)
        {
            foreach (NodeInfo info in GetNodesList(nodesId))
            {
                NodeSettingInfo settings = info.Settings;
                if (checkItem["EnableComment"])
                {
                    settings.EnableComment = nodeInfo.Settings.EnableComment;
                    settings.CommentNeedCheck = nodeInfo.Settings.CommentNeedCheck;
                    settings.EnableTouristsComment = nodeInfo.Settings.EnableTouristsComment;
                }
                if (checkItem["EnableProtect"])
                {
                    settings.EnableProtect = nodeInfo.Settings.EnableProtect;
                }
                if (checkItem["EnableAddWhenHasChild"])
                {
                    settings.EnableAddWhenHasChild = nodeInfo.Settings.EnableAddWhenHasChild;
                }
                if (checkItem["PresentExp"])
                {
                    settings.PresentExp = nodeInfo.Settings.PresentExp;
                }
                if (checkItem["DefaultItemPoint"])
                {
                    settings.DefaultItemPoint = nodeInfo.Settings.DefaultItemPoint;
                }
                if (checkItem["ShowChargeType"])
                {
                    settings.DefaultItemChargeType = nodeInfo.Settings.DefaultItemChargeType;
                    settings.DefaultItemPitchTime = nodeInfo.Settings.DefaultItemPitchTime;
                    settings.DefaultItemReadTimes = nodeInfo.Settings.DefaultItemReadTimes;
                }
                if (checkItem["DefaultItemDividePercent"])
                {
                    settings.DefaultItemDividePercent = nodeInfo.Settings.DefaultItemDividePercent;
                }
                dal.UpdateSettings(settings, info.NodeId);
            }
            BatchUpdateField(nodeInfo, nodesId, checkItem);
            RemoveCacheAllNodeInfo();
            return true;
        }

        public static bool BatchUpdateField(NodeInfo nodeInfo, string nodesId, Dictionary<string, bool> checkItem)
        {
            if (!DataValidator.IsValidId(nodesId))
            {
                return false;
            }
            if (!checkItem.ContainsValue(true))
            {
                return false;
            }
            return dal.BatchUpdateField(nodeInfo, nodesId, checkItem);
        }

        public static bool CheckNodePermission(int nodeId)
        {
            if (nodeId > 0)
            {
                NodeInfo cacheNodeById = GetCacheNodeById(nodeId);
                if (!cacheNodeById.Settings.EnableAddWhenHasChild && (cacheNodeById.Child > 0))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool CheckRoleNodePurview(int nodeId, OperateCode operateCode)
        {
            bool flag = true;
            if (PEContext.Current.Admin.IsSuperAdmin || RolePermissions.AccessCheckNodePermission(operateCode, -1))
            {
                return flag;
            }
            if (nodeId > 0)
            {
                NodeInfo cacheNodeById = GetCacheNodeById(nodeId);
                flag = RolePermissions.AccessCheckNodePermission(operateCode, cacheNodeById.NodeId);
                if (!flag && (cacheNodeById.ParentId > 0))
                {
                    string roleNodeId = RolePermissions.GetRoleNodeId(PEContext.Current.Admin.Roles, operateCode);
                    string findStr = cacheNodeById.ParentPath + "," + cacheNodeById.NodeId.ToString();
                    flag = StringHelper.FoundCharInArr(roleNodeId, findStr);
                }
                return flag;
            }
            return false;
        }

        public static int Delete(int nodeId)
        {
            if (nodeId == 0)
            {
                return 3;
            }
            NodeInfo cacheNodeById = GetCacheNodeById(nodeId);
            if (cacheNodeById.IsNull)
            {
                return 2;
            }
            DeleteNodeRelationByNodeId(nodeId);
            DeleteInArrChild(cacheNodeById.ArrChildId);
            if (cacheNodeById.ParentId > 0)
            {
                NodeInfo nodeInfo = GetCacheNodeById(cacheNodeById.ParentId);
                nodeInfo.Child--;
                nodeInfo.ArrChildId = nodeInfo.ArrChildId.Replace("," + cacheNodeById.ArrChildId, "");
                Update(nodeInfo);
            }
            PatchNodes();
            ContentManage.DeleteByNodeId(cacheNodeById.ArrChildId, -4);
            DeleteNodeFolder(cacheNodeById);
            RemoveCacheAllNodeInfo();
            return 1;
        }

        public static bool DeleteInArrChild(string arrChildId)
        {
            if (!DataValidator.IsValidId(arrChildId))
            {
                return false;
            }
            RemoveCacheAllNodeInfo();
            return dal.DeleteInArrChild(arrChildId);
        }

        private static void DeleteNodeFolder(NodeInfo nodeInfo)
        {
            if (!string.IsNullOrEmpty(nodeInfo.ParentDir) && !string.IsNullOrEmpty(nodeInfo.NodeDir))
            {
                string path = "~/" + SiteConfig.SiteOption.CreateHtmlPath + nodeInfo.ParentDir + nodeInfo.NodeDir;
                if (HttpContext.Current != null)
                {
                    FileSystemObject.Delete(HttpContext.Current.Server.MapPath(path), FsoMethod.Folder);
                }
            }
        }

        private static void DeleteNodeRelationByNodeId(int nodeId)
        {
            RolePermissions.DeleteNodePermissionFromRoles(-1, nodeId);
            UserPermissions.DeleteNodePermissionsByNodeId(nodeId, OperateCode.None);
            ModelManager.DeleteNodeModel(nodeId);
            ModelManager.DeleteNodeTemplateId(nodeId);
            ModelManager.DeleteNodesModelTemplateRelationShip(nodeId);
        }

        public static bool ExistsNodeDir(int parentId, string nodeDir)
        {
            if (string.IsNullOrEmpty(nodeDir))
            {
                return false;
            }
            return dal.ExistNodeDir(parentId, nodeDir);
        }

        public static bool ExistsNodeIdentifier(int parentId, string nodeIdentifier)
        {
            return dal.ExistsNodeIdentifier(parentId, nodeIdentifier);
        }

        public static bool ExistsNodeName(int parentId, string nodeName)
        {
            return dal.ExistsNodeName(parentId, nodeName);
        }

        private static bool ExistsNodeNameTwo(int parentId, string nodeName)
        {
            return dal.ExistsNodeName(parentId, nodeName);
        }

        public static bool ExistsTargetNodeIdInArrChildId(int targetNodeId, string arrChildId)
        {
            if (!DataValidator.IsValidId(arrChildId))
            {
                return false;
            }
            return dal.ExistsTargetNodeIdInArrChildId(targetNodeId, arrChildId);
        }

        public static IList<NodeInfo> GetAnonymousNodeId(int groupId, OperateCode operateCode)
        {
            return dal.GetAnonymousNodeId(groupId, operateCode);
        }

        public static NodeInfo GetCacheNodeById(int nodeId)
        {
            if ((nodeId != -2) && (nodeId <= 0))
            {
                return new NodeInfo(true);
            }
            string key = "CK_Content_NodeInfo_NodeId_" + nodeId.ToString();
            NodeInfo nodeById = SiteCache.Get(key) as NodeInfo;
            if (nodeById == null)
            {
                nodeById = dal.GetNodeById(nodeId);
                if (!nodeById.IsNull)
                {
                    SiteCache.Insert(key, nodeById, 0x4380);
                }
            }
            return nodeById;
        }

        public static int GetCountModelByNodeId(int nodeId)
        {
            return dal.GetCountModelByNodeId(nodeId);
        }

        public static int GetCountNodesBySameNodeDir(int parentId, int nodeId, string nodeDir)
        {
            return dal.GetCountNodesBySameNodeDir(parentId, nodeId, nodeDir);
        }

        public static bool GetDefaultTemplate(int nodeId, int templateId)
        {
            return dal.GetDefaultTemplate(nodeId, templateId);
        }

        public static int GetMaxNodeId()
        {
            int maxNodeId = dal.GetMaxNodeId();
            if (maxNodeId < 0)
            {
                maxNodeId = 0;
            }
            return maxNodeId;
        }

        public static int GetMaxPurviewTypeInParentPath(int nodeId)
        {
            NodeInfo cacheNodeById = GetCacheNodeById(nodeId);
            if (!DataValidator.IsValidId(cacheNodeById.ParentPath))
            {
                return 0;
            }
            return dal.GetMaxPurviewTypeInParentPath(cacheNodeById.ParentPath);
        }

        private static int GetMaxRootId()
        {
            return dal.GetMaxRootId();
        }

        public static int GetNextIdByDepth(int depth, string parentPath)
        {
            if (DataValidator.IsValidId(parentPath))
            {
                return dal.GetNextIdByDepth(depth, parentPath);
            }
            return 0;
        }

        public static NodeInfo GetNodeById(int nodeId)
        {
            if ((nodeId != -2) && (nodeId <= 0))
            {
                return new NodeInfo(true);
            }
            return dal.GetNodeById(nodeId);
        }

        public static int GetNodeByIdCopyNode(int nodeId)
        {
            int maxNodeId = 0;
            if (nodeId > 0)
            {
                NodeInfo nodeById = GetNodeById(nodeId);
                nodeById.NodeName = StringHelper.CopyString(nodeById.NodeName);
                nodeById.NodeIdentifier = StringHelper.CopyStringNum(nodeById.NodeIdentifier);
                nodeById.NodeDir = StringHelper.CopyStringNum(nodeById.NodeDir);
                while (ExistsNodeNameTwo(nodeById.ParentId, nodeById.NodeName))
                {
                    nodeById.NodeName = StringHelper.CopyString(nodeById.NodeName);
                }
                while (ExistsNodeIdentifier(nodeById.ParentId, nodeById.NodeIdentifier))
                {
                    nodeById.NodeIdentifier = StringHelper.CopyStringNum(nodeById.NodeIdentifier);
                }
                while (ExistsNodeDir(nodeById.ParentId, nodeById.NodeDir))
                {
                    nodeById.NodeDir = StringHelper.CopyStringNum(nodeById.NodeDir);
                }
                if (Add(nodeById) != 1)
                {
                    return maxNodeId;
                }
                maxNodeId = GetMaxNodeId();
                foreach (NodesModelTemplateRelationShipInfo info2 in ModelManager.GetNodesModelTemplateList(nodeId))
                {
                    info2.NodeId = maxNodeId;
                    if (!ModelManager.ExistsNodesModelTemplateRelationShip(info2) && !ModelManager.AddNodesModelTemplateRelationShip(info2))
                    {
                        maxNodeId = -1;
                    }
                }
                if (PEContext.Current.Admin.IsSuperAdmin)
                {
                    return maxNodeId;
                }
                foreach (RoleNodePermissionsInfo info3 in RolePermissions.GetNodePermissionsByNodeId(nodeId))
                {
                    info3.NodeId = maxNodeId;
                    RolePermissions.AddNodePermissionToRoles(info3.GroupId, info3.NodeId, info3.OperateCode);
                }
            }
            return maxNodeId;
        }

        public static string GetNodeDir(int child, NodeType nodeType, string nodeDir)
        {
            StringBuilder builder = new StringBuilder();
            if (child > 0)
            {
                builder.Append("（");
                builder.Append(child);
                builder.Append("）");
            }
            if (NodeType.Link == nodeType)
            {
                builder.Append(" <font color=blue>（外）</font>");
            }
            else if (!string.IsNullOrEmpty(nodeDir))
            {
                builder.Append(" [");
                builder.Append(nodeDir);
                builder.Append("]");
            }
            return builder.ToString();
        }

        public static IList<NodeInfo> GetNodeList()
        {
            IList<NodeInfo> nodesList = GetNodesList();
            NodeInfo item = new NodeInfo();
            item.NodeName = "所有栏目";
            item.NodeId = -1;
            item.Depth = 0;
            item.ParentPath = "0";
            item.NextId = 0;
            item.Child = 0;
            item.NodeType = NodeType.Container;
            item.NodeDir = "";
            nodesList.Insert(0, item);
            return nodesList;
        }

        public static IList<NodeInfo> GetNodeListExecptLinkType()
        {
            IList<NodeInfo> nodesList = GetNodesList(NodeType.Container);
            NodeInfo item = new NodeInfo();
            item.NodeName = "所有栏目";
            item.NodeId = -1;
            item.Depth = 0;
            item.ParentPath = "0";
            item.NextId = 0;
            item.Child = 0;
            item.NodeType = NodeType.Container;
            item.NodeDir = "";
            nodesList.Insert(0, item);
            return nodesList;
        }

        public static DataTable GetNodeNameByModelId(int modelId, NodeType nodeType)
        {
            return dal.GetNodeNameByModelId(modelId, nodeType);
        }

        public static IList<NodeInfo> GetNodeNameForContainerItems()
        {
            return NodeTreeItems(GetNodesList(NodeType.Container));
        }

        public static IList<NodeInfo> GetNodeNameForItems()
        {
            return NodeTreeItems(GetNodesList());
        }

        public static IList<NodeInfo> GetNodeNameForItemsExceptOutLinks()
        {
            IList<NodeInfo> nodesList = GetNodesList();
            IList<NodeInfo> nodeList = new List<NodeInfo>();
            foreach (NodeInfo info in nodesList)
            {
                if (info.NodeType != NodeType.Link)
                {
                    nodeList.Add(info);
                }
            }
            return NodeTreeItems(nodeList);
        }

        public static IList<NodeInfo> GetNodesList()
        {
            return dal.GetNodesList(NodeType.None);
        }

        public static IList<NodeInfo> GetNodesList(NodeType nodeType)
        {
            return dal.GetNodesList(nodeType);
        }

        public static IList<NodeInfo> GetNodesList(string nodesId)
        {
            if (!DataValidator.IsValidId(nodesId))
            {
                return new List<NodeInfo>();
            }
            return dal.GetNodesList(nodesId);
        }

        public static IList<NodeInfo> GetNodesListByParentId(int parentId)
        {
            return dal.GetNodesListByParentId(parentId);
        }

        public static IList<NodeInfo> GetNodesListByRootId(int rootId)
        {
            return dal.GetNodesListByRootId(rootId);
        }

        public static IList<NodeInfo> GetNodesListInArrChildId(string arrChildId)
        {
            if (!DataValidator.IsValidId(arrChildId))
            {
                return null;
            }
            RemoveCacheAllNodeInfo();
            return dal.GetNodesListInArrChildId(arrChildId);
        }

        public static IList<NodeInfo> GetNodesListInParentPath(string parentPath)
        {
            if (DataValidator.IsValidId(parentPath))
            {
                return dal.GetNodesListInParentPath(parentPath);
            }
            return null;
        }

        public static int GetNodeWorkFlowId(int nodeId)
        {
            return dal.GetNodeWorkFlowId(nodeId);
        }

        public static NodeInfo GetParentNodeByNodeId(int parentId, int nodeId)
        {
            return dal.GetParentNodeByNodeId(parentId, nodeId);
        }

        private static NodeInfo GetParentNodeInfo(NodeInfo nodeInfo)
        {
            if (nodeInfo.ParentId != 0)
            {
                while (nodeInfo.ParentId != 0)
                {
                    nodeInfo = GetNodeById(nodeInfo.ParentId);
                }
                return nodeInfo;
            }
            return nodeInfo;
        }

        public static Dictionary<int, string> GetParentPathArrChildId(string parentPath)
        {
            if (DataValidator.IsValidId(parentPath))
            {
                return dal.GetParentPathArrChildId(parentPath);
            }
            return null;
        }

        private static int GetPrevId(int parentId)
        {
            return dal.GetPrevId(parentId);
        }

        public static int GetPrevOrderId(string arrChildId)
        {
            if (DataValidator.IsValidId(arrChildId))
            {
                return dal.GetPrevOrderId(arrChildId);
            }
            return 0;
        }

        public static IList<NodeInfo> GetShopNodeList()
        {
            return NodeTreeItems(dal.GetShopNodeList());
        }

        public static int GetTotalOfNodes()
        {
            return dal.GetTotalOfNodes();
        }

        public static string GetTreeLine(int depth, string parentPath, int nextId, int child)
        {
            StringBuilder builder = new StringBuilder("");
            string str = DataSecurity.FilterBadChar(parentPath);
            string str2 = "";
            str2 = HttpContext.Current.Request.ApplicationPath.Equals("/") ? string.Empty : HttpContext.Current.Request.ApplicationPath;
            str2 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + str2;
            if (depth > 0)
            {
                for (int i = 1; i <= depth; i++)
                {
                    if (i == depth)
                    {
                        if (nextId > 0)
                        {
                            builder.Append("<img src='" + str2 + "/Admin/images/Node/tree_line1.gif' width='17' height='16' valign='abvmiddle'>");
                        }
                        else
                        {
                            builder.Append("<img src='" + str2 + "/Admin/images/Node/tree_line2.gif' width='17' height='16' valign='abvmiddle'>");
                        }
                    }
                    else if (GetNextIdByDepth(i, str) > 0)
                    {
                        builder.Append("<img src='" + str2 + "/Admin/images/Node/tree_line3.gif' width='17' height='16' valign='abvmiddle'>");
                    }
                    else
                    {
                        builder.Append("<img src='" + str2 + "/Admin/images/Node/tree_line4.gif' width='17' height='16' valign='abvmiddle'>");
                    }
                }
            }
            if (child > 0)
            {
                builder.Append("<img src='" + str2 + "/Admin/images/Node/tree_folder4.gif' width='15' height='15' valign='abvmiddle'>");
            }
            else
            {
                builder.Append("<img src='" + str2 + "/Admin/images/Node/tree_folder3.gif' width='15' height='15' valign='abvmiddle'>");
            }
            if (depth == 0)
            {
                builder.Append("<b>");
            }
            return builder.ToString();
        }

        public static void ModifyNodeStructure(NodeInfo nodeInfo)
        {
            nodeInfo = GetParentNodeInfo(nodeInfo);
            if (nodeInfo.ParentId == 0)
            {
                orderId = 0;
                UpateNodeStructure(nodeInfo.NodeId, nodeInfo.Depth + 1, nodeInfo.ParentPath, "/" + nodeInfo.NodeDir + "/");
            }
            RemoveCacheAllNodeInfo();
        }

        public static int NodesMove(int nodeId, int moveToNodeId)
        {
            int num = 0;
            if (nodeId == moveToNodeId)
            {
                return 2;
            }
            NodeInfo cacheNodeById = GetCacheNodeById(moveToNodeId);
            NodeInfo nodeInfo = GetCacheNodeById(nodeId);
            if (moveToNodeId > 0)
            {
                num = NodesMoveValidCheck(nodeInfo, cacheNodeById);
                if (num > 0)
                {
                    return num;
                }
            }
            nodeInfo.ParentId = moveToNodeId;
            if ((moveToNodeId == 0) || cacheNodeById.IsNull)
            {
                nodeInfo.RootId = GetMaxRootId() + 1;
                nodeInfo.ParentPath = "0";
                nodeInfo.Depth = 0;
                nodeInfo.ParentDir = "/";
                nodeInfo.PrevId = GetMaxNodeId();
                nodeInfo.NextId = 0;
            }
            else
            {
                nodeInfo.ParentPath = cacheNodeById.ParentPath + "," + moveToNodeId.ToString();
                nodeInfo.RootId = cacheNodeById.RootId;
                nodeInfo.Depth = cacheNodeById.Depth + 1;
                nodeInfo.ParentDir = cacheNodeById.ParentDir + cacheNodeById.NodeDir + "/";
                nodeInfo.PrevId = GetPrevId(moveToNodeId);
                nodeInfo.NextId = 0;
            }
            Update(nodeInfo);
            PatchNodes();
            RemoveCacheAllNodeInfo();
            return num;
        }

        private static int NodesMoveValidCheck(NodeInfo nodeInfo, NodeInfo moveToNodeInfo)
        {
            if (nodeInfo.IsNull)
            {
                return 1;
            }
            if (nodeInfo.ParentId == moveToNodeInfo.NodeId)
            {
                return 3;
            }
            if (moveToNodeInfo.NodeType != NodeType.Container)
            {
                return 4;
            }
            if (StringHelper.FoundCharInArr(nodeInfo.ArrChildId, moveToNodeInfo.NodeId.ToString()))
            {
                return 5;
            }
            if (ExistsNodeName(moveToNodeInfo.NodeId, nodeInfo.NodeName))
            {
                return 6;
            }
            if (ExistsNodeDir(moveToNodeInfo.NodeId, nodeInfo.NodeDir))
            {
                return 7;
            }
            return 0;
        }

        public static int NodesUnite(int nodeId, int targetNodeId)
        {
            if (nodeId <= 0)
            {
                return 1;
            }
            if (targetNodeId <= 0)
            {
                return 2;
            }
            if (nodeId == targetNodeId)
            {
                return 3;
            }
            NodeInfo nodeById = GetNodeById(targetNodeId);
            if (nodeById.IsNull)
            {
                return 4;
            }
            if (nodeById.Child > 0)
            {
                return 5;
            }
            if (nodeById.NodeType != NodeType.Container)
            {
                return 6;
            }
            NodeInfo cacheNodeById = GetCacheNodeById(nodeId);
            if (cacheNodeById.IsNull)
            {
                return 7;
            }
            if (StringHelper.FoundCharInArr(cacheNodeById.ArrChildId, targetNodeId.ToString()))
            {
                return 8;
            }
            ContentManage.UpdateNodeId(targetNodeId, cacheNodeById.ArrChildId);
            Delete(cacheNodeById.NodeId);
            RemoveCacheAllNodeInfo();
            return 0;
        }

        private static IList<NodeInfo> NodeTreeItems(IList<NodeInfo> nodeList)
        {
            int index = 0;
            char ch = '\x00a0';
            bool[] flagArray = new bool[50];
            foreach (NodeInfo info in nodeList)
            {
                index = info.Depth;
                if (info.NextId > 0)
                {
                    flagArray[index] = true;
                }
                else
                {
                    flagArray[index] = false;
                }
                StringBuilder builder = new StringBuilder();
                if (index > 0)
                {
                    for (int i = 1; i <= index; i++)
                    {
                        builder.Append(ch);
                        builder.Append(ch);
                        if (i == index)
                        {
                            if (info.NextId > 0)
                            {
                                builder.Append("├");
                                builder.Append(ch);
                            }
                            else
                            {
                                builder.Append("└");
                                builder.Append(ch);
                            }
                        }
                        else if (flagArray[i])
                        {
                            builder.Append("│");
                        }
                        else
                        {
                            builder.Append(ch);
                        }
                    }
                }
                builder.Append(info.NodeName);
                if (info.NodeType == NodeType.Link)
                {
                    builder.Append("(外)");
                }
                info.NodeName = builder.ToString();
            }
            return nodeList;
        }

        public static void OrderNode(IList<NodeInfo> nodeList)
        {
            List<NodeInfo> list = (List<NodeInfo>) nodeList;
            list.Sort();
            int count = list.Count;
            for (int i = 0; i < count; i++)
            {
                NodeInfo nodeInfo = list[i];
                if (i == 0)
                {
                    nodeInfo.PrevId = 0;
                }
                else
                {
                    nodeInfo.PrevId = list[i - 1].NodeId;
                }
                if (i == (count - 1))
                {
                    nodeInfo.NextId = 0;
                }
                else
                {
                    nodeInfo.NextId = list[i + 1].NodeId;
                }
                if (nodeInfo.OrderType == 1)
                {
                    UpdateRootId(nodeInfo.ArrChildId, nodeInfo.RootId);
                }
                Update(nodeInfo);
            }
            PatchNodes();
        }

        public static void PatchNodes()
        {
            IList<NodeInfo> nodesListByParentId = GetNodesListByParentId(0);
            rootId = 0;
            foreach (NodeInfo info in nodesListByParentId)
            {
                ModifyNodeStructure(info);
            }
            RemoveCacheAllNodeInfo();
        }

        public static void RemoveCacheAllNodeInfo()
        {
            SiteCache.RemoveByPattern(@"CK_Content_NodeInfo_NodeId_\S*");
        }

        public static void RemoveCacheByNodeId(int nodeId)
        {
            SiteCache.Remove("CK_Content_NodeInfo_NodeId_" + nodeId.ToString());
        }

        public static int ResetChildNodes(int nodeId)
        {
            NodeInfo cacheNodeById = GetCacheNodeById(nodeId);
            if (cacheNodeById.IsNull || (cacheNodeById.ParentId != 0))
            {
                return 1;
            }
            IList<NodeInfo> nodesListInArrChildId = GetNodesListInArrChildId(cacheNodeById.ArrChildId);
            nodesListInArrChildId.Remove(cacheNodeById);
            int num = 0;
            int count = 0;
            count = nodesListInArrChildId.Count;
            foreach (NodeInfo info2 in nodesListInArrChildId)
            {
                if (num == 0)
                {
                    info2.PrevId = 0;
                }
                else
                {
                    info2.PrevId = nodesListInArrChildId[num - 1].NodeId;
                }
                if (num == (count - 1))
                {
                    info2.NextId = 0;
                }
                else
                {
                    info2.NextId = nodesListInArrChildId[num + 1].NodeId;
                }
                info2.NodeDir = info2.ParentDir.Replace("/", "") + info2.NodeDir;
                info2.RootId = cacheNodeById.RootId;
                num++;
                info2.OrderId = num;
                info2.ParentId = cacheNodeById.NodeId;
                info2.Child = 0;
                info2.ArrChildId = info2.NodeId.ToString();
                info2.ParentPath = "0," + cacheNodeById.NodeId.ToString();
                info2.Depth = cacheNodeById.Depth + 1;
                info2.ParentDir = cacheNodeById.ParentDir + cacheNodeById.NodeDir + info2.NodeDir;
                Update(info2);
            }
            cacheNodeById.Child = count;
            Update(cacheNodeById);
            return 0;
        }

        public static void ResetNodes()
        {
            int num = 0;
            int count = 0;
            IList<NodeInfo> nodesList = GetNodesList();
            count = nodesList.Count;
            foreach (NodeInfo info in nodesList)
            {
                if (num == 0)
                {
                    info.PrevId = 0;
                }
                else
                {
                    info.PrevId = nodesList[num - 1].NodeId;
                }
                if (num == (count - 1))
                {
                    info.NextId = 0;
                }
                else
                {
                    info.NextId = nodesList[num + 1].NodeId;
                }
                info.RootId = num + 1;
                info.OrderId = 0;
                info.ParentId = 0;
                info.Child = 0;
                info.ArrChildId = info.NodeId.ToString();
                info.ParentPath = "0";
                info.Depth = 0;
                info.ParentDir = "/";
                num++;
                Update(info);
            }
            RemoveCacheAllNodeInfo();
        }

        public static string ResolveUploadDir(NodeInfo nodeInfo, string uploadFilePathRule)
        {
            uploadFilePathRule = uploadFilePathRule.Replace("{$NodeDir}", nodeInfo.NodeDir);
            uploadFilePathRule = uploadFilePathRule.Replace("{$ParentDir}", nodeInfo.ParentDir);
            uploadFilePathRule = uploadFilePathRule.Replace("{$NodeIdentifier}", nodeInfo.NodeIdentifier);
            if (nodeInfo.ParentPath.Contains(","))
            {
                string[] strArray = nodeInfo.ParentPath.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                uploadFilePathRule = uploadFilePathRule.Replace("{$RootDir}", GetCacheNodeById(DataConverter.CLng(strArray[1])).NodeDir);
                return uploadFilePathRule;
            }
            uploadFilePathRule = uploadFilePathRule.Replace("{$RootDir}", nodeInfo.NodeDir);
            return uploadFilePathRule;
        }

        public static string ShowNodeNavigation(int nodeId)
        {
            return ShowNodeNavigation(nodeId, 0, "");
        }

        public static string ShowNodeNavigation(int nodeId, string navigation)
        {
            return ShowNodeNavigation(nodeId, 0, navigation);
        }

        private static string ShowNodeNavigation(int nodeId, int type, string navigation)
        {
            if (string.IsNullOrEmpty(navigation))
            {
                navigation = "ContentManage.aspx";
            }
            NodeInfo cacheNodeById = GetCacheNodeById(nodeId);
            if (cacheNodeById.IsNull)
            {
                return string.Empty;
            }
            IList<NodeInfo> nodesListInParentPath = GetNodesListInParentPath(cacheNodeById.ParentPath);
            StringBuilder builder = new StringBuilder();
            if (nodesListInParentPath.Count > 0)
            {
                foreach (NodeInfo info2 in nodesListInParentPath)
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(" >> ");
                    }
                    if (type == 0)
                    {
                        builder.Append(string.Concat(new object[] { "<a href='", navigation, "?NodeID=", info2.NodeId, "&NodeName=", DataSecurity.UrlEncode(info2.NodeName), "'>" }));
                        builder.Append(DataSecurity.HtmlEncode(info2.NodeName));
                        builder.Append("</a>");
                    }
                    else
                    {
                        builder.Append(DataSecurity.HtmlEncode(info2.NodeName));
                    }
                }
            }
            else
            {
                builder.Append("根节点");
            }
            if ((type == 0) || (type == 2))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" >> ");
                }
                builder.Append(DataSecurity.HtmlEncode(cacheNodeById.NodeName));
            }
            return builder.ToString();
        }

        public static string ShowNodesAndRootNavigation(int nodeId)
        {
            return ShowNodeNavigation(nodeId, 2, "");
        }

        public static string ShowParentNodesNavigation(int nodeId)
        {
            return ShowNodeNavigation(nodeId, 1, "");
        }

        private static void UpateNodeStructure(int parentId, int depth, string parentPath, string parentDir)
        {
            if (string.IsNullOrEmpty(parentPath))
            {
                parentPath = parentId.ToString();
            }
            else
            {
                parentPath = parentPath + "," + parentId.ToString();
            }
            IList<NodeInfo> nodesListByParentId = GetNodesListByParentId(parentId);
            NodeInfo cacheNodeById = GetCacheNodeById(parentId);
            cacheNodeById.Child = nodesListByParentId.Count;
            cacheNodeById.OrderId = orderId;
            cacheNodeById.ArrChildId = parentId.ToString();
            if (cacheNodeById.ParentId == 0)
            {
                rootId++;
                cacheNodeById.RootId = rootId;
                cacheNodeById.Depth = 0;
            }
            Update(cacheNodeById);
            int num = 0;
            foreach (NodeInfo info2 in nodesListByParentId)
            {
                orderId++;
                info2.OrderId = orderId;
                info2.Depth = depth;
                info2.ParentPath = parentPath;
                info2.RootId = cacheNodeById.RootId;
                if (info2.NodeType == NodeType.Container)
                {
                    info2.ParentDir = parentDir;
                }
                if (num == 0)
                {
                    info2.PrevId = 0;
                }
                else
                {
                    info2.PrevId = nodesListByParentId[num - 1].NodeId;
                }
                if (num == (nodesListByParentId.Count - 1))
                {
                    info2.NextId = 0;
                }
                else
                {
                    info2.NextId = nodesListByParentId[num + 1].NodeId;
                }
                info2.ArrChildId = info2.NodeId.ToString();
                Update(info2);
                string str = (VirtualPathUtility.AppendTrailingSlash(info2.ParentDir) + info2.NodeDir + "/").Replace("//", "/");
                UpateNodeStructure(info2.NodeId, info2.Depth + 1, info2.ParentPath, str);
                num++;
            }
            UpdateArrChildStructure(nodesListByParentId);
        }

        public static int Update(NodeInfo nodeInfo)
        {
            NodeInfo cacheNodeById = GetCacheNodeById(nodeInfo.NodeId);
            if ((nodeInfo.NodeName != cacheNodeById.NodeName) && ExistsNodeName(nodeInfo.ParentId, nodeInfo.NodeName))
            {
                return 2;
            }
            if ((nodeInfo.NodeIdentifier != cacheNodeById.NodeIdentifier) && ExistsNodeIdentifier(nodeInfo.ParentId, nodeInfo.NodeIdentifier))
            {
                return 3;
            }
            if (dal.Update(nodeInfo))
            {
                RemoveCacheByNodeId(nodeInfo.NodeId);
                return 1;
            }
            return 0;
        }

        public static int Update(NodeInfo nodeInfo, IList<NodesModelTemplateRelationShipInfo> infoList)
        {
            int num = Update(nodeInfo);
            if (num == 1)
            {
                ModelManager.UpdateNodesModelTemplateRelationShip(nodeInfo.NodeId, infoList);
            }
            return num;
        }

        public static bool UpdateArrChildId(int nodeId, string arrChildId)
        {
            RemoveCacheAllNodeInfo();
            return dal.UpdateArrChildId(nodeId, arrChildId);
        }

        private static void UpdateArrChildStructure(IList<NodeInfo> nodeList)
        {
            foreach (NodeInfo info in nodeList)
            {
                foreach (NodeInfo info2 in GetNodesListInArrChildId(info.ParentPath))
                {
                    info2.ArrChildId = info2.ArrChildId + "," + info.NodeId.ToString();
                    UpdateArrChildId(info2.NodeId, info2.ArrChildId);
                }
            }
        }

        public static bool UpdateChild(int parentId)
        {
            RemoveCacheAllNodeInfo();
            return dal.UpdateChild(parentId);
        }

        public static bool UpdateChild(int nodeId, int child)
        {
            RemoveCacheAllNodeInfo();
            return dal.UpdateChild(nodeId, child);
        }

        public static bool UpdateChildPurview(int nodeId, int purviewType)
        {
            bool flag = true;
            NodeInfo cacheNodeById = GetCacheNodeById(nodeId);
            if (!DataValidator.IsValidId(cacheNodeById.ArrChildId))
            {
                return false;
            }
            if ((purviewType == 1) || (purviewType == 2))
            {
                flag = dal.UpdateChildPurview(cacheNodeById.ArrChildId, purviewType);
            }
            RemoveCacheAllNodeInfo();
            return flag;
        }

        public static bool UpdateNeedCreateHtml(string arrNodeId, bool needCreateHtml)
        {
            if (!DataValidator.IsValidId(arrNodeId))
            {
                return false;
            }
            return dal.UpdateNeedCreateHtml(arrNodeId, needCreateHtml);
        }

        private static bool UpdateNextId(int nodeId, int nextId)
        {
            RemoveCacheByNodeId(nodeId);
            return dal.UpdateNextId(nodeId, nextId);
        }

        public static bool UpdateNodePurviewType(int nodeId)
        {
            bool flag = true;
            NodeInfo cacheNodeById = GetCacheNodeById(nodeId);
            int maxPurviewTypeInParentPath = GetMaxPurviewTypeInParentPath(cacheNodeById.NodeId);
            if (cacheNodeById.PurviewType < maxPurviewTypeInParentPath)
            {
                flag = dal.UpdateNodePurviewType(cacheNodeById.NodeId, maxPurviewTypeInParentPath);
            }
            RemoveCacheAllNodeInfo();
            return flag;
        }

        public static bool UpdateOrderId(int nodeId, int orderId)
        {
            RemoveCacheByNodeId(nodeId);
            return dal.UpdateOrderId(nodeId, orderId);
        }

        public static bool UpdateOrderId(int rootId, int orderId, int addNum)
        {
            RemoveCacheAllNodeInfo();
            return dal.UpdateOrderId(rootId, orderId, addNum);
        }

        private static bool UpdateRootId(string nodeId, int rootId)
        {
            return dal.UpdateRootId(nodeId, rootId);
        }

        public static string UploadPathParse(NodeInfo nodeInfo, string fileName)
        {
            string uploadFilePathRule = SiteConfig.SiteOption.UploadFilePathRule;
            return ((ResolveUploadDir(nodeInfo, uploadFilePathRule).Replace("{$FileType}", Path.GetExtension(fileName).ToLower().Replace(".", "")).Replace("{$Year}", DateTime.Now.Year.ToString()).Replace("{$Month}", DateTime.Now.Month.ToString()) + "/").Replace("{$Day}", DateTime.Now.Day.ToString()) + "/").Replace("//", "/");
        }

        public static string WriteMessageByErrorNum(int errorNum)
        {
            string str = "";
            switch (errorNum)
            {
                case 0:
                    return str;

                case 1:
                    return "找不到指定的节点或者已经被删除！";

                case 2:
                    return "要移动节点和目标节点相同，无需移动！";

                case 3:
                    return "目标节点与当前父节点相同，无需移动！";

                case 4:
                    return "不能指定外部节点为所属节点！";

                case 5:
                    return "不能指定该节点的下属节点作为所属节点！";

                case 6:
                    return "目标节点的子节点中已经存在与此节点名称相同的节点，不能移动！";

                case 7:
                    return "目标节点的子节点中是否已经存在与此节点目录相同的节点，不能移动！";
            }
            return "节点移动失败！";
        }

        public static string WriteNodesUniteMessage(int errorType)
        {
            string str = "";
            switch (errorType)
            {
                case 0:
                    return str;

                case 1:
                    return "未指定要合并的节点！";

                case 2:
                    return "未指定目标节点！";

                case 3:
                    return "要合并的节点与目标节点相同，请不要在相同节点内进行操作！";

                case 4:
                    return "目标节点不存在，可能已经被删除！";

                case 5:
                    return "目标节点中含有子节点，不能合并！";

                case 6:
                    return "目标节点是专题、单个页面或外部节点！";

                case 7:
                    return "找不到指定的节点，可能已经被删除！";

                case 8:
                    return "不能将一个节点合并到其下属子节点中！";
            }
            return "节点合并失败！";
        }
    }
}

