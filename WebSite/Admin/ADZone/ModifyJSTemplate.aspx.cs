namespace EasyOne.WebSite.Admin.AD
{
    using EasyOne.AccessManage;
    using EasyOne.AD;
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class ModifyJSTemplate : AdminPage
    {

        protected void EBtnSaverTemplate_Click(object sender, EventArgs e)
        {
            ADZoneJS ejs = new ADZoneJS();
            if (ejs.SaveJSTemplate(this.TxtADTemplate.Text.Trim(), (ADZoneType) DataConverter.CLng(this.HdnZoneType.Value)))
            {
                AdminPage.WriteSuccessMsg("<li>修改成功！</li>", "JSTemplate.aspx");
            }
            else
            {
                AdminPage.WriteErrMsg("修改错误，检查您的服务器是否给文件写入权限！", "JSTemplate.aspx");
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            RolePermissions.BusinessAccessCheck(OperateCode.AdManage);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                int num = BasePage.RequestInt32("ZoneType");
                ADZoneType zoneType = (ADZoneType) num;
                this.HdnZoneType.Value = num.ToString();
                this.TxtADTemplate.Text = new ADZoneJS().GetADZoneJSTemplateContent(zoneType);
            }
        }
    }
}

