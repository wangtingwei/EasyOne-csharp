<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.User.AddIncome"
    MasterPageFile="~/Admin/MasterPage.master" Title="��ӻ�Ա���л����Ϣ" Codebehind="AddIncome.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <div style="text-align: center">
        <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
            <tr align="center" class="title">
                <td colspan="2">
                    <asp:Label ID="LblTitle" runat="server" Text="��ӻ�Ա���л����Ϣ" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr class="tdbg">
                <td align="right" style="width: 100%;" colspan="2">
                    <pe:ShowUserInfo ID="ShowUserInfo" runat="server" />
                </td>
            </tr>
            <tr class="tdbg">
                <td align="right" style="width: 15%;" class="tdbgleft">
                    �������ڣ�</td>
                <td align="left">
                    <pe:DatePicker ID="DpkReceipt" runat="server">
                    </pe:DatePicker>
                    <span style="color: Blue;">����Ϊ��������</span>
                </td>
            </tr>
            <tr class="tdbg">
                <td align="right" style="width: 15%;" class="tdbgleft">
                    ����</td>
                <td align="left">
                    <asp:TextBox ID="TxtMoney" runat="server" Columns="8" MaxLength="8"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="ValrMoney" ControlToValidate="TxtMoney" runat="server"
                        ErrorMessage="������Ϊ��" Display="Dynamic"></pe:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="ValeMoney" runat="server" ControlToValidate="TxtMoney"
                        ErrorMessage="ֻ����������ַ������Ҳ���Ϊ����" ValidationExpression="^[0-9]+(\.?[0-9]{1,4})?"
                        Display="Dynamic"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td style="width: 15%" class="tdbgleft" align="right">
                </td>
                <td style="text-align: left">
                    <asp:CheckBox ID="ChkPoint" runat="server" />���û�����<asp:TextBox ID="TxtPoint" runat="server"
                        Width="50px" MaxLength="9"></asp:TextBox><pe:ShowPointName ID="ShowPointName2" PointType="PointUnit"
                            runat="server" /><pe:ShowPointName ID="ShowPointName1" runat="server" />
                    <asp:RegularExpressionValidator ID="ValrPoint" ControlToValidate="TxtPoint" SetFocusOnError="true"
                        Display="dynamic" ValidationExpression="\d*" runat="server" ErrorMessage="������Ӧ��������"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td align="right" style="width: 15%;" class="tdbgleft">
                    �������У�</td>
                <td align="left">
                    <asp:DropDownList ID="DropBankShortName" runat="server" Width="97px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="tdbg">
                <td align="right" style="width: 15%;" class="tdbgleft">
                    ��ע��</td>
                <td align="left">
                    <asp:TextBox ID="TxtRemark" runat="server" Height="50px" Width="300px" TextMode="MultiLine"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="ValrMRemark" ControlToValidate="TxtRemark" runat="server"
                        ErrorMessage="��ע����Ϊ��"></pe:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td style="width: 15%" class="tdbgleft" align="right">
                    �ڲ���¼��</td>
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
                    <asp:Button ID="EBtnSubmit" Text="��������Ϣ" OnClick="EBtnSubmit_Click" runat="server" />&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
