<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.User.UserGroupManage" Title="��Ա�����" Codebehind="UserGroupManage.aspx.cs" %>

<%@ Import Namespace="EasyOne.UserManage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="GdvUserGroup" runat="server" AutoGenerateColumns="False"
        DataSourceID="OdsUserGroup" DataKeyNames="GroupId" ItemName="��" ItemUnit="��"
        OnRowDataBound="GdvUserGroup_RowDataBound" CheckBoxFieldHeaderWidth="3%" SerialText="" OnRowCommand="EgvUserGroup_RowCommand" 
        RowDblclickBoundField="GroupId" 
        RowDblclickUrl="UserGroup.aspx?Action=Modify&amp;GroupId={$Field}">
        <Columns>
            <pe:BoundField DataField="GroupId" HeaderText="ID">
                <HeaderStyle Width="5%" />
            </pe:BoundField>
            <pe:BoundField DataField="GroupName" HeaderText="��Ա����" >
                <HeaderStyle Width="15%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="��Ա����">
                <ItemStyle HorizontalAlign="Left" />
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="��Ա������">
                <HeaderStyle Width="12%" />
                <ItemTemplate>
                    <asp:Label ID="LblGroupType" runat="server"></asp:Label>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="��Ա����">
                <HeaderStyle Width="8%" />
                <ItemTemplate>
                    <asp:Label ID="LblUserNumber" runat="server" Text='<%# Bind("UserInGroupNumber") %>'></asp:Label>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="�� ��">
                <HeaderStyle Width="30%" />
                <ItemTemplate>
                    <a href='UserGroup.aspx?Action=Modify&GroupId=<%#Eval("GroupId")%>'>�޸�</a> | <a href='UserGroupPermissions.aspx?Action=Modify&GroupId=<%#Eval("GroupId")%>'>
                        Ȩ������</a> | <asp:LinkButton ID="LnkDelete" CommandName="DeleteUserGroup" OnClientClick="if(!this.disabled) return confirm('ȷʵҪɾ��ѡ�еĻ�Ա�飿');" CommandArgument='<%# Eval("GroupId")%>' runat="server">ɾ��</asp:LinkButton> |      
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
