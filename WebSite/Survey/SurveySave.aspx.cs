namespace EasyOne.WebSite.Survey
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Model.Survey;
    using EasyOne.Model.UserManage;
    using EasyOne.Survey;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web;
    using System.Web.UI.HtmlControls;

    public partial class SurveySave : DynamicPage
    {


        private void CheckForm(SurveyFieldInfo fieldInfo, string answer)
        {
            switch (fieldInfo.QuestionType)
            {
                case 0:
                case 1:
                    if (!string.IsNullOrEmpty(answer) || !fieldInfo.EnableNull)
                    {
                        break;
                    }
                    DynamicPage.WriteErrMsg(fieldInfo.QuestionContent + "不能为空！");
                    return;

                case 2:
                case 3:
                case 4:
                case 5:
                case 7:
                    if (!string.IsNullOrEmpty(answer) || !fieldInfo.EnableNull)
                    {
                        break;
                    }
                    DynamicPage.WriteErrMsg("请选择" + fieldInfo.QuestionContent);
                    return;

                case 6:
                    DateTime time;
                    if (fieldInfo.EnableNull && string.IsNullOrEmpty(answer))
                    {
                        DynamicPage.WriteErrMsg(fieldInfo.QuestionContent + "不能为空！");
                    }
                    if (DateTime.TryParse(answer, out time))
                    {
                        break;
                    }
                    DynamicPage.WriteErrMsg(fieldInfo.QuestionContent + " 填写的日期格式不正确！");
                    return;

                case 8:
                    if (DataValidator.IsNumber(answer) || !fieldInfo.EnableNull)
                    {
                        break;
                    }
                    DynamicPage.WriteErrMsg(fieldInfo.QuestionContent + "中输入的不是数字！");
                    return;

                case 9:
                    if (!DataValidator.IsEmail(answer) && fieldInfo.EnableNull)
                    {
                        DynamicPage.WriteErrMsg("请选择正确的Emial地址！");
                    }
                    break;

                default:
                    return;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int surveyId = DataConverter.CLng(base.Request.Form["SurveyID"]);
            this.SaveSurveyRecord(surveyId);
        }

        private void SaveSurveyRecord(int surveyId)
        {
            if (surveyId == 0)
            {
                DynamicPage.WriteErrMsg("问卷调查ID错误！");
            }
            else
            {
                SurveyInfo surveyById = SurveyManager.GetSurveyById(surveyId);
                if (surveyById.IsOpen != 1)
                {
                    DynamicPage.WriteErrMsg("问卷调查尚未启用！！！");
                }
                if (surveyById.EndTime!=null)//.HasValue)
                {
                    DateTime? endTime = surveyById.EndTime;
                    DateTime now = DateTime.Now;
                    if (endTime.HasValue ? (endTime.GetValueOrDefault() < now) : false)
                    {
                        DynamicPage.WriteErrMsg("问卷调查已经结束！！！");
                    }
                }
                if (!SurveyManager.CheckLockUrl(base.Request.ServerVariables["HTTP_REFERER"], surveyById))
                {
                    DynamicPage.WriteErrMsg("不允许从外部链接地址提交！！！");
                }
                if (SurveyManager.CheckRepeatIP(PEContext.Current.UserHostAddress, surveyId, surveyById))
                {
                    DynamicPage.WriteErrMsg("同一用户不允许填写问卷调查超过" + surveyById.IPRepeat.ToString() + "次！！！");
                }
                if (!string.IsNullOrEmpty(surveyById.SetPassword) && (surveyById.SetPassword != base.Request.Form["SurveyPassword"].ToString()))
                {
                    DynamicPage.WriteErrMsg("问卷密码错误！！！");
                }
                if (SurveyManager.CheckIPLock(PEContext.Current.UserHostAddress, surveyById))
                {
                    DynamicPage.WriteErrMsg("对不起！您的IP（" + PEContext.Current.UserHostAddress + "）被系统限定，您可以和站长联系。");
                }
                if (surveyById.NeedLogin == 1)
                {
                    if (PEContext.Current.User.Identity.IsAuthenticated)
                    {
                        if (surveyById.PresentPoint != 0)
                        {
                            int userId = PEContext.Current.User.UserInfo.UserId;
                            Users.Update(userId, "UserPoint", (PEContext.Current.User.UserInfo.UserPoint + surveyById.PresentPoint).ToString());
                            UserPointLogInfo userPointLogInfo = new UserPointLogInfo();
                            userPointLogInfo.IncomePayOut = 1;
                            userPointLogInfo.InfoId = 0;
                            userPointLogInfo.Inputer = "System";
                            userPointLogInfo.IP = PEContext.Current.UserHostAddress;
                            userPointLogInfo.LogId = UserPointLog.GetTotalInComeAndPayOutAll().Count;
                            userPointLogInfo.LogTime = DateTime.Now;
                            userPointLogInfo.Memo = "";
                            userPointLogInfo.ModuleType = 9;
                            userPointLogInfo.Point = surveyById.PresentPoint;
                            userPointLogInfo.Remark = "参与“" + surveyById.SurveyName + "”--问卷调查获得奖励点数";
                            userPointLogInfo.Times = 1;
                            userPointLogInfo.UserName = PEContext.Current.User.UserInfo.UserName;
                            UserPointLog.Add(userPointLogInfo);
                        }
                    }
                    else
                    {
                        string absoluteUri = HttpContext.Current.Request.Url.AbsoluteUri;
                        BasePage.ResponseRedirect("../User/Login.aspx?ReturnUrl=" + HttpUtility.UrlEncode(absoluteUri));
                    }
                }
                IList<SurveyFieldInfo> fieldList = SurveyField.GetFieldList(surveyId);
                SurveyRecordInfo surveyrecordinfo = new SurveyRecordInfo();
                DataTable table = new DataTable();
                table.Columns.Add("Option", typeof(string));
                table.Columns.Add("Input", typeof(string));
                table.Columns.Add("QuestionType", typeof(int));
                table.Columns.Add("InputType", typeof(int));
                table.Columns.Add("QuestionId", typeof(int));
                foreach (SurveyFieldInfo info4 in fieldList)
                {
                    string answer = DataSecurity.FilterBadChar(base.Request.Form["Q" + info4.QuestionId]);
                    this.CheckForm(info4, answer);
                    DataRow row = table.NewRow();
                    row["Option"] = answer;
                    row["QuestionType"] = info4.QuestionType;
                    row["InputType"] = info4.InputType;
                    row["QuestionId"] = info4.QuestionId;
                    if (info4.InputType > 0)
                    {
                        string str3 = DataSecurity.FilterBadChar(base.Request.Form["Q" + info4.QuestionId + "Input"]);
                        row["Input"] = str3;
                    }
                    if (((info4.QuestionType == 2) || (info4.QuestionType == 4)) || (info4.QuestionType == 7))
                    {
                        SurveyVote.Vote(surveyId, info4.QuestionId, DataConverter.CLng(answer));
                    }
                    if ((info4.QuestionType == 3) || (info4.QuestionType == 5))
                    {
                        foreach (string str4 in answer.Split(new char[] { ',' }))
                        {
                            SurveyVote.Vote(surveyId, info4.QuestionId, DataConverter.CLng(str4));
                        }
                    }
                    table.Rows.Add(row);
                }
                surveyrecordinfo.Answer = table;
                surveyrecordinfo.UserName = PEContext.Current.User.UserInfo.UserName;
                surveyrecordinfo.IP = PEContext.Current.UserHostAddress;
                surveyrecordinfo.SubmitTime = DateTime.Now;
                surveyrecordinfo.SurveyId = surveyId;
                if (!SurveyRecord.SaveSurveyRecord(surveyrecordinfo))
                {
                    DynamicPage.WriteErrMsg("提交问卷失败！");
                }
                else
                {
                    DynamicPage.WriteSuccessMsg("提交问卷成功！", "");
                }
            }
        }
    }
}

