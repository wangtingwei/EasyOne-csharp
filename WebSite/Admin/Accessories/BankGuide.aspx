<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master" Inherits="EasyOne.WebSite.Admin.Accessories.BankGuide" AutoEventWireup="true" Title="�����˻�������" Codebehind="BankGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    �����˻�����
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        �������</div>
    <div class="guide">
        <ul>
            <li><a href="BankManage.aspx" target="main_right">�����˻�����</a></li>
            <li><a href="Bank.aspx" target="main_right">��������˻�</a></li>
        </ul>
    </div>
</asp:Content>
