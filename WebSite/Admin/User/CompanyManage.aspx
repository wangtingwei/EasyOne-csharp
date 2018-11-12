<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.User.CompanyManage" Title="无标题页" Codebehind="CompanyManage.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="server">
    <pe:ExtendedGridView ID="EgvCompany" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        AutoGenerateCheckBoxColumn="True" DataKeyNames="CompanyId" DataSourceID="OdsCompany" 
        ItemName="企业" ItemUnit="个" OnRowDataBound="EgvCompany_RowDataBound" OnRowCommand="EgvCompany_RowCommand"
        RowDblclickBoundField="CompanyID" 
        RowDblclickUrl="CompanyShow.aspx?CompanyID={$Field}">
        <Columns>
            <pe:BoundField DataField="CompanyName" HeaderText="企业名称" SortExpression="CompanyName"
                >
            </pe:BoundField>
            <pe:BoundField DataField="Phone" HeaderText="联系电话" SortExpression="Phone">
            </pe:BoundField>        
            <pe:TemplateField HeaderText="行业地位" SortExpression="StatusInField">
                <ItemTemplate>
                    <asp:Label ID="LblStatusInField" runat="server"></asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="10%" />
            </pe:TemplateField>
            <pe:TemplateField HeaderText="公司规模" SortExpression="CompanySize">
                <ItemTemplate>
                    <asp:Label ID="LblCompanySize" runat="server"></asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="10%" />
            </pe:TemplateField>
            <pe:TemplateField HeaderText="经营状态" SortExpression="ManagementForms">
                <ItemTemplate>
                    <asp:Label ID="LblManagementForms" runat="server"></asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="10%" />
            </pe:TemplateField>
            <pe:TemplateField HeaderText="操作">
                <HeaderStyle Width="20%" />
                <ItemTemplate>
                   <a href='CompanyShow.aspx?CompanyID=<%#Eval("CompanyID")%>'>详细信息</a>
                   <asp:LinkButton ID="LbtnDelete" CommandArgument='<%# Eval("CompanyID") %>' runat="server" CommandName="Del" OnClientClick="return confirm('是否删除该企业？')">删除</asp:LinkButton>              
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <asp:ObjectDataSource ID="OdsCompany" runat="server" SelectCountMethod="GetTotalOfCompany"
        SelectMethod="GetCompanyList" TypeName="EasyOne.Crm.Company" EnablePaging="True"
        StartRowIndexParameterName="startRowIndexId" MaximumRowsParameterName="maxNumberRows">
        <SelectParameters>
            <asp:QueryStringParameter Name="field" QueryStringField="Field" Type="String" />
            <asp:QueryStringParameter Name="keyword" QueryStringField="KeyWord" Type="String" />
            <asp:Parameter DefaultValue="false" Name="allowEmptyName" Type="Boolean" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:Button ID="BtnDelete" runat="server" OnClientClick="return confirm('是否要删除企业？')" Text="删除选中的企业" OnClick="BtnDelete_Click" />
    <br />
</asp:Content>
