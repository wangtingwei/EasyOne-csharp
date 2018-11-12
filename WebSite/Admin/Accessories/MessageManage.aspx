<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master"
    Inherits="EasyOne.WebSite.Admin.Accessories.MessageManage" Codebehind="MessageManage.aspx.cs" %>

<%@ Register TagPrefix="pe" Src="~/Controls/ShowMessageListAdmin.ascx" TagName="ShowMessageListAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ShowMessageListAdmin ID="ShowAdminMessageList" runat="server" />
    <br />
    <br />
    <table width="100%" cellpadding="3" cellspacing="0" class="border">
        <tr class="tdbg">
            <td align="right" style="width: 15%;">
                <strong>����Ϣ������</strong>
            </td>
            <td>
                <asp:DropDownList ID="DropSearch" runat="server">
                    <asp:ListItem Value="0">��������</asp:ListItem>
                    <asp:ListItem Value="1">����Ϣ����</asp:ListItem>
                    <asp:ListItem Value="2">����Ϣ����</asp:ListItem>
                    <asp:ListItem Value="3">�ռ���</asp:ListItem>
                    <asp:ListItem Value="4">������</asp:ListItem>
                    <asp:ListItem Value="5">ĳ����</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="TxtKeyword" runat="server">�ؼ���</asp:TextBox>
                <asp:Button ID="BtnSubmit" runat="server" Text="����" OnClick="BtnSubmit_Click" />
                <pe:RequiredFieldValidator ID="ValrKeyword" runat="server" ControlToValidate="TxtKeyWord"
                    ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" ShowRequiredText="false">�����������Ĺؼ��֣�</pe:RequiredFieldValidator></td>
        </tr>
    </table>
    <br />
</asp:Content>
