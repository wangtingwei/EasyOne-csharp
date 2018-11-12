namespace EasyOne.Controls
{
    using EasyOne.Common;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:ExtendedGridView ID=\"Egv\" runat=\"server\"></{0}:ExtendedGridView>"), Themeable(true), ToolboxBitmap(typeof(ExtendedGridView), "ExtendedGridView.bmp")]
    public class ExtendedGridView : GridView
    {
        private const string CheckBoxColumHeaderID = "{0}_HeaderButton";
        private const string CheckBoxColumHeaderTemplate = "<input type='checkbox' hidefocus='true' id='{0}' name='{0}' onclick='CheckAll(this)'>";
        private const string ExtendedGridView_JS = "EasyOne.Controls.ExtendedGridView.ExtendedGridView.js";
        private int m_RawPageIndex;
        private string m_UniqueControlPageIndex;
        private string m_UniqueControlPageSize;

        private ArrayList AddCheckBoxColumn(ICollection columnList)
        {
            ArrayList list = new ArrayList(columnList);
            string format = "<input type='checkbox' hidefocus='true' id='{0}' name='{0}' onclick='CheckAll(this,\"" + base.RowStyle.CssClass + "\",\"" + this.SelectedCssClass + "\")'>";
            InputCheckBoxField field = new InputCheckBoxField();
            string str2 = string.Format("{0}_HeaderButton", this.ClientID);
            field.HeaderText = string.Format(format, str2);
            field.HeaderStyle.Width = this.CheckBoxFieldHeaderWidth;
            field.ReadOnly = true;
            field.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            if (this.CheckBoxColumnIndex > list.Count)
            {
                list.Add(field);
                this.CheckBoxColumnIndex = list.Count - 1;
                return list;
            }
            list.Insert(this.CheckBoxColumnIndex, field);
            return list;
        }

        private ArrayList AddSerialColumn(ICollection columnList)
        {
            ArrayList list = new ArrayList(columnList);
            EasyOne.Controls.TemplateField field = new EasyOne.Controls.TemplateField {
                HeaderText = this.SerialText
            };
            if (this.SerialColumnIndex > list.Count)
            {
                list.Add(field);
                this.SerialColumnIndex = list.Count - 1;
                return list;
            }
            list.Insert(this.SerialColumnIndex, field);
            return list;
        }

        protected override ICollection CreateColumns(PagedDataSource dataSource, bool useDataSource)
        {
            ICollection columnList = base.CreateColumns(dataSource, useDataSource);
            if (!this.AutoGenerateCheckBoxColumn && !this.AutoGenerateSerialColumn)
            {
                return columnList;
            }
            ArrayList list = new ArrayList();
            if (this.AutoGenerateCheckBoxColumn)
            {
                list = this.AddCheckBoxColumn(columnList);
            }
            if (this.AutoGenerateSerialColumn)
            {
                list = this.AddSerialColumn(columnList);
            }
            return list;
        }

        private void CreateCustomPagerRow(GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Controls.Clear();
            e.Row.Cells[0].Controls.Add(new LiteralControl("共&nbsp;"));
            Label child = new Label {
                ID = "LblRowsCount",
                Text = this.VirtualItemCount.ToString(),
                Font = { Bold = true }
            };
            e.Row.Cells[0].Controls.Add(child);
            e.Row.Cells[0].Controls.Add(new LiteralControl("&nbsp;" + this.ItemUnit + this.ItemName));
            e.Row.Cells[0].Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
            LinkButton button = new LinkButton {
                CommandName = "Page",
                CommandArgument = "First",
                Enabled = this.PageIndex != 0,
                Text = "首页"
            };
            e.Row.Cells[0].Controls.Add(button);
            e.Row.Cells[0].Controls.Add(new LiteralControl("&nbsp;"));
            LinkButton button2 = new LinkButton {
                CommandName = "Page",
                CommandArgument = "Prev",
                Enabled = this.PageIndex != 0,
                Text = "上一页"
            };
            e.Row.Cells[0].Controls.Add(button2);
            e.Row.Cells[0].Controls.Add(new LiteralControl("&nbsp;"));
            LinkButton button3 = new LinkButton {
                CommandName = "Page",
                CommandArgument = "Next",
                Enabled = this.PageIndex != (this.PageCount - 1),
                Text = "下一页"
            };
            e.Row.Cells[0].Controls.Add(button3);
            e.Row.Cells[0].Controls.Add(new LiteralControl("&nbsp;"));
            LinkButton button4 = new LinkButton {
                CommandName = "Page",
                CommandArgument = "Last",
                Enabled = this.PageIndex != (this.PageCount - 1),
                Text = "尾页"
            };
            e.Row.Cells[0].Controls.Add(button4);
            e.Row.Cells[0].Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
            if (this.IsHoldState)
            {
                button.Click += new EventHandler(this.LbtnFirst_Click);
                button2.Click += new EventHandler(this.LbtnPrev_Click);
                button3.Click += new EventHandler(this.LbtnNext_Click);
                button4.Click += new EventHandler(this.LbtnLast_Click);
            }
            e.Row.Cells[0].Controls.Add(new LiteralControl("页次："));
            Label label2 = new Label {
                Text = Convert.ToString((int) (this.PageIndex + 1)),
                Font = { Bold = true },
                ForeColor = Color.Red
            };
            e.Row.Cells[0].Controls.Add(label2);
            e.Row.Cells[0].Controls.Add(new LiteralControl("/"));
            Label label3 = new Label {
                Text = Convert.ToString(this.PageCount),
                Font = { Bold = true }
            };
            e.Row.Cells[0].Controls.Add(label3);
            e.Row.Cells[0].Controls.Add(new LiteralControl("页"));
            e.Row.Cells[0].Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
            TextBox box = new TextBox();
            box.ApplyStyleSheetSkin(this.Page);
            box.MaxLength = 3;
            box.Width = 0x16;
            box.Text = Convert.ToString(this.PageSize);
            box.AutoPostBack = true;
            box.TextChanged += new EventHandler(this.TxtMaxPerPage_TextChanged);
            e.Row.Cells[0].Controls.Add(box);
            e.Row.Cells[0].Controls.Add(new LiteralControl(this.ItemUnit + this.ItemName + "/页"));
            e.Row.Cells[0].Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
            e.Row.Cells[0].Controls.Add(new LiteralControl("转到第"));
            if (this.PageCount < 10)
            {
                DropDownList list = new DropDownList();
                list.ApplyStyleSheetSkin(this.Page);
                list.AutoPostBack = true;
                list.SelectedIndexChanged += new EventHandler(this.DropCurrentPage_SelectedIndexChanged);
                ArrayList list2 = new ArrayList();
                for (int i = 1; i <= this.PageCount; i++)
                {
                    list2.Add(i);
                }
                list.DataSource = list2;
                list.DataBind();
                list.SelectedIndex = this.PageIndex;
                e.Row.Cells[0].Controls.Add(list);
            }
            else
            {
                TextBox box2 = new TextBox();
                box2.ApplyStyleSheetSkin(this.Page);
                box2.Width = 30;
                box2.Text = Convert.ToString((int) (this.PageIndex + 1));
                box2.AutoPostBack = true;
                box2.TextChanged += new EventHandler(this.TxtCurrentPage_TextChanged);
                e.Row.Cells[0].Controls.Add(box2);
            }
            e.Row.Cells[0].Controls.Add(new LiteralControl("页"));
        }

        protected void DropCurrentPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList list = (DropDownList) sender;
            this.PageIndex = Convert.ToInt32(list.SelectedValue) - 1;
            if (this.IsHoldState)
            {
                this.Context.Session[this.m_UniqueControlPageIndex] = this.PageIndex;
            }
        }

        protected void LbtnFirst_Click(object sender, EventArgs e)
        {
            this.Context.Session[this.m_UniqueControlPageIndex] = 0;
        }

        protected void LbtnLast_Click(object sender, EventArgs e)
        {
            this.Context.Session[this.m_UniqueControlPageIndex] = this.PageCount - 1;
        }

        protected void LbtnNext_Click(object sender, EventArgs e)
        {
            this.Context.Session[this.m_UniqueControlPageIndex] = this.PageIndex + 1;
        }

        protected void LbtnPrev_Click(object sender, EventArgs e)
        {
            this.Context.Session[this.m_UniqueControlPageIndex] = this.PageIndex - 1;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Type type = base.GetType();
            string webResourceUrl = this.Page.ClientScript.GetWebResourceUrl(type, "EasyOne.Controls.ExtendedGridView.ExtendedGridView.js");
            if (!this.Page.ClientScript.IsClientScriptIncludeRegistered(type, "EasyOne.Controls.ExtendedGridView.ExtendedGridView.js"))
            {
                this.Page.ClientScript.RegisterClientScriptInclude(type, "EasyOne.Controls.ExtendedGridView.ExtendedGridView.js", webResourceUrl);
            }
            if (this.IsHoldState)
            {
                this.m_UniqueControlPageSize = this.Page.GetType().Name + "_" + this.UniqueID + "_PageSize";
                this.m_UniqueControlPageIndex = this.Page.GetType().Name + "_" + this.UniqueID + "_PageIndex";
                if (this.Context.Session[this.m_UniqueControlPageSize] != null)
                {
                    this.PageSize = (int) this.Context.Session[this.m_UniqueControlPageSize];
                }
                if (this.Context.Session[this.m_UniqueControlPageIndex] != null)
                {
                    this.PageIndex = (int) this.Context.Session[this.m_UniqueControlPageIndex];
                    this.m_RawPageIndex = this.PageIndex;
                }
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (((this.PageCount > 1) && ((this.PageIndex + 1) == this.PageCount)) && ((this.Rows.Count == 0) && base.Initialized))
            {
                base.RequiresDataBinding = true;
            }
            if ((this.PageIndex != this.m_RawPageIndex) && (this.Rows.Count == 0))
            {
                this.PageIndex = 0;
                if (base.Initialized)
                {
                    base.RequiresDataBinding = true;
                }
            }
            if (this.AllowPaging && (this.BottomPagerRow != null))
            {
                this.BottomPagerRow.Visible = true;
                if (this.ShowCustomPager)
                {
                    Label label = this.BottomPagerRow.Cells[0].FindControl("LblRowsCount") as Label;
                    if (label != null)
                    {
                        label.Text = this.VirtualItemCount.ToString();
                    }
                }
            }
            if (this.AutoGenerateCheckBoxColumn)
            {
                string str = string.Format("{0}_HeaderButton", this.ClientID);
                foreach (GridViewRow row in this.Rows)
                {
                    CheckBox box = (CheckBox) row.FindControl("CheckBoxButton");
                    box.Attributes["onclick"] = string.Concat(new object[] { "CheckItem(this,\"", str, "\",\"", base.RowStyle.CssClass, "\",\"", this.SelectedCssClass, "\",", this.Rows.Count, ")" });
                }
            }
        }

        protected override void OnRowCreated(GridViewRowEventArgs e)
        {
            base.OnRowCreated(e);
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (!string.IsNullOrEmpty(this.MouseOverCssClass))
                {
                    e.Row.Attributes.Add("onmouseover", "MouseOver(this,\"" + this.MouseOverCssClass + "\")");
                    e.Row.Attributes.Add("onmouseout", "MouseOut(this)");
                }
                if (this.AutoGenerateSerialColumn)
                {
                    e.Row.Cells[this.SerialColumnIndex].Text = Convert.ToString((int) ((e.Row.DataItemIndex + (this.PageIndex * this.PageSize)) + 1));
                }
            }
            if ((e.Row.RowType == DataControlRowType.Pager) && this.ShowCustomPager)
            {
                this.CreateCustomPagerRow(e);
            }
        }

        protected override void OnRowDataBound(GridViewRowEventArgs e)
        {
            base.OnRowDataBound(e);
            if ((e.Row.RowType == DataControlRowType.DataRow) && !string.IsNullOrEmpty(this.RowDblclickUrl))
            {
                if (!string.IsNullOrEmpty(this.RowDblclickBoundField))
                {
                    e.Row.Attributes.Add("ondblclick", "RowDblclick('" + StringHelper.ReplaceIgnoreCase(this.RowDblclickUrl, "{$Field}", DataBinder.Eval(e.Row.DataItem, this.RowDblclickBoundField).ToString()) + "');");
                }
                else
                {
                    e.Row.Attributes.Add("ondblclick", "RowDblclick('" + this.RowDblclickUrl + "');");
                }
            }
        }

        protected override void PerformDataBinding(IEnumerable data)
        {
            base.PerformDataBinding(data);
            this.ViewState["VirtualItemCount"] = this.ViewState["_!ItemCount"];
        }

        protected override void PrepareControlHierarchy()
        {
            base.PrepareControlHierarchy();
        }

        protected void TxtCurrentPage_TextChanged(object sender, EventArgs e)
        {
            int pageCount;
            TextBox box = (TextBox) sender;
            if (!int.TryParse(box.Text, out pageCount))
            {
                pageCount = 1;
            }
            else if (pageCount < 1)
            {
                pageCount = 1;
            }
            if (pageCount > this.PageCount)
            {
                pageCount = this.PageCount;
            }
            this.PageIndex = pageCount - 1;
            box.Text = pageCount.ToString();
            if (this.IsHoldState)
            {
                this.Context.Session[this.m_UniqueControlPageIndex] = this.PageIndex;
            }
        }

        protected void TxtMaxPerPage_TextChanged(object sender, EventArgs e)
        {
            int pageSize;
            TextBox box = (TextBox) sender;
            if (!int.TryParse(box.Text, out pageSize))
            {
                pageSize = this.PageSize;
            }
            else if (pageSize < 1)
            {
                pageSize = this.PageSize;
            }
            this.PageSize = pageSize;
            this.PageIndex = 0;
            box.Text = pageSize.ToString();
            if (this.IsHoldState)
            {
                this.Context.Session[this.m_UniqueControlPageSize] = this.PageSize;
            }
        }

        [Bindable(true), Category("自定义"), Localizable(true), DefaultValue(false), Description("是否自动生成复选框列")]
        public bool AutoGenerateCheckBoxColumn
        {
            get
            {
                object obj2 = this.ViewState["AutoGenerateCheckBoxColumn"];
                if (obj2 == null)
                {
                    return false;
                }
                return (bool) obj2;
            }
            set
            {
                this.ViewState["AutoGenerateCheckBoxColumn"] = value;
            }
        }

        [Description("是否显示序号列"), DefaultValue(false), Localizable(true), Bindable(true), Category("自定义")]
        public bool AutoGenerateSerialColumn
        {
            get
            {
                object obj2 = this.ViewState["AutoGenerateSerialColumn"];
                return ((obj2 != null) && ((bool) obj2));
            }
            set
            {
                this.ViewState["AutoGenerateSerialColumn"] = value;
            }
        }

        [Bindable(true), DefaultValue(0), Category("自定义"), Description("复选框列的索引")]
        public int CheckBoxColumnIndex
        {
            get
            {
                object obj2 = this.ViewState["CheckBoxColumnIndex"];
                if (obj2 == null)
                {
                    return 0;
                }
                return (int) obj2;
            }
            set
            {
                this.ViewState["CheckBoxColumnIndex"] = (value < 0) ? 0 : value;
            }
        }

        [Description("复选框列宽度"), DefaultValue(20), Localizable(true), Category("自定义"), Bindable(true)]
        public Unit CheckBoxFieldHeaderWidth
        {
            get
            {
                object obj2 = this.ViewState["CheckBoxFieldHeaderWidth"];
                if (obj2 == null)
                {
                    return Unit.Percentage(3.0);
                }
                return (Unit) obj2;
            }
            set
            {
                this.ViewState["CheckBoxFieldHeaderWidth"] = value;
            }
        }

        [Bindable(true), DefaultValue(false), Localizable(true), Category("自定义"), Description("是否保持当前状态")]
        public bool IsHoldState
        {
            get
            {
                object obj2 = this.ViewState["IsHoldState"];
                if (obj2 != null)
                {
                    return (bool) obj2;
                }
                return true;
            }
            set
            {
                this.ViewState["IsHoldState"] = value;
            }
        }

        [Localizable(true), Description("分页导航处显示的项目名称"), Bindable(true), Category("自定义"), DefaultValue("记录")]
        public string ItemName
        {
            get
            {
                string str = (string) this.ViewState["ItemName"];
                if (str != null)
                {
                    return str;
                }
                return "记录";
            }
            set
            {
                this.ViewState["ItemName"] = value;
            }
        }

        [DefaultValue("条"), Localizable(true), Description("分页导航处显示的项目单位"), Bindable(true), Category("自定义")]
        public string ItemUnit
        {
            get
            {
                string str = (string) this.ViewState["ItemUnit"];
                if (str != null)
                {
                    return str;
                }
                return "条";
            }
            set
            {
                this.ViewState["ItemUnit"] = value;
            }
        }

        [DefaultValue(""), Category("自定义"), Bindable(true), Description("鼠标移动到数据行上显示的CSS效果"), Localizable(true)]
        public string MouseOverCssClass
        {
            get
            {
                string str = (string) this.ViewState["MouseOverCssClass"];
                if (str != null)
                {
                    return str;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["MouseOverCssClass"] = value;
            }
        }

        public override int PageIndex
        {
            set
            {
                base.PageIndex = value;
                if (!base.DesignMode)
                {
                    this.Context.Session[this.m_UniqueControlPageIndex] = value;
                }
            }
        }

        public override int PageSize
        {
            set
            {
                base.PageSize = value;
                if (!base.DesignMode)
                {
                    this.Context.Session[this.m_UniqueControlPageSize] = value;
                }
            }
        }

        [DefaultValue(""), Description("行双击时绑定的数据列"), Bindable(true), Category("自定义")]
        public virtual string RowDblclickBoundField
        {
            get
            {
                string str = (string) this.ViewState["RowDblclickBoundField"];
                if (str != null)
                {
                    return str;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["RowDblclickBoundField"] = value;
            }
        }

        [Bindable(true), DefaultValue(""), Category("自定义"), Description("行双击时跳转的URL，可以包含{$Field}来代替绑定的数据列，比如：UserShow.aspx?UserID={$Field}")]
        public virtual string RowDblclickUrl
        {
            get
            {
                string str = (string) this.ViewState["RowDblclickUrl"];
                if (str != null)
                {
                    return str;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["RowDblclickUrl"] = value;
            }
        }

        [Bindable(true), Category("自定义"), DefaultValue(""), Description("选中的数据行上显示的CSS效果"), Localizable(true)]
        public string SelectedCssClass
        {
            get
            {
                string str = (string) this.ViewState["SelectedCssClass"];
                if (str != null)
                {
                    return str;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["SelectedCssClass"] = value;
            }
        }

        public StringBuilder SelectList
        {
            get
            {
                string append = "";
                StringBuilder sb = new StringBuilder("");
                for (int i = 0; i < this.Rows.Count; i++)
                {
                    CheckBox box = (CheckBox) this.Rows[i].Cells[this.CheckBoxColumnIndex].FindControl("CheckBoxButton");
                    if (box.Checked)
                    {
                        append = this.DataKeys[i].Value.ToString();
                        StringHelper.AppendString(sb, append);
                    }
                }
                return sb;
            }
        }

        [DefaultValue(0), Localizable(true), Bindable(true), Category("自定义"), Description("序号列的索引")]
        public int SerialColumnIndex
        {
            get
            {
                object obj2 = this.ViewState["SerialColumnIndex"];
                if (obj2 != null)
                {
                    return (int) obj2;
                }
                return 0;
            }
            set
            {
                this.ViewState["SerialColumnIndex"] = value;
            }
        }

        [Category("自定义"), DefaultValue("名次"), Description("序号列的标题文字"), Localizable(true), Bindable(true)]
        public string SerialText
        {
            get
            {
                string str = (string) this.ViewState["SerialText"];
                if (str != null)
                {
                    return str;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["SerialText"] = value;
            }
        }

        [Category("自定义"), Localizable(true), Bindable(true), DefaultValue(true), Description("是否显示控件默认的分页导航方式")]
        public bool ShowCustomPager
        {
            get
            {
                object obj2 = this.ViewState["ShowCustomPager"];
                if (obj2 != null)
                {
                    return (bool) obj2;
                }
                return true;
            }
            set
            {
                this.ViewState["ShowCustomPager"] = value;
            }
        }

        public int VirtualItemCount
        {
            get
            {
                object obj2 = this.ViewState["VirtualItemCount"];
                if (obj2 != null)
                {
                    return (int) obj2;
                }
                return 0;
            }
        }
    }
}

