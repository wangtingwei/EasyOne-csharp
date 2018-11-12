namespace EasyOne.StaticHtml
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Model.Contents;
    using EasyOne.Model.TemplateProc;
    using EasyOne.Templates;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Web;

    public sealed class HtmlSpecial : HtmlAbstract
    {
        private string m_SpecialIdArray;

        private static bool CheckIsNeedCreatHtml(SpecialInfo specialInfo)
        {
            return specialInfo.IsCreateListPage;
        }

        private bool CreateAndCheckFolderPermission(SpecialInfo specialInfo)
        {
            string file = VirtualPathUtility.AppendTrailingSlash(this.PhysicalApplicationPath) + VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.CreateHtmlPath) + specialInfo.SpecialDir;
            if (!FileSystemObject.IsExist(file, FsoMethod.Folder))
            {
                try
                {
                    FileSystemObject.Create(file, FsoMethod.Folder);
                }
                catch
                {
                    string errMsg = "专题ID：" + specialInfo.SpecialId.ToString() + "  " + specialInfo.SpecialName + " 生成失败！ 失败原因：请检查服务器是否给网站" + SiteConfig.SiteOption.CreateHtmlPath + "文件夹写入权限！";
                    this.ErrorLog(errMsg);
                    return false;
                }
            }
            return true;
        }

        public void CreateSpecial()
        {
            IList<SpecialInfo> specialList = Special.GetSpecialList(this.m_SpecialIdArray);
            this.CreateCount = specialList.Count;
            this.CreateStartTime = DateTime.Now;
            this.CreateMessage = "";
            foreach (SpecialInfo info in specialList)
            {
                this.CreateMessage = "正在生成：" + info.SpecialName;
                if (CheckIsNeedCreatHtml(info))
                {
                    if (!this.CreateAndCheckFolderPermission(info))
                    {
                        this.CreateMessage = "<li>专题ID：" + info.SpecialId.ToString() + "&nbsp;&nbsp;专题名： " + info.SpecialName + " 生成失败！ 失败原因：请检查服务器是否给网站" + SiteConfig.SiteOption.CreateHtmlPath + info.SpecialDir + "文件夹写入权限！</li>";
                        break;
                    }
                    this.CreateSpecialsHtml(info);
                    this.CreateCompleted++;
                    continue;
                }
                this.CreateMessage = "<li><font color='red'>专题ID：" + info.SpecialId.ToString() + "&nbsp;&nbsp;专题名： " + info.SpecialName + "因设置不需要生成而跳过生成！</font></li>" + this.CreateMessage;
            }
            this.CreateMessage = "全部生成完成！" + this.CreateMessage;
            this.CreateCompleted = this.CreateCount;
            this.CreateEndTime = DateTime.Now;
        }

        public void CreateSpecialsHtml(SpecialInfo specialInfo)
        {
            TemplateInfo templateInfo = new TemplateInfo();
            NameValueCollection values = new NameValueCollection();
            values.Add("id", specialInfo.SpecialId.ToString());
            values.Add("page", "1");
            templateInfo.QueryList = values;
            templateInfo.CurrentPage = 1;
            templateInfo.RootPath = this.PhysicalApplicationPath;
            templateInfo.SiteUrl = this.SiteUrl;
            templateInfo.IsDynamicPage = false;
            templateInfo.PageType = 1;
            templateInfo.PageName = SiteConfig.SiteInfo.VirtualPath + SiteConfig.SiteOption.CreateHtmlPath + specialInfo.ListHtmlPagePath("{$pageid/}");
            templateInfo.TemplateContent = Template.GetTemplateContent(specialInfo.SpecialTemplatePath, this.PhysicalApplicationPath);
            try
            {
                TemplateTransform.GetHtml(templateInfo);
                HtmlAbstract.AddHeardRunatServer(templateInfo, templateInfo.PageName);
                string str = VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.CreateHtmlPath) + specialInfo.ListHtmlPagePath("");
                FileSystemObject.WriteFile(VirtualPathUtility.AppendTrailingSlash(this.PhysicalApplicationPath) + str, templateInfo.TemplateContent);
                string str2 = (VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteInfo.VirtualPath) + str).Replace("//", "/");
                this.CreateMessage = string.Concat(new object[] { "<li>成功生成第", this.CreateCompleted + 1, "个专题的专题首页，专题名：<a target=_blank href=", str2, ">", specialInfo.SpecialName, "</a></li>", this.CreateMessage });
            }
            catch
            {
                this.CreateMessage = string.Concat(new object[] { "<li>第<font color='red'><b>", this.CreateCompleted + 1, "</b>专题的专题列表页生成失败</font>&nbsp;&nbsp;专题名：", specialInfo.SpecialName, "&nbsp;&nbsp;标签解析出现异常，跳过生成</li>", this.CreateMessage });
            }
            int pageNum = templateInfo.PageNum;
            if (pageNum > 1)
            {
                for (int i = 1; i <= pageNum; i++)
                {
                    templateInfo.TemplateContent = Template.GetTemplateContent(specialInfo.SpecialTemplatePath, this.PhysicalApplicationPath);
                    values = new NameValueCollection();
                    values.Add("id", specialInfo.SpecialId.ToString());
                    values.Add("page", i.ToString());
                    templateInfo.QueryList = values;
                    templateInfo.CurrentPage = i;
                    templateInfo.SiteUrl = this.SiteUrl;
                    templateInfo.PageName = SiteConfig.SiteInfo.VirtualPath + SiteConfig.SiteOption.CreateHtmlPath + specialInfo.ListHtmlPagePath("{$pageid/}");
                    try
                    {
                        TemplateTransform.GetHtml(templateInfo);
                        HtmlAbstract.AddHeardRunatServer(templateInfo, templateInfo.PageName);
                        string str3 = VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.CreateHtmlPath) + specialInfo.ListHtmlPagePath(i.ToString());
                        FileSystemObject.WriteFile(VirtualPathUtility.AppendTrailingSlash(this.PhysicalApplicationPath) + str3, templateInfo.TemplateContent);
                        string str4 = (VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteInfo.VirtualPath) + str3).Replace("//", "/");
                        this.CreateMessage = string.Concat(new object[] { "<li>成功生成第", this.CreateCompleted + 1, "个专题的专题列表页的第", i, "个分页，专题名：<a target=_blank href=", str4, ">", specialInfo.SpecialName, "</a></li>", this.CreateMessage });
                    }
                    catch
                    {
                        this.CreateMessage = string.Concat(new object[] { "<li>第<font color='red'><b>", this.CreateCompleted + 1, "</b>个专题的专题列表页的第", i, "个分页生成失败</font>&nbsp;&nbsp;专题名：", specialInfo.SpecialName, "&nbsp;&nbsp;标签解析出现异常，跳过生成</li>", this.CreateMessage });
                    }
                }
            }
        }

        public override void Work()
        {
            this.CreateMessage = "正在准备生成．．．．．．";
            try
            {
                this.CreateSpecial();
            }
            finally
            {
                if (this.CreateThread != null)
                {
                    this.CreateThread.Abort();
                }
            }
        }

        public string SpecialIdArray
        {
            get
            {
                return this.m_SpecialIdArray;
            }
            set
            {
                this.m_SpecialIdArray = value;
            }
        }
    }
}

