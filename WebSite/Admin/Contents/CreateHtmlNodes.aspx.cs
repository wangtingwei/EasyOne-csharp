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

    public partial class CreateHtmlNodes : AdminPage
    {

        protected void CreateAllCategoryList_Click(object sender, EventArgs e)
        {
            string str = this.CreateAllHtmlNodesList();
            if (string.IsNullOrEmpty(str))
            {
                AdminPage.WriteErrMsg("没有栏目需要生成！", "CreateHtmlNodes.aspx");
            }
            HtmlCategory category = new HtmlCategory();
            category.NodeIdArray = str;
            category.CommonCreateHtml();
            Nodes.UpdateNeedCreateHtml(str, false);
            BasePage.ResponseRedirect("CreateHtmlProgress.aspx?workId=" + category.CreateId);
        }

        private string CreateAllHtmlNodesList()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this.LstNodes.Items.Count; i++)
            {
                NodeInfo cacheNodeById = Nodes.GetCacheNodeById(DataConverter.CLng(this.LstNodes.Items[i].Value));
                if (cacheNodeById.IsCreateListPage && (cacheNodeById.PurviewType == 0))
                {
                    StringHelper.AppendString(sb, this.LstNodes.Items[i].Value);
                }
            }
            return sb.ToString();
        }

        protected void CreateCategoryListById_Click(object sender, EventArgs e)
        {
            string str = this.CreateHtmlNodesList();
            if (string.IsNullOrEmpty(str))
            {
                AdminPage.WriteErrMsg("请选择要生成的栏目！", "CreateHtmlNodes.aspx");
            }
            HtmlCategory category = new HtmlCategory();
            category.NodeIdArray = str;
            category.CommonCreateHtml();
            Nodes.UpdateNeedCreateHtml(str, false);
            BasePage.ResponseRedirect("CreateHtmlProgress.aspx?workId=" + category.CreateId);
        }

        protected string CreateHtmlNodesList()
        {
            StringBuilder sb = new StringBuilder();
            string str = string.Empty;
            bool flag = DataConverter.CBoolean(this.RdlIsCreateChild.SelectedValue);
            for (int i = 0; i < this.LstNodes.Items.Count; i++)
            {
                if (this.LstNodes.Items[i].Selected)
                {
                    if (flag)
                    {
                        if (!str.Contains(this.LstNodes.Items[i].Value))
                        {
                            NodeInfo cacheNodeById = Nodes.GetCacheNodeById(DataConverter.CLng(this.LstNodes.Items[i].Value));
                            if (string.IsNullOrEmpty(str))
                            {
                                str = cacheNodeById.ArrChildId + "," + str + ",";
                            }
                            else
                            {
                                str = str.Replace(cacheNodeById.ArrChildId, "") + "," + cacheNodeById.ArrChildId;
                            }
                        }
                    }
                    else
                    {
                        str = this.LstNodes.Items[i].Value + "," + str;
                    }
                }
            }
            if (!string.IsNullOrEmpty(str))
            {
                string[] strArray = str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < strArray.Length; j++)
                {
                    NodeInfo info2 = Nodes.GetCacheNodeById(DataConverter.CLng(strArray[j]));
                    if (info2.IsCreateListPage && (info2.PurviewType == 0))
                    {
                        StringHelper.AppendString(sb, strArray[j], ",");
                    }
                }
            }
            return sb.ToString();
        }

        protected void CreateNeedCreateHtmlList_Click(object sender, EventArgs e)
        {
            string str = this.CreateNeedCreateHtmlNodesList();
            if (string.IsNullOrEmpty(str))
            {
                AdminPage.WriteErrMsg("请选择要生成的待发布栏目！", "CreateHtmlNodes.aspx");
            }
            HtmlCategory category = new HtmlCategory();
            category.NodeIdArray = str;
            category.CommonCreateHtml();
            Nodes.UpdateNeedCreateHtml(str, false);
            BasePage.ResponseRedirect("CreateHtmlProgress.aspx?workId=" + category.CreateId);
        }

        private string CreateNeedCreateHtmlNodesList()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this.LstNodes.Items.Count; i++)
            {
                NodeInfo cacheNodeById = Nodes.GetCacheNodeById(DataConverter.CLng(this.LstNodes.Items[i].Value));
                if ((cacheNodeById.IsCreateListPage && (cacheNodeById.PurviewType == 0)) && cacheNodeById.NeedCreateHtml)
                {
                    StringHelper.AppendString(sb, this.LstNodes.Items[i].Value);
                }
            }
            return sb.ToString();
        }

        public void ListDataBind()
        {
            foreach (NodeInfo info in Nodes.GetNodeNameForContainerItems())
            {
                ListItem item = new ListItem();
                if (info.NeedCreateHtml)
                {
                    item.Text = info.NodeName + "(需要重新生成)";
                }
                else
                {
                    item.Text = info.NodeName;
                }
                item.Value = info.NodeId.ToString();
                if (!info.IsCreateListPage || (info.PurviewType > 0))
                {
                    item.Enabled = false;
                }
                else
                {
                    item.Enabled = true;
                }
                this.LstNodes.Items.Add(item);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                RolePermissions.BusinessAccessCheck(OperateCode.CreateHtmlManage);
                this.ListDataBind();
            }
        }
    }
}

