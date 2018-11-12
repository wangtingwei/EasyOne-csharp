<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Accessories.CacheShow"
    Title="缓存详细信息" Codebehind="CacheShow.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="title" align="center">
                <asp:Label ID="LblTitle" runat="server" Text="Label" />
                缓存值
            </td>
        </tr>
        <tr class="tdbg">
            <td align="center">
                <asp:TextBox ID="TxtCacheContent" ReadOnly="true" TextMode="MultiLine" Height="300px"
                    Width="98%" runat="server" />
            </td>
        </tr>
        <tr class="tdbg">
            <td style="height: 40px; text-align: center;">
                <input name="Cancel" type="button" class="inputbutton" id="Cancel" value="返回缓存管理"
                    onclick="Redirect('CacheManage.aspx')" />
            </td>
        </tr>
    </table>
</asp:Content>
