namespace EasyOne.Web.Configuration
{
    using System;
    using System.Configuration;
    using System.Globalization;

    public sealed class PermissionsPageElement : ConfigurationElement
    {
        private static readonly ConfigurationProperty _CheckType = new ConfigurationProperty("checkType", typeof(string), "or");
        private static readonly ConfigurationProperty _OperateCode = new ConfigurationProperty("operateCode", typeof(string), "", ConfigurationPropertyOptions.IsRequired);
        private static readonly ConfigurationProperty _Pageurl = new ConfigurationProperty("url", typeof(string), "", ConfigurationPropertyOptions.IsKey | ConfigurationPropertyOptions.IsRequired);
        private static ConfigurationPropertyCollection _Properties = new ConfigurationPropertyCollection();

        public PermissionsPageElement()
        {
            _Properties.Add(_Pageurl);
            _Properties.Add(_OperateCode);
            _Properties.Add(_CheckType);
        }

        public PermissionsPageElement(string elementName)
        {
            this.Pageurl = elementName;
        }

        [ConfigurationProperty("checkType")]
        public string CheckType
        {
            get
            {
                return ((string) base[_CheckType]).ToLower(CultureInfo.CurrentCulture);
            }
            set
            {
                base[_CheckType] = value;
            }
        }

        [ConfigurationProperty("operateCode", IsRequired=true)]
        public string OperateCode
        {
            get
            {
                return ((string) base[_OperateCode]).ToLower(CultureInfo.CurrentCulture);
            }
            set
            {
                base[_OperateCode] = value;
            }
        }

        [ConfigurationProperty("url", IsKey=true, IsRequired=true)]
        public string Pageurl
        {
            get
            {
                return ((string) base[_Pageurl]).ToLower(CultureInfo.CurrentCulture);
            }
            set
            {
                base[_Pageurl] = value;
            }
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                return _Properties;
            }
        }
    }
}

