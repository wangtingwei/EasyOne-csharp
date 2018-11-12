namespace EasyOne.WebSite.Admin.Survey
{
    using EasyOne.Common;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.Survey;
    using EasyOne.Survey;
    using EasyOne.Web.UI;
    using System;
    using System.IO;
    using System.Web;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class SurveyFormEdit : AdminPage
    {

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (DataConverter.CLng(this.HdnSurveyId.Value) == 0)
            {
                AdminPage.WriteErrMsg("请指定问卷ID！");
            }
            else
            {
                FileSystemObject.WriteFile(HttpContext.Current.Server.MapPath(@"~\Survey\") + DataSecurity.FilterBadChar(this.LtrFileName.Text), this.SurveyContent.Value);
                AdminPage.WriteSuccessMsg("编辑页面成功！保存在../Survey/" + this.LtrFileName.Text, "SurveyManage.aspx");
            }
        }

        protected void DataBind(int surveyId)
        {
            SurveyInfo surveyById = SurveyManager.GetSurveyById(surveyId);
            if (string.IsNullOrEmpty(surveyById.FileName))
            {
                AdminPage.WriteErrMsg("数据库无对应的文件名！！");
            }
            else
            {
                string path = HttpContext.Current.Server.MapPath(@"~\Survey\") + surveyById.FileName;
                if (!File.Exists(path))
                {
                    AdminPage.WriteErrMsg("不存在文件：" + path + "！！");
                }
                else
                {
                    this.SurveyContent.Value = FileSystemObject.ReadFile(path);
                    this.LtrFileName.Text = surveyById.SurveyName;
                    this.HdnSurveyId.Value = surveyId.ToString();
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                int surveyId = BasePage.RequestInt32("SurveyID");
                if (surveyId == 0)
                {
                    AdminPage.WriteErrMsg("请指定问卷ID！");
                }
                else
                {
                    this.DataBind(surveyId);
                }
            }
        }
    }
}

