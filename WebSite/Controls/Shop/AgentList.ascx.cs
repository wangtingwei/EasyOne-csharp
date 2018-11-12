namespace EasyOne.WebSite.Controls
{
    using EasyOne.Controls;
    using EasyOne.Shop;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class AgentList : BaseUserControl
    {
        private string m_Keyword;

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            this.m_Keyword = this.TxtKeyWord.Text;
            this.DlstAgentBindData();
        }

        protected void DlstAgent_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                LinkButton button = (LinkButton) e.Item.FindControl("LbtnAgentName");
                string str = BaseUserControl.RequestString("OpenerText");
                if (string.IsNullOrEmpty(str))
                {
                    button.OnClientClick = string.Format("Javascript:opener.__doPostBack(\"AgentList_PostBack\",\"{0}\");window.close();", e.Item.DataItem.ToString());
                }
                else
                {
                    button.OnClientClick = "window.opener.document.getElementById('" + str + "').value='" + e.Item.DataItem.ToString() + "';window.close();";
                }
            }
        }

        private void DlstAgentBindData()
        {
            this.DlstAgent.DataSource = Agent.GetAgentNameList((this.Pager.CurrentPageIndex - 1) * this.Pager.PageSize, this.Pager.PageSize, this.m_Keyword);
            this.Pager.RecordCount = Agent.GetTotal();
            this.DlstAgent.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Pager.PageSize = 20;
            if (!this.Page.IsPostBack)
            {
                this.DlstAgentBindData();
                string str = BaseUserControl.RequestString("OpenerText");
                if (!string.IsNullOrEmpty(str))
                {
                    this.LbtnDelAgent.OnClientClick = "window.opener.document.getElementById('" + str + "').value='';window.close();";
                }
                else
                {
                    this.LbtnDelAgent.OnClientClick = "Javascript:opener.__doPostBack('AgentList_PostBack','');window.close();";
                }
            }
        }

        protected void Pager_PageChanged(object src, PageChangedEventArgs e)
        {
            this.Pager.CurrentPageIndex = e.NewPageIndex;
            this.DlstAgentBindData();
        }
    }
}

