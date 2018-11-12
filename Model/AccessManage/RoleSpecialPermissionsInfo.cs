namespace EasyOne.Model.AccessManage
{
    using EasyOne.Enumerations;
    using EasyOne.Model;
    using System;

    public class RoleSpecialPermissionsInfo : EasyOne.Model.Nullable
    {
        private int m_GroupId;
        private EasyOne.Enumerations.OperateCode m_OperateCode;
        private int m_SpecialId;

        public RoleSpecialPermissionsInfo()
        {
        }

        public RoleSpecialPermissionsInfo(bool value)
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

        public int SpecialId
        {
            get
            {
                return this.m_SpecialId;
            }
            set
            {
                this.m_SpecialId = value;
            }
        }
    }
}

