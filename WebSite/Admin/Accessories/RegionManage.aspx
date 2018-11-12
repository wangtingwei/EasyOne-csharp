<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Accessories.RegionManage" Title="行政区划管理" Codebehind="RegionManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="GdvRegion" AutoGenerateColumns="False" runat="server" AllowPaging="True"
        DataSourceID="OdsRegion" OnRowCommand="GdvRegion_RowCommand" CheckBoxFieldHeaderWidth="3%"
        RowDblclickBoundField="RegionID" RowDblclickUrl="Region.aspx?Action=Modify&amp;ID={$Field}" SerialText="">
        <Columns>
            <pe:BoundField HeaderText="国家" DataField="Country"/>
            <pe:BoundField HeaderText="省份" DataField="Province" />
            <pe:BoundField HeaderText="城市" DataField="City" />
            <pe:BoundField HeaderText="地区(县)" DataField="Area" />
            <pe:BoundField HeaderText="邮政编码" DataField="PostCode" />
            <pe:BoundField HeaderText="区号" DataField="AreaCode" />
            <pe:TemplateField HeaderText="操作" ShowHeader="False">
                <ItemTemplate>
                    <a href='<%# Eval("RegionID","Region.aspx?Action=Modify&ID={0}")%>'>修改</a>
                    <asp:LinkButton ID="LbtnDel" CommandName="Del" CommandArgument='<%# Eval("RegionID") %>'
                        OnClientClick="return confirm('确定要删除此记录吗？')" runat="server">删除</asp:LinkButton>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <asp:ObjectDataSource ID="OdsRegion" runat="server" SelectMethod="GetRegionList"
        TypeName="EasyOne.Accessories.Region" SelectCountMethod="GetTotalOfRegion"
        EnablePaging="True" MaximumRowsParameterName="maxiNumRows" StartRowIndexParameterName="startRowIndexId">
        <SelectParameters>
            <asp:QueryStringParameter Name="searchType" Type="String" QueryStringField="SearchType" />
            <asp:QueryStringParameter Name="keyword" Type="String" QueryStringField="KeyWord" />
            <asp:Parameter Name="startRowIndexId" Type="Int32" />
            <asp:Parameter Name="maxiNumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
</asp:Content>
