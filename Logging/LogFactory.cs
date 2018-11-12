namespace EasyOne.Logging
{
    using System;
    using System.Reflection;
    using System.Web.Configuration;

    public sealed class LogFactory
    {
        private static readonly string path = WebConfigurationManager.AppSettings["LogFactoryName"];

        private LogFactory()
        {
        }

        public static ILog CreateLog()
        {
            string typeName = "EasyOne.Logging." + path;
            return (ILog) Assembly.Load("EasyOne.Logging").CreateInstance(typeName);
        }

        public static ILog CreateLog(LogType logType)
        {
            switch (logType)
            {
                case LogType.System:
                    return new DBLog();

                case LogType.Files:
                    return new DBLog();
            }
            return new DBLog();
        }
    }
}

