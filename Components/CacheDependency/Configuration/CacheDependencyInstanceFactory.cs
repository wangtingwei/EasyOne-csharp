namespace EasyOne.Components.CacheDependency.Configuration
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder;
    using System;

    internal class CacheDependencyInstanceFactory : NameTypeFactoryBase<SqlCacheDependency>
    {
        public CacheDependencyInstanceFactory(IConfigurationSource configurationSource) : base(configurationSource)
        {
        }
    }
}

