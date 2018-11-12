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
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class ExchangeValidDate : DynamicPage
    {

        protected ShowUserInfo showUserInfo;


        protected void BtnExpSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                UserInfo usersByUserName = Users.GetUsersByUserName(PEContext.Current.User.UserName);
                double input = 0.0;
                int num2 = Convert.ToInt32(this.TxtUserExp.Text);
                if (num2 > usersByUserName.UserExp)
                {
                    DynamicPage.WriteErrMsg("<li>您的积分不足以进行此次兑换！</li>");
                }
                if (SiteConfig.UserConfig.UserExpExchangeValidDay <= 0.0)
                {
                    DynamicPage.WriteErrMsg("<li>兑换功能关闭，请与管理员联系！</li>");
                }
                if (num2 <= 0)
                {
                    DynamicPage.WriteErrMsg("<li>填写积分必须大于0！</li>");
                }
                input = ((double) num2) / SiteConfig.UserConfig.UserExpExchangeValidDay;
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
                    UserDate date = new UserDate();
                    string reason = string.Concat(new object[] { PEContext.Current.User.UserName, "用", num2, "积分兑换", input, "天有效期" });
                    if (date.IncreaseForUsers(usersByUserName.UserId.ToString(), DataConverter.CLng(input), reason, true, ""))
                    {
                        DynamicPage.WriteSuccessMsg("<li>会员有效期兑换成功！</li>", "ExchangeValidDate.aspx");
                    }
                    else
                    {
                        DynamicPage.WriteErrMsg("<li>会员有效期兑换失败！</li>");
                    }
                }
            }
        }

        protected void BtnMoneySubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                UserInfo usersByUserName = Users.GetUsersByUserName(PEContext.Current.User.UserName);
                double input = 0.0;
                decimal d = Convert.ToDecimal(this.TxtMoney.Text);
                decimal num3 = DataConverter.CDecimal(usersByUserName.UserPurview.Overdraft);
                if (d > (usersByUserName.Balance + num3))
                {
                    DynamicPage.WriteErrMsg("<li>您的余额不足以进行此次兑换！</li>");
                }
                if (SiteConfig.UserConfig.MoneyExchangeValidDay <= 0.0)
                {
                    DynamicPage.WriteErrMsg("<li>兑换功能关闭，请与管理员联系！</li>");
                }
                if (d <= 0M)
                {
                    DynamicPage.WriteErrMsg("<li>填写资金必须大于0！</li>");
                }
                input = ((double) d) / SiteConfig.UserConfig.MoneyExchangeValidDay;
                if (input <= 0.0)
                {
                    DynamicPage.WriteErrMsg("<li>有效期兑换比率有误！</li>");
                }
                usersByUserName.Balance -= d;
                if (Users.Update(usersByUserName))
                {
                    UserDate date = new UserDate();
                    string reason = string.Concat(new object[] { PEContext.Current.User.UserName, "用", d, "元资金兑换", input, "天有效期" });
                    if (date.IncreaseForUsers(usersByUserName.UserId.ToString(), DataConverter.CLng(input), reason, true, ""))
                    {
                        BankrollItemInfo bankrollItemInfo = new BankrollItemInfo();
                        bankrollItemInfo.Inputer = "System";
                        bankrollItemInfo.UserName = PEContext.Current.User.UserName;
                        bankrollItemInfo.ClientId = usersByUserName.ClientId;
                        bankrollItemInfo.Money = -(d);
                        bankrollItemInfo.MoneyType = 4;
                        bankrollItemInfo.Remark = string.Concat(new object[] { PEContext.Current.User.UserName, "用", d, "元资金兑换", input, "天有效期" });
                        bankrollItemInfo.DateAndTime = new DateTime?(DateTime.Now);
                        BankrollItem.Add(bankrollItemInfo);
                        DynamicPage.WriteSuccessMsg("<li>会员有效期兑换成功！</li>", "ExchangeValidDate.aspx");
                    }
                    else
                    {
                        DynamicPage.WriteErrMsg("<li>会员有效期兑换失败！</li>");
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
                if (!Users.GetUsersByUserName(PEContext.Current.User.UserName).UserPurview.EnableExchangeValidDate)
                {
                    DynamicPage.WriteErrMsg("对不起，您没有自助兑换有效期权限！");
                }
                this.showUserInfo.UserInfo = Users.GetUsersByUserName(PEContext.Current.User.UserName);
                this.LitExMoney.Text = SiteConfig.UserConfig.MoneyExchangeValidDayByMoney.ToString();
                this.LitExUserExp.Text = SiteConfig.UserConfig.UserExpExchangeValidDayByExp.ToString();
                this.LitExRMoney.Text = SiteConfig.UserConfig.MoneyExchangeValidDayByValidDay.ToString();
                this.LitExRUserExp.Text = SiteConfig.UserConfig.UserExpExchangeValidDayByValidDay.ToString();
            }
        }
    }
}

