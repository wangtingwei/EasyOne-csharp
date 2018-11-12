<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Collection.Guide"
    Title="采集管理向导" Codebehind="Guide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    采集管理向导
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        采集管理操作</div>
    <div class="guide">
        <ul>
            <li><a href="CollectionMain.aspx" target="main_right">开始采集</a></li>
            <li><a href="ConfigStep1.aspx?ModelId=1&NodeId=1" target="main_right">添加采集项目</a></li>
            <li><a href="ItemManage.aspx" target="main_right">采集项目管理</a></li>
            <li><a href="HistoryManage.aspx" target="main_right">采集历史记录</a></li>
            <li><a href="CollectionFilter.aspx" target="main_right">添加采集过滤</a></li>
            <li><a href="CollectionFilterManage.aspx" target="main_right">管理采集过滤</a></li>
            <li><a href="Exclosion.aspx" target="main_right">添加采集排除规则</a></li>
            <li><a href="ExclosionManage.aspx" target="main_right">管理采集排除规则</a></li>
            <li><a href="CollectionProc.aspx" target="main_right">查看采集进度</a></li>
        </ul>
    </div>
</asp:Content>
