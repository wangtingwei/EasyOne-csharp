<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Accessories.DownServerOrder" Title="�������������" Codebehind="DownServerOrder.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="GdvDownServerOrder" runat="server" DataSourceID="OdsDownServerOrder"
        GridLines="None" DataKeyNames="ServerID" Width="100%" AutoGenerateColumns="false">
        <Columns>
            <pe:BoundField DataField="ServerID" HeaderText="ID">
                <ItemStyle Width="10%" />
            </pe:BoundField>
            <pe:BoundField DataField="ServerName" HeaderText="��������" SortExpression="ServerName">
                <ItemStyle Width="20%" />
            </pe:BoundField>
            <pe:BoundField DataField="OrderID" HeaderText="���">
                <ItemStyle Width="10%" />
            </pe:BoundField>
            
            <asp:TemplateField HeaderText="����">
                <headerstyle width="10%" />
                <itemtemplate>
                    <asp:DropDownList ID="DropOrderId" runat="server">
                    </asp:DropDownList>
                </itemtemplate>
            </asp:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
     <br />
    <div style="text-align: center;">
        <asp:Button ID="EBtnSetOrderId" Text="��������" OnClick="EBtnSetOrderId_Click" runat="server" /></div>
    <asp:ObjectDataSource ID="OdsDownServerOrder" runat="server" DataObjectTypeName="EasyOne.Model.Accessories.DownServerInfo"
        DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="GetDownServerList"
        TypeName="EasyOne.Accessories.DownServer" UpdateMethod="Update">
        <DeleteParameters>
            <asp:Parameter Name="SubModuleID" Type="Int32" />
        </DeleteParameters>
    </asp:ObjectDataSource>
</asp:Content>
