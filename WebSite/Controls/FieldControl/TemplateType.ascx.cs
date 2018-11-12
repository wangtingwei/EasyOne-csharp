namespace EasyOne.WebSite.Controls.FieldControl
{
    using EasyOne.Controls;
    using EasyOne.ModelControls;
    using System;
    using System.Web.UI.HtmlControls;

    public partial class TemplateType : BaseFieldControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (base.EnableNull)
            {
                this.ReqFscTemplate.Visible = true;
            }
            if (!base.IsPostBack)
            {
                if (!string.IsNullOrEmpty(this.FieldValue))
                {
                    this.FscTemplate.Text = this.FieldValue;
                }
            }
            else
            {
                this.FieldValue = this.FscTemplate.Text;
            }
        }
    }
}

