namespace EasyOne.WebSite.User.Accessories
{
    using EasyOne.Controls;
    using EasyOne.Shop;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class ProducerList : BasePage
    {
        protected HtmlForm form2;
        protected HtmlHead Head1;
        protected AspNetPager Pager;
        protected string ProducerInput;
        protected Repeater RepProducers;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.ProducersBindData(BasePage.RequestString("ProducerType"), base.Request.Form["Producer"]);
        }

        protected void Pager_PageChanged(object src, PageChangedEventArgs e)
        {
            this.Pager.CurrentPageIndex = e.NewPageIndex;
            this.ProducersBindData(BasePage.RequestString("ProducerType"), base.Request.Form["Producer"]);
        }

        private void ProducersBindData(string producerType, string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                this.RepProducers.DataSource = Producer.GetList((this.Pager.CurrentPageIndex - 1) * this.Pager.PageSize, this.Pager.PageSize, "", keyword, producerType, true);
            }
            else
            {
                this.RepProducers.DataSource = Producer.GetList((this.Pager.CurrentPageIndex - 1) * this.Pager.PageSize, this.Pager.PageSize, "ProducerName", keyword, producerType, true);
            }
            this.Pager.RecordCount = Producer.GetTotalOfProducer("ProducerName", keyword, "", true);
            this.RepProducers.DataBind();
        }
    }
}

