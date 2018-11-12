<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
  protected void Page_Load(object sender, EventArgs e)
  {
      string payOnlineShopID;
      string PayOnlineKey;

      int payPlatformId = 3;  //上海环迅
      EasyOne.Model.Accessories.PayPlatformInfo payPlatform = EasyOne.Accessories.PayPlatform.GetPayPlatformById(payPlatformId);
      
      payOnlineShopID = payPlatform.AccountsId;
      PayOnlineKey = payPlatform.MD5;
      
      string billno = Request.QueryString["billno"];
      string currency_type = Request.QueryString["currency_type"];
      string amount = Request.QueryString["amount"];
      string mydate = Request.QueryString["date"];
      string succ = Request.QueryString["succ"];
      //string msg = Request.QueryString["msg"];
      string attach = Request.QueryString["attach"];
      string ipsbillno = Request.QueryString["ipsbillno"];  
      string retEncodeType = Request.QueryString["retencodetype"];
      string signature = Request.QueryString["signature"].ToLower();
      
      bool paySuccess = false;
      string md5 = "";
      if(succ == "Y")
      {
          StringBuilder md5string = new StringBuilder();
          md5string.Append(billno);
          md5string.Append(amount); 
          md5string.Append(mydate); 
          md5string.Append(succ); 
          md5string.Append(ipsbillno); 
          md5string.Append(currency_type); 
          md5string.Append(PayOnlineKey); 
          md5 = EasyOne.Common.StringHelper.MD5(md5string.ToString()).ToLower();
          if(md5 != signature)
          {
               LblMsg.Text = "签名不正确!";
          }
          else
          {
              paySuccess = true;
          }
          if(paySuccess)
          {
              string v_oid = billno;

              StringBuilder message = new StringBuilder();
              message.Append("<br>恭喜你！在线支付成功！<br><br>");
              EasyOne.Shop.PayOnline payOnline = new EasyOne.Shop.PayOnline();
              EasyOne.Enumerations.PayOnlineState payOnlineState = payOnline.UpdateOrder(v_oid, EasyOne.Common.DataConverter.CDecimal(amount), "", 3, "", true, true);
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
      }
      EasyOne.Shop.PayOnline.TestLog(false, "上海环迅", succ, signature, md5);
      
  }
      

</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>上海环迅</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <pe:ExtendedLabel HtmlEncode="false" ID="LblMsg" runat="server"></pe:ExtendedLabel>
    </div>
    </form>
</body>
</html>
