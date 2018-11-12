namespace EasyOne.WebSite.User.Accessories
{
    using EasyOne.Controls;
    using EasyOne.Shop;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class TrademarkList : BasePage
    {
        protected HtmlForm form2;
        protected HtmlHead Head1;
        protected AspNetPager Pager;
        protected Repeater RepTrademarks;
        protected string TrademarkInput;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.TrademarksBindData(BasePage.RequestString("TrademarkType"), base.Request.Form["Trademark"]);
        }

        protected void Pager_PageChanged(object src, PageChangedEventArgs e)
        {
            this.Pager.CurrentPageIndex = e.NewPageIndex;
            this.TrademarksBindData(BasePage.RequestString("TrademarkType"), base.Request.Form["Trademark"]);
        }

        private void TrademarksBindData(string trademarkType, string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                this.RepTrademarks.DataSource = Trademark.GetList((this.Pager.CurrentPageIndex - 1) * this.Pager.PageSize, this.Pager.PageSize, "", keyword, trademarkType, true);
            }
            else
            {
                this.RepTrademarks.DataSource = Trademark.GetList((this.Pager.CurrentPageIndex - 1) * this.Pager.PageSize, this.Pager.PageSize, "TrademarkName", keyword, trademarkType, true);
            }
            this.Pager.RecordCount = Trademark.GetTotalOfTrademark("TrademarkName", keyword, "", true);
            this.RepTrademarks.DataBind();
        }
    }
}

