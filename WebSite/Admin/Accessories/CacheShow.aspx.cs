namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using System;
    using System.Collections;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class CacheShow : AdminPage
    {
        protected Label LblTitle;
        protected ExtendedSiteMapPath SmpNavigator;
        protected TextBox TxtCacheContent;

        protected void Page_Load(object sender, EventArgs e)
        {
            string key = BasePage.RequestString("CacheKey");
            object obj2 = SiteCache.Get(key);
            if (obj2 == null)
            {
                AdminPage.WriteErrMsg("<li>未找到对应的缓存信息！</li>");
            }
            if (obj2.GetType().IsGenericType)
            {
                IList list = (IList) obj2;
                StringBuilder sb = new StringBuilder();
                foreach (object obj3 in list)
                {
                    StringHelper.AppendString(sb, obj3.ToString(), "\n");
                }
                this.TxtCacheContent.Text = sb.ToString();
            }
            else
            {
                this.TxtCacheContent.Text = obj2.ToString();
            }
            this.LblTitle.Text = key;
        }
    }
}

