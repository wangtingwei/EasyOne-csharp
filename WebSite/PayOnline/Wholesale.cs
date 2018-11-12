namespace EasyOne.WebSite.Shop
{
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.Shop;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using EasyOne.Model.CommonModel;

    public class Wholesale : DynamicPage
    {
        protected Button BtnBuy;
        protected Button BtnSearch;
        protected DropDownList DropField;
        protected DropDownList DropModel;
        protected DropDownList DropSearch;
        protected ExtendedGridView EgvProduct;
        protected HtmlForm form1;
        protected ObjectDataSource OdsProduct;
        protected TextBox TxtKeyword;
        protected UserNavigation UserCenterNavigation;
        protected ExtendedSiteMapPath YourPosition;

        protected void BtnBuy_OnClick(object sender, EventArgs e)
        {
            StringBuilder selectList = new StringBuilder();
            selectList = this.EgvProduct.SelectList;
            if (selectList.Length == 0)
            {
                DynamicPage.WriteErrMsg("<li>对不起，您还没选择要批发的商品！</li>", "Wholesale.aspx");
            }
            StringBuilder sb = new StringBuilder();
            string[] strArray = selectList.ToString().Split(new char[] { ',' });
            for (int i = 0; i < this.EgvProduct.Rows.Count; i++)
            {
                HiddenField field = this.EgvProduct.Rows[i].FindControl("HdnGeneralId") as HiddenField;
                foreach (string str in strArray)
                {
                    if (str == field.Value)
                    {
                        StringHelper.AppendString(sb, str + "$$$" + DataConverter.CLng((this.EgvProduct.Rows[i].FindControl("TxtAmount") as TextBox).Text, 10).ToString());
                        break;
                    }
                }
            }
            base.Response.Redirect("~/Shop/ShoppingCart.aspx?Action=Wholesale&IDList=" + sb);
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            this.DropSearch.SelectedValue = "0";
        }

        protected void DropSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DropModel.SelectedIndex = -1;
            this.DropField.SelectedValue = "0";
            this.TxtKeyword.Text = "";
        }

        protected void EgvProduct_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
            {
                return;
            }
            ProductDetailInfo dataItem = (ProductDetailInfo) e.Row.DataItem;
            Literal literal = e.Row.FindControl("LtrProductName") as Literal;
            HiddenField field = e.Row.FindControl("HdnGeneralId") as HiddenField;
            Literal literal2 = e.Row.FindControl("LtrProductType") as Literal;
            Literal literal3 = e.Row.FindControl("LtrPrice") as Literal;
            Literal literal4 = e.Row.FindControl("LtrPrice1") as Literal;
            Literal literal5 = e.Row.FindControl("LtrPrice2") as Literal;
            Literal literal6 = e.Row.FindControl("LtrPrice3") as Literal;
            field.Value = dataItem.GeneralId.ToString();
            if (string.IsNullOrEmpty(dataItem.NodeName))
            {
                literal.Text = "[" + dataItem.NodeName + "] " + dataItem.ProductName;
            }
            else
            {
                literal.Text = dataItem.ProductName;
            }
            switch (dataItem.ProductType)
            {
                case ProductType.Normal:
                    literal2.Text = "正常";
                    break;

                case ProductType.Special:
                    literal2.Text = "特价";
                    goto Label_013D;
            }
        Label_013D:
            literal3.Text = dataItem.PriceInfo.Price.ToString("N2");
            literal4.Text = dataItem.PriceInfo.NumberWholesale1.ToString() + " / " + dataItem.PriceInfo.PriceWholesale1.ToString("N2");
            literal5.Text = dataItem.PriceInfo.NumberWholesale2.ToString() + " / " + dataItem.PriceInfo.PriceWholesale2.ToString("N2");
            literal6.Text = dataItem.PriceInfo.NumberWholesale3.ToString() + " / " + dataItem.PriceInfo.PriceWholesale3.ToString("N2");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.BtnBuy.Attributes.Add("onclick", "this.form.target='_newName'");
            if (!this.Page.IsPostBack)
            {
                IList<ModelInfo> shopModelList = ModelManager.GetShopModelList(ModelShowType.Enable);
                this.DropModel.DataSource = shopModelList;
                ListItem item = new ListItem();
                item.Text = "所有模型";
                item.Value = "0";
                this.DropModel.DataBind();
                this.DropModel.Items.Insert(0, item);
            }
        }
    }
}

