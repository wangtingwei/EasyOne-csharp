namespace EasyOne.WebSite.Controls.FieldControl
{
    using EasyOne.Common;
    using EasyOne.Controls;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using EasyOne.ExtendedControls;
    public partial class LinkType : BaseFieldControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            this.TxtLinkUrl.TextMode = TextBoxMode.SingleLine;
            this.TxtLinkUrl.MaxLength = DataConverter.CLng(base.Settings[0]);
            this.TxtLinkUrl.Columns = DataConverter.CLng(base.Settings[1]);
            if (base.EnableNull)
            {
                this.ReqTxtLinkUrl.Visible = true;
            }
            if (!base.IsPostBack)
            {
                this.TxtLinkUrl.Text = this.FieldValue;
            }
            else
            {
                this.FieldValue = this.TxtLinkUrl.Text;
            }
        }
    }
}

