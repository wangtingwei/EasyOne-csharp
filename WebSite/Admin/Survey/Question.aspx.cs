namespace EasyOne.WebSite.Admin.Survey
{
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.Survey;
    using EasyOne.Survey;
    using EasyOne.Web.UI;
    using System;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using EasyOne.ModelControls;

    public partial class QuestionUI : AdminPage
    {
        private string m_Action;
        private int m_SurveyId;

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("QuestionManage.aspx?SurveyID=" + this.m_SurveyId);
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                bool flag = false;
                SurveyFieldInfo info = new SurveyFieldInfo();
                int num = DataConverter.CLng(this.RadlQuestionType.SelectedValue);
                switch (num)
                {
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    {
                        string[] settings = this.TxtSettings.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                        info.CopyToSettings(settings);
                        break;
                    }
                    case 7:
                        info.CopyToSettings(new string[] { "0", "1" });
                        break;

                    default:
                        info.CopyToSettings(new string[] { "" });
                        break;
                }
                info.QuestionContent = this.TxtQuestionContent.Text;
                info.QuestionType = num;
                info.EnableNull = DataConverter.CBoolean(this.RadlEnableNull.SelectedValue);
                info.InputType = DataConverter.CLng(this.RadlInputType.SelectedValue);
                info.ContentLength = DataConverter.CLng(this.TxtContentLength.Text);
                if (this.m_Action == "modify")
                {
                    info.QuestionId = DataConverter.CLng(this.HdnQuestionId.Value);
                    flag = SurveyField.Update(this.m_SurveyId, info);
                }
                else
                {
                    flag = SurveyField.Add(this.m_SurveyId, info);
                    if (DataConverter.CLng(this.HdnIsOpen.Value) == 2)
                    {
                        string tableName = "PE_SurveyRecord" + this.m_SurveyId;
                        flag = flag && SurveyField.AddFieldToTable(info, tableName);
                    }
                }
                if (flag)
                {
                    AdminPage.WriteSuccessMsg("保存题目信息成功！", "QuestionManage.aspx?SurveyID=" + this.m_SurveyId);
                }
                else
                {
                    AdminPage.WriteErrMsg("<li>保存题目信息失败！</li>");
                }
            }
        }

        protected void ModifyInitialize()
        {
            SurveyFieldInfo fieldInfoById = new SurveyFieldInfo();
            fieldInfoById = SurveyField.GetFieldInfoById(this.m_SurveyId, DataConverter.CLng(this.HdnQuestionId.Value));
            if (fieldInfoById.IsNull)
            {
                AdminPage.WriteErrMsg("<li>找不到对应的问卷题目信息</li>");
            }
            else
            {
                SurveyInfo info2 = new SurveyInfo();
                int isOpen = SurveyManager.GetSurveyById(this.m_SurveyId).IsOpen;
                if (isOpen != 0)
                {
                    this.RadlQuestionType.Enabled = false;
                }
                this.RadlEnableNull.SelectedValue = fieldInfoById.EnableNull.ToString();
                this.TxtQuestionContent.Text = fieldInfoById.QuestionContent;
                this.RadlQuestionType.SelectedValue = fieldInfoById.QuestionType.ToString();
                switch (fieldInfoById.QuestionType)
                {
                    case 0:
                        this.PnlText.Visible = true;
                        this.PnlChoice.Visible = false;
                        this.PnlInputType.Visible = false;
                        break;

                    case 1:
                        this.PnlText.Visible = false;
                        this.PnlChoice.Visible = false;
                        this.PnlInputType.Visible = false;
                        break;

                    case 2:
                        this.PnlText.Visible = false;
                        this.PnlChoice.Visible = true;
                        this.PnlInputType.Visible = true;
                        break;

                    case 3:
                        this.PnlText.Visible = false;
                        this.PnlChoice.Visible = true;
                        this.PnlInputType.Visible = true;
                        break;

                    case 4:
                        this.PnlText.Visible = false;
                        this.PnlChoice.Visible = true;
                        this.PnlInputType.Visible = false;
                        break;

                    case 5:
                        this.PnlText.Visible = false;
                        this.PnlChoice.Visible = true;
                        this.PnlInputType.Visible = false;
                        break;

                    case 6:
                        this.PnlText.Visible = false;
                        this.PnlChoice.Visible = false;
                        this.PnlInputType.Visible = false;
                        break;

                    case 7:
                        this.PnlText.Visible = false;
                        this.PnlChoice.Visible = false;
                        this.PnlInputType.Visible = false;
                        break;

                    case 8:
                        this.PnlText.Visible = true;
                        this.PnlChoice.Visible = false;
                        this.PnlInputType.Visible = false;
                        break;

                    case 9:
                        this.PnlText.Visible = true;
                        this.PnlChoice.Visible = false;
                        this.PnlInputType.Visible = false;
                        break;
                }
                if (this.PnlChoice.Visible)
                {
                    if (fieldInfoById.Settings != null)
                    {
                        StringBuilder builder = new StringBuilder();
                        foreach (string str in fieldInfoById.Settings)
                        {
                            builder.Append(str);
                            builder.Append("\n");
                        }
                        this.TxtSettings.Text = builder.ToString();
                    }
                    if (isOpen != 0)
                    {
                        this.TxtSettings.ReadOnly = true;
                        this.RadlInputType.Enabled = false;
                    }
                    if (this.PnlInputType.Visible)
                    {
                        this.RadlInputType.SelectedValue = fieldInfoById.InputType.ToString();
                        if (fieldInfoById.InputType == 1)
                        {
                            this.PnlText.Visible = true;
                        }
                    }
                    else
                    {
                        this.PnlText.Visible = false;
                    }
                }
                if (this.PnlText.Visible)
                {
                    this.TxtContentLength.Text = fieldInfoById.ContentLength.ToString();
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.m_Action = BasePage.RequestStringToLower("Action");
            this.m_SurveyId = BasePage.RequestInt32("SurveyID");
            if (!this.Page.IsPostBack)
            {
                SurveyInfo surveyById = new SurveyInfo();
                surveyById = SurveyManager.GetSurveyById(this.m_SurveyId);
                this.HdnIsOpen.Value = surveyById.IsOpen.ToString();
                string surveyName = surveyById.SurveyName;
                this.LnkSurveyName.Text = surveyName;
                this.LnkSurveyName.NavigateUrl = "QuestionManage.aspx?SurveyID=" + this.m_SurveyId;
                if (this.m_SurveyId != 0)
                {
                    if (this.m_Action == "modify")
                    {
                        int num = BasePage.RequestInt32("QuestionId");
                        this.LblTitle.Text = "修改问卷";
                        if (num != 0)
                        {
                            this.HdnQuestionId.Value = num.ToString();
                            this.ModifyInitialize();
                        }
                        else
                        {
                            AdminPage.WriteErrMsg("<li>问题ID不能为空</li>");
                        }
                    }
                }
                else
                {
                    AdminPage.WriteErrMsg("<li>问卷ID不能为空</li>");
                }
            }
        }

        protected void RadlInputType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.RadlInputType.SelectedValue == "1")
            {
                this.PnlText.Visible = true;
            }
            else
            {
                this.PnlText.Visible = false;
            }
        }

        protected void RadlQuestionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowPanel(DataConverter.CLng(this.RadlQuestionType.SelectedValue));
        }

        private void ShowPanel(int showType)
        {
            switch (showType)
            {
                case 0:
                    this.PnlText.Visible = true;
                    this.PnlChoice.Visible = false;
                    this.PnlInputType.Visible = false;
                    return;

                case 1:
                    this.PnlText.Visible = false;
                    this.PnlChoice.Visible = false;
                    this.PnlInputType.Visible = false;
                    return;

                case 2:
                    this.PnlText.Visible = false;
                    this.PnlChoice.Visible = true;
                    this.PnlInputType.Visible = true;
                    this.RadlInputType.SelectedValue = "0";
                    return;

                case 3:
                    this.PnlText.Visible = false;
                    this.PnlChoice.Visible = true;
                    this.PnlInputType.Visible = true;
                    this.RadlInputType.SelectedValue = "0";
                    return;

                case 4:
                    this.PnlText.Visible = false;
                    this.PnlChoice.Visible = true;
                    this.PnlInputType.Visible = false;
                    return;

                case 5:
                    this.PnlText.Visible = false;
                    this.PnlChoice.Visible = true;
                    this.PnlInputType.Visible = false;
                    return;

                case 6:
                    this.PnlText.Visible = false;
                    this.PnlChoice.Visible = false;
                    this.PnlInputType.Visible = false;
                    return;

                case 7:
                    this.PnlText.Visible = false;
                    this.PnlChoice.Visible = false;
                    this.PnlInputType.Visible = false;
                    return;

                case 8:
                    this.PnlText.Visible = true;
                    this.PnlChoice.Visible = false;
                    this.PnlInputType.Visible = false;
                    return;

                case 9:
                    this.PnlText.Visible = true;
                    this.PnlChoice.Visible = false;
                    this.PnlInputType.Visible = false;
                    return;
            }
        }
    }
}

