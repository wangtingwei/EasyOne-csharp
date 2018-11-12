namespace EasyOne.WebSite.Controls
{
    using EasyOne.Components;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class SelectAgent : BaseUserControl
    {
        protected bool m_IsAdminDir = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = base.Request.Params.Get("__EVENTTARGET");
            string str2 = base.Request.Params.Get("__EVENTARGUMENT");
            if (str == "AgentList_PostBack")
            {
                this.AgentName = str2;
            }
        }

        public string AgentName
        {
            get
            {
                return this.HdnAgentName.Value;
            }
            set
            {
                this.TxtAgentName.Text = value;
                this.HdnAgentName.Value = value;
            }
        }

        public bool IsAdminDir
        {
            get
            {
                return this.m_IsAdminDir;
            }
            set
            {
                this.m_IsAdminDir = value;
            }
        }

        protected string ManageDir
        {
            get
            {
                if (this.IsAdminDir)
                {
                    string manageDir = SiteConfig.SiteOption.ManageDir;
                    return (base.BasePath + (string.IsNullOrEmpty("adminDir") ? "admin" : manageDir) + "/");
                }
                return base.BasePath;
            }
        }
    }
}

