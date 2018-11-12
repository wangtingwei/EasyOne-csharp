namespace EasyOne.WebSite.Controls
{
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class IPLock1 : BaseUserControl
    {
        private static int s_Index;
        private static bool s_IsEdit;

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            DataTable iPTable = this.IPTable;
            if (s_IsEdit)
            {
                if (StringHelper.EncodeIP(this.TxtTo.Text) < StringHelper.EncodeIP(this.TxtFrom.Text))
                {
                    iPTable.Rows[s_Index]["IPFrom"] = this.TxtTo.Text;
                    iPTable.Rows[s_Index]["IPTo"] = this.TxtFrom.Text;
                }
                else
                {
                    iPTable.Rows[s_Index]["IPFrom"] = this.TxtFrom.Text;
                    iPTable.Rows[s_Index]["IPTo"] = this.TxtTo.Text;
                }
                s_IsEdit = false;
                this.BtnSave.Text = "添加";
            }
            else
            {
                DataRow row = iPTable.NewRow();
                if (StringHelper.EncodeIP(this.TxtTo.Text) < StringHelper.EncodeIP(this.TxtFrom.Text))
                {
                    row["IPFrom"] = this.TxtTo.Text;
                    row["IPTo"] = this.TxtFrom.Text;
                }
                else
                {
                    row["IPFrom"] = this.TxtFrom.Text;
                    row["IPTo"] = this.TxtTo.Text;
                }
                iPTable.Rows.Add(row);
            }
            this.IPTable = iPTable;
            this.TxtFrom.Text = this.TxtTo.Text = string.Empty;
            this.EgvIPLock.DataSource = iPTable;
            this.EgvIPLock.DataBind();
        }

        protected void EgvIPLock_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable iPTable = this.IPTable;
            iPTable.Rows.RemoveAt(e.RowIndex);
            this.IPTable = iPTable;
            this.TxtFrom.Text = this.TxtTo.Text = string.Empty;
            s_IsEdit = false;
            this.EgvIPLock.DataSource = iPTable;
            this.EgvIPLock.DataBind();
            this.BtnSave.Text = "添加";
        }

        protected void EgvIPLock_RowEditing(object sender, GridViewEditEventArgs e)
        {
            s_IsEdit = true;
            this.BtnSave.Text = "保存";
            s_Index = e.NewEditIndex;
            this.TxtFrom.Text = this.EgvIPLock.Rows[s_Index].Cells[0].Text;
            this.TxtTo.Text = this.EgvIPLock.Rows[s_Index].Cells[1].Text;
            this.TxtFrom.Focus();
        }

        protected override void OnInit(EventArgs e)
        {
            this.BtnSave.ValidationGroup = this.ID;
            this.TxtFrom.ValidationGroup = this.ID;
            this.TxtTo.ValidationGroup = this.ID;
            this.ValeIPFrom.ValidationGroup = this.ID;
            this.ValeIPTo.ValidationGroup = this.ID;
            this.ValrIPFrom.ValidationGroup = this.ID;
            this.ValrIPTo.ValidationGroup = this.ID;
            this.EgvIPLock.EmptyDataText = string.Empty;
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        private DataTable IPTable
        {
            get
            {
                if (this.ViewState["IPTable"] == null)
                {
                    DataTable table = new DataTable();
                    table.Columns.Add("IPTo", typeof(string));
                    table.Columns.Add("IPFrom", typeof(string));
                    this.ViewState["IPTable"] = table;
                }
                return (this.ViewState["IPTable"] as DataTable);
            }
            set
            {
                this.ViewState["IPTable"] = value;
            }
        }

        public string Value
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                foreach (GridViewRow row in this.EgvIPLock.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        builder.Append(StringHelper.EncodeIP(row.Cells[0].Text).ToString() + "----" + StringHelper.EncodeIP(row.Cells[1].Text).ToString());
                        builder.Append("$$$");
                    }
                }
                return builder.ToString().TrimEnd(new char[] { '$' });
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    DataTable table = new DataTable();
                    table.Columns.Add("IPTo", typeof(string));
                    table.Columns.Add("IPFrom", typeof(string));
                    foreach (string str in value.Split(new string[] { "$$$" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        string[] field = str.Split(new string[] { "----" }, StringSplitOptions.None);
                        DataRow row = table.NewRow();
                        row["IPFrom"] = StringHelper.DecodeIP(Convert.ToInt64(DataSecurity.GetArrayValue(0, field)));
                        row["IPTo"] = StringHelper.DecodeIP(Convert.ToInt64(DataSecurity.GetArrayValue(1, field)));
                        table.Rows.Add(row);
                    }
                    this.IPTable = table;
                    this.EgvIPLock.DataSource = table;
                    this.EgvIPLock.DataBind();
                }
            }
        }
    }
}

