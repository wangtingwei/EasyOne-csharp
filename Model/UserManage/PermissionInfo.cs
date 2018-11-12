namespace EasyOne.Model.UserManage
{
    using EasyOne.Model;
    using System;

    public class PermissionInfo : EasyOne.Model.Nullable
    {
        private string m_OperationCode;
        private int m_RoleId;

        public PermissionInfo()
        {
        }

        public PermissionInfo(bool value)
        {
            base.IsNull = value;
        }

        public string OperationCode
        {
            get
            {
                return this.m_OperationCode;
            }
            set
            {
                this.m_OperationCode = value;
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

