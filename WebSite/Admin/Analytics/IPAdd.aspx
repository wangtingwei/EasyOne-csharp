<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Analytics.IPAdd" Title="ͳ��IP�����" Codebehind="IPAdd.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table border="0" style="text-align: center; width: 100%" cellpadding="2" cellspacing="1"
        class="border">
        <tr class="spacingtitle">
            <td align="center" colspan="2">
                <strong>ͳ��IP��<asp:Label ID="LblTitle" runat="server" Text="���" /></strong>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width: 350px; text-align: left;" class="tdbgleft">
                <strong>��ʼ IP��</strong><br />
                ע�� ��ӵ�IP�β��������ݿ��е�IP���ص�����������ӡ�</td>
            <td class="tdbg" style="text-align: left">
                <asp:TextBox ID="TxtStartIP" runat="server" Width="200px" />
                <pe:RequiredFieldValidator ID="ValrStartIP" runat="server" ControlToValidate="TxtStartIP"
                    Display="Dynamic" ErrorMessage="����Ϊ��" SetFocusOnError="True" /><asp:RegularExpressionValidator
                        ID="ValeStartIP" runat="server" ControlToValidate="TxtStartIP" Display="Dynamic"
                        ErrorMessage="������Ч��IP��ַ" SetFocusOnError="True" ValidationExpression="^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$"
                        EnableTheming="True" /></td>
        </tr>
        <tr class="tdbg">
            <td style="width: 350px; text-align: left; height: 23px;" class="tdbgleft">
                <strong>��β IP��</strong></td>
            <td class="tdbg" style="text-align: left; height: 23px;">
                <asp:TextBox ID="TxtEndIP" runat="server" Width="200px" />
                <pe:RequiredFieldValidator ID="ValrEndIP" runat="server" ControlToValidate="TxtEndIP"
                    Display="Dynamic" ErrorMessage="����Ϊ��" SetFocusOnError="True" /><asp:RegularExpressionValidator
                        ID="ValeEndIp" runat="server" ControlToValidate="TxtEndIP" Display="Dynamic"
                        ErrorMessage="������Ч��IP��ַ" SetFocusOnError="True" ValidationExpression="^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$" /></td>
        </tr>
        <tr class="tdbg">
            <td style="width: 350px; text-align: left;" class="tdbgleft">
                <strong>��Դ��ϸ��ַ��</strong>
            </td>
            <td class="tdbg" style="text-align: left">
                <asp:TextBox ID="TxtIPAddress" runat="server" Width="200px" MaxLength="50" />
                <pe:RequiredFieldValidator ID="ValrAddress" runat="server" ControlToValidate="TxtIPAddress"
                    Display="Dynamic" ErrorMessage="����Ϊ��" SetFocusOnError="True" /></td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:Button ID="BtnSave" runat="server" Text="���" OnClick="BtnSave_Click" />&nbsp;
                <asp:Button ID="BtnCancel" runat="server" CausesValidation="false" Text="ȡ��" UseSubmitBehavior="false"
                    OnClick="BtnCancel_Click" /></td>
        </tr>
    </table>
    <asp:HiddenField ID="HdnOldStartIP" runat="server" />
    <asp:HiddenField ID="HdnOldEndIP" runat="server" />
</asp:Content>
