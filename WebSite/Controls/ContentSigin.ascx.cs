namespace EasyOne.WebSite.Controls
{
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Model.CommonModel;
    using EasyOne.Model.Contents;
    using EasyOne.Web.UI;
    using System;
    using System.Data;
    using System.Web.UI.WebControls;
    using EasyOne.Enumerations;

    public partial class ContentSigin : BaseUserControl
    {
        protected DataTable contentDataTable;
        private int m_GeneralId;

        private void InitSigin(int generalId, bool enableSignin)
        {
            if (enableSignin)
            {
                SignInContentInfo signInContentByGeneralId = SignInContent.GetSignInContentByGeneralId(generalId);
                if (!signInContentByGeneralId.IsNull)
                {
                    this.LblSigninType.Text = BaseUserControl.EnumToHtml<SignInType>(signInContentByGeneralId.SignInType);
                    this.LblEndTime.Text = signInContentByGeneralId.EndTime.ToString();
                    this.LblPriority.Text = signInContentByGeneralId.Priority.ToString();
                    this.LblStatus.Text = BaseUserControl.EnumToHtml<SignInStatus>(signInContentByGeneralId.Status);
                }
                this.RptSigninLog.DataSource = SignInLog.GetList(generalId);
                this.RptSigninLog.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.m_GeneralId = BaseUserControl.RequestInt32("GeneralID");
            this.contentDataTable = ContentManage.GetContentDataById(this.m_GeneralId);
            if (this.contentDataTable.Rows.Count > 0)
            {
                ModelInfo modelInfoById = ModelManager.GetModelInfoById(DataConverter.CLng(this.contentDataTable.Rows[0]["ModelID"].ToString()));
                this.InitSigin(this.m_GeneralId, modelInfoById.EnableSignIn);
            }
            else
            {
                BaseUserControl.WriteErrMsg("对不起，错误的参数！", SiteConfig.SiteInfo.VirtualPath + "Default.aspx");
            }
        }
    }
}

