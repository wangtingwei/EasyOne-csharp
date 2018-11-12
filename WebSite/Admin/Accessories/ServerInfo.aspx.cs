namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using System;
    using System.Data;
    using System.Diagnostics;
    using System.IO;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class ServerInfo : AdminPage, ICallbackEventHandler
    {
        private string checkResult;


        private bool CheckObject(string objName)
        {
            try
            {
                if (base.Server.CreateObject(objName) != null)
                {
                    return true;
                }
                return false;
            }
            catch (HttpException exception)
            {
                this.DropException(exception.Message);
                return false;
            }
        }

        private void DropException(string message)
        {
        }

        public virtual string GetCallbackResult()
        {
            return this.checkResult;
        }

        private double GetDirectorySize(string path, bool countSubDirectory)
        {
            double num = 0.0;
            DirectoryInfo info = new DirectoryInfo(path);
            foreach (FileInfo info2 in info.GetFiles())
            {
                num += info2.Length;
            }
            if (countSubDirectory && (info.GetDirectories().Length > 0))
            {
                foreach (DirectoryInfo info3 in info.GetDirectories())
                {
                    num += this.GetDirectorySize(info3.FullName, true);
                }
            }
            return (num / 1024.0);
        }

        public string IsObjectInstalled(string objId)
        {
            if (!this.CheckObject(objId))
            {
                return "\x00d7";
            }
            return "√";
        }

        protected void Page_Load(object sender, EventArgs args)
        {
            if (!base.IsPostBack && !base.IsCallback)
            {
                this.WriteGeneralInfo();
                this.RptServerVeriable.DataSource = this.ServerInfoTable();
                this.RptServerVeriable.DataBind();
                this.RptSpaceUsage.DataSource = this.SiteSpaceUsage();
                this.RptSpaceUsage.DataBind();
            }
            this.BtnCheckObject.Attributes["onclick"] = string.Format("javascript:{0}", "CallServer(objToCheck.value, '')");
        }

        public virtual void RaiseCallbackEvent(string eventArgument)
        {
            if (this.CheckObject(eventArgument))
            {
                this.checkResult = "<strong>检测结果：</strong>当前服务器支持组件 <span style=\"font-weight:bold;color:green;\">" + eventArgument + "</span>";
            }
            else
            {
                this.checkResult = "<strong>检测结果：</strong>当前服务器不支持组建 <span style=\"font-weight:bold;color:gray;\">" + eventArgument + "</span>";
            }
        }

        private DataTable ServerInfoTable()
        {
            DataTable table = new DataTable("veriables");
            table.Columns.Add("key", Type.GetType("System.String"));
            table.Columns.Add("value", Type.GetType("System.String"));
            foreach (string str in base.Request.ServerVariables.AllKeys)
            {
                table.Rows.Add(new object[] { str, base.Request.ServerVariables[str] });
            }
            return table;
        }

        private DataTable SiteSpaceUsage()
        {
            DataTable table = new DataTable("spaces");
            table.Columns.Add("Name", Type.GetType("System.String"));
            table.Columns.Add("SpaceUsage", Type.GetType("System.String"));
            string appDomainAppPath = HttpRuntime.AppDomainAppPath;
            double num = 0.0;
            foreach (DirectoryInfo info in new DirectoryInfo(appDomainAppPath).GetDirectories())
            {
                double directorySize = this.GetDirectorySize(info.FullName, true);
                table.Rows.Add(new object[] { HttpRuntime.AppDomainAppVirtualPath + "/" + info.Name, directorySize.ToString("0.00K") });
                num += directorySize;
            }
            object[] values = new object[] { "系统空间总占用", (num / 1024.0).ToString("0.00M") };
            table.Rows.Add(values);
            return table;
        }

        private void WriteGeneralInfo()
        {
            this.LblServerName.Text = Environment.MachineName;
            this.LblSiteDomain.Text = base.Request.ServerVariables["SERVER_NAME"];
            this.LblSiteIP.Text = base.Request.ServerVariables["LOCAL_ADDR"];
            this.LblSitePort.Text = base.Request.ServerVariables["SERVER_PORT"];
            this.LblOsVersion.Text = Environment.OSVersion.VersionString;
            this.LblServerSoft.Text = base.Request.ServerVariables["SERVER_SOFTWARE"] + "(.NET CLR v" + Environment.Version.ToString() + ")";
            string str = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).ToString();
            if (str.StartsWith("-", StringComparison.Ordinal))
            {
                str = str.Substring(0, str.Length - 3);
            }
            else
            {
                str = "+" + str.Substring(0, str.Length - 3);
            }
            this.LblTimeZone.Text = "GMT " + str;
            this.LblNowTime.Text = DateTime.Now.ToString();
            try
            {
                this.LblCpuType.Text = Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER");
                this.LblCpuNumber.Text = Environment.ProcessorCount.ToString();
            }
            catch (NullReferenceException exception)
            {
                this.DropException(exception.Message);
            }
            this.LblScriptTimeout.Text = base.Server.ScriptTimeout.ToString();
            try
            {
                this.LblSiteMemory.Text = (((double) Process.GetCurrentProcess().WorkingSet64) / 1048576.0).ToString("N2");
            }
            catch (NullReferenceException exception2)
            {
                this.DropException(exception2.Message);
            }
            this.LabelVitualPath.Text = HttpRuntime.AppDomainAppVirtualPath;
            this.LblScriptPath.Text = base.Request.Path;
            this.LblScriptPhysicalPath.Text = base.Request.PhysicalPath;
            this.LblApplicationCount.Text = base.Application.Count.ToString();
            this.LblSessionCount.Text = this.Session.Count.ToString();
        }
    }
}

