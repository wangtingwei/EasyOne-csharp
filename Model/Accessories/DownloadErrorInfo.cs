namespace EasyOne.Model.Accessories
{
    using EasyOne.Model;
    using System;

    public class DownloadErrorInfo : EasyOne.Model.Nullable
    {
        private DateTime m_ErrorDate;
        private int m_ErrorId;
        private int m_ErrorTimes;
        private string m_ErrorUrl;
        private int m_InfoId;

        public DownloadErrorInfo()
        {
        }

        public DownloadErrorInfo(bool value)
        {
            base.IsNull = value;
        }

        public DateTime ErrorDate
        {
            get
            {
                return this.m_ErrorDate;
            }
            set
            {
                this.m_ErrorDate = value;
            }
        }

        public int ErrorId
        {
            get
            {
                return this.m_ErrorId;
            }
            set
            {
                this.m_ErrorId = value;
            }
        }

        public int ErrorTimes
        {
            get
            {
                return this.m_ErrorTimes;
            }
            set
            {
                this.m_ErrorTimes = value;
            }
        }

        public string ErrorUrl
        {
            get
            {
                return this.m_ErrorUrl;
            }
            set
            {
                this.m_ErrorUrl = value;
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
    }
}

