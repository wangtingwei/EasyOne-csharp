namespace EasyOne.WebSite.Controls
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Crm;
    using EasyOne.Model.Accessories;
    using EasyOne.Model.Crm;
    using EasyOne.Model.Shop;
    using EasyOne.Shop;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class ShowOrder : BaseUserControl
    {
        private string m_ClientName;
        private int m_ShowOptions;
        private decimal m_TotalPayout;

        protected void EgvBankroll_DataBound(object sender, EventArgs e)
        {
            if (this.EgvBankroll.Rows.Count > 0)
            {
                GridViewRow footerRow = this.EgvBankroll.FooterRow;
                footerRow.Cells.Clear();
                TableCell cell = new TableCell();
                cell.ColumnSpan = 5;
                cell.Text = "合计金额：";
                cell.HorizontalAlign = HorizontalAlign.Right;
                footerRow.Cells.Add(cell);
                TableCell cell2 = new TableCell();
                cell2.Text = Math.Abs(this.m_TotalPayout).ToString("N2");
                cell2.HorizontalAlign = HorizontalAlign.Right;
                footerRow.Cells.Add(cell2);
                TableCell cell3 = new TableCell();
                cell3.ColumnSpan = 3;
                footerRow.Cells.Add(cell3);
            }
        }

        protected void EgvBankroll_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                BankrollItemInfo dataItem = e.Row.DataItem as BankrollItemInfo;
                if (this.ShowOptions == 1)
                {
                    HyperLink link = (HyperLink) e.Row.FindControl("LnkUserName");
                    link.NavigateUrl = string.Empty;
                }
                e.Row.Cells[3].Text = BankrollItem.GetMoneyType(dataItem.MoneyType);
                e.Row.Cells[4].Text = BankrollItem.GetCurrencyType(dataItem.CurrencyType);
                e.Row.Cells[6].Text = (dataItem.Money > 0M) ? "收入" : "支出";
                this.m_TotalPayout += dataItem.Money;
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
                if (this.ShowOptions == 1)
                {
                    HyperLink link = (HyperLink) e.Row.FindControl("LnkClientShow2");
                    link.NavigateUrl = string.Empty;
                    HyperLink link2 = (HyperLink) e.Row.FindControl("LnkComplainTitle");
                    link2.NavigateUrl = string.Empty;
                }
            }
        }

        protected void EgvDeliverItem_RowCommand(object sender, CommandEventArgs e)
        {
            if ((e.CommandName == "Received") && (this.OrderId != "0"))
            {
                int orderId = DataConverter.CLng(this.OrderId);
                DeliverItem.UpdateReceive(orderId);
                Order.Recieve(orderId);
                if (this.ShowOptions == 1)
                {
                    BaseUserControl.ResponseRedirect("ShowOrder.aspx?OrderId=" + this.OrderId);
                }
            }
        }

        protected void EgvDeliverItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DeliverItemInfo dataItem = e.Row.DataItem as DeliverItemInfo;
                e.Row.Cells[1].Text = (dataItem.DeliverDirection == 1) ? "发货" : "<span style='color:#F00'>退货</span>";
                e.Row.Cells[6].Text = dataItem.Received ? "<span style='color:#F00'><strong>√</strong></span>" : "";
                Literal literal = (Literal) e.Row.FindControl("LitExpressCompony");
                if (dataItem.CourierId > 0)
                {
                    literal.Text = this.GetExpressCompony(dataItem.CourierId);
                }
                if ((dataItem.DeliverDirection == 1) && !dataItem.Received)
                {
                    e.Row.Cells[8].Enabled = true;
                }
                else
                {
                    e.Row.Cells[8].Text = "";
                }
                Literal literal2 = (Literal) e.Row.FindControl("LitExpressNumber");
                literal2.Text = dataItem.ExpressNumber;
                Literal literal3 = (Literal) e.Row.FindControl("LitExpressState");
                CourierInfo courier = Courier.GetCourier(dataItem.CourierId);
                if (!string.IsNullOrEmpty(courier.SearchUrl))
                {
                    literal3.Text = "<a href=" + courier.SearchUrl.Replace("{$ExpressNumber}", dataItem.ExpressNumber) + " Target=\"_Blank\">查看</a>";
                }
            }
        }

        protected void EgvFeedback_RowCommand(object sender, CommandEventArgs e)
        {
            string commandName = e.CommandName;
            if (commandName != null)
            {
                if (!(commandName == "Del"))
                {
                    if (!(commandName == "DelReply"))
                    {
                        if (commandName == "ReplyContent")
                        {
                            BaseUserControl.ResponseRedirect("OrderFeedbackModify.aspx?Action=ReplyContent&ID=" + e.CommandArgument.ToString());
                        }
                        return;
                    }
                }
                else
                {
                    if (OrderFeedback.Delete(e.CommandArgument.ToString()))
                    {
                        BaseUserControl.WriteSuccessMsg("删除反馈信息成功", "OrderManage.aspx?OrderID=" + this.OrderId);
                        return;
                    }
                    BaseUserControl.WriteErrMsg("删除反馈信息失败");
                    return;
                }
                OrderFeedbackInfo orderFeedbackById = OrderFeedback.GetOrderFeedbackById(DataConverter.CLng(e.CommandArgument));
                if (!orderFeedbackById.IsNull)
                {
                    orderFeedbackById.ReplyContent = string.Empty;
                    orderFeedbackById.ReplyName = string.Empty;
                }
                if (OrderFeedback.Update(orderFeedbackById))
                {
                    BaseUserControl.WriteSuccessMsg("删除回复信息成功！", "OrderManage.aspx?OrderID=" + this.OrderId);
                }
                else
                {
                    BaseUserControl.WriteErrMsg("删除回复信息失败！");
                }
            }
        }

        protected void EgvInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Text = Invoice.GetInvoiceType(((InvoiceInfo) e.Row.DataItem).InvoiceType);
                if (this.ShowOptions == 1)
                {
                    HyperLink link = (HyperLink) e.Row.FindControl("LnkInvoiceID");
                    link.NavigateUrl = string.Empty;
                }
            }
        }

        protected void EgvService_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ServiceInfo dataItem = e.Row.DataItem as ServiceInfo;
                e.Row.Cells[3].Text = Service.GetFiledNameById("ServiceType", DataConverter.CLng(dataItem.ServiceType));
                e.Row.Cells[4].Text = Service.GetFiledNameById("ServiceMode", DataConverter.CLng(dataItem.ServiceMode));
                e.Row.Cells[6].Text = Service.GetFiledNameById("Result", dataItem.ServiceResult);
                e.Row.Cells[8].Text = !dataItem.ConfirmTime.HasValue ? "" : Service.GetFiledNameById("ConfirmScore", dataItem.ConfirmScore);
                if (this.ShowOptions == 1)
                {
                    HyperLink link = (HyperLink) e.Row.FindControl("LnkClientShow");
                    link.NavigateUrl = string.Empty;
                    HyperLink link2 = (HyperLink) e.Row.FindControl("LnkServiceTitle");
                    link2.NavigateUrl = string.Empty;
                }
            }
        }

        private void FeedbackDataBind()
        {
            IList<OrderFeedbackInfo> list = OrderFeedback.GetList(DataConverter.CLng(this.OrderId));
            IList<OrderFeedbackInfo> list2 = new List<OrderFeedbackInfo>();
            foreach (OrderFeedbackInfo info in list)
            {
                string replyName = info.ReplyName;
                info.ReplyName = string.Empty;
                list2.Add(info);
                if (!string.IsNullOrEmpty(info.ReplyContent))
                {
                    OrderFeedbackInfo item = new OrderFeedbackInfo();
                    item.Content = "管理员回复：" + info.ReplyContent;
                    item.ReplyName = replyName;
                    item.WriteTime = info.ReplyTime;
                    item.ReplyContent = info.ReplyContent;
                    item.Id = info.Id;
                    list2.Add(item);
                }
            }
            this.EgvFeedback.DataSource = list2;
            this.EgvFeedback.DataBind();
            if (this.ShowOptions == 1)
            {
                this.EgvFeedback.Columns[2].Visible = false;
                this.EgvFeedback.Columns[3].Visible = false;
            }
        }

        protected void GdvPaymentLogList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                PaymentLogInfo dataItem = e.Row.DataItem as PaymentLogInfo;
                object obj1 = e.Row.DataItem;
                if (dataItem != null)
                {
                    Label label = (Label) e.Row.FindControl("LblStatus");
                    Label label2 = (Label) e.Row.FindControl("LblPlatform");
                    HyperLink link = (HyperLink) e.Row.FindControl("LnkPaymentNum");
                    if (this.ShowOptions == 1)
                    {
                        link.Text = dataItem.PaymentNum;
                    }
                    else
                    {
                        link.Text = dataItem.PaymentNum;
                        link.NavigateUrl = string.Concat(new object[] { "../User/PaymentLogDetail.aspx?ReturnUrl=../Shop/OrderManage.aspx?OrderID=", dataItem.OrderId, "&PaymentLogID=", dataItem.PaymentLogId.ToString() });
                    }
                    label.Text = PaymentLog.GetStatusDepict(dataItem.PlatformId, dataItem.Status);
                    if (dataItem.Status != 1)
                    {
                        e.Row.Cells[0].Enabled = false;
                    }
                    label2.Text = PayPlatform.GetPayPlatformById(dataItem.PlatformId).PayPlatformName;
                }
            }
        }

        public string GetExpressCompony(int courierId)
        {
            return Courier.GetCourier(courierId).ShortName;
        }

        private void GridViewBind()
        {
            if (!base.IsPostBack)
            {
                if (this.ShowOptions != 0)
                {
                    this.EgvBankroll.Columns[0].Visible = false;
                    this.EgvBankroll.Columns[8].Visible = false;
                    this.EgvBankroll.Columns[1].HeaderText = "付款人";
                    this.EgvInvoice.Columns[6].Visible = false;
                    this.EgvInvoice.Columns[7].HeaderText = "开票时间";
                    this.EgvDeliverItem.Columns[8].Visible = true;
                }
                if (this.OrderId != "0")
                {
                    this.EgvBankroll.DataSource = BankrollItem.GetList(0, 0, 10, 5, this.OrderId);
                    this.EgvInvoice.DataSource = Invoice.GetList(0, 0, 5, this.OrderId, 0);
                    this.EgvDeliverItem.DataSource = DeliverItem.GetList(0, 0, 8, this.OrderId, 0);
                    this.EgvTransferLog.DataSource = TransferLog.GetList(0, 0, "10", "OrderID", this.OrderId);
                    this.EgvBankroll.DataBind();
                    this.EgvInvoice.DataBind();
                    this.EgvDeliverItem.DataBind();
                    this.EgvTransferLog.DataBind();
                    if (string.IsNullOrEmpty(this.ClientName))
                    {
                        this.EgvService.DataSource = null;
                        this.EgvComplain.DataSource = null;
                    }
                    else
                    {
                        this.EgvService.DataSource = Service.GetListByClientName(0, 0x7fffffff, this.ClientName);
                        this.EgvComplain.DataSource = Complain.GetListByClientName(0, 0x7fffffff, this.ClientName);
                    }
                    this.EgvService.DataBind();
                    this.EgvComplain.DataBind();
                    this.FeedbackDataBind();
                    this.GdvPaymentLogList.DataSource = PaymentLog.GetListByOrderId(DataConverter.CLng(this.OrderId));
                    this.GdvPaymentLogList.DataBind();
                }
            }
        }

        protected string IsShow()
        {
            string str = "";
            if (this.ShowOptions == 1)
            {
                str = "style=\"display:none;\"";
            }
            return str;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.GridViewBind();
        }

        public string ClientName
        {
            get
            {
                return this.m_ClientName;
            }
            set
            {
                this.m_ClientName = value;
            }
        }

        public string OrderId
        {
            get
            {
                return DataConverter.CLng(this.ViewState["OrderID"]).ToString();
            }
            set
            {
                this.ViewState["OrderID"] = value;
            }
        }

        public int ShowOptions
        {
            get
            {
                return this.m_ShowOptions;
            }
            set
            {
                this.m_ShowOptions = value;
            }
        }
    }
}

