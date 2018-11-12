namespace EasyOne.WebSite.User.Contents
{
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.Contents;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.HtmlControls;

    public partial  class CommentXml : DynamicPage
    {
        private void AddXTreeItem(XTreeCollection xTreeList, NodeInfo nodeInfo)
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
            item.Icon = str;
            item.AnchorType = str2;
            if (nodeInfo.Child > 0)
            {
                item.XmlSrc = "CommentXml.aspx?NodeID=" + nodeInfo.NodeId;
            }
            xTreeList.Add(item);
        }

        private void CommentNodeXml(XTreeCollection xTreeList)
        {
            foreach (NodeInfo info in Nodes.GetNodesListByParentId(BasePage.RequestInt32("NodeID")))
            {
                if (info.NodeType == NodeType.Container)
                {
                    this.AddXTreeItem(xTreeList, info);
                }
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
    }
}

