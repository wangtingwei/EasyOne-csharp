namespace EasyOne.StaticHtml
{
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Enumerations;
    using EasyOne.Model.CommonModel;
    using EasyOne.Model.Contents;
    using EasyOne.Model.TemplateProc;
    using EasyOne.Templates;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Data;
    using System.Threading;
    using System.Web;

    public class HtmlContent : HtmlAbstract
    {
        private IList<CommonModelInfo> m_CommonList;
        private DateTime m_ContentBeginTime;
        private DateTime m_ContentEndTime;
        private string m_ContentGeneralIdArray;
        private int m_ContentMaxId;
        private int m_ContentMinId;
        private bool m_EnableCreateIndexPage;
        private bool m_EnableCreateNodePage;
        private int m_LatestNumber;
        private string m_NodeIdArray;

        private static string ChargeTipsMessage(int generalId, int modelId)
        {
            ModelInfo cacheModelById = ModelManager.GetCacheModelById(modelId);
            return cacheModelById.ChargeTips.Replace("{$FileName}", SiteConfig.SiteInfo.VirtualPath + "Item").Replace("{$Id}", generalId.ToString()).Replace("{$ModelName}", cacheModelById.ModelName);
        }

        private void Create()
        {
            int startIndex = 0;
            int pageSize = 20;
            this.CreateStartTime = DateTime.Now;
            if ((base.CreateMethod == CreateContentType.CreateLatest) && (this.m_LatestNumber < pageSize))
            {
                pageSize = this.m_LatestNumber;
            }
            this.m_CommonList = this.GetCommonList(startIndex, pageSize);
            if (base.CreateMethod == CreateContentType.CreateByGeneralId)
            {
                pageSize = this.CreateCount;
            }
            int num3 = 1;
            if ((this.CreateCount % pageSize) == 0)
            {
                num3 = this.CreateCount / pageSize;
            }
            else
            {
                num3 = (this.CreateCount / pageSize) + 1;
            }
            this.CreateMessage = "分成" + num3.ToString() + "页生成，当前是第1页";
            this.CreateAllContent();
            for (int i = 1; i < num3; i++)
            {
                if (base.CreateMethod == CreateContentType.CreateByNotCreate)
                {
                    startIndex = 0;
                }
                else
                {
                    startIndex = pageSize * i;
                }
                this.m_CommonList = this.GetCommonList(startIndex, pageSize);
                string[] strArray = new string[] { "分成", num3.ToString(), "页生成，当前是第", (i + 1).ToString(), "页" };
                this.CreateMessage = string.Concat(strArray);
                this.CreateAllContent();
                if ((i % 20) == 0)
                {
                    this.CreateMessage = "为了缓解服务器压力，暂停一秒生成";
                    Thread.Sleep(0x3e8);
                }
            }
            this.CreateMessage = "全部生成完成！" + this.CreateMessage;
            this.CreateCompleted = this.CreateCount;
            this.CreateEndTime = DateTime.Now;
        }

        private void CreateAllContent()
        {
            foreach (CommonModelInfo info in this.m_CommonList)
            {
                if (PermissionContent.GetContentPermissionInfoById(info.GeneralId).PermissionType > 0)
                {
                    this.CreateMessage = "<li><font color='red'>ID号为：" + info.GeneralId.ToString() + "的项目因为设置了游客不能查看，所以没有生成。</font></li>" + this.CreateMessage;
                }
                else
                {
                    this.CreateContentHtml(info);
                }
                this.CreateCompleted++;
            }
        }

        private static void CreateContenAndNode(NodeInfo nodeInfo, string generalId)
        {
            if (nodeInfo.IsCreateListPage)
            {
                HtmlCategory category = new HtmlCategory();
                category.NodeIdArray = nodeInfo.NodeId.ToString();
                category.CommonCreateHtml();
            }
            if (nodeInfo.IsCreateContentPage)
            {
                CreateContent(nodeInfo, generalId);
            }
        }

        private static void CreateContenAndNodeAndParentNode(NodeInfo nodeInfo, string generalIds)
        {
            CreateContenAndNode(nodeInfo, generalIds);
            if (nodeInfo.ParentId > 0)
            {
                HtmlCategory category = new HtmlCategory();
                category.NodeIdArray = nodeInfo.ParentPath;
                category.CommonCreateHtml();
            }
        }

        private static void CreateContent(NodeInfo nodeInfo, string generalId)
        {
            if (nodeInfo.IsCreateContentPage)
            {
                HtmlContent content = new HtmlContent();
                content.CreateMethod = CreateContentType.CreateByGeneralId;
                content.NodeIdArray = string.Empty;
                content.ContentGeneralIdArray = generalId;
                content.CommonCreateHtml();
            }
        }

        private static void CreateContentAndNodeAndParentNodeAndSpecial(NodeInfo nodeInfo, DataTable dataTable, string generalIds)
        {
            string filterExpression = "FieldLevel=0 AND FieldName ='SpecialId'";
            DataRow[] rowArray = dataTable.Select(filterExpression);
            if (rowArray.Length > 0)
            {
                int specialId = DataConverter.CLng(rowArray[0]["FieldValue"].ToString());
                if (Special.GetSpecialInfoById(specialId).IsCreateListPage)
                {
                    HtmlSpecial special = new HtmlSpecial();
                    special.SpecialIdArray = specialId.ToString();
                    special.CommonCreateHtml();
                }
            }
            CreateContenAndNodeAndParentNode(nodeInfo, generalIds);
        }

        private static void CreateContentAndRelateNode(NodeInfo nodeInfo, DataTable dataTable, string generalIds)
        {
            if (!string.IsNullOrEmpty(nodeInfo.RelateNode))
            {
                HtmlCategory category = new HtmlCategory();
                category.NodeIdArray = nodeInfo.RelateNode;
                category.CommonCreateHtml();
            }
            if (!string.IsNullOrEmpty(nodeInfo.RelateSpecial))
            {
                HtmlSpecial special = new HtmlSpecial();
                special.SpecialIdArray = nodeInfo.RelateSpecial;
                special.CommonCreateHtml();
            }
            CreateContentAndNodeAndParentNodeAndSpecial(nodeInfo, dataTable, generalIds);
        }

        public void CreateContentHtml(CommonModelInfo commonModelInfo)
        {
            NodeInfo cacheNodeById = EasyOne.Contents.Nodes.GetCacheNodeById(commonModelInfo.NodeId);
            if (cacheNodeById.IsCreateContentPage && (cacheNodeById.PurviewType <= 0))
            {
                TemplateInfo templateInfo = new TemplateInfo();
                NameValueCollection values = new NameValueCollection();
                values.Add("id", commonModelInfo.GeneralId.ToString());
                values.Add("page", "1");
                templateInfo.QueryList = values;
                templateInfo.CurrentPage = 1;
                templateInfo.RootPath = this.PhysicalApplicationPath;
                templateInfo.SiteUrl = this.SiteUrl;
                templateInfo.IsDynamicPage = false;
                templateInfo.PageType = 0;
                templateInfo.PageName = this.TemplatePageName(commonModelInfo, cacheNodeById);
                templateInfo.TemplateContent = Template.GetTemplateContent(ItemTemplateFilePath(commonModelInfo), this.PhysicalApplicationPath);
                int pageNum = 0;
                try
                {
                    TemplateTransform.GetHtml(templateInfo);
                }
                catch
                {
                    this.CreateMessage = string.Concat(new object[] { "<li>第<font color='red'><b>", this.CreateCompleted + 1, "</b></font>条信息生成失败。&nbsp;&nbsp;ID：", commonModelInfo.GeneralId.ToString(), "&nbsp;&nbsp;标题：<a target=_blank href=#>", commonModelInfo.Title, "</a>内容解析出现异常，跳过生成</li>", this.CreateMessage });
                }
                finally
                {
                    string str = string.Empty;
                    string pageName = string.Empty;
                    pageNum = templateInfo.PageNum;
                    HtmlAbstract.AddHeardRunatServer(templateInfo, pageName);
                    if (templateInfo.TemplateContent.IndexOf("$$$EasyOne.ChargeTips$$$", StringComparison.Ordinal) > 0)
                    {
                        string newValue = ChargeTipsMessage(commonModelInfo.GeneralId, commonModelInfo.ModelId);
                        templateInfo.TemplateContent = templateInfo.TemplateContent.Replace("$$$EasyOne.ChargeTips$$$", newValue);
                        pageNum = 0;
                    }
                    pageName = ContentManage.ContentHtmlName(commonModelInfo, cacheNodeById, 0);
                    str = VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.CreateHtmlPath) + pageName;
                    FileSystemObject.WriteFile(VirtualPathUtility.AppendTrailingSlash(this.PhysicalApplicationPath) + str, templateInfo.TemplateContent);
                    string str4 = (VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteInfo.VirtualPath) + str).Replace("//", "/");
                    this.CreateMessage = string.Concat(new object[] { "<li>成功生成第<font color='blue'><b>", this.CreateCompleted + 1, "</b></font>条信息。&nbsp;&nbsp;ID：", commonModelInfo.GeneralId.ToString(), "&nbsp;&nbsp;标题：<a target=_blank href=", str4, ">", commonModelInfo.Title, "</a></li>", this.CreateMessage });
                }
                if (pageNum > 1)
                {
                    for (int i = 1; i < pageNum; i++)
                    {
                        templateInfo.TemplateContent = Template.GetTemplateContent(ItemTemplateFilePath(commonModelInfo), this.PhysicalApplicationPath);
                        values = new NameValueCollection();
                        values.Add("id", commonModelInfo.GeneralId.ToString());
                        values.Add("page", (i + 1).ToString());
                        templateInfo.QueryList = values;
                        templateInfo.PageName = this.TemplatePageName(commonModelInfo, cacheNodeById);
                        templateInfo.CurrentPage = i + 1;
                        TemplateTransform.GetHtml(templateInfo);
                        string str5 = ContentManage.ContentHtmlName(commonModelInfo, cacheNodeById, i + 1);
                        HtmlAbstract.AddHeardRunatServer(templateInfo, str5);
                        string str6 = VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.CreateHtmlPath) + str5;
                        FileSystemObject.WriteFile(VirtualPathUtility.AppendTrailingSlash(this.PhysicalApplicationPath) + str6, templateInfo.TemplateContent);
                        string str7 = (VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteInfo.VirtualPath) + str6).Replace("//", "/");
                        this.CreateMessage = string.Concat(new object[] { "<li>成功生成第<font color='blue'><b>", this.CreateCompleted + 1, "</b></font>篇文章的第<font color='red'><b>", i, "</b></font>个分页。。&nbsp;&nbsp;ID：", commonModelInfo.GeneralId.ToString(), "&nbsp;&nbsp;标题：<a target=_blank href=", str7, ">", commonModelInfo.Title, "</a></li>", this.CreateMessage });
                        if ((i % 20) == 0)
                        {
                            this.CreateMessage = "为了缓解服务器压力，暂停一秒生成";
                            Thread.Sleep(0x3e8);
                        }
                    }
                }
                ContentManage.UpdateCreateTime(commonModelInfo.GeneralId, new DateTime?(DateTime.Now));
                if (this.m_EnableCreateNodePage)
                {
                    HtmlCategory category = new HtmlCategory();
                    category.PhysicalApplicationPath = this.PhysicalApplicationPath;
                    category.SiteUrl = this.SiteUrl;
                    category.CreateNodesHtml(cacheNodeById);
                }
                if (this.m_EnableCreateIndexPage)
                {
                    HtmlCategory category2 = new HtmlCategory();
                    category2.PhysicalApplicationPath = this.PhysicalApplicationPath;
                    category2.SiteUrl = this.SiteUrl;
                    NodeInfo nodeInfo = EasyOne.Contents.Nodes.GetCacheNodeById(-2);
                    category2.CreateNodesHtml(nodeInfo);
                }
            }
        }

        public static void CreateHtml(DataTable dataTable)
        {
            string filterExpression = "FieldLevel=0 AND FieldName = 'Status'";
            DataRow[] rowArray = dataTable.Select(filterExpression);
            if ((rowArray.Length > 0) && (DataConverter.CLng(rowArray[0]["FieldValue"].ToString()) == 0x63))
            {
                string[] strArray = GetNodeId(dataTable).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                int generalId = GetGeneralId(dataTable);
                for (int i = 0; i < strArray.Length; i++)
                {
                    NodeInfo cacheNodeById = EasyOne.Contents.Nodes.GetCacheNodeById(DataConverter.CLng(strArray[i]));
                    string generalIdsByItemId = ContentManage.GetGeneralIdsByItemId(generalId);
                    switch (cacheNodeById.AutoCreateHtmlType)
                    {
                        case AutoCreateHtmlType.Content:
                            CreateContent(cacheNodeById, generalIdsByItemId);
                            break;

                        case AutoCreateHtmlType.ContentAndNode:
                            CreateContenAndNode(cacheNodeById, generalIdsByItemId);
                            break;

                        case AutoCreateHtmlType.ContentAndNodeAndParentNode:
                            CreateContenAndNodeAndParentNode(cacheNodeById, generalIdsByItemId);
                            break;

                        case AutoCreateHtmlType.ContentAndNodeAndParentNodeAndSpecial:
                            CreateContentAndNodeAndParentNodeAndSpecial(cacheNodeById, dataTable, generalIdsByItemId);
                            break;

                        case AutoCreateHtmlType.ContentAndRelatedNode:
                            CreateContentAndRelateNode(cacheNodeById, dataTable, generalIdsByItemId);
                            break;
                    }
                }
            }
        }

        private IList<CommonModelInfo> GetCommonList(int startIndex, int pageSize)
        {
            IList<CommonModelInfo> commonModelInfoList = new List<CommonModelInfo>();
            switch (base.CreateMethod)
            {
                case CreateContentType.CreateLatest:
                    commonModelInfoList = ContentManage.GetCommonModelInfoList(this.m_NodeIdArray, pageSize);
                    break;

                case CreateContentType.CreateByUpdateTime:
                    commonModelInfoList = ContentManage.GetCommonModelInfoList(this.m_NodeIdArray, this.m_ContentBeginTime, this.m_ContentEndTime, startIndex, pageSize);
                    break;

                case CreateContentType.CreateBetweenId:
                    commonModelInfoList = ContentManage.GetCommonModelInfoList(this.m_NodeIdArray, this.m_ContentMinId, this.m_ContentMaxId, startIndex, pageSize);
                    break;

                case CreateContentType.CreateByGeneralId:
                    commonModelInfoList = ContentManage.GetCommonModelInfoList(this.m_NodeIdArray, this.m_ContentGeneralIdArray);
                    break;

                case CreateContentType.CreateByNotCreate:
                    commonModelInfoList = ContentManage.CreateByNotCreate(this.m_NodeIdArray, startIndex, pageSize);
                    break;

                case CreateContentType.CreateAll:
                    commonModelInfoList = ContentManage.CreateAll(this.m_NodeIdArray, startIndex, pageSize);
                    break;

                case CreateContentType.CreateAuto:
                    commonModelInfoList = ContentManage.CreateAll(this.m_NodeIdArray, 0, 20);
                    break;
            }
            if (base.CreateMethod == CreateContentType.CreateByGeneralId)
            {
                this.CreateCount = commonModelInfoList.Count;
                return commonModelInfoList;
            }
            if (base.CreateMethod == CreateContentType.CreateLatest)
            {
                if (this.CreateCount == 0)
                {
                    this.CreateCount = this.m_LatestNumber;
                }
                return commonModelInfoList;
            }
            if (this.CreateCount == 0)
            {
                this.CreateCount = ContentManage.GetTotalOfCommonModelInfo(0, ContentSortType.DayHitsAsc, 0x63);
            }
            return commonModelInfoList;
        }

        private static int GetGeneralId(DataTable dataTable)
        {
            return DataConverter.CLng(dataTable.Select("FieldName = 'generalId'")[0]["FieldValue"].ToString());
        }

        private static string GetNodeId(DataTable dataTable)
        {
            DataRow[] rowArray = dataTable.Select("FieldLevel=0 AND FieldName='infoid'");
            string str = string.Empty;
            if (rowArray.Length > 0)
            {
                str = str + rowArray[0]["FieldValue"].ToString();
            }
            rowArray = dataTable.Select("FieldLevel=0 AND FieldName = 'NodeId'");
            if (rowArray.Length > 0)
            {
                str = str + "," + rowArray[0]["FieldValue"].ToString();
            }
            return str;
        }

        private static string ItemTemplateFilePath(CommonModelInfo commonModelInfo)
        {
            string defaultTemplateFile = "";
            if (!string.IsNullOrEmpty(commonModelInfo.TemplateFile))
            {
                return commonModelInfo.TemplateFile;
            }
            NodesModelTemplateRelationShipInfo nodesModelTemplateRelationShip = ModelManager.GetNodesModelTemplateRelationShip(commonModelInfo.NodeId, commonModelInfo.ModelId);
            if (!nodesModelTemplateRelationShip.IsNull)
            {
                if (!string.IsNullOrEmpty(nodesModelTemplateRelationShip.DefaultTemplateFile))
                {
                    defaultTemplateFile = nodesModelTemplateRelationShip.DefaultTemplateFile;
                }
                return defaultTemplateFile;
            }
            ModelInfo modelInfoById = ModelManager.GetModelInfoById(commonModelInfo.ModelId);
            if (!string.IsNullOrEmpty(modelInfoById.DefaultTemplateFile))
            {
                defaultTemplateFile = modelInfoById.DefaultTemplateFile;
            }
            return defaultTemplateFile;
        }

        private string TemplatePageName(CommonModelInfo commonModelInfo, NodeInfo node)
        {
            string contentPageHtmlRule = node.ContentPageHtmlRule;
            int index = contentPageHtmlRule.IndexOf(".");
            contentPageHtmlRule = (contentPageHtmlRule.Substring(0, index) + "_{$pageid/}" + contentPageHtmlRule.Substring(index, contentPageHtmlRule.Length - index)).ToLower().Replace("{$categorydir}", node.ParentDir + node.NodeDir).Replace("{$year}", commonModelInfo.InputTime.Year.ToString("0000")).Replace("{$month}", commonModelInfo.InputTime.Month.ToString("00")).Replace("{$day}", commonModelInfo.InputTime.Day.ToString("00")).Replace("{$pinyinoftitle}", commonModelInfo.PinyinTitle).Replace("{$time}", commonModelInfo.InputTime.Hour.ToString("00") + commonModelInfo.InputTime.Minute.ToString("00") + commonModelInfo.InputTime.Second.ToString("00"));
            contentPageHtmlRule = (this.SiteUrl + SiteConfig.SiteOption.CreateHtmlPath + contentPageHtmlRule.Replace("{$infoid}", commonModelInfo.GeneralId.ToString())).Replace("//", "/");
            return ("http://" + contentPageHtmlRule);
        }

        public override void Work()
        {
            this.CreateMessage = "正在准备生成.....";
            try
            {
                this.Create();
            }
            catch
            {
                this.CreateCompleted = this.CreateCount;
            }
            finally
            {
                if (this.CreateThread != null)
                {
                    this.CreateThread.Abort();
                }
            }
        }

        public DateTime ContentBeginTime
        {
            get
            {
                return this.m_ContentBeginTime;
            }
            set
            {
                this.m_ContentBeginTime = value;
            }
        }

        public DateTime ContentEndTime
        {
            get
            {
                return this.m_ContentEndTime;
            }
            set
            {
                this.m_ContentEndTime = value;
            }
        }

        public string ContentGeneralIdArray
        {
            get
            {
                return this.m_ContentGeneralIdArray;
            }
            set
            {
                this.m_ContentGeneralIdArray = value;
            }
        }

        public int ContentMaxId
        {
            get
            {
                return this.m_ContentMaxId;
            }
            set
            {
                this.m_ContentMaxId = value;
            }
        }

        public int ContentMinId
        {
            get
            {
                return this.m_ContentMinId;
            }
            set
            {
                this.m_ContentMinId = value;
            }
        }

        public bool EnableCreateIndexPage
        {
            get
            {
                return this.m_EnableCreateIndexPage;
            }
            set
            {
                this.m_EnableCreateIndexPage = value;
            }
        }

        public bool EnableCreateNodePage
        {
            get
            {
                return this.m_EnableCreateNodePage;
            }
            set
            {
                this.m_EnableCreateNodePage = value;
            }
        }

        public int LatestNumber
        {
            get
            {
                return this.m_LatestNumber;
            }
            set
            {
                this.m_LatestNumber = value;
            }
        }

        public string NodeIdArray
        {
            get
            {
                return this.m_NodeIdArray;
            }
            set
            {
                this.m_NodeIdArray = value;
            }
        }
    }
}

