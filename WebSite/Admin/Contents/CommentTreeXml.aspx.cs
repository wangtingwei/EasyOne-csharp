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
    using System.Web;
    using System.Web.UI.HtmlControls;

    public partial class CommentTreeXml : AdminPage
    {

        private void AddXTreeItem(XTreeCollection xTreeList, NodeInfo nodeInfo)
        {
            bool flag = false;
            bool flag2 = false;
            if (nodeInfo.NodeType == NodeType.Container)
            {
                flag2 = RolePermissions.AccessCheckNodePermission(OperateCode.NodeCommentManage, nodeInfo.NodeId);
                if (!Administrator && (nodeInfo.ArrChildId.IndexOf(",", StringComparison.Ordinal) > 0))
                {
                    foreach (NodeInfo info in Nodes.GetNodesListInArrChildId(nodeInfo.ArrChildId))
                    {
                        if (RolePermissions.AccessCheckNodePermission(OperateCode.NodeCommentManage, info.NodeId))
                        {
                            flag = true;
                            break;
                        }
                    }
                }
            }
            if (flag2 || flag)
            {
                XTreeItem item = new XTreeItem();
                item.NodeId = nodeInfo.NodeId.ToString();
                item.Text = nodeInfo.NodeName;
                item.ArrModelId = "";
                item.ArrModelName = "";
                item.Target = "main_right";
                item.Expand = "0";
                item.Action = string.Concat(new object[] { "CommentManage.aspx?NodeID=", nodeInfo.NodeId, "&NodeName=", base.Server.UrlEncode(nodeInfo.NodeName) });
                string str = "";
                string str2 = "";
                if (!flag2)
                {
                    str = "Forbid";
                    str2 = "0";
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
                    str2 = "2";
                }
                item.Icon = str;
                item.AnchorType = str2;
                if (nodeInfo.Child > 0)
                {
                    item.XmlSrc = "CommentTreeXml.aspx?NodeID=" + nodeInfo.NodeId;
                }
                xTreeList.Add(item);
            }
        }

        private void CommentNodeXml(XTreeCollection xTreeList)
        {
            foreach (NodeInfo info in Nodes.GetNodesListByParentId(DataConverter.CLng(HttpContext.Current.Request.QueryString["NodeID"])))
            {
                this.AddXTreeItem(xTreeList, info);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            base.Response.Clear();
            base.Response.ContentType = "text/xml; charset=UTF-8";
            base.Response.CacheControl = "no-cache";
            XTreeCollection xTreeList = new XTreeCollection();
            this.CommentNodeXml(xTreeList);
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

