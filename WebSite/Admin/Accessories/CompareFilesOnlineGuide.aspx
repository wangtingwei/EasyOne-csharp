<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Accessories.CompareFilesOnlineGuide"
    Title="���߱Ƚ���վ�ļ���" Codebehind="CompareFilesOnlineGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    ���߱Ƚ���վ�ļ�
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        �������</div>
    <div class="guide">
        <ul>
            <li><a href="CompareFilesOnline.aspx?IsShowAll=0" target="main_right">��ʾȫ���ȽϽ��</a></li>
            <li><a href="CompareFilesOnline.aspx?IsShowAll=1" target="main_right">ֻ��ʾ���첿���ļ�</a></li>
        </ul>
    </div>
</asp:Content>
