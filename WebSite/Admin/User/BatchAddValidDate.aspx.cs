namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Web.UI.WebControls;

    public partial class BatchAddValidDate : AdminPage
    {

        private void CheckUserInput()
        {
            switch (this.SelectUser.UserType)
            {
                case 1:
                    if (string.IsNullOrEmpty(this.SelectUser.GroupId))
                    {
                        AdminPage.WriteErrMsg("<li>请选择会员组！</li>");
                    }
                    break;

                case 2:
                    if (string.IsNullOrEmpty(this.SelectUser.UserId))
                    {
                        AdminPage.WriteErrMsg("<li>请指定会员ID！</li>");
                    }
                    break;
            }
            if (this.RadValidType.Checked && string.IsNullOrEmpty(this.TxtValidNum.Text))
            {
                AdminPage.WriteErrMsg("<li>请输入指定期限ID！</li>");
            }
            if (string.IsNullOrEmpty(this.TxtReason.Text))
            {
                AdminPage.WriteErrMsg("<li>请输入原因ID！</li>");
            }
        }

        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            int num3;
            string str3;
            if (!this.Page.IsValid)
            {
                return;
            }
            this.CheckUserInput();
            int num = DataConverter.CLng(this.TxtValidNum.Text.Trim());
            int num2 = DataConverter.CLng(this.DropValidUnit.SelectedValue);
            string reason = this.TxtReason.Text.Trim();
            bool isRecord = this.ChkSaveItem.Checked;
            string text = this.TxtMemo.Text;
            if (this.RadValidType.Checked)
            {
                switch (num2)
                {
                    case 1:
                        num3 = num;
                        goto Label_00B2;

                    case 2:
                        num3 = num * 30;
                        goto Label_00B2;

                    case 3:
                        num3 = num * 0x16d;
                        goto Label_00B2;
                }
                num3 = num;
            }
            else
            {
                num3 = 0x270f;
            }
        Label_00B2:
            str3 = "批量添加有效期成功！";
            string str4 = "批量添加有效期失败";
            string strA = this.ViewState["Action"] as string;
            if (string.Compare(strA, "SubtractValidDate", StringComparison.OrdinalIgnoreCase) == 0)
            {
                num3 = -num3;
                str3 = "批量扣除有效期成功！";
                str4 = "批量扣除有效期失败！";
            }
            IEncourageStrategy<int> strategy = new UserDate();
            bool flag2 = false;
            switch (this.SelectUser.UserType)
            {
                case 0:
                    flag2 = strategy.IncreaseForAll(num3, reason, isRecord, text);
                    break;

                case 1:
                    flag2 = strategy.IncreaseForGroup(this.SelectUser.GroupId, num3, reason, isRecord, text);
                    break;

                case 2:
                    flag2 = strategy.IncreaseForUsers(this.SelectUser.UserId, num3, reason, isRecord, text);
                    break;
            }
            if (flag2)
            {
                AdminPage.WriteSuccessMsg("<li>" + str3 + "</li>", "UserManage.aspx");
            }
            else
            {
                AdminPage.WriteErrMsg("<li>" + str4 + "</li>", "UserManage.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (string.Compare(BasePage.RequestString("Action"), "AddValidDate", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    this.Lbltype.Text = "奖励";
                    this.LblMsg.Text = "批量添加用户有效期";
                    this.ViewState["Action"] = "AddValidDate";
                    this.LabValidDescription.Text = "&nbsp;&nbsp;&nbsp;&nbsp;若目前会员尚未到期，则追加相应天数<br>&nbsp;&nbsp;&nbsp;&nbsp;若目前会员已经过了有效期，则有效期从续费之日起重新计数。<br>";
                }
                else
                {
                    this.Lbltype.Text = "扣除";
                    this.LblMsg.Text = "批量扣除用户有效期";
                    this.ViewState["Action"] = "SubtractValidDate";
                    this.LabValidDescription.Text = "";
                    this.RadValidType2.Text = "有效期归零";
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

