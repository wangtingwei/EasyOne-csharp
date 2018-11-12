<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Template.FrontPageTemplateConfigGuide" Title="无标题页" Codebehind="FrontPageTemplateConfigGuide.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="server">
动态页面模板配置
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="server">
    <div class="guideexpand" onclick="Switch(this)">
        管理操作</div>
    <div class="guide">
        <ul>
            <li><a href="FrontPageTemplateConfig.aspx" target="main_right">动态页面模板配置</a></li>
        </ul>
    </div>
</asp:Content>
