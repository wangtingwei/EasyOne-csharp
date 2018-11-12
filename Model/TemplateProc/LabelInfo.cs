namespace EasyOne.Model.TemplateProc
{
    using EasyOne.Model;
    using System;
    using System.Collections.Specialized;
    using System.Text;

    public class LabelInfo : EasyOne.Model.Nullable
    {
        private NameValueCollection m_AttributesData;
        private int m_Error;
        private StringBuilder m_LabelContent;
        private NameValueCollection m_LabelDefineData;
        private NameValueCollection m_OriginalData;
        private int m_Page;
        private string m_PageName;
        private int m_PageSize;
        private string m_RootPath;
        private int m_TotalPub;

        public LabelInfo()
        {
            this.m_Page = 1;
            this.m_OriginalData = new NameValueCollection();
            this.m_LabelDefineData = new NameValueCollection();
            this.m_AttributesData = new NameValueCollection();
            this.m_LabelContent = new StringBuilder();
        }

        public LabelInfo(bool value)
        {
            this.m_Page = 1;
            this.m_OriginalData = new NameValueCollection();
            this.m_LabelDefineData = new NameValueCollection();
            this.m_AttributesData = new NameValueCollection();
            this.m_LabelContent = new StringBuilder();
            base.IsNull = value;
        }

        public LabelInfo(int page, string rootpath, string pagename, StringBuilder labelcontent, int totalpub, int pagesize)
        {
            this.m_Page = 1;
            this.m_OriginalData = new NameValueCollection();
            this.m_LabelDefineData = new NameValueCollection();
            this.m_AttributesData = new NameValueCollection();
            this.m_LabelContent = new StringBuilder();
            this.m_Page = page;
            this.m_RootPath = rootpath;
            this.m_PageName = pagename;
            this.m_LabelContent = labelcontent;
            this.m_TotalPub = totalpub;
            this.m_PageSize = pagesize;
        }
        /// <summary>
        /// 标签属性集合
        /// </summary>
        public NameValueCollection AttributesData
        {
            get
            {
                return this.m_AttributesData;
            }
        }
        /// <summary>
        /// 标签错误代号
        /// </summary>
        public int Error
        {
            get
            {
                return this.m_Error;
            }
            set
            {
                this.m_Error = value;
            }
        }
        /// <summary>
        /// 标签内容可变字符串
        /// </summary>
        public StringBuilder LabelContent
        {
            get
            {
                return this.m_LabelContent;
            }
            set
            {
                this.m_LabelContent = value;
            }
        }
        /// <summary>
        /// 标签定义数据集合
        /// </summary>
        public NameValueCollection LabelDefineData
        {
            get
            {
                return this.m_LabelDefineData;
            }
        }
        /// <summary>
        /// 标签原始数据集合
        /// </summary>
        public NameValueCollection OriginalData
        {
            get
            {
                return this.m_OriginalData;
            }
        }
        /// <summary>
        /// 当前页码
        /// </summary>
        public int Page
        {
            get
            {
                return this.m_Page;
            }
            set
            {
                this.m_Page = value;
            }
        }

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
        /// 页数据大小
        /// </summary>
        public int PageSize
        {
            get
            {
                return this.m_PageSize;
            }
            set
            {
                this.m_PageSize = value;
            }
        }
        /// <summary>
        /// 标签根路径
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
        /// 总记录条数
        /// </summary>
        public int TotalPub
        {
            get
            {
                return this.m_TotalPub;
            }
            set
            {
                this.m_TotalPub = value;
            }
        }
    }
}

