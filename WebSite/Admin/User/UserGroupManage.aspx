<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.User.UserGroupManage" Title="会员组管理" Codebehind="UserGroupManage.aspx.cs" %>

<%@ Import Namespace="EasyOne.UserManage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="GdvUserGroup" runat="server" AutoGenerateColumns="False"
        DataSourceID="OdsUserGroup" DataKeyNames="GroupId" ItemName="组" ItemUnit="个"
        OnRowDataBound="GdvUserGroup_RowDataBound" CheckBoxFieldHeaderWidth="3%" SerialText="" OnRowCommand="EgvUserGroup_RowCommand" 
        RowDblclickBoundField="GroupId" 
        RowDblclickUrl="UserGroup.aspx?Action=Modify&amp;GroupId={$Field}">
        <Columns>
            <pe:BoundField DataField="GroupId" HeaderText="ID">
                <HeaderStyle Width="5%" />
            </pe:BoundField>
            <pe:BoundField DataField="GroupName" HeaderText="会员组名" >
                <HeaderStyle Width="15%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="会员组简介">
                <ItemStyle HorizontalAlign="Left" />
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="会员组类型">
                <HeaderStyle Width="12%" />
                <ItemTemplate>
                    <asp:Label ID="LblGroupType" runat="server"></asp:Label>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="会员数量">
                <HeaderStyle Width="8%" />
                <ItemTemplate>
                    <asp:Label ID="LblUserNumber" runat="server" Text='<%# Bind("UserInGroupNumber") %>'></asp:Label>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="操 作">
                <HeaderStyle Width="30%" />
                <ItemTemplate>
                    <a href='UserGroup.aspx?Action=Modify&GroupId=<%#Eval("GroupId")%>'>修改</a> | <a href='UserGroupPermissions.aspx?Action=Modify&GroupId=<%#Eval("GroupId")%>'>
                        权限设置</a> | <asp:LinkButton ID="LnkDelete" CommandName="DeleteUserGroup" OnClientClick="if(!this.disabled) return confirm('确实要删除选中的会员组？');" CommandArgument='<%# Eval("GroupId")%>' runat="server">删除</asp:LinkButton> |      
                    <asp:HyperLink ID="HypUserList" runat="server"></asp:HyperLink>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <asp:ObjectDataSource ID="OdsUserGroup" runat="server" SelectMethod="GetUserGroupList"
        StartRowIndexParameterName="startRowIndexId" MaximumRowsParameterName="maxNumberRows"
        TypeName="EasyOne.UserManage.UserGroups" SelectCountMethod="GetNumberUserGroups"
        EnablePaging="false">
        <SelectParameters>
            <asp:Parameter DefaultValue="0" Name="startRowIndexId" Type="Int32" />
            <asp:Parameter DefaultValue="0" Name="maxNumberRows" Type="Int32" />
            <asp:Parameter DefaultValue="true" Name="isNeedUserNumber" Type="boolean" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
