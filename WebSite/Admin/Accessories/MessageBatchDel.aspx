<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master"
    Inherits="EasyOne.WebSite.Admin.Accessories.MessageBatchDel" Codebehind="MessageBatchDel.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <b>����ɾ������</b>
            </td>
        </tr>
        <tr class="tdbg">
            <td align="right" style="height: 28px; width: 45%;">
                <strong>����ɾ����Ա�������ˣ�����Ϣ��<br />
                </strong>������Ӣ��״̬�µĶ��Ž��û�������ʵ�ֶ��Աͬʱɾ��</td>
            <td style="height: 28px; width: 511px;">
                <asp:TextBox ID="TxtSender" runat="server"></asp:TextBox>
                <asp:Button ID="BtnDelSender" runat="server" Text="ɾ��" OnClientClick="return confirm('ȷ��Ҫɾ����');"
                    OnClick="BtnDelSender_Click" /></td>
        </tr>
        <tr class="tdbg">
            <td align="right" style="height: 28px; width: 45%;">
                <strong>����ɾ��ָ�����ڷ�Χ�ڵĶ���Ϣ��<br />
                </strong>Ĭ��Ϊɾ���Ѷ���Ϣ</td>
            <td style="height: 28px; width: 511px;">
                <asp:DropDownList ID="DropDelDate" runat="server">
                    <asp:ListItem Value="1">һ��ǰ</asp:ListItem>
                    <asp:ListItem Value="3">����ǰ</asp:ListItem>
                    <asp:ListItem Value="7">һ����ǰ</asp:ListItem>
                    <asp:ListItem Value="30">һ����ǰ</asp:ListItem>
                    <asp:ListItem Value="60">������ǰ</asp:ListItem>
                    <asp:ListItem Value="180">����ǰ</asp:ListItem>
                    <asp:ListItem Value="0">���ж���Ϣ</asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="BtnDelDate" runat="server" Text="ɾ��" OnClientClick="return confirm('ȷ��Ҫɾ����');"
                    OnClick="BtnDelDate_Click" /></td>
        </tr>
    </table>
</asp:Content>
