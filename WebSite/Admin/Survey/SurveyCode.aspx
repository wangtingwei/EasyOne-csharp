<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" ValidateRequest="false"
    Inherits="EasyOne.WebSite.Admin.Survey.SurveyCode" EnableViewState="true" Codebehind="SurveyCode.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="EgvCode" runat="server" AutoGenerateColumns="False" SerialText=""
        ItemName="问卷" ItemUnit="篇" DataSourceID="OdsSurvey" AllowPaging="True" OnRowDataBound="Egv_RowDataBound">
        <Columns>
            <pe:BoundField DataField="SurveyName" HeaderText="问卷名称" SortExpression="SurveyName"
                >
                <ItemStyle Width="160px" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="问卷页面调用代码" SortExpression="Disabled">
                <ItemTemplate>
                    <asp:TextBox ID="TxtFrameCode" runat="server" TextMode="MultiLine" Width="230" Height="50"
                        onmouseover="this.select();" Text=''></asp:TextBox>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="问卷页面链接调用代码" SortExpression="Disabled">
                <ItemTemplate>
                    <asp:TextBox ID="TxtLinkCode" runat="server" TextMode="MultiLine" Width="230" Height="50"
                        onmouseover="this.select();" Text=''></asp:TextBox>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="问卷结果调用代码" SortExpression="Disabled">
                <ItemTemplate>
                    <asp:TextBox ID="TxtResultCode" runat="server" TextMode="MultiLine" Width="230" Height="50"
                        onmouseover="this.select();" Text=''></asp:TextBox>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <asp:ObjectDataSource ID="OdsSurvey" runat="server" SelectMethod="GetList" TypeName="EasyOne.Survey.SurveyManager"
        EnablePaging="True" StartRowIndexParameterName="startRowIndexId" MaximumRowsParameterName="maxNumberRows">
        <SelectParameters>
            <%--<asp:Parameter Name="startRowIndexId" Type="Int32" />
            <asp:Parameter Name="maxNumberRows" Type="Int32" />
            <asp:Parameter Name="searchType" Type="Int32" />--%>
            <asp:QueryStringParameter DefaultValue="0" Name="SearchType" QueryStringField="SearchType" Type="Int32" />
            <asp:QueryStringParameter Name="Keyword" QueryStringField="Keyword" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
