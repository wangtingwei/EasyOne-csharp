<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.CommonModel.ModelTemplate"
    MasterPageFile="~/Admin/MasterPage.master" Title="添加模型模板" Codebehind="ModelTemplate.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellspacing="1" cellpadding="2" class="border">
        <tr align="center">
            <td colspan="2" class="title">
                <b>
                    <asp:Label ID="LblTitle" runat="server" Text="添加模型模板"></asp:Label></b>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width: 150px;" class="tdbgleft" align="right">
                <strong>模型模板名称：&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtTemplateName" MaxLength="150" runat="server"></asp:TextBox>
                <pe:RequiredFieldValidator ControlToValidate="TxtTemplateName" ID="ValrTemplateName"
                    runat="server" ErrorMessage="模型模板名称不能为空！"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width: 150px;" class="tdbgleft" align="right">
                <strong>模型模板描述：&nbsp;</strong></td>
            <td align="left">
                <asp:TextBox ID="TxtTemplateDescription" runat="server" Height="79px" Width="391px"
                    TextMode="MultiLine"></asp:TextBox>&nbsp;</td>
        </tr>
        <tr align="center">
            <td colspan="2" class="tdbg">
                <asp:HiddenField ID="HdnModelId" runat="server" />
                <pe:AlternateButton ID="BtnSubmit" runat="server" Text="添加" AlternateText="修改结果"
                    Width="80px" OnClick="BtnSubmit_Click" />&nbsp;&nbsp; &nbsp;
                <input name="Cancel" type="button" class="inputbutton" id="Cancel" value="取消" onclick="window.history.go(-1)" />
            </td>
        </tr>
    </table>
</asp:Content>
