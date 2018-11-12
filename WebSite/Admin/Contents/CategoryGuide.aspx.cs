namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.AccessManage;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Web.UI;
    using System;
    using System.Text;

    public partial class CategoryGuide : AdminPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            this.XLoadNodeTree.RootText = SiteConfig.SiteInfo.SiteName;
            this.XLoadNodeTree.RootAction = "CategoryManage.aspx";
            this.XLoadNodeTree.XmlSrc = "NodeTreeXml.aspx";
            this.RegisterRightMenuJs();
        }

        private void RegisterRightMenuJs()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<script language=\"JavaScript\" type=\"text/javascript\">\n");
            builder.Append("<!--\n");
            builder.Append("function rightMenu(nodeId,arrModelId,arrModelName,event,extra,nodeType,arrPurview) {\n");
            builder.Append("    var toolMenu = new WebFXMenu;\n");
            builder.Append("    toolMenu.width = 100;\n");
            builder.Append("    var isMenu = false;\n");
            builder.Append("    if(nodeId == 'root')\n");
            builder.Append("    {\n");
            if (RolePermissions.AccessCheckNodePermission(OperateCode.CurrentNodesManage, -1) || PEContext.Current.Admin.IsSuperAdmin)
            {
                builder.Append("        toolMenu.add(new WebFXMenuItem('添加栏目节点','javascript:going(\"addNode\",\"\",\"' + nodeId + '\")','添加栏目节点'));\n");
                builder.Append("        toolMenu.add(new WebFXMenuItem('添加单页节点','javascript:going(\"single\",\"\",\"' + nodeId + '\")','添加单页节点'));\n");
                builder.Append("        toolMenu.add(new WebFXMenuItem('添加外部链接','javascript:going(\"link\",\"\",\"' + nodeId + '\")','添加外部链接'));\n");
                builder.Append("        isMenu = true;\n");
            }
            builder.Append("    } \n");
            builder.Append("    else \n");
            builder.Append("    {\n");
            builder.Append("        var currentNodesManage = arrPurview.indexOf(\"1\");\n");
            builder.Append("        var childNodesManage = arrPurview.indexOf(\"2\");\n");
            builder.Append("        switch(nodeType) {\n");
            builder.Append("          case \"Container\": \n");
            builder.Append("              if (currentNodesManage >= 0)\n");
            builder.Append("              {\n");
            builder.Append("                  toolMenu.add(new WebFXMenuItem('修改设置','javascript:going(\"setNode\",\"\",\"' + nodeId + '\")','修改设置'));\n");
            builder.Append("                  toolMenu.add(new WebFXMenuItem('复制节点','javascript:going(\"copyNode\",\"\",\"' + nodeId + '\")','复制节点'));\n");
            builder.Append("              }\n");
            builder.Append("              if (childNodesManage >= 0)\n");
            builder.Append("              {\n");
            builder.Append("                  toolMenu.add(new WebFXMenuItem('添加子节点','javascript:going(\"addChildNode\",\"\",\"' + nodeId + '\")','添加子节点'));\n");
            builder.Append("                  toolMenu.add(new WebFXMenuItem('添加单页节点','javascript:going(\"single\",\"\",\"' + nodeId + '\")','添加单页节点'));\n");
            builder.Append("                  toolMenu.add(new WebFXMenuItem('添加外部链接','javascript:going(\"link\",\"\",\"' + nodeId + '\")','添加外部链接'));\n");
            builder.Append("                  if(arrModelName == 'child')\n");
            builder.Append("                  {\n");
            builder.Append("                      toolMenu.add(new WebFXMenuItem('复位子节点','javascript:going(\"resetChildNode\",\"\",\"' + nodeId + '\")','复位子节点 '));\n");
            builder.Append("                  } \n");
            builder.Append("                  isMenu = true;\n");
            builder.Append("              }\n");
            builder.Append("              if (currentNodesManage >= 0)\n");
            builder.Append("              {\n");
            builder.Append("                  toolMenu.add(new WebFXMenuItem('移动节点','javascript:going(\"move\",\"\",\"' + nodeId + '\")','移动节点'));\n");
            builder.Append("                  toolMenu.add(new WebFXMenuItem('清空节点','javascript:going(\"clear\",\"\",\"' + nodeId + '\")','清空节点'));\n");
            builder.Append("                  if(nodeId!=-2){toolMenu.add(new WebFXMenuItem('删除节点','javascript:going(\"delete\",\"\",\"' + nodeId + '\")','删除节点'));}\n");
            builder.Append("                  isMenu = true;\n");
            builder.Append("              }\n");
            builder.Append("              break;\n");
            builder.Append("          case \"Link\": \n");
            builder.Append("          case \"Single\": \n");
            builder.Append("              if (currentNodesManage >= 0)\n");
            builder.Append("              {\n");
            builder.Append("                  isMenu = true;\n");
            builder.Append("                  toolMenu.add(new WebFXMenuItem('修改设置','javascript:going(\"setNode\",\"\",\"' + nodeId + '\")','修改设置'));\n");
            builder.Append("                  if(nodeId!=-2)");
            builder.Append("                  {    toolMenu.add(new WebFXMenuItem('复制节点','javascript:going(\"copyNode\",\"\",\"' + nodeId + '\")','复制节点'));\n");
            builder.Append("                      toolMenu.add(new WebFXMenuItem('删除节点','javascript:going(\"delete\",\"\",\"' + nodeId + '\")','删除节点'));}\n");
            builder.Append("              }\n");
            builder.Append("              break;\n");
            builder.Append("       }\n");
            builder.Append("    }\n");
            builder.Append("    if (isMenu == true) {\n");
            builder.Append("      document.getElementById(\"menudata\").innerHTML = toolMenu;\n");
            builder.Append("      var yScrolltop;\n");
            builder.Append("      var xScrollleft;\n");
            builder.Append("      if (self.pageYOffset || self.pageXOffset) {\n");
            builder.Append("          yScrolltop = self.pageYOffset;\n");
            builder.Append("          xScrollleft = self.pageXOffset;\n");
            builder.Append("      } else if (document.documentElement && document.documentElement.scrollTop || document.documentElement.scrollLeft ){// Explorer 6 Strict \n");
            builder.Append("          yScrolltop = document.documentElement.scrollTop;\n");
            builder.Append("          xScrollleft = document.documentElement.scrollLeft;\n");
            builder.Append("      } else if (document.body) {// all other Explorers\n");
            builder.Append("          yScrolltop = document.body.scrollTop;\n");
            builder.Append("          xScrollleft = document.body.scrollLeft;\n");
            builder.Append("      }\n");
            builder.Append("      toolMenu.left = event.clientX + xScrollleft;\n");
            builder.Append("      toolMenu.top = event.clientY + document.body.scrollTop + yScrolltop;\n");
            builder.Append("      toolMenu.show();\n");
            builder.Append("    }\n");
            builder.Append("}\n");
            builder.Append("function going(type,modelId,nodeId)\n");
            builder.Append("{\n");
            builder.Append("    if(type==\"addcontent\")\n");
            builder.Append("    {\n");
            builder.Append("        var url = \"Content.aspx?Action=add&NodeId=\" + nodeId + \"&ModelID=\" + modelId;\n");
            builder.Append("          JumpToMainRight(url);\n");
            builder.Append("    }\n");
            builder.Append("    if(type==\"setNode\")\n");
            builder.Append("    {\n");
            builder.Append("        var url = \"Category.aspx?Action=Modify&NodeID=\" + nodeId;\n");
            builder.Append("          JumpToMainRight(url);\n");
            builder.Append("    }\n");
            builder.Append("    if(type==\"copyNode\")\n");
            builder.Append("    {\n");
            builder.Append("        var url = \"Category.aspx?Action=Copy&NodeID=\" + nodeId;\n");
            builder.Append("          JumpToMainRight(url);\n");
            builder.Append("    }\n");
            builder.Append("    if(type==\"addChildNode\")\n");
            builder.Append("    {\n");
            builder.Append("        var url = \"Category.aspx?Action=Add&NodeID=\" + nodeId + \"&ParentID=\" + nodeId;\n");
            builder.Append("          JumpToMainRight(url);\n");
            builder.Append("    }\n");
            builder.Append("    if(type==\"addNode\")\n");
            builder.Append("    {\n");
            builder.Append("        var url = \"Category.aspx\";\n");
            builder.Append("          JumpToMainRight(url);\n");
            builder.Append("    }\n");
            builder.Append("    if(type==\"move\")\n");
            builder.Append("    {\n");
            builder.Append("        var url = \"CategoryMove.aspx?NodeID=\" + nodeId;\n");
            builder.Append("          JumpToMainRight(url);\n");
            builder.Append("    }\n");
            builder.Append("    if(type==\"clear\")\n");
            builder.Append("    {\n");
            builder.Append("        var isConfirm = confirm('清空节点将把节点（包括子节点）的所有文章放入回收站中！确定要清空此节点吗？');\n");
            builder.Append("        if(isConfirm)\n");
            builder.Append("        {\n");
            builder.Append("          var url = \"CategoryManage.aspx?Action=clear&NodeId=\" + nodeId;\n");
            builder.Append("            JumpToMainRight(url);\n");
            builder.Append("        }\n");
            builder.Append("    }\n");
            builder.Append("    if(type==\"resetChildNode\")\n");
            builder.Append("    {\n");
            builder.Append("        var isConfirm = confirm('“复位子节点”将把此节点的所有子节点都复位成二级子节点！请慎重操作！确定要复位子节点吗');\n");
            builder.Append("        if(isConfirm)\n");
            builder.Append("        {\n");
            builder.Append("          var url = \"CategoryManage.aspx?Action=ResetChildNodes&NodeID=\" + nodeId;\n");
            builder.Append("            JumpToMainRight(url);\n");
            builder.Append("        }\n");
            builder.Append("    }\n");
            builder.Append("    if(type==\"delete\")\n");
            builder.Append("    {\n");
            builder.Append("        var isConfirm = confirm('删除节点将删除该节点所有相关数据，确定要删除此节点吗？');\n");
            builder.Append("        if(isConfirm)\n");
            builder.Append("        {\n");
            builder.Append("            var url = \"" + AdminPage.AppendSecurityCode("CategoryManage.aspx?Action=Delete") + "&NodeID=\" + nodeId;\n");
            builder.Append("            JumpToMainRight(url);\n");
            builder.Append("        }\n");
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

