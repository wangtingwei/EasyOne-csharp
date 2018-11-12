namespace EasyOne.WebSite.Admin.Collection
{
    using EasyOne.Collection;
    using EasyOne.Common;
    using EasyOne.Enumerations;
    using EasyOne.Model.Collection;
    using EasyOne.Web.UI;
    using System;
    using System.Collections;
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Xml;

    public partial class Ajax : BasePage
    {
        private static string CommonFilter(string filterRuleId, string filter, CollectionCommon collectionCommon, string testContent)
        {
            if (filterRuleId.IndexOf(',') > 0)
            {
                foreach (string str in filterRuleId.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                {
                    testContent = FilterRule(DataConverter.CLng(str), collectionCommon, testContent);
                }
            }
            else
            {
                testContent = FilterRule(DataConverter.CLng(filterRuleId), collectionCommon, testContent);
            }
            testContent = StringHelper.FilterScript(testContent, filter);
            return testContent;
        }

        private static string FilterRule(int filterRuleId, CollectionCommon collectionCommon, string testContent)
        {
            if (filterRuleId > 0)
            {
                CollectionFilterRuleInfo infoById = CollectionFilterRules.GetInfoById(filterRuleId);
                if (!infoById.IsNull)
                {
                    switch (infoById.FilterType)
                    {
                        case 1:
                            if (!string.IsNullOrEmpty(infoById.BeginCode))
                            {
                                testContent = testContent.Replace(infoById.BeginCode, infoById.Replace);
                            }
                            return testContent;

                        case 2:
                        {
                            string str = collectionCommon.GetInterceptionString(testContent, infoById.BeginCode, infoById.EndCode, true, true);
                            if (!string.IsNullOrEmpty(str))
                            {
                                testContent = testContent.Replace(str, "");
                            }
                            return testContent;
                        }
                    }
                }
            }
            return testContent;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            XmlDocument ixml = new XmlDocument();
            try
            {
                ixml.Load(base.Request.InputStream);
            }
            catch (XmlException exception)
            {
                string path = "~/Temp/ajaxnote.txt";
                path = HttpContext.Current.Server.MapPath(path);
                if (File.Exists(path))
                {
                    base.Response.Write(File.ReadAllText(path));
                }
                else
                {
                    base.Response.Write(exception.Message);
                }
                return;
            }
            base.Response.Clear();
            base.Response.Buffer = true;
            base.Response.Charset = "utf-8";
            base.Response.AddHeader("contenttype", "text/xml");
            base.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
            base.Response.ContentType = "text/xml";
            if (ixml.HasChildNodes)
            {
                string str2 = (ixml.DocumentElement.SelectSingleNode("//type") == null) ? "" : ixml.DocumentElement.SelectSingleNode("//type").InnerText.Trim();
                switch (str2)
                {
                    case "testList":
                        this.TestList(ixml);
                        goto Label_01E6;

                    case "testLink":
                        this.TestLink(ixml);
                        goto Label_01E6;

                    case "testPaing":
                        this.TestPaing(ixml);
                        goto Label_01E6;

                    case "testPaing2":
                        this.TestPaing2(ixml);
                        goto Label_01E6;

                    case "testShowContent":
                        this.TestShowContent(ixml);
                        goto Label_01E6;

                    case "testField":
                        this.TestField(ixml);
                        goto Label_01E6;

                    default:
                        if (!string.IsNullOrEmpty(str2))
                        {
                            goto Label_01E6;
                        }
                        this.PutErrMessage("没有要测试的采集类型！");
                        return;
                }
                return;
            }
        Label_01E6:
            base.Response.End();
        }

        private void PutErrMessage(string message)
        {
            XmlTextWriter writer = new XmlTextWriter(HttpContext.Current.Response.OutputStream, Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 4;
            writer.WriteStartDocument();
            writer.WriteStartElement("root", "");
            writer.WriteElementString("status", "err");
            writer.WriteElementString("body", message);
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }

        private void TestField(XmlDocument ixml)
        {
            string uriString = (ixml.DocumentElement.SelectSingleNode("//url") == null) ? "" : ixml.DocumentElement.SelectSingleNode("//url").InnerText.Trim();
            string coding = (ixml.DocumentElement.SelectSingleNode("//codeType") == null) ? "" : ixml.DocumentElement.SelectSingleNode("//codeType").InnerText.Trim();
            string startStr = (ixml.DocumentElement.SelectSingleNode("//listBegin") == null) ? "" : ixml.DocumentElement.SelectSingleNode("//listBegin").InnerText;
            string overStr = (ixml.DocumentElement.SelectSingleNode("//listEnd") == null) ? "" : ixml.DocumentElement.SelectSingleNode("//listEnd").InnerText;
            string input = (ixml.DocumentElement.SelectSingleNode("//keyword") == null) ? "" : ixml.DocumentElement.SelectSingleNode("//keyword").InnerText.Trim();
            string filterRuleId = (ixml.DocumentElement.SelectSingleNode("//filterRuleId") == null) ? "" : ixml.DocumentElement.SelectSingleNode("//filterRuleId").InnerText.Trim();
            string filter = (ixml.DocumentElement.SelectSingleNode("//filter") == null) ? "" : ixml.DocumentElement.SelectSingleNode("//filter").InnerText.Trim();
            string str8 = (ixml.DocumentElement.SelectSingleNode("//fieldType") == null) ? "" : ixml.DocumentElement.SelectSingleNode("//fieldType").InnerText.Trim();
            CollectionCommon collectionCommon = new CollectionCommon();
            Uri url = new Uri(uriString);
            string httpPage = collectionCommon.GetHttpPage(url, coding);
            string testContent = collectionCommon.GetInterceptionString(httpPage, startStr, overStr);
            FieldType none = FieldType.None;
            if (Enum.IsDefined(typeof(FieldType), str8))
            {
                none = (FieldType) Enum.Parse(typeof(FieldType), str8);
            }
            switch (none)
            {
                case FieldType.TextType:
                case FieldType.ListBoxType:
                case FieldType.LookType:
                case FieldType.CountType:
                case FieldType.ColorType:
                case FieldType.TemplateType:
                case FieldType.AuthorType:
                case FieldType.SourceType:
                case FieldType.OperatingType:
                case FieldType.Producer:
                case FieldType.Trademark:
                case FieldType.TitleType:
                    if (testContent.Length > 0xff)
                    {
                        testContent = testContent.Substring(0, 0xff);
                    }
                    testContent = CommonFilter(filterRuleId, filter, collectionCommon, testContent);
                    break;

                case FieldType.NumberType:
                    testContent = DataConverter.CSingle(testContent).ToString();
                    break;

                case FieldType.MoneyType:
                    testContent = DataConverter.CDecimal(testContent).ToString();
                    break;

                case FieldType.DateTimeType:
                    testContent = DataConverter.CDate(testContent).ToString();
                    break;

                case FieldType.BoolType:
                    testContent = DataConverter.CBoolean(testContent).ToString();
                    break;

                case FieldType.KeywordType:
                    testContent = CollectionCommon.CreateKeyWord(CommonFilter(filterRuleId, filter, collectionCommon, testContent).Replace(",", "|").Replace("，", "|"), DataConverter.CLng(input));
                    break;

                default:
                    testContent = CommonFilter(filterRuleId, filter, collectionCommon, testContent);
                    break;
            }
            if (string.IsNullOrEmpty(testContent))
            {
                testContent = "没有截取到代码，请加载内容页源代码重新设置下。";
            }
            XmlTextWriter writer = new XmlTextWriter(HttpContext.Current.Response.OutputStream, Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 4;
            writer.WriteStartDocument();
            writer.WriteStartElement("root", "");
            writer.WriteElementString("testContent", testContent);
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }

        private void TestLink(XmlDocument ixml)
        {
            string str = (ixml.DocumentElement.SelectSingleNode("//url") == null) ? "" : ixml.DocumentElement.SelectSingleNode("//url").InnerText.Trim();
            string wordsBegin = (ixml.DocumentElement.SelectSingleNode("//linkBegin") == null) ? "" : ixml.DocumentElement.SelectSingleNode("//linkBegin").InnerText;
            string wordsEnd = (ixml.DocumentElement.SelectSingleNode("//linkEnd") == null) ? "" : ixml.DocumentElement.SelectSingleNode("//linkEnd").InnerText;
            string code = (ixml.DocumentElement.SelectSingleNode("//testContent") == null) ? "" : ixml.DocumentElement.SelectSingleNode("//testContent").InnerText;
            CollectionCommon common = new CollectionCommon();
            ArrayList list = common.GetArray(code, wordsBegin, wordsEnd);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                builder.Append(common.ConvertToAbsluteUrl(list[i].ToString(), str.ToString()) + "\r\n");
            }
            string str5 = builder.ToString();
            if (string.IsNullOrEmpty(str5))
            {
                str5 = "没有截取到链接列表，请加载源代码重新设置下。";
            }
            XmlTextWriter writer = new XmlTextWriter(HttpContext.Current.Response.OutputStream, Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 4;
            writer.WriteStartDocument();
            writer.WriteStartElement("root", "");
            writer.WriteElementString("testContent", str5);
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }

        private void TestList(XmlDocument ixml)
        {
            string uriString = (ixml.DocumentElement.SelectSingleNode("//url") == null) ? "" : ixml.DocumentElement.SelectSingleNode("//url").InnerText.Trim();
            string coding = (ixml.DocumentElement.SelectSingleNode("//codeType") == null) ? "" : ixml.DocumentElement.SelectSingleNode("//codeType").InnerText.Trim();
            string startStr = (ixml.DocumentElement.SelectSingleNode("//listBegin") == null) ? "" : ixml.DocumentElement.SelectSingleNode("//listBegin").InnerText;
            string overStr = (ixml.DocumentElement.SelectSingleNode("//listEnd") == null) ? "" : ixml.DocumentElement.SelectSingleNode("//listEnd").InnerText;
            CollectionCommon common = new CollectionCommon();
            Uri url = new Uri(uriString);
            string httpPage = common.GetHttpPage(url, coding);
            string str6 = common.GetInterceptionString(httpPage, startStr, overStr);
            if (string.IsNullOrEmpty(str6))
            {
                str6 = "没有截取到列表页，请加载源代码重新设置下。";
            }
            XmlTextWriter writer = new XmlTextWriter(HttpContext.Current.Response.OutputStream, Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 4;
            writer.WriteStartDocument();
            writer.WriteStartElement("root", "");
            writer.WriteElementString("testContent", str6);
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }

        private void TestPaing(XmlDocument ixml)
        {
            string str5;
            string absoluteAddress = (ixml.DocumentElement.SelectSingleNode("//url") == null) ? "" : ixml.DocumentElement.SelectSingleNode("//url").InnerText.Trim();
            string wordsBegin = (ixml.DocumentElement.SelectSingleNode("//paingBegin") == null) ? "" : ixml.DocumentElement.SelectSingleNode("//paingBegin").InnerText;
            string wordsEnd = (ixml.DocumentElement.SelectSingleNode("//paingEnd") == null) ? "" : ixml.DocumentElement.SelectSingleNode("//paingEnd").InnerText;
            string code = (ixml.DocumentElement.SelectSingleNode("//testContent") == null) ? "" : ixml.DocumentElement.SelectSingleNode("//testContent").InnerText;
            CollectionCommon common = new CollectionCommon();
            if (!string.IsNullOrEmpty(common.GetPaing(code, wordsBegin, wordsEnd)))
            {
                str5 = common.ConvertToAbsluteUrl(common.GetPaing(code, wordsBegin, wordsEnd), absoluteAddress);
            }
            else
            {
                str5 = "没有截取到下一页URL，请加载源代码重新设置下。";
            }
            XmlTextWriter writer = new XmlTextWriter(HttpContext.Current.Response.OutputStream, Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 4;
            writer.WriteStartDocument();
            writer.WriteStartElement("root", "");
            writer.WriteElementString("testContent", str5);
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }

        private void TestPaing2(XmlDocument ixml)
        {
            string str = (ixml.DocumentElement.SelectSingleNode("//url") == null) ? "" : ixml.DocumentElement.SelectSingleNode("//url").InnerText.Trim();
            string startStr = (ixml.DocumentElement.SelectSingleNode("//paingBegin2") == null) ? "" : ixml.DocumentElement.SelectSingleNode("//paingBegin2").InnerText;
            string overStr = (ixml.DocumentElement.SelectSingleNode("//paingEnd2") == null) ? "" : ixml.DocumentElement.SelectSingleNode("//paingEnd2").InnerText;
            string wordsBegin = (ixml.DocumentElement.SelectSingleNode("//linkBegin2") == null) ? "" : ixml.DocumentElement.SelectSingleNode("//linkBegin2").InnerText;
            string wordsEnd = (ixml.DocumentElement.SelectSingleNode("//linkEnd2") == null) ? "" : ixml.DocumentElement.SelectSingleNode("//linkEnd2").InnerText;
            string conStr = (ixml.DocumentElement.SelectSingleNode("//testContent") == null) ? "" : ixml.DocumentElement.SelectSingleNode("//testContent").InnerText;
            CollectionCommon common = new CollectionCommon();
            string str7 = common.GetInterceptionString(conStr, startStr, overStr);
            if (string.IsNullOrEmpty(str7))
            {
                str7 = "没有截取到分页URL列表，请加载源代码重新设置下。";
            }
            ArrayList list = common.GetArray(str7, wordsBegin, wordsEnd);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                builder.Append(common.ConvertToAbsluteUrl(list[i].ToString(), str.ToString()) + "\r\n");
            }
            string str8 = builder.ToString();
            if (string.IsNullOrEmpty(str8))
            {
                str8 = "没有截取到分页URL链接，请加载源代码重新设置下。";
            }
            XmlTextWriter writer = new XmlTextWriter(HttpContext.Current.Response.OutputStream, Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 4;
            writer.WriteStartDocument();
            writer.WriteStartElement("root", "");
            writer.WriteElementString("testContent", str8);
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }

        private void TestShowContent(XmlDocument ixml)
        {
            string uriString = (ixml.DocumentElement.SelectSingleNode("//url") == null) ? "" : ixml.DocumentElement.SelectSingleNode("//url").InnerText.Trim();
            string coding = (ixml.DocumentElement.SelectSingleNode("//codeType") == null) ? "" : ixml.DocumentElement.SelectSingleNode("//codeType").InnerText.Trim();
            CollectionCommon common = new CollectionCommon();
            Uri url = new Uri(uriString);
            string httpPage = common.GetHttpPage(url, coding);
            if (string.IsNullOrEmpty(httpPage))
            {
                httpPage = "没有获取到内容页，请检查内容页URL是否正确，如果不正确请返回列表页设置重新设置。";
            }
            else
            {
                this.Session["ShowCode"] = httpPage;
            }
            XmlTextWriter writer = new XmlTextWriter(HttpContext.Current.Response.OutputStream, Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 4;
            writer.WriteStartDocument();
            writer.WriteStartElement("root", "");
            writer.WriteElementString("testContent", httpPage);
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }
    }
}

