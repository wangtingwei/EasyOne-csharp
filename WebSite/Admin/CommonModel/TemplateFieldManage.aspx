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
        DataSourceID="OdsField" ItemName="�ֶ�" ItemUnit="��" SerialText="" OnRowCommand="EgvField_RowCommand"
        OnRowDataBound="EgvField_RowDataBound">
        <Columns>
            <pe:TemplateField HeaderText="�ֶ���" SortExpression="TemplateName">
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <%# Eval("FieldName")%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="�ֶα���" SortExpression="FieldAlias">
                <ItemTemplate>
                    <%# Eval("FieldAlias")%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="�ֶ�����" SortExpression="FieldType">
                <ItemTemplate>
                    <%# EasyOne.CommonModel.Field.GetFieldTypeName((int)Eval("FieldType"))%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="�Ƿ����" SortExpression="EnableNull">
                <ItemTemplate>
                    <%# (bool)Eval("EnableNull") ? "<span style=\"color:Blue\">����</span>" : "ѡ��"%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="�ֶμ���" SortExpression="EnableNull">
                <ItemTemplate>
                    <%# (int)Eval("FieldLevel") == 0 ? "<span style=\"color:Green\">ϵͳ</span>" : "�Զ���"%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="�������">
                <ItemTemplate>
                    <pe:ExtendedLinkButton ID="ELbtnDelField" Text="ɾ��" IsChecked="true" OperateCode="ContentModelManage"
                        OnClientClick="return confirm('ɾ���ֶν�ɾ����Ӧ�������и��ֶε����ݣ��Ƿ�ɾ�����ֶΣ�')" runat="server"
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
