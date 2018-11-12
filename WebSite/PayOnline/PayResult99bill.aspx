<%@ Page Language="C#" StylesheetTheme="" EnableTheming="false" Inherits="EasyOne.Web.UI.BasePage" %>

<%@ Import Namespace="EasyOne.Shop" %>
<%@ Import Namespace="EasyOne.Common" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        string v_mid, v_oid, v_pstatus, v_amount, v_md5, md5string;

        int payPlatformId = 9;  //快钱支付
        EasyOne.Model.Accessories.PayPlatformInfo payPlatform = EasyOne.Accessories.PayPlatform.GetPayPlatformById(payPlatformId);
        v_mid = payPlatform.AccountsId;

        string merchantAcctId = StringHelper.Trim(Request["merchantAcctId"]); //获取人民币网关账户号
        string key = payPlatform.MD5; //设置人民币网关密钥
        string version = StringHelper.Trim(Request["version"]); //获取网关版本
        string language = StringHelper.Trim(Request["language"]); //获取语言种类,1代表中文；2代表英文
        string signType = StringHelper.Trim(Request["signType"]); //签名类型,1代表MD5签名
        string payType = StringHelper.Trim(Request["payType"]); //获取支付方式,00：组合支付,10：银行卡支付,11：电话银行支付,12：快钱账户支付,13：线下支付,14：B2B支付
        string bankId = StringHelper.Trim(Request["bankId"]); //获取银行代码
        string orderId = StringHelper.Trim(Request["orderId"]); //获取商户订单号
        string orderTime = StringHelper.Trim(Request["orderTime"]); //获取订单提交时间
        string orderAmount = StringHelper.Trim(Request["orderAmount"]); //获取原始订单金额
        string dealId = StringHelper.Trim(Request["dealId"]); //获取快钱交易号
        string bankDealId = StringHelper.Trim(Request["bankDealId"]); //获取银行交易号
        string dealTime = StringHelper.Trim(Request["dealTime"]); //获取在快钱交易时间
        string payAmount = StringHelper.Trim(Request["payAmount"]); //获取实际支付金额,单位为分
        string fee = StringHelper.Trim(Request["fee"]); //获取交易手续费
        string ext1 = StringHelper.Trim(Request["ext1"]); //获取扩展字段1
        string ext2 = StringHelper.Trim(Request["ext2"]); //获取扩展字段2



        //获取处理结果
        //10代表 成功11代表 失败
        //00代表 下订单成功（仅对电话银行支付订单返回）;01代表 下订单失败（仅对电话银行支付订单返回）
        string payResult = StringHelper.Trim(Request["payResult"]);
        string errCode = StringHelper.Trim(Request["errCode"]);     //获取错误代码,详细见文档错误代码列表
        string signMsg = StringHelper.Trim(Request["signMsg"]);     //获取加密签名串

        //生成加密串。必须保持如下顺序。
        string merchantSignMsgVal = "";
        merchantSignMsgVal = PayOnline.AppendParam(merchantSignMsgVal, "merchantAcctId", merchantAcctId);
        merchantSignMsgVal = PayOnline.AppendParam(merchantSignMsgVal, "version", version);
        merchantSignMsgVal = PayOnline.AppendParam(merchantSignMsgVal, "language", language);
        merchantSignMsgVal = PayOnline.AppendParam(merchantSignMsgVal, "signType", signType);
        merchantSignMsgVal = PayOnline.AppendParam(merchantSignMsgVal, "payType", payType);
        merchantSignMsgVal = PayOnline.AppendParam(merchantSignMsgVal, "bankId", bankId);
        merchantSignMsgVal = PayOnline.AppendParam(merchantSignMsgVal, "orderId", orderId);
        merchantSignMsgVal = PayOnline.AppendParam(merchantSignMsgVal, "orderTime", orderTime);
        merchantSignMsgVal = PayOnline.AppendParam(merchantSignMsgVal, "orderAmount", orderAmount);
        merchantSignMsgVal = PayOnline.AppendParam(merchantSignMsgVal, "dealId", dealId);
        merchantSignMsgVal = PayOnline.AppendParam(merchantSignMsgVal, "bankDealId", bankDealId);
        merchantSignMsgVal = PayOnline.AppendParam(merchantSignMsgVal, "dealTime", dealTime);
        merchantSignMsgVal = PayOnline.AppendParam(merchantSignMsgVal, "payAmount", payAmount);
        merchantSignMsgVal = PayOnline.AppendParam(merchantSignMsgVal, "fee", fee);
        merchantSignMsgVal = PayOnline.AppendParam(merchantSignMsgVal, "ext1", ext1);
        merchantSignMsgVal = PayOnline.AppendParam(merchantSignMsgVal, "ext2", ext2);
        merchantSignMsgVal = PayOnline.AppendParam(merchantSignMsgVal, "payResult", payResult);
        merchantSignMsgVal = PayOnline.AppendParam(merchantSignMsgVal, "errCode", errCode);
        merchantSignMsgVal = PayOnline.AppendParam(merchantSignMsgVal, "key", key);
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
        PayOnline.TestLog(false, "快钱支付", payResult, signMsg.ToUpper(), md5string.ToUpper());
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>快钱支付</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <pe:ExtendedLabel HtmlEncode="false" ID="LblMsg" runat="server"></pe:ExtendedLabel>
        </div>
    </form>
</body>
</html>
