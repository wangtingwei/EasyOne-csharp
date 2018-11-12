namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using System;
    using System.Text;

    public partial class CommentGuide : AdminPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            this.XLoadNodeTree.RootText = SiteConfig.SiteInfo.SiteName;
            this.XLoadNodeTree.RootAction = "CommentManage.aspx";
            this.XLoadNodeTree.XmlSrc = "CommentTreeXml.aspx";
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
            builder.Append("                  toolMenu.add(new WebFXMenuItem(arr[0],'javascript:going(\"addcontent\",\"'+ arrId[i] +'\",\"'+ nodeId + '\",\"' + arr[1] +'\")',arr[0]));\n");
            builder.Append("              }\n");
            builder.Append("          }\n");
            builder.Append("          else\n");
            builder.Append("          {\n var arr = arrModelName.split(\"||\");");
            builder.Append("              toolMenu.add(new WebFXMenuItem(arr[0],'javascript:going(\"addcontent\",\"'+ arrModelId +'\",\"'+ nodeId + '\",\"' + arr[1] +'\")',arr[0]));\n");
            builder.Append("          }\n");
            builder.Append("        }\n");
            builder.Append("    }\n");
            builder.Append("}\n");
            builder.Append("//-->\n");
            builder.Append("</script>\n");
            this.Page.ClientScript.RegisterClientScriptBlock(base.GetType(), "EasyOne.Controls.XLoadTree.Resources.rightMenujs", builder.ToString());
        }
    }
}

