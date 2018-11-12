<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Template.PagerManageUI" Title="分页标签管理" Codebehind="PagerManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr class="title">
            <td style="width: 88px">
                <strong>分类列表：</strong></td>
            <td align="left">
                <a href="PagerManage.aspx">显示全部</a>
                <asp:Repeater ID="RptLabel" runat="server" DataSourceID="OdsPagerType">
                    <ItemTemplate>
                        | <a href="PagerManage.aspx?type=<%# Eval("Name") %>">
                            <%# Eval("Name") %>
                        </a>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:ObjectDataSource ID="OdsPagerType" runat="server" SelectMethod="GetPagerTypeList"
                    TypeName="EasyOne.Templates.PagerManage"></asp:ObjectDataSource>
            </td>
        </tr>
    </table>
    <pe:ExtendedGridView ID="GdvPagerList" AutoGenerateColumns="False" runat="Server"
        DataSourceID="OdsPager" DataKeyNames="Name" AllowPaging="True" ItemName="标签"
        ItemUnit="个" AutoGenerateCheckBoxColumn="True" OnRowCommand="GdvPagerList_RowCommand"
        PageSize="20">
        <Columns>
            <pe:BoundField DataField="Type" HeaderText="分类" SortExpression="Type" >
                <HeaderStyle Width="10%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="名称">
                <ItemStyle HorizontalAlign="Left" />
                <ItemTemplate>
                    <a href="Pager.aspx?Action=Modify&Name=<%# Server.UrlEncode(Eval("Name").ToString()) %>">
                        <%# Eval("Name") %>
                    </a>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="简介">
                <ItemStyle HorizontalAlign="Left" />
                <ItemTemplate>
                    <%# Eval("Intro") %>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:BoundField DataField="UpDateTime" HeaderText="更新时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                SortExpression="UpDateTime" HtmlEncode="False">
                <HeaderStyle Width="15%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="操作">
                <HeaderStyle Width="16%" />
                <ItemTemplate>
                    <a href="Pager.aspx?Action=Modify&Name=<%# Server.UrlEncode(Eval("Name").ToString()) %>">
                        修改</a>
                    <asp:LinkButton ID="LbtnDelete" runat="server" CausesValidation="False" CommandArgument='<%# Bind("Name") %>'
                        CommandName="Deleted" OnClientClick="return confirm('是否删除本标签？')">删除</asp:LinkButton>
                    <asp:LinkButton ID="LbtnCopy" runat="server" CausesValidation="False" CommandArgument='<%# Bind("Name") %>'
                        CommandName="Copy">复制</asp:LinkButton>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <br />
    <asp:Button ID="BtnDel" runat="server" Text="批量删除选定标签" CausesValidation="False" OnClientClick="return confirm('确实要删除选中的标签？');"
        OnClick="BtnDel_Click" />&nbsp;&nbsp;<asp:Button ID="BtnAdd" runat="server" Text="增加一个新标签"
            OnClick="BtnAdd_Click" />
    <asp:ObjectDataSource ID="OdsPager" runat="server" TypeName="EasyOne.Templates.PagerManage"
        SelectMethod="GetPagerList">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="" Name="type" QueryStringField="type" Type="string" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
