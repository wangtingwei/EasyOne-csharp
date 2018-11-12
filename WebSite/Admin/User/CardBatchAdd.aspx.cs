namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.Accessories;
    using EasyOne.Model.Shop;
    using EasyOne.Shop;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using EasyOne.Model.UserManage;

    public partial class CardBatchAdd : AdminPage
    {

        private static void AddStock(CardInfo info, int cartAmount)
        {
            StockInfo stockInfo = new StockInfo();
            stockInfo.Inputer = PEContext.Current.Admin.AdminName;
            stockInfo.InputTime = DateTime.Now;
            stockInfo.Remark = "批量生成点卡";
            stockInfo.StockId = StockManage.GetMaxId() + 1;
            stockInfo.StockNum = StockItem.GetInStockNum();
            stockInfo.StockType = StockType.InStock;
            if (StockManage.Add(stockInfo))
            {
                decimal price = 0M;
                string productNum = string.Empty;
                string unit = string.Empty;
                string productName = string.Empty;
                if (!string.IsNullOrEmpty(info.TableName))
                {
                    Product.AddStocks(info.ProductId, cartAmount);
                    ProductInfo productById = Product.GetProductById(info.ProductId);
                    price = productById.PriceInfo.Price;
                    productNum = productById.ProductNum;
                    unit = productById.Unit;
                    productName = productById.ProductName;
                }
                else
                {
                    Present.AddStocks(info.ProductId, cartAmount);
                    PresentInfo presentById = Present.GetPresentById(info.ProductId);
                    price = presentById.Price;
                    productNum = presentById.PresentNum;
                    unit = presentById.Unit;
                    productName = presentById.PresentName;
                }
                if (!string.IsNullOrEmpty(productName))
                {
                    StockItemInfo info5 = new StockItemInfo();
                    info5.Amount = cartAmount;
                    info5.ItemId = StockItem.GetMaxId() + 1;
                    info5.ProductId = info.ProductId;
                    info5.TableName = info.TableName;
                    info5.Price = price;
                    info5.ProductNum = productNum;
                    info5.Unit = unit;
                    info5.ProductName = productName;
                    StockItem.Add(info5, stockInfo.StockId);
                }
            }
        }

        protected void BtnBatchAdd_Click(object sender, EventArgs e)
        {
            string str = this.Valid();
            if (!string.IsNullOrEmpty(str))
            {
                AdminPage.WriteErrMsg(str);
            }
            else
            {
                CardInfo info = new CardInfo();
                info.CardType = 0;
                string[] field = this.DropProductId.SelectedValue.Split(new char[] { '|' });
                info.ProductId = DataConverter.CLng(DataSecurity.GetArrayValue(0, field));
                info.TableName = DataSecurity.GetArrayValue(1, field);
                if (string.IsNullOrEmpty(info.TableName))
                {
                    info.ProductName = Present.GetPresentNameById(info.ProductId);
                }
                else
                {
                    info.ProductName = ProductCommon.GetProductName(info.ProductId);
                }
                info.Money = DataConverter.CDecimal(this.TxtMoney.Text);
                info.ValidNum = (this.DropValidUnit.SelectedValue == "5") ? DataConverter.CLng(this.DropUserGroup.SelectedValue) : DataConverter.CLng(this.TxtValidNum.Text);
                info.ValidUnit = DataConverter.CLng(this.DropValidUnit.SelectedValue);
                info.EndDate = this.DpkEnd.Date;
                info.AgentName = this.TxtAgentName.Text;
                info.UserName = "";
                info.CreateTime = DateTime.Now;
                info.OrderItemId = 0;
                IList<string[]> list = new List<string[]>();
                int num = DataConverter.CLng(this.TxtNums.Text);
                int num2 = 0;
                do
                {
                    info.CardNum = RandomManage.GetRandStringByPattern(this.TxtCardNum.Text).ToUpper();
                    string input = RandomManage.GetRandStringByPattern(this.TxtPassword.Text).ToUpper();
                    info.Password = StringHelper.Base64StringEncode(input);
                    if (Cards.CardAdd(info))
                    {
                        list.Add(new string[] { info.CardNum, input });
                    }
                    num2++;
                }
                while (num2 < num);
                if ((info.ProductId > 0) && (list.Count > 0))
                {
                    int count = list.Count;
                    AddStock(info, count);
                }
                this.ShowAddedCard(info, list);
            }
        }

        private string GetValidUnit()
        {
            if (DataConverter.CLng(this.DropValidUnit.SelectedItem.Value) == 5)
            {
                return this.DropUserGroup.SelectedItem.Text;
            }
            return this.DropValidUnit.SelectedItem.Text;
        }

        private void InitControls()
        {
            IList<ProductInfo> productCommonListByCharacter = Product.GetProductCommonListByCharacter(ProductCharacter.Card);
            IList<PresentInfo> presentByCharacter = Present.GetPresentByCharacter(ProductCharacter.Card);
            this.DropProductId.Items.Add(new ListItem("不通过商店销售", "0"));
            foreach (ProductInfo info in productCommonListByCharacter)
            {
                this.DropProductId.Items.Add(new ListItem(info.ProductName, info.ProductId.ToString() + "|" + info.TableName));
            }
            foreach (PresentInfo info2 in presentByCharacter)
            {
                this.DropProductId.Items.Add(new ListItem(info2.PresentName, info2.PresentId.ToString()));
            }
            this.DpkEnd.Text = DateTime.Today.AddYears(1).ToString("yyyy-MM-dd");
            IList<UserGroupsInfo> userGroupList = UserGroups.GetUserGroupList(0, 0);
            this.DropUserGroup.DataSource = userGroupList;
            this.DropUserGroup.DataTextField = "GroupName";
            this.DropUserGroup.DataValueField = "GroupId";
            this.DropUserGroup.DataBind();
            this.DropUserGroup.Attributes.Add("onchange", "selectValue()");
            this.DropValidUnit.Attributes.Add("onchange", "selectGroup()");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.InitControls();
            }
        }

        private void ShowAddedCard(CardInfo info, IList<string[]> list)
        {
            this.TdCardType.InnerText = Cards.GetCardType(info.CardType);
            this.TdCount.InnerText = list.Count.ToString();
            this.TdMoney.InnerText = info.Money.ToString() + "元";
            this.TdValidNum.InnerText = info.ValidNum.ToString() + this.GetValidUnit();
            this.TdEndDate.InnerText = info.EndDate.ToString("yyyy-MM-dd");
            this.TdAgentName.InnerText = info.AgentName;
            for (int i = 0; i < list.Count; i++)
            {
                TableRow row = new TableRow();
                row.CssClass = "tdbg";
                row.HorizontalAlign = HorizontalAlign.Center;
                TableCell cell = new TableCell();
                TableCell cell2 = new TableCell();
                cell.Text = list[i][0];
                cell2.Text = list[i][1];
                row.Cells.Add(cell);
                row.Cells.Add(cell2);
                this.TbCardList.Rows.Add(row);
            }
            this.PnlShow.Visible = true;
            this.PnlAdd.Visible = false;
        }

        private string Valid()
        {
            StringBuilder builder = new StringBuilder();
            if (DataConverter.CLng(this.TxtNums.Text) <= 0)
            {
                builder.Append("<li>请指定要生成的充值卡数量！</li>");
            }
            if (string.IsNullOrEmpty(this.TxtCardNum.Text.Trim()))
            {
                builder.Append("<li>充值卡号码不能为空！</li>");
            }
            if (string.IsNullOrEmpty(this.TxtPassword.Text))
            {
                builder.Append("<li>充值卡密码不能为空！</li>");
            }
            if (DataConverter.CLng(this.TxtMoney.Text) <= 0)
            {
                builder.Append("<li>请指定充值卡的面值！</li>");
            }
            if (DataConverter.CLng(this.TxtValidNum.Text) <= 0)
            {
                builder.Append("<li>请指定充值卡的点数！</li>");
            }
            if (this.DpkEnd.Date <= DateTime.Today)
            {
                builder.Append("<li>充值截止日期不能比当前日期还早</li>");
            }
            return builder.ToString();
        }
    }
}

