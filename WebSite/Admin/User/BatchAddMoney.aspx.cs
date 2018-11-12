namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Web.UI.WebControls;

    public partial class BatchAddMoney : AdminPage
    {

        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            decimal d = DataConverter.CDecimal(this.TxtMoney.Text);
            string str = this.TxtReason.Text.Trim();
            bool isRecord = this.ChkSaveItem.Checked;
            bool flag2 = false;
            string str2 = "批量发奖金成功！";
            string str3 = "批量发奖金失败！";
            string text = this.TxtMemo.Text;
            if (string.IsNullOrEmpty(str))
            {
                AdminPage.WriteErrMsg("<li>请输入原因！</li>");
            }
            string strA = this.ViewState["Action"] as string;
            if (string.CompareOrdinal(strA, "SubtractMoney") == 0)
            {
                d = -(d);
                str2 = "批量扣奖金成功！";
                str3 = "批量扣奖金失败！";
            }
            IEncourageStrategy<decimal> strategy = new UserMoney();
            switch (this.SelectUser.UserType)
            {
                case 0:
                    flag2 = strategy.IncreaseForAll(d, str, isRecord, text);
                    break;

                case 1:
                    if (string.IsNullOrEmpty(this.SelectUser.GroupId))
                    {
                        AdminPage.WriteErrMsg("<li>请选择会员组！</li>");
                    }
                    flag2 = strategy.IncreaseForGroup(this.SelectUser.GroupId, d, str, isRecord, text);
                    break;

                case 2:
                    if (string.IsNullOrEmpty(this.SelectUser.UserId))
                    {
                        AdminPage.WriteErrMsg("<li>请指定会员！</li>");
                    }
                    flag2 = strategy.IncreaseForUsers(this.SelectUser.UserId, d, str, isRecord, text);
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
            if (!this.Page.IsPostBack)
            {
                if (string.Compare(BasePage.RequestString("Action"), "SubtractMoeny", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    this.LblMsg.Text = "批量扣除奖金";
                    this.ViewState["Action"] = "SubtractMoney";
                }
                else
                {
                    this.LblMsg.Text = "批量添加奖金";
                    this.ViewState["Action"] = "AddMoney";
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

