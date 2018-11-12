<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Accessories.LogDetail"
    Title="网站日志详细信息" Codebehind="LogDetail.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" runat="server" SiteMapProvider="AdminMapProvider" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="server">
    <table border="0" cellpadding="0" cellspacing="1" width="100%" class="border">
        <tr>
            <td align="center" class="title" colspan="2">
                网站日志详细信息</td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 30%;">
                <strong>日志序号：</strong></td>
            <td>
                <asp:Label ID="LblLogId" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>日志类型：</strong></td>
            <td>
                <asp:Label ID="LblCategory" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>日志优先级别：</strong></td>
            <td>
                <asp:Label ID="LblPriority" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>发生异常的页面：</strong></td>
            <td>
                <asp:Label ID="LblScriptName" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg" style="color: #000000">
            <td class="tdbgleft">
                <strong>日志记录时间：</strong></td>
            <td>
                <asp:Label ID="LblTimestamp" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg" style="color: #000000">
            <td class="tdbgleft">
                <strong>用户名：</strong></td>
            <td>
                <asp:Label ID="LblUserName" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg" style="color: #000000">
            <td class="tdbgleft">
                <strong>用户IP：</strong></td>
            <td>
                <asp:Label ID="LblUserIP" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg" style="color: #000000">
            <td class="tdbgleft">
                <strong>用户提交的信息：</strong></td>
            <td>
                <asp:Label ID="LblPostString" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg" style="color: #000000">
            <td class="tdbgleft">
                <strong>日志标题：</strong></td>
            <td>
                <asp:Label ID="LblTitle" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>日志内容：</strong></td>
            <td>
                <asp:Label ID="LblMessage" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>异常源、堆栈跟踪等异常信息：</strong></td>
            <td style="word-break: break-all;">
                <asp:Label ID="LblSource" runat="server"></asp:Label></td>
        </tr>
    </table>
    <p style="text-align: center">
        <input type="button" class="inputbutton" value="返回" onclick="window.history.go(-1)" />
    </p>
</asp:Content>
