namespace EasyOne.Components.CacheDependency.Configuration
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder;
    using System;

    internal class CacheDependencyCustomFactory : AssemblerBasedCustomFactory<SqlCacheDependency, CacheDependencyData>
    {
        protected override CacheDependencyData GetConfiguration(string name, IConfigurationSource configurationSource)
        {
            CacheDependencySettings section = (CacheDependencySettings) configurationSource.GetSection("sqlCacheConfiguration");
            return section.QueueManageProviders.Get(name);
        }
    }
}

