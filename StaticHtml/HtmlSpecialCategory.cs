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

    public sealed class HtmlSpecialCategory : HtmlAbstract
    {
        private bool m_IsCreateSpecial;
        private string m_SpecialCategoryIdArray;

        private static bool CheckIsNeedCreatHtml(SpecialCategoryInfo categoryInfo)
        {
            return categoryInfo.IsCreateHtml;
        }

        private bool CreateAndCheckFolderPermission(SpecialCategoryInfo categoryInfo)
        {
            string file = VirtualPathUtility.AppendTrailingSlash(this.PhysicalApplicationPath) + VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.CreateHtmlPath) + categoryInfo.SpecialCategoryDir;
            if (!FileSystemObject.IsExist(file, FsoMethod.Folder))
            {
                try
                {
                    FileSystemObject.Create(file, FsoMethod.Folder);
                }
                catch
                {
                    string errMsg = string.Concat(new object[] { "专题类别ID为：", categoryInfo.SpecialCategoryId, "  ", categoryInfo.SpecialCategoryName, " 生成失败！ 失败原因：请检查服务器是否给网站", SiteConfig.SiteOption.CreateHtmlPath, "文件夹写入权限！" });
                    this.ErrorLog(errMsg);
                    return false;
                }
            }
            return true;
        }

        private void CreateSpecialCategory()
        {
            IList<SpecialCategoryInfo> specialCategoryList = Special.GetSpecialCategoryList(this.m_SpecialCategoryIdArray);
            this.CreateCount = specialCategoryList.Count;
            this.CreateStartTime = DateTime.Now;
            this.CreateMessage = "";
            foreach (SpecialCategoryInfo info in specialCategoryList)
            {
                if (CheckIsNeedCreatHtml(info))
                {
                    if (!this.CreateAndCheckFolderPermission(info))
                    {
                        this.CreateMessage = "<li>专题类别ID：" + info.SpecialCategoryId.ToString() + "&nbsp;&nbsp;专题类别名： " + info.SpecialCategoryName + " 生成失败！ 失败原因：请检查服务器是否给网站" + SiteConfig.SiteOption.CreateHtmlPath + info.SpecialCategoryDir + "文件夹写入权限！</li>";
                        break;
                    }
                    this.CreateSpecialsCategoryHtml(info);
                    this.CreateCompleted++;
                    continue;
                }
                this.CreateMessage = "<li><font color='red'>专题类别ID：" + info.SpecialCategoryId.ToString() + "&nbsp;&nbsp;专题类别名： " + info.SpecialCategoryName + "因设置不需要生成而跳过生成！</font></li>" + this.CreateMessage;
            }
            this.CreateMessage = "全部生成完成！" + this.CreateMessage;
            this.CreateCompleted = this.CreateCount;
            this.CreateEndTime = DateTime.Now;
        }

        private void CreateSpecialsCategoryHtml(SpecialCategoryInfo specialCategoryInfo)
        {
            TemplateInfo templateInfo = new TemplateInfo();
            NameValueCollection values = new NameValueCollection();
            values.Add("id", specialCategoryInfo.SpecialCategoryId.ToString());
            templateInfo.QueryList = values;
            templateInfo.RootPath = this.PhysicalApplicationPath;
            templateInfo.SiteUrl = this.SiteUrl;
            templateInfo.TemplateContent = Template.GetTemplateContent(specialCategoryInfo.SpecialTemplatePath, this.PhysicalApplicationPath);
            templateInfo.CurrentPage = 1;
            templateInfo.PageName = SiteConfig.SiteInfo.VirtualPath + VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.CreateHtmlPath) + specialCategoryInfo.CategoryHtmlPageName;
            string str = VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.CreateHtmlPath) + specialCategoryInfo.CategoryHtmlPageName;
            HtmlAbstract.AddHeardRunatServer(templateInfo, templateInfo.PageName);
            try
            {
                FileSystemObject.WriteFile(VirtualPathUtility.AppendTrailingSlash(this.PhysicalApplicationPath) + str, TemplateTransform.GetHtml(templateInfo).TemplateContent);
                string str2 = (VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteInfo.VirtualPath) + str).Replace("//", "/");
                this.CreateMessage = string.Concat(new object[] { "<li>成功生成第", this.CreateCompleted + 1, "个专题类别页，专题类别名：<a target=_blank href=", str2, ">", specialCategoryInfo.SpecialCategoryName, "</a></li>", this.CreateMessage });
            }
            catch
            {
                this.CreateMessage = string.Concat(new object[] { "<li>第<font color='red'><b>", this.CreateCompleted + 1, "</b>个专题类别页生成失败</font>&nbsp;&nbsp;专题类别名：", specialCategoryInfo.SpecialCategoryName, "&nbsp;&nbsp;标签解析出现异常，跳过生成</li>", this.CreateMessage });
            }
        }

        public override void Work()
        {
            this.CreateMessage = "正在准备生成．．．．．．";
            try
            {
                this.CreateSpecialCategory();
            }
            finally
            {
                if (this.CreateThread != null)
                {
                    this.CreateThread.Abort();
                }
            }
        }

        public bool IsCreateSpecial
        {
            get
            {
                return this.m_IsCreateSpecial;
            }
            set
            {
                this.m_IsCreateSpecial = value;
            }
        }

        public string SpecialCategoryIdArray
        {
            get
            {
                return this.m_SpecialCategoryIdArray;
            }
            set
            {
                this.m_SpecialCategoryIdArray = value;
            }
        }
    }
}

