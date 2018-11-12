namespace EasyOne.StaticHtml
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Enumerations;
    using EasyOne.Model.Contents;
    using EasyOne.Model.TemplateProc;
    using EasyOne.Templates;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Threading;
    using System.Web;

    public sealed class HtmlCategory : HtmlAbstract
    {
        private string m_NodeIdArray;

        private static bool CheckIsNeedCreatHtml(NodeInfo nodeInfo)
        {
            return (nodeInfo.IsCreateListPage && (nodeInfo.PurviewType == 0));
        }

        private bool CreateAndCheckFolderPermission(NodeInfo nodeInfo)
        {
            string file = VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.CreateHtmlPath) + nodeInfo.ParentDir + nodeInfo.NodeDir;
            file = VirtualPathUtility.AppendTrailingSlash(this.PhysicalApplicationPath) + file;
            if (!FileSystemObject.IsExist(file, FsoMethod.Folder))
            {
                try
                {
                    FileSystemObject.Create(file, FsoMethod.Folder);
                }
                catch
                {
                    string errMsg = "栏目ID：" + nodeInfo.NodeId.ToString() + "  " + nodeInfo.NodeName + " 生成失败！ 失败原因：请检查服务器是否给网站" + SiteConfig.SiteOption.CreateHtmlPath + "文件夹写入权限！";
                    this.ErrorLog(errMsg);
                    return false;
                }
            }
            return true;
        }

        public void CreateNode()
        {
            IList<NodeInfo> nodesList = Nodes.GetNodesList(this.m_NodeIdArray);
            this.CreateCount = nodesList.Count;
            this.CreateStartTime = DateTime.Now;
            this.CreateMessage = "";
            foreach (NodeInfo info in nodesList)
            {
                if (CheckIsNeedCreatHtml(info))
                {
                    if (!this.CreateAndCheckFolderPermission(info))
                    {
                        this.CreateMessage = "<li>栏目ID：" + info.NodeId.ToString() + "&nbsp;&nbsp;栏目名： " + info.NodeName + " 生成失败！ 失败原因：请检查服务器是否给网站" + SiteConfig.SiteOption.CreateHtmlPath + "文件夹写入权限！</li>";
                        break;
                    }
                    this.CreateNodesHtml(info);
                }
                else
                {
                    this.CreateMessage = "<li><font color='red'>栏目ID：" + info.NodeId.ToString() + "&nbsp;&nbsp;栏目名： " + info.NodeName + "因设置了访问权限而跳过生成！</font></li>" + this.CreateMessage;
                }
                this.CreateCompleted++;
            }
            this.CreateMessage = "全部生成完成！" + this.CreateMessage;
            this.CreateCompleted = this.CreateCount;
            this.CreateEndTime = DateTime.Now;
        }

        private void CreateNodeDefalutPageHtml(NodeInfo nodeInfo, TemplateInfo templateInfo)
        {
            string path = string.Empty;
            string str2 = SiteConfig.SiteInfo.VirtualPath + VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.CreateHtmlPath);
            if (string.IsNullOrEmpty(nodeInfo.DefaultTemplateFile))
            {
                path = nodeInfo.ContainChildTemplateFile;
                templateInfo.PageName = str2 + nodeInfo.ListHtmlPageName("{$pageid/}");
            }
            else
            {
                templateInfo.PageName = str2 + nodeInfo.ListHtmlPageName("0");
                path = nodeInfo.DefaultTemplateFile;
            }
            try
            {
                templateInfo.CurrentPage = 1;
                templateInfo.TemplateContent = Template.GetTemplateContent(path, this.PhysicalApplicationPath);
                TemplateTransform.GetHtml(templateInfo);
            }
            catch
            {
                this.CreateMessage = string.Concat(new object[] { "<li>第<font color='red'><b>", this.CreateCompleted + 1, "</b>条信息生成失败</font>&nbsp;&nbsp;栏目名：", nodeInfo.NodeName, "&nbsp;&nbsp;标签解析出现异常</li>", this.CreateMessage });
            }
            finally
            {
                string str3;
                if (nodeInfo.NodeId == -2)
                {
                    str3 = nodeInfo.ListHtmlPageName("0");
                }
                else
                {
                    str3 = VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.CreateHtmlPath) + nodeInfo.ListHtmlPageName("0");
                }
                FileSystemObject.WriteFile(VirtualPathUtility.AppendTrailingSlash(this.PhysicalApplicationPath) + str3, templateInfo.TemplateContent);
                string str4 = (VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteInfo.VirtualPath) + str3).Replace("//", "/");
                this.CreateMessage = string.Concat(new object[] { "<li>成功生成第", this.CreateCompleted + 1, "个栏目的栏目首页，栏目名：<a target=_blank href=", str4, ">", nodeInfo.NodeName, "</a></li>", this.CreateMessage });
            }
        }

        private void CreateNodeListPageHtml(NodeInfo nodeInfo, TemplateInfo templateInfo)
        {
            if (nodeInfo.NodeType != NodeType.Single)
            {
                string str = string.Empty;
                string str2 = string.Empty;
                string str3 = SiteConfig.SiteInfo.VirtualPath + VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.CreateHtmlPath);
                try
                {
                    templateInfo.PageName = str3 + nodeInfo.ListHtmlPageName("{$pageid/}");
                    templateInfo.TemplateContent = Template.GetTemplateContent(nodeInfo.ContainChildTemplateFile, this.PhysicalApplicationPath);
                    TemplateTransform.GetHtml(templateInfo);
                }
                catch
                {
                }
                finally
                {
                    str = VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.CreateHtmlPath) + nodeInfo.ListHtmlPageName("1");
                    FileSystemObject.WriteFile(VirtualPathUtility.AppendTrailingSlash(this.PhysicalApplicationPath) + str, templateInfo.TemplateContent);
                    str2 = (VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteInfo.VirtualPath) + str).Replace("//", "/");
                    this.CreateMessage = string.Concat(new object[] { "<li>成功生成第", this.CreateCompleted + 1, "个栏目的栏目列表页，栏目名：<a target=_blank href=", str2, ">", nodeInfo.NodeName, "</a></li>", this.CreateMessage });
                }
                int pageNum = templateInfo.PageNum;
                int num2 = 1;
                for (num2 = 1; num2 < pageNum; num2++)
                {
                    try
                    {
                        if ((num2 % 7) == 0)
                        {
                            this.CreateMessage = "为了缓解服务器压力，暂停一秒生成";
                            Thread.Sleep(0x3e8);
                            this.CreateMessage = "";
                        }
                        templateInfo.TemplateContent = Template.GetTemplateContent(nodeInfo.ContainChildTemplateFile, this.PhysicalApplicationPath);
                        NameValueCollection values = new NameValueCollection();
                        values.Add("id", nodeInfo.NodeId.ToString());
                        values.Add("page", (num2 + 1).ToString());
                        templateInfo.QueryList = values;
                        templateInfo.PageName = str3 + nodeInfo.ListHtmlPageName("{$pageid/}");
                        templateInfo.CurrentPage = num2 + 1;
                        TemplateTransform.GetHtml(templateInfo);
                    }
                    catch
                    {
                        this.CreateMessage = string.Concat(new object[] { "<li>第<font color='red'><b>", this.CreateCompleted + 1, "</b>条信息生成失败</font>&nbsp;&nbsp;栏目名：", nodeInfo.NodeName, "&nbsp;&nbsp;标签解析出现异常，跳过生成</li>", this.CreateMessage });
                    }
                    finally
                    {
                        int num5 = num2 + 1;
                        str = VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.CreateHtmlPath) + nodeInfo.ListHtmlPageName(num5.ToString());
                        FileSystemObject.WriteFile(VirtualPathUtility.AppendTrailingSlash(this.PhysicalApplicationPath) + str, templateInfo.TemplateContent);
                        str2 = (VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteInfo.VirtualPath) + str).Replace("//", "/");
                        this.CreateMessage = string.Concat(new object[] { "<li>成功生成第", this.CreateCompleted + 1, "个栏目的栏目列表页的第", num2, "个分页，栏目名：<a target=_blank href=", str2, ">", nodeInfo.NodeName, "</a></li>", this.CreateMessage });
                    }
                }
            }
        }

        public void CreateNodesHtml(NodeInfo nodeInfo)
        {
            TemplateInfo templateInfo = new TemplateInfo();
            NameValueCollection values = new NameValueCollection();
            values.Add("id", nodeInfo.NodeId.ToString());
            values.Add("page", "1");
            templateInfo.QueryList = values;
            templateInfo.RootPath = this.PhysicalApplicationPath;
            templateInfo.SiteUrl = this.SiteUrl;
            templateInfo.IsDynamicPage = false;
            templateInfo.PageType = 1;
            this.CreateNodeDefalutPageHtml(nodeInfo, templateInfo);
            templateInfo = new TemplateInfo();
            values = new NameValueCollection();
            values.Add("id", nodeInfo.NodeId.ToString());
            values.Add("page", "1");
            templateInfo.QueryList = values;
            templateInfo.RootPath = this.PhysicalApplicationPath;
            templateInfo.SiteUrl = this.SiteUrl;
            templateInfo.IsDynamicPage = false;
            templateInfo.PageType = 1;
            this.CreateNodeListPageHtml(nodeInfo, templateInfo);
        }

        public override void Work()
        {
            this.CreateMessage = "正在准备生成．．．．．．";
            try
            {
                lock (this)
                {
                    this.CreateNode();
                }
            }
            catch
            {
                this.CreateCompleted = this.CreateCount;
            }
            finally
            {
                if ((this.CreateThread != null) && (this.CreateCompleted == this.CreateCount))
                {
                    this.CreateThread.Abort();
                }
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

