namespace EasyOne.WebSite.Controls
{
    using EasyOne.Common;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class InputerMonthControl : BaseUserControl
    {

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                DateTime endDate = DataConverter.CDate(this.DpkEndDate.Text);
                DateTime beginDate = DataConverter.CDate(this.DpkStartDate.Text);
                DataTable dataTable = ContentManage.GetCountByInputerAndMonth(DataConverter.CLng(this.DrpCategory.SelectedValue), this.DrpInputer.SelectedValue, beginDate, endDate);
                StringBuilder sb = new StringBuilder("<table class=\"border\" id=\"statistics\" width=\"100%\" cellspacing=\"1\" cellpadding=\"2\" border=\"0\">");
                int dateIntervalMonth = this.GetDateIntervalMonth(beginDate, endDate);
                IList<UserInfo> userList = new List<UserInfo>();
                if (string.IsNullOrEmpty(this.DrpInputer.SelectedValue))
                {
                    userList = Users.GetUsersByPost();
                }
                else
                {
                    UserInfo item = new UserInfo();
                    item.UserName = this.DrpInputer.SelectedValue;
                    userList.Add(item);
                }
                this.BuildTableHead(sb, dateIntervalMonth, beginDate);
                this.BuildTableBody(sb, dateIntervalMonth, beginDate, dataTable, userList);
                this.BuildTableFooter(sb, dateIntervalMonth);
                sb.Append("</table>");
                this.SpanCount.InnerHtml = sb.ToString();
            }
        }

        private void BuildTableBody(StringBuilder sb, int length, DateTime startDate, DataTable dataTable, IList<UserInfo> userList)
        {
            foreach (UserInfo info in userList)
            {
                this.BuildTableTd(sb, length, startDate, dataTable, info.UserName);
            }
        }

        private void BuildTableFooter(StringBuilder sb, int length)
        {
            sb.Append("<tr class=\"tdbg\" align=\"center\" ><td >合计</td>");
            for (int i = 0; i <= length; i++)
            {
                sb.Append("<td></td>");
            }
            sb.Append("<td></td></tr>");
        }

        private void BuildTableHead(StringBuilder sb, int length, DateTime startDate)
        {
            sb.Append("<tr class='title'><td align=\"center\">录入者</td>");
            for (int i = 0; i <= length; i++)
            {
                sb.Append("<td>");
                string str = startDate.AddMonths(i).ToString("yyyy-MM");
                sb.Append(str);
                sb.Append("</td>");
            }
            sb.Append("<td>合计</td></tr>");
        }

        private void BuildTableTd(StringBuilder sb, int length, DateTime startDate, DataTable dataTable, string userName)
        {
            int num = 0;
            sb.Append("<tr class=\"tdbg\" align=\"center\">");
            sb.Append("<td>");
            sb.Append(userName);
            sb.Append("</td>");
            for (int i = 0; i <= length; i++)
            {
                string str = string.Empty;
                sb.Append("<td>");
                DataRow[] rowArray = dataTable.Select("InputTime='" + startDate.AddMonths(i).ToString("yyyy-M") + "'");
                if (rowArray.Length > 0)
                {
                    for (int j = 0; j < rowArray.Length; j++)
                    {
                        if (string.Compare(rowArray[j]["Inputer"].ToString(), userName, StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            num += DataConverter.CLng(rowArray[j]["Count"]);
                            str = rowArray[j]["Count"].ToString();
                            sb.Append(str);
                        }
                    }
                }
                if (string.IsNullOrEmpty(str))
                {
                    sb.Append("0");
                }
                sb.Append("</td>");
            }
            sb.Append("<td >");
            sb.Append(num.ToString());
            sb.Append("</td>");
            sb.Append("</tr>");
        }

        private int GetDateIntervalMonth(DateTime startDate, DateTime endDate)
        {
            return (((endDate.Year - startDate.Year) * 12) + Math.Abs((int) (endDate.Month - startDate.Month)));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.DrpCategory.DataSource = EasyOne.Contents.Nodes.GetNodeNameForContainerItems();
                this.DrpCategory.DataTextField = "NodeName";
                this.DrpCategory.DataValueField = "NodeId";
                this.DrpCategory.DataBind();
                this.DrpCategory.Items.Insert(0, new ListItem("所有栏目", "0"));
                this.DrpInputer.DataSource = Users.GetUsersByPost();
                this.DrpInputer.DataTextField = "UserName";
                this.DrpInputer.DataValueField = "UserName";
                this.DrpInputer.DataBind();
                this.DrpInputer.Items.Insert(0, new ListItem("所有录入者", ""));
                this.DpkStartDate.Text = DateTime.Now.ToString("yyyy") + "-01-01";
                this.DpkEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }
    }
}

