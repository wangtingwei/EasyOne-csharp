namespace EasyOne.Web.Configuration
{
    using System;
    using System.Configuration;

    public sealed class CheckSecurityCodeElement : ConfigurationElement
    {
        private static readonly ConfigurationProperty _Page = new ConfigurationProperty(null, typeof(SecurityCheckPageElementCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
        private static ConfigurationPropertyCollection _Properties = new ConfigurationPropertyCollection();

        public CheckSecurityCodeElement()
        {
            _Properties.Add(_Page);
        }

        [ConfigurationProperty("page", IsDefaultCollection=true)]
        public SecurityCheckPageElementCollection Page
        {
            get
            {
                return (SecurityCheckPageElementCollection) base[_Page];
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

