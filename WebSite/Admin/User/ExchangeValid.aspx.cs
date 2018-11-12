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

    public partial class ExchangeValid : AdminPage
    {

        private static DateTime? CalculationDate(double days, int validNum, UserInfo userInfo)
        {
            if ((days - validNum) > 0.0)
            {
                userInfo.EndTime = new DateTime?(userInfo.EndTime.Value.AddDays((double) validNum));
            }
            else
            {
                userInfo.EndTime = new DateTime?(DateTime.MaxValue);
            }
            return userInfo.EndTime;
        }

        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            int num5;
            int userId = DataConverter.CLng(this.HdnUsersId.Value);
            decimal balance = DataConverter.CDecimal(this.TxtMoney.Text);
            int months = DataConverter.CLng(this.TxtValidNum.Text.Trim());
            int validUnit = DataConverter.CLng(this.DropValidUnit.SelectedValue);
            if (this.RadValidType.Checked)
            {
                switch (validUnit)
                {
                    case 1:
                        num5 = months;
                        goto Label_0094;

                    case 2:
                        num5 = months * 30;
                        goto Label_0094;

                    case 3:
                        num5 = months * 0x16d;
                        goto Label_0094;
                }
                num5 = months;
            }
            else
            {
                num5 = 0x270f;
            }
        Label_0094:
            if (this.RadValidType.Checked && ((months <= 0) || (months > 0x270f)))
            {
                AdminPage.WriteErrMsg("<li>指定期限必须为数字并且必须大于0小于9999！</li>");
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
            if (this.RadValidType.Checked)
            {
                if (!userById.EndTime.HasValue)
                {
                    switch (validUnit)
                    {
                        case 1:
                            userById.EndTime = new DateTime?(DateTime.Now.AddDays((double) months));
                            goto Label_019B;

                        case 2:
                            userById.EndTime = new DateTime?(DateTime.Now.AddMonths(months));
                            goto Label_019B;

                        case 3:
                            userById.EndTime = new DateTime?(DateTime.Now.AddYears(months));
                            goto Label_019B;
                    }
                }
                else
                {
                    SetValid(months, validUnit, userById);
                }
            }
            else
            {
                userById.EndTime = new DateTime?(DateTime.MaxValue);
            }
        Label_019B:
            userById.Balance -= balance;
            if (Users.Update(userById))
            {
                this.SaveBankrollItemInfo(balance, userById);
                this.SaveUserValid(balance, months, userById);
                StringBuilder builder = new StringBuilder();
                builder.Append("<li>会员有效期兑换成功！</li>");
                if (this.ChkIsSendMessage.Checked)
                {
                    string sendContent = SiteConfig.SmsConfig.ExchangePeriodMessage.Replace("{$Money}", balance.ToString());
                    if (this.RadValidType2.Checked)
                    {
                        sendContent = sendContent.Replace("{$Valid}", "无限期");
                    }
                    else
                    {
                        sendContent = sendContent.Replace("{$Valid}", num5.ToString());
                    }
                    builder.Append(Users.SendMessageToUser(userById, sendContent));
                }
                AdminPage.WriteSuccessMsg(builder.ToString(), "UserShow.aspx?UserID=" + userId.ToString());
            }
            else
            {
                AdminPage.WriteErrMsg("<li>会员有效期兑换失败！</li>");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.HdnUsersId.Value = BasePage.RequestInt32("UserID").ToString();
            }
        }

        private void SaveBankrollItemInfo(decimal balance, UserInfo userInfo)
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
            bankrollItemInfo.Money = -(balance);
            bankrollItemInfo.Remark = string.Concat(new object[] { "消费", balance, "资金，有效期截至日期为", userInfo.EndTime.Value.ToString() });
            bankrollItemInfo.Status = BankrollItemStatus.Confirm;
            BankrollItem.Add(bankrollItemInfo);
        }

        private void SaveUserValid(decimal balance, int validNum, UserInfo userInfo)
        {
            UserValidLogInfo userValidLogInfo = new UserValidLogInfo();
            userValidLogInfo.Inputer = PEContext.Current.Admin.AdminName;
            userValidLogInfo.IP = PEContext.Current.UserHostAddress;
            userValidLogInfo.UserName = userInfo.UserName;
            userValidLogInfo.LogTime = DateTime.Now;
            userValidLogInfo.Remark = string.Concat(new object[] { "消费", balance, "资金，有效期截至日期为", userInfo.EndTime.Value.ToString() });
            userValidLogInfo.IncomePayout = 1;
            userValidLogInfo.ValidNum = validNum;
            UserValidLog.Add(userValidLogInfo);
        }

        private static void SetValid(int validNum, int validUnit, UserInfo userInfo)
        {
            TimeSpan span = (TimeSpan) (DateTime.MaxValue - userInfo.EndTime.Value);
            switch (validUnit)
            {
                case 1:
                    userInfo.EndTime = CalculationDate(span.TotalDays, validNum, userInfo);
                    return;

                case 2:
                    userInfo.EndTime = CalculationDate(span.TotalDays * 30.0, validNum, userInfo);
                    return;

                case 3:
                    userInfo.EndTime = CalculationDate(span.TotalDays * 365.0, validNum, userInfo);
                    return;
            }
        }
    }
}

