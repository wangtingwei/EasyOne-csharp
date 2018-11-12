<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.User.ExchangePoint"
    MasterPageFile="~/Admin/MasterPage.master" Title="��Ա��ȯ�һ�" Codebehind="ExchangePoint.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <div style="text-align: center">
        <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
            <tr align="center" class="title">
                <td colspan="2">
                    <asp:Label ID="LblTitle" runat="server" Text=" ��Ա��ȯ�һ� " Font-Bold="True"></asp:Label></td>
            </tr>
            <tr class="tdbg">
                <td align="right" style="width: 100%;" colspan="2">
                    <pe:ShowUserInfo ID="showUserInfo" runat="server" />
                </td>
            </tr>
            <tr class="tdbg">
                <td align="right" style="width: 15%;" class="tdbgleft">
                    ����<%=m_PointName%>��</td>
                <td align="left">
                    <asp:TextBox ID="TxtPoint" runat="server" Text="100" MaxLength="6" Width="50px"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="ValrPoint" ControlToValidate="TxtPoint" runat="server"
                        ErrorMessage="���ӵ�ȯ����Ϊ��" Display="Dynamic"></pe:RequiredFieldValidator>
                    <asp:RangeValidator ID="ValgTxtPoint" ControlToValidate="TxtPoint" runat="server"
                        ErrorMessage="��ȯ��Χ��1��100000֮��" Type="Double" Display="Dynamic" MaximumValue="100000"
                        MinimumValue="1"></asp:RangeValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td align="right" style="width: 15%;" class="tdbgleft">
                    ͬʱ��ȥ�ʽ�</td>
                <td align="left">
                    <asp:TextBox ID="TxtMoney" runat="server" MaxLength="6" Width="50px"></asp:TextBox>Ԫ
                    <pe:RequiredFieldValidator ID="ValrMoney" ControlToValidate="TxtMoney" runat="server"
                        ErrorMessage="��ȥ�ʽ���Ϊ��" Display="Dynamic"></pe:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="ValeMoney" runat="server" ControlToValidate="TxtMoney"
                        ErrorMessage="ֻ����������ַ�" ValidationExpression="^-?[0-9]+(\.?[0-9]{1,4})?" Display="Dynamic"></asp:RegularExpressionValidator>
                    <asp:RangeValidator ID="ValrMoney2" ControlToValidate="TxtMoney" runat="server" ErrorMessage="�ʽ�Χ��0��100000֮��"
                        Type="Double" Display="Dynamic" MaximumValue="100000" MinimumValue="0"></asp:RangeValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td style="width: 15%" class="tdbgleft" align="right">
                </td>
                <td align="left">
                    <asp:CheckBox ID="ChkIsSendMessage" runat="server" />ͬʱ�����ֻ�����֪ͨ��Ա
                </td>
            </tr>
            <tr align="center" class="tdbg">
                <td style="height: 30px;" colspan="2">
                    <asp:Button ID="EBtnSubmit" Text="ִ�жһ�" OnClick="EBtnSubmit_Click" runat="server" />
                </td>
            </tr>
            <asp:HiddenField ID="HdnUsersId" runat="server" />
        </table>
    </div>
</asp:Content>
