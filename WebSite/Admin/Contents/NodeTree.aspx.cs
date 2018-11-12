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

    public partial class NodeTree : AdminPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            this.XLoadNodeTree.RootText = SiteConfig.SiteInfo.SiteName;
            this.XLoadNodeTree.RootAction = "ContentManage.aspx";
            this.XLoadNodeTree.XmlSrc = "ContentTreeXml.aspx";
            this.RegisterRightMenuJs();
            if (RolePermissions.AccessCheck(OperateCode.SpecialInfoManage))
            {
                this.LblNavigationLink.Text = " <a href=\"specialtree.aspx\" onclick='Reflash_main_right()'>切换到网站专题</a>";
            }
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
            builder.Append("    if(nodeId == 'root')\n");
            builder.Append("    {\n");
            builder.Append("        toolMenu.add(new WebFXMenuItem('整站更新','javascript:going(\"refresh\",\"0\")','整站更新'));\n");
            builder.Append("        toolMenu.add(new WebFXMenuItem('整站发布','javascript:going(\"sitepublish\",\"0\")','整站发布'));\n");
            builder.Append("    } \n");
            builder.Append("    else \n");
            builder.Append("    {\n");
            builder.Append("        if(arrModelId!='undefined')\n");
            builder.Append("        {\n");
            builder.Append("          if (arrModelId.indexOf(\",\") > 0)\n");
            builder.Append("          {\n");
            builder.Append("              var arrId = arrModelId.split(\",\");\n");
            builder.Append("              var arrName = arrModelName.split(\"$$$\");\n");
            builder.Append("              for (i=0; i < arrId.length;i++)\n");
            builder.Append("              {   var arr = arrName[i].split(\"||\"); \n");
            builder.Append("                  toolMenu.add(new WebFXMenuItem(arr[0],'javascript:going(\"addcontent\",\"'+ arrId[i] +'\",\"'+ nodeId + '\",\"' + arr[1] +'\",\"' + arr[2] +'\")',arr[0]));\n");
            builder.Append("                  toolMenu.add(new WebFXMenuItem(arr[3],'javascript:going(\"manageinfo\",\"'+ arrId[i] +'\",\"'+ nodeId + '\",\"' + arr[4] +'\",\"' + arr[5] +'\")',arr[3]));\n");
            builder.Append("              }\n");
            builder.Append("          }\n");
            builder.Append("          else\n");
            builder.Append("          {\n var arr = arrModelName.split(\"||\");");
            builder.Append("              toolMenu.add(new WebFXMenuItem(arr[0],'javascript:going(\"addcontent\",\"'+ arrModelId +'\",\"'+ nodeId + '\",\"' + arr[1] +'\",\"' + arr[2] +'\")',arr[0]));\n");
            builder.Append("              toolMenu.add(new WebFXMenuItem(arr[3],'javascript:going(\"manageinfo\",\"'+ arrModelId +'\",\"'+ nodeId + '\",\"' + arr[4] +'\",\"' + arr[5] +'\")',arr[3]));\n");
            builder.Append("          }\n");
            builder.Append("          }\n");
            builder.Append("              toolMenu.add(new WebFXMenuItem('回收站管理','javascript:going(\"recycle\",\"\",\"' + nodeId + '\")','回收站管理'));\n");
            builder.Append("              toolMenu.add(new WebFXMenuItem('签收管理','javascript:going(\"signin\",\"\",\"' + nodeId + '\")','签收管理'));\n");
            builder.Append("          if (currentNodesManage >= 0 || arrPurview == 'AllowSetNode')\n");
            builder.Append("          {\n");
            builder.Append("             toolMenu.add(new WebFXMenuItem('设置节点','javascript:going(\"setNode\",\"\",\"' + nodeId + '\")','设置节点'));\n");
            builder.Append("          }\n");
            builder.Append("    }\n");
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
            builder.Append("//-->\n");
            builder.Append("</script>\n");
            this.Page.ClientScript.RegisterClientScriptBlock(base.GetType(), "EasyOne.Controls.XLoadTree.Resources.rightMenujs", builder.ToString());
        }
    }
}

