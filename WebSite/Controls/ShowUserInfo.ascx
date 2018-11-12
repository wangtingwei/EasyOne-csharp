<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.ShowUserInfo" Codebehind="ShowUserInfo.ascx.cs" %>
<tr class='tdbg'>
    <td align='right' style="width: 15%;" class='tdbgleft'>
        会员名：</td>
    <td align='left'>
        <asp:Label ID="LblUserName" runat="server" Text="" />
    </td>
</tr>
<tr class='tdbg'>
    <td align='right' style="width: 15%;" class='tdbgleft'>
        所属会员组：</td>
    <td align='left'>
        <asp:Label ID="LblUserGroup" runat="server" Text="" />
    </td>
</tr>
<tr class='tdbg' runat="server" id="Balance">
    <td align='right' style="width: 15%;" class='tdbgleft'>
        资金余额：</td>
    <td align='left'>
        <asp:Label ID="LblBalance" runat="server" Text="" />
        元
    </td>
</tr>
<tr class='tdbg' runat="server" id="Point">
    <td align='right' style="width: 15%;" class='tdbgleft'>
        <pe:ShowPointName ID="ShowPointName1" runat="server" />数：</td>
    <td align='left'>
        <asp:Label ID="LblPoint" runat="server" Text="" />
        <pe:ShowPointName ID="ShowPointName2" PointType="PointUnit" runat="server" />
    </td>
</tr>
<tr class='tdbg' runat="server" id="UserExp">
    <td align='right' style="width: 15%;" class='tdbgleft'>
        积分：</td>
    <td align='left'>
        <asp:Label ID="LblUserExp" runat="server" Text="" />
        分
    </td>
</tr>
<tr class='tdbg' runat="server" id="EndTime">
    <td style="width: 15%;" class='tdbgleft' align='right'>
        有效期信息：</td>
    <td align='left'>
        截止日期：<pe:ExtendedLabel HtmlEncode="false" ID="LblEndTime" runat="server" Text="" />&nbsp;&nbsp;&nbsp;&nbsp;
        剩余天数：<pe:ExtendedLabel HtmlEncode="false" ID="LblValidDays" runat="server" Text="" />&nbsp;天
    </td>
</tr>
