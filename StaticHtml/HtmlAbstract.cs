namespace EasyOne.StaticHtml
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Enumerations;
    using EasyOne.Model.TemplateProc;
    using System;
    using System.IO;
    using System.Threading;
    using System.Web;
    using System.Xml;

    public abstract class HtmlAbstract
    {
        private int m_CreateCompleted;
        private int m_CreateCount;
        private DateTime m_CreateEndTime;
        private DateTime m_CreateErrorTime;
        private bool m_CreateHasNewError;
        private string m_CreateId;
        private string m_CreateMessage;
        private CreateContentType m_CreateMethod;
        private DateTime m_CreateStartTime;
        private int m_CreateStatus;
        private Thread m_CreateThread;
        private string m_CreateUnitName;
        private string m_MapPath;
        private string m_PhysicalApplicationPath;
        private string m_SiteUrl;

        protected HtmlAbstract()
        {
        }

        protected static void AddHeardRunatServer(TemplateInfo templateInfo, string pageName)
        {
            if (Path.GetExtension(pageName).Contains("aspx"))
            {
                templateInfo.TemplateContent = templateInfo.TemplateContent.Replace("<head>", "<head runat=\"server\"></head>");
            }
        }

        public virtual void CommonCreateHtml()
        {
            this.m_CreateId = Guid.NewGuid().ToString();
            if (HttpContext.Current != null)
            {
                this.m_PhysicalApplicationPath = HttpContext.Current.Request.PhysicalApplicationPath;
                this.m_MapPath = VirtualPathUtility.AppendTrailingSlash(HttpContext.Current.Server.MapPath("~/"));
                this.m_SiteUrl = HttpContext.Current.Request.Url.Authority + VirtualPathUtility.AppendTrailingSlash(HttpContext.Current.Request.ApplicationPath);
            }
            this.m_MapPath = this.m_PhysicalApplicationPath;
            try
            {
                string filename = this.m_MapPath + "Config/CreateHtmlWork.config";
                XmlDocument document = new XmlDocument();
                document.Load(filename);
                XmlAttribute attribute = document.CreateAttribute("id");
                attribute.Value = this.m_CreateId;
                XmlNode newChild = document.CreateElement("WorkId");
                newChild.Attributes.Append(attribute);
                XmlNode node2 = document.CreateElement("ErrorTime");
                newChild.AppendChild(node2);
                node2 = document.CreateElement("ErrorMessage");
                newChild.AppendChild(node2);
                document.SelectSingleNode("CreateWork").AppendChild(newChild);
                document.Save(filename);
            }
            catch (FileNotFoundException)
            {
                CustomException.ThrowBllException("CreateHtmlWork.config文件未找到。");
            }
            catch
            {
                CustomException.ThrowBllException("检查您的服务器是否给配置文件CreateHtmlWork.config或文件夹写入权限。");
            }
            this.StartCreate();
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Application[this.m_CreateId] = this;
            }
        }

        public virtual void ErrorLog(string errMsg)
        {
            string file = this.m_MapPath + "Admin/CreateHtmlLog/" + this.CreateId + ".txt";
            if (!FileSystemObject.IsExist(this.m_MapPath + "Admin/CreateHtmlLog", FsoMethod.Folder))
            {
                FileSystemObject.Create(this.m_MapPath + "Admin/CreateHtmlLog", FsoMethod.Folder);
            }
            if (FileSystemObject.IsExist(file, FsoMethod.File))
            {
                FileSystemObject.WriteAppend(file, "\r\n" + errMsg);
            }
            else
            {
                FileSystemObject.Create(file, FsoMethod.File);
                FileSystemObject.WriteFile(file, "生成时间：" + this.CreateStartTime.ToString() + "\r\n" + errMsg);
            }
            this.m_CreateHasNewError = true;
        }

        private void StartCreate()
        {
            this.m_CreateThread = new Thread(new ThreadStart(this.Work));
            this.m_CreateThread.Start();
        }

        public abstract void Work();

        public virtual int CreateCompleted
        {
            get
            {
                return this.m_CreateCompleted;
            }
            set
            {
                this.m_CreateCompleted = value;
            }
        }

        public virtual int CreateCount
        {
            get
            {
                return this.m_CreateCount;
            }
            set
            {
                this.m_CreateCount = value;
            }
        }

        public virtual DateTime CreateEndTime
        {
            get
            {
                return this.m_CreateEndTime;
            }
            set
            {
                this.m_CreateEndTime = value;
            }
        }

        public virtual DateTime CreateErrorTime
        {
            get
            {
                return this.m_CreateErrorTime;
            }
            set
            {
                this.m_CreateErrorTime = value;
            }
        }

        public virtual bool CreateHasNewError
        {
            get
            {
                return this.m_CreateHasNewError;
            }
            set
            {
                this.m_CreateHasNewError = value;
            }
        }

        public virtual string CreateId
        {
            get
            {
                return this.m_CreateId;
            }
            set
            {
                this.m_CreateId = value;
            }
        }

        public virtual string CreateMessage
        {
            get
            {
                return this.m_CreateMessage;
            }
            set
            {
                this.m_CreateMessage = value;
            }
        }

        public CreateContentType CreateMethod
        {
            get
            {
                return this.m_CreateMethod;
            }
            set
            {
                this.m_CreateMethod = value;
            }
        }

        public virtual DateTime CreateStartTime
        {
            get
            {
                return this.m_CreateStartTime;
            }
            set
            {
                this.m_CreateStartTime = value;
            }
        }

        public virtual int CreateStatus
        {
            get
            {
                return this.m_CreateStatus;
            }
            set
            {
                this.m_CreateStatus = value;
            }
        }

        public virtual Thread CreateThread
        {
            get
            {
                return this.m_CreateThread;
            }
        }

        public virtual string CreateUnitName
        {
            get
            {
                return this.m_CreateUnitName;
            }
            set
            {
                this.m_CreateUnitName = value;
            }
        }

        public virtual string PhysicalApplicationPath
        {
            get
            {
                return this.m_PhysicalApplicationPath;
            }
            set
            {
                this.m_PhysicalApplicationPath = value;
            }
        }

        public virtual string SiteUrl
        {
            get
            {
                return this.m_SiteUrl;
            }
            set
            {
                this.m_SiteUrl = value;
            }
        }
    }
}

