namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.Accessories;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class ExchangePoint : AdminPage
    {
        protected string m_PointName = SiteConfig.UserConfig.PointName;

        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            int userId = DataConverter.CLng(this.HdnUsersId.Value);
            int point = DataConverter.CLng(this.TxtPoint.Text);
            decimal balance = DataConverter.CDecimal(this.TxtMoney.Text);
            if ((point <= 0) || (point > 0x5f5e100))
            {
                AdminPage.WriteErrMsg("<li>请输入" + this.m_PointName + "，" + this.m_PointName + "不能为负数，数字不能大过1亿！</li>");
            }
            if (balance <= 0M)
            {
                AdminPage.WriteErrMsg("<li>减去资金不能小于等于零！</li>");
            }
            UserInfo userById = Users.GetUserById(userId);
            if (userById.IsNull)
            {
                AdminPage.WriteErrMsg("<li>该会员不存在！</li>");
            }
            if (userById.Balance < balance)
            {
                AdminPage.WriteErrMsg("<li>该会员的余额不足此次兑换！</li>");
            }
            userById.UserPoint += point;
            userById.Balance -= balance;
            if (Users.Update(userById))
            {
                this.SaveUserPointLog(point, balance, userById);
                this.SaveBankrollItemInfo(point, balance, userById);
                StringBuilder builder = new StringBuilder();
                builder.Append("<li>会员" + this.m_PointName + "兑换成功！</li>");
                if (this.ChkIsSendMessage.Checked)
                {
                    string sendContent = SiteConfig.SmsConfig.ExchangePointMessage.Replace("{$Point}", point.ToString()).Replace("{$Money}", balance.ToString());
                    builder.Append(Users.SendMessageToUser(userById, sendContent));
                }
                AdminPage.WriteSuccessMsg(builder.ToString(), "UserShow.aspx?UserID=" + userById.UserId);
            }
            else
            {
                AdminPage.WriteErrMsg("<li>会员" + this.m_PointName + "兑换失败！</li>");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.LblTitle.Text = " 会员" + this.m_PointName + "兑换 ";
            this.ValrPoint.ErrorMessage = "增加" + this.m_PointName + "不能为空";
            this.ValgTxtPoint.ErrorMessage = this.m_PointName + "范围在1—100000之间";
            if (!base.IsPostBack)
            {
                this.HdnUsersId.Value = BasePage.RequestInt32("UserID").ToString();
            }
        }

        private void SaveBankrollItemInfo(int point, decimal balance, UserInfo userInfo)
        {
            BankrollItemInfo bankrollItemInfo = new BankrollItemInfo();
            bankrollItemInfo.UserName = userInfo.UserName;
            bankrollItemInfo.MoneyType = 4;
            bankrollItemInfo.CurrencyType = 3;
            bankrollItemInfo.DateAndTime = new DateTime?(DateTime.Now);
            bankrollItemInfo.Inputer = PEContext.Current.Admin.AdminName;
            bankrollItemInfo.Bank = "";
            bankrollItemInfo.ClientName = "";
            bankrollItemInfo.IP = PEContext.Current.UserHostAddress;
            bankrollItemInfo.LogTime = new DateTime?(DateTime.Now);
            bankrollItemInfo.Money = -balance;
            bankrollItemInfo.Remark = string.Concat(new object[] { "用于", this.m_PointName, "兑换，消费", balance, "资金，增加", this.m_PointName, point, SiteConfig.UserConfig.PointUnit });
            bankrollItemInfo.Status = BankrollItemStatus.Confirm;
            BankrollItem.Add(bankrollItemInfo);
        }

        private void SaveUserPointLog(int point, decimal balance, UserInfo userInfo)
        {
            UserPointLogInfo userPointLogInfo = new UserPointLogInfo();
            userPointLogInfo.UserName = userInfo.UserName;
            userPointLogInfo.IncomePayOut = 1;
            userPointLogInfo.LogTime = DateTime.Now;
            userPointLogInfo.IP = PEContext.Current.UserHostAddress;
            userPointLogInfo.Inputer = PEContext.Current.Admin.AdminName;
            userPointLogInfo.Point = point;
            userPointLogInfo.Remark = string.Concat(new object[] { "用于", this.m_PointName, "兑换，消费", balance, "资金，增加", this.m_PointName, point, SiteConfig.UserConfig.PointUnit });
            UserPointLog.Add(userPointLogInfo);
        }
    }
}

