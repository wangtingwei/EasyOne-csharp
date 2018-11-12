<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Contents.StatusManage"
    MasterPageFile="~/Admin/MasterPage.master" Title="稿件状态码管理" Codebehind="StatusManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="EgvStatus" runat="server" AutoGenerateColumns="False" DataKeyNames="StatusID"
        DataSourceID="OdsStatus" SerialText="" OnRowCommand="EgvStatus_RowCommand" OnRowDataBound="EgvStatus_RowDataBound">
        <Columns>
            <pe:BoundField DataField="StatusCode" HeaderText="状态码" SortExpression="StatusCode">
                <HeaderStyle Width="5%" />
            </pe:BoundField>
            <pe:BoundField DataField="StatusName" HeaderText="状态名称" SortExpression="StatusName">
            </pe:BoundField>
            <pe:TemplateField HeaderText="状态码类型" SortExpression="Description">
                <ItemTemplate>
                    <%# (int)Eval("StatusType") == 0 ? "<span style='color:Green'>系统</span>" : "自定义"%>
                </ItemTemplate>
                <HeaderStyle Width="15%" />
            </pe:TemplateField>
            <pe:TemplateField>
                <HeaderStyle Width="10%" />
                <HeaderTemplate>
                    操作
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="ELbtnModify" Text="修改" runat="server" CommandArgument='<%# Eval("StatusID")%>'
                        CommandName="ModifyStatus" />
                    <asp:LinkButton ID="ELbtnDelete" Text="删除" OnClientClick="if(!this.disabled) return confirm('确定要删除此状态吗？');"
                        runat="server" CommandArgument='<%# Eval("StatusCode")%>' CommandName="DeleteStatus" />
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <asp:ObjectDataSource ID="OdsStatus" runat="server" SelectMethod="GetStatusList"
        TypeName="EasyOne.WorkFlows.Status"></asp:ObjectDataSource>
</asp:Content>
