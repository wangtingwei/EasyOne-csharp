<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.SelectUser" Codebehind="SelectUser.ascx.cs" %>
<table>
    <tr>
        <td>
            <asp:RadioButton ID="RadUserType0" GroupName="UserType" runat="server" Checked="true"
                Text="所有会员" />
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td valign='top'>
            <asp:RadioButton ID="RadUserType1" GroupName="UserType" runat="server" Checked="false"
                Text="指定会员组" />
        </td>
        <td>
            <asp:CheckBoxList ID="ChklUserGroupList" runat="server" RepeatColumns="5" Width="400px">
            </asp:CheckBoxList>
        </td>
    </tr>
    <tr>
        <td valign='top'>
            <asp:RadioButton ID="RadUserType2" GroupName="UserType" runat="server" Checked="false"
                Text="指定会员ID" />
        </td>
        <td>
            <asp:TextBox ID="TxtUserID" runat="server" Height="20px" Width="350px"></asp:TextBox>&nbsp;&nbsp;用","隔开每个会员ID
        </td>
    </tr>
</table>
