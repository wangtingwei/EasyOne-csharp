namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.Accessories;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class KeyWordList : AdminPage
    {
        protected string allKeyword;
        protected int i;
        protected string keyword;
        protected string keywordInput;

        private void BindData()
        {
            this.allKeyword = "";
            this.RepKeyWords.DataSource = Keywords.GetList((this.Pager.CurrentPageIndex - 1) * this.Pager.PageSize, this.Pager.PageSize, 2, DataSecurity.FilterBadChar(this.TxtKeyWord.Text), 2);
            this.Pager.RecordCount = Keywords.GetTotalOfKeyword(0, "0", 2);
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
                KeywordInfo dataItem = (KeywordInfo) e.Item.DataItem;
                this.allKeyword = this.allKeyword + " " + dataItem.KeywordText;
                ExtendedLiteral literal = e.Item.FindControl("LitKeyword") as ExtendedLiteral;
                string format = "<a href=\"#\" onclick=\"add('{0}')\" title='{1}'>{2}</a>";
                literal.HtmlEncode = false;
                literal.Text = string.Format(format, DataSecurity.HtmlEncode(DataSecurity.ConvertToJavaScript(dataItem.KeywordText)), DataSecurity.HtmlEncode(dataItem.KeywordText), (dataItem.KeywordText.Length < 6) ? DataSecurity.HtmlEncode(dataItem.KeywordText) : (DataSecurity.HtmlEncode(dataItem.KeywordText.Substring(0, 6)) + "..."));
            }
        }
    }
}

