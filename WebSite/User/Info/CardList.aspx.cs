namespace EasyOne.WebSite.User.Info
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Model.Accessories;
    using EasyOne.Model.Shop;
    using EasyOne.Shop;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Collections.Generic;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class CardList : DynamicPage
    {


        private TableRow CreateTableRow(string css, params string[] cells)
        {
            TableRow row = new TableRow();
            row.HorizontalAlign = HorizontalAlign.Center;
            row.CssClass = css;
            foreach (string str in cells)
            {
                TableCell cell = new TableCell();
                cell.Text = str;
                row.Cells.Add(cell);
            }
            if (row.Cells.Count == 2)
            {
                row.Cells[1].ColumnSpan = 6;
            }
            return row;
        }

        private string GetCardKind(int validNum, int validUnit)
        {
            if (validUnit == 5)
            {
                return UserGroups.GetUserGroupById(validNum).GroupName;
            }
            return (validNum.ToString() + Cards.GetValidUnitType(validUnit));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                IList<UserOrderCommonInfo> cardList = Order.GetCardList(PEContext.Current.User.UserName);
                if (cardList.Count <= 0)
                {
                    TableRow row = this.CreateTableRow("tdbg", new string[] { "您还没有购买任何点卡类商品！" });
                    row.Cells[0].Height = Unit.Pixel(100);
                    this.TbCardList.Rows.Add(row);
                }
                else
                {
                    this.ShowCards(cardList);
                }
            }
        }

        private void ShowCards(IList<UserOrderCommonInfo> userOrderCommonInfoList)
        {
            this.TbCardList.Rows.Add(this.CreateTableRow("title", new string[] { "商品名称", "充值卡类型", "充值卡卡号", "充值卡密码", "充值卡面值", "充值卡点数", "充值截止日期" }));
            foreach (UserOrderCommonInfo info in userOrderCommonInfoList)
            {
                IList<CardInfo> list = Cards.GetCardList(info.TableName, info.ProductId, info.OrderItemId);
                if (list.Count <= 0)
                {
                    this.TbCardList.Rows.Add(this.CreateTableRow("tdbg", new string[] { info.ProductName, "尚没有交付卡号和密码，请您与我们联系。" }));
                }
                else
                {
                    int num = 0;
                    foreach (CardInfo info2 in list)
                    {
                        if (string.IsNullOrEmpty(info2.UserName))
                        {
                            this.TbCardList.Rows.Add(this.CreateTableRow("tdbg", new string[] { info.ProductName, Cards.GetCardType(info2.CardType), info2.CardNum, StringHelper.Base64StringDecode(info2.Password), info2.Money.ToString("N2"), this.GetCardKind(info2.ValidNum, info2.ValidUnit), info2.EndDate.ToString("yyyy-MM-dd") }));
                            num++;
                        }
                    }
                    if (num == 0)
                    {
                        this.TbCardList.Rows.Add(this.CreateTableRow("tdbg", new string[] { info.ProductName, "您购买的充值卡已经使用。" }));
                    }
                }
            }
        }
    }
}

