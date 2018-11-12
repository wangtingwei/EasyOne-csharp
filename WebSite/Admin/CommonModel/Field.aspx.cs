namespace EasyOne.WebSite.Admin.CommonModel
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.AccessManage;
    using EasyOne.Model.CommonModel;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;
    using System.Web;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class Field : AdminPage
    {
        private string m_ModelName;


        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("FieldManage.aspx?ModelID=" + BasePage.RequestString("ModelID") + "&ModelType=" + BasePage.RequestInt32("ModelType").ToString() + "&ModelName=" + this.m_ModelName);
        }

        protected void DropLookupTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList list = (DropDownList) sender;
            int modelId = DataConverter.CLng(list.SelectedValue);
            FieldType[] fieldType = new FieldType[] { FieldType.TextType, FieldType.TitleType };
            this.DropLookupField.DataSource = EasyOne.CommonModel.Field.GetFieldNames(modelId, fieldType);
            this.DropLookupField.DataTextField = "FieldAlias";
            this.DropLookupField.DataValueField = "FieldName";
            this.DropLookupField.DataBind();
        }

        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            if (!this.Page.IsValid)
            {
                return;
            }
            int modelId = BasePage.RequestInt32("ModelID");
            IList<FieldInfo> fieldList = EasyOne.CommonModel.Field.GetFieldList(modelId);
            int orderId = 0;
            foreach (FieldInfo info in fieldList)
            {
                if (info.OrderId > orderId)
                {
                    orderId = info.OrderId;
                }
            }
            orderId++;
            FieldInfo fieldInfo = new FieldInfo();
            fieldInfo.Id = DataSecurity.FilterBadChar(this.TxtFieldName.Text.Trim());
            fieldInfo.FieldName = DataSecurity.FilterBadChar(this.TxtFieldName.Text.Trim());
            fieldInfo.FieldAlias = this.TxtFieldAliax.Text.Trim();
            fieldInfo.Description = this.TxtDescription.Text.Trim();
            fieldInfo.Tips = this.TxtTips.Text;
            fieldInfo.EnableNull = DataConverter.CBoolean(this.RadlEnableNull.SelectedValue);
            fieldInfo.FieldType = (FieldType) Enum.Parse(typeof(FieldType), this.HdnFieldType.Value);
            fieldInfo.CopyToSettings(this.GetSettingsByFieldType((FieldType) Enum.Parse(typeof(FieldType), this.HdnFieldType.Value)));
            fieldInfo.OrderId = orderId;
            fieldInfo.Disabled = false;
            fieldInfo.EnableShowOnSearchForm = DataConverter.CBoolean(this.RadlEnableShowOnSearchForm.SelectedValue);
            fieldInfo.DefaultValue = this.GetDefaultValue((FieldType) Enum.Parse(typeof(FieldType), this.HdnFieldType.Value));
            DataActionState unknown = DataActionState.Unknown;
            string str = null;
            if ((fieldInfo.FieldType == FieldType.FileType) && DataConverter.CBoolean(fieldInfo.Settings[3]))
            {
                if (string.IsNullOrEmpty(fieldInfo.Settings[4]))
                {
                    AdminPage.WriteErrMsg("<li>记录文件大小字段名不能为空！</li>");
                }
                if (string.Compare(fieldInfo.Settings[4], fieldInfo.FieldName, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    AdminPage.WriteErrMsg("<li>保存文件大小的字段名不能与主字段名重复！</li>");
                }
            }
            string str2 = this.ViewState["action"].ToString();
            if (str2 != null)
            {
                if (!(str2 == "Add"))
                {
                    if (str2 == "Copy")
                    {
                        fieldInfo.FieldLevel = 1;
                        unknown = EasyOne.CommonModel.Field.Add(fieldInfo, modelId);
                        str = "复制";
                        goto Label_035A;
                    }
                }
                else
                {
                    str = "添加";
                    fieldInfo.FieldLevel = 1;
                    unknown = EasyOne.CommonModel.Field.Add(fieldInfo, modelId);
                    if (unknown == DataActionState.Successed)
                    {
                        UserPermissions.AddFieldPermission(this.EChkGroupList.SelectList(), modelId, fieldInfo.FieldName, OperateCode.ContentFieldInput, 1);
                        RolePermissions.AddFieldPermissionToRoles(this.EChkRoleList.SelectList(), modelId, fieldInfo.FieldName, OperateCode.ContentFieldInput);
                    }
                    goto Label_035A;
                }
            }
            str = "修改";
            fieldInfo.FieldLevel = DataConverter.CLng(this.HdnFieldLevel.Value);
            fieldInfo.OrderId = DataConverter.CLng(this.HdnOrderId.Value);
            fieldInfo.Disabled = DataConverter.CBoolean(this.HdnDisabled.Value);
            unknown = EasyOne.CommonModel.Field.Update(fieldInfo, modelId);
            if (unknown == DataActionState.Successed)
            {
                UserPermissions.AddFieldPermission(this.EChkGroupList.SelectList(), modelId, fieldInfo.FieldName, OperateCode.ContentFieldInput, 1);
                RolePermissions.AddFieldPermissionToRoles(this.EChkRoleList.SelectList(), modelId, fieldInfo.FieldName, OperateCode.ContentFieldInput);
            }
        Label_035A:
            switch (unknown)
            {
                case DataActionState.Successed:
                    AdminPage.WriteSuccessMsg("<li>字段" + str + "成功！</li>", string.Concat(new object[] { "FieldManage.aspx?ModelType=", BasePage.RequestInt32("ModelType").ToString(), "&ModelID=", modelId, "&ModelName=", HttpUtility.UrlEncode(this.m_ModelName) }));
                    return;

                case DataActionState.Exist:
                    AdminPage.WriteErrMsg("<li>该模型下已经存在此字段，请指定其它的字段！</li>");
                    return;

                case DataActionState.Unknown:
                    AdminPage.WriteErrMsg("<li>对不起，字段" + str + "失败！</li>");
                    return;

                default:
                    return;
            }
        }

        private string GetDefaultValue(FieldType fieldType)
        {
            string str = "";
            switch (fieldType)
            {
                case FieldType.TextType:
                    return this.TxtTextDefaultValue.Text.Trim();

                case FieldType.MultipleTextType:
                    return this.TxtMultiDefault.Text.Trim();

                case FieldType.MultipleHtmlTextType:
                    return this.TxtMulitHtmlDefault.Text.Trim();

                case FieldType.ListBoxType:
                    return this.TxtChoiceDefaultValue.Text.Trim();

                case FieldType.NumberType:
                    return this.TxtNumberDefaultValue.Text.Trim();

                case FieldType.MoneyType:
                    return this.TxtCurrencyDefaultValue.Text;

                case FieldType.DateTimeType:
                    if (this.RadlDateTimeDefaultType.SelectedValue == "1")
                    {
                        str = "Now";
                    }
                    if (this.RadlDateTimeDefaultType.SelectedValue == "2")
                    {
                        str = this.DpkDateTimeInputDefaultValue.Date.ToString();
                    }
                    return str;

                case FieldType.LookType:
                case FieldType.CountType:
                case FieldType.NodeType:
                case FieldType.TemplateType:
                case FieldType.InfoType:
                case FieldType.SkinType:
                case FieldType.SpecialType:
                case FieldType.StatusType:
                case FieldType.ProductType:
                case FieldType.ContentType:
                    return str;

                case FieldType.LinkType:
                    return this.TxtURLDefaultValue.Text.Trim();

                case FieldType.BoolType:
                    return this.DropBoolean.SelectedValue;

                case FieldType.PictureType:
                    return this.TxtPicDefaultUrl.Text.Trim();

                case FieldType.FileType:
                    return this.TxtFileDefaultValue.Text.Trim();

                case FieldType.ColorType:
                    return this.CpkColorDefault.Text;

                case FieldType.AuthorType:
                    return this.TxtAuthorDefaultValue.Text.Trim();

                case FieldType.SourceType:
                    return this.TxtSourceDefaultValue.Text.Trim();

                case FieldType.KeywordType:
                    return this.TxtKeywordDefaultValue.Text.Trim();

                case FieldType.OperatingType:
                    return this.TxtOperatingSystemDefaultValue.Text.Trim();

                case FieldType.DownServerType:
                    return this.TxtDownServerDefaultValue.Text.Trim();

                case FieldType.Producer:
                    return this.TxtProducerDefaultValue.Text.Trim();

                case FieldType.Trademark:
                    return this.TxtTrademarkDefaultValue.Text.Trim();

                case FieldType.TitleType:
                    return this.TxtTitleDefaultValue.Text.Trim();

                case FieldType.MultiplePhotoType:
                    return this.TxtMultiPhotoDefaultValue.Text.Trim();
            }
            return str;
        }

        private int GetEnumIntValue(FieldType fieldType)
        {
            return (((int) fieldType) - 1);
        }

        private string GetOperatingSystemSelectItem(string operatingSystemSelectItem)
        {
            string str = operatingSystemSelectItem;
            StringBuilder builder = new StringBuilder();
            string[] separator = new string[] { "\r\n" };
            foreach (string str2 in str.Split(separator, StringSplitOptions.RemoveEmptyEntries))
            {
                if (!string.IsNullOrEmpty(str2))
                {
                    builder.Append(str2);
                    builder.Append("$$$");
                }
            }
            string str3 = "$$$";
            return builder.ToString().TrimEnd(str3.ToCharArray());
        }

        private string GetProperties(string properties)
        {
            string[] strArray = DataSecurity.HtmlEncode(properties).Split(new string[] { "<br>" }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder builder = new StringBuilder();
            foreach (string str2 in strArray)
            {
                builder.Append(str2.Trim());
                builder.Append("|");
            }
            return builder.ToString().TrimEnd(new char[] { '|' });
        }

        private Collection<string> GetSettingsByFieldType(FieldType fieldType)
        {
            Collection<string> collection = new Collection<string>();
            switch (fieldType)
            {
                case FieldType.TextType:
                    collection.Add(this.TxtTextMaxLength.Text.Trim());
                    collection.Add(this.TxtTextSize.Text.Trim());
                    collection.Add("False");
                    collection.Add(this.DropTextIMEMode.SelectedValue);
                    collection.Add(this.AttachSingle.EnableInsideLink);
                    collection.Add(this.AttachSingle.EnableFilterWord);
                    collection.Add(this.AttachSingle.EnableShieldWord);
                    return collection;

                case FieldType.MultipleTextType:
                    collection.Add(this.TxtMultiTextWidth.Text.Trim());
                    collection.Add(this.TxtMultiTextRow.Text.Trim());
                    collection.Add(this.AttachMulit.EnableInsideLink);
                    collection.Add(this.AttachMulit.EnableFilterWord);
                    collection.Add(this.AttachMulit.EnableShieldWord);
                    return collection;

                case FieldType.MultipleHtmlTextType:
                    collection.Add(this.DropEditorType.SelectedValue);
                    collection.Add(this.TxtEditorWidth.Text.Trim());
                    collection.Add(this.TxtEditorHight.Text.Trim());
                    collection.Add(this.AttachMulitHtml.EnableInsideLink);
                    collection.Add(this.AttachMulitHtml.EnableFilterWord);
                    collection.Add(this.AttachMulitHtml.EnableShieldWord);
                    return collection;

                case FieldType.ListBoxType:
                    collection.Add(this.HdnChoiceUrls.Value);
                    collection.Add(this.RadlChoiceType.SelectedValue);
                    collection.Add(this.RadlEnableFill.SelectedValue);
                    collection.Add(DataConverter.CLng(this.TxtRepeatColumns.Text, 1).ToString());
                    return collection;

                case FieldType.NumberType:
                    collection.Add(this.TxtNumberMinLength.Text.Trim());
                    collection.Add(this.TxtNumberMaxlength.Text.Trim());
                    collection.Add(this.DropNumberDecimals.SelectedValue);
                    collection.Add(this.ChkNumberPercent.Checked.ToString());
                    return collection;

                case FieldType.MoneyType:
                    collection.Add(this.TxtCurrencyMinLength.Text.Trim());
                    collection.Add(this.TxtCurrencyMaxLength.Text.Trim());
                    return collection;

                case FieldType.DateTimeType:
                    collection.Add(this.RadioDateTimeType.SelectedValue);
                    collection.Add(this.RadlDateTimeDefaultType.SelectedValue);
                    return collection;

                case FieldType.LookType:
                    collection.Add(this.DropLookupTable.SelectedValue);
                    collection.Add(this.DropLookupField.SelectedValue);
                    return collection;

                case FieldType.LinkType:
                    collection.Add(this.TxtURLMaxLength.Text.Trim());
                    collection.Add(this.TxtURLSize.Text.Trim());
                    return collection;

                case FieldType.BoolType:
                    collection.Add(this.DropBoolean.SelectedValue);
                    return collection;

                case FieldType.CountType:
                case FieldType.ColorType:
                case FieldType.NodeType:
                case FieldType.TemplateType:
                case FieldType.SkinType:
                case FieldType.StatusType:
                case FieldType.ProductType:
                    return collection;

                case FieldType.PictureType:
                    collection.Add(this.TxtImageTextLength.Text.Trim());
                    collection.Add(this.TxtImageSize.Text.Trim());
                    collection.Add(this.TextImageType.Text.Trim());
                    collection.Add(this.RadlIsFromSelected.SelectedValue);
                    collection.Add(this.RadlWsImage.SelectedValue);
                    collection.Add(this.RadlThumb.SelectedValue);
                    collection.Add(this.RadlIsUpload.SelectedValue);
                    collection.Add(this.RadlUploadFiles.SelectedValue);
                    return collection;

                case FieldType.FileType:
                    collection.Add(this.TxtFileSize.Text.Trim());
                    collection.Add(this.TextFileType.Text.Trim());
                    collection.Add(this.ChkDownLoadUrl.Checked.ToString());
                    collection.Add(this.ChkSoftSize.Checked.ToString());
                    collection.Add(this.TxtFileSizeField.Text);
                    return collection;

                case FieldType.InfoType:
                    collection.Add(this.TxtVirtualLinkWidth.Text.Trim());
                    collection.Add(this.TxtVirtualLinkRow.Text.Trim());
                    return collection;

                case FieldType.AuthorType:
                    collection.Add(this.TxtAuthorSize.Text);
                    collection.Add(this.RadAuthorIsPSession.SelectedValue);
                    return collection;

                case FieldType.SourceType:
                    collection.Add(this.TxtSourceSize.Text);
                    collection.Add(this.RadSourceIsPSession.SelectedValue);
                    return collection;

                case FieldType.KeywordType:
                    collection.Add(this.TxtKeywordSize.Text);
                    return collection;

                case FieldType.OperatingType:
                    collection.Add(this.GetOperatingSystemSelectItem(this.TxtOperatingSystemSelectItem.Text));
                    collection.Add(this.TxtOperatingSystemSize.Text);
                    return collection;

                case FieldType.DownServerType:
                    collection.Add(this.TxtDownServerWidth.Text);
                    return collection;

                case FieldType.SpecialType:
                    collection.Add(this.TxtSpecialWidth.Text.Trim());
                    collection.Add(this.TxtSpecialRow.Text.Trim());
                    return collection;

                case FieldType.Producer:
                    collection.Add(this.TxtProducerSize.Text);
                    return collection;

                case FieldType.Trademark:
                    collection.Add(this.TxtTrademarkSize.Text);
                    return collection;

                case FieldType.ContentType:
                    collection.Add(this.TxtContentEditorWidth.Text);
                    collection.Add(this.TxtContentEditorHight.Text);
                    collection.Add(this.TextUploadImpType.Text);
                    collection.Add(this.TextUploadFlashType.Text);
                    collection.Add(this.TextUploadAnnexType.Text);
                    collection.Add(this.RadlContentWsImage.SelectedValue);
                    collection.Add(this.RadlContentThumb.SelectedValue);
                    collection.Add(this.TxtUploadSize.Text);
                    return collection;

                case FieldType.TitleType:
                    collection.Add(this.TxtTitleMaxLength.Text);
                    collection.Add(this.TxtTitleSize.Text);
                    collection.Add(this.RadCheckTitleValue.SelectedValue);
                    collection.Add(this.RadCreatePinyinTitle.SelectedValue);
                    return collection;

                case FieldType.MultiplePhotoType:
                    collection.Add(this.TxtMultiPhotoSize.Text);
                    collection.Add(this.TxtMultiPhotoExt.Text);
                    collection.Add(this.RadlMWsImage.SelectedValue);
                    return collection;

                case FieldType.Property:
                    collection.Add(this.GetProperties(this.TxtProperty.Text));
                    return collection;
            }
            return collection;
        }

        private void InitFieldSettingControls(FieldInfo fieldInfo)
        {
            Collection<string> settings = fieldInfo.Settings;
            switch (fieldInfo.FieldType)
            {
                case FieldType.TextType:
                    this.InitTxtText(fieldInfo);
                    return;

                case FieldType.MultipleTextType:
                    this.InitTxtMultiText(fieldInfo);
                    return;

                case FieldType.MultipleHtmlTextType:
                    this.DropEditorType.SelectedValue = settings[0];
                    this.TxtEditorWidth.Text = settings[1];
                    this.TxtEditorHight.Text = settings[2];
                    this.TxtMulitHtmlDefault.Text = fieldInfo.DefaultValue;
                    this.AttachMulitHtml.EnableInsideLink = settings[3];
                    this.AttachMulitHtml.EnableFilterWord = settings[4];
                    this.AttachMulitHtml.EnableShieldWord = settings[5];
                    return;

                case FieldType.ListBoxType:
                    this.HdnChoiceUrls.Value = settings[0];
                    foreach (string str in this.HdnChoiceUrls.Value.Split(new string[] { "$$$" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        this.ChoiceUrl.Items.Add(new ListItem(str, str));
                    }
                    this.RadlChoiceType.SelectedValue = settings[1];
                    this.RadlEnableFill.SelectedValue = settings[2];
                    this.TxtChoiceDefaultValue.Text = fieldInfo.DefaultValue;
                    if (settings.Count > 3)
                    {
                        this.TxtRepeatColumns.Text = settings[3];
                    }
                    this.EBtnSubmit.Attributes.Add("onclick", "if(!ChangeHiddenFieldValue()){return false;}");
                    return;

                case FieldType.NumberType:
                    this.TxtNumberMinLength.Text = settings[0];
                    this.TxtNumberMaxlength.Text = settings[1];
                    this.DropNumberDecimals.SelectedValue = settings[2];
                    this.ChkNumberPercent.Checked = DataConverter.CBoolean(settings[3]);
                    this.TxtNumberDefaultValue.Text = fieldInfo.DefaultValue;
                    return;

                case FieldType.MoneyType:
                    this.TxtCurrencyMinLength.Text = settings[0];
                    this.TxtCurrencyMaxLength.Text = settings[1];
                    this.TxtCurrencyDefaultValue.Text = fieldInfo.DefaultValue;
                    return;

                case FieldType.DateTimeType:
                    BasePage.SetSelectedIndexByValue(this.RadioDateTimeType, settings[0]);
                    this.RadlDateTimeDefaultType.SelectedValue = settings[1];
                    if (settings[1] != "2")
                    {
                        break;
                    }
                    this.DpkDateTimeInputDefaultValue.Text = fieldInfo.DefaultValue;
                    return;

                case FieldType.LookType:
                    this.DropLookupTable.SelectedValue = settings[0];
                    this.DropLookupField.SelectedValue = settings[1];
                    this.DropLookupTable.Enabled = false;
                    this.DropLookupField.Enabled = false;
                    return;

                case FieldType.LinkType:
                    this.TxtURLMaxLength.Text = settings[0];
                    this.TxtURLSize.Text = settings[1];
                    this.TxtURLDefaultValue.Text = fieldInfo.DefaultValue;
                    return;

                case FieldType.BoolType:
                    this.DropBoolean.SelectedValue = fieldInfo.DefaultValue;
                    return;

                case FieldType.CountType:
                case FieldType.NodeType:
                case FieldType.TemplateType:
                case FieldType.SkinType:
                case FieldType.StatusType:
                case FieldType.ProductType:
                    break;

                case FieldType.PictureType:
                    this.TxtPicDefaultUrl.Text = fieldInfo.DefaultValue;
                    this.TxtImageTextLength.Text = settings[0];
                    this.TxtImageSize.Text = settings[1];
                    this.TextImageType.Text = settings[2];
                    this.RadlIsFromSelected.SelectedValue = settings[3];
                    this.RadlWsImage.SelectedValue = settings[4];
                    this.RadlThumb.SelectedValue = settings[5];
                    if (settings.Count > 6)
                    {
                        this.RadlIsUpload.SelectedValue = settings[6];
                    }
                    if (fieldInfo.FieldLevel != 0)
                    {
                        break;
                    }
                    this.UploadFiles.Style.Add("display", "");
                    if (settings.Count <= 7)
                    {
                        break;
                    }
                    this.RadlUploadFiles.SelectedValue = settings[7];
                    return;

                case FieldType.FileType:
                    this.TxtFileSize.Text = settings[0];
                    this.TextFileType.Text = settings[1];
                    this.ChkDownLoadUrl.Checked = DataConverter.CBoolean(settings[2]);
                    this.ChkSoftSize.Checked = DataConverter.CBoolean(settings[3]);
                    this.TxtFileSizeField.Text = DataConverter.CBoolean(settings[3]) ? settings[4] : "";
                    this.TxtFileSizeField.Enabled = !DataConverter.CBoolean(settings[3]);
                    this.TxtFileDefaultValue.Text = fieldInfo.DefaultValue;
                    return;

                case FieldType.ColorType:
                    this.CpkColorDefault.Text = fieldInfo.DefaultValue;
                    return;

                case FieldType.InfoType:
                    this.InitTxtVirtualLink(fieldInfo);
                    return;

                case FieldType.AuthorType:
                    this.TxtAuthorSize.Text = settings[0];
                    this.TxtAuthorDefaultValue.Text = fieldInfo.DefaultValue;
                    if (settings.Count <= 1)
                    {
                        this.RadAuthorIsPSession.SelectedValue = "false";
                        return;
                    }
                    this.RadAuthorIsPSession.SelectedValue = settings[1];
                    return;

                case FieldType.SourceType:
                    this.TxtSourceSize.Text = settings[0];
                    this.TxtSourceDefaultValue.Text = fieldInfo.DefaultValue;
                    if (settings.Count <= 1)
                    {
                        this.RadSourceIsPSession.SelectedValue = "false";
                        return;
                    }
                    this.RadSourceIsPSession.SelectedValue = settings[1];
                    return;

                case FieldType.KeywordType:
                    this.TxtKeywordSize.Text = settings[0];
                    this.TxtKeywordDefaultValue.Text = fieldInfo.DefaultValue;
                    return;

                case FieldType.OperatingType:
                    this.TxtOperatingSystemSelectItem.Text = settings[0].Replace("$$$", "\n");
                    this.TxtOperatingSystemSize.Text = settings[1];
                    this.TxtOperatingSystemDefaultValue.Text = fieldInfo.DefaultValue;
                    return;

                case FieldType.DownServerType:
                    this.TxtDownServerWidth.Text = settings[0];
                    this.TxtDownServerDefaultValue.Text = fieldInfo.DefaultValue;
                    return;

                case FieldType.SpecialType:
                    this.InitTxtSpecial(fieldInfo);
                    return;

                case FieldType.Producer:
                    this.TxtProducerSize.Text = settings[0];
                    this.TxtProducerDefaultValue.Text = fieldInfo.DefaultValue;
                    return;

                case FieldType.Trademark:
                    this.TxtTrademarkSize.Text = settings[0];
                    this.TxtTrademarkDefaultValue.Text = fieldInfo.DefaultValue;
                    return;

                case FieldType.ContentType:
                    this.TxtContentEditorWidth.Text = settings[0];
                    this.TxtContentEditorHight.Text = settings[1];
                    if (settings.Count <= 9)
                    {
                        this.TextUploadImpType.Text = settings[2];
                        this.TextUploadFlashType.Text = settings[3];
                        this.TextUploadAnnexType.Text = settings[4];
                        this.RadlContentWsImage.SelectedValue = settings[5];
                        if (settings.Count > 6)
                        {
                            this.RadlContentThumb.SelectedValue = settings[6];
                        }
                        if (settings.Count <= 7)
                        {
                            break;
                        }
                        this.TxtUploadSize.Text = settings[7];
                        return;
                    }
                    this.TextUploadImpType.Text = settings[6];
                    this.TextUploadFlashType.Text = settings[7];
                    this.TextUploadAnnexType.Text = settings[8];
                    this.RadlContentWsImage.SelectedValue = settings[9];
                    this.RadlContentThumb.SelectedValue = settings[10];
                    return;

                case FieldType.TitleType:
                    this.TxtTitleMaxLength.Text = settings[0];
                    this.TxtTitleSize.Text = settings[1];
                    this.RadCheckTitleValue.SelectedValue = settings[2];
                    if (settings.Count > 3)
                    {
                        this.RadCreatePinyinTitle.SelectedValue = settings[3];
                    }
                    this.TxtTitleDefaultValue.Text = fieldInfo.DefaultValue;
                    return;

                case FieldType.MultiplePhotoType:
                    this.TxtMultiPhotoSize.Text = settings[0];
                    this.TxtMultiPhotoExt.Text = settings[1];
                    if (settings.Count > 2)
                    {
                        this.RadlMWsImage.SelectedValue = settings[2];
                    }
                    this.TxtMulitHtmlDefault.Text = fieldInfo.DefaultValue;
                    return;

                case FieldType.Property:
                {
                    string[] strArray2 = settings[0].Split(new char[] { '|' });
                    StringBuilder builder = new StringBuilder();
                    foreach (string str2 in strArray2)
                    {
                        builder.Append(str2);
                        builder.Append('\n');
                    }
                    this.TxtProperty.Text = builder.ToString().TrimEnd(new char[] { '\n' });
                    break;
                }
                default:
                    return;
            }
        }

        private void InitRoleCheckBoxList()
        {
            this.EChkGroupList.Items.Clear();
            this.EChkGroupList.DataSource = UserGroups.GetUserGroupList(0, 0);
            this.EChkGroupList.DataTextField = "GroupName";
            this.EChkGroupList.DataValueField = "GroupId";
            this.EChkGroupList.DataBind();
            this.EChkRoleList.Items.Clear();
            this.EChkRoleList.DataSource = UserRole.GetRoleList();
            this.EChkRoleList.DataTextField = "RoleName";
            this.EChkRoleList.DataValueField = "RoleId";
            this.EChkRoleList.DataBind();
        }

        private void InitTxtMultiText(FieldInfo fieldInfo)
        {
            Collection<string> settings = fieldInfo.Settings;
            this.TxtMultiTextWidth.Text = settings[0];
            this.TxtMultiTextRow.Text = settings[1];
            this.TxtMultiDefault.Text = fieldInfo.DefaultValue;
            this.AttachMulit.EnableInsideLink = settings[2];
            this.AttachMulit.EnableFilterWord = settings[3];
            this.AttachMulit.EnableShieldWord = settings[4];
            this.RadTextType.Enabled = false;
            this.RadMultipleTextType.Enabled = true;
            this.RadMultipleHtmlTextType.Enabled = true;
            this.RadListBoxType.Enabled = false;
            this.RadNumberType.Enabled = false;
            this.RadMoneyType.Enabled = false;
            this.RadDateTimeType.Enabled = false;
            this.RadLookType.Enabled = false;
            this.RadLinkType.Enabled = false;
            this.RadBoolType.Enabled = false;
            this.RadPictureType.Enabled = false;
            this.RadFileType.Enabled = false;
            this.RadColorType.Enabled = false;
            this.RadAuthorType.Enabled = false;
            this.RadSourceType.Enabled = false;
            this.RadKeywordType.Enabled = false;
            this.RadOperatingType.Enabled = false;
            this.RadNodeType.Enabled = false;
            this.RadInfoType.Enabled = false;
            this.RadSkinType.Enabled = false;
            this.RadTemplateType.Enabled = false;
            this.RadDownServerType.Enabled = false;
            this.RadSpecialType.Enabled = false;
            this.RadProducer.Enabled = false;
            this.RadTrademark.Enabled = false;
            this.RadContentType.Enabled = false;
            this.RadTitleType.Enabled = false;
        }

        private void InitTxtSpecial(FieldInfo fieldInfo)
        {
            Collection<string> settings = fieldInfo.Settings;
            this.TxtSpecialWidth.Text = settings[0];
            this.TxtSpecialRow.Text = settings[1];
        }

        private void InitTxtText(FieldInfo fieldInfo)
        {
            Collection<string> settings = fieldInfo.Settings;
            this.TxtTextMaxLength.Text = settings[0];
            this.TxtTextSize.Text = settings[1];
            this.DropTextIMEMode.SelectedValue = settings[3];
            this.TxtTextDefaultValue.Text = fieldInfo.DefaultValue;
            this.AttachSingle.EnableInsideLink = settings[4];
            this.AttachSingle.EnableFilterWord = settings[5];
            this.AttachSingle.EnableShieldWord = settings[6];
            this.RadTextType.Enabled = true;
            this.RadMultipleTextType.Enabled = true;
            this.RadMultipleHtmlTextType.Enabled = true;
            this.RadListBoxType.Enabled = false;
            this.RadNumberType.Enabled = false;
            this.RadMoneyType.Enabled = false;
            this.RadDateTimeType.Enabled = false;
            this.RadLookType.Enabled = false;
            this.RadLinkType.Enabled = false;
            this.RadBoolType.Enabled = false;
            this.RadPictureType.Enabled = false;
            this.RadFileType.Enabled = false;
            this.RadColorType.Enabled = false;
            this.RadAuthorType.Enabled = false;
            this.RadSourceType.Enabled = false;
            this.RadKeywordType.Enabled = false;
            this.RadOperatingType.Enabled = false;
            this.RadNodeType.Enabled = false;
            this.RadInfoType.Enabled = false;
            this.RadSkinType.Enabled = false;
            this.RadTemplateType.Enabled = false;
            this.RadDownServerType.Enabled = false;
            this.RadSpecialType.Enabled = false;
            this.RadStatusType.Enabled = false;
            this.RadProducer.Enabled = false;
            this.RadTrademark.Enabled = false;
            this.RadContentType.Enabled = false;
            this.RadTitleType.Enabled = false;
        }

        private void InitTxtVirtualLink(FieldInfo fieldInfo)
        {
            Collection<string> settings = fieldInfo.Settings;
            this.TxtVirtualLinkWidth.Text = settings[0];
            this.TxtVirtualLinkRow.Text = settings[1];
        }

        private void LookupDataBind()
        {
            IList<ModelInfo> modelList = ModelManager.GetModelList(ModelType.None, ModelShowType.Enable);
            this.DropLookupTable.DataSource = modelList;
            this.DropLookupTable.DataTextField = "ModelName";
            this.DropLookupTable.DataValueField = "ModelId";
            this.DropLookupTable.DataBind();
            int modelId = modelList[0].ModelId;
            FieldType[] fieldType = new FieldType[] { FieldType.TextType, FieldType.TitleType };
            this.DropLookupField.DataSource = EasyOne.CommonModel.Field.GetFieldNames(modelId, fieldType);
            this.DropLookupField.DataTextField = "FieldAlias";
            this.DropLookupField.DataValueField = "FieldName";
            this.DropLookupField.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = BasePage.RequestString("FieldName");
            int modelId = BasePage.RequestInt32("ModelID");
            this.m_ModelName = base.Server.UrlDecode(BasePage.RequestString("ModelName"));
            string str2 = BasePage.RequestString("Action", "Add");
            this.ViewState["action"] = str2;
            this.TxtFieldName.Attributes.Add("onpropertychange", "SetLitFieldName(this.value)");
            if (!base.IsPostBack)
            {
                this.SmpNavigator.CurrentNode = "<a href='FieldManage.aspx?ModelType=" + BasePage.RequestInt32("ModelType").ToString() + "&ModelId=" + modelId.ToString() + "&ModelName=" + base.Server.HtmlEncode(this.m_ModelName) + "'>字段管理</a>";
                this.LookupDataBind();
                if (BasePage.RequestInt32("ModelType") == 1)
                {
                    this.InitRoleCheckBoxList();
                }
                this.EBtnSubmit.Text = "保存字段";
                switch (str2)
                {
                    case "Modify":
                    case "Copy":
                        if (!string.IsNullOrEmpty(str))
                        {
                            FieldInfo fieldInfoByFieldName = EasyOne.CommonModel.Field.GetFieldInfoByFieldName(modelId, str);
                            this.ShowPanel(fieldInfoByFieldName.FieldType);
                            if (str2 == "Modify")
                            {
                                this.SmpNavigator.AdditionalNode = "修改字段";
                                this.LblTitle.Text = "修改字段：" + fieldInfoByFieldName.FieldAlias;
                                this.TxtFieldName.Enabled = false;
                                if (BasePage.RequestInt32("ModelType") == 1)
                                {
                                    IList<RoleFieldPermissionsInfo> fieldPermissionById = UserPermissions.GetFieldPermissionById(modelId, str);
                                    StringBuilder sb = new StringBuilder();
                                    foreach (RoleFieldPermissionsInfo info2 in fieldPermissionById)
                                    {
                                        StringHelper.AppendString(sb, info2.RoleId.ToString());
                                    }
                                    IList<RoleFieldPermissionsInfo> fieldPermissionByModelId = RolePermissions.GetFieldPermissionByModelId(modelId, str);
                                    StringBuilder builder2 = new StringBuilder();
                                    foreach (RoleFieldPermissionsInfo info3 in fieldPermissionByModelId)
                                    {
                                        StringHelper.AppendString(builder2, info3.RoleId.ToString());
                                    }
                                    this.EChkGroupList.SetSelectValue(sb.ToString());
                                    this.EChkRoleList.SetSelectValue(builder2.ToString());
                                }
                            }
                            else
                            {
                                fieldInfoByFieldName.FieldName = "";
                                fieldInfoByFieldName.FieldAlias = "";
                                fieldInfoByFieldName.Description = "";
                                fieldInfoByFieldName.Tips = "";
                                this.EBtnSubmit.Text = " 复制 ";
                                this.LblTitle.Text = "复制字段：" + str;
                                this.SmpNavigator.AdditionalNode = "复制字段";
                            }
                            this.TxtFieldName.Text = fieldInfoByFieldName.FieldName;
                            this.LblFieldName.Text = fieldInfoByFieldName.FieldName;
                            this.TxtFieldAliax.Text = fieldInfoByFieldName.FieldAlias;
                            this.TxtDescription.Text = fieldInfoByFieldName.Description;
                            this.TxtTips.Text = fieldInfoByFieldName.Tips;
                            this.RadlEnableNull.SelectedValue = fieldInfoByFieldName.EnableNull.ToString();
                            this.RadlEnableShowOnSearchForm.SelectedValue = fieldInfoByFieldName.EnableShowOnSearchForm.ToString();
                            string id = "Rad" + Enum.GetName(typeof(FieldType), fieldInfoByFieldName.FieldType);
                            ((RadioButton) this.TdFieldType.FindControl(id)).Checked = true;
                            if ((fieldInfoByFieldName.FieldType != FieldType.TextType) && (fieldInfoByFieldName.FieldType != FieldType.MultipleTextType))
                            {
                                this.RadTextType.Enabled = false;
                                this.RadMultipleTextType.Enabled = false;
                                this.RadMultipleHtmlTextType.Enabled = false;
                                this.RadListBoxType.Enabled = false;
                                this.RadNumberType.Enabled = false;
                                this.RadMoneyType.Enabled = false;
                                this.RadDateTimeType.Enabled = false;
                                this.RadLookType.Enabled = false;
                                this.RadLinkType.Enabled = false;
                                this.RadBoolType.Enabled = false;
                                this.RadPictureType.Enabled = false;
                                this.RadFileType.Enabled = false;
                                this.RadColorType.Enabled = false;
                                this.RadAuthorType.Enabled = false;
                                this.RadSourceType.Enabled = false;
                                this.RadKeywordType.Enabled = false;
                                this.RadOperatingType.Enabled = false;
                                this.RadNodeType.Enabled = false;
                                this.RadInfoType.Enabled = false;
                                this.RadSkinType.Enabled = false;
                                this.RadTemplateType.Enabled = false;
                                this.RadDownServerType.Enabled = false;
                                this.RadSpecialType.Enabled = false;
                                this.RadStatusType.Enabled = false;
                                this.RadProducer.Enabled = false;
                                this.RadTrademark.Enabled = false;
                                this.RadContentType.Enabled = false;
                                this.RadTitleType.Enabled = false;
                                this.RadMultiplePhotoType.Enabled = false;
                                this.RadProperty.Enabled = false;
                            }
                            this.InitFieldSettingControls(fieldInfoByFieldName);
                            this.HdnFieldLevel.Value = fieldInfoByFieldName.FieldLevel.ToString();
                            this.HdnFieldType.Value = ((int) fieldInfoByFieldName.FieldType).ToString();
                            this.HdnOrderId.Value = fieldInfoByFieldName.OrderId.ToString();
                            this.HdnDisabled.Value = fieldInfoByFieldName.Disabled.ToString();
                        }
                        break;

                    default:
                        str = "添加字段";
                        this.SmpNavigator.AdditionalNode = "添加字段";
                        this.ShowPanel(FieldType.TextType);
                        this.HdnFieldType.Value = 1.ToString();
                        this.RadNodeType.Visible = false;
                        this.RadInfoType.Visible = false;
                        this.RadSkinType.Visible = false;
                        this.RadTemplateType.Visible = false;
                        this.RadSpecialType.Visible = false;
                        this.RadStatusType.Visible = false;
                        this.RadTitleType.Visible = false;
                        break;
                }
                this.LblModelName.Text = "当前模型：<a href='FieldManage.aspx?ModelType=" + BasePage.RequestInt32("ModelType").ToString() + "&ModelId=" + modelId.ToString() + "&ModelName=" + base.Server.UrlEncode(this.m_ModelName) + "'>" + this.m_ModelName + "</a>";
                this.ShowEshopFields();
            }
            if (!PEContext.Current.Admin.IsSuperAdmin)
            {
                this.PnlRoleList.Visible = false;
            }
        }

        protected void RadlFieldType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string iD = ((RadioButton) sender).ID;
            iD = iD.Substring(3, iD.Length - 3);
            FieldType showType = (FieldType) Enum.Parse(typeof(FieldType), iD);
            this.HdnFieldType.Value = showType.ToString();
            this.ShowPanel(showType);
        }

        private void ShowEshopFields()
        {
            if (BasePage.RequestInt32("ModelType") == 2)
            {
                this.RadAuthorType.Visible = false;
                this.RadSkinType.Visible = false;
                this.RadStatusType.Visible = false;
                this.RadSourceType.Visible = false;
                this.RadTemplateType.Visible = false;
                this.RadNodeType.Visible = false;
                this.RadSpecialType.Visible = false;
                this.RadInfoType.Visible = false;
                this.PnlRoleList.Visible = false;
                this.RadTitleType.Visible = false;
                this.RadDownServerType.Visible = false;
            }
            else
            {
                this.RadProperty.Visible = false;
            }
        }

        private void ShowPanel(FieldType showType)
        {
            int length = Enum.GetValues(typeof(FieldType)).Length;
            bool[] flagArray = new bool[length];
            for (int i = 0; i < length; i++)
            {
                if (i == (int)(showType - 1))
                {
                    flagArray[i] = true;
                }
                else
                {
                    flagArray[i] = false;
                }
            }
            if (showType == FieldType.ListBoxType)
            {
                this.EBtnSubmit.Attributes.Add("onclick", "if(!ChangeHiddenFieldValue()){return false;}");
            }
            this.PnlText.Visible = flagArray[this.GetEnumIntValue(FieldType.TextType)];
            this.PnlMultiText.Visible = flagArray[this.GetEnumIntValue(FieldType.MultipleTextType)];
            this.PnlEditor.Visible = flagArray[this.GetEnumIntValue(FieldType.MultipleHtmlTextType)];
            this.PnlChoice.Visible = flagArray[this.GetEnumIntValue(FieldType.ListBoxType)];
            this.PnlNumber.Visible = flagArray[this.GetEnumIntValue(FieldType.NumberType)];
            this.PnlCurrency.Visible = flagArray[this.GetEnumIntValue(FieldType.MoneyType)];
            this.PnlDateTime.Visible = flagArray[this.GetEnumIntValue(FieldType.DateTimeType)];
            this.PnlLookup.Visible = flagArray[this.GetEnumIntValue(FieldType.LookType)];
            this.PnlURL.Visible = flagArray[this.GetEnumIntValue(FieldType.LinkType)];
            this.PnlBoolean.Visible = flagArray[this.GetEnumIntValue(FieldType.BoolType)];
            this.PnlImage.Visible = flagArray[this.GetEnumIntValue(FieldType.PictureType)];
            this.PnlFile.Visible = flagArray[this.GetEnumIntValue(FieldType.FileType)];
            this.PnlColor.Visible = flagArray[this.GetEnumIntValue(FieldType.ColorType)];
            this.PnlAuthor.Visible = flagArray[this.GetEnumIntValue(FieldType.AuthorType)];
            this.PnlSource.Visible = flagArray[this.GetEnumIntValue(FieldType.SourceType)];
            this.PnlKeyword.Visible = flagArray[this.GetEnumIntValue(FieldType.KeywordType)];
            this.PnlOperatingSystem.Visible = flagArray[this.GetEnumIntValue(FieldType.OperatingType)];
            this.PnlVirtualLink.Visible = flagArray[this.GetEnumIntValue(FieldType.InfoType)];
            this.PnlDownServer.Visible = flagArray[this.GetEnumIntValue(FieldType.DownServerType)];
            this.PnlSpecial.Visible = flagArray[this.GetEnumIntValue(FieldType.SpecialType)];
            this.PnlProducer.Visible = flagArray[this.GetEnumIntValue(FieldType.Producer)];
            this.PnlTrademark.Visible = flagArray[this.GetEnumIntValue(FieldType.Trademark)];
            this.PnlContent.Visible = flagArray[this.GetEnumIntValue(FieldType.ContentType)];
            this.PnlTitle.Visible = flagArray[this.GetEnumIntValue(FieldType.TitleType)];
            this.PnlMultiplePhoto.Visible = flagArray[this.GetEnumIntValue(FieldType.MultiplePhotoType)];
            this.PnlProperty.Visible = flagArray[this.GetEnumIntValue(FieldType.Property)];
        }
    }
}

