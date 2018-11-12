namespace EasyOne.Model.Accessories
{
    using EasyOne.Model;
    using System;

    public class FileInfo : EasyOne.Model.Nullable
    {
        private int m_Id;
        private bool m_IsThumb;
        private string m_Name;
        private string m_Path;
        private int m_Quote;
        private int m_Size;

        public FileInfo()
        {
        }

        public FileInfo(bool value)
        {
            base.IsNull = value;
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

        public bool IsThumb
        {
            get
            {
                return this.m_IsThumb;
            }
            set
            {
                this.m_IsThumb = value;
            }
        }

        public string Name
        {
            get
            {
                return this.m_Name;
            }
            set
            {
                this.m_Name = value;
            }
        }

        public string Path
        {
            get
            {
                return this.m_Path;
            }
            set
            {
                this.m_Path = value;
            }
        }

        public int Quote
        {
            get
            {
                return this.m_Quote;
            }
            set
            {
                this.m_Quote = value;
            }
        }

        public int Size
        {
            get
            {
                return this.m_Size;
            }
            set
            {
                this.m_Size = value;
            }
        }
    }
}

