namespace EasyOne.WebSite.Controls.FieldControl
{
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.ModelControls;
    using System;
    using System.Web.UI.HtmlControls;
    using EasyOne.ExtendedControls;

    public partial class AuthorType : BaseFieldControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Author.Columns = DataConverter.CLng(base.Settings[0]);
            this.Author.IsAdminManage = base.IsAdminManage;
            if (base.EnableNull)
            {
                this.ReqAuthor.Visible = true;
            }
            if (!base.IsPostBack)
            {
                this.Author.Text = this.FieldValue;
            }
            else
            {
                this.FieldValue = this.Author.Text;
            }
        }
    }
}

