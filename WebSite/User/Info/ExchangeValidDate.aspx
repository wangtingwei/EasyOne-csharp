<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.User.Info.ExchangeValidDate" Codebehind="ExchangeValidDate.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>会员有效期兑换</title>
</head>
<body>
    <pe:UserNavigation Tab="charge" ID="UserCenterNavigation" runat="server" />
    <pe:ExtendedSiteMapPath ID="YourPosition" SiteMapProvider="UserMapProvider" runat="server" />
    <form id="form1" runat="server">
        <table width="100%" cellpadding="2" cellspacing="1" class="border">
            <tr align="center" class="spacingtitle">
                <td colspan="2">
                    <strong>会员有效期兑换</strong>
                </td>
            </tr>
            <pe:ShowUserInfo ID="showUserInfo" runat="server" />
            <tr class="tdbg">
                <td align="right" style="width: 15%;" class="tdbgleft">
                    <asp:Label ID="LblValid" Text="兑换有效期：" runat="server" />
                </td>
                <td>
                    <asp:RadioButton ID="RadWithMoney" runat="server" GroupName="WithWhat" Checked="true" />
                    使用资金余额：将
                    <asp:TextBox ID="TxtMoney" runat="server" Text="10" Width="30" Columns="8" MaxLength="8" />
                    元兑换成有效期 &nbsp;&nbsp; 兑换比率：<asp:Literal ID="LitExMoney" runat="server" />元/<asp:Literal ID="LitExRMoney" runat="server" />天
                    <pe:RequiredFieldValidator ID="ValrMoney" ControlToValidate="TxtMoney" Display="Dynamic"
                        SetFocusOnError="true" runat="server" ErrorMessage="减去资金不能为空" ValidationGroup="Money"
                        ShowRequiredText="false" />
                    <pe:MoneyValidator ID="ValMoney" runat="server" ControlToValidate="TxtMoney" Display="Dynamic"
                        SetFocusOnError="true" ValidationGroup="Money" />
                    <br />
                    <asp:RadioButton ID="RadWithUserExp" runat="server" GroupName="WithWhat" />
                    使用经验积分：将
                    <asp:TextBox ID="TxtUserExp" runat="server" Text="10" Width="30" Columns="8" MaxLength="8" />
                    分兑换成有效期 &nbsp;&nbsp; 兑换比率：<asp:Literal ID="LitExUserExp" runat="server" />分/<asp:Literal ID="LitExRUserExp" runat="server" />天
                    <pe:RequiredFieldValidator ID="ValrUserExp" ControlToValidate="TxtUserExp" Display="Dynamic"
                        SetFocusOnError="true" runat="server" ErrorMessage="减去积分不能为空" ValidationGroup="UserExp"
                        ShowRequiredText="false" />
                    <pe:NumberValidator ID="NumValUserExp" runat="server" ControlToValidate="TxtUserExp"
                        SetFocusOnError="true" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="ValgUserExp" runat="server" ControlToValidate="TxtUserExp"
                        ErrorMessage="只能输入正整数" ValidationExpression="^([0-9])(\d{0,})(\d{0,})$" Display="Dynamic"
                        ValidationGroup="UserExp" />
                </td>
            </tr>
            <tr class="tdbgbottom">
                <td colspan="2">
                    <asp:Button ID="BtnSubmitMoney" runat="server" Text="执行兑换" OnClick="BtnMoneySubmit_Click"
                        ValidationGroup="Money" />
                    <asp:Button ID="BtnSubmitExp" runat="server" Text="执行兑换" OnClick="BtnExpSubmit_Click"
                        ValidationGroup="UserExp" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
