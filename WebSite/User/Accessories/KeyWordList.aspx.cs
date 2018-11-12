namespace EasyOne.WebSite.User.Accessories
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Model.Accessories;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class KeyWordList : DynamicPage
    {
        protected string allKeyword;
        protected Button BtnSearch;
        protected HtmlForm form1;
        protected int i;
        protected string keyword;
        protected string keywordInput;
        protected AspNetPager Pager;
        protected Repeater RepKeyWords;
        protected TextBox TxtKeyWord;

        private void BindData()
        {
            this.RepKeyWords.DataSource = Keywords.GetList((this.Pager.CurrentPageIndex - 1) * this.Pager.PageSize, this.Pager.PageSize, 2, DataSecurity.FilterBadChar(this.TxtKeyWord.Text), 2);
            this.Pager.RecordCount = Keywords.GetTotalOfKeyword(0, "0", 2);
            this.allKeyword = "";
            this.RepKeyWords.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.Pager.PageSize = 40;
            }
            this.BindData();
        }

        protected void Pager_PageChanged(object src, PageChangedEventArgs e)
        {
            this.Pager.CurrentPageIndex = e.NewPageIndex;
            this.BindData();
        }

        protected void RepKeyWords_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                this.allKeyword = this.allKeyword + "|" + ((KeywordInfo) e.Item.DataItem).KeywordText;
            }
        }
    }
}

