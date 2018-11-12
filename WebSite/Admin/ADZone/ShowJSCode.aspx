<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.AD.ShowJSCode" Title="广告版位管理" Codebehind="ShowJSCode.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr class="title">
            <td colspan="2" align="center">
                <strong>版位JS调用代码</strong></td>
        </tr>
        <tr class="tdbgleft">
            <td align="center">
            <span style="color:Green">调用方法：将下面的代码插入到网页中预定的广告位置</span></td>
        </tr>
        <tr class="tdbg">
            <td align="center" style="height: 27px">
                <asp:TextBox ID="TxtZoneCode" runat="server" Height="102px" TextMode="MultiLine"
                    Width="578px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td align="center">
                <a href="ADZoneManage.aspx">返回</a></td>
        </tr>
    </table>
</asp:Content>
