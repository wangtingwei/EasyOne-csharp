namespace EasyOne.Components.CacheDependency.Configuration
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using System;
    using System.Configuration;

    internal class CacheDependencySettings : SerializableConfigurationSection
    {
        private const string queueProviderProperty = "cacheProviders";
        public const string SectionName = "sqlCacheConfiguration";

        [ConfigurationProperty("cacheProviders", IsRequired=true)]
        public NameTypeConfigurationElementCollection<CacheDependencyData, CacheDependencyData> QueueManageProviders
        {
            get
            {
                return (NameTypeConfigurationElementCollection<CacheDependencyData, CacheDependencyData>) base["cacheProviders"];
            }
        }
    }
}

