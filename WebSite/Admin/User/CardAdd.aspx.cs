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
    using System.Web.UI.WebControls;
    using EasyOne.ModelControls;
    using EasyOne.Model.UserManage;

    public partial class CardAdd : AdminPage
    {

        private void AddSingleCard()
        {
            CardInfo info = this.CreateCardInfo();
            info.CardNum = this.TxtCardNum.Text.Trim().ToUpper();
            info.Password = StringHelper.Base64StringEncode(this.TxtPassword.Text.Trim().ToUpper());
            info.AgentName = this.TxtAgentName.Text.Trim();
            if (!Cards.CardAdd(info))
            {
                AdminPage.WriteErrMsg("输入的充值卡卡号已经存在！");
            }
            else
            {
                AddStocks(info, 1);
                BasePage.ResponseRedirect("CardsManage.aspx");
            }
        }

        private static void AddStocks(CardInfo info, int qty)
        {
            if (info.ProductId > 0)
            {
                StockInfo stockInfo = new StockInfo();
                stockInfo.StockId = StockManage.GetMaxId() + 1;
                stockInfo.Inputer = PEContext.Current.Admin.AdminName;
                stockInfo.InputTime = DateTime.Now;
                stockInfo.Remark = (qty > 1) ? "批量生成点卡" : "生成点卡";
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
                        Product.AddStocks(info.ProductId, qty);
                        ProductInfo productById = Product.GetProductById(info.ProductId);
                        price = productById.PriceInfo.Price;
                        productNum = productById.ProductNum;
                        unit = productById.Unit;
                        productName = productById.ProductName;
                    }
                    else
                    {
                        Present.AddStocks(info.ProductId, qty);
                        PresentInfo presentById = Present.GetPresentById(info.ProductId);
                        price = presentById.Price;
                        productNum = presentById.PresentNum;
                        unit = presentById.Unit;
                        productName = presentById.PresentName;
                    }
                    if (!string.IsNullOrEmpty(productName))
                    {
                        StockItemInfo info5 = new StockItemInfo();
                        info5.Amount = qty;
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
        }

        private void BatchAddCards()
        {
            Cards cards = new Cards();
            CardInfo info = this.CreateCardInfo();
            int qty = cards.BatchAddCards(info, this.TxtCardText.Text.Trim().ToUpper(), this.TxtSplit.Text, this.TxtAgentName.Text.Trim());
            if (qty > 0)
            {
                AddStocks(info, qty);
                AdminPage.WriteSuccessMsg(cards.Message, "CardAdd.aspx");
            }
            else
            {
                AdminPage.WriteErrMsg(cards.Message);
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            string str = this.Valid();
            if (!string.IsNullOrEmpty(str))
            {
                AdminPage.WriteErrMsg(str.ToString());
            }
            else if (this.RadAddTypeSingle.Checked)
            {
                this.AddSingleCard();
            }
            else
            {
                this.BatchAddCards();
            }
        }

        private CardInfo CreateCardInfo()
        {
            CardInfo info = new CardInfo();
            info.CardType = DataConverter.CLng(this.RadlCardType.SelectedValue);
            string[] field = this.DropProductId.SelectedValue.Split(new char[] { '|' });
            info.ProductId = DataConverter.CLng(DataSecurity.GetArrayValue(0, field));
            info.TableName = DataSecurity.GetArrayValue(1, field);
            info.Money = DataConverter.CDecimal(this.TxtMoney.Text);
            info.ValidNum = (this.DropValidUnit.SelectedValue == "5") ? DataConverter.CLng(this.DropUserGroup.SelectedValue) : DataConverter.CLng(this.TxtValidNum.Text);
            info.ValidUnit = DataConverter.CLng(this.DropValidUnit.SelectedValue);
            info.EndDate = this.DpkEnd.Date;
            info.CreateTime = DateTime.Now;
            info.UserName = "";
            info.OrderItemId = 0;
            if (string.IsNullOrEmpty(info.TableName))
            {
                info.ProductName = Present.GetPresentNameById(info.ProductId);
                return info;
            }
            info.ProductName = ProductCommon.GetProductName(info.ProductId);
            return info;
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
            IList<UserGroupsInfo> userGroupList = UserGroups.GetUserGroupList(0, 0);
            this.DropUserGroup.DataSource = userGroupList;
            this.DropUserGroup.DataTextField = "GroupName";
            this.DropUserGroup.DataValueField = "GroupId";
            this.DropUserGroup.DataBind();
            this.DpkEnd.Text = DateTime.Today.AddYears(1).ToString("yyyy-MM-dd");
            this.RadAddTypeSingle.Attributes.Add("onclick", "document.getElementById('trSingle1').style.display='';document.getElementById('trSingle2').style.display='';document.getElementById('trBatch').style.display='none';");
            this.RadAddTypeBatch.Attributes.Add("onclick", "document.getElementById('trSingle1').style.display='none';document.getElementById('trSingle2').style.display='none';document.getElementById('trBatch').style.display='';");
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

        private string Valid()
        {
            StringBuilder builder = new StringBuilder();
            if ((this.RadlCardType.SelectedValue == "1") && (this.DropProductId.SelectedValue == "0"))
            {
                builder.Append("<li>其他公司卡必须通过商店销售。请指定所属商品。</li>");
            }
            if (this.RadAddTypeSingle.Checked)
            {
                if (string.IsNullOrEmpty(this.TxtCardNum.Text))
                {
                    builder.Append("<li>请指定充值卡ID</li>");
                }
                if (string.IsNullOrEmpty(this.TxtPassword.Text))
                {
                    builder.Append("<li>请指定充值卡密码</li>");
                }
            }
            else
            {
                if (string.IsNullOrEmpty(this.TxtCardText.Text))
                {
                    builder.Append("<li>请输入批量添加的充值卡格式文本</li>");
                }
                if (string.IsNullOrEmpty(this.TxtSplit.Text))
                {
                    builder.Append("<li>请指定分隔符</li>");
                }
            }
            if (DataConverter.CLng(this.TxtMoney.Text) <= 0)
            {
                builder.Append("<li>请指定充值卡的面值！</li>");
            }
            if (DataConverter.CLng(this.TxtValidNum.Text) <= 0)
            {
                builder.Append("<li>请指定充值卡的点数！</li>");
            }
            if (this.DpkEnd.Date < DateTime.Today)
            {
                builder.Append("<li>充值截止日期不能比当前日期还早</li>");
            }
            return builder.ToString();
        }
    }
}

