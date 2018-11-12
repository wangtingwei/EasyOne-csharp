namespace EasyOne.Model.Contents
{
    using EasyOne.Enumerations;
    using EasyOne.Model;
    using System;

    public class SpecialInfo : EasyOne.Model.Nullable, IComparable<SpecialInfo>
    {
        private string m_Custom_Content;
        private string m_Description;
        private bool m_IsCreateListPage;
        private bool m_IsElite;
        private string m_ListPagePostfix;
        private ListPagePathType m_ListPageSavePathType;
        private bool m_NeedCreateHtml;
        private int m_OpenType;
        private int m_OrderId;
        private string m_SearchTemplatePath;
        private int m_SpecialCategoryId;
        private string m_SpecialDir;
        private int m_SpecialId;
        private string m_SpecialIdentifier;
        private string m_SpecialName;
        private string m_SpecialPic;
        private string m_SpecialTemplatePath;
        private string m_SpecialTips;

        public SpecialInfo()
        {
        }

        public SpecialInfo(bool value)
        {
            base.IsNull = value;
        }

        public int CompareTo(SpecialInfo other)
        {
            return this.m_OrderId.CompareTo(other.m_OrderId);
        }

        private static string GetRootPath(string path)
        {
            string[] strArray = path.Split(new char[] { '/' });
            if (strArray.Length > 0)
            {
                return strArray[0];
            }
            return "/";
        }

        public string ListHtmlPagePath(string pageIndex)
        {
            string specialDir = this.m_SpecialDir;
            switch (this.m_ListPageSavePathType)
            {
                case ListPagePathType.NodePath:
                    if (!string.IsNullOrEmpty(pageIndex))
                    {
                        return (specialDir + "/List_" + pageIndex + "." + this.m_ListPagePostfix);
                    }
                    return (specialDir + "/Index." + this.m_ListPagePostfix);

                case ListPagePathType.ListPath:
                    if (!string.IsNullOrEmpty(pageIndex))
                    {
                        return string.Concat(new object[] { GetRootPath(specialDir), "/List/List_", this.m_SpecialId, "_", pageIndex, ".", this.m_ListPagePostfix });
                    }
                    return string.Concat(new object[] { GetRootPath(specialDir), "/List/List_", this.m_SpecialId, ".", this.m_ListPagePostfix });

                case ListPagePathType.RootPath:
                    if (!string.IsNullOrEmpty(pageIndex))
                    {
                        return string.Concat(new object[] { "Special/List_", this.m_SpecialId, "_", pageIndex, ".", this.m_ListPagePostfix });
                    }
                    return string.Concat(new object[] { "Special/List_", this.m_SpecialId, ".", this.m_ListPagePostfix });
            }
            return specialDir;
        }

        public string CustomContent
        {
            get
            {
                return this.m_Custom_Content;
            }
            set
            {
                this.m_Custom_Content = value;
            }
        }

        public string Description
        {
            get
            {
                return this.m_Description;
            }
            set
            {
                this.m_Description = value;
            }
        }

        public bool IsCreateListPage
        {
            get
            {
                return this.m_IsCreateListPage;
            }
            set
            {
                this.m_IsCreateListPage = value;
            }
        }

        public bool IsElite
        {
            get
            {
                return this.m_IsElite;
            }
            set
            {
                this.m_IsElite = value;
            }
        }

        public string ListPagePostfix
        {
            get
            {
                return this.m_ListPagePostfix;
            }
            set
            {
                this.m_ListPagePostfix = value;
            }
        }

        public ListPagePathType ListPageSavePathType
        {
            get
            {
                return this.m_ListPageSavePathType;
            }
            set
            {
                this.m_ListPageSavePathType = value;
            }
        }

        public bool NeedCreateHtml
        {
            get
            {
                return this.m_NeedCreateHtml;
            }
            set
            {
                this.m_NeedCreateHtml = value;
            }
        }

        public int OpenType
        {
            get
            {
                return this.m_OpenType;
            }
            set
            {
                this.m_OpenType = value;
            }
        }

        public int OrderId
        {
            get
            {
                return this.m_OrderId;
            }
            set
            {
                this.m_OrderId = value;
            }
        }

        public string SearchTemplatePath
        {
            get
            {
                return this.m_SearchTemplatePath;
            }
            set
            {
                this.m_SearchTemplatePath = value;
            }
        }

        public int SpecialCategoryId
        {
            get
            {
                return this.m_SpecialCategoryId;
            }
            set
            {
                this.m_SpecialCategoryId = value;
            }
        }

        public string SpecialDir
        {
            get
            {
                return this.m_SpecialDir;
            }
            set
            {
                this.m_SpecialDir = value;
            }
        }

        public int SpecialId
        {
            get
            {
                return this.m_SpecialId;
            }
            set
            {
                this.m_SpecialId = value;
            }
        }

        public string SpecialIdentifier
        {
            get
            {
                return this.m_SpecialIdentifier;
            }
            set
            {
                this.m_SpecialIdentifier = value;
            }
        }

        public string SpecialName
        {
            get
            {
                return this.m_SpecialName;
            }
            set
            {
                this.m_SpecialName = value;
            }
        }

        public string SpecialPic
        {
            get
            {
                return this.m_SpecialPic;
            }
            set
            {
                this.m_SpecialPic = value;
            }
        }

        public string SpecialTemplatePath
        {
            get
            {
                return this.m_SpecialTemplatePath;
            }
            set
            {
                this.m_SpecialTemplatePath = value;
            }
        }

        public string SpecialTips
        {
            get
            {
                return this.m_SpecialTips;
            }
            set
            {
                this.m_SpecialTips = value;
            }
        }
    }
}

