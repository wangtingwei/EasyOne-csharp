namespace EasyOne.WebSite.Admin
{
    using EasyOne.ExtendedControls;
    using EasyOne.Controls;
    using EasyOne.ModelControls;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls.WebPart;
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
using System.Web.Services;
    using System.Web.Script.Services;
    using System.ComponentModel;
    using EasyOne.Components;

    public partial class MyWorktable : AdminPage
    {


        protected void LnkBrowseDisplayMode_Click(object sender, EventArgs e)
        {
            this.WpmMyWorktable.DisplayMode = WebPartManager.BrowseDisplayMode;
            this.LitHelp.Text = "";
        }

        protected void LnkCatalogDisplayMode_Click(object sender, EventArgs e)
        {
            this.WpmMyWorktable.DisplayMode = WebPartManager.CatalogDisplayMode;
            this.LitHelp.Text = "<span style=\"color:Red\">添加新模块</span>";
        }

        protected void LnkDesignDisplayMode_Click(object sender, EventArgs e)
        {
            this.WpmMyWorktable.DisplayMode = WebPartManager.DesignDisplayMode;
            this.LitHelp.Text = "<span style=\"color:Red\">一直单击某模块标题进行拖动到你需要的位置</span>";
        }

        protected void LnkEditDisplayMode_Click(object sender, EventArgs e)
        {
            this.WpmMyWorktable.DisplayMode = WebPartManager.EditDisplayMode;
            this.LitHelp.Text = "<span style=\"color:Red\">点击模块标题上的菜单，并选择编辑来编辑此模块的属性</span>";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.WpmMyWorktable.DisplayMode = WebPartManager.BrowseDisplayMode;
            }
        }
    }
    [WebService(Namespace = "http://tempuri.org/"), ScriptService, WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1), ToolboxItem(false)]
    public class GetSystemDiagnostics : WebService
    {
        [WebMethod]
        public SystemDiagnosticsInfo SystemDiagnostics()
        {
            SystemDiagnosticsInfo info = new SystemDiagnosticsInfo();
            WebSystemDiagnostics diagnostics = new WebSystemDiagnostics();
            info.CpuUseRatio = diagnostics.CpuUseRatio;
            info.Caches = WebSystemDiagnostics.Caches;
            info.Runtime = diagnostics.Runtime;
            info.ScriptTimeOut = WebSystemDiagnostics.ScriptTimeOut;
            info.SessionCount = WebSystemDiagnostics.SessionCount;
            info.StartTime = diagnostics.StartTime;
            info.Memory = diagnostics.Memory;
            return info;
        }
    }
    public class SystemDiagnosticsInfo
    {
        private string m_Caches;
        private string m_CpuUseRatio;
        private string m_Memory;
        private string m_Runtime;
        private string m_ScriptTimeOut;
        private string m_SessionCount;
        private string m_StartTime;

        public string Caches
        {
            get
            {
                return this.m_Caches;
            }
            set
            {
                this.m_Caches = value;
            }
        }

        public string CpuUseRatio
        {
            get
            {
                return this.m_CpuUseRatio;
            }
            set
            {
                this.m_CpuUseRatio = value;
            }
        }

        public string Memory
        {
            get
            {
                return this.m_Memory;
            }
            set
            {
                this.m_Memory = value;
            }
        }

        public string Runtime
        {
            get
            {
                return this.m_Runtime;
            }
            set
            {
                this.m_Runtime = value;
            }
        }

        public string ScriptTimeOut
        {
            get
            {
                return this.m_ScriptTimeOut;
            }
            set
            {
                this.m_ScriptTimeOut = value;
            }
        }

        public string SessionCount
        {
            get
            {
                return this.m_SessionCount;
            }
            set
            {
                this.m_SessionCount = value;
            }
        }

        public string StartTime
        {
            get
            {
                return this.m_StartTime;
            }
            set
            {
                this.m_StartTime = value;
            }
        }
    }
}

