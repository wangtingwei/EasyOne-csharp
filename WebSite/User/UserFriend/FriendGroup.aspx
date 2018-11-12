<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.User.FriendGroup" Codebehind="FriendGroup.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>创建新组</title>
</head>
<body>
    <pe:UserNavigation ID="UserCenterNavigation" Tab="friend" runat="server" />
    <pe:ExtendedSiteMapPath ID="YourPosition" SiteMapProvider="UserMapProvider" runat="server" />
    <form id="form1" runat="server">
        <table width="100%" cellpadding="2" cellspacing="1" class="border">
            <tr align="center" class="spacingtitle">
                <td colspan="2">
                    <asp:Label ID="LblTitle" runat="server" Text="创建新组" Font-Bold="True" />
                </td>
            </tr>
            <tr class="tdbg">
                <td align="right" style="width: 40%;" class="tdbgleft">
                    名称：
                </td>
                <td>
                    <asp:TextBox ID="TxtGroupName" MaxLength="12" Width="160px" runat="server" />
                    <pe:RequiredFieldValidator ID="ValrGroupName" runat="server" Display="dynamic" ControlToValidate="TxtGroupName"
                        ErrorMessage="新创建的组名称不能为空！" />
                    <asp:RegularExpressionValidator ID="ValeGroupName" ControlToValidate="TxtGroupName"
                        Display="Dynamic" ValidationExpression="[^$]*" runat="server" ErrorMessage="组名称不能带有$字符" />
                </td>
            </tr>
            <tr class="tdbgbottom">
                <td colspan="2">
                    <asp:Button ID="BtnSubmit" runat="server" Text="创建" OnClick="BtnSubmit_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="button" onclick="javascript:history.go(-1)" value="取消" class="inputbutton" />
                </td>
            </tr>
        </table>
        <br />
        注： 网站限制创建8个分组
    </form>
</body>
</html>
