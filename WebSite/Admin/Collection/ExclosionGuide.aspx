<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Collection.CollectionExclosionGuide"
    Title="采集排除管理向导" Codebehind="ExclosionGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    采集排除规则管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        采集排除规则操作</div>
    <div class="guide">
        <ul>
            <li><a href="Exclosion.aspx" target="main_right">添加采集排除规则</a></li>
            <li><a href="ExclosionManage.aspx" target="main_right">管理采集排除规则</a></li>
        </ul>
    </div>
</asp:Content>
