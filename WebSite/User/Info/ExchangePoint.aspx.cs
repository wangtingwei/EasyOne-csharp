namespace EasyOne.WebSite.User.Info
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.Accessories;
    using EasyOne.Model.UserManage;
    using EasyOne.ModelControls;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class ExchangePoint : DynamicPage
    {
 
        protected ShowUserInfo showUserInfo;


        protected void BtnSubmitExp_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                UserInfo usersByUserName = Users.GetUsersByUserName(PEContext.Current.User.UserName);
                int howMany = 0;
                int num2 = Convert.ToInt32(this.TxtUserExp.Text);
                if (num2 > usersByUserName.UserExp)
                {
                    DynamicPage.WriteErrMsg("<li>您的积分不足以进行此次兑换！</li>");
                }
                if (SiteConfig.UserConfig.UserExpExchangePoint <= 0.0)
                {
                    DynamicPage.WriteErrMsg("<li>兑换功能关闭，请与管理员联系！</li>", "../Default.aspx");
                }
                if (num2 <= 0)
                {
                    DynamicPage.WriteErrMsg("<li>填写积分必须大于0！</li>");
                }
                howMany = (int) (((double) num2) / SiteConfig.UserConfig.UserExpExchangePoint);
                if (((howMany == 0) || (howMany < 0)) || (howMany > 0x5f5e100))
                {
                    DynamicPage.WriteErrMsg("<li>请输入资金有误或兑换比率有误！</li>");
                }
                if ((usersByUserName.UserExp - num2) < 0)
                {
                    usersByUserName.UserExp = 0;
                }
                else
                {
                    usersByUserName.UserExp -= num2;
                }
                if (Users.Update(usersByUserName))
                {
                    UserPoint point = new UserPoint();
                    string reason = string.Concat(new object[] { PEContext.Current.User.UserName, "用", num2, "积分兑换", howMany, SiteConfig.UserConfig.PointName });
                    if (point.IncreaseForUsers(usersByUserName.UserId.ToString(), howMany, reason, true, ""))
                    {
                        DynamicPage.WriteSuccessMsg("<li>会员" + SiteConfig.UserConfig.PointName + "兑换成功！</li>", "ExchangePoint.aspx");
                    }
                    else
                    {
                        DynamicPage.WriteErrMsg("<li>会员" + SiteConfig.UserConfig.PointName + "兑换失败！</li>");
                    }
                }
            }
        }

        protected void BtnSubmitMoney_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                UserInfo usersByUserName = Users.GetUsersByUserName(PEContext.Current.User.UserName);
                int howMany = 0;
                decimal d = Convert.ToDecimal(this.TxtMoney.Text);
                decimal num3 = DataConverter.CDecimal(usersByUserName.UserPurview.Overdraft);
                if (d > (usersByUserName.Balance + num3))
                {
                    DynamicPage.WriteErrMsg("<li>您的余额不足以进行此次兑换！</li>");
                }
                if (SiteConfig.UserConfig.MoneyExchangePoint <= 0.0)
                {
                    DynamicPage.WriteErrMsg("<li>兑换功能关闭，请与管理员联系！</li>", "../Default.aspx");
                }
                if (d <= 0M)
                {
                    DynamicPage.WriteErrMsg("<li>填写资金必须大于0！</li>");
                }
                howMany = (int) (((double) d) / SiteConfig.UserConfig.MoneyExchangePoint);
                if (((howMany == 0) || (howMany < 0)) || (howMany > 0x5f5e100))
                {
                    DynamicPage.WriteErrMsg("<li>输入资金有误或兑换比率有误！</li>");
                }
                usersByUserName.Balance -= d;
                if (Users.Update(usersByUserName))
                {
                    UserPoint point = new UserPoint();
                    string reason = string.Concat(new object[] { PEContext.Current.User.UserName, "用", d, "元资金兑换", howMany, SiteConfig.UserConfig.PointName });
                    if (point.IncreaseForUsers(usersByUserName.UserId.ToString(), howMany, reason, true, ""))
                    {
                        BankrollItemInfo bankrollItemInfo = new BankrollItemInfo();
                        bankrollItemInfo.UserName = usersByUserName.UserName;
                        bankrollItemInfo.MoneyType = 4;
                        bankrollItemInfo.CurrencyType = 3;
                        bankrollItemInfo.DateAndTime = new DateTime?(DateTime.Now);
                        bankrollItemInfo.Inputer = PEContext.Current.User.UserName;
                        bankrollItemInfo.IP = PEContext.Current.UserHostAddress;
                        bankrollItemInfo.LogTime = new DateTime?(DateTime.Now);
                        bankrollItemInfo.Money = -(d);
                        bankrollItemInfo.Remark = string.Concat(new object[] { "用于", SiteConfig.UserConfig.PointName, "兑换，消费", d, "元资金，增加", SiteConfig.UserConfig.PointName, howMany, SiteConfig.UserConfig.PointUnit });
                        bankrollItemInfo.Status = BankrollItemStatus.Confirm;
                        BankrollItem.Add(bankrollItemInfo);
                        DynamicPage.WriteSuccessMsg("<li>会员" + SiteConfig.UserConfig.PointName + "兑换成功！</li>", "ExchangePoint.aspx");
                    }
                    else
                    {
                        DynamicPage.WriteErrMsg("<li>会员" + SiteConfig.UserConfig.PointName + "兑换失败！</li>");
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.BtnSubmitExp.Style.Add("display", "none");
            this.RadWithMoney.Attributes.Add("onclick", "javascript:" + this.BtnSubmitMoney.ClientID + ".style.display='';" + this.BtnSubmitExp.ClientID + ".style.display='none';");
            this.RadWithUserExp.Attributes.Add("onclick", "javascript:" + this.BtnSubmitExp.ClientID + ".style.display='';" + this.BtnSubmitMoney.ClientID + ".style.display='none';");
            if (!base.IsPostBack)
            {
                if (!Users.GetUsersByUserName(PEContext.Current.User.UserName).UserPurview.EnableExchangePoint)
                {
                    DynamicPage.WriteErrMsg("对不起，您没有自助兑换点券权限！");
                }
                this.showUserInfo.UserInfo = Users.GetUsersByUserName(PEContext.Current.User.UserName);
                this.LitExMoney.Text = SiteConfig.UserConfig.MoneyExchangePointByMoney.ToString();
                this.LitExUserExp.Text = SiteConfig.UserConfig.UserExpExchangePointByExp.ToString();
                this.LitExRMoney.Text = SiteConfig.UserConfig.MoneyExchangePointByPoint.ToString();
                this.LitExRUserExp.Text = SiteConfig.UserConfig.UserExpExchangePointByPoint.ToString();
            }
        }
    }
}

