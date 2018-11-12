namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class LogSearch : AdminPage
    {

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect(BasePage.RequestString("Action") + ".aspx?SearchType=11&KeyWord=" + this.GetKeywords());
        }

        private string GetKeywords()
        {
            int num = DataConverter.CLng(this.TxtBeginId.Text);
            int num2 = DataConverter.CLng(this.TxtEndId.Text);
            string str = this.DpkBeginDate.Date.ToString("yyyy-MM-dd");
            string str2 = this.DpkEndDate.Date.ToString("yyyy-MM-dd");
            string str3 = this.DpkLogTime.Date.ToString("yyyy-MM-dd");
            if ((str == DateTime.Now.ToString("yyyy-MM-dd")) && (str2 == DateTime.Now.ToString("yyyy-MM-dd")))
            {
                str = "";
                str2 = "";
                if (str3 == DateTime.Now.ToString("yyyy-MM-dd"))
                {
                    str3 = "";
                }
            }
            else if (str3 == DateTime.Now.ToString("yyyy-MM-dd"))
            {
                str3 = "";
            }
            string str4 = this.TxtUserName.Text.Trim().Replace("|", "");
            string str5 = this.TxtInputer.Text.Trim().Replace("|", "");
            string str6 = this.TxtIP.Text.Trim().Replace("|", "");
            return string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}", new object[] { num.ToString(), num2.ToString(), str, str2, str4, str3, str5, str6 });
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}

