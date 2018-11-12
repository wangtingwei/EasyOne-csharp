namespace EasyOne.Templates
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Model.Contents;
    using EasyOne.Model.TemplateProc;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Xml;
    /// <summary>
    /// 模板转换类
    /// </summary>
    public sealed class TemplateTransform
    {
        private int loopmark;
        private IList<PageInfo> tempLabelPageList = new List<PageInfo>();
        private NameValueCollection tempstr = new NameValueCollection();
        /// <summary>
        /// 支付模板处理
        /// </summary>
        /// <param name="templateInfo"></param>
        private void Charge(TemplateInfo templateInfo)
        {
            if (templateInfo.IsDynamicPage)
            {
                ChargeManage manage = new ChargeManage();
                if (manage.CheckPermission())
                {
                    manage.ExecuteContentCharge();
                }
                if (!string.IsNullOrEmpty(manage.ErrMsg))
                {
                    string pattern = @"{PE\.Charge}(([\s\S](?!{PE\.Charge))*?){\/PE.Charge}";
                    foreach (Match match in Regex.Matches(templateInfo.TemplateContent, pattern, RegexOptions.IgnoreCase))
                    {
                        templateInfo.TemplateContent = templateInfo.TemplateContent.Replace(match.Value, manage.ErrMsg);
                    }
                }
            }
            else if (templateInfo.PageType == 0)
            {
                ContentPermissionInfo contentPermissionInfoById = PermissionContent.GetContentPermissionInfoById(DataConverter.CLng(templateInfo.QueryList["id"]));
                if (!contentPermissionInfoById.IsNull)
                {
                    if (contentPermissionInfoById.PermissionType == 0)
                    {
                        if (!ContentCharge.GetContentChargeInfoById(DataConverter.CLng(templateInfo.QueryList["id"])).IsNull)
                        {
                            this.ChargeTips(templateInfo);
                        }
                    }
                    else
                    {
                        this.ChargeTips(templateInfo);
                    }
                }
            }
            templateInfo.TemplateContent = templateInfo.TemplateContent.Replace("{PE.Charge}", string.Empty).Replace("{/PE.Charge}", string.Empty);
        }

        private void ChargeTips(TemplateInfo templateInfo)
        {
            string newValue = "$$$EasyOne.ChargeTips$$$";
            string pattern = @"{PE\.Charge}(([\s\S](?!{PE\.Charge))*?){\/PE.Charge}";
            foreach (Match match in Regex.Matches(templateInfo.TemplateContent, pattern, RegexOptions.IgnoreCase))
            {
                templateInfo.TemplateContent = templateInfo.TemplateContent.Replace(match.Value, newValue);
            }
        }
        /// <summary>
        /// 内容标签转换
        /// </summary>
        /// <param name="OrangeStr"></param>
        /// <param name="templateInfo"></param>
        private void ContentLabelProc(string OrangeStr, TemplateInfo templateInfo)
        {
            XmlDocument document = new XmlDocument();
            LabelInfo labelInfo = new LabelInfo();
            labelInfo.RootPath = templateInfo.RootPath;
            labelInfo.PageName = templateInfo.PageName;
            try
            {
                document.LoadXml(FormatLabel(OrangeStr));
                foreach (XmlAttribute attribute in document.FirstChild.Attributes)
                {
                    labelInfo.OriginalData[attribute.Name.ToLower()] = attribute.Value;
                }
            }
            catch (XmlException exception)
            {
                templateInfo.TemplateContent = templateInfo.TemplateContent.Replace(OrangeStr, "[err:内容标签" + OrangeStr.Replace("{", string.Empty).Replace("/}", string.Empty) + "错，原因：" + exception.Message + "]");
                return;
            }
            bool flag = DataConverter.CBoolean(labelInfo.OriginalData["urlpage"]);
            if (flag)
            {
                labelInfo.Page = templateInfo.CurrentPage;
            }
            string key = "CK_Label_TransformCacheData_" + labelInfo.OriginalData["id"] + "_" + labelInfo.OriginalData["cacheid"];
            int seconds = DataConverter.CLng(labelInfo.OriginalData["cachetime"]);
            if ((seconds > 0) && (SiteCache.Get(key) != null))
            {
                labelInfo = (LabelInfo) SiteCache.Get(key);
            }
            else
            {
                labelInfo = LabelTransform.GetLabel(labelInfo);
                if (seconds > 0)
                {
                    SiteCache.Insert(key, labelInfo, seconds);
                }
            }
            string str2 = labelInfo.OriginalData["span"];
            string str3 = labelInfo.OriginalData["id"].ToLower().Trim();
            string str4 = labelInfo.OriginalData["class"];
            if (!string.IsNullOrEmpty(str2))
            {
                if (string.IsNullOrEmpty(str4))
                {
                    labelInfo.LabelContent.Insert(0, "<" + str2 + " id=\"pe100_" + str3 + "\">");
                }
                else
                {
                    labelInfo.LabelContent.Insert(0, "<" + str2 + " id=\"pe100_" + str3 + "\" class=\"" + str4 + "\">");
                }
                labelInfo.LabelContent.Append("</" + str2 + ">");
            }
            if (DataConverter.CBoolean(labelInfo.OriginalData["noprocinlabel"]))
            {
                this.tempstr.Add(this.loopmark.ToString(), labelInfo.LabelContent.ToString());
                templateInfo.TemplateContent = templateInfo.TemplateContent.Replace(OrangeStr, "###labelmark" + this.loopmark.ToString() + "###");
                this.loopmark++;
            }
            else
            {
                templateInfo.TemplateContent = templateInfo.TemplateContent.Replace(OrangeStr, labelInfo.LabelContent.ToString());
            }
            if (DataConverter.CBoolean(labelInfo.OriginalData["page"]))
            {
                PageInfo item = new PageInfo();
                item.PageName = templateInfo.PageName;
                item.SpanName = str3;
                item.IsDynamicPage = templateInfo.IsDynamicPage;
                if ((labelInfo.PageSize > 0) && (labelInfo.TotalPub > 0))
                {
                    int num2 = labelInfo.TotalPub / labelInfo.PageSize;
                    if ((labelInfo.TotalPub % labelInfo.PageSize) > 0)
                    {
                        num2++;
                    }
                    if (num2 < 1)
                    {
                        num2 = 1;
                    }
                    item.PageNum = num2;
                    item.PageSize = labelInfo.PageSize;
                    item.CurrentPage = templateInfo.CurrentPage;
                    item.TotalPub = labelInfo.TotalPub;
                    if (flag)
                    {
                        item.IsMainPage = true;
                    }
                    this.tempLabelPageList.Add(item);
                }
            }
        }
        /// <summary>
        /// 数据源标签转换
        /// </summary>
        /// <param name="getLabel"></param>
        /// <param name="templateInfo"></param>
        private void DatasourceLabelProc(string getLabel, TemplateInfo templateInfo)
        {
            XmlDocument document = new XmlDocument();
            string xml = getLabel.Replace("{", "<").Replace("}", ">");
            LabelInfo inLabel = new LabelInfo();
            inLabel.RootPath = templateInfo.RootPath;
            inLabel.PageName = templateInfo.PageName;
            try
            {
                document.LoadXml(xml);
                foreach (XmlAttribute attribute in document.FirstChild.Attributes)
                {
                    inLabel.OriginalData[attribute.Name.ToLower()] = attribute.Value;
                }
            }
            catch (XmlException exception)
            {
                templateInfo.TemplateContent = templateInfo.TemplateContent.Replace(getLabel, "[err:数据源标签" + getLabel.Replace("{", string.Empty).Replace("}", string.Empty) + "格式错，原因：" + exception.Message + "]");
                return;
            }
            string str2 = inLabel.OriginalData["id"];
            string str3 = inLabel.OriginalData["datasource"];
            if (!string.IsNullOrEmpty(str2))
            {
                str2 = str2.ToLower().Trim();
                if (string.IsNullOrEmpty(str3))
                {
                    templateInfo.TemplateContent = templateInfo.TemplateContent.Replace(getLabel, "[err:数据源标签" + getLabel.Replace("{", string.Empty).Replace("}", string.Empty) + "错，原因：请指定DataSource]");
                }
                else
                {
                    str3 = str3.ToLower().Trim();
                    bool flag = DataConverter.CBoolean(inLabel.OriginalData["urlpage"]);
                    if (flag)
                    {
                        inLabel.Page = templateInfo.CurrentPage;
                    }
                    string key = "CK_Label_TransformCacheXmlData_" + str2 + "_" + inLabel.OriginalData["cacheid"];
                    int seconds = DataConverter.CLng(inLabel.OriginalData["cachetime"]);
                    if ((seconds > 0) && (SiteCache.Get(key) != null))
                    {
                        inLabel = (LabelInfo) SiteCache.Get(key);
                    }
                    else
                    {
                        inLabel = LabelTransform.GetLabelDataTable(str3, templateInfo.CurrentPage, inLabel);
                        if (seconds > 0)
                        {
                            SiteCache.Insert(key, inLabel, seconds);
                        }
                    }
                    try
                    {
                        document.LoadXml(inLabel.LabelContent.ToString());
                    }
                    catch (XmlException exception2)
                    {
                        templateInfo.TemplateContent = templateInfo.TemplateContent.Replace(getLabel, "[err:数据源标签" + getLabel.Replace("{", string.Empty).Replace("}", string.Empty) + "返回数据错，原因：" + exception2.Message + "，源码：" + inLabel.LabelContent.ToString() + "]");
                        return;
                    }
                    templateInfo.TemplateContent = templateInfo.TemplateContent.Replace(getLabel, string.Empty);
                    string pattern = @"{PE\.Repeat(.*)}(([\s\S](?!{PE\.Repeat))*?)\{\/PE.Repeat}";
                    foreach (Match match in Regex.Matches(templateInfo.TemplateContent, pattern, RegexOptions.IgnoreCase))
                    {
                        XmlNode firstChild;
                        if (match.Groups.Count < 3)
                        {
                            templateInfo.TemplateContent = templateInfo.TemplateContent.Replace(match.Value, "[err:循环标签" + match.Value.Replace("{", string.Empty).Replace("}", string.Empty) + "格式错");
                            continue;
                        }
                        XmlDocument document2 = new XmlDocument();
                        xml = "<root " + match.Groups[1].Value + " />";
                        try
                        {
                            document2.LoadXml(xml);
                            firstChild = document2.FirstChild;
                        }
                        catch (XmlException exception3)
                        {
                            templateInfo.TemplateContent = templateInfo.TemplateContent.Replace(match.Value, "[err:循环标签{PE:Repeat " + match.Groups[1].Value + " /}错，原因：" + exception3.Message + "]");
                            continue;
                        }
                        if (string.Compare(XmlManage.GetAttributesValue(firstChild, "id"), str2, StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            string input = match.Groups[2].Value;
                            int num2 = 0;
                            string str7 = string.Empty;
                            string str8 = @"{PE\.RMod(.*)}(([\s\S](?!{PE\.RMod))*?)\{\/PE.RMod}";
                            foreach (Match match2 in Regex.Matches(input, str8, RegexOptions.IgnoreCase))
                            {
                                if (match2.Groups.Count >= 3)
                                {
                                    XmlDocument document3 = new XmlDocument();
                                    xml = "<root " + match2.Groups[1].Value + " />";
                                    bool flag2 = true;
                                    try
                                    {
                                        document3.LoadXml(xml);
                                        num2 = DataConverter.CLng(XmlManage.GetAttributesValue(document3.FirstChild, "mod"));
                                    }
                                    catch (XmlException)
                                    {
                                        flag2 = false;
                                    }
                                    if (flag2)
                                    {
                                        str7 = match2.Groups[2].Value;
                                    }
                                    input = input.Replace(match2.Value, string.Empty);
                                }
                            }
                            int num3 = DataConverter.CLng(XmlManage.GetAttributesValue(firstChild, "loopbegin"), 0);
                            int count = DataConverter.CLng(XmlManage.GetAttributesValue(firstChild, "loop"), 0);
                            int num5 = DataConverter.CLng(XmlManage.GetAttributesValue(firstChild, "countbase"), 0);
                            if ((num3 >= 0) && (count >= 0))
                            {
                                if (num3 >= document.DocumentElement.ChildNodes.Count)
                                {
                                    num3 = document.DocumentElement.ChildNodes.Count - 1;
                                    if (num3 < 0)
                                    {
                                        num3 = 0;
                                    }
                                }
                                if ((count == 0) || (count > document.DocumentElement.ChildNodes.Count))
                                {
                                    count = document.DocumentElement.ChildNodes.Count;
                                    if (count < 0)
                                    {
                                        count = 0;
                                    }
                                }
                                if (num3 > count)
                                {
                                    num3 = count;
                                }
                                StringBuilder builder = new StringBuilder();
                                for (int i = num3; i < count; i++)
                                {
                                    input = Regex.Replace(input, "{PE.Field.AutoId/}", (i + num5).ToString(), RegexOptions.IgnoreCase);
                                    str7 = Regex.Replace(str7, "{PE.Field.AutoId/}", (i + num5).ToString(), RegexOptions.IgnoreCase);
                                    builder.Append(this.FieldProc(input, str2, (XmlElement) document.DocumentElement.ChildNodes[i], 1, templateInfo));
                                    if ((num2 > 0) && (((i + 1) % num2) == 0))
                                    {
                                        builder.Append(str7);
                                    }
                                }
                                templateInfo.TemplateContent = templateInfo.TemplateContent.Replace(match.Value, builder.ToString());
                                continue;
                            }
                            templateInfo.TemplateContent = templateInfo.TemplateContent.Replace(match.Value, string.Empty);
                        }
                    }
                    templateInfo.TemplateContent = this.FieldProc(templateInfo.TemplateContent, str2, document.DocumentElement, 0, templateInfo);
                    if (DataConverter.CBoolean(inLabel.OriginalData["page"]))
                    {
                        PageInfo item = new PageInfo();
                        item.PageName = templateInfo.PageName;
                        item.SpanName = str2;
                        item.IsDynamicPage = templateInfo.IsDynamicPage;
                        if ((inLabel.PageSize > 0) && (inLabel.TotalPub > 0))
                        {
                            int num7 = inLabel.TotalPub / inLabel.PageSize;
                            if ((inLabel.TotalPub % inLabel.PageSize) > 0)
                            {
                                num7++;
                            }
                            if (num7 < 1)
                            {
                                num7 = 1;
                            }
                            item.PageNum = num7;
                            item.PageSize = inLabel.PageSize;
                            item.CurrentPage = inLabel.Page;
                            item.TotalPub = inLabel.TotalPub;
                            if (flag)
                            {
                                item.IsMainPage = true;
                            }
                            this.tempLabelPageList.Add(item);
                        }
                    }
                }
            }
            else
            {
                templateInfo.TemplateContent = templateInfo.TemplateContent.Replace(getLabel, "[err:数据源标签" + getLabel.Replace("{", string.Empty).Replace("}", string.Empty) + "错，原因：请指定ID]");
            }
        }
        /// <summary>
        /// 数据字段标签解析
        /// </summary>
        /// <param name="templateStr"></param>
        /// <param name="dataSourceid"></param>
        /// <param name="inode"></param>
        /// <param name="proctype"></param>
        /// <param name="templateInfo"></param>
        /// <returns></returns>
        private string FieldProc(string templateStr, string dataSourceid, XmlElement inode, int proctype, TemplateInfo templateInfo)
        {
            XmlDocument document = new XmlDocument();
            string pattern = @"{PE\.Field([\s\S](?!{PE))*?\/}";
            foreach (Match match in Regex.Matches(templateStr, pattern, RegexOptions.IgnoreCase))
            {
                XmlElement firstChild;
                try
                {
                    document.LoadXml(match.Value.Replace("{", "<").Replace("}", ">"));
                    firstChild = (XmlElement) document.FirstChild;
                }
                catch (XmlException exception)
                {
                    templateStr = templateStr.Replace(match.Value, "[err:数据字段标签" + match.Value.Replace("{", string.Empty).Replace("}", string.Empty) + "错，原因：" + exception.Message + "]");
                    continue;
                }
                if ((proctype == 1) || (string.Compare(dataSourceid, XmlManage.GetAttributesValue(firstChild, "id"), StringComparison.OrdinalIgnoreCase) == 0))
                {
                    if (firstChild.HasAttribute("fieldname"))
                    {
                        string xpath = string.Empty;
                        if (proctype == 1)
                        {
                            xpath = firstChild.GetAttribute("fieldname");
                        }
                        else
                        {
                            xpath = "//" + firstChild.GetAttribute("fieldname");
                        }
                        string errMsg = (inode.SelectSingleNode(xpath) == null) ? string.Empty : inode.SelectSingleNode(xpath).InnerText;
                        if (DataConverter.CBoolean(XmlManage.GetAttributesValue(firstChild, "htmldecode")))
                        {
                            errMsg = DataSecurity.HtmlDecode(errMsg);
                        }
                        if (DataConverter.CLng(XmlManage.GetAttributesValue(firstChild, "length")) > 0)
                        {
                            errMsg = StringHelper.SubString(errMsg, DataConverter.CLng(XmlManage.GetAttributesValue(firstChild, "length")), "");
                        }
                        if (DataConverter.CBoolean(XmlManage.GetAttributesValue(firstChild, "charge")))
                        {
                            if (templateInfo.IsDynamicPage)
                            {
                                ChargeManage manage = new ChargeManage();
                                if (manage.CheckPermission())
                                {
                                    manage.ExecuteContentCharge();
                                }
                                if (!string.IsNullOrEmpty(manage.ErrMsg))
                                {
                                    if (DataConverter.CBoolean(XmlManage.GetAttributesValue(firstChild, "showerr")))
                                    {
                                        errMsg = manage.ErrMsg;
                                    }
                                    else
                                    {
                                        errMsg = "";
                                    }
                                }
                            }
                            else if (templateInfo.PageType == 0)
                            {
                                if (DataConverter.CBoolean(XmlManage.GetAttributesValue(firstChild, "showerr")))
                                {
                                    ContentPermissionInfo contentPermissionInfoById = PermissionContent.GetContentPermissionInfoById(DataConverter.CLng(templateInfo.QueryList["id"]));
                                    if (!contentPermissionInfoById.IsNull)
                                    {
                                        if (contentPermissionInfoById.PermissionType == 0)
                                        {
                                            if (!ContentCharge.GetContentChargeInfoById(DataConverter.CLng(templateInfo.QueryList["id"])).IsNull)
                                            {
                                                errMsg = "$$$EasyOne.ChargeTips$$$";
                                            }
                                        }
                                        else
                                        {
                                            errMsg = "$$$EasyOne.ChargeTips$$$";
                                        }
                                    }
                                }
                                else
                                {
                                    errMsg = "";
                                }
                            }
                        }
                        if (DataConverter.CBoolean(XmlManage.GetAttributesValue(firstChild, "noprocinlabel")))
                        {
                            this.tempstr.Add(this.loopmark.ToString(), errMsg);
                            templateStr = templateStr.Replace(match.Value, "###labelmark" + this.loopmark.ToString() + "###");
                            this.loopmark++;
                        }
                        else
                        {
                            templateStr = templateStr.Replace(match.Value, errMsg);
                        }
                        continue;
                    }
                    templateStr = templateStr.Replace(match.Value, string.Empty);
                }
            }
            return templateStr;
        }
        /// <summary>
        /// 分页标签解析
        /// </summary>
        /// <param name="templateInfo"></param>
        private void FiltPage(TemplateInfo templateInfo)
        {
            XmlDocument document = new XmlDocument();
            string pattern = @"{PE\.Page([\s\S](?!{PE))*?\/}";
            foreach (Match match in Regex.Matches(templateInfo.TemplateContent, pattern, RegexOptions.IgnoreCase))
            {
                try
                {
                    document.LoadXml(FormatLabel(match.Value));
                }
                catch (XmlException exception)
                {
                    templateInfo.TemplateContent = templateInfo.TemplateContent.Replace(match.ToString(), "[err:分页标签" + match.Value.Replace("{", string.Empty).Replace("/}", string.Empty) + "错，原因：" + exception.Message + "]");
                    continue;
                }
                XmlElement firstChild = (XmlElement) document.FirstChild;
                foreach (PageInfo info in this.tempLabelPageList)
                {
                    string str2;
                    if (string.Compare(info.SpanName, XmlManage.GetAttributesValue(firstChild, "datasource"), StringComparison.OrdinalIgnoreCase) != 0)
                    {
                        continue;
                    }
                    if (templateInfo.PageType == 1)
                    {
                        str2 = LabelTransform.GetListPage(XmlManage.GetAttributesValue(firstChild, "id"), info, XmlManage.GetAttributesValue(firstChild, "unitname"));
                    }
                    else
                    {
                        str2 = LabelTransform.GetPage(XmlManage.GetAttributesValue(firstChild, "id"), info, XmlManage.GetAttributesValue(firstChild, "unitname"));
                    }
                    string attributesValue = XmlManage.GetAttributesValue(firstChild, "span");
                    string str4 = XmlManage.GetAttributesValue(firstChild, "class");
                    if (string.IsNullOrEmpty(attributesValue))
                    {
                        attributesValue = "span";
                    }
                    if (string.IsNullOrEmpty(str4))
                    {
                        str4 = "pagecss";
                    }
                    string str5 = "<" + attributesValue + " id=\"pe100_page_" + info.SpanName + "\"";
                    if (!string.IsNullOrEmpty(str4))
                    {
                        str5 = str5 + " class=\"" + str4 + "\"";
                    }
                    str5 = str5 + ">";
                    str2 = str5 + str2 + "</" + attributesValue + ">";
                    templateInfo.TemplateContent = templateInfo.TemplateContent.Replace(match.Value, str2);
                    if ((info.PageNum > 0) && info.IsMainPage)
                    {
                        templateInfo.PageNum = info.PageNum;
                        templateInfo.TotalPub = info.TotalPub;
                    }
                }
                templateInfo.TemplateContent = templateInfo.TemplateContent.Replace(match.Value, string.Empty);
            }
        }
        /// <summary>
        /// 内容分页标签解析
        /// </summary>
        /// <param name="templateInfo"></param>
        private void FiltPageContent(TemplateInfo templateInfo)
        {
            string pattern = @"{PE\.ContentPage(.*)}(([\s\S](?!{PE\.ContentPage))*?){\/PE.ContentPage}";
            foreach (Match match in Regex.Matches(templateInfo.TemplateContent, pattern, RegexOptions.IgnoreCase))
            {
                XmlElement firstChild;
                XmlDocument document = new XmlDocument();
                try
                {
                    document.LoadXml("<root " + match.Groups[1].Value + " />");
                    firstChild = (XmlElement) document.FirstChild;
                }
                catch (XmlException exception)
                {
                    templateInfo.TemplateContent = templateInfo.TemplateContent.Replace(match.ToString(), "[err:内容分页标签参数" + match.Groups[1].Value + "错，原因：" + exception.Message + "]");
                    continue;
                }
                string attributesValue = XmlManage.GetAttributesValue(firstChild, "id");
                bool flag = DataConverter.CBoolean(XmlManage.GetAttributesValue(firstChild, "urlpage"));
                if (!string.IsNullOrEmpty(attributesValue))
                {
                    attributesValue = attributesValue.ToLower().Trim();
                    ContentPageInfo contentPageInfo = new ContentPageInfo();
                    contentPageInfo.Parameter = "<root " + match.Groups[1].Value + " />";
                    contentPageInfo.Content = match.Groups[2].Value;
                    contentPageInfo.PageName = templateInfo.PageName;
                    if (flag)
                    {
                        contentPageInfo.CurrentPage = templateInfo.CurrentPage;
                    }
                    else
                    {
                        contentPageInfo.CurrentPage = 1;
                    }
                    contentPageInfo.IsDynamicPage = templateInfo.IsDynamicPage;
                    LabelTransform.TransContentPageLabel(contentPageInfo);
                    templateInfo.TemplateContent = templateInfo.TemplateContent.Replace(match.Value, contentPageInfo.Content);
                    PageInfo item = new PageInfo();
                    item.PageName = templateInfo.PageName;
                    item.SpanName = attributesValue;
                    item.IsDynamicPage = templateInfo.IsDynamicPage;
                    item.CurrentPage = contentPageInfo.CurrentPage;
                    if (flag)
                    {
                        item.IsMainPage = true;
                    }
                    item.PageNum = contentPageInfo.PageNum;
                    item.TotalPub = contentPageInfo.Content.Length;
                    string str3 = @"\[PageSet\]([\s\S]*?)\[\/PageSet\]";
                    foreach (Match match2 in Regex.Matches(match.Groups[2].Value, str3, RegexOptions.IgnoreCase))
                    {
                        item.PageOtherSet = match2.ToString().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&apos;", "'").Replace("&quot;", "\"");
                        templateInfo.TemplateContent = templateInfo.TemplateContent.Replace(match2.Value, string.Empty);
                    }
                    this.tempLabelPageList.Add(item);
                }
            }
        }
        /// <summary>
        /// 标签格式化
        /// </summary>
        /// <param name="ilabelstr"></param>
        /// <returns></returns>
        private static string FormatLabel(string ilabelstr)
        {
            return ilabelstr.Replace("/n", string.Empty).Replace("{", "<").Replace("}", ">");
        }
        /// <summary>
        /// 获取html代码,页面调用此方法解析
        /// </summary>
        /// <param name="templateInfo"></param>
        /// <returns></returns>
        public static TemplateInfo GetHtml(TemplateInfo templateInfo)
        {
            TemplateTransform transform = new TemplateTransform();
            return transform.ParseHtml(templateInfo);
        }
        /// <summary>
        /// 模板转换
        /// </summary>
        /// <param name="templateInfo"></param>
        /// <returns></returns>
        private TemplateInfo ParseHtml(TemplateInfo templateInfo)
        {
            if (!string.IsNullOrEmpty(templateInfo.TemplateContent))
            {
                bool flag = false;
                string pattern = @"{PE\.Label([\s\S](?!{PE))*?\/}";
                string str2 = @"{PE\.DataSource([\s\S](?!{PE))*?\/}";
                this.loopmark = 0;
                do
                {
                    flag = false;
                    this.SystemLabeTransform(templateInfo, templateInfo.QueryList);
                    //采用正则表达式检测数据源标签
                    foreach (Match match in Regex.Matches(templateInfo.TemplateContent, str2, RegexOptions.IgnoreCase))
                    {
                        this.DatasourceLabelProc(match.Value, templateInfo);
                        flag = true;
                    }
                    this.SystemLabeTransform(templateInfo, templateInfo.QueryList);
                    //采用正则表达式检测内容标签
                    foreach (Match match2 in Regex.Matches(templateInfo.TemplateContent, pattern, RegexOptions.IgnoreCase))
                    {
                        this.ContentLabelProc(match2.Value, templateInfo);
                        flag = true;
                    }
                }
                while (flag);
                for (int i = 0; i < this.loopmark; i++)
                {
                    templateInfo.TemplateContent = templateInfo.TemplateContent.Replace("###labelmark" + i.ToString() + "###", this.tempstr[i.ToString()]);
                }
                this.FiltPageContent(templateInfo);
                this.FiltPage(templateInfo);
                this.tempLabelPageList.Clear();
                this.Charge(templateInfo);
                //通过xls解析模板
                templateInfo.TemplateContent = DataSecurity.PELabelDecode(templateInfo.TemplateContent);
            }
            return templateInfo;
        }
        /// <summary>
        /// 时间转换
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string SysCDate(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                input = DataConverter.CDate(input).ToString("yyyy-MM-dd");
                return input;
            }
            input = string.Empty;
            return input;
        }
        /// <summary>
        ///  系统标签转换
        /// </summary>
        /// <param name="templateInfo"></param>
        /// <param name="queryList"></param>
        private void SystemLabeTransform(TemplateInfo templateInfo, NameValueCollection queryList)
        {
            string pattern = "@Request(int|Bool|Date|Dec)?_";
            foreach (Match match in Regex.Matches(templateInfo.TemplateContent, pattern, RegexOptions.IgnoreCase))
            {
                for (int i = 0; i < queryList.Count; i++)
                {
                    string str4 = match.Groups[1].Value.ToLower();
                    if (str4 == null)
                    {
                        goto Label_0185;
                    }
                    if (!(str4 == "int"))
                    {
                        if (str4 == "bool")
                        {
                            goto Label_00D4;
                        }
                        if (str4 == "date")
                        {
                            goto Label_0112;
                        }
                        if (str4 == "dec")
                        {
                            goto Label_014A;
                        }
                        goto Label_0185;
                    }
                    templateInfo.TemplateContent = Regex.Replace(templateInfo.TemplateContent, match.Value + queryList.GetKey(i), DataConverter.CLng(queryList.Get(i)).ToString(), RegexOptions.IgnoreCase);
                    continue;
                Label_00D4:
                    templateInfo.TemplateContent = Regex.Replace(templateInfo.TemplateContent, match.Value + queryList.GetKey(i), DataConverter.CBoolean(queryList.Get(i)).ToString(), RegexOptions.IgnoreCase);
                    continue;
                Label_0112:
                    templateInfo.TemplateContent = Regex.Replace(templateInfo.TemplateContent, match.Value + queryList.GetKey(i), this.SysCDate(queryList.Get(i)).ToString(), RegexOptions.IgnoreCase);
                    continue;
                Label_014A:
                    templateInfo.TemplateContent = Regex.Replace(templateInfo.TemplateContent, match.Value + queryList.GetKey(i), DataConverter.CDecimal(queryList.Get(i)).ToString(), RegexOptions.IgnoreCase);
                    continue;
                Label_0185:
                    templateInfo.TemplateContent = Regex.Replace(templateInfo.TemplateContent, match.Value + queryList.GetKey(i), DataSecurity.FilterBadChar(queryList.Get(i)), RegexOptions.IgnoreCase);
                }
            }
            pattern = @"{PE\.SiteConfig\.(([\s\S](?!{PE))*?)\/}";
            foreach (Match match2 in Regex.Matches(templateInfo.TemplateContent, pattern, RegexOptions.IgnoreCase))
            {
                if (match2.Groups.Count > 1)
                {
                    string siteConfigLabel = LabelTransform.GetSiteConfigLabel(match2.Groups[1].Value.Trim().ToLower());
                    templateInfo.TemplateContent = templateInfo.TemplateContent.Replace(match2.Value, siteConfigLabel);
                }
                else
                {
                    templateInfo.TemplateContent = templateInfo.TemplateContent.Replace(match2.Value, "[err:系统标签'" + match2.Value.Replace("{", string.Empty).Replace("}", string.Empty) + "'错误！]");
                }
            }
        }
    }
}

