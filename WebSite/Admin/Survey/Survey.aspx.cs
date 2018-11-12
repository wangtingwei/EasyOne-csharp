namespace EasyOne.WebSite.Admin.Survey
{
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.Survey;
    using EasyOne.ModelControls;
    using EasyOne.Survey;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class SurveyUI : AdminPage
    {

        private void AddSurvey()
        {
            if (base.IsValid)
            {
                SurveyInfo surveyInfo = new SurveyInfo();
                surveyInfo.SurveyName = this.TxtSurveyName.Text;
                surveyInfo.Description = this.TxtDescription.Text;
                surveyInfo.NeedLogin = this.RadNeedLogin0.Checked ? 0 : 1;
                surveyInfo.SetPassword = this.TxtSetPassword.Text;
                surveyInfo.IPRepeat = DataConverter.CLng(this.TxtIPRepeat.Text, 1);
                surveyInfo.PresentPoint = DataConverter.CLng(this.TxtPresentPoint.Text);
                surveyInfo.LockIPType = DataConverter.CLng(this.RadlLockIPType.SelectedValue);
                surveyInfo.EndTime =DateTime.Parse( this.DateEnd.Date.ToString());
                surveyInfo.Template = this.FscTemplate.Text;
                surveyInfo.LockUrl = this.TxtLockUrl.Text.Trim();
                surveyInfo.FileName = string.Empty;
                surveyInfo.IsOpen = 0;
                surveyInfo.CreateDate = DateTime.Now;
                surveyInfo.SurveyId = 0;
                if (this.DateEnd.Date < DateTime.Now)
                {
                    AdminPage.WriteErrMsg("问卷结束时间早于创建时间！");
                }
                else
                {
                    surveyInfo.EndTime =DateTime.Parse( this.DateEnd.Date.ToString());
                }
                surveyInfo.SetIPLock = this.IPLockWrite.Value + "|||" + this.IPLockBlack.Value;
                surveyInfo.LockUrl = this.TxtLockUrl.Text;
                if (SurveyManager.Add(surveyInfo))
                {
                    AdminPage.WriteSuccessMsg("添加问卷成功！", "SurveyManage.aspx");
                }
                else
                {
                    AdminPage.WriteErrMsg("<li>添加问卷失败！");
                }
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            string str = this.HdnAction.Value;
            if (str != null)
            {
                if (!(str == "Modify"))
                {
                    if (str == "Add")
                    {
                        this.AddSurvey();
                    }
                }
                else
                {
                    this.UpdateSurvey();
                }
            }
            BasePage.ResponseRedirect("SurveyManage.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                string str;
                this.RadNeedLogin0.Attributes.Add("onclick", "javascript:TrEncourage.style.display='none';");
                this.RadNeedLogin1.Attributes.Add("onclick", "javascript:TrEncourage.style.display='';");
                if (((str = BasePage.RequestString("Action")) != null) && (str == "Modify"))
                {
                    this.HdnAction.Value = "Modify";
                    this.LblTitle.Text = "修改问卷";
                    this.ShowSurveyInfo(BasePage.RequestInt32("SurveyID"));
                }
                else
                {
                    this.HdnAction.Value = "Add";
                    this.LblTitle.Text = "添加问卷";
                    this.DateEnd.Text = DateTime.Today.AddMonths(1).ToString("yyyy-MM-dd");
                }
            }
        }

        private void ShowSurveyInfo(int surveyId)
        {
            if (surveyId != 0)
            {
                SurveyInfo surveyById = SurveyManager.GetSurveyById(surveyId);
                this.ViewState["Info"] = surveyById;
                if (surveyById != null)
                {
                    this.TxtSurveyName.Text = surveyById.SurveyName;
                    this.TxtDescription.Text = surveyById.Description;
                    this.TxtIPRepeat.Text = surveyById.IPRepeat.ToString();
                    if (surveyById.NeedLogin == 1)
                    {
                        this.RadNeedLogin1.Checked = true;
                        this.RadNeedLogin0.Checked = false;
                        this.TxtPresentPoint.Text = surveyById.PresentPoint.ToString();
                    }
                    this.TxtSetPassword.Attributes.Add("value", surveyById.SetPassword);
                    this.RadlLockIPType.SelectedValue = surveyById.LockIPType.ToString();
                    string[] field = surveyById.SetIPLock.Split(new string[] { "|||" }, StringSplitOptions.None);
                    this.IPLockWrite.Value = DataSecurity.GetArrayValue(0, field);
                    this.IPLockBlack.Value = DataSecurity.GetArrayValue(1, field);
                    this.DateEnd.Text = surveyById.EndTime.ToString("yyyy-MM-dd");
                    this.FscTemplate.Text = surveyById.Template;
                    this.TxtLockUrl.Text = surveyById.LockUrl;
                }
                else
                {
                    AdminPage.WriteErrMsg("找不到该问卷!", "SurveyManage.aspx");
                }
            }
        }

        private void UpdateSurvey()
        {
            if (this.ViewState["Info"] != null)
            {
                SurveyInfo surveyInfo = this.ViewState["Info"] as SurveyInfo;
                surveyInfo.SurveyName = this.TxtSurveyName.Text;
                surveyInfo.Description = this.TxtDescription.Text;
                surveyInfo.NeedLogin = this.RadNeedLogin0.Checked ? 0 : 1;
                surveyInfo.SetPassword = this.TxtSetPassword.Text;
                surveyInfo.IPRepeat = DataConverter.CLng(this.TxtIPRepeat.Text, 1);
                surveyInfo.PresentPoint = DataConverter.CLng(this.TxtPresentPoint.Text);
                surveyInfo.LockIPType = DataConverter.CLng(this.RadlLockIPType.SelectedValue);
                surveyInfo.EndTime = DateTime.Parse(this.DateEnd.Date.ToString());
                surveyInfo.Template = this.FscTemplate.Text;
                surveyInfo.LockUrl = this.TxtLockUrl.Text.Trim();
                if (this.DateEnd.Date < DateTime.Now)
                {
                    AdminPage.WriteErrMsg("<li>问卷结束时间早于创建时间！</li>");
                }
                else
                {
                    surveyInfo.EndTime = DateTime.Parse(this.DateEnd.Date.ToString());
                }
                surveyInfo.SetIPLock = this.IPLockWrite.Value + "|||" + this.IPLockBlack.Value;
                surveyInfo.LockUrl = this.TxtLockUrl.Text;
                if (SurveyManager.Update(surveyInfo))
                {
                    AdminPage.WriteSuccessMsg("修改问卷成功！", "SurveyManage.aspx");
                }
                else
                {
                    AdminPage.WriteErrMsg("<li>修改问卷失败！");
                }
            }
        }
    }
}

