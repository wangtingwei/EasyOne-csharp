<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.User.AddPayment"
    MasterPageFile="~/Admin/MasterPage.master" Title="添加会员支出信息" Codebehind="AddPayment.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <div style="text-align: center">
        <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
            <tr align="center">
                <td colspan="2" class="spacingtitle">
                    <asp:Label ID="LblTitle" runat="server" Text="添加会员支出金额 " Font-Bold="True"></asp:Label></td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>会 员 名：</strong>
                </td>
                <td align="left">
                    <asp:Label ID="LblUserName" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>资金余额：</strong>
                </td>
                <td align="left">
                    <asp:Label ID="LblBalance" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <asp:Panel ID="PnlPayment" runat="server" Visible="false">
                <tr class="tdbg">
                    <td class="tdbgleft">
                        支付内容：</td>
                    <td align="left">
                        <table border="0" cellspacing="2" cellpadding="0">
                            <tr>
                                <td class="tdbgleft">
                                    订单编号：</td>
                                <td align="left">
                                    <asp:Label ID="LblOrderFormNum" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 15%;" class="tdbgleft">
                                    订单金额：</td>
                                <td align="left">
                                    <asp:Label ID="LblMoneyTotal" runat="server" Text="元"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 15%;" class="tdbgleft">
                                    已 付 款：</td>
                                <td align="left">
                                    <asp:Label ID="LblMoneyReceipt" runat="server" Text="元"></asp:Label></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </asp:Panel>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>支出金额：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtMoney" runat="server" MaxLength="20"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="ValrMoney" ControlToValidate="TxtMoney" runat="server"
                        ErrorMessage="支出金额不能为空" Display="Dynamic"></pe:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="ValeMoney" runat="server" ControlToValidate="TxtMoney"
                        ErrorMessage="只能输入货币字符，并且不能为零和负数" ValidationExpression="^[0-9]+(\.?[0-9]{1,4})?"
                        Display="Dynamic" />
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>备注：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtRemark" runat="server" Height="50px" Width="300px" TextMode="MultiLine"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="ValrMRemark" ControlToValidate="TxtRemark" runat="server"
                        ErrorMessage="备注不能为空"></pe:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>内部记录：</strong>
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
                    <asp:CheckBox ID="ChkIsSendMessage" runat="server" />同时发送手机短信通知会员
                </td>
            </tr>
            <tr class="tdbg">
                <td style="height: 30px;" colspan="2">
                    <span style="color: #FF0000;">注意：汇款/收入信息一旦录入，就不能再修改或删除！所以在保存之前确认输入无误！</span></td>
            </tr>
            <tr align="center" class="tdbg">
                <td style="height: 30px;" colspan="2">
                    <asp:Button ID="EBtnSubmit" Text="保存支付信息" OnClick="EBtnSubmit_Click" runat="server" />&nbsp;&nbsp;
                </td>
            </tr>
            <asp:HiddenField ID="HdnUsersId" runat="server" />
            <asp:HiddenField ID="HdnorderId" runat="server" />
        </table>
    </div>
</asp:Content>
