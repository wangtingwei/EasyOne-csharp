<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.User.AdministratorManage"
    Title="管理员管理" Codebehind="AdministratorManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="server">
    <pe:ExtendedGridView ID="Egv" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        CheckBoxFieldHeaderWidth="3%" DataSourceID="ObjectDataSource1" SerialText=""
        OnRowDataBound="Egv_RowDataBound" OnRowCommand="Egv_RowCommand"
        RowDblclickBoundField="AdminId" 
        RowDblclickUrl="Administrator.aspx?Action=Modify&amp;AdminId={$Field}">
        <Columns>
            <pe:BoundField DataField="AdminId" HeaderText="ID" HeaderStyle-Width="4%" />
            <pe:TemplateField HeaderText="管理员名">
                <ItemTemplate>
                    <asp:HyperLink ID="LnkManageName" runat="server"></asp:HyperLink>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="前台用户名">
                <ItemTemplate>
                    <pe:ExtendedHyperLink ID="HypUserName" runat="server"></pe:ExtendedHyperLink>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="所属角色">
                <ItemStyle HorizontalAlign="left" />
                <ItemTemplate>
                    <asp:Label runat="server" ID="LabRoleList"></asp:Label>
                    <pe:ExtendedLiteral HtmlEncode="false" ID="LtrRoleList" runat="server"></pe:ExtendedLiteral>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="多人登录">
                <ItemTemplate>
                    <pe:ExtendedLabel HtmlEncode="false" runat="server" ID="LabEnableMultiLogin"></pe:ExtendedLabel>
                </ItemTemplate>
            </pe:TemplateField>
           <%-- <pe:BoundField DataField="LastLoginIP" HeaderText="最后登录IP" />--%>
            <pe:TemplateField HeaderText="最后登录IP<br/>最后登录时间">
                <ItemTemplate>
                    <asp:Label runat="server" ID="LabLastLoginIp"></asp:Label><br />
                    <asp:Label runat="server" ID="LabLastLoginTime"></asp:Label>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="上次修改密码时间">
                <ItemTemplate>
                    <asp:Label runat="server" ID="LabLastModifyPasswordTime"></asp:Label>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:BoundField DataField="LogOnTimes" HeaderText="登录次数" />
          
            <pe:TemplateField HeaderText="管理员状态">
                <ItemTemplate>
                    <pe:ExtendedLabel HtmlEncode="false" runat="server" ID="LabIsLock"></pe:ExtendedLabel>
                    
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <asp:LinkButton ID="LnkLock" CommandName="LockAdmin" CommandArgument='<%# Eval("AdminId")%>'
                        runat="server">禁用</asp:LinkButton>
                    <asp:LinkButton ID="LnkModify" CommandName="ModifyAdmin" CommandArgument='<%# Eval("AdminId")%>'
                        runat="server">修改</asp:LinkButton>
                    <asp:LinkButton ID="LnkDelete" CommandName="DeleteAdmin" OnClientClick="if(!this.disabled) return confirm('确实要删除此管理员吗？');"
                        CommandArgument='<%# Eval("AdminId")%>' runat="server">删除</asp:LinkButton>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <asp:HiddenField ID="HdnRoleId" runat="server" Value="-1" />
    <asp:HiddenField ID="HdnListType" runat="server" Value="0" />
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="true" SelectMethod="AdminList"
        TypeName="EasyOne.UserManage.Administrators" StartRowIndexParameterName="startRowIndexId"
        MaximumRowsParameterName="maxNumberRows" SelectCountMethod="GetTotalOfAdmin">
        <SelectParameters>
            <asp:ControlParameter ControlID="HdnRoleId" Type="Int32" Name="roleId" PropertyName="Value" />
            <asp:ControlParameter ControlID="HdnListType" Type="Int32" Name="listType" PropertyName="Value" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
