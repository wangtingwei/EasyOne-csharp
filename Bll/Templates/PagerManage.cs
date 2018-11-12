namespace EasyOne.Templates
{
    using EasyOne.Components;
    using EasyOne.Model.TemplateProc;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Web.Caching;
    using System.Xml;

    public sealed class PagerManage
    {
        private PagerManage()
        {
        }

        public static bool Add(PagerManageInfo ainfo)
        {
            string path = HttpContext.Current.Server.MapPath("~/" + PagerLabelLibPath) + @"\" + ainfo.Name + ".config";
            string str2 = HttpContext.Current.Server.MapPath("~/" + PagerLabelLibPath);
            try
            {
                if (!Directory.Exists(str2))
                {
                    Directory.CreateDirectory(str2);
                }
                if (!File.Exists(path))
                {
                    XmlDocument document = new XmlDocument();
                    XmlNode newChild = document.CreateNode(XmlNodeType.XmlDeclaration, "", "");
                    document.AppendChild(newChild);
                    XmlElement element = document.CreateElement("", "root", "");
                    XmlElement element2 = document.CreateElement("", "LabelType", "");
                    element2.InnerText = ainfo.Type;
                    element.AppendChild(element2);
                    XmlElement element3 = document.CreateElement("", "LabelImage", "");
                    element3.InnerText = ainfo.Image;
                    element.AppendChild(element3);
                    XmlElement element4 = document.CreateElement("", "LabelIntro", "");
                    element4.InnerText = ainfo.Intro;
                    element.AppendChild(element4);
                    XmlElement element5 = document.CreateElement("", "LabelTemplate", "");
                    element5.InnerText = ainfo.Template.ToString();
                    element.AppendChild(element5);
                    document.AppendChild(element);
                    using (StreamWriter writer = new StreamWriter(path, false, Encoding.GetEncoding("utf-8")))
                    {
                        writer.Write(document.InnerXml);
                    }
                    return true;
                }
                return false;
            }
            catch (IOException)
            {
                return false;
            }
            catch (XmlException)
            {
                return false;
            }
        }

        public static bool Copy(string labelName)
        {
            string sourceFileName = HttpContext.Current.Server.MapPath("~/" + PagerLabelLibPath) + @"\" + labelName + ".config";
            string destFileName = HttpContext.Current.Server.MapPath("~/" + PagerLabelLibPath) + @"\";
            for (int i = 1; i < 100; i++)
            {
                string path = destFileName + labelName + "(" + i.ToString() + ").config";
                if (!File.Exists(path))
                {
                    destFileName = path;
                    break;
                }
            }
            try
            {
                File.Copy(sourceFileName, destFileName, false);
            }
            catch (IOException)
            {
                return false;
            }
            return true;
        }

        public static bool Delete(string labelNames)
        {
            foreach (string str in labelNames.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
            {
                string path = HttpContext.Current.Server.MapPath("~/" + PagerLabelLibPath) + @"\" + str + ".config";
                try
                {
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                }
                catch (IOException exception)
                {
                    throw new IOException(exception.Message);
                }
            }
            return true;
        }

        public static bool Exists(string pagerName)
        {
            return File.Exists(HttpContext.Current.Server.MapPath("~/" + PagerLabelLibPath) + @"\" + pagerName + ".config");
        }

        public static PagerManageInfo GetPagerByName(string name)
        {
            string str;
            if (HttpContext.Current != null)
            {
                str = HttpContext.Current.Server.MapPath("~/" + PagerLabelLibPath) + @"\" + name + ".config";
            }
            else
            {
                str = AppDomain.CurrentDomain.BaseDirectory + PagerLabelLibPath + @"\" + name + ".config";
            }
            if (File.Exists(str))
            {
                try
                {
                    PagerManageInfo info;
                    FileInfo info2 = new FileInfo(str);
                    XmlDocument document = new XmlDocument();
                    using (StreamReader reader = info2.OpenText())
                    {
                        document.Load(reader);
                        info = new PagerManageInfo();
                        info.Image = document.SelectSingleNode("root/LabelImage").InnerText;
                        info.Intro = document.SelectSingleNode("root/LabelIntro").InnerText;
                        info.Template = new StringBuilder(document.SelectSingleNode("root/LabelTemplate").InnerText);
                        info.Type = document.SelectSingleNode("root/LabelType").InnerText;
                        info.Name = name;
                        info.UpDateTime = info2.LastWriteTime;
                    }
                    return info;
                }
                catch (IOException)
                {
                    return new PagerManageInfo(true);
                }
                catch (XmlException)
                {
                    return new PagerManageInfo(true);
                }
            }
            return new PagerManageInfo(true);
        }

        public static IList<PagerManageInfo> GetPagerList(string type)
        {
            string key = "CK_Label_PagerLabelList_Type_" + type;
            List<PagerManageInfo> list = new List<PagerManageInfo>();
            if (SiteCache.Get(key) == null)
            {
                string str2;
                if (HttpContext.Current != null)
                {
                    str2 = HttpContext.Current.Server.MapPath("~/" + PagerLabelLibPath);
                }
                else
                {
                    str2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, PagerLabelLibPath);
                }
                DirectoryInfo info = new DirectoryInfo(str2);
                if (info.Exists)
                {
                    foreach (FileInfo info2 in info.GetFiles())
                    {
                        try
                        {
                            using (StreamReader reader = info2.OpenText())
                            {
                                XmlDocument document = new XmlDocument();
                                document.Load(reader);
                                PagerManageInfo item = new PagerManageInfo();
                                item.Name = Path.GetFileNameWithoutExtension(info2.Name);
                                item.Type = document.SelectSingleNode("root/LabelType").InnerText;
                                item.Image = document.SelectSingleNode("root/LabelImage").InnerText;
                                item.Intro = document.SelectSingleNode("root/LabelIntro").InnerText;
                                if (string.IsNullOrEmpty(item.Image))
                                {
                                    item.Image = "~/Admin/Images/LabelIco/def.ico";
                                }
                                item.UpDateTime = info2.LastWriteTime;
                                if (((type == item.Type) || string.IsNullOrEmpty(type)) || (type == "全部分类"))
                                {
                                    list.Add(item);
                                }
                            }
                        }
                        catch (XmlException exception)
                        {
                            exception.ToString();
                        }
                    }
                }
                SiteCache.Insert(key, list, new CacheDependency(str2));//add sql
                return list;
            }
            return (List<PagerManageInfo>) SiteCache.Get(key);
        }

        public static IList<PagerManageInfo> GetPagerList(string searchType, string keyword)
        {
            string str;
            List<PagerManageInfo> list = new List<PagerManageInfo>();
            if (HttpContext.Current != null)
            {
                str = HttpContext.Current.Server.MapPath("~/" + PagerLabelLibPath);
            }
            else
            {
                str = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, PagerLabelLibPath);
            }
            DirectoryInfo info = new DirectoryInfo(str);
            if (info.Exists)
            {
                foreach (FileInfo info2 in info.GetFiles())
                {
                    try
                    {
                        using (StreamReader reader = info2.OpenText())
                        {
                            XmlDocument document = new XmlDocument();
                            document.Load(reader);
                            PagerManageInfo item = new PagerManageInfo();
                            item.Name = Path.GetFileNameWithoutExtension(info2.Name);
                            item.Type = document.SelectSingleNode("root/LabelType").InnerText;
                            item.Image = document.SelectSingleNode("root/LabelImage").InnerText;
                            item.Intro = document.SelectSingleNode("root/LabelIntro").InnerText;
                            if (string.IsNullOrEmpty(item.Image))
                            {
                                item.Image = "~/Admin/Images/LabelIco/def.ico";
                            }
                            item.UpDateTime = info2.LastWriteTime;
                            if ((searchType == "0") && (item.Name.IndexOf(keyword, 0, StringComparison.CurrentCultureIgnoreCase) >= 0))
                            {
                                list.Add(item);
                            }
                            if ((searchType == "1") && (item.Template.ToString().IndexOf(keyword, 0, StringComparison.CurrentCultureIgnoreCase) >= 0))
                            {
                                list.Add(item);
                            }
                        }
                    }
                    catch (XmlException exception)
                    {
                        exception.ToString();
                    }
                }
            }
            return list;
        }

        public static IList<PagerManageInfo> GetPagerTypeList()
        {
            string key = "CK_Label_PagerLabel_Type";
            List<PagerManageInfo> list = new List<PagerManageInfo>();
            if (SiteCache.Get(key) == null)
            {
                string str2;
                List<string> list2 = new List<string>();
                if (HttpContext.Current != null)
                {
                    str2 = HttpContext.Current.Server.MapPath("~/" + PagerLabelLibPath);
                }
                else
                {
                    str2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, PagerLabelLibPath);
                }
                DirectoryInfo info = new DirectoryInfo(str2);
                if (info.Exists)
                {
                    foreach (FileInfo info2 in info.GetFiles())
                    {
                        XmlDocument document = new XmlDocument();
                        try
                        {
                            using (StreamReader reader = info2.OpenText())
                            {
                                document.Load(reader);
                                string innerText = document.SelectSingleNode("root/LabelType").InnerText;
                                if (!list2.Contains(innerText) && !string.IsNullOrEmpty(innerText))
                                {
                                    list2.Add(innerText);
                                }
                            }
                        }
                        catch (XmlException exception)
                        {
                            exception.ToString();
                        }
                    }
                }
                foreach (string str4 in list2)
                {
                    list.Add(new PagerManageInfo(str4));
                }
                SiteCache.Insert(key, list, new CacheDependency(str2));//add sql
                return list;
            }
            return (List<PagerManageInfo>) SiteCache.Get(key);
        }

        public static bool Update(PagerManageInfo ainfo, string oldName)
        {
            string path = VirtualPathUtility.AppendTrailingSlash(HttpContext.Current.Server.MapPath("~/" + PagerLabelLibPath));
            try
            {
                if (string.Compare(oldName, ainfo.Name, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    path = path + ainfo.Name + ".config";
                    XmlDocument document = new XmlDocument();
                    XmlNode newChild = document.CreateNode(XmlNodeType.XmlDeclaration, "", "");
                    document.AppendChild(newChild);
                    XmlElement element = document.CreateElement("", "root", "");
                    XmlElement element2 = document.CreateElement("", "LabelType", "");
                    element2.InnerText = ainfo.Type;
                    element.AppendChild(element2);
                    XmlElement element3 = document.CreateElement("", "LabelImage", "");
                    element3.InnerText = ainfo.Image;
                    element.AppendChild(element3);
                    XmlElement element4 = document.CreateElement("", "LabelIntro", "");
                    element4.InnerText = ainfo.Intro;
                    element.AppendChild(element4);
                    XmlElement element5 = document.CreateElement("", "LabelTemplate", "");
                    element5.InnerText = ainfo.Template.ToString();
                    element.AppendChild(element5);
                    document.AppendChild(element);
                    using (StreamWriter writer = new StreamWriter(path, false, Encoding.GetEncoding("utf-8")))
                    {
                        writer.Write(document.InnerXml);
                    }
                    return true;
                }
                Delete(oldName);
                return Add(ainfo);
            }
            catch (IOException)
            {
                return false;
            }
            catch (XmlException)
            {
                return false;
            }
        }

        public static string PagerLabelLibPath
        {
            get
            {
                return SiteConfig.SiteOption.PagerLabelDir;
            }
        }
    }
}

