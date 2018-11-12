namespace EasyOne.WebSite.Admin.Collection
{
    using EasyOne.Collection;
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.Collection;
    using EasyOne.Model.CommonModel;
    using EasyOne.Web.UI;
    using EasyOne.WorkFlows;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Web.UI.WebControls;

    public partial class ConfigStep3 : AdminPage
    {
        protected int m_itemId;
        private int m_ModelId;
        private int m_NodeId;


        protected void BtnCancel1_Click(object sender, EventArgs e)
        {
            this.Session["ShowCode"] = null;
            BasePage.ResponseRedirect("ConfigStep2.aspx?Action=Modify&ItemId=" + this.m_itemId);
        }

        protected void BtnCancel2_Click(object sender, EventArgs e)
        {
            this.Session["ShowCode"] = null;
            BasePage.ResponseRedirect("ItemManage.aspx");
        }

        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                bool flag = false;
                this.Session["ShowCode"] = null;
                string fieldName = "";
                foreach (RepeaterItem item in this.RptModelList.Items)
                {
                    RadioButton button = item.FindControl("RadDefault") as RadioButton;
                    RadioButton button2 = item.FindControl("RadDesignated") as RadioButton;
                    RadioButton button3 = item.FindControl("RadSet") as RadioButton;
                    TextBox box = item.FindControl("TxtDesignated") as TextBox;
                    Label label = item.FindControl("LblFieldName") as Label;
                    HiddenField field = item.FindControl("HdnFieldName") as HiddenField;
                    HiddenField field2 = item.FindControl("HdnFieldType") as HiddenField;
                    DropDownList list = item.FindControl("DropStatusType") as DropDownList;
                    bool flag2 = false;
                    if (((field.Value != "NodeId") && (field.Value != "InfoId")) && ((field.Value != "SpecialId") && (field.Value != "DefaultPicUrl")))
                    {
                        CollectionFieldRuleInfo infoById = CollectionFieldRules.GetInfoById(this.m_itemId, field.Value);
                        if (!infoById.IsNull)
                        {
                            flag2 = true;
                        }
                        else
                        {
                            infoById = new CollectionFieldRuleInfo();
                        }
                        infoById.FieldName = field.Value;
                        infoById.FieldType = field2.Value;
                        infoById.RuleType = 0;
                        infoById.ItemId = this.m_itemId;
                        if ((!button.Checked && !button2.Checked) && !button3.Checked)
                        {
                            AdminPage.WriteErrMsg(label.Text + "没有选择规则类型！");
                        }
                        if (button.Checked)
                        {
                            FieldInfo fieldInfoByFieldName = Field.GetFieldInfoByFieldName(this.m_ModelId, infoById.FieldName);
                            if (fieldInfoByFieldName.IsNull)
                            {
                                AdminPage.WriteErrMsg("<li>没有" + infoById.FieldName + "字段，请检查模型！");
                            }
                            infoById.SpecialSetting = fieldInfoByFieldName.DefaultValue;
                        }
                        if (button2.Checked)
                        {
                            infoById.RuleType = 1;
                            if (field.Value == "Status")
                            {
                                infoById.SpecialSetting = list.SelectedValue;
                            }
                            else
                            {
                                infoById.SpecialSetting = box.Text;
                            }
                        }
                        if (button3.Checked)
                        {
                            infoById.RuleType = 2;
                            if (string.IsNullOrEmpty(infoById.BeginCode) || string.IsNullOrEmpty(infoById.EndCode))
                            {
                                AdminPage.WriteErrMsg(label.Text + "使用采集规则，但规则为空请返回重新设置该字段采集规则！");
                            }
                        }
                        if (string.IsNullOrEmpty(fieldName))
                        {
                            fieldName = infoById.FieldName;
                        }
                        else
                        {
                            fieldName = fieldName + "," + infoById.FieldName;
                        }
                        if (flag2)
                        {
                            flag = CollectionFieldRules.Update(infoById);
                        }
                        else
                        {
                            flag = CollectionFieldRules.Add(infoById);
                        }
                        if (!flag)
                        {
                            AdminPage.WriteErrMsg(label.Text + "保存失败！");
                        }
                    }
                }
                if (!string.IsNullOrEmpty(fieldName))
                {
                    IList<CollectionFieldRuleInfo> list2 = CollectionFieldRules.GetList(this.m_ModelId);
                    string str2 = "";
                    foreach (CollectionFieldRuleInfo info3 in list2)
                    {
                        if (string.IsNullOrEmpty(str2))
                        {
                            str2 = info3.FieldName;
                        }
                        else
                        {
                            str2 = str2 + "," + info3.FieldName;
                        }
                    }
                    foreach (string str3 in str2.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (!StringHelper.FoundCharInArr(fieldName, str3))
                        {
                            CollectionFieldRules.DeleteFieldName(this.m_itemId, str3);
                        }
                    }
                }
                if (flag)
                {
                    AdminPage.WriteSuccessMsg(this.LblItemName.Text + "采集项目创建完毕！", "ItemManage.aspx");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int id = BasePage.RequestInt32("ItemId");
            if (id <= 0)
            {
                AdminPage.WriteErrMsg("<li>ItemID不存在！");
            }
            CollectionItemInfo infoById = CollectionItem.GetInfoById(id);
            if (infoById.IsNull)
            {
                AdminPage.WriteErrMsg("<li>当前采集项目不存在！");
            }
            this.LblItemName.Text = infoById.ItemName;
            this.m_itemId = infoById.ItemId;
            this.m_ModelId = infoById.ModelId;
            this.m_NodeId = infoById.NodeId;
            string str = BasePage.RequestString("Action", "Add");
            if (!base.IsPostBack)
            {
                this.RptModelList.DataSource = Field.GetFieldList(infoById.ModelId, false);
                this.RptModelList.DataBind();
                if (str == "Modify")
                {
                    this.BtnCancel1.Visible = true;
                }
                this.SmpNavigator.CurrentNode = string.Concat(new object[] { "<a title=\"采集项目设置\" href=\"ConfigStep1.aspx?Action=Modify&ItemId=", this.m_itemId, "&ModelId=", this.m_ModelId.ToString(), "&NodeId=", this.m_NodeId.ToString(), "\">采集项目设置</a> >> <a title=\"列表页采集设置\" href=\"ConfigStep2.aspx?Action=Modify&amp;ItemID=", id.ToString(), "\">列表页采集设置</a> >> <span style='color:red;'>内容页采集设置</span>" });
            }
            this.HdnAction.Value = str;
            CollectionCommon common = new CollectionCommon();
            Uri url = new Uri(infoById.Url);
            string httpPage = common.GetHttpPage(url, infoById.CodeType);
            CollectionListRuleInfo info2 = CollectionListRules.GetInfoById(infoById.ItemId);
            if (info2.IsNull)
            {
                AdminPage.WriteErrMsg("<li>隶属于" + infoById.ItemName + "采集项目的列表规则不存在！");
            }
            string code = common.GetInterceptionString(httpPage, info2.ListBeginCode, info2.ListEndCode);
            ArrayList list = common.GetArray(code, info2.LinkBeginCode, info2.LinkEndCode);
            if (list.Count < 1)
            {
                AdminPage.WriteErrMsg("<li>隶属于" + infoById.ItemName + "采集项目的链接规则设置错误无法捕捉到链接组！");
            }
            Uri uri2 = new Uri(common.ConvertToAbsluteUrl(list[0].ToString(), infoById.Url));
            this.Session["ShowCode"] = common.GetHttpPage(uri2, infoById.CodeType);
        }

        protected void RptModelList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType != ListItemType.Item) && (e.Item.ItemType != ListItemType.AlternatingItem))
            {
                return;
            }
            RadioButton button = e.Item.FindControl("RadDefault") as RadioButton;
            RadioButton button2 = e.Item.FindControl("RadDesignated") as RadioButton;
            RadioButton button3 = e.Item.FindControl("RadSet") as RadioButton;
            Label label = e.Item.FindControl("LblFieldName") as Label;
            Label label2 = e.Item.FindControl("LblSetField") as Label;
            HiddenField field = e.Item.FindControl("HdnFieldName") as HiddenField;
            HiddenField field2 = e.Item.FindControl("HdnFieldType") as HiddenField;
            TextBox box = e.Item.FindControl("TxtDesignated") as TextBox;
            e.Item.FindControl("ValrNumberValidator");
            DropDownList list = e.Item.FindControl("DropStatusType") as DropDownList;
            FieldInfo dataItem = (FieldInfo) e.Item.DataItem;
            label.Text = dataItem.FieldAlias;
            button2.GroupName = dataItem.FieldName;
            button.GroupName = dataItem.FieldName;
            button3.GroupName = dataItem.FieldName;
            field.Value = dataItem.FieldName;
            switch (dataItem.FieldType)
            {
                case FieldType.NodeType:
                case FieldType.InfoType:
                case FieldType.SpecialType:
                    e.Item.Visible = false;
                    break;

                case FieldType.StatusType:
                    list.Visible = true;
                    box.Visible = false;
                    button3.Enabled = false;
                    label2.Enabled = false;
                    break;
            }
            if (dataItem.FieldName == "DefaultPicUrl")
            {
                e.Item.Visible = false;
            }
            string s = dataItem.FieldType.ToString();
            string str2 = string.Concat(new object[] { "onclick=\"SetField(", this.m_ModelId, ",", this.m_itemId, ",'", base.Server.UrlEncode(field.Value), "','", base.Server.UrlEncode(s), "','", base.Server.UrlEncode(dataItem.FieldAlias), "')\"" });
            string str3 = BasePage.RequestString("Action", "Add");
            if (dataItem.FieldType == FieldType.StatusType)
            {
                button2.Checked = true;
                list.DataSource = Status.GetStatusList();
                list.DataTextField = "StatusName";
                list.DataValueField = "StatusCode";
                list.DataBind();
            }
            if (!(str3 == "Modify"))
            {
                if (dataItem.FieldName.CompareTo("PaginationType") == 0)
                {
                    button2.Checked = true;
                    box.Text = "手动分页";
                }
            }
            else
            {
                CollectionFieldRuleInfo infoById = CollectionFieldRules.GetInfoById(this.m_itemId, field.Value);
                if (!infoById.IsNull)
                {
                    switch (infoById.RuleType)
                    {
                        case 0:
                            box.Text = infoById.SpecialSetting;
                            goto Label_0394;

                        case 1:
                            button2.Checked = true;
                            if (dataItem.FieldType != FieldType.StatusType)
                            {
                                box.Text = infoById.SpecialSetting;
                            }
                            else
                            {
                                list.SelectedValue = infoById.SpecialSetting;
                            }
                            goto Label_0394;

                        case 2:
                            button3.Checked = true;
                            goto Label_0394;
                    }
                }
            }
        Label_0394:
            if (dataItem.EnableNull)
            {
                button.Checked = false;
                if (!button3.Checked && !button2.Checked)
                {
                    button2.Checked = true;
                    box.Text = dataItem.DefaultValue;
                }
            }
            field2.Value = s;
            if (dataItem.FieldType == FieldType.StatusType)
            {
                str2 = " disabled='disabled' ";
            }
            label2.Text = "<input type='button' value='设置采集规则' " + str2 + " >";
        }
    }
}

