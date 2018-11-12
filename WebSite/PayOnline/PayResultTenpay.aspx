<%@ Page Language="C#" StylesheetTheme="" EnableTheming="false"%>
<%@ Import Namespace="EasyOne.Shop" %>
<%@ Import Namespace="EasyOne.Common" %>
<%@ Import Namespace="EasyOne.Enumerations" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
protected void Page_Load(object sender, EventArgs e)
    {
        string payOnlineShopID;
        string payOnlineKey;
        string v_mid, v_oid, v_pmode, v_pstatus, v_pstring, v_amount, v_md5, v_moneytype, md5string;
    
        int payPlatformId = 13;  //财付通
        EasyOne.Model.Accessories.PayPlatformInfo payPlatform = EasyOne.Accessories.PayPlatform.GetPayPlatformById(payPlatformId);
        payOnlineShopID = payPlatform.AccountsId;
        payOnlineKey = payPlatform.MD5;
        v_mid = payPlatform.AccountsId;
        v_oid = "";
        v_pstring = "";
        v_pmode = "";
        v_amount = "";
    
        string cmdno = StringHelper.Trim(Request["cmdno"]);
        string pay_result = StringHelper.Trim(Request["pay_result"]);
        string pay_info = StringHelper.Trim(Request["pay_info"]);
        string bill_date = StringHelper.Trim(Request["date"]);
        string bargainor_id = StringHelper.Trim(Request["bargainor_id"]);
        string transaction_id = StringHelper.Trim(Request["transaction_id"]);
        string sp_billno = StringHelper.Trim(Request["sp_billno"]);
        string total_fee = StringHelper.Trim(Request["total_fee"]);
        string fee_type = StringHelper.Trim(Request["fee_type"]);
        string attach = StringHelper.Trim(Request["attach"]);
        string md5_sign = StringHelper.Trim(Request["sign"]);
        
        md5string =  StringHelper.MD5("cmdno=" + cmdno + "&pay_result=" + pay_result + "&date=" + bill_date + "&transaction_id=" + transaction_id + "&sp_billno=" + sp_billno + "&total_fee=" + total_fee + "&fee_type=" + fee_type + "&attach=" + attach + "&key=" + payOnlineKey);

        StringBuilder message = new StringBuilder();
        bool payResult = false;
        if(bargainor_id == v_mid && md5string.ToUpper() == md5_sign.ToUpper() && pay_result == "0")
        {
            message.Append("<br>恭喜您！在线支付成功！<br><br>");
            payResult = true;
            v_oid = sp_billno;
            v_amount =Convert.ToString(DataConverter.CDecimal(total_fee) / 100);
        }
        else
        {
            message.Append("在线支付失败！");
        }
        if (payResult)
        {
            PayOnline payOnline = new PayOnline();
            PayOnlineState payOnlineState = payOnline.UpdateOrder(v_oid, DataConverter.CDecimal(v_amount), v_pstring, 3, v_pmode, true, true);
            if (payOnlineState == PayOnlineState.Ok)
            {
                message.Append(payOnline.Message);
            }
            else
            {
                message.Append(PayOnline.GetStateDescription(payOnlineState));
            }
        }
       LblMsg.Text = message.ToString();
       EasyOne.Shop.PayOnline.TestLog(false, "财付通", pay_result, md5_sign.ToUpper(), md5string.ToUpper());
   }
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>财付通</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
         <pe:ExtendedLabel HtmlEncode="false" ID="LblMsg" runat="server"></pe:ExtendedLabel>
    </div>
    </form>
</body>
</html>
