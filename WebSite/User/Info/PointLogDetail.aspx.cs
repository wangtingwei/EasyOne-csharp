namespace EasyOne.WebSite.User.Info
{
    using EasyOne.Controls;
    using EasyOne.Model.UserManage;
    using EasyOne.ModelControls;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class PointLogDetail : DynamicPage
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            int logId = BasePage.RequestInt32("LogID");
            if (!this.Page.IsPostBack)
            {
                UserPointLogInfo pointLogByIdAndUserName = UserPointLog.GetPointLogByIdAndUserName(logId);
                if (!pointLogByIdAndUserName.IsNull)
                {
                    this.LblLogTime.Text = pointLogByIdAndUserName.LogTime.ToString();
                    this.LblIP.Text = pointLogByIdAndUserName.IP;
                    if (pointLogByIdAndUserName.IncomePayOut == 1)
                    {
                        this.LblIncomePayOut.Text = "收入 " + pointLogByIdAndUserName.Point.ToString();
                    }
                    else
                    {
                        this.LblIncomePayOut.Text = "支出 " + pointLogByIdAndUserName.Point.ToString();
                    }
                    this.LblTimes.Text = pointLogByIdAndUserName.Times.ToString();
                    this.LblRemark.Text = pointLogByIdAndUserName.Remark;
                }
                else
                {
                    DynamicPage.WriteErrMsg("<li>找不到对应记录！</li>");
                }
            }
        }
    }
}

