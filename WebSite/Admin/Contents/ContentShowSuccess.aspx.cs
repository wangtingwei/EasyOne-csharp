namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.CommonModel;
    using EasyOne.Model.Contents;
    using EasyOne.Model.WorkFlow;
    using EasyOne.Web.UI;
    using EasyOne.WorkFlows;
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class ContentShowSuccess : AdminPage
    {
        protected string m_GeneralId;
        protected string m_ModelId;
        protected string m_NodeId;

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = BasePage.RequestString("Action");
            int generalId = BasePage.RequestInt32("GeneralID");
            this.m_GeneralId = generalId.ToString();
            this.m_NodeId = BasePage.RequestInt32("NodeID").ToString();
            this.m_ModelId = BasePage.RequestInt32("ModelID").ToString();
            string str2 = BasePage.RequestString("ContentFieldName");
            if (generalId <= 0)
            {
                AdminPage.WriteErrMsg("<li>参数不全，请返回！</li>");
            }
            CommonModelInfo commonModelInfoById = ContentManage.GetCommonModelInfoById(generalId);
            this.m_NodeId = commonModelInfoById.NodeId.ToString();
            NodeInfo cacheNodeById = EasyOne.Contents.Nodes.GetCacheNodeById(commonModelInfoById.NodeId);
            DataTable contentDataById = ContentManage.GetContentDataById(generalId);
            if ((contentDataById == null) || (contentDataById.Rows.Count == 0))
            {
                AdminPage.WriteErrMsg("<li>指定项目不存在！</li>");
            }
            if (cacheNodeById.IsNull || commonModelInfoById.IsNull)
            {
                AdminPage.WriteErrMsg("<li>请检查栏目或项目是否已被删除！</li>");
            }
            string str3 = str;
            if (str3 != null)
            {
                if (!(str3 == "Add"))
                {
                    if (str3 == "Modify")
                    {
                        this.LblType.Text = "修改项目成功";
                    }
                }
                else
                {
                    this.LblType.Text = "添加项目成功";
                }
            }
            this.LblNode.Text = cacheNodeById.NodeName;
            this.LblTitle.Text = commonModelInfoById.Title;
            this.Author.Style.Add("display", "none");
            this.CopyFrom.Style.Add("display", "none");
            this.Keylord.Style.Add("display", "none");
            StringBuilder builder = new StringBuilder();
            builder.Append("<a href=\"");
            string tableName = commonModelInfoById.TableName;
            if (tableName != null)
            {
                if (!(tableName == "PE_U_Article"))
                {
                    if (tableName == "PE_U_Photo")
                    {
                        builder.Append("PhotoPreview.aspx");
                        goto Label_0383;
                    }
                    if (tableName == "PE_U_Soft")
                    {
                        builder.Append("SoftPreview.aspx");
                        goto Label_0383;
                    }
                }
                else
                {
                    this.Author.Style.Add("display", "");
                    this.CopyFrom.Style.Add("display", "");
                    this.Keylord.Style.Add("display", "");
                    foreach (FieldInfo info3 in ModelManager.GetFieldListByModelId(commonModelInfoById.ModelId))
                    {
                        string fieldName = info3.FieldName;
                        if (fieldName != null)
                        {
                            if (!(fieldName == "Author"))
                            {
                                if (fieldName == "CopyFrom")
                                {
                                    goto Label_02BD;
                                }
                                if (fieldName == "Keyword")
                                {
                                    goto Label_02E6;
                                }
                            }
                            else
                            {
                                this.LblAuthor.Text = contentDataById.Rows[0]["Author"].ToString();
                            }
                        }
                        continue;
                    Label_02BD:
                        this.LblCopyFrom.Text = contentDataById.Rows[0]["CopyFrom"].ToString();
                        continue;
                    Label_02E6:
                        if (contentDataById.Rows[0]["Keyword"] != null)
                        {
                            this.LblKeylord.Text = StringHelper.ReplaceChar(contentDataById.Rows[0]["Keyword"].ToString(), '|', ' ');
                        }
                    }
                    builder.Append("ArticlePreview.aspx");
                    goto Label_0383;
                }
            }
            builder.Append("ContentView.aspx");
        Label_0383:;
            builder.Append("?GeneralID=" + this.m_GeneralId + "&NodeID=" + this.m_NodeId + "&ModelID=" + this.m_ModelId);
            builder.Append("\">查看内容</a>");
            this.ShowContentPreview.Text = builder.ToString();
            switch (commonModelInfoById.Status)
            {
                case -3:
                    this.LblStatus.Text = "删除";
                    break;

                case -2:
                    this.LblStatus.Text = "退稿";
                    break;

                case -1:
                    this.LblStatus.Text = "草稿";
                    break;

                case 0:
                    this.LblStatus.Text = "待审核";
                    break;

                case 0x63:
                    this.LblStatus.Text = "终审通过";
                    break;

                default:
                    foreach (StatusInfo info4 in Status.GetStatusList())
                    {
                        if (info4.StatusId == commonModelInfoById.Status)
                        {
                            this.LblStatus.Text = info4.StatusName;
                        }
                        break;
                    }
                    break;
            }
            if (!string.IsNullOrEmpty(str2))
            {
                this.SavePemotePic(str2, commonModelInfoById, cacheNodeById, contentDataById);
            }
        }

        private void SavePemotePic(string contentFieldName, CommonModelInfo commonModelInfo, NodeInfo nodeInfo, DataTable content)
        {
            string[] strArray = new string[0];
            if (!SiteConfig.SiteOption.EnableUploadFiles)
            {
                base.Response.Write("权限错误：你当前的系统配置没有开启上传功能，所以不能获取远程图片。");
            }
            else if (nodeInfo.IsNull)
            {
                base.Response.Write("获取错误：缺少栏目ID，所以不能得到保存地址。");
            }
            else
            {
                if (contentFieldName.IndexOf('$') > 0)
                {
                    strArray = contentFieldName.Split(new string[] { "$" }, StringSplitOptions.None);
                    for (int i = 0; i < strArray.Length; i++)
                    {
                        content = ContentManage.SavePemotePic(content, nodeInfo, strArray[i]);
                    }
                }
                else
                {
                    content = ContentManage.SavePemotePic(content, nodeInfo, contentFieldName);
                }
                ContentManage.UpdateField(commonModelInfo.ItemId, commonModelInfo.TableName, contentFieldName, content);
            }
        }
    }
}

