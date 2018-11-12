namespace EasyOne.Controls
{
    using System;
    using System.ComponentModel;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:ColorPicker ID=\"Cpk\" runat=\"server\"></{0}:ColorPicker>"), Themeable(true)]
    public class ColorPicker : TextBox
    {
        private const string ColorPicker_Html = "EasyOne.Controls.ColorPicker.EditColor.html";
        private Image m_Image;

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            this.m_Image = new Image();
            if (string.IsNullOrEmpty(this.ColorImage))
            {
                string webResourceUrl = this.Page.ClientScript.GetWebResourceUrl(base.GetType(), "EasyOne.Controls.ColorPicker.Rect.gif");
                this.m_Image.ImageUrl = webResourceUrl;
            }
            else
            {
                this.m_Image.ImageUrl = this.ColorImage;
            }
            this.m_Image.ImageAlign = ImageAlign.AbsMiddle;
            this.Controls.Add(this.m_Image);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Type type = base.GetType();
            string webResourceUrl = this.Page.ClientScript.GetWebResourceUrl(type, "EasyOne.Controls.ColorPicker.EditColor.html");
            if (!this.Page.ClientScript.IsClientScriptBlockRegistered(type, "EasyOne.Controls.ColorPicker.EditColor.html"))
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("<script language=\"JavaScript\">\n");
                builder.Append("function SelectColor(t,clientId){\n");
                builder.Append("var url=\"");
                builder.Append(webResourceUrl);
                builder.Append("\";\n");
                builder.Append("var old_color = ( document.getElementById(clientId).value.indexOf('#') == 0 ) ? '&'+document.getElementById(clientId).value.substr(1) : '&' + document.getElementById(clientId).value;\n");
                builder.Append("if(document.all){\n");
                builder.Append("  var color = showModalDialog(url+old_color, \"\", \"dialogWidth:18.5em; dialogHeight:16.0em; status:0\");\n");
                builder.Append("  if(color!=null){\n");
                builder.Append("      document.getElementById(clientId).value=color;\n");
                builder.Append("    }else{\n");
                builder.Append("      document.getElementById(clientId).focus();\n");
                builder.Append("    }\n");
                builder.Append("  }else{\n");
                builder.Append("    var color = window.open(url+ '&'+clientId,  \"hbcmsPop\", \"top=200,left=200,scrollbars=yes,dialog=yes,modal=no,width=300,height=260,resizable=yes\");\n");
                builder.Append("  }\n");
                builder.Append("}\n");
                builder.Append("</script>\n");
                this.Page.ClientScript.RegisterClientScriptBlock(type, "EasyOne.Controls.ColorPicker.EditColor.html", builder.ToString());
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            this.m_Image.Attributes.Add("onclick", "SelectColor(this,'" + this.ClientID + "');");
            this.m_Image.Attributes.Add("style", "CURSOR: pointer");
            this.m_Image.RenderControl(writer);
        }

        [DefaultValue(""), Bindable(true), Description("控件中显示图片的路径"), Localizable(true), Category("自定义")]
        public string ColorImage
        {
            get
            {
                this.EnsureChildControls();
                return this.m_Image.ImageUrl;
            }
            set
            {
                this.EnsureChildControls();
                this.m_Image.ImageUrl = value;
            }
        }
    }
}

