<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.CommonModel.ModelPreView"
    MasterPageFile="~/Admin/MasterPage.master" Title="模型预览" Debug="true" Codebehind="ModelPreView.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <asp:ScriptManager runat="server" ID="SmgeProperties" EnablePartialRendering="true"></asp:ScriptManager>
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td class="spacingtitle" colspan="2">
                <asp:Label ID="LblTitle" runat="server" Text="模型预览" Font-Bold="True"></asp:Label></td>
        </tr>
        <asp:Repeater ID="RepModel" runat="server" OnItemDataBound="RepModel_OnItemDataBound">
            <ItemTemplate>
                <pe:FieldControl ID="Field" runat="server" EnableNull='<%# (bool)Eval("EnableNull") %>'
                    FieldAlias='<%# Eval("FieldAlias")%>' Tips='<%# Eval("Tips") %>' FieldName='<%#Eval("FieldName")%>'
                    ControlType='<%# Eval("FieldType") %>' FieldLevel='<%# Eval("FieldLevel") %>'
                    Description='<%# Eval("Description")%>' Settings='<%# ((EasyOne.Model.CommonModel.FieldInfo)Container.DataItem).Settings %>'
                    Value='<%# Eval("DefaultValue") %>'>
                </pe:FieldControl>
            </ItemTemplate>
        </asp:Repeater>
        <tr class="tdbg">
            <td colspan="2" align="center">
                <asp:Button ID="BtnBack" runat="server"  CausesValidation="false" Text="返回字段列表" OnClick="BtnBack_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
