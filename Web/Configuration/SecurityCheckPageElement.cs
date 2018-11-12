namespace EasyOne.Web.Configuration
{
    using System;
    using System.Configuration;
    using System.Globalization;

    public sealed class SecurityCheckPageElement : ConfigurationElement
    {
        private static readonly ConfigurationProperty _Pageurl = new ConfigurationProperty("url", typeof(string), "", ConfigurationPropertyOptions.IsKey | ConfigurationPropertyOptions.IsRequired);
        private static ConfigurationPropertyCollection _Properties = new ConfigurationPropertyCollection();

        public SecurityCheckPageElement()
        {
            _Properties.Add(_Pageurl);
        }

        public SecurityCheckPageElement(string elementName)
        {
            this.Pageurl = elementName;
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

