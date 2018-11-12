<%@ Page Language="C#"  StylesheetTheme="" EnableTheming="false"%>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        EasyOne.Shop.PayOnline.TestLog(true, "支付宝即时到帐f", "", "", "");
        string payOnlineShopID;
        string PayOnlineKey;

        int payPlatformId = 8;  //支付宝
        EasyOne.Model.Accessories.PayPlatformInfo payPlatform = EasyOne.Accessories.PayPlatform.GetPayPlatformById(payPlatformId);
        payOnlineShopID = payPlatform.AccountsId;
        PayOnlineKey = payPlatform.MD5;

        string partner = "", key = "";

        if (PayOnlineKey.IndexOf("|") > 0)
        {
            string[] ArrMD5Key = payPlatform.MD5.Split(new char[] { '|' });
            key = ArrMD5Key[0];        //partner 的对应交易安全校验码（必须填写）
            partner = ArrMD5Key[1];   //partner合作伙伴id（必须填写）
        }


        string v_oid = DelStr(Request["out_trade_no"]);           //商户定单号
        string trade_status = DelStr(Request["trade_status"]);
        string v_amount = DelStr(Request["price"]);
        string notify_id = Request.Form["notify_id"];

        string alipayNotifyURL = "https://www.alipay.com/cooperate/gateway.do?";

        alipayNotifyURL = alipayNotifyURL + "service=notify_verify" + "&partner=" + partner + "&notify_id=" + Request.Form["notify_id"];

        //获取支付宝ATN返回结果，true是正确的订单信息，false 是无效的
        string responseTxt = Get_Http(alipayNotifyURL, 120000);

        int i;
        NameValueCollection coll;

        coll = Request.Form;

        String[] requestarr = coll.AllKeys;

        //进行排序；
        string[] Sortedstr = BubbleSort(requestarr);

        //构造待md5摘要字符串 ；
        string prestr = "";
        for (i = 0; i < Sortedstr.Length; i++)
        {
            if (Request.Form[Sortedstr[i]] != "" && Sortedstr[i] != "sign" && Sortedstr[i] != "sign_type")
            {
                if (i == Sortedstr.Length - 1)
                {
                    prestr = prestr + Sortedstr[i] + "=" + Request.Form[Sortedstr[i]];
                }
                else
                {
                    prestr = prestr + Sortedstr[i] + "=" + Request.Form[Sortedstr[i]] + "&";
                }
            }

        }
        prestr = prestr + key;

        string mysign = GetMD5(prestr);


        string sign = Request.Form["sign"];
        string returnTxt = "fail";


        if (sign == mysign && responseTxt == "true")
        {
            if(trade_status == "TRADE_FINISHED")  //交易成功，更新订单
            {
                returnTxt = "success";
                EasyOne.Shop.PayOnline payOnline = new EasyOne.Shop.PayOnline();
                payOnline.UpdateOrder(v_oid, EasyOne.Common.DataConverter.CDecimal(v_amount), "", 3, "", true, true);
            }
            else
            {
                returnTxt = "success";
            }
        }
        Response.Write(returnTxt);
        EasyOne.Shop.PayOnline.TestLog(false, "支付宝即时到帐", trade_status, sign, mysign);
    }
    
    
    public static string GetMD5(string s)
    {
        /// <summary>
        /// 与ASP兼容的MD5加密算法
        /// </summary>

        System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] t = md5.ComputeHash(Encoding.GetEncoding("gb2312").GetBytes(s));
        StringBuilder sb = new StringBuilder(32);
        for (int i = 0; i < t.Length; i++)
        {
            sb.Append(t[i].ToString("x").PadLeft(2, '0'));
        }
        return sb.ToString();
    }
    public static string[] BubbleSort(string[] R)
    {
        /// <summary>
        /// 冒泡排序法
        /// </summary>
        int i, j; //交换标志 
        string temp;

        bool exchange;

        for (i = 0; i < R.Length; i++) //最多做R.Length-1趟排序 
        {
            exchange = false; //本趟排序开始前，交换标志应为假

            for (j = R.Length - 2; j >= i; j--)
            {
                if (System.String.CompareOrdinal(R[j + 1], R[j]) < 0)　//交换条件
                {
                    temp = R[j + 1];
                    R[j + 1] = R[j];
                    R[j] = temp;

                    exchange = true; //发生了交换，故将交换标志置为真 
                }
            }

            if (!exchange) //本趟排序未发生交换，提前终止算法 
            {
                break;
            }

        }
        return R;
    }
    //获取远程服务器ATN结果
    public String Get_Http(String a_strUrl, int timeout)
    {
        string strResult;
        try
        {

            System.Net.HttpWebRequest myReq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(a_strUrl);
            myReq.Timeout = timeout;
            System.Net.HttpWebResponse HttpWResp = (System.Net.HttpWebResponse)myReq.GetResponse();
            System.IO.Stream myStream = HttpWResp.GetResponseStream();
            System.IO.StreamReader sr = new System.IO.StreamReader(myStream, Encoding.Default);
            StringBuilder strBuilder = new StringBuilder();
            while (-1 != sr.Peek())
            {
                strBuilder.Append(sr.ReadLine());
            }

            strResult = strBuilder.ToString();
        }
        catch (Exception exp)
        {

            strResult = "错误：" + exp.Message;
        }

        return strResult;
    }

    

    private string DelStr(string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            str = "";
        }
        str = str.Replace(";", "");
        str = str.Replace("'", "");
        str = str.Replace("&", "");
        str = str.Replace(" ", "");
        str = str.Replace("　", "");
        str = str.Replace("%20", "");
        str = str.Replace("--", "");
        str = str.Replace("==", "");
        str = str.Replace("<", "");
        str = str.Replace(">", "");
        str = str.Replace("%", "");

        return str;
    }
    

    
</script>

