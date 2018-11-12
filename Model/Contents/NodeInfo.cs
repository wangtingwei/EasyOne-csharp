namespace EasyOne.Model.Contents
{
    using EasyOne.Enumerations;
    using EasyOne.Model;
    using System;
    using System.Web;

    public class NodeInfo : EasyOne.Model.Nullable, IComparable<NodeInfo>
    {
        private string m_ArrChildId;
        private EasyOne.Enumerations.AutoCreateHtmlType m_AutoCreateHtmlType;
        private int m_Child;
        private int m_CommentCount;
        private string m_ContainChildTemplateFile;
        private string m_ContentPageHtmlRule;
        private string m_Creater;
        private string m_CustomContent;
        private string m_DefaultTemplateFile;
        private int m_Depth;
        private string m_Description;
        private int m_HitsOfHot;
        private int m_InheritPurviewFromParent;
        private bool m_IsCreateContentPage;
        private bool m_IsCreateListPage;
        private string m_ItemAspxFileName;
        private int m_ItemChecked;
        private int m_ItemCount;
        private int m_ItemListOrderType;
        private int m_ItemOpenType;
        private int m_ItemPageSize;
        private string m_LinkUrl;
        private string m_ListPageHtmlRule;
        private string m_ListPagePostfix;
        private ListPagePathType m_ListPageSavePathType;
        private string m_MetaDescription;
        private string m_MetaKeywords;
        private bool m_NeedCreateHtml;
        private int m_NextId;
        private string m_NodeDir;
        private int m_NodeId;
        private string m_NodeIdentifier;
        private string m_NodeName;
        private string m_NodePicUrl;
        private EasyOne.Enumerations.NodeType m_NodeType;
        private int m_OpenType;
        private int m_OrderId;
        private int m_OrderType;
        private string m_ParentDir;
        private int m_ParentId;
        private string m_ParentPath;
        private int m_PrevId;
        private int m_PurviewType;
        private string m_RelateNode;
        private string m_RelateSpecial;
        private int m_RootId;
        private NodeSettingInfo m_Settings;
        private bool m_ShowOnListIndex;
        private bool m_ShowOnListParent;
        private bool m_ShowOnMap;
        private bool m_ShowOnMenu;
        private bool m_ShowOnPath;
        private string m_Tips;
        private int m_WorkFlowId;

        public NodeInfo()
        {
        }

        public NodeInfo(bool value)
        {
            base.IsNull = value;
        }

        public int CompareTo(NodeInfo nodeInfo)
        {
            return this.m_OrderId.CompareTo(nodeInfo.m_OrderId);
        }

        private static string GetRootPath(string path)
        {
            string[] strArray = path.Split(new char[] { '/' });
            if (strArray.Length > 0)
            {
                return VirtualPathUtility.AppendTrailingSlash(strArray[1]);
            }
            return "";
        }
        /// <summary>
        /// 根据页面ID号获取页面名称
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public string ListHtmlPageName(string pageIndex)
        {
            string str = string.Empty;
            switch (this.m_ListPageSavePathType)
            {
                case ListPagePathType.NodePath:
                    if (!(pageIndex == "0"))
                    {
                        if (this.m_ParentDir.StartsWith("/", StringComparison.CurrentCultureIgnoreCase))
                        {
                            this.m_ParentDir = this.m_ParentDir.Substring(1, this.m_ParentDir.Length - 1);
                        }
                        return (this.m_ParentDir + this.m_NodeDir + "/List_" + pageIndex + "." + this.m_ListPagePostfix);
                    }
                    if (this.m_ParentDir.StartsWith("/", StringComparison.CurrentCultureIgnoreCase))
                    {
                        this.m_ParentDir = this.m_ParentDir.Substring(1, this.m_ParentDir.Length - 1);
                    }
                    return (this.m_ParentDir + this.m_NodeDir + "/index." + this.m_ListPagePostfix);

                case ListPagePathType.ListPath:
                    if (!(pageIndex == "0"))
                    {
                        return string.Concat(new object[] { GetRootPath(this.m_ParentDir), "List/List_", this.m_NodeId, "_", pageIndex, ".", this.m_ListPagePostfix });
                    }
                    return string.Concat(new object[] { GetRootPath(this.m_ParentDir), "List/List_", this.m_NodeId, ".", this.m_ListPagePostfix });

                case ListPagePathType.RootPath:
                    if (!(pageIndex == "0"))
                    {
                        return string.Concat(new object[] { GetRootPath(this.m_ParentDir), "List_", this.m_NodeId, "_", pageIndex, ".", this.m_ListPagePostfix });
                    }
                    return string.Concat(new object[] { GetRootPath(this.m_ParentDir), "List_", this.m_NodeId, ".", this.m_ListPagePostfix });

                case ListPagePathType.SinglePath:
                    return (this.m_ParentDir + this.m_NodeDir + this.m_ListPageHtmlRule + "." + this.m_ListPagePostfix);
            }
            return str;
        }

        int IComparable<NodeInfo>.CompareTo(NodeInfo nodeInfo)
        {
            if (this.m_OrderType == 0)
            {
                return this.CompareTo(nodeInfo);
            }
            return this.m_RootId.CompareTo(nodeInfo.m_RootId);
        }

        public string ArrChildId
        {
            get
            {
                return this.m_ArrChildId;
            }
            set
            {
                this.m_ArrChildId = value;
            }
        }

        public EasyOne.Enumerations.AutoCreateHtmlType AutoCreateHtmlType
        {
            get
            {
                return this.m_AutoCreateHtmlType;
            }
            set
            {
                this.m_AutoCreateHtmlType = value;
            }
        }

        public int Child
        {
            get
            {
                return this.m_Child;
            }
            set
            {
                this.m_Child = value;
            }
        }

        public int CommentCount
        {
            get
            {
                return this.m_CommentCount;
            }
            set
            {
                this.m_CommentCount = value;
            }
        }

        public string ContainChildTemplateFile
        {
            get
            {
                return this.m_ContainChildTemplateFile;
            }
            set
            {
                this.m_ContainChildTemplateFile = value;
            }
        }

        public string ContentPageHtmlRule
        {
            get
            {
                return this.m_ContentPageHtmlRule;
            }
            set
            {
                this.m_ContentPageHtmlRule = value;
            }
        }

        public string Creater
        {
            get
            {
                return this.m_Creater;
            }
            set
            {
                this.m_Creater = value;
            }
        }

        public string CustomContent
        {
            get
            {
                return this.m_CustomContent;
            }
            set
            {
                this.m_CustomContent = value;
            }
        }

        public string DefaultTemplateFile
        {
            get
            {
                return this.m_DefaultTemplateFile;
            }
            set
            {
                this.m_DefaultTemplateFile = value;
            }
        }

        public int Depth
        {
            get
            {
                return this.m_Depth;
            }
            set
            {
                this.m_Depth = value;
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

        public int HitsOfHot
        {
            get
            {
                return this.m_HitsOfHot;
            }
            set
            {
                this.m_HitsOfHot = value;
            }
        }

        public int InheritPurviewFromParent
        {
            get
            {
                return this.m_InheritPurviewFromParent;
            }
            set
            {
                this.m_InheritPurviewFromParent = value;
            }
        }

        public bool IsCreateContentPage
        {
            get
            {
                return this.m_IsCreateContentPage;
            }
            set
            {
                this.m_IsCreateContentPage = value;
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

        public string ItemAspxFileName
        {
            get
            {
                return this.m_ItemAspxFileName;
            }
            set
            {
                this.m_ItemAspxFileName = value;
            }
        }

        public int ItemChecked
        {
            get
            {
                return this.m_ItemChecked;
            }
            set
            {
                this.m_ItemChecked = value;
            }
        }

        public int ItemCount
        {
            get
            {
                return this.m_ItemCount;
            }
            set
            {
                this.m_ItemCount = value;
            }
        }

        public int ItemListOrderType
        {
            get
            {
                return this.m_ItemListOrderType;
            }
            set
            {
                this.m_ItemListOrderType = value;
            }
        }

        public int ItemOpenType
        {
            get
            {
                return this.m_ItemOpenType;
            }
            set
            {
                this.m_ItemOpenType = value;
            }
        }

        public int ItemPageSize
        {
            get
            {
                return this.m_ItemPageSize;
            }
            set
            {
                this.m_ItemPageSize = value;
            }
        }

        public string LinkUrl
        {
            get
            {
                return this.m_LinkUrl;
            }
            set
            {
                this.m_LinkUrl = value;
            }
        }

        public string ListPageHtmlRule
        {
            get
            {
                return this.m_ListPageHtmlRule;
            }
            set
            {
                this.m_ListPageHtmlRule = value;
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

        public string MetaDescription
        {
            get
            {
                return this.m_MetaDescription;
            }
            set
            {
                this.m_MetaDescription = value;
            }
        }

        public string MetaKeywords
        {
            get
            {
                return this.m_MetaKeywords;
            }
            set
            {
                this.m_MetaKeywords = value;
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

        public int NextId
        {
            get
            {
                return this.m_NextId;
            }
            set
            {
                this.m_NextId = value;
            }
        }

        public string NodeDir
        {
            get
            {
                return this.m_NodeDir;
            }
            set
            {
                this.m_NodeDir = value;
            }
        }

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

        public string NodeIdentifier
        {
            get
            {
                return this.m_NodeIdentifier;
            }
            set
            {
                this.m_NodeIdentifier = value;
            }
        }

        public string NodeName
        {
            get
            {
                return this.m_NodeName;
            }
            set
            {
                this.m_NodeName = value;
            }
        }

        public string NodePicUrl
        {
            get
            {
                return this.m_NodePicUrl;
            }
            set
            {
                this.m_NodePicUrl = value;
            }
        }

        public EasyOne.Enumerations.NodeType NodeType
        {
            get
            {
                return this.m_NodeType;
            }
            set
            {
                this.m_NodeType = value;
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

        public int OrderType
        {
            get
            {
                return this.m_OrderType;
            }
            set
            {
                this.m_OrderType = value;
            }
        }

        public string ParentDir
        {
            get
            {
                return this.m_ParentDir;
            }
            set
            {
                this.m_ParentDir = value;
            }
        }

        public int ParentId
        {
            get
            {
                return this.m_ParentId;
            }
            set
            {
                this.m_ParentId = value;
            }
        }

        public string ParentPath
        {
            get
            {
                return this.m_ParentPath;
            }
            set
            {
                this.m_ParentPath = value;
            }
        }

        public int PrevId
        {
            get
            {
                return this.m_PrevId;
            }
            set
            {
                this.m_PrevId = value;
            }
        }

        public int PurviewType
        {
            get
            {
                return this.m_PurviewType;
            }
            set
            {
                this.m_PurviewType = value;
            }
        }

        public string RelateNode
        {
            get
            {
                return this.m_RelateNode;
            }
            set
            {
                this.m_RelateNode = value;
            }
        }

        public string RelateSpecial
        {
            get
            {
                return this.m_RelateSpecial;
            }
            set
            {
                this.m_RelateSpecial = value;
            }
        }

        public int RootId
        {
            get
            {
                return this.m_RootId;
            }
            set
            {
                this.m_RootId = value;
            }
        }

        public NodeSettingInfo Settings
        {
            get
            {
                return this.m_Settings;
            }
            set
            {
                this.m_Settings = value;
            }
        }

        public bool ShowOnListIndex
        {
            get
            {
                return this.m_ShowOnListIndex;
            }
            set
            {
                this.m_ShowOnListIndex = value;
            }
        }

        public bool ShowOnListParent
        {
            get
            {
                return this.m_ShowOnListParent;
            }
            set
            {
                this.m_ShowOnListParent = value;
            }
        }

        public bool ShowOnMap
        {
            get
            {
                return this.m_ShowOnMap;
            }
            set
            {
                this.m_ShowOnMap = value;
            }
        }

        public bool ShowOnMenu
        {
            get
            {
                return this.m_ShowOnMenu;
            }
            set
            {
                this.m_ShowOnMenu = value;
            }
        }

        public bool ShowOnPath
        {
            get
            {
                return this.m_ShowOnPath;
            }
            set
            {
                this.m_ShowOnPath = value;
            }
        }

        public string Tips
        {
            get
            {
                return this.m_Tips;
            }
            set
            {
                this.m_Tips = value;
            }
        }

        public int WorkFlowId
        {
            get
            {
                return this.m_WorkFlowId;
            }
            set
            {
                this.m_WorkFlowId = value;
            }
        }
    }
}

