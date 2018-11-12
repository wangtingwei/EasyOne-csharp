namespace EasyOne.ExtendedControls
{
    using EasyOne.AccessManage;
    using EasyOne.Enumerations;
    using System;
    using System.ComponentModel;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    [ToolboxData("<{0}:ExtendedHtmlButton ID=\"EhBtn\" runat=\"server\"></{0}:ExtendedHtmlButton>")]
    public class ExtendedHtmlButton : HtmlButton
    {
        private bool m_IsChecked;
        private bool m_IsVisible;
        private EasyOne.Enumerations.OperateCode m_Operatecode;

        protected override void OnInit(EventArgs e)
        {
            if (this.IsChecked)
            {
                base.Disabled = !RolePermissions.AccessCheck(this.m_Operatecode);
            }
            if (this.IsVisible)
            {
                this.Visible = !base.Disabled;
            }
            base.OnInit(e);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (base.Disabled)
            {
                base.Attributes.Remove("onclick");
            }
        }

        [DefaultValue(false), Localizable(true), Bindable(true), Category("自定义"), Description("是否启用检查")]
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

        [Bindable(true), DefaultValue(false), Description("没有权限时是否可见"), Localizable(true), Category("自定义")]
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

