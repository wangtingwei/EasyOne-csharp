<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Controls.AttachFieldControl" Codebehind="AttachFieldControl.ascx.cs" %>
<tr class='tdbg'>
    <td class='tdbgleft'>
        <strong>�Ƿ�����վ�����ӹ��ܣ�&nbsp;</strong></td>
    <td class='tdbg' align='left'>
        <asp:RadioButtonList ID="RadlInsideLink" runat="server" RepeatDirection="Horizontal">
            <asp:ListItem Value="True">��</asp:ListItem>
            <asp:ListItem Value="False" Selected="True">��</asp:ListItem>
        </asp:RadioButtonList>
    </td>
</tr>
<tr class='tdbg'>
    <td class='tdbgleft'>
        <strong>�Ƿ������ַ����˹��ܣ�&nbsp;</strong></td>
    <td class='tdbg' align='left'>
        <asp:RadioButtonList ID="RadlFilterWord" runat="server" RepeatDirection="Horizontal">
            <asp:ListItem Value="True">��</asp:ListItem>
            <asp:ListItem Value="False" Selected="True">��</asp:ListItem>
        </asp:RadioButtonList>
    </td>
</tr>
<tr class='tdbg'>
    <td class='tdbgleft'>
        <strong>�Ƿ������ַ����ι��ܣ�&nbsp;</strong></td>
    <td class='tdbg' align='left'>
        <asp:RadioButtonList ID="RadlShieldWord" runat="server" RepeatDirection="Horizontal">
            <asp:ListItem Value="True">��</asp:ListItem>
            <asp:ListItem Value="False" Selected="True">��</asp:ListItem>
        </asp:RadioButtonList>
    </td>
</tr>
