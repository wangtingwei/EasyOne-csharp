<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Accessories.WordFilter"
    MasterPageFile="~/Admin/MasterPage.master" Title="�ַ����˹���" ValidateRequest="false" Codebehind="WordFilter.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <pe:AlternateLiteral ID="AltrTitle" Text="����ַ�������Ϣ" AlternateText="�޸��ַ�������Ϣ" runat="Server" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>�滻Ŀ�꣺&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtSourceWord" runat="server" Columns="50"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrSourceWord" ControlToValidate="TxtSourceWord"
                    runat="server" ErrorMessage="�滻Ŀ�겻��Ϊ�գ�" Display="Dynamic"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>�滻�����&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtTargetWord" runat="server" Columns="50"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrTargetWord" ControlToValidate="TxtTargetWord"
                    runat="server" ErrorMessage="�滻�������Ϊ�գ�" Display="Dynamic"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>���ȼ���&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtPriority" runat="server" Columns="5" MaxLength ="5"></asp:TextBox>
                <span style="color: blue">����Խ��Ȩ��Խ��Խ�������滻</span>
                <pe:RequiredFieldValidator ID="ValrPriority" ControlToValidate="TxtPriority" runat="server"
                    ErrorMessage="���ȼ�����Ϊ�գ�" Display="Dynamic"></pe:RequiredFieldValidator>
                <pe:NumberValidator ID="ValrNumberValidator" ControlToValidate="TxtPriority" runat="server"
                    Display="Dynamic" ErrorMessage="ֻ���������֣�"></pe:NumberValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>�Ƿ����ã�&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:RadioButtonList ID="RadioIsEnabled" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="True">����</asp:ListItem>
                    <asp:ListItem Value="False">����</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:Button ID="EBtnSubmit" Text="����" OnClick="EBtnSubmit_Click" runat="server" />&nbsp;&nbsp;
                <input name="Cancel" type="button" class="inputbutton" id="Cancel" value="ȡ��" onclick="Redirect('WordFilterManage.aspx')" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdnSource" runat="server" />
</asp:Content>
