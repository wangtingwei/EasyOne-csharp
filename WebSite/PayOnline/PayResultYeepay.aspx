<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        string payOnlineShopID;
        string PayOnlineKey;

        int payPlatformId = 5;  //西部支付
        EasyOne.Model.Accessories.PayPlatformInfo payPlatform = EasyOne.Accessories.PayPlatform.GetPayPlatformById(payPlatformId); 
        
        payOnlineShopID = payPlatform.AccountsId;
        PayOnlineKey = payPlatform.MD5;
        bool paySuccess = false;
        
        string v_mid = Request["MerchantID"];
        string v_oid = Request["MerchantOrderNumber"];
        string v_amount = Request["PaidAmount"];

        StringBuilder stringBuilder = new StringBuilder("http://www.yeepay.com/pay/ISPN.asp?");
        stringBuilder.Append(Request.Form.ToString());
        stringBuilder.Append("&cmd=validate");
        string responseTxt = Get_Http(stringBuilder.ToString(), 120000);
        
        if (responseTxt == "VERIFIED")
        {
            if (v_mid.Trim() == payOnlineShopID.Trim())
            {
                paySuccess = true;    //支付通知验证成功
            }
        }
        else if (responseTxt == "INVALID")
        {
            LblMsg.Text = "Invalid";    //支付通知验证失败
        }
        else
        {
            LblMsg.Text = responseTxt;
        }

        if (paySuccess)
        {
            StringBuilder message = new StringBuilder("<br>恭喜你！在线支付成功！<br><br>");
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
        EasyOne.Shop.PayOnline.TestLog(false, "西部支付", responseTxt, v_mid.Trim(), payOnlineShopID.Trim());

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
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>西部支付</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <pe:ExtendedLabel HtmlEncode="false" ID="LblMsg" runat="server"></pe:ExtendedLabel>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        
    </div>
    </form>
</body>
</html>
