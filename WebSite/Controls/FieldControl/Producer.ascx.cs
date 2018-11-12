namespace EasyOne.WebSite.Controls.FieldControl
{
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.ModelControls;
    using System;
    using System.Web.UI.HtmlControls;

    public partial class Producer : BaseFieldControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            this.PrcTxtProducer.Columns = DataConverter.CLng(base.Settings[0]);
            this.PrcTxtProducer.IsAdminManage = base.IsAdminManage;
            if (base.EnableNull)
            {
                this.ReqPrcTxtProducer.Visible = true;
            }
            if (!base.IsPostBack)
            {
                this.PrcTxtProducer.Text = this.FieldValue;
            }
            else
            {
                this.FieldValue = this.PrcTxtProducer.Text;
            }
        }
    }
}

