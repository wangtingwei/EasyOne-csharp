namespace EasyOne.WebSite.User.Info
{
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Model.UserManage;
    using EasyOne.ModelControls;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class BuyPoint : DynamicPage
    {


        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect(base.FullBasePath + "PayOnline/SelectPayPlatform.aspx?PointAmount=" + this.TxtPointAmount.Text);
        }

        private void InitBuyPoint()
        {
            this.LblPrice.Text = (SiteConfig.UserConfig.MoneyExchangePointByMoney / SiteConfig.UserConfig.MoneyExchangePointByPoint).ToString("0.00");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                UserInfo usersByUserName = Users.GetUsersByUserName(PEContext.Current.User.UserName);
                if (!usersByUserName.UserPurview.EnableBuyPoint)
                {
                    DynamicPage.WriteErrMsg("对不起，您没有购买" + SiteConfig.UserConfig.PointName + "的权限！");
                }
                this.showUserInfo.UserInfo = usersByUserName;
                this.InitBuyPoint();
            }
        }
    }
}

