namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Model.Accessories;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class BankrollItemListSearch : AdminPage
    {

        protected void BtnExportExcel_Click(object sender, EventArgs e)
        {
            IList<BankrollItemInfo> list = BankrollItem.GetList(0, 0x7fffffff, 11, 0, this.GetKeywords());
            StringBuilder builder = new StringBuilder();
            base.Response.Clear();
            base.Response.AppendHeader("content-disposition", "attachment;filename=BankrollItemList.xls");
            base.Response.ContentEncoding = Encoding.GetEncoding("GB2312");
            base.Response.ContentType = "application/vnd.xls";
            builder.Append("<table border='1' cellspacing='1' style='border-collapse: collapse;table-layout:fixed' id='AutoNumber1' height='32'><tr>");
            builder.Append("<td align='center'><b>交易时间</b></td>");
            builder.Append("<td align='center'><b>客户名称</b></td>");
            builder.Append("<td align='center'><b>用户名</b></td>");
            builder.Append("<td align='center'><b>交易方式</b></td>");
            builder.Append("<td align='center'><b>币种</b></td>");
            builder.Append("<td align='center'><b>收入金额</b></td>");
            builder.Append("<td align='center'><b>支出金额</b></td>");
            builder.Append("<td align='center'><b>摘要</b></td>");
            builder.Append("<td align='center'><b>银行名称</b></td>");
            builder.Append("<td align='center'><b>备注/说明</b></td></tr>");
            foreach (BankrollItemInfo info in list)
            {
                builder.Append("<tr>");
                builder.Append("<td align='center'>" + info.DateAndTime.ToString() + "</td>");
                builder.Append("<td align='center'>" + DataSecurity.HtmlEncode(info.ClientName) + "</td>");
                builder.Append("<td align='center'>" + DataSecurity.HtmlEncode(info.UserName) + "</td>");
                builder.Append("<td align='center'>" + this.GetMoneyType(info.MoneyType) + "</td>");
                builder.Append("<td align='center'>" + this.GetCurrencyType(info.CurrencyType) + "</td>");
                builder.Append("<td align='center'>" + ((info.Money > 0M) ? info.Money.ToString() : "") + "</td>");
                builder.Append("<td align='center'>" + ((info.Money < 0M) ? Math.Abs(info.Money).ToString() : "") + "</td>");
                builder.Append("<td align='center'>" + ((info.Money > 0M) ? "收入" : "支出") + "</td>");
                builder.Append("<td align='center'>" + ((info.MoneyType == 3) ? DataSecurity.HtmlEncode(PayPlatform.GetPayPlatformById(info.EBankId).PayPlatformName) : DataSecurity.HtmlEncode(info.Bank)) + "</td>");
                builder.Append("<td align='center'>" + DataSecurity.HtmlEncode(info.Remark) + "</td>");
                builder.Append("</tr>");
            }
            base.Response.Write(builder.ToString());
            base.Response.End();
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("BankrollItemList.aspx?SearchType=11&KeyWord=" + this.GetKeywords());
        }

        private string GetCurrencyType(int type)
        {
            string[] strArray = new string[] { "", "人民币", "美元" };
            if (type < strArray.Length)
            {
                return strArray[type];
            }
            return "其它";
        }

        private string GetKeywords()
        {
            string str = this.TxtBeginId.Text.Trim().Replace("|", "");
            string str2 = this.TxtEndId.Text.Trim().Replace("|", "");
            string str3 = this.DpkBegin.Text.Trim().Replace("|", "");
            string str4 = string.IsNullOrEmpty(this.DpkEnd.Text) ? this.DpkEnd.Text.Trim().Replace("|", "") : this.DpkEnd.Date.ToShortDateString().Replace("|", "");
            string str5 = this.TxtClientName.Text.Trim().Replace("|", "");
            string str6 = this.TxtUserName.Text.Trim().Replace("|", "");
            string str7 = this.TxtBank.Text.Trim().Replace("|", "");
            return string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}", new object[] { str, str2, str3, str4, str5, str6, str7 });
        }

        private string GetMoneyType(int type)
        {
            string[] strArray = new string[] { "", "现金", "银行汇款", "在线支付", "虚拟货币" };
            if (type < strArray.Length)
            {
                return strArray[type];
            }
            return "";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}

