namespace EasyOne.Controls
{
    using System;
    using System.ComponentModel;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:AlternateButton ID=\"Abtn\" Text=\"AlternateButton\" AlternateText=\"\" runat=\"server\" />"), Themeable(true)]
    public class AlternateButton : Button
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (HttpContext.Current.Request.QueryString["Action"] == this.AlternateAction)
            {
                base.Text = this.AlternateText;
            }
        }

        [Category("自定义"), DefaultValue("Modify"), Localizable(true), Bindable(true), Description("交替动作")]
        public string AlternateAction
        {
            get
            {
                string str = (string) this.ViewState["AlternateAction"];
                if (str != null)
                {
                    return str;
                }
                return "Modify";
            }
            set
            {
                this.ViewState["AlternateAction"] = value;
            }
        }

        [Localizable(true), Bindable(true), Category("自定义"), DefaultValue(""), Description("交替的标题")]
        public string AlternateText
        {
            get
            {
                string str = (string) this.ViewState["AlternateText"];
                if (str != null)
                {
                    return str;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["AlternateText"] = value;
            }
        }
    }
}

