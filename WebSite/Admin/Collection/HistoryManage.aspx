<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Collection.HistoryManage"
    MasterPageFile="~/Admin/MasterPage.master" Title="采集历史记录管理" ValidateRequest="false" Codebehind="HistoryManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="EgvHistory" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        DataKeyNames="HistoryID" DataSourceID="OdsCollectionHistory" ItemName="历史记录"
        ItemUnit="个" CheckBoxFieldHeaderWidth="3%" SerialText="" AutoGenerateCheckBoxColumn="True"
        OnRowDataBound="EgvHistory_RowDataBound">
        <Columns>
            <pe:BoundField DataField="HistoryID" HeaderText="HistoryID" SortExpression="HistoryID">
                <HeaderStyle Width="5%" />
            </pe:BoundField>
            <pe:BoundField DataField="ItemName" HeaderText="项目名称" SortExpression="ItemName">
                <HeaderStyle Width="10%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="标题" SortExpression="UrlName">
                <HeaderStyle Width="10%" />
                <ItemTemplate>
                    <asp:HyperLink ID="LnkTitle" runat="server"></asp:HyperLink>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:BoundField DataField="NodeName" HeaderText="所属栏目" SortExpression="ItemName">
                <HeaderStyle Width="10%" />
            </pe:BoundField>
            <pe:BoundField DataField="ModelName" HeaderText="所属模型" SortExpression="ItemName">
                <HeaderStyle Width="10%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="采集网站" SortExpression="UrlName">
                <HeaderStyle Width="15%" />
                <ItemTemplate>
                    <a href="<%#Eval("NewsUrl")%>" target='_blank'>点击浏览</a>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="结果 " SortExpression="NodeID">
                <HeaderStyle Width="5%" />
                <ItemTemplate>
                    <pe:ExtendedLabel HtmlEncode="false" ID="LblResult" runat="server" Text=""></pe:ExtendedLabel>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="操作">
                <HeaderStyle Width="10%" />
                <ItemTemplate>
                    <a href="HistoryManage.aspx?Action=Delete&HistoryID=<%#Eval("HistoryID")%>" onclick="return confirm('是否删除该采集历史记录？');">
                        删除</a>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <br />
    <input id="ChkAll" onclick="javascript:CheckAll(this);" type="checkbox" />选中本页显示的所有项目
    &nbsp;&nbsp;
    <asp:Button ID="BtnBatchDelete" Text="批量删除选定采集历史记录" OnClientClick="return batchconfirm('确实要删除选中的采集历史记录么？')"
        runat="server" OnClick="BtnBatchDelete_Click" />&nbsp;&nbsp;<asp:Button ID="BtnDeleteErr"
            runat="server" Text="删除全部失败记录" OnClick="BtnDeleteErr_Click" OnClientClick="return confirm('确实要删除全部失败记录？')" />&nbsp;&nbsp;<asp:Button
                ID="BtnDeleteSuccess" runat="server" Text="删除全部成功记录" OnClick="BtnDeleteSuccess_Click" OnClientClick="return confirm('确实要删除全部成功记录？')" />&nbsp;&nbsp;<asp:Button
                    ID="BtnDeleteAll" runat="server" Text="清空采集历史记录" OnClick="BtnDeleteAll_Click"
                    OnClientClick="return confirm('确实要清空采集历史记录么？')" />
    <br />
    <asp:ObjectDataSource ID="OdsCollectionHistory" runat="server" SelectMethod="GetCollectionHistory"
        SelectCountMethod="GetCountNumber" TypeName="EasyOne.Collection.CollectionHistory"
        EnablePaging="True" StartRowIndexParameterName="startRowIndexId" MaximumRowsParameterName="maxNumberRows"
        OldValuesParameterFormatString="original_{0}">
        <DeleteParameters>
            <asp:Parameter Name="id" Type="String" />
        </DeleteParameters>
    </asp:ObjectDataSource>
</asp:Content>
