namespace EasyOne.StaticHtml
{
    using EasyOne.Common;
    using EasyOne.Enumerations;
    using System;
    using System.Threading;
    using System.Xml;

    public class Job : IDisposable
    {
        private bool disposed;
        private bool m_CreateIndexPage;
        private bool m_CreateNodePage;
        private bool m_Enabled = true;
        private bool m_EnableShutDown;
        private bool m_IsRunning;
        private HtmlContent m_Job;
        private Type m_JobType;
        private DateTime m_LastEnd;
        private DateTime m_LastStart;
        private DateTime m_LastSucess;
        private int m_Minutes = 15;
        private string m_Name;
        private XmlNode m_Node;
        private string m_NodeArry;
        private int m_Number;
        private int m_Seconds = -1;
        private bool m_SingleThread = true;
        private string m_SiteMapth;
        private string m_SiteUrl;
        private Timer m_Timer;

        public Job(Type jobType, XmlNode node)
        {
            this.m_Node = node;
            this.m_JobType = jobType;
            XmlAttribute attribute = node.Attributes["enabled"];
            if (attribute != null)
            {
                this.m_Enabled = DataConverter.CBoolean(attribute.Value);
            }
            attribute = node.Attributes["enableShutDown"];
            if (attribute != null)
            {
                this.m_EnableShutDown = DataConverter.CBoolean(attribute.Value);
            }
            attribute = node.Attributes["name"];
            if (attribute != null)
            {
                this.m_Name = attribute.Value;
            }
            attribute = node.Attributes["seconds"];
            if (attribute != null)
            {
                this.m_Seconds = DataConverter.CLng(attribute.Value);
            }
            attribute = node.Attributes["createNodePage"];
            if (attribute != null)
            {
                this.m_CreateNodePage = DataConverter.CBoolean(attribute.Value);
            }
            attribute = node.Attributes["createIndexPage"];
            if (attribute != null)
            {
                this.m_CreateIndexPage = DataConverter.CBoolean(attribute.Value);
            }
            attribute = node.Attributes["siteUrl"];
            if (attribute != null)
            {
                this.m_SiteUrl = attribute.Value;
            }
            attribute = node.Attributes["siteMap"];
            if (attribute != null)
            {
                this.m_SiteMapth = attribute.Value;
            }
            attribute = node.Attributes["minutes"];
            if (attribute != null)
            {
                try
                {
                    this.m_Minutes = DataConverter.CLng(attribute.Value);
                }
                catch
                {
                    this.m_Minutes = 15;
                }
            }
            attribute = node.Attributes["number"];
            if (attribute != null)
            {
                this.m_Number = DataConverter.CLng(attribute.Value);
            }
            attribute = node.Attributes["nodeIdArray"];
            if (attribute != null)
            {
                this.m_NodeArry = attribute.Value;
            }
            attribute = node.Attributes["singleThread"];
            if ((attribute != null) && (string.Compare(attribute.Value, "false", StringComparison.OrdinalIgnoreCase) == 0))
            {
                this.m_SingleThread = false;
            }
        }

        public HtmlContent CreateJobInstance()
        {
            if (this.Enabled && (this.m_Job == null))
            {
                if (this.m_JobType != null)
                {
                    this.m_Job = Activator.CreateInstance(this.m_JobType) as HtmlContent;
                }
                this.m_Enabled = this.m_Job != null;
                if (!this.m_Enabled)
                {
                    this.Dispose();
                }
            }
            return this.m_Job;
        }

        public void Dispose()
        {
            if ((this.m_Timer != null) && !this.disposed)
            {
                lock (this)
                {
                    this.m_Timer.Dispose();
                    this.m_Timer = null;
                    this.disposed = true;
                }
            }
        }

        public void ExecuteJob()
        {
            this.m_IsRunning = true;
            HtmlContent content = this.CreateJobInstance();
            if (content != null)
            {
                this.m_LastStart = DateTime.Now;
                try
                {
                    content.CreateMethod = CreateContentType.CreateAuto;
                    content.NodeIdArray = this.m_NodeArry;
                    content.SiteUrl = this.m_SiteUrl;
                    content.PhysicalApplicationPath = this.m_SiteMapth;
                    content.EnableCreateNodePage = this.m_CreateNodePage;
                    content.EnableCreateIndexPage = this.m_CreateIndexPage;
                    content.Work();
                    this.m_LastEnd = this.m_LastSucess = DateTime.Now;
                }
                catch (Exception)
                {
                    this.m_Enabled = !this.EnableShutDown;
                    this.m_LastEnd = DateTime.Now;
                }
            }
            this.m_IsRunning = false;
        }

        public void InitializeTimer()
        {
            if ((this.m_Timer == null) && this.Enabled)
            {
                this.m_Timer = new Timer(new TimerCallback(this.timer_Callback), null, this.Interval, this.Interval);
            }
        }

        private void timer_Callback(object state)
        {
            if (this.Enabled)
            {
                this.m_Timer.Change(-1, -1);
                this.ExecuteJob();
                if (this.Enabled)
                {
                    this.m_Timer.Change(this.Interval, this.Interval);
                }
                else
                {
                    this.Dispose();
                }
            }
        }

        public bool Enabled
        {
            get
            {
                return this.m_Enabled;
            }
        }

        public bool EnableShutDown
        {
            get
            {
                return this.m_EnableShutDown;
            }
        }

        protected int Interval
        {
            get
            {
                if (this.m_Seconds > 0)
                {
                    return (this.m_Seconds * 0x3e8);
                }
                return (this.Minutes * 0xea60);
            }
        }

        public bool IsRunning
        {
            get
            {
                return this.m_IsRunning;
            }
        }

        public Type JobType
        {
            get
            {
                return this.m_JobType;
            }
        }

        public DateTime LastEnd
        {
            get
            {
                return this.m_LastEnd;
            }
        }

        public DateTime LastStarted
        {
            get
            {
                return this.m_LastStart;
            }
        }

        public DateTime LastSuccess
        {
            get
            {
                return this.m_LastSucess;
            }
        }

        public int Minutes
        {
            get
            {
                return this.m_Minutes;
            }
            set
            {
                this.m_Minutes = value;
            }
        }

        public string Name
        {
            get
            {
                return this.m_Name;
            }
        }

        public bool SingleThreaded
        {
            get
            {
                return this.m_SingleThread;
            }
        }
    }
}

