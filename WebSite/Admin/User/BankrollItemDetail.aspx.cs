namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.Accessories;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Crm;
    using EasyOne.Enumerations;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.Accessories;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class BankrollItemDetail : AdminPage
    {

        protected void BtnConfirm_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect(base.BasePath + SiteConfig.SiteOption.ManageDir + "/Shop/RemittanceAdd.aspx?Action=Confirm&ItemID=" + BasePage.RequestInt32("BankrollItemID").ToString());
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            if (BankrollItem.Delete(BasePage.RequestInt32("BankrollItemID")))
            {
                AdminPage.WriteSuccessMsg("记录删除成功！", "BankrollItemList.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                BankrollItemInfo bankrollItemById = BankrollItem.GetBankrollItemById(BasePage.RequestInt32("BankrollItemID"));
                if (!bankrollItemById.IsNull)
                {
                    this.LblDateAndTime.Text = bankrollItemById.DateAndTime.ToString();
                    this.LblClientName.Text = Client.GetClientNameById(bankrollItemById.ClientId);
                    this.LblUserName.Text = bankrollItemById.UserName;
                    this.LblMoneyType.Text = BankrollItem.GetMoneyType(bankrollItemById.MoneyType);
                    this.LblCurrencyType.Text = BankrollItem.GetCurrencyType(bankrollItemById.CurrencyType);
                    if (bankrollItemById.Money > 0M)
                    {
                        this.LblIncomeMoney.Text = bankrollItemById.Money.ToString("N2");
                    }
                    else if (bankrollItemById.Money < 0M)
                    {
                        this.LblPaymentMoney.Text = Math.Abs(bankrollItemById.Money).ToString("N2");
                    }
                    if (bankrollItemById.MoneyType == 3)
                    {
                        this.LblBank.Text = PayPlatform.GetPayPlatformById(bankrollItemById.EBankId).PayPlatformName;
                    }
                    else
                    {
                        this.LblBank.Text = bankrollItemById.Bank;
                    }
                    if (bankrollItemById.Status == BankrollItemStatus.NoConfirm)
                    {
                        this.LblStatus.Text = "未确认";
                        this.BtnConfirm.Visible = true;
                        this.BtnDelete.Visible = true;
                    }
                    else
                    {
                        this.LblStatus.Text = "已确认";
                    }
                    this.LblIP.Text = bankrollItemById.IP;
                    this.LblInputer.Text = bankrollItemById.Inputer;
                    this.LblLogTime.Text = bankrollItemById.LogTime.ToString();
                    this.LblRemark.Text = bankrollItemById.Remark;
                    this.LblMemo.Text = bankrollItemById.Memo;
                }
                else
                {
                    AdminPage.WriteErrMsg("<li>未找到对应的资金明细记录</li>");
                }
            }
        }
    }
}

