namespace EasyOne.WebSite.Controls
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Collections;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class PointLog : BaseUserControl
    {
        private int m_CurrentPageIncome;
        private int m_CurrentPagePayout;
        private string m_PointName = SiteConfig.UserConfig.PointName;
        private int m_ShowType;

        private static Table CreateTable(string rowOneText, string rowTwoText, string align)
        {
            Table table = new Table();
            table.Width = Unit.Percentage(100.0);
            TableRow row = new TableRow();
            TableCell cell = new TableCell();
            cell.Attributes.Add("align", align);
            row.Cells.Add(cell);
            TableRow row2 = new TableRow();
            TableCell cell2 = new TableCell();
            cell2.Attributes.Add("align", align);
            row2.Cells.Add(cell2);
            table.Rows.Add(row);
            table.Rows.Add(row2);
            table.Rows[0].Cells[0].Text = rowOneText;
            table.Rows[1].Cells[0].Text = rowTwoText;
            return table;
        }

        protected void DropCurrentPage_DataBind()
        {
            ArrayList list = new ArrayList();
            for (int i = 1; i <= this.EgvUserPoint.PageCount; i++)
            {
                list.Add(i);
            }
            DropDownList list2 = this.EgvUserPoint.BottomPagerRow.FindControl("DropCurrentPage") as DropDownList;
            list2.DataSource = list;
            list2.DataBind();
            if (this.EgvUserPoint.PageIndex > 0)
            {
                list2.SelectedIndex = this.EgvUserPoint.PageIndex;
            }
        }

        protected void EgvUserPoint_DataBound(object sender, EventArgs e)
        {
            if (this.EgvUserPoint.Rows.Count > 0)
            {
                int num2;
                int num3;
                GridViewRow footerRow = this.EgvUserPoint.FooterRow;
                int num = 3;
                while (footerRow.Cells.Count != num)
                {
                    footerRow.Cells.RemoveAt(0);
                }
                footerRow.CssClass = this.EgvUserPoint.RowStyle.CssClass;
                if (this.m_ShowType == 1)
                {
                    footerRow.Cells[1].ColumnSpan = 3;
                    footerRow.Cells[2].ColumnSpan = 3;
                    ArrayList totalInComeAndPayOutAll = UserPointLog.GetTotalInComeAndPayOutAll(PEContext.Current.User.UserName);
                    num2 = DataConverter.CLng(totalInComeAndPayOutAll[0]);
                    num3 = DataConverter.CLng(totalInComeAndPayOutAll[1]);
                    footerRow.Cells[1].Controls.Add(CreateTable("本页合计 收入：" + this.m_CurrentPageIncome.ToString("D") + "&nbsp;&nbsp;支出：" + Math.Abs(this.m_CurrentPagePayout).ToString("D"), "总计" + this.m_PointName + " 收入：" + Math.Abs(num2).ToString("D") + "&nbsp;&nbsp;支出：" + Math.Abs(num3).ToString("D"), "right"));
                    Table child = CreateTable("&nbsp;&nbsp;", string.Format(this.m_PointName + "余额：{0:0}", num2 - num3), "right");
                    child.Rows[1].Cells[0].Attributes.Add("align", "center");
                    footerRow.Cells[2].Controls.Add(child);
                }
                else
                {
                    footerRow.Cells[0].ColumnSpan = 3;
                    footerRow.Cells[1].ColumnSpan = 3;
                    footerRow.Cells[2].ColumnSpan = 2;
                    ArrayList list2 = UserPointLog.GetTotalInComeAndPayOutAll();
                    num2 = DataConverter.CLng(list2[0]);
                    num3 = DataConverter.CLng(list2[1]);
                    footerRow.Cells[0].Controls.Add(CreateTable("本页合计", "总计" + this.m_PointName, "right"));
                    footerRow.Cells[1].Controls.Add(CreateTable("收入：" + this.m_CurrentPageIncome.ToString("D") + "&nbsp;&nbsp;支出：" + Math.Abs(this.m_CurrentPagePayout).ToString("D"), "收入：" + Math.Abs(num2).ToString("D") + "&nbsp;&nbsp;支出：" + Math.Abs(num3).ToString("D"), "left"));
                    Table table2 = CreateTable("&nbsp;&nbsp;", string.Format(this.m_PointName + "余额：{0:0}", num2 - num3), "right");
                    table2.Rows[1].Cells[0].Attributes.Add("align", "center");
                    footerRow.Cells[2].Controls.Add(table2);
                }
            }
        }

        protected void EgvUserPoint_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                UserPointLogInfo dataItem = (UserPointLogInfo) e.Row.DataItem;
                if (dataItem != null)
                {
                    HyperLink link = (HyperLink) e.Row.FindControl("HypUserName");
                    if (this.m_ShowType == 1)
                    {
                        link.NavigateUrl = "~/User/Default.aspx?UserName=" + dataItem.UserName;
                    }
                    else
                    {
                        link.NavigateUrl = "~/Admin/User/UserShow.aspx?UserName=" + dataItem.UserName;
                    }
                    link.Text = dataItem.UserName;
                    Label label = (Label) e.Row.FindControl("LblIncomePayOut");
                    label.Text = this.GetIncomePayOut(dataItem.Point, dataItem.IncomePayOut);
                }
            }
        }

        public string GetIncomePayOut(int point, int incomePayOut)
        {
            StringBuilder builder = new StringBuilder();
            if (incomePayOut == 1)
            {
                builder.Append(" <span style='color:blue'> +" + point.ToString("D") + "</span>");
                this.m_CurrentPageIncome += point;
            }
            else
            {
                string str = point.ToString("D");
                builder.Append(" <span style='color:red'>");
                if (str == "0")
                {
                    builder.Append(str);
                }
                else
                {
                    builder.Append("-" + str);
                }
                builder.Append("</span>");
                this.m_CurrentPagePayout += point;
            }
            return builder.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.EgvUserPoint.Columns[3].HeaderText = SiteConfig.UserConfig.PointName + "数";
            if (string.IsNullOrEmpty(this.HdnField.Value))
            {
                this.HdnField.Value = BaseUserControl.RequestInt32("Field").ToString();
            }
            if (string.IsNullOrEmpty(this.HdnKeyword.Value))
            {
                this.HdnKeyword.Value = BaseUserControl.RequestString("KeyWord");
            }
            if (string.IsNullOrEmpty(this.HdnSearchType.Value))
            {
                this.HdnSearchType.Value = BaseUserControl.RequestInt32("SearchType").ToString();
            }
            if (this.m_ShowType == 1)
            {
                this.EgvUserPoint.Columns[1].Visible = false;
                this.EgvUserPoint.Columns[5].Visible = false;
            }
        }

        public int Field
        {
            get
            {
                return DataConverter.CLng(this.HdnField.Value);
            }
            set
            {
                this.HdnField.Value = value.ToString();
            }
        }

        public string Keyword
        {
            get
            {
                return this.HdnKeyword.Value;
            }
            set
            {
                this.HdnKeyword.Value = value;
            }
        }

        public int SearchType
        {
            get
            {
                return DataConverter.CLng(this.HdnSearchType.Value);
            }
            set
            {
                this.HdnSearchType.Value = value.ToString();
            }
        }

        public int ShowType
        {
            get
            {
                if ((this.m_ShowType != 1) && string.IsNullOrEmpty(PEContext.Current.Admin.AdminName))
                {
                    this.m_ShowType = 1;
                }
                return this.m_ShowType;
            }
            set
            {
                this.m_ShowType = value;
            }
        }
    }
}

