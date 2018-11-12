namespace EasyOne.Controls
{
    using System;
    using System.ComponentModel;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:OneClickButton ID=\"OCBtn\" runat=\"server\"></{0}:OneClickButton>")]
    public class OneClickButton : Button
    {
        private string waitText;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            StringBuilder builder = new StringBuilder();
            builder.Append("if (typeof(Page_ClientValidate) == 'function') { ");
            if (!string.IsNullOrEmpty(this.ValidationGroup))
            {
                builder.Append("if (Page_ClientValidate('");
                builder.Append(this.ValidationGroup);
                builder.Append("')== false) { return false; }} ");
            }
            else
            {
                builder.Append("if (Page_ClientValidate() == false) { return false; }} ");
            }
            if (!string.IsNullOrEmpty(this.waitText))
            {
                builder.Append("this.value = '");
                builder.Append(this.waitText);
                builder.Append("';");
            }
            builder.Append("this.disabled = true;");
            builder.Append(this.Page.ClientScript.GetPostBackEventReference(this, ""));
            builder.Append(";");
            base.Attributes.Add("onclick", builder.ToString());
        }

        [DefaultValue("处理中.."), Description("按钮变为不可点击时按钮上显示的文本"), Category("自定义"), Localizable(true), Bindable(true)]
        public string WaitText
        {
            get
            {
                return this.waitText;
            }
            set
            {
                this.waitText = value;
            }
        }
    }
}

