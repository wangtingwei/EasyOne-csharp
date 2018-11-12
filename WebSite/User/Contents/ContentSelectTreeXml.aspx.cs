namespace EasyOne.WebSite.User.Contents
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Enumerations;
    using EasyOne.Model.Contents;
    using EasyOne.Web.UI;
    using System;
    using System.Text;
    using System.Web;
    using System.Web.UI.HtmlControls;

    public  partial class ContentSelectTreeXml : DynamicPage
    {

        private static void CheckTreePermission(StringBuilder xmlBuilder, NodeInfo nodeInfo, ref string arrModelName, ref string icon, ref string action)
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
            action = string.Concat(new object[] { obj2, "?NodeID=", nodeInfo.NodeId, "&amp;NodeName=", HttpContext.Current.Server.UrlEncode(DataSecurity.XmlEncode(nodeInfo.NodeName)) });
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
                xmlBuilder.Append("target=\"\" ");
                xmlBuilder.Append("expand=\"0\" ");
                if (nodeInfo.Child > 0)
                {
                    xmlBuilder.Append("src=\"ContentSelectTreeXml.aspx?Action=Content&amp;NodeID=" + nodeInfo.NodeId + "\" ");
                }
                xmlBuilder.Append("action=\"#\" ");
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
                    if (!nodeInfo.Settings.EnableAddWhenHasChild && (nodeInfo.Child > 0))
                    {
                        icon = "Forbid";
                        xmlBuilder.Append(" anchorType=\"0\" ");
                    }
                    else
                    {
                        xmlBuilder.Append(" anchorType=\"2\" ");
                    }
                }
                xmlBuilder.Append("icon=\"");
                xmlBuilder.Append((string) icon);
                xmlBuilder.Append("\" ");
                xmlBuilder.Append(" />");
            }
        }

        public static string ContentXml()
        {
            StringBuilder xmlBuilder = new StringBuilder();
            xmlBuilder.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n");
            xmlBuilder.Append("<tree>");
            foreach (NodeInfo info in Nodes.GetNodesListByParentId(BasePage.RequestInt32("NodeID")))
            {
                string arrModelName = "";
                string icon = "";
                string action = "";
                if (info.NodeType == NodeType.Container)
                {
                    CheckTreePermission(xmlBuilder, info, ref arrModelName, ref icon, ref action);
                }
            }
            xmlBuilder.Append("</tree>\n");
            return xmlBuilder.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str2;
            base.Response.Clear();
            base.Response.ContentType = "text/xml; charset=UTF-8";
            base.Response.CacheControl = "no-cache";
            string s = "";
            if (((str2 = BasePage.RequestStringToLower("Action")) != null) && (str2 == "content"))
            {
                s = ContentXml();
            }
            base.Response.Write(s);
            base.Response.End();
        }
    }
}

