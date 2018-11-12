<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        string payOnlineShopID;
        string PayOnlineKey;
        
        int payPlatformId = 7;  //云网支付
        EasyOne.Model.Accessories.PayPlatformInfo payPlatform = EasyOne.Accessories.PayPlatform.GetPayPlatformById(payPlatformId);
        
        payOnlineShopID = payPlatform.AccountsId;
        PayOnlineKey = payPlatform.MD5;

        string c_mid = Request["c_mid"];                    //商户编号，在申请商户成功后即可获得，可以在申请商户成功的邮件中获取该编号
        string c_order = Request["c_order"];                //商户提供的订单号
        string c_orderamount = Request["c_orderamount"];    //商户提供的订单总金额，以元为单位，小数点后保留两位，如：13.05
        string c_ymd = Request["c_ymd"];                    //商户传输过来的订单产生日期，格式为"yyyymmdd"，如20050102
        string c_transnum = Request["c_transnum"];          //云网支付网关提供的该笔订单的交易流水号，供日后查询、核对使用；
        string c_succmark = Request["c_succmark"];          //交易成功标志，Y-成功 N-失败
        string c_moneytype = Request["c_moneytype"];        //支付币种，0为人民币
        string c_cause = Request["c_cause"];                //如果订单支付失败，则该值代表失败原因
        string c_memo1 = Request["c_memo1"];                //商户提供的需要在支付结果通知中转发的商户参数一
        string c_memo2 = Request["c_memo2"];                //商户提供的需要在支付结果通知中转发的商户参数二
        string c_signstr = Request["c_signstr"];            //云网支付网关对已上信息进行MD5加密后的字符串
        
        StringBuilder md5string = new StringBuilder();
        md5string.Append(c_mid);
        md5string.Append(c_order);
        md5string.Append(c_orderamount);
        md5string.Append(c_ymd);
        md5string.Append(c_transnum);
        md5string.Append(c_succmark);
        md5string.Append(c_moneytype);
        md5string.Append(c_memo1);
        md5string.Append(c_memo2);
        md5string.Append(PayOnlineKey);
        
        string md5 = EasyOne.Common.StringHelper.MD5(md5string.ToString());
        
        bool paySuccess = false;
        if(md5.ToUpper() == c_signstr.ToUpper())
        {
            if(payOnlineShopID.Trim() == c_mid)
            {
                if(c_succmark != "Y" && c_succmark != "N")
                {
                       LblMsg.Text = "参数提交有误";
                }
                else
                {
                     paySuccess = true;
                }
            }
            else
            {
                LblMsg.Text = "提交的商户编号有误";
            }
        }
        else
        {
            LblMsg.Text = "签名验证失败";
        }
        
        if(paySuccess)
        {
            StringBuilder message = new StringBuilder();
            message.Append("<br>恭喜你！在线支付成功！<br><br>");
            EasyOne.Shop.PayOnline payOnline = new EasyOne.Shop.PayOnline();
            EasyOne.Enumerations.PayOnlineState payOnlineState = payOnline.UpdateOrder(c_order, EasyOne.Common.DataConverter.CDecimal(c_orderamount), "", 3, "", true, true);
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
        EasyOne.Shop.PayOnline.TestLog(false, "云网支付", c_succmark, c_signstr.ToUpper(), md5.ToUpper());
    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>云网支付</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <pe:ExtendedLabel HtmlEncode="false" ID="LblMsg" runat="server"></pe:ExtendedLabel>
    </div>
    </form>
</body>
</html>
