<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.AD.ModifyJSTemplate" ValidateRequest="false"
    Title="修改模板内容" Codebehind="ModifyJSTemplate.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" class="border" border="0" cellpadding="2" cellspacing="1">
        <tr align="center">
            <td class="spacingtitle">
                <b>修改模板内容</b>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="height: 350px" align="center">
                <asp:TextBox ID="TxtADTemplate" runat="server" Height="326px" TextMode="MultiLine"
                    Width="582px"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td style="height: 50px" align="center">
                <asp:Button ID="EBtnSaverTemplate" runat="server" Text="保存修改结果" OnClick="EBtnSaverTemplate_Click" />
            &nbsp;&nbsp;
        </tr>
    </table>
    <asp:HiddenField ID="HdnZoneType" runat="server" />
</asp:Content>
