namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Model.Accessories;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class PayPlatforms : AdminPage
    {

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            PayPlatformInfo payPlatformInfo = new PayPlatformInfo();
            if (BasePage.RequestString("Action") == "Modify")
            {
                payPlatformInfo.PayPlatformId = BasePage.RequestInt32("ID");
                payPlatformInfo.OrderId = DataConverter.CLng(this.ViewState["orderId"]);
            }
            payPlatformInfo.PayPlatformName = this.TxtPlatformName.Text;
            payPlatformInfo.AccountsId = this.TxtAccountsID.Text;
            payPlatformInfo.MD5 = this.TxtMD5.Text;
            payPlatformInfo.Rate = DataConverter.CDouble(this.TxtRate.Text);
            payPlatformInfo.IsDisabled = this.ChkIsDisabled.Checked;
            payPlatformInfo.IsDefault = this.ChkIsDefault.Checked;
            bool flag = false;
            if (BasePage.RequestString("Action") == "Modify")
            {
                flag = PayPlatform.Update(payPlatformInfo);
            }
            else
            {
                flag = PayPlatform.Add(payPlatformInfo);
            }
            if (flag)
            {
                AdminPage.WriteSuccessMsg("保存数据成功！", "PayPlatformManage.aspx");
            }
            else
            {
                AdminPage.WriteErrMsg("保存数据失败！");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.ChkIsDefault.Attributes.Add("onchange", "DefalutChange()");
                if (BasePage.RequestString("Action") == "Modify")
                {
                    PayPlatformInfo payPlatformById = PayPlatform.GetPayPlatformById(BasePage.RequestInt32("ID"));
                    this.TxtPlatformName.Text = payPlatformById.PayPlatformName;
                    this.TxtAccountsID.Text = payPlatformById.AccountsId;
                    this.TxtMD5.Text = payPlatformById.MD5;
                    this.TxtRate.Text = payPlatformById.Rate.ToString();
                    this.ViewState["orderId"] = payPlatformById.OrderId;
                    if (payPlatformById.IsDisabled)
                    {
                        this.ChkIsDisabled.Checked = true;
                    }
                    if (payPlatformById.IsDefault)
                    {
                        this.ChkIsDefault.Checked = true;
                        this.ChkIsDisabled.Disabled = true;
                    }
                }
            }
        }
    }
}

