<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Guide.master"
    Inherits="EasyOne.WebSite.Admin.Accessories.PayPlatformGuide"
    Title="在线支付平台管理向导" Codebehind="PayPlatformGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    在线支付平台管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        管理操作</div>
    <div class="guide">
        <ul>
            <li><a href="PayPlatformManage.aspx" target="main_right">在线支付平台管理</a></li>
            <li><a href="PayPlatform.aspx" target="main_right">添加在线支付平台</a></li>
        </ul>
    </div>
</asp:Content>
