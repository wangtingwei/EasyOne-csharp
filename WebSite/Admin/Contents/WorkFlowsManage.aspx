<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Contents.WorkFlowsManage" MasterPageFile="~/Admin/MasterPage.master"
    Title="流程管理" Codebehind="WorkFlowsManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="EgvWorkFlows" runat="server" AutoGenerateColumns="False"
        DataKeyNames="FlowID" DataSourceID="OdsWorkFlows" SerialText="" OnRowCommand="EgvWorkFlows_RowCommand"
        CheckBoxFieldHeaderWidth="3%" RowDblclickBoundField="FlowID" RowDblclickUrl="WorkFlows.aspx?Action=Modify&amp;FlowID={$Field}">
        <Columns>
            <pe:BoundField DataField="FlowID" HeaderText="ID" SortExpression="FlowID">
                <HeaderStyle Width="5%" />
            </pe:BoundField>
            <pe:BoundField DataField="FlowName" HeaderText="流程名称" SortExpression="FlowName">
                <HeaderStyle Width="15%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="流程描述" SortExpression="Description">
                <ItemTemplate>
                    <%# Eval("Description").ToString()%>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" />
            </pe:TemplateField>
            <pe:TemplateField>
                <HeaderTemplate>
                    流程操作
                </HeaderTemplate>
                <HeaderStyle Width="50%" />
                <ItemTemplate>
                    <a id="EahFlowProcessAdd" href='<%# "FlowProcess.aspx?Action=Add&FlowID=" + Eval("FlowID")%>'
                        runat="server">添加步骤</a> | <a id="EahFlowProcessManage" href='<%# "FlowProcessManage.aspx?Action=Modify&FlowID=" + Eval("FlowID")%>'
                            runat="server">步骤列表</a> | <a id="EahWorkFlowsModify" href='<%# "WorkFlows.aspx?Action=Modify&FlowID=" + Eval("FlowID")%>'
                                runat="server">修改流程</a> |
                    <asp:LinkButton ID="ELbtnDelField" Text="删除流程" OnClientClick="return confirm('确定要删除此流程吗？')"
                        runat="server" CommandArgument='<%# Eval("FlowID")%>' CommandName="DeleteWorkFlows" />
                    | <a id="EahWorkFlowsCopy" href='<%# AppendSecurityCode("WorkFlowsManage.aspx?Action=Copy&FlowID=" + Eval("FlowID"))%>'
                        runat="server">复制流程</a>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <asp:ObjectDataSource ID="OdsWorkFlows" runat="server" SelectMethod="GetWorkFlowsList"
        TypeName="EasyOne.WorkFlows.WorkFlow"></asp:ObjectDataSource>
</asp:Content>
