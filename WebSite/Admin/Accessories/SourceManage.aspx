<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Accessories.SourceManage" Title="��Դ����" Codebehind="SourceManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="GdvSourceList" AutoGenerateColumns="False" runat="server"
        DataSourceID="Ods1" DataKeyNames="id" AllowPaging="True" ItemName="��Դ" ItemUnit="��"
        AutoGenerateCheckBoxColumn="True" OnRowCommand="SourceList_RowCommand" CheckBoxFieldHeaderWidth="3%"
        SerialText="" RowDblclickBoundField="id" RowDblclickUrl="Source.aspx?Action=Modify&Id={$Field}">
        <Columns>
            <pe:BoundField DataField="id" HeaderText="���">
                <HeaderStyle Width="5%" />
            </pe:BoundField>
            <pe:BoundField DataField="name" HeaderText="��Դ����" SortExpression="name">
                <HeaderStyle Width="20%" />
            </pe:BoundField>
            <pe:BoundField DataField="type" HeaderText="��Դ����" SortExpression="type">
                <HeaderStyle Width="15%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="�Ƿ�����">
                <HeaderStyle Width="8%" />
                <ItemTemplate>
                    <%# (bool)Eval("passed") == false ? "<span style=\"color:Red\">��<span>" : "<span style=\"color:green\">��<span>"%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="����">
                <HeaderStyle Width="5%" />
                <ItemTemplate>
                    <%# (bool)Eval("elite") == false ? "&nbsp;" : "��"%>
                    <%# (bool)Eval("ontop") == false ? "&nbsp;" : "��"%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="����">
                <HeaderStyle Width="20%" />
                <ItemTemplate>
                    <pe:ExtendedLinkButton ID="ELbtnPass" Text='<%# (bool)Eval("passed") == false ? "����" : "����"%>'
                        IsChecked="true" OperateCode="SourceManage" runat="server" CommandArgument='<%# Bind("id") %>'
                        CommandName="Passed" />
                    <pe:ExtendedLinkButton ID="ELbtnElite" Text='<%#(bool)Eval("elite") == false ? "�Ƽ�" : "ȡ���Ƽ�"%>'
                        IsChecked="true" OperateCode="SourceManage" runat="server" CommandArgument='<%# Bind("id") %>'
                        CommandName="Elite" />
                    <pe:ExtendedLinkButton ID="ELbtnTop" Text='<%# (bool)Eval("ontop") == false ? "�ö�" : "ȡ���ö�"%>'
                        IsChecked="true" OperateCode="SourceManage" runat="server" CommandArgument='<%# Bind("id") %>'
                        CommandName="OnTop" />
                    <pe:ExtendedAnchor ID="ELbtnModify" IsChecked="true" OperateCode="SourceManage" href='<%# "Source.aspx?Action=Modify&Id=" + Eval("id")%>'
                        runat="server">�޸�</pe:ExtendedAnchor>
                    <pe:ExtendedLinkButton ID="ELbtnDelete" Text="ɾ��" IsChecked="true" OperateCode="SourceManage"
                        OnClientClick="return confirm('�Ƿ�ɾ������Դ��')" runat="server" CommandArgument='<%# Bind("id") %>'
                        CommandName="Deleted" />
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <br />
    <pe:ExtendedButton ID="EBtnBatchDelete" Text="����ɾ��ѡ����Դ" OnClientClick="return batchconfirm('ȷʵҪɾ��ѡ�е���Դ��')"
        OnClick="EBtnBatchDelete_Click" CausesValidation="False" runat="server" />&nbsp;&nbsp;
    <asp:Button ID="EBtnAdd" Text="����һ������Դ" runat="server" OnClick="EBtnAdd_Click" />
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
