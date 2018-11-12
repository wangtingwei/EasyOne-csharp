<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.User.Info.ValidLog" Codebehind="ValidLog.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>有效期明细</title>
</head>
<body>
 <pe:UserNavigation Tab="user" ID="UserCenterNavigation" runat="server" />
 <pe:ExtendedSiteMapPath ID="YourPosition" SiteMapProvider="UserMapProvider" runat="server" />
    <form id="form1" runat="server">
        <div>
            <pe:ValidLog ID="SystemValidLog" runat="server"></pe:ValidLog>
        </div>
    </form>
</body>
</html>
