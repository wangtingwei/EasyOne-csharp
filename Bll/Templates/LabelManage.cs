namespace EasyOne.Templates
{
    using EasyOne.Components;
    using EasyOne.Enumerations;
    using EasyOne.IDal.TemplateProc;
    using EasyOne.Model.TemplateProc;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Web.Caching;
    using System.Xml;
    using EasyOne.DalFactory;

    public sealed class LabelManage
    {
        private static readonly ILabelManage dal = DataAccess.CreateLabelManage();

        private LabelManage()
        {
        }

        public static bool Add(LabelManageInfo ainfo)
        {
            string path = HttpContext.Current.Server.MapPath("~/" + LabelLibPath);
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                if (!Exists(ainfo.Name))
                {
                    File.WriteAllText(HttpContext.Current.Server.MapPath("~/" + LabelLibPath) + @"\" + ainfo.Name + ".config", ainfo.Define.ToString(), Encoding.UTF8);
                    return true;
                }
                return false;
            }
            catch (IOException)
            {
                return false;
            }
        }

        public static bool AddAttribute(string xmlfilepath, string attributename, string defaultvalue, string intro)
        {
            if (!string.IsNullOrEmpty(attributename))
            {
                string strA = attributename.ToLower();
                if ((((string.Compare(strA, "id", StringComparison.OrdinalIgnoreCase) != 0) && (string.Compare(strA, "page", StringComparison.OrdinalIgnoreCase) != 0)) && ((string.Compare(strA, "pagesize", StringComparison.OrdinalIgnoreCase) != 0) && (string.Compare(strA, "begintime", StringComparison.OrdinalIgnoreCase) != 0))) && ((string.Compare(strA, "endtime", StringComparison.OrdinalIgnoreCase) != 0) && (string.Compare(strA, "outtime", StringComparison.OrdinalIgnoreCase) != 0)))
                {
                    XmlDocument document = new XmlDocument();
                    try
                    {
                        document.Load(xmlfilepath);
                        XmlNode node = document.SelectSingleNode("root");
                        XmlElement newChild = document.CreateElement("", "attributes", "");
                        XmlElement element2 = document.CreateElement("", "name", "");
                        XmlText text = document.CreateTextNode(attributename);
                        element2.AppendChild(text);
                        newChild.AppendChild(element2);
                        element2 = document.CreateElement("", "default", "");
                        text = document.CreateTextNode(defaultvalue);
                        element2.AppendChild(text);
                        newChild.AppendChild(element2);
                        element2 = document.CreateElement("", "intro", "");
                        text = document.CreateTextNode(intro);
                        element2.AppendChild(text);
                        newChild.AppendChild(element2);
                        node.AppendChild(newChild);
                        document.Save(xmlfilepath);
                        return true;
                    }
                    catch (XmlException)
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        public static bool AttributeExists(string xmlfilepath, string attributename)
        {
            XmlDocument document = new XmlDocument();
            try
            {
                bool flag = true;
                document.Load(xmlfilepath);
                foreach (XmlNode node in document.SelectNodes("root/attributes"))
                {
                    if (string.Compare(attributename, node.FirstChild.InnerText, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        flag = false;
                        break;
                    }
                }
                return flag;
            }
            catch (XmlException)
            {
                return true;
            }
        }

        public static bool Copy(string labelName)
        {
            string sourceFileName = HttpContext.Current.Server.MapPath("~/" + LabelLibPath) + @"\" + labelName + ".config";
            string destFileName = HttpContext.Current.Server.MapPath("~/" + LabelLibPath) + @"\";
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
                string path = HttpContext.Current.Server.MapPath("~/" + LabelLibPath) + @"\" + str + ".config";
                try
                {
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                }
                catch (IOException)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool DeleteAttribute(string xmlfilepath, string attributename)
        {
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(xmlfilepath);
                foreach (XmlNode node in document.SelectNodes("root/attributes"))
                {
                    if (string.Compare(node.FirstChild.InnerText, attributename, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        node.ParentNode.RemoveChild(node);
                        break;
                    }
                }
                document.Save(xmlfilepath);
                return true;
            }
            catch (XmlException)
            {
                return false;
            }
        }

        public static bool Exists(string iname)
        {
            return File.Exists(HttpContext.Current.Server.MapPath("~/" + LabelLibPath) + @"\" + iname + ".config");
        }

        public static IList<LabelAttributeInfo> GetAttributeList(string xmlfilepath)
        {
            XmlDocument document = new XmlDocument();
            IList<LabelAttributeInfo> list = new List<LabelAttributeInfo>();
            if (string.IsNullOrEmpty(xmlfilepath))
            {
                return list;
            }
            try
            {
                document.Load(xmlfilepath);
                foreach (XmlNode node in document.SelectNodes("root/attributes"))
                {
                    LabelAttributeInfo item = new LabelAttributeInfo(node.ChildNodes.Item(0).InnerText, node.ChildNodes.Item(1).InnerText, node.ChildNodes.Item(2).InnerText);
                    list.Add(item);
                }
                return list;
            }
            catch (XmlException exception)
            {
                LabelAttributeInfo info2 = new LabelAttributeInfo("error", string.Empty, exception.ToString());
                list.Add(info2);
                return list;
            }
        }

        public static LabelManageInfo GetCacheLabelByName(string labelName)
        {
            string str2;
            string key = "CK_Label_LabelManageInfo_" + labelName;
            if (HttpContext.Current != null)
            {
                str2 = HttpContext.Current.Server.MapPath("~/" + LabelLibPath) + @"\" + labelName + ".config";
            }
            else
            {
                str2 = AppDomain.CurrentDomain.BaseDirectory + LabelLibPath + @"\" + labelName + ".config";
            }
            LabelManageInfo labelByName = SiteCache.Get(key) as LabelManageInfo;
            if (labelByName == null)
            {
                labelByName = GetLabelByName(labelName);
                if (File.Exists(str2))
                {
                    SiteCache.Insert(key, labelByName, new System.Web.Caching.CacheDependency(str2));//将EasyOne.Components.改了
                }
            }
            return labelByName;
        }

        public static string GetDBQuery(string dbtype, string dbconn, string sqlstr, XmlNodeList attrib)
        {
            switch (dbtype)
            {
                case "sql_sysquery":
                    return dal.GetMainDBQuery(sqlstr, attrib, false);

                case "sql_sysstoredquery":
                    return dal.GetMainDBQuery(sqlstr, attrib, true);

                case "sql_outquery":
                    return dal.GetOutSqlDBQuery(dbconn, sqlstr);

                case "ole_read":
                    return dal.GetOleDBQuery(dbconn, sqlstr);

                case "odbc_read":
                    return dal.GetOdbcDBQuery(dbconn, sqlstr);

                case "orc_read":
                    return dal.GetOracleDBQuery(dbconn, sqlstr);
            }
            return string.Empty;
        }

        public static LabelManageInfo GetLabelByName(string labelName)
        {
            string str;
            XmlDocument document = new XmlDocument();
            LabelManageInfo info = new LabelManageInfo();
            if (HttpContext.Current != null)
            {
                str = HttpContext.Current.Server.MapPath("~/" + LabelLibPath) + @"\" + labelName + ".config";
            }
            else
            {
                str = AppDomain.CurrentDomain.BaseDirectory + LabelLibPath + @"\" + labelName + ".config";
            }
            if (!File.Exists(str))
            {
                return new LabelManageInfo(true);
            }
            try
            {
                FileInfo info2 = new FileInfo(str);
                using (StreamReader reader = info2.OpenText())
                {
                    document.Load(reader);
                    info.Name = labelName;
                    info.Type = document.SelectSingleNode("root/LabelType").InnerText;
                    info.Define = new StringBuilder(document.InnerXml.ToString());
                    info.Template = new StringBuilder(document.SelectSingleNode("root/LabelTemplate").InnerText);
                    return info;
                }
            }
            catch (XmlException)
            {
                return new LabelManageInfo(true);
            }
            return info;
        }

        public static IList<LabelManageInfo> GetLabelList(string type)
        {
            string key = "CK_Label_LabelManageInfoList_Type__" + type;
            List<LabelManageInfo> list = new List<LabelManageInfo>();
            if (SiteCache.Get(key) == null)
            {
                string path = HttpContext.Current.Server.MapPath("~/" + LabelLibPath);
                DirectoryInfo info = new DirectoryInfo(path);
                if (info.Exists)
                {
                    foreach (FileInfo info2 in info.GetFiles())
                    {
                        try
                        {
                            XmlDocument document = new XmlDocument();
                            using (StreamReader reader = info2.OpenText())
                            {
                                document.Load(reader);
                                LabelManageInfo item = new LabelManageInfo();
                                item.Name = Path.GetFileNameWithoutExtension(info2.Name);
                                item.Type = document.SelectSingleNode("root/LabelType").InnerText;
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
                SiteCache.Insert(key, list, new CacheDependency(path));
                return list;
            }
            return (List<LabelManageInfo>) SiteCache.Get(key);
        }

        public static IList<LabelManageInfo> GetLabelList(int type, int field, string keyword, string labelCategory)
        {
            string key = "CK_Label_LabelManageInfoList_Type_Search__" + labelCategory;
            List<LabelManageInfo> list = new List<LabelManageInfo>();
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.Trim();
            }
            if ((type == 1) || (SiteCache.Get(key) == null))
            {
                string path = HttpContext.Current.Server.MapPath("~/" + LabelLibPath);
                DirectoryInfo info = new DirectoryInfo(path);
                if (info.Exists)
                {
                    List<FileInfo> list2 = new List<FileInfo>();
                    list2.AddRange(info.GetFiles());
                    foreach (FileInfo info2 in list2)
                    {
                        try
                        {
                            using (StreamReader reader = info2.OpenText())
                            {
                                XmlDocument document = new XmlDocument();
                                document.Load(reader);
                                LabelManageInfo item = new LabelManageInfo();
                                item.Name = Path.GetFileNameWithoutExtension(info2.Name);
                                item.Type = document.SelectSingleNode("root/LabelType").InnerText;
                                item.Intro = document.SelectSingleNode("root/LabelIntro").InnerText;
                                item.UpDateTime = info2.LastWriteTime;
                                switch (type)
                                {
                                    case 0:
                                    {
                                        list.Add(item);
                                        continue;
                                    }
                                    case 1:
                                    {
                                        if ((field != 0) || string.IsNullOrEmpty(keyword))
                                        {
                                            break;
                                        }
                                        if (item.Name.IndexOf(keyword, 0, StringComparison.CurrentCultureIgnoreCase) >= 0)
                                        {
                                            list.Add(item);
                                        }
                                        continue;
                                    }
                                    default:
                                        goto Label_016C;
                                }
                                if ((field == 1) && (document.SelectSingleNode("root").InnerText.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0))
                                {
                                    list.Add(item);
                                }
                                continue;
                            Label_016C:
                                if (((labelCategory == item.Type) || string.IsNullOrEmpty(labelCategory)) || (labelCategory == "全部分类"))
                                {
                                    list.Add(item);
                                }
                            }
                            continue;
                        }
                        catch (XmlException exception)
                        {
                            exception.ToString();
                            continue;
                        }
                    }
                }
                if (type != 1)
                {
                    SiteCache.Insert(key, list, new CacheDependency(path));
                }
                return list;
            }
            return (List<LabelManageInfo>) SiteCache.Get(key);
        }

        public static IList<LabelManageInfo> GetLabelTypeList()
        {
            string key = "CK_Label_LabelTypeList";
            List<LabelManageInfo> list = new List<LabelManageInfo>();
            if (SiteCache.Get(key) == null)
            {
                string path = HttpContext.Current.Server.MapPath("~/" + LabelLibPath);
                DirectoryInfo info = new DirectoryInfo(path);
                List<string> list2 = new List<string>();
                if (info.Exists)
                {
                    foreach (FileInfo info2 in info.GetFiles("*.config"))
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
                    list.Add(new LabelManageInfo(str4));
                }
                SiteCache.Insert(key, list, new CacheDependency(path));
                return list;
            }
            return (List<LabelManageInfo>) SiteCache.Get(key);
        }

        public static DataTable GetSchemaDataBase(string dbconn, DataSourceType dataSourceType)
        {
            return dal.GetSchemaDataBase(dbconn, dataSourceType);
        }

        public static DataTable GetSchemaTable(string tableName, string dbconn, DataSourceType dataSourceType)
        {
            return dal.GetSchemaTable(tableName, dbconn, dataSourceType);
        }

        public static DataTable GetSystemSchemaDataBases()
        {
            return GetSchemaDataBase(null, DataSourceType.None);
        }

        public static bool TestOutSideDatabase(string dbtype, string dbconn)
        {
            if (!string.IsNullOrEmpty(dbconn))
            {
                switch (dbtype)
                {
                    case "sql_outquery":
                        return dal.TestOutSql(dbconn);

                    case "sql_outstoredquery":
                        return dal.TestOutSql(dbconn);

                    case "ole_read":
                        return dal.TestOle(dbconn);

                    case "xml_read":
                        return XmlManage.CheckXmlDataSource(dbconn);

                    case "odbc_read":
                        return dal.TestOdbc(dbconn);

                    case "orc_read":
                        return dal.TestOracle(dbconn);
                }
            }
            return false;
        }

        public static bool Update(LabelManageInfo ainfo)
        {
            try
            {
                File.WriteAllText(HttpContext.Current.Server.MapPath("~/" + LabelLibPath) + @"\" + ainfo.Name + ".config", ainfo.Define.ToString(), Encoding.UTF8);
                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }

        public static bool Update(LabelManageInfo ainfo, string newLableName)
        {
            if (ainfo.Name == newLableName)
            {
                return Update(ainfo);
            }
            if (!Exists(newLableName))
            {
                try
                {
                    string path = HttpContext.Current.Server.MapPath("~/" + LabelLibPath) + @"\" + ainfo.Name + ".config";
                    string str2 = HttpContext.Current.Server.MapPath("~/" + LabelLibPath) + @"\" + newLableName + ".config";
                    File.Delete(path);
                    File.WriteAllText(str2, ainfo.Define.ToString(), Encoding.UTF8);
                    return true;
                }
                catch (IOException)
                {
                    return false;
                }
            }
            return false;
        }

        public static bool UpdateAttribute(string xmlfilepath, string attributename, string defaultvalue, string intro)
        {
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(xmlfilepath);
                foreach (XmlNode node in document.SelectNodes("root/attributes"))
                {
                    if (string.Compare(node.FirstChild.InnerText, attributename, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        foreach (XmlNode node2 in node.ChildNodes)
                        {
                            if (string.Compare(node2.Name, "name", StringComparison.OrdinalIgnoreCase) == 0)
                            {
                                node2.InnerText = attributename;
                            }
                            if (string.Compare(node2.Name, "default", StringComparison.OrdinalIgnoreCase) == 0)
                            {
                                node2.InnerText = defaultvalue;
                            }
                            if (string.Compare(node2.Name, "intro", StringComparison.OrdinalIgnoreCase) == 0)
                            {
                                node2.InnerText = intro;
                            }
                        }
                        break;
                    }
                }
                document.Save(xmlfilepath);
                return true;
            }
            catch (XmlException)
            {
                return false;
            }
        }

        public static bool UpdateAttribute(string xmlfilepath, string oldAttributeName, string attributename, string defaultvalue, string intro)
        {
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(xmlfilepath);
                foreach (XmlNode node in document.SelectNodes("root/attributes"))
                {
                    if (string.Compare(node.FirstChild.InnerText, oldAttributeName, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        foreach (XmlNode node2 in node.ChildNodes)
                        {
                            if (string.Compare(node2.Name, "name", StringComparison.OrdinalIgnoreCase) == 0)
                            {
                                node2.InnerText = attributename;
                            }
                            if (string.Compare(node2.Name, "default", StringComparison.OrdinalIgnoreCase) == 0)
                            {
                                node2.InnerText = defaultvalue;
                            }
                            if (string.Compare(node2.Name, "intro", StringComparison.OrdinalIgnoreCase) == 0)
                            {
                                node2.InnerText = intro;
                            }
                        }
                        break;
                    }
                }
                document.Save(xmlfilepath);
                return true;
            }
            catch (XmlException)
            {
                return false;
            }
        }

        public static string LabelLibPath
        {
            get
            {
                return SiteConfig.SiteOption.LabelDir;
            }
        }
    }
}

