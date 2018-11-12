namespace EasyOne.Templates
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Enumerations;
    using EasyOne.IDal.Templates;
    using EasyOne.Model.TemplateProc;
    using EasyOne.Model.Templates;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IO;
    using System.Web;
    using EasyOne.DalFactory;

    public sealed class IncludeFile
    {
        private static readonly IIncludeFile dal = DataAccess.CreateIncludeFile();

        private IncludeFile()
        {
        }

        public static bool Add(IncludeFileInfo includeFileInfo)
        {
            return dal.Add(includeFileInfo);
        }

        public static bool CreateAllIncludeFile()
        {
            foreach (IncludeFileInfo info in GetIncludeFileInfoList())
            {
                CreateIncludeFile(info);
            }
            return true;
        }

        private static bool CreateIncludeFile(IncludeFileInfo includeFileInfo)
        {
            if (includeFileInfo.IsNull)
            {
                return false;
            }
            string includeFilePath = SiteConfig.SiteOption.IncludeFilePath;
            includeFilePath = "~/" + includeFilePath + "/" + includeFileInfo.FileName;
            includeFilePath = HttpContext.Current.Request.MapPath(includeFilePath);
            TemplateInfo templateInfo = new TemplateInfo();
            templateInfo.QueryList = new NameValueCollection();
            templateInfo.PageName = "";
            templateInfo.TemplateContent = includeFileInfo.Template;
            templateInfo.RootPath = HttpContext.Current.Request.PhysicalApplicationPath;
            templateInfo.CurrentPage = 1;
            string templateContent = TemplateTransform.GetHtml(templateInfo).TemplateContent;
            if (includeFileInfo.IncludeType == IncludeType.JSWriteHtml)
            {
                templateContent = "document.write(\"" + DataSecurity.ConvertToJavaScript(templateContent) + "\")";
            }
            FileSystemObject.WriteFile(includeFilePath, templateContent);
            return true;
        }

        public static bool CreateIncludeFile(int id)
        {
            return CreateIncludeFile(GetIncludeFileInfoById(id));
        }

        public static bool CreateIncludeFileByAssociateType(AssociateType associateType)
        {
            foreach (IncludeFileInfo info in GetIncludeFileListByAssociateType(associateType))
            {
                CreateIncludeFile(info);
            }
            return true;
        }

        public static bool Delete(int id)
        {
            IncludeFileInfo includeFileInfoById = GetIncludeFileInfoById(id);
            if (!includeFileInfoById.IsNull && dal.Delete(id))
            {
                try
                {
                    string includeFilePath = SiteConfig.SiteOption.IncludeFilePath;
                    includeFilePath = "~/" + includeFilePath + "/" + includeFileInfoById.FileName;
                    FileInfo info2 = new FileInfo(HttpContext.Current.Request.MapPath(includeFilePath));
                    if (info2.Exists)
                    {
                        info2.Delete();
                    }
                    return true;
                }
                catch (IOException)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool ExistsFileName(string fileName)
        {
            return dal.ExistsFileName(fileName);
        }

        public static bool ExistsName(string name)
        {
            return dal.ExistsName(name);
        }

        public static IncludeFileInfo GetIncludeFileInfoById(int id)
        {
            return dal.GetIncludeFileInfoById(id);
        }

        public static IList<IncludeFileInfo> GetIncludeFileInfoList()
        {
            return dal.GetIncludeFileInfoList();
        }

        public static IList<IncludeFileInfo> GetIncludeFileInfoList(int startRowIndexId, int maxNumberRows)
        {
            return dal.GetIncludeFileInfoList(startRowIndexId, maxNumberRows);
        }

        public static IList<IncludeFileInfo> GetIncludeFileListByAssociateType(AssociateType associateType)
        {
            return dal.GetIncludeFileListByAssociateType(associateType);
        }

        public static int GetTotalOfIncludeFileInfo()
        {
            return dal.GetTotalOfIncludeFileInfo();
        }

        public static bool Update(IncludeFileInfo includeFileInfo)
        {
            return dal.Update(includeFileInfo);
        }
    }
}

