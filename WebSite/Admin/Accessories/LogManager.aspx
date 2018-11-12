<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Accessories.LogManager"
    Title="日志管理" Codebehind="LogManager.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="server">
    <pe:ExtendedGridView ID="GdvLogManager" runat="server" AutoGenerateCheckBoxColumn="True"
        CheckBoxFieldHeaderWidth="3%" SerialText="" AllowPaging="True" DataSourceID="OdsLog"
        AutoGenerateColumns="False" OnRowCommand="GdvLogManager_RowCommand" DataKeyNames="LogId"
        CssClass="TableWrap">
        <Columns>
            <pe:BoundField DataField="Title" HeaderText="标题" HeaderStyle-Width="">
                <HeaderStyle Width="28%" />
                <ItemStyle CssClass="TdWrap" HorizontalAlign="Left" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="类型">
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle Width="8%" />
                <ItemTemplate>
                    <%# EnumToHtml((EasyOne.Logging.LogCategory)Eval("Category")) %>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:BoundField DataField="ScriptName" HeaderText="访问地址">
                <ItemStyle CssClass="TdWrap" HorizontalAlign="Left" />
            </pe:BoundField>
            <pe:BoundField DataField="Timestamp" HeaderText="操作时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                HtmlEncode="False" HeaderStyle-Width="16%" />
            <pe:BoundField DataField="UserIP" HeaderText="IP地址" HeaderStyle-Width="12%" />
            <pe:BoundField DataField="UserName" HeaderText="操作人" HeaderStyle-Width="8%" />
            <pe:TemplateField HeaderText="查看">
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle Width="5%" />
                <ItemTemplate>
                    <asp:LinkButton ID="LBtnDetail" runat="server" CausesValidation="False" CommandName="Detail"
                        Text="详细" CommandArgument='<%# Eval("LogId") %>' PostBackUrl="~/Admin/Accessories/LogDetail.aspx"></asp:LinkButton>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <asp:ObjectDataSource ID="OdsLog" runat="server" SelectMethod="GetList" TypeName="EasyOne.Logging.DBLog"
        EnablePaging="True" MaximumRowsParameterName="maxNumberRows" StartRowIndexParameterName="startRowIndexId"
        SelectCountMethod="GetTotalOfLog">
        <SelectParameters>
            <asp:Parameter DefaultValue="0" Name="startRowIndexId" Type="Int32" />
            <asp:Parameter DefaultValue="10" Name="maxNumberRows" Type="Int32" />
            <asp:QueryStringParameter DefaultValue="0" Name="category" QueryStringField="Category"
                Type="Int32" />
            <asp:QueryStringParameter DefaultValue="" Name="searchType" QueryStringField="SearchType"
                Type="String" />
            <asp:QueryStringParameter Name="keyword" QueryStringField="KeyWord" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <asp:Button ID="BtnDelete" runat="server" Text="删除选定的日志" OnClick="BtnDelete_Click" />&nbsp;<asp:Button
        ID="BtnClearLog" runat="server" Text="清空日志记录" OnClick="BtnClearLog_Click" OnClientClick="return confirm('是否要清空日志记录？')" />
</asp:Content>
