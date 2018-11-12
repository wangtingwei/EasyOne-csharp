namespace EasyOne.ExtendedControls
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Enumerations;
    using EasyOne.Model.Contents;
    using System;
    using System.ComponentModel;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:ExtendedNodeButton ID=\"EBtn\" runat=\"server\"></{0}:ExtendedNodeButton>")]
    public class ExtendedNodeButton : Button
    {
        private bool m_IsChecked;
        private int m_NodeId;
        private EasyOne.Enumerations.OperateCode m_Operatecode;

        protected override void OnInit(EventArgs e)
        {
            if (this.IsChecked)
            {
                if (PEContext.Current.Admin.IsSuperAdmin)
                {
                    this.Enabled = true;
                }
                else
                {
                    string roleNodeId = RolePermissions.GetRoleNodeId(PEContext.Current.Admin.Roles, this.OperateCode);
                    if (this.m_NodeId > 0)
                    {
                        bool flag = false;
                        string findStr = "";
                        NodeInfo cacheNodeById = Nodes.GetCacheNodeById(this.m_NodeId);
                        if (!cacheNodeById.IsNull)
                        {
                            findStr = findStr + this.m_NodeId;
                            if (cacheNodeById.ParentId > 0)
                            {
                                findStr = findStr + "," + cacheNodeById.ParentPath;
                            }
                            flag = StringHelper.FoundCharInArr(roleNodeId, findStr);
                        }
                        this.Enabled = flag;
                    }
                    else
                    {
                        this.Enabled = RolePermissions.AccessCheckNodePermission(this.OperateCode, -1);
                    }
                }
            }
            this.Visible = this.Enabled;
            base.OnInit(e);
        }

        [Category("自定义"), Description("是否启用检查"), Localizable(true), Bindable(true), DefaultValue(false)]
        public bool IsChecked
        {
            get
            {
                return this.m_IsChecked;
            }
            set
            {
                this.m_IsChecked = value;
            }
        }

        [Category("自定义"), DefaultValue(0), Description("节点ID"), Bindable(true), Localizable(true)]
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

        [Localizable(true), Description("操作资源码"), Bindable(true), Category("自定义"), DefaultValue("")]
        public EasyOne.Enumerations.OperateCode OperateCode
        {
            get
            {
                return this.m_Operatecode;
            }
            set
            {
                this.m_Operatecode = value;
            }
        }
    }
}

