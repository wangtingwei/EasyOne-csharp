namespace EasyOne.Model.Accessories
{
    using EasyOne.Model;
    using System;

    public class FavoriteInfo : EasyOne.Model.Nullable
    {
        private int m_FavoriteId;
        private DateTime m_FavoriteTime;
        private int m_InfoId;
        private string m_Title;
        private int m_UserId;

        public FavoriteInfo()
        {
        }

        public FavoriteInfo(bool value)
        {
            base.IsNull = value;
        }

        public int FavoriteId
        {
            get
            {
                return this.m_FavoriteId;
            }
            set
            {
                this.m_FavoriteId = value;
            }
        }

        public DateTime FavoriteTime
        {
            get
            {
                return this.m_FavoriteTime;
            }
            set
            {
                this.m_FavoriteTime = value;
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

        public int UserId
        {
            get
            {
                return this.m_UserId;
            }
            set
            {
                this.m_UserId = value;
            }
        }
    }
}

