<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Contents.ManageGuide"
    MasterPageFile="~/Admin/Guide.master" Title="����ģ�͹�����" Codebehind="ManageGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    ����ģ�͹���
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        ����ģ�͹���</div>
    <div class="guide">
        <ul>
            <li>
                <a ID="EahModelAdd" 
                    href="Model.aspx?Action=Add" runat="server" target="main_right">�������ģ��</a></li>
            <li>
                <a ID="EahModelManage" 
                    href="ModelManage.aspx" runat="server" target="main_right">����ģ�͹���</a></li>
            <li>
                <a ID="EahModelTemplateManage" 
                    href="~/Admin/CommonModel/ModelTemplateManage.aspx?ModelType=1" runat="server" target="main_right">����ģ��ģ�����</a></li>
            <li>
                <a ID="EahModelTemplateImport" 
                    href="~/Admin/CommonModel/ModelTemplateImport.aspx?ModelType=1" runat="server" target="main_right">����ģ��ģ�嵼��</a></li>
            <li>
                <a ID="EahModelTemplateExport" 
                    href="~/Admin/CommonModel/ModelTemplateExport.aspx?ModelType=1" runat="server" target="main_right">����ģ��ģ�嵼��</a></li>
        </ul>
    </div>
    <div class="guideexpand" onclick="Switch(this)">
        ��ģ�͵��ֶι���</div>
    <div class="guide">
        <asp:Repeater ID="RptModelList" runat="server">
            <HeaderTemplate>
                <ul>
            </HeaderTemplate>
            <ItemTemplate>
                <li><a href='<%# "~/Admin/CommonModel/FieldManage.aspx?ModelID=" + Eval("ModelId").ToString() + "&ModelName=" + Server.UrlEncode(Eval("ModelName").ToString()) + "&ModelType=1" %>'
                    target="main_right" id="link"  runat="server">
                    <%# Eval("ModelName") %>
                </a></li>
            </ItemTemplate>
            <FooterTemplate>
                </ul></FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
