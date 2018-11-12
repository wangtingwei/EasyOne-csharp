namespace EasyOne.WebSite.Controls.FieldControl
{
    using EasyOne.Common;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using EasyOne.ModelControls;

    public partial class BoolType : BaseFieldControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                if (string.IsNullOrEmpty(this.FieldValue))
                {
                    this.ChkBoolean.Checked = DataConverter.CBoolean(base.Settings[0]);
                }
                else
                {
                    this.ChkBoolean.Checked = DataConverter.CBoolean(this.FieldValue);
                }
            }
            else
            {
                this.FieldValue = this.ChkBoolean.Checked.ToString();
            }
        }
    }
}

