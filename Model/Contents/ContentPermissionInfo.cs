namespace EasyOne.Model.Contents
{
    using EasyOne.Model;
    using System;

    public class ContentPermissionInfo : EasyOne.Model.Nullable
    {
        private string m_ArrGroupId;
        private int m_GeneralId;
        private int m_PermissionType;

        public ContentPermissionInfo()
        {
        }

        public ContentPermissionInfo(bool value)
        {
            base.IsNull = value;
        }

        public string ArrGroupId
        {
            get
            {
                return this.m_ArrGroupId;
            }
            set
            {
                this.m_ArrGroupId = value;
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

        public int PermissionType
        {
            get
            {
                return this.m_PermissionType;
            }
            set
            {
                this.m_PermissionType = value;
            }
        }
    }
}

