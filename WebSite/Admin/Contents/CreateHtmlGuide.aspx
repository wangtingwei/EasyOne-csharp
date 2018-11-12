<%@ Page AutoEventWireup="true" MasterPageFile="~/Admin/Guide.master"
    Inherits="EasyOne.WebSite.Admin.Contents.CreateHtmlGuide" Language="C#" Title="生成管理向导" Codebehind="CreateHtmlGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    生成管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        管理操作</div>
    <div class="guide">
        <ul>
            <li><a id="EahCreateHtmlContent" href="CreateHtmlContent.aspx" runat="server" target="main_right">
                生成内容页</a></li>
            <li><a id="EahCreateHtmlSingle" href="CreateHtmlSingle.aspx" runat="server" target="main_right">
                生成单页节点</a></li>
            <li><a id="EahCreateHtmlNodes" href="CreateHtmlNodes.aspx" runat="server" target="main_right">
                生成栏目列表页</a></li>
            <li><a id="EahCreateHtmlSpecialCatecory" href="CreateHtmlSpecialCategory.aspx" runat="server"
                target="main_right">生成专题类别页</a></li>
            <li><a id="EahCreateHtmlSpecial" href="CreateHtmlSpecial.aspx" runat="server" target="main_right">
                生成专题列表页</a></li>
            <li><a id="EahOtherCeate" href=" SiteOtherCreate.aspx" runat="server" target="main_right">
                生成网站综合数据</a></li>
            <li><a id="EahCreateHtmlProgress" href="CreateHtmlProgress.aspx" runat="server" target="main_right">
                查看生成进度</a></li>
            <li><a id="EahAutoCreateHtmlContent" href="AutoCreateHtml.aspx" runat="server" target="main_right">
                定时生成配置</a></li>
        </ul>
    </div>
</asp:Content>
