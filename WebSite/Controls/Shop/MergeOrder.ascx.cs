namespace EasyOne.WebSite.Controls
{
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.Shop;
    using EasyOne.Shop;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class MergeOrder : BaseUserControl
    {

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                if (this.TxtPrincipalOrder.Text == this.TxtSubordinateOrder.Text)
                {
                    BaseUserControl.WriteErrMsg("<li>合并的订单不能为同一个订单！</li>");
                }
                OrderInfo orderByOrderNum = Order.GetOrderByOrderNum(this.TxtPrincipalOrder.Text);
                if (orderByOrderNum.IsNull)
                {
                    BaseUserControl.WriteErrMsg("<li>主订单（" + this.TxtPrincipalOrder.Text + "）不存在！</li>");
                }
                if (orderByOrderNum.Status > OrderStatus.WaitForConfirm)
                {
                    BaseUserControl.WriteErrMsg("<li>合并的主订单必须为未确认的订单！</li>");
                }
                OrderInfo info2 = Order.GetOrderByOrderNum(this.TxtSubordinateOrder.Text);
                if (info2.IsNull)
                {
                    BaseUserControl.WriteErrMsg("<li>从订单（" + this.TxtSubordinateOrder.Text + "）不存在！</li>");
                }
                if (info2.Status > OrderStatus.WaitForConfirm)
                {
                    BaseUserControl.WriteErrMsg("<li>合并的订单必须为未确认的订单！</li>");
                }
                if (orderByOrderNum.UserName != info2.UserName)
                {
                    BaseUserControl.WriteErrMsg("<li>合并的订单必须属于同个用户</li>");
                }
                if (!string.IsNullOrEmpty(this.OrderUserName) && (this.OrderUserName != orderByOrderNum.UserName))
                {
                    BaseUserControl.WriteErrMsg("<li>只能合并属于自己的订单！</li>");
                }
                IList<OrderItemInfo> infoListByOrderId = OrderItem.GetInfoListByOrderId(orderByOrderNum.OrderId);
                foreach (OrderItemInfo info3 in OrderItem.GetInfoListByOrderId(info2.OrderId))
                {
                    info3.OrderId = orderByOrderNum.OrderId;
                    OrderItem.Update(info3);
                    infoListByOrderId.Add(info3);
                }
                string selectedValue = this.RadlMergeType.SelectedValue;
                if (selectedValue != null)
                {
                    if (!(selectedValue == "1"))
                    {
                        if (selectedValue == "2")
                        {
                            orderByOrderNum.Remark = orderByOrderNum.Remark + info2.Remark;
                            orderByOrderNum.Memo = orderByOrderNum.Memo + info2.Memo;
                        }
                    }
                    else
                    {
                        orderByOrderNum.Remark = info2.Remark;
                        orderByOrderNum.Memo = info2.Memo;
                    }
                }
                Order.UpdateOrderInfo(orderByOrderNum, infoListByOrderId);
                Order.Delete(info2.OrderId.ToString());
                BaseUserControl.WriteSuccessMsg("订单合并成功！", this.Returnurl + "?OrderID=" + orderByOrderNum.OrderId.ToString());
            }
        }

        protected void DropPrincipalOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.TxtPrincipalOrder.Text = this.DropPrincipalOrder.SelectedItem.Text;
        }

        protected void DropSubordinateOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.TxtSubordinateOrder.Text = this.DropSubordinateOrder.SelectedItem.Text;
        }

        private void Initialize()
        {
            IDictionary<int, string> listByUserName = Order.GetListByUserName(this.OrderUserName);
            this.DropPrincipalOrder.DataSource = listByUserName;
            this.DropPrincipalOrder.DataBind();
            this.DropPrincipalOrder.Items.Insert(0, "请选择...");
            if (string.IsNullOrEmpty(this.SubordinateOrderNum))
            {
                this.DropSubordinateOrder.DataSource = listByUserName;
                this.DropSubordinateOrder.DataBind();
                this.DropSubordinateOrder.Items.Insert(0, "请选择...");
            }
            else
            {
                this.DropSubordinateOrder.Visible = false;
                this.TxtSubordinateOrder.Text = this.SubordinateOrderNum;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.Initialize();
            }
        }

        public string OrderUserName
        {
            get
            {
                return Convert.ToString(this.ViewState["OrderUserName"]);
            }
            set
            {
                this.ViewState["OrderUserName"] = value;
            }
        }

        public string Returnurl
        {
            get
            {
                if (string.IsNullOrEmpty(Convert.ToString(this.ViewState["Returnurl"])))
                {
                    return "OrderManage.aspx";
                }
                return Convert.ToString(this.ViewState["Returnurl"]);
            }
            set
            {
                this.ViewState["Returnurl"] = value;
            }
        }

        public string SubordinateOrderNum
        {
            get
            {
                return Convert.ToString(this.ViewState["SubordinateOrderNum"]);
            }
            set
            {
                this.ViewState["SubordinateOrderNum"] = value;
            }
        }
    }
}

