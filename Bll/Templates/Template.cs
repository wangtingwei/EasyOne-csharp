namespace EasyOne.Templates
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Enumerations;
    using EasyOne.IDal.Contents;
    using EasyOne.Logging;
    using EasyOne.Model.Contents;
    using EasyOne.DalFactory;
    using System;
    using System.IO;
    using System.Text;
    using System.Web;

    public sealed class Template
    {
        private static readonly IContentManage dalContentManage = EasyOne.DalFactory.DataAccess.CreateContentManage();
        private static readonly INodes dalNodes = EasyOne.DalFactory.DataAccess.CreateNodes();
        private static readonly ISpecial dalSpecial = EasyOne.DalFactory.DataAccess.CreateSpecial();

        private Template()
        {
        }

        public static bool BatchModifyName(StringBuilder replaceData)
        {
            if (replaceData.Length < 1)
            {
                return false;
            }
            foreach (string str in replaceData.ToString().Split(new string[] { "|||" }, StringSplitOptions.RemoveEmptyEntries))
            {
                string[] strArray2 = str.Split(new string[] { "$$$" }, StringSplitOptions.RemoveEmptyEntries);
                dalNodes.ReplaceTemplateFileName(strArray2[0].ToString(), strArray2[1].ToString());
                dalContentManage.ReplaceTemplateFileName(strArray2[0].ToString(), strArray2[1].ToString());
                dalSpecial.ReplaceTemplateFileName(strArray2[0].ToString(), strArray2[1].ToString());
            }
            RemoveCacheAllNodeInfo();
            return true;
        }

        public static bool BatchModifyName(string originalContent, string newContent)
        {
            if (string.IsNullOrEmpty(originalContent))
            {
                return false;
            }
            if (string.IsNullOrEmpty(newContent))
            {
                return false;
            }
            foreach (NodeInfo info in dalNodes.GetNodesList(NodeType.None))
            {
                info.DefaultTemplateFile = info.DefaultTemplateFile.Replace(originalContent, newContent);
                info.ContainChildTemplateFile = info.ContainChildTemplateFile.Replace(originalContent, newContent);
                dalNodes.Update(info);
            }
            foreach (CommonModelInfo info2 in dalContentManage.GetCommonModelInfoList())
            {
                info2.TemplateFile = info2.TemplateFile.Replace(originalContent, newContent);
                dalContentManage.UpdateTemplateFile(info2.GeneralId, info2.TemplateFile.Replace(originalContent, newContent));
            }
            foreach (SpecialInfo info3 in dalSpecial.GetSpecialList())
            {
                info3.SpecialTemplatePath = info3.SpecialTemplatePath.Replace(originalContent, newContent);
                dalSpecial.UpdateSpecial(info3);
            }
            foreach (SpecialCategoryInfo info4 in dalSpecial.GetSpecialCategoryList())
            {
                info4.SpecialTemplatePath = info4.SpecialTemplatePath.Replace(originalContent, newContent);
                dalSpecial.UpdateSpecialCategory(info4);
            }
            RemoveCacheAllNodeInfo();
            return true;
        }

        public static string GetTemplateContent(string path)
        {
            return GetTemplateContent(path, string.Empty);
        }
        /// <summary>
        /// 根据物理地址获取模板内容
        /// </summary>
        /// <param name="path"></param>
        /// <param name="physicalApplicationPath"></param>
        /// <returns></returns>
        public static string GetTemplateContent(string path, string physicalApplicationPath)
        {
            string str;
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }
            if ((HttpContext.Current != null) && string.IsNullOrEmpty(physicalApplicationPath))
            {
                str = HttpContext.Current.Request.PhysicalApplicationPath + SiteConfig.SiteOption.TemplateDir + path;
            }
            else
            {
                str = physicalApplicationPath + SiteConfig.SiteOption.TemplateDir + path;
            }
            str = str.Replace("/", @"\");
            try
            {
                return FileSystemObject.ReadFile(str);
            }
            catch (FileNotFoundException exception)
            {
                ILog log = LogFactory.CreateLog();
                LogInfo info = new LogInfo();
                info.Category = LogCategory.Exception;
                info.Priority = LogPriority.High;
                info.Message = exception.Message;
                if (HttpContext.Current != null)
                {
                    info.ScriptName = HttpContext.Current.Request.RawUrl;
                }
                else
                {
                    info.ScriptName = "系统线程访问！";
                }
                info.Source = exception.Source;
                info.Timestamp = DateTime.Now;
                info.Title = "模板文件“" + path + "”未找到";
                if (HttpContext.Current != null)
                {
                    info.UserIP = PEContext.Current.UserHostAddress;
                }
                else
                {
                    info.UserIP = "";
                }
                info.UserName = "system";
                log.Add(info);
                return "模板文件未找到";
            }
            catch (UnauthorizedAccessException exception2)
            {
                ILog log2 = LogFactory.CreateLog();
                LogInfo info2 = new LogInfo();
                info2.Category = LogCategory.Exception;
                info2.Priority = LogPriority.High;
                info2.Message = exception2.Message;
                if (HttpContext.Current != null)
                {
                    info2.ScriptName = HttpContext.Current.Request.RawUrl;
                }
                else
                {
                    info2.ScriptName = "系统线程访问！";
                }
                info2.Source = exception2.Source;
                info2.Timestamp = DateTime.Now;
                info2.Title = "没有模板文件“" + path + "”的访问权限";
                if (HttpContext.Current != null)
                {
                    info2.UserIP = PEContext.Current.UserHostAddress;
                }
                else
                {
                    info2.UserIP = "";
                }
                info2.UserName = "system";
                log2.Add(info2);
                return "没有模板文件访问权限";
            }
            catch (Exception exception3)
            {
                ILog log3 = LogFactory.CreateLog();
                LogInfo info3 = new LogInfo();
                info3.Category = LogCategory.Exception;
                info3.Priority = LogPriority.High;
                info3.Message = exception3.Message;
                if (HttpContext.Current != null)
                {
                    info3.ScriptName = HttpContext.Current.Request.RawUrl;
                }
                else
                {
                    info3.ScriptName = "系统线程访问！";
                }
                info3.Source = exception3.Source;
                info3.Timestamp = DateTime.Now;
                info3.Title = "读取模板文件“" + path + "”出错";
                if (HttpContext.Current != null)
                {
                    info3.UserIP = PEContext.Current.UserHostAddress;
                }
                else
                {
                    info3.UserIP = "";
                }
                info3.UserName = "system";
                log3.Add(info3);
                return "读取模板文件出错";
            }
        }

        public static void RemoveCacheAllNodeInfo()
        {
            SiteCache.RemoveByPattern(@"CK_Content_NodeInfo_NodeId_\S*");
        }

        public static bool ReplaceTemplateDir(string oldDir, string newDir)
        {
            if (string.IsNullOrEmpty(oldDir) && string.IsNullOrEmpty(newDir))
            {
                return false;
            }
            dalNodes.ReplaceTemplateDir(oldDir, newDir);
            dalContentManage.ReplaceTemplateDir(oldDir, newDir);
            dalSpecial.ReplaceTemplateDir(oldDir, newDir);
            RemoveCacheAllNodeInfo();
            return true;
        }

        public static bool UpdateFileName(string dir, string newdir)
        {
            if (string.IsNullOrEmpty(dir) && string.IsNullOrEmpty(newdir))
            {
                return false;
            }
            dalNodes.ReplaceTemplateFileName(dir, newdir);
            dalContentManage.ReplaceTemplateFileName(dir, newdir);
            dalSpecial.ReplaceTemplateFileName(dir, newdir);
            RemoveCacheAllNodeInfo();
            return true;
        }
    }
}

