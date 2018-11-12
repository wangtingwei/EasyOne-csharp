namespace EasyOne.Components.CacheDependency.Configuration
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder;
    using Microsoft.Practices.ObjectBuilder;
    using EasyOne.Components.CacheDependency;
    using System;

    internal class CacheDependencyAssembler : IAssembler<SqlCacheDependency, CacheDependencyData>
    {
        SqlCacheDependency IAssembler<SqlCacheDependency, CacheDependencyData>.Assemble(IBuilderContext context, CacheDependencyData objectConfiguration, IConfigurationSource configurationSource, ConfigurationReflectionCache reflectionCache)
        {
            return new SqlCacheDependency(objectConfiguration.Database, objectConfiguration.Table);
        }
    }
}

