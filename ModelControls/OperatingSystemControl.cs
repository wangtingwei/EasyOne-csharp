namespace EasyOne.ModelControls
{
    using System;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [Themeable(true), ToolboxData("<{0}:OperatingSystemControl runat=\"server\"></{0}:OperatingSystemControl>")]
    public class OperatingSystemControl : TextBox
    {
        private const string OperatingSystemControljs = "OperatingSystemControljs";

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Type type = base.GetType();
            if (!this.Page.ClientScript.IsClientScriptBlockRegistered(type, "OperatingSystemControljs"))
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("<script language = 'JavaScript'>\n");
                builder.Append("function ToSystem(addTitle,clientID){\n");
                builder.Append("    var str=document.getElementById(clientID).value;\n");
                builder.Append("    if (document.getElementById(clientID).value==\"\") {\n");
                builder.Append("        document.getElementById(clientID).value=document.getElementById(clientID).value+addTitle;\n");
                builder.Append("    }else{\n");
                builder.Append("        if (str.substr(str.length-1,1)=='/'){\n");
                builder.Append("            document.getElementById(clientID).value=document.getElementById(clientID).value+addTitle;\n");
                builder.Append("        }else{\n");
                builder.Append("            document.getElementById(clientID).value=document.getElementById(clientID).value + '/' + addTitle;\n");
                builder.Append("        }\n");
                builder.Append("    }\n");
                builder.Append("    document.getElementById(clientID).focus();\n");
                builder.Append("}\n");
                builder.Append("</script>\n");
                this.Page.ClientScript.RegisterClientScriptBlock(type, "OperatingSystemControljs", builder.ToString());
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            writer.Write("<br /><span style=\"color: #808080\">平台选择：</span>");
            string[] separator = new string[] { "$$$" };
            foreach (string str in this.OperatingSystemValue.Split(separator, StringSplitOptions.RemoveEmptyEntries))
            {
                if (!string.IsNullOrEmpty(str.Trim()))
                {
                    writer.Write("<a href=\"javascript:ToSystem('" + str + "','" + this.ClientID + "')\">" + str + "</a>");
                    writer.Write("<span style=\"color: #808080\">/</span>");
                }
            }
        }

        public string OperatingSystemValue
        {
            get
            {
                object obj2 = this.ViewState["OperatingSystemValue"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["OperatingSystemValue"] = value;
            }
        }
    }
}

