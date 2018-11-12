<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Analytics.StatVisitorReport" Title="网站统计管理" Codebehind="StatVisitorReport.aspx.cs" %>

<%@ Import Namespace="EasyOne.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="ExtendedGridView1" runat="server" AutoGenerateColumns="False"
        DataKeyNames="Id" DataSourceID="OdsCounter" SerialText="" AllowPaging="True"
        CheckBoxFieldHeaderWidth="3%" OnRowCommand="ExtendedGridView1_RowCommand" ItemName="访问记录"
        ItemUnit="个">
        <Columns>
            <pe:BoundField DataField="VTime" HeaderText="访问时间(服务器端)" SortExpression="VTime" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                HtmlEncode="False" />
            <pe:TemplateField HeaderText="访问时间(客户端)">
                <ItemTemplate>
                    <%# String.Format("{0:yyyy-MM-dd HH:mm:ss}",Convert.ToDateTime(Eval("VTime")).AddHours(-Convert.ToInt32(Eval("Timezone"))- DataConverter.CLng(HdnTimezone.Value))) %>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:BoundField DataField="IP" HeaderText="访问者IP" SortExpression="IP" />
            <pe:BoundField DataField="Address" HeaderText="地址" SortExpression="Address"/>
            <pe:BoundField DataField="Referer" HeaderText="链接页面" SortExpression="Referer"/>
            <pe:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("Id") %>'
                        CommandName="ShowDetail" PostBackUrl="~/Admin/Analytics/ShowClientDetail.aspx">查看明细</asp:LinkButton>
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
