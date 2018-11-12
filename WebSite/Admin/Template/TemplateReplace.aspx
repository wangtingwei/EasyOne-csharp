<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Template.TemplateReplace"
    MasterPageFile="~/Admin/MasterPage.master" Title="模板批量替换"
    ValidateRequest="false" Codebehind="TemplateReplace.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <b>模板内容替换 </b>
            </td>
        </tr>
        <tr class="tdbg">
            <td align="right" class="tdbgleft" style="width: 10%">
                <b>替换范围：</b>
            </td>
            <td>
                <asp:DropDownList ID="DropReplaceFile" runat="server" Width="50%">
                    <asp:ListItem Text="/" Value="/"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr class="tdbg">
            <td align="right" class="tdbgleft" style="width: 10%">
                <b>要替换的内容：</b>
            </td>
            <td>
                <asp:TextBox ID="TxtOriginalContent" Width="50%" runat="server" Height="54px" TextMode="MultiLine"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrOriginalContent" ControlToValidate="TxtOriginalContent" Display="Dynamic" runat="server"
                        ErrorMessage="要替换的内容不能为空"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td align="right" class="tdbgleft" style="width: 10%">
                <b>替换后的内容：</b>
            </td>
            <td>
                <asp:TextBox ID="TxtNewContent" Width="50%" runat="server" Height="51px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
    </table>
    <br />
    <center>
        <asp:Button ID="BtnReplace" runat="server" Text=" 替换 " OnClick="BtnReplace_Click" />
        <asp:Button ID="BtnBack" runat="server" Text=" 返回 " OnClick="BtnBack_Click" UseSubmitBehavior="False"   CausesValidation="False"/>
</asp:Content>
