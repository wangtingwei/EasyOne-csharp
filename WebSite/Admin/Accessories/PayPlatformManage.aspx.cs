namespace EasyOne.WebSite.Admin.Accessories
{
    using AjaxControlToolkit;
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Model.Accessories;
    using EasyOne.Web.UI;
    using System;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class PayPlatformManage : AdminPage
    {

        protected void BtnSaveSort_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in this.EgvPayPlatform.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList list = (DropDownList) row.FindControl("DropOrderId");
                    int orderId = DataConverter.CLng(list.SelectedValue);
                    PayPlatform.SetOrderId(DataConverter.CLng(this.EgvPayPlatform.DataKeys[row.RowIndex].Value), orderId);
                }
            }
            this.EgvPayPlatform.DataBind();
        }

        protected void EgvPayPlatform_RowCommand(object sender, CommandEventArgs e)
        {
            int payPlatformId = DataConverter.CLng(e.CommandArgument);
            string commandName = e.CommandName;
            if (commandName != null)
            {
                if (!(commandName == "Disabled"))
                {
                    if (commandName == "Enabled")
                    {
                        PayPlatform.DisablePayPlatform(payPlatformId, false);
                    }
                    else if (commandName == "Del")
                    {
                        PayPlatform.Delete(payPlatformId);
                    }
                    else if (commandName == "SetDefault")
                    {
                        PayPlatform.SetDefault(payPlatformId);
                    }
                }
                else
                {
                    PayPlatform.DisablePayPlatform(payPlatformId, true);
                }
            }
            this.EgvPayPlatform.DataBind();
        }

        protected void EgvPayPlatform_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                PayPlatformInfo dataItem = e.Row.DataItem as PayPlatformInfo;
                if (dataItem != null)
                {
                    LinkButton button = e.Row.Cells[6].FindControl("LbtnSetDefault") as LinkButton;
                    LinkButton button2 = e.Row.Cells[6].FindControl("LbtnDel") as LinkButton;
                    LinkButton button3 = e.Row.FindControl("LbtnDisabled") as LinkButton;
                    HyperLink link = e.Row.FindControl("HlnkApply") as HyperLink;
                    if (dataItem.IsDefault)
                    {
                        button.Enabled = false;
                        button2.Enabled = false;
                        button3.Enabled = false;
                    }
                    if (dataItem.IsDisabled)
                    {
                        button.Enabled = false;
                    }
                    if (dataItem.PayPlatformId < 100)
                    {
                        button2.Enabled = false;
                    }
                    if (button2.Enabled)
                    {
                        button2.OnClientClick = "return confirm('确定要删除此记录吗？');";
                    }
                    if (link != null)
                    {
                        switch (dataItem.PayPlatformId)
                        {
                            case 1:
                                link.NavigateUrl = "http://merchant3.chinabank.com.cn/register.do";
                                break;

                            case 2:
                                link.NavigateUrl = "http://www.ipay.cn";
                                break;

                            case 3:
                                link.NavigateUrl = "https://www.ips.com.cn";
                                break;

                            case 4:
                                link.NavigateUrl = "";
                                break;

                            case 5:
                                link.NavigateUrl = "http://www.yeepay.com/";
                                break;

                            case 6:
                                link.NavigateUrl = "http://new.xpay.cn/SignUp/Default.aspx";
                                break;

                            case 7:
                                link.NavigateUrl = "https://www.cncard.net";
                                break;

                            case 8:
                                link.NavigateUrl = "https://www.alipay.com/";
                                break;

                            case 9:
                                link.NavigateUrl = "http://www.99bill.com/";
                                break;

                            case 10:
                                link.NavigateUrl = "";
                                break;

                            case 11:
                                link.NavigateUrl = "http://www.99bill.com/";
                                break;

                            case 12:
                                link.NavigateUrl = "https://www.alipay.com/";
                                break;

                            case 13:
                                link.NavigateUrl = "http://union.tenpay.com/mch/mch_register.shtml?posid=123&actid=84&opid=50&whoid=31&sp_suggestuser=1201648901";
                                break;

                            default:
                                link.NavigateUrl = "";
                                break;
                        }
                        if (string.IsNullOrEmpty(link.NavigateUrl))
                        {
                            link.Enabled = false;
                        }
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            bool isPostBack = this.Page.IsPostBack;
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            int count = this.EgvPayPlatform.Rows.Count;
            string[] strArray = new string[count];
            ListItemCollection items = new ListItemCollection();
            for (int i = 1; i <= count; i++)
            {
                ListItem item = new ListItem(i.ToString(), i.ToString());
                items.Add(item);
            }
            foreach (GridViewRow row in this.EgvPayPlatform.Rows)
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

