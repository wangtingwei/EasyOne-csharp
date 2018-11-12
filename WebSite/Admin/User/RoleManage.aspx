<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.User.RoleManage"
    Title="角色管理" Codebehind="RoleManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="server">
    <pe:ExtendedGridView ID="Egv" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        OnRowCommand="EgvUserRole_RowCommand" DataKeyNames="RoleId" CheckBoxFieldHeaderWidth="3%"
        DataSourceID="ObjectDataSource1" SerialText="" OnRowDataBound="Egv_RowDataBound"
        RowDblclickBoundField="RoleId" RowDblclickUrl="Role.aspx?Action=Modify&amp;RoleId={$Field}">
        <Columns>
            <pe:BoundField DataField="RoleId" HeaderText="ID" HeaderStyle-Width="4%" />
            <pe:BoundField DataField="RoleName" HeaderText="角色名" HeaderStyle-Width="15%"  />
            <pe:TemplateField HeaderText="描述">
                <ItemStyle HorizontalAlign="Left" />
                <ItemTemplate>
                    <%# EasyOne.Common.StringHelper.SubString(Eval("Description").ToString(),53,"...")%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="管理操作">
                <HeaderStyle Width="16%" />
                <ItemTemplate>
                    <a href="RoleMember.aspx?RoleId=<%# Eval("RoleId")%>&RoleName=<%#Server.UrlEncode(Eval("RoleName").ToString())%>">
                        成员管理</a>
                    <asp:LinkButton ID="LnkModify" CommandName="ModifyRole" CommandArgument='<%# Eval("RoleId")%>'
                        runat="server">修改</asp:LinkButton>
                    <asp:LinkButton ID="LnkDelete" CommandName="DeleteRole" OnClientClick="if(!this.disabled) return confirm('确实要删除此角色吗？');"
                        CommandArgument='<%# Eval("RoleId")%>' runat="server">删除</asp:LinkButton>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="权限设置">
                <HeaderStyle Width="22%" />
                <ItemTemplate>
                    <asp:LinkButton ID="LnkModifyCommonPermissions" CommandName="CommonPermissions" CommandArgument='<%# Eval("RoleId")%>'
                        runat="server">常规权限设置</asp:LinkButton>
                    <asp:LinkButton ID="LnkModifyFieldPermissions" CommandName="FieldPermissions" CommandArgument='<%# Eval("RoleId")%>'
                        runat="server">字段权限设置</asp:LinkButton>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetRoleList"
        TypeName="EasyOne.UserManage.UserRole">
        <SelectParameters>
            <asp:Parameter Name="startRowIndexId" Type="Int32" />
            <asp:Parameter Name="maxNumberRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
