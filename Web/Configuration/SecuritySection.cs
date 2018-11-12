namespace EasyOne.Web.Configuration
{
    using System;
    using System.Configuration;

    public sealed class SecuritySection : ConfigurationSection
    {
        private static readonly ConfigurationProperty _CheckPermissions = new ConfigurationProperty("checkPermissions", typeof(CheckPermissionsElement), null);
        private static readonly ConfigurationProperty _CheckSecurityCode = new ConfigurationProperty("checkSecurityCode", typeof(CheckSecurityCodeElement), null);
        private static readonly ConfigurationProperty _NoCheckAdminLogOn = new ConfigurationProperty("noCheckAdminLogOn", typeof(NoCheckAdminLogOnElement), null);
        private static readonly ConfigurationProperty _NoCheckUrlReferrer = new ConfigurationProperty("noCheckUrlReferrer", typeof(NoCheckUrlReferrerElement), null);
        private static ConfigurationPropertyCollection _Properties = new ConfigurationPropertyCollection();

        public SecuritySection()
        {
            _Properties.Add(_NoCheckAdminLogOn);
            _Properties.Add(_NoCheckUrlReferrer);
            _Properties.Add(_CheckSecurityCode);
            _Properties.Add(_CheckPermissions);
        }

        public CheckPermissionsElement CheckPermissions
        {
            get
            {
                return (CheckPermissionsElement) base[_CheckPermissions];
            }
        }

        public CheckSecurityCodeElement CheckSecurityCode
        {
            get
            {
                return (CheckSecurityCodeElement) base[_CheckSecurityCode];
            }
        }

        public NoCheckAdminLogOnElement NoCheckAdminLogOn
        {
            get
            {
                return (NoCheckAdminLogOnElement) base[_NoCheckAdminLogOn];
            }
        }

        public NoCheckUrlReferrerElement NoCheckUrlReferrer
        {
            get
            {
                return (NoCheckUrlReferrerElement) base[_NoCheckUrlReferrer];
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

