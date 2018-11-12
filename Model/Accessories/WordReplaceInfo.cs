namespace EasyOne.Model.Accessories
{
    using EasyOne.Model;
    using System;

    public class WordReplaceInfo : EasyOne.Model.Nullable
    {
        private bool m_IsEnabled;
        private int m_ItemId;
        private bool m_OpenType;
        private int m_Priority;
        private int m_ReplaceTimes;
        private int m_ReplaceType;
        private int m_ScopesType;
        private string m_SourceWord;
        private string m_TargetWord;
        private string m_Title;

        public WordReplaceInfo()
        {
        }

        public WordReplaceInfo(bool value)
        {
            base.IsNull = value;
        }

        public bool IsEnabled
        {
            get
            {
                return this.m_IsEnabled;
            }
            set
            {
                this.m_IsEnabled = value;
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

        public bool OpenType
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

        public int ReplaceTimes
        {
            get
            {
                return this.m_ReplaceTimes;
            }
            set
            {
                this.m_ReplaceTimes = value;
            }
        }

        public int ReplaceType
        {
            get
            {
                return this.m_ReplaceType;
            }
            set
            {
                this.m_ReplaceType = value;
            }
        }

        public int ScopesType
        {
            get
            {
                return this.m_ScopesType;
            }
            set
            {
                this.m_ScopesType = value;
            }
        }

        public string SourceWord
        {
            get
            {
                return this.m_SourceWord;
            }
            set
            {
                this.m_SourceWord = value;
            }
        }

        public string TargetWord
        {
            get
            {
                return this.m_TargetWord;
            }
            set
            {
                this.m_TargetWord = value;
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
    }
}

