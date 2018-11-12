namespace EasyOne.ExtendedControls
{
    using EasyOne.AccessManage;
    using EasyOne.Enumerations;
    using System;
    using System.ComponentModel;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    [ToolboxData("<{0}:ExtendedAnchor ID=\"Eah\" runat=\"server\"></{0}:ExtendedAnchor>")]
    public class ExtendedAnchor : HtmlAnchor
    {
        private bool m_IsChecked;
        private bool m_IsVisible;
        private EasyOne.Enumerations.OperateCode m_Operatecode;

        protected override void OnInit(EventArgs e)
        {
            bool flag = RolePermissions.AccessCheck(this.m_Operatecode);
            if (this.IsChecked && !flag)
            {
                base.Disabled = true;
            }
            if (this.IsVisible && !flag)
            {
                this.Visible = false;
            }
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

        [Localizable(true), Bindable(true), Category("自定义"), DefaultValue(false), Description("是否启用检查")]
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

        [Description("没有权限时是否可见"), Localizable(true), Bindable(true), Category("自定义"), DefaultValue(false)]
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

        [Localizable(true), Category("自定义"), Description("操作资源码"), Bindable(true), DefaultValue("")]
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

