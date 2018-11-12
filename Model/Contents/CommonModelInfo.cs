namespace EasyOne.Model.Contents
{
    using EasyOne.Model;
    using System;

    public class CommonModelInfo : EasyOne.Model.Nullable
    {
        private int m_CommentAudited;
        private int m_CommentUnAudited;
        private DateTime? m_CreateTime;
        private int m_DayHits;
        private string m_DefaultPicurl;
        private string m_Editor;
        private int m_EliteLevel;
        private int m_GeneralId;
        private int m_Hits;
        private int m_InfoId;
        private string m_Inputer;
        private DateTime m_InputTime;
        private bool m_IsEshop;
        private int m_ItemId;
        private DateTime? m_LastHitTime;
        private int m_LinkType;
        private int m_ModelId;
        private int m_MonthHits;
        private int m_NodeId;
        private DateTime? m_PassedTime;
        private string m_PinyinTitle;
        private int m_Priority;
        private int m_Status;
        private string m_TableName;
        private string m_TemplateFile;
        private string m_Title;
        private DateTime m_UpdateTime;
        private string m_UploadFiles;
        private int m_WeekHits;

        public CommonModelInfo()
        {
        }

        public CommonModelInfo(bool value)
        {
            base.IsNull = value;
        }

        public int CommentAudited
        {
            get
            {
                return this.m_CommentAudited;
            }
            set
            {
                this.m_CommentAudited = value;
            }
        }

        public int CommentUNAudited
        {
            get
            {
                return this.m_CommentUnAudited;
            }
            set
            {
                this.m_CommentUnAudited = value;
            }
        }

        public DateTime? CreateTime
        {
            get
            {
                return this.m_CreateTime;
            }
            set
            {
                this.m_CreateTime = value;
            }
        }

        public int DayHits
        {
            get
            {
                return this.m_DayHits;
            }
            set
            {
                this.m_DayHits = value;
            }
        }

        public string DefaultPicurl
        {
            get
            {
                return this.m_DefaultPicurl;
            }
            set
            {
                this.m_DefaultPicurl = value;
            }
        }

        public string Editor
        {
            get
            {
                return this.m_Editor;
            }
            set
            {
                this.m_Editor = value;
            }
        }

        public int EliteLevel
        {
            get
            {
                return this.m_EliteLevel;
            }
            set
            {
                this.m_EliteLevel = value;
            }
        }

        public int GeneralId
        {
            get
            {
                return this.m_GeneralId;
            }
            set
            {
                this.m_GeneralId = value;
            }
        }

        public int Hits
        {
            get
            {
                return this.m_Hits;
            }
            set
            {
                this.m_Hits = value;
            }
        }

        public int InfoId
        {
            get
            {
                return this.m_InfoId;
            }
            set
            {
                this.m_InfoId = value;
            }
        }

        public string Inputer
        {
            get
            {
                return this.m_Inputer;
            }
            set
            {
                this.m_Inputer = value;
            }
        }

        public DateTime InputTime
        {
            get
            {
                return this.m_InputTime;
            }
            set
            {
                this.m_InputTime = value;
            }
        }

        public bool IsEshop
        {
            get
            {
                return this.m_IsEshop;
            }
            set
            {
                this.m_IsEshop = value;
            }
        }

        public int ItemId
        {
            get
            {
                return this.m_ItemId;
            }
            set
            {
                this.m_ItemId = value;
            }
        }

        public DateTime? LastHitTime
        {
            get
            {
                return this.m_LastHitTime;
            }
            set
            {
                this.m_LastHitTime = value;
            }
        }

        public int LinkType
        {
            get
            {
                return this.m_LinkType;
            }
            set
            {
                this.m_LinkType = value;
            }
        }

        public int ModelId
        {
            get
            {
                return this.m_ModelId;
            }
            set
            {
                this.m_ModelId = value;
            }
        }

        public int MonthHits
        {
            get
            {
                return this.m_MonthHits;
            }
            set
            {
                this.m_MonthHits = value;
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

        public DateTime? PassedTime
        {
            get
            {
                return this.m_PassedTime;
            }
            set
            {
                this.m_PassedTime = value;
            }
        }

        public string PinyinTitle
        {
            get
            {
                return this.m_PinyinTitle;
            }
            set
            {
                this.m_PinyinTitle = value;
            }
        }

        public int Priority
        {
            get
            {
                return this.m_Priority;
            }
            set
            {
                this.m_Priority = value;
            }
        }

        public int Status
        {
            get
            {
                return this.m_Status;
            }
            set
            {
                this.m_Status = value;
            }
        }

        public string TableName
        {
            get
            {
                return this.m_TableName;
            }
            set
            {
                this.m_TableName = value;
            }
        }

        public string TemplateFile
        {
            get
            {
                return this.m_TemplateFile;
            }
            set
            {
                this.m_TemplateFile = value;
            }
        }

        public string Title
        {
            get
            {
                return this.m_Title;
            }
            set
            {
                this.m_Title = value;
            }
        }

        public DateTime UpdateTime
        {
            get
            {
                return this.m_UpdateTime;
            }
            set
            {
                this.m_UpdateTime = value;
            }
        }

        public string UploadFiles
        {
            get
            {
                return this.m_UploadFiles;
            }
            set
            {
                this.m_UploadFiles = value;
            }
        }

        public int WeekHits
        {
            get
            {
                return this.m_WeekHits;
            }
            set
            {
                this.m_WeekHits = value;
            }
        }
    }
}

