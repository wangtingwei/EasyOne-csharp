<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Template.IncludeFileGuide" Codebehind="IncludeFileGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    内嵌代码管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        管理操作</div>
    <div class="guide">
        <ul>
            <li><a href="IncludeFile.aspx" target="main_right">添加内嵌代码</a></li>
            <li><a href="IncludeFileManage.aspx" target="main_right">内嵌代码管理</a></li>
        </ul>
    </div>
</asp:Content>
