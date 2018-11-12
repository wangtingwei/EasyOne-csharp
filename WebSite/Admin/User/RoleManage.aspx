<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.User.RoleManage"
    Title="��ɫ����" Codebehind="RoleManage.aspx.cs" %>

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
            <pe:BoundField DataField="RoleName" HeaderText="��ɫ��" HeaderStyle-Width="15%"  />
            <pe:TemplateField HeaderText="����">
                <ItemStyle HorizontalAlign="Left" />
                <ItemTemplate>
                    <%# EasyOne.Common.StringHelper.SubString(Eval("Description").ToString(),53,"...")%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="�������">
                <HeaderStyle Width="16%" />
                <ItemTemplate>
                    <a href="RoleMember.aspx?RoleId=<%# Eval("RoleId")%>&RoleName=<%#Server.UrlEncode(Eval("RoleName").ToString())%>">
                        ��Ա����</a>
                    <asp:LinkButton ID="LnkModify" CommandName="ModifyRole" CommandArgument='<%# Eval("RoleId")%>'
                        runat="server">�޸�</asp:LinkButton>
                    <asp:LinkButton ID="LnkDelete" CommandName="DeleteRole" OnClientClick="if(!this.disabled) return confirm('ȷʵҪɾ���˽�ɫ��');"
                        CommandArgument='<%# Eval("RoleId")%>' runat="server">ɾ��</asp:LinkButton>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="Ȩ������">
                <HeaderStyle Width="22%" />
                <ItemTemplate>
                    <asp:LinkButton ID="LnkModifyCommonPermissions" CommandName="CommonPermissions" CommandArgument='<%# Eval("RoleId")%>'
                        runat="server">����Ȩ������</asp:LinkButton>
                    <asp:LinkButton ID="LnkModifyFieldPermissions" CommandName="FieldPermissions" CommandArgument='<%# Eval("RoleId")%>'
                        runat="server">�ֶ�Ȩ������</asp:LinkButton>
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
