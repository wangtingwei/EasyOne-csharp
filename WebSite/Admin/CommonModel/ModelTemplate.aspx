<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.CommonModel.ModelTemplate"
    MasterPageFile="~/Admin/MasterPage.master" Title="���ģ��ģ��" Codebehind="ModelTemplate.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellspacing="1" cellpadding="2" class="border">
        <tr align="center">
            <td colspan="2" class="title">
                <b>
                    <asp:Label ID="LblTitle" runat="server" Text="���ģ��ģ��"></asp:Label></b>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width: 150px;" class="tdbgleft" align="right">
                <strong>ģ��ģ�����ƣ�&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtTemplateName" MaxLength="150" runat="server"></asp:TextBox>
                <pe:RequiredFieldValidator ControlToValidate="TxtTemplateName" ID="ValrTemplateName"
                    runat="server" ErrorMessage="ģ��ģ�����Ʋ���Ϊ�գ�"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width: 150px;" class="tdbgleft" align="right">
                <strong>ģ��ģ��������&nbsp;</strong></td>
            <td align="left">
                <asp:TextBox ID="TxtTemplateDescription" runat="server" Height="79px" Width="391px"
                    TextMode="MultiLine"></asp:TextBox>&nbsp;</td>
        </tr>
        <tr align="center">
            <td colspan="2" class="tdbg">
                <asp:HiddenField ID="HdnModelId" runat="server" />
                <pe:AlternateButton ID="BtnSubmit" runat="server" Text="���" AlternateText="�޸Ľ��"
                    Width="80px" OnClick="BtnSubmit_Click" />&nbsp;&nbsp; &nbsp;
                <input name="Cancel" type="button" class="inputbutton" id="Cancel" value="ȡ��" onclick="window.history.go(-1)" />
            </td>
        </tr>
    </table>
</asp:Content>
