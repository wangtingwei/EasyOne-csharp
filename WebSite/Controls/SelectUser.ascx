<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.SelectUser" Codebehind="SelectUser.ascx.cs" %>
<table>
    <tr>
        <td>
            <asp:RadioButton ID="RadUserType0" GroupName="UserType" runat="server" Checked="true"
                Text="���л�Ա" />
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td valign='top'>
            <asp:RadioButton ID="RadUserType1" GroupName="UserType" runat="server" Checked="false"
                Text="ָ����Ա��" />
        </td>
        <td>
            <asp:CheckBoxList ID="ChklUserGroupList" runat="server" RepeatColumns="5" Width="400px">
            </asp:CheckBoxList>
        </td>
    </tr>
    <tr>
        <td valign='top'>
            <asp:RadioButton ID="RadUserType2" GroupName="UserType" runat="server" Checked="false"
                Text="ָ����ԱID" />
        </td>
        <td>
            <asp:TextBox ID="TxtUserID" runat="server" Height="20px" Width="350px"></asp:TextBox>&nbsp;&nbsp;��","����ÿ����ԱID
        </td>
    </tr>
</table>
