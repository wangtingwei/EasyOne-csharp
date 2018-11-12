namespace EasyOne.WebSite.Admin.Survey
{
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.Survey;
    using EasyOne.Survey;
    using EasyOne.Web.UI;
    using System;
    using System.Web;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class SurveyFormCreate : AdminPage
    {

        protected void BtnCreate_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                int surveyId = DataConverter.CLng(this.HdnSurveyId.Value);
                if (surveyId == 0)
                {
                    AdminPage.WriteErrMsg("<li>请指定问卷ID！</li>");
                }
                else
                {
                    bool flag = false;
                    if ((this.TxtFileName.Text != this.HdnFileName.Value) && SurveyCreate.FileNameExists(this.TxtFileName.Text))
                    {
                        flag = true;
                    }
                    if (!flag)
                    {
                        string file = HttpContext.Current.Server.MapPath(@"~\Survey\") + DataSecurity.FilterBadChar(this.TxtFileName.Text);
                        string createContent = this.SurveyContent.Value;
                        if (this.RadlPageType.SelectedValue == "1")
                        {
                            createContent = SurveyCreate.GetDynamicContent(createContent, surveyId);
                        }
                        FileSystemObject.WriteFile(file, createContent);
                        SurveyInfo surveyInfo = this.ViewState["surveyInfo"] as SurveyInfo;
                        surveyInfo.FileName = this.TxtFileName.Text;
                        SurveyManager.Update(surveyInfo);
                        AdminPage.WriteSuccessMsg("创建页面成功！保存在../Survey/" + this.TxtFileName.Text, "SurveyManage.aspx");
                    }
                    else
                    {
                        AdminPage.WriteErrMsg("<li>已存在相同的问卷名，请指定另外的文件名！</li>");
                    }
                }
            }
        }

        protected void DataBind(int surveyId)
        {
            SurveyInfo surveyById = SurveyManager.GetSurveyById(surveyId);
            this.ViewState["surveyInfo"] = surveyById;
            this.SurveyContent.Value = SurveyCreate.GetSurveyTemplate(surveyById);
            string fileName = surveyById.FileName;
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".html";
            }
            string[] strArray = fileName.Split(new char[] { '.' });
            string str2 = strArray[1];
            this.HdnShortFileName.Value = strArray[0];
            this.HdnFileName.Value = fileName;
            if (str2 == "html")
            {
                this.RadlPageType.SelectedValue = "0";
            }
            else
            {
                this.RadlPageType.SelectedValue = "1";
            }
            this.TxtFileName.Text = fileName;
            this.HdnSurveyId.Value = surveyId.ToString();
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

        protected void RadlPageType_TextChanged(object sender, EventArgs e)
        {
            if (this.RadlPageType.SelectedIndex == 1)
            {
                this.TxtFileName.Text = this.HdnShortFileName.Value + ".aspx";
            }
            else
            {
                this.TxtFileName.Text = this.HdnShortFileName.Value + ".html";
            }
        }
    }
}

