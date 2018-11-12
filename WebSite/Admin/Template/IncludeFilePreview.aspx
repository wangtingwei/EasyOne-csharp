<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Template.IncludeFilePreview" Codebehind="IncludeFilePreview.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr class="tdbg" align="center">
            <td class="title" style="height:25px">
                <b>预览内嵌代码文件效果----<asp:Literal ID="LitName" runat="server"></asp:Literal>
                </b>
            </td>
        </tr>
        <tr class="tdbg">
            <td align="center">
                <pe:ExtendedLiteral HtmlEncode="false" ID="LitPreview" runat="server"></pe:ExtendedLiteral>
            </td>
        </tr>
        <tr class="tdbg">
            <td align="center">
                <a href="javascript:this.location.reload();">刷新本页</a>&nbsp;&nbsp;&nbsp;&nbsp;<a href="IncludeFileManage.aspx">返回上页</a>
            </td>
        </tr>
    </table>
</asp:Content>
