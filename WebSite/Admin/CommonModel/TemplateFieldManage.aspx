<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master"
    Inherits="EasyOne.WebSite.Admin.CommonModel.TemplateFieldManage" Codebehind="TemplateFieldManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <table style="width: 100%; margin: 0 auto;" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
            </td>
            <td align="right">
                <pe:ExtendedLabel  ID="LblTemplateName" runat="server" Text="" HtmlEncode="false"></pe:ExtendedLabel>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="EgvField" runat="server" AutoGenerateColumns="False" DataKeyNames="FieldName"
        DataSourceID="OdsField" ItemName="字段" ItemUnit="个" SerialText="" OnRowCommand="EgvField_RowCommand"
        OnRowDataBound="EgvField_RowDataBound">
        <Columns>
            <pe:TemplateField HeaderText="字段名" SortExpression="TemplateName">
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <%# Eval("FieldName")%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="字段别名" SortExpression="FieldAlias">
                <ItemTemplate>
                    <%# Eval("FieldAlias")%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="字段类型" SortExpression="FieldType">
                <ItemTemplate>
                    <%# EasyOne.CommonModel.Field.GetFieldTypeName((int)Eval("FieldType"))%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="是否必填" SortExpression="EnableNull">
                <ItemTemplate>
                    <%# (bool)Eval("EnableNull") ? "<span style=\"color:Blue\">必填</span>" : "选填"%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="字段级别" SortExpression="EnableNull">
                <ItemTemplate>
                    <%# (int)Eval("FieldLevel") == 0 ? "<span style=\"color:Green\">系统</span>" : "自定义"%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="管理操作">
                <ItemTemplate>
                    <pe:ExtendedLinkButton ID="ELbtnDelField" Text="删除" IsChecked="true" OperateCode="ContentModelManage"
                        OnClientClick="return confirm('删除字段将删除对应表中所有该字段的数据，是否删除该字段？')" runat="server"
                        CommandArgument='<%# Eval("FieldName")%>' CommandName="DeleteField" />
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <br />
    <asp:ObjectDataSource ID="OdsField" runat="server" SelectMethod="GetFieldList" TypeName="EasyOne.CommonModel.TemplateField">
        <SelectParameters>
            <asp:QueryStringParameter Name="TemplateID" QueryStringField="TemplateId" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
