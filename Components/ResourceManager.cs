namespace EasyOne.Components
{
    using EasyOne.Enumerations;
    using System;
    using System.Collections;
    using System.IO;
    using System.Web;
    using System.Xml;

    public static class ResourceManager
    {
        public static ErrMessage GetMessage(PEExceptionType exceptionType)
        {
            Hashtable hashtable = GetResource(ResourceManagerType.ErrorMessage, "zh-CHS", "Messages.xml", false);
            if (hashtable[(int) exceptionType] == null)
            {
                throw new CustomException(PEExceptionType.ResourceNotFound, "文件 Messages.xml 中未找到： " + exceptionType);
            }
            return (ErrMessage) hashtable[(int) exceptionType];
        }

        private static Hashtable GetResource(ResourceManagerType resourceType, string userLanguage, string fileName, bool defaultOnly)
        {
            if (defaultOnly)
            {
                defaultOnly = false;
            }
            string cacheKey = userLanguage + fileName;
            Hashtable target = null;
            if (target == null)
            {
                target = new Hashtable();
                if ("en-US" != userLanguage)
                {
                    target = LoadResource(resourceType, target, userLanguage, cacheKey, fileName);
                }
            }
            return target;
        }

        public static string GetString(string name)
        {
            return GetString(name, false);
        }

        public static string GetString(string name, bool defaultOnly)
        {
            return GetString(name, "Resources.xml", defaultOnly);
        }

        public static string GetString(string name, string fileName)
        {
            return GetString(name, fileName, false);
        }

        public static string GetString(string name, string fileName, bool defaultOnly)
        {
            Hashtable hashtable = null;
            string userLanguage = "zh-CHS";
            if (!string.IsNullOrEmpty(fileName))
            {
                hashtable = GetResource(ResourceManagerType.String, userLanguage, fileName, defaultOnly);
            }
            else
            {
                hashtable = GetResource(ResourceManagerType.String, userLanguage, "Resources.xml", defaultOnly);
            }
            string str2 = hashtable[name] as string;
            if (string.IsNullOrEmpty(str2) && !string.IsNullOrEmpty(fileName))
            {
                str2 = GetResource(0, userLanguage, "Resources.xml", false)[name] as string;//将0改为 false//
            }
            return str2;
        }

        private static Hashtable LoadResource(ResourceManagerType resourceType, Hashtable target, string language, string cacheKey, string fileName)
        {
            string str = cacheKey;
            str = @"Languages\" + language + @"\" + fileName;
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string newValue = Path.DirectorySeparatorChar.ToString();
            string filename = baseDirectory.Replace("/", newValue).TrimEnd(new char[] { Path.DirectorySeparatorChar }) + Path.DirectorySeparatorChar.ToString() + str.TrimStart(new char[] { Path.DirectorySeparatorChar });
            HttpContext current = HttpContext.Current;
            if (current != null)
            {
                filename = current.Server.MapPath("~/Languages/zh-CHS/" + fileName);
            }
            else
            {
                filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Languages/zh-CHS/" + fileName);
            }
            XmlDocument document = new XmlDocument();
            document.Load(filename);
            foreach (XmlNode node in document.SelectSingleNode("root").ChildNodes)
            {
                if (node.NodeType != XmlNodeType.Comment)
                {
                    switch (resourceType)
                    {
                        case ResourceManagerType.String:
                        {
                            if (target[node.Attributes["name"].Value] != null)
                            {
                                goto Label_0188;
                            }
                            target.Add(node.Attributes["name"].Value, node.InnerText);
                            continue;
                        }
                        case ResourceManagerType.ErrorMessage:
                        {
                            ErrMessage message = new ErrMessage(node);
                            target[message.MessageId] = message;
                            continue;
                        }
                        case ResourceManagerType.Template:
                        {
                            continue;
                        }
                    }
                }
                continue;
            Label_0188:
                target[node.Attributes["name"].Value] = node.InnerText;
            }
            return target;
        }
    }
}

