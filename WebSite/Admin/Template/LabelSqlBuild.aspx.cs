namespace EasyOne.WebSite.Admin.Template
{
    using AjaxControlToolkit;
    using CodeEngine.Framework.QueryBuilder;
    using CodeEngine.Framework.QueryBuilder.Enums;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.TemplateProc;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.Data;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Configuration;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class LabelSqlBuild : AdminPage
    {
        private string action;
        private string Dbtype;
        private string m_LabelLibPath;
        private string m_LabelName;
        private string xmlfilepath;
        private SelectQueryBuilder query;
        private SelectQueryBuilder querycount;

        protected void BtnClausebilud_Click(object sender, EventArgs e)
        {
            if (this.DbTableDownList.SelectedIndex == 0)
            {
                this.TxtSqlstr.Text = "您尚未选择主表";
            }
            else
            {
                bool flag = true;
                DataTable table = GetDataBaseSchema(this.Dbtype, this.xmlfilepath, this.DbTableDownList.SelectedValue);
                DataTable table2 = table.Clone();
                for (int i = 0; i < this.DbFieldDownList.Items.Count; i++)
                {
                    if (this.DbFieldDownList.Items[i].Selected)
                    {
                        DataRow[] rowArray = table.Select("ColumnName = '" + this.DbFieldDownList.Items[i].Value + "'");
                        for (int k = 0; k < rowArray.Length; k++)
                        {
                            table2.ImportRow(rowArray[k]);
                            flag = false;
                        }
                    }
                }
                if (flag)
                {
                    table2 = table;
                }
                if (this.DbTableDownList2.SelectedIndex > 0)
                {
                    flag = true;
                    table = GetDataBaseSchema(this.Dbtype, this.xmlfilepath, this.DbTableDownList2.SelectedValue);
                    for (int m = 0; m < this.DbFieldDownList2.Items.Count; m++)
                    {
                        if (this.DbFieldDownList2.Items[m].Selected)
                        {
                            DataRow[] rowArray2 = table.Select("ColumnName = '" + this.DbFieldDownList2.Items[m].Value + "'");
                            for (int n = 0; n < rowArray2.Length; n++)
                            {
                                table2.ImportRow(rowArray2[n]);
                                flag = false;
                            }
                        }
                    }
                    if (flag)
                    {
                        for (int num5 = 0; num5 < table.Rows.Count; num5++)
                        {
                            table2.ImportRow(table.Rows[num5]);
                        }
                    }
                }
                this.GridView_Clause.DataSource = table2;
                this.GridView_Clause.DataBind();
                for (int j = 0; j < this.GridView_Clause.Rows.Count; j++)
                {
                    TextBox box = (TextBox) this.GridView_Clause.Rows[j].FindControl("txtbox1");
                    box.Attributes.Add("onmouseup", "dragend(0);");
                }
            }
        }

        protected void BtnNext_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.TxtSqlstr.Text) && (this.TxtSqlstr.Text != "请先建立SQL语句"))
            {
                bool flag = XmlManage.SaveFileNode(this.xmlfilepath, "root", "LabelSqlString", this.TxtSqlstr.Text);
                if (this.ChkPage.Checked)
                {
                    if (!string.IsNullOrEmpty(this.TxtSqlCount.Text) && (this.TxtSqlCount.Text != "请先建立SQL统计语句"))
                    {
                        flag = XmlManage.SaveFileNode(this.xmlfilepath, "root", "LabelSqlCount", this.TxtSqlCount.Text);
                    }
                    else
                    {
                        flag = false;
                        this.TxtSqlCount.Text = "请先建立SQL统计语句";
                    }
                    if ((string.Compare(this.Dbtype.Split(new char[] { '_' })[0], "sql", StringComparison.Ordinal) != 0) && flag)
                    {
                        if (!string.IsNullOrEmpty(this.TxtSqlPage.Text) && (this.TxtSqlPage.Text != "请先建立SQL分页语句"))
                        {
                            flag = XmlManage.SaveFileNode(this.xmlfilepath, "root", "LabelSqlPage", this.TxtSqlPage.Text);
                        }
                        else
                        {
                            flag = false;
                            this.TxtSqlPage.Text = "请先建立SQL分页语句";
                        }
                    }
                }
                if (flag)
                {
                    BasePage.ResponseRedirect("LabelTemplate.aspx?action=" + this.action + "&name=" + base.Server.UrlEncode(this.m_LabelName));
                }
            }
            else
            {
                this.TxtSqlstr.Text = "请先建立SQL语句";
            }
        }

        protected void BtnPrv_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("LabelProperty.aspx?action=" + this.action + "&name=" + base.Server.UrlEncode(this.m_LabelName));
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.TxtSqlstr.Text) && (this.TxtSqlstr.Text != "请先建立SQL语句"))
                {
                    bool flag = XmlManage.SaveFileNode(this.xmlfilepath, "root", "LabelSqlString", this.TxtSqlstr.Text);
                    if (this.ChkPage.Checked)
                    {
                        if (!string.IsNullOrEmpty(this.TxtSqlCount.Text) && (this.TxtSqlCount.Text != "请先建立SQL统计语句"))
                        {
                            flag = XmlManage.SaveFileNode(this.xmlfilepath, "root", "LabelSqlCount", this.TxtSqlCount.Text);
                        }
                        else
                        {
                            flag = false;
                            this.TxtSqlCount.Text = "请先建立SQL统计语句";
                        }
                        if ((string.Compare(this.Dbtype.Split(new char[] { '_' })[0], "sql", StringComparison.Ordinal) != 0) && flag)
                        {
                            if (!string.IsNullOrEmpty(this.TxtSqlPage.Text) && (this.TxtSqlPage.Text != "请先建立SQL分页语句"))
                            {
                                flag = XmlManage.SaveFileNode(this.xmlfilepath, "root", "LabelSqlPage", this.TxtSqlPage.Text);
                            }
                            else
                            {
                                flag = false;
                                this.TxtSqlPage.Text = "请先建立SQL分页语句";
                            }
                            flag = XmlManage.SaveFileNode(this.xmlfilepath, "root", "LabelSqlPage", this.TxtSqlPage.Text);
                        }
                    }
                    if (flag)
                    {
                        File.Copy(this.xmlfilepath, HttpContext.Current.Server.MapPath(this.m_LabelLibPath) + @"\" + this.m_LabelName + ".config", true);
                        BasePage.ResponseRedirect("LabelManage.aspx");
                    }
                }
                else
                {
                    this.TxtSqlstr.Text = "请先建立SQL语句";
                }
            }
            catch (IOException)
            {
                AdminPage.WriteErrMsg("没有标签目录或临时目录或标签文件的访问权限！", "LabelManage.aspx");
            }
            catch (UnauthorizedAccessException)
            {
                AdminPage.WriteErrMsg("没有标签目录或临时目录或标签文件的访问权限！", "LabelManage.aspx");
            }
        }

        protected void BtnSqlbilud_Click(object sender, EventArgs e)
        {
            this.query = new SelectQueryBuilder();
            this.querycount = new SelectQueryBuilder();
            if (this.DbTableDownList.SelectedIndex > 0)
            {
                bool flag = true;
                bool flag2 = true;
                bool flag3 = false;
                bool flag4 = false;
                if (string.Compare(this.Dbtype, "orc_read", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    flag3 = true;
                }
                else if ((string.Compare(this.Dbtype, "odbc_read", StringComparison.OrdinalIgnoreCase) == 0) && Regex.IsMatch(XmlManage.ReadFileNode(this.xmlfilepath, "root/LabelType"), "mysql", RegexOptions.IgnoreCase))
                {
                    flag4 = true;
                }
                string input = XmlManage.ReadFileNode(this.xmlfilepath, "root/LabelDbPath");
                if ((string.Compare(this.Dbtype, "ole_read", StringComparison.OrdinalIgnoreCase) == 0) && Regex.IsMatch(input, "xls", RegexOptions.IgnoreCase))
                {
                    this.query.SelectFromTable("[" + this.DbTableDownList.SelectedValue + "]");
                    this.querycount.SelectFromTable("[" + this.DbTableDownList.SelectedValue + "]");
                }
                else
                {
                    this.query.SelectFromTable(this.DbTableDownList.SelectedValue);
                    this.querycount.SelectFromTable(this.DbTableDownList.SelectedValue);
                }
                StringBuilder builder = new StringBuilder();
                if ((this.DbTableDownList2.SelectedIndex <= 0) || (this.DbTableDownList.SelectedIndex == this.DbTableDownList2.SelectedIndex))
                {
                    for (int i = 0; i < this.DbFieldDownList.Items.Count; i++)
                    {
                        if (this.DbFieldDownList.Items[i].Selected)
                        {
                            builder.Append(this.DbFieldDownList.Items[i].Value + ",");
                            flag = false;
                        }
                    }
                    if (flag)
                    {
                        this.query.SelectAllColumns();
                    }
                    else
                    {
                        this.query.SelectColumns(builder.ToString().Split(new char[] { ',' }));
                    }
                    this.querycount.SelectColumn("count(*)");
                    this.QueryProc(-1);
                }
                else
                {
                    string selectedValue = this.Dbtj.SelectedValue;
                    if (selectedValue != null)
                    {
                        if (!(selectedValue == "InnerJoin"))
                        {
                            if (selectedValue == "LeftJoin")
                            {
                                if (this.Dbys.SelectedValue == "=")
                                {
                                    this.query.AddJoin(JoinType.LeftJoin, this.DbTableDownList2.SelectedValue, this.DbFieldList2.SelectedValue, Comparison.Equals, this.DbTableDownList.SelectedValue, this.DbFieldList.SelectedValue);
                                    this.querycount.AddJoin(JoinType.LeftJoin, this.DbTableDownList2.SelectedValue, this.DbFieldList2.SelectedValue, Comparison.Equals, this.DbTableDownList.SelectedValue, this.DbFieldList.SelectedValue);
                                }
                                else
                                {
                                    this.query.AddJoin(JoinType.LeftJoin, this.DbTableDownList2.SelectedValue, this.DbFieldList2.SelectedValue, Comparison.Like, this.DbTableDownList.SelectedValue, this.DbFieldList.SelectedValue);
                                    this.querycount.AddJoin(JoinType.LeftJoin, this.DbTableDownList2.SelectedValue, this.DbFieldList2.SelectedValue, Comparison.Like, this.DbTableDownList.SelectedValue, this.DbFieldList.SelectedValue);
                                }
                            }
                            else if (selectedValue == "OuterJoin")
                            {
                                if (this.Dbys.SelectedValue == "=")
                                {
                                    this.query.AddJoin(JoinType.OuterJoin, this.DbTableDownList2.SelectedValue, this.DbFieldList2.SelectedValue, Comparison.Equals, this.DbTableDownList.SelectedValue, this.DbFieldList.SelectedValue);
                                    this.querycount.AddJoin(JoinType.OuterJoin, this.DbTableDownList2.SelectedValue, this.DbFieldList2.SelectedValue, Comparison.Equals, this.DbTableDownList.SelectedValue, this.DbFieldList.SelectedValue);
                                }
                                else
                                {
                                    this.query.AddJoin(JoinType.OuterJoin, this.DbTableDownList2.SelectedValue, this.DbFieldList2.SelectedValue, Comparison.Like, this.DbTableDownList.SelectedValue, this.DbFieldList.SelectedValue);
                                    this.querycount.AddJoin(JoinType.OuterJoin, this.DbTableDownList2.SelectedValue, this.DbFieldList2.SelectedValue, Comparison.Like, this.DbTableDownList.SelectedValue, this.DbFieldList.SelectedValue);
                                }
                            }
                            else if (selectedValue == "RightJoin")
                            {
                                if (this.Dbys.SelectedValue == "=")
                                {
                                    this.query.AddJoin(JoinType.RightJoin, this.DbTableDownList2.SelectedValue, this.DbFieldList2.SelectedValue, Comparison.Equals, this.DbTableDownList.SelectedValue, this.DbFieldList.SelectedValue);
                                    this.querycount.AddJoin(JoinType.RightJoin, this.DbTableDownList2.SelectedValue, this.DbFieldList2.SelectedValue, Comparison.Equals, this.DbTableDownList.SelectedValue, this.DbFieldList.SelectedValue);
                                }
                                else
                                {
                                    this.query.AddJoin(JoinType.RightJoin, this.DbTableDownList2.SelectedValue, this.DbFieldList2.SelectedValue, Comparison.Like, this.DbTableDownList.SelectedValue, this.DbFieldList.SelectedValue);
                                    this.querycount.AddJoin(JoinType.RightJoin, this.DbTableDownList2.SelectedValue, this.DbFieldList2.SelectedValue, Comparison.Like, this.DbTableDownList.SelectedValue, this.DbFieldList.SelectedValue);
                                }
                            }
                        }
                        else if (this.Dbys.SelectedValue == "=")
                        {
                            this.query.AddJoin(JoinType.InnerJoin, this.DbTableDownList2.SelectedValue, this.DbFieldList2.SelectedValue, Comparison.Equals, this.DbTableDownList.SelectedValue, this.DbFieldList.SelectedValue);
                            this.querycount.AddJoin(JoinType.InnerJoin, this.DbTableDownList2.SelectedValue, this.DbFieldList2.SelectedValue, Comparison.Equals, this.DbTableDownList.SelectedValue, this.DbFieldList.SelectedValue);
                        }
                        else
                        {
                            this.query.AddJoin(JoinType.InnerJoin, this.DbTableDownList2.SelectedValue, this.DbFieldList2.SelectedValue, Comparison.Like, this.DbTableDownList.SelectedValue, this.DbFieldList.SelectedValue);
                            this.querycount.AddJoin(JoinType.InnerJoin, this.DbTableDownList2.SelectedValue, this.DbFieldList2.SelectedValue, Comparison.Like, this.DbTableDownList.SelectedValue, this.DbFieldList.SelectedValue);
                        }
                    }
                    int irlist = 0;
                    for (int j = 0; j < this.DbFieldDownList.Items.Count; j++)
                    {
                        if (this.DbFieldDownList.Items[j].Selected)
                        {
                            builder.Append(this.DbTableDownList.SelectedValue + "." + this.DbFieldDownList.Items[j].Value + ",");
                            flag = false;
                            irlist++;
                        }
                    }
                    if (flag)
                    {
                        irlist = this.DbFieldDownList.Items.Count;
                        builder.Append(this.DbTableDownList.SelectedValue + ".*,");
                    }
                    for (int k = 0; k < this.DbFieldDownList2.Items.Count; k++)
                    {
                        if (this.DbFieldDownList2.Items[k].Selected)
                        {
                            builder.Append(this.DbTableDownList2.SelectedValue + "." + this.DbFieldDownList2.Items[k].Value + ",");
                            flag2 = false;
                        }
                    }
                    if (flag2)
                    {
                        builder.Append(this.DbTableDownList2.SelectedValue + ".*");
                    }
                    this.query.SelectColumns(builder.ToString().Split(new char[] { ',' }));
                    this.querycount.SelectColumn("count(*)");
                    this.QueryProc(irlist);
                }
                if (flag3)
                {
                    this.query.AddWhere("rownum", Comparison.LessOrEquals, DataConverter.CLng(this.TxtOutNum.Text), 0);
                }
                else if (!flag4)
                {
                    this.query.TopRecords = DataConverter.CLng(this.TxtOutNum.Text);
                }
                if (flag4)
                {
                    this.TxtSqlstr.Text = this.query.BuildQuery();
                    if (DataConverter.CLng(this.TxtOutNum.Text) > 0)
                    {
                        this.TxtSqlstr.Text = this.TxtSqlstr.Text + " limit " + this.TxtOutNum.Text;
                    }
                }
                else
                {
                    this.TxtSqlstr.Text = this.query.BuildQuery();
                }
                if ((string.Compare(this.Dbtype.Split(new char[] { '_' })[0], "sql", StringComparison.Ordinal) != 0) && !flag3)
                {
                    this.TxtSqlPage.Text = this.query.BuildQuery();
                }
            }
        }

        protected void ChkPage_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ChkPage.Checked)
            {
                if (XmlManage.SaveFileNode(this.xmlfilepath, "root", "UsePage", "True"))
                {
                    this.CountShow.Visible = true;
                    if ((string.Compare(this.Dbtype.Split(new char[] { '_' })[0], "sql", StringComparison.OrdinalIgnoreCase) != 0) && (string.Compare(this.Dbtype, "orc_read", StringComparison.OrdinalIgnoreCase) != 0))
                    {
                        this.PageShow.Visible = true;
                    }
                }
            }
            else if (XmlManage.SaveFileNode(this.xmlfilepath, "root", "UsePage", ""))
            {
                this.CountShow.Visible = false;
                this.PageShow.Visible = false;
            }
        }

        protected void DBTableDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.DbTableDownList.SelectedIndex > 0)
            {
                DataTable table = new DataTable();
                string dbtype = this.Dbtype;
                if (dbtype != null)
                {
                    if (!(dbtype == "sql_sysquery"))
                    {
                        if (dbtype == "sql_outquery")
                        {
                            table = LabelManage.GetSchemaTable(this.DbTableDownList.SelectedValue, XmlManage.ReadFileNode(this.xmlfilepath, "root/LabelDataSource"), DataSourceType.Sql);
                        }
                        else if (dbtype == "ole_read")
                        {
                            table = LabelManage.GetSchemaTable(this.DbTableDownList.SelectedValue, XmlManage.ReadFileNode(this.xmlfilepath, "root/LabelDataSource"), DataSourceType.Ole);
                        }
                        else if (dbtype == "odbc_read")
                        {
                            table = LabelManage.GetSchemaTable(this.DbTableDownList.SelectedValue, XmlManage.ReadFileNode(this.xmlfilepath, "root/LabelDataSource"), DataSourceType.Odbc);
                        }
                        else if (dbtype == "orc_read")
                        {
                            table = LabelManage.GetSchemaTable(this.DbTableDownList.SelectedValue, XmlManage.ReadFileNode(this.xmlfilepath, "root/LabelDataSource"), DataSourceType.Oracle);
                        }
                    }
                    else
                    {
                        table = LabelManage.GetSchemaTable(this.DbTableDownList.SelectedValue, string.Empty, DataSourceType.None);
                    }
                }
                this.DbFieldDownList.DataSource = table;
                this.DbFieldList.DataSource = table;
                this.DbFieldDownList.DataTextField = "ColumnName";
                this.DbFieldDownList.DataValueField = "ColumnName";
                this.DbFieldDownList.DataBind();
                this.DbFieldList.DataTextField = "ColumnName";
                this.DbFieldList.DataValueField = "ColumnName";
                this.DbFieldList.DataBind();
            }
        }

        protected void DBTableDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.DbTableDownList2.SelectedIndex <= 0)
            {
                this.Span1.Visible = false;
            }
            else
            {
                DataTable table = new DataTable();
                string dbtype = this.Dbtype;
                if (dbtype != null)
                {
                    if (!(dbtype == "sql_sysquery"))
                    {
                        if (dbtype == "sql_outquery")
                        {
                            table = LabelManage.GetSchemaTable(this.DbTableDownList2.SelectedValue, XmlManage.ReadFileNode(this.xmlfilepath, "root/LabelDataSource"), DataSourceType.Sql);
                        }
                        else if (dbtype == "ole_read")
                        {
                            table = LabelManage.GetSchemaTable(this.DbTableDownList2.SelectedValue, XmlManage.ReadFileNode(this.xmlfilepath, "root/LabelDataSource"), DataSourceType.Ole);
                        }
                        else if (dbtype == "odbc_read")
                        {
                            table = LabelManage.GetSchemaTable(this.DbTableDownList2.SelectedValue, XmlManage.ReadFileNode(this.xmlfilepath, "root/LabelDataSource"), DataSourceType.Odbc);
                        }
                        else if (dbtype == "orc_read")
                        {
                            table = LabelManage.GetSchemaTable(this.DbTableDownList2.SelectedValue, XmlManage.ReadFileNode(this.xmlfilepath, "root/LabelDataSource"), DataSourceType.Oracle);
                        }
                    }
                    else
                    {
                        table = LabelManage.GetSchemaTable(this.DbTableDownList2.SelectedValue, string.Empty, DataSourceType.None);
                    }
                }
                this.DbFieldDownList2.DataSource = table;
                this.DbFieldList2.DataSource = table;
                this.DbFieldDownList2.DataTextField = "ColumnName";
                this.DbFieldDownList2.DataValueField = "ColumnName";
                this.DbFieldDownList2.DataBind();
                this.DbFieldList2.DataTextField = "ColumnName";
                this.DbFieldList2.DataValueField = "ColumnName";
                this.DbFieldList2.DataBind();
                this.Span1.Visible = true;
            }
        }

        protected static DataTable GetDataBaseSchema(string dtype, string xpath, string tablename)
        {
            switch (dtype)
            {
                case "sql_sysquery":
                    return LabelManage.GetSchemaTable(tablename, string.Empty, DataSourceType.None);

                case "sql_outquery":
                    return LabelManage.GetSchemaTable(tablename, XmlManage.ReadFileNode(xpath, "root/LabelDataSource"), DataSourceType.Sql);

                case "ole_read":
                    return LabelManage.GetSchemaTable(tablename, XmlManage.ReadFileNode(xpath, "root/LabelDataSource"), DataSourceType.Ole);

                case "odbc_read":
                    return LabelManage.GetSchemaTable(tablename, XmlManage.ReadFileNode(xpath, "root/LabelDataSource"), DataSourceType.Odbc);

                case "orc_read":
                    return LabelManage.GetSchemaTable(tablename, XmlManage.ReadFileNode(xpath, "root/LabelDataSource"), DataSourceType.Oracle);
            }
            return null;
        }

        protected void GreatWhere(string fieldtype, string fieldtext, string ifieldname, string tjenu, int dw1Value)
        {
            Comparison @operator = (Comparison) 0;
            switch (tjenu)
            {
                case "2":
                    @operator = Comparison.GreaterThan;
                    break;

                case "3":
                    @operator = Comparison.LessThan;
                    break;

                case "4":
                    @operator = Comparison.GreaterOrEquals;
                    break;

                case "5":
                    @operator = Comparison.LessOrEquals;
                    break;

                case "6":
                    @operator = Comparison.NotEquals;
                    break;

                case "7":
                    @operator = Comparison.In;
                    break;

                case "8":
                    @operator = Comparison.Like;
                    break;

                case "9":
                    @operator = Comparison.NotIn;
                    break;

                case "10":
                    @operator = Comparison.NotLike;
                    break;

                default:
                    @operator = Comparison.Equals;
                    break;
            }
            try
            {
                if (((string.Compare(fieldtype, "System.Int", StringComparison.OrdinalIgnoreCase) == 0) || (string.Compare(fieldtype, "System.Int32", StringComparison.OrdinalIgnoreCase) == 0)) || (string.Compare(fieldtype, "System.Int16", StringComparison.OrdinalIgnoreCase) == 0))
                {
                    if (fieldtext.IndexOf("@", 0, 1, StringComparison.Ordinal) < 0)
                    {
                        this.query.AddWhere(ifieldname, @operator, DataConverter.CLng(fieldtext), dw1Value);
                        this.querycount.AddWhere(ifieldname, @operator, DataConverter.CLng(fieldtext), dw1Value);
                    }
                    else
                    {
                        this.query.AddWhere(ifieldname, @operator, new SqlLiteral(fieldtext), dw1Value);
                        this.querycount.AddWhere(ifieldname, @operator, new SqlLiteral(fieldtext), dw1Value);
                    }
                }
                else
                {
                    this.query.AddWhere(ifieldname, @operator, fieldtext, dw1Value);
                    this.querycount.AddWhere(ifieldname, @operator, fieldtext, dw1Value);
                }
            }
            catch (Exception)
            {
                this.TxtSqlstr.Text = "条件关系设置错误";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.action = BasePage.RequestString("action");
            this.m_LabelName = BasePage.RequestString("name");
            this.m_LabelLibPath = "~/" + SiteConfig.SiteOption.LabelDir;
            if (string.IsNullOrEmpty(this.m_LabelName))
            {
                BasePage.ResponseRedirect("Label.aspx");
            }
            string path = WebConfigurationManager.AppSettings["EasyOne:LabelXsltPath"];
            this.xmlfilepath = HttpContext.Current.Server.MapPath(path) + @"\" + this.m_LabelName + ".config";
            if (string.Compare(this.action, "modify", StringComparison.OrdinalIgnoreCase) == 0)
            {
                this.BtnSave.Visible = true;
            }
            this.Dbtype = XmlManage.ReadFileNode(this.xmlfilepath, "root/LabelDataType");
            if (!base.IsPostBack)
            {
                string str2;
                DataRow[] rowArray;
                this.TxtSqlstr.Text = XmlManage.ReadFileNode(this.xmlfilepath, "root/LabelSqlString");
                DataTable table = new DataTable();
                if ((string.Compare("sql_sysquery", this.Dbtype, StringComparison.OrdinalIgnoreCase) == 0) || (string.Compare("sql_outquery", this.Dbtype, StringComparison.OrdinalIgnoreCase) == 0))
                {
                    if (DataConverter.CBoolean(XmlManage.ReadFileNode(this.xmlfilepath, "root/UsePage")))
                    {
                        this.ChkPage.Checked = true;
                        this.CountShow.Visible = true;
                        this.TxtSqlCount.Text = XmlManage.ReadFileNode(this.xmlfilepath, "root/LabelSqlCount");
                    }
                    if (string.Compare("sql_sysquery", this.Dbtype, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        rowArray = LabelManage.GetSystemSchemaDataBases().Select("", " TABLE_NAME ");
                        table = LabelManage.GetSystemSchemaDataBases().Clone();
                        table.Rows.Clear();
                        foreach (DataRow row in rowArray)
                        {
                            table.ImportRow(row);
                        }
                    }
                    else
                    {
                        str2 = XmlManage.ReadFileNode(this.xmlfilepath, "root/LabelDataSource");
                        rowArray = LabelManage.GetSchemaDataBase(str2, DataSourceType.Sql).Select("", " TABLE_NAME ");
                        table = LabelManage.GetSchemaDataBase(str2, DataSourceType.Sql).Clone();
                        table.Rows.Clear();
                        foreach (DataRow row2 in rowArray)
                        {
                            table.ImportRow(row2);
                        }
                    }
                }
                else if (string.Compare(this.Dbtype, "ole_read", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    if (DataConverter.CBoolean(XmlManage.ReadFileNode(this.xmlfilepath, "root/UsePage")))
                    {
                        this.ChkPage.Checked = true;
                        this.CountShow.Visible = true;
                        this.PageShow.Visible = true;
                        this.TxtSqlCount.Text = XmlManage.ReadFileNode(this.xmlfilepath, "root/LabelSqlCount");
                        this.TxtSqlPage.Text = XmlManage.ReadFileNode(this.xmlfilepath, "root/LabelSqlPage");
                    }
                    str2 = XmlManage.ReadFileNode(this.xmlfilepath, "root/LabelDataSource");
                    rowArray = LabelManage.GetSchemaDataBase(str2, DataSourceType.Ole).Select("", " TABLE_NAME ");
                    table = LabelManage.GetSchemaDataBase(str2, DataSourceType.Ole).Clone();
                    table.Rows.Clear();
                    foreach (DataRow row3 in rowArray)
                    {
                        table.ImportRow(row3);
                    }
                }
                else if (string.Compare(this.Dbtype, "odbc_read", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    if (DataConverter.CBoolean(XmlManage.ReadFileNode(this.xmlfilepath, "root/UsePage")))
                    {
                        this.ChkPage.Checked = true;
                        this.CountShow.Visible = true;
                        this.PageShow.Visible = true;
                        this.TxtSqlCount.Text = XmlManage.ReadFileNode(this.xmlfilepath, "root/LabelSqlCount");
                        this.TxtSqlPage.Text = XmlManage.ReadFileNode(this.xmlfilepath, "root/LabelSqlPage");
                    }
                    str2 = XmlManage.ReadFileNode(this.xmlfilepath, "root/LabelDataSource");
                    rowArray = LabelManage.GetSchemaDataBase(str2, DataSourceType.Odbc).Select("", " TABLE_NAME ");
                    table = LabelManage.GetSchemaDataBase(str2, DataSourceType.Odbc).Clone();
                    table.Rows.Clear();
                    foreach (DataRow row4 in rowArray)
                    {
                        table.ImportRow(row4);
                    }
                }
                else if (string.Compare(this.Dbtype, "orc_read", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    if (DataConverter.CBoolean(XmlManage.ReadFileNode(this.xmlfilepath, "root/UsePage")))
                    {
                        this.ChkPage.Checked = true;
                        this.CountShow.Visible = true;
                        this.TxtSqlCount.Text = XmlManage.ReadFileNode(this.xmlfilepath, "root/LabelSqlCount");
                    }
                    str2 = XmlManage.ReadFileNode(this.xmlfilepath, "root/LabelDataSource");
                    rowArray = LabelManage.GetSchemaDataBase(str2, DataSourceType.Oracle).Select("", " TABLE_NAME ");
                    table = LabelManage.GetSchemaDataBase(str2, DataSourceType.Oracle).Clone();
                    table.Rows.Clear();
                    foreach (DataRow row5 in rowArray)
                    {
                        table.ImportRow(row5);
                    }
                }
                this.DbTableDownList.DataSource = table;
                this.DbTableDownList.DataTextField = "TABLE_NAME";
                this.DbTableDownList.DataValueField = "TABLE_NAME";
                this.DbTableDownList.DataBind();
                ListItem item = new ListItem();
                item.Text = "请选择一个表";
                this.DbTableDownList.Items.Insert(0, item);
                this.DbTableDownList2.DataSource = table;
                this.DbTableDownList2.DataTextField = "TABLE_NAME";
                this.DbTableDownList2.DataValueField = "TABLE_NAME";
                this.DbTableDownList2.DataBind();
                item.Text = "请选择一个表";
                this.DbTableDownList2.Items.Insert(0, item);
                if (LabelManage.GetAttributeList(this.xmlfilepath).Count == 0)
                {
                    this.attlist.Text = "您尚未添加参数!<a href=\"LabelProperty.aspx?action=" + this.action + "&name=" + this.m_LabelName + "\">添加参数</a>";
                }
                else
                {
                    foreach (LabelAttributeInfo info in LabelManage.GetAttributeList(this.xmlfilepath))
                    {
                        this.attlist.Text = this.attlist.Text + "<div onmousedown=\"dragstart();\" class=\"spanfixdiv\">" + info.AttributeName + "</div>";
                    }
                }
            }
            base.Form.Attributes.Add("onmouseup", "dragclear()");
            base.Form.Attributes.Add("onmousemove", "dragmove()");
            this.TxtSqlstr.Attributes.Add("onmouseup", "dragend(1);");
            this.TxtSqlstr.Attributes.Add("onmousemove", "movePoint();;");
            this.TxtSqlPage.Attributes.Add("onmouseup", "dragend(1);");
            this.TxtSqlPage.Attributes.Add("onmousemove", "movePoint();;");
            this.TxtSqlCount.Attributes.Add("onmouseup", "dragend(1);");
            this.TxtSqlCount.Attributes.Add("onmousemove", "movePoint();;");
            this.SmpNavigator.AdditionalNode = this.m_LabelName;
        }

        protected void QueryProc(int irlist)
        {
            for (int i = 0; i < this.GridView_Clause.Rows.Count; i++)
            {
                DropDownList list = (DropDownList) this.GridView_Clause.Rows[i].FindControl("dropdowntj1");
                DropDownList list2 = (DropDownList) this.GridView_Clause.Rows[i].FindControl("dropdowntj2");
                DropDownList list3 = (DropDownList) this.GridView_Clause.Rows[i].FindControl("dropdownorder");
                TextBox box = (TextBox) this.GridView_Clause.Rows[i].FindControl("txtbox1");
                if (!string.IsNullOrEmpty(list2.SelectedValue) || !string.IsNullOrEmpty(list3.SelectedValue))
                {
                    string text = box.Text;
                    string ifieldname = string.Empty;
                    if (irlist == -1)
                    {
                        ifieldname = this.GridView_Clause.Rows[i].Cells[0].Text;
                    }
                    else if (i > irlist)
                    {
                        ifieldname = this.DbTableDownList2.SelectedValue + "." + this.GridView_Clause.Rows[i].Cells[0].Text;
                    }
                    else
                    {
                        ifieldname = this.DbTableDownList.SelectedValue + "." + this.GridView_Clause.Rows[i].Cells[0].Text;
                    }
                    if (!string.IsNullOrEmpty(text))
                    {
                        this.GreatWhere(this.GridView_Clause.Rows[i].Cells[1].Text, text, ifieldname, list2.SelectedValue, DataConverter.CLng(list.SelectedValue));
                    }
                    if (string.Compare(list3.SelectedValue, "up", StringComparison.Ordinal) == 0)
                    {
                        this.query.AddOrderBy(ifieldname, Sorting.Ascending);
                    }
                    else if (string.Compare(list3.SelectedValue, "down", StringComparison.Ordinal) == 0)
                    {
                        this.query.AddOrderBy(ifieldname, Sorting.Descending);
                    }
                }
            }
        }
    }
}

