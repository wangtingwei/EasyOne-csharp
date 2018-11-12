<%@ Page Language="C#" AutoEventWireup="True" Inherits="EasyOne.WebSite.Admin.Accessories.DownServerGuide"
    MasterPageFile="~/Admin/Guide.master" Title="其他管理向导" Codebehind="DownServerGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    下载服务器管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        管理操作</div>
    <div class="guide">
        <ul>
            <li>
                <pe:ExtendedAnchor ID="EahAuthorAdd" IsChecked="true" OperateCode="DownServerManage"
                    href="DownServer.aspx" runat="server" target="main_right">添加下载服务器</pe:ExtendedAnchor></li>
            <li>
                <pe:ExtendedAnchor ID="EahAuthorManage" IsChecked="true" OperateCode="DownServerManage"
                    href="DownServerManage.aspx" runat="server" target="main_right">下载服务器管理</pe:ExtendedAnchor></li>
            <li>
                <pe:ExtendedAnchor ID="EahAuthorOrder" IsChecked="true" OperateCode="DownServerManage"
                    href="DownServerOrder.aspx" runat="server" target="main_right">下载服务器排序</pe:ExtendedAnchor></li>
        </ul>
    </div>
</asp:Content>
