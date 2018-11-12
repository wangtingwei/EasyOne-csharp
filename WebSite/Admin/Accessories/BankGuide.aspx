<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master" Inherits="EasyOne.WebSite.Admin.Accessories.BankGuide" AutoEventWireup="true" Title="银行账户管理向导" Codebehind="BankGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    银行账户管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        管理操作</div>
    <div class="guide">
        <ul>
            <li><a href="BankManage.aspx" target="main_right">银行账户管理</a></li>
            <li><a href="Bank.aspx" target="main_right">添加银行账户</a></li>
        </ul>
    </div>
</asp:Content>
