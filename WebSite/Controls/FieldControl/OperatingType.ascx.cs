namespace EasyOne.WebSite.Controls.FieldControl
{
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.ModelControls;
    using System;
    using System.Web.UI.HtmlControls;

    public partial class OperatingType : BaseFieldControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            this.OscTxtOperat.OperatingSystemValue = base.Settings[0];
            this.OscTxtOperat.Columns = DataConverter.CLng(base.Settings[1]);
            if (base.EnableNull)
            {
                this.ReqOscTxtOperat.Visible = true;
            }
            if (!base.IsPostBack)
            {
                this.OscTxtOperat.Text = this.FieldValue;
            }
            else
            {
                this.FieldValue = this.OscTxtOperat.Text;
            }
        }
    }
}

