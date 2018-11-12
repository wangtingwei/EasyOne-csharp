namespace EasyOne.WebSite.Controls
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class AuthorListControl : BaseUserControl
    {
        protected string AuthorInput;
        private void AuthorsBindData(int listType, string searchType, string keyword)
        {
            this.RepAuthors.DataSource = Author.GetAuthorList((this.Pager.CurrentPageIndex - 1) * this.Pager.PageSize, this.Pager.PageSize, listType, searchType, keyword, true);
            this.Pager.RecordCount = Author.GetTotalOfAuthor(4, searchType, keyword);
            this.RepAuthors.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string searchType = BaseUserControl.RequestString("SearchType");
            string keyword = base.Request.Form["Author"];
            int listType = BaseUserControl.RequestInt32("ListType", 0);
            if (listType == 0)
            {
                listType = DataConverter.CLng(base.Request.Form["HdnListType"]);
            }
            this.RptAuthorType.DataSource = Choiceset.GetDictionaryFieldValueByName("PE_Author", "Type");
            this.RptAuthorType.DataBind();
            this.AuthorsBindData(listType, searchType, keyword);
        }

        protected void Pager_PageChanged(object src, PageChangedEventArgs e)
        {
            this.Pager.CurrentPageIndex = e.NewPageIndex;
            string searchType = BaseUserControl.RequestString("SearchType");
            string keyword = base.Request.Form["Author"];
            int listType = BaseUserControl.RequestInt32("ListType");
            this.AuthorsBindData(listType, searchType, keyword);
        }
    }
}

