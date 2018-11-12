namespace EasyOne.StaticHtml
{
    using EasyOne.Common;
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Threading;
    using System.Web;
    using System.Xml;

    public class Jobs
    {
        private DateTime m_Completed;
        private DateTime m_Created = DateTime.Now;
        private static int m_InstancesOfParent;
        private int m_Interval = 0xdbba0;
        private bool m_IsRunning;
        private Hashtable m_JobList = new Hashtable();
        private static readonly Jobs m_Jobs = new Jobs();
        private Timer m_SingleTimer;
        private DateTime m_Started;

        private Jobs()
        {
        }

        private void call_back(object state)
        {
            this.m_IsRunning = true;
            this.m_Started = DateTime.Now;
            this.m_SingleTimer.Change(-1, -1);
            foreach (Job job in this.m_JobList.Values)
            {
                if (job.Enabled && job.SingleThreaded)
                {
                    job.ExecuteJob();
                }
            }
            this.m_SingleTimer.Change(this.m_Interval, this.m_Interval);
            this.m_IsRunning = false;
            this.m_Completed = DateTime.Now;
        }

        public static Jobs Instance()
        {
            return m_Jobs;
        }

        public bool IsJobEnabled(string jobName)
        {
            if (!this.m_JobList.Contains(jobName))
            {
                return false;
            }
            return ((Job) this.m_JobList[jobName]).Enabled;
        }

        public void Start()
        {
            Interlocked.Increment(ref m_InstancesOfParent);
            lock (this.m_JobList.SyncRoot)
            {
                if (this.m_JobList.Count == 0)
                {
                    string filename = HttpContext.Current.Server.MapPath("~/Config/AutoCreate.config");
                    XmlDocument document = new XmlDocument();
                    document.Load(filename);
                    XmlNode node = document.SelectSingleNode("Jobs");
                    if (node != null)
                    {
                        bool flag = true;
                        XmlAttribute attribute1 = node.Attributes["singleThread"];
                        XmlAttribute attribute = node.Attributes["minutes"];
                        this.m_Interval = DataConverter.CLng(attribute.Value) * 0xea60;
                        string str2 = HttpContext.Current.Request.Url.Authority + VirtualPathUtility.AppendTrailingSlash(HttpContext.Current.Request.ApplicationPath);
                        string physicalApplicationPath = HttpContext.Current.Request.PhysicalApplicationPath;
                        foreach (XmlNode node2 in node.ChildNodes)
                        {
                            XmlAttribute attribute2 = node2.Attributes["type"];
                            XmlAttribute attribute3 = node2.Attributes["name"];
                            XmlElement element = (XmlElement) node2;
                            element.SetAttribute("siteMap", physicalApplicationPath);
                            element.SetAttribute("siteUrl", str2);
                            Type jobType = Type.GetType(attribute2.Value);
                            if ((jobType != null) && !this.m_JobList.Contains(attribute3.Value))
                            {
                                Job job = new Job(jobType, node2);
                                this.m_JobList[attribute3.Value] = job;
                                if (!flag || !job.SingleThreaded)
                                {
                                    job.InitializeTimer();
                                }
                            }
                        }
                        if (flag)
                        {
                            this.m_SingleTimer = new Timer(new TimerCallback(this.call_back), null, this.m_Interval, this.m_Interval);
                        }
                    }
                }
            }
        }

        public void Stop()
        {
            Interlocked.Decrement(ref m_InstancesOfParent);
            if ((m_InstancesOfParent <= 0) && (this.m_JobList != null))
            {
                lock (this.m_JobList.SyncRoot)
                {
                    foreach (Job job in this.m_JobList.Values)
                    {
                        job.Dispose();
                    }
                    this.m_JobList.Clear();
                    if (this.m_SingleTimer != null)
                    {
                        this.m_SingleTimer.Dispose();
                        this.m_SingleTimer = null;
                    }
                }
            }
        }

        public override string ToString()
        {
            return string.Format("Created: {0}, LastStart: {1}, LastStop: {2}, IsRunning: {3}, Minutes: {4}", new object[] { this.m_Created, this.m_Started, this.m_Completed, this.m_IsRunning, this.m_Interval / 0xea60 });
        }

        public Hashtable CurrentJobs
        {
            get
            {
                return this.m_JobList;
            }
        }

        public ListDictionary CurrentStats
        {
            get
            {
                ListDictionary dictionary = new ListDictionary();
                dictionary.Add("Created", this.m_Created);
                dictionary.Add("LastStart", this.m_Started);
                dictionary.Add("LastStop", this.m_Completed);
                dictionary.Add("IsRunning", this.m_IsRunning);
                dictionary.Add("Minutes", this.m_Interval / 0xea60);
                return dictionary;
            }
        }
    }
}

