namespace EasyOne.Components.CacheDependency
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder;
    using EasyOne.Components.CacheDependency.Configuration;
    using System;
    using System.Web.Caching;
    /// <summary>
    /// SqlCacheDependency 类监视特定 SQL Server 数据库表。当该表更改时，将从 Cache 中移除与该表关联的项，并向 Cache 中添加该项的新版本
    /// </summary>
    [CustomFactory(typeof(CacheDependencyCustomFactory)), ConfigurationElementType(typeof(CacheDependencyData))]
    public class SqlCacheDependency : ICacheDependency, IDisposable
    {
        private char[] configurationSeparator = new char[] { ',' };
        private AggregateCacheDependency dependency = new AggregateCacheDependency();
        private string strDatabase = string.Empty;
        private string strTable = string.Empty;

        public SqlCacheDependency(string database, string tables)
        {
            this.strDatabase = database;
            this.strTable = tables;
        }

        public AggregateCacheDependency Dependency()
        {
            foreach (string str in this.strTable.Split(this.configurationSeparator))
            {
                this.dependency.Add(new CacheDependency[] { new System.Web.Caching.SqlCacheDependency(this.strDatabase, str) });
            }
            return this.dependency;
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.dependency.Dispose();
            }
        }
        /// <summary>
        /// 只读属性DataBase-----------代表关联的数据库
        /// </summary>
        public string Database
        {
            get
            {
                return this.strDatabase;
            }
        }
        /// <summary>
        /// 只读属性Table----------表示数据库中的表
        /// </summary>
        public string Table
        {
            get
            {
                return this.strTable;
            }
        }
    }
}

