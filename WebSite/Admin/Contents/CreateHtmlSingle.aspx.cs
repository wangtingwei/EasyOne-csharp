namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.Contents;
    using EasyOne.StaticHtml;
    using EasyOne.Web.UI;
    using System;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class CreateHtmlSingle : AdminPage
    {

        protected void CreateAllSinglePage_Click(object sender, EventArgs e)
        {
            HtmlCategory category = new HtmlCategory();
            category.NodeIdArray = this.GetCreateHtmlNodesList(true);
            category.CommonCreateHtml();
            BasePage.ResponseRedirect("CreateHtmlProgress.aspx?workId=" + category.CreateId);
        }

        protected void CreateSinglePage_Click(object sender, EventArgs e)
        {
            string createHtmlNodesList = this.GetCreateHtmlNodesList(false);
            if (string.IsNullOrEmpty(createHtmlNodesList))
            {
                AdminPage.WriteErrMsg("请选择要生成的单页节点！", "CreateHtmlSingle.aspx");
            }
            HtmlCategory category = new HtmlCategory();
            category.NodeIdArray = createHtmlNodesList;
            category.CommonCreateHtml();
            BasePage.ResponseRedirect("CreateHtmlProgress.aspx?workId=" + category.CreateId);
        }

        protected string GetCreateHtmlNodesList(bool isSelectedAll)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this.LstSinglePage.Items.Count; i++)
            {
                if (isSelectedAll)
                {
                    StringHelper.AppendString(sb, this.LstSinglePage.Items[i].Value);
                }
                else if (this.LstSinglePage.Items[i].Selected)
                {
                    StringHelper.AppendString(sb, this.LstSinglePage.Items[i].Value);
                }
            }
            return sb.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RolePermissions.BusinessAccessCheck(OperateCode.CreateHtmlManage);
            if (!base.IsPostBack)
            {
                foreach (NodeInfo info in Nodes.GetNodesList(NodeType.Single))
                {
                    ListItem item = new ListItem();
                    item.Text = info.NodeName;
                    item.Value = info.NodeId.ToString();
                    item.Enabled = info.IsCreateListPage;
                    this.LstSinglePage.Items.Add(item);
                }
            }
        }
    }
}

