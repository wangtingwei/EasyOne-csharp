<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Guide.master"
    Inherits="EasyOne.WebSite.Admin.Accessories.PayPlatformGuide"
    Title="����֧��ƽ̨������" Codebehind="PayPlatformGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    ����֧��ƽ̨����
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        �������</div>
    <div class="guide">
        <ul>
            <li><a href="PayPlatformManage.aspx" target="main_right">����֧��ƽ̨����</a></li>
            <li><a href="PayPlatform.aspx" target="main_right">�������֧��ƽ̨</a></li>
        </ul>
    </div>
</asp:Content>
