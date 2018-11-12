namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using System;
    using System.Text;
    using EasyOne.ExtendedControls;
    using EasyOne.ModelControls;

    public partial class CategoryOrderGuid : AdminPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            this.XLoadNodeTree.RootText = SiteConfig.SiteInfo.SiteName;
            this.XLoadNodeTree.RootAction = "CategoryOrder.aspx";
            this.XLoadNodeTree.XmlSrc = "NodeTreeXml.aspx?Action=Order";
            this.RegisterRightMenuJs();
        }

        private void RegisterRightMenuJs()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<script language=\"JavaScript\" type=\"text/javascript\">\n");
            builder.Append("<!--\n");
            builder.Append("function rightMenu(nodeId,arrModelId,arrModelName,event,extra,nodeType,arrPurview) {\n");
            builder.Append("    var currentNodesManage = arrPurview?arrPurview.indexOf(\"1\"):false;\n");
            builder.Append("    var toolMenu = new WebFXMenu;\n");
            builder.Append("    toolMenu.width = 100;\n");
            builder.Append("  document.getElementById(\"menudata\").innerHTML = toolMenu;\n");
            builder.Append("  var yScrolltop;\n");
            builder.Append("  var xScrollleft;\n");
            builder.Append("  if (self.pageYOffset || self.pageXOffset) {\n");
            builder.Append("      yScrolltop = self.pageYOffset;\n");
            builder.Append("      xScrollleft = self.pageXOffset;\n");
            builder.Append("  } else if (document.documentElement && document.documentElement.scrollTop ||         document.documentElement.scrollLeft ){     // Explorer 6 Strict \n");
            builder.Append("      yScrolltop = document.documentElement.scrollTop;\n");
            builder.Append("      xScrollleft = document.documentElement.scrollLeft;\n");
            builder.Append("  } else if (document.body) {// all other Explorers\n");
            builder.Append("      yScrolltop = document.body.scrollTop;\n");
            builder.Append("      xScrollleft = document.body.scrollLeft;\n");
            builder.Append("  }\n");
            builder.Append("  toolMenu.left = event.clientX + xScrollleft;\n");
            builder.Append("  toolMenu.top = event.clientY + document.body.scrollTop + yScrolltop;\n");
            builder.Append("  toolMenu.show();\n");
            builder.Append("}\n");
            builder.Append("function going(type,modelId,nodeId)\n");
            builder.Append("{\n");
            builder.Append("    if(type==\"sortChildNode\")\n");
            builder.Append("    {\n");
            builder.Append("        var url = \"CategoryOrder.aspx?ParentID=\" + nodeId;\n");
            builder.Append("          JumpToMainRight(url);\n");
            builder.Append("    }\n");
            builder.Append("    if(type==\"link\")\n");
            builder.Append("    {\n");
            builder.Append("        var url = \"OutLink.aspx?NodeId=\" + nodeId\n");
            builder.Append("          JumpToMainRight(url);\n");
            builder.Append("    }\n");
            builder.Append("    if(type==\"single\")\n");
            builder.Append("    {\n");
            builder.Append("        var url = \"Single.aspx?NodeId=\" + nodeId\n");
            builder.Append("         JumpToMainRight(url);\n");
            builder.Append("    }\n");
            builder.Append("}\n");
            builder.Append("//-->\n");
            builder.Append("</script>\n");
            this.Page.ClientScript.RegisterClientScriptBlock(base.GetType(), "EasyOne.Controls.XLoadTree.Resources.rightMenujs", builder.ToString());
        }
    }
}

