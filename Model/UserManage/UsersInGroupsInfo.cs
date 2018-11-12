namespace EasyOne.Model.UserManage
{
    using EasyOne.Model;
    using System;

    public class UsersInGroupsInfo : EasyOne.Model.Nullable
    {
        private int m_GroupId;
        private int m_UserId;

        public UsersInGroupsInfo()
        {
        }

        public UsersInGroupsInfo(bool value)
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

