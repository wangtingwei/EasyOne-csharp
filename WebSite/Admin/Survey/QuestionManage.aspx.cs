namespace EasyOne.WebSite.Admin.Survey
{
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.Survey;
    using EasyOne.Survey;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class QuestionManage : AdminPage
    {
        protected int isOpen;
        protected int surveyId;

        protected void BtnAddQuestion_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("Question.aspx?Action=Add&SurveyID=" + this.surveyId);
        }

        protected void BtnDel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.EgvQuestion.SelectList.ToString()))
            {
                AdminPage.WriteErrMsg("请指定要删除的问题ID");
            }
            else if (SurveyField.BatchDelete(this.surveyId, this.EgvQuestion.SelectList.ToString()))
            {
                AdminPage.WriteSuccessMsg("删除问题成功！", "QuestionManage.aspx?SurveyID=" + this.surveyId);
            }
        }

        protected void BtnOrderId_Click(object sender, EventArgs e)
        {
            IList<SurveyFieldInfo> fieldList = SurveyField.GetFieldList(this.surveyId);
            Dictionary<int, SurveyFieldInfo> temdic = new Dictionary<int, SurveyFieldInfo>();
            int count = fieldList.Count;
            int num2 = 0;
            foreach (GridViewRow row in this.EgvQuestion.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    int key = DataConverter.CLng(((DropDownList) row.FindControl("DropOrderId")).SelectedValue);
                    temdic.Add(key, fieldList[num2]);
                    num2++;
                }
            }
            if (SurveyField.Update(this.surveyId, this.Sort(temdic, count)))
            {
                AdminPage.WriteSuccessMsg("保存排序成功！", "QuestionManage.aspx?SurveyID=" + this.surveyId);
            }
            else
            {
                AdminPage.WriteErrMsg("保存排序失败！");
            }
        }

        protected void EgvQuestion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                SurveyFieldInfo dataItem = e.Row.DataItem as SurveyFieldInfo;
                if (this.EgvQuestion.AutoGenerateCheckBoxColumn)
                {
                    e.Row.Cells[2].Text = SurveyField.GetQuestionType(dataItem.QuestionType);
                }
                else
                {
                    e.Row.Cells[1].Text = SurveyField.GetQuestionType(dataItem.QuestionType);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.surveyId = DataConverter.CLng(base.Request.QueryString["SurveyID"]);
            SurveyInfo surveyById = SurveyManager.GetSurveyById(this.surveyId);
            this.SmpNavigator.AdditionalNode = "<b>[</b><font color=red>" + DataSecurity.HtmlEncode(surveyById.SurveyName) + "</font><b>]</b>题目列表";
            this.isOpen = surveyById.IsOpen;
            if (this.isOpen == 1)
            {
                this.EgvQuestion.Columns[3].Visible = false;
                this.BtnAddQuestion.Visible = false;
                this.BtnSetOrderId.Visible = false;
                this.BtnDel.Visible = false;
                this.EgvQuestion.AutoGenerateCheckBoxColumn = false;
            }
            if (!base.IsPostBack)
            {
                int questionId = DataConverter.CLng(base.Request.QueryString["QuestionId"]);
                string str = base.Request.QueryString["Action"];
                if (str == "Delete")
                {
                    SurveyField.Delete(this.surveyId, questionId);
                }
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            int count = this.EgvQuestion.Rows.Count;
            string[] strArray = new string[count];
            ListItemCollection items = new ListItemCollection();
            for (int i = 1; i <= count; i++)
            {
                ListItem item = new ListItem(i.ToString(), i.ToString());
                items.Add(item);
            }
            foreach (GridViewRow row in this.EgvQuestion.Rows)
            {
                DropDownList list = (DropDownList) row.FindControl("DropOrderId");
                list.DataSource = items;
                list.DataBind();
                list.SelectedIndex = row.RowIndex;
                strArray[row.RowIndex] = list.ClientID;
                list.Attributes.Add("onchange", "Reorder(this, " + row.RowIndex.ToString() + "," + count.ToString() + ")");
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("<script language=\"JavaScript\" type=\"text/JavaScript\">\n");
            builder.Append("var arrId = new Array( ");
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

        protected IList<SurveyFieldInfo> Sort(Dictionary<int, SurveyFieldInfo> temdic, int questionNum)
        {
            IList<SurveyFieldInfo> list = new List<SurveyFieldInfo>();
            for (int i = 1; i <= questionNum; i++)
            {
                foreach (KeyValuePair<int, SurveyFieldInfo> pair in temdic)
                {
                    if (pair.Key == i)
                    {
                        list.Add(pair.Value);
                    }
                }
            }
            return list;
        }
    }
}

