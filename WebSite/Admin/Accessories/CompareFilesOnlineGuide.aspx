<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Accessories.CompareFilesOnlineGuide"
    Title="在线比较网站文件向导" Codebehind="CompareFilesOnlineGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    在线比较网站文件
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        管理操作</div>
    <div class="guide">
        <ul>
            <li><a href="CompareFilesOnline.aspx?IsShowAll=0" target="main_right">显示全部比较结果</a></li>
            <li><a href="CompareFilesOnline.aspx?IsShowAll=1" target="main_right">只显示差异部分文件</a></li>
        </ul>
    </div>
</asp:Content>
