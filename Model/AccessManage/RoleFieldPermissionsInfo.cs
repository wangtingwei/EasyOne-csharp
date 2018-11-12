namespace EasyOne.Model.AccessManage
{
    using EasyOne.Enumerations;
    using EasyOne.Model;
    using System;

    public class RoleFieldPermissionsInfo : EasyOne.Model.Nullable
    {
        private string m_FieldName;
        private int m_ModelId;
        private EasyOne.Enumerations.OperateCode m_OperateCode;
        private int m_RoleId;

        public RoleFieldPermissionsInfo()
        {
        }

        public RoleFieldPermissionsInfo(bool value)
        {
            base.IsNull = value;
        }

        public string FieldName
        {
            get
            {
                return this.m_FieldName;
            }
            set
            {
                this.m_FieldName = value;
            }
        }

        public int ModelId
        {
            get
            {
                return this.m_ModelId;
            }
            set
            {
                this.m_ModelId = value;
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

