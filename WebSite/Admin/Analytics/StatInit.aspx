<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Analytics.StatInit" Title="数据初始化" Codebehind="StatInit.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellspacing="1" cellpadding="2" class="border">
        <tr align="center">
            <td class="spacingtitle">
                <strong>数据初始化</strong>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="height: 150px">
                <p style="text-align: center">
                    <strong><span style="color: #ff0000">请慎用此功能，因为一旦清除将无法恢复！</span></strong>
                    <br />
                    此操作将清除数据库中的所有统计数据，用于系统初始化时及需要对网站的访问统计数据进行重新统计时使用。
                </p>
                <p style="text-align: center">
                    <asp:Button ID="BtnInit" runat="server" Text="统计数据初始化" OnClick="BtnInit_Click" OnClientClick="return confirm('确实要初始化吗？一旦清除将无法恢复！')" />
                    &nbsp;</p>
            </td>
        </tr>
    </table>
</asp:Content>
