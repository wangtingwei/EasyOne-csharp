namespace EasyOne.WebSite.Admin.User
{
    using AjaxControlToolkit;
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
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class CardsManage : AdminPage
    {
        protected void BtnDel_Click(object sender, EventArgs e)
        {
            if (this.EgvCards.SelectList.Length == 0)
            {
                AdminPage.WriteErrMsg("请指定要删除的充值卡ID");
            }
            else
            {
                string[] strArray = this.EgvCards.SelectList.ToString().Split(new char[] { ',' });
                int num = 0;
                StockInfo stockInfo = new StockInfo();
                stockInfo.Inputer = PEContext.Current.Admin.AdminName;
                stockInfo.InputTime = DateTime.Now;
                stockInfo.StockId = StockManage.GetMaxId() + 1;
                stockInfo.StockNum = StockItem.GetShipmentNum();
                stockInfo.StockType = StockType.Shipment;
                foreach (string str in strArray)
                {
                    int cardId = DataConverter.CLng(str);
                    CardInfo cardById = Cards.GetCardById(cardId);
                    if ((Cards.DelCard(cardId) && (cardById.ProductId > 0)) && (!string.IsNullOrEmpty(cardById.TableName) && DeleteSingleCard(cardById, stockInfo.StockId)))
                    {
                        num++;
                    }
                }
                if (num > 0)
                {
                    stockInfo.Remark = (num > 1) ? "批量删除点卡" : "删除点卡";
                    StockManage.Add(stockInfo);
                }
                this.EgvCards.DataBind();
            }
        }

        protected void BtnExportExcel_Click(object sender, EventArgs e)
        {
            if (this.EgvCards.Rows.Count > 0)
            {
                IList<CardInfo> list = Cards.GetCardList(0, 0, BasePage.RequestString("CardType"), BasePage.RequestString("CardStatus"), BasePage.RequestString("Field"), BasePage.RequestString("Keyword"), BasePage.RequestString("AgentName"));
                StringBuilder builder = new StringBuilder();
                base.Response.Clear();
                base.Response.AppendHeader("content-disposition", "attachment;filename=Cards.xls");
                base.Response.ContentEncoding = Encoding.GetEncoding("GB2312");
                base.Response.ContentType = "application/vnd.xls";
                builder.Append("<table border='1' cellspacing='1' style='border-collapse: collapse;table-layout:fixed' id='AutoNumber1' height='32'><tr>");
                builder.Append("<td align='center'><b>类型</b></td>");
                builder.Append("<td align='center'><b>卡号</b></td>");
                builder.Append("<td align='center'><b>密码</b></td>");
                builder.Append("<td align='center'><b>面值(元)</b></td>");
                builder.Append("<td align='center'><b>点数</b></td>");
                builder.Append("<td align='center'><b>截止日期</b></td>");
                builder.Append("<td align='center'><b>所属商品</b></td>");
                builder.Append("<td align='center'><b>状态</b></td>");
                builder.Append("<td align='center'><b>使用者</b></td>");
                builder.Append("<td align='center'><b>充值时间</b></td>");
                builder.Append("<td align='center'><b>代理商</b></td></tr>");
                foreach (CardInfo info in list)
                {
                    builder.Append("<tr>");
                    builder.Append("<td align='center'>" + Cards.GetCardType(info.CardType) + "</td>");
                    builder.Append("<td align='center'>" + info.CardNum + "</td>");
                    builder.Append("<td align='center'>" + StringHelper.Base64StringDecode(info.Password) + "</td>");
                    builder.Append("<td align='center'>" + info.Money.ToString("N2") + "</td>");
                    builder.Append("<td align='center'>" + info.ValidNum.ToString() + Cards.GetValidUnitType(info.ValidUnit) + "</td>");
                    builder.Append("<td align='center'>" + info.EndDate.ToString() + "</td>");
                    builder.Append("<td align='center'>" + Cards.GetProductName(DataSecurity.HtmlEncode(info.ProductName)) + "</td>");
                    builder.Append("<td align='center'>" + Cards.GetCardStatus(info) + "</td>");
                    builder.Append("<td align='center'>" + DataSecurity.HtmlEncode(info.UserName) + "</td>");
                    builder.Append("<td align='center'>" + info.UseTime.ToString() + "</td>");
                    builder.Append("<td align='center'>" + DataSecurity.HtmlEncode(info.AgentName) + "</td>");
                    builder.Append("</tr>");
                }
                base.Response.Write(builder.ToString());
                base.Response.End();
            }
        }

        private static bool DeleteSingleCard(CardInfo info, int stockId)
        {
            if (Product.AddStocks(info.ProductId, -1))
            {
                ProductInfo productById = Product.GetProductById(info.ProductId, info.TableName);
                if (!productById.IsNull)
                {
                    StockItemInfo info3 = new StockItemInfo();
                    info3.Amount = 1;
                    info3.Price = productById.PriceInfo.Price;
                    info3.ProductId = productById.ProductId;
                    info3.TableName = productById.TableName;
                    info3.Unit = productById.Unit;
                    info3.ProductNum = productById.ProductNum;
                    info3.ProductName = productById.ProductName;
                    return StockItem.Add(info3, stockId);
                }
            }
            return false;
        }

        protected void EgvCards_DataBound(object sender, EventArgs e)
        {
            if (this.EgvCards.Rows.Count <= 0)
            {
                this.BtnDel.Enabled = false;
                this.BtnExportExcel.Enabled = false;
            }
        }

        protected void EgvCards_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CardInfo dataItem = e.Row.DataItem as CardInfo;
                if (dataItem != null)
                {
                    e.Row.Cells[1].Text = Cards.GetCardType(dataItem.CardType);
                    if (dataItem.ValidUnit == 5)
                    {
                        e.Row.Cells[4].Text = UserGroups.GetUserGroupById(dataItem.ValidNum).GroupName;
                    }
                    else
                    {
                        e.Row.Cells[4].Text = dataItem.ValidNum.ToString() + Cards.GetValidUnitType(dataItem.ValidUnit);
                    }
                    e.Row.Cells[6].Text = Cards.GetProductName(dataItem.ProductName);
                    e.Row.Cells[7].Text = Cards.GetCardStatus(dataItem);
                    if (!string.IsNullOrEmpty(dataItem.UserName) || (dataItem.OrderItemId > 0))
                    {
                        HyperLink link = e.Row.FindControl("HlnkModify") as HyperLink;
                        LinkButton button = e.Row.FindControl("LBtnDelete") as LinkButton;
                        if (link != null)
                        {
                            link.Visible = false;
                        }
                        if (button != null)
                        {
                            button.Visible = false;
                        }
                        e.Row.Cells[0].Enabled = false;
                    }
                }
            }
        }

        protected void EgvCards_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int cardId = DataConverter.CLng(e.Keys["CardId"]);
            CardInfo cardById = Cards.GetCardById(cardId);
            if ((Cards.DelCard(cardId) && (cardById.ProductId > 0)) && !string.IsNullOrEmpty(cardById.TableName))
            {
                StockInfo stockInfo = new StockInfo();
                stockInfo.Inputer = PEContext.Current.Admin.AdminName;
                stockInfo.InputTime = DateTime.Now;
                stockInfo.Remark = "删除点卡";
                stockInfo.StockId = StockManage.GetMaxId() + 1;
                stockInfo.StockNum = StockItem.GetShipmentNum();
                stockInfo.StockType = StockType.Shipment;
                if (DeleteSingleCard(cardById, stockInfo.StockId))
                {
                    StockManage.Add(stockInfo);
                }
            }
        }
    }
}

