<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Accessories.CacheManageGuide" Title="缓存管理" Codebehind="CacheManageGuide.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="server">
缓存管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="server">
    <div class="guideexpand" onclick="Switch(this)">
        管理操作</div>
    <div class="guide">
        <ul>
            <li><a href="CacheManage.aspx" target="main_right">缓存管理</a></li>
            <li><a href="CacheManage.aspx?CacheType=1" target="main_right">节点设置缓存</a></li>
            <li><a href="CacheManage.aspx?CacheType=2" target="main_right">模型缓存</a></li>
            <li><a href="CacheManage.aspx?CacheType=3" target="main_right">模板标签缓存</a></li>
            <li><a href="CacheManage.aspx?CacheType=4" target="main_right">节点页缓存</a></li>
        </ul>
    </div>
</asp:Content>
