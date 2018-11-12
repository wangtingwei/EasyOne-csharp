<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.User.UserGroupGuide"
    Title="会员组管理向导" Codebehind="UserGroupGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    会员组管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        会员组管理</div>
    <div class="guide">
        <ul>
            <li>
                <pe:ExtendedAnchor ID="EahUserGroupsManage" IsChecked="true" OperateCode="UserGroupManage"
                    href="UserGroupManage.aspx" runat="server" target="main_right">会员组管理首页</pe:ExtendedAnchor></li>
            <li>
                <pe:ExtendedAnchor ID="EahUserGroupsAdd" IsChecked="true" OperateCode="UserGroupManage"
                    href="UserGroup.aspx" runat="server" target="main_right">添加会员组</pe:ExtendedAnchor></li>
        </ul>
    </div>
</asp:Content>
