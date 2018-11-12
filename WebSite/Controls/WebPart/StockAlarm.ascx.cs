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
    using System.Drawing;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using EasyOne.WebSite.Controls.WebPart;

    public partial class StockAlarm : BaseWebPart, IWebEditable, IWebPartPermissibility
    {
        //protected ExtendedGridView EgvStockAlarm;
        protected bool m_HasPermissions = true;
        protected string m_Keyword;
        protected string m_OperateCode;
        protected int m_PageSize = 10;

        public EditorPartCollection CreateEditorParts()
        {
            ArrayList editorParts = new ArrayList();
            StockAlarmEditorPart part = new StockAlarmEditorPart();
                ID = this.ID + "_editorPart1";
            
            editorParts.Add(part);
            return new EditorPartCollection(editorParts);
        }

        protected void EgvStockAlarm_DataBound(object sender, EventArgs e)
        {
            if (this.EgvStockAlarm.Rows.Count > 0)
            {
                this.EgvStockAlarm.Columns[this.EgvStockAlarm.Columns.Count - 1].Visible = this.Keyword == "30";
            }
        }

        protected void EgvStockAlarm_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ProductDetailInfo dataItem = (ProductDetailInfo) e.Row.DataItem;
                HyperLink link = (HyperLink) e.Row.FindControl("HlnkProductName");
                link.Text = "[" + dataItem.NodeName + "] " + dataItem.ProductName;
                link.NavigateUrl = base.BasePath + SiteConfig.SiteOption.ManageDir + "/Shop/ProductView.aspx?GeneralID=" + dataItem.GeneralId.ToString();
                int stocks = 0;
                string str = string.Empty;
                if (this.Keyword == "29")
                {
                    stocks = dataItem.Stocks;
                    str = "实际";
                }
                else
                {
                    stocks = dataItem.Stocks - dataItem.OrderNum;
                    str = "预订";
                }
                if ((stocks <= dataItem.AlarmNum) && (stocks > 0))
                {
                    e.Row.Cells[1].ForeColor = Color.Green;
                    link.ToolTip = string.Format("{0} 的{1}库存量已低于库存报警下限！", link.Text, str);
                }
                else
                {
                    e.Row.Cells[1].ForeColor = Color.Red;
                    link.ToolTip = string.Format("{0} 已售完！", link.Text);
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
                base.Title = "库存报警的商品";
            }
            this.EgvStockAlarm.DataSource = Product.GetProductsList(0, this.PageSize, "SpeedSearch", this.Keyword, 0, 0, 100);
            this.EgvStockAlarm.DataBind();
            base.Subtitle = "共" + Product.GetTotalOfAllProducts("SpeedSearch", this.Keyword, 0, 0, 100).ToString() + "条";
            base.TitleUrl = base.BasePath + SiteConfig.SiteOption.ManageDir + "/Shop/ProductManage.aspx?SearchType=SpeedSearch&Keyword=" + this.Keyword;
        }

        [Personalizable(PersonalizationScope.User)]
        public string Keyword
        {
            get
            {
                if (string.IsNullOrEmpty(this.m_Keyword))
                {
                    this.m_Keyword = "29";
                }
                return this.m_Keyword;
            }
            set
            {
                this.m_Keyword = value;
            }
        }

        [Personalizable(PersonalizationScope.User)]
        public string OperateCode
        {
            get
            {
                if (string.IsNullOrEmpty(this.m_OperateCode))
                {
                    this.m_OperateCode = EasyOne.Enumerations.OperateCode.ProductManage.ToString();
                }
                return this.m_OperateCode;
            }
            set
            {
                this.m_OperateCode = value;
            }
        }

        [Personalizable(PersonalizationScope.User), WebBrowsable, WebDisplayName("显示项目数"), WebDescription("显示项目数")]
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

        public object WebBrowsableObject
        {
            get
            {
                return this;
            }
        }
    }
    public class StockAlarmEditorPart : EditorPart
    {
        private DropDownList m_DropKeyword;

        public override bool ApplyChanges()
        {
            StockAlarm webBrowsableObject = (StockAlarm)base.WebPartToEdit.WebBrowsableObject;
            webBrowsableObject.Keyword = this.DropKeyword.SelectedValue;
            return true;
        }

        protected override void CreateChildControls()
        {
            this.Controls.Clear();
            this.m_DropKeyword = new DropDownList();
            this.m_DropKeyword.Items.Add(new ListItem("实际库存报警的商品", "29"));
            this.m_DropKeyword.Items.Add(new ListItem("预订库存报警的商品", "30"));
            this.Controls.Add(this.m_DropKeyword);
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            writer.Write("库存报警类型");
            this.m_DropKeyword.RenderControl(writer);
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
            StockAlarm webBrowsableObject = (StockAlarm)base.WebPartToEdit.WebBrowsableObject;
            this.SetListControlsSelect(this.DropKeyword, webBrowsableObject.Keyword);
        }

        private DropDownList DropKeyword
        {
            get
            {
                this.EnsureChildControls();
                return this.m_DropKeyword;
            }
        }
    }
}

