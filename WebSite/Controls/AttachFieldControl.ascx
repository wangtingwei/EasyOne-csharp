<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Controls.AttachFieldControl" Codebehind="AttachFieldControl.ascx.cs" %>
<tr class='tdbg'>
    <td class='tdbgleft'>
        <strong>是否启用站内链接功能：&nbsp;</strong></td>
    <td class='tdbg' align='left'>
        <asp:RadioButtonList ID="RadlInsideLink" runat="server" RepeatDirection="Horizontal">
            <asp:ListItem Value="True">是</asp:ListItem>
            <asp:ListItem Value="False" Selected="True">否</asp:ListItem>
        </asp:RadioButtonList>
    </td>
</tr>
<tr class='tdbg'>
    <td class='tdbgleft'>
        <strong>是否启用字符过滤功能：&nbsp;</strong></td>
    <td class='tdbg' align='left'>
        <asp:RadioButtonList ID="RadlFilterWord" runat="server" RepeatDirection="Horizontal">
            <asp:ListItem Value="True">是</asp:ListItem>
            <asp:ListItem Value="False" Selected="True">否</asp:ListItem>
        </asp:RadioButtonList>
    </td>
</tr>
<tr class='tdbg'>
    <td class='tdbgleft'>
        <strong>是否启用字符屏蔽功能：&nbsp;</strong></td>
    <td class='tdbg' align='left'>
        <asp:RadioButtonList ID="RadlShieldWord" runat="server" RepeatDirection="Horizontal">
            <asp:ListItem Value="True">是</asp:ListItem>
            <asp:ListItem Value="False" Selected="True">否</asp:ListItem>
        </asp:RadioButtonList>
    </td>
</tr>
