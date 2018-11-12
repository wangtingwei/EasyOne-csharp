namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.ModelControls;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Web.UI.WebControls;
    using EasyOne.Components;

    public partial class BatchAddPoint : AdminPage
    {
        protected string m_PointName = SiteConfig.UserConfig.PointName;

        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            int howMany = DataConverter.CLng(this.TxtPoint.Text);
            string str = this.TxtReason.Text.Trim();
            bool isRecord = this.ChkSaveItem.Checked;
            bool flag2 = false;
            string str2 = "批量发" + this.m_PointName + "成功！";
            string str3 = "批量发" + this.m_PointName + "失败！";
            string text = this.TxtMemo.Text;
            if (string.IsNullOrEmpty(str))
            {
                AdminPage.WriteErrMsg("<li>请输入原因！</li>");
            }
            IEncourageStrategy<int> strategy = new UserPoint();
            string strA = this.ViewState["Action"] as string;
            if (string.Compare(strA, "SubtractPoint", StringComparison.OrdinalIgnoreCase) == 0)
            {
                howMany = -howMany;
                str2 = "批量扣" + this.m_PointName + "成功！";
                str3 = "批量扣" + this.m_PointName + "失败！";
            }
            switch (this.SelectUser.UserType)
            {
                case 0:
                    flag2 = strategy.IncreaseForAll(howMany, str, isRecord, text);
                    break;

                case 1:
                    if (string.IsNullOrEmpty(this.SelectUser.GroupId))
                    {
                        AdminPage.WriteErrMsg("<li>请选择会员组！</li>");
                    }
                    flag2 = strategy.IncreaseForGroup(this.SelectUser.GroupId, howMany, str, isRecord, text);
                    break;

                case 2:
                    if (string.IsNullOrEmpty(this.SelectUser.UserId))
                    {
                        AdminPage.WriteErrMsg("<li>请指定会员ID！</li>");
                    }
                    flag2 = strategy.IncreaseForUsers(this.SelectUser.UserId, howMany, str, isRecord, text);
                    break;
            }
            if (flag2)
            {
                AdminPage.WriteSuccessMsg("<li>" + str2 + "</li>", "UserManage.aspx");
            }
            else
            {
                AdminPage.WriteErrMsg("<li>" + str3 + "</li>");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.ValrPoint.ErrorMessage = "奖励" + this.m_PointName + "不能为空";
            if (!this.Page.IsPostBack)
            {
                if (string.Compare(BasePage.RequestString("Action"), "SubtractPoint", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    this.LblMsg.Text = "批量扣除" + this.m_PointName;
                    this.ViewState["Action"] = "SubtractPoint";
                }
                else
                {
                    this.LblMsg.Text = "批量添加" + this.m_PointName;
                    this.ViewState["Action"] = "AddPoint";
                }
                if (!string.IsNullOrEmpty(BasePage.RequestString("UserID")))
                {
                    this.SelectUser.UserId = BasePage.RequestString("UserID");
                    this.SelectUser.UserType = 2;
                }
            }
        }
    }
}

