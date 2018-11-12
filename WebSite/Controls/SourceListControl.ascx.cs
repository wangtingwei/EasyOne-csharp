namespace EasyOne.WebSite.Controls
{
    using EasyOne.Accessories;
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Web.UI.WebControls;
    using EasyOne.Model;
    using EasyOne.Model.Accessories;

    public partial class SourceListControl : BaseUserControl
    {
        protected string openerInput;

        private void BindData()
        {
            IList<SourceInfo> list = Source.GetSourceList((this.Pager.CurrentPageIndex - 1) * this.Pager.PageSize, this.Pager.PageSize, 0, this.TxtSource.Text, BaseUserControl.RequestString("type"), true);
            this.Pager.RecordCount = Source.GetTotalOfSource(0, this.TxtSource.Text, BaseUserControl.RequestString("type"));
            this.Pager.PageSize = 10;
            this.RptSource.DataSource = list;
            this.RptSource.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.BindData();
        }

        protected void Pager_PageChanged(object src, PageChangedEventArgs e)
        {
            this.Pager.CurrentPageIndex = e.NewPageIndex;
            this.BindData();
        }
    }
}

