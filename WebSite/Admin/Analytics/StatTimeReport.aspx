<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Analytics.StatTimeResport" Title="无标题页" Codebehind="StatTimeReport.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="width: 80%">
                <pe:ExtendedSiteMapPath ID="SmpNavigator" runat="server" SiteMapProvider="AdminMapProvider">
                </pe:ExtendedSiteMapPath>
            </td>
            <td style="width: 20%; text-align: right">
                有效统计：<asp:Label ID="LblCount" runat="server" ForeColor="Red"></asp:Label>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="server">
    <asp:PlaceHolder ID="PlhStat" runat="server" EnableViewState="False"></asp:PlaceHolder>
</asp:Content>
