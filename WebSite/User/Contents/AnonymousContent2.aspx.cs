namespace EasyOne.WebSite.User.Contents
{
    using EasyOne.AccessManage;
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Contents;
    using EasyOne.Enumerations;
    using EasyOne.Model.Accessories;
    using EasyOne.Model.CommonModel;
    using EasyOne.Model.Contents;
    using EasyOne.Model.UserManage;
    using EasyOne.ModelControls;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls.FieldControl;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class AnonymousContent2 : DynamicPage
    {
        private bool m_FieldInput;
        private int m_ModelId;
        private int m_NodeId;
        private void Add()
        {
            DataTable newContentData = ContentManage.GetNewContentData(this.GetDataTableFromRepeater());
            UserGroupsInfo userGroupById = UserGroups.GetUserGroupById(-2);
            if (userGroupById.IsNull || string.IsNullOrEmpty(userGroupById.GroupSetting))
            {
                DynamicPage.WriteErrMsg("匿名会员组不存在！");
            }
            else
            {
                UserPurviewInfo groupSetting = UserGroups.GetGroupSetting(userGroupById.GroupSetting);
                if (groupSetting.IsNull)
                {
                    DynamicPage.WriteErrMsg("匿名会员组没有进行权限设置！");
                }
                if (groupSetting.PublicInfoNoNeedCheck)
                {
                    DataRow[] rowArray = newContentData.Select("FieldName = 'status'");
                    if (rowArray[0]["FieldValue"].ToString() == "0")
                    {
                        rowArray[0]["FieldValue"] = "99";
                    }
                }
            }
            if (ContentManage.Add(this.m_ModelId, newContentData))
            {
                this.AddKeywordsToTable(newContentData);
                DynamicPage.WriteSuccessMsg("添加成功！", "../../Default.aspx");
            }
            else
            {
                DynamicPage.WriteErrMsg("添加失败！");
            }
        }

        private void AddKeywordsToTable(DataTable dataTable)
        {
            int generalId = GetGeneralId(dataTable);
            DataRow[] rowArray = dataTable.Select("FieldName = 'keyword'");
            if (rowArray.Length > 0)
            {
                string str = rowArray[0]["FieldValue"].ToString();
                if (!string.IsNullOrEmpty(str))
                {
                    this.SaveKeywordToTable(str, generalId);
                }
            }
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("AnonymousContent.aspx?NodeID=" + this.m_NodeId.ToString() + "&ModelID=" + this.m_ModelId.ToString());
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            this.Add();
        }

        private DataTable GetDataTableFromRepeater()
        {
            DataTable table = new DataTable();
            table.Columns.Add("FieldName");
            table.Columns.Add("FieldValue");
            table.Columns.Add("FieldType");
            table.Columns.Add("FieldLevel");
            foreach (RepeaterItem item in this.RepContentForm.Items)
            {
                FieldControl control = (FieldControl) item.FindControl("Field");
                DataRow row = table.NewRow();
                switch (control.ControlType)
                {
                    case FieldType.PictureType:
                    {
                        PictureType type2 = (PictureType) control.FindControl("EasyOne2007");
                        row = table.NewRow();
                        row["FieldName"] = control.FieldName;
                        row["FieldValue"] = type2.FieldValue;
                        row["FieldType"] = control.ControlType;
                        row["FieldLevel"] = control.FieldLevel;
                        table.Rows.Add(row);
                        if ((control.Settings.Count > 7) && DataConverter.CBoolean(control.Settings[7]))
                        {
                            DataRow row2 = table.NewRow();
                            row2["FieldName"] = "UploadFiles";
                            row2["FieldValue"] = type2.UploadFiles;
                            row2["FieldType"] = FieldType.TextType;
                            row2["FieldLevel"] = 0;
                            table.Rows.Add(row2);
                        }
                        continue;
                    }
                    case FieldType.FileType:
                    {
                        FileType type3 = (FileType) control.FindControl("EasyOne2007");
                        row["FieldName"] = control.FieldName;
                        row["FieldValue"] = type3.FieldValue;
                        row["FieldType"] = control.ControlType;
                        row["FieldLevel"] = control.FieldLevel;
                        table.Rows.Add(row);
                        if (DataConverter.CBoolean(control.Settings[3]))
                        {
                            DataRow row3 = table.NewRow();
                            row3["FieldName"] = control.Settings[4];
                            row3["FieldValue"] = type3.FileSize;
                            row3["FieldType"] = FieldType.TextType;
                            row3["FieldLevel"] = control.FieldLevel;
                            table.Rows.Add(row3);
                        }
                        continue;
                    }
                    case FieldType.NodeType:
                    {
                        row["FieldName"] = control.FieldName;
                        row["FieldValue"] = this.m_NodeId.ToString();
                        row["FieldType"] = control.ControlType;
                        row["FieldLevel"] = control.FieldLevel;
                        table.Rows.Add(row);
                        continue;
                    }
                    case FieldType.AuthorType:
                    {
                        AuthorType type6 = (AuthorType) control.FindControl("EasyOne2007");
                        row["FieldName"] = control.FieldName;
                        row["FieldValue"] = this.ReplaceQutoChar(type6.FieldValue);
                        row["FieldType"] = control.ControlType;
                        row["FieldLevel"] = control.FieldLevel;
                        table.Rows.Add(row);
                        continue;
                    }
                    case FieldType.SourceType:
                    {
                        SourceType type7 = (SourceType) control.FindControl("EasyOne2007");
                        row["FieldName"] = control.FieldName;
                        row["FieldValue"] = this.ReplaceQutoChar(type7.FieldValue);
                        row["FieldType"] = control.ControlType;
                        row["FieldLevel"] = control.FieldLevel;
                        table.Rows.Add(row);
                        continue;
                    }
                    case FieldType.KeywordType:
                    {
                        KeywordType type5 = (KeywordType) control.FindControl("EasyOne2007");
                        row["FieldName"] = control.FieldName;
                        row["FieldValue"] = this.ReplaceQutoChar(StringHelper.ReplaceChar(type5.FieldValue, ' ', '|'));
                        row["FieldType"] = control.ControlType;
                        row["FieldLevel"] = control.FieldLevel;
                        table.Rows.Add(row);
                        continue;
                    }
                    case FieldType.StatusType:
                    {
                        row["FieldName"] = control.FieldName;
                        row["FieldValue"] = "0";
                        row["FieldType"] = control.ControlType;
                        row["FieldLevel"] = control.FieldLevel;
                        table.Rows.Add(row);
                        continue;
                    }
                    case FieldType.ContentType:
                    {
                        ContentType type = (ContentType) control.FindControl("EasyOne2007");
                        row["FieldName"] = control.FieldName;
                        row["FieldValue"] = type.Content;
                        row["FieldType"] = control.ControlType;
                        row["FieldLevel"] = control.FieldLevel;
                        table.Rows.Add(row);
                        continue;
                    }
                    case FieldType.MultiplePhotoType:
                    {
                        MultiplePhotoType type4 = (MultiplePhotoType) control.FindControl("EasyOne2007");
                        row["FieldName"] = control.FieldName;
                        row["FieldValue"] = type4.FieldValue;
                        row["FieldType"] = control.ControlType;
                        row["FieldLevel"] = control.FieldLevel;
                        table.Rows.Add(row);
                        continue;
                    }
                }
                row["FieldName"] = control.FieldName;
                row["FieldValue"] = control.Value;
                row["FieldType"] = control.ControlType;
                row["FieldLevel"] = control.FieldLevel;
                table.Rows.Add(row);
            }
            DataRow row4 = table.NewRow();
            row4["FieldName"] = "Inputer";
            row4["FieldValue"] = "Anonymous";
            row4["FieldType"] = FieldType.TextType;
            row4["FieldLevel"] = 0;
            table.Rows.Add(row4);
            return table;
        }

        private static int GetGeneralId(DataTable dataTable)
        {
            return DataConverter.CLng(dataTable.Select("FieldName = 'generalId'")[0]["FieldValue"].ToString());
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.m_ModelId = BasePage.RequestInt32("ModelID");
            this.m_NodeId = BasePage.RequestInt32("NodeID");
            if (!UserPermissions.AccessCheck(OperateCode.NodeContentInput, this.m_NodeId))
            {
                DynamicPage.WriteErrMsg("您没有发表信息的权限");
            }
            bool flag = false;
            foreach (NodesModelTemplateRelationShipInfo info in ModelManager.GetNodesModelTemplateList(this.m_NodeId))
            {
                if (info.ModelId == this.m_ModelId)
                {
                    ModelInfo cacheModelById = ModelManager.GetCacheModelById(info.ModelId);
                    if (!cacheModelById.IsNull && !cacheModelById.IsEshop)
                    {
                        flag = true;
                        break;
                    }
                }
            }
            if (!flag)
            {
                DynamicPage.WriteErrMsg("<li>您没有发表信息的权限</li>");
            }
            if (!base.IsPostBack)
            {
                IList<FieldInfo> fieldList = Field.GetFieldList(this.m_ModelId, false);
                this.RepContentForm.DataSource = fieldList;
                this.RepContentForm.DataBind();
                if (!this.m_FieldInput)
                {
                    DynamicPage.WriteErrMsg("<li>该模型没有允许添加的字段，请返回从新选择模型！</li>", "AnonymousContent.aspx?NodeID=" + this.m_NodeId.ToString() + "&ModelID=" + this.m_ModelId.ToString());
                }
            }
        }

        protected void RepContentForm_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                FieldControl control = (FieldControl) e.Item.FindControl("Field");
                FieldInfo dataItem = (FieldInfo) e.Item.DataItem;
                FieldType controlType = control.ControlType;
                if (controlType == FieldType.NodeType)
                {
                    control.Value = this.m_NodeId.ToString();
                }
                if (((control.FieldLevel == 0) && (dataItem.Id != "title")) && (dataItem.FieldType != FieldType.SpecialType))
                {
                    control.FindControl("EasyOne2007").Visible = false;
                }
                if (controlType == FieldType.ContentType)
                {
                    ((ContentType) control.FindControl("EasyOne2007")).IsUpload = true;
                }
                if (control.Visible)
                {
                    this.m_FieldInput = true;
                }
            }
        }

        private string ReplaceQutoChar(string source)
        {
            source = source.Replace("'", "");
            return source;
        }

        private void SaveKeywordToTable(string txtKeyWord, int generalId)
        {
            foreach (string str in txtKeyWord.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (EasyOne.Accessories.Keywords.Exists(str))
                {
                    KeywordInfo keywordByKeywordName = EasyOne.Accessories.Keywords.GetKeywordByKeywordName(str);
                    string str2 = ContentManage.RebuildArr(keywordByKeywordName.ArrayGeneralId + "," + generalId.ToString());
                    int length = str2.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Length;
                    DateTime lastUseTime = keywordByKeywordName.LastUseTime;
                    keywordByKeywordName.ArrayGeneralId = str2;
                    keywordByKeywordName.QuoteTimes = length;
                    EasyOne.Accessories.Keywords.Update(keywordByKeywordName);
                }
                else
                {
                    KeywordInfo keywordInfo = new KeywordInfo();
                    keywordInfo.KeywordText = str;
                    keywordInfo.KeywordType = 1;
                    keywordInfo.LastUseTime = DateTime.Now;
                    keywordInfo.Priority = 0;
                    keywordInfo.ArrayGeneralId = generalId.ToString();
                    keywordInfo.QuoteTimes = 1;
                    EasyOne.Accessories.Keywords.Add(keywordInfo);
                }
            }
        }
    }
}

