namespace EasyOne.ModelControls
{
    using EasyOne.Common;
    using EasyOne.Components;
    using System;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:KeyWordControl runat=\"server\"></{0}:KeyWordControl>"), Themeable(true)]
    public class KeyWordControl : TextBox
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Type type = base.GetType();
            if (!this.Page.ClientScript.IsClientScriptBlockRegistered(type, "KeyWordsControlsScript"))
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("<script language=\"JavaScript\">\n");
                builder.Append("<!--\n");
                builder.Append("function add(obj,clientId)\n");
                builder.Append("{\n");
                builder.Append("    if(obj==\"\"){return false;}\n");
                builder.Append("    if(document.getElementById(clientId.toString()).value==\"\")\n");
                builder.Append("    {\n");
                builder.Append("        document.getElementById(clientId.toString()).value=obj;\n");
                builder.Append("        return false;\n");
                builder.Append("    }\n");
                builder.Append("    var singleKey=obj.split(\"|\");\n");
                builder.Append("    var ignoreKey=\"\";\n");
                builder.Append("    for(i=0;i<singleKey.length;i++)\n");
                builder.Append("    {\n");
                builder.Append("        if(checkKey(document.getElementById(clientId.toString()).value,singleKey[i]))\n");
                builder.Append("        {\n");
                builder.Append("            ignoreKey=ignoreKey+singleKey[i]+\" \";\n");
                builder.Append("        }\n");
                builder.Append("        else\n");
                builder.Append("        {\n");
                builder.Append("            document.getElementById(clientId.toString()).value = document.getElementById(clientId.toString()).value + \" \" + singleKey[i];\n");
                builder.Append("        }\n");
                builder.Append("    }\n");
                builder.Append("    if(ignoreKey!=\"\")\n");
                builder.Append("    {\n");
                builder.Append("        alert(ignoreKey+\" 关键字已经存在，此操作已经忽略！\");\n");
                builder.Append("    }\n");
                builder.Append("}\n");
                builder.Append("function checkKey(Keylist,thisKey)\n");
                builder.Append("{\n");
                builder.Append("  if (Keylist==thisKey){\n");
                builder.Append("        return true;\n");
                builder.Append("  }\n");
                builder.Append("  else{\n");
                builder.Append("    var s=Keylist.split(\" \");\n");
                builder.Append("    for (j=0;j<s.length;j++){\n");
                builder.Append("        if(s[j]==thisKey)\n");
                builder.Append("            return true;\n");
                builder.Append("    }\n");
                builder.Append("   return false;\n");
                builder.Append("  }\n");
                builder.Append("}\n");
                builder.Append("//-->\n");
                builder.Append("</script>\n");
                this.Page.ClientScript.RegisterClientScriptBlock(type, "KeyWordsControlsScript", builder.ToString());
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            writer.Write("<span style=\"color: blue\"><=");
            if (!string.IsNullOrEmpty(this.KeyWords))
            {
                string[] strArray = this.KeyWords.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                string str = string.Empty;
                foreach (string str2 in strArray)
                {
                    str = DataSecurity.HtmlEncode(str2);
                    writer.Write("【<span onclick=\"add('");
                    writer.Write(DataSecurity.HtmlEncode(DataSecurity.ConvertToJavaScript(str2)));
                    writer.Write("','");
                    writer.Write(this.ClientID);
                    writer.Write("')\" style=\"cursor:pointer;color: red;\">");
                    writer.Write(str);
                    writer.Write("</span>】");
                }
            }
            string str3 = "";
            str3 = this.Page.Request.ApplicationPath.Equals("/") ? string.Empty : this.Page.Request.ApplicationPath;
            str3 = this.Page.Request.Url.Scheme + "://" + this.Page.Request.Url.Authority + str3;
            if (this.IsAdminManage)
            {
                str3 = str3 + "/" + SiteConfig.SiteOption.ManageDir;
            }
            else
            {
                str3 = str3 + "/User";
            }
            if (this.IsAdminManage || PEContext.Current.User.Identity.IsAuthenticated)
            {
                writer.Write("【<span style=\"cursor:pointer;color: red;\" onclick=\"window.open('" + str3 + "/Accessories/KeyWordList.aspx?OpenerText=" + this.ClientID + "','KeyWordList' ,'width=600,height=450,resizable=0,scrollbars=yes');\">更多</span>】");
                writer.Write("</span>");
            }
        }

        public bool IsAdminManage
        {
            get
            {
                object obj2 = this.ViewState["IsAdminManage"];
                if (obj2 != null)
                {
                    return (bool) obj2;
                }
                return true;
            }
            set
            {
                this.ViewState["IsAdminManage"] = value;
            }
        }

        public string KeyWords
        {
            get
            {
                object obj2 = this.ViewState["KeyWords"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["KeyWords"] = value;
            }
        }
    }
}

