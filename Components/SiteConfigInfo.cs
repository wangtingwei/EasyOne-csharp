namespace EasyOne.Components
{
    using System;
    using System.Collections.ObjectModel;
    using System.Xml.Serialization;

    [Serializable, XmlRoot("SiteConfig")]
    public class SiteConfigInfo
    {
        private Collection<FrontTemplate> m_FrontTemplateList;
        private EasyOne.Components.IPLockConfig m_IPLockConfig;
        private EasyOne.Components.MailConfig m_MailConfig;
        private EasyOne.Components.ShopConfig m_ShopConfig;
        private EasyOne.Components.SiteInfo m_SiteInfo;
        private EasyOne.Components.SiteOption m_SiteOption;
        private EasyOne.Components.SmsConfig m_SmsConfig;
        private EasyOne.Components.ThumbsConfig m_ThumbsConfig;
        private EasyOne.Components.UserConfig m_UserConfig;
        private EasyOne.Components.WaterMarkConfig m_WaterMarkConfig;

        public SiteConfigInfo()
        {
            if (this.m_MailConfig == null)
            {
                this.m_MailConfig = new EasyOne.Components.MailConfig();
            }
            if (this.m_IPLockConfig == null)
            {
                this.m_IPLockConfig = new EasyOne.Components.IPLockConfig();
            }
            if (this.m_UserConfig == null)
            {
                this.m_UserConfig = new EasyOne.Components.UserConfig();
            }
            if (this.m_ShopConfig == null)
            {
                this.m_ShopConfig = new EasyOne.Components.ShopConfig();
            }
            if (this.m_ThumbsConfig == null)
            {
                this.m_ThumbsConfig = new EasyOne.Components.ThumbsConfig();
            }
            if (this.m_WaterMarkConfig == null)
            {
                this.m_WaterMarkConfig = new EasyOne.Components.WaterMarkConfig();
            }
            if (this.m_SiteOption == null)
            {
                this.m_SiteOption = new EasyOne.Components.SiteOption();
            }
            if (this.m_SiteInfo == null)
            {
                this.m_SiteInfo = new EasyOne.Components.SiteInfo();
            }
            if (this.m_SmsConfig == null)
            {
                this.m_SmsConfig = new EasyOne.Components.SmsConfig();
            }
        }

        public void CopyToTemplateInfoList(Collection<FrontTemplate> infoList)
        {
            this.m_FrontTemplateList = new Collection<FrontTemplate>();
            foreach (FrontTemplate template in infoList)
            {
                this.m_FrontTemplateList.Add(template);
            }
        }

        public Collection<FrontTemplate> FrontTemplateList
        {
            get
            {
                if (this.m_FrontTemplateList == null)
                {
                    this.m_FrontTemplateList = new Collection<FrontTemplate>();
                }
                return this.m_FrontTemplateList;
            }
        }

        public EasyOne.Components.IPLockConfig IPLockConfig
        {
            get
            {
                return this.m_IPLockConfig;
            }
            set
            {
                this.m_IPLockConfig = value;
            }
        }

        public EasyOne.Components.MailConfig MailConfig
        {
            get
            {
                return this.m_MailConfig;
            }
            set
            {
                this.m_MailConfig = value;
            }
        }

        public EasyOne.Components.ShopConfig ShopConfig
        {
            get
            {
                return this.m_ShopConfig;
            }
            set
            {
                this.m_ShopConfig = value;
            }
        }

        public EasyOne.Components.SiteInfo SiteInfo
        {
            get
            {
                return this.m_SiteInfo;
            }
            set
            {
                this.m_SiteInfo = value;
            }
        }

        public EasyOne.Components.SiteOption SiteOption
        {
            get
            {
                return this.m_SiteOption;
            }
            set
            {
                this.m_SiteOption = value;
            }
        }

        public EasyOne.Components.SmsConfig SmsConfig
        {
            get
            {
                return this.m_SmsConfig;
            }
            set
            {
                this.m_SmsConfig = value;
            }
        }

        public EasyOne.Components.ThumbsConfig ThumbsConfig
        {
            get
            {
                return this.m_ThumbsConfig;
            }
            set
            {
                this.m_ThumbsConfig = value;
            }
        }

        public EasyOne.Components.UserConfig UserConfig
        {
            get
            {
                return this.m_UserConfig;
            }
            set
            {
                this.m_UserConfig = value;
            }
        }

        public EasyOne.Components.WaterMarkConfig WaterMarkConfig
        {
            get
            {
                return this.m_WaterMarkConfig;
            }
            set
            {
                this.m_WaterMarkConfig = value;
            }
        }
    }
}

