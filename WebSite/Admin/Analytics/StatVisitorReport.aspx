<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Analytics.StatVisitorReport" Title="��վͳ�ƹ���" Codebehind="StatVisitorReport.aspx.cs" %>

<%@ Import Namespace="EasyOne.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="ExtendedGridView1" runat="server" AutoGenerateColumns="False"
        DataKeyNames="Id" DataSourceID="OdsCounter" SerialText="" AllowPaging="True"
        CheckBoxFieldHeaderWidth="3%" OnRowCommand="ExtendedGridView1_RowCommand" ItemName="���ʼ�¼"
        ItemUnit="��">
        <Columns>
            <pe:BoundField DataField="VTime" HeaderText="����ʱ��(��������)" SortExpression="VTime" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                HtmlEncode="False" />
            <pe:TemplateField HeaderText="����ʱ��(�ͻ���)">
                <ItemTemplate>
                    <%# String.Format("{0:yyyy-MM-dd HH:mm:ss}",Convert.ToDateTime(Eval("VTime")).AddHours(-Convert.ToInt32(Eval("Timezone"))- DataConverter.CLng(HdnTimezone.Value))) %>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:BoundField DataField="IP" HeaderText="������IP" SortExpression="IP" />
            <pe:BoundField DataField="Address" HeaderText="��ַ" SortExpression="Address"/>
            <pe:BoundField DataField="Referer" HeaderText="����ҳ��" SortExpression="Referer"/>
            <pe:TemplateField HeaderText="����">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("Id") %>'
                        CommandName="ShowDetail" PostBackUrl="~/Admin/Analytics/ShowClientDetail.aspx">�鿴��ϸ</asp:LinkButton>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <asp:ObjectDataSource ID="OdsCounter" runat="server" SelectMethod="GetStatVisitorList"
        TypeName="EasyOne.Analytics.OtherReport" EnablePaging="True" MaximumRowsParameterName="maxNumberRows"
        SelectCountMethod="GetTotalOfStatVisitor" StartRowIndexParameterName="startRowIndexId">
    </asp:ObjectDataSource>
    <asp:HiddenField ID="HdnTimezone" runat="server" />
</asp:Content>
