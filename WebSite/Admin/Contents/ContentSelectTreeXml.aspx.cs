namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.Contents;
    using EasyOne.Web.UI;
    using System;
    using System.Web;
    using System.Web.UI.HtmlControls;

    public partial class ContentSelectTreeXml : AdminPage
    {
        private static bool s_EnableAddWhenHasChild;
        private static bool s_IsChildNodePurview;
        private static bool s_IsInput;
        private static bool s_IsManage;
        private static bool s_IsShow;

        private static void AddXTreeItem(XTreeCollection xTreeList, NodeInfo nodeInfo)
        {
            s_IsShow = RolePermissions.AccessCheckNodePermission(OperateCode.NodeContentPreview, nodeInfo.NodeId);
            s_IsInput = RolePermissions.AccessCheckNodePermission(OperateCode.NodeContentInput, nodeInfo.NodeId);
            s_IsManage = RolePermissions.AccessCheckNodePermission(OperateCode.NodeContentManage, nodeInfo.NodeId);
            string str = "";
            if (!s_IsAdministrator)
            {
                if (nodeInfo.ArrChildId.IndexOf(",", StringComparison.Ordinal) > 0)
                {
                    foreach (NodeInfo info in Nodes.GetNodesListInArrChildId(nodeInfo.ArrChildId))
                    {
                        s_IsChildNodePurview = (RolePermissions.AccessCheckNodePermission(OperateCode.NodeContentPreview, info.NodeId) || RolePermissions.AccessCheckNodePermission(OperateCode.NodeContentInput, info.NodeId)) || RolePermissions.AccessCheckNodePermission(OperateCode.ContentManage, info.NodeId);
                        if (s_IsChildNodePurview)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    s_EnableAddWhenHasChild = true;
                }
            }
            if (ModelManager.GetNodesModelTemplateRelationShip(nodeInfo.NodeId, DataConverter.CLng(HttpContext.Current.Request["ModelId"])).IsNull)
            {
                s_IsInput = false;
                s_IsManage = false;
            }
            if ((s_IsShow || s_IsInput) || (s_IsManage || s_IsChildNodePurview))
            {
                XTreeItem item = new XTreeItem();
                item.Text = nodeInfo.NodeName;
                item.ArrModelId = "0";
                item.ArrModelName = "";
                item.NodeId = nodeInfo.NodeId.ToString();
                item.Target = "";
                item.Expand = "0";
                if (nodeInfo.Child > 0)
                {
                    item.XmlSrc = string.Concat(new object[] { "ContentSelectTreeXml.aspx?NodeID=", nodeInfo.NodeId, "&ModelId=", HttpContext.Current.Request["ModelId"] });
                }
                if (!s_IsInput && !s_IsManage)
                {
                    str = "Forbid";
                    item.AnchorType = "0";
                }
                else
                {
                    switch (nodeInfo.PurviewType)
                    {
                        case 0:
                            str = "Container";
                            break;

                        case 1:
                            str = "HalfOpen";
                            break;

                        case 2:
                            str = "Purview";
                            break;

                        default:
                            str = "Container";
                            break;
                    }
                    item.AnchorType = "2";
                    if (!s_EnableAddWhenHasChild)
                    {
                        str = "Forbid";
                        item.AnchorType = "0";
                    }
                }
                item.Icon = str;
                xTreeList.Add(item);
            }
        }

        private static void ContentXml(XTreeCollection xTreeList)
        {
            foreach (NodeInfo info in Nodes.GetNodesListByParentId(DataConverter.CLng(HttpContext.Current.Request.QueryString["NodeID"])))
            {
                s_IsShow = false;
                s_IsInput = false;
                s_IsManage = false;
                s_IsChildNodePurview = false;
                if (info.Settings.EnableAddWhenHasChild)
                {
                    s_EnableAddWhenHasChild = true;
                }
                else if (info.Child > 0)
                {
                    s_EnableAddWhenHasChild = false;
                }
                else
                {
                    s_EnableAddWhenHasChild = true;
                }
                AddXTreeItem(xTreeList, info);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            base.Response.Clear();
            base.Response.ContentType = "text/xml; charset=UTF-8";
            base.Response.CacheControl = "no-cache";
            XTreeCollection xTreeList = new XTreeCollection();
            ContentXml(xTreeList);
            base.Response.Write(xTreeList.ToString());
            base.Response.End();
        }

        private static bool s_IsAdministrator
        {
            get
            {
                return PEContext.Current.Admin.IsSuperAdmin;
            }
        }
    }
}

