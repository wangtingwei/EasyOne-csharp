namespace EasyOne.Model.Accessories
{
    using System;

    public class CacheInfo
    {
        private string m_CacheName;
        private string m_CacheValue;

        public string CacheName
        {
            get
            {
                return this.m_CacheName;
            }
            set
            {
                this.m_CacheName = value;
            }
        }

        public string CacheValue
        {
            get
            {
                return this.m_CacheValue;
            }
            set
            {
                this.m_CacheValue = value;
            }
        }
    }
}

