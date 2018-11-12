<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master" Title="风格管理向导" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Template.StyleGuide" Codebehind="StyleGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    风格管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        管理操作</div>
    <div class="guide">
        <ul>
            <li><a href="StyleManage.aspx" target="main_right">风格管理</a></li>
        </ul>
    </div>
</asp:Content>
