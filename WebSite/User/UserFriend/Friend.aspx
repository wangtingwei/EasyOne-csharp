<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.User.Friend" Codebehind="Friend.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>添加成员</title>
</head>
<body>
    <pe:UserNavigation ID="UserCenterNavigation" Tab="friend" runat="server" />
    <pe:ExtendedSiteMapPath ID="YourPosition" SiteMapProvider="UserMapProvider" runat="server" />
    <form id="form1" runat="server">
        <div style="text-align: center">
            <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
                <tr align="center" class="spacingtitle">
                    <td colspan="2">
                        <asp:Label ID="LblTitle" runat="server" Text="添加好友" Font-Bold="True" />
                    </td>
                </tr>
                <tr class="tdbg">
                    <td align="right" style="width: 40%;" class="tdbgleft">
                        好友用户名：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="TxtFriendName" MaxLength="105" Width="150px" runat="server" />
                        <pe:RequiredFieldValidator ID="ValrFriendName" ControlToValidate="TxtFriendName"
                            runat="server" ErrorMessage="好友用户名不能为空！" />
                    </td>
                </tr>
                <tr class="tdbg">
                    <td align="right" style="width: 15%;" class="tdbgleft">
                        成 员 组：
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="DropFriendGroup" DataTextField="FriendGroupName" DataValueField="FriendGroupID"
                            runat="server" />
                    </td>
                </tr>
                <tr class="tdbgbottom">
                    <td colspan="2">
                        <asp:Button ID="BtnSubmit" runat="server" Text="添加" OnClick="BtnSubmit_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                        <input type="button" onclick="javascript:history.go(-1)" value="取消" class="inputbutton" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
