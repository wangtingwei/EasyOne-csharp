namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Model.Accessories;
    using EasyOne.Web.UI;
    using System;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class Choicesets : AdminPage
    {
        private ChoicesetInfo choicesetInfo;

        private void CreateFormFieldValueTable()
        {
            Table child = new Table();
            child.Width = Unit.Parse("100%");
            child.Attributes.Add("class", "border");
            TableRow row = new TableRow();
            row.Attributes.Add("class", "title");
            row.Attributes.Add("align", "center");
            row.Attributes.Add("height", "25");
            TableCell cell = new TableCell();
            cell.Text = "序号";
            row.Cells.Add(cell);
            TableCell cell2 = new TableCell();
            cell2.Text = "默认";
            row.Cells.Add(cell2);
            TableCell cell3 = new TableCell();
            cell3.Text = "启用";
            row.Cells.Add(cell3);
            TableCell cell4 = new TableCell();
            cell4.Text = "选项值";
            row.Cells.Add(cell4);
            child.Rows.Add(row);
            ChoicesetValueInfoCollection dictionaryFieldValue = Choiceset.GetDictionaryFieldValue(this.choicesetInfo);
            for (int i = 0; i < (dictionaryFieldValue.Count + 3); i++)
            {
                ChoicesetValueInfo info;
                if (i < dictionaryFieldValue.Count)
                {
                    info = dictionaryFieldValue[i];
                }
                else
                {
                    info = new ChoicesetValueInfo();
                    info.IsDefault = false;
                    info.IsEnable = false;
                    info.DataTextField = string.Empty;
                }
                TableRow row2 = new TableRow();
                row2.Attributes.Add("class", "tdbg");
                row2.Attributes.Add("align", "center");
                TableCell cell5 = new TableCell();
                cell5.Text = i.ToString();
                row2.Cells.Add(cell5);
                TableCell cell6 = new TableCell();
                StringBuilder builder = new StringBuilder();
                builder.Append("");
                builder.Append("<Input type='radio' name='rad' ");
                builder.Append("id='rad");
                builder.Append(i.ToString());
                builder.Append("' value=");
                builder.Append(i.ToString());
                if (info.IsDefault)
                {
                    builder.Append(" checked");
                }
                builder.Append(">");
                cell6.Text = builder.ToString();
                row2.Cells.Add(cell6);
                TableCell cell7 = new TableCell();
                CheckBox box = new CheckBox();
                box.ID = "chk" + ((i + 1)).ToString();
                box.EnableViewState = false;
                box.Checked = info.IsEnable;
                cell7.Controls.Add(box);
                row2.Cells.Add(cell7);
                TableCell cell8 = new TableCell();
                cell8.Attributes.Add("align", "left");
                TextBox box2 = new TextBox();
                box2.ApplyStyleSheetSkin(this);
                box2.Text = info.DataTextField;
                box2.ID = "txt" + ((i + 1)).ToString();
                box2.EnableViewState = false;
                cell8.Controls.Add(box2);
                row2.Cells.Add(cell8);
                child.Rows.Add(row2);
            }
            child.CellPadding = 0;
            child.CellSpacing = 1;
            this.PlhFormFieldValue.Controls.Add(child);
        }

        protected void EBtnSave_Click(object sender, EventArgs e)
        {
            string tableName = DataSecurity.FilterBadChar(BasePage.RequestString("TableName", "PE_Client"));
            string fieldName = DataSecurity.FilterBadChar(BasePage.RequestString("FieldName", "Area"));
            ChoicesetValueInfoCollection dictionaryFieldValueByName = Choiceset.GetDictionaryFieldValueByName(tableName, fieldName);
            int num = 0;
            bool flag = false;
            string text = "";
            string id = "";
            string str5 = "";
            num = DataConverter.CLng(base.Request.Form["rad"]);
            bool flag3 = false;
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < (dictionaryFieldValueByName.Count + 3); i++)
            {
                id = "chk" + ((i + 1)).ToString();
                str5 = "txt" + ((i + 1)).ToString();
                flag = ((CheckBox) this.PlhFormFieldValue.FindControl(id)).Checked;
                text = ((TextBox) this.PlhFormFieldValue.FindControl(str5)).Text;
                if (i == num)
                {
                    flag3 = true;
                }
                if (!string.IsNullOrEmpty(text))
                {
                    builder.Append(text);
                    builder.Append("|");
                    if (flag)
                    {
                        builder.Append("1");
                    }
                    else
                    {
                        builder.Append("0");
                    }
                    builder.Append("|");
                    if (flag3)
                    {
                        builder.Append("1");
                    }
                    else
                    {
                        builder.Append("0");
                    }
                    builder.Append("$");
                }
                flag3 = false;
            }
            if (Choiceset.SetFieldValue(builder.ToString(), tableName, fieldName))
            {
                AdminPage.WriteSuccessMsg("已经成功保存您所设置的数据字典信息！", "Choiceset.aspx?TableName=" + tableName + "&FieldName=" + fieldName);
            }
            else
            {
                AdminPage.WriteErrMsg("数据字典信息保存失败！", "Choiceset.aspx?TableName=" + tableName + "&FieldName=" + fieldName);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            string tableName = DataSecurity.FilterBadChar(BasePage.RequestString("TableName", "PE_Client"));
            string fieldName = DataSecurity.FilterBadChar(BasePage.RequestString("FieldName", "Area"));
            this.choicesetInfo = Choiceset.GetChoicesetInfoByFieldAndTableName(tableName, fieldName);
            this.CreateFormFieldValueTable();
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.SmpNavigator.AdditionalNode = this.choicesetInfo.Title;
        }
    }
}

