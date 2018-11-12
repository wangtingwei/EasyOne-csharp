namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.Accessories;
    using EasyOne.Model.Shop;
    using EasyOne.Model.UserManage;
    using EasyOne.Shop;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class AddPayment : AdminPage
    {

        private void AddBankroll(OrderInfo orderInfo, decimal money)
        {
            BankrollItemInfo bankrollItemInfo = new BankrollItemInfo();
            bankrollItemInfo.ClientId = orderInfo.ClientId;
            bankrollItemInfo.UserName = orderInfo.UserName;
            bankrollItemInfo.CurrencyType = 1;
            bankrollItemInfo.MoneyType = 4;
            bankrollItemInfo.Inputer = PEContext.Current.Admin.AdminName;
            bankrollItemInfo.LogTime = new DateTime?(DateTime.Now);
            bankrollItemInfo.DateAndTime = new DateTime?(DateTime.Now);
            bankrollItemInfo.Money = (money > 0M) ? (-1M * money) : money;
            bankrollItemInfo.OrderId = orderInfo.OrderId;
            bankrollItemInfo.Remark = this.TxtRemark.Text.Trim();
            bankrollItemInfo.Memo = this.TxtMemo.Text;
            BankrollItem.Add(bankrollItemInfo);
        }

        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            string input = this.HdnUsersId.Value;
            int orderId = DataConverter.CLng(this.HdnorderId.Value);
            decimal d = DataConverter.CDecimal(this.TxtMoney.Text);
            UserInfo userById = Users.GetUserById(DataConverter.CLng(input));
            if (userById.IsNull)
            {
                AdminPage.WriteErrMsg("<li>找不到指定的会员！</li>", "UserShow.aspx?UserID=" + input);
            }
            else
            {
                OrderInfo orderInfo = new OrderInfo(true);
                if ((userById.Balance + DataConverter.CDecimal(userById.UserPurview.Overdraft)) < d)
                {
                    AdminPage.WriteErrMsg("<li>会员资金余额小于支出金额！</li>", base.Request.UrlReferrer.ToString());
                }
                if (orderId > 0)
                {
                    orderInfo = Order.GetOrderById(orderId);
                    this.ValidateOrder(orderInfo, DataConverter.CLng(input));
                    orderInfo.MoneyReceipt += d;
                    if (orderInfo.Status <= OrderStatus.WaitForConfirm)
                    {
                        orderInfo.Status = OrderStatus.Confirmed;
                    }
                    if (!Order.UserPayment(orderInfo.OrderId, orderInfo.MoneyReceipt, orderInfo.Status))
                    {
                        AdminPage.WriteErrMsg("<li>给订单添加付款失败！</li>", "../Shop/OrderManage.aspx?OrderID=" + orderId);
                    }
                }
                IEncourageStrategy<decimal> strategy = new UserMoney();
                bool isRecord = true;
                if (orderId > 0)
                {
                    isRecord = false;
                }
                if (strategy.IncreaseForUsers(input,-(d), this.TxtRemark.Text.Trim(), isRecord, this.TxtMemo.Text))
                {
                    if (orderId > 0)
                    {
                        this.AddBankroll(orderInfo, d);
                        AdminPage.WriteSuccessMsg("<li>给订单添加付款成功！</li>", "../Shop/OrderManage.aspx?OrderID=" + orderId);
                    }
                    else
                    {
                        StringBuilder builder = new StringBuilder();
                        builder.Append("<li>给会员添加支出成功！</li>");
                        if (this.ChkIsSendMessage.Checked)
                        {
                            string sendContent = SiteConfig.SmsConfig.PayoutLogMessage.Replace("{$Money}", Math.Abs(d).ToString()).Replace("{$Reason}", this.TxtRemark.Text);
                            builder.Append(Users.SendMessageToUser(userById, sendContent));
                        }
                        AdminPage.WriteSuccessMsg(builder.ToString(), "UserShow.aspx?UserID=" + input);
                    }
                }
                else
                {
                    AdminPage.WriteErrMsg("<li>给会员添加支出失败！</li>");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                int userId = BasePage.RequestInt32("UserID");
                string str = BasePage.RequestString("UserName");
                int orderId = BasePage.RequestInt32("OrderID");
                UserInfo userById = new UserInfo(true);
                if (string.IsNullOrEmpty(str))
                {
                    userById = Users.GetUserById(userId);
                }
                if (userId <= 0)
                {
                    userById = Users.GetUsersByUserName(str);
                }
                if (userById.IsNull)
                {
                    AdminPage.WriteErrMsg("<li>找不到指定的会员！</li>", "UserShow.aspx?UserID=" + userId);
                }
                else
                {
                    this.LblUserName.Text = userById.UserName.ToString();
                    this.LblBalance.Text = userById.Balance.ToString("C");
                    this.HdnUsersId.Value = userById.UserId.ToString();
                    this.HdnorderId.Value = orderId.ToString();
                    userId = userById.UserId;
                    if (orderId > 0)
                    {
                        this.PnlPayment.Visible = true;
                        OrderInfo orderById = Order.GetOrderById(orderId);
                        decimal balance = orderById.MoneyTotal - orderById.MoneyReceipt;
                        decimal num4 = DataConverter.CDecimal(userById.UserPurview.Overdraft);
                        if ((userById.Balance + num4) <= 0M)
                        {
                            AdminPage.WriteErrMsg("您的资金余额不足！不能使用虚拟货币支付。");
                        }
                        this.ValidateOrder(orderById, userId);
                        if (!orderById.IsNull)
                        {
                            this.LblOrderFormNum.Text = orderById.OrderNum;
                            this.LblMoneyTotal.Text = orderById.MoneyTotal.ToString("N2");
                            this.LblMoneyReceipt.Text = orderById.MoneyReceipt.ToString("N2");
                        }
                        if (userById.Balance <= balance)
                        {
                            balance = userById.Balance;
                        }
                        this.TxtMoney.Text = (balance > 0M) ? balance.ToString("0.00") : orderById.MoneyTotal.ToString("0.00");
                        this.TxtRemark.Text = "支付订单费用。订单号：" + orderById.OrderNum;
                    }
                }
            }
        }

        private void ValidateOrder(OrderInfo orderInfo, int userId)
        {
            if (orderInfo == null)
            {
                AdminPage.WriteErrMsg("<li>找不到指定的订单！</li>", "UserShow.aspx?UserID=" + userId.ToString());
            }
            else if (orderInfo.MoneyTotal <= orderInfo.MoneyReceipt)
            {
                AdminPage.WriteErrMsg("<li>此订单已经付清，无需再支付！</li>", "UserShow.aspx?UserID=" + userId.ToString());
            }
        }
    }
}

