<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.User.RegisterCheck" Codebehind="RegisterCheck.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>注册用户认证</title>
</head>
<body>
    <pe:ExtendedSiteMapPath ID="YourPosition" SiteMapProvider="UserMapProvider" runat="server" />
    <form id="form1" runat="server">
        <div>
            <table width="760" border="0" align="center" cellpadding="0" cellspacing="0" class="center_tdbgall">
                <tr>
                    <td>
                        <br>
                        <table width="400" border="0" align="center" cellpadding="5" cellspacing="0" class="border">
                            <tr class="title">
                                <td colspan="2" align="center">
                                    <strong>注册用户认证</strong></td>
                            </tr>
                            <tr>
                                <td height="120" colspan="2" class="tdbg">
                                    请输入您注册时填写的用户名和密码，以及本站发给你的确认信中的随机验证码。必须完全正确后，你的帐户才会激活。
                                    <table width="250" border="0" cellspacing="8" cellpadding="0" align="center">
                                        <tr>
                                            <td align="right">
                                                用户名称：</td>
                                            <td>
                                                <asp:TextBox ID="TxtUserName" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                用户密码：</td>
                                            <td>
                                                <asp:TextBox ID="TxtPassword" TextMode="Password" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                随机验证码：</td>
                                            <td>
                                                <asp:TextBox ID="TxtCheckNum" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td colspan="2">
                                                <asp:Button ID="BtnRegCheck" runat="server" Text="验证" OnClick="BtnRegCheck_Click" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <br>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
