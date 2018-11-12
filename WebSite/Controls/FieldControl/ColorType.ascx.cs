namespace EasyOne.WebSite.Controls.FieldControl
{
    using EasyOne.Controls;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using EasyOne.ExtendedControls;
    using EasyOne.ModelControls;

    public partial class ColorType : BaseFieldControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.PickColor.MaxLength = 7;
            if (base.EnableNull)
            {
                this.ReqPickColor.Visible = true;
            }
            if (!base.IsPostBack)
            {
                this.PickColor.Text = this.FieldValue;
            }
            else
            {
                this.FieldValue = this.PickColor.Text;
            }
        }
    }
}

