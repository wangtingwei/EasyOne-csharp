<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Accessories.SourceManage" Title="来源管理" Codebehind="SourceManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="GdvSourceList" AutoGenerateColumns="False" runat="server"
        DataSourceID="Ods1" DataKeyNames="id" AllowPaging="True" ItemName="来源" ItemUnit="个"
        AutoGenerateCheckBoxColumn="True" OnRowCommand="SourceList_RowCommand" CheckBoxFieldHeaderWidth="3%"
        SerialText="" RowDblclickBoundField="id" RowDblclickUrl="Source.aspx?Action=Modify&Id={$Field}">
        <Columns>
            <pe:BoundField DataField="id" HeaderText="序号">
                <HeaderStyle Width="5%" />
            </pe:BoundField>
            <pe:BoundField DataField="name" HeaderText="来源名称" SortExpression="name">
                <HeaderStyle Width="20%" />
            </pe:BoundField>
            <pe:BoundField DataField="type" HeaderText="来源分类" SortExpression="type">
                <HeaderStyle Width="15%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="是否启用">
                <HeaderStyle Width="8%" />
                <ItemTemplate>
                    <%# (bool)Eval("passed") == false ? "<span style=\"color:Red\">×<span>" : "<span style=\"color:green\">√<span>"%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="属性">
                <HeaderStyle Width="5%" />
                <ItemTemplate>
                    <%# (bool)Eval("elite") == false ? "&nbsp;" : "荐"%>
                    <%# (bool)Eval("ontop") == false ? "&nbsp;" : "顶"%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="操作">
                <HeaderStyle Width="20%" />
                <ItemTemplate>
                    <pe:ExtendedLinkButton ID="ELbtnPass" Text='<%# (bool)Eval("passed") == false ? "启用" : "禁用"%>'
                        IsChecked="true" OperateCode="SourceManage" runat="server" CommandArgument='<%# Bind("id") %>'
                        CommandName="Passed" />
                    <pe:ExtendedLinkButton ID="ELbtnElite" Text='<%#(bool)Eval("elite") == false ? "推荐" : "取消推荐"%>'
                        IsChecked="true" OperateCode="SourceManage" runat="server" CommandArgument='<%# Bind("id") %>'
                        CommandName="Elite" />
                    <pe:ExtendedLinkButton ID="ELbtnTop" Text='<%# (bool)Eval("ontop") == false ? "置顶" : "取消置顶"%>'
                        IsChecked="true" OperateCode="SourceManage" runat="server" CommandArgument='<%# Bind("id") %>'
                        CommandName="OnTop" />
                    <pe:ExtendedAnchor ID="ELbtnModify" IsChecked="true" OperateCode="SourceManage" href='<%# "Source.aspx?Action=Modify&Id=" + Eval("id")%>'
                        runat="server">修改</pe:ExtendedAnchor>
                    <pe:ExtendedLinkButton ID="ELbtnDelete" Text="删除" IsChecked="true" OperateCode="SourceManage"
                        OnClientClick="return confirm('是否删除本来源？')" runat="server" CommandArgument='<%# Bind("id") %>'
                        CommandName="Deleted" />
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <br />
    <pe:ExtendedButton ID="EBtnBatchDelete" Text="批量删除选定来源" OnClientClick="return batchconfirm('确实要删除选中的来源？')"
        OnClick="EBtnBatchDelete_Click" CausesValidation="False" runat="server" />&nbsp;&nbsp;
    <asp:Button ID="EBtnAdd" Text="增加一个新来源" runat="server" OnClick="EBtnAdd_Click" />
    <asp:ObjectDataSource ID="Ods1" runat="server" TypeName="EasyOne.Accessories.Source"
        SelectMethod="GetSourceList">
        <SelectParameters>
            <asp:Parameter Name="startRowIndexId" Type="Int32" />
            <asp:Parameter Name="maxNumberRows" Type="Int32" />
            <asp:QueryStringParameter Type="Int32" Name="searchType" DefaultValue="-1" QueryStringField="SearchType" />
            <asp:QueryStringParameter Type="String" Name="keyword" QueryStringField="Keyword" />
            <asp:QueryStringParameter Type="String" Name="sourceType" QueryStringField="SourceType" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
