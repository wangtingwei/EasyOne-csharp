namespace EasyOne.WebSite.Controls.FieldControl
{
    using EasyOne.Common;
    using EasyOne.Controls;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class TextType : BaseFieldControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            string str;
            this.TxtSingleLine.MaxLength = DataConverter.CLng(base.Settings[0]);
            this.TxtSingleLine.Columns = DataConverter.CLng(base.Settings[1]);
            if (DataConverter.CBoolean(base.Settings[2]))
            {
                this.TxtSingleLine.TextMode = TextBoxMode.Password;
            }
            else
            {
                this.TxtSingleLine.TextMode = TextBoxMode.SingleLine;
            }
            if (((str = base.Settings[3]) != null) && (str != "0"))
            {
                if (!(str == "1"))
                {
                    if (str == "2")
                    {
                        this.TxtSingleLine.Attributes.Add("style", "ime-mode:disabled;");
                    }
                }
                else
                {
                    this.TxtSingleLine.Attributes.Add("style", "auto");
                }
            }
            if (base.EnableNull)
            {
                this.ReqTextSingleLine.Visible = true;
            }
            if (!base.IsPostBack)
            {
                this.TxtSingleLine.Text = this.FieldValue;
            }
            else
            {
                this.FieldValue = this.TxtSingleLine.Text;
            }
        }
    }
}

