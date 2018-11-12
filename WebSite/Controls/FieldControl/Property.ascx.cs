namespace EasyOne.WebSite.Controls.FieldControl
{
    using AjaxControlToolkit;
    using EasyOne.Common;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class Property : BaseFieldControl
    {
        private bool m_IsModify;

        private void AddProperty(string item)
        {
            if (!string.IsNullOrEmpty(item))
            {
                IList<string> propertyItemList = this.GetPropertyItemList(item);
                this.RptSelectPropertyItem.DataSource = propertyItemList;
                this.RptSelectPropertyItem.DataBind();
                this.SetProperties(propertyItemList);
            }
        }

        protected void DelItem_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "DelPropertyItem")
            {
                this.DelProperty(e.CommandArgument.ToString());
            }
            this.RptSelectPropertyItem.DataSource = this.ViewState["PropertyItemList" + base.FieldName] as List<string>;
            this.RptSelectPropertyItem.DataBind();
        }

        private void DelProperty(string delItem)
        {
            IList<string> propertyList = new List<string>();
            if (this.ViewState["PropertyItemList" + base.FieldName] != null)
            {
                propertyList = this.ViewState["PropertyItemList" + base.FieldName] as List<string>;
            }
            propertyList.Remove(delItem);
            this.SetProperties(propertyList);
        }

        private IList<string> GetPropertyItemList(string addItem)
        {
            IList<string> list = new List<string>();
            if (this.ViewState["PropertyItemList" + base.FieldName] != null)
            {
                list = this.ViewState["PropertyItemList" + base.FieldName] as List<string>;
            }
            if (!list.Contains(addItem))
            {
                list.Add(addItem);
            }
            this.ViewState["PropertyItemList" + base.FieldName] = list;
            return list;
        }

        private void InitializeDropPanel()
        {
            if (base.Settings.Count > 0)
            {
                string[] strArray = base.Settings[0].Split(new char[] { '|' });
                int num = 0;
                foreach (string str in strArray)
                {
                    LinkButton child = new LinkButton();
                    child.Text = str;
                    child.ID = "LbtnProperty" + num;
                    child.CssClass = "ContextMenuItem";
                    child.CausesValidation = false;
                    child.Click += new EventHandler(this.OnSelect);
                    this.DropPanel.Controls.Add(child);
                    num++;
                }
            }
        }

        protected void OnSelect(object sender, EventArgs e)
        {
            this.AddProperty(((LinkButton) sender).Text);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.m_IsModify = BaseUserControl.RequestStringToLower("Action") == "modify";
            this.InitializeDropPanel();
            if (!this.Page.IsPostBack && this.m_IsModify)
            {
                List<string> list = new List<string>(this.FieldValue.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries));
                this.ViewState["ReadOnlyCount"] = list.Count;
                this.RptSelectPropertyItem.DataSource = list;
                this.RptSelectPropertyItem.DataBind();
                this.ViewState["PropertyItemList" + base.FieldName] = list;
                this.Properties = this.FieldValue;
            }
        }

        protected void RptSelectPropertyItem_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.AlternatingItem) || (e.Item.ItemType == ListItemType.Item))
            {
                LinkButton button = (LinkButton) e.Item.FindControl("DelItem");
                int num = DataConverter.CLng(this.ViewState["ReadOnlyCount"]);
                if (this.m_IsModify && (e.Item.ItemIndex < num))
                {
                    if (num != 0)
                    {
                        button.Enabled = false;
                        button.ForeColor = Color.Gray;
                    }
                }
                else
                {
                    button.OnClientClick = "javascript:return confirm('确定不选择该属性项？')";
                }
            }
        }

        private void SetProperties(IList<string> propertyList)
        {
            string[] array = new string[propertyList.Count];
            propertyList.CopyTo(array, 0);
            this.Properties = string.Join("|", array);
        }

        protected void TxtProperty_TextChanged(object sender, EventArgs e)
        {
            string str = this.TxtProperty.Text.Trim().Replace("$", "").Replace("|", "").Replace("*", "");
            if (!string.IsNullOrEmpty(str))
            {
                this.AddProperty(str);
            }
            this.TxtProperty.Text = "点击选择";
        }

        public string Properties
        {
            get
            {
                return (this.ViewState["Properties"] as string);
            }
            set
            {
                this.FieldValue = value;
                this.ViewState["Properties"] = value;
            }
        }
    }
}

