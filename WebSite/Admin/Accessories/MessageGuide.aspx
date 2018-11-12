<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Guide.master"
    Inherits="EasyOne.WebSite.Admin.Accessories.MessageGuide"
    Title="短消息管理向导" Codebehind="MessageGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    短消息管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        管理操作</div>
    <div class="guide">
        <ul>
            <li><a href="MessageManage.aspx?action=me" target="main_right">我的短消息</a></li>            
            <li><a href="MessageManage.aspx" target="main_right">短消息管理</a></li>
            <li><a href="MessageSend.aspx" target="main_right">发送短消息</a></li>
            <li><a href="MessageBatchDel.aspx" target="main_right">批量删除短消息</a></li>
        </ul>
    </div>
</asp:Content>
