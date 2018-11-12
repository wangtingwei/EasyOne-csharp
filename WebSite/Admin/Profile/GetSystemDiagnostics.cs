namespace EasyOne.WebSite.Admin.Profile
{
    using EasyOne.Components;
    using System.ComponentModel;
    using System.Web.Script.Services;
    using System.Web.Services;

    [WebService(Namespace="http://tempuri.org/"), ScriptService, WebServiceBinding(ConformsTo=WsiProfiles.BasicProfile1_1), ToolboxItem(false)]
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

