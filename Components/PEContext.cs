namespace EasyOne.Components
{
    using EasyOne.Common;
    using EasyOne.Enumerations;
    using System;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Threading;
    using System.Web;

    public class PEContext
    {
        private Uri currentUrl;
        private const string dataKey = "PEContextStore";
        private string hostPath;
        private HttpContext httpContext;
        private bool isUrlRewritten;
        private AdminPrincipal m_Admin;
        private UserPrincipal m_User;
        private NameValueCollection queryString;
        private string returnurl;
        private string rewriteUrlName;
        private string siteurl;

        private PEContext(HttpContext context, bool includeQS)
        {
            this.httpContext = context;
            if (includeQS)
            {
                this.Initialize(new NameValueCollection(context.Request.QueryString), context.Request.Url, context.Request.RawUrl, "");
            }
            else
            {
                this.Initialize(null, context.Request.Url, context.Request.RawUrl, SiteConfig.SiteInfo.SiteUrl);
            }
        }

        public static PEContext Create(HttpContext context)
        {
            return Create(context, false);
        }

        public static PEContext Create(HttpContext context, bool isRewritten)
        {
            PEContext context2 = new PEContext(context, true);
            context2.IsUrlRewritten = isRewritten;
            SaveContextToStore(context2);
            return context2;
        }

        public static string EnumToHtml<T>(T enumValue) where T: struct
        {
            string resourceKey = enumValue.GetType().Name + "_" + enumValue.ToString();
            return GetGlobalString("EnumResources", resourceKey);
        }

        public static string GetGlobalString(string classKey, string resourceKey)
        {
            string str = (string) HttpContext.GetGlobalResourceObject(classKey, resourceKey, CultureInfo.CurrentCulture);
            if (str == null)
            {
                str = string.Empty;
            }
            return str;
        }

        private static LocalDataStoreSlot GetSlot()
        {
            return Thread.GetNamedDataSlot("PEContextStore");
        }

        private void Initialize(NameValueCollection qs, Uri uri, string rawUrl, string siteUri)
        {
            this.queryString = qs;
            this.currentUrl = uri;
            this.rewriteUrlName = rawUrl;
            this.siteurl = siteUri;
        }

        private static void SaveContextToStore(PEContext context)
        {
            if (context.IsWebRequest)
            {
                context.Context.Items["PEContextStore"] = context;
            }
            else
            {
                Thread.SetData(GetSlot(), context);
            }
        }

        public static void Unload()
        {
            Thread.FreeNamedDataSlot("PEContextStore");
        }

        public AdminPrincipal Admin
        {
            get
            {
                if (this.m_Admin == null)
                {
                    this.m_Admin = new AdminPrincipal(new NoAuthenticateIdentity(), null);
                }
                return this.m_Admin;
            }
            set
            {
                this.m_Admin = value;
            }
        }

        public HttpContext Context
        {
            get
            {
                return this.httpContext;
            }
        }

        public static PEContext Current
        {
            get
            {
                HttpContext current = HttpContext.Current;
                PEContext context = null;
                if (current != null)
                {
                    context = current.Items["PEContextStore"] as PEContext;
                }
                else
                {
                    context = Thread.GetData(GetSlot()) as PEContext;
                }
                if (context == null)
                {
                    if (current == null)
                    {
                        throw new CustomException(PEExceptionType.NullHttpContext, "No PEContext exists in the Current Application. AutoCreate fails since HttpContext.Current is not accessible.");
                    }
                    context = new PEContext(current, true);
                    SaveContextToStore(context);
                }
                return context;
            }
        }

        public Uri CurrentUri
        {
            get
            {
                if (this.currentUrl == null)
                {
                    this.currentUrl = new Uri("http://localhost/");
                }
                return this.currentUrl;
            }
            set
            {
                this.currentUrl = value;
            }
        }

        public string HostPath
        {
            get
            {
                if (this.hostPath == null)
                {
                    string str = (this.CurrentUri.Port == 80) ? string.Empty : (":" + this.CurrentUri.Port.ToString());
                    this.hostPath = string.Format("{0}://{1}{2}", this.CurrentUri.Scheme, this.CurrentUri.Host, str);
                }
                return this.hostPath;
            }
        }

        public bool IsUrlRewritten
        {
            get
            {
                return this.isUrlRewritten;
            }
            set
            {
                this.isUrlRewritten = value;
            }
        }

        public bool IsWebRequest
        {
            get
            {
                return (this.Context != null);
            }
        }

        public NameValueCollection QueryString
        {
            get
            {
                return this.queryString;
            }
        }

        public string Rawurl
        {
            get
            {
                return this.rewriteUrlName;
            }
            set
            {
                this.rewriteUrlName = value;
            }
        }

        public string Returnurl
        {
            get
            {
                if (this.returnurl == null)
                {
                    this.returnurl = this.QueryString["returnUrl"];
                }
                return this.returnurl;
            }
            set
            {
                this.returnurl = value;
            }
        }

        public string Siteurl
        {
            get
            {
                return this.siteurl;
            }
        }

        public UserPrincipal User
        {
            get
            {
                if (this.m_User == null)
                {
                    this.m_User = new UserPrincipal(new NoAuthenticateIdentity());
                }
                return this.m_User;
            }
            set
            {
                this.m_User = value;
            }
        }

        public string UserHostAddress
        {
            get
            {
                if (DataValidator.IsIP(this.Context.Request.UserHostAddress))
                {
                    return this.Context.Request.UserHostAddress;
                }
                return "0.0.0.0";
            }
        }
    }
}

