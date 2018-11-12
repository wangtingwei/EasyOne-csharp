namespace EasyOne.WebSite.Controls.FieldControl
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.ModelControls;
    using System;
    using System.Web.UI.HtmlControls;

    public partial class KeywordType : BaseFieldControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            this.TxtKeyWord.Columns = DataConverter.CLng(base.Settings[0]);
            this.TxtKeyWord.KeyWords = Keywords.GetStrArrayKeywords(4);
            this.TxtKeyWord.IsAdminManage = base.IsAdminManage;
            if (base.EnableNull)
            {
                this.ReqTxtKeyWord.Visible = true;
            }
            if (!base.IsPostBack)
            {
                this.TxtKeyWord.Text = this.FieldValue;
            }
            else
            {
                this.FieldValue = this.TxtKeyWord.Text;
            }
        }
    }
}

