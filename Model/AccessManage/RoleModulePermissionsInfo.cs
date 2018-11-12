namespace EasyOne.Model.AccessManage
{
    using EasyOne.Enumerations;
    using EasyOne.Model;
    using System;

    public class RoleModulePermissionsInfo : EasyOne.Model.Nullable
    {
        private EasyOne.Enumerations.OperateCode m_OperateCode;
        private int m_RoleId;

        public RoleModulePermissionsInfo()
        {
        }

        public RoleModulePermissionsInfo(bool value)
        {
            base.IsNull = value;
        }

        public EasyOne.Enumerations.OperateCode OperateCode
        {
            get
            {
                return this.m_OperateCode;
            }
            set
            {
                this.m_OperateCode = value;
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

