<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Accessories.MailListGuide"
    Title="�ʼ��б������" Codebehind="MailListGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    �ʼ��б����
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        �������</div>
    <div class="guide">
        <ul>
            <li><a href="MailListSend.aspx" target="main_right">�����ʼ�</a></li>
            <li><a href="MailListExport.aspx" target="main_right">�����ʼ�</a></li>
        </ul>
    </div>
</asp:Content>
