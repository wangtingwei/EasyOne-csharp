namespace EasyOne.WebSite.User.Info
{
    using EasyOne.Controls;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class ValidLogDetail : DynamicPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            int logId = BasePage.RequestInt32("LogID");
            if (!this.Page.IsPostBack)
            {
                UserValidLogInfo validLogByIdAndUserName = UserValidLog.GetValidLogByIdAndUserName(logId);
                if (!validLogByIdAndUserName.IsNull)
                {
                    this.LblLogTime.Text = validLogByIdAndUserName.LogTime.ToString();
                    this.LblIP.Text = validLogByIdAndUserName.IP;
                    this.LblIncomePayOut.Text = validLogByIdAndUserName.ValidNum.ToString() + " 天";
                    this.LblRemark.Text = validLogByIdAndUserName.Remark;
                }
                else
                {
                    DynamicPage.WriteErrMsg("<li>找不到对应记录！</li>");
                }
            }
        }
    }
}

