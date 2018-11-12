namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Model.Accessories;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using EasyOne.Model.UserManage;

    public partial class CardModify : AdminPage
    {

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (base.IsValid)
            {
                CardInfo info = this.ViewState["CardInfo"] as CardInfo;
                if (!string.IsNullOrEmpty(info.UserName))
                {
                    AdminPage.WriteErrMsg("<li>此充值卡已经被使用，不能再修改！</li>");
                }
                else if (info.OrderItemId > 0)
                {
                    AdminPage.WriteErrMsg("<li>此充值卡已经售出，不能再修改！</li>");
                }
                else
                {
                    info.Password = StringHelper.Base64StringEncode(this.TxtPassword.Text.Trim());
                    info.Money = DataConverter.CDecimal(this.TxtMoney.Text);
                    info.ValidNum = (this.DropValidUnit.SelectedValue == "5") ? DataConverter.CLng(this.DropUserGroup.SelectedValue) : DataConverter.CLng(this.TxtValidNum.Text);
                    info.ValidUnit = DataConverter.CLng(this.DropValidUnit.SelectedValue);
                    info.EndDate = this.DpkEnd.Date;
                    info.AgentName = this.TxtAgentName.Text;
                    if (Cards.Update(info))
                    {
                        BasePage.ResponseRedirect("CardsManage.aspx");
                    }
                    else
                    {
                        AdminPage.WriteErrMsg("<li>充值卡修改失败，请认真检查输入的数据是否有效！</li>");
                    }
                }
            }
        }

        private void InitControls()
        {
            int cardId = BasePage.RequestInt32("CardId");
            if (cardId > 0)
            {
                CardInfo cardById = Cards.GetCardById(cardId);
                this.ViewState.Add("CardInfo", cardById);
                this.TxtCardNum.Text = cardById.CardNum;
                this.TdProductName.InnerHtml = Cards.GetProductName(cardById.ProductName);
                this.TxtPassword.Text = StringHelper.Base64StringDecode(cardById.Password);
                this.TxtMoney.Text = cardById.Money.ToString("N2");
                this.TxtValidNum.Text = cardById.ValidNum.ToString();
                this.DropValidUnit.Items.FindByValue(cardById.ValidUnit.ToString()).Selected = true;
                this.DpkEnd.Text = cardById.EndDate.ToString("yyyy-MM-dd");
                this.TxtAgentName.Text = cardById.AgentName;
                IList<UserGroupsInfo> userGroupList = UserGroups.GetUserGroupList(0, 0);
                this.DropUserGroup.DataSource = userGroupList;
                this.DropUserGroup.DataTextField = "GroupName";
                this.DropUserGroup.DataValueField = "GroupId";
                this.DropUserGroup.DataBind();
                if (cardById.ValidUnit == 5)
                {
                    BasePage.SetSelectedIndexByValue(this.DropUserGroup, cardById.ValidNum.ToString());
                    this.TxtValidNum.Enabled = false;
                }
                this.DropUserGroup.Attributes.Add("onchange", "selectValue()");
                this.DropValidUnit.Attributes.Add("onchange", "selectGroup()");
                if (!string.IsNullOrEmpty(cardById.UserName) || (cardById.OrderItemId > 0))
                {
                    this.BtnSave.Enabled = false;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.InitControls();
            }
        }

        protected void TxtMoney_TextChanged(object sender, EventArgs e)
        {
        }
    }
}

