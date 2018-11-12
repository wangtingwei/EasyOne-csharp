namespace EasyOne.Model.UserManage
{
    using System;

    public class UserFriendInfo
    {
        private DateTime m_AddTime;
        private string m_FriendName;
        private int m_GroupId;
        private string m_UserName;

        public DateTime AddTime
        {
            get
            {
                return this.m_AddTime;
            }
            set
            {
                this.m_AddTime = value;
            }
        }

        public string FriendName
        {
            get
            {
                return this.m_FriendName;
            }
            set
            {
                this.m_FriendName = value;
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

        public string UserName
        {
            get
            {
                return this.m_UserName;
            }
            set
            {
                this.m_UserName = value;
            }
        }
    }
}

