<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Guide.master"
    Inherits="EasyOne.WebSite.Admin.Accessories.MessageGuide"
    Title="����Ϣ������" Codebehind="MessageGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    ����Ϣ����
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        �������</div>
    <div class="guide">
        <ul>
            <li><a href="MessageManage.aspx?action=me" target="main_right">�ҵĶ���Ϣ</a></li>            
            <li><a href="MessageManage.aspx" target="main_right">����Ϣ����</a></li>
            <li><a href="MessageSend.aspx" target="main_right">���Ͷ���Ϣ</a></li>
            <li><a href="MessageBatchDel.aspx" target="main_right">����ɾ������Ϣ</a></li>
        </ul>
    </div>
</asp:Content>
