namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Model.Accessories;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class DownServerOrder : AdminPage
    {

        protected void EBtnSetOrderId_Click(object sender, EventArgs e)
        {
            List<DownServerInfo> list = new List<DownServerInfo>();
            foreach (GridViewRow row in this.GdvDownServerOrder.Rows)
            {
                DropDownList list2 = (DropDownList) row.FindControl("DropOrderId");
                int serverId = (int) this.GdvDownServerOrder.DataKeys[row.RowIndex].Value;
                int num2 = DataConverter.CLng(list2.SelectedValue);
                DownServerInfo downServerById = DownServer.GetDownServerById(serverId);
                downServerById.OrderId = num2;
                if (!downServerById.IsNull)
                {
                    list.Add(downServerById);
                }
            }
            DownServer.OrderDownServer(list);
            this.GdvDownServerOrder.DataBind();
            base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
            AdminPage.WriteSuccessMsg("下载服务器排序成功！", "DownServerOrder.aspx");
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            int count = this.GdvDownServerOrder.Rows.Count;
            if (count > 0)
            {
                string[] strArray = new string[count];
                ListItemCollection items = new ListItemCollection();
                for (int i = 1; i <= count; i++)
                {
                    ListItem item = new ListItem(i.ToString(), i.ToString());
                    items.Add(item);
                }
                foreach (GridViewRow row in this.GdvDownServerOrder.Rows)
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
                builder.Remove(builder.Length - 1, 1);
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
}

