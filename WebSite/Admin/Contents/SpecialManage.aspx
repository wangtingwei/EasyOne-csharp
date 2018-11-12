<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master" Inherits="EasyOne.WebSite.Admin.Contents.SpecialManage"
    Title="专题管理" Codebehind="SpecialManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <table style="width: 100%; margin: 0 auto;" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
            </td>
            <td align="right">
                <asp:Label ID="LblOrderType" Text="排序方式：" runat="server"></asp:Label>
                <asp:DropDownList ID="DropRescentQuery" AutoPostBack="true" OnSelectedIndexChanged="DropSelectedIndex_Changed"
                    runat="server">
                    <asp:ListItem Value="1">按ID降序</asp:ListItem>
                    <asp:ListItem Value="-2">按ID升序</asp:ListItem>
                    <asp:ListItem Value="2">按专题类别升序</asp:ListItem>
                    <asp:ListItem Value="3">按专题类别降序</asp:ListItem>
                </asp:DropDownList>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="EgvSpecial" runat="server" AutoGenerateColumns="False" AllowPaging="True"
        DataKeyNames="SpecialID" DataSourceID="OdsSpecial" SerialText="" OnRowDataBound="EgvSpecial_RowDataBound">
        <Columns>
            <pe:BoundField DataField="SpecialID" HeaderText="ID" SortExpression="SpecialID">
                <HeaderStyle Width="8%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="专题名称" SortExpression="SpecialName">
                <ItemStyle HorizontalAlign="Left" />
                <ItemTemplate>
                    <pe:ExtendedHyperLink ID="LblSpecialCategoryLink" runat="server" />
                    <asp:HyperLink ID="HypTitle" runat="server"  />
                </ItemTemplate>
                <HeaderStyle Width="27%" />
            </pe:TemplateField>
            <pe:BoundField DataField="SpecialDir" HeaderText="专题目录" SortExpression="SpecialDir">
                <HeaderStyle Width="10%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="打开方式" SortExpression="SpecialID">
                <ItemTemplate>
                    <%# Eval("OpenType").ToString() =="0" ? "原窗口":"新窗口"%>
                </ItemTemplate>
                <HeaderStyle Width="8%" />
            </pe:TemplateField>
            <pe:TemplateField HeaderText="专题描述" SortExpression="Description">
                <ItemStyle HorizontalAlign="Left" />
                <ItemTemplate>
                    <%# Eval("Description").ToString()%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField>
                <HeaderTemplate>
                    专题操作
                </HeaderTemplate>
                <ItemStyle />
                <HeaderStyle Width="20%" />
                <ItemTemplate>
                    <a ID="EahSpecialModify"  href='<%# "Special.aspx?Action=Modify&SpecialID=" + Eval("SpecialID")%>' runat="server">修改</a>
                    <a ID="EahSpecialSpecialDel" href='<%# AppendSecurityCode("SpecialManage.aspx?Action=Delete&SpecialID=" + Eval("SpecialID"))%>'
                        onclick="return confirm('确定要删除此专题吗？');" runat="server">删除</a>
                    <a ID="EahSpecialClear"  href='<%# AppendSecurityCode("SpecialManage.aspx?Action=Clear&SpecialID=" + Eval("SpecialID"))%>'
                        onclick="return confirm('确定要清空该专题的所有内容吗？');" runat="server">清空</a>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <asp:HiddenField ID="HdnListType" runat="server" Value="-1" />
    <asp:ObjectDataSource ID="OdsSpecial" runat="server" SelectMethod="GetSpecialList"
        TypeName="EasyOne.Contents.Special" EnablePaging="True" MaximumRowsParameterName="maxNumberRows"
        StartRowIndexParameterName="startRowIndexId" SelectCountMethod="GetTotalOfSpecial">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="specialCategoryId" QueryStringField="SpecialCategoryId"
                Type="Int32" />
            <asp:ControlParameter ControlID="HdnListType" Type="Int32" Name="listType" PropertyName="Value" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
