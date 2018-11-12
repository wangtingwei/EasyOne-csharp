namespace EasyOne.Controls
{
    using System;
    using System.ComponentModel;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:AlternateLiteral Text=\"\" AlternateText=\"\" runat=\"server\" />"), Themeable(true)]
    public class AlternateLiteral : Literal
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (HttpContext.Current.Request.QueryString["Action"] == this.AlternateAction)
            {
                base.Text = this.AlternateText;
            }
        }

        [Localizable(true), Description("交替动作"), Category("自定义"), Bindable(true), DefaultValue("Modify")]
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

        [Localizable(true), DefaultValue(""), Bindable(true), Description("交替的标题"), Category("自定义")]
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

