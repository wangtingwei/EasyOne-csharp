namespace EasyOne.WebSite.User.Info
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Model.Accessories;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class Recharge : DynamicPage
    {
        

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            double totalDays;
            int validNum;
            IEncourageStrategy<int> strategy2;
            if (!base.IsValid)
            {
                return;
            }
            string password = StringHelper.Base64StringEncode(this.TxtPassword.Text);
            CardInfo cardByNumAndPassword = Cards.GetCardByNumAndPassword(this.TxtCardNum.Text.Trim(), password);
            if (cardByNumAndPassword.IsNull)
            {
                DynamicPage.WriteErrMsg("卡号或密码错误！");
            }
            else
            {
                if (cardByNumAndPassword.CardType != 0)
                {
                    DynamicPage.WriteErrMsg("你输入的充值卡是其他公司的卡，不能在本站进行充值。请尽快去有关公司或网站的充值入口进行充值。");
                }
                if (!string.IsNullOrEmpty(cardByNumAndPassword.UserName))
                {
                    DynamicPage.WriteErrMsg("你输入的充值卡已经使用过了！");
                }
                if (cardByNumAndPassword.EndDate < DateTime.Today)
                {
                    DynamicPage.WriteErrMsg("你输入的充值卡已经失效！此卡的充值截止日期为：" + cardByNumAndPassword.EndDate.ToString("yyyy-MM-dd"));
                }
            }
            UserInfo userInfo = this.showUserInfo.UserInfo;
            bool flag = false;
            string groupName = string.Empty;
            StringBuilder builder = new StringBuilder();
            builder.Append("<li>充值成功！</li>");
            if (cardByNumAndPassword.ValidUnit == 5)
            {
                groupName = UserGroups.GetUserGroupById(cardByNumAndPassword.ValidNum).GroupName;
                builder.Append("<li><span style='Color:#F00'>恭喜您已升级成\"" + groupName + "\"</span></li>");
            }
            builder.Append("<li>充值卡卡号：" + cardByNumAndPassword.CardNum + "</li>");
            builder.Append("<li>充值卡面值：" + cardByNumAndPassword.Money.ToString("N2") + "元</li>");
            builder.Append("<li>充值截止日期：" + cardByNumAndPassword.EndDate.ToString("yyyy-MM-dd") + "</li>");
            switch (cardByNumAndPassword.ValidUnit)
            {
                case 0:
                {
                    builder.Append("<li>充值卡点数：" + cardByNumAndPassword.ValidNum.ToString() + Cards.GetValidUnitType(cardByNumAndPassword.ValidUnit) + "</li>");
                    builder.Append("<li>您充值前的" + SiteConfig.UserConfig.PointName + "数：" + userInfo.UserPoint.ToString() + "</li>");
                    IEncourageStrategy<int> strategy = new UserPoint();
                    flag = strategy.IncreaseForUsers(userInfo.UserId.ToString(), cardByNumAndPassword.ValidNum, "充值卡充值。卡号：" + cardByNumAndPassword.CardNum, true, "");
                    string[] strArray2 = new string[] { "<li>您充值后的", SiteConfig.UserConfig.PointName, "数：", (userInfo.UserPoint + cardByNumAndPassword.ValidNum).ToString(), "</li>" };
                    builder.Append(string.Concat(strArray2));
                    goto Label_059A;
                }
                case 1:
                case 2:
                case 3:
                {
                    builder.Append("<li>充值卡内含有效期：" + cardByNumAndPassword.ValidNum.ToString() + Cards.GetValidUnitType(cardByNumAndPassword.ValidUnit) + "</li>");
                    TimeSpan span = (TimeSpan) (DataConverter.CDate(userInfo.EndTime).Date - DateTime.Today.Date);
                    totalDays = span.TotalDays;
                    validNum = cardByNumAndPassword.ValidNum;
                    strategy2 = new UserDate();
                    switch (cardByNumAndPassword.ValidUnit)
                    {
                        case 2:
                            validNum *= 30;
                            break;

                        case 3:
                            validNum *= 0x16d;
                            break;
                    }
                    break;
                }
                case 4:
                {
                    builder.Append("<li>充值卡内含资金：" + cardByNumAndPassword.ValidNum.ToString() + Cards.GetValidUnitType(cardByNumAndPassword.ValidUnit) + "</li>");
                    builder.Append("<li>您充值前的资金余额为：" + userInfo.Balance.ToString("N2") + "元</li>");
                    IEncourageStrategy<decimal> strategy3 = new UserMoney();
                    flag = strategy3.IncreaseForUsers(userInfo.UserId.ToString(), cardByNumAndPassword.ValidNum, "充值卡充值。卡号：" + cardByNumAndPassword.CardNum, true, "");
                    builder.Append("<li>您充值后的资金余额为：" + ((userInfo.Balance + cardByNumAndPassword.ValidNum)).ToString("N2") + "元</li>");
                    goto Label_059A;
                }
                case 5:
                    builder.Append("<li>会员级别：" + groupName + "</li>");
                    flag = Users.MoveByUserName(userInfo.UserName, cardByNumAndPassword.ValidNum);
                    goto Label_059A;

                default:
                    goto Label_059A;
            }
            if (totalDays > 0.0)
            {
                builder.Append("<li>您充值前的有效期：" + totalDays.ToString() + "天</li>");
                flag = strategy2.IncreaseForUsers(userInfo.UserId.ToString(), validNum, "充值卡充值。卡号：" + cardByNumAndPassword.CardNum, true, "");
                builder.Append("<li>您充值后的有效期：" + ((totalDays + validNum)).ToString() + "天</li>");
            }
            else
            {
                builder.Append("<li>您充值前有效期已经过期" + Math.Abs(totalDays).ToString() + "天</li>");
                flag = strategy2.IncreaseForUsers(userInfo.UserId.ToString(), validNum, "充值卡充值。卡号：" + cardByNumAndPassword.CardNum, true, "");
                builder.Append("<li>您充值后的有效期：" + validNum.ToString() + "天，开始计算日期：" + DateTime.Today.ToString("yyyy-MM-dd") + "</li>");
            }
        Label_059A:
            if (flag)
            {
                cardByNumAndPassword.UserName = userInfo.UserName;
                cardByNumAndPassword.UseTime = new DateTime?(DateTime.Now);
                Cards.Update(cardByNumAndPassword);
                DynamicPage.WriteSuccessMsg(builder.ToString(), "Recharge.aspx");
            }
            else
            {
                DynamicPage.WriteErrMsg("充值失败！");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.showUserInfo.UserInfo = Users.GetUsersByUserName(PEContext.Current.User.UserName);
            }
        }
    }
}

