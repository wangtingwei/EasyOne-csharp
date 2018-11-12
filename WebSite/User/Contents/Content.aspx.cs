namespace EasyOne.WebSite.User.Contents
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Controls;
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

    public partial class Content : DynamicPage
    {
        private string m_Action;
        protected DataTable m_ContentData;
        private int m_ModelId;
        private int m_NodeId;
        private UserInfo m_User;
        private UserPurviewInfo m_UserPurviewInfo;

        private void Add()
        {
            DataTable newContentData = ContentManage.GetNewContentData(this.GetDataTableFromRepeater());
            if (this.m_UserPurviewInfo == null)
            {
                this.m_UserPurviewInfo = new UserPurviewInfo();
                this.m_UserPurviewInfo.MaxPublicInfoOneDay = -1;
                this.m_UserPurviewInfo.GetExp = 1;
            }
            if ((this.m_UserPurviewInfo.MaxPublicInfoOneDay == 0) || (this.m_UserPurviewInfo.MaxPublicInfoOneDay > ContentManage.GetTodayPublicInfoCountByUserName(PEContext.Current.User.UserName)))
            {
                if (this.m_UserPurviewInfo.PublicInfoNoNeedCheck)
                {
                    DataRow[] rowArray = newContentData.Select("FieldName = 'status'");
                    if (rowArray[0]["FieldValue"].ToString() == "0")
                    {
                        rowArray[0]["FieldValue"] = "99";
                    }
                }
                this.SavePresentExp(newContentData, this.m_UserPurviewInfo, this.m_User);
                if (ContentManage.Add(this.m_ModelId, newContentData))
                {
                    if (ModelManager.GetModelInfoById(this.m_ModelId).EnableCharge)
                    {
                        this.AddCharge(newContentData);
                    }
                    this.AddKeywordsToTable(newContentData);
                    DynamicPage.WriteUserSuccessMsg("添加成功！", "ContentManage.aspx");
                }
                else
                {
                    DynamicPage.WriteUserErrMsg("添加失败！");
                }
            }
            else
            {
                DynamicPage.WriteUserErrMsg("你今天发布的内容信息总数大于网站设定的值，如要添加请与管理员联系！");
            }
        }

        private void AddCharge(DataTable dataTable)
        {
            int generalId = DataConverter.CLng(dataTable.Select("FieldName = 'generalId'")[0]["FieldValue"].ToString());
            NodeInfo cacheNodeById = EasyOne.Contents.Nodes.GetCacheNodeById(DataConverter.CLng(dataTable.Select("FieldName = 'nodeId'")[0]["FieldValue"].ToString()));
            ContentPermissionInfo contentPermissionInfo = new ContentPermissionInfo();
            contentPermissionInfo.GeneralId = generalId;
            contentPermissionInfo.PermissionType = 0;
            PermissionContent.Delete(generalId);
            if (PermissionContent.Add(contentPermissionInfo))
            {
                ContentCharge.Delete(generalId);
                if (cacheNodeById.Settings.DefaultItemPoint > 0)
                {
                    ContentChargeInfo contentChargeInfo = new ContentChargeInfo();
                    contentChargeInfo.GeneralId = generalId;
                    contentChargeInfo.InfoPoint = cacheNodeById.Settings.DefaultItemPoint;
                    contentChargeInfo.ChargeType = cacheNodeById.Settings.DefaultItemChargeType;
                    contentChargeInfo.PitchTime = cacheNodeById.Settings.DefaultItemPitchTime;
                    contentChargeInfo.ReadTimes = cacheNodeById.Settings.DefaultItemReadTimes;
                    contentChargeInfo.DividePercent = cacheNodeById.Settings.DefaultItemDividePercent;
                    ContentCharge.Add(contentChargeInfo);
                }
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

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (this.m_Action == "add")
            {
                this.Add();
            }
            if (this.m_Action == "modify")
            {
                this.Update();
            }
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
                        EasyOne.WebSite.Controls.FieldControl.NodeType type7 = (EasyOne.WebSite.Controls.FieldControl.NodeType) control.FindControl("EasyOne2007");
                        row["FieldName"] = control.FieldName;
                        row["FieldValue"] = type7.FieldValue;
                        row["FieldType"] = control.ControlType;
                        row["FieldLevel"] = control.FieldLevel;
                        table.Rows.Add(row);
                        DataRow row4 = table.NewRow();
                        row4["FieldName"] = "infoid";
                        row4["FieldValue"] = type7.InfoNodeId;
                        row4["FieldType"] = FieldType.InfoType;
                        row4["FieldLevel"] = control.FieldLevel;
                        table.Rows.Add(row4);
                        continue;
                    }
                    case FieldType.AuthorType:
                    {
                        AuthorType type8 = (AuthorType) control.FindControl("EasyOne2007");
                        row["FieldName"] = control.FieldName;
                        row["FieldValue"] = this.ReplaceQutoChar(type8.FieldValue);
                        row["FieldType"] = control.ControlType;
                        row["FieldLevel"] = control.FieldLevel;
                        table.Rows.Add(row);
                        continue;
                    }
                    case FieldType.SourceType:
                    {
                        SourceType type6 = (SourceType) control.FindControl("EasyOne2007");
                        row["FieldName"] = control.FieldName;
                        row["FieldValue"] = this.ReplaceQutoChar(type6.FieldValue);
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
            DataRow row5 = table.NewRow();
            row5["FieldName"] = "Inputer";
            row5["FieldValue"] = PEContext.Current.User.UserName;
            row5["FieldType"] = FieldType.TextType;
            row5["FieldLevel"] = 0;
            table.Rows.Add(row5);
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
            this.m_Action = BasePage.RequestStringToLower("Action");
            if (((this.m_ModelId <= 0) || (this.m_NodeId <= 0)) || string.IsNullOrEmpty(this.m_Action))
            {
                DynamicPage.WriteUserErrMsg("添加信息参数错误！", "NavContent.aspx");
            }
            this.m_User = Users.GetUsersByUserName(PEContext.Current.User.UserName);
            this.m_UserPurviewInfo = this.m_User.UserPurview;
            if (!base.IsPostBack)
            {
                if (this.m_Action == "add")
                {
                    if (!EasyOne.Contents.Nodes.CheckNodePermission(this.m_NodeId))
                    {
                        DynamicPage.WriteUserErrMsg("此栏目设置了有子节点时不允许向该栏目添加信息！", "ContentManage.aspx");
                    }
                    IList<FieldInfo> fieldList = Field.GetFieldList(this.m_ModelId, false);
                    this.RepContentForm.DataSource = fieldList;
                    this.RepContentForm.DataBind();
                }
                if (this.m_Action == "modify")
                {
                    int generalId = BasePage.RequestInt32("GeneralID");
                    this.m_ContentData = ContentManage.GetUserContentDataById(generalId);
                    if (this.m_ContentData.Rows.Count <= 0)
                    {
                        DynamicPage.WriteUserErrMsg("信息不存在", "ContentManage.aspx");
                    }
                    if (!this.m_UserPurviewInfo.ManageSelfPublicInfo && (DataConverter.CLng(this.m_ContentData.Rows[0]["Status"].ToString()) == 0x63))
                    {
                        DynamicPage.WriteUserErrMsg("不能修改审核通过的信息", "ContentManage.aspx");
                    }
                    IList<FieldInfo> list2 = Field.GetFieldList(DataConverter.CLng(this.m_ContentData.Rows[0]["ModelID"].ToString()), false);
                    this.RepContentForm.DataSource = list2;
                    this.RepContentForm.DataBind();
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
                if ((((control.FieldLevel == 0) && (dataItem.Id != "title")) && ((controlType != FieldType.NodeType) && (controlType != FieldType.SpecialType))) && ((controlType != FieldType.StatusType) && (controlType != FieldType.PictureType)))
                {
                    control.FindControl("EasyOne2007").Visible = false;
                }
                if ((controlType == FieldType.ContentType) && !this.m_UserPurviewInfo.SetEditor)
                {
                    ContentType type2 = (ContentType) control.FindControl("EasyOne2007");
                    type2.Editor.ToolbarSet = "Simple";
                }
                if (controlType == FieldType.ContentType)
                {
                    ((ContentType) control.FindControl("EasyOne2007")).IsUpload = true;
                }
                if (this.m_Action == "modify")
                {
                    if (controlType == FieldType.ContentType)
                    {
                        ContentType type3 = (ContentType) control.FindControl("EasyOne2007");
                        type3.Content = ContentManage.ToFieldType(this.m_ContentData.Rows[0][dataItem.FieldName].ToString(), dataItem.FieldType);
                        type3.DefaultPicurl = ContentManage.ToFieldType(this.m_ContentData.Rows[0]["DefaultPicurl"].ToString(), FieldType.TextType);
                        type3.IsUpload = true;
                    }
                    else
                    {
                        control.Value = ContentManage.ToFieldType(this.m_ContentData.Rows[0][dataItem.FieldName].ToString(), dataItem.FieldType);
                    }
                    if (controlType == FieldType.KeywordType)
                    {
                        control.Value = StringHelper.ReplaceChar(control.Value, '|', ' ');
                    }
                    if ((controlType == FieldType.PictureType) && (dataItem.FieldLevel == 0))
                    {
                        PictureType type4 = (PictureType) control.FindControl("EasyOne2007");
                        type4.UploadFiles = ContentManage.ToFieldType(this.m_ContentData.Rows[0]["UploadFiles"].ToString(), FieldType.TextType);
                    }
                    if (controlType == FieldType.FileType)
                    {
                        FileType type5 = (FileType) control.FindControl("EasyOne2007");
                        if (DataConverter.CBoolean(dataItem.Settings[3]))
                        {
                            type5.FileSize = ContentManage.ToFieldType(this.m_ContentData.Rows[0][dataItem.Settings[4]].ToString(), dataItem.FieldType);
                        }
                    }
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

        private void SavePresentExp(DataTable dataTable, UserPurviewInfo up, UserInfo user)
        {
            int num = DataConverter.CLng(dataTable.Select("FieldName = 'status'")[0]["FieldValue"].ToString());
            int nodeId = DataConverter.CLng(dataTable.Select("FieldName = 'nodeId'")[0]["FieldValue"].ToString());
            if (up.GetExp == 0)
            {
                up.GetExp = 1;
            }
            int num3 = EasyOne.Contents.Nodes.GetCacheNodeById(nodeId).Settings.PresentExp * up.GetExp;
            if (num == 0x63)
            {
                user.UserExp += num3;
                user.PassedItems++;
            }
            else if (this.m_Action == "modify")
            {
                user.UserExp -= num3;
                user.PassedItems--;
            }
            if (this.m_Action == "add")
            {
                user.PostItems++;
            }
            Users.Update(user);
        }

        private void Update()
        {
            DataTable dataTableFromRepeater = this.GetDataTableFromRepeater();
            DataRow[] rowArray = dataTableFromRepeater.Select("FieldName = 'status'");
            if ((DataConverter.CLng(rowArray[0]["FieldValue"]) == 0x63) && !this.m_UserPurviewInfo.ManageSelfPublicInfo)
            {
                DynamicPage.WriteUserErrMsg("已经被审核通过，您不能再进行修改！");
            }
            DataRow[] rowArray2 = dataTableFromRepeater.Select("FieldName = 'updatetime'");
            if (rowArray2.Length > 0)
            {
                rowArray2[0]["FieldValue"] = DateTime.Now;
            }
            if (this.m_UserPurviewInfo.SetToNotCheck && !this.m_UserPurviewInfo.PublicInfoNoNeedCheck)
            {
                rowArray[0]["FieldValue"] = "0";
            }
            int generalId = BasePage.RequestInt32("GeneralID");
            if (ContentManage.UpdateByUser(generalId, ContentManage.GetNewContentData(dataTableFromRepeater)))
            {
                this.SavePresentExp(dataTableFromRepeater, this.m_UserPurviewInfo, this.m_User);
                this.UpdateKeywordsToTable(generalId, dataTableFromRepeater);
                DynamicPage.WriteUserSuccessMsg("修改成功！", "ContentManage.aspx");
            }
            else
            {
                DynamicPage.WriteUserErrMsg("修改失败！");
            }
        }

        private void UpdateKeywordsToTable(int generalId, DataTable dataTable)
        {
            DataRow[] rowArray = dataTable.Select("FieldName = 'keyword'");
            if (rowArray.Length > 0)
            {
                string str = rowArray[0]["FieldValue"].ToString();
                if (!string.IsNullOrEmpty(str))
                {
                    KeywordInfo keywordById = EasyOne.Accessories.Keywords.GetKeywordById(generalId);
                    if (str != keywordById.KeywordText)
                    {
                        this.SaveKeywordToTable(str, generalId);
                    }
                }
            }
        }
    }
}

