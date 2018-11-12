<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Accessories.Choicesets" Title="数据字典管理" Codebehind="Choiceset.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <div>
        <table width="100%">
            <tr>
                <td style="width: 100%" align="center">
                    <asp:PlaceHolder ID="PlhFormFieldValue" runat="server"></asp:PlaceHolder>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="EBtnSave" Text="保存设置" OnClick="EBtnSave_Click" runat="server" /></td>
            </tr>
        </table>
    </div>
</asp:Content>
