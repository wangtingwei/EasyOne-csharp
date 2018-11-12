<%@ Page MasterPageFile="~/Admin/Guide.master" Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.User.RoleGuide" Codebehind="RoleGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="server">
    角色管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="server">
    <div class="guideexpand" onclick="Switch(this)">
        角色管理</div>
    <div class="guide">
        <ul>
            <li><a href="RoleManage.aspx" target="main_right">角色管理</a></li>
            <li><a href="Role.aspx" target="main_right">添加角色</a> </li>
        </ul>
    </div>
</asp:Content>
