namespace EasyOne.WebSite.Controls.FieldControl
{
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.ModelControls;
    using System;
    using System.Web.UI.HtmlControls;

    public partial class SourceType : BaseFieldControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            this.SocTxtSource.Columns = DataConverter.CLng(base.Settings[0]);
            this.SocTxtSource.IsAdminManage = base.IsAdminManage;
            if (base.EnableNull)
            {
                this.ReqSocTxtSource.Visible = true;
            }
            if (!base.IsPostBack)
            {
                this.SocTxtSource.Text = this.FieldValue;
            }
            else
            {
                this.FieldValue = this.SocTxtSource.Text;
            }
        }
    }
}

