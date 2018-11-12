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
    using System.Web.UI.HtmlControls;

    [ToolboxData("<{0}:ExtendedNodeAnchor ID=\"Eah\" runat=\"server\"></{0}:ExtendedNodeAnchor>")]
    public class ExtendedNodeAnchor : HtmlAnchor
    {
        private bool m_IsChecked;
        private int m_NodeId;
        private EasyOne.Enumerations.OperateCode m_Operatecode;

        protected override void OnInit(EventArgs e)
        {
            if (this.IsChecked)
            {
                base.Disabled = true;
                if (PEContext.Current.Admin.IsSuperAdmin)
                {
                    base.Disabled = false;
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
                        if (flag)
                        {
                            base.Disabled = false;
                        }
                    }
                    else if (RolePermissions.AccessCheckNodePermission(this.OperateCode, -1))
                    {
                        base.Disabled = false;
                    }
                }
            }
            this.Visible = true;
            base.OnInit(e);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (base.Disabled)
            {
                base.HRef = "";
                base.Attributes.Remove("onclick");
            }
        }

        [Bindable(true), Description("是否启用检查"), Localizable(true), DefaultValue(false), Category("自定义")]
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

        [Description("节点ID"), Localizable(true), DefaultValue(0), Bindable(true), Category("自定义")]
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

        [Bindable(true), Localizable(true), Category("自定义"), DefaultValue(""), Description("操作资源码")]
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

