<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Shop.SelectPayPlatform" Codebehind="SelectPayPlatform.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>选择在线支付平台</title>
</head>
<body>
    <form id="form1" runat="server">
        <table class="border" cellspacing="1">
            <tr class="title">
                <td>
                    在线支付操作(选择支付平台)</td>
            </tr>
            <tr class="tdbg">
                <td style=" text-align:center;">
                    <br />
                    <table width="500px" cellspacing="1" cellpadding="2" style="background-color: #CCCCCC;">
                        <tr class="title">
                            <td colspan="2">
                                <b>平台选择</b></td>
                        </tr>
                        <asp:PlaceHolder ID="PlhOrderInfo" runat="server" Visible="false">
                            <tr class="tdbg">
                                <td style="width: 30%; text-align: right;">
                                    订单号码：</td>
                                <td style="text-align: left;">
                                    <asp:Label ID="LblOrderNum" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr class="tdbg">
                                <td style="width: 30%; text-align: right;">
                                    订单金额：</td>
                                <td style="text-align: left;">
                                    <asp:Label ID="LblMoneyTotal" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr class="tdbg">
                                <td style="width: 30%; text-align: right;">
                                    已 付 款：</td>
                                <td style="text-align: left;">
                                    <asp:Label ID="LblMoneyReceipt" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr class="tdbg">
                                <td style="width: 30%; text-align: right;">
                                    需要支付：</td>
                                <td style="text-align: left;">
                                    <asp:Label ID="LblNeedPay" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </asp:PlaceHolder>
                        <tr class="tdbg">
                            <td style="width: 30%; text-align: right;">
                                支付平台：</td>
                            <td style="text-align: left;">
                                <asp:DropDownList ID="DropPayPlatform" DataTextField="PayPlatformName" DataValueField="PayPlatformId" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <asp:PlaceHolder runat="server" ID="PlhMoney">
                        <tr class="tdbg">
                            <td style="text-align: right;">
                                请输入你要汇的金额：</td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="TxtvMoney" Text="0" runat="server"></asp:TextBox>
                                </td>
                        </tr>
                        </asp:PlaceHolder>
                        <asp:PlaceHolder runat="server" ID="PlhBuyPoint" Visible="false">
                        <tr class="tdbg">
                            <td style="text-align: right;">
                                购买<pe:ShowPointName ID="ShowPointName6" runat="server"></pe:ShowPointName>数量：</td>
                            <td style="text-align: left;">
                                <asp:Label ID="LblPointAmount" runat="server"></asp:Label><pe:ShowPointName ID="ShowPointName1" PointType="PointUnit" runat="server"></pe:ShowPointName>
                                </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="text-align: right;">
                               购买单价：</td>
                            <td style="text-align: left;">
                                每<pe:ShowPointName ID="ShowPointName5" PointType="PointUnit" runat="server" /><asp:Label ID="LblPointPrice" runat="server"></asp:Label>元
                                </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="text-align: right;">
                               需要支付：</td>
                            <td style="text-align: left;">
                                <asp:Label ID="LblPayForPoint" runat="server"></asp:Label>元
                            </td>
                        </tr>
                        </asp:PlaceHolder>
                        <tr class="tdbg">
                            <td colspan="2">
                                <asp:Button ID="BtnSubmit" runat="server" Text=" 下一步 " OnClick="BtnSubmit_Click" />
                            </td>
                        </tr>
                        
                    </table>
                    <br />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
