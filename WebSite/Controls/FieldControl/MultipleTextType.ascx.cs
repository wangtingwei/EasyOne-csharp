namespace EasyOne.WebSite.Controls.FieldControl
{
    using EasyOne.Common;
    using EasyOne.Controls;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using EasyOne.ExtendedControls;
    public partial class MultipleTextType : BaseFieldControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            this.TxtMultiple.TextMode = TextBoxMode.MultiLine;
            this.TxtMultiple.Width = DataConverter.CLng(base.Settings[0]);
            this.TxtMultiple.Height = DataConverter.CLng(base.Settings[1]);
            if (base.EnableNull)
            {
                this.ReqTxtMultiple.Visible = true;
            }
            if (!base.IsPostBack)
            {
                this.TxtMultiple.Text = this.FieldValue;
            }
            else
            {
                this.FieldValue = this.TxtMultiple.Text;
            }
        }
    }
}

