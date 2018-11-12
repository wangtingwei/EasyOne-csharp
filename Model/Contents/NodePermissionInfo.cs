namespace EasyOne.Model.Contents
{
    using EasyOne.Enumerations;
    using EasyOne.Model;
    using System;

    public class NodePermissionInfo : EasyOne.Model.Nullable
    {
        private AccessControlEntry m_Browse;
        private AccessControlEntry m_Delete;
        private AccessControlEntry m_Edit;
        private int m_NodeId;
        private AccessControlEntry m_Post;
        private int m_RoleId;
        private string m_RoleName;
        private AccessControlEntry m_View;

        public NodePermissionInfo()
        {
        }

        public NodePermissionInfo(bool value)
        {
            base.IsNull = value;
        }

        public AccessControlEntry Browse
        {
            get
            {
                return this.m_Browse;
            }
            set
            {
                this.m_Browse = value;
            }
        }

        public AccessControlEntry Delete
        {
            get
            {
                return this.m_Delete;
            }
            set
            {
                this.m_Delete = value;
            }
        }

        public AccessControlEntry Edit
        {
            get
            {
                return this.m_Edit;
            }
            set
            {
                this.m_Edit = value;
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

        public AccessControlEntry Post
        {
            get
            {
                return this.m_Post;
            }
            set
            {
                this.m_Post = value;
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

        public AccessControlEntry View
        {
            get
            {
                return this.m_View;
            }
            set
            {
                this.m_View = value;
            }
        }
    }
}

