namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.Controls;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Web.UI.WebControls;

    public partial class ValidLog : AdminPage
    {

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            string str = "";
            switch (this.RadlDatepartType.SelectedValue)
            {
                case "0":
                    now = DateTime.Now.AddDays(-10.0);
                    str = "10天前";
                    break;

                case "1":
                    now = DateTime.Now.AddMonths(-1);
                    str = "1个月前";
                    break;

                case "2":
                    now = DateTime.Now.AddMonths(-2);
                    str = "2个月前";
                    break;

                case "3":
                    now = DateTime.Now.AddMonths(-3);
                    str = "3个月前";
                    break;

                case "4":
                    now = DateTime.Now.AddMonths(-6);
                    str = "6个月前";
                    break;

                case "5":
                    now = DateTime.Now.AddYears(-1);
                    str = "1年前";
                    break;
            }
            if (UserValidLog.Delete(now))
            {
                AdminPage.WriteSuccessMsg("<li>删除有效期记录成功！</li>", "validlog.aspx");
            }
            else
            {
                AdminPage.WriteErrMsg("<li>已经没有" + str + "的有效期记录！</li>");
            }
        }

        public string GetIncomePayout(int consumeType, int validNum)
        {
            string str2 = validNum.ToString() + "天";
            switch (consumeType)
            {
                case 1:
                    return ("<span style='color:blue'> +" + str2 + "</span>");

                case 2:
                    return ("<span style='color:red'> " + str2 + "</span>");
            }
            return "<span style='color:#000000'>其他</span>";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}

