<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master"
    Inherits="EasyOne.WebSite.Admin.Accessories.MessageRead" Codebehind="MessageRead.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <b>会员短消息</b></td>
        </tr>
        <tr class="tdbg">
            <td align="left" style="height: 28px; width: 45%;">
                <strong>发件人：</strong>
                <asp:Label ID="LblSender" runat="server" Text="Label"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td align="left" style="height: 28px; width: 45%;">
                <strong>收件人：</strong>
                <asp:Label ID="LblIncept" runat="server" Text="Label"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td align="left" style="height: 28px; width: 45%;">
                <strong>发送时间：</strong>
                <asp:Label ID="LblSendTime" runat="server" Text="Label"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td align="left" style="height: 28px; width: 45%;">
                <strong>消息主题：</strong>
                <asp:Label ID="LblTitle" runat="server" Text="Label"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td align="left" style="height: 28px; width: 45%;">
                <strong>消息内容：</strong>
            </td>
        </tr>
        <tr class="tdbg">
            <td align="left" style="height: 28px; width: 45%;">
                <pe:ExtendedLabel HtmlEncode="false" ID="LblContent" runat="server" Text="Label"></pe:ExtendedLabel></td>
        </tr>
        <tr class="tdbg">
            <td align="left" style="height: 28px; width: 45%; text-align: center">
                <asp:Button ID="BtnDelete" runat="server" Visible="false" Text="删除" OnClick="BtnDelete_Click" OnClientClick="return confirm('是否要删除此短消息？')" />
                <asp:Button ID="BtnReply" runat="server" Visible="false" Text="回复" OnClick="BtnReply_Click" />
                <asp:Button ID="BtnForward" runat="server" Visible="false" Text="转发" OnClick="BtnForward_Click" />
                <asp:Button ID="BtnReturn" runat="server" OnClick="BtnReturn_Click" Text="返回" />
            </td>
        </tr>
    </table>
</asp:Content>
