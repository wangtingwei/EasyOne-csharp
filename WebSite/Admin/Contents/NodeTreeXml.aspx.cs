namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.Contents;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.UI.HtmlControls;

    public partial class NodeTreeXml : AdminPage
    {
        private static string action;
        private static string AddRight(bool isCurrentNodesManage, bool isChildNodesManage)
        {
            string str = "";
            if (isCurrentNodesManage)
            {
                str = "1";
            }
            if (!isChildNodesManage)
            {
                return str;
            }
            if (!string.IsNullOrEmpty(str))
            {
                return (str + ",2");
            }
            return (str + "2");
        }

        private static void CategoryXml(XTreeCollection xTreeList)
        {
            IList<NodeInfo> nodesListByParentId = Nodes.GetNodesListByParentId(DataConverter.CLng(HttpContext.Current.Request.QueryString["NodeID"]));
            action = HttpContext.Current.Request.QueryString["Action"];
            foreach (NodeInfo info in nodesListByParentId)
            {
                bool isShow = true;
                bool isCurrentNodesManage = true;
                bool isChildNodesManage = true;
                string arrCurrentNodesManage = "";
                string str2 = "";
                CheckPurview(info, ref isShow, ref isCurrentNodesManage, ref isChildNodesManage, ref arrCurrentNodesManage);
                if (!isShow && !isCurrentNodesManage)
                {
                    continue;
                }
                XTreeItem item = new XTreeItem();
                string str3 = "";
                if (info.Child > 0)
                {
                    str3 = "child";
                }
                string str4 = AddRight(isCurrentNodesManage, isChildNodesManage);
                item.Text = info.NodeName;
                item.ArrModelId = "";
                item.ArrModelName = str3;
                item.NodeId = info.NodeId.ToString();
                item.ArrPurview = str4;
                item.Target = "main_right";
                item.Expand = "1";
                item.NodeType = info.NodeType.ToString();
                if (string.Compare(action, "Order", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    item.Title = "小贴士：如果您想对某个栏目的子栏目进行排序，请点击左侧栏目树中的对应节点，然后在右边的栏目列表中排序并保存。";
                }
                else
                {
                    item.Title = "小贴士：您可以在节点名称上点击鼠标右键，从弹出菜单中选择相关操作。";
                }
                if (info.Child > 0)
                {
                    if (string.Compare(action, "Order", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        item.XmlSrc = "NodeTreeXml.aspx?Action=Order&NodeID=" + info.NodeId;
                    }
                    else
                    {
                        item.XmlSrc = "NodeTreeXml.aspx?NodeID=" + info.NodeId;
                    }
                }
                if (string.Compare(action, "Order", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    item.Action = "CategoryOrder.aspx?Action=Order&NodeID=" + info.NodeId;
                }
                else
                {
                    item.Action = "Category.aspx?Action=Modify&NodeID=" + info.NodeId;
                }
                switch (info.NodeType)
                {
                    case NodeType.Container:
                        if (!isCurrentNodesManage)
                        {
                            goto Label_025B;
                        }
                        switch (info.PurviewType)
                        {
                            case 1:
                                goto Label_0234;

                            case 2:
                                goto Label_023D;
                        }
                        goto Label_0246;

                    case NodeType.Single:
                        str2 = "Single";
                        goto Label_0280;

                    case NodeType.Link:
                        str2 = "Link";
                        goto Label_0280;

                    default:
                        goto Label_0280;
                }
                str2 = "Container";
                goto Label_024D;
            Label_0234:
                str2 = "HalfOpen";
                goto Label_024D;
            Label_023D:
                str2 = "Purview";
                goto Label_024D;
            Label_0246:
                str2 = "Container";
            Label_024D:
                item.AnchorType = "2";
                goto Label_0280;
            Label_025B:
                str2 = "Forbid";
                item.AnchorType = "0";
            Label_0280:
                item.Icon = str2;
                xTreeList.Add(item);
            }
        }

        private static void CheckPurview(NodeInfo nodeInfo, ref bool isShow, ref bool isCurrentNodesManage, ref bool isChildNodesManage, ref string arrCurrentNodesManage)
        {
            if (!Administrator)
            {
                isCurrentNodesManage = RolePermissions.AccessCheckNodePermission(OperateCode.CurrentNodesManage, -1);
                if (!isCurrentNodesManage)
                {
                    isCurrentNodesManage = RolePermissions.AccessCheckNodePermission(OperateCode.CurrentNodesManage, nodeInfo.NodeId);
                }
                isChildNodesManage = !RolePermissions.AccessCheckNodePermission(OperateCode.ChildNodesManage, -1);
                if (!isChildNodesManage)
                {
                    isChildNodesManage = RolePermissions.AccessCheckNodePermission(OperateCode.ChildNodesManage, nodeInfo.NodeId);
                }
                switch (nodeInfo.NodeType)
                {
                    case NodeType.Container:
                    {
                        string findStr = nodeInfo.NodeId.ToString();
                        if ((nodeInfo.ArrChildId.IndexOf(",", StringComparison.Ordinal) > 0) && !isCurrentNodesManage)
                        {
                            if (!string.IsNullOrEmpty(nodeInfo.ArrChildId))
                            {
                                findStr = nodeInfo.ArrChildId;
                            }
                            arrCurrentNodesManage = RolePermissions.GetRoleNodeId(PEContext.Current.Admin.Roles, OperateCode.CurrentNodesManage);
                            if (StringHelper.FoundCharInArr(arrCurrentNodesManage, findStr))
                            {
                                isShow = true;
                            }
                        }
                        if (nodeInfo.ParentId > 0)
                        {
                            foreach (string str2 in nodeInfo.ParentPath.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                if (str2 != "0")
                                {
                                    if (!isCurrentNodesManage)
                                    {
                                        isCurrentNodesManage = RolePermissions.AccessCheckNodePermission(OperateCode.CurrentNodesManage, DataConverter.CLng(str2));
                                    }
                                    if (!isChildNodesManage)
                                    {
                                        isChildNodesManage = RolePermissions.AccessCheckNodePermission(OperateCode.ChildNodesManage, DataConverter.CLng(str2));
                                    }
                                    if (isCurrentNodesManage && isChildNodesManage)
                                    {
                                        return;
                                    }
                                }
                            }
                            return;
                        }
                        return;
                    }
                    case NodeType.Special:
                        return;

                    case NodeType.Single:
                    case NodeType.Link:
                        if (!isCurrentNodesManage)
                        {
                            isShow = false;
                            isCurrentNodesManage = false;
                        }
                        return;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            base.Response.Clear();
            base.Response.ContentType = "text/xml; charset=UTF-8";
            base.Response.CacheControl = "no-cache";
            XTreeCollection xTreeList = new XTreeCollection();
            CategoryXml(xTreeList);
            base.Response.Write(xTreeList.ToString());
            base.Response.End();
        }

        private static bool Administrator
        {
            get
            {
                return PEContext.Current.Admin.IsSuperAdmin;
            }
        }
    }
}

