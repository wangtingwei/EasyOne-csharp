<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Analytics.ShowClientDetail"
    Title="访问记录详情" Codebehind="ShowClientDetail.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" runat="server" SiteMapProvider="AdminMapProvider" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="server">
    <table border="0" cellpadding="2" cellspacing="1" class="border" style="width: 100%;">
        <tr class="title">
            <td colspan="2" style="text-align: center;">
                访问记录详情</td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 30%;">
                <strong>访问时间（以服务器端时区记）：</strong></td>
            <td class="tdbg">
                <asp:Label ID="LblVTime" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>访问者IP：</strong></td>
            <td class="tdbg">
                <asp:Label ID="LblIP" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>访问者所在时区：</strong></td>
            <td class="tdbg">
                <asp:Label ID="LblTimezone" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>所在地址：</strong></td>
            <td class="tdbg">
                <asp:Label ID="LblAddress" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>访问时间（以客户端时区记）：</strong></td>
            <td class="tdbg">
                <asp:Label ID="LblClientTime" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>链接页面：</strong></td>
            <td class="tdbg">
                <asp:Label ID="LblReferer" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>操作系统：</strong></td>
            <td class="tdbg">
                <asp:Label ID="LblSystem" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>浏览器：</strong></td>
            <td class="tdbg">
                <asp:Label ID="LblBrowser" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>屏幕大小：</strong></td>
            <td class="tdbg">
                <asp:Label ID="LblScreen" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>屏幕色深：</strong></td>
            <td class="tdbg">
                <asp:Label ID="LblColor" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td align="center" class="tdbg" colspan="2">
                <input id="Button1" type="button" class="inputbutton" value="返回" onclick="window.history.go(-1)" />&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
