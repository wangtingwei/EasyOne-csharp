namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Model.Accessories;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class Banks : AdminPage
    {

        private string m_action;

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                BankInfo bankInfo = new BankInfo();
                if (this.m_action == "Modify")
                {
                    bankInfo.BankId = BasePage.RequestInt32("ID");
                    bankInfo.OrderId = DataConverter.CLng(this.ViewState["orderId"]);
                }
                bankInfo.BankShortName = this.TxtBankShortName.Text;
                bankInfo.Accounts = this.TxtAccounts.Text;
                bankInfo.BankIntro = this.TxtBankIntro.Text;
                bankInfo.BankName = this.TxtBankName.Text;
                bankInfo.BankPic = this.TxtBankPic.Text;
                bankInfo.CardNum = this.TxtCardNum.Text;
                bankInfo.HolderName = this.TxtHolderName.Text;
                bankInfo.IsDefault = this.ChkIsDefault.Checked;
                bankInfo.IsDisabled = false;
                bool flag = false;
                if (this.m_action == "Modify")
                {
                    flag = Bank.Update(bankInfo);
                }
                else if (Bank.Exists(bankInfo.BankShortName))
                {
                    AdminPage.WriteErrMsg("已存在相同的账户名！");
                }
                else
                {
                    flag = Bank.Add(bankInfo);
                }
                if (flag)
                {
                    AdminPage.WriteSuccessMsg("保存数据成功！", "BankManage.aspx");
                }
                else
                {
                    AdminPage.WriteErrMsg("保存数据失败！");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.m_action = BasePage.RequestString("Action", "Add");
            if (!this.Page.IsPostBack && (this.m_action == "Modify"))
            {
                BankInfo bankById = Bank.GetBankById(BasePage.RequestInt32("ID"));
                this.TxtBankShortName.Text = bankById.BankShortName;
                this.TxtAccounts.Text = bankById.Accounts;
                this.TxtBankIntro.Text = bankById.BankIntro;
                this.TxtBankName.Text = bankById.BankName;
                this.TxtBankPic.Text = bankById.BankPic;
                this.TxtCardNum.Text = bankById.CardNum;
                this.TxtHolderName.Text = bankById.HolderName;
                this.ChkIsDefault.Checked = bankById.IsDefault;
                this.ViewState["orderId"] = bankById.OrderId;
                if (bankById.IsDefault)
                {
                    this.ChkIsDefault.Checked = true;
                }
                this.TxtBankShortName.Enabled = false;
            }
        }

        protected void ValxAccounts_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (string.IsNullOrEmpty(this.TxtAccounts.Text) && string.IsNullOrEmpty(this.TxtCardNum.Text))
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }
    }
}

