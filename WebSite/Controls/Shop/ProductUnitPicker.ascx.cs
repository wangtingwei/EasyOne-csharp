namespace EasyOne.WebSite.Controls.Shop
{
    using EasyOne.Controls;
    using EasyOne.Shop;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Web.UI.WebControls;

    public partial class ProductUnitPicker : BaseUserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                IList<string> unitList;
                if (this.IsPresent)
                {
                    unitList = Present.GetUnitList();
                }
                else
                {
                    unitList = Product.GetUnitList();
                }
                foreach (string str in unitList)
                {
                    this.DropProductUnit.Items.Add(str);
                }
                this.DropProductUnit.Attributes.Add("onchange", "PickUnit()");
            }
        }

        [Category("自定义"), Description("true 是针对促销商品的单位 ，否则是针对商品的单位"), Browsable(true)]
        public bool IsPresent
        {
            get
            {
                return ((this.ViewState["IsPresent"] != null) && ((bool) this.ViewState["IsPresent"]));
            }
            set
            {
                this.ViewState["IsPresent"] = value;
            }
        }

        public bool NoValidator
        {
            get
            {
                return false;
            }
            set
            {
                if (value)
                {
                    this.ValrUnit.Enabled = false;
                    this.ValrUnit.ShowRequiredText = false;
                }
            }
        }

        public string ProductUnit
        {
            get
            {
                return this.TxtUnit.Text;
            }
            set
            {
                this.TxtUnit.Text = value;
            }
        }
    }
}

