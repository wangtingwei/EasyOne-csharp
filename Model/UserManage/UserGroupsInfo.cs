namespace EasyOne.Model.UserManage
{
    using EasyOne.Enumerations;
    using EasyOne.Model;
    using System;

    [Serializable]
    public class UserGroupsInfo : EasyOne.Model.Nullable
    {
        private string m_Description;
        private int m_GroupId;
        private string m_GroupName;
        private string m_GroupSetting;
        private EasyOne.Enumerations.GroupType m_GroupType;
        private string m_Settings;
        private int m_UserInGroupNumber;

        public UserGroupsInfo()
        {
        }

        public UserGroupsInfo(bool value)
        {
            base.IsNull = value;
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

        public int GroupId
        {
            get
            {
                return this.m_GroupId;
            }
            set
            {
                this.m_GroupId = value;
            }
        }

        public string GroupName
        {
            get
            {
                return this.m_GroupName;
            }
            set
            {
                this.m_GroupName = value;
            }
        }

        public string GroupSetting
        {
            get
            {
                return this.m_GroupSetting;
            }
            set
            {
                this.m_GroupSetting = value;
            }
        }

        public EasyOne.Enumerations.GroupType GroupType
        {
            get
            {
                return this.m_GroupType;
            }
            set
            {
                this.m_GroupType = value;
            }
        }

        public string Settings
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

        public int UserInGroupNumber
        {
            get
            {
                return this.m_UserInGroupNumber;
            }
            set
            {
                this.m_UserInGroupNumber = value;
            }
        }
    }
}

