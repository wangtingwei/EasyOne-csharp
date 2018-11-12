namespace EasyOne.Model.UserManage
{
    using EasyOne.Model;
    using System;

    public class RoleInfo : EasyOne.Model.Nullable
    {
        private string m_Description;
        private int m_RoleId;
        private string m_RoleName;

        public RoleInfo()
        {
        }

        public RoleInfo(bool value)
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

        public int RoleId
        {
            get
            {
                return this.m_RoleId;
            }
            set
            {
                this.m_RoleId = value;
            }
        }

        public string RoleName
        {
            get
            {
                return this.m_RoleName;
            }
            set
            {
                this.m_RoleName = value;
            }
        }

        public string RoleNameAndDescription
        {
            get
            {
                return (this.m_RoleName + "： " + this.m_Description);
            }
        }
    }
}

