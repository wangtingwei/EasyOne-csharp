namespace EasyOne.Model.TemplateProc
{
    using EasyOne.Model;
    using System;
    using System.Collections.Specialized;

    public class TemplateInfo : EasyOne.Model.Nullable
    {
        private int m_CurrentPage;
        private int m_Id;
        private bool m_IsDynamicPage;
        private int m_NodeId;
        private string m_PageName;
        private int m_PageNum;
        private int m_PageType;
        private NameValueCollection m_QueryList;
        private string m_RootPath;
        private string m_SiteUrl;
        private string m_TemplateContent;
        private int m_TemplateType;
        private int m_TotalPub;
        /// <summary>
        /// 构造方法初始化
        /// </summary>
        public TemplateInfo()
        {
            this.m_CurrentPage = 1;
            this.m_PageNum = 1;
        }

        public TemplateInfo(bool value)
        {
            this.m_CurrentPage = 1;
            this.m_PageNum = 1;
            base.IsNull = value;
        }

        public TemplateInfo(int id, int nodeid, int templatetype, NameValueCollection querylist, string templatecontent, int currentpage, int totalpub, int pagenum, string rootpath, string pagename)
        {
            this.m_CurrentPage = 1;
            this.m_PageNum = 1;
            this.m_Id = id;
            this.m_NodeId = nodeid;
            this.m_TemplateType = templatetype;
            this.m_QueryList = querylist;
            this.m_TemplateContent = templatecontent;
            this.m_CurrentPage = currentpage;
            this.m_TotalPub = totalpub;
            this.m_PageNum = pagenum;
            this.m_RootPath = rootpath;
            this.m_PageName = pagename;
        }
        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrentPage
        {
            get
            {
                return this.m_CurrentPage;
            }
            set
            {
                if (value < 1)
                {
                    this.m_CurrentPage = 1;
                }
                else
                {
                    this.m_CurrentPage = value;
                }
            }
        }

        public int Id
        {
            get
            {
                return this.m_Id;
            }
            set
            {
                this.m_Id = value;
            }
        }
        /// <summary>
        /// 是否为动态页
        /// </summary>
        public bool IsDynamicPage
        {
            get
            {
                return this.m_IsDynamicPage;
            }
            set
            {
                this.m_IsDynamicPage = value;
            }
        }
        /// <summary>
        ///  节点ID
        /// </summary>
        public int NodeId
        {
            get
            {
                return this.m_NodeId;
            }
            set
            {
                this.m_NodeId = value;
            }
        }
        /// <summary>
        /// 页面名称
        /// </summary>
        public string PageName
        {
            get
            {
                return this.m_PageName;
            }
            set
            {
                this.m_PageName = value;
            }
        }
        /// <summary>
        ///  页面总数
        /// </summary>
        public int PageNum
        {
            get
            {
                return this.m_PageNum;
            }
            set
            {
                this.m_PageNum = value;
            }
        }
        /// <summary>
        ///  页面类型
        /// </summary>
        public int PageType
        {
            get
            {
                return this.m_PageType;
            }
            set
            {
                this.m_PageType = value;
            }
        }
        /// <summary>
        /// 查询参数
        /// </summary>
        public NameValueCollection QueryList
        {
            get
            {
                return this.m_QueryList;
            }
            set
            {
                this.m_QueryList = value;
            }
        }
        /// <summary>
        ///  根目录
        /// </summary>
        public string RootPath
        {
            get
            {
                return this.m_RootPath;
            }
            set
            {
                this.m_RootPath = value;
            }
        }
        /// <summary>
        ///  网站URL地址
        /// </summary>
        public string SiteUrl
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
        /// <summary>
        ///  模板内容
        /// </summary>
        public string TemplateContent
        {
            get{ return this.m_TemplateContent;}
            set{ this.m_TemplateContent = value;}
        }
        /// <summary>
        ///  模板类型
        /// </summary>
        public int TemplateType
        {
            get{ return this.m_TemplateType;}
            set{ this.m_TemplateType = value;}
        }

        public int TotalPub
        {
            get{ return this.m_TotalPub;}
            set{ this.m_TotalPub = value;}
        }
    }
}

