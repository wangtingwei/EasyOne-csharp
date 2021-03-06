﻿<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.User.ValidLogDetail"
    Title="无标题页" Codebehind="ValidLogDetail.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <b>查看有效期明细记录详情</b>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width: 15%" class="tdbgleft" align="right">
                时间：</td>
            <td>
                <asp:Label ID="LblLogTime" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                用户名：</td>
            <td>
                <asp:Label ID="LblUserName" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                IP地址：</td>
            <td>
                <asp:Label ID="LblIP" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                有效期：</td>
            <td>
                <asp:Label ID="LblIncomePayOut" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                操作员：</td>
            <td>
                <asp:Label ID="LblInputer" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                备注/说明：</td>
            <td>
                <asp:Label ID="LblRemark" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                内部记录：</td>
            <td style="word-break: break-all; overflow: hidden;">
                <asp:Label ID="LblMemo" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td colspan="2" align="center">
                <input type="button" value="返回" class="inputbutton" onclick="Redirect('ValidLog.aspx')" />
            </td>
        </tr>
    </table>
</asp:Content>
