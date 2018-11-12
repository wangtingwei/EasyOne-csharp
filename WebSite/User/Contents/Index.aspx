<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.User.Contents.Index" Codebehind="Index.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>信息管理</title>
</head>
<body>
    <pe:UserNavigation Tab="content" ID="UserCenterNavigation" runat="server" />
    <pe:ExtendedSiteMapPath ID="YourPosition" SiteMapProvider="UserMapProvider" runat="server" />
    <pe:ExtendedLiteral HtmlEncode="false" ID="LitLeft" runat="server"></pe:ExtendedLiteral>
    <pe:ExtendedLiteral HtmlEncode="false" ID="LitRight" runat="server"></pe:ExtendedLiteral>
    
</body>
</html>
