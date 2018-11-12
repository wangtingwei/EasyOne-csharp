namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.AccessManage;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.ExtendedControls;
    using EasyOne.Web.UI;
    using System;
    using System.Text;

    public partial class SpecialTree : AdminPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            this.XLoadNodeTree.RootText = SiteConfig.SiteInfo.SiteName;
            this.XLoadNodeTree.RootAction = "SpecialInfosManage.aspx";
            this.XLoadNodeTree.XmlSrc = "SpecialInfoTreeXml.aspx";
            this.RegisterRightMenuJs();
            if (RolePermissions.AccessCheck(OperateCode.CategoryInfoManage))
            {
                this.LblNavigationLink.Text = "<a href=\"nodeTree.aspx\" onclick='Reflash_main_right()'>切换到网站节点</a> ";
            }
        }

        private void RegisterRightMenuJs()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<script language=\"JavaScript\" type=\"text/javascript\">\n");
            builder.Append("<!--\n");
            builder.Append("function rightMenu(nodeId,arrModelId,arrModelName,event) {\n");
            builder.Append("    var toolMenu = new WebFXMenu;\n");
            builder.Append("    toolMenu.width = 100;\n");
            builder.Append("    if(nodeId == 'root')\n");
            builder.Append("    {\n");
            builder.Append("        toolMenu.add(new WebFXMenuItem('整站更新','javascript:going(\"refresh\",\"0\")','整站更新'));\n");
            builder.Append("        toolMenu.add(new WebFXMenuItem('整站发布','javascript:going(\"sitepublish\",\"0\")','整站发布'));\n");
            builder.Append("    } \n");
            builder.Append("    else \n");
            builder.Append("    {\n");
            builder.Append("        switch(arrModelId) {\n");
            builder.Append("          case \"0\": \n");
            builder.Append("              toolMenu.add(new WebFXMenuItem('设置专题','javascript:going(\"setNode\",\"\",\"' + nodeId + '\",\"\")','设置专题'));\n");
            builder.Append("              break;\n");
            builder.Append("          case \"1\": \n");
            builder.Append("              toolMenu.add(new WebFXMenuItem('设置专题类别','javascript:going(\"modifySpecialCategory\",\"\",\"' + nodeId + '\",\"\")','设置专题类别'));\n");
            builder.Append("              break;\n");
            builder.Append("          case \"2\": \n");
            builder.Append("              break;\n");
            builder.Append("       }\n");
            builder.Append("    }\n");
            builder.Append("    document.getElementById(\"menudata\").innerHTML = toolMenu;\n");
            builder.Append("    var yScrolltop;\n");
            builder.Append("    var xScrollleft;\n");
            builder.Append("    if (self.pageYOffset || self.pageXOffset) {\n");
            builder.Append("        yScrolltop = self.pageYOffset;\n");
            builder.Append("        xScrollleft = self.pageXOffset;\n");
            builder.Append("    } else if (document.documentElement && document.documentElement.scrollTop ||         document.documentElement.scrollLeft ){     // Explorer 6 Strict \n");
            builder.Append("        yScrolltop = document.documentElement.scrollTop;\n");
            builder.Append("        xScrollleft = document.documentElement.scrollLeft;\n");
            builder.Append("    } else if (document.body) {// all other Explorers\n");
            builder.Append("        yScrolltop = document.body.scrollTop;\n");
            builder.Append("        xScrollleft = document.body.scrollLeft;\n");
            builder.Append("    }\n");
            builder.Append("    toolMenu.left = event.clientX + xScrollleft;\n");
            builder.Append("    toolMenu.top = event.clientY + document.body.scrollTop + yScrolltop;\n");
            builder.Append("    toolMenu.show();\n");
            builder.Append("}\n");
            builder.Append("//-->\n");
            builder.Append("</script>\n");
            this.Page.ClientScript.RegisterClientScriptBlock(base.GetType(), "EasyOne.Controls.XLoadTree.Resources.rightMenujs", builder.ToString());
        }
    }
}

