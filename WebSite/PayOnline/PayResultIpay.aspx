<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        string payOnlineShopID;
        string PayOnlineKey;

        int payPlatformId = 2;  //中国在线支付
        EasyOne.Model.Accessories.PayPlatformInfo payPlatform = EasyOne.Accessories.PayPlatform.GetPayPlatformById(payPlatformId);

        payOnlineShopID = payPlatform.AccountsId;
        PayOnlineKey = payPlatform.MD5;

        string v_mid = payOnlineShopID;
        string v_date = Request["v_date"].Trim();    //订单日期
        string v_oid = Request["v_oid"].Trim();       //支付定单号
        string v_amount = Request["v_amount"].Trim();     //订单金额
        string v_pstatus = Request["v_pstatus"].Trim();  //订单状态
        string v_md5 = Request["v_md5"].Trim();            //MD5签名

        StringBuilder md5string = new StringBuilder();
        md5string.Append(v_date);
        md5string.Append(v_mid);
        md5string.Append(v_oid);
        md5string.Append(v_amount);
        md5string.Append(v_pstatus);
        md5string.Append(PayOnlineKey);
        string md5 = EasyOne.Common.StringHelper.MD5(md5string.ToString());

        bool paySuccess = false;
        if (v_md5.ToUpper() == md5.ToUpper() && v_pstatus == "00")
        {
            paySuccess = true;

        }
        if (paySuccess)
        {
            string prefix_PaymentNum = EasyOne.Components.SiteConfig.ShopConfig.PrefixPaymentNum;    //支付序号前缀
            v_oid = prefix_PaymentNum + v_oid;

            StringBuilder message = new StringBuilder();
            message.Append("<br>恭喜你！在线支付成功！<br><br>");
            EasyOne.Shop.PayOnline payOnline = new EasyOne.Shop.PayOnline();
            EasyOne.Enumerations.PayOnlineState payOnlineState = payOnline.UpdateOrder(v_oid, EasyOne.Common.DataConverter.CDecimal(v_amount), "", 3, "", true, true);
            if (payOnlineState == EasyOne.Enumerations.PayOnlineState.Ok)
            {
                message.Append(payOnline.Message);
            }
            else
            {
                message.Append(EasyOne.Shop.PayOnline.GetStateDescription(payOnlineState));
            }
            LblMsg.Text = message.ToString();
        }
        else
        {
            LblMsg.Text = "在线支付失败！";
        }
        EasyOne.Shop.PayOnline.TestLog(false, "中国在线支付", v_pstatus, v_md5.ToUpper(), md5.ToUpper());
    }      
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>中国在线支付</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <pe:ExtendedLabel HtmlEncode="false" ID="LblMsg" runat="server"></pe:ExtendedLabel>
        </div>
    </form>
</body>
</html>
