<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master" Inherits="EasyOne.WebSite.Admin.Contents.SpecialCategoryManage"
    Title="专题类别管理" Codebehind="SpecialCategoryManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="EgvSpecialCategory" runat="server" AutoGenerateColumns="False"
        DataKeyNames="SpecialCategoryID" DataSourceID="OdsSpecialCategory" SerialText=""
        OnRowDataBound="EgvSpecialCategory_RowDataBound"
         RowDblclickBoundField="SpecialCategoryID" 
        RowDblclickUrl="SpecialCategory.aspx?Action=Modify&amp;SpecialCategoryID={$Field}">
        <Columns>
            <pe:BoundField DataField="SpecialCategoryID" HeaderText="ID" SortExpression="SpecialCategoryID">
                <HeaderStyle Width="10%" />
            </pe:BoundField>
            <pe:BoundField DataField="SpecialCategoryName" HeaderText="专题类别名称" SortExpression="SpecialCategoryName">
                <HeaderStyle Width="20%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="专题类别描述" SortExpression="Description">
                <ItemStyle HorizontalAlign="Left" />
                <ItemTemplate>
                    <%# Eval("Description").ToString()%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField>
                <HeaderTemplate>
                    专题类别操作
                </HeaderTemplate>
                <HeaderStyle Width="35%" />
                <ItemTemplate>
                    <asp:HyperLink ID="HypSpecialAdd" runat="server"></asp:HyperLink> | 
                    <asp:HyperLink ID="HypSpecialBathAdd" runat="server"></asp:HyperLink>
                    | <a id="EahSpecialManage" href='<%# "SpecialManage.aspx?SpecialCategoryID=" + Eval("SpecialCategoryID")%>'
                        runat="server">专题列表</a> |
                    <asp:HyperLink ID="HypSpecialOrder" runat="server"></asp:HyperLink>
                    | <a id="EahSpecialCategoryModify" href='<%# "SpecialCategory.aspx?Action=Modify&SpecialCategoryID=" + Eval("SpecialCategoryID")%>'
                        runat="server">修改</a> | <a id="EahSpecialCategoryDelete" href='<%# AppendSecurityCode("SpecialCategoryManage.aspx?Action=Delete&SpecialCategoryID=" + Eval("SpecialCategoryID"))%>'
                            onclick="return confirm('确定要删除此专题类别吗？');" runat="server">删除</a>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <asp:ObjectDataSource ID="OdsSpecialCategory" runat="server" SelectMethod="GetSpecialCategoryList"
        TypeName="EasyOne.Contents.Special"></asp:ObjectDataSource>
<br />
        <input type='button' value='添加专题类别' onclick="location.href='SpecialCategory.aspx'" />
        &nbsp;&nbsp;
        <input type='button' value='专题类别排序' onclick="location.href='SpecialCategoryOrder.aspx'" />
</asp:Content>
