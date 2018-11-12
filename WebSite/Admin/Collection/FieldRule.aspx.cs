namespace EasyOne.WebSite.Admin.Collection
{
    using EasyOne.Collection;
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.Collection;
    using EasyOne.Model.CommonModel;
    using EasyOne.Web.UI;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class FieldRule : AdminPage
    {
        protected string m_CodeType = "GB2312";
        private string m_FieldName;
        protected string m_FieldType;
        private int m_ItemId;
        protected string m_Url = "";

        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            CollectionFieldRuleInfo collectionFieldRuleInfo = new CollectionFieldRuleInfo();
            collectionFieldRuleInfo.ItemId = BasePage.RequestInt32("ItemId");
            collectionFieldRuleInfo.FieldName = BasePage.RequestString("FieldName");
            collectionFieldRuleInfo.FieldType = BasePage.RequestString("FieldType");
            collectionFieldRuleInfo.RuleType = 2;
            collectionFieldRuleInfo.BeginCode = this.TxtFieldBegin.Text;
            collectionFieldRuleInfo.EndCode = this.TxtFieldEnd.Text;
            if (!this.RadlPaingType.Checked)
            {
                collectionFieldRuleInfo.UsePaging = true;
            }
            FieldType none = FieldType.None;
            if (Enum.IsDefined(typeof(FieldType), collectionFieldRuleInfo.FieldType))
            {
                none = (FieldType) Enum.Parse(typeof(FieldType), collectionFieldRuleInfo.FieldType);
            }
            if (((none != FieldType.BoolType) || (none != FieldType.NumberType)) || ((none != FieldType.MoneyType) || (none != FieldType.DateTimeType)))
            {
                StringBuilder builder = new StringBuilder();
                foreach (ListItem item in this.ListFilterRuleID.Items)
                {
                    if (item.Selected)
                    {
                        if (builder.Length > 0)
                        {
                            builder.Append("," + item.Value);
                        }
                        else
                        {
                            builder.Append(item.Value);
                        }
                    }
                }
                collectionFieldRuleInfo.FilterRuleId = builder.ToString();
                StringBuilder builder2 = new StringBuilder();
                foreach (ListItem item2 in this.ListFilterSelect.Items)
                {
                    if (item2.Selected)
                    {
                        if (builder2.Length > 0)
                        {
                            builder2.Append("," + item2.Value);
                        }
                        else
                        {
                            builder2.Append(item2.Value);
                        }
                    }
                }
                collectionFieldRuleInfo.PrivateFilter = builder2.ToString();
                switch (none)
                {
                    case FieldType.FileType:
                    {
                        FieldInfo fieldInfoByFieldName = Field.GetFieldInfoByFieldName(BasePage.RequestInt32("ModelId"), collectionFieldRuleInfo.FieldName);
                        collectionFieldRuleInfo.SpecialSetting = fieldInfoByFieldName.Settings[1].ToString() + "$$$" + fieldInfoByFieldName.Settings[3].ToString() + "$$$" + fieldInfoByFieldName.Settings[4].ToString();
                        goto Label_02BF;
                    }
                    case FieldType.KeywordType:
                        collectionFieldRuleInfo.SpecialSetting = this.TxtKeyWord.Text;
                        break;

                    case FieldType.ContentType:
                        collectionFieldRuleInfo.SpecialSetting = this.SavePhoto.Checked.ToString();
                        goto Label_02BF;
                }
            }
            else
            {
                collectionFieldRuleInfo.FilterRuleId = "";
                collectionFieldRuleInfo.PrivateFilter = "";
            }
        Label_02BF:
            collectionFieldRuleInfo.ExclosionId = DataConverter.CLng(this.DropExclosionId.SelectedValue);
            if (CollectionFieldRules.Exists(this.m_ItemId, this.m_FieldName))
            {
                CollectionFieldRules.Update(collectionFieldRuleInfo);
            }
            else
            {
                CollectionFieldRules.Add(collectionFieldRuleInfo);
            }
            if (none == FieldType.ContentType)
            {
                this.SavePaing();
            }
            this.Page.ClientScript.RegisterClientScriptBlock(base.GetType(), "windowsclose", "<script type='text/javascript'>window.close();</script>");
        }

        protected void ExclosionList(int exclosionId, int exclosionType)
        {
            IList<CollectionExclosionInfo> list = CollectionExclosion.GetList(exclosionType);
            this.DropExclosionId.DataSource = list;
            this.DropExclosionId.DataTextField = "ExclosionName";
            this.DropExclosionId.DataValueField = "ExclosionID";
            this.DropExclosionId.DataBind();
            ListItem item = new ListItem();
            item.Text = "不选择排除";
            item.Value = "0";
            this.DropExclosionId.Items.Insert(0, item);
            this.DropExclosionId.SelectedValue = exclosionId.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.m_ItemId = BasePage.RequestInt32("ItemId");
            this.m_FieldName = BasePage.RequestString("FieldName");
            this.m_FieldType = BasePage.RequestString("FieldType");
            string str = BasePage.RequestString("FieldAlias");
            if (this.m_ItemId <= 0)
            {
                AdminPage.WriteErrMsg("<li>ItemID不存在！");
            }
            if (string.IsNullOrEmpty(this.m_FieldName))
            {
                AdminPage.WriteErrMsg("<li>字段名称不存在！");
            }
            if (string.IsNullOrEmpty(this.m_FieldType))
            {
                AdminPage.WriteErrMsg("<li>字段类型不能为空！");
            }
            this.LblFieldName.Text = str + "[" + this.m_FieldType + "]";
            FieldType none = FieldType.None;
            if (Enum.IsDefined(typeof(FieldType), this.m_FieldType))
            {
                none = (FieldType) Enum.Parse(typeof(FieldType), this.m_FieldType);
            }
            else
            {
                AdminPage.WriteErrMsg("<li>不是系统字段类型！");
            }
            CollectionItemInfo infoById = CollectionItem.GetInfoById(this.m_ItemId);
            if (infoById.IsNull)
            {
                AdminPage.WriteErrMsg("<li>当前采集项目不存在！");
            }
            CollectionListRuleInfo info2 = CollectionListRules.GetInfoById(infoById.ItemId);
            if (info2.IsNull)
            {
                AdminPage.WriteErrMsg("<li>隶属于" + infoById.ItemName + "采集项目的列表规则不存在！");
            }
            CollectionCommon common = new CollectionCommon();
            Uri url = new Uri(infoById.Url);
            string httpPage = common.GetHttpPage(url, infoById.CodeType);
            string code = common.GetInterceptionString(httpPage, info2.ListBeginCode, info2.ListEndCode);
            try
            {
                ArrayList list = common.GetArray(code, info2.LinkBeginCode, info2.LinkEndCode);
                if ((this.Session["ShowCode"] != null) && !string.IsNullOrEmpty(this.Session["ShowCode"].ToString()))
                {
                    this.TxtShowCode.Text = this.Session["ShowCode"].ToString();
                }
                else
                {
                    Uri uri2 = new Uri(common.ConvertToAbsluteUrl(list[0].ToString(), infoById.Url));
                    this.TxtShowCode.Text = common.GetHttpPage(uri2, infoById.CodeType);
                    this.Session["ShowCode"] = this.TxtShowCode.Text;
                }
                this.HdnTestContent.Value = this.TxtShowCode.Text;
                StringBuilder builder = new StringBuilder();
                builder.Append("<select name='ArrContentUrl' onchange=\"testContentLink(this.options[this.selectedIndex].value)\">");
                string str4 = "";
                for (int i = 0; i < list.Count; i++)
                {
                    str4 = common.ConvertToAbsluteUrl(list[i].ToString(), url.ToString());
                    builder.Append(" <option value='" + str4 + "'>" + str4 + "</option>");
                }
                this.m_Url = common.ConvertToAbsluteUrl(list[0].ToString(), url.ToString());
                builder.Append("</select>&nbsp;&nbsp;<a href='" + this.m_Url + "' id='contentLink' target='_blank' >查看原始网页</a>");
                this.LblArrContentUrl.Text = builder.ToString();
                this.m_CodeType = infoById.CodeType;
            }
            catch (ArgumentOutOfRangeException exception)
            {
                AdminPage.WriteErrMsg("<li>" + exception.Message + "</li>");
            }
            this.ListFilterRuleID.Items.Add(new ListItem("不设置过滤", "0"));
            this.ListFilterRuleID.AppendDataBoundItems = true;
            this.ListFilterRuleID.DataSource = CollectionFilterRules.GetList(0, 0);
            this.ListFilterRuleID.DataBind();
            if (!base.IsPostBack)
            {
                CollectionFieldRuleInfo info3 = CollectionFieldRules.GetInfoById(infoById.ItemId, this.m_FieldName);
                if (!info3.IsNull)
                {
                    this.TxtFieldBegin.Text = info3.BeginCode;
                    this.TxtFieldEnd.Text = info3.EndCode;
                    this.TxtKeyWord.Text = info3.SpecialSetting;
                    this.SavePhoto.Checked = DataConverter.CBoolean(info3.SpecialSetting);
                    foreach (ListItem item in this.ListFilterRuleID.Items)
                    {
                        if (StringHelper.FoundCharInArr(info3.FilterRuleId, item.Value))
                        {
                            item.Selected = true;
                        }
                    }
                    foreach (ListItem item2 in this.ListFilterSelect.Items)
                    {
                        if (StringHelper.FoundCharInArr(info3.PrivateFilter, item2.Value))
                        {
                            item2.Selected = true;
                        }
                    }
                }
                FieldType type2 = none;
                if (type2 == FieldType.KeywordType)
                {
                    this.LblKeyWord.Visible = true;
                    this.TxtKeyWord.Visible = true;
                    this.ValeKeyWord.Visible = true;
                }
                else if (type2 == FieldType.ContentType)
                {
                    this.TabTitle1.Visible = true;
                    this.SavePhoto.Visible = true;
                    this.RadlPaingType.Attributes.Add("onclick", "javascript:ListPaing(0);");
                    this.RadlPaingType1.Attributes.Add("onclick", "javascript:ListPaing(1);");
                    this.RadlPaingType2.Attributes.Add("onclick", "javascript:ListPaing(2);");
                    this.RadlPaingType3.Attributes.Add("onclick", "javascript:ListPaing(3);");
                    this.RadlPaingType4.Attributes.Add("onclick", "javascript:ListPaing(4);");
                    if (info3.UsePaging)
                    {
                        CollectionPagingRuleInfo info4 = CollectionPagingRules.GetInfoById(infoById.ItemId, 1);
                        this.TxtPaingBegin.Text = info4.PagingBeginCode;
                        this.TxtPaingEnd.Text = info4.PagingEndCode;
                        switch (info4.PagingType)
                        {
                            case 0:
                                this.RadlPaingType.Checked = true;
                                break;

                            case 1:
                                this.RadlPaingType1.Checked = true;
                                break;

                            case 2:
                                this.RadlPaingType2.Checked = true;
                                break;

                            case 3:
                                this.RadlPaingType3.Checked = true;
                                break;

                            case 4:
                                this.RadlPaingType4.Checked = true;
                                break;

                            default:
                                this.RadlPaingType.Checked = true;
                                break;
                        }
                        this.TxtPaingAddress.Text = info4.DesignatedUrl;
                        this.TxtScopeBegin.Text = info4.ScopeBegin.ToString();
                        this.TxtScopeEnd.Text = info4.ScopeEnd.ToString();
                        this.TxtListPaing.Text = info4.PagingUrlList;
                        this.TxtPaingBegin2.Text = info4.PagingBeginCode;
                        this.TxtPaingEnd2.Text = info4.PagingEndCode;
                        this.TxtLinkBegin2.Text = info4.LinkBeginCode;
                        this.TxtLinkEnd2.Text = info4.LinkEndCode;
                        this.Page.ClientScript.RegisterStartupScript(base.GetType(), "Init", "<script type='text/javascript'>ListPaing(" + DataConverter.CLng(info4.PagingType).ToString() + ");</script>");
                    }
                }
                this.ShowExclosionList(info3.ExclosionId, none);
            }
        }

        private void SavePaing()
        {
            bool flag = true;
            int num = 0;
            if (this.RadlPaingType1.Checked)
            {
                if (string.IsNullOrEmpty(this.TxtPaingBegin.Text))
                {
                    AdminPage.WriteErrMsg("“下一页”URL开始代码不能为空！");
                }
                if (string.IsNullOrEmpty(this.TxtPaingEnd.Text))
                {
                    AdminPage.WriteErrMsg("“下一页”URL结束代码不能为空！");
                }
                num = 1;
            }
            if (this.RadlPaingType2.Checked)
            {
                if (string.IsNullOrEmpty(this.TxtPaingAddress.Text))
                {
                    AdminPage.WriteErrMsg("URL地址不能为空！");
                }
                if (DataValidator.IsUrl(this.TxtPaingAddress.Text))
                {
                    AdminPage.WriteErrMsg("URL地址不是有效的URL！");
                }
                if (DataConverter.CLng(this.TxtScopeBegin.Text) <= 0)
                {
                    AdminPage.WriteErrMsg("ID开始范围为不是数字或小于1！");
                }
                if (DataConverter.CLng(this.TxtScopeEnd.Text) <= 0)
                {
                    AdminPage.WriteErrMsg("ID结束范围为不是数字或小于1！");
                }
                num = 2;
            }
            if (this.RadlPaingType3.Checked)
            {
                if (string.IsNullOrEmpty(this.TxtListPaing.Text))
                {
                    AdminPage.WriteErrMsg("URL列表不能为空！");
                }
                num = 3;
            }
            if (this.RadlPaingType4.Checked)
            {
                if (string.IsNullOrEmpty(this.TxtPaingBegin2.Text))
                {
                    AdminPage.WriteErrMsg("分页代码开始不能为空！");
                }
                if (string.IsNullOrEmpty(this.TxtPaingEnd2.Text))
                {
                    AdminPage.WriteErrMsg("分页代码结束不能为空！");
                }
                if (string.IsNullOrEmpty(this.TxtLinkBegin2.Text))
                {
                    AdminPage.WriteErrMsg("分页URL开始代码不能为空！");
                }
                if (string.IsNullOrEmpty(this.TxtLinkEnd2.Text))
                {
                    AdminPage.WriteErrMsg("分页URL结束代码不能为空！");
                }
                num = 4;
            }
            if (!this.RadlPaingType.Checked)
            {
                CollectionPagingRuleInfo collectionPagingRuleInfo = new CollectionPagingRuleInfo();
                collectionPagingRuleInfo.ItemId = this.m_ItemId;
                collectionPagingRuleInfo.RuleType = 1;
                collectionPagingRuleInfo.PagingType = num;
                collectionPagingRuleInfo.PagingBeginCode = this.TxtPaingBegin.Text;
                collectionPagingRuleInfo.PagingEndCode = this.TxtPaingEnd.Text;
                collectionPagingRuleInfo.DesignatedUrl = this.TxtPaingAddress.Text;
                collectionPagingRuleInfo.ScopeBegin = DataConverter.CLng(this.TxtScopeBegin.Text);
                collectionPagingRuleInfo.ScopeEnd = DataConverter.CLng(this.TxtScopeEnd.Text);
                collectionPagingRuleInfo.PagingUrlList = this.TxtListPaing.Text;
                if (num == 4)
                {
                    collectionPagingRuleInfo.PagingBeginCode = this.TxtPaingBegin2.Text;
                    collectionPagingRuleInfo.PagingEndCode = this.TxtPaingEnd2.Text;
                    collectionPagingRuleInfo.LinkBeginCode = this.TxtLinkBegin2.Text;
                    collectionPagingRuleInfo.LinkEndCode = this.TxtLinkEnd2.Text;
                }
                if (CollectionPagingRules.Exists(this.m_ItemId, 1))
                {
                    flag = CollectionPagingRules.Update(collectionPagingRuleInfo);
                }
                else
                {
                    flag = CollectionPagingRules.Add(collectionPagingRuleInfo);
                }
                if (!flag)
                {
                    AdminPage.WriteErrMsg("分页设置失败！");
                }
            }
        }

        private void ShowExclosionList(int exclosionId, FieldType fieldType)
        {
            switch (fieldType)
            {
                case FieldType.TextType:
                case FieldType.ListBoxType:
                case FieldType.LookType:
                case FieldType.CountType:
                case FieldType.ColorType:
                case FieldType.TemplateType:
                case FieldType.AuthorType:
                case FieldType.SourceType:
                case FieldType.KeywordType:
                case FieldType.OperatingType:
                case FieldType.Producer:
                case FieldType.Trademark:
                case FieldType.ContentType:
                case FieldType.TitleType:
                    this.ExclosionList(exclosionId, 1);
                    return;

                case FieldType.MultipleTextType:
                case FieldType.MultipleHtmlTextType:
                case FieldType.LinkType:
                case FieldType.BoolType:
                case FieldType.PictureType:
                case FieldType.FileType:
                case FieldType.NodeType:
                case FieldType.InfoType:
                case FieldType.SkinType:
                case FieldType.DownServerType:
                case FieldType.SpecialType:
                case FieldType.StatusType:
                case FieldType.ProductType:
                    break;

                case FieldType.NumberType:
                case FieldType.MoneyType:
                    this.ExclosionList(exclosionId, 3);
                    return;

                case FieldType.DateTimeType:
                    this.ExclosionList(exclosionId, 2);
                    break;

                default:
                    return;
            }
        }
    }
}

