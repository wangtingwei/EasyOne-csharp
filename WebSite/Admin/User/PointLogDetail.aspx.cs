namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.Controls;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class PointLogDetail : AdminPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            int logId = BasePage.RequestInt32("LogID");
            if (!this.Page.IsPostBack)
            {
                UserPointLogInfo pointLogById = UserPointLog.GetPointLogById(logId);
                if (!pointLogById.IsNull)
                {
                    this.LblLogTime.Text = pointLogById.LogTime.ToString();
                    this.LblUserName.Text = pointLogById.UserName;
                    this.LblIP.Text = pointLogById.IP;
                    if (pointLogById.IncomePayOut == 1)
                    {
                        this.LblIncomePayOut.Text = "收入 " + pointLogById.Point.ToString();
                    }
                    else
                    {
                        this.LblIncomePayOut.Text = "支出 " + pointLogById.Point.ToString();
                    }
                    this.LblTimes.Text = pointLogById.Times.ToString();
                    this.LblInputer.Text = pointLogById.Inputer;
                    this.LblRemark.Text = pointLogById.Remark;
                    this.LblMemo.Text = pointLogById.Memo;
                }
                else
                {
                    AdminPage.WriteErrMsg("<li>找不到对应记录！</li>");
                }
            }
        }
    }
}

