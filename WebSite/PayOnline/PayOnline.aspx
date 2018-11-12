<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Shop.PayOnlineUI" Codebehind="PayOnline.aspx.cs" %>
<%@ Import Namespace="EasyOne.Common" %>
<%@ Import Namespace="EasyOne.Model.Accessories" %>
<%@ Import Namespace="EasyOne.Shop" %>
<%@ Import Namespace="EasyOne.Accessories" %>
<%@ Import Namespace="EasyOne.Model.Shop" %>
<%@ Import Namespace="EasyOne.Model.Accessories" %>
<%@ Import Namespace="EasyOne.Model.Accessories" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>在线支付</title>
</head>
<body>
        <asp:PlaceHolder ID="form1" runat="server">
            <table class="border" cellspacing="1">
                <tr class="title">
                    <td>
                        <strong>在线支付操作（确认支付款项）</strong></td>
                </tr>
                <tr>
                    <td class="tdbg">
                        <div class="p_center">
                        <table width="500" cellspacing="1" cellpadding="2" style="background-color: #CCCCCC; margin:auto;">
                            <tr class="title">
                                <td colspan="2">
                                    <b>确 认 款 项</b></td>
                            </tr>
                            <tr class="tdbg">
                                <td style="width: 30%" align="right">
                                    支付平台：</td>
                                <td align="left">
                                    <asp:Label ID="LblPayPlatform" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr class="tdbg">
                                <td align="right">
                                    支付序列号：</td>
                                <td align="left">
                                    <asp:Label ID="LblOid" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr class="tdbg">
                                <td align="right">
                                    支付金额：</td>
                                <td align="left">
                                    <asp:Label ID="LblPayMoney" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr class="tdbg">
                                <td align="right">
                                    手续费：</td>
                                <td align="left">
                                    <asp:Label ID="LblRate" runat="server"></asp:Label></td>
                            </tr>
                            <tr class="tdbg">
                                <td align="right">
                                    实际划款金额：</td>
                                <td align="left">
                                    <asp:Label ID="LblvMoney" runat="server"> </asp:Label>
                                </td>
                            </tr>
                            <tr class="tdbg">
                                <td colspan="2">
                                    点击“确认支付”按钮后，将进入<asp:Label ID="LblPayPlatformName" runat="server"></asp:Label>支付界面，在此页面选择您的银行卡。</td>
                            </tr>
                            <tr class="tdbg">
                                <td colspan="2">
                                    <pe:ExtendedLabel HtmlEncode="false" ID="LblHiddenValue" runat="server"></pe:ExtendedLabel>
                                </td>
                            </tr>
                        </table>
                        </div>
                    </td>
                </tr>
            </table>
    </asp:PlaceHolder>
