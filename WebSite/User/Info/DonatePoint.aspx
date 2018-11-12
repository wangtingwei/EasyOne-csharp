<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.User.Info.DonatePoint" Codebehind="DonatePoint.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>赠送点券</title>
</head>
<body>
    <pe:UserNavigation Tab="charge" ID="UserCenterNavigation" runat="server" />
    <pe:ExtendedSiteMapPath ID="YourPosition" SiteMapProvider="UserMapProvider" runat="server" />
    <form id="form1" runat="server">
        <table width="100%" cellpadding="2" cellspacing="1" class="border">
            <tr align="center" class="spacingtitle">
                <td colspan="2">
                    <strong>赠送<pe:ShowPointName ID="ShowPointName4" runat="server" /></strong>
                </td>
            </tr>
            <pe:ShowUserInfo ID="showUserInfo" runat="server" />
            <tr class="tdbg">
                <td align="right" style="width: 15%;" class="tdbgleft">
                    获赠人的用户名：
                </td>
                <td>
                    <asp:TextBox ID="TxtDonateUserName" runat="server" />
                    <pe:RequiredFieldValidator ID="ValrTxtDonateUserName" ControlToValidate="TxtDonateUserName"
                        runat="server" SetFocusOnError="true" Display="Dynamic" ErrorMessage="获赠人的用户名不能为空！" />
                </td>
            </tr>
            <tr class="tdbg">
                <td align="right" style="width: 15%;" class="tdbgleft">
                    赠送的<pe:ShowPointName ID="ShowPointName1" runat="server" />数：
                </td>
                <td>
                    <asp:TextBox ID="TxtPoint" runat="server" Columns="8" MaxLength="8" />
                    <pe:RequiredFieldValidator ID="ValrTxtPoint" ControlToValidate="TxtPoint" runat="server"
                        SetFocusOnError="true" Display="Dynamic" ErrorMessage="赠送的点券数不能为空！" />
                    <asp:RegularExpressionValidator ID="ValgTxtPoint" runat="server" ControlToValidate="TxtPoint"
                        ErrorMessage="赠送的点券数必须是大于0的整数！" ValidationExpression="^([1-9])(\d{0,})(\d{0,})$"
                        Display="Dynamic" />
                </td>
            </tr>
            <tr class="tdbgbottom">
                <td colspan="2">
                    <asp:Button ID="BtnSubmit" runat="server" Text="赠送" OnClick="BtnSubmit_Click" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
