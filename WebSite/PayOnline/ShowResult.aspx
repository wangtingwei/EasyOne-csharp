<%@ Page Language="C#" AutoEventWireup="true"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>在线支付成功</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <br />
        <pe:ExtendedLabel HtmlEncode="false" ID="LblResult" runat="server" Text="在线支付成功"></pe:ExtendedLabel>
    </div>
    </form>
</body>
</html>
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {            
            if(Request.QueryString["PayMessage"] == "ok")
            {
                LblResult.Text = "在线支付成功！";
                int orderId = 0;
                if (!string.IsNullOrEmpty(Request.QueryString["PaymentNum"]))
                {
                    orderId = EasyOne.Common.DataConverter.CLng(EasyOne.Accessories.PaymentLog.GetInfoByPaymentNum(Request.QueryString["PaymentNum"]).OrderId);
                }
                if (orderId > 0)
                {
                    LblResult.Text = LblResult.Text + "<br><a href='../User/Shop/ShowOrder.aspx?OrderId=" + orderId.ToString() + "'>点此查看订单信息</a>";
                }
                else
                {
                    LblResult.Text = LblResult.Text + "<br><a href='../User/Info/PaymentLog.aspx'>点此查看在线支付记录</a>";
                }
            }
            else
            {
                LblResult.Text = "在线支付失败！";
            }
        }
    }
</script>