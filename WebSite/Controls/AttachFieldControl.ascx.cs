namespace EasyOne.WebSite.Controls
{
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class AttachFieldControl : BaseUserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public string EnableFilterWord
        {
            get
            {
                return this.RadlFilterWord.SelectedValue;
            }
            set
            {
                BaseUserControl.SetSelectedIndexByValue(this.RadlFilterWord, value);
            }
        }

        public string EnableInsideLink
        {
            get
            {
                return this.RadlInsideLink.SelectedValue;
            }
            set
            {
                BaseUserControl.SetSelectedIndexByValue(this.RadlInsideLink, value);
            }
        }

        public string EnableShieldWord
        {
            get
            {
                return this.RadlShieldWord.SelectedValue;
            }
            set
            {
                BaseUserControl.SetSelectedIndexByValue(this.RadlShieldWord, value);
            }
        }
    }
}

