namespace EasyOne.Web.HttpModule
{
    using EasyOne.Components;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.UI;

    public class RefreshModule : IHttpModule
    {
        private static Queue<Guid> guids;
        private const string HIDDEN_FIELD_ID = "__REFRESH_FIELD";
        private static int queueSize = 100;

        private static void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            HttpApplication application = sender as HttpApplication;
            if (application != null)
            {
                BasePage currentHandler = application.Context.CurrentHandler as BasePage;
                if ((currentHandler != null) && currentHandler.IsCheckRefreshed)
                {
                    currentHandler.Init += new EventHandler(RefreshModule.Page_Init);
                }
            }
        }

        public void Dispose()
        {
        }

        private static Guid GetPageGuid(Page page)
        {
            string str = page.Request.Form["__REFRESH_FIELD"];
            if (string.IsNullOrEmpty(str))
            {
                return Guid.Empty;
            }
            return new Guid(str);
        }

        public void Init(HttpApplication context)
        {
            guids = new Queue<Guid>(queueSize);
            context.PreRequestHandlerExecute += new EventHandler(RefreshModule.Application_PreRequestHandlerExecute);
        }

        private static void Page_Init(object sender, EventArgs e)
        {
            BasePage page = sender as BasePage;
            if (page != null)
            {
                page.ClientScript.RegisterClientScriptResource(typeof(RefreshModule), "EasyOne.Web.HttpModule.RefreshModule.js");
                page.ClientScript.RegisterOnSubmitStatement(typeof(RefreshModule), "onsubmit", "refreshModule.createPageIdentifier()");
                page.ClientScript.RegisterHiddenField("__REFRESH_FIELD", "");
                HttpContext.Current.Items["IsRefreshed"] = false;
                if (page.Request.HttpMethod == "POST")
                {
                    Guid pageGuid = GetPageGuid(page);
                    bool flag = guids.Contains(pageGuid);
                    HttpContext.Current.Items["IsRefreshed"] = flag;
                    if (flag)
                    {
                        page.OnRefreshed(e);
                    }
                    if (!flag && (pageGuid != Guid.Empty))
                    {
                        guids.Enqueue(pageGuid);
                        if (guids.Count > SiteConfig.SiteOption.RefreshQueueSize)
                        {
                            guids.Dequeue();
                        }
                    }
                }
            }
        }

        public static Queue<Guid> Guids
        {
            get
            {
                return guids;
            }
        }
    }
}

