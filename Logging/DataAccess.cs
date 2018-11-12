namespace EasyOne.Logging
{
    using System;
    using System.Reflection;

    public sealed class DataAccess
    {
        private static readonly string path = System.Web.Configuration.WebConfigurationManager.AppSettings["WebDAL"];

        private DataAccess()
        {
        }

        public static ILogManager CreateLogManager()
        {
            string typeName = path + ".Accessories.LogManager";
            return (ILogManager) Assembly.Load(path).CreateInstance(typeName);
        }
    }
}

