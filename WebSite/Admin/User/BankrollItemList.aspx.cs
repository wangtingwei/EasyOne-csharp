namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.Accessories;
    using EasyOne.Web.UI;
    using System;
    using System.Collections;
    using System.Web.UI.WebControls;

    public partial class BankrollItemList : AdminPage
    {
        private decimal m_CurrentPageIncome;
        private decimal m_CurrentPagePayout;

        protected void EgdvBankrollItem_DataBound(object sender, EventArgs e)
        {
            if (this.EgdvBankrollItem.Rows.Count > 0)
            {
                GridViewRow footerRow = this.EgdvBankrollItem.FooterRow;
                while (footerRow.Cells.Count != 4)
                {
                    footerRow.Cells.RemoveAt(0);
                }
                footerRow.CssClass = this.EgdvBankrollItem.RowStyle.CssClass;
                footerRow.HorizontalAlign = HorizontalAlign.Right;
                ArrayList totalInComeAndPayOutAll = BankrollItem.GetTotalInComeAndPayOutAll();
                decimal num = DataConverter.CDecimal(totalInComeAndPayOutAll[0]);
                decimal num2 = DataConverter.CDecimal(totalInComeAndPayOutAll[1]);
                footerRow.Cells[0].ColumnSpan = 5;
                footerRow.Cells[3].ColumnSpan = 4;
                footerRow.Cells[0].Text = "本页合计：<br/>总计金额：";
                footerRow.Cells[1].Text = this.m_CurrentPageIncome.ToString("N2") + "<br/>" + num.ToString("N2");
                footerRow.Cells[2].Text = Math.Abs(this.m_CurrentPagePayout).ToString("N2") + "<br/>" + Math.Abs(num2).ToString("N2");
            }
        }

        protected void EgdvBankrollItem_RowCommand(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Confirm")
            {
                BasePage.ResponseRedirect(base.BasePath + SiteConfig.SiteOption.ManageDir + "/Shop/RemittanceAdd.aspx?Action=Confirm&ItemID=" + DataConverter.CLng(e.CommandArgument).ToString());
            }
        }

        protected void EgdvBankrollItem_RowDataBound(object sender, GridViewRowEventArgs e)
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
                            this.m_CurrentPagePayout += dataItem.Money;
                        }
                    }
                    else
                    {
                        LinkButton button = (LinkButton) e.Row.FindControl("LBtnDel");
                        button.OnClientClick = "return confirm('是否确定要删除这条汇款通知记录？')";
                    }
                    Label label = e.Row.FindControl("LblRemark") as Label;
                    if (label != null)
                    {
                        label.Text = StringHelper.SubString(dataItem.Remark, 30, "...");
                        label.ToolTip = dataItem.Remark;
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                int searchType = BasePage.RequestInt32("SearchType");
                int field = BasePage.RequestInt32("Field");
                string keyword = BasePage.RequestString("Keyword");
                this.SmpNavigator.AdditionalNode = BankrollItem.GetCurrentNode(searchType, field, keyword);
            }
        }
    }
}

