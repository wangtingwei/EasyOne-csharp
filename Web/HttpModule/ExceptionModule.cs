namespace EasyOne.Web.HttpModule
{
    using EasyOne.Components;
    using EasyOne.Enumerations;
    using System;
    using System.Data.SqlClient;
    using System.Text;
    using System.Web;
    using System.Web.Configuration;
    /// <summary>
    /// 异常处理模块类
    /// </summary>
    public class ExceptionModule : IHttpModule
    {
        private void Application_OnError(object source, EventArgs e)
        {
            HttpApplication application = (HttpApplication) source;
            HttpContext context = application.Context;
            CustomException lastError = context.Server.GetLastError() as CustomException;
            if (lastError == null)
            {
                lastError = context.Server.GetLastError().GetBaseException() as CustomException;
            }
            try
            {
                if (lastError != null)
                {
                    switch (lastError.ExceptionType)
                    {
                        case PEExceptionType.NoSuchUser:
                        case PEExceptionType.PasswordNotMatch:
                        case PEExceptionType.LockedUser:
                        case PEExceptionType.ConnectionFalse:
                        case PEExceptionType.SameCard:
                        case PEExceptionType.NotenoughMoney:
                            lastError.Log();
                            goto Label_01E0;
                            
                    }
                }
                else
                {
                    Exception innerException = context.Server.GetLastError();
                    if (innerException.InnerException != null)
                    {
                        innerException = innerException.InnerException;
                    }
                    if (innerException is HttpException)
                    {
                        HttpException exception3 = (HttpException) innerException;
                        if (exception3.GetHttpCode() == 0x194)
                        {
                            return;
                        }
                    }
                    SqlException exception4 = innerException as SqlException;
                    if (exception4 == null)
                    {
                        lastError = new CustomException(PEExceptionType.UnknownError, innerException.Message, context.Server.GetLastError());
                        lastError.Log();
                    }
                    else
                    {
                        StringBuilder builder = new StringBuilder();
                        for (int i = 0; i < exception4.Errors.Count; i++)
                        {
                            builder.Append(string.Concat(new object[] { "Index #", i, "\nMessage: ", exception4.Errors[i].Message, "\nLineNumber: ", exception4.Errors[i].LineNumber, "\nSource: ", exception4.Errors[i].Source, "\nProcedure: ", exception4.Errors[i].Procedure, "\n" }));
                        }
                        lastError = new CustomException(PEExceptionType.UnknownError, builder.ToString());
                    }
                }
            }
            catch
            {
            }
        Label_01E0:
            if (((lastError != null) && (lastError.ExceptionType == PEExceptionType.BllError)) || (CompilationDebug() != CustomErrorsMode.Off))
            {
                PEEvents.PEException(lastError);
            }
        }

        private static CustomErrorsMode CompilationDebug()
        {
            string currentExecutionFilePath = HttpContext.Current.Request.CurrentExecutionFilePath;
            CustomErrorsSection section = WebConfigurationManager.OpenWebConfiguration(currentExecutionFilePath.Substring(0, currentExecutionFilePath.LastIndexOf("/", StringComparison.CurrentCultureIgnoreCase))).GetSection("system.web/customErrors") as CustomErrorsSection;
            return section.Mode;
        }

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            //当引发未处理的异常时发生的事件
            context.Error += new EventHandler(this.Application_OnError);
        }
    }
}

