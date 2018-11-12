<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        string payOnlineShopID;
        string PayOnlineKey;

        int payPlatformId = 6;  //易付通
        EasyOne.Model.Accessories.PayPlatformInfo payPlatform = EasyOne.Accessories.PayPlatform.GetPayPlatformById(payPlatformId);  
              
        payOnlineShopID = payPlatform.AccountsId;
        PayOnlineKey = payPlatform.MD5;
        
       string  v_mid = payOnlineShopID;
       string  v_oid = Request["bid"].Trim();       //支付定单号
       string v_sid = Request["sid"].Trim();         //易付通交易成功 流水号
       string v_md5 = Request["md"].Trim();      //数字签名
       string v_amount = Request["prc"].Trim();       //支付金额
       string v_pstatus = Request["success"].Trim();       //支付状态
       string v_pmode = Request["bankcode"].Trim();       //支付银行
       string v_pstring = Request["v_pstring"].Trim();       //支付结果说明
        
       StringBuilder md5string = new StringBuilder();
        md5string.Append(PayOnlineKey);
         md5string.Append(":");
         md5string.Append(v_oid);
         md5string.Append(",");
         md5string.Append(v_sid);
         md5string.Append(",");
         md5string.Append(v_amount);
         md5string.Append(",sell,,");
         md5string.Append(v_mid);
         md5string.Append(",bank,");
        md5string.Append(v_pstatus);
        string md5 = EasyOne.Common.StringHelper.MD5(md5string.ToString());
        
        bool paySuccess = false;
        if(v_md5.ToUpper() == md5.ToUpper() && v_pstatus.ToLower() == "true")
        {
            paySuccess =  true;
        }
        if(paySuccess)
        {
            StringBuilder msg = new StringBuilder();
            msg.Append("<br>恭喜你！在线支付成功！<br><br>");
            EasyOne.Shop.PayOnline payOnline = new EasyOne.Shop.PayOnline();
            EasyOne.Enumerations.PayOnlineState payOnlineState = payOnline.UpdateOrder(v_oid, EasyOne.Common.DataConverter.CDecimal(v_amount), v_pstring, 3, v_pmode, true, true);
            if (payOnlineState == EasyOne.Enumerations.PayOnlineState.Ok)
            {
                msg.Append(payOnline.Message);
            }
            else
            {
                msg.Append(EasyOne.Shop.PayOnline.GetStateDescription(payOnlineState));
            }
            LblMsg.Text = msg.ToString();  
        }
        else
        {
            LblMsg.Text = "MD5校验失败！";
        }
        EasyOne.Shop.PayOnline.TestLog(false, "易付通", v_pstatus.ToLower(), v_md5.ToUpper(), md5.ToUpper());
    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>易付通</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <pe:ExtendedLabel HtmlEncode="false" ID="LblMsg" runat="server"></pe:ExtendedLabel>
    </div>
    </form>
</body>
</html>
