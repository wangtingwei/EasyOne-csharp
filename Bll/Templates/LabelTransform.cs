namespace EasyOne.Templates
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Enumerations;
    using EasyOne.IDal.TemplateProc;
    using EasyOne.DalFactory;
    using EasyOne.Model.TemplateProc;
    using System;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Caching;
    using System.Xml;
    using System.Xml.XPath;
    using System.Xml.Xsl;
    using EasyOne.Logging;
    /// <summary>
    /// 标签解析类
    /// </summary>
    public sealed class LabelTransform
    {
        private static readonly ILabelProc dal = EasyOne.DalFactory.DataAccess.LabelCode();
        /// <summary>
        /// 构造方法初始化
        /// </summary>
        private LabelTransform()
        {
        }
        /// <summary>
        /// 检测标签是否过期
        /// </summary>
        /// <param name="getdate"></param>
        /// <returns></returns>
        private static bool CheckLabelDate(LabelInfo getdate)
        {
            DateTime time;
            DateTime time2;
            string str = getdate.OriginalData["begintime"];
            string str2 = getdate.OriginalData["endtime"];
            if (string.IsNullOrEmpty(str) || DateTime.TryParse(str, out time))
            {
                time = new DateTime(0x76c, 1, 1);
            }
            if (string.IsNullOrEmpty(str2) || DateTime.TryParse(str2, out time2))
            {
                time2 = new DateTime(0x270f, 12, 0x1f);
            }
            return ((DateTime.Now.ToLocalTime() >= time) && (DateTime.Now.ToLocalTime() <= time2));
        }
        /// <summary>
        /// 将XML转化为指定的样式的转换器
        /// </summary>
        /// <param name="labelInfo">标签实体</param>
        /// <param name="xsltTemplate">XSLT样式表</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        private static XslCompiledTransform GetCacheXslCompiledTransform(LabelInfo labelInfo, string xsltTemplate, string type)
        {
            string str = "";
            if (type == "DataSource")
            {
                str = labelInfo.OriginalData["datasource"];
            }
            else
            {
                str = labelInfo.OriginalData["id"];
            }
            string labelDir = SiteConfig.SiteOption.LabelDir;
            try
            {
                string str4;
                string key = "CK_Label_LabelTransform_XslCompiledTransform_" + str;
                if (HttpContext.Current != null)
                {
                    str4 = HttpContext.Current.Server.MapPath("~/" + labelDir) + @"\" + str + ".config";
                }
                else
                {
                    str4 = AppDomain.CurrentDomain.BaseDirectory + labelDir + @"\" + str + ".config";
                }
                
                XslCompiledTransform transform = SiteCache.Get(key) as XslCompiledTransform;
                //检测缓存中是否存在
                if (transform == null)
                {
                    XmlDocument stylesheet = new XmlDocument();
                    transform = new XslCompiledTransform();
                    string strA = (labelInfo.LabelDefineData["OutType"] == null) ? "" : labelInfo.LabelDefineData["OutType"].Trim();
                    //是否启用对 XSLT document() 函数的支持，是否启用对嵌入式脚本块的支持
                    XsltSettings settings = new XsltSettings(true, true);
                    if (string.Compare(strA, "sin", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        settings = new XsltSettings(true, false);
                    }
                    stylesheet.LoadXml(xsltTemplate);
                    transform.Load(stylesheet, settings, null);
                    if (File.Exists(str4))
                    {
                        SiteCache.Insert(key, transform, new CacheDependency(str4));//add sql
                    }
                }
                return transform;
            }
            catch (XmlException exception)
            {
                labelInfo.LabelContent = new StringBuilder("[err:标签'" + str + "'模板不符合XML规范，原因：" + exception.Message + "]");
                return null;
            }
            catch (XsltException exception2)
            {
                labelInfo.LabelContent = new StringBuilder("[err:标签'" + str + "'装载XSLT模板错误，原因：" + exception2.Message + "]");
                return null;
            }
        }

        public static LabelInfo GetLabel(LabelInfo labelInfo)
        {
            XPathNavigator navigator;
            string str = labelInfo.OriginalData["id"];
            if (string.IsNullOrEmpty(str))//检测标签是否有ID号
            {
                labelInfo.LabelContent = new StringBuilder("[err:标签没有设置ID属性]");
                return labelInfo;
            }
            str = str.ToLower().Trim();
            //根据标签ID获取标签实体
            LabelManageInfo cacheLabelByName = LabelManage.GetCacheLabelByName(str);
            if (cacheLabelByName.IsNull || string.IsNullOrEmpty(cacheLabelByName.Name))
            {
                labelInfo.LabelContent = new StringBuilder("[err:标签'" + str + "'不存在]");
                return labelInfo;
            }
            try
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(cacheLabelByName.Define.ToString());
                foreach (XmlNode node in document.DocumentElement.ChildNodes)
                {
                    if (string.Compare(node.Name, "attributes", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        string innerText = node.SelectSingleNode("name").InnerText;
                        string str3 = labelInfo.OriginalData[innerText];
                        string[] strArray = node.SelectSingleNode("default").InnerText.Split(new string[] { "|||" }, StringSplitOptions.None);
                        if (strArray.Length > 1)
                        {
                            if (string.IsNullOrEmpty(str3))
                            {
                                labelInfo.AttributesData[innerText] = strArray[0];
                            }
                            else
                            {
                                int index = DataConverter.CLng(str3);
                                if (index >= strArray.Length)
                                {
                                    index = 0;
                                }
                                labelInfo.AttributesData[innerText] = strArray[index];
                            }
                        }
                        else if (string.IsNullOrEmpty(str3))
                        {
                            labelInfo.AttributesData[innerText] = node.SelectSingleNode("default").InnerText;
                        }
                        else
                        {
                            labelInfo.AttributesData[innerText] = str3;
                        }
                        continue;
                    }
                    labelInfo.LabelDefineData[node.Name] = node.InnerText;
                }
            }
            catch (XmlException exception)
            {
                labelInfo.LabelContent = new StringBuilder("[err:标签'" + str + "'XML定义错，原因：" + exception.Message + "]");
                return labelInfo;
            }
            if (!CheckLabelDate(labelInfo))
            {
                labelInfo.LabelContent = new StringBuilder(labelInfo.OriginalData["outtime"]);
                return labelInfo;
            }
            if (DataConverter.CBoolean(labelInfo.OriginalData["page"]))
            {
                labelInfo.PageSize = DataConverter.CLng(labelInfo.OriginalData["pagesize"]);
            }
            StringBuilder builder = new StringBuilder(DataSecurity.PELabelEncode(SqlAttributesProc(labelInfo).LabelContent.ToString()));
            if (labelInfo.Error > 0)
            {
                if (labelInfo.Error >= 2)
                {
                    LogLabelTransformException(labelInfo);
                    labelInfo.LabelContent.Remove(0, labelInfo.LabelContent.Length);
                    labelInfo.LabelContent.Append("[err:标签\"" + labelInfo.OriginalData["id"] + "\"查询数据库时出现异常。有关错误的完整说明，请到后台日志管理中查看“异常记录”。]");
                }
                return labelInfo;
            }
            if (string.Compare(builder.ToString(), "static", StringComparison.OrdinalIgnoreCase) == 0)
            {
                labelInfo.LabelContent = StaticLabelProc(labelInfo, cacheLabelByName.Template);
                return labelInfo;
            }
            if (string.Compare(builder.ToString(), "QueryLabel", StringComparison.Ordinal) == 0)
            {
                labelInfo.LabelContent.Remove(0, labelInfo.LabelContent.Length);
                XmlManage manage = XmlManage.Instance(cacheLabelByName.Define.ToString(), XmlType.Content);
                if (labelInfo.TotalPub == -999)
                {
                    labelInfo.LabelContent.Append(manage.GetNodeValue("root/QueryError"));
                    if (labelInfo.LabelContent.Length < 1)
                    {
                        labelInfo.LabelContent.Append("err");
                    }
                    return labelInfo;
                }
                labelInfo.LabelContent.Append(manage.GetNodeValue("root/QuerySucceed"));
                if (labelInfo.LabelContent.Length < 1)
                {
                    labelInfo.LabelContent.Append("ok");
                }
                labelInfo.LabelContent.Replace("<xsl:value-of select=\"TotalPub\"/>", labelInfo.TotalPub.ToString());
                return labelInfo;
            }
            string strA = (labelInfo.LabelDefineData["OutType"] == null) ? "" : labelInfo.LabelDefineData["OutType"].Trim();
            if (string.Compare(strA, "xml", StringComparison.OrdinalIgnoreCase) == 0)
            {
                labelInfo.LabelContent = builder;
                return labelInfo;
            }
            XmlDocument document2 = new XmlDocument();
            try
            {
                document2.LoadXml(builder.ToString());
                navigator = document2.CreateNavigator();
            }
            catch (XmlException exception2)
            {
                labelInfo.LabelContent = new StringBuilder("[err:标签'" + str + "'得到的XML数据错误，原因：" + exception2.Message + "，原始数据:" + builder.ToString() + "]");
                return labelInfo;
            }
            if (string.Compare(strA, "txt", StringComparison.OrdinalIgnoreCase) == 0)
            {
                StringBuilder sb = new StringBuilder();
                if (document2.FirstChild.HasChildNodes)
                {
                    for (int i = 0; i < document2.FirstChild.FirstChild.ChildNodes.Count; i++)
                    {
                        StringHelper.AppendString(sb, document2.FirstChild.FirstChild.ChildNodes.Item(i).InnerText);
                    }
                }
                labelInfo.LabelContent = sb;
                return labelInfo;
            }
            if (cacheLabelByName.Template.Length == 0)
            {
                labelInfo.LabelContent = builder;
                return labelInfo;
            }
            labelInfo.LabelContent.Remove(0, labelInfo.LabelContent.Length);
            XsltArgumentList arguments = new XsltArgumentList();
            XslCompiledTransform transform = GetCacheXslCompiledTransform(labelInfo, cacheLabelByName.Template.ToString(), string.Empty);
            if (transform != null)
            {
                foreach (string str5 in labelInfo.AttributesData.AllKeys)
                {
                    arguments.AddParam(str5, "", labelInfo.AttributesData[str5]);
                }
                arguments.AddParam("SystemCurrentPage", "", labelInfo.Page.ToString());
                arguments.AddParam("SystemCount", "", labelInfo.TotalPub.ToString());
                arguments.AddExtensionObject("labelproc", InsideStaticLabelObject);
                try
                {
                    transform.Transform((IXPathNavigable) navigator, arguments, (TextWriter) new StringWriter(labelInfo.LabelContent));
                }
                catch (XsltException exception3)
                {
                    labelInfo.LabelContent = new StringBuilder("[err:标签'" + str + "'模板转换错误，原因：" + exception3.Message + "]");
                    return labelInfo;
                }
                catch (ArgumentException exception4)
                {
                    labelInfo.LabelContent = new StringBuilder("[err:标签'" + str + "'模板转换错误，原因：" + exception4.Message + "]");
                    return labelInfo;
                }
                labelInfo.LabelContent.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", string.Empty);
            }
            return labelInfo;
        }
        /// <summary>
        /// 数据标签解析
        /// </summary>
        /// <param name="labelId">标签名称</param>
        /// <param name="cPage">当前页码</param>
        /// <param name="inLabel">标签实体</param>
        /// <returns></returns>
        public static LabelInfo GetLabelDataTable(string labelId, int cPage, LabelInfo inLabel)
        {
            LabelInfo labelInfo = inLabel;
            labelInfo.Page = cPage;
            LabelManageInfo cacheLabelByName = LabelManage.GetCacheLabelByName(labelId);
            if (string.IsNullOrEmpty(cacheLabelByName.Name))
            {
                labelInfo.LabelContent = new StringBuilder("[err:DataSource'" + labelId + "'不存在]");
                return labelInfo;
            }
            try
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(cacheLabelByName.Define.ToString().Replace("<?xml version=\"1.0\"?>", string.Empty));
                foreach (XmlNode node2 in document.FirstChild.ChildNodes)
                {
                    if (node2.Name != "attributes")
                    {
                        labelInfo.LabelDefineData[node2.Name] = node2.InnerText;
                    }
                    else
                    {
                        string innerText = node2.SelectSingleNode("name").InnerText;
                        string str2 = labelInfo.OriginalData[innerText];
                        string[] strArray = node2.SelectSingleNode("default").InnerText.Split(new string[] { "|||" }, StringSplitOptions.None);
                        if (strArray.Length > 1)
                        {
                            if (string.IsNullOrEmpty(str2))
                            {
                                labelInfo.AttributesData[innerText] = strArray[0];
                            }
                            else
                            {
                                int index = DataConverter.CLng(str2);
                                if (index >= strArray.Length)
                                {
                                    index = 0;
                                }
                                labelInfo.AttributesData[innerText] = strArray[index];
                            }
                            continue;
                        }
                        if (string.IsNullOrEmpty(str2))
                        {
                            labelInfo.AttributesData[innerText] = node2.SelectSingleNode("default").InnerText;
                        }
                        else
                        {
                            labelInfo.AttributesData[innerText] = str2;
                        }
                    }
                }
            }
            catch (XmlException exception)
            {
                labelInfo.LabelContent = new StringBuilder("[err:DataSource'" + labelId + "'XML定义错，原因：" + exception.Message + "]");
                return labelInfo;
            }
            if (DataConverter.CBoolean(labelInfo.OriginalData["page"]))
            {
                labelInfo.PageSize = DataConverter.CLng(labelInfo.OriginalData["pagesize"]);
            }
            labelInfo.LabelContent = SqlAttributesProc(labelInfo).LabelContent.Replace("&", "&amp;");
            labelInfo.LabelContent = new StringBuilder(DataSecurity.PELabelEncode(labelInfo.LabelContent.ToString()));
            if (labelInfo.Error > 0)
            {
                if (labelInfo.Error >= 2)
                {
                    LogLabelTransformException(labelInfo);
                    labelInfo.LabelContent.Remove(0, labelInfo.LabelContent.Length);
                    labelInfo.LabelContent.Append("[err:标签\"" + labelId + "\"查询数据库时出现异常。有关错误的完整说明，请到后台日志管理中查看“异常记录”。]");
                }
                return labelInfo;
            }
            if (DataConverter.CBoolean(labelInfo.OriginalData["xslt"]))
            {
                XPathNavigator navigator;
                XmlDocument document2 = new XmlDocument();
                try
                {
                    document2.LoadXml(labelInfo.LabelContent.ToString());
                    navigator = document2.CreateNavigator();
                }
                catch (XmlException exception2)
                {
                    labelInfo.LabelContent = new StringBuilder("[err:DataSource'" + labelId + "'得到的XML数据错误，原因：" + exception2.Message + "]");
                    return labelInfo;
                }
                labelInfo.LabelContent = new StringBuilder();
                XsltArgumentList arguments = new XsltArgumentList();
                XslCompiledTransform transform = GetCacheXslCompiledTransform(labelInfo, cacheLabelByName.Template.ToString(), "DataSource");
                if (transform == null)
                {
                    return labelInfo;
                }
                foreach (string str3 in labelInfo.AttributesData.AllKeys)
                {
                    arguments.AddParam(str3, "", labelInfo.AttributesData[str3]);
                }
                arguments.AddExtensionObject("labelproc", InsideStaticLabelObject);
                try
                {
                    transform.Transform((IXPathNavigable) navigator, arguments, (TextWriter) new StringWriter(labelInfo.LabelContent));
                }
                catch (XsltException exception3)
                {
                    labelInfo.LabelContent = new StringBuilder("[err:DataSource'" + labelId + "'模板转换错误，原因：" + exception3.Message + "]");
                    return labelInfo;
                }
            }
            return labelInfo;
        }

        public static string GetListPage(string pagerName, PageInfo iPage, string unitname)
        {
            if (string.IsNullOrEmpty(pagerName) || (iPage == null))
            {
                return string.Empty;
            }
            if (iPage.PageNum <= 0)
            {
                return string.Empty;
            }
            XmlDocument document = new XmlDocument();
            StringBuilder template = PagerManage.GetPagerByName(pagerName).Template;
            if (!string.IsNullOrEmpty(template.ToString()))
            {
                XmlNodeList list = null;
                bool flag = false;
                int count = 0;
                if (!string.IsNullOrEmpty(iPage.PageOtherSet))
                {
                    try
                    {
                        document.LoadXml(iPage.PageOtherSet);
                        list = document.SelectNodes("pageset/item");
                        count = list.Count;
                        flag = true;
                    }
                    catch (XmlException)
                    {
                    }
                }
                template.Replace("{$pagename/}", iPage.PageName).Replace("{$sourcename/}", iPage.SpanName).Replace("{$spanname/}", pagerName);
                string pattern = @"\{\$loop(([\s\S])*?)\}(([\s\S])*?)\{\$\/loop\}";
                foreach (Match match in Regex.Matches(template.ToString(), pattern, RegexOptions.Compiled))
                {
                    string str2 = match.Groups[3].Value;
                    int num2 = 0;
                    if (!string.IsNullOrEmpty(match.Groups[1].Value))
                    {
                        string xml = "<loopbody " + match.Groups[1].Value + "/>";
                        try
                        {
                            document.LoadXml(xml);
                            XmlElement firstChild = (XmlElement) document.FirstChild;
                            num2 = DataConverter.CLng(XmlManage.GetAttributesValue(firstChild, "range"));
                        }
                        catch (XmlException exception)
                        {
                            return ("[err:分页标签'" + pagerName + "'{loop}部位定义格式错，原因：" + exception.Message + "]");
                        }
                    }
                    string[] strArray = str2.Split(new string[] { "$$$" }, StringSplitOptions.None);
                    int num3 = 0;
                    int pageNum = iPage.PageNum;
                    if (count > pageNum)
                    {
                        pageNum = count;
                    }
                    if ((num2 > 0) && (iPage.PageNum > ((num2 * 2) + 1)))
                    {
                        if (iPage.CurrentPage > (num2 + 1))
                        {
                            if (iPage.CurrentPage > (iPage.PageNum - (num2 + 1)))
                            {
                                num3 = iPage.PageNum - ((num2 * 2) + 1);
                                pageNum = iPage.PageNum;
                            }
                            else
                            {
                                num3 = iPage.CurrentPage - (num2 + 1);
                                pageNum = iPage.CurrentPage + num2;
                            }
                        }
                        else
                        {
                            pageNum = (num2 * 2) + 1;
                        }
                    }
                    StringBuilder builder2 = new StringBuilder();
                    for (int i = num3; i < pageNum; i++)
                    {
                        string str4 = string.Empty;
                        if ((iPage.CurrentPage == (i + 1)) && (strArray.Length > 1))
                        {
                            str4 = strArray[strArray.Length - 1].Replace("{$pageid/}", Convert.ToString((int) (i + 1)));
                        }
                        else
                        {
                            str4 = strArray[0].Replace("{$pageid/}", Convert.ToString((int) (i + 1)));
                        }
                        if (i < count)
                        {
                            string innerText = string.Empty;
                            string str6 = string.Empty;
                            if (flag)
                            {
                                foreach (XmlNode node in list[i].SelectNodes("title"))
                                {
                                    innerText = node.InnerText;
                                }
                                foreach (XmlNode node2 in list[i].SelectNodes("url"))
                                {
                                    str6 = node2.InnerText;
                                }
                            }
                            if (!string.IsNullOrEmpty(str4))
                            {
                                if (!string.IsNullOrEmpty(innerText))
                                {
                                    str4 = str4.Replace("{$pagetitle/}", innerText);
                                }
                                else
                                {
                                    str4 = str4.Replace("{$pagetitle/}", i.ToString());
                                }
                                if (!string.IsNullOrEmpty(str6))
                                {
                                    str4 = str4.Replace("{$pageurl/}", str6);
                                }
                                else if ((iPage != null) && !string.IsNullOrEmpty(iPage.PageName))
                                {
                                    str4 = str4.Replace("{$pageurl/}", iPage.PageName.Replace("{$pageid/}", Convert.ToString((int) (i + 1))));
                                }
                            }
                        }
                        else if (!string.IsNullOrEmpty(str4))
                        {
                            str4 = str4.Replace("{$pagetitle/}", Convert.ToString((int) (i + 1)));
                            if ((iPage != null) && !string.IsNullOrEmpty(iPage.PageName))
                            {
                                str4 = str4.Replace("{$pageurl/}", iPage.PageName.Replace("{$pageid/}", Convert.ToString((int) (i + 1))));
                            }
                        }
                        builder2.Append(str4);
                    }
                    template.Replace(match.ToString(), builder2.ToString());
                }
                if ((iPage != null) && !string.IsNullOrEmpty(iPage.PageName))
                {
                    template.Replace("{$unitname/}", unitname);
                    template.Replace("{$firsturl/}", iPage.PageName.Replace("{$pageid/}", "1"));
                    template.Replace("{$endid/}", iPage.PageNum.ToString());
                    template.Replace("{$endurl/}", iPage.PageName.Replace("{$pageid/}", iPage.PageNum.ToString()));
                    int num6 = 1;
                    if (iPage.CurrentPage > 1)
                    {
                        num6 = iPage.CurrentPage - 1;
                    }
                    template.Replace("{$prvurl/}", iPage.PageName.Replace("{$pageid/}", num6.ToString()));
                    template.Replace("{$prvid/}", num6.ToString());
                    int num7 = iPage.PageNum;
                    if (iPage.CurrentPage < iPage.PageNum)
                    {
                        num7 = iPage.CurrentPage + 1;
                    }
                    template.Replace("{$nexturl/}", iPage.PageName.Replace("{$pageid/}", num7.ToString()));
                    template.Replace("{$nextid/}", num7.ToString());
                    template.Replace("{$currentid/}", iPage.CurrentPage.ToString());
                    template.Replace("{$currenturl/}", iPage.PageName.Replace("{$pageid/}", iPage.CurrentPage.ToString()));
                    template.Replace("{$totalpub/}", iPage.TotalPub.ToString());
                    template.Replace("{$pagesize/}", iPage.PageSize.ToString());
                    template.Replace("{$installdir/}", InsideStaticLabelObject.InstallDir());
                    template.Replace("{$originurl/}", iPage.PageName);
                }
            }
            return template.ToString();
        }

        public static string GetPage(string pagerName, PageInfo iPage, string unitname)
        {
            if (string.IsNullOrEmpty(pagerName) || (iPage == null))
            {
                return string.Empty;
            }
            if (iPage.PageNum <= 0)
            {
                return string.Empty;
            }
            XmlDocument document = new XmlDocument();
            StringBuilder template = PagerManage.GetPagerByName(pagerName).Template;
            if (!string.IsNullOrEmpty(template.ToString()))
            {
                XmlNodeList list = null;
                bool flag = false;
                int count = 0;
                if (!string.IsNullOrEmpty(iPage.PageOtherSet))
                {
                    try
                    {
                        document.LoadXml(iPage.PageOtherSet);
                        list = document.SelectNodes("pageset/item");
                        count = list.Count;
                        flag = true;
                    }
                    catch (XmlException)
                    {
                    }
                }
                template.Replace("{$pagename/}", iPage.PageName).Replace("{$sourcename/}", iPage.SpanName).Replace("{$spanname/}", pagerName);
                string pattern = @"\{\$loop(([\s\S])*?)\}(([\s\S])*?)\{\$\/loop\}";
                foreach (Match match in Regex.Matches(template.ToString(), pattern, RegexOptions.Compiled))
                {
                    string str2 = match.Groups[3].Value;
                    int num2 = 0;
                    if (!string.IsNullOrEmpty(match.Groups[1].Value))
                    {
                        string xml = "<loopbody " + match.Groups[1].Value + "/>";
                        try
                        {
                            document.LoadXml(xml);
                            XmlElement firstChild = (XmlElement) document.FirstChild;
                            num2 = DataConverter.CLng(XmlManage.GetAttributesValue(firstChild, "range"));
                        }
                        catch (XmlException exception)
                        {
                            return ("[err:分页标签'" + pagerName + "'{loop}部位定义格式错，原因：" + exception.Message + "]");
                        }
                    }
                    string[] strArray = str2.Split(new string[] { "$$$" }, StringSplitOptions.None);
                    int num3 = 0;
                    int pageNum = iPage.PageNum;
                    if (count > pageNum)
                    {
                        pageNum = count;
                    }
                    if ((num2 > 0) && (iPage.PageNum > ((num2 * 2) + 1)))
                    {
                        if (iPage.CurrentPage > (num2 + 1))
                        {
                            if (iPage.CurrentPage > (iPage.PageNum - (num2 + 1)))
                            {
                                num3 = iPage.PageNum - ((num2 * 2) + 1);
                                pageNum = iPage.PageNum;
                            }
                            else
                            {
                                num3 = iPage.CurrentPage - (num2 + 1);
                                pageNum = iPage.CurrentPage + num2;
                            }
                        }
                        else
                        {
                            pageNum = (num2 * 2) + 1;
                        }
                    }
                    StringBuilder builder2 = new StringBuilder();
                    for (int i = num3; i < pageNum; i++)
                    {
                        string str4 = string.Empty;
                        if ((iPage.CurrentPage == (i + 1)) && (strArray.Length > 1))
                        {
                            str4 = strArray[strArray.Length - 1].Replace("{$pageid/}", Convert.ToString((int) (i + 1)));
                        }
                        else
                        {
                            str4 = strArray[0].Replace("{$pageid/}", Convert.ToString((int) (i + 1)));
                        }
                        if (i < count)
                        {
                            string innerText = string.Empty;
                            string str6 = string.Empty;
                            if (flag)
                            {
                                foreach (XmlNode node in list[i].SelectNodes("title"))
                                {
                                    innerText = node.InnerText;
                                }
                                foreach (XmlNode node2 in list[i].SelectNodes("url"))
                                {
                                    str6 = node2.InnerText;
                                }
                            }
                            if (!string.IsNullOrEmpty(str4))
                            {
                                if (!string.IsNullOrEmpty(innerText))
                                {
                                    str4 = str4.Replace("{$pagetitle/}", innerText);
                                }
                                else
                                {
                                    str4 = str4.Replace("{$pagetitle/}", i.ToString());
                                }
                                if (!string.IsNullOrEmpty(str6))
                                {
                                    str4 = str4.Replace("{$pageurl/}", str6);
                                }
                                else if ((iPage != null) && !string.IsNullOrEmpty(iPage.PageName))
                                {
                                    str4 = str4.Replace("{$pageurl/}", iPage.PageName.Replace("{$pageid/}", Convert.ToString(i)));
                                }
                            }
                        }
                        else if (!string.IsNullOrEmpty(str4))
                        {
                            str4 = str4.Replace("{$pagetitle/}", Convert.ToString((int) (i + 1)));
                            if (i == 0)
                            {
                                str4 = str4.Replace("{$pageurl/}", iPage.PageName.Replace("_{$pageid/}", ""));
                            }
                            else
                            {
                                int num8 = i + 1;
                                str4 = str4.Replace("{$pageurl/}", iPage.PageName.Replace("{$pageid/}", num8.ToString()));
                            }
                        }
                        builder2.Append(str4);
                    }
                    template.Replace(match.ToString(), builder2.ToString());
                }
                if ((iPage != null) && !string.IsNullOrEmpty(iPage.PageName))
                {
                    int num7;
                    template.Replace("{$unitname/}", unitname);
                    template.Replace("{$firsturl/}", iPage.PageName.Replace("_{$pageid/}", ""));
                    template.Replace("{$endid/}", iPage.PageNum.ToString());
                    if (iPage.PageNum == 1)
                    {
                        template.Replace("{$endurl/}", iPage.PageName.Replace("_{$pageid/}", ""));
                    }
                    else
                    {
                        template.Replace("{$endurl/}", iPage.PageName.Replace("{$pageid/}", iPage.PageNum.ToString()));
                    }
                    if (iPage.CurrentPage < 3)
                    {
                        template.Replace("{$prvurl/}", iPage.PageName.Replace("_{$pageid/}", ""));
                        template.Replace("{$prvid/}", iPage.CurrentPage.ToString());
                    }
                    else
                    {
                        int num6 = 1;
                        if (iPage.CurrentPage >= 3)
                        {
                            num6 = iPage.CurrentPage - 1;
                        }
                        template.Replace("{$prvurl/}", iPage.PageName.Replace("{$pageid/}", num6.ToString()));
                        template.Replace("{$prvid/}", num6.ToString());
                    }
                    if (iPage.CurrentPage < iPage.PageNum)
                    {
                        num7 = iPage.CurrentPage + 1;
                    }
                    else
                    {
                        num7 = iPage.PageNum;
                    }
                    if (iPage.PageNum == 1)
                    {
                        template.Replace("{$nexturl/}", iPage.PageName.Replace("_{$pageid/}", ""));
                        template.Replace("{$nextid/}", num7.ToString());
                    }
                    else
                    {
                        template.Replace("{$nexturl/}", iPage.PageName.Replace("{$pageid/}", num7.ToString()));
                        template.Replace("{$nextid/}", num7.ToString());
                    }
                    template.Replace("{$currentid/}", iPage.CurrentPage.ToString());
                    if (iPage.CurrentPage == 1)
                    {
                        template.Replace("{$currenturl/}", iPage.PageName.Replace("_{$pageid/}", ""));
                    }
                    else
                    {
                        template.Replace("{$currenturl/}", iPage.PageName.Replace("{$pageid/}", iPage.CurrentPage.ToString()));
                    }
                    template.Replace("{$totalpub/}", iPage.TotalPub.ToString());
                    template.Replace("{$pagesize/}", iPage.PageSize.ToString());
                    template.Replace("{$installdir/}", InsideStaticLabelObject.InstallDir());
                    template.Replace("{$originurl/}", iPage.PageName);
                }
            }
            return template.ToString();
        }
        /// <summary>
        /// 获取系统配置标签
        /// </summary>
        /// <param name="labelName"></param>
        /// <returns></returns>
        public static string GetSiteConfigLabel(string labelName)
        {
            XmlElement firstChild;
            XmlDocument document = new XmlDocument();
            string str = string.Empty;
            switch (labelName)
            {
                case "sitename":
                    return SiteConfig.SiteInfo.SiteName;

                case "sitetitle":
                    return SiteConfig.SiteInfo.SiteTitle;

                case "installdir":
                    return SiteConfig.SiteInfo.VirtualPath;

                case "includefilepath":
                    return SiteConfig.SiteOption.IncludeFilePath;

                case "sitepath":
                    return SiteConfig.SiteInfo.SiteUrl;

                case "logo":
                    return Utility.ConvertAbsolutePath(SiteConfig.SiteInfo.VirtualPath, SiteConfig.SiteInfo.LogoUrl);

                case "banner":
                    return Utility.ConvertAbsolutePath(SiteConfig.SiteInfo.VirtualPath, SiteConfig.SiteInfo.BannerUrl);

                case "webmaster":
                    return SiteConfig.SiteInfo.Webmaster;

                case "webmasteremail":
                    return SiteConfig.SiteInfo.WebmasterEmail;

                case "copyright":
                    return SiteConfig.SiteInfo.Copyright;

                case "managedir":
                    return SiteConfig.SiteOption.ManageDir;

                case "addir":
                    return SiteConfig.SiteOption.AdvertisementDir;

                case "metakeywords":
                    return SiteConfig.SiteInfo.MetaKeywords;

                case "metadescription":
                    return SiteConfig.SiteInfo.MetaDescription;

                case "defaultcss":
                    try
                    {
                        document.LoadXml("<" + labelName + " />");
                        firstChild = (XmlElement) document.FirstChild;
                    }
                    catch (XmlException exception)
                    {
                        return ("[err:系统标签" + labelName + "错，原因：" + exception.Message + "]");
                    }
                    if (string.IsNullOrEmpty(XmlManage.GetAttributesValue(firstChild, "name")))
                    {
                        return ("<link href=\"" + SiteConfig.SiteInfo.VirtualPath + "Skin/DefaultSkin.css\" rel=\"stylesheet\" type=\"text/css\">");
                    }
                    return ("<link href=\"Skin/" + XmlManage.GetAttributesValue(firstChild, "name") + ".css\" rel=\"stylesheet\" type=\"text/css\">");

                case "timenow":
                    return DateTime.Now.ToLocalTime().ToString();

                case "uploaddir":
                    return SiteConfig.SiteOption.UploadDir;

                case "readfile":
                {
                    try
                    {
                        document.LoadXml("<" + labelName + " />");
                        firstChild = (XmlElement) document.FirstChild;
                    }
                    catch (XmlException exception2)
                    {
                        return ("[err:系统标签" + labelName + "错，原因：" + exception2.Message + "]");
                    }
                    string attributesValue = XmlManage.GetAttributesValue(firstChild, "path");
                    if (!string.IsNullOrEmpty(attributesValue))
                    {
                        attributesValue = HttpContext.Current.Server.MapPath(attributesValue);
                        if (File.Exists(attributesValue))
                        {
                            str = File.ReadAllText(attributesValue);
                        }
                    }
                    return str;
                }
                case "applicationpath":
                    if (!SiteConfig.SiteOption.IsAbsoluatePath)
                    {
                        return SiteConfig.SiteInfo.VirtualPath;
                    }
                    return SiteConfig.SiteInfo.SiteUrl;

                case "adpath":
                    return (SiteConfig.SiteInfo.VirtualPath + SiteConfig.SiteOption.AdvertisementDir);
            }
            return ("[err:SiteConfig没有'" + labelName + "'这个内置方法]");
        }

        private static int GetSpecialPosition(string content, int beginPoint)
        {
            string pattern = "^(([^<]*>)[^<]{0,100})(?:<p|<img|<br|<li)*";
            MatchCollection matchs = new Regex(pattern, RegexOptions.IgnoreCase).Matches(content, beginPoint);
            if (matchs.Count > 0)
            {
                return (matchs[0].Length + beginPoint);
            }
            return beginPoint;
        }

        private static bool LogLabelTransformException(LabelInfo labelInfo)
        {
            if (labelInfo.Error >= 2)
            {
                ILog log = LogFactory.CreateLog();
                LogInfo info = new LogInfo();
                info.Category = LogCategory.Exception;
                info.Priority = LogPriority.High;
                info.Message = labelInfo.LabelContent.ToString();
                info.ScriptName = HttpContext.Current.Request.RawUrl;
                info.Source = "标签：\"" + labelInfo.OriginalData["id"] + "\"";
                info.Timestamp = DateTime.Now;
                info.Title = "标签：\"" + labelInfo.OriginalData["id"] + "\"引发异常";
                info.UserIP = PEContext.Current.UserHostAddress;
                info.UserName = "";
                log.Add(info);
                return true;
            }
            return false;
        }

        private static void ManualPage(ContentPageInfo contentPageInfo)
        {
            string pattern = @"\[NextPage(.*?)\]";
            Regex regex = new Regex(pattern, RegexOptions.Compiled);
            string format = "<select Name='PageSelect' id='PageSelect' onchange=javascript:window.location=(this.options[this.selectedIndex].value)>{0}</select>";
            StringBuilder builder = new StringBuilder();
            MatchCollection matchs = regex.Matches(contentPageInfo.Content);
            for (int i = 1; i < (matchs.Count + 1); i++)
            {
                if (!string.IsNullOrEmpty(matchs[i - 1].Groups[1].Value.Trim()))
                {
                    builder.Append("<option ");
                    if (contentPageInfo.CurrentPage == i)
                    {
                        builder.Append(" selected=\"true\" ");
                    }
                    if (i == 1)
                    {
                        builder.Append(" value=\"" + contentPageInfo.PageName.Replace("_{$pageid/}", "") + "\"");
                    }
                    else
                    {
                        builder.Append(" value=\"" + contentPageInfo.PageName.Replace("{$pageid/}", i.ToString()) + "\"");
                    }
                    builder.Append(">");
                    builder.Append("第 " + i.ToString() + " 页：");
                    builder.Append(matchs[i - 1].Groups[1].Value);
                    builder.Append("</option>");
                }
            }
            if (builder.Length > 0)
            {
                format = "<p>" + string.Format(format, builder.ToString()) + "</p>";
            }
            else
            {
                format = string.Empty;
            }
            if (matchs.Count == 0)
            {
                contentPageInfo.PageNum = 1;
                contentPageInfo.Content = contentPageInfo.Content;
            }
            else
            {
                contentPageInfo.Content = regex.Replace(contentPageInfo.Content, "[NextPage]");
                pattern = @"\[NextPage\]";
                string[] strArray = new Regex(pattern, RegexOptions.Compiled).Split(contentPageInfo.Content);
                if (string.IsNullOrEmpty(format))
                {
                    contentPageInfo.PageNum = strArray.Length;
                    if (contentPageInfo.CurrentPage <= strArray.Length)
                    {
                        contentPageInfo.Content = strArray[contentPageInfo.CurrentPage - 1];
                    }
                    else
                    {
                        contentPageInfo.Content = strArray[strArray.Length - 1];
                    }
                }
                else
                {
                    contentPageInfo.PageNum = strArray.Length - 1;
                    if (contentPageInfo.CurrentPage <= strArray.Length)
                    {
                        contentPageInfo.Content = format + strArray[contentPageInfo.CurrentPage];
                    }
                    else
                    {
                        contentPageInfo.Content = format + strArray[strArray.Length - 1];
                    }
                }
            }
        }

        private static LabelInfo SqlAttributesProc(LabelInfo labelInfo)
        {
            string str = labelInfo.LabelDefineData["LabelDataType"];
            switch (str)
            {
                case "static":
                    labelInfo.LabelContent = new StringBuilder("static");
                    return labelInfo;

                case "sql_syscommand":
                    labelInfo.TotalPub = dal.GetSqlCommand(labelInfo);
                    labelInfo.LabelContent = new StringBuilder("QueryLabel");
                    return labelInfo;

                case "sql_sysquery":
                    return dal.GetSqlQuery(labelInfo);

                case "sql_sysstoredcommand":
                    labelInfo.TotalPub = dal.GetStoreCommand(labelInfo);
                    labelInfo.LabelContent = new StringBuilder("QueryLabel");
                    return labelInfo;

                case "sql_sysstoredquery":
                    return dal.GetStoreQuery(labelInfo);

                case "sql_outquery":
                    return dal.GetOutSqlQuery(labelInfo);

                case "xml_read":
                    return dal.GetXmlQuery(labelInfo);

                case "ole_read":
                    return dal.GetOleQuery(labelInfo);

                case "odbc_read":
                    return dal.GetOdbcQuery(labelInfo);

                case "orc_read":
                    return dal.GetOracleQuery(labelInfo);
            }
            labelInfo.LabelContent = new StringBuilder("[err:错误的查询类型," + str + "]");
            return labelInfo;
        }

        private static StringBuilder StaticLabelProc(LabelInfo labelInfo, StringBuilder labeltemplate)
        {
            string strA = (labelInfo.LabelDefineData["OutType"] == null) ? "" : labelInfo.LabelDefineData["OutType"].Trim();
            if (string.Compare(strA, "txt", StringComparison.OrdinalIgnoreCase) == 0)
            {
                StringBuilder builder = new StringBuilder(labeltemplate.ToString());
                foreach (string str2 in labelInfo.AttributesData.AllKeys)
                {
                    builder.Replace("<xsl:value-of select=\"$" + str2 + "\"/>", labelInfo.AttributesData[str2]);
                }
                return builder;
            }
            StringBuilder sb = new StringBuilder();
            XmlDocument document = new XmlDocument();
            document.LoadXml("<root>abc</root>");
            XPathNavigator navigator = document.CreateNavigator();
            XsltArgumentList arguments = new XsltArgumentList();
            XslCompiledTransform transform = GetCacheXslCompiledTransform(labelInfo, labeltemplate.ToString(), string.Empty);
            if (transform == null)
            {
                return labelInfo.LabelContent;
            }
            foreach (string str3 in labelInfo.AttributesData.AllKeys)
            {
                arguments.AddParam(str3, "", labelInfo.AttributesData[str3]);
            }
            arguments.AddExtensionObject("labelproc", InsideStaticLabelObject);
            try
            {
                transform.Transform((IXPathNavigable) navigator, arguments, (TextWriter) new StringWriter(sb));
            }
            catch (XsltException exception)
            {
                sb = new StringBuilder("[err:标签'" + labelInfo.OriginalData["id"] + "'模板转换错误，原因：" + exception.Message + "]");
            }
            return sb;
        }

        public static void TransContentPageLabel(ContentPageInfo contentPageInfo)
        {
            if (!string.IsNullOrEmpty(contentPageInfo.Content))
            {
                XmlDocument document = new XmlDocument();
                string strA = string.Empty;
                int num = 2;
                try
                {
                    document.LoadXml(contentPageInfo.Parameter);
                    XmlElement firstChild = (XmlElement) document.FirstChild;
                    strA = XmlManage.GetAttributesValue(firstChild, "mode");
                    num = DataConverter.CLng(XmlManage.GetAttributesValue(firstChild, "pagesize"), 2);
                }
                catch (XmlException)
                {
                    throw new XmlException("内容分页标签解析错误！");
                }
                if (string.Compare(strA, "auto", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    contentPageInfo.Content = contentPageInfo.Content.Replace("[NextPage]", string.Empty);
                    int length = contentPageInfo.Content.Length;
                    if ((num > 100) && (length > num))
                    {
                        if ((length % num) == 0)
                        {
                            contentPageInfo.PageNum = length / num;
                        }
                        else
                        {
                            contentPageInfo.PageNum = (length / num) + 1;
                        }
                        if (contentPageInfo.CurrentPage < 1)
                        {
                            contentPageInfo.CurrentPage = 1;
                        }
                        if (contentPageInfo.CurrentPage > contentPageInfo.PageNum)
                        {
                            contentPageInfo.CurrentPage = contentPageInfo.PageNum;
                        }
                        int beginPoint = 0;
                        if (contentPageInfo.CurrentPage == 1)
                        {
                            beginPoint = 1;
                        }
                        else
                        {
                            beginPoint = num * (contentPageInfo.CurrentPage - 1);
                            beginPoint = GetSpecialPosition(contentPageInfo.Content, beginPoint);
                        }
                        int specialPosition = 0;
                        if (contentPageInfo.CurrentPage == contentPageInfo.PageNum)
                        {
                            specialPosition = length;
                        }
                        else
                        {
                            specialPosition = num * contentPageInfo.CurrentPage;
                            if (specialPosition > length)
                            {
                                specialPosition = length;
                            }
                            else
                            {
                                specialPosition = GetSpecialPosition(contentPageInfo.Content, specialPosition);
                            }
                        }
                        if (specialPosition < beginPoint)
                        {
                            specialPosition = beginPoint;
                        }
                        contentPageInfo.Content = contentPageInfo.Content.Substring(beginPoint, specialPosition - beginPoint);
                    }
                }
                else if (string.Compare(strA, "manual", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    ManualPage(contentPageInfo);
                }
                else
                {
                    contentPageInfo.Content = contentPageInfo.Content.Replace("[NextPage]", string.Empty);
                }
            }
        }
        /// <summary>
        /// 内置静态标签
        /// </summary>
        public static InsideStaticLabel InsideStaticLabelObject
        {
            get
            {
                InsideStaticLabel label = SiteCache.Get("CK_Label_LabelTransform_InsideStaticLabelObject") as InsideStaticLabel;
                if (label == null)
                {
                    label = new InsideStaticLabel();
                    SiteCache.Insert("CK_Label_LabelTransform_InsideStaticLabelObject", label, 0x4380);
                }
                return label;
            }
        }
    }
}

