namespace EasyOne.WebSite.Controls.FieldControl
{
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.ModelControls;
    using System;
    using System.Web.UI.HtmlControls;
    using EasyOne.ExtendedControls;

    public partial class DownServerType : BaseFieldControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            
            this.TxtDownServer.Columns = DataConverter.CLng(base.Settings[0]);
            this.TxtDownServer.IsAdminManage = base.IsAdminManage;
            if (base.EnableNull)
            {
                this.ReqTxtDownServer.Visible = true;
            }
            if (!base.IsPostBack)
            {
                this.TxtDownServer.Text = this.FieldValue;
            }
            else
            {
                this.FieldValue = this.TxtDownServer.Text;
            }
        }
    }
}

