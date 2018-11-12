<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Contents.ManageGuide"
    MasterPageFile="~/Admin/Guide.master" Title="内容模型管理向导" Codebehind="ManageGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    内容模型管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        内容模型管理</div>
    <div class="guide">
        <ul>
            <li>
                <a ID="EahModelAdd" 
                    href="Model.aspx?Action=Add" runat="server" target="main_right">添加内容模型</a></li>
            <li>
                <a ID="EahModelManage" 
                    href="ModelManage.aspx" runat="server" target="main_right">内容模型管理</a></li>
            <li>
                <a ID="EahModelTemplateManage" 
                    href="~/Admin/CommonModel/ModelTemplateManage.aspx?ModelType=1" runat="server" target="main_right">内容模型模板管理</a></li>
            <li>
                <a ID="EahModelTemplateImport" 
                    href="~/Admin/CommonModel/ModelTemplateImport.aspx?ModelType=1" runat="server" target="main_right">内容模型模板导入</a></li>
            <li>
                <a ID="EahModelTemplateExport" 
                    href="~/Admin/CommonModel/ModelTemplateExport.aspx?ModelType=1" runat="server" target="main_right">内容模型模板导出</a></li>
        </ul>
    </div>
    <div class="guideexpand" onclick="Switch(this)">
        各模型的字段管理</div>
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
