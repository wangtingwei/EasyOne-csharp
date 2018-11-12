namespace EasyOne.WebSite.Admin.Analytics
{
    using EasyOne.Analytics;
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using System;
    using System.Web;
    using System.Web.UI.WebControls;

    public partial class StatInit : AdminPage
    {

        protected void BtnInit_Click(object sender, EventArgs e)
        {
            if (Counter.DoInit())
            {
                HttpCookie cookie = base.Response.Cookies["VisitNum"];
                cookie.Expires = DateTime.MinValue;
                base.Response.Cookies.Add(cookie);
                AdminPage.WriteSuccessMsg("数据初始化成功！");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}

