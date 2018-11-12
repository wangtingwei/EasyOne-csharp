<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.User.Info.ExchangePoint" Codebehind="ExchangePoint.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>会员点券兑换</title>
</head>
<body>
    <pe:UserNavigation Tab="charge" ID="UserCenterNavigation" runat="server" />
    <pe:ExtendedSiteMapPath ID="YourPosition" SiteMapProvider="UserMapProvider" runat="server" />
    <form id="form1" runat="server">
        <table width="100%" cellpadding="2" cellspacing="1" class="border">
            <tr align="center" class="spacingtitle">
                <td colspan="2">
                    <strong>会员<pe:ShowPointName ID="ShowPointName4" runat="server" />兑换</strong>
                </td>
            </tr>
            <pe:ShowUserInfo ID="showUserInfo" runat="server" />
            <tr class="tdbg">
                <td style="width: 15%;" align="right" class="tdbgleft">
                    兑换<pe:ShowPointName ID="ShowPointName1" runat="server" />：
                </td>
                <td>
                    <asp:RadioButton ID="RadWithMoney" runat="server" GroupName="WithWhat" Checked="true" />
                    使用资金余额：将
                    <asp:TextBox ID="TxtMoney" runat="server" Text="10" Width="30" />
                    元兑换成<pe:ShowPointName ID="ShowPointName2" runat="server" />
                    &nbsp;&nbsp; 兑换比率：<asp:Literal ID="LitExMoney" runat="server" />元/<asp:Literal ID="LitExRMoney" runat="server" /><pe:ShowPointName ID="ShowPointName" PointType ="PointUnit" runat="server"></pe:ShowPointName>
                    <pe:RequiredFieldValidator ID="ValrMoney" ControlToValidate="TxtMoney" Display="Dynamic"
                        RequiredText="" SetFocusOnError="true" runat="server" ErrorMessage="减去资金不能为空"
                        ValidationGroup="Money" />
                    <pe:MoneyValidator ID="ValMoney" runat="server" ControlToValidate="TxtMoney" Display="Dynamic"
                        SetFocusOnError="true" ValidationGroup="Money" />
                    <br />
                    <asp:RadioButton ID="RadWithUserExp" runat="server" GroupName="WithWhat" />
                    使用经验积分：将
                    <asp:TextBox ID="TxtUserExp" runat="server" Text="10" Width="30" />
                    分兑换成<pe:ShowPointName ID="ShowPointName3" runat="server" />
                    &nbsp;&nbsp; 兑换比率：<asp:Literal ID="LitExUserExp" runat="server" />分/<asp:Literal ID="LitExRUserExp" runat="server" /><pe:ShowPointName ID="ShowPointName5" PointType ="PointUnit" runat="server"></pe:ShowPointName>
                    <pe:RequiredFieldValidator ID="ValrExp" ControlToValidate="TxtUserExp" Display="Dynamic"
                        RequiredText="" SetFocusOnError="true" runat="server" ErrorMessage="减去积分不能为空"
                        ValidationGroup="Exp" />
                    <pe:NumberValidator ID="NumValUserExp" runat="server" ControlToValidate="TxtUserExp"
                        SetFocusOnError="true" Display="Dynamic" ValidationGroup="Exp" />
                </td>
            </tr>
            <tr class="tdbgbottom">
                <td colspan="2">
                    <asp:Button ID="BtnSubmitMoney" runat="server" Text="执行兑换" OnClick="BtnSubmitMoney_Click"
                        ValidationGroup="Money" />
                    <asp:Button ID="BtnSubmitExp" runat="server" Text="执行兑换" OnClick="BtnSubmitExp_Click"
                        ValidationGroup="Exp" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
