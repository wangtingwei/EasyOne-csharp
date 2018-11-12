<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Accessories.MailListGuide"
    Title="邮件列表管理向导" Codebehind="MailListGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    邮件列表管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        管理操作</div>
    <div class="guide">
        <ul>
            <li><a href="MailListSend.aspx" target="main_right">发送邮件</a></li>
            <li><a href="MailListExport.aspx" target="main_right">导出邮件</a></li>
        </ul>
    </div>
</asp:Content>
