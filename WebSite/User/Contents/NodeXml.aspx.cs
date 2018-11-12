namespace EasyOne.WebSite.User.Contents
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Enumerations;
    using EasyOne.Model.Contents;
    using EasyOne.Web.UI;
    using System;
    using System.Data;
    using System.Text;
    using System.Web;
    using System.Web.UI.HtmlControls;

    public partial class NodeXml : DynamicPage
    {
        private static void CheckTreePermission(StringBuilder xmlBuilder, NodeInfo nodeInfo, ref string arrModelName, ref string icon, ref string action, string target)
        {
            bool flag = UserPermissions.AccessCheck(OperateCode.NodeContentInput, nodeInfo.NodeId);
            string checkStr = UserPermissions.GetRoleNodeId(PEContext.Current.User.RoleId, OperateCode.NodeContentInput, PEContext.Current.User.UserInfo.IsInheritGroupRole ? 1 : 0);
            string findStr = nodeInfo.NodeId.ToString();
            bool flag2 = false;
            if ((nodeInfo.ArrChildId.IndexOf(",", StringComparison.Ordinal) > 0) && !flag)
            {
                if (!string.IsNullOrEmpty(nodeInfo.ArrChildId))
                {
                    findStr = nodeInfo.ArrChildId;
                }
                if (StringHelper.FoundCharInArr(checkStr, findStr))
                {
                    flag2 = true;
                }
            }
            if ((nodeInfo.ParentId > 0) && !flag)
            {
                findStr = nodeInfo.ParentPath + "," + nodeInfo.NodeId.ToString();
                flag = StringHelper.FoundCharInArr(checkStr, findStr);
            }
            StringBuilder builder = new StringBuilder();
            if (string.IsNullOrEmpty(action))
            {
                action = " ContentManage.aspx";
            }
            object obj2 = action;
            action = string.Concat(new object[] { obj2, "?NodeID=", nodeInfo.NodeId, "&amp;NodeName=", HttpUtility.UrlEncode(DataSecurity.XmlEncode(nodeInfo.NodeName)) });
            if (flag || flag2)
            {
                xmlBuilder.Append("<tree ");
                xmlBuilder.Append("text=\"" + DataSecurity.XmlEncode(nodeInfo.NodeName) + "\" ");
                xmlBuilder.Append("arrModelId=\"");
                xmlBuilder.Append(builder.ToString());
                xmlBuilder.Append("\" ");
                xmlBuilder.Append("arrModelName=\"");
                xmlBuilder.Append((string) arrModelName);
                xmlBuilder.Append("\" ");
                xmlBuilder.Append("nodeId=\"");
                xmlBuilder.Append(nodeInfo.NodeId);
                xmlBuilder.Append("\" ");
                xmlBuilder.Append("target=\"");
                xmlBuilder.Append(target);
                xmlBuilder.Append("\" ");
                xmlBuilder.Append(" expand=\"0\" ");
                xmlBuilder.Append(" childNumber=\"");
                xmlBuilder.Append(nodeInfo.Child);
                xmlBuilder.Append("\"");
                xmlBuilder.Append(" depath=\"");
                xmlBuilder.Append(nodeInfo.Depth);
                xmlBuilder.Append("\"");
                if (action != "selectnode")
                {
                    xmlBuilder.Append(" action=\"" + action + "\" ");
                }
                if (nodeInfo.Child > 0)
                {
                    xmlBuilder.Append("src=\"NodeXml.aspx?Action=Content&amp;NodeID=" + nodeInfo.NodeId + "\" ");
                }
                if (!flag)
                {
                    icon = "Forbid";
                    xmlBuilder.Append(" anchorType=\"0\" ");
                }
                else
                {
                    switch (nodeInfo.PurviewType)
                    {
                        case 0:
                            icon = "Container";
                            break;

                        case 1:
                            icon = "HalfOpen";
                            break;

                        case 2:
                            icon = "Purview";
                            break;

                        default:
                            icon = "Container";
                            break;
                    }
                    xmlBuilder.Append(" anchorType=\"2\" ");
                }
                xmlBuilder.Append("icon=\"");
                xmlBuilder.Append((string) icon);
                xmlBuilder.Append("\" ");
                xmlBuilder.Append(" />");
            }
        }

        private string ContentXml()
        {
            base.Response.Clear();
            base.Response.ContentType = "text/xml; charset=UTF-8";
            base.Response.CacheControl = "no-cache";
            StringBuilder xmlBuilder = new StringBuilder();
            xmlBuilder.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n");
            xmlBuilder.Append("<tree>");
            foreach (NodeInfo info in EasyOne.Contents.Nodes.GetNodesListByParentId(BasePage.RequestInt32("NodeID")))
            {
                string arrModelName = "";
                string icon = "";
                string action = "";
                string target = "main_right";
                switch (info.NodeType)
                {
                    case NodeType.Container:
                    {
                        CheckTreePermission(xmlBuilder, info, ref arrModelName, ref icon, ref action, target);
                        continue;
                    }
                    case NodeType.Special:
                    {
                        continue;
                    }
                    case NodeType.Single:
                    {
                        icon = "Single";
                        continue;
                    }
                    case NodeType.Link:
                    {
                        icon = "Link";
                        action = info.LinkUrl;
                        target = "_blank";
                        continue;
                    }
                }
            }
            xmlBuilder.Append("</tree>\n");
            return xmlBuilder.ToString();
        }

        private string CreateNodeXml()
        {
            base.Response.Clear();
            base.Response.ContentType = "text/xml; charset=UTF-8";
            base.Response.CacheControl = "no-cache";
            StringBuilder builder = new StringBuilder();
            builder.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n");
            builder.Append("<tree>");
            NodeInfo cacheNodeById = EasyOne.Contents.Nodes.GetCacheNodeById(BasePage.RequestInt32("NodeId"));
            if (!cacheNodeById.IsNull)
            {
                DataRowCollection rows = ModelManager.GetModelListByNodeId(cacheNodeById.NodeId, true).Rows;
                if (rows.Count > 0)
                {
                    for (int i = 0; i < rows.Count; i++)
                    {
                        builder.Append("<tree");
                        builder.Append(" modelId=\"");
                        builder.Append(rows[i]["ModelId"].ToString());
                        builder.Append("\"");
                        builder.Append(">");
                        builder.Append(rows[i]["ItemName"].ToString());
                        builder.Append("(");
                        builder.Append(rows[i]["ModelName"].ToString());
                        builder.Append(")");
                        builder.Append("</tree>");
                    }
                }
            }
            builder.Append("</tree>\n");
            return builder.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string s = "";
            string str2 = BasePage.RequestStringToLower("Action");
            if (str2 != null)
            {
                if (!(str2 == "content"))
                {
                    if (str2 == "nodeinfo")
                    {
                        s = this.CreateNodeXml();
                    }
                }
                else
                {
                    s = this.ContentXml();
                }
            }
            base.Response.Write(s);
            base.Response.End();
        }
    }
}

