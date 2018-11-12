<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
protected void Page_Load(object sender, EventArgs e)
    {
        string payOnlineShopID;
        string PayOnlineKey;

        int payPlatformId = 1;  //网银在线
        EasyOne.Model.Accessories.PayPlatformInfo payPlatform = EasyOne.Accessories.PayPlatform.GetPayPlatformById(payPlatformId);
        payOnlineShopID = payPlatform.AccountsId;
        PayOnlineKey = payPlatform.MD5;
        Page.Request.ContentEncoding = Encoding.GetEncoding("gb2312");
       string v_mid, v_oid, v_pmode, v_pstatus, v_pstring, v_amount, v_md5, v_moneytype;
    
        v_mid = payOnlineShopID;
        v_oid = Request["v_oid"].Trim();
        v_md5 = Request["v_md5str"].Trim();            //数字签名
        v_amount = Request["v_amount"].Trim();         //支付金额
        v_pstatus = Request["v_pstatus"].Trim();       //支付状态
        v_moneytype = Request["v_moneytype"].Trim();   //支付币种
        //v_pmode = Server.(Request["v_pmode"].Trim());           //支付银行
        //v_pstring = Server.UrlEncode(Request["v_pstring"].Trim());       //支付结果说明
        v_pmode = "";
        v_pstring = "";
        string md5string = EasyOne.Common.StringHelper.MD5(v_oid + v_pstatus + v_amount + v_moneytype + PayOnlineKey);

        StringBuilder message = new StringBuilder();
        bool payResult = false;
        if (md5string.ToUpper() == v_md5.ToUpper() && v_pstatus == "20")
        {
            message.Append("<br>恭喜您！在线支付成功！<br><br>");
            payResult = true;
        }
        else
        {
            message.Append("在线支付失败！");
        }
        if (payResult)
        {
            EasyOne.Shop.PayOnline payOnline = new EasyOne.Shop.PayOnline();
            EasyOne.Enumerations.PayOnlineState payOnlineState = payOnline.UpdateOrder(v_oid, EasyOne.Common.DataConverter.CDecimal(v_amount), v_pstring, 3, v_pmode, true, true);
            if (payOnlineState == EasyOne.Enumerations.PayOnlineState.Ok)
            {
                message.Append(payOnline.Message);
            }
            else
            {
                message.Append(EasyOne.Shop.PayOnline.GetStateDescription(payOnlineState));
            }
        }
        LblMsg.Text = message.ToString();
        EasyOne.Shop.PayOnline.TestLog(false, "网银在线", v_pstatus, v_md5.ToUpper(), md5string.ToUpper());
    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>网银在线</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
         <pe:ExtendedLabel HtmlEncode="false" ID="LblMsg" runat="server"></pe:ExtendedLabel>
    </div>
    </form>
</body>
</html>
