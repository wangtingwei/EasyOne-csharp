namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.AccessManage;
    using EasyOne.Accessories;
    using EasyOne.Api;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Crm;
    using EasyOne.Enumerations;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.Accessories;
    using EasyOne.Model.Crm;
    using EasyOne.Model.Shop;
    using EasyOne.Model.UserManage;
    using EasyOne.ModelControls;
    using EasyOne.Shop;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Collections;
    using System.Data;
    using System.Web;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class UserShow : AdminPage
    {
        private int m_ClientId;
        private decimal m_CurrentPageIncome;
        private decimal m_CurrentPagePayout;
        private decimal m_MoneyReceipt;
        private decimal m_MoneyTotal;
        protected string m_PointName = SiteConfig.UserConfig.PointName;
        private bool m_ShowCompanyInfo;
        private decimal m_TotalIncome;
        private decimal m_TotalPayout;
        private int userId;
        private string userName;

        protected void BtnAddPoint_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("AddPoint.aspx?UserID=" + this.userId);
        }

        protected void BtnAddValidDate_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("AddValidDate.aspx?UserID=" + this.userId);
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            if (ApiData.IsAPiEnable())
            {
                if (!DataValidator.IsValidId(this.userId.ToString()))
                {
                    AdminPage.WriteErrMsg("<li>删除失败！</li>");
                }
                string str2 = ApiFunction.DeleteUsers(Users.GetUserById(this.userId).UserName);
                if (str2 != "true")
                {
                    AdminPage.WriteErrMsg("<li>" + str2 + "</li><br><li>删除失败！</li>");
                }
            }
            if (Users.Delete(this.userId.ToString()))
            {
                BasePage.ResponseRedirect("UserManage.aspx");
            }
            else
            {
                AdminPage.WriteErrMsg("<li>删除会员失败！</li>");
            }
        }

        protected void BtnExchangePoint_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("ExchangePoint.aspx?UserID=" + this.userId);
        }

        protected void BtnExchangeValid_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("ExchangeValid.aspx?UserID=" + this.userId);
        }

        protected void BtnIncome_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("AddIncome.aspx?UserID=" + this.userId);
        }

        protected void BtnLock_Click(object sender, EventArgs e)
        {
            if (this.HdnLockType.Value == "1")
            {
                Users.BatchLock(this.userId.ToString());
                AdminPage.WriteSuccessMsg("<li>操作锁定会员成功！</li>", "UserShow.aspx?UserID=" + this.userId);
            }
            else
            {
                Users.BatchUnlock(this.userId.ToString());
                AdminPage.WriteSuccessMsg("<li>操作解锁会员成功！</li>", "UserShow.aspx?UserID=" + this.userId);
            }
        }

        protected void BtnMessage_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("../Accessories/MessageSend.aspx?UserName=" + HttpUtility.UrlEncode(this.LblUserName.Text));
        }

        protected void BtnMinusPoint_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("MinusPoint.aspx?UserID=" + this.userId);
        }

        protected void BtnMinusValidDate_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("MinusValidDate.aspx?UserID=" + this.userId);
        }

        protected void BtnModifyPurview_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("ModifyPurview.aspx?UserID=" + this.userId);
        }

        protected void BtnModifyUserSubmit_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("User.aspx?Action=Modify&UserID=" + this.userId.ToString());
        }

        protected void BtnOrderAdd_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("../Shop/OrderAdd.aspx?UserName=" + this.userName);
        }

        protected void BtnPayment_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("AddPayment.aspx?UserID=" + this.userId);
        }

        protected void BtnToClient_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("../Crm/ClientShow.aspx?ClientId=" + this.m_ClientId);
        }

        private void CheckeShop(UserInfo userInfo)
        {
            GroupType groupType = UserGroups.GetUserGroupById(userInfo.GroupId).GroupType;
            if (string.Compare(SiteConfig.SiteInfo.ProductEdition, "eShop", StringComparison.OrdinalIgnoreCase) == 0)
            {
                this.BtnOrderAdd.Visible = true;
                if (groupType == GroupType.Agent)
                {
                    this.InfoTabTitle5.Visible = true;
                    this.InfoTabTitle6.Visible = true;
                    this.InfoTabTitle7.Visible = true;
                }
            }
        }

        private void CompanySet(UserInfo usersInfo)
        {
            if (usersInfo.CompanyId > 0)
            {
                if (usersInfo.UserType > UserType.Persional)
                {
                    this.m_ShowCompanyInfo = true;
                }
                else
                {
                    this.m_ShowCompanyInfo = false;
                }
                this.EBtnRegCompany.Visible = false;
            }
            else
            {
                this.m_ShowCompanyInfo = false;
            }
            if (usersInfo.ClientId > 0)
            {
                this.EBtnRegClient.Visible = false;
            }
            else
            {
                this.BtnToClient.Visible = false;
            }
            if (this.m_ShowCompanyInfo)
            {
                this.CompanyInfo1.CompanyId = usersInfo.CompanyId;
                this.CompanyMemberManage1.CompanyId = usersInfo.CompanyId;
                this.CompanyMemberManage1.UserType = UserType.Creator;
                this.CompanyMemberManage1.UserId = usersInfo.UserId;
            }
        }

        private static Table CreateTable(string rowOneText, string rowTwoText, string align)
        {
            Table table = new Table();
            table.Width = Unit.Percentage(100.0);
            TableRow row = new TableRow();
            TableCell cell = new TableCell();
            cell.Attributes.Add("align", align);
            row.Cells.Add(cell);
            TableRow row2 = new TableRow();
            TableCell cell2 = new TableCell();
            cell2.Attributes.Add("align", align);
            row2.Cells.Add(cell2);
            table.Rows.Add(row);
            table.Rows.Add(row2);
            table.Rows[0].Cells[0].Text = rowOneText;
            table.Rows[1].Cells[0].Text = rowTwoText;
            return table;
        }

        protected void EBtnRegClient_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("RegClient.aspx?UserID=" + this.userId);
        }

        protected void EBtnRegCompany_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("RegCompany.aspx?UserID=" + this.userId);
        }

        protected void EBtnSendEmail_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect(SiteConfig.SiteInfo.VirtualPath + SiteConfig.SiteOption.ManageDir + "/Accessories/MailListSend.aspx");
        }

        protected void EBtnSendTelMessage_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect(SiteConfig.SiteInfo.VirtualPath + SiteConfig.SiteOption.ManageDir + "/SMS/SmsMessageToUser.aspx?UserName=" + this.userName);
        }

        protected void EgvAgentOrders_DataBound(object sender, EventArgs e)
        {
            if (this.EgvAgentOrders.Rows.Count > 0)
            {
                GridViewRow footerRow = this.EgvAgentOrders.FooterRow;
                ArrayList totalofMoneyAndReceiptByAgentName = Order.GetTotalofMoneyAndReceiptByAgentName(this.userName);
                footerRow.CssClass = this.EgvAgentOrders.RowStyle.CssClass;
                while (footerRow.Cells.Count != 4)
                {
                    footerRow.Cells.RemoveAt(0);
                }
                footerRow.Cells[0].ColumnSpan = 4;
                footerRow.Cells[3].ColumnSpan = 5;
                footerRow.HorizontalAlign = HorizontalAlign.Right;
                footerRow.Cells[0].Text = "本页合计：<br />总计金额：";
                footerRow.Cells[1].Text = this.m_MoneyTotal.ToString("0.00") + "<br />" + DataConverter.CDecimal(totalofMoneyAndReceiptByAgentName[0]).ToString("0.00");
                footerRow.Cells[2].Text = this.m_MoneyReceipt.ToString("0.00") + "<br />" + DataConverter.CDecimal(totalofMoneyAndReceiptByAgentName[1]).ToString("0.00");
                this.m_MoneyReceipt = this.m_MoneyTotal = 0M;
            }
        }

        protected void EgvAgentOrders_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                OrderInfo dataItem = (OrderInfo) e.Row.DataItem;
                Label label = e.Row.FindControl("LblStatus") as Label;
                Label label2 = e.Row.FindControl("LblPaymentStatus") as Label;
                Label label3 = e.Row.FindControl("LblDeliverStatus") as Label;
                label.Text = BasePage.EnumToHtml<OrderStatus>(dataItem.Status);
                label2.Text = BasePage.EnumToHtml<PayStatus>(Order.GetPayStatus(dataItem));
                label3.Text = BasePage.EnumToHtml<DeliverStatus>(dataItem.DeliverStatus);
                this.m_MoneyReceipt += dataItem.MoneyReceipt;
                this.m_MoneyTotal += dataItem.MoneyTotal;
            }
        }

        protected void EgvBankrollItem_DataBound(object sender, EventArgs e)
        {
            if (this.EgvBankrollItem.Rows.Count > 0)
            {
                GridViewRow footerRow = this.EgvBankrollItem.FooterRow;
                while (footerRow.Cells.Count != 4)
                {
                    footerRow.Cells.RemoveAt(0);
                }
                footerRow.CssClass = this.EgvBankrollItem.RowStyle.CssClass;
                ArrayList totalInComeAndPayOutAll = BankrollItem.GetTotalInComeAndPayOutAll(this.userName);
                decimal num = DataConverter.CDecimal(totalInComeAndPayOutAll[0]);
                decimal num2 = DataConverter.CDecimal(totalInComeAndPayOutAll[1]);
                footerRow.HorizontalAlign = HorizontalAlign.Right;
                footerRow.Cells[0].ColumnSpan = 3;
                footerRow.Cells[3].ColumnSpan = 3;
                footerRow.Cells[0].Text = "本页合计：<br/>总计金额：";
                footerRow.Cells[1].Text = this.m_CurrentPageIncome.ToString("N2") + "<br/>" + num.ToString("N2");
                footerRow.Cells[2].Text = Math.Abs(this.m_CurrentPagePayout).ToString("N2") + "<br/>" + Math.Abs(num2).ToString("N2");
                footerRow.Cells[3].HorizontalAlign = HorizontalAlign.Center;
                footerRow.Cells[3].Text = "&nbsp;<br />资金余额：" + ((num + num2)).ToString("N2");
                this.m_CurrentPagePayout = this.m_CurrentPageIncome = 0M;
            }
        }

        protected void EgvBankrollItem_RowCommand(object sender, CommandEventArgs e)
        {
            if ((e.CommandName == "Confirm") && BankrollItem.Confirm(DataConverter.CLng(e.CommandArgument), BankrollItemStatus.Confirm))
            {
                this.EgvBankrollItem.DataBind();
            }
        }

        protected void EgvBankrollItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                BankrollItemInfo dataItem = e.Row.DataItem as BankrollItemInfo;
                if (dataItem != null)
                {
                    if (dataItem.Status == BankrollItemStatus.Confirm)
                    {
                        if (dataItem.Money > 0M)
                        {
                            this.m_CurrentPageIncome += dataItem.Money;
                        }
                        else
                        {
                            this.m_CurrentPagePayout += dataItem.Money;
                        }
                    }
                    Label label = e.Row.FindControl("LblRemark") as Label;
                    if (label != null)
                    {
                        dataItem.Remark = dataItem.Remark;
                        label.Text = StringHelper.SubString(dataItem.Remark, 40, "...");
                        label.ToolTip = dataItem.Remark;
                    }
                }
            }
        }

        protected void EgvBill_OnDataBound(object sender, EventArgs e)
        {
            if (this.EgvBill.Rows.Count > 0)
            {
                GridViewRow footerRow = this.EgvBill.FooterRow;
                while (footerRow.Cells.Count != 4)
                {
                    footerRow.Cells.RemoveAt(0);
                }
                footerRow.HorizontalAlign = HorizontalAlign.Right;
                footerRow.Cells[0].ColumnSpan = 2;
                ArrayList totalInComeAndPayOutAll = BankrollItem.GetTotalInComeAndPayOutAll(this.userName);
                double num = DataConverter.CDouble(totalInComeAndPayOutAll[0]);
                double num2 = DataConverter.CDouble(totalInComeAndPayOutAll[1]);
                footerRow.Cells[0].Text = "本页合计：<br/>总计金额：";
                footerRow.Cells[1].Text = this.m_TotalIncome.ToString("0.00") + "<br />" + num.ToString("0.00");
                footerRow.Cells[2].Text = Math.Abs(this.m_TotalPayout).ToString("0.00") + "<br />" + Math.Abs(num2).ToString("0.00");
                footerRow.Cells[3].Text = "<br />资金余额：" + ((num + num2)).ToString("0.00");
                this.m_TotalIncome = this.m_TotalPayout = 0M;
            }
        }

        protected void EgvBill_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dataItem = e.Row.DataItem as DataRowView;
                decimal num = (decimal) dataItem["Money"];
                if (num > 0M)
                {
                    this.m_TotalIncome += num;
                }
                else
                {
                    this.m_TotalPayout += num;
                }
                Label label = e.Row.FindControl("LblRecieveMoney") as Label;
                Label label2 = e.Row.FindControl("LblPayoutMoney") as Label;
                if ((label != null) && (num > 0M))
                {
                    label.Text = num.ToString("0.00");
                }
                if ((label2 != null) && (num < 0M))
                {
                    label2.Text = Math.Abs(num).ToString("0.00");
                }
            }
        }

        protected void EgvComplain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ComplainItemInfo dataItem = e.Row.DataItem as ComplainItemInfo;
                e.Row.Cells[2].Text = Complain.GetFiledNameById("ComplainType", dataItem.ComplainType);
                e.Row.Cells[4].Text = Complain.GetFiledNameById("MagnitudeOfExigence", dataItem.MagnitudeOfExigence);
                e.Row.Cells[5].Text = Complain.GetStatus(dataItem.Status);
            }
        }

        protected void EgvOrder_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                OrderInfo dataItem = e.Row.DataItem as OrderInfo;
                Label label = (Label) e.Row.FindControl("LblOrderStatus");
                Label label2 = (Label) e.Row.FindControl("LblPayStatus");
                Label label3 = (Label) e.Row.FindControl("LblDeliverStatus");
                label.Text = BasePage.EnumToHtml<OrderStatus>(dataItem.Status);
                label3.Text = BasePage.EnumToHtml<DeliverStatus>(dataItem.DeliverStatus);
                switch (Order.GetPayStatus(dataItem))
                {
                    case PayStatus.WaitForPay:
                        label2.Text = BasePage.EnumToHtml<PayStatus>(PayStatus.WaitForPay);
                        return;

                    case PayStatus.ReceivedEarnest:
                        label2.Text = BasePage.EnumToHtml<PayStatus>(PayStatus.ReceivedEarnest);
                        return;

                    case PayStatus.Payoff:
                        label2.Text = BasePage.EnumToHtml<PayStatus>(PayStatus.Payoff);
                        return;

                    default:
                        return;
                }
            }
        }

        protected void EgvUserPoint_DataBound(object sender, EventArgs e)
        {
            if (this.EgvUserPoint.Rows.Count > 0)
            {
                GridViewRow footerRow = this.EgvUserPoint.FooterRow;
                int num = 3;
                while (footerRow.Cells.Count != num)
                {
                    footerRow.Cells.RemoveAt(0);
                }
                footerRow.CssClass = this.EgvUserPoint.RowStyle.CssClass;
                ArrayList totalInComeAndPayOutAll = UserPointLog.GetTotalInComeAndPayOutAll(this.userName);
                decimal num2 = DataConverter.CDecimal(totalInComeAndPayOutAll[0]);
                decimal num3 = DataConverter.CDecimal(totalInComeAndPayOutAll[1]);
                footerRow.Cells[0].ColumnSpan = 3;
                footerRow.Cells[1].ColumnSpan = 2;
                footerRow.Cells[2].ColumnSpan = 1;
                footerRow.Cells[0].Controls.Add(CreateTable("本页合计", "总计" + this.m_PointName, "right"));
                footerRow.Cells[1].Controls.Add(CreateTable("收入：" + this.m_CurrentPageIncome.ToString() + "&nbsp;&nbsp;支出：" + Math.Abs(this.m_CurrentPagePayout).ToString(), "收入：" + Math.Abs(num2).ToString() + "&nbsp;&nbsp;支出：" + Math.Abs(num3).ToString(), "left"));
                Table child = CreateTable("&nbsp;&nbsp;", string.Format(this.m_PointName + "余额：{0:0}", num2 - num3), "right");
                child.Rows[1].Cells[0].Attributes.Add("align", "center");
                footerRow.Cells[2].Controls.Add(child);
            }
        }

        protected void EgvUserPoint_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                UserPointLogInfo dataItem = (UserPointLogInfo) e.Row.DataItem;
                if (dataItem != null)
                {
                    Label label = (Label) e.Row.FindControl("LblIncomePayOut");
                    label.Text = this.GetIncomePayOut(dataItem.Point, dataItem.IncomePayOut);
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

        protected string GetCurrencyType(object type)
        {
            return BankrollItem.GetCurrencyType(type);
        }

        public string GetIncomePayOut(decimal point, int incomePayOut)
        {
            string str = "";
            if (incomePayOut == 1)
            {
                str = " <span style='color:blue'> +" + point.ToString() + "</span>";
                this.m_CurrentPageIncome += point;
                return str;
            }
            str = " <span style='color:red'> -" + point.ToString() + "</span>";
            this.m_CurrentPagePayout += point;
            return str;
        }

        protected string GetMoneyType(object type)
        {
            return BankrollItem.GetMoneyType(type);
        }

        protected PayPlatformInfo GetPayPlatformById(int payPlatformId)
        {
            return PayPlatform.GetPayPlatformById(payPlatformId);
        }

        public string IncomePayout(int consumeType, int validNum)
        {
            string str2 = validNum.ToString() + "天";
            switch (consumeType)
            {
                case 1:
                    return ("<span style='color:blue'> +" + str2 + "</span>");

                case 2:
                    return ("<span style='color:red'> " + str2 + "</span>");
            }
            return "<span style='color:#000000'>其他</span>";
        }

        private void InitPrivew()
        {
            if (PEContext.Current.Admin.UserName.Equals(BasePage.RequestString("UserName")))
            {
                this.BtnModifyUserSubmit.Enabled = true;
            }
            else
            {
                this.BtnModifyUserSubmit.Enabled = RolePermissions.AccessCheck(OperateCode.UserModify);
            }
            this.BtnAddPoint.Enabled = RolePermissions.AccessCheck(OperateCode.UserPointManage);
            this.BtnAddValidDate.Enabled = RolePermissions.AccessCheck(OperateCode.UserValidDateManage);
            this.BtnDelete.Enabled = RolePermissions.AccessCheck(OperateCode.UserDelete);
            this.BtnLock.Enabled = RolePermissions.AccessCheck(OperateCode.UserLock);
            this.BtnMinusPoint.Enabled = RolePermissions.AccessCheck(OperateCode.UserPointManage);
            this.BtnMinusValidDate.Enabled = RolePermissions.AccessCheck(OperateCode.UserValidDateManage);
        }

        protected string IsShow()
        {
            string str = "";
            if (!this.m_ShowCompanyInfo)
            {
                str = "display:none";
            }
            return str;
        }

        private void LoadContacter(UserInfo usersInfo)
        {
            ContacterInfo contacterByUserName = new ContacterInfo();
            contacterByUserName = Contacter.GetContacterByUserName(usersInfo.UserName);
            if (contacterByUserName != null)
            {
                this.LblTrueName.Text = contacterByUserName.TrueName;
                this.LblTitle.Text = contacterByUserName.Title;
                this.LblCountry.Text = contacterByUserName.Country;
                this.LblProvince.Text = contacterByUserName.Province;
                this.LblCity.Text = contacterByUserName.City;
                this.LblZipCode.Text = contacterByUserName.ZipCode;
                this.LblAddress.Text = contacterByUserName.Address;
                this.LblOfficePhone.Text = contacterByUserName.OfficePhone;
                this.LblHomephone.Text = contacterByUserName.HomePhone;
                this.LblMobile.Text = contacterByUserName.Mobile;
                this.LblFax.Text = contacterByUserName.Fax;
                this.LblPHS.Text = contacterByUserName.Phs;
                this.LblHomePage.Text = contacterByUserName.Homepage;
                this.LnkEmail1.Text = contacterByUserName.Email;
                this.LnkEmail1.NavigateUrl = "mailto:" + contacterByUserName.Email;
                this.LnkEmail1.Target = "_blank";
                if (!string.IsNullOrEmpty(contacterByUserName.QQ))
                {
                    this.LblQQ.Text = contacterByUserName.QQ;
                    this.LblQQ.EndTag = "<a target=blank href=tencent://message/?uin=" + contacterByUserName.QQ + "><img border=\"0\" SRC=\"" + SiteConfig.SiteInfo.VirtualPath + SiteConfig.SiteOption.ManageDir + "/images/qq.gif\" alt=\"点击这里发即时消息\"></a>";
                }
                this.LblMSN.Text = contacterByUserName.Msn;
                this.LblICQ.Text = contacterByUserName.Icq;
                this.LblYahoo.Text = contacterByUserName.Yahoo;
                this.LblUC.Text = contacterByUserName.UC;
                this.LblAim.Text = contacterByUserName.Aim;
                if (contacterByUserName.Birthday.HasValue)
                {
                    this.LblBirthday.Text = contacterByUserName.Birthday.Value.ToString("yyyy年MM月dd日");
                }
                this.LblIDCard.Text = contacterByUserName.IdCard;
                this.LblNativePlace.Text = contacterByUserName.NativePlace;
                this.LblNation.Text = contacterByUserName.Nation;
                this.LblSex.Text = BasePage.EnumToHtml<UserSexType>(contacterByUserName.Sex);
                this.LblMarriage.Text = BasePage.EnumToHtml<UserMarriageType>(contacterByUserName.Marriage);
                this.LblEducation.Text = Choiceset.GetDictionaryFieldValueByName("PE_Contacter", "Education")[contacterByUserName.Education].DataTextField;
                this.LblGraduateFrom.Text = contacterByUserName.GraduateFrom;
                this.LblInterestsOfLife.Text = contacterByUserName.InterestsOfLife;
                this.LblInterestsOfCulture.Text = contacterByUserName.InterestsOfCulture;
                this.LblInterestsOfAmusement.Text = contacterByUserName.InterestsOfAmusement;
                this.LblInterestsOfSport.Text = contacterByUserName.InterestsOfSport;
                this.LblInterestsOfOther.Text = contacterByUserName.InterestsOfOther;
                this.LblIncome.Text = Choiceset.GetDictionaryFieldValueByName("PE_Contacter", "Income")[contacterByUserName.Income].DataTextField;
                this.LblCompany.Text = contacterByUserName.Company;
                this.LblDepartment.Text = contacterByUserName.Department;
                this.LblPosition.Text = contacterByUserName.Position;
                this.LblOperation.Text = contacterByUserName.Operation;
                this.LblCompanyAddress.Text = contacterByUserName.CompanyAddress;
            }
        }

        protected void OtherIncome_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("AddOtherIncome.aspx?UserID=" + this.userId.ToString());
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            UserInfo userById;
            this.m_CurrentPagePayout = 0M;
            this.m_CurrentPageIncome = 0M;
            this.userId = BasePage.RequestInt32("UserID");
            this.userName = BasePage.RequestString("UserName");
            int infoType = BasePage.RequestInt32("InfoType");
            if ((this.userId <= 0) && string.IsNullOrEmpty(this.userName))
            {
                AdminPage.WriteErrMsg("<li>参数不足！</li>", "UserManage.aspx");
            }
            this.BtnExchangePoint.Text = "  " + this.m_PointName + "兑换  ";
            this.BtnAddPoint.Text = "  奖励" + this.m_PointName + "  ";
            this.BtnMinusPoint.Text = "  扣除" + this.m_PointName + "  ";
            this.EgvUserPoint.Columns[2].HeaderText = SiteConfig.UserConfig.PointName + "数";
            if (this.userId != 0)
            {
                userById = Users.GetUserById(this.userId);
                this.userName = userById.UserName;
            }
            else
            {
                userById = Users.GetUsersByUserName(this.userName);
                this.userId = userById.UserId;
            }
            if (!userById.IsNull)
            {
                this.m_ClientId = userById.ClientId;
                if (!base.IsPostBack)
                {
                    this.InitPrivew();
                    if (!SiteConfig.ConfigInfo().UserConfig.EnableRegCompany)
                    {
                        this.EBtnRegCompany.Visible = false;
                    }
                    this.CompanySet(userById);
                    this.CheckeShop(userById);
                    this.LblUserId.Text = userById.UserId.ToString();
                    this.LblUserStatus.Text = BasePage.EnumToHtml<UserStatus>(userById.Status);
                    this.LblUserFriendGroup.Text = userById.UserFriendGroup.Replace("$", "，");
                    this.LblConsumeMoney.Text = userById.ConsumeMoney.ToString("0.00") + " 元";
                    this.LblConsumePoint.Text = userById.ConsumePoint.ToString() + " " + SiteConfig.UserConfig.PointUnit;
                    this.LblConsumeExp.Text = userById.ConsumeExp.ToString() + " 分";
                    this.LblPostItems.Text = userById.PostItems.ToString();
                    this.LblPassedItems.Text = userById.PassedItems.ToString();
                    this.LblRejectItems.Text = userById.RejectItems.ToString();
                    this.LblDelItems.Text = userById.DelItems.ToString();
                    this.LblLoginTimes.Text = userById.LogOnTimes.ToString();
                    this.LblUserName.Text = userById.UserName;
                    this.LnkEmail.Text = userById.Email;
                    this.LnkEmail.NavigateUrl = "mailto:" + userById.Email;
                    this.LnkEmail.Target = "_blank";
                    this.LblGroupName.Text = UserGroups.GetGroupName(userById.GroupId);
                    this.LblUserType.Text = BasePage.EnumToHtml<UserType>(userById.UserType);
                    this.LblBalance.Text = userById.Balance.ToString("0.00") + " 元";
                    this.LblUserPoint.Text = userById.UserPoint.ToString() + " " + SiteConfig.UserConfig.PointUnit;
                    this.LblUserExp.Text = userById.UserExp.ToString() + " 分";
                    this.LblValidNum.Text = Users.GetValidNum(userById.EndTime);
                    this.LblUnsignedItems.Text = SignInLog.GetNotSignInContentCountByUserName(userById.UserName).ToString() + " 篇";
                    this.LblUnreadMsg.Text = Message.UnreadMessageCount(userById.UserName).ToString() + " 条";
                    if (userById.IsInheritGroupRole)
                    {
                        this.LblSpecialPermission.Text = "继承会员组权限";
                    }
                    else
                    {
                        this.LblSpecialPermission.Text = "单独权限设置";
                    }
                    this.LblRegTime.Text = userById.RegTime.ToString("yyyy年MM月dd日");
                    this.LblJoinTime.Text = userById.JoinTime.ToString("yyyy年MM月dd日");
                    if (!string.IsNullOrEmpty(userById.LastLogOnTime.ToString()))
                    {
                        this.LblLastLoginTime.Text = userById.LastLogOnTime.Value.ToString("yyyy年MM月dd日 HH时mm分ss秒");
                    }
                    this.LblLastLoginIP.Text = userById.LastLogOnIP;
                    if (userById.Status == UserStatus.Locked)
                    {
                        this.BtnLock.Text = " 解锁此会员 ";
                        this.BtnLock.OnClientClick = "return confirm('确定要解锁此会员吗？');";
                        this.HdnLockType.Value = "0";
                    }
                    else
                    {
                        this.BtnLock.Text = " 锁定此会员 ";
                        this.BtnLock.OnClientClick = "return confirm('确定要锁定此会员吗？');";
                        this.HdnLockType.Value = "1";
                    }
                    this.LoadContacter(userById);
                    if (!SiteConfig.SiteOption.EnablePointMoneyExp)
                    {
                        this.BalancePoint.Style.Add("display", "none");
                        this.ExpValid.Style.Add("display", "none");
                        this.ConsumeMoney.Style.Add("display", "none");
                        this.ConsumeExp.Style.Add("display", "none");
                        this.Details.Style.Add("display", "none");
                        this.BtnIncome.Visible = false;
                        this.OtherIncome.Visible = false;
                        this.BtnPayment.Visible = false;
                        this.BtnExchangePoint.Visible = false;
                        this.BtnAddPoint.Visible = false;
                        this.BtnMinusPoint.Visible = false;
                        this.BtnExchangeValid.Visible = false;
                        this.BtnAddValidDate.Visible = false;
                        this.BtnMinusValidDate.Visible = false;
                    }
                }
                this.ShowInfoInitialize(infoType, userById);
            }
        }

        private void ShowInfoInitialize(int infoType, UserInfo userInfo)
        {
            if (infoType == 0)
            {
                this.OdsInfo.StartRowIndexParameterName = "startRowIndexId";
                this.OdsInfo.MaximumRowsParameterName = "maxNumberRows";
                this.OdsInfo.TypeName = "EasyOne.Shop.Order";
                this.OdsInfo.SelectMethod = "GetList";
                this.OdsInfo.SelectCountMethod = "GetTotalOfOrder";
                if (!this.Page.IsPostBack)
                {
                    this.InfoTabTitle0.Attributes.Add("class", "titlemouseover");
                    this.OdsInfo.SelectParameters.Add("searchType", TypeCode.String, "19");
                    this.OdsInfo.SelectParameters.Add("field", TypeCode.String, "UserName");
                    this.OdsInfo.SelectParameters.Add("keyword", TypeCode.String, userInfo.UserName);
                    this.OdsInfo.SelectParameters.Add("action", TypeCode.String, "search");
                    this.EgvOrder.DataSourceID = "OdsInfo";
                    this.EgvOrder.DataBind();
                    this.EgvOrder.Visible = true;
                }
            }
            else
            {
                this.InfoTabTitle0.Attributes.Add("onclick", "window.location.href='UserShow.aspx?InfoType=0&UserID=" + userInfo.UserId.ToString() + "'");
            }
            if (infoType == 1)
            {
                this.OdsInfo.StartRowIndexParameterName = "startRowIndex";
                this.OdsInfo.MaximumRowsParameterName = "maximumRows";
                this.OdsInfo.TypeName = "EasyOne.Accessories.BankrollItem";
                this.OdsInfo.SelectMethod = "GetList";
                this.OdsInfo.SelectCountMethod = "GetTotalOfBankrollItem";
                if (!this.Page.IsPostBack)
                {
                    this.InfoTabTitle1.Attributes.Add("class", "titlemouseover");
                    this.OdsInfo.SelectParameters.Add("searchType", TypeCode.String, "10");
                    this.OdsInfo.SelectParameters.Add("field", TypeCode.Int32, "6");
                    this.OdsInfo.SelectParameters.Add("keyword", TypeCode.String, userInfo.UserName);
                    this.EgvBankrollItem.DataSourceID = "OdsInfo";
                    this.EgvBankrollItem.DataBind();
                    this.EgvBankrollItem.Visible = true;
                    this.LblBankrollItemNotice.Visible = true;
                }
            }
            else
            {
                this.InfoTabTitle1.Attributes.Add("onclick", "window.location.href='UserShow.aspx?InfoType=1&UserID=" + userInfo.UserId.ToString() + "'");
                this.LblBankrollItemNotice.Visible = false;
            }
            if (infoType == 2)
            {
                this.OdsInfo.TypeName = "EasyOne.UserManage.UserPointLog";
                this.OdsInfo.StartRowIndexParameterName = "startRowIndexId";
                this.OdsInfo.MaximumRowsParameterName = "maxNumberRows";
                this.OdsInfo.SelectMethod = "GetPointList";
                this.OdsInfo.SelectCountMethod = "GetNumberOfUsersOnline";
                this.EgvUserPoint.DataSourceID = "OdsInfo";
                if (!this.Page.IsPostBack)
                {
                    this.InfoTabTitle2.Attributes.Add("class", "titlemouseover");
                    this.OdsInfo.SelectParameters.Add("scopesType", TypeCode.String, "10");
                    this.OdsInfo.SelectParameters.Add("field", TypeCode.String, "1");
                    this.OdsInfo.SelectParameters.Add("keyword", TypeCode.String, userInfo.UserName);
                    this.EgvUserPoint.DataBind();
                    this.EgvUserPoint.Visible = true;
                }
            }
            else
            {
                this.InfoTabTitle2.Attributes.Add("onclick", "window.location.href='UserShow.aspx?InfoType=2&UserID=" + userInfo.UserId.ToString() + "'");
            }
            if (infoType == 3)
            {
                this.OdsInfo.TypeName = "EasyOne.UserManage.UserValidLog";
                this.OdsInfo.StartRowIndexParameterName = "startRowIndexId";
                this.OdsInfo.MaximumRowsParameterName = "maxNumberRows";
                this.OdsInfo.SelectMethod = "GetValidList";
                this.OdsInfo.SelectCountMethod = "GetNumberOfUsersOnline";
                if (!this.Page.IsPostBack)
                {
                    this.InfoTabTitle3.Attributes.Add("class", "titlemouseover");
                    this.OdsInfo.SelectParameters.Add("scopesType", TypeCode.String, "10");
                    this.OdsInfo.SelectParameters.Add("field", TypeCode.String, "1");
                    this.OdsInfo.SelectParameters.Add("keyword", TypeCode.String, userInfo.UserName);
                    this.EgvUserValid.DataSourceID = "OdsInfo";
                    this.EgvUserValid.DataBind();
                    this.EgvUserValid.Visible = true;
                }
            }
            else
            {
                this.InfoTabTitle3.Attributes.Add("onclick", "window.location.href='UserShow.aspx?InfoType=3&UserID=" + userInfo.UserId.ToString() + "'");
            }
            if (infoType == 4)
            {
                this.OdsInfo.TypeName = "EasyOne.Accessories.PaymentLog";
                this.OdsInfo.StartRowIndexParameterName = "startRowIndexId";
                this.OdsInfo.MaximumRowsParameterName = "maxNumberRows";
                this.OdsInfo.SelectMethod = "GetList";
                this.OdsInfo.SelectCountMethod = "GetTotalOfPaymentLog";
                if (!this.Page.IsPostBack)
                {
                    this.InfoTabTitle4.Attributes.Add("class", "titlemouseover");
                    this.OdsInfo.SelectParameters.Add("searchType", TypeCode.String, "10");
                    this.OdsInfo.SelectParameters.Add("field", TypeCode.String, "UserName");
                    this.OdsInfo.SelectParameters.Add("keyword", TypeCode.String, userInfo.UserName);
                    this.GdvPaymentLogList.DataSourceID = "OdsInfo";
                    this.GdvPaymentLogList.DataBind();
                    this.GdvPaymentLogList.Visible = true;
                }
            }
            else
            {
                this.InfoTabTitle4.Attributes.Add("onclick", "window.location.href='UserShow.aspx?InfoType=4&UserID=" + userInfo.UserId.ToString() + "'");
            }
            if (infoType == 5)
            {
                this.OdsInfo.TypeName = "EasyOne.Crm.Complain";
                this.OdsInfo.StartRowIndexParameterName = "startRowIndexId";
                this.OdsInfo.MaximumRowsParameterName = "maxNumberRows";
                this.OdsInfo.SelectMethod = "GetList";
                this.OdsInfo.SelectCountMethod = "GetTotal";
                if (!this.Page.IsPostBack)
                {
                    this.InfoTabTitle5.Attributes.Add("class", "titlemouseover");
                    this.OdsInfo.SelectParameters.Add("searchType", TypeCode.String, "0");
                    this.OdsInfo.SelectParameters.Add("field", TypeCode.Int32, "3");
                    this.OdsInfo.SelectParameters.Add("keyword", TypeCode.String, userInfo.UserName);
                    this.EgvComplain.DataSourceID = "OdsInfo";
                    this.EgvComplain.DataBind();
                    this.EgvComplain.Visible = true;
                }
            }
            else
            {
                this.InfoTabTitle5.Attributes.Add("onclick", "window.location.href='UserShow.aspx?InfoType=5&UserID=" + userInfo.UserId.ToString() + "'");
            }
            if (infoType == 6)
            {
                this.OdsInfo.TypeName = "EasyOne.Shop.Order";
                this.OdsInfo.StartRowIndexParameterName = "startRowIndexId";
                this.OdsInfo.MaximumRowsParameterName = "maxNumberRows";
                this.OdsInfo.SelectMethod = "GetList";
                this.OdsInfo.SelectCountMethod = "GetTotalOfOrder";
                if (!this.Page.IsPostBack)
                {
                    this.InfoTabTitle6.Attributes.Add("class", "titlemouseover");
                    this.OdsInfo.SelectParameters.Add("searchType", TypeCode.String, "10");
                    this.OdsInfo.SelectParameters.Add("field", TypeCode.String, "AgentName");
                    this.OdsInfo.SelectParameters.Add("keyword", TypeCode.String, userInfo.UserName);
                    this.OdsInfo.SelectParameters.Add("action", TypeCode.String, string.Empty);
                    this.EgvAgentOrders.DataSourceID = "OdsInfo";
                    this.EgvAgentOrders.DataBind();
                    this.EgvAgentOrders.Visible = true;
                }
            }
            else
            {
                this.InfoTabTitle6.Attributes.Add("onclick", "window.location.href='UserShow.aspx?InfoType=6&UserID=" + userInfo.UserId.ToString() + "'");
            }
            if (infoType == 7)
            {
                this.OdsInfo.TypeName = "EasyOne.Accessories.BankrollItem";
                this.OdsInfo.StartRowIndexParameterName = "startRowIndex";
                this.OdsInfo.MaximumRowsParameterName = "maximumRows";
                this.OdsInfo.SelectMethod = "GetBillOfAgent";
                this.OdsInfo.SelectCountMethod = "GetTotalOfBill";
                if (!this.Page.IsPostBack)
                {
                    this.InfoTabTitle7.Attributes.Add("class", "titlemouseover");
                    this.OdsInfo.SelectParameters.Add(new Parameter("startRowIndex", TypeCode.Int32));
                    this.OdsInfo.SelectParameters.Add(new Parameter("maximumRows", TypeCode.Int32));
                    this.OdsInfo.SelectParameters.Add(new Parameter("userName", TypeCode.String, userInfo.UserName));
                    this.EgvBill.DataSourceID = "OdsInfo";
                    this.EgvBill.DataBind();
                    this.EgvBill.Visible = true;
                }
            }
            else
            {
                this.InfoTabTitle7.Attributes.Add("onclick", "window.location.href='UserShow.aspx?InfoType=7&UserID=" + userInfo.UserId.ToString() + "'");
            }
        }
    }
}

