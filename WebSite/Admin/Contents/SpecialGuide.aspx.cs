namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using System;
    using System.Text;

    public partial class SpecialGuide : AdminPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            this.XLoadNodeTree.RootText = SiteConfig.SiteInfo.SiteName;
            this.XLoadNodeTree.RootAction = "SpecialManage.aspx";
            this.XLoadNodeTree.XmlSrc = "SpecialTreeXml.aspx";
            this.RegisterRightMenuJs();
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
            builder.Append("        toolMenu.add(new WebFXMenuItem('添加专题类别','javascript:going(\"addSpecialCategory\",\"\",\"\",\"\")','添加专题类别'));\n");
            builder.Append("        toolMenu.add(new WebFXMenuItem('专题类别排序','javascript:going(\"sortSpecialCategory\",\"\",\"\",\"\")','专题类别排序'));\n");
            builder.Append("    } \n");
            builder.Append("    else \n");
            builder.Append("    {\n");
            builder.Append("        switch(arrModelId) {\n");
            builder.Append("          case \"0\": \n");
            builder.Append("              toolMenu.add(new WebFXMenuItem('修改设置','javascript:going(\"setNode\",\"\",\"' + nodeId + '\",\"\")','修改设置'));\n");
            builder.Append("              toolMenu.add(new WebFXMenuItem('复制专题','javascript:going(\"copyNode\",\"\",\"' + nodeId + '\",\"\")','复制专题'));\n");
            builder.Append("              toolMenu.add(new WebFXMenuItem('清空专题','javascript:going(\"clear\",\"\",\"' + nodeId + '\",\"\")','清空专题'));\n");
            builder.Append("              toolMenu.add(new WebFXMenuItem('删除专题','javascript:going(\"delete\",\"\",\"' + nodeId + '\",\"\")','删除专题'));\n");
            builder.Append("              break;\n");
            builder.Append("          case \"1\": \n");
            builder.Append("              toolMenu.add(new WebFXMenuItem('添加专题','javascript:going(\"addSpecial\",\"\",\"' + nodeId + '\",\"\")','添加专题'));\n");
            builder.Append("              toolMenu.add(new WebFXMenuItem('专题排序','javascript:going(\"sortSpecial\",\"\",\"' + nodeId + '\",\"\")','专题排序'));\n");
            builder.Append("              toolMenu.add(new WebFXMenuItem('修改类别','javascript:going(\"modifySpecialCategory\",\"\",\"' + nodeId + '\",\"\")','修改类别'));\n");
            builder.Append("              toolMenu.add(new WebFXMenuItem('删除类别','javascript:going(\"deleteSpecialCategory\",\"\",\"' + nodeId + '\",\"\")','删除类别'));\n");
            builder.Append("              break;\n");
            builder.Append("          case \"2\": \n");
            builder.Append("              toolMenu.add(new WebFXMenuItem('添加专题','javascript:going(\"addSpecial\",\"\",\"' + nodeId + '\",\"\")','添加专题'));\n");
            builder.Append("              toolMenu.add(new WebFXMenuItem('修改类别','javascript:going(\"modifySpecialCategory\",\"\",\"' + nodeId + '\",\"\")','修改类别'));\n");
            builder.Append("              toolMenu.add(new WebFXMenuItem('删除类别','javascript:going(\"deleteSpecialCategory\",\"\",\"' + nodeId + '\",\"\")','删除类别'));\n");
            builder.Append("              break;\n");
            builder.Append("       }\n");
            builder.Append("    }\n");
            builder.Append("    document.getElementById(\"menudata\").innerHTML = toolMenu;\n");
            builder.Append("    var yScrolltop;\n");
            builder.Append("    var xScrollleft;\n");
            builder.Append("    if (self.pageYOffset || self.pageXOffset) {\n");
            builder.Append("         yScrolltop = self.pageYOffset;\n");
            builder.Append("         xScrollleft = self.pageXOffset;\n");
            builder.Append("    } else if (document.documentElement && document.documentElement.scrollTop ||         document.documentElement.scrollLeft ){     // Explorer 6 Strict \n");
            builder.Append("         yScrolltop = document.documentElement.scrollTop;\n");
            builder.Append("         xScrollleft = document.documentElement.scrollLeft;\n");
            builder.Append("    } else if (document.body) {// all other Explorers\n");
            builder.Append("         yScrolltop = document.body.scrollTop;\n");
            builder.Append("         xScrollleft = document.body.scrollLeft;\n");
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

