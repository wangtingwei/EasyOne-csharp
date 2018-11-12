namespace EasyOne.Components.CacheDependency
{
    using EasyOne.Components.CacheDependency.Configuration;
    using System;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

    public static class CacheDependencyFactory
    {
        private static CacheDependencyInstanceFactory factory = new CacheDependencyInstanceFactory(ConfigurationSourceFactory.Create("Cache"));

        public static SqlCacheDependency CreateDependency(string dependencyName)
        {
            return factory.Create(dependencyName);
        }
    }
}

