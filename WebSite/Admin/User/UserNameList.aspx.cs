namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.Components;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class UserNameList : AdminPage
    {
        private string m_Keyword;
        private int m_MaxiNumRows;
        private int m_PageCount;
        private int m_SearchType;
        private int m_Select;
        private int m_StartRowIndexId;

        private void BindData()
        {
            if (string.Compare(BasePage.RequestStringToLower("Select"), "single", StringComparison.OrdinalIgnoreCase) == 0)
            {
                this.m_Select = 0;
            }
            else
            {
                this.m_Select = 1;
            }
            if (string.IsNullOrEmpty(this.m_Keyword))
            {
                this.m_SearchType = 0;
                this.m_Keyword = "1";
            }
            IList<string> list = Users.GetUserNameList(this.m_StartRowIndexId, this.m_MaxiNumRows, this.m_SearchType, this.m_Keyword);
            Literal child = new Literal();
            StringBuilder builder = new StringBuilder("");
            builder.Append("<table cellSpacing='1' cellPadding='1' width='80%' align='center' border='0' style='background: #000000;'>");
            if (list.Count <= 0)
            {
                child.Text = "<li>没有任何用户</li>";
            }
            else
            {
                int num = list.Count / 5;
                int num2 = list.Count % 5;
                for (int i = 0; i < num; i++)
                {
                    builder.Append("<tr style='height:20px;'>");
                    builder.Append(string.Concat(new object[] { "<td style='background: #e6eff8;' width='20%'><a href='#' style='color:Blue' onclick=\"add('", list[5 * i], "',", this.m_Select, ")\">", list[5 * i], "</a></td>" }));
                    builder.Append(string.Concat(new object[] { "<td style='background: #e6eff8;' width='20%'><a href='#' style='color:Blue' onclick=\"add('", list[1 + (5 * i)], "',", this.m_Select, ")\">", list[1 + (5 * i)], "</a></td>" }));
                    builder.Append(string.Concat(new object[] { "<td style='background: #e6eff8;' width='20%'><a href='#' style='color:Blue' onclick=\"add('", list[2 + (5 * i)], "',", this.m_Select, ")\">", list[2 + (5 * i)], "</a></td>" }));
                    builder.Append(string.Concat(new object[] { "<td style='background: #e6eff8;' width='20%'><a href='#' style='color:Blue' onclick=\"add('", list[3 + (5 * i)], "',", this.m_Select, ")\">", list[3 + (5 * i)], "</a></td>" }));
                    builder.Append(string.Concat(new object[] { "<td style='background: #e6eff8;' width='20%'><a href='#' style='color:Blue' onclick=\"add('", list[4 + (5 * i)], "',", this.m_Select, ")\">", list[4 + (5 * i)], "</a></td></tr>" }));
                }
                if (num2 != 0)
                {
                    builder.Append("<tr style='height:20px;'>");
                    for (int j = 0; j < num2; j++)
                    {
                        builder.Append(string.Concat(new object[] { "<td style='background: #e6eff8;' width='20%'><a href='#' style='color:Blue' onclick=\"add('", list[j + (5 * num)], "',", this.m_Select, ")\">", list[j + (5 * num)], "</a></td>" }));
                    }
                    for (int k = 0; k < (5 - num2); k++)
                    {
                        builder.Append("<td style='background: #e6eff8;' width='20%'></td>");
                    }
                    builder.Append("</tr>");
                }
                builder.Append("</table>");
                child.Text = builder.ToString();
            }
            this.PlhUserList.Controls.Clear();
            this.PlhUserList.Controls.Add(child);
            int userNameListTotal = Users.GetUserNameListTotal(this.m_SearchType, this.m_Keyword);
            if ((userNameListTotal % this.m_MaxiNumRows) > 0)
            {
                this.m_PageCount = (userNameListTotal / this.m_MaxiNumRows) + 1;
            }
            else
            {
                this.m_PageCount = userNameListTotal / this.m_MaxiNumRows;
            }
            this.LblMaxPage.Text = this.m_PageCount.ToString();
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            this.m_SearchType = 1;
            this.m_Keyword = this.TxtSearchUser.Text;
            this.BindData();
        }

        protected void DlstGroup_ItemCommand(object source, CommandEventArgs e)
        {
            this.m_SearchType = 0;
            this.m_Keyword = e.CommandArgument.ToString();
            this.m_StartRowIndexId = 0;
            this.BindData();
        }

        protected void LbtnFirst_Click(object sender, EventArgs e)
        {
            this.m_StartRowIndexId = 0;
            this.ViewState["currentPage"] = 1;
            this.LbtnPrevious.Enabled = false;
            if (this.m_PageCount > 1)
            {
                this.LbtnNext.Enabled = true;
            }
            else
            {
                this.LbtnNext.Enabled = false;
            }
            this.LblCurrenPage.Text = this.ViewState["currentPage"].ToString();
            this.BindData();
        }

        protected void LbtnLast_Click(object sender, EventArgs e)
        {
            this.m_StartRowIndexId = (this.m_PageCount - 1) * this.m_MaxiNumRows;
            this.ViewState["currentPage"] = this.m_PageCount;
            this.LbtnNext.Enabled = false;
            if (this.m_PageCount > 1)
            {
                this.LbtnPrevious.Enabled = true;
            }
            else
            {
                this.LbtnPrevious.Enabled = false;
            }
            this.LblCurrenPage.Text = this.ViewState["currentPage"].ToString();
            this.BindData();
        }

        protected void LbtnNext_Click(object sender, EventArgs e)
        {
            this.LbtnPrevious.Enabled = true;
            this.m_StartRowIndexId = Convert.ToInt32(this.ViewState["currentPage"]) * this.m_MaxiNumRows;
            this.ViewState["currentPage"] = Convert.ToInt32(this.ViewState["currentPage"]) + 1;
            if (Convert.ToInt32(this.ViewState["currentPage"]) == this.m_PageCount)
            {
                this.ViewState["currentPage"] = this.m_PageCount;
                this.LbtnNext.Enabled = false;
            }
            else
            {
                this.LbtnNext.Enabled = true;
            }
            this.LblCurrenPage.Text = this.ViewState["currentPage"].ToString();
            this.BindData();
        }

        protected void LbtnPrevious_Click(object sender, EventArgs e)
        {
            this.LbtnNext.Enabled = true;
            this.m_StartRowIndexId = (Convert.ToInt32(this.ViewState["currentPage"]) - 2) * this.m_MaxiNumRows;
            this.ViewState["currentPage"] = Convert.ToInt32(this.ViewState["currentPage"]) - 1;
            if (Convert.ToInt32(this.ViewState["currentPage"]) == 1)
            {
                this.ViewState["currentPage"] = 1;
                this.LbtnPrevious.Enabled = false;
            }
            else
            {
                this.LbtnPrevious.Enabled = true;
            }
            this.LblCurrenPage.Text = this.ViewState["currentPage"].ToString();
            this.BindData();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!PEContext.Current.Admin.Identity.IsAuthenticated)
            {
                AdminPage.WriteErrMsg("<li>对不起，您还没有登录不能访问此页面。</li>");
            }
            this.m_MaxiNumRows = string.IsNullOrEmpty(this.TxtUserNameNum.Text) ? 50 : Convert.ToInt32(this.TxtUserNameNum.Text);
            if (this.ViewState["currentPage"] == null)
            {
                this.ViewState["currentPage"] = 1;
                this.LblCurrenPage.Text = "1";
                this.LbtnPrevious.Enabled = false;
            }
            this.BindData();
            if (this.m_PageCount > 1)
            {
                this.LbtnNext.Enabled = true;
            }
            else
            {
                this.LbtnNext.Enabled = false;
            }
        }

        protected void TxtPage_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(this.TxtPage.Text) >= this.m_PageCount)
            {
                this.m_StartRowIndexId = (this.m_PageCount - 1) * this.m_MaxiNumRows;
                this.LblCurrenPage.Text = this.m_PageCount.ToString();
                this.LbtnFirst.Enabled = true;
                this.LbtnPrevious.Enabled = true;
                this.LbtnNext.Enabled = false;
                this.LbtnLast.Enabled = false;
            }
            else
            {
                if ((Convert.ToInt32(this.TxtPage.Text) == 0) || (Convert.ToInt32(this.TxtPage.Text) == 1))
                {
                    this.m_StartRowIndexId = 0;
                    this.LblCurrenPage.Text = "1";
                    this.LbtnFirst.Enabled = false;
                    this.LbtnPrevious.Enabled = false;
                }
                else
                {
                    this.m_StartRowIndexId = (Convert.ToInt32(this.TxtPage.Text) - 1) * this.m_MaxiNumRows;
                    this.LblCurrenPage.Text = this.TxtPage.Text;
                    this.LbtnFirst.Enabled = true;
                    this.LbtnPrevious.Enabled = true;
                }
                this.LbtnNext.Enabled = true;
                this.LbtnLast.Enabled = true;
            }
            this.BindData();
        }
    }
}

