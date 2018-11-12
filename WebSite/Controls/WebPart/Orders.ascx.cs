namespace EasyOne.WebSite.Controls.WebPart
{
    using EasyOne.AccessManage;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.Shop;
    using EasyOne.ModelControls;
    using EasyOne.Shop;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI;
    using System.Collections;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;

    public partial class Orders : BaseWebPart, IWebEditable, IWebPartPermissibility
    {
        protected string m_AdminPath;
        protected bool m_HasPermissions = true;
        protected string m_OperateCode;
        protected int m_PageSize = 10;
        protected string m_SearchType;

        public EditorPartCollection CreateEditorParts()
        {
            ArrayList editorParts = new ArrayList();
            OrderEditorPart part = new OrderEditorPart ();
            ID = this.ID + "_editorPart1";
            
            editorParts.Add(part);
            return new EditorPartCollection(editorParts);
        }

        protected void EgvOrders_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                OrderInfo dataItem = (OrderInfo) e.Row.DataItem;
                HyperLink link = (HyperLink) e.Row.FindControl("HlnkOrderNum");
                Label label = (Label) e.Row.FindControl("LblOrderStatus");
                Label label2 = (Label) e.Row.FindControl("LblPayStatus");
                link.NavigateUrl = base.BasePath + SiteConfig.SiteOption.ManageDir + "/Shop/OrderManage.aspx?OrderID=" + dataItem.OrderId.ToString();
                link.Enabled = this.m_HasPermissions;
                label.Text = BaseUserControl.EnumToHtml<OrderStatus>(dataItem.Status);
                switch (Order.GetPayStatus(dataItem))
                {
                    case PayStatus.WaitForPay:
                        label2.Text = BaseUserControl.EnumToHtml<PayStatus>(PayStatus.WaitForPay);
                        return;

                    case PayStatus.ReceivedEarnest:
                        label2.Text = BaseUserControl.EnumToHtml<PayStatus>(PayStatus.ReceivedEarnest);
                        return;

                    case PayStatus.Payoff:
                        label2.Text = BaseUserControl.EnumToHtml<PayStatus>(PayStatus.Payoff);
                        return;

                    default:
                        return;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!PEContext.Current.Admin.IsSuperAdmin)
            {
                this.m_HasPermissions = RolePermissions.AccessCheck(this.OperateCode);
            }
            if (string.IsNullOrEmpty(base.Title))
            {
                base.Title = "我负责跟踪的订单";
            }
            this.EgvOrders.DataSource = Order.GetList(0, this.PageSize, this.SearchType, "", "", "");
            this.EgvOrders.DataBind();
            base.Subtitle = "共" + Order.GetTotalOfOrder(this.SearchType, "", "", "").ToString() + "条";
            base.TitleUrl = base.BasePath + SiteConfig.SiteOption.ManageDir + "/Shop/OrderList.aspx?SearchType=" + this.SearchType;
        }

        [Personalizable(PersonalizationScope.User)]
        public string OperateCode
        {
            get
            {
                if (string.IsNullOrEmpty(this.m_OperateCode))
                {
                    this.m_OperateCode = EasyOne.Enumerations.OperateCode.OrderView.ToString();
                }
                return this.m_OperateCode;
            }
            set
            {
                this.m_OperateCode = value;
            }
        }

        [WebDescription("显示项目数"), WebBrowsable, WebDisplayName("显示项目数"), Personalizable(PersonalizationScope.User)]
        public int PageSize
        {
            get
            {
                return this.m_PageSize;
            }
            set
            {
                this.m_PageSize = value;
            }
        }

        [Personalizable(PersonalizationScope.User)]
        public string SearchType
        {
            get
            {
                if (string.IsNullOrEmpty(this.m_SearchType))
                {
                    return "18";
                }
                return this.m_SearchType;
            }
            set
            {
                this.m_SearchType = value;
            }
        }

        public object WebBrowsableObject
        {
            get
            {
                return this;
            }
        }
    }
    public class OrderEditorPart : EditorPart
    {
        private DropDownList m_DropSearchType;

        public override bool ApplyChanges()
        {
            Orders webBrowsableObject = (Orders)base.WebPartToEdit.WebBrowsableObject;
            webBrowsableObject.SearchType = this.DropSearchType.SelectedValue;
            return true;
        }

        protected override void CreateChildControls()
        {
            this.Controls.Clear();
            this.m_DropSearchType = new DropDownList();
            this.m_DropSearchType.Items.Add(new ListItem("今天的新订单", "1"));
            this.m_DropSearchType.Items.Add(new ListItem("未确认的订单", "4"));
            this.m_DropSearchType.Items.Add(new ListItem("未送货的订单", "7"));
            this.m_DropSearchType.Items.Add(new ListItem("未开发票的订单", "9"));
            this.m_DropSearchType.Items.Add(new ListItem("我负责跟踪的订单", "18"));
            this.Controls.Add(this.m_DropSearchType);
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            writer.Write("待处理的订单类型");
            this.m_DropSearchType.RenderControl(writer);
            writer.WriteBreak();
            writer.WriteBreak();
        }

        public void SetListControlsSelect(ListControl control, string value)
        {
            foreach (ListItem item in control.Items)
            {
                if (item.Value == value)
                {
                    item.Selected = true;
                    break;
                }
            }
        }

        public override void SyncChanges()
        {
            Orders webBrowsableObject = (Orders)base.WebPartToEdit.WebBrowsableObject;
            this.SetListControlsSelect(this.DropSearchType, webBrowsableObject.SearchType);
        }

        private DropDownList DropSearchType
        {
            get
            {
                this.EnsureChildControls();
                return this.m_DropSearchType;
            }
        }
    }
}

