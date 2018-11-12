namespace EasyOne.Model.CommonModel
{
    using EasyOne.Enumerations;
    using System;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Xml.Serialization;

    [XmlRoot("FieldInfo")]
    public class FieldInfo
    {
        private string m_DefaultValue;
        private string m_Description;
        private bool m_Disabled;
        private bool m_EnableNull;
        private bool m_EnableShowOnSearchForm;
        private string m_FieldAlias;
        private int m_FieldLevel;
        private string m_FieldName;
        private EasyOne.Enumerations.FieldType m_FieldType;
        private string m_Id;
        private bool m_IsNull;
        private int m_OrderId;
        private Collection<string> m_Settings;
        private string m_Tips;

        public FieldInfo()
        {
            if (this.m_Settings == null)
            {
                this.m_Settings = new Collection<string>();
            }
            this.m_Settings.Clear();
        }

        public FieldInfo(bool value)
        {
            this.m_IsNull = value;
        }

        public void CopyToSettings(Collection<string> settings)
        {
            this.m_Settings = settings;
        }

        public string DefaultValue
        {
            get
            {
                return this.m_DefaultValue;
            }
            set
            {
                this.m_DefaultValue = value;
            }
        }

        public string Description
        {
            get
            {
                return this.m_Description;
            }
            set
            {
                this.m_Description = value;
            }
        }

        [XmlAttribute("Disabled")]
        public bool Disabled
        {
            get
            {
                return this.m_Disabled;
            }
            set
            {
                this.m_Disabled = value;
            }
        }

        public bool EnableNull
        {
            get
            {
                return this.m_EnableNull;
            }
            set
            {
                this.m_EnableNull = value;
            }
        }

        public bool EnableShowOnSearchForm
        {
            get
            {
                return this.m_EnableShowOnSearchForm;
            }
            set
            {
                this.m_EnableShowOnSearchForm = value;
            }
        }

        public string FieldAlias
        {
            get
            {
                return this.m_FieldAlias;
            }
            set
            {
                this.m_FieldAlias = value;
            }
        }

        public int FieldLevel
        {
            get
            {
                return this.m_FieldLevel;
            }
            set
            {
                this.m_FieldLevel = value;
            }
        }

        public string FieldName
        {
            get
            {
                return this.m_FieldName;
            }
            set
            {
                this.m_FieldName = value;
            }
        }

        public EasyOne.Enumerations.FieldType FieldType
        {
            get
            {
                return this.m_FieldType;
            }
            set
            {
                this.m_FieldType = value;
            }
        }

        [XmlAttribute("Id")]
        public string Id
        {
            get
            {
                return this.m_Id;
            }
            set
            {
                this.m_Id = value.ToLower(CultureInfo.CurrentCulture);
            }
        }

        public bool IsNull
        {
            get
            {
                return this.m_IsNull;
            }
        }

        [XmlAttribute("OrderId")]
        public int OrderId
        {
            get
            {
                return this.m_OrderId;
            }
            set
            {
                this.m_OrderId = value;
            }
        }

        public Collection<string> Settings
        {
            get
            {
                return this.m_Settings;
            }
        }

        public string Tips
        {
            get
            {
                return this.m_Tips;
            }
            set
            {
                this.m_Tips = value;
            }
        }
    }
}

