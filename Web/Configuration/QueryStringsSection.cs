namespace EasyOne.Web.Configuration
{
    using System;
    using System.Configuration;

    public sealed class QueryStringsSection : ConfigurationSection
    {
        private static readonly ConfigurationProperty _Mode = new ConfigurationProperty("mode", typeof(QueryStringsMode), QueryStringsMode.All, ConfigurationPropertyOptions.IsRequired);
        private static readonly ConfigurationProperty _Page = new ConfigurationProperty(null, typeof(PageElementCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
        private static ConfigurationPropertyCollection _Properties = new ConfigurationPropertyCollection();

        public QueryStringsSection()
        {
            _Properties.Add(_Mode);
            _Properties.Add(_Page);
        }

        [ConfigurationProperty("mode", DefaultValue=0, IsRequired=true)]
        public string Mode
        {
            get
            {
                return (string) base[_Mode];
            }
            set
            {
                base[_Mode] = value;
            }
        }
        /// <summary>
        /// 设置自定义属性Page,指定此属性为集合
        /// </summary>
        [ConfigurationProperty("page", IsDefaultCollection=true)]
        public PageElementCollection Page
        {
            get
            {
                return (PageElementCollection) base[_Page];
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

