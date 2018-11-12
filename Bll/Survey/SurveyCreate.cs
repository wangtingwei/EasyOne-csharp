namespace EasyOne.Survey
{
    using EasyOne.Common;
    using EasyOne.IDal.Survey;
    using EasyOne.Model.Survey;
    using EasyOne.Model.TemplateProc;
    using EasyOne.Templates;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using EasyOne.DalFactory;

    public sealed class SurveyCreate
    {
        private static readonly ISurveyCreate dal = DataAccess.CreateSurveyCreate();

        private SurveyCreate()
        {
        }

        private static void ContentLengthJS(SurveyFieldInfo info, StringBuilder tempFormJs, string otherName)
        {
            if ((0 < info.ContentLength) && (info.ContentLength <= 0xff))
            {
                tempFormJs.Append(string.Concat(new object[] { "if (document.myform.", otherName, ".value.length>255||document.myform.", otherName, ".value.length>", info.ContentLength, ")\n" }));
                tempFormJs.Append("{flag=false;alert('填写的内容过长');\n");
                tempFormJs.Append("document.myform." + otherName + ".focus();return flag}\n");
            }
        }

        private static void EnableNullJS(SurveyFieldInfo info, StringBuilder tempFormJs, string problemName)
        {
            if (info.EnableNull)
            {
                tempFormJs.Append("if (document.myform." + problemName + ".value=='')\n");
                tempFormJs.Append("{flag=false;");
                tempFormJs.Append("alert('" + info.QuestionContent + "不能为空');\n");
                tempFormJs.Append("document.myform." + problemName + ".focus();\n");
                tempFormJs.Append("return flag}\n");
            }
        }

        public static bool FileNameExists(string fileName)
        {
            return dal.FileNameExists(fileName);
        }

        public static string GetDynamicContent(string createContent, int surveyId)
        {
            Match match = Regex.Match(createContent, @"<head[^>]*>([\s\S]*)</head>");
            if (match.Success)
            {
                createContent = Regex.Replace(createContent, @"<head[^>]*>[\s\S]*</head>", "<head runat=\"server\">" + match.Groups[1].Value + "</head>", RegexOptions.IgnoreCase);
            }
            else
            {
                createContent = Regex.Replace(createContent, "(<html[^>]*>)", "<html xmlns=\"http://www.w3.org/1999/xhtml\">\n<head runat=\"server\"/>", RegexOptions.IgnoreCase);
            }
            createContent = "<%@ Page Language=\"c#\" StylesheetTheme=\"\"%>\n" + createContent;
            createContent = GetServerCode(createContent, surveyId);
            return createContent;
        }

        private static string GetFormJS(SurveyFieldInfo info)
        {
            StringBuilder tempFormJs = new StringBuilder();
            tempFormJs.Append("");
            string problemName = "Q" + info.QuestionId.ToString();
            string otherName = "Q" + info.QuestionId.ToString() + "Input";
            info.QuestionContent = DataSecurity.HtmlEncode(info.QuestionContent);
            switch (info.QuestionType)
            {
                case 0:
                    EnableNullJS(info, tempFormJs, problemName);
                    ContentLengthJS(info, tempFormJs, problemName);
                    break;

                case 1:
                    EnableNullJS(info, tempFormJs, problemName);
                    break;

                case 2:
                    if (info.EnableNull)
                    {
                        tempFormJs.Append("var Input1=false\n");
                        tempFormJs.Append("for (i=0;i<document.myform." + problemName + ".length;i++) \n");
                        tempFormJs.Append("{Input1=Input1||document.myform." + problemName + "[i].checked;} \n");
                        tempFormJs.Append("if (Input1==false){alert('请选择" + info.QuestionContent + "');\n");
                        tempFormJs.Append("document.myform." + problemName + "[0].focus();\n");
                        tempFormJs.Append("return Input1};\n");
                    }
                    if (info.InputType == 1)
                    {
                        ContentLengthJS(info, tempFormJs, otherName);
                    }
                    break;

                case 3:
                    if (info.EnableNull)
                    {
                        tempFormJs.Append("var Input2=false\n");
                        tempFormJs.Append("for (i=0;i<document.myform." + problemName + ".length;i++) \n");
                        tempFormJs.Append("{Input2=Input2||document.myform." + problemName + "[i].checked;} \n");
                        tempFormJs.Append("document.myform." + problemName + "[0].focus();\n");
                        tempFormJs.Append("if (Input2==false){alert('请选择" + info.QuestionContent + "');\n");
                        tempFormJs.Append("return Input2};\n");
                    }
                    if (info.InputType == 1)
                    {
                        ContentLengthJS(info, tempFormJs, otherName);
                    }
                    break;

                case 4:
                    if (info.EnableNull)
                    {
                        tempFormJs.Append("if (document.myform." + problemName + ".selectedIndex==-1)\n");
                        tempFormJs.Append("{flag=false;alert('请选择" + info.QuestionContent + "');\n");
                        tempFormJs.Append("document.myform." + problemName + ".focus();return flag}\n");
                    }
                    break;

                case 6:
                    EnableNullJS(info, tempFormJs, problemName);
                    break;

                case 7:
                    if (info.EnableNull)
                    {
                        tempFormJs.Append("var Input1=false\n");
                        tempFormJs.Append("for (i=0;i<document.myform." + problemName + ".length;i++) \n");
                        tempFormJs.Append("{Input1=Input1||document.myform." + problemName + "[i].checked;} \n");
                        tempFormJs.Append("if (Input1==false){alert('请选择" + info.QuestionContent + "');\n");
                        tempFormJs.Append("document.myform." + problemName + "[0].focus();\n");
                        tempFormJs.Append("return Input1};\n");
                    }
                    break;

                case 8:
                    EnableNullJS(info, tempFormJs, problemName);
                    tempFormJs.Append("if (document.myform." + problemName + ".value!=''&&isNaN(myform." + problemName + ".value))\n");
                    tempFormJs.Append("{alert('请用数字填写" + info.QuestionContent + "');\n");
                    tempFormJs.Append("flag=false;\n");
                    tempFormJs.Append("document.myform." + problemName + ".focus();\n");
                    tempFormJs.Append("return flag}\n");
                    break;

                case 9:
                    EnableNullJS(info, tempFormJs, problemName);
                    tempFormJs.Append("if (myform." + problemName + ".value!='')  \n");
                    tempFormJs.Append("{if (myform." + problemName + ".value.length<6)  {\n");
                    tempFormJs.Append("flag=false;\n");
                    tempFormJs.Append("alert('您所填的Email太短！');\n");
                    tempFormJs.Append("document.myform." + problemName + ".focus();\n");
                    tempFormJs.Append("return flag}\n");
                    tempFormJs.Append("if (myform." + problemName + ".value.indexOf('@')==-1||myform." + problemName + ".value.indexOf('.')==-1)\n");
                    tempFormJs.Append("{flag=false;\n");
                    tempFormJs.Append("alert('Email格式有问题，是否包含了@和.');\n");
                    tempFormJs.Append("document.myform." + problemName + ".focus();\n");
                    tempFormJs.Append("return flag}}\n");
                    break;
            }
            return tempFormJs.ToString();
        }

        private static string GetServerCode(string createContent, int surveyId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<script runat=\"server\">\r\n");
            builder.Append("    protected void Page_Load(object sender, EventArgs e)\r\n");
            builder.Append("    {\r\n");
            builder.Append("        string returnurl = Request.UrlReferrer == null?string.Empty : Request.UrlReferrer.AbsoluteUri;\r\n");
            builder.Append("        int surveyId = " + surveyId.ToString() + ";\r\n");
            builder.Append("        string ip =EasyOne.Components.PEContext.Current.UserHostAddress;\r\n");
            builder.Append("        EasyOne.Model.Survey.SurveyInfo surveyInfo = EasyOne.Survey.SurveyManager.GetSurveyById(surveyId);\r\n");
            builder.Append("        if (surveyInfo.IsNull)\r\n");
            builder.Append("        {\r\n");
            builder.Append("            EasyOne.Web.UI.DynamicPage.WriteErrMsg(\"<li>该问卷不存在！</li>\",returnurl);\r\n");
            builder.Append("        }\r\n");
            builder.Append("        if (surveyInfo.IsOpen != 1)\r\n");
            builder.Append("        {\r\n");
            builder.Append("            EasyOne.Web.UI.DynamicPage.WriteErrMsg(\"<li>问卷调查尚未启用！</li>\",returnurl);\r\n");
            builder.Append("        }\r\n");
            builder.Append("        if (surveyInfo.EndTime != null && surveyInfo.EndTime < DateTime.Now)\r\n");
            builder.Append("        {\r\n");
            builder.Append("            EasyOne.Web.UI.DynamicPage.WriteErrMsg(\"<li>问卷调查已经结束！</li>\",returnurl);\r\n");
            builder.Append("        }\r\n");
            builder.Append("        if (EasyOne.Survey.SurveyManager.CheckIPLock(ip, surveyInfo))\r\n");
            builder.Append("        {\r\n");
            builder.Append("            EasyOne.Web.UI.DynamicPage.WriteErrMsg(\"<li>对不起！您的IP（\" + ip + \"）被系统限定。您可以和站长联系。</li>\",returnurl);\r\n");
            builder.Append("        }\r\n");
            builder.Append("        if (EasyOne.Survey.SurveyManager.CheckRepeatIP(ip, surveyId, surveyInfo))\r\n");
            builder.Append("        {\r\n");
            builder.Append("            EasyOne.Web.UI.DynamicPage.WriteErrMsg(\"<li>同一用户不允许填写问卷调查超过\" + surveyInfo.IPRepeat.ToString() + \"次！</li>\",returnurl);\r\n");
            builder.Append("        }\r\n");
            builder.Append("        string url = HttpContext.Current.Request.Url.AbsoluteUri;\r\n");
            builder.Append("        if (surveyInfo.NeedLogin == 1)\r\n");
            builder.Append("        {\r\n");
            builder.Append("            if (string.IsNullOrEmpty(EasyOne.Components.PEContext.Current.User.UserName))\r\n");
            builder.Append("            {\r\n");
            builder.Append("                  Response.Redirect(\"../User/Login.aspx?ReturnUrl=\" + HttpUtility.UrlEncode(url));\r\n");
            builder.Append("            }\r\n");
            builder.Append("        }\r\n");
            builder.Append("    }\r\n");
            builder.Append("</script>\r\n");
            createContent = createContent + "\r\n" + builder.ToString();
            return createContent;
        }

        private static string GetSurveyContent(IList<SurveyFieldInfo> infoList)
        {
            int num = 1;
            StringBuilder builder = new StringBuilder();
            foreach (SurveyFieldInfo info in infoList)
            {
                int num2;
                int num3;
                int num4;
                int num5;
                info.QuestionContent = DataSecurity.HtmlEncode(info.QuestionContent);
                switch (info.QuestionType)
                {
                    case 0:
                        builder.Append(num + "、" + info.QuestionContent);
                        builder.Append(string.Concat(new object[] { "<input type='text' MaxLength='", info.ContentLength, "' name='Q", info.QuestionId, "'><br /><br />\n" }));
                        goto Label_0724;

                    case 1:
                        builder.Append(num + "、" + info.QuestionContent);
                        builder.Append("<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<textarea name='Q" + info.QuestionId + "' cols='50' rows='5'></textarea><br /><br />\n");
                        goto Label_0724;

                    case 2:
                        builder.Append(string.Concat(new object[] { num, "、", info.QuestionContent, "<br />" }));
                        num2 = 0;
                        goto Label_01C5;

                    case 3:
                        builder.Append(string.Concat(new object[] { num, "、", info.QuestionContent, "<br />\n" }));
                        num3 = 0;
                        goto Label_0325;

                    case 4:
                        builder.Append(string.Concat(new object[] { num, "、", info.QuestionContent, ":\n" }));
                        builder.Append("<select name='Q" + info.QuestionId + "'>\n");
                        num4 = 0;
                        goto Label_048F;

                    case 5:
                        builder.Append(string.Concat(new object[] { num, "、", info.QuestionContent, ":" }));
                        builder.Append("<br />&nbsp;&nbsp;&nbsp;&nbsp;\n<select multiple name='Q" + info.QuestionId + "'>\n");
                        num5 = 0;
                        goto Label_0564;

                    case 6:
                        builder.Append(num + "、" + info.QuestionContent);
                        builder.Append("<input type='text' name='Q" + info.QuestionId + "'><br /><br />\n");
                        goto Label_0724;

                    case 7:
                        builder.Append(string.Concat(new object[] { num, "、", info.QuestionContent, ":\n" }));
                        builder.Append("&nbsp;&nbsp;<input name='Q" + info.QuestionId + "' type='radio' value='1'>是\n");
                        builder.Append("&nbsp;&nbsp;<input name='Q" + info.QuestionId + "' type='radio' value='0'>否<br /><br />\n");
                        goto Label_0724;

                    case 8:
                        builder.Append(num + "、" + info.QuestionContent);
                        builder.Append(string.Concat(new object[] { "<input type='text' MaxLength='", info.ContentLength, "' name='Q", info.QuestionId, "'><br /><br />\n" }));
                        goto Label_0724;

                    case 9:
                        builder.Append(num + "、" + info.QuestionContent);
                        builder.Append(string.Concat(new object[] { "<input type='text' MaxLength='", info.ContentLength, "' name='Q", info.QuestionId, "'><br /><br />\n" }));
                        goto Label_0724;

                    default:
                        goto Label_0724;
                }
            Label_015E:;
                builder.Append(string.Concat(new object[] { "&nbsp;&nbsp;&nbsp;&nbsp;<input name='Q", info.QuestionId, "' type='radio' value='", num2, "'>", info.Settings[num2], "<br />\n" }));
                num2++;
            Label_01C5:
                if (num2 != info.Settings.Count)
                {
                    goto Label_015E;
                }
                switch (info.InputType)
                {
                    case 1:
                        builder.Append(string.Concat(new object[] { "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type='text' name='Q", info.QuestionId, "Input' MaxLength='", info.ContentLength, "'><br /><br />\n" }));
                        goto Label_0724;

                    case 2:
                        builder.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<textarea name='Q" + info.QuestionId + "Input' cols='50' rows='5'></textarea><br /><br />\n");
                        goto Label_0724;

                    default:
                        builder.Append("<br />");
                        goto Label_0724;
                }
            Label_02BA:;
                builder.Append(string.Concat(new object[] { "&nbsp;&nbsp;&nbsp;&nbsp;<input name='Q", info.QuestionId, "' type='checkbox' value='", num3, "'>", info.Settings[num3], "<br />\n" }));
                num3++;
            Label_0325:
                if (num3 != info.Settings.Count)
                {
                    goto Label_02BA;
                }
                switch (info.InputType)
                {
                    case 1:
                        builder.Append(string.Concat(new object[] { "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type='text' name='Q", info.QuestionId, "Input' MaxLength='", info.ContentLength, "'><br /><br />\n" }));
                        goto Label_0724;

                    case 2:
                        builder.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<textarea name='Q" + info.QuestionId + "Input' cols='50' rows='5'></textarea><br /><br />\n");
                        goto Label_0724;

                    default:
                        builder.Append("<br />");
                        goto Label_0724;
                }
            Label_043C:;
                builder.Append(string.Concat(new object[] { "&nbsp;&nbsp;&nbsp;&nbsp;<option value='", num4, "'>", info.Settings[num4], "</option>\n" }));
                num4++;
            Label_048F:
                if (num4 != info.Settings.Count)
                {
                    goto Label_043C;
                }
                builder.Append("</select><br /><br />\n");
                goto Label_0724;
            Label_0511:;
                builder.Append(string.Concat(new object[] { "&nbsp;&nbsp;&nbsp;&nbsp;<option value='", num5, "'>", info.Settings[num5], "</option>\n" }));
                num5++;
            Label_0564:
                if (num5 != info.Settings.Count)
                {
                    goto Label_0511;
                }
                builder.Append("</select><br /><br />\n");
            Label_0724:
                num++;
            }
            return builder.ToString();
        }

        public static string GetSurveyTemplate(SurveyInfo info)
        {
            StringBuilder builder = new StringBuilder(0x80);
            IList<SurveyFieldInfo> fieldList = SurveyField.GetFieldList(info.SurveyId);
            builder.AppendLine("<form name='myform' method='post' action='../Survey/SurveySave.aspx' onSubmit='return CheckForm();'>\n");
            builder.AppendLine("<TABLE cellSpacing=0 cellPadding=0 width=100% border=0>");
            builder.AppendLine("<TBODY>");
            builder.AppendLine("<TR>");
            builder.AppendLine("<TD vAlign=top height=500>");
            builder.AppendLine("<P align=center>" + DataSecurity.HtmlEncode(info.SurveyName) + "</P>");
            if (!string.IsNullOrEmpty(info.SetPassword))
            {
                builder.AppendLine("请输入问卷密码：<input type='password' name='SurveyPassword'><br /><br />");
            }
            builder.AppendLine(GetSurveyContent(fieldList));
            builder.AppendLine("<input name='SurveyID' type='hidden' id='SurveyID' value='" + info.SurveyId + "'>");
            builder.AppendLine("</TD>");
            builder.AppendLine("</TR>");
            builder.AppendLine("</TBODY>");
            builder.AppendLine("</TABLE>");
            builder.AppendLine("<P align=center>");
            builder.AppendLine(" <INPUT type=submit value=提交问卷 name=Submit>");
            builder.AppendLine("</P></form>\n");
            string templateContent = GetTemplateContent(info.Template);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("<script language='javascript'>\n");
            builder2.Append("function CheckForm(){\n");
            builder2.Append("flag=true;\n");
            foreach (SurveyFieldInfo info2 in fieldList)
            {
                builder2.Append("\n" + GetFormJS(info2));
            }
            builder2.Append("return flag }\n");
            builder2.Append("</script>\n");
            return Regex.Replace(Regex.Replace(Regex.Replace(templateContent, @"{PE\.SurveyJS(\s)*\/}", builder2.ToString(), RegexOptions.IgnoreCase), @"{PE\.SurveyName(\s)*\/}", DataSecurity.HtmlEncode(info.SurveyName), RegexOptions.IgnoreCase), @"{PE\.GetSurveyForm(\s)*\/}", builder.ToString(), RegexOptions.IgnoreCase);
        }

        private static string GetTemplateContent(string templatePath)
        {
            TemplateInfo templateInfo = new TemplateInfo();
            templateInfo.QueryList = HttpContext.Current.Request.QueryString;
            templateInfo.PageName = "SurveyFormCreate.aspx";
            templateInfo.TemplateContent = Template.GetTemplateContent(templatePath);
            templateInfo.RootPath = HttpContext.Current.Request.PhysicalApplicationPath;
            templateInfo.CurrentPage = DataConverter.CLng(HttpContext.Current.Request.QueryString["page"], 1);
            return TemplateTransform.GetHtml(templateInfo).TemplateContent;
        }
    }
}

