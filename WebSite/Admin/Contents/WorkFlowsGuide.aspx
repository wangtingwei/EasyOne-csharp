<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Contents.WorkFlowsGuide"
    MasterPageFile="~/Admin/Guide.master" Title="流程管理向导" Codebehind="WorkFlowsGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    流程管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        流程管理</div>
    <div class="guide">
        <ul>
            <li>
                <a ID="EahWorkFlowsManage" 
                    href="../Contents/WorkFlowsManage.aspx" runat="server" target="main_right">流程管理</a></li>
            <li>
                <a ID="EahWorkFlowsAdd" 
                    href="../Contents/WorkFlows.aspx" runat="server" target="main_right">添加流程</a></li>
        </ul>
    </div>
    <div class="guideexpand" onclick="Switch(this)">
        各流程的步骤管理</div>
    <div class="guide">
        <asp:Repeater ID="RptWorkFlowsList" runat="server">
            <HeaderTemplate>
                <ul>
            </HeaderTemplate>
            <ItemTemplate>
                <li><a href='FlowProcessManage.aspx?Action=Modify&FlowID=<%# DataBinder.Eval(Container.DataItem, "FlowID")%>&FlowName=<%# DataBinder.Eval(Container.DataItem, "FlowName")%>'
                    target="main_right">
                    <%# Eval("FlowName")%>
                </a></li>
            </ItemTemplate>
            <FooterTemplate>
                </ul></FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
