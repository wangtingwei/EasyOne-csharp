<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master"
    Inherits="EasyOne.WebSite.Admin.Contents.StatusUI" Title="添加稿件状态码" Codebehind="Status.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center" class="spacingtitle">
            <td colspan="2">
                <pe:AlternateLiteral ID="AlternateLiteral1" Text="添加稿件状态码" AlternateText="修改稿件状态码"
                    runat="Server" />
            </td>
        </tr>
        <tr class="tdbg">
            <td align="right" class="tdbgleft" style="width: 15%;">
                录入状态码：</td>
            <td>
                <asp:DropDownList ID="DropStatusCode" runat="server" Width="39px">
                </asp:DropDownList></td>
        </tr>
        <tr class="tdbg">
            <td align="right" class="tdbgleft">
                状态码名称：</td>
            <td>
                <asp:TextBox ID="TxtStatusName" runat="server" Text="" Columns="50"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrStatusName" ControlToValidate="TxtStatusName"
                    runat="server" ErrorMessage="稿件状态码名称不能为空"></pe:RequiredFieldValidator></td>
        </tr>
        <tr align="center" class="tdbg">
            <td style="height: 40px;" colspan="2">
                <asp:Button ID="EBtnSubmit" Text="保存稿件状态码" runat="server" OnClick="EBtnSubmit_Click" />
            </td>
        </tr>
        <asp:HiddenField ID="HdnStatusCode" runat="server" />
        <asp:HiddenField ID="HdnAction" runat="server" />
    </table>
</asp:Content>
