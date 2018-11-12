<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.User.RoleUI" Title="角色管理" Codebehind="Role.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <b>
                    <pe:AlternateLiteral ID="LblTitle" Text="添加角色" AlternateText="修改角色" runat="Server" /></b>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>角色名：</strong></td>
            <td>
                <asp:TextBox ID="TxtRoleName" runat="server"></asp:TextBox>
                <pe:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TxtRoleName"
                    ErrorMessage="角色名不能为空！" runat="server"></pe:RequiredFieldValidator></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="height: 79px">
                <strong>角色描述：</strong></td>
            <td style="height: 79px">
                <asp:TextBox ID="TxtDescription" TextMode="MultiLine" runat="server" Height="70px"
                    Width="300px" MaxLength="200"></asp:TextBox>
            </td>
        </tr>
    </table>
    <br />
    <center>
        <asp:Button ID="BtnSubmit" runat="server" Text="保存角色并进行权限设置" OnClick="BtnSubmit_Click" />&nbsp;&nbsp;
        <asp:Button ID="BtnCancle" runat="server" Text="返回角色管理" ValidationGroup="BtnCancleValidationGroup"
            OnClick="BtnCancle_Click" />
    </center>
</asp:Content>
