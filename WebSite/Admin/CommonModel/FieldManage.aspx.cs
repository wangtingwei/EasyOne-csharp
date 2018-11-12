namespace EasyOne.WebSite.Admin.CommonModel
{
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Controls;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.CommonModel;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class FieldManage : AdminPage
    {

        protected void BtnPreView_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("ModelPreView.aspx?ModelType=" + BasePage.RequestString("ModelType") + "&ModelId=" + BasePage.RequestString("ModelId") + "&ModelName=" + base.Server.UrlEncode(BasePage.RequestString("ModelName")) + "&Action=Model");
        }

        protected void EBtnSetOrderId_Click(object sender, EventArgs e)
        {
            List<FieldInfo> fieldInfoList = new List<FieldInfo>();
            foreach (GridViewRow row in this.EgvField.Rows)
            {
                FieldInfo item = new FieldInfo();
                int num = DataConverter.CLng(((DropDownList) row.FindControl("DropOrderId")).SelectedValue);
                string str = (string) this.EgvField.DataKeys[row.RowIndex].Value;
                item.OrderId = num;
                item.Id = str;
                fieldInfoList.Add(item);
            }
            int modelId = BasePage.RequestInt32("ModelID");
            EasyOne.CommonModel.Field.SetOrderId(fieldInfoList, modelId);
            this.EgvField.DataBind();
        }

        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("Field.aspx?ModelType=" + BasePage.RequestInt32("ModelType").ToString() + "&ModelId=" + BasePage.RequestString("ModelId") + "&ModelName=" + base.Server.HtmlEncode(BasePage.RequestString("ModelName")));
        }

        protected void EgvField_RowCommand(object sender, CommandEventArgs e)
        {
            int modelId = BasePage.RequestInt32("ModelID");
            if (e.CommandName == "DeleteField")
            {
                if (EasyOne.CommonModel.Field.Delete((string) e.CommandArgument, modelId))
                {
                    AdminPage.WriteSuccessMsg("删除字段成功。", "FieldManage.aspx?ModelType=" + BasePage.RequestInt32("ModelType").ToString() + "&ModelID=" + modelId.ToString());
                }
                else
                {
                    AdminPage.WriteErrMsg("删除字段失败！", "FieldManage.aspx?ModelType=" + BasePage.RequestInt32("ModelType").ToString() + "&ModelID=" + modelId.ToString());
                }
            }
            if (e.CommandName == "Disabled")
            {
                EasyOne.CommonModel.Field.SetDisabled((string) e.CommandArgument, modelId, true);
                this.EgvField.DataBind();
            }
            if (e.CommandName == "Enabled")
            {
                EasyOne.CommonModel.Field.SetDisabled((string) e.CommandArgument, modelId, false);
                this.EgvField.DataBind();
            }
        }

        protected void EgvField_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                FieldInfo dataItem = (FieldInfo) e.Row.DataItem;
                LinkButton button = e.Row.FindControl("ELbtnDelField") as LinkButton;
                if (dataItem.FieldLevel == 0)
                {
                    if ((dataItem.FieldName != "DefaultPicUrl") || (dataItem.FieldLevel != 0))
                    {
                        ((LinkButton) e.Row.FindControl("ELbtnDisabled")).Enabled = false;
                    }
                    if (button != null)
                    {
                        button.Enabled = false;
                    }
                }
                else
                {
                    button.Attributes.Add("onclick", "if(!this.disabled) return confirm('删除字段将删除对应表中所有该字段的数据，是否删除该字段？');");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ModelManager.GetModelInfoById(BasePage.RequestInt32("ModelID")).IsNull)
            {
                AdminPage.WriteErrMsg("<li>模型不存在！</li>");
            }
            this.LblModelName.Text = "当前模型：<a href='FieldManage.aspx?ModelType=" + BasePage.RequestInt32("ModelType").ToString() + "&ModelId=" + BasePage.RequestString("ModelId") + "&ModelName=" + base.Server.UrlEncode(BasePage.RequestString("ModelName")) + "'>" + BasePage.RequestString("ModelName") + "</a>";
            this.SmpNavigator.CurrentNode = "<a href='FieldManage.aspx?ModelType=" + BasePage.RequestInt32("ModelType").ToString() + "&ModelId=" + BasePage.RequestString("ModelId") + "&ModelName=" + base.Server.HtmlEncode(BasePage.RequestString("ModelName")) + "'>字段管理</a>";
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            int count = this.EgvField.Rows.Count;
            string[] strArray = new string[count];
            ListItemCollection items = new ListItemCollection();
            for (int i = 1; i <= count; i++)
            {
                ListItem item = new ListItem(i.ToString(), i.ToString());
                items.Add(item);
            }
            foreach (GridViewRow row in this.EgvField.Rows)
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

