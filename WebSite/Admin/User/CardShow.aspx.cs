namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Model.Accessories;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.HtmlControls;

    public partial class CardShow : AdminPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.ShowCard();
            }
        }

        private void ShowCard()
        {
            int cardId = BasePage.RequestInt32("CardId");
            if (cardId > 0)
            {
                CardInfo cardById = Cards.GetCardById(cardId);
                this.TdCardType.InnerHtml = Cards.GetCardType(cardById.CardType);
                this.TdProductName.InnerHtml = Cards.GetProductName(cardById.ProductName);
                this.TdCardNum.InnerHtml = cardById.CardNum;
                this.TdPassword.InnerHtml = StringHelper.Base64StringDecode(cardById.Password);
                this.TdMoney.InnerHtml = cardById.Money.ToString("N2");
                this.TdValidNum.InnerHtml = cardById.ValidNum.ToString() + Cards.GetValidUnitType(cardById.ValidUnit);
                this.TdEndDate.InnerHtml = cardById.EndDate.ToString("yyyy-MM-dd");
                this.TdCreateTime.InnerHtml = cardById.CreateTime.ToString();
                this.TdStatus.InnerHtml = Cards.GetCardStatus(cardById);
                this.TdUserName.InnerHtml = cardById.UserName;
                this.TdUseTime.InnerHtml = !cardById.UseTime.HasValue ? "" : cardById.UseTime.ToString();
                this.TdAgentName.InnerHtml = cardById.AgentName;
            }
        }
    }
}

