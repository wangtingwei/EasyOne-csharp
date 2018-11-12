<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.User.Info.BuyPoint" Codebehind="BuyPoint.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>购买点券</title>
</head>
<body>
    <pe:UserNavigation Tab="charge" ID="UserCenterNavigation" runat="server" />
    <pe:ExtendedSiteMapPath ID="YourPosition" SiteMapProvider="UserMapProvider" runat="server" />
    <form id="form1" runat="server">
        <table width="100%" cellpadding="2" cellspacing="1" class="border">
            <tr align="center" class="spacingtitle">
                <td colspan="2">
                    <strong>购买<pe:ShowPointName ID="ShowPointName4" runat="server" /></strong>
                </td>
            </tr>
            <pe:ShowUserInfo ID="showUserInfo" runat="server" />
            <tr class="tdbg">
                <td align="right" style="width: 15%;" class="tdbgleft">购买单价：</td>
                <td>
                    每<pe:ShowPointName ID="ShowPointName5" PointType="PointUnit" runat="server" /><asp:Label ID="LblPrice" runat="server"></asp:Label>元
                </td>
            </tr>
            <tr class="tdbg">
                <td align="right" style="width: 15%;" class="tdbgleft">购买数：</td>
                <td>
                    <asp:TextBox ID="TxtPointAmount" runat="server" MaxLength="6" Width="60px" /><input type="button" value="计算" onclick="account()" />
                    <pe:RequiredFieldValidator ID="ValrTxtPointAmount" ControlToValidate="TxtPointAmount"
                        runat="server" SetFocusOnError="true" Display="Dynamic" ErrorMessage="请输入购买的数量！" />
                    <asp:RegularExpressionValidator ID="ValeTxtPointAmount" ControlToValidate="TxtPointAmount" ValidationExpression="^[1-9]\d*$" SetFocusOnError="true" Display="dynamic" runat="server" ErrorMessage="请输入大于0的整数"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td align="right" style="width: 15%;" class="tdbgleft">需要支付：</td>
                <td>
                    <span id="NeedPay"></span>
                </td>
            </tr>
            <tr class="tdbgbottom">
                <td colspan="2">
                    <asp:Button ID="BtnSubmit" runat="server" Text="购买" OnClick="BtnSubmit_Click"/>
                </td>
            </tr>
        </table>
<script language="javascript" type="text/javascript">
    function account()
    {
        var LblNeedPay = document.getElementById('NeedPay');
        var price = document.getElementById('<%=LblPrice.ClientID %>').innerHTML;
        var amount = document.getElementById('<%=TxtPointAmount.ClientID %>').value;
        var needPay = price * amount;
        if(isNaN(needPay)==false)
        {
            LblNeedPay.innerHTML = round(needPay,2) + "元";
        }
    }
    function round(v,e) 
    { 
        var t=1; 
        for(;e>0;t*=10,e--); 
        for(;e<0;t/=10,e ); 
        return Math.round(v*t)/t; 
    } 
</script>    </form>
</body>
</html>
