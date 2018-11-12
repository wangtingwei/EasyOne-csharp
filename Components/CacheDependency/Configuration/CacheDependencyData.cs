namespace EasyOne.Components.CacheDependency.Configuration
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder;
    using System;
    using System.Configuration;

    [Assembler(typeof(CacheDependencyAssembler))]
    internal class CacheDependencyData : NameTypeConfigurationElement
    {
        private const string dataBaseProperty = "database";
        private const string dataTableProperty = "table";

        private static void Settings()
        {
            new CacheDependencySettings().IsReadOnly();
        }

        [ConfigurationProperty("database", IsRequired=true)]
        public string Database
        {
            get
            {
                return (string) base["database"];
            }
            set
            {
                base["database"] = value;
            }
        }

        [ConfigurationProperty("table", IsRequired=true)]
        public string Table
        {
            get
            {
                return (string) base["table"];
            }
            set
            {
                base["table"] = value;
            }
        }
    }
}

