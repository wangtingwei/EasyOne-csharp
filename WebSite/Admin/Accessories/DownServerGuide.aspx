<%@ Page Language="C#" AutoEventWireup="True" Inherits="EasyOne.WebSite.Admin.Accessories.DownServerGuide"
    MasterPageFile="~/Admin/Guide.master" Title="����������" Codebehind="DownServerGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    ���ط���������
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        �������</div>
    <div class="guide">
        <ul>
            <li>
                <pe:ExtendedAnchor ID="EahAuthorAdd" IsChecked="true" OperateCode="DownServerManage"
                    href="DownServer.aspx" runat="server" target="main_right">������ط�����</pe:ExtendedAnchor></li>
            <li>
                <pe:ExtendedAnchor ID="EahAuthorManage" IsChecked="true" OperateCode="DownServerManage"
                    href="DownServerManage.aspx" runat="server" target="main_right">���ط���������</pe:ExtendedAnchor></li>
            <li>
                <pe:ExtendedAnchor ID="EahAuthorOrder" IsChecked="true" OperateCode="DownServerManage"
                    href="DownServerOrder.aspx" runat="server" target="main_right">���ط���������</pe:ExtendedAnchor></li>
        </ul>
    </div>
</asp:Content>
