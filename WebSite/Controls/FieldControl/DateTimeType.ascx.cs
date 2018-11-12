namespace EasyOne.WebSite.Controls.FieldControl
{
    using System;
    using System.Web.UI.HtmlControls;
    using EasyOne.ModelControls;

    public partial class DateTimeType : BaseFieldControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (base.EnableNull)
            {
                this.ReqPickDate.Visible = true;
                
            }
            if (base.IsPostBack)
            {
                this.FieldValue = this.PickDate.Text;
            }
            else
            {
                this.PickDate.DateFormat = base.Settings[0];
                string str = base.Settings[1];
                if (str != null)
                {
                    if (!(str == "0"))
                    {
                        if (str == "1")
                        {
                            this.PickDate.Text = DateTime.Now.ToString();
                        }
                    }
                    else
                    {
                        this.PickDate.Text = "";
                    }
                }
                if (this.FieldValue == "Now")
                {
                    this.PickDate.Text = DateTime.Now.ToString();
                }
                else
                {
                    this.PickDate.Text = this.FieldValue;
                }
            }
        }
    }
}

