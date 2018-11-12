namespace EasyOne.Components
{
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Security;
    using System.Web;
    using System.Web.Caching;
    using System.Xml.Serialization;

    public sealed class SiteConfig
    {
        private string filePath;

        public SiteConfig()
        {
            if (this.filePath == null)
            {
                HttpContext current = HttpContext.Current;
                if (current != null)
                {
                    this.filePath = current.Server.MapPath("~/Config/Site.config");
                }
                else
                {
                    this.filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config/Site.config");
                }
            }
        }

        public SiteConfig(string path)
        {
            this.filePath = path;
        }

        public static SiteConfigInfo ConfigInfo()
        {
            SiteConfigInfo info = SiteCache.Get("CK_System_SiteConfigInfo") as SiteConfigInfo;
            if (info == null)
            {
                info = ConfigReadFromFile();
                SiteCache.Insert("CK_System_SiteConfigInfo", info, new System.Web.Caching.CacheDependency(new SiteConfig().FilePath));//  将new CacheDependency(new SiteConfig().FilePath)); 删除了new CacheDependency
            }
            if (info == null)
            {
                info = new SiteConfigInfo();
            }
            return info;
        }

        public static SiteConfigInfo ConfigReadFromFile()
        {
            using (Stream stream = new FileStream(new SiteConfig().FilePath, FileMode.Open, FileAccess.Read))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(SiteConfigInfo));
                return (SiteConfigInfo) serializer.Deserialize(stream);
            }
        }

        public void Update(SiteConfigInfo config)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(SiteConfigInfo));
                using (Stream stream = new FileStream(this.filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                    namespaces.Add("", "");
                    serializer.Serialize(stream, config, namespaces);
                }
            }
            catch (SecurityException exception)
            {
                throw new SecurityException(exception.Message, exception.DenySetInstance, exception.PermitOnlySetInstance, exception.Method, exception.Demanded, exception.FirstPermissionThatFailed);
            }
        }

        public string FilePath
        {
            get
            {
                return this.filePath;
            }
            set
            {
                this.filePath = value;
            }
        }

        public static Collection<FrontTemplate> FrontTemplateList
        {
            get
            {
                return ConfigInfo().FrontTemplateList;
            }
        }

        public static EasyOne.Components.IPLockConfig IPLockConfig
        {
            get
            {
                return ConfigInfo().IPLockConfig;
            }
        }

        public static EasyOne.Components.MailConfig MailConfig
        {
            get
            {
                return ConfigInfo().MailConfig;
            }
        }

        public static EasyOne.Components.ShopConfig ShopConfig
        {
            get
            {
                return ConfigInfo().ShopConfig;
            }
        }

        public static EasyOne.Components.SiteInfo SiteInfo
        {
            get
            {
                return ConfigInfo().SiteInfo;
            }
        }

        public static EasyOne.Components.SiteOption SiteOption
        {
            get
            {
                return ConfigInfo().SiteOption;
            }
        }

        public static EasyOne.Components.SmsConfig SmsConfig
        {
            get
            {
                return ConfigInfo().SmsConfig;
            }
        }

        public static EasyOne.Components.ThumbsConfig ThumbsConfig
        {
            get
            {
                return ConfigInfo().ThumbsConfig;
            }
        }

        public static EasyOne.Components.UserConfig UserConfig
        {
            get
            {
                return ConfigInfo().UserConfig;
            }
        }

        public static EasyOne.Components.WaterMarkConfig WaterMarkConfig
        {
            get
            {
                return ConfigInfo().WaterMarkConfig;
            }
        }
    }
}

