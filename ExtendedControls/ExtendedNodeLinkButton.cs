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

    [ToolboxData("<{0}:ExtendedNodeLinkButton ID=\"ELbtn\" runat=\"server\"></{0}:ExtendedNodeLinkButton>")]
    public class ExtendedNodeLinkButton : LinkButton
    {
        private bool m_IsChecked;
        private bool m_IsVisible;
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

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (!this.Enabled)
            {
                this.OnClientClick = "";
            }
        }

        [Category("自定义"), Localizable(true), DefaultValue(false), Bindable(true), Description("是否启用检查")]
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

        [Bindable(true), Category("自定义"), DefaultValue(false), Description("没有权限时是否可见"), Localizable(true)]
        public bool IsVisible
        {
            get
            {
                return this.m_IsVisible;
            }
            set
            {
                this.m_IsVisible = value;
            }
        }

        [Category("自定义"), Description("节点ID"), Localizable(true), DefaultValue(0), Bindable(true)]
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

