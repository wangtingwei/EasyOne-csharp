<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Accessories.TemplateTree" Codebehind="TemplateTree.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>模板树形列表</title>
</head>
<body class="tdbg">
    <form id="form1" runat="server">
        <div>
            <pe:FileTreeView ID="TrvTemplateDir" NodeNavigateUrl="ShowTemplates.aspx?FilesDir=" RootNodeName="网站模板" RootAction="ShowTemplates.aspx"
                runat="server">
            </pe:FileTreeView>
        </div>
    </form>
</body>
</html>
