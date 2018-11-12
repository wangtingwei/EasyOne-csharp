namespace EasyOne.WebSite.Controls.FieldControl
{
    using EasyOne.Common;
    using EasyOne.ExtendedControls;
    using EasyOne.Web.UI;
    using System;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class ListBoxType : BaseFieldControl
    {

        protected bool CheckSelectValue(string value)
        {
            string[] separator = new string[] { "$$$" };
            string[] strArray2 = base.Settings[0].Split(separator, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in strArray2)
            {
                string[] strArray3 = new string[] { "|" };
                string[] strArray4 = str.Split(strArray3, StringSplitOptions.None);
                if (strArray4.Length > 1)
                {
                    if (strArray4[1] == value)
                    {
                        return true;
                    }
                }
                else if (str == value)
                {
                    return true;
                }
            }
            return false;
        }

        protected string GetListControlSelectedValue(ListControl control)
        {
            StringBuilder sb = new StringBuilder("");
            foreach (ListItem item in control.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "listboxType_IsSecltCustom_Item")
                    {
                        StringHelper.AppendString(sb, item.Value);
                    }
                    else if (!string.IsNullOrEmpty(this.TxtListItem.Text))
                    {
                        StringHelper.AppendString(sb, this.TxtListItem.Text);
                    }
                }
            }
            return sb.ToString();
        }

        private string GetSelectedValue()
        {
            string listControlSelectedValue = "";
            string str2 = base.Settings[1];
            if (str2 != null)
            {
                if (!(str2 == "1"))
                {
                    if (str2 != "2")
                    {
                        if (str2 == "3")
                        {
                            if (this.RadlList.SelectedValue == "listboxType_IsSecltCustom_Item")
                            {
                                return this.TxtListItem.Text;
                            }
                            return this.RadlList.SelectedValue;
                        }
                        if (str2 != "4")
                        {
                            return listControlSelectedValue;
                        }
                        return this.GetListControlSelectedValue(this.ChkList);
                    }
                }
                else
                {
                    if (DataConverter.CBoolean(base.Settings[2]) && this.RadSelectTxt.Checked)
                    {
                        return this.TxtListItem.Text;
                    }
                    return this.DrpList.SelectedValue;
                }
                listControlSelectedValue = this.GetListControlSelectedValue(this.LstListBox);
                if (!string.IsNullOrEmpty(this.TxtListItem.Text))
                {
                    listControlSelectedValue = listControlSelectedValue + "," + this.TxtListItem.Text;
                }
            }
            return listControlSelectedValue;
        }

        protected void InitContorol()
        {
            string[] separator = new string[] { "$$$" };
            string[] selectItem = base.Settings[0].Split(separator, StringSplitOptions.RemoveEmptyEntries);
            string str = base.Settings[1];
            if (str != null)
            {
                if (!(str == "1"))
                {
                    if (str == "2")
                    {
                        this.PlhListBox.Visible = true;
                        this.LstListBox.SelectionMode = ListSelectionMode.Multiple;
                        this.InitListControl(this.LstListBox, selectItem);
                    }
                    else if (str == "3")
                    {
                        if (DataConverter.CBoolean(base.Settings[2]))
                        {
                            this.LitNbsp.Visible = true;
                        }
                        this.PlhRadioList.Visible = true;
                        if (base.Settings.Count > 3)
                        {
                            this.RadlList.RepeatDirection = RepeatDirection.Horizontal;
                            this.RadlList.RepeatColumns = DataConverter.CLng(base.Settings[3], 1);
                        }
                        this.InitListControl(this.RadlList, selectItem);
                    }
                    else if (str == "4")
                    {
                        if (DataConverter.CBoolean(base.Settings[2]))
                        {
                            this.LitNbsp.Visible = true;
                        }
                        this.PlhCheckBox.Visible = true;
                        if (base.Settings.Count > 3)
                        {
                            this.ChkList.RepeatDirection = RepeatDirection.Horizontal;
                            this.ChkList.RepeatColumns = DataConverter.CLng(base.Settings[3], 1);
                        }
                        this.InitListControl(this.ChkList, selectItem);
                    }
                }
                else
                {
                    this.PlhDropList.Visible = true;
                    if (DataConverter.CBoolean(base.Settings[2]))
                    {
                        this.RadSelectDrop.Visible = true;
                        this.RadSelectTxt.Visible = true;
                        this.LitBr.Visible = true;
                        this.LitNbsp.Visible = true;
                    }
                    this.InitListControl(this.DrpList, selectItem);
                }
            }
            if (DataConverter.CBoolean(base.Settings[2]))
            {
                this.PlhTextBox.Visible = true;
            }
        }

        protected void InitContorolValue()
        {
            if (!string.IsNullOrEmpty(this.FieldValue))
            {
                if (this.IsSingleSelect)
                {
                    if (!this.CheckSelectValue(this.FieldValue))
                    {
                        this.TxtListItem.Text = this.FieldValue;
                        if (base.Settings[1] == "1")
                        {
                            this.RadSelectTxt.Checked = true;
                            this.RadSelectDrop.Checked = false;
                        }
                        else if (this.RadlList.Items.FindByValue("listboxType_IsSecltCustom_Item") != null)
                        {
                            this.RadlList.Items.FindByValue("listboxType_IsSecltCustom_Item").Selected = true;
                        }
                    }
                    else
                    {
                        this.SetSelectedValue(this.FieldValue);
                        if (base.Settings[1] == "1")
                        {
                            this.RadSelectTxt.Checked = false;
                            this.RadSelectDrop.Checked = true;
                        }
                    }
                }
                else
                {
                    this.SetSelectedValue(this.FieldValue);
                }
            }
        }

        protected void InitListControl(ListControl control, string[] selectItem)
        {
            string[] separator = new string[] { "|" };
            for (int i = 0; i < selectItem.Length; i++)
            {
                ListItem item = new ListItem();
                string[] strArray2 = selectItem[i].Split(separator, StringSplitOptions.RemoveEmptyEntries);
                if (strArray2.Length > 1)
                {
                    item.Text = strArray2[0];
                    item.Value = strArray2[1];
                }
                else
                {
                    item.Text = strArray2[0];
                    item.Value = strArray2[0];
                }
                control.Items.Add(item);
            }
            if (DataConverter.CBoolean(base.Settings[2]) && ((base.Settings[1] == "3") || (base.Settings[1] == "4")))
            {
                control.Items.Add(new ListItem("指定自定义值", "listboxType_IsSecltCustom_Item"));
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (base.IsPostBack)
            {
                this.FieldValue = this.GetSelectedValue();
            }
            else
            {
                this.InitContorol();
                this.InitContorolValue();
            }
        }

        protected void SetListControlValue(ListControl control, string value)
        {
            foreach (string str in value.Split(new char[] { ',' }))
            {
                bool flag = false;
                foreach (ListItem item in control.Items)
                {
                    if (item.Value == str)
                    {
                        item.Selected = true;
                        flag = true;
                        break;
                    }
                }
                if (!flag && !string.IsNullOrEmpty(str))
                {
                    if (control.Items.FindByValue("listboxType_IsSecltCustom_Item") != null)
                    {
                        control.Items.FindByValue("listboxType_IsSecltCustom_Item").Selected = true;
                    }
                    this.TxtListItem.Text = str;
                }
            }
        }

        protected void SetSelectedValue(string value)
        {
            string str = base.Settings[1];
            if (str != null)
            {
                if (!(str == "1"))
                {
                    if (!(str == "2"))
                    {
                        if (!(str == "3"))
                        {
                            if (str == "4")
                            {
                                this.SetListControlValue(this.ChkList, value);
                            }
                            return;
                        }
                        BaseUserControl.SetSelectedIndexByValue(this.RadlList, value);
                        return;
                    }
                }
                else
                {
                    BaseUserControl.SetSelectedIndexByValue(this.DrpList, value);
                    return;
                }
                this.SetListControlValue(this.LstListBox, value);
            }
        }

        protected bool IsSingleSelect
        {
            get
            {
                if (base.Settings[1] != "1")
                {
                    return (base.Settings[1] == "3");
                }
                return true;
            }
        }
    }
}

