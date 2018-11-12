<%@ Page Language="C#" StylesheetTheme="" EnableTheming="false"%>

<script runat="server">
     protected void Page_Load(object sender, EventArgs e)
    {
        string payOnlineShopID;
        string PayOnlineKey;
        int payPlatformId = 1;  //网银在线
        EasyOne.Model.Accessories.PayPlatformInfo payPlatform = EasyOne.Accessories.PayPlatform.GetPayPlatformById(payPlatformId);
        payOnlineShopID = payPlatform.AccountsId;
        PayOnlineKey = payPlatform.MD5;
         
         
         string v_mid, v_oid, v_pmode, v_pstatus, v_pstring, v_amount, v_md5, v_moneytype;

        v_mid = payOnlineShopID;
        v_oid = Request["v_oid"] == null ? "" : Request["v_oid"].Trim();                //支付定单号
        v_md5 = Request["v_md5str"] == null ? "" : Request["v_md5str"].Trim();           //数字签名
        v_amount = Request["v_amount"] == null ? "" : Request["v_amount"].Trim();         //支付金额
        v_pstatus = Request["v_pstatus"] == null ? "" : Request["v_pstatus"].Trim();       //支付状态
        v_moneytype = Request["v_moneytype"] == null ? "" : Request["v_moneytype"].Trim();   //支付币种
        v_pmode = Request["v_pmode"] == null ? "" : Request["v_pmode"].Trim();           //支付银行
        v_pstring = Request["v_pstring"] == null ? "" : Request["v_pstring"].Trim(); ;       //支付结果说明

        string md5string = EasyOne.Common.StringHelper.MD5(v_oid + v_pstatus + v_amount + v_moneytype + PayOnlineKey);

        if (((md5string.ToUpper()) == (v_md5.ToUpper())) && v_pstatus == "20")
        {
            Response.Write("ok");
            try
            {
                EasyOne.Shop.PayOnline payOnline = new EasyOne.Shop.PayOnline();
                payOnline.UpdateOrder(v_oid, EasyOne.Common.DataConverter.CDecimal(v_amount), v_pstring, 3, v_pmode, true, true);
            }
            catch
            { }
        }
        else
        {
            Response.Write("error");
        }
        EasyOne.Shop.PayOnline.TestLog(false, "网银在线自动接口", v_pstatus, v_md5.ToUpper(), md5string.ToUpper());
    }
</script>


