namespace EasyOne.Model.UserManage
{
    using EasyOne.Model;
    using System;

    public class GroupInRoleInfo : EasyOne.Model.Nullable
    {
        private int m_GroupId;
        private int m_RoleId;

        public GroupInRoleInfo()
        {
        }

        public GroupInRoleInfo(bool value)
        {
            base.IsNull = value;
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
    }
}

