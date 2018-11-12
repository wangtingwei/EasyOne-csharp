<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Accessories.DownServers" Title="���ط���������" Codebehind="DownServer.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <strong>
                    <pe:AlternateLiteral ID="AltrTitle" Text="��ӷ�����" AlternateText="�޸ķ�����" runat="Server" />
                </strong>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="left" style="width: 40%">
                <strong>���������ƣ�</strong><br />
                �ڴ�������ǰ̨��ʾ�ľ��������������㶫���ء��Ϻ����صȡ�
            </td>
            <td class="tdbg" style="text-align: left; width: 60%;">
                <asp:TextBox ID="TxtServerName" runat="server" Width="290px"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrServerName" runat="server" ErrorMessage="���ط��������Ʋ���Ϊ��"
                    ControlToValidate="TxtServerName"></pe:RequiredFieldValidator></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="left" style="width: 40%">
                <strong>������LOGO��</strong><br />
                ���������LOGO�ľ��Ե�ַ����http://www.EasyOne.net/Soft/Images/ServerLogo.gif
            </td>
            <td class="tdbg" style="text-align: left; width: 60%;">
                <asp:TextBox ID="TxtServerLogo" runat="server" Width="290px"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="left" style="width: 40%; height: 49px;">
                <strong>��������ַ��</strong><br />
                ������������ȷ�ķ�������ַ��<br />
                ��http://www.EasyOne.net/�����ĵ�ַ
            </td>
            <td class="tdbg" style="text-align: left; width: 60%; height: 49px;">
                <asp:TextBox ID="TxtServerUrl" runat="server" Width="290px"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrServerUrl" runat="server" ErrorMessage="���ط�������ַ����Ϊ��"
                    ControlToValidate="TxtServerUrl"></pe:RequiredFieldValidator></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="left" style="width: 40%">
                <strong>��ʾ��ʽ��</strong>
            </td>
            <td class="tdbg" style="text-align: left; width: 60%;">
                <asp:DropDownList ID="DropShowType" runat="server">
                    <asp:ListItem Value="0">��ʾ����</asp:ListItem>
                    <asp:ListItem Value="1">��ʾLOGO</asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr class="tdbg">
            <td style="text-align: center" colspan="2">
                <br />
                <asp:Button ID="EBtnSubmit" Text="����" OnClick="EBtnSubmit_Click" runat="server" />&nbsp;&nbsp;
                <input name="BtnCancel" type="button" class="inputbutton" onclick="Redirect('DownServerManage.aspx')"
                    value=" ȡ�� " />
            </td>
        </tr>
    </table>
</asp:Content>
