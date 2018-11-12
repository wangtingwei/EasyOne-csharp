<%@ Page Language="C#" StylesheetTheme="" EnableTheming="false"%>
<%@ Import Namespace="EasyOne.Shop" %>
<%@ Import Namespace="EasyOne.Common" %>
<script runat="server">
     protected void Page_Load(object sender, EventArgs e)
    {
        string v_mid, v_oid, v_pstatus, v_amount, v_md5, md5string;

        int payPlatformId = 11;  //快钱神州行支付
        EasyOne.Model.Accessories.PayPlatformInfo payPlatform = EasyOne.Accessories.PayPlatform.GetPayPlatformById(payPlatformId);
        v_mid = payPlatform.AccountsId;
           
        string merchantAcctId = StringHelper.Trim(Request["merchantAcctId"]) ; //获取人民币网关账户号
        string key = payPlatform.MD5; //设置人民币网关密钥
        string version = StringHelper.Trim(Request["version"]);        //获取网关版本
        string language = StringHelper.Trim(Request["language"]);      //获取语言种类,1代表中文；2代表英文
        string payType = StringHelper.Trim(Request["payType"]); //获取支付方式,00：组合支付,10：银行卡支付,11：电话银行支付,12：快钱账户支付,13：线下支付,14：B2B支付
        string cardNumber = StringHelper.Trim(Request["cardNumber"]);  //神州行卡序号,如果通过神州行卡直接支付时返回
        string cardPwd = StringHelper.Trim(Request["cardPwd"]);             //获取神州行卡密码,如果通过神州行卡直接支付时返回
        string orderId = StringHelper.Trim(Request["orderId"]);      //获取商户订单号
        string orderAmount = StringHelper.Trim(Request["orderAmount"]);    //获取原始订单金额
        string dealId = StringHelper.Trim(Request["dealId"]);      //获取快钱交易号
        string orderTime = StringHelper.Trim(Request["orderTime"]); //获取订单提交时间
        string ext1 = StringHelper.Trim(Request["ext1"]);   //获取扩展字段1
        string ext2 = StringHelper.Trim(Request["ext2"]);     //获取扩展字段2
        string payAmount = StringHelper.Trim(Request["payAmount"]);    //获取实际支付金额,单位为分
        string billOrderTime = StringHelper.Trim(Request["billOrderTime"]);    //获取快钱处理时间

        string payResult = StringHelper.Trim(Request["payResult"]);    //10代表 成功11代表 失败
        string signType = StringHelper.Trim(Request["signType"]);   //签名类型,1代表MD5签名
        string signMsg = StringHelper.Trim(Request["signMsg"]);       //获取加密签名串

        //生成加密串。必须保持如下顺序。
        string merchantSignMsgVal = "";
        merchantSignMsgVal=PayOnline.AppendParam(merchantSignMsgVal, "merchantAcctId", merchantAcctId);
        merchantSignMsgVal=PayOnline.AppendParam(merchantSignMsgVal, "version", version);
        merchantSignMsgVal=PayOnline.AppendParam(merchantSignMsgVal, "language", language);
        merchantSignMsgVal=PayOnline.AppendParam(merchantSignMsgVal, "payType", payType);
        merchantSignMsgVal = PayOnline.AppendParam(merchantSignMsgVal, "cardNumber", cardNumber);
        merchantSignMsgVal = PayOnline.AppendParam(merchantSignMsgVal, "cardPwd", cardPwd);
        merchantSignMsgVal=PayOnline.AppendParam(merchantSignMsgVal, "orderId", orderId);
        merchantSignMsgVal=PayOnline.AppendParam(merchantSignMsgVal, "orderAmount", orderAmount);
        merchantSignMsgVal=PayOnline.AppendParam(merchantSignMsgVal, "dealId", dealId);
        merchantSignMsgVal=PayOnline.AppendParam(merchantSignMsgVal, "orderTime", orderTime);
        merchantSignMsgVal=PayOnline.AppendParam(merchantSignMsgVal, "ext1", ext1);
        merchantSignMsgVal=PayOnline.AppendParam(merchantSignMsgVal, "ext2", ext2);
        merchantSignMsgVal=PayOnline.AppendParam(merchantSignMsgVal, "payAmount", payAmount);
        merchantSignMsgVal = PayOnline.AppendParam(merchantSignMsgVal, "billOrderTime", billOrderTime);
        merchantSignMsgVal=PayOnline.AppendParam(merchantSignMsgVal, "payResult", payResult);
        merchantSignMsgVal=PayOnline.AppendParam(merchantSignMsgVal, "signType", signType);
        merchantSignMsgVal=PayOnline.AppendParam(merchantSignMsgVal, "key", key);
        md5string = StringHelper.MD5(merchantSignMsgVal);

        v_oid = orderId;
        v_amount = orderAmount;
        //首先进行签名字符串验证
        StringBuilder message = new StringBuilder();
        bool paySuccess = false;
        if (signMsg.ToUpper() == md5string.ToUpper() && payResult == "10")
        {
            paySuccess = true;
            message.Append("<br>恭喜您！在线支付成功！<br><br>");
        }
        else
        {
            message.Append("在线支付失败！");
        }
        if (paySuccess)
        {
            PayOnline payOnline = new PayOnline();
            EasyOne.Enumerations.PayOnlineState payOnlineState = payOnline.UpdateOrder(v_oid, DataConverter.CDecimal(v_amount) / 100, "", 3, "", true, true);
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
        PayOnline.TestLog(false, "快钱神州行支付", payResult, signMsg.ToUpper(), md5string.ToUpper());   
  
 }

</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>快钱神州行</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <pe:ExtendedLabel HtmlEncode="false" ID="LblMsg" runat="server"></pe:ExtendedLabel>
        </div>
    </form>
</body>
</html>
         