<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Accessories.InsideLink"
    MasterPageFile="~/Admin/MasterPage.master" Title="վ�����ӹ���" Codebehind="InsideLink.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <b>
                    <pe:AlternateLiteral ID="AltrTitle" Text="���վ������" AlternateText="�޸�վ������" runat="Server" />
                </b>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="left" style="width: 30%">
                <strong>����Ŀ�꣺&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtSourceWord" runat="server" Width="200px"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrSourceWord" ControlToValidate="TxtSourceWord"
                    runat="server" ErrorMessage="����Ŀ�겻��Ϊ�գ�" Display="Dynamic"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="left" style="width: 30%">
                <strong>����Title��&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtTitle" runat="server" Width="200px"></asp:TextBox>
                <span style="color: blue">���ӵ�ַ��Title���ԣ�������SEO</span>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="left" style="width: 30%">
                <strong>���ӵ�ַ��&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtTargetWord" runat="server" Width="200px">http://</asp:TextBox>
                <span style="color: blue">��ʹ�þ��Ե�ַ</span>
                <pe:RequiredFieldValidator ID="ValrTargetWord" ControlToValidate="TxtTargetWord"
                    runat="server" ErrorMessage="���ӵ�ַ����Ϊ�գ�" Display="Dynamic"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="left" style="width: 30%">
                <strong>���ȼ���&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtPriority" runat="server" Width="64px"></asp:TextBox>
                <span style="color: blue">����Խ��Ȩ��Խ��Խ�������滻</span>
                <pe:RequiredFieldValidator ID="ValrPriority" ControlToValidate="TxtPriority" runat="server"
                    ErrorMessage="���ȼ�����Ϊ�գ�" Display="Dynamic"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="left" style="width: 30%">
                <strong>�滻������&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtReplaceTimes" runat="server" Width="64px"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrReplaceTimes" ControlToValidate="TxtReplaceTimes"
                    runat="server" ErrorMessage="�滻��������Ϊ�գ�" Display="Dynamic"></pe:RequiredFieldValidator>
                <pe:NumberValidator ID="ValrNumberValidator" ControlToValidate="TxtReplaceTimes"
                    runat="server" Display="Dynamic" ErrorMessage="ֻ���������֣�"></pe:NumberValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="left" style="width: 30%">
                <strong>�򿪷�ʽ��&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:RadioButtonList ID="RadlOpenType" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="True">ԭ����</asp:ListItem>
                    <asp:ListItem Value="False">�´���</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="left" style="width: 30%">
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
                <input name="Cancel" type="button" class="inputbutton" id="Cancel" value="ȡ��" onclick="Redirect('InsideLinkManage.aspx')" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdnSource" runat="server" />
</asp:Content>
