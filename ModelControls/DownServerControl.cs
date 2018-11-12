namespace EasyOne.ModelControls
{
    using EasyOne.Components;
    using System;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:DownServerControl runat=\"server\"></{0}:DownServerControl>"), Themeable(true)]
    public class DownServerControl : TextBox
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Type type = base.GetType();
            if (!this.Page.ClientScript.IsClientScriptBlockRegistered(type, "DownServerControlsScript"))
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("<script language=\"JavaScript\">\n");
                builder.Append("<!--\n");
                builder.Append("function addDownServer(obj,clientId)\n");
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
                builder.Append("            document.getElementById(clientId.toString()).value = document.getElementById(clientId.toString()).value + \"|\" + singleKey[i];\n");
                builder.Append("        }\n");
                builder.Append("    }\n");
                builder.Append("    if(ignoreKey!=\"\")\n");
                builder.Append("    {\n");
                builder.Append("        alert(ignoreKey+\" 下载服务器已经存在，此操作已经忽略！\");\n");
                builder.Append("    }\n");
                builder.Append("}\n");
                builder.Append("function checkDownServer(Keylist,thisKey)\n");
                builder.Append("{\n");
                builder.Append("  if (Keylist==thisKey){\n");
                builder.Append("        return true;\n");
                builder.Append("  }\n");
                builder.Append("  else{\n");
                builder.Append("    var s=Keylist.split(\"|\");\n");
                builder.Append("    for (j=0;j<s.length;j++){\n");
                builder.Append("        if(s[j]==thisKey)\n");
                builder.Append("            return true;\n");
                builder.Append("    }\n");
                builder.Append("   return false;\n");
                builder.Append("  }\n");
                builder.Append("}\n");
                builder.Append("//-->\n");
                builder.Append("</script>\n");
                this.Page.ClientScript.RegisterClientScriptBlock(type, "DownServerControlsScript", builder.ToString());
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            string str = "";
            str = this.Page.Request.ApplicationPath.Equals("/") ? string.Empty : this.Page.Request.ApplicationPath;
            str = this.Page.Request.Url.Scheme + "://" + this.Page.Request.Url.Authority + str;
            if (this.IsAdminManage)
            {
                str = str + "/" + SiteConfig.SiteOption.ManageDir;
            }
            else
            {
                str = str + "/User";
            }
            writer.Write("<input type=\"button\" id=\"{0}\" name=\"{1}\" value=\"{2}\" onclick=\"window.open('" + str + "/Accessories/DownServerList.aspx?OpenerText=" + this.ClientID + "','DownServerList' ,'width=600,height=450,resizable=0,scrollbars=yes');\">", this.ClientID, this.UniqueID, "选择下载服务器");
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
    }
}

