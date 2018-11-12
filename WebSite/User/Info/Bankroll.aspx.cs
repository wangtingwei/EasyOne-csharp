namespace EasyOne.WebSite.User.Info
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.Accessories;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Collections;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class Bankroll : DynamicPage
    {

        private decimal m_CurrentPageIncome;
        private decimal m_CurrentPagePayout;


        protected void EgvBankroll_DataBound(object sender, EventArgs e)
        {
            if (this.EgvBankroll.Rows.Count > 0)
            {
                GridViewRow footerRow = this.EgvBankroll.FooterRow;
                footerRow.CssClass = this.EgvBankroll.RowStyle.CssClass;
                while (footerRow.Cells.Count > 4)
                {
                    footerRow.Cells.RemoveAt(0);
                }
                ArrayList totalInComeAndPayOutAll = BankrollItem.GetTotalInComeAndPayOutAll(this.HdnUserName.Value);
                decimal num = DataConverter.CDecimal(totalInComeAndPayOutAll[0]);
                decimal num2 = DataConverter.CDecimal(totalInComeAndPayOutAll[1]);
                footerRow.HorizontalAlign = HorizontalAlign.Right;
                footerRow.Cells[0].ColumnSpan = 3;
                footerRow.Cells[3].ColumnSpan = 2;
                footerRow.Cells[0].Text = "本页合计：<br />总计金额：";
                footerRow.Cells[1].Text = this.m_CurrentPageIncome.ToString("N2") + "<br />" + num.ToString("N2");
                footerRow.Cells[2].Text = this.m_CurrentPagePayout.ToString("N2") + "<br />" + Math.Abs(num2).ToString("N2");
                footerRow.Cells[3].HorizontalAlign = HorizontalAlign.Center;
                footerRow.Cells[3].Text = "&nbsp;<br />资金余额：" + ((num + num2)).ToString("N2");
            }
        }

        protected void EgvBankroll_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                BankrollItemInfo dataItem = e.Row.DataItem as BankrollItemInfo;
                if (dataItem != null)
                {
                    if (dataItem.Status == BankrollItemStatus.Confirm)
                    {
                        if (dataItem.Money > 0M)
                        {
                            this.m_CurrentPageIncome += dataItem.Money;
                        }
                        else
                        {
                            this.m_CurrentPagePayout += Math.Abs(dataItem.Money);
                        }
                    }
                    Label label = e.Row.FindControl("LblRemark") as Label;
                    if (label != null)
                    {
                        dataItem.Remark = dataItem.Remark;
                        label.Text = StringHelper.SubString(dataItem.Remark, 0x2e, "...");
                        label.ToolTip = dataItem.Remark;
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.HdnUserName.Value = PEContext.Current.User.UserName;
            }
        }
    }
}

