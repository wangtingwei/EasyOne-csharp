namespace EasyOne.WebSite.Controls
{
    using EasyOne.Common;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.Contents;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class CategoryInputerControl : BaseUserControl
    {
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                int nodeId = DataConverter.CLng(this.DrpCategory.SelectedValue);
                DataTable dataTable = ContentManage.GetCountByNodeAndInputer(nodeId, null, DataConverter.CDate(this.DpkStartDate.Text.Trim()), DataConverter.CDate(this.DpkEndDate.Text.Trim()));
                IList<NodeInfo> nodeList = new List<NodeInfo>();
                if (nodeId > 0)
                {
                    NodeInfo item = new NodeInfo();
                    item.NodeId = nodeId;
                    item.NodeName = EasyOne.Contents.Nodes.GetCacheNodeById(nodeId).NodeName;
                    nodeList.Add(item);
                }
                else
                {
                    nodeList = EasyOne.Contents.Nodes.GetNodesList(NodeType.Container);
                }
                IList<UserInfo> usersByPost = Users.GetUsersByPost();
                StringBuilder sb = new StringBuilder("<table class=\"border\" id=\"statistics\" cellspacing=\"1\" width=\"100%\" cellpadding=\"2\" border=\"0\">");
                this.BuildTableHead(sb, usersByPost);
                this.BuildTableBody(sb, dataTable, nodeList, usersByPost);
                this.BuildTableFooter(sb, usersByPost.Count);
                sb.Append("</table>");
                this.SpanCount.InnerHtml = sb.ToString();
            }
        }

        private void BuildTableBody(StringBuilder sb, DataTable dataTable, IList<NodeInfo> nodeList, IList<UserInfo> userList)
        {
            foreach (NodeInfo info in nodeList)
            {
                this.BuildTableTd(sb, dataTable, userList, info.NodeName);
            }
        }

        private void BuildTableFooter(StringBuilder sb, int length)
        {
            sb.Append("<tr class=\"tdbg\" align=\"center\"><td >合计</td>");
            for (int i = 0; i <= length; i++)
            {
                sb.Append("<td></td>");
            }
            sb.Append("</tr>");
        }

        private void BuildTableHead(StringBuilder sb, IList<UserInfo> userList)
        {
            sb.Append("<tr align=\"center\" class='title'><td>栏目名称</td>");
            foreach (UserInfo info in userList)
            {
                sb.Append("<td >");
                sb.Append(info.UserName);
                sb.Append("</td>");
            }
            sb.Append("<td>合计</td></tr>");
        }

        private void BuildTableTd(StringBuilder sb, DataTable dataTable, IList<UserInfo> userList, string nodeName)
        {
            int num = 0;
            sb.Append("<tr class=\"tdbg\" align=\"center\">");
            sb.Append("<td>");
            sb.Append(nodeName);
            sb.Append("</td>");
            for (int i = 0; i < userList.Count; i++)
            {
                sb.Append("<td>");
                DataRow[] rowArray = dataTable.Select("NodeName='" + nodeName + "' AND UserName='" + userList[i].UserName + "'");
                if (rowArray.Length > 0)
                {
                    num += DataConverter.CLng(rowArray[0]["Count"]);
                    sb.Append(rowArray[0]["Count"]);
                }
                else
                {
                    sb.Append("0");
                }
                sb.Append("</td>");
            }
            sb.Append("<td >");
            sb.Append(num.ToString());
            sb.Append("</td>");
            sb.Append("</tr>");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.DrpCategory.DataSource = EasyOne.Contents.Nodes.GetNodeNameForContainerItems();
                this.DrpCategory.DataTextField = "NodeName";
                this.DrpCategory.DataValueField = "NodeId";
                this.DrpCategory.DataBind();
                this.DrpCategory.Items.Insert(0, new ListItem("所有栏目", "0"));
                this.DpkStartDate.Text = DateTime.Now.ToString("yyyy") + "-01-01";
                this.DpkEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }
    }
}

