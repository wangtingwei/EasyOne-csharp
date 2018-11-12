<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.LogOn" Codebehind="Login.aspx.cs" %>
    

<!DOCTYPE html PUBdivC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>简一CMS管理登录</title>
    <link href="../Admin/Common/Login.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="Login" runat="server">
        <div class="loginBox">
            <div class="loginLeft">用户名：</div>
            <div class="loginRight">
                <asp:TextBox ID="TxtUserName" MaxLength="20" runat="server"></asp:TextBox>
            </div>
            <div class="loginLeft">密 码：</div>
            <div class="loginRight">
                <asp:TextBox ID="TxtPassword" MaxLength="20" TextMode="password" runat="server"></asp:TextBox>
            </div>
            <div>
                <asp:Button ID="IbtnEnter"  runat="server" Style="width: 76px; height: 26px;" OnClick="IbtnEnter_Click" Text="登录" />
            </div>
        </div>
    </form>
</body>
</html>
