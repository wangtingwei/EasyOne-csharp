<%@ Page Language="C#" AutoEventWireup="True" Inherits="EasyOne.WebSite.LoginIndex" Codebehind="LoginIndex.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>会员登录</title>
    <link href="Skin/Default/user.css" rel="stylesheet" type="text/css" />
</head>
<body id="LoginStatusbody">
    <form id="form1" runat="server">
    <asp:Panel ID="PnlLogOn" runat="server">
        <table cellspacing="0">
            <tr>
                <td>
                    用户名：
                </td>
                <td>
                    <asp:TextBox ID="TxtUserName" MaxLength="20" Width="95" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    密 码：
                </td>
                <td>
                    <asp:TextBox ID="TxtPassword" runat="server" Width="95" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <asp:PlaceHolder ID="PhValCode" runat="server">
                <tr>
                    <td>
                        验证码：
                    </td>
                    <td>
                        <asp:TextBox ID="TxtValidateCode" MaxLength="6" Width="60" runat="server" onfocus="this.select();"></asp:TextBox>
                        <pe:ValidateCode ImageAlign="AbsMiddle" ID="VcodeLogOn" runat="server" RefreshLinkToolTip="看不清楚，换一个" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <tr>
                <td>
                    Cookie：
                </td>
                <td>
                    <asp:DropDownList ID="DropExpiration" runat="server">
                        <asp:ListItem Value="None" Text="不保存"></asp:ListItem>
                        <asp:ListItem Value="Day" Text="保存一天"></asp:ListItem>
                        <asp:ListItem Value="Month" Text="保存一月"></asp:ListItem>
                        <asp:ListItem Value="Year" Text="保存一年"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <asp:Button ID="BtnLogOn" runat="server" Text="登录" OnClick="BtnLogOn_Click" />&nbsp;&nbsp;
        <a href="User/Register.aspx" target="_top">注册</a>｜<a href="User/GetPassword.aspx"
            target="_top">忘记密码</a>
        <div class="clearbox">
        </div>
        <a class="bt_login" href="User/Contents/AnonymousContent.aspx" target="_blank">匿名投稿</a><a
            class="bt_login" href="shop/OrderForm.aspx" target="_blank">订单查询</a>
        <div class="clearbox">
        </div>
        <pe:RequiredFieldValidator ID="ValrUserName" runat="server" ErrorMessage="请输入用户名！"
            ControlToValidate="TxtUserName" Display="None" SetFocusOnError="True"></pe:RequiredFieldValidator>
        <pe:RequiredFieldValidator ID="ValrPassword" runat="server" ErrorMessage="请输入密码！"
            ControlToValidate="TxtPassword" Display="None" SetFocusOnError="True"></pe:RequiredFieldValidator>
        <pe:RequiredFieldValidator ID="ValrValidateCode" runat="server" ErrorMessage="请输入验证码！"
            ControlToValidate="TxtValidateCode" Display="None" SetFocusOnError="True"></pe:RequiredFieldValidator>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
            ShowSummary="False" />
    </asp:Panel>
    <asp:Panel ID="PnlLogOnStatus" runat="server">
        <div class="u_login">
            <asp:Literal ID="LitUserName" runat="server"></asp:Literal>，您好！您有：<br />
            <asp:Literal ID="LitMoney" runat="server">0</asp:Literal><br />
            <asp:Literal ID="LitUserExp" runat="server">0</asp:Literal><br />
            <pe:ExtendedLiteral HtmlEncode="false" ID="LitMessage" runat="server">0</pe:ExtendedLiteral><br />
            <asp:Literal ID="LitLogOnTime" runat="server">0</asp:Literal><br />
            <asp:Literal ID="LitPoint" runat="server">0</asp:Literal><br />
            <asp:Literal ID="LitSignIn" runat="server">0</asp:Literal><br />
            <div style="text-align: center">
                <a href="User/Default.aspx" target="_top">会员中心</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a href="User/Logout.aspx"
                    target="_top">退出登录</a></div>
        </div>
    </asp:Panel>
    <asp:Panel ID="PnlLogOnMessage" runat="server" Visible="false">
        <pe:ExtendedLiteral HtmlEncode="false" ID="LitErrorMessage" runat="server"></pe:ExtendedLiteral>
        <asp:Button ID="BtnReturn" runat="server" Text="返回" OnClick="BtnReturn_Click" />
    </asp:Panel>
    </form>
</body>
</html>
