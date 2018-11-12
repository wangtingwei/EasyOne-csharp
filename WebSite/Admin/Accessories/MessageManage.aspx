<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master"
    Inherits="EasyOne.WebSite.Admin.Accessories.MessageManage" Codebehind="MessageManage.aspx.cs" %>

<%@ Register TagPrefix="pe" Src="~/Controls/ShowMessageListAdmin.ascx" TagName="ShowMessageListAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ShowMessageListAdmin ID="ShowAdminMessageList" runat="server" />
    <br />
    <br />
    <table width="100%" cellpadding="3" cellspacing="0" class="border">
        <tr class="tdbg">
            <td align="right" style="width: 15%;">
                <strong>短消息搜索：</strong>
            </td>
            <td>
                <asp:DropDownList ID="DropSearch" runat="server">
                    <asp:ListItem Value="0">搜索类型</asp:ListItem>
                    <asp:ListItem Value="1">短消息主题</asp:ListItem>
                    <asp:ListItem Value="2">短消息内容</asp:ListItem>
                    <asp:ListItem Value="3">收件人</asp:ListItem>
                    <asp:ListItem Value="4">发件人</asp:ListItem>
                    <asp:ListItem Value="5">某个人</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="TxtKeyword" runat="server">关键字</asp:TextBox>
                <asp:Button ID="BtnSubmit" runat="server" Text="搜索" OnClick="BtnSubmit_Click" />
                <pe:RequiredFieldValidator ID="ValrKeyword" runat="server" ControlToValidate="TxtKeyWord"
                    ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" ShowRequiredText="false">请输入搜索的关键字！</pe:RequiredFieldValidator></td>
        </tr>
    </table>
    <br />
</asp:Content>
