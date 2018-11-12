<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Controls.ContentCharge" Codebehind="ContentCharge.ascx.cs" %>
<tr class='tdbg'>
    <td style="width: 150px;" align='right' class='tdbgleft'>
        <strong>阅读权限：&nbsp;</strong></td>
    <td>
        <asp:Literal ID="LitInfoPurview" runat="server">不重复收费</asp:Literal>
    </td>
</tr>
<tr class='tdbg'>
    <td style="width: 150px;" align='right' class='tdbgleft'>
        <strong>消费点数：&nbsp;</strong></td>
    <td style="height: 17px">
        <asp:Label ID="LblInfoPoint" runat="server" Text="0"></asp:Label></td>
</tr>
<tr class='tdbg'>
    <td style="width: 150px;" align='right' class='tdbgleft'>
        <strong>重复收费：&nbsp;</strong></td>
    <td>
        <asp:Label ID="LblChargeType" runat="server" Text="0"></asp:Label>
    </td>
</tr>
<tr class='tdbg'>
    <td style="width: 150px;" align='right' class='tdbgleft'>
        <strong>分成比例：&nbsp;</strong></td>
    <td>
        <asp:Label ID="LblDividePercent" runat="server" Text="0"></asp:Label></td>
</tr>
