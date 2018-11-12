<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.User.UserGroup" Title="�û���Ա�����" Codebehind="UserGroup.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <b>
                    <pe:AlternateLiteral ID="LblTitle" Text="��ӻ�Ա��" AlternateText="�޸Ļ�Ա��" runat="Server" /></b>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>��Ա�����ƣ�</strong></td>
            <td>
                <asp:TextBox ID="TxtGroupName" runat="server" />
                <pe:RequiredFieldValidator ControlToValidate="TxtGroupName" ID="ValrGroupName" runat="server"
                    ErrorMessage="��Ա��������Ϊ�գ�" SetFocusOnError="True" /><div id="Msg">
                    </div>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>��Ա�����ͣ�</strong></td>
            <td>
                <asp:DropDownList ID="DropGropType" runat="server" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>��Ա��˵����</strong></td>
            <td>
                <asp:TextBox ID="TxtDescription" runat="server" Height="69px" TextMode="MultiLine"
                    Width="232px" />
            </td>
        </tr>
    </table>
    <center>
        <br />
        <asp:Button ID="BtnSubmit" runat="server" Text="�����Ա�鲢����Ȩ������" OnClick="ButtonSubmit_Click" />&nbsp;&nbsp;&nbsp;
        <asp:Button ID="BtnCancel" runat="server" Text="ȡ��" OnClick="ButtonCancel_Click"
            ValidationGroup="BtnCancel" />
    </center>
    <br />
    <asp:HiddenField ID="HdnGroupName" runat="server" />
    <asp:HiddenField ID="HdnAction" runat="server" />
    <asp:HiddenField ID="HdnGroupId" runat="server" />
</asp:Content>
