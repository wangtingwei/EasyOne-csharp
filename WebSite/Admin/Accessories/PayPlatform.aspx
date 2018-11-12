<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Accessories.PayPlatforms" Codebehind="PayPlatform.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <b>
                    <pe:AlternateLiteral ID="AltrTitle" Text="���֧��ƽ̨" AlternateText="�޸�֧��ƽ̨" runat="Server" />
                </b>
            </td>
        </tr>
        <tr class="tdbg">
            <td width="30%" align="right">
                ƽ̨���ƣ�</td>
            <td>
                <asp:TextBox ID="TxtPlatformName" runat="server" MaxLength="50"></asp:TextBox>
                <span style="color: #000000">
                    <pe:RequiredFieldValidator ID="ValrPlatformName" runat="server" ControlToValidate="TxtPlatformName"
                        ErrorMessage="ƽ̨���Ʋ���Ϊ��" Display="Dynamic"></pe:RequiredFieldValidator></span>
            </td>
        </tr>
        <tr class="tdbg">
            <td width="30%" align="right">
                �̻�ID��</td>
            <td>
                <asp:TextBox ID="TxtAccountsID" runat="server" MaxLength="50"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrAccountsID" runat="server" ControlToValidate="TxtAccountsID"
                    ErrorMessage="�̻�ID����Ϊ��" Display="Dynamic"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td width="30%" align="right">
                MD5��Կ��</td>
            <td>
                <asp:TextBox ID="TxtMD5" runat="server" MaxLength="255"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrMD5" runat="server" ControlToValidate="TxtMD5"
                    ErrorMessage="MD5��Կ����Ϊ��" Display="Dynamic"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td width="30%" align="right">
                �������ʣ�</td>
            <td>
                <asp:TextBox ID="TxtRate" runat="server" Width="59px" MaxLength="6"></asp:TextBox>%
            </td>
        </tr>
        <tr class="tdbg">
            <td align="right" width="30%">
                ��ΪĬ�ϣ�</td>
            <td>
                <input type="checkbox" id="ChkIsDefault" runat="server" onclick="DefalutChange()" />
                &nbsp; &nbsp; ���ã�<input type="checkbox" id="ChkIsDisabled" runat="server" /></td>
        </tr>
        <tr align="center" class="tdbg">
            <td height="50" colspan="2">
                <pe:AlternateButton ID="BtnSubmit" runat="server" Text="���֧��ƽ̨" AlternateText="�޸�֧��ƽ̨"
                    OnClick="BtnSubmit_Click" />
                <input type="button" id="return" onclick="window.location.href='PayPlatformManage.aspx';" class="inputbutton" value="����" />
            </td>
        </tr>
    </table>

    <script type="text/javascript">
        function DefalutChange()
        {
            document.getElementById('<%=ChkIsDisabled.ClientID %>').disabled = document.getElementById('<%=ChkIsDefault.ClientID %>').checked;
            if(document.getElementById('<%=ChkIsDefault.ClientID %>').checked)
            {
                document.getElementById('<%=ChkIsDisabled.ClientID %>').checked = false;
            }
        }
    </script>

</asp:Content>
