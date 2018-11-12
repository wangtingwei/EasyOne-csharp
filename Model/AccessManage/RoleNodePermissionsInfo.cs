namespace EasyOne.Model.AccessManage
{
    using EasyOne.Enumerations;
    using EasyOne.Model;
    using System;

    public class RoleNodePermissionsInfo : EasyOne.Model.Nullable
    {
        private int m_GroupId;
        private int m_NodeId;
        private EasyOne.Enumerations.OperateCode m_OperateCode;

        public RoleNodePermissionsInfo()
        {
        }

        public RoleNodePermissionsInfo(bool value)
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

        public int NodeId
        {
            get
            {
                return this.m_NodeId;
            }
            set
            {
                this.m_NodeId = value;
            }
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
    }
}

