<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Collection.CollectionFilterManage"
    MasterPageFile="~/Admin/MasterPage.master" Title="采集过滤管理" Codebehind="CollectionFilterManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="EgvFilterRules" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        DataKeyNames="FilterRuleID" DataSourceID="OdsFilterRules" ItemName="记录" ItemUnit="条"
        CheckBoxFieldHeaderWidth="3%" SerialText="" AutoGenerateCheckBoxColumn="True"
        RowDblclickBoundField="FilterRuleID" RowDblclickUrl="CollectionFilter.aspx?Action=Modify&FilterRuleID={$Field}">
        <Columns>
            <pe:BoundField DataField="FilterRuleID" HeaderText="ID" SortExpression="ID">
                <HeaderStyle Width="5%" />
            </pe:BoundField>
            <pe:BoundField DataField="FilterName" HeaderText="过滤名称" SortExpression="FilterName">
                <HeaderStyle Width="30%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="过滤类型" SortExpression="FilterType">
                <HeaderStyle Width="10%" />
                <ItemTemplate>
                    <%# (int)Eval("FilterType") == 2 ? "高级过滤" : "简单过滤"%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a href="CollectionFilter.aspx?Action=Modify&FilterRuleID=<%#Eval("FilterRuleID")%>">
                        修改</a> <a href="CollectionFilterManage.aspx?Action=Delete&FilterRuleID=<%#Eval("FilterRuleID")%>"
                            onclick="return confirm('是否删除该采集过滤？');">删除</a>
                </ItemTemplate>
                <HeaderStyle Width="55%" />
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <br />
        <input id="ChkAll" onclick="javascript:CheckAll(this);" type="checkbox" />选中本页显示的所有项目
    &nbsp;&nbsp;<asp:Button ID="BtnBatchDelete" Text="批量删除选定采集过滤项目" OnClientClick="return batchconfirm('确实要删除选中的采集过滤么？')"
        runat="server" OnClick="BtnBatchDelete_Click" />&nbsp;&nbsp;
    <br />
    <asp:ObjectDataSource ID="OdsFilterRules" runat="server" SelectMethod="GetList" SelectCountMethod="GetCountNumber"
        TypeName="EasyOne.Collection.CollectionFilterRules" EnablePaging="True" StartRowIndexParameterName="startRowIndexId"
        MaximumRowsParameterName="maxNumberRows" OldValuesParameterFormatString="original_{0}">
        <DeleteParameters>
            <asp:Parameter Name="id" Type="String" />
        </DeleteParameters>
    </asp:ObjectDataSource>
</asp:Content>
