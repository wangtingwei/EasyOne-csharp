namespace EasyOne.Web.Configuration
{
    using System;
    using System.Configuration;

    public sealed class CheckPermissionsElement : ConfigurationElement
    {
        private static readonly ConfigurationProperty _Mode = new ConfigurationProperty("mode", typeof(NoCheckType), NoCheckType.OnlyList, ConfigurationPropertyOptions.IsRequired);
        private static readonly ConfigurationProperty _Page = new ConfigurationProperty(null, typeof(PermissionsPageElementCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
        private static ConfigurationPropertyCollection _Properties = new ConfigurationPropertyCollection();

        public CheckPermissionsElement()
        {
            _Properties.Add(_Page);
            _Properties.Add(_Mode);
        }

        [ConfigurationProperty("mode", DefaultValue=1, IsRequired=true)]
        public NoCheckType Mode
        {
            get
            {
                return (NoCheckType) base[_Mode];
            }
            set
            {
                base[_Mode] = value;
            }
        }

        [ConfigurationProperty("page", IsDefaultCollection=true)]
        public PermissionsPageElementCollection Page
        {
            get
            {
                return (PermissionsPageElementCollection) base[_Page];
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

