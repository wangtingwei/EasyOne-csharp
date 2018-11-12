﻿<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.User.MessageRead" Codebehind="MessageRead.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>阅读短消息</title>
</head>
<body>
    <pe:UserNavigation Tab="message" ID="UserCenterNavigation" runat="server" />
    <pe:ExtendedSiteMapPath ID="YourPosition" SiteMapProvider="UserMapProvider" runat="server" />
    <form id="form1" runat="server">
        <table width="100%" border="0" cellpadding="2" cellspacing="1" class="TableWrap">
            <tr align="center">
                <td class="spacingtitle" style="height: 23px;">
                    <b>阅读短消息</b></td>
            </tr>
            <tr class="tdbg">
                <td align="left" style="height: 28px;">
                    <strong>发件人：</strong>
                    <asp:Label ID="LblSender" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr class="tdbg">
                <td align="left" style="height: 28px;">
                    <strong>收件人：</strong>
                    <asp:Label ID="LblIncept" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr class="tdbg">
                <td align="left" style="height: 28px;">
                    <strong>发送时间：</strong>
                    <asp:Label ID="LblSendTime" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr class="tdbg">
                <td align="left" style="height: 28px;" class="TdWrap">
                    <strong>消息主题：</strong>
                    <asp:Label ID="LblTitle" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr class="tdbg">
                <td style="height: 28px;">
                    <strong>消息内容：</strong>
                </td>
            </tr>
            <tr class="tdbg">
                <td align="left" class="TdWrap">
                    <pe:ExtendedLabel HtmlEncode="false" ID="LblContent" runat="server" Text="Label"></pe:ExtendedLabel></td>
            </tr>
            <tr class="tdbg">
                <td align="left" style="height: 28px; text-align: center">
                    <asp:Button ID="BtnDelete" runat="server" Text="删除" OnClick="BtnDelete_Click" OnClientClick="return confirm('是否要删除此短消息？')" />
                    <asp:Button ID="BtnWrite" runat="server" Text="撰写" OnClick="BtnWrite_Click" />
                    <asp:Button ID="BtnReply" runat="server" Text="回复" OnClick="BtnReply_Click" />
                    <asp:Button ID="BtnForward" runat="server" Text="转发" OnClick="BtnForward_Click" />
                    <asp:Button ID="BtnReturn" runat="server" OnClick="BtnReturn_Click" Text="返回" /></td>
            </tr>
        </table>
    </form>
</body>
</html>
