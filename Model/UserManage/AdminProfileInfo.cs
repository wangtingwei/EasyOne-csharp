namespace EasyOne.Model.UserManage
{
    using EasyOne.Model;
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class AdminProfileInfo : EasyOne.Model.Nullable
    {
        private string m_AdminName;
        private string m_NoteText;
        private string m_QuickLinksConfig;
        private string m_Theme;
        private string m_WebPartSetting;

        public AdminProfileInfo()
        {
        }

        public AdminProfileInfo(bool isNull)
        {
            base.IsNull = isNull;
        }

        [XmlIgnore]
        public string AdminName
        {
            get
            {
                return this.m_AdminName;
            }
            set
            {
                this.m_AdminName = value;
            }
        }

        public string NoteText
        {
            get
            {
                return this.m_NoteText;
            }
            set
            {
                this.m_NoteText = value;
            }
        }

        public string QuickLinksConfig
        {
            get
            {
                return this.m_QuickLinksConfig;
            }
            set
            {
                this.m_QuickLinksConfig = value;
            }
        }

        public string Theme
        {
            get
            {
                return this.m_Theme;
            }
            set
            {
                this.m_Theme = value;
            }
        }

        [XmlIgnore]
        public string WebPartSetting
        {
            get
            {
                return this.m_WebPartSetting;
            }
            set
            {
                this.m_WebPartSetting = value;
            }
        }
    }
}

