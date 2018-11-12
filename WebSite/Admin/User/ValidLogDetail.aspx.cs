namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.Controls;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class ValidLogDetail : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int logId = BasePage.RequestInt32("LogID");
            if (!this.Page.IsPostBack)
            {
                UserValidLogInfo validLogById = UserValidLog.GetValidLogById(logId);
                if (!validLogById.IsNull)
                {
                    this.LblLogTime.Text = validLogById.LogTime.ToString();
                    this.LblUserName.Text = validLogById.UserName;
                    this.LblIP.Text = validLogById.IP;
                    this.LblIncomePayOut.Text = validLogById.ValidNum.ToString() + " 天";
                    this.LblInputer.Text = validLogById.Inputer;
                    this.LblRemark.Text = validLogById.Remark;
                    this.LblMemo.Text = validLogById.Memo;
                }
                else
                {
                    AdminPage.WriteErrMsg("<li>找不到对应记录！</li>");
                }
            }
        }
    }
}

