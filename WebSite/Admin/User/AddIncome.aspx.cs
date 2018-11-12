namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Model.Accessories;
    using EasyOne.Model.UserManage;
    using EasyOne.ModelControls;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class AddIncome : AdminPage
    {

        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                decimal num = DataConverter.CDecimal(this.TxtMoney.Text);
                UserInfo userInfo = this.ShowUserInfo.UserInfo;
                if (userInfo == null)
                {
                    AdminPage.WriteErrMsg("<li>找不到指定的会员！</li>");
                }
                else
                {
                    if (string.IsNullOrEmpty(this.DropBankShortName.SelectedValue))
                    {
                        AdminPage.WriteErrMsg("<li>请选择汇入银行！如果没有请到系统设置下的银行账户管理中添加银行账户 ！</li>", "UserShow.aspx?UserID=" + userInfo.UserId);
                    }
                    userInfo.Balance += num;
                    if (Users.Update(userInfo))
                    {
                        BankrollItemInfo bankrollItemInfo = new BankrollItemInfo();
                        bankrollItemInfo.UserName = userInfo.UserName;
                        bankrollItemInfo.ClientId = userInfo.ClientId;
                        bankrollItemInfo.DateAndTime = new DateTime?(this.DpkReceipt.Date);
                        bankrollItemInfo.Money = num;
                        bankrollItemInfo.MoneyType = 2;
                        bankrollItemInfo.CurrencyType = 1;
                        bankrollItemInfo.Bank = this.DropBankShortName.SelectedItem.Text;
                        bankrollItemInfo.EBankId = 0;
                        bankrollItemInfo.PaymentId = 0;
                        bankrollItemInfo.OrderId = 0;
                        bankrollItemInfo.Remark = this.TxtRemark.Text.Trim();
                        bankrollItemInfo.LogTime = new DateTime?(DateTime.Now);
                        bankrollItemInfo.Inputer = PEContext.Current.Admin.AdminName;
                        bankrollItemInfo.IP = PEContext.Current.UserHostAddress;
                        bankrollItemInfo.Memo = this.TxtMemo.Text;
                        if (this.ChkPoint.Checked && !string.IsNullOrEmpty(userInfo.UserName))
                        {
                            int infoPoint = DataConverter.CLng(this.TxtPoint.Text);
                            if (Users.AddPoint(infoPoint, userInfo.UserName))
                            {
                                UserPointLogInfo userPointLogInfo = new UserPointLogInfo();
                                userPointLogInfo.Inputer = PEContext.Current.Admin.AdminName;
                                userPointLogInfo.IP = PEContext.Current.UserHostAddress;
                                userPointLogInfo.UserName = userInfo.UserName;
                                userPointLogInfo.LogTime = DateTime.Now;
                                userPointLogInfo.Remark = "银行汇款赠送" + SiteConfig.UserConfig.PointName;
                                userPointLogInfo.Memo = "";
                                userPointLogInfo.IncomePayOut = 1;
                                userPointLogInfo.Point = infoPoint;
                                UserPointLog.Add(userPointLogInfo);
                            }
                        }
                        if (BankrollItem.Add(bankrollItemInfo))
                        {
                            StringBuilder builder = new StringBuilder();
                            builder.Append("<li>给会员添加银行汇款记录成功！</li>");
                            if (this.ChkIsSendMessage.Checked)
                            {
                                string sendContent = SiteConfig.SmsConfig.BankLogMessage.Replace("{$Money}", Math.Abs(bankrollItemInfo.Money).ToString()).Replace("{$BankName}", bankrollItemInfo.Bank).Replace("{$ReceiptDate}", this.DpkReceipt.Date.ToString());
                                builder.Append(Users.SendMessageToUser(userInfo, sendContent));
                            }
                            AdminPage.WriteSuccessMsg(builder.ToString(), "UserShow.aspx?UserID=" + userInfo.UserId);
                        }
                        else
                        {
                            AdminPage.WriteErrMsg("<li>向资金明细表中添加收入记录失败！</li>", "UserShow.aspx?UserID=" + userInfo.UserId);
                        }
                    }
                    else
                    {
                        AdminPage.WriteErrMsg("<li>给会员添加银行汇款记录失败！</li>");
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.ShowBankShortName();
            }
        }

        private void ShowBankShortName()
        {
            IList<BankInfo> listByEnabled = Bank.GetListByEnabled();
            this.DropBankShortName.DataSource = listByEnabled;
            this.DropBankShortName.DataTextField = "BankShortName";
            this.DropBankShortName.DataValueField = "BankID";
            this.DropBankShortName.DataBind();
        }
    }
}

