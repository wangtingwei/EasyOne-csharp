namespace EasyOne.Components
{
    using EasyOne.Enumerations;
    using EasyOne.Logging;
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using System.Web;

    [Serializable]
    public class CustomException : Exception
    {
        private DateTime m_DateCreated;
        private DateTime m_DateLastOccurred;
        private int m_ExceptionId;
        private PEExceptionType m_ExceptionType;
        private int m_Frequency;
        private string m_HttpPathAndQuery;
        private string m_HttpReferrer;
        private string m_HttpVerb;
        private string m_IpAddress;
        private string m_StackTrace;
        private string m_UserAgent;

        public CustomException()
        {
            this.m_UserAgent = string.Empty;
            this.m_IpAddress = string.Empty;
            this.m_HttpReferrer = string.Empty;
            this.m_HttpVerb = string.Empty;
            this.m_HttpPathAndQuery = string.Empty;
            this.m_StackTrace = string.Empty;
        }

        public CustomException(PEExceptionType exceptionType)
        {
            this.m_UserAgent = string.Empty;
            this.m_IpAddress = string.Empty;
            this.m_HttpReferrer = string.Empty;
            this.m_HttpVerb = string.Empty;
            this.m_HttpPathAndQuery = string.Empty;
            this.m_StackTrace = string.Empty;
            this.Init();
            this.m_ExceptionType = exceptionType;
        }

        public CustomException(string message) : base(message)
        {
            this.m_UserAgent = string.Empty;
            this.m_IpAddress = string.Empty;
            this.m_HttpReferrer = string.Empty;
            this.m_HttpVerb = string.Empty;
            this.m_HttpPathAndQuery = string.Empty;
            this.m_StackTrace = string.Empty;
        }

        public CustomException(PEExceptionType exceptionType, string message) : base(message)
        {
            this.m_UserAgent = string.Empty;
            this.m_IpAddress = string.Empty;
            this.m_HttpReferrer = string.Empty;
            this.m_HttpVerb = string.Empty;
            this.m_HttpPathAndQuery = string.Empty;
            this.m_StackTrace = string.Empty;
            this.Init();
            this.m_ExceptionType = exceptionType;
        }

        protected CustomException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.m_UserAgent = string.Empty;
            this.m_IpAddress = string.Empty;
            this.m_HttpReferrer = string.Empty;
            this.m_HttpVerb = string.Empty;
            this.m_HttpPathAndQuery = string.Empty;
            this.m_StackTrace = string.Empty;
            this.Init();
        }

        public CustomException(string message, Exception innerException) : base(message, innerException)
        {
            this.m_UserAgent = string.Empty;
            this.m_IpAddress = string.Empty;
            this.m_HttpReferrer = string.Empty;
            this.m_HttpVerb = string.Empty;
            this.m_HttpPathAndQuery = string.Empty;
            this.m_StackTrace = string.Empty;
        }

        public CustomException(PEExceptionType exceptionType, string message, Exception inner) : base(message, inner)
        {
            this.m_UserAgent = string.Empty;
            this.m_IpAddress = string.Empty;
            this.m_HttpReferrer = string.Empty;
            this.m_HttpVerb = string.Empty;
            this.m_HttpPathAndQuery = string.Empty;
            this.m_StackTrace = string.Empty;
            this.Init();
            this.m_ExceptionType = exceptionType;
        }

        public override int GetHashCode()
        {
            return (this.m_ExceptionType + base.Message).GetHashCode();
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter=true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("baseValue", "");
            base.GetObjectData(info, context);
        }

        private void Init()
        {
            try
            {
                HttpContext current = HttpContext.Current;
                if ((current != null) && (current.Request != null))
                {
                    if (current.Request.UrlReferrer != null)
                    {
                        this.m_HttpReferrer = current.Request.UrlReferrer.ToString();
                    }
                    if (current.Request.UserAgent != null)
                    {
                        this.m_UserAgent = current.Request.UserAgent;
                    }
                    if (PEContext.Current.UserHostAddress != null)
                    {
                        this.m_IpAddress = PEContext.Current.UserHostAddress;
                    }
                    try
                    {
                        if (current.Request.RequestType != null)
                        {
                            this.m_HttpVerb = current.Request.RequestType;
                        }
                    }
                    catch (Exception)
                    {
                    }
                    if ((current.Request.Url != null) && (current.Request.Url.PathAndQuery != null))
                    {
                        this.m_HttpPathAndQuery = current.Request.Url.PathAndQuery;
                    }
                    if ((current.Request.UrlReferrer != null) && (current.Request.UrlReferrer.PathAndQuery != null))
                    {
                        this.m_HttpReferrer = current.Request.UrlReferrer.PathAndQuery;
                    }
                }
            }
            catch
            {
            }
        }

        public void Log()
        {
            LogInfo info = new LogInfo();
            info.Message = this.Message;
            info.Category = LogCategory.Exception;
            info.Source = string.Concat(new object[] { "SOURCE: ", base.Source, "\r\nFORM: ", HttpContext.Current.Request.Form.ToString(), "\r\nQUERYSTRING: ", HttpContext.Current.Request.QueryString.ToString(), "\r\nTARGETSITE: ", base.TargetSite, "\r\nSTACKTRACE: ", base.StackTrace });
            info.Timestamp = DateTime.Now;
            info.Title = info.Message.Substring(0, 0x1c);
            info.UserIP = this.m_IpAddress;
            info.UserName = PEContext.Current.Admin.AdminName;
            info.ScriptName = this.m_HttpReferrer;
            info.PostString = this.m_HttpPathAndQuery;
            info.Priority = LogPriority.High;
            this.Log(info);
        }

        public void Log(LogInfo info)
        {
            LogFactory.CreateLog().Add(info);
        }

        public static void ThrowBllException(string errorMessage)
        {
            throw new CustomException(PEExceptionType.BllError, errorMessage);
        }

        public int Category
        {
            get
            {
                return (int) this.m_ExceptionType;
            }
            set
            {
                this.m_ExceptionType = (PEExceptionType) value;
            }
        }

        public DateTime DateCreated
        {
            get
            {
                return this.m_DateCreated;
            }
            set
            {
                this.m_DateCreated = value;
            }
        }

        public DateTime DateLastOccurred
        {
            get
            {
                return this.m_DateLastOccurred;
            }
            set
            {
                this.m_DateLastOccurred = value;
            }
        }

        public int ExceptionId
        {
            get
            {
                return this.m_ExceptionId;
            }
            set
            {
                this.m_ExceptionId = value;
            }
        }

        public PEExceptionType ExceptionType
        {
            get
            {
                return this.m_ExceptionType;
            }
        }

        public int Frequency
        {
            get
            {
                return this.m_Frequency;
            }
            set
            {
                this.m_Frequency = value;
            }
        }

        public string HttpPathAndQuery
        {
            get
            {
                return this.m_HttpPathAndQuery;
            }
            set
            {
                this.m_HttpPathAndQuery = value;
            }
        }

        public string HttpReferrer
        {
            get
            {
                return this.m_HttpReferrer;
            }
            set
            {
                this.m_HttpReferrer = value;
            }
        }

        public string HttpVerb
        {
            get
            {
                return this.m_HttpVerb;
            }
            set
            {
                this.m_HttpVerb = value;
            }
        }

        public string IPAddress
        {
            get
            {
                return this.m_IpAddress;
            }
            set
            {
                this.m_IpAddress = value;
            }
        }

        public string LoggedStackTrace
        {
            get
            {
                return this.m_StackTrace;
            }
            set
            {
                this.m_StackTrace = value;
            }
        }

        public override string Message
        {
            get
            {
                switch (this.m_ExceptionType)
                {
                    case PEExceptionType.NoSuchUser:
                        return string.Format(ResourceManager.GetString("Exception_NoSuchUser"), base.Message);

                    case PEExceptionType.PasswordNotMatch:
                        return string.Format(ResourceManager.GetString("Exception_PasswordNotMatch"), base.Message);

                    case PEExceptionType.LockedUser:
                        return string.Format(ResourceManager.GetString("Exception_LockedUser"), base.Message);

                    case PEExceptionType.ConnectionFalse:
                        return ResourceManager.GetString("Exception_ConnectionFalse");

                    case PEExceptionType.SameCard:
                        return ResourceManager.GetString("Exception_SameCard");

                    case PEExceptionType.NotenoughMoney:
                        return string.Format(ResourceManager.GetString("Exception_NotenoughMoney"), base.Message);

                    case PEExceptionType.ExceedAuthority:
                        return ResourceManager.GetString("ExceedAuthority");

                    case PEExceptionType.RefreshedError:
                        return ResourceManager.GetString("Exception_RefreshedError");
                }
                return base.Message;
            }
        }

        public string UserAgent
        {
            get
            {
                return this.m_UserAgent;
            }
            set
            {
                this.m_UserAgent = value;
            }
        }
    }
}

