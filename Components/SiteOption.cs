namespace EasyOne.Components
{
    using System;

    [Serializable]
    public class SiteOption
    {
        private string m_AdvertisementDir;
        private int m_AutoSignInTime;
        private int m_CollectionSleep;
        private string m_CreateHtmlPath;
        private bool m_EnablePointMoneyExp;
        private bool m_EnableSiteManageCode;
        private bool m_EnableSoftKey;
        private bool m_EnableUploadFiles;
        private int m_HitsOfHot;
        private string m_IncludeFilePath;
        private bool m_IsAbsoluatePath;
        private bool m_IsAutoSignin;
        private string m_ManageDir;
        private int m_RefreshQueueSize;
        private bool m_RssEnable;
        private string m_SiteManageCode;
        private string m_TemplateDir;
        private int m_TicketTime;
        private string m_UploadDir;
        private string m_UploadFileExts;
        private int m_UploadFileMaxSize;
        private string m_UploadFilePathRule;
        private bool m_WapEnable;

        public string AdvertisementDir
        {
            get
            {
                return this.m_AdvertisementDir;
            }
            set
            {
                this.m_AdvertisementDir = value;
            }
        }

        public int AutoSignInTime
        {
            get
            {
                return this.m_AutoSignInTime;
            }
            set
            {
                this.m_AutoSignInTime = value;
            }
        }

        public int CollectionSleep
        {
            get
            {
                return this.m_CollectionSleep;
            }
            set
            {
                this.m_CollectionSleep = value;
            }
        }

        public string CreateHtmlPath
        {
            get
            {
                return this.m_CreateHtmlPath;
            }
            set
            {
                this.m_CreateHtmlPath = value;
            }
        }

        public bool EnablePointMoneyExp
        {
            get
            {
                return this.m_EnablePointMoneyExp;
            }
            set
            {
                this.m_EnablePointMoneyExp = value;
            }
        }

        public bool EnableSiteManageCode
        {
            get
            {
                return this.m_EnableSiteManageCode;
            }
            set
            {
                this.m_EnableSiteManageCode = value;
            }
        }

        public bool EnableSoftKey
        {
            get
            {
                return this.m_EnableSoftKey;
            }
            set
            {
                this.m_EnableSoftKey = value;
            }
        }

        public bool EnableUploadFiles
        {
            get
            {
                return this.m_EnableUploadFiles;
            }
            set
            {
                this.m_EnableUploadFiles = value;
            }
        }

        public int HitsOfHot
        {
            get
            {
                return this.m_HitsOfHot;
            }
            set
            {
                this.m_HitsOfHot = value;
            }
        }

        public string IncludeFilePath
        {
            get
            {
                return this.m_IncludeFilePath;
            }
            set
            {
                this.m_IncludeFilePath = value;
            }
        }

        public bool IsAbsoluatePath
        {
            get
            {
                return this.m_IsAbsoluatePath;
            }
            set
            {
                this.m_IsAbsoluatePath = value;
            }
        }

        public bool IsAutoSignIn
        {
            get
            {
                return this.m_IsAutoSignin;
            }
            set
            {
                this.m_IsAutoSignin = value;
            }
        }

        public string LabelDir
        {
            get
            {
                return (this.m_TemplateDir + "/标签库");
            }
        }

        public string ManageDir
        {
            get
            {
                return this.m_ManageDir;
            }
            set
            {
                this.m_ManageDir = value;
            }
        }

        public string PagerLabelDir
        {
            get
            {
                return (this.m_TemplateDir + "/分页标签库");
            }
        }

        public int RefreshQueueSize
        {
            get
            {
                return this.m_RefreshQueueSize;
            }
            set
            {
                this.m_RefreshQueueSize = value;
            }
        }

        public bool RssEnable
        {
            get
            {
                return this.m_RssEnable;
            }
            set
            {
                this.m_RssEnable = value;
            }
        }

        public string SiteManageCode
        {
            get
            {
                return this.m_SiteManageCode;
            }
            set
            {
                this.m_SiteManageCode = value;
            }
        }

        public string TemplateDir
        {
            get
            {
                return this.m_TemplateDir;
            }
            set
            {
                this.m_TemplateDir = value;
            }
        }

        public int TicketTime
        {
            get
            {
                return this.m_TicketTime;
            }
            set
            {
                this.m_TicketTime = value;
            }
        }

        public string UploadDir
        {
            get
            {
                return this.m_UploadDir;
            }
            set
            {
                this.m_UploadDir = value;
            }
        }

        public string UploadFileExts
        {
            get
            {
                return this.m_UploadFileExts;
            }
            set
            {
                this.m_UploadFileExts = value;
            }
        }

        public int UploadFileMaxSize
        {
            get
            {
                return this.m_UploadFileMaxSize;
            }
            set
            {
                this.m_UploadFileMaxSize = value;
            }
        }

        public string UploadFilePathRule
        {
            get
            {
                return this.m_UploadFilePathRule;
            }
            set
            {
                this.m_UploadFilePathRule = value;
            }
        }

        public bool WapEnable
        {
            get
            {
                return this.m_WapEnable;
            }
            set
            {
                this.m_WapEnable = value;
            }
        }
    }
}

