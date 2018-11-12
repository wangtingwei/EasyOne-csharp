namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.AccessManage;
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.Accessories;
    using EasyOne.Web.UI;
    using System;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class BankManage : AdminPage
    {

        protected void BtnSaveSort_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in this.EgvBankList.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList list = (DropDownList) row.FindControl("DropOrderId");
                    int orderId = DataConverter.CLng(list.SelectedValue);
                    Bank.SetOrderId(DataConverter.CLng(this.EgvBankList.DataKeys[row.RowIndex].Value), orderId);
                }
            }
            this.EgvBankList.DataBind();
        }

        protected void EgvBankList_RowCommand(object sender, CommandEventArgs e)
        {
            int bankId = DataConverter.CLng(e.CommandArgument);
            string commandName = e.CommandName;
            if (commandName != null)
            {
                if (!(commandName == "Default"))
                {
                    if (commandName == "Disabled")
                    {
                        Bank.SetDisabled(bankId, true);
                    }
                    else if (commandName == "Enabled")
                    {
                        Bank.SetDisabled(bankId, false);
                    }
                    else if (commandName == "Del")
                    {
                        Bank.Delete(bankId);
                    }
                }
                else
                {
                    Bank.SetDefault(bankId);
                }
            }
            this.EgvBankList.DataBind();
        }

        protected void EgvBankList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                BankInfo dataItem = e.Row.DataItem as BankInfo;
                if (dataItem != null)
                {
                    bool flag = RolePermissions.AccessCheck(OperateCode.BankAccountManage);
                    LinkButton button = e.Row.Cells[7].FindControl("LbtnDefault") as LinkButton;
                    LinkButton button2 = e.Row.Cells[7].FindControl("LbtnDisabled") as LinkButton;
                    LinkButton button3 = e.Row.Cells[7].FindControl("LbtnDel") as LinkButton;
                    button.Enabled = flag && !dataItem.IsDefault;
                    button2.Enabled = flag && !dataItem.IsDefault;
                    button3.Enabled = flag && !dataItem.IsDefault;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            int count = this.EgvBankList.Rows.Count;
            string[] strArray = new string[count];
            ListItemCollection items = new ListItemCollection();
            for (int i = 1; i <= count; i++)
            {
                ListItem item = new ListItem(i.ToString(), i.ToString());
                items.Add(item);
            }
            foreach (GridViewRow row in this.EgvBankList.Rows)
            {
                DropDownList list = (DropDownList) row.FindControl("DropOrderId");
                list.DataSource = items;
                list.DataBind();
                list.SelectedIndex = row.RowIndex;
                strArray[row.RowIndex] = list.ClientID;
                list.Attributes.Add("onchange", "Reorder(this, " + row.RowIndex.ToString() + "," + count.ToString() + ")");
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("<script language=\"JavaScript\" type=\"text/JavaScript\">");
            builder.Append("var arrId = new Array(");
            for (int j = 0; j < count; j++)
            {
                builder.Append("\"" + strArray[j] + "\",");
            }
            if (count > 0)
            {
                builder.Remove(builder.Length - 1, 1);
            }
            builder.Append(");\n");
            builder.Append("function Reorder(eSelect, iCurrentField, numSelects)\n");
            builder.Append("{\n");
            builder.Append("    var eForm = eSelect.form;\n");
            builder.Append("    var iNewOrder = eSelect.selectedIndex + 1;\n");
            builder.Append("    var iPrevOrder;\n");
            builder.Append("    var positions = new Array(numSelects);\n");
            builder.Append("    var ix;\n");
            builder.Append("    for (ix = 0; ix < numSelects; ix++)\n");
            builder.Append("    {\n");
            builder.Append("            positions[ix] = 0;\n");
            builder.Append("    }\n");
            builder.Append("    for (ix = 0; ix < numSelects; ix++)\n");
            builder.Append("    {\n");
            builder.Append("            positions[eSelect.form[arrId[ix].toString()].selectedIndex] = 1;\n");
            builder.Append("    }\n");
            builder.Append("    for (ix = 0; ix < numSelects; ix++)\n");
            builder.Append("    {\n");
            builder.Append("            if (positions[ix] == 0)\n");
            builder.Append("            {\n");
            builder.Append("                    iPrevOrder = ix + 1;\n");
            builder.Append("                    break;\n");
            builder.Append("            }\n");
            builder.Append("    }\n");
            builder.Append("    if (iNewOrder != iPrevOrder)\n");
            builder.Append("    {\n");
            builder.Append("            var iInc = iNewOrder > iPrevOrder? -1:1\n");
            builder.Append("            var iMin = Math.min(iNewOrder, iPrevOrder);\n");
            builder.Append("            var iMax = Math.max(iNewOrder, iPrevOrder);\n");
            builder.Append("            for (var iField = 0; iField < numSelects; iField++)\n");
            builder.Append("            {\n");
            builder.Append("                    if (iField != iCurrentField)\n");
            builder.Append("                    {\n");
            builder.Append("                            if (eSelect.form[arrId[iField].toString()].selectedIndex + 1 >= iMin && eSelect.form[arrId[iField].toString()].selectedIndex + 1<= iMax)\n");
            builder.Append("                            {\n");
            builder.Append("                                    eSelect.form[arrId[iField].toString()].selectedIndex += iInc;\n");
            builder.Append("                            }\n");
            builder.Append("                    }\n");
            builder.Append("            }\n");
            builder.Append("    }\n");
            builder.Append("}\n");
            builder.Append("</script>\n");
            this.Page.ClientScript.RegisterClientScriptBlock(base.GetType(), "Order", builder.ToString());
        }
    }
}

