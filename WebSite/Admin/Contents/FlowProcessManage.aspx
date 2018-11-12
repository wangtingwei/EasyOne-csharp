<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Contents.FlowProcessManage" MasterPageFile="~/Admin/MasterPage.master"
    Title="流程步骤管理" Codebehind="FlowProcessManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="EgvFlowProcess" runat="server" AutoGenerateColumns="False"
        DataKeyNames="ProcessID" DataSourceID="OdsFlowProcess" SerialText="">
        <Columns>
            <pe:BoundField DataField="ProcessID" HeaderText="ID" SortExpression="ProcessID">
                <HeaderStyle Width="5%" />
            </pe:BoundField>
            <pe:BoundField DataField="ProcessName" HeaderText="流程步骤名称" SortExpression="ProcessName">
                <HeaderStyle Width="20%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="流程步骤描述" SortExpression="Description">
                <ItemTemplate>
                    <%# Eval("Description").ToString().Length <= 20 ? Eval("Description") : Eval("Description").ToString().Substring(0, 20) + ".."%>
                </ItemTemplate>
                <itemstyle horizontalalign="Left" />
            </pe:TemplateField>
            <pe:BoundField DataField="PassActionName" HeaderText="通过操作的操作名" SortExpression="ProcessName">
                <HeaderStyle Width="15%" />
            </pe:BoundField>
            <pe:BoundField DataField="RejectActionName" HeaderText="打回操作的操作名" SortExpression="ProcessName">
                <HeaderStyle Width="15%" />
            </pe:BoundField>
            <pe:TemplateField>
                <HeaderTemplate>
                    流程步骤操作
                </HeaderTemplate>
                <HeaderStyle Width="25%" />
                <ItemTemplate>
                    <a ID="EahFlowProcessModify" 
                        href='<%# "FlowProcess.aspx?Action=Modify&ProcessID=" + Eval("ProcessID") + "&FlowID=" + Eval("FlowID")%>'
                        runat="server">修改流程步骤</a>
                    |
                    <a ID="EahFlowProcessDelete" 
                        href='<%# AppendSecurityCode("FlowProcessManage.aspx?Action=Delete&ProcessID=" + Eval("ProcessID") + "&FlowID=" + Eval("FlowID"))%>'
                        onclick="return confirm('确定要删除此流程步骤吗？');" runat="server">删除流程步骤</a>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <asp:ObjectDataSource ID="OdsFlowProcess" runat="server" SelectMethod="GetFlowProcessList"
        TypeName="EasyOne.WorkFlows.FlowProcess">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="flowId" QueryStringField="FlowId"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <asp:Button ID="EBtnSubmit" Text="添加步骤"  runat="server" OnClick="EBtnSubmit_Click" />
</asp:Content>
