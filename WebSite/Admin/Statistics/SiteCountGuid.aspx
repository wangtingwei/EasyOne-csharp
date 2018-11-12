<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Statistics.SiteCountGuid" Title="无标题页" Codebehind="SiteCountGuid.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="server">
网站统计
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="server">
 <div class="guideexpand" onclick="Switch(this)">
        管理操作</div>
    <div class="guide">
        <ul>
            <li><a href="SiteCount.aspx?Action=CategoryMonth" target="main_right">按栏目/月份统计</a></li>
            <li><a href="SiteCount.aspx?Action=CategoryInputer" target="main_right">按栏目/录入者统计</a></li>
            <li><a href="SiteCount.aspx?Action=CategoryManager" target="main_right">按栏目/审核者统计</a></li>
            <li><a href="SiteCount.aspx?Action=InputerMonth" target="main_right">按录入者/月份统计</a></li>
            <li><a href="SiteCount.aspx?Action=ManagerMonth" target="main_right">按审核者/月份统计</a></li>
            
        </ul>
    </div>
</asp:Content>
