<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Collection.CollectionFilterGuide"
    Title="采集过滤管理向导" Codebehind="CollectionFilterGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    采集过滤管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        采集过滤操作</div>
    <div class="guide">
        <ul>
            <li><a href="CollectionFilter.aspx" target="main_right">添加采集过滤</a></li>
            <li><a href="CollectionFilterManage.aspx" target="main_right">管理采集过滤</a></li>
        </ul>
    </div>
</asp:Content>