</body>
</html>
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        string v_oid = "";                 //订单编号
        string v_amount = "";              //实际支付金额       
        string v_mid = "";                 //商户编号
        string v_url = "";                 //支付动作完成后返回到该url，支付结果以POST方式发送
        string payOnlineKey = "";        //MD5私钥

        decimal vmoney = 0;           //支付金额   
        int payPlatformId = 0;           //支付平台ID
        string md5string;             //订单MD5校验码
        int orderId;              //订单ID
        string userName = "";          //登录用户名
        string paymentNum = "";       //支付序号
        int pointAmount = 0;        //购买点券数


        orderId = DataConverter.CLng(Request.QueryString["OrderId"]);
        payPlatformId = DataConverter.CLng(Request.QueryString["PayPlatformId"]);
        userName = EasyOne.Components.PEContext.Current.User.UserName;
        pointAmount = DataConverter.CLng(Request.QueryString["PointAmount"]);
        string addOrder = RequestString("AddOrder");
        if (orderId > 0)
        {
            OrderInfo orderInfo = Order.GetOrderById(orderId);
            if (orderInfo.IsNull)
            {
                WriteErrMsg("<li>找不到对应的订单信息！</li>", "");
            }
            if (orderInfo.MoneyReceipt >= orderInfo.MoneyTotal)
            {
                if (addOrder == "success")
                {
                    WriteSuccessMsg("订单已经成功提交！", "../User/Shop/ShowOrder.aspx?OrderID=" + orderId);
                }
                else
                {
                    WriteErrMsg("此订单已经付清，无需再支付！", "../User/Shop/ShowOrder.aspx?OrderID=" + orderId);
                }
            }
            vmoney = orderInfo.MoneyTotal - orderInfo.MoneyReceipt;

        }
        else
        {
            if (string.IsNullOrEmpty(userName))
            {
                Response.Redirect("../User/Login.aspx?ReturnUrl=" + HttpUtility.UrlEncode("../PayOnline/PayOnline.aspx?PayPlatformId=" + payPlatformId.ToString() + "&vMoney=" + vmoney.ToString()));
            }
            if (pointAmount > 0)
            {
                double pointPrice = EasyOne.Components.SiteConfig.UserConfig.MoneyExchangePointByMoney / EasyOne.Components.SiteConfig.UserConfig.MoneyExchangePointByPoint;
                vmoney = DataConverter.CDecimal(pointPrice * pointAmount);
            }
            else
            {
                vmoney = DataConverter.CDecimal(Request["vMoney"]);
            }      
        }

        PayPlatformInfo payPlatformInfo = PayPlatform.GetPayPlatformById(payPlatformId);
        if (!payPlatformInfo.IsNull)
        {
            v_mid = payPlatformInfo.AccountsId;
            payOnlineKey = payPlatformInfo.MD5;
        }
        else
        {
            WriteErrMsg("<li>没有找到对应的支付平台信息！</li>", "");
        }

        v_oid = PaymentLog.GetPaymentNum();    //构造支付ID

        if (payPlatformInfo.PayPlatformId == 11)  //快钱神州行
        {
            v_amount = decimal.Ceiling(vmoney + vmoney * DataConverter.CDecimal(payPlatformInfo.Rate) / 100).ToString();
            vmoney = decimal.Ceiling(vmoney);
        }
        else
        {
            v_amount = decimal.Round(vmoney + vmoney * DataConverter.CDecimal(payPlatformInfo.Rate) / 100, 2).ToString();
            vmoney = decimal.Round(vmoney, 2);
        }

        if (vmoney < 0.01M)
        {
            WriteErrMsg("<li>每次划款金额不能低于0.01元</li>", "");
        }

        paymentNum = v_oid;

        PaymentLogInfo paymentLogInfo = new PaymentLogInfo();
        paymentLogInfo.UserName = userName;
        paymentLogInfo.OrderId = orderId;
        paymentLogInfo.PaymentNum = paymentNum;
        paymentLogInfo.PlatformId = payPlatformInfo.PayPlatformId;
        paymentLogInfo.MoneyPay = vmoney;
        paymentLogInfo.MoneyTrue = DataConverter.CDecimal(v_amount);
        paymentLogInfo.PayTime = DateTime.Now;
        paymentLogInfo.Status = 1;
        paymentLogInfo.PlatformInfo = "";
        paymentLogInfo.Remark = "";
        paymentLogInfo.SuccessTime = null;
        paymentLogInfo.Point = pointAmount;

        if (!PaymentLog.Add(paymentLogInfo))
        {
            WriteErrMsg("<li>保存在线支付记录不成功！</li>", "");
        }

        DateTime datatime = DateTime.Now;
        string v_hms = datatime.ToString("HHmmss", System.Globalization.DateTimeFormatInfo.InvariantInfo);
        string v_ymd = datatime.ToString("yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo);

        StringBuilder strHiddenField = new StringBuilder();
        StringBuilder md5Builder = new StringBuilder();
        StringBuilder v_urlBuilder = new StringBuilder();    //构造返回URL

        string applicationName = PayOnline.GetApplicationName();
        if (!applicationName.EndsWith("/"))
        {
            applicationName = applicationName + "/";
        }
        bool isFabrication = false;

        v_urlBuilder.Append("http://");
        v_urlBuilder.Append(applicationName);

        string v_ShowResultUrl = v_urlBuilder.ToString() + "PayOnline/ShowResult.aspx?PayMessage=ok";


        switch (paymentLogInfo.PlatformId)
        {
            case 1:    //网银在线
                m_PayOnlineProviderUrl = "https://pay3.chinabank.com.cn/PayGate";
                //生成返回URL
                v_urlBuilder.Append("PayOnline/PayResultChinabank.aspx");
                v_url = v_urlBuilder.ToString();
                //生成MD5校验数据字符串
                md5Builder.Append(v_amount);
                md5Builder.Append("0");
                md5Builder.Append(v_oid);
                md5Builder.Append(v_mid);
                md5Builder.Append(v_url);
                md5Builder.Append(payOnlineKey);
                md5string = StringHelper.MD5(md5Builder.ToString()).ToUpper();

                strHiddenField.Append("<input type='hidden' name='v_md5info' value='" + md5string + "'>");
                strHiddenField.Append("<input type='hidden' name='v_mid' value='" + v_mid + "'>");
                strHiddenField.Append("<input type='hidden' name='v_oid' value='" + v_oid + "'>");
                strHiddenField.Append("<input type='hidden' name='v_amount' value='" + v_amount + "'>");
                strHiddenField.Append("<input type='hidden' name='v_moneytype'  value='0'>");
                strHiddenField.Append("<input type='hidden' name='v_url' value='" + v_url + "'>");

                break;
                
            case 2://'中国在线支付网
                m_PayOnlineProviderUrl = "http://www.ipay.cn/4.0/bank.shtml";
                v_urlBuilder.Append("PayOnline/PayResultIpay.aspx");
                v_url = v_urlBuilder.ToString();

                md5Builder.Append(v_mid);
                md5Builder.Append(v_oid);
                md5Builder.Append(v_amount);
                md5Builder.Append("test@Ipay.com.cn13800138000");
                md5Builder.Append(payOnlineKey);
                md5string = StringHelper.MD5(md5Builder.ToString());

                strHiddenField.Append("<input type='hidden' name='v_mid' value='" + v_mid + "'>");
                strHiddenField.Append("<input type='hidden' name='v_oid' value='" + v_oid + "'>");
                strHiddenField.Append("<input type='hidden' name='v_amount' value='" + v_amount + "'>");
                strHiddenField.Append("<input type='hidden' name='v_email' value='test@Ipay.com.cn'>");
                strHiddenField.Append("<input type='hidden' name='v_mobile' value='13800138000'>");
                strHiddenField.Append("<input type='hidden' name='v_md5' value='" + md5string + "'>");
                strHiddenField.Append("<input type='hidden' name='v_url' value='" + v_url + "'>");
                break;

            case 3://上海环迅
                m_PayOnlineProviderUrl = "http://pay.ips.com.cn/ipayment.aspx";
                //m_PayOnlineProviderUrl = "http://pay.ips.net.cn/ipayment.aspx";  //测试接口，配合测试帐号测试
                v_urlBuilder.Append("PayOnline/PayResultIps.aspx");
                v_url = v_urlBuilder.ToString();

                md5Builder.Append(v_oid);
                md5Builder.Append(v_amount);
                md5Builder.Append(v_ymd);
                md5Builder.Append("RMB");
                md5Builder.Append(payOnlineKey);
                md5string = StringHelper.MD5(md5Builder.ToString()).ToLower();

                strHiddenField.Append("<input type='hidden' name='mer_code' value='" + v_mid + "'>");
                strHiddenField.Append("<input type='hidden' name='billNo' value='" + v_oid + "'>");
                strHiddenField.Append("<input type='hidden' name='amount' value='" + v_amount + "'>");
                strHiddenField.Append("<input type='hidden' name='date' value='" + v_ymd + "'>");
                strHiddenField.Append("<input type='hidden' name='lang' value='GB'>");
                strHiddenField.Append("<input type='hidden' name='Gateway_type' value='01'>");
                strHiddenField.Append("<input type='hidden' name='Currency_Type' value='RMB'>");
                strHiddenField.Append("<input type='hidden' name='Merchanturl' value='" + v_url + "'>");
                strHiddenField.Append("<input type='hidden' name='OrderEncodeType' value='2'>");
                strHiddenField.Append("<input type='hidden' name='RetEncodeType' value='12'>");
                strHiddenField.Append("<input type='hidden' name='RetType' value='0'>");
                strHiddenField.Append("<input type='hidden' name='SignMD5' value='" + md5string + "'>");
                strHiddenField.Append("<input type='hidden' name='ServerUrl' value=''>");
                break;
            case 5://西部支付
                m_PayOnlineProviderUrl = "http://www.yeepay.com/Pay/WestPayReceiveOrderFromMerchant.asp";
                v_urlBuilder.Append("PayOnline/PayResultYeepay.aspx");
                v_url = v_urlBuilder.ToString();

                strHiddenField.Append("<input type='hidden' name='MerchantID' value='" + v_mid + "'>");
                strHiddenField.Append("<input type='hidden' name='OrderNumber' value='" + v_oid + "'>");
                strHiddenField.Append("<input type='hidden' name='OrderAmount' value='" + v_amount + "'>");
                strHiddenField.Append("<input type='hidden' name='PostBackURL' value='" + v_url + "'>");
                break;

            case 6://易付通
                m_PayOnlineProviderUrl = "http://pay.xpay.cn/Pay.aspx";
                v_urlBuilder.Append("PayOnline/PayResultXpay.aspx");
                v_url = v_urlBuilder.ToString();

                md5Builder.Append(payOnlineKey);
                md5Builder.Append(":");
                md5Builder.Append(v_amount);
                md5Builder.Append(",");
                md5Builder.Append(v_oid);
                md5Builder.Append(",");
                md5Builder.Append(v_mid);
                md5Builder.Append(",bank,,sell,,2.0");
                md5string = StringHelper.MD5(md5Builder.ToString()).ToLower();

                strHiddenField.Append("<input type='hidden' name='Tid' value='" + v_mid + "'>");
                strHiddenField.Append("<input type='hidden' name='Bid' value='" + v_oid + "'>");
                strHiddenField.Append("<input type='hidden' name='Prc' value='" + v_amount + "'>");
                strHiddenField.Append("<input type='hidden' name='url' value='" + v_url + "'>");
                strHiddenField.Append("<input type='hidden' name='Card' value='bank'>");
                strHiddenField.Append("<input type='hidden' name='Scard' value=''>");
                strHiddenField.Append("<input type='hidden' name='ActionCode' value='sell'>");
                strHiddenField.Append("<input type='hidden' name='ActionParameter' value=''>");
                strHiddenField.Append("<input type='hidden' name='Ver' value='2.0'>");
                strHiddenField.Append("<input type='hidden' name='Pdt' value='" + applicationName + "'>");
                strHiddenField.Append("<input type='hidden' name='type' value=''>");
                strHiddenField.Append("<input type='hidden' name='lang' value='gb2312'>");
                strHiddenField.Append("<input type='hidden' name='md' value='" + md5string + "'>");
                break;

            case 7://云网支付
                m_PayOnlineProviderUrl = "https://www.cncard.net/purchase/getorder.asp";
                v_urlBuilder.Append("PayOnline/PayResultCncard.aspx");
                v_url = v_urlBuilder.ToString();

                md5Builder.Append(v_mid);
                md5Builder.Append(v_oid);
                md5Builder.Append(v_amount);
                md5Builder.Append(v_ymd);
                md5Builder.Append("01");
                md5Builder.Append(v_url);
                md5Builder.Append("00");
                md5Builder.Append(payOnlineKey);
                md5string = StringHelper.MD5(md5Builder.ToString()).ToLower();

                strHiddenField.Append("<input type='hidden' name='c_mid' value='" + v_mid + "'>");
                strHiddenField.Append("<input type='hidden' name='c_order' value='" + v_oid + "'>");
                strHiddenField.Append("<input type='hidden' name='c_orderamount' value='" + v_amount + "'>");
                strHiddenField.Append("<input type='hidden' name='c_ymd' value='" + v_ymd + "'>");
                strHiddenField.Append("<input type='hidden' name='c_moneytype' value='0'>");
                strHiddenField.Append("<input type='hidden' name='c_retflag' value='1'>");
                strHiddenField.Append("<input type='hidden' name='c_paygate' value=''>");
                strHiddenField.Append("<input type='hidden' name='c_returl' value='" + v_url + "'>");
                strHiddenField.Append("<input type='hidden' name='c_memo1' value=''>");
                strHiddenField.Append("<input type='hidden' name='c_memo2' value=''>");
                strHiddenField.Append("<input type='hidden' name='c_language' value='0'>");
                strHiddenField.Append("<input type='hidden' name='notifytype' value='0'>");
                strHiddenField.Append("<input type='hidden' name='c_signstr' value='" + md5string + "'>");
                break;
            case 8:    //支付宝
                m_PayOnlineProviderUrl = "https://www.alipay.com/cooperate/gateway.do";
                v_urlBuilder.Append("PayOnline/PayResultAlipay.aspx");
                v_url = v_urlBuilder.ToString();  
                         

                //订单含有虚拟商品的情况
                if (orderId == 0)
                {
                    isFabrication = true;   //会员充值，视为虚拟物品
                }
                else
                {
                    isFabrication = PayOnline.IsFabrication(orderId);   //检测订单中的物品是否含有虚拟物品
                }

                string partner = "";
                if (payOnlineKey.IndexOf("|") > 0)
                {
                    string[] ArrMD5Key = payOnlineKey.Split(new char[] { '|' });
                    payOnlineKey = ArrMD5Key[0];
                    partner = ArrMD5Key[1];
                }

                if (isFabrication)
                {
                    md5Builder.Append("notify_url=" + v_url);
                    md5Builder.Append("&out_trade_no=" + v_oid);
                    md5Builder.Append("&partner=" + partner);
                    md5Builder.Append("&price=" + v_amount);
                    md5Builder.Append("&quantity=1");
                    md5Builder.Append("&return_url=" + v_ShowResultUrl);
                    md5Builder.Append("&seller_email=" + v_mid);
                    md5Builder.Append("&service=create_digital_goods_trade_p");
                    md5Builder.Append("&subject=" + v_oid);
                    md5Builder.Append("");
                    md5Builder.Append(payOnlineKey);
                    strHiddenField.Append("<input type='hidden' name='service' value='create_digital_goods_trade_p'>");
                }
                else
                {
                    md5Builder.Append("logistics_fee=0");
                    md5Builder.Append("&logistics_payment=SELLER_PAY");
                    md5Builder.Append("&logistics_type=EXPRESS");
                    md5Builder.Append("&notify_url=" + v_url);
                    md5Builder.Append("&out_trade_no=" + v_oid);
                    md5Builder.Append("&partner=" + partner);
                    md5Builder.Append("&payment_type=1");
                    md5Builder.Append("&price=" + v_amount);
                    md5Builder.Append("&quantity=1");
                    md5Builder.Append("&return_url=" + v_ShowResultUrl);
                    md5Builder.Append("&seller_email=" + v_mid);
                    md5Builder.Append("&service=trade_create_by_buyer");
                    md5Builder.Append("&subject=" + v_oid);
                    md5Builder.Append(payOnlineKey);
                    strHiddenField.Append("<input type='hidden' name='service' value='trade_create_by_buyer'>");
                    strHiddenField.Append("<input type='hidden' name='logistics_type' value='EXPRESS'>");
                    strHiddenField.Append("<input type='hidden' name='logistics_fee' value='0'>");
                    strHiddenField.Append("<input type='hidden' name='logistics_payment' value='SELLER_PAY'>");
                    strHiddenField.Append("<input type='hidden' name='payment_type' value='1'>");
                }
                md5string = StringHelper.MD5(md5Builder.ToString()).ToLower();

                strHiddenField.Append("<input type='hidden' name='seller_email' value='" + v_mid + "'>");
                strHiddenField.Append("<input type='hidden' name='subject' value='" + v_oid + "'>");
                strHiddenField.Append("<input type='hidden' name='out_trade_no' value='" + v_oid + "'>");
                strHiddenField.Append("<input type='hidden' name='price' value='" + v_amount + "'>");
                strHiddenField.Append("<input type='hidden' name='partner' value='" + partner + "'>");
                strHiddenField.Append("<input type='hidden' name='quantity' value='1'>");
                strHiddenField.Append("<input type='hidden' name='notify_url' value='" + v_url + "'>");
                strHiddenField.Append("<input type='hidden' name='sign' value='" + md5string + "'>");
                strHiddenField.Append("<input type='hidden' name='sign_type' value='MD5'>");
                strHiddenField.Append("<input type='hidden' name='return_url' value='" + v_ShowResultUrl + "'>\n");
                break;                
            case 9:      //快钱支付
                m_PayOnlineProviderUrl = "https://www.99bill.com/gateway/recvMerchantInfoAction.htm";
                //生成返回URL
                v_urlBuilder.Append("PayOnline/PayResult99bill.aspx");
                v_url = v_urlBuilder.ToString();

                string merchantAcctId = v_mid;   //网关账户号
                string key = payOnlineKey; //网关密钥
                string inputCharset = "3"; //1代表UTF-8; 2代表GBK; 3代表gb2312
                string pageUrl = v_url; //接受支付结果的页面地址
                string bgUrl = ""; //服务器接受支付结果的后台地址
                string version = "v2.0"; //网关版本.固定值
                string language = "1"; //1代表中文；2代表英文
                string signType = "1"; //1代表MD5签名
                string payerName = ""; //支付人姓名
                string payerContactType = ""; //支付人联系方式类型 1代表Email；2代表手机号
                string payerContact = ""; //支付人联系方式,只能选择Email或手机号
                string orderAmount = Convert.ToString(decimal.Ceiling(DataConverter.CDecimal(v_amount) * 100)); //订单金额,以分为单位
                string orderTime = v_ymd + v_hms; //订单提交时间,14位数字
                string productName = ""; //商品名称
                string productNum = ""; //商品数量
                string productId = ""; //商品代码
                string productDesc = ""; //商品描述
                string ext1 = ""; //扩展字段1,在支付结束后原样返回给商户
                string ext2 = ""; //扩展字段2
                string payType = "00"; //支付方式,00：组合支付,显示快钱支持的各种支付方式,11：电话银行支付,12：快钱账户支付,13：线下支付,14：B2B支付
                string bankId = ""; //银行代码,实现直接跳转到银行页面去支付,具体代码参见 接口文档银行代码列表,只在payType=10时才需设置参数
                string redoFlag = "1"; //同一订单禁止重复提交标志:1代表同一订单号只允许提交1次,0表示同一订单号在没有支付成功的前提下可重复提交多次
                string pid = ""; //快钱的合作伙伴的账户号

                string signMsgVal = "";
                signMsgVal = PayOnline.AppendParam(signMsgVal, "inputCharset", inputCharset);
                signMsgVal = PayOnline.AppendParam(signMsgVal, "pageUrl", pageUrl);
                signMsgVal = PayOnline.AppendParam(signMsgVal, "bgUrl", bgUrl);
                signMsgVal = PayOnline.AppendParam(signMsgVal, "version", version);
                signMsgVal = PayOnline.AppendParam(signMsgVal, "language", language);
                signMsgVal = PayOnline.AppendParam(signMsgVal, "signType", signType);
                signMsgVal = PayOnline.AppendParam(signMsgVal, "merchantAcctId", merchantAcctId);
                signMsgVal = PayOnline.AppendParam(signMsgVal, "payerName", payerName);
                signMsgVal = PayOnline.AppendParam(signMsgVal, "payerContactType", payerContactType);
                signMsgVal = PayOnline.AppendParam(signMsgVal, "payerContact", payerContact);
                signMsgVal = PayOnline.AppendParam(signMsgVal, "orderId", v_oid);  //商户订单号
                signMsgVal = PayOnline.AppendParam(signMsgVal, "orderAmount", orderAmount);
                signMsgVal = PayOnline.AppendParam(signMsgVal, "orderTime", orderTime);
                signMsgVal = PayOnline.AppendParam(signMsgVal, "productName", productName);
                signMsgVal = PayOnline.AppendParam(signMsgVal, "productNum", productNum);
                signMsgVal = PayOnline.AppendParam(signMsgVal, "productId", productId);
                signMsgVal = PayOnline.AppendParam(signMsgVal, "productDesc", productDesc);
                signMsgVal = PayOnline.AppendParam(signMsgVal, "ext1", ext1);
                signMsgVal = PayOnline.AppendParam(signMsgVal, "ext2", ext2);
                signMsgVal = PayOnline.AppendParam(signMsgVal, "payType", payType);
                signMsgVal = PayOnline.AppendParam(signMsgVal, "bankId", bankId);
                signMsgVal = PayOnline.AppendParam(signMsgVal, "redoFlag", redoFlag);
                signMsgVal = PayOnline.AppendParam(signMsgVal, "pid", pid);
                signMsgVal = PayOnline.AppendParam(signMsgVal, "key", key);

                md5string = StringHelper.MD5(signMsgVal).ToUpper();

                strHiddenField.Append("<input type='hidden' name='inputCharset' value='" + inputCharset + "'>\n");
                strHiddenField.Append("<input type='hidden' name='bgUrl' value='" + bgUrl + "'>\n");
                strHiddenField.Append("<input type='hidden' name='pageUrl' value='" + pageUrl + "'>\n");
                strHiddenField.Append("<input type='hidden' name='version' value='" + version + "'>\n");
                strHiddenField.Append("<input type='hidden' name='language' value='" + language + "'>\n");
                strHiddenField.Append("<input type='hidden' name='signType' value='" + signType + "'>\n");
                strHiddenField.Append("<input type='hidden' name='signMsg' value='" + md5string + "'>\n");
                strHiddenField.Append("<input type='hidden' name='merchantAcctId' value='" + merchantAcctId + "'>\n");
                strHiddenField.Append("<input type='hidden' name='payerName' value='" + payerName + "'>\n");
                strHiddenField.Append("<input type='hidden' name='payerContactType' value='" + payerContactType + "'>\n");
                strHiddenField.Append("<input type='hidden' name='payerContact' value='" + payerContact + "'>\n");
                strHiddenField.Append("<input type='hidden' name='orderId' value='" + v_oid + "'>\n");
                strHiddenField.Append("<input type='hidden' name='orderAmount' value='" + orderAmount + "'>\n");
                strHiddenField.Append("<input type='hidden' name='orderTime' value='" + orderTime + "'>\n");
                strHiddenField.Append("<input type='hidden' name='productName' value='" + productName + "'>\n");
                strHiddenField.Append("<input type='hidden' name='productNum' value='" + productNum + "'>\n");
                strHiddenField.Append("<input type='hidden' name='productId' value='" + productId + "'>\n");
                strHiddenField.Append("<input type='hidden' name='productDesc' value='" + productDesc + "'>\n");
                strHiddenField.Append("<input type='hidden' name='ext1' value='" + ext1 + "'>\n");
                strHiddenField.Append("<input type='hidden' name='ext2' value='" + ext2 + "'>\n");
                strHiddenField.Append("<input type='hidden' name='payType' value='" + payType + "'>\n");
                strHiddenField.Append("<input type='hidden' name='bankId' value='" + bankId + "'>\n");
                strHiddenField.Append("<input type='hidden' name='redoFlag' value='" + redoFlag + "'>\n");
                strHiddenField.Append("<input type='hidden' name='pid' value='" + pid + "'>\n");
                break;
            case 11:  //快钱神州行
                m_PayOnlineProviderUrl = "https://www.99bill.com/szxgateway/recvMerchantInfoAction.htm";
                //生成返回URL
                v_urlBuilder.Append("PayOnline/PayResult99billSzx.aspx");
                v_url = v_urlBuilder.ToString();

                merchantAcctId = v_mid; //神州行网关账户号
                key = payOnlineKey; //设置人民币网关密钥
                inputCharset = "3"; //1代表UTF-8; 2代表GBK; 3代表gb2312
                bgUrl = ""; //服务器接受支付结果的后台地址
                pageUrl = v_url; //接受支付结果的页面地址
                version = "v2.0"; //网关版本.固定值
                language = "1"; //1代表中文；2代表英文
                signType = "1"; //签名类型.固定值
                payerName = ""; //支付人姓名
                payerContactType = ""; //支付人联系方式类型,1代表Email；2代表手机号
                payerContact = ""; //支付人联系方式,只能选择Email或手机号
                orderAmount = Convert.ToString(decimal.Ceiling(DataConverter.CDecimal(v_amount) * 100)); //订单金额,以分为单位，必须是整型数字
                orderTime = v_ymd + v_hms; //订单提交时间
                productName = ""; //商品名称
                productNum = ""; //商品数量
                productId = ""; //商品代码
                productDesc = ""; //商品描述
                ext1 = ""; //扩展字段1
                ext2 = ""; //扩展字段2
                payType = "00"; //只能选择00,代表支持神州行卡和快钱帐户支付
                string cardNumber = ""; //神州行卡序号,仅在商户定制了神州行卡密直连功能时填写
                string cardPwd = ""; //神州行卡密码,仅在商户定制了神州行卡密直连功能时填写
                //全额支付标志       ////0代表卡面额小于订单金额时返回支付结果为失败；1代表卡面额小于订单金额是返回支付结果为成功，同时订单金额和实际支付金额都为神州行卡的面额.如果商户定制神州行卡密直连时，本参数固定值为1
                string fullAmountFlag = "0"; //0代表卡面额小于订单金额时返回支付结果为失败

                //请务必按照如下顺序和规则组成加密串！
                signMsgVal = "";
                signMsgVal = PayOnline.AppendParam(signMsgVal, "inputCharset", inputCharset);
                signMsgVal = PayOnline.AppendParam(signMsgVal, "bgUrl", bgUrl);
                signMsgVal = PayOnline.AppendParam(signMsgVal, "pageUrl", pageUrl);
                signMsgVal = PayOnline.AppendParam(signMsgVal, "version", version);
                signMsgVal = PayOnline.AppendParam(signMsgVal, "language", language);
                signMsgVal = PayOnline.AppendParam(signMsgVal, "signType", signType);
                signMsgVal = PayOnline.AppendParam(signMsgVal, "merchantAcctId", merchantAcctId);
                signMsgVal = PayOnline.AppendParam(signMsgVal, "payerName", payerName);
                signMsgVal = PayOnline.AppendParam(signMsgVal, "payerContactType", payerContactType);
                signMsgVal = PayOnline.AppendParam(signMsgVal, "payerContact", payerContact);
                signMsgVal = PayOnline.AppendParam(signMsgVal, "orderId", v_oid);
                signMsgVal = PayOnline.AppendParam(signMsgVal, "orderAmount", orderAmount);
                signMsgVal = PayOnline.AppendParam(signMsgVal, "payType", payType);
                signMsgVal = PayOnline.AppendParam(signMsgVal, "cardNumber", cardNumber);
                signMsgVal = PayOnline.AppendParam(signMsgVal, "cardPwd", cardPwd);
                signMsgVal = PayOnline.AppendParam(signMsgVal, "fullAmountFlag", fullAmountFlag);
                signMsgVal = PayOnline.AppendParam(signMsgVal, "orderTime", orderTime);
                signMsgVal = PayOnline.AppendParam(signMsgVal, "productName", productName);
                signMsgVal = PayOnline.AppendParam(signMsgVal, "productNum", productNum);
                signMsgVal = PayOnline.AppendParam(signMsgVal, "productId", productId);
                signMsgVal = PayOnline.AppendParam(signMsgVal, "productDesc", productDesc);
                signMsgVal = PayOnline.AppendParam(signMsgVal, "ext1", ext1);
                signMsgVal = PayOnline.AppendParam(signMsgVal, "ext2", ext2);
                signMsgVal = PayOnline.AppendParam(signMsgVal, "key", key);
                md5string = StringHelper.MD5(signMsgVal).ToUpper();

                strHiddenField.Append("<input type='hidden' name='inputCharset' value='" + inputCharset + "'>\n");
                strHiddenField.Append("<input type='hidden' name='bgUrl' value='" + bgUrl + "'>\n");
                strHiddenField.Append("<input type='hidden' name='pageUrl' value='" + pageUrl + "'>\n");
                strHiddenField.Append("<input type='hidden' name='version' value='" + version + "'>\n");
                strHiddenField.Append("<input type='hidden' name='language' value='" + language + "'>\n");
                strHiddenField.Append("<input type='hidden' name='signType' value='" + signType + "'>\n");
                strHiddenField.Append("<input type='hidden' name='merchantAcctId' value='" + merchantAcctId + "'>\n");
                strHiddenField.Append("<input type='hidden' name='payerName' value='" + payerName + "'>\n");
                strHiddenField.Append("<input type='hidden' name='payerContactType' value='" + payerContactType + "'>\n");
                strHiddenField.Append("<input type='hidden' name='payerContact' value='" + payerContact + "'>\n");
                strHiddenField.Append("<input type='hidden' name='orderId' value='" + v_oid + "'>\n");
                strHiddenField.Append("<input type='hidden' name='orderAmount' value='" + orderAmount + "'>\n");
                strHiddenField.Append("<input type='hidden' name='orderTime' value='" + orderTime + "'>\n");
                strHiddenField.Append("<input type='hidden' name='productName' value='" + productName + "'>\n");
                strHiddenField.Append("<input type='hidden' name='productNum' value='" + productNum + "'>\n");
                strHiddenField.Append("<input type='hidden' name='productId' value='" + productId + "'>\n");
                strHiddenField.Append("<input type='hidden' name='productDesc' value='" + productDesc + "'>\n");
                strHiddenField.Append("<input type='hidden' name='ext1' value='" + ext1 + "'>\n");
                strHiddenField.Append("<input type='hidden' name='ext2' value='" + ext2 + "'>\n");
                strHiddenField.Append("<input type='hidden' name='payType' value='" + payType + "'>\n");
                strHiddenField.Append("<input type='hidden' name='fullAmountFlag' value='" + fullAmountFlag + "'>\n");
                strHiddenField.Append("<input type='hidden' name='cardNumber' value='" + cardNumber + "'>\n");
                strHiddenField.Append("<input type='hidden' name='cardPwd' value='" + cardPwd + "'>\n");
                strHiddenField.Append("<input type='hidden' name='signMsg' value='" + md5string + "'>\n");
                break;
            case 12:   //支付宝即时到帐
                m_PayOnlineProviderUrl = "https://www.alipay.com/cooperate/gateway.do";
                v_urlBuilder.Append("PayOnline/PayResultAlipayInstant.aspx");
                v_url = v_urlBuilder.ToString();
                v_ShowResultUrl = v_ShowResultUrl + "&PaymentNum=" + v_oid;
                partner = "";
                if (payOnlineKey.IndexOf("|") > 0)
                {
                    string[] ArrMD5Key = payOnlineKey.Split(new char[] { '|' });
                    payOnlineKey = ArrMD5Key[0];
                    partner = ArrMD5Key[1];
                }
                
                md5Builder.Append("discount=0");
                md5Builder.Append("&notify_url=" + v_url);
                md5Builder.Append("&out_trade_no=" + v_oid);
                md5Builder.Append("&partner=" + partner);
                md5Builder.Append("&payment_type=1");
                md5Builder.Append("&price=" + v_amount);
                md5Builder.Append("&quantity=1");
                md5Builder.Append("&return_url=" + v_ShowResultUrl);
                md5Builder.Append("&seller_email=" + v_mid);
                md5Builder.Append("&service=create_direct_pay_by_user");
                md5Builder.Append("&subject=" + v_oid);
                md5Builder.Append(payOnlineKey);          
                md5string = StringHelper.MD5(md5Builder.ToString()).ToLower();
                
                strHiddenField.Append("<input type='hidden' name='discount' value='0'>\n"); 
                strHiddenField.Append("<input type='hidden' name='notify_url' value='" + v_url + "'>\n");
                strHiddenField.Append("<input type='hidden' name='out_trade_no' value='" + v_oid + "'>\n");
                strHiddenField.Append("<input type='hidden' name='payment_type' value='1'>\n");
                strHiddenField.Append("<input type='hidden' name='partner' value='" + partner + "'>\n");
                strHiddenField.Append("<input type='hidden' name='price' value='" + v_amount + "'>\n");
                strHiddenField.Append("<input type='hidden' name='quantity' value='1'>\n");
                strHiddenField.Append("<input type='hidden' name='seller_email' value='" + v_mid + "'>\n");
                strHiddenField.Append("<input type='hidden' name='service' value='create_direct_pay_by_user'>\n");
                strHiddenField.Append("<input type='hidden' name='subject' value='" + v_oid + "'>\n");
                strHiddenField.Append("<input type='hidden' name='sign' value='" + md5string + "'>\n");
                strHiddenField.Append("<input type='hidden' name='sign_type' value='MD5'>\n");
                strHiddenField.Append("<input type='hidden' name='return_url' value='" + v_ShowResultUrl + "'>\n");
                break;              
            case 13: //财付通
                v_urlBuilder.Append("PayOnline/PayResultTenpay.aspx");
                v_url = v_urlBuilder.ToString();
                
                //string transaction_id = v_mid + v_ymd + v_oid.Substring(0,10);
                string transaction_id = v_mid + v_ymd + v_oid.Substring(v_oid.Length-10, 10);
                
                
                m_PayOnlineProviderUrl = "https://www.tenpay.com/cgi-bin/v1.0/pay_gate.cgi";            
                string amount = Convert.ToString(decimal.Round(DataConverter.CDecimal(v_amount) * 100,0));
                md5string = StringHelper.MD5("cmdno=1&date=" + v_ymd + "&bargainor_id=" + v_mid + "&transaction_id=" + transaction_id + "&sp_billno=" + v_oid + "&total_fee=" + amount + "&fee_type=1&return_url=" + v_url + "&attach=my_magic_string&key=" + payOnlineKey);
                strHiddenField.Append("<input type='hidden' name='cmdno' value='1'>\n");        //业务代码,1表示支付
                strHiddenField.Append("<input type='hidden' name='date' value='" + v_ymd + "'>\n");       //商户日期
                strHiddenField.Append("<input type='hidden' name='bank_type' value='0'>\n");        //银行类型:财付通,0
                strHiddenField.Append("<input type='hidden' name='desc' value='" + v_oid + "'>\n");       //交易的商品名称
                strHiddenField.Append("<input type='hidden' name='purchaser_id' value=''>\n");       //用户(买方)的财付通帐户,可以为空
                strHiddenField.Append("<input type='hidden' name='bargainor_id' value='" + v_mid + "'>\n");       //商家的商户号
                strHiddenField.Append("<input type='hidden' name='transaction_id' value='" + transaction_id + "'>\n");      //交易号(订单号)
                strHiddenField.Append("<input type='hidden' name='sp_billno' value='" + v_oid + "'>\n");       //商户系统内部的定单号
                strHiddenField.Append("<input type='hidden' name='total_fee' value='" + amount + "'>\n");    //总金额，以分为单位
                strHiddenField.Append("<input type='hidden' name='fee_type' value='1'>\n");      //现金支付币种,1人民币
                strHiddenField.Append("<input type='hidden' name='return_url' value='" + v_url + "'>\n");    //接收财付通返回结果的URL
                strHiddenField.Append("<input type='hidden' name='attach' value='my_magic_string'>\n");      //商家数据包，原样返回
                strHiddenField.Append("<input type='hidden' name='sign' value='" + md5string + "'>\n");     //MD5签名           
                break;   
            default:
                WriteErrMsg("<li>选择的支付平台未集成，请重新选择另外的支付平台！</li>");
                break;
        }
        string newForm = "<form method=\"post\" action='" + m_PayOnlineProviderUrl + "' id=\"form2\">";
        string endFrom = "<input type=\"submit\" id=\"submit\" class=\"inputbutton\" value=\"确认支付\">  <input type=\"button\" class=\"inputbutton\" id=\"Cancel\" value=\"取消支付\" onclick=\"window.location.href='../User/Info/PaymentLog.aspx'\" />";
        endFrom += "  <input type='button' class='inputbutton' id='GotoUserCenter' value='进入会员中心' onclick='window.location.href=\"../User/Default.aspx\"' />";
        if (orderId > 0) 
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                endFrom += "  <input type='button' class='inputbutton' id='GotoUserCenter' value='查看订单明细' onclick='window.location.href=\"../User/Shop/ShowOrder.aspx?OrderId=" + orderId.ToString() + "\"' />";
            }
            else 
            {
                OrderInfo info = Order.GetOrderById(orderId);
                endFrom += "  <input type='button' class='inputbutton' id='GotoUserCenter' value='查看订单明细' onclick='window.location.href=\"../Shop/OrderForm.aspx?OrderNum=" + info.OrderNum + "&Name="
                    +DataSecurity.UrlEncode(info.ContacterName)+"\"' />";
            }            
        }

        endFrom += "</form>";
        m_HiddenValue = newForm + strHiddenField.ToString() + endFrom;

        //给页面赋值
        LblPayPlatform.Text = payPlatformInfo.PayPlatformName;
        LblPayPlatformName.Text = payPlatformInfo.PayPlatformName;
        LblOid.Text = v_oid;
        LblPayMoney.Text = vmoney + " 元";
        LblRate.Text = payPlatformInfo.Rate.ToString() + " %";
        LblvMoney.Text = v_amount + " 元";
        //submit.Attributes.Add("OnClick", "document.getElementById('form2').action='" + m_PayOnlineProviderUrl + "';");
        LblHiddenValue.Text = m_HiddenValue;
    }
</script>
