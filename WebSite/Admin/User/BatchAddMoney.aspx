<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master"
    Inherits="EasyOne.WebSite.Admin.User.BatchAddMoney" Codebehind="BatchAddMoney.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <b>
                    <asp:Label ID="LblMsg" runat="server" Text="������ӽ���"></asp:Label></b>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width: 15%;" class="tdbgleft">
                <strong>ѡ���Ա��</strong></td>
            <td>
                <pe:SelectUser ID="SelectUser" runat="server" />
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width: 15%;" class="tdbgleft">
                <strong>��</strong></td>
            <td align="left">
                <asp:TextBox ID="TxtMoney" runat="server" Columns="7" MaxLength ="7"></asp:TextBox>
                Ԫ
                <pe:RequiredFieldValidator ID="ValrMoney" ControlToValidate="TxtMoney" runat="server"
                    ErrorMessage="��������Ϊ��" Display="Dynamic"></pe:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="ValeMoney" runat="server" ControlToValidate="TxtMoney"
                    ErrorMessage="ֻ����������ַ�" ValidationExpression="^-?[0-9]+(\.?[0-9]{1,2})?" Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width: 15%;" class="tdbgleft">
                <strong>ԭ��</strong></td>
            <td align="left">
                <asp:TextBox ID="TxtReason" runat="server" Height="50px" Width="300px" TextMode="MultiLine"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrMRemark" ControlToValidate="TxtReason" runat="server"
                    ErrorMessage="����ԭ����Ϊ��"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width: 15%" class="tdbgleft">
                <strong>�ڲ���¼��</strong></td>
            <td>
                <asp:TextBox ID="TxtMemo" runat="server" Width="400px" Columns="50" Height="60px"
                    Rows="4" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td>
            </td>
            <td align="left">
                <asp:CheckBox ID="ChkSaveItem" runat="server" Checked="True" />
                <strong>Ϊÿ����Ա��¼��ϸ��¼���Ƽ���</strong>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="height: 40px;" colspan="2" align="center">
                <asp:Button ID="EBtnSubmit" Text="ִ����������" OnClick="EBtnSubmit_Click" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
