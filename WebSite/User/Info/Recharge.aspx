<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.User.Info.Recharge" Codebehind="Recharge.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>充值卡充值</title>
</head>
<body>
    <pe:UserNavigation ID="UserCenterNavigation" Tab="charge" runat="server" />
    <pe:ExtendedSiteMapPath ID="YourPosition" SiteMapProvider="UserMapProvider" runat="server" />
    <form id="form1" runat="server">
        <table width="100%" cellpadding="2" cellspacing="1" class="border">
            <tr class="spacingtitle">
                <td align="center" colspan="2">
                    <strong>充值卡充值</strong>
                </td>
            </tr>
            <pe:ShowUserInfo ID="showUserInfo" runat="server" />
            <tr class="tdbg">
                <td align="right" class="tdbgleft" style="width: 15%">
                    充值卡卡号：
                </td>
                <td>
                    <asp:TextBox ID="TxtCardNum" runat="server" />
                    <pe:RequiredFieldValidator ID="ValrCardNum" runat="server" ControlToValidate="TxtCardNum"
                        Display="Dynamic" ErrorMessage="请输入充值卡卡号！" SetFocusOnError="True" />
                </td>
            </tr>
            <tr class="tdbg">
                <td align="right" class="tdbgleft">
                    充值卡密码：
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtPassword" runat="server" TextMode="Password" />
                    <pe:RequiredFieldValidator ID="ValrPwd" runat="server" ControlToValidate="TxtPassword"
                        Display="Dynamic" ErrorMessage="请输入充值卡密码！" SetFocusOnError="True" />
                </td>
            </tr>
            <tr class="tdbgbottom">
                <td colspan="2">
                    <asp:Button ID="BtnSave" runat="server" Text="执行充值" OnClick="BtnSave_Click" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
