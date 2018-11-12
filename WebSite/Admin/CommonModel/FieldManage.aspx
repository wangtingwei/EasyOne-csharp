<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.CommonModel.FieldManage"
    Title="字段管理" Codebehind="FieldManage.aspx.cs" %>

<asp:Content ID="Content3" ContentPlaceHolderID="CphNavigation" runat="Server">
    <table style="width: 100%; margin: 0 auto;" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
            </td>
            <td align="right">
                <pe:ExtendedLabel HtmlEncode="false" ID="LblModelName" runat="server" Text=""></pe:ExtendedLabel>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="EgvField" runat="server" AutoGenerateColumns="False" DataKeyNames="FieldName"
        DataSourceID="OdsField" ItemName="字段" ItemUnit="个" SerialText="" OnRowCommand="EgvField_RowCommand"
        OnRowDataBound="EgvField_RowDataBound">
        <Columns>
            <pe:TemplateField HeaderText="字段名" SortExpression="FieldName">
                <HeaderStyle Width="12%" />
                <ItemTemplate>
                    <a href='Field.aspx?Action=Modify&ModelType=<%# RequestInt32("ModelType").ToString() %>&ModelID=<%# Request.QueryString["ModelID"]%>&ModelName=<%# Request.QueryString["ModelName"]%>&FieldName=<%#Server.HtmlEncode(Eval("FieldName").ToString())%>'>
                        <%# Eval("FieldName").ToString().Length <= 20 ? Eval("FieldName") : Eval("FieldName").ToString().Substring(0, 5) + ".."%>
                    </a>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="字段别名" SortExpression="FieldAlias">
                <HeaderStyle Width="12%" />
                <ItemTemplate>
                    <%# Eval("FieldAlias") == null ? "" : (Eval("FieldAlias").ToString().Length <= 10 ? Eval("FieldAlias") : Eval("FieldAlias").ToString().Substring(0, 10) + "..")%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="字段类型" SortExpression="FieldType">
                <HeaderStyle Width="12%" />
                <ItemTemplate>
                    <%# EasyOne.CommonModel.Field.GetFieldTypeName((int)Eval("FieldType"))%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="字段级别" SortExpression="EnableNull">
                <HeaderStyle Width="10%" />
                <ItemTemplate>
                    <%# (int)Eval("FieldLevel")==0 ? "<span style='color:Green'>系统</span>" : "自定义"%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="是否必填" SortExpression="EnableNull">
                <HeaderStyle Width="10%" />
                <ItemTemplate>
                    <%# (bool)Eval("EnableNull") ? "<B><span style='color:#000000'>√</span></B>" : "<B><span style='color:Red'>×</span></B>"%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="是否启用" SortExpression="Disabled">
                <HeaderStyle Width="10%" />
                <ItemTemplate>
                    <%# (bool)Eval("Disabled") ? "<span style='color:Red'>×</span>" : "<B>√</B>"%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="排序" SortExpression="Disabled">
                <HeaderStyle Width="10%" />
                <ItemTemplate>
                    <asp:DropDownList ID="DropOrderId" runat="server">
                    </asp:DropDownList>
                </ItemTemplate>
            </pe:TemplateField>
                        <pe:TemplateField HeaderText="管理操作">
                <HeaderStyle Width="24%" />
                <ItemTemplate>
                    <asp:LinkButton ID="ELbtnDelField" Text="删除"  runat="server" CommandName="DeleteField" CommandArgument='<%# Eval("FieldName")%>' />
                    | <a href='<%# "Field.aspx?Action=Modify&ModelID=" + Request.QueryString["ModelID"] +"&ModelType="+Request.QueryString["ModelType"]+"&ModelName="+Server.UrlEncode(Request.QueryString["ModelName"]) +"&FieldName="+Server.UrlEncode(Eval("FieldName").ToString())%>'>
                        修改</a> |
                    <asp:LinkButton runat="server" ID="ELbtnDisabled" Text='<%# (bool)Eval("Disabled") ? "启用" : "禁用"%>'
                        CommandName='<%# (bool)Eval("Disabled") ? "Enabled" : "Disabled"%>' CommandArgument='<%# Eval("FieldName")%>' />
                    | <a href='<%# AppendSecurityCode("Field.aspx?Action=Copy&ModelID=" + Request.QueryString["ModelID"] +"&ModelType="+Request.QueryString["ModelType"]+"&ModelName="+Server.UrlEncode(Request.QueryString["ModelName"]) +"&FieldName="+Server.UrlEncode(Eval("FieldName").ToString()))%>'>
                        复制</a>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <br />
    <asp:Button ID="EBtnSubmit" Text="添加字段" OnClick="EBtnSubmit_Click" runat="server" />&nbsp;&nbsp;
    <asp:Button ID="EBtnSetOrderId" Text="保存排序" OnClick="EBtnSetOrderId_Click" runat="server" />&nbsp;&nbsp;
    <asp:Button ID="EBtnPreView" Text="模型预览" OnClick="BtnPreView_Click" runat="server" />&nbsp;&nbsp;
    <asp:ObjectDataSource ID="OdsField" runat="server" SelectMethod="GetFieldList" TypeName="EasyOne.CommonModel.Field">
        <SelectParameters>
            <asp:QueryStringParameter Name="modelId" QueryStringField="modelId" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
