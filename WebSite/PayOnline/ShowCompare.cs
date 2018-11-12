namespace EasyOne.WebSite.Shop
{
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Enumerations;
    using EasyOne.Model.CommonModel;
    using EasyOne.Model.Shop;
    using EasyOne.Shop;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using System.Data;

    public class ShowCompare : DynamicPage
    {
        protected Button BtnAddProduct;
        protected DropDownList DropNodeId;
        protected DropDownList DropProducts;
        protected DropDownList DropTrademark;
        protected HtmlForm form1;
        private InsideStaticLabel m_InsideStaticlabel;
        protected int m_MaxProductCount = 4;
        private const string s_Tdbg = "tdbg";
        private const string s_Tdbgleft = "tdbgleft";
        protected ScriptManager ScriptManager1;
        protected Table TbProduct;
        protected UpdatePanel UpdatePanel1;
        protected UpdatePanel UpdatePanel3;

        private void AddButton()
        {
            TableRow row = this.NewRow();
            row.CssClass = "tdbg";
            row.Cells.Add(NewTitleCell("取消比较："));
            List<ProductDetailInfo> productInfoList = this.ProductInfoList;
            for (int i = 0; i < productInfoList.Count; i++)
            {
                TableCell cell = new TableCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                Button child = new Button();
                Button button2 = new Button();
                Button button3 = new Button();
                if (productInfoList[i].IsNull)
                {
                    string str = Convert.ToString((int) (i * -1));
                    button2.ID = "BtnProv" + str;
                    button3.ID = "BtnNext" + str;
                    child.ID = "BtnRemoveProduct" + str;
                }
                else
                {
                    button2.ID = "BtnProv" + productInfoList[i].GeneralId.ToString();
                    button3.ID = "BtnNext" + productInfoList[i].GeneralId.ToString();
                    child.ID = "BtnRemoveProduct" + productInfoList[i].GeneralId.ToString();
                }
                child.CommandArgument = productInfoList[i].GeneralId.ToString();
                button2.CommandArgument = (i - 1).ToString();
                button3.CommandArgument = i.ToString();
                button2.Text = "<<";
                button3.Text = ">>";
                button2.Visible = i != 0;
                button3.Visible = i != (productInfoList.Count - 1);
                child.Text = "移除商品";
                child.Command += new CommandEventHandler(this.BtnRemoveProduct_Click);
                button2.Command += new CommandEventHandler(this.BtnReverse_Click);
                button3.Command += new CommandEventHandler(this.BtnReverse_Click);
                cell.Controls.Add(button2);
                cell.Controls.Add(child);
                cell.Controls.Add(button3);
                if (productInfoList[i].IsNull)
                {
                    button2.Enabled = button3.Enabled = child.Enabled = false;
                }
                row.Cells.Add(cell);
            }
            this.TbProduct.Rows.Add(row);
        }

        private void AddEmptyInfo()
        {
            while (this.ProductInfoList.Count < this.m_MaxProductCount)
            {
                ProductDetailInfo item = new ProductDetailInfo(true);
                this.ProductInfoList.Add(item);
            }
        }

        private void AddFieldRows()
        {
            Dictionary<int, IList<EasyOne.Model.CommonModel.FieldInfo>> dictionary = new Dictionary<int, IList<EasyOne.Model.CommonModel.FieldInfo>>();
            List<ProductDetailInfo> productInfoList = this.ProductInfoList;
            bool flag = false;
            foreach (ProductDetailInfo info in productInfoList)
            {
                if ((info.ModelId != 0) && !dictionary.ContainsKey(info.ModelId))
                {
                    dictionary.Add(info.ModelId, Field.GetFieldList(info.ModelId));
                    if (!flag)
                    {
                        this.NewCategoryRow("详细参数");
                        flag = true;
                    }
                }
            }
            List<DataTable> list2 = new List<DataTable>();
            for (int i = 0; i < productInfoList.Count; i++)
            {
                list2.Add(ContentManage.GetContentDataById(productInfoList[i].ProductId));
            }
            foreach (KeyValuePair<int, IList<EasyOne.Model.CommonModel.FieldInfo>> pair in dictionary)
            {
                foreach (EasyOne.Model.CommonModel.FieldInfo info2 in pair.Value)
                {
                    if (info2.FieldType != FieldType.Property)
                    {
                        TableRow row = this.NewRow();
                        row.CssClass = "tdbg";
                        row.Cells.Add(NewTitleCell(info2.FieldAlias + "："));
                        for (int j = 0; j < productInfoList.Count; j++)
                        {
                            if (productInfoList[j].ModelId == pair.Key)
                            {
                                TableCell cell = NewCell(list2[j].Rows[0][info2.FieldName].ToString());
                                cell.HorizontalAlign = HorizontalAlign.Center;
                                row.Cells.Add(cell);
                            }
                            else
                            {
                                row.Cells.Add(NewCell(string.Empty));
                            }
                        }
                        this.TbProduct.Rows.Add(row);
                    }
                }
            }
        }

        private void AddProductAttributeRows()
        {
            List<ProductDetailInfo> productInfoList = this.ProductInfoList;
            TableRow row = new TableRow();
            row.CssClass = "tdbg";
            row.Cells.Add(NewTitleCell("商品属性："));
            foreach (ProductDetailInfo info in productInfoList)
            {
                TableCell cell = NewCell(this.GetProductAttributes(info));
                cell.HorizontalAlign = HorizontalAlign.Center;
                row.Cells.Add(cell);
            }
            this.TbProduct.Rows.Add(row);
        }

        private void AddServiceTime()
        {
            TableRow row = this.NewRow();
            row.CssClass = "tdbg";
            row.Cells.Add(NewTitleCell("服务期限："));
            foreach (ProductDetailInfo info in this.ProductInfoList)
            {
                TableCell cell;
                if (info.IsNull)
                {
                    cell = NewCell(string.Empty);
                }
                else
                {
                    cell = NewCell(info.ServiceTerm.ToString() + BasePage.EnumToHtml<ServiceTermUnit>(info.ServiceTermUnit));
                }
                cell.HorizontalAlign = HorizontalAlign.Center;
                row.Cells.Add(cell);
            }
            this.TbProduct.Rows.Add(row);
        }

        protected void BtnAddProduct_Click(object sender, EventArgs e)
        {
            if (this.DropProducts.SelectedIndex != 0)
            {
                int id = DataConverter.CLng(this.DropProducts.SelectedValue);
                foreach (ProductDetailInfo info in this.ProductInfoList)
                {
                    if (info.GeneralId == id)
                    {
                        return;
                    }
                }
                for (int i = 0; i < this.ProductInfoList.Count; i++)
                {
                    if (this.ProductInfoList[i].IsNull)
                    {
                        this.ProductInfoList[i] = Product.GetProductDetailInfoById(id);
                        break;
                    }
                }
                this.Compare();
            }
        }

        private void BtnRemoveProduct_Click(object sender, CommandEventArgs e)
        {
            int num = DataConverter.CLng(e.CommandArgument);
            for (int i = 0; i < this.ProductInfoList.Count; i++)
            {
                if (this.ProductInfoList[i].GeneralId == num)
                {
                    this.ProductInfoList.RemoveAt(i);
                    this.Compare();
                    return;
                }
            }
        }

        private void BtnReverse_Click(object sender, CommandEventArgs e)
        {
            this.ProductInfoList.Reverse(DataConverter.CLng(e.CommandArgument), 2);
            this.Compare();
        }

        private void Compare()
        {
            List<ProductDetailInfo> productInfoList = this.ProductInfoList;
            int num = 0;
            for (int i = 0; i < productInfoList.Count; i++)
            {
                if (!productInfoList[i].IsNull)
                {
                    num++;
                }
            }
            this.BtnAddProduct.Enabled = num < this.m_MaxProductCount;
            this.AddEmptyInfo();
            this.TbProduct.Rows.Clear();
            this.NewRow("商品名称：", "ProductName", new FormatMethod(this.GetProductName));
            TableRow row = this.TbProduct.Rows[0];
            row.Cells[0].Width = Unit.Percentage(15.0);
            double n = 0x55 / productInfoList.Count;
            for (int j = 1; j < row.Cells.Count; j++)
            {
                row.Cells[j].Width = Unit.Percentage(n);
            }
            this.AddButton();
            this.NewCategoryRow("基本参数");
            this.NewRow("所属节点：", "NodeName", null);
            this.NewRow("所属模型：", "ModelId", new FormatMethod(this.GetModelName));
            this.NewRow("商品图片：", "ProductThumb", new FormatMethod(this.GetImagePath));
            this.NewRow("信誉评级：", "Stars", new FormatMethod(this.GetStars));
            this.AddProductAttributeRows();
            this.NewRow("商品厂商：", "ProducerName", null);
            this.NewRow("商品品牌：", "TrademarkName", null);
            this.NewRow("市场价：", "PriceMarket", new FormatMethod(this.FormatPrice));
            this.NewRow("零售价：", "PriceInfo", new FormatMethod(this.GetPrice));
            this.NewRow("商品重量", "Weight", new FormatMethod(this.FormatWeight));
            this.AddServiceTime();
            this.NewRow("商品简介：", "ProductIntro", null, HorizontalAlign.Left);
            this.AddFieldRows();
        }

        private void DropDownListDataBind(ListControl lst, object dataSource, string firstItem)
        {
            lst.Items.Clear();
            lst.DataSource = dataSource;
            lst.DataBind();
            if (!string.IsNullOrEmpty(firstItem))
            {
                lst.Items.Insert(0, new ListItem(firstItem, "0"));
            }
        }

        protected void DropNodeId_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DropDownListDataBind(this.DropTrademark, Product.GetTrademarkListByNodeId(DataConverter.CLng(this.DropNodeId.SelectedValue)), "==全部品牌==");
            this.DropTrademark_SelectedIndexChanged(sender, e);
        }

        protected void DropTrademark_SelectedIndexChanged(object sender, EventArgs e)
        {
            string trademarkName = string.Empty;
            if (this.DropTrademark.SelectedIndex != 0)
            {
                trademarkName = this.DropTrademark.SelectedValue;
            }
            IDictionary<int, string> listByNodeIdAndTrademark = Product.GetListByNodeIdAndTrademark(DataConverter.CLng(this.DropNodeId.SelectedValue), trademarkName);
            this.DropDownListDataBind(this.DropProducts, listByNodeIdAndTrademark, "==选择商品==");
        }

        private string FormatPrice(ProductDetailInfo info)
        {
            if (info.IsNull)
            {
                return string.Empty;
            }
            return info.PriceMarket.ToString("￥0.00");
        }

        private string FormatWeight(ProductDetailInfo info)
        {
            if (info.IsNull)
            {
                return string.Empty;
            }
            return (info.Weight.ToString() + "Kg");
        }

        private string GetImagePath(ProductDetailInfo info)
        {
            string productThumb = info.ProductThumb;
            string str2 = string.Empty;
            string str3 = string.Empty;
            if (SiteConfig.ThumbsConfig.ThumbsWidth != 0)
            {
                str2 = "width='" + SiteConfig.ThumbsConfig.ThumbsWidth.ToString() + "px'";
            }
            else if (SiteConfig.ThumbsConfig.ThumbsHeight != 0)
            {
                str2 = "height='" + SiteConfig.ThumbsConfig.ThumbsHeight.ToString() + "'";
            }
            if (info.IsNull)
            {
                str3 = " style='filter:gray; '";
            }
            if (!string.IsNullOrEmpty(productThumb))
            {
                string str4 = "<img src='" + base.BasePath + SiteConfig.SiteOption.UploadDir + "/" + productThumb.TrimEnd(new char[] { '/' }) + "' alt='立刻购买' " + str2 + str3 + " border='0'/>";
                return this.GetLink(str4, info.GeneralId);
            }
            string content = "<img src='" + base.BasePath + SiteConfig.SiteOption.UploadDir + "/nopic.gif' alt='立刻购买' " + str2 + str3 + " border='0'/>";
            return this.GetLink(content, info.GeneralId);
        }

        private string GetLink(string content, int generalId)
        {
            if (this.m_InsideStaticlabel == null)
            {
                this.m_InsideStaticlabel = new InsideStaticLabel();
            }
            if (generalId == 0)
            {
                return content;
            }
            return (("<a href='" + this.m_InsideStaticlabel.GetInfoPath(generalId.ToString()) + "'target='_blank' title='立刻购买' >") + content + "</a>");
        }

        private string GetModelName(ProductDetailInfo info)
        {
            return ModelManager.GetCacheModelById(info.ModelId).ModelName;
        }

        private string GetPrice(ProductDetailInfo info)
        {
            if (info.IsNull)
            {
                return string.Empty;
            }
            return info.PriceInfo.Price.ToString("￥0.00");
        }

        private string GetProductAttributes(ProductDetailInfo info)
        {
            string str = string.Empty;
            if (info.IsBest)
            {
                str = str + "<span style='color:blue'>精</span>&nbsp;&nbsp;&nbsp;";
            }
            if (info.IsHot)
            {
                str = str + "<span style='color:red'>热</span>&nbsp;&nbsp;&nbsp;";
            }
            if (info.IsNew)
            {
                str = str + "<span style='color:green'>新</span> ";
            }
            return str;
        }

        private string GetProductName(ProductDetailInfo info)
        {
            return this.GetLink(info.ProductName, info.GeneralId);
        }

        private string GetStars(ProductDetailInfo info)
        {
            return ("<span style='color:Green'>" + new string('★', info.Stars) + "</span>");
        }

        private void NewCategoryRow(string desc)
        {
            TableRow row = new TableRow();
            row.CssClass = "tdbgleft";
            TableCell cell = NewTitleCell(desc);
            cell.Font.Bold = true;
            TableCell cell2 = NewTitleCell(string.Empty);
            cell2.ColumnSpan = this.ProductInfoList.Count;
            row.Cells.Add(cell);
            row.Cells.Add(cell2);
            this.TbProduct.Rows.Add(row);
        }

        private static TableCell NewCell(string name)
        {
            TableCell cell = new TableCell();
            cell.Text = string.IsNullOrEmpty(name) ? "&nbsp;" : name;
            return cell;
        }

        private TableRow NewRow()
        {
            TableRow row = new TableRow();
            row.Attributes.Add("onmouseover", "this.className='tdbgleft'");
            row.Attributes.Add("onmouseout", "this.className='tdbg'");
            return row;
        }

        private void NewRow(string desc, string propertyName, FormatMethod fm)
        {
            this.NewRow(desc, propertyName, fm, HorizontalAlign.Center);
        }

        private void NewRow(string desc, string propertyName, FormatMethod fm, HorizontalAlign cellAlign)
        {
            TableRow row = this.NewRow();
            row.CssClass = "tdbg";
            row.Cells.Add(NewTitleCell(desc));
            for (int i = 0; i < this.ProductInfoList.Count; i++)
            {
                ProductDetailInfo info = this.ProductInfoList[i];
                string name = string.Empty;
                if (fm != null)
                {
                    name = fm(info);
                }
                else
                {
                    MethodInfo method = info.GetType().GetMethod("get_" + propertyName);
                    if (method != null)
                    {
                        name = Convert.ToString(method.Invoke(info, null));
                    }
                }
                TableCell cell = NewCell(name);
                cell.HorizontalAlign = cellAlign;
                row.Cells.Add(cell);
            }
            this.TbProduct.Rows.Add(row);
        }

        private static TableCell NewTitleCell(string name)
        {
            TableCell cell = NewCell(name);
            cell.Width = Unit.Percentage(10.0);
            cell.CssClass = "tdbgleft";
            return cell;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Compare();
            if (!base.IsPostBack)
            {
                this.DropDownListDataBind(this.DropNodeId, Nodes.GetShopNodeList(), "==选择节点==");
            }
        }

        private List<ProductDetailInfo> ProductInfoList
        {
            get
            {
                if (this.ViewState["ProductInfoList"] == null)
                {
                    this.ViewState["ProductInfoList"] = new List<ProductDetailInfo>();
                }
                return (List<ProductDetailInfo>) this.ViewState["ProductInfoList"];
            }
            set
            {
                this.ViewState["ProductInfoList"] = value;
            }
        }

        private delegate string FormatMethod(ProductDetailInfo info);
    }
}

