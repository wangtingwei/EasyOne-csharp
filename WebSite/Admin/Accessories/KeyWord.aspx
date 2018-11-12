<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master"
    Inherits="EasyOne.WebSite.Admin.Accessories.KeyWord" Title="�ؼ��ֹ���" Codebehind="KeyWord.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <asp:Label ID="LblTitle" runat="server" Text="��ӹؼ���" Font-Bold="True"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>�ؼ������ƣ�&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtKeywordText" runat="server"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrKeywordText" ControlToValidate="TxtKeywordText"
                    runat="server" ErrorMessage="�ؼ������Ʋ���Ϊ�գ�" Display="Dynamic"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>�ؼ������&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:RadioButtonList ID="RadlKeywordType" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="0">����ؼ���</asp:ListItem>
                    <asp:ListItem Value="1">�����ؼ���</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>�ؼ���Ȩ�أ�&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtPriority" runat="server" Columns="5"></asp:TextBox>
                <span style="color: blue">����Խ��Ȩ��Խ��Խ������</span>
                <pe:RequiredFieldValidator ID="ValrPriority" ControlToValidate="TxtPriority" runat="server"
                    ErrorMessage="�ؼ���Ȩ�ز���Ϊ�գ�" Display="Dynamic"></pe:RequiredFieldValidator>
                <pe:NumberValidator ID="ValrNumberValidator" ControlToValidate="TxtPriority" runat="server"
                    Display="Dynamic" ErrorMessage="ֻ���������֣�"></pe:NumberValidator>
            </td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:Button ID="EBtnSubmit" Text="����" OnClick="EBtnSubmit_Click" runat="server" />&nbsp;&nbsp;
                <input name="Cancel" type="button" class="inputbutton" id="Cancel" value="ȡ��" onclick="Redirect('KeyWordManage.aspx')" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdnKeywordText" runat="server" />
</asp:Content>
