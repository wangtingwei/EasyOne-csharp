<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.ShowUserInfo" Codebehind="ShowUserInfo.ascx.cs" %>
<tr class='tdbg'>
    <td align='right' style="width: 15%;" class='tdbgleft'>
        ��Ա����</td>
    <td align='left'>
        <asp:Label ID="LblUserName" runat="server" Text="" />
    </td>
</tr>
<tr class='tdbg'>
    <td align='right' style="width: 15%;" class='tdbgleft'>
        ������Ա�飺</td>
    <td align='left'>
        <asp:Label ID="LblUserGroup" runat="server" Text="" />
    </td>
</tr>
<tr class='tdbg' runat="server" id="Balance">
    <td align='right' style="width: 15%;" class='tdbgleft'>
        �ʽ���</td>
    <td align='left'>
        <asp:Label ID="LblBalance" runat="server" Text="" />
        Ԫ
    </td>
</tr>
<tr class='tdbg' runat="server" id="Point">
    <td align='right' style="width: 15%;" class='tdbgleft'>
        <pe:ShowPointName ID="ShowPointName1" runat="server" />����</td>
    <td align='left'>
        <asp:Label ID="LblPoint" runat="server" Text="" />
        <pe:ShowPointName ID="ShowPointName2" PointType="PointUnit" runat="server" />
    </td>
</tr>
<tr class='tdbg' runat="server" id="UserExp">
    <td align='right' style="width: 15%;" class='tdbgleft'>
        ���֣�</td>
    <td align='left'>
        <asp:Label ID="LblUserExp" runat="server" Text="" />
        ��
    </td>
</tr>
<tr class='tdbg' runat="server" id="EndTime">
    <td style="width: 15%;" class='tdbgleft' align='right'>
        ��Ч����Ϣ��</td>
    <td align='left'>
        ��ֹ���ڣ�<pe:ExtendedLabel HtmlEncode="false" ID="LblEndTime" runat="server" Text="" />&nbsp;&nbsp;&nbsp;&nbsp;
        ʣ��������<pe:ExtendedLabel HtmlEncode="false" ID="LblValidDays" runat="server" Text="" />&nbsp;��
    </td>
</tr>
