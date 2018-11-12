<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master"
    Inherits="EasyOne.WebSite.Admin.Accessories.Banks" Codebehind="Bank.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <pe:AlternateLiteral ID="AltrTitle" Text="��������˻�" AlternateText="�޸������˻�" runat="Server" />
            </td>
        </tr>
        <tr class="tdbg">
            <td align="right" style="width: 28%">
                <strong>�˻����ƣ�</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtBankShortName" runat="server" Width="220px" MaxLength="20" />
                <pe:RequiredFieldValidator ID="ValrBankShortName" runat="server" ControlToValidate="TxtBankShortName"
                    ErrorMessage="�˻����Ʋ���Ϊ�գ�" SetFocusOnError="true" Display="Dynamic" />
                ¼���Ͳ������޸�
            </td>
        </tr>
        <tr class="tdbg">
            <td align="right">
                <strong>�����У�</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtBankName" runat="server" Width="220px" MaxLength="50" />
                <pe:RequiredFieldValidator ID="ValrBankName" runat="server" ControlToValidate="TxtBankName"
                    ErrorMessage="�����в���Ϊ�գ�" SetFocusOnError="true" Display="Dynamic" />
            </td>
        </tr>
        <tr class="tdbg">
            <td align="right">
                <strong>������</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtHolderName" runat="server" Width="220px" MaxLength="20" />
                <pe:RequiredFieldValidator ID="ValrHolderName" runat="server" ControlToValidate="TxtHolderName"
                    ErrorMessage="��������Ϊ��" SetFocusOnError="true" Display="Dynamic" />
            </td>
        </tr>
        <tr class="tdbg">
            <td align="right">
                <strong>�˻���</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtAccounts" runat="server" Width="220px" MaxLength="30" />
                <asp:CustomValidator ID="ValxAccounts" ClientValidationFunction="ValxAccounts_ClientValidate"
                    Display="dynamic" ValidateEmptyText="true" SetFocusOnError="true" runat="server"
                    ErrorMessage="����Ҫ�����ʺźͿ��ŵ�����һ��" OnServerValidate="ValxAccounts_ServerValidate"
                    ControlToValidate="TxtAccounts" />
            </td>
        </tr>
        <tr class="tdbg">
            <td align="right">
                <strong>���ţ�</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtCardNum" runat="server" Width="220px" MaxLength="30" />
            </td>
        </tr>
        <tr class="tdbg">
            <td align="right">
                <strong>����ͼ�꣺</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtBankPic" runat="server" Width="300px" MaxLength="200" /></td>
        </tr>
        <tr class="tdbg">
            <td align="right">
                <strong>�˻�˵����</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtBankIntro" runat="server" TextMode="MultiLine" Width="300px"
                    Height="50px" MaxLength="255" />
                <asp:CustomValidator ID="ValxBankIntro" ClientValidationFunction="ValxBankIntro_ClientValidate"
                    Display="dynamic" ValidateEmptyText="true" SetFocusOnError="true" runat="server"
                    ErrorMessage="�˻�˵�����Ȳ��ܳ���255" ControlToValidate="TxtBankIntro" />
            </td>
        </tr>
        <tr class="tdbg">
            <td align="right">
                &nbsp;</td>
            <td>
                <asp:CheckBox ID="ChkIsDefault" runat="server" />��ΪĬ�������˻�</td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="2">
                <pe:AlternateButton ID="BtnSubmit" runat="server" Text="���������˻�" AlternateText="�޸������˻���Ϣ"
                    OnClick="BtnSubmit_Click" />
            </td>
        </tr>
    </table>

    <script language="javascript" type="text/javascript">
        function ValxAccounts_ClientValidate(s,e)
        {
            var accounts = document.getElementById('<%=TxtAccounts.ClientID %>').value;
            var cardNum = document.getElementById('<%=TxtCardNum.ClientID %>').value;
            if(accounts == "" && cardNum == "")
            {
                e.IsValid = false;
            }
            else
            {
                e.IsValid = true;
            }
        }
        function ValxBankIntro_ClientValidate(s,e)
        {
            var accounts = document.getElementById('<%=TxtBankIntro.ClientID %>').value;
            if(accounts.length >255)
            {
                e.IsValid = false;
            }
            else
            {
                e.IsValid = true;
            }
        }
    </script>

</asp:Content>
