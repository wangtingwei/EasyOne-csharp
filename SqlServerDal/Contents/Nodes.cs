namespace EasyOne.SqlServerDal.Contents
{
    using EasyOne.Common;
    using EasyOne.Enumerations;
    using EasyOne.IDal.Contents;
    using EasyOne.Model.Contents;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;

    public class Nodes : INodes
    {
        private static Serialize<NodeSettingInfo> ser = new Serialize<NodeSettingInfo>();

        public bool Add(NodeInfo nodeInfo)
        {
            Parameters cmdParams = GetParameters(nodeInfo);
            return DBHelper.ExecuteProc("PR_Contents_Nodes_Add", cmdParams);
        }

        public bool BatchUpdateField(NodeInfo nodeInfo, string nodesId, Dictionary<string, bool> checkItem)
        {
            StringBuilder builder = new StringBuilder();
            Parameters cmdParams = new Parameters();
            builder.Append("UPDATE PE_Nodes SET ");
            if (checkItem["OpenType"])
            {
                builder.Append("OpenType = @OpenType,");
                cmdParams.AddInParameter("@OpenType", DbType.Int32, nodeInfo.OpenType);
            }
            if (checkItem["PurviewType"])
            {
                builder.Append("PurviewType = @PurviewType,");
                cmdParams.AddInParameter("@PurviewType", DbType.Int32, nodeInfo.PurviewType);
            }
            if (checkItem["WorkFlowId"])
            {
                builder.Append("WorkFlowID = @WorkFlowID,");
                cmdParams.AddInParameter("@WorkFlowID", DbType.Int32, nodeInfo.WorkFlowId);
            }
            if (checkItem["HitsOfHot"])
            {
                builder.Append("HitsOfHot = @HitsOfHot,");
                cmdParams.AddInParameter("@HitsOfHot", DbType.Int32, nodeInfo.HitsOfHot);
            }
            if (checkItem["FileCdefaultListTmeplate"])
            {
                builder.Append("DefaultTemplateFile = @DefaultTemplateFile,");
                cmdParams.AddInParameter("@DefaultTemplateFile", DbType.String, nodeInfo.DefaultTemplateFile);
            }
            if (checkItem["FileContainChildTemplate"])
            {
                builder.Append("ContainChildTemplateFile = @ContainChildTemplateFile,");
                cmdParams.AddInParameter("@ContainChildTemplateFile", DbType.String, nodeInfo.ContainChildTemplateFile);
            }
            if (checkItem["ShowOnMenu"])
            {
                builder.Append("ShowOnMenu = @ShowOnMenu,");
                cmdParams.AddInParameter("ShowOnMenu", DbType.Boolean, nodeInfo.ShowOnMenu);
            }
            if (checkItem["ShowOnPath"])
            {
                builder.Append("ShowOnPath = @ShowOnPath,");
                cmdParams.AddInParameter("@ShowOnPath", DbType.Boolean, nodeInfo.ShowOnPath);
            }
            if (checkItem["ShowOnMap"])
            {
                builder.Append("ShowOnMap = @ShowOnMap,");
                cmdParams.AddInParameter("@ShowOnMap", DbType.Boolean, nodeInfo.ShowOnMap);
            }
            if (checkItem["ShowOnListIndex"])
            {
                builder.Append("ShowOnList_Index = @ShowOnListIndex,");
                cmdParams.AddInParameter("@ShowOnListIndex", DbType.Boolean, nodeInfo.ShowOnListIndex);
            }
            if (checkItem["ShowOnListParent"])
            {
                builder.Append("ShowOnList_Parent = @ShowOnListParent,");
                cmdParams.AddInParameter("@ShowOnListParent", DbType.Boolean, nodeInfo.ShowOnListParent);
            }
            if (checkItem["ItemPageSize"])
            {
                builder.Append("ItemPageSize = @ItemPageSize,");
                cmdParams.AddInParameter("@ItemPageSize", DbType.Int32, nodeInfo.ItemPageSize);
            }
            if (checkItem["ItemOpenType"])
            {
                builder.Append("ItemOpenType = @ItemOpenType,");
                cmdParams.AddInParameter("@ItemOpenType", DbType.Int32, nodeInfo.ItemOpenType);
            }
            if (checkItem["ItemListOrderType"])
            {
                builder.Append("ItemListOrderType = @ItemListOrderType,");
                cmdParams.AddInParameter("@ItemListOrderType", DbType.Int32, nodeInfo.ItemListOrderType);
            }
            if (checkItem["ListPageCreateHtmlType"])
            {
                builder.Append("IsCreateListPage = @IsCreateListPage,");
                cmdParams.AddInParameter("@IsCreateListPage", DbType.Boolean, nodeInfo.IsCreateListPage);
            }
            if (checkItem["AutoCreateHtmlType"])
            {
                builder.Append("AutoCreateHtmlType = @AutoCreateHtmlType,");
                cmdParams.AddInParameter("@AutoCreateHtmlType", DbType.Int32, nodeInfo.AutoCreateHtmlType);
            }
            if (checkItem["Relation"])
            {
                builder.Append("RelateSpecial = @RelateSpecial,");
                cmdParams.AddInParameter("@RelateSpecial", DbType.String, nodeInfo.RelateSpecial);
                builder.Append("RelateNode = @RelateNode,");
                cmdParams.AddInParameter("@RelateNode", DbType.String, nodeInfo.RelateNode);
            }
            if (checkItem["ListPageHtmlDirType"])
            {
                builder.Append("ListPageSavePathType = @ListPageSavePathType,");
                cmdParams.AddInParameter("@ListPageSavePathType", DbType.Int32, nodeInfo.ListPageSavePathType);
            }
            if (checkItem["PagePostfix"])
            {
                builder.Append("ListPagePostfix = @ListPagePostfix,");
                cmdParams.AddInParameter("@ListPagePostfix", DbType.String, nodeInfo.ListPagePostfix);
            }
            if (checkItem["ContentPageCreateHtmlType"])
            {
                builder.Append("IsCreateContentPage = @IsCreateContentPage,");
                cmdParams.AddInParameter("@IsCreateContentPage", DbType.Boolean, nodeInfo.IsCreateContentPage);
            }
            if (checkItem["ContentHtmlDir"])
            {
                builder.Append("ContentPageHtmlRule = @ContentPageHtmlRule,");
                cmdParams.AddInParameter("@ContentPageHtmlRule", DbType.String, nodeInfo.ContentPageHtmlRule);
            }
            if (builder.Length <= 20)
            {
                return true;
            }
            builder.Remove(builder.Length - 1, 1);
            builder.Append(" WHERE NodeID IN ( ");
            builder.Append(DBHelper.ToValidId(nodesId));
            builder.Append(" )");
            return DBHelper.ExecuteSql(builder.ToString(), cmdParams);
        }

        public bool DeleteInArrChild(string arrChildId)
        {
            return DBHelper.ExecuteSql("DELETE FROM PE_Nodes WHERE NodeID IN ( " + DBHelper.ToValidId(arrChildId) + " )");
        }

        private static NodeSettingInfo DeserializeSettings(string xmlString)
        {
            return ser.DeserializeField(xmlString);
        }

        public bool ExistNodeDir(int nodeId, string nodeDir)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@NodeId", DbType.Int32, nodeId);
            cmdParams.AddInParameter("@NodeDir", DbType.String, nodeDir);
            return DBHelper.ExistsSql("SELECT COUNT(*) FROM PE_Nodes WHERE ParentID = @NodeId AND NodeDir = @NodeDir", cmdParams);
        }

        public bool ExistsNodeIdentifier(int parentId, string nodeIdentifier)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ParentID", DbType.Int32, parentId);
            cmdParams.AddInParameter("@NodeIdentifier", DbType.String, nodeIdentifier);
            return DBHelper.ExistsSql("SELECT COUNT(*) FROM PE_Nodes WHERE ParentID = @ParentID AND NodeIdentifier = @NodeIdentifier", cmdParams);
        }

        public bool ExistsNodeName(int parentId, string nodeName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ParentID", DbType.Int32, parentId);
            cmdParams.AddInParameter("@NodeName", DbType.String, nodeName);
            return DBHelper.ExistsSql("SELECT COUNT(*) FROM PE_Nodes WHERE ParentID = @ParentID AND NodeName = @NodeName", cmdParams);
        }

        public bool ExistsTargetNodeIdInArrChildId(int targetNodeId, string arrChildId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@TargetParentID", DbType.Int32, targetNodeId);
            return DBHelper.ExistsSql("SELECT COUNT(*) FROM PE_Nodes WHERE NodeID = @TargetParentID AND NodeID IN (" + DBHelper.ToValidId(arrChildId) + ")", cmdParams);
        }

        public IList<NodeInfo> GetAnonymousNodeId(int groupId, OperateCode operateCode)
        {
            IList<NodeInfo> list = new List<NodeInfo>();
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@GroupId", DbType.Int32, groupId);
            cmdParams.AddInParameter("@OperateCode", DbType.Int32, operateCode);
            string strCommand = "SELECT  * FROM PE_Nodes WHERE NodeId IN (SELECT DISTINCT NodeId FROM PE_GroupNodePermissions WHERE GroupId = @GroupId AND OperateCode = @OperateCode) ORDER BY NodeId";
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, cmdParams))
            {
                while (reader.Read())
                {
                    list.Add(NodesFromrdr(reader));
                }
            }
            return list;
        }

        public int GetCountModelByNodeId(int nodeId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@NodeID", DbType.Int32, nodeId);
            return DataConverter.CLng(DBHelper.ExecuteScalarSql("SELECT COUNT(*) FROM PE_CommonModel WHERE NodeID = @NodeID", cmdParams));
        }

        public int GetCountNodesBySameNodeDir(int parentId, int nodeId, string nodeDir)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ParentID", DbType.Int32, parentId);
            cmdParams.AddInParameter("@NodeID", DbType.Int32, nodeId);
            cmdParams.AddInParameter("@NodeDir", DbType.String, nodeDir);
            string strSql = "SELECT COUNT(*) FROM PE_Nodes WHERE ParentID = @ParentID AND NodeID<>@NodeID AND NodeDir = @NodeDir";
            return DataConverter.CLng(DBHelper.ExecuteScalarSql(strSql, cmdParams));
        }

        public bool GetDefaultTemplate(int nodeId, int templateId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@NodeID", DbType.Int32, nodeId);
            cmdParams.AddInParameter("@TemplateID", DbType.Int32, templateId);
            string strCommand = "SELECT IsDefault FROM PE_Nodes_Template WHERE NodeID = @NodeID AND TemplateID = @TemplateID";
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, cmdParams))
            {
                return (reader.Read() && reader.GetBoolean("IsDefault"));
            }
        }

        public int GetMaxNodeId()
        {
            return DBHelper.GetMaxId("PE_Nodes", "NodeID");
        }

        public int GetMaxPurviewTypeInParentPath(string parentPath)
        {
            return DataConverter.CLng(DBHelper.ExecuteScalarSql("SELECT MAX(PurviewType) FROM PE_Nodes WHERE NodeID IN (" + DBHelper.ToValidId(parentPath) + ")"));
        }

        public int GetMaxRootId()
        {
            return DBHelper.GetMaxId("PE_Nodes", "RootID");
        }

        public int GetNextIdByDepth(int depth, string parentPath)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@Depth", DbType.Int32, depth);
            return DataConverter.CLng(DBHelper.ExecuteScalarSql("SELECT NextID FROM PE_Nodes WHERE Depth = @Depth AND NodeID IN (" + DBHelper.ToValidId(parentPath) + ")", cmdParams));
        }

        public NodeInfo GetNodeById(int nodeId)
        {
            NodeInfo info;
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@NodeID", DbType.Int32, nodeId);
            using (NullableDataReader reader = DBHelper.ExecuteReaderProc("PR_Contents_Nodes_GetById", cmdParams))
            {
                if (reader.Read())
                {
                    return NodesFromrdr(reader);
                }
                info = new NodeInfo(true);
                NodeSettingInfo info2 = new NodeSettingInfo(true);
                info.Settings = info2;
            }
            return info;
        }

        public DataTable GetNodeNameByModelId(int modelId, NodeType nodeType)
        {
            DataTable table = new DataTable();
            table.Columns.Add("NodeId");
            table.Columns.Add("NodeName");
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ModelId", DbType.Int32, modelId);
            cmdParams.AddInParameter("@NodeType", DbType.Int32, nodeType);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT N.NodeId, N.NodeName FROM PE_Nodes_Model_Template R INNER JOIN PE_Nodes N ON R.NodeId = N.NodeId WHERE R.ModelId = @ModelId AND N.NodeType = @NodeType", cmdParams))
            {
                while (reader.Read())
                {
                    DataRow row = table.NewRow();
                    row["NodeId"] = reader.GetInt32("NodeId");
                    row["NodeName"] = reader.GetString("NodeName");
                    table.Rows.Add(row);
                }
            }
            return table;
        }

        public IList<NodeInfo> GetNodesList(NodeType nodeType)
        {
            IList<NodeInfo> list = new List<NodeInfo>();
            Parameters cmdParams = null;
            string str = string.Empty;
            if (nodeType != NodeType.None)
            {
                cmdParams = new Parameters();
                cmdParams.AddInParameter("@NodeType", DbType.Int32, nodeType);
                str = "AND NodeType = @NodeType";
            }
            string format = "SELECT * FROM PE_Nodes WHERE 1 = 1 {0} ORDER BY RootID, OrderID ASC";
            format = string.Format(format, str);
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, format, cmdParams))
            {
                while (reader.Read())
                {
                    list.Add(NodesFromrdr(reader));
                }
            }
            return list;
        }

        public IList<NodeInfo> GetNodesList(string nodesId)
        {
            List<NodeInfo> list = new List<NodeInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_Nodes WHERE NodeID IN (" + DBHelper.ToValidId(nodesId) + ")"))
            {
                while (reader.Read())
                {
                    list.Add(NodesFromrdr(reader));
                }
            }
            return list;
        }

        public IList<NodeInfo> GetNodesListByParentId(int parentId)
        {
            IList<NodeInfo> list = new List<NodeInfo>();
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ParentID", DbType.Int32, parentId);
            string strCommand = "SELECT * FROM PE_Nodes WHERE ParentID = @ParentID ORDER BY RootID, OrderID ASC";
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, cmdParams))
            {
                while (reader.Read())
                {
                    list.Add(NodesFromrdr(reader));
                }
            }
            return list;
        }

        public IList<NodeInfo> GetNodesListByRootId(int rootId)
        {
            IList<NodeInfo> list = new List<NodeInfo>();
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@RootID", DbType.Int32, rootId);
            string strCommand = "SELECT * FROM PE_Nodes WHERE RootID = @RootID AND ParentID > 0 ORDER BY OrderID";
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, cmdParams))
            {
                while (reader.Read())
                {
                    list.Add(NodesFromrdr(reader));
                }
            }
            return list;
        }

        public IList<NodeInfo> GetNodesListInArrChildId(string arrChildId)
        {
            IList<NodeInfo> list = new List<NodeInfo>();
            string strCommand = "SELECT * FROM PE_Nodes WHERE NodeID IN (" + DBHelper.ToValidId(arrChildId) + ")";
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, null))
            {
                while (reader.Read())
                {
                    list.Add(NodesFromrdr(reader));
                }
            }
            return list;
        }

        public IList<NodeInfo> GetNodesListInParentPath(string parentPath)
        {
            IList<NodeInfo> list = new List<NodeInfo>();
            string strCommand = "SELECT NodeID, arrChildID, Child, NodeName FROM PE_Nodes WHERE NodeID IN (" + DBHelper.ToValidId(parentPath) + ") ORDER BY Depth ASC";
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, null))
            {
                while (reader.Read())
                {
                    NodeInfo item = new NodeInfo();
                    item.NodeId = reader.GetInt32("NodeID");
                    item.ArrChildId = reader.GetString("arrChildID");
                    item.Child = reader.GetInt32("Child");
                    item.NodeName = reader.GetString("NodeName");
                    list.Add(item);
                }
            }
            return list;
        }

        public int GetNodeWorkFlowId(int nodeId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@NodeID", DbType.Int32, nodeId);
            object obj2 = DBHelper.ExecuteScalarSql("SELECT WorkFlowID FROM PE_Nodes WHERE NodeID = @NodeID", cmdParams);
            if (obj2 == null)
            {
                return 0;
            }
            return (int) obj2;
        }

        private static Parameters GetParameters(NodeInfo nodeInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@NodeId", DbType.Int32, nodeInfo.NodeId);
            parameters.AddInParameter("@ArrChildId", DbType.String, nodeInfo.ArrChildId);
            parameters.AddInParameter("@Child", DbType.Int32, nodeInfo.Child);
            parameters.AddInParameter("@CommentCount", DbType.Int32, nodeInfo.CommentCount);
            parameters.AddInParameter("@Creater", DbType.String, nodeInfo.Creater);
            parameters.AddInParameter("@CustomContent", DbType.String, nodeInfo.CustomContent);
            parameters.AddInParameter("@Depth", DbType.Int32, nodeInfo.Depth);
            parameters.AddInParameter("@Description", DbType.String, nodeInfo.Description);
            parameters.AddInParameter("@HitsOfHot", DbType.Int32, nodeInfo.HitsOfHot);
            parameters.AddInParameter("@InheritPurviewFromParent", DbType.Int32, nodeInfo.InheritPurviewFromParent);
            parameters.AddInParameter("@ItemAspxFileName", DbType.String, nodeInfo.ItemAspxFileName);
            parameters.AddInParameter("@ItemChecked", DbType.Int32, nodeInfo.ItemChecked);
            parameters.AddInParameter("@ItemCount", DbType.Int32, nodeInfo.ItemCount);
            parameters.AddInParameter("@IsCreateContentPage", DbType.Boolean, nodeInfo.IsCreateContentPage);
            parameters.AddInParameter("@IsCreateListPage", DbType.Boolean, nodeInfo.IsCreateListPage);
            parameters.AddInParameter("@AutoCreateHtmlType", DbType.Int32, nodeInfo.AutoCreateHtmlType);
            parameters.AddInParameter("@ListPageHtmlRule", DbType.String, nodeInfo.ListPageHtmlRule);
            parameters.AddInParameter("@ListPageSavePathType", DbType.Int32, nodeInfo.ListPageSavePathType);
            parameters.AddInParameter("@ListPagePostfix", DbType.String, nodeInfo.ListPagePostfix);
            parameters.AddInParameter("@ContentPageHtmlRule", DbType.String, nodeInfo.ContentPageHtmlRule);
            parameters.AddInParameter("@RelateNode", DbType.String, nodeInfo.RelateNode);
            parameters.AddInParameter("@RelateSpecial", DbType.String, nodeInfo.RelateSpecial);
            parameters.AddInParameter("@ItemListOrderType", DbType.Int32, nodeInfo.ItemListOrderType);
            parameters.AddInParameter("@ItemOpenType", DbType.Int32, nodeInfo.ItemOpenType);
            parameters.AddInParameter("@ItemPageSize", DbType.Int32, nodeInfo.ItemPageSize);
            parameters.AddInParameter("@DefaultTemplateFile", DbType.String, nodeInfo.DefaultTemplateFile);
            parameters.AddInParameter("@ContainChildTemplateFile", DbType.String, nodeInfo.ContainChildTemplateFile);
            parameters.AddInParameter("@MetaDescription", DbType.String, nodeInfo.MetaDescription);
            parameters.AddInParameter("@MetaKeywords", DbType.String, nodeInfo.MetaKeywords);
            parameters.AddInParameter("@NextId", DbType.Int32, nodeInfo.NextId);
            parameters.AddInParameter("@NodeDir", DbType.String, nodeInfo.NodeDir);
            parameters.AddInParameter("@NodeIdentifier", DbType.String, nodeInfo.NodeIdentifier);
            parameters.AddInParameter("@NodeName", DbType.String, nodeInfo.NodeName);
            parameters.AddInParameter("@NodePicUrl", DbType.String, nodeInfo.NodePicUrl);
            parameters.AddInParameter("@NodeType", DbType.Int32, nodeInfo.NodeType);
            parameters.AddInParameter("@OpenType", DbType.Int32, nodeInfo.OpenType);
            parameters.AddInParameter("@OrderId", DbType.Int32, nodeInfo.OrderId);
            parameters.AddInParameter("@ParentDir", DbType.String, nodeInfo.ParentDir);
            parameters.AddInParameter("@ParentId", DbType.Int32, nodeInfo.ParentId);
            parameters.AddInParameter("@ParentPath", DbType.String, nodeInfo.ParentPath);
            parameters.AddInParameter("@PrevId", DbType.Int32, nodeInfo.PrevId);
            parameters.AddInParameter("@PurviewType", DbType.Int32, nodeInfo.PurviewType);
            parameters.AddInParameter("@RootId", DbType.Int32, nodeInfo.RootId);
            parameters.AddInParameter("@ShowOnListIndex", DbType.Boolean, nodeInfo.ShowOnListIndex);
            parameters.AddInParameter("@ShowOnListParent", DbType.Boolean, nodeInfo.ShowOnListParent);
            parameters.AddInParameter("@ShowOnMap", DbType.Boolean, nodeInfo.ShowOnMap);
            parameters.AddInParameter("@ShowOnMenu", DbType.Boolean, nodeInfo.ShowOnMenu);
            parameters.AddInParameter("@ShowOnPath", DbType.Boolean, nodeInfo.ShowOnPath);
            parameters.AddInParameter("@Tips", DbType.String, nodeInfo.Tips);
            parameters.AddInParameter("@WorkFlowId", DbType.Int32, nodeInfo.WorkFlowId);
            parameters.AddInParameter("@LinkUrl", DbType.String, nodeInfo.LinkUrl);
            parameters.AddInParameter("@Settings", DbType.String, SerializeSettings(nodeInfo.Settings));
            parameters.AddInParameter("@NeedCreateHtml", DbType.Boolean, nodeInfo.NeedCreateHtml);
            return parameters;
        }

        public NodeInfo GetParentNodeByNodeId(int parentId, int nodeId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ParentID", DbType.Int32, parentId);
            cmdParams.AddInParameter("@NodeID", DbType.Int32, nodeId);
            string strCommand = "SELECT NodeID, RootID, NodeDir FROM PE_Nodes WHERE ParentID = @ParentID AND NodeID = @NodeID";
            NodeInfo info = new NodeInfo();
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, cmdParams))
            {
                if (reader.Read())
                {
                    info.NodeId = reader.GetInt32("NodeID");
                    info.RootId = reader.GetInt32("RootID");
                    info.NodeDir = reader.GetString("NodeDir");
                    return info;
                }
                return new NodeInfo(true);
            }
        }

        public Dictionary<int, string> GetParentPathArrChildId(string parentPath)
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            string strCommand = "SELECT NodeID, arrChildID FROM PE_Nodes WHERE NodeID IN ( " + DBHelper.ToValidId(parentPath) + " )";
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, null))
            {
                while (reader.Read())
                {
                    dictionary.Add(reader.GetInt32("NodeID"), reader.GetString("ArrChildID"));
                }
            }
            return dictionary;
        }

        public int GetPrevId(int parentId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ParentID", DbType.Int32, parentId);
            return DataConverter.CLng(DBHelper.ExecuteScalarSql("SELECT TOP 1 NodeID FROM PE_Nodes WHERE ParentID = @ParentID ORDER BY OrderID DESC", cmdParams));
        }

        public int GetPrevOrderId(string arrChildId)
        {
            return DataConverter.CLng(DBHelper.ExecuteScalarSql("SELECT MAX(OrderID) FROM PE_Nodes WHERE NodeID IN ( " + DBHelper.ToValidId(arrChildId) + " )"));
        }

        public int GetRootPrevId(int maxRootId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@RootID", DbType.Int32, maxRootId);
            return DataConverter.CLng(DBHelper.ExecuteScalarSql("SELECT NodeID FROM PE_Nodes WHERE RootID = @RootID AND Depth = 0", cmdParams));
        }

        public IList<NodeInfo> GetShopNodeList()
        {
            string strSql = "SELECT * FROM PE_Nodes WHERE Nodetype = @NodeType AND NodeID IN (SELECT NodeID FROM PE_Nodes_Model_Template WHERE modelID IN (select ModelID FROM PE_Model WHERE IsEshop = 1)) ORDER BY RootID, OrderID ASC";
            IList<NodeInfo> list = new List<NodeInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, new Parameters("@NodeType", DbType.Int32, NodeType.Container)))
            {
                while (reader.Read())
                {
                    list.Add(NodesFromrdr(reader));
                }
            }
            return list;
        }

        public int GetTotalOfNodes()
        {
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT COUNT(*) FROM PE_Nodes"))
            {
                if (reader.Read())
                {
                    return reader.GetInt32(0);
                }
                return 0;
            }
        }

        private static NodeInfo NodesFromrdr(NullableDataReader rdr)
        {
            NodeInfo info = new NodeInfo();
            info.NodeId = rdr.GetInt32("NodeID");
            info.NodeIdentifier = rdr.GetString("NodeIdentifier");
            info.NodeType = (NodeType) rdr.GetInt32("NodeType");
            info.ParentId = rdr.GetInt32("ParentID");
            info.ParentPath = rdr.GetString("ParentPath");
            info.Depth = rdr.GetInt32("Depth");
            info.RootId = rdr.GetInt32("RootID");
            info.Child = rdr.GetInt32("Child");
            info.ArrChildId = rdr.GetString("ArrChildID");
            info.PrevId = rdr.GetInt32("PrevID");
            info.NextId = rdr.GetInt32("NextID");
            info.OrderId = rdr.GetInt32("OrderID");
            info.NodeDir = rdr.GetString("NodeDir");
            info.ParentDir = rdr.GetString("ParentDir");
            info.NodeName = rdr.GetString("NodeName");
            info.Tips = rdr.GetString("Tips");
            info.Description = rdr.GetString("Description");
            info.NodePicUrl = rdr.GetString("NodePicUrl");
            info.MetaKeywords = rdr.GetString("Meta_Keywords");
            info.MetaDescription = rdr.GetString("Meta_Description");
            info.ShowOnMenu = rdr.GetBoolean("ShowOnMenu");
            info.ShowOnPath = rdr.GetBoolean("ShowOnPath");
            info.ShowOnMap = rdr.GetBoolean("ShowOnMap");
            info.ShowOnListIndex = rdr.GetBoolean("ShowOnList_Index");
            info.ShowOnListParent = rdr.GetBoolean("ShowOnList_Parent");
            info.PurviewType = rdr.GetInt32("PurviewType");
            info.Creater = rdr.GetString("Creater");
            info.InheritPurviewFromParent = rdr.GetInt32("InheritPurviewFromParent");
            info.WorkFlowId = rdr.GetInt32("WorkFlowID");
            info.HitsOfHot = rdr.GetInt32("HitsOfHot");
            info.OpenType = rdr.GetInt32("OpenType");
            info.ItemCount = rdr.GetInt32("ItemCount");
            info.ItemChecked = rdr.GetInt32("ItemChecked");
            info.CommentCount = rdr.GetInt32("CommentCount");
            info.CustomContent = rdr.GetString("Custom_Content");
            info.IsCreateContentPage = rdr.GetBoolean("IsCreateContentPage");
            info.IsCreateListPage = rdr.GetBoolean("IsCreateListPage");
            info.AutoCreateHtmlType = (AutoCreateHtmlType) rdr.GetInt32("AutoCreateHtmlType");
            info.ContentPageHtmlRule = rdr.GetString("ContentPageHtmlRule");
            info.ListPageHtmlRule = rdr.GetString("ListPageHtmlRule");
            info.ListPageSavePathType = (ListPagePathType) rdr.GetInt32("ListPageSavePathType");
            info.ListPagePostfix = rdr.GetString("ListPagePostFix");
            info.RelateNode = rdr.GetString("RelateNode");
            info.RelateSpecial = rdr.GetString("RelateSpecial");
            info.ItemAspxFileName = rdr.GetString("ItemAspxFileName");
            info.DefaultTemplateFile = rdr.GetString("DefaultTemplateFile");
            info.ContainChildTemplateFile = rdr.GetString("ContainChildTemplateFile");
            info.ItemOpenType = rdr.GetInt32("ItemOpenType");
            info.ItemListOrderType = rdr.GetInt32("ItemListOrderType");
            info.ItemPageSize = rdr.GetInt32("ItemPageSize");
            info.LinkUrl = rdr.GetString("LinkUrl");
            info.Settings = DeserializeSettings(rdr.GetString("Settings"));
            info.NeedCreateHtml = rdr.GetBoolean("NeedCreateHtml");
            return info;
        }

        public void ReplaceTemplateDir(string oldDir, string newDir)
        {
            oldDir = oldDir.Replace("'", "''");
            newDir = newDir.Replace("'", "''");
            string str = "'" + oldDir + "%'";
            DBHelper.ExecuteSql("UPDATE PE_Nodes SET DefaultTemplateFile = replace(cast(DefaultTemplateFile AS nvarchar(4000)), '" + oldDir + "', '" + newDir + "') WHERE DefaultTemplateFile LIKE " + str);
            DBHelper.ExecuteSql("UPDATE PE_Nodes SET ContainChildTemplateFile = replace(cast(ContainChildTemplateFile AS nvarchar(4000)), '" + oldDir + "', '" + newDir + "') WHERE ContainChildTemplateFile LIKE " + str);
            DBHelper.ExecuteSql("UPDATE PE_Nodes_Model_Template SET DefaultTemplateFile = replace(cast(DefaultTemplateFile AS nvarchar(4000)), '" + oldDir + "', '" + newDir + "') WHERE DefaultTemplateFile LIKE " + str);
            DBHelper.ExecuteSql("UPDATE PE_Model SET DefaultTemplateFile = replace(cast(DefaultTemplateFile AS nvarchar(4000)), '" + oldDir + "', '" + newDir + "') WHERE DefaultTemplateFile LIKE " + str);
            DBHelper.ExecuteSql("UPDATE PE_Model SET PrintTemplate = replace(cast(PrintTemplate AS nvarchar(4000)), '" + oldDir + "', '" + newDir + "') WHERE PrintTemplate LIKE " + str);
        }

        public void ReplaceTemplateFileName(string replaceFormer, string replaceAfter)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ReplaceFormer", DbType.String, replaceFormer);
            cmdParams.AddInParameter("@ReplaceAfter", DbType.String, replaceAfter);
            DBHelper.ExecuteSql("UPDATE PE_Nodes SET DefaultTemplateFile = @ReplaceAfter WHERE DefaultTemplateFile = @ReplaceFormer", cmdParams);
            DBHelper.ExecuteSql("UPDATE PE_Nodes SET ContainChildTemplateFile = @ReplaceAfter WHERE ContainChildTemplateFile = @ReplaceFormer", cmdParams);
            DBHelper.ExecuteSql("UPDATE PE_Nodes_Model_Template SET DefaultTemplateFile = @ReplaceAfter WHERE DefaultTemplateFile = @ReplaceFormer", cmdParams);
            DBHelper.ExecuteSql("UPDATE PE_Model SET DefaultTemplateFile = @ReplaceAfter WHERE DefaultTemplateFile = @ReplaceFormer", cmdParams);
            DBHelper.ExecuteSql("UPDATE PE_Model SET PrintTemplate = @ReplaceAfter WHERE PrintTemplate = @ReplaceFormer", cmdParams);
        }

        private static string SerializeSettings(NodeSettingInfo nodeSettingInfo)
        {
            return ser.SerializeField(nodeSettingInfo);
        }

        public bool Update(NodeInfo nodeInfo)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@NodeID", DbType.Int32, nodeInfo.NodeId);
            cmdParams.AddInParameter("@NodeIdentifier", DbType.String, nodeInfo.NodeIdentifier);
            cmdParams.AddInParameter("@ParentId", DbType.Int32, nodeInfo.ParentId);
            cmdParams.AddInParameter("@ParentPath", DbType.String, nodeInfo.ParentPath);
            cmdParams.AddInParameter("@Depth", DbType.Int32, nodeInfo.Depth);
            cmdParams.AddInParameter("@RootId", DbType.Int32, nodeInfo.RootId);
            cmdParams.AddInParameter("@Child", DbType.Int32, nodeInfo.Child);
            cmdParams.AddInParameter("@ArrChildId", DbType.String, nodeInfo.ArrChildId);
            cmdParams.AddInParameter("@PrevId", DbType.Int32, nodeInfo.PrevId);
            cmdParams.AddInParameter("@NextId", DbType.Int32, nodeInfo.NextId);
            cmdParams.AddInParameter("@OrderId", DbType.Int32, nodeInfo.OrderId);
            cmdParams.AddInParameter("@NodeDir", DbType.String, nodeInfo.NodeDir);
            cmdParams.AddInParameter("@ParentDir", DbType.String, nodeInfo.ParentDir);
            cmdParams.AddInParameter("@NodeName", DbType.String, nodeInfo.NodeName);
            cmdParams.AddInParameter("@NodePicUrl", DbType.String, nodeInfo.NodePicUrl);
            cmdParams.AddInParameter("@MetaDescription", DbType.String, nodeInfo.MetaDescription);
            cmdParams.AddInParameter("@MetaKeywords", DbType.String, nodeInfo.MetaKeywords);
            cmdParams.AddInParameter("@OpenType", DbType.Int32, nodeInfo.OpenType);
            cmdParams.AddInParameter("@PurviewType", DbType.Int32, nodeInfo.PurviewType);
            cmdParams.AddInParameter("@CommentCount", DbType.Int32, nodeInfo.CommentCount);
            cmdParams.AddInParameter("@Creater", DbType.String, nodeInfo.Creater);
            cmdParams.AddInParameter("@CustomContent", DbType.String, nodeInfo.CustomContent);
            cmdParams.AddInParameter("@Description", DbType.String, nodeInfo.Description);
            cmdParams.AddInParameter("@HitsOfHot", DbType.Int32, nodeInfo.HitsOfHot);
            cmdParams.AddInParameter("@InheritPurviewFromParent", DbType.Int32, nodeInfo.InheritPurviewFromParent);
            cmdParams.AddInParameter("@ItemAspxFileName", DbType.String, nodeInfo.ItemAspxFileName);
            cmdParams.AddInParameter("@ItemChecked", DbType.Int32, nodeInfo.ItemChecked);
            cmdParams.AddInParameter("@ItemCount", DbType.Int32, nodeInfo.ItemCount);
            cmdParams.AddInParameter("@IsCreateContentPage", DbType.Boolean, nodeInfo.IsCreateContentPage);
            cmdParams.AddInParameter("@IsCreateListPage", DbType.Boolean, nodeInfo.IsCreateListPage);
            cmdParams.AddInParameter("@ListPageHtmlRule", DbType.String, nodeInfo.ListPageHtmlRule);
            cmdParams.AddInParameter("@ListPageSavePathType", DbType.Int32, nodeInfo.ListPageSavePathType);
            cmdParams.AddInParameter("@ListPagePostfix", DbType.String, nodeInfo.ListPagePostfix);
            cmdParams.AddInParameter("@ContentPageHtmlRule", DbType.String, nodeInfo.ContentPageHtmlRule);
            cmdParams.AddInParameter("@RelateNode", DbType.String, nodeInfo.RelateNode);
            cmdParams.AddInParameter("@RelateSpecial", DbType.String, nodeInfo.RelateSpecial);
            cmdParams.AddInParameter("@AutoCreateHtmlType", DbType.Int32, nodeInfo.AutoCreateHtmlType);
            cmdParams.AddInParameter("@ItemListOrderType", DbType.Int32, nodeInfo.ItemListOrderType);
            cmdParams.AddInParameter("@ItemOpenType", DbType.Int32, nodeInfo.ItemOpenType);
            cmdParams.AddInParameter("@ItemPageSize", DbType.Int32, nodeInfo.ItemPageSize);
            cmdParams.AddInParameter("@DefaultTemplateFile", DbType.String, nodeInfo.DefaultTemplateFile);
            cmdParams.AddInParameter("@ContainChildTemplateFile", DbType.String, nodeInfo.ContainChildTemplateFile);
            cmdParams.AddInParameter("@ShowOnListIndex", DbType.Boolean, nodeInfo.ShowOnListIndex);
            cmdParams.AddInParameter("@ShowOnListParent", DbType.Boolean, nodeInfo.ShowOnListParent);
            cmdParams.AddInParameter("@ShowOnMap", DbType.Boolean, nodeInfo.ShowOnMap);
            cmdParams.AddInParameter("@ShowOnMenu", DbType.Boolean, nodeInfo.ShowOnMenu);
            cmdParams.AddInParameter("@ShowOnPath", DbType.Boolean, nodeInfo.ShowOnPath);
            cmdParams.AddInParameter("@Tips", DbType.String, nodeInfo.Tips);
            cmdParams.AddInParameter("@WorkFlowId", DbType.Int32, nodeInfo.WorkFlowId);
            cmdParams.AddInParameter("@LinkUrl", DbType.String, nodeInfo.LinkUrl);
            cmdParams.AddInParameter("@Settings", DbType.String, SerializeSettings(nodeInfo.Settings));
            cmdParams.AddInParameter("@NeedCreateHtml", DbType.Boolean, nodeInfo.NeedCreateHtml);
            return DBHelper.ExecuteProc("PR_Contents_Nodes_Update", cmdParams);
        }

        public bool UpdateArrChildId(int nodeId, string arrChildId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@NodeID", DbType.Int32, nodeId);
            cmdParams.AddInParameter("@ArrChildID", DbType.String, arrChildId);
            return DBHelper.ExecuteSql("UPDATE PE_Nodes SET ArrChildID = @ArrChildID WHERE NodeID = @NodeID", cmdParams);
        }

        public bool UpdateChild(int parentId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ParentID", DbType.Int32, parentId);
            return DBHelper.ExecuteSql("UPDATE PE_Nodes SET Child = Child + 1 WHERE NodeID = @ParentID", cmdParams);
        }

        public bool UpdateChild(int nodeId, int child)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@NodeID", DbType.Int32, nodeId);
            cmdParams.AddInParameter("@Child", DbType.Int32, child);
            return DBHelper.ExecuteSql("UPDATE PE_Nodes SET Child = @Child WHERE NodeID = @NodeID", cmdParams);
        }

        public bool UpdateChildPurview(string arrChildId, int purviewType)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ArrChildID", DbType.String, arrChildId);
            cmdParams.AddInParameter("@PurviewType", DbType.Int32, purviewType);
            return DBHelper.ExecuteSql("UPDATE PE_Nodes SET PurviewType = @PurviewType WHERE PurviewType <= @PurviewType AND NodeID IN (" + arrChildId + ")", cmdParams);
        }

        public bool UpdateNeedCreateHtml(string arrNodeId, bool needCreateHtml)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@needCreateHtml", DbType.Boolean, needCreateHtml);
            return DBHelper.ExecuteSql("UPDATE PE_Nodes SET NeedCreateHtml = @needCreateHtml WHERE NodeID IN (" + DBHelper.ToValidId(arrNodeId) + ")", cmdParams);
        }

        public bool UpdateNextId(int nodeId, int nextId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@NodeID", DbType.Int32, nodeId);
            cmdParams.AddInParameter("@NextID", DbType.Int32, nextId);
            try
            {
                return (DBHelper.ExecuteNonQueryProc("PR_Contents_Nodes_UpdateOrderId", cmdParams) > 0);
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateNodePurviewType(int nodeId, int purviewType)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@NodeID", DbType.Int32, nodeId);
            cmdParams.AddInParameter("@PurviewType", DbType.Int32, purviewType);
            return DBHelper.ExecuteSql("UPDATE PE_Nodes SET PurviewType = @PurviewType WHERE NodeID = @NodeID", cmdParams);
        }

        public bool UpdateOrderId(int nodeId, int orderId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@NodeID", DbType.Int32, nodeId);
            cmdParams.AddInParameter("@OrderID", DbType.Int32, orderId);
            try
            {
                return (DBHelper.ExecuteNonQueryProc("PR_Contents_Nodes_UpdateOrderId", cmdParams) > 0);
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateOrderId(int rootId, int orderId, int addNum)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@RootID", DbType.Int32, rootId);
            cmdParams.AddInParameter("@OrderID", DbType.Int32, orderId);
            cmdParams.AddInParameter("@AddNum", DbType.Int32, addNum);
            return DBHelper.ExecuteSql("UPDATE PE_Nodes SET OrderID = OrderID + @AddNum WHERE RootID = @RootId AND OrderID > @OrderID", cmdParams);
        }

        public void UpdateOrderIdByRootIdAndOrderd(int orderId, int rootId)
        {
            string strSql = "UPDATE PE_Nodes SET OrderId = OrderId + 1 WHERE OrderId > @OrderId AND RootId = @RootId";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@OrderId", DbType.Int32, orderId);
            cmdParams.AddInParameter("@RootId", DbType.Int32, rootId);
            DBHelper.ExecuteNonQuerySql(strSql, cmdParams);
        }

        public bool UpdateRootId(string nodeId, int rootId)
        {
            string strSql = "UPDATE PE_Nodes SET RootID = @RootId WHERE NodeId IN(" + DBHelper.ToValidId(nodeId) + ")";
            Parameters cmdParams = new Parameters("@RootId", DbType.Int32, rootId);
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool UpdateSettings(NodeSettingInfo settingsInfo, int nodeId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@NodeID", DbType.Int32, nodeId);
            cmdParams.AddInParameter("@Settings", DbType.String, SerializeSettings(settingsInfo));
            return DBHelper.ExecuteSql("UPDATE PE_Nodes SET Settings = @Settings WHERE NodeID = @NodeID", cmdParams);
        }
    }
}

