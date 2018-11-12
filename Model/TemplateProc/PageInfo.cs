namespace EasyOne.Model.TemplateProc
{
    using System;

    public class PageInfo
    {
        private int m_CurrentPage;
        private bool m_IsDynamicPage;
        private bool m_IsMainPage;
        private string m_PageName;
        private int m_PageNum;
        private string m_PageOtherSet;
        private int m_PageSize;
        private string m_SpanName;
        private int m_TotalPub;

        public PageInfo()
        {
            this.m_CurrentPage = 1;
            this.m_PageNum = 1;
            this.m_TotalPub = 1;
            this.m_PageOtherSet = string.Empty;
            this.m_PageSize = 1;
        }

        public PageInfo(string pagename, string spanname, int currentpage, int pagenum, int totalpub, bool ismainpage, string pageotherset, int pagesize)
        {
            this.m_CurrentPage = 1;
            this.m_PageNum = 1;
            this.m_TotalPub = 1;
            this.m_PageOtherSet = string.Empty;
            this.m_PageSize = 1;
            this.m_PageName = pagename;
            this.m_SpanName = spanname;
            this.m_CurrentPage = currentpage;
            this.m_PageNum = pagenum;
            this.m_TotalPub = totalpub;
            this.m_IsMainPage = ismainpage;
            this.m_PageOtherSet = pageotherset;
            this.m_PageSize = pagesize;
        }

        public int CurrentPage
        {
            get
            {
                return this.m_CurrentPage;
            }
            set
            {
                this.m_CurrentPage = value;
            }
        }

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

        public bool IsMainPage
        {
            get
            {
                return this.m_IsMainPage;
            }
            set
            {
                this.m_IsMainPage = value;
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

        public string PageOtherSet
        {
            get
            {
                return this.m_PageOtherSet;
            }
            set
            {
                this.m_PageOtherSet = value;
            }
        }

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

        public string SpanName
        {
            get
            {
                return this.m_SpanName;
            }
            set
            {
                this.m_SpanName = value;
            }
        }

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

