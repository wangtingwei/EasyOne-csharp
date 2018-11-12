namespace EasyOne.WebSite.Controls.FieldControl
{
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.ModelControls;
    using System;
    using System.Web.UI.HtmlControls;

    public partial class Trademark : BaseFieldControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            this.TmcTrademark.Columns = DataConverter.CLng(base.Settings[0]);
            this.TmcTrademark.IsAdminManage = base.IsAdminManage;
            if (base.EnableNull)
            {
                this.ReqTmcTrademark.Visible = true;
            }
            if (!base.IsPostBack)
            {
                this.TmcTrademark.Text = this.FieldValue;
            }
            else
            {
                this.FieldValue = this.TmcTrademark.Text;
            }
        }
    }
}

