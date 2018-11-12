<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.AD.PreviewAD" Title="预览版位JS效果" Buffer="false" Codebehind="PreviewAD.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" align="center" cellpadding="2" cellspacing="1" class="border">
        <tr class="title">
            <td colspan="2" align="center">
                <strong>预览版位JS效果</strong></td>
        </tr>
        <tr class="tdbg2">
            <td style="height: 25px" align="center">
                <a href="javascript:this.location.reload();">刷新页面</a>&nbsp;&nbsp;&nbsp;&nbsp; <a
                    href="ADZoneManage.aspx">返回上页</a>
            </td>
        </tr>
        <tr valign="top">
            <td>
                <div style="height: 400px" id="ShowJS" runat="server">
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
