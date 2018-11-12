namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.Accessories;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.Accessories;
    using EasyOne.Model.Shop;
    using EasyOne.Model.UserManage;
    using EasyOne.Shop;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class PaymentLogManage : AdminPage
    {

        private void AddBankroll(int paymentLogId)
        {
            if (!BankrollItem.ExistsPaymentLog(paymentLogId))
            {
                PaymentLogInfo paymentLogById = PaymentLog.GetPaymentLogById(paymentLogId);
                if (!paymentLogById.IsNull)
                {
                    IEncourageStrategy<decimal> strategy = new UserMoney();
                    int userId = 0;
                    UserInfo usersByUserName = new UserInfo(true);
                    if (!string.IsNullOrEmpty(paymentLogById.UserName))
                    {
                        usersByUserName = Users.GetUsersByUserName(paymentLogById.UserName);
                        userId = usersByUserName.UserId;
                        if (userId > 0)
                        {
                            strategy.IncreaseForUsers(userId.ToString(), paymentLogById.MoneyPay, "", false, "");
                            BankrollItemInfo bankrollItemInfo = new BankrollItemInfo();
                            bankrollItemInfo.UserName = paymentLogById.UserName;
                            bankrollItemInfo.Money = paymentLogById.MoneyPay;
                            bankrollItemInfo.MoneyType = 3;
                            bankrollItemInfo.EBankId = paymentLogById.PlatformId;
                            bankrollItemInfo.OrderId = paymentLogById.OrderId;
                            bankrollItemInfo.PaymentId = paymentLogId;
                            bankrollItemInfo.Remark = "在线支付单号：" + paymentLogById.PaymentNum;
                            bankrollItemInfo.DateAndTime = DateTime.Now;
                            bankrollItemInfo.CurrencyType = 1;
                            bankrollItemInfo.ClientId = usersByUserName.ClientId;
                            BankrollItem.Add(bankrollItemInfo);
                        }
                    }
                    if (paymentLogById.OrderId > 0)
                    {
                        OrderInfo orderById = Order.GetOrderById(paymentLogById.OrderId);
                        if (!orderById.IsNull)
                        {
                            decimal d = 0M;
                            if (orderById.MoneyTotal > orderById.MoneyReceipt)
                            {
                                if ((orderById.MoneyTotal - orderById.MoneyReceipt) > paymentLogById.MoneyPay)
                                {
                                    if (SiteConfig.ShopConfig.EnablePartPay)
                                    {
                                        d = paymentLogById.MoneyPay;
                                        orderById.MoneyReceipt += paymentLogById.MoneyPay;
                                    }
                                }
                                else
                                {
                                    d = orderById.MoneyTotal - orderById.MoneyReceipt;
                                    orderById.MoneyReceipt = orderById.MoneyTotal;
                                }
                                orderById.Status = OrderStatus.Confirmed;
                                Order.Update(orderById);
                            }
                            if (d > 0M)
                            {
                                strategy.IncreaseForUsers(userId.ToString(), -d, "", false, "");
                                BankrollItemInfo info5 = new BankrollItemInfo();
                                info5.UserName = usersByUserName.UserName;
                                info5.ClientId = usersByUserName.ClientId;
                                info5.Money = -d;
                                info5.MoneyType = 4;
                                info5.EBankId = 0;
                                info5.OrderId = orderById.OrderId;
                                info5.PaymentId = 0;
                                info5.Remark = "支付订单费用，订单号：" + orderById.OrderNum;
                                info5.DateAndTime = DateTime.Now;
                                info5.CurrencyType = 1;
                                BankrollItem.Add(info5);
                            }
                        }
                    }
                    else if (paymentLogById.Point > 0)
                    {
                        IEncourageStrategy<int> strategy2 = new UserPoint();
                        strategy2.IncreaseForUsers(userId.ToString(), paymentLogById.Point, "购买" + SiteConfig.UserConfig.PointName, true, "");
                        BankrollItemInfo info6 = new BankrollItemInfo();
                        info6.UserName = usersByUserName.UserName;
                        info6.ClientId = usersByUserName.ClientId;
                        info6.Money = (paymentLogById.MoneyPay > 0M) ? (-1M * paymentLogById.MoneyPay) : paymentLogById.MoneyPay;
                        info6.MoneyType = 4;
                        info6.EBankId = 0;
                        info6.OrderId = 0;
                        info6.PaymentId = 0;
                        info6.Remark = "购买" + SiteConfig.UserConfig.PointName + "，购买数：" + paymentLogById.Point.ToString() + SiteConfig.UserConfig.PointUnit;
                        info6.DateAndTime = new DateTime?(DateTime.Now);
                        info6.CurrencyType = 1;
                        BankrollItem.Add(info6);
                        if (usersByUserName.UserId > 0)
                        {
                            strategy.IncreaseForUsers(usersByUserName.UserId.ToString(), -(paymentLogById.MoneyPay), "", false, "");
                        }
                    }
                }
            }
        }

        protected void BtnBatchDelete_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            switch (this.RadlDatepartType.SelectedValue)
            {
                case "0":
                    now = DateTime.Now.AddDays(-10.0);
                    break;

                case "1":
                    now = DateTime.Now.AddMonths(-1);
                    break;

                case "2":
                    now = DateTime.Now.AddMonths(-2);
                    break;

                case "3":
                    now = DateTime.Now.AddMonths(-3);
                    break;

                case "4":
                    now = DateTime.Now.AddMonths(-6);
                    break;

                case "5":
                    now = DateTime.Now.AddYears(-1);
                    break;
            }
            if (PaymentLog.Delete(now))
            {
                AdminPage.WriteSuccessMsg("批量删除记录成功！", "PaymentLogManage.aspx");
            }
            else
            {
                AdminPage.WriteErrMsg("<li>已经没有要删除的支付记录！</li>");
            }
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            StringBuilder selectList = new StringBuilder("");
            selectList = this.GdvPaymentLogList.SelectList;
            if (selectList.Length == 0)
            {
                AdminPage.WriteErrMsg("<li>对不起，您还没选择要删除的支付记录！</li>", "PaymentLogManage.aspx");
            }
            else if (PaymentLog.Delete(selectList.ToString()))
            {
                BasePage.ResponseRedirect("PaymentLogManage.aspx");
            }
            else
            {
                AdminPage.WriteErrMsg("<li>删除失败！！</li>", "PaymentLogManage.aspx");
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
        }

        protected void GdvPaymentLogList_RowCommand(object sender, CommandEventArgs e)
        {
            if (string.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                AdminPage.WriteErrMsg("<li>请指定支付单ID！</li>", "PaymentLogManage.aspx");
            }
            else
            {
                string str;
                if (((str = e.CommandName) != null) && (str == "Status"))
                {
                    int paymentLogId = Convert.ToInt32(e.CommandArgument, null);
                    switch (PaymentLog.Update(paymentLogId))
                    {
                        case 0:
                            AdminPage.WriteErrMsg("<li>找不到指定的支付单！</li>", "PaymentLogManage.aspx");
                            return;

                        case 1:
                            AdminPage.WriteSuccessMsg("<li>此支付单已经提交给银行！</li>", "PaymentLogManage.aspx");
                            return;

                        case 2:
                            this.AddBankroll(paymentLogId);
                            AdminPage.WriteSuccessMsg("<li>在线支付成功！</li>", "PaymentLogManage.aspx");
                            return;
                    }
                }
            }
        }

        protected void GdvPaymentLogList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                PaymentLogInfo dataItem = e.Row.DataItem as PaymentLogInfo;
                if (dataItem != null)
                {
                    Label label = (Label) e.Row.FindControl("LblStatus");
                    Label label2 = (Label) e.Row.FindControl("LblPlatform");
                    label.Text = PaymentLog.GetStatusDepict(dataItem.PlatformId, dataItem.Status);
                    if (dataItem.Status != 1)
                    {
                        e.Row.Cells[0].Enabled = false;
                    }
                    label2.Text = PayPlatform.GetPayPlatformById(dataItem.PlatformId).PayPlatformName;
                }
            }
        }
    }
}

