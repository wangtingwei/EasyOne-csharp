namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.Accessories;
    using EasyOne.Model.CommonModel;
    using EasyOne.Model.Contents;
    using EasyOne.Model.UserManage;
    using EasyOne.ModelControls;
    using EasyOne.StaticHtml;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using EasyOne.WebSite.Controls.FieldControl;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class Content : AdminPage
    {
        public StringBuilder arrTrs0 = new StringBuilder();
        public StringBuilder arrTrs1 = new StringBuilder();
        private string m_Action;
        protected DataTable m_ContentDataTable;
        private string m_ContentFieldName = "";
        private int m_ModelId;
        private int m_NodeId;
        protected string m_TbodyChargeId = "";

        private void Add()
        {
            DataTable newContentData = ContentManage.GetNewContentData(this.GetDataTableFromRepeater());
            if (ContentManage.Add(this.m_ModelId, newContentData))
            {
                ModelInfo modelInfoById = ModelManager.GetModelInfoById(this.m_ModelId);
                if (modelInfoById.EnableSignIn)
                {
                    this.AddSignin(newContentData);
                }
                this.AddPermissionAndCharge(newContentData, modelInfoById.EnableCharge);
                this.SavePresentExp(newContentData, true);
                if (((SignInType) Enum.Parse(typeof(SignInType), this.DrpSigninType.SelectedValue)) != SignInType.EnableSignInPrivate)
                {
                    HtmlContent.CreateHtml(newContentData);
                }
                this.AddKeywordsToTable(newContentData);
                int generalId = GetGeneralId(newContentData);
                if (modelInfoById.EnbaleVote)
                {
                    this.Vote.Add(generalId);
                }
                SiteCache.Remove("CK_Page_Category_" + this.m_NodeId.ToString());
                BasePage.ResponseRedirect(AdminPage.AppendSecurityCode("ContentShowSuccess.aspx?Action=Add&GeneralID=" + generalId.ToString() + "&NodeID=" + this.m_NodeId.ToString() + "&ModelID=" + this.m_ModelId.ToString() + "&ContentFieldName=" + this.m_ContentFieldName));
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

        private int AddPermissionAndCharge(DataTable dataTable, bool enableCharge)
        {
            int generalId = GetGeneralId(dataTable);
            ContentPermissionInfo contentPermissionInfo = new ContentPermissionInfo();
            contentPermissionInfo.GeneralId = generalId;
            contentPermissionInfo.PermissionType = DataConverter.CLng(this.RadlInfoPurview.SelectedValue);
            contentPermissionInfo.ArrGroupId = this.EChklUserGroupList.SelectList();
            int num2 = DataConverter.CLng(this.TxtInfoPoint.Text);
            PermissionContent.Delete(generalId);
            if (PermissionContent.Add(contentPermissionInfo))
            {
                EasyOne.Contents.ContentCharge.Delete(generalId);
                if (enableCharge && (num2 > 0))
                {
                    ContentChargeInfo contentChargeInfo = new ContentChargeInfo();
                    contentChargeInfo.GeneralId = generalId;
                    contentChargeInfo.InfoPoint = num2;
                    contentChargeInfo.ChargeType = this.ShowChargeType.ChargeType;
                    contentChargeInfo.PitchTime = this.ShowChargeType.PitchTime;
                    contentChargeInfo.ReadTimes = this.ShowChargeType.ReadTimes;
                    contentChargeInfo.DividePercent = DataConverter.CLng(this.TxtDividePercent.Text);
                    EasyOne.Contents.ContentCharge.Add(contentChargeInfo);
                }
            }
            return num2;
        }

        private void AddSignin(DataTable dataTable)
        {
            if (!string.IsNullOrEmpty(this.TxtSigninUser.Text))
            {
                int generalId = GetGeneralId(dataTable);
                string str = dataTable.Select("FieldName = 'title'")[0]["FieldValue"].ToString();
                SignInContentInfo signInContentInfo = new SignInContentInfo();
                signInContentInfo.GeneralId = generalId;
                signInContentInfo.EndTime = this.DpkEndTime.Date;
                signInContentInfo.Priority = DataConverter.CLng(this.TxtPriority.Text);
                signInContentInfo.SignInType = (SignInType) Enum.Parse(typeof(SignInType), this.DrpSigninType.SelectedValue);
                signInContentInfo.Status = SignInStatus.NotSignIn;
                signInContentInfo.Title = str;
                SignInContent.AddSignInContent(signInContentInfo);
                SignInLog.Add(generalId, this.TxtSigninUser.Text);
            }
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("ContentManage.aspx?NodeID=" + this.m_NodeId.ToString() + "&ModelID=" + this.m_ModelId.ToString());
        }

        protected void EBtnNewAddItem_Click(object sender, EventArgs e)
        {
            this.m_Action = "add";
            this.Add();
        }

        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
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
        }

        private DataTable GetDataTableFromRepeater()
        {
            DataRow row;
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("FieldName");
            dataTable.Columns.Add("FieldValue");
            dataTable.Columns.Add("FieldType");
            dataTable.Columns.Add("FieldLevel");
            foreach (RepeaterItem item in this.RepModel.Items)
            {
                FieldControl control = (FieldControl) item.FindControl("Field");
                switch (control.ControlType)
                {
                    case FieldType.PictureType:
                    {
                        PictureType type2 = (PictureType) control.FindControl("EasyOne2007");
                        row = dataTable.NewRow();
                        row["FieldName"] = control.FieldName;
                        row["FieldValue"] = type2.FieldValue;
                        row["FieldType"] = control.ControlType;
                        row["FieldLevel"] = control.FieldLevel;
                        dataTable.Rows.Add(row);
                        if ((control.Settings.Count > 7) && DataConverter.CBoolean(control.Settings[7]))
                        {
                            DataRow row2 = dataTable.NewRow();
                            row2["FieldName"] = "UploadFiles";
                            row2["FieldValue"] = type2.UploadFiles;
                            row2["FieldType"] = FieldType.TextType;
                            row2["FieldLevel"] = 0;
                            dataTable.Rows.Add(row2);
                        }
                        continue;
                    }
                    case FieldType.FileType:
                    {
                        FileType type3 = (FileType) control.FindControl("EasyOne2007");
                        row = dataTable.NewRow();
                        row["FieldName"] = control.FieldName;
                        row["FieldValue"] = type3.FieldValue;
                        row["FieldType"] = control.ControlType;
                        row["FieldLevel"] = control.FieldLevel;
                        dataTable.Rows.Add(row);
                        if (DataConverter.CBoolean(control.Settings[3]))
                        {
                            DataRow row3 = dataTable.NewRow();
                            row3["FieldName"] = control.Settings[4];
                            row3["FieldValue"] = type3.FileSize;
                            row3["FieldType"] = FieldType.TextType;
                            row3["FieldLevel"] = control.FieldLevel;
                            dataTable.Rows.Add(row3);
                        }
                        continue;
                    }
                    case FieldType.NodeType:
                    {
                        EasyOne.WebSite.Controls.FieldControl.NodeType type5 = (EasyOne.WebSite.Controls.FieldControl.NodeType) control.FindControl("EasyOne2007");
                        row = dataTable.NewRow();
                        row["FieldName"] = control.FieldName;
                        row["FieldValue"] = type5.FieldValue;
                        row["FieldType"] = control.ControlType;
                        row["FieldLevel"] = control.FieldLevel;
                        dataTable.Rows.Add(row);
                        row = dataTable.NewRow();
                        row["FieldName"] = "infoid";
                        row["FieldValue"] = type5.InfoNodeId;
                        row["FieldType"] = FieldType.InfoType;
                        row["FieldLevel"] = control.FieldLevel;
                        dataTable.Rows.Add(row);
                        continue;
                    }
                    case FieldType.InfoType:
                    {
                        continue;
                    }
                    case FieldType.AuthorType:
                    {
                        if ((control.Settings.Count > 1) && DataConverter.CBoolean(control.Settings[1]))
                        {
                            this.Session["AuthorValue"] = control.Value;
                        }
                        row = dataTable.NewRow();
                        row["FieldName"] = control.FieldName;
                        row["FieldValue"] = control.Value;
                        row["FieldType"] = control.ControlType;
                        row["FieldLevel"] = control.FieldLevel;
                        dataTable.Rows.Add(row);
                        continue;
                    }
                    case FieldType.SourceType:
                    {
                        if ((control.Settings.Count > 1) && DataConverter.CBoolean(control.Settings[1]))
                        {
                            this.Session["SourceValue"] = control.Value;
                        }
                        row = dataTable.NewRow();
                        row["FieldName"] = control.FieldName;
                        row["FieldValue"] = control.Value;
                        row["FieldType"] = control.ControlType;
                        row["FieldLevel"] = control.FieldLevel;
                        dataTable.Rows.Add(row);
                        continue;
                    }
                    case FieldType.KeywordType:
                    {
                        row = dataTable.NewRow();
                        row["FieldName"] = control.FieldName;
                        row["FieldValue"] = StringHelper.ReplaceChar(control.Value.Trim(), ' ', '|');
                        row["FieldType"] = control.ControlType;
                        row["FieldLevel"] = control.FieldLevel;
                        dataTable.Rows.Add(row);
                        continue;
                    }
                    case FieldType.ContentType:
                    {
                        ContentType type = (ContentType) control.FindControl("EasyOne2007");
                        row = dataTable.NewRow();
                        row["FieldName"] = control.FieldName;
                        row["FieldValue"] = type.Content;
                        row["FieldType"] = control.ControlType;
                        row["FieldLevel"] = control.FieldLevel;
                        dataTable.Rows.Add(row);
                        if (type.SaveRemotePic)
                        {
                            if (string.IsNullOrEmpty(this.m_ContentFieldName))
                            {
                                break;
                            }
                            this.m_ContentFieldName = this.m_ContentFieldName + "$" + control.FieldName;
                        }
                        continue;
                    }
                    case FieldType.TitleType:
                    {
                        TitleType type6 = (TitleType) control.FindControl("EasyOne2007");
                        row = dataTable.NewRow();
                        row["FieldName"] = control.FieldName;
                        row["FieldValue"] = control.Value;
                        row["FieldType"] = control.ControlType;
                        row["FieldLevel"] = control.FieldLevel;
                        dataTable.Rows.Add(row);
                        string pinyinTitle = type6.PinyinTitle;
                        if (string.IsNullOrEmpty(pinyinTitle))
                        {
                            pinyinTitle = ChineseSpell.MakeSpellCode(control.Value, SpellOptions.EnableUnicodeLetter);
                        }
                        row = dataTable.NewRow();
                        row["FieldName"] = "PinyinTitle";
                        row["FieldValue"] = pinyinTitle;
                        row["FieldType"] = FieldType.TextType;
                        row["FieldLevel"] = 0;
                        dataTable.Rows.Add(row);
                        continue;
                    }
                    case FieldType.MultiplePhotoType:
                    {
                        MultiplePhotoType type4 = (MultiplePhotoType) control.FindControl("EasyOne2007");
                        row = dataTable.NewRow();
                        row["FieldName"] = control.FieldName;
                        row["FieldValue"] = type4.FieldValue;
                        row["FieldType"] = control.ControlType;
                        row["FieldLevel"] = control.FieldLevel;
                        dataTable.Rows.Add(row);
                        continue;
                    }
                    default:
                        goto Label_0773;
                }
                this.m_ContentFieldName = this.m_ContentFieldName + control.FieldName;
                continue;
            Label_0773:
                row = dataTable.NewRow();
                row["FieldName"] = control.FieldName;
                row["FieldValue"] = control.Value;
                row["FieldType"] = control.ControlType;
                row["FieldLevel"] = control.FieldLevel;
                dataTable.Rows.Add(row);
            }
            if (this.m_Action == "add")
            {
                row = dataTable.NewRow();
                row["FieldName"] = "Inputer";
                row["FieldValue"] = PEContext.Current.Admin.UserName;
                row["FieldType"] = FieldType.TextType;
                row["FieldLevel"] = 0;
                dataTable.Rows.Add(row);
            }
            if (GetStatusFromDataTable(dataTable) == 0x63)
            {
                row = dataTable.NewRow();
                row["FieldName"] = "Editor";
                row["FieldValue"] = PEContext.Current.Admin.AdminName;
                row["FieldType"] = FieldType.TextType;
                row["FieldLevel"] = 0;
                dataTable.Rows.Add(row);
                row = dataTable.NewRow();
                row["FieldName"] = "PassedTime";
                row["FieldValue"] = DateTime.Now.ToString("yyyy-MM-dd");
                row["FieldType"] = FieldType.DateTimeType;
                row["FieldLevel"] = 0;
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }

        private static int GetGeneralId(DataTable dataTable)
        {
            return DataConverter.CLng(dataTable.Select("FieldName = 'generalId'")[0]["FieldValue"].ToString());
        }

        private static int GetStatusFromDataTable(DataTable dataTable)
        {
            return DataConverter.CLng(dataTable.Select("FieldName = 'status'")[0]["FieldValue"].ToString());
        }

        private void InitCharge(int generalId, bool enableCharge)
        {
            ContentPermissionInfo contentPermissionInfoById = PermissionContent.GetContentPermissionInfoById(generalId);
            if (!contentPermissionInfoById.IsNull)
            {
                this.RadlInfoPurview.SelectedValue = contentPermissionInfoById.PermissionType.ToString();
                if (!string.IsNullOrEmpty(contentPermissionInfoById.ArrGroupId))
                {
                    string[] strArray = contentPermissionInfoById.ArrGroupId.Split(new char[] { ',' });
                    for (int i = 0; i < strArray.Length; i++)
                    {
                        this.EChklUserGroupList.Items.FindByValue(strArray[i]).Selected = true;
                    }
                }
            }
            if (enableCharge)
            {
                ContentChargeInfo contentChargeInfoById = EasyOne.Contents.ContentCharge.GetContentChargeInfoById(generalId);
                if (!contentChargeInfoById.IsNull)
                {
                    this.TxtInfoPoint.Text = contentChargeInfoById.InfoPoint.ToString();
                    this.ShowChargeType.ChargeType = contentChargeInfoById.ChargeType;
                    this.ShowChargeType.PitchTime = contentChargeInfoById.PitchTime;
                    this.ShowChargeType.ReadTimes = contentChargeInfoById.ReadTimes;
                    this.TxtDividePercent.Text = contentChargeInfoById.DividePercent.ToString();
                }
            }
        }

        private void InitializePage()
        {
            if (!this.Page.IsPostBack)
            {
                string str;
                ModelInfo modelInfoById = ModelManager.GetModelInfoById(this.m_ModelId);
                if (this.m_Action == "add")
                {
                    if (!EasyOne.Contents.Nodes.CheckNodePermission(this.m_NodeId))
                    {
                        AdminPage.WriteErrMsg("此栏目设置了有子节点时不允许向该栏目添加信息！", "ContentManage.aspx");
                    }
                    str = "添加" + modelInfoById.ItemName;
                }
                else
                {
                    str = "修改" + modelInfoById.ItemName;
                }
                if (this.m_NodeId > 0)
                {
                    NodeInfo cacheNodeById = EasyOne.Contents.Nodes.GetCacheNodeById(this.m_NodeId);
                    this.LblNodeName.Text = "所属节点：<span style='color:red'>" + cacheNodeById.NodeName + "</span>";
                    str = (EasyOne.Contents.Nodes.ShowNodeNavigation(this.m_NodeId) + " >> " + str).Replace("根节点 >>", "");
                }
                this.SmpNavigator.CurrentNode = "</a>" + str;
                this.m_TbodyChargeId = ",\"" + this.TbodyCharge.ClientID + "\"";
                if (!modelInfoById.EnableCharge)
                {
                    this.m_TbodyChargeId = "";
                }
                if (!modelInfoById.EnableSignIn)
                {
                    this.TabTitle3.Style.Add("display", "none");
                }
                if (!modelInfoById.EnbaleVote)
                {
                    this.TabTitle4.Style.Add("display", "none");
                }
                this.InitUserGroupCheckBoxList();
                if (this.m_Action == "add")
                {
                    IList<FieldInfo> fieldList = Field.GetFieldList(this.m_ModelId, false);
                    this.RepModel.DataSource = fieldList;
                    this.RepModel.DataBind();
                    NodeInfo info3 = EasyOne.Contents.Nodes.GetCacheNodeById(this.m_NodeId);
                    if (!info3.IsNull)
                    {
                        this.TxtInfoPoint.Text = info3.Settings.DefaultItemPoint.ToString();
                        this.ShowChargeType.ChargeType = info3.Settings.DefaultItemChargeType;
                        this.ShowChargeType.PitchTime = info3.Settings.DefaultItemPitchTime;
                        this.ShowChargeType.ReadTimes = info3.Settings.DefaultItemReadTimes;
                        this.TxtDividePercent.Text = info3.Settings.DefaultItemDividePercent.ToString();
                    }
                }
                else
                {
                    int generalId = BasePage.RequestInt32("GeneralID");
                    this.m_ContentDataTable = ContentManage.GetContentDataById(generalId);
                    if ((this.m_ContentDataTable == null) || (this.m_ContentDataTable.Rows.Count == 0))
                    {
                        AdminPage.WriteErrMsg("<li>指定项目不存在！</li>");
                    }
                    IList<FieldInfo> list2 = Field.GetFieldList(DataConverter.CLng(this.m_ContentDataTable.Rows[0]["ModelID"].ToString()), false);
                    this.RepModel.DataSource = list2;
                    this.RepModel.DataBind();
                    this.InitSignin(generalId, modelInfoById.EnableSignIn);
                    this.InitCharge(generalId, modelInfoById.EnableCharge);
                }
                if (!SiteConfig.SiteOption.EnablePointMoneyExp)
                {
                    this.m_TbodyChargeId = "";
                }
            }
        }

        private void InitSignin(int generalId, bool enableSignin)
        {
            if (enableSignin)
            {
                SignInContentInfo signInContentByGeneralId = SignInContent.GetSignInContentByGeneralId(generalId);
                if (!signInContentByGeneralId.IsNull)
                {
                    this.TxtPriority.Text = signInContentByGeneralId.Priority.ToString();
                    BasePage.SetSelectedIndexByValue(this.DrpSigninType, signInContentByGeneralId.SignInType.ToString());
                    this.DpkEndTime.Text = signInContentByGeneralId.EndTime.ToString();
                }
                this.TxtSigninUser.Text = SignInLog.GetSignInUsers(generalId);
            }
        }

        private HtmlControl InitTabByFieldType(RepeaterItemEventArgs e, FieldInfo fieldInfo)
        {
            HtmlControl control = (HtmlControl) e.Item.FindControl("Field").FindControl("EasyOne2007").FindControl("Tab");
            if (e.Item.FindControl("Field").FindControl("EasyOne2007").Visible && control.Visible)
            {
                if (((((fieldInfo.FieldLevel == 0) && (fieldInfo.Id != "title")) && ((fieldInfo.FieldType != FieldType.NodeType) && (fieldInfo.Id != "elitelevel"))) && (((fieldInfo.Id != "priority") && (fieldInfo.Id != "status")) && ((fieldInfo.FieldType != FieldType.InfoType) && (fieldInfo.FieldType != FieldType.SpecialType)))) && (fieldInfo.Id != "defaultpicurl"))
                {
                    control.Style.Add("display", "none");
                    if (this.arrTrs1.Length == 0)
                    {
                        this.arrTrs1.Append("\"" + control.ClientID + "\"");
                        return control;
                    }
                    this.arrTrs1.Append(",\"" + control.ClientID + "\"");
                    return control;
                }
                if (this.arrTrs0.Length == 0)
                {
                    this.arrTrs0.Append("\"" + control.ClientID + "\"");
                    return control;
                }
                this.arrTrs0.Append(",\"" + control.ClientID + "\"");
            }
            return control;
        }

        private void InitUserGroupCheckBoxList()
        {
            this.EChklUserGroupList.Items.Clear();
            IList<UserGroupsInfo> userGroupList = UserGroups.GetUserGroupList(0, 0);
            userGroupList.RemoveAt(0);
            this.EChklUserGroupList.DataSource = userGroupList;
            this.EChklUserGroupList.DataTextField = "GroupName";
            this.EChklUserGroupList.DataValueField = "GroupId";
            this.EChklUserGroupList.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.m_ModelId = BasePage.RequestInt32("ModelID");
            this.m_NodeId = BasePage.RequestInt32("NodeID");
            this.m_Action = BasePage.RequestStringToLower("Action");
            if (this.m_Action == "modify")
            {
                this.EBtnSubmit.Text = " 保存修改的项目 ";
                this.EBtnNewAddItem.Visible = true;
            }
            this.InitializePage();
        }

        protected void RepModel_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                FieldControl control = (FieldControl) e.Item.FindControl("Field");
                FieldInfo dataItem = (FieldInfo) e.Item.DataItem;
                FieldType controlType = control.ControlType;
                switch (controlType)
                {
                    case FieldType.LookType:
                    {
                        int modelId = DataConverter.CLng(dataItem.Settings[0]);
                        if (!Field.FieldExists(modelId, dataItem.Settings[1]))
                        {
                            Field.SetDisabled(dataItem.FieldName, modelId, true);
                            e.Item.Visible = false;
                        }
                        break;
                    }
                    case FieldType.NodeType:
                        control.Value = this.m_NodeId.ToString();
                        break;
                }
                if (this.m_Action == "add")
                {
                    if ((((controlType == FieldType.AuthorType) && string.IsNullOrEmpty(dataItem.DefaultValue)) && ((dataItem.Settings.Count > 1) && DataConverter.CBoolean(dataItem.Settings[1]))) && (this.Session["AuthorValue"] != null))
                    {
                        control.Value = this.Session["AuthorValue"].ToString();
                    }
                    if ((((controlType == FieldType.SourceType) && string.IsNullOrEmpty(dataItem.DefaultValue)) && ((dataItem.Settings.Count > 1) && DataConverter.CBoolean(dataItem.Settings[1]))) && (this.Session["SourceValue"] != null))
                    {
                        control.Value = this.Session["SourceValue"].ToString();
                    }
                }
                if (controlType == FieldType.ContentType)
                {
                    ((ContentType) control.FindControl("EasyOne2007")).IsUpload = true;
                }
                HtmlControl control2 = this.InitTabByFieldType(e, dataItem);
                if (this.m_Action == "modify")
                {
                    if (((BasePage.RequestInt32("LinkType") == 1) && (dataItem.FieldLevel == 1)) && control.FindControl("EasyOne2007").Visible)
                    {
                        if (this.arrTrs0.Length > (control2.ClientID.Length + 3))
                        {
                            this.arrTrs0.Remove((this.arrTrs0.Length - control2.ClientID.Length) - 3, control2.ClientID.Length + 3);
                        }
                        else
                        {
                            this.arrTrs0.Remove((this.arrTrs0.Length - control2.ClientID.Length) - 2, control2.ClientID.Length + 2);
                        }
                        control.FindControl("EasyOne2007").Visible = false;
                    }
                    if (controlType == FieldType.ContentType)
                    {
                        ContentType type2 = (ContentType) control.FindControl("EasyOne2007");
                        type2.Content = ContentManage.ToFieldType(this.m_ContentDataTable.Rows[0][dataItem.FieldName].ToString(), dataItem.FieldType);
                        type2.DefaultPicurl = ContentManage.ToFieldType(this.m_ContentDataTable.Rows[0]["DefaultPicurl"].ToString(), FieldType.TextType);
                    }
                    else
                    {
                        control.Value = ContentManage.ToFieldType(this.m_ContentDataTable.Rows[0][dataItem.FieldName].ToString(), dataItem.FieldType);
                    }
                    if ((controlType == FieldType.PictureType) && (dataItem.FieldLevel == 0))
                    {
                        PictureType type3 = (PictureType) control.FindControl("EasyOne2007");
                        type3.UploadFiles = ContentManage.ToFieldType(this.m_ContentDataTable.Rows[0]["UploadFiles"].ToString(), FieldType.TextType);
                    }
                    if (controlType == FieldType.FileType)
                    {
                        FileType type4 = (FileType) control.FindControl("EasyOne2007");
                        if (DataConverter.CBoolean(dataItem.Settings[3]))
                        {
                            type4.FileSize = ContentManage.ToFieldType(this.m_ContentDataTable.Rows[0][dataItem.Settings[4]].ToString(), dataItem.FieldType);
                        }
                    }
                    if ((string.Compare("UpdateTime", control.FieldName) == 0) && (control.FieldLevel == 0))
                    {
                        control.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (controlType == FieldType.KeywordType)
                    {
                        control.Value = StringHelper.ReplaceChar(control.Value, '|', ' ');
                    }
                    if (((controlType == FieldType.TitleType) && (control.Settings.Count > 3)) && DataConverter.CBoolean(control.Settings[3]))
                    {
                        TitleType type5 = (TitleType) control.FindControl("EasyOne2007");
                        type5.PinyinTitle = this.m_ContentDataTable.Rows[0]["PinyinTitle"].ToString();
                    }
                }
            }
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

        private void SavePresentExp(DataTable dataTable, bool isAdd)
        {
            string userName = PEContext.Current.Admin.UserName;
            CommonModelInfo commonModelInfoById = null;
            if (!isAdd)
            {
                commonModelInfoById = ContentManage.GetCommonModelInfoById(BasePage.RequestInt32("GeneralID"));
                userName = commonModelInfoById.Inputer;
            }
            UserInfo usersByUserName = Users.GetUsersByUserName(userName);
            if (!usersByUserName.IsNull)
            {
                int statusFromDataTable = GetStatusFromDataTable(dataTable);
                UserPurviewInfo userPurview = usersByUserName.UserPurview;
                if (userPurview.IsNull)
                {
                    userPurview.MaxPublicInfoOneDay = -1;
                    userPurview.GetExp = 1;
                }
                if (userPurview.GetExp == 0)
                {
                    userPurview.GetExp = 1;
                }
                int num3 = EasyOne.Contents.Nodes.GetCacheNodeById(this.m_NodeId).Settings.PresentExp * userPurview.GetExp;
                if (isAdd)
                {
                    int generalId = GetGeneralId(dataTable);
                    if (statusFromDataTable == 0x63)
                    {
                        usersByUserName.UserExp += num3;
                        usersByUserName.PassedItems++;
                    }
                    usersByUserName.PostItems++;
                }
                else
                {
                    if ((statusFromDataTable == 0x63) && (commonModelInfoById.Status < 0x63))
                    {
                        usersByUserName.UserExp += num3;
                        usersByUserName.PassedItems++;
                    }
                    if ((commonModelInfoById.Status == 0x63) && (statusFromDataTable < 0x63))
                    {
                        usersByUserName.UserExp -= num3;
                        usersByUserName.PassedItems--;
                    }
                    if (statusFromDataTable == -2)
                    {
                        usersByUserName.RejectItems++;
                    }
                    if (statusFromDataTable == -3)
                    {
                        usersByUserName.DelItems++;
                    }
                }
                Users.Update(usersByUserName);
            }
        }

        private void Update()
        {
            DataTable dataTableFromRepeater = this.GetDataTableFromRepeater();
            int generalId = BasePage.RequestInt32("GeneralID");
            this.SavePresentExp(dataTableFromRepeater, false);
            if (ContentManage.Update(generalId, ContentManage.GetNewContentData(dataTableFromRepeater)))
            {
                ModelInfo modelInfoById = ModelManager.GetModelInfoById(this.m_ModelId);
                this.AddPermissionAndCharge(dataTableFromRepeater, modelInfoById.EnableCharge);
                if (modelInfoById.EnableSignIn)
                {
                    this.UpdateSignin(generalId, dataTableFromRepeater);
                }
                if (((SignInType) Enum.Parse(typeof(SignInType), this.DrpSigninType.SelectedValue)) != SignInType.EnableSignInPrivate)
                {
                    HtmlContent.CreateHtml(dataTableFromRepeater);
                }
                this.UpdateKeywordsToTable(generalId, dataTableFromRepeater);
                if (modelInfoById.EnbaleVote)
                {
                    this.Vote.Add(generalId);
                }
                SiteCache.Remove("CK_Page_Category_" + this.m_NodeId.ToString());
                BasePage.ResponseRedirect(AdminPage.AppendSecurityCode("ContentShowSuccess.aspx?Action=Modify&GeneralID=" + generalId.ToString() + "&NodeID=" + this.m_NodeId.ToString() + "&ModelID=" + this.m_ModelId.ToString() + "&ContentFieldName=" + this.m_ContentFieldName));
            }
            else
            {
                AdminPage.WriteErrMsg("修改失败！");
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

        private void UpdateSignin(int generalId, DataTable dataTable)
        {
            if (!string.IsNullOrEmpty(this.TxtSigninUser.Text))
            {
                string str = dataTable.Select("FieldName = 'title'")[0]["FieldValue"].ToString();
                SignInContentInfo signInContentInfo = new SignInContentInfo();
                signInContentInfo.GeneralId = generalId;
                signInContentInfo.EndTime = this.DpkEndTime.Date;
                signInContentInfo.Priority = DataConverter.CLng(this.TxtPriority.Text);
                signInContentInfo.SignInType = (SignInType) Enum.Parse(typeof(SignInType), this.DrpSigninType.SelectedValue);
                signInContentInfo.Title = str;
                SignInContent.UpdateSignInContent(signInContentInfo);
                SignInContent.UpdateContentSignInType(generalId, signInContentInfo.SignInType);
                SignInLog.Update(generalId, this.TxtSigninUser.Text);
            }
            else
            {
                SignInContent.Delete(generalId);
                SignInLog.Delete(generalId);
                SignInContent.UpdateContentSignInType(generalId, SignInType.DisableSignIn);
            }
        }
    }
}

