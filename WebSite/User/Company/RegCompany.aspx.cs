namespace EasyOne.WebSite.User
{
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class RegCompany : DynamicPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SiteConfig.UserConfig.EnableRegCompany)
            {
                DynamicPage.WriteErrMsg("系统禁用了企业注册功能，不能注册！");
            }
        }

        public string CompanyName
        {
            get
            {
                return this.TxtCompanyName.Text;
            }
        }
    }
}

