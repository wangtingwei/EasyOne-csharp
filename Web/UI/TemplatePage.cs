namespace EasyOne.Web.UI
{
    using EasyOne.Components;
    using EasyOne.Model.TemplateProc;
    using EasyOne.Templates;
    using EasyOne.Web;
    using System;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Web;

    public class TemplatePage : BasePage
    {
        private static readonly object EventInitTemplateInfo = new object();
        private static readonly object EventInitTemplatePage = new object();
        private string m_CacheKey;
        private int m_CacheTime;
        private bool m_IsCache;
        private EasyOne.Model.TemplateProc.TemplateInfo m_TemplateInfo;
        /// <summary>
        /// 声明模板基类数据初始化事件
        /// </summary>
        public event EventHandler InitTemplateInfo
        {
            add{base.Events.AddHandler(EventInitTemplateInfo, value);}
            remove{base.Events.RemoveHandler(EventInitTemplateInfo, value);}
        }
        /// <summary>
        /// 声明模板页初始化事件
        /// </summary>
        public event EventHandler InitTemplatePage
        {
            add{base.Events.AddHandler(EventInitTemplatePage, value);}
            remove{base.Events.RemoveHandler(EventInitTemplatePage, value);}
        }
        /// <summary>
        /// 获取动态模板路径
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <returns></returns>
        public static string GetDynamicConfigTemplatePath(string fileName)
        {
            string str = string.Empty;
            string str2 = string.Empty;
            foreach (FrontTemplate template in SiteConfig.FrontTemplateList)
            {
                if (string.Compare(fileName, template.Key, true, CultureInfo.CurrentCulture) == 0)
                {
                    str = template.Value;
                }
                else if (string.Compare("DynamicPageDefault", template.Key, true, CultureInfo.CurrentCulture) == 0)
                {
                    str2 = template.Value;
                }
            }
            if (string.IsNullOrEmpty(str))
            {
                return str2;
            }
            return str;
        }

        public virtual void OnInitTemplateInfo(EventArgs e)
        {
            EventHandler handler = base.Events[EventInitTemplateInfo] as EventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public virtual void OnInitTemplatePage(EventArgs e)
        {
            EventHandler handler = base.Events[EventInitTemplatePage] as EventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        /// <summary>
        /// 输出模板解析后的内容
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            string templateContent;
            base.OnPreInit(e);
            this.OnInitTemplatePage(EventArgs.Empty);
            //if ((this.IsCache && (this.CacheTime > 0)) && (SiteCache.Get(this.CacheKey) != null))
            //{
                //templateContent = (string) SiteCache.Get(this.CacheKey);
            //}
            //else
            //{
            int start= Environment.TickCount; 

                this.OnInitTemplateInfo(EventArgs.Empty);
                templateContent = TemplateTransform.GetHtml(this.TemplateInfo).TemplateContent;
                //if (this.IsCache && (this.CacheTime > 0))
                //{
                    //SiteCache.Insert(this.CacheKey, templateContent, this.CacheTime);
                //}
            //}
            Response.Write("模板解析时间："+(Environment.TickCount-start).ToString()+"毫秒");
            base.Response.Write(templateContent);
            
            base.Response.End();
        }
        /// <summary>
        /// 重新构建页面名称
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="querylist"></param>
        /// <returns></returns>
        public static string RebuildPageName(string fileName, NameValueCollection querylist)
        {
            return Utility.RebuildPageName(fileName, querylist);
        }

        public static void WriteErrMsg(string errorMessage)
        {
            WriteErrMsg(errorMessage, string.Empty);
        }

        public static void WriteErrMsg(string errorMessage, string returnurl)
        {
            HttpContext.Current.Items["ErrorMessage"] = errorMessage;
            HttpContext.Current.Items["ReturnUrl"] = returnurl;
            HttpContext.Current.Server.Transfer("~/Prompt/ShowError.aspx");
        }

        public static void WriteMessage(string message)
        {
            WriteMessage(message, string.Empty, string.Empty);
        }

        public static void WriteMessage(string message, string returnurl)
        {
            WriteMessage(message, returnurl, string.Empty);
        }

        public static void WriteMessage(string message, string returnurl, string messageTitle)
        {
            Utility.WriteMessage(message, returnurl, messageTitle);
        }

        public static void WriteSuccessMsg(string successMessage)
        {
            WriteSuccessMsg(successMessage, string.Empty);
        }

        public static void WriteSuccessMsg(string successMessage, string returnurl)
        {
            HttpContext.Current.Items["SuccessMessage"] = successMessage;
            HttpContext.Current.Items["ReturnUrl"] = returnurl;
            HttpContext.Current.Server.Transfer("~/Prompt/ShowSuccess.aspx");
        }

        public static void WriteUserErrMsg(string errorMessage)
        {
            WriteUserErrMsg(errorMessage, string.Empty);
        }

        public static void WriteUserErrMsg(string errorMessage, string returnurl)
        {
            HttpContext.Current.Items["ErrorMessage"] = errorMessage;
            HttpContext.Current.Items["ReturnUrl"] = returnurl;
            HttpContext.Current.Server.Transfer("~/Prompt/ShowError.aspx?Action=User");
        }

        public static void WriteUserSuccessMsg(string successMessage)
        {
            WriteUserSuccessMsg(successMessage, string.Empty);
        }
        /// <summary>
        /// 写成功消息
        /// </summary>
        /// <param name="successMessage">消息内容</param>
        /// <param name="returnurl">返回URL地址</param>
        public static void WriteUserSuccessMsg(string successMessage, string returnurl)
        {
            HttpContext.Current.Items["SuccessMessage"] = successMessage;
            HttpContext.Current.Items["ReturnUrl"] = returnurl;
            HttpContext.Current.Server.Transfer("~/Prompt/ShowSuccess.aspx?Action=User");
        }

        public string CacheKey
        {
            get
            {
                return this.m_CacheKey;
            }
            set
            {
                this.m_CacheKey = value;
            }
        }

        public int CacheTime
        {
            get
            {
                return this.m_CacheTime;
            }
            set
            {
                this.m_CacheTime = value;
            }
        }

        public bool IsCache
        {
            get
            {
                return this.m_IsCache;
            }
            set
            {
                this.m_IsCache = value;
            }
        }
        /// <summary>
        /// 模板信息属性
        /// </summary>
        public EasyOne.Model.TemplateProc.TemplateInfo TemplateInfo
        {
            get
            {   return this.m_TemplateInfo;}
            set
            {   this.m_TemplateInfo = value;}
        }
    }
}

