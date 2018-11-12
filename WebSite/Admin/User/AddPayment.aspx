<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.User.AddPayment"
    MasterPageFile="~/Admin/MasterPage.master" Title="��ӻ�Ա֧����Ϣ" Codebehind="AddPayment.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <div style="text-align: center">
        <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
            <tr align="center">
                <td colspan="2" class="spacingtitle">
                    <asp:Label ID="LblTitle" runat="server" Text="��ӻ�Ա֧����� " Font-Bold="True"></asp:Label></td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>�� Ա ����</strong>
                </td>
                <td align="left">
                    <asp:Label ID="LblUserName" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>�ʽ���</strong>
                </td>
                <td align="left">
                    <asp:Label ID="LblBalance" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <asp:Panel ID="PnlPayment" runat="server" Visible="false">
                <tr class="tdbg">
                    <td class="tdbgleft">
                        ֧�����ݣ�</td>
                    <td align="left">
                        <table border="0" cellspacing="2" cellpadding="0">
                            <tr>
                                <td class="tdbgleft">
                                    ������ţ�</td>
                                <td align="left">
                                    <asp:Label ID="LblOrderFormNum" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 15%;" class="tdbgleft">
                                    ������</td>
                                <td align="left">
                                    <asp:Label ID="LblMoneyTotal" runat="server" Text="Ԫ"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 15%;" class="tdbgleft">
                                    �� �� �</td>
                                <td align="left">
                                    <asp:Label ID="LblMoneyReceipt" runat="server" Text="Ԫ"></asp:Label></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </asp:Panel>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>֧����</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtMoney" runat="server" MaxLength="20"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="ValrMoney" ControlToValidate="TxtMoney" runat="server"
                        ErrorMessage="֧������Ϊ��" Display="Dynamic"></pe:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="ValeMoney" runat="server" ControlToValidate="TxtMoney"
                        ErrorMessage="ֻ����������ַ������Ҳ���Ϊ��͸���" ValidationExpression="^[0-9]+(\.?[0-9]{1,4})?"
                        Display="Dynamic" />
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>��ע��</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtRemark" runat="server" Height="50px" Width="300px" TextMode="MultiLine"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="ValrMRemark" ControlToValidate="TxtRemark" runat="server"
                        ErrorMessage="��ע����Ϊ��"></pe:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>�ڲ���¼��</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtMemo" runat="server" Width="400px" Columns="50" Height="60px"
                        Rows="4" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr class="tdbg">
                <td style="width: 15%" class="tdbgleft" align="right">
                </td>
                <td align="left">
                    <asp:CheckBox ID="ChkIsSendMessage" runat="server" />ͬʱ�����ֻ�����֪ͨ��Ա
                </td>
            </tr>
            <tr class="tdbg">
                <td style="height: 30px;" colspan="2">
                    <span style="color: #FF0000;">ע�⣺���/������Ϣһ��¼�룬�Ͳ������޸Ļ�ɾ���������ڱ���֮ǰȷ����������</span></td>
            </tr>
            <tr align="center" class="tdbg">
                <td style="height: 30px;" colspan="2">
                    <asp:Button ID="EBtnSubmit" Text="����֧����Ϣ" OnClick="EBtnSubmit_Click" runat="server" />&nbsp;&nbsp;
                </td>
            </tr>
            <asp:HiddenField ID="HdnUsersId" runat="server" />
            <asp:HiddenField ID="HdnorderId" runat="server" />
        </table>
    </div>
</asp:Content>
