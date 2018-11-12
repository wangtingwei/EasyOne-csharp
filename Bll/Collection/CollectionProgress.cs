namespace EasyOne.Collection
{
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Enumerations;
    using EasyOne.Model.Collection;
    using EasyOne.Model.CommonModel;
    using EasyOne.Model.Contents;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Web;

    public class CollectionProgress
    {
        private string m_AdminName;
        private string m_ArrPaging;
        private int m_CollectionCompleted;
        private int m_CollectionCount;
        private DateTime m_CollectionErrorTime;
        private IList<CollectionExclosionInfo> m_CollectionExclosionInfoList;
        private IList<CollectionFieldRuleInfo> m_CollectionFieldRuleInfoList;
        private CollectionItemInfo m_CollectionItemInfo;
        private string m_CollectionMessage;
        private int m_CollectionPhotoFailure;
        private int m_CollectionPhotoSuccess;
        private DateTime m_CollectionStartTime;
        private string m_CreateId;
        private Thread m_CreateThread;
        private string m_DefaultPicUrl;
        private string m_ErrorInfo;
        private string m_FileDown;
        private bool m_IsCreateHtml;
        private bool m_IsExclosion;
        private bool m_IsInput;
        private bool m_IsTitle;
        private bool m_IsTitleRepeat;
        private int m_ItemCompleted;
        private int m_ItemCount;
        private string m_ItemId;
        private ArrayList m_ListContentLinks;
        private string m_PhysicalApplicationPath;
        protected AutoResetEvent m_ResetEvent;
        private bool m_Result;
        private string m_ThumbPhoto;
        private string m_UploadFiles;
        private string m_UserName;

        private DataRow AddDataRow(DataTable dataTable, string fieldName, object fieldValue, FieldType fieldType, int fieldLevel)
        {
            DataRow row = dataTable.NewRow();
            row["FieldName"] = fieldName;
            row["FieldValue"] = fieldValue;
            row["FieldType"] = fieldType;
            row["FieldLevel"] = fieldLevel;
            dataTable.Rows.Add(row);
            return row;
        }

        private static ArrayList AddLinks(CollectionCommon collectionCommon, CollectionListRuleInfo collectionListRuleInfo, string constr)
        {
            ArrayList list = new ArrayList();
            if (!string.IsNullOrEmpty(constr))
            {
                string str = collectionCommon.GetInterceptionString(constr, collectionListRuleInfo.ListBeginCode, collectionListRuleInfo.ListEndCode);
                if (string.IsNullOrEmpty(str))
                {
                    return list;
                }
                list = collectionCommon.GetArray(str, collectionListRuleInfo.LinkBeginCode, collectionListRuleInfo.LinkEndCode);
                if (list.Count < 1)
                {
                    return list;
                }
            }
            return list;
        }

        private void AddListContentLinks(string nexturl, CollectionCommon collectionCommon, CollectionListRuleInfo collectionListRuleInfo)
        {
            if (DataValidator.IsUrl(nexturl))
            {
                Uri url = new Uri(nexturl);
                string httpPage = collectionCommon.GetHttpPage(url, this.m_CollectionItemInfo.CodeType);
                ArrayList list = AddLinks(collectionCommon, collectionListRuleInfo, httpPage);
                if (list.Count >= 1)
                {
                    foreach (string str2 in list)
                    {
                        this.m_ListContentLinks.Add(str2);
                    }
                }
            }
        }

        private string AddPagingContent(string nexturl, CollectionCommon collectionCommon, CollectionFieldRuleInfo collectionFieldRuleInfo, string interceptionContent)
        {
            if (!DataValidator.IsUrl(nexturl))
            {
                return interceptionContent;
            }
            Uri url = new Uri(nexturl);
            string httpPage = collectionCommon.GetHttpPage(url, this.m_CollectionItemInfo.CodeType);
            if (string.IsNullOrEmpty(httpPage))
            {
                return interceptionContent;
            }
            string str2 = collectionCommon.GetInterceptionString(httpPage, collectionFieldRuleInfo.BeginCode, collectionFieldRuleInfo.EndCode);
            if (string.IsNullOrEmpty(str2))
            {
                return interceptionContent;
            }
            return (interceptionContent + "[NextPage]" + str2);
        }

        private string AddPagingContent(string nextLink, string interceptionContent, CollectionCommon collectionCommon, CollectionFieldRuleInfo collectionFieldRuleInfo, CollectionPagingRuleInfo collectionPagingRuleInfo)
        {
            if (DataValidator.IsUrl(nextLink))
            {
                if (string.IsNullOrEmpty(this.m_ArrPaging))
                {
                    this.m_ArrPaging = nextLink;
                }
                else
                {
                    if (this.m_ArrPaging.IndexOf(nextLink, StringComparison.Ordinal) >= 0)
                    {
                        return interceptionContent;
                    }
                    this.m_ArrPaging = this.m_ArrPaging + nextLink;
                }
                Uri url = new Uri(nextLink);
                string httpPage = collectionCommon.GetHttpPage(url, this.m_CollectionItemInfo.CodeType);
                if (!string.IsNullOrEmpty(httpPage))
                {
                    string str2 = collectionCommon.GetInterceptionString(httpPage, collectionFieldRuleInfo.BeginCode, collectionFieldRuleInfo.EndCode);
                    if (string.IsNullOrEmpty(str2))
                    {
                        return interceptionContent;
                    }
                    interceptionContent = interceptionContent + "[NextPage]" + str2;
                    nextLink = collectionCommon.ConvertToAbsluteUrl(collectionCommon.GetPaing(httpPage, collectionPagingRuleInfo.PagingBeginCode, collectionPagingRuleInfo.PagingEndCode), this.m_CollectionItemInfo.Url);
                    if (!string.IsNullOrEmpty(nextLink))
                    {
                        this.m_ArrPaging = this.m_ArrPaging + nextLink;
                        interceptionContent = this.AddPagingContent(nextLink, interceptionContent, collectionCommon, collectionFieldRuleInfo, collectionPagingRuleInfo);
                    }
                }
            }
            return interceptionContent;
        }

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
            return testContent.Trim();
        }

        private void CountNextLinks(string nextLink, string constr, CollectionCommon collectionCommon, CollectionListRuleInfo collectionListRuleInfo, CollectionPagingRuleInfo collectionPagingRuleInfo)
        {
            if (DataValidator.IsUrl(nextLink))
            {
                if (string.IsNullOrEmpty(this.m_ArrPaging))
                {
                    this.m_ArrPaging = nextLink;
                }
                else
                {
                    if (this.m_ArrPaging.IndexOf(nextLink, StringComparison.Ordinal) >= 0)
                    {
                        return;
                    }
                    this.m_ArrPaging = this.m_ArrPaging + nextLink;
                }
                Uri url = new Uri(nextLink);
                constr = collectionCommon.GetHttpPage(url, this.m_CollectionItemInfo.CodeType);
                ArrayList list = AddLinks(collectionCommon, collectionListRuleInfo, constr);
                if (list.Count >= 1)
                {
                    foreach (string str in list)
                    {
                        this.m_ListContentLinks.Add(str);
                    }
                    nextLink = collectionCommon.ConvertToAbsluteUrl(collectionCommon.GetPaing(constr, collectionPagingRuleInfo.PagingBeginCode, collectionPagingRuleInfo.PagingEndCode), this.m_CollectionItemInfo.Url);
                    if (!string.IsNullOrEmpty(nextLink))
                    {
                        this.m_ArrPaging = this.m_ArrPaging + nextLink;
                        this.CountNextLinks(nextLink, constr, collectionCommon, collectionListRuleInfo, collectionPagingRuleInfo);
                    }
                }
            }
        }

        public void CreateCollectionProc()
        {
            this.m_CreateId = Guid.NewGuid().ToString();
            if (HttpContext.Current != null)
            {
                this.m_PhysicalApplicationPath = this.PhysicalApplicationPath;
            }
            XmlManage manage = XmlManage.Instance("Config/CreateCollectionWork.config", XmlType.File);
            manage.SetNodeValue("CollectionWork/WorkId", this.m_CreateId);
            try
            {
                manage.Save("Config/CreateCollectionWork.config");
            }
            catch (FileNotFoundException)
            {
                throw new CustomException("CreateCollectionWork.config文件未找到。");
            }
            catch
            {
                throw new CustomException("检查您的服务器是否给配置文件CreateCollectionWork.config或文件夹写入权限。");
            }
            this.StartCreate();
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Application[this.m_CreateId] = this;
            }
        }

        private DataRow DesignatedDataRow(DataTable dataTable, DataRow dataRow, FieldType fieldType, CollectionFieldRuleInfo collectionFieldRuleInfo, int fieldLevel)
        {
            dataRow = dataTable.NewRow();
            dataRow["FieldName"] = collectionFieldRuleInfo.FieldName;
            switch (fieldType)
            {
                case FieldType.NumberType:
                case FieldType.StatusType:
                    dataRow["FieldValue"] = DataConverter.CSingle(collectionFieldRuleInfo.SpecialSetting).ToString();
                    break;

                case FieldType.MoneyType:
                    dataRow["FieldValue"] = DataConverter.CDecimal(collectionFieldRuleInfo.SpecialSetting).ToString();
                    break;

                case FieldType.DateTimeType:
                    dataRow["FieldValue"] = DataConverter.CDate(collectionFieldRuleInfo.SpecialSetting).ToString();
                    break;

                case FieldType.BoolType:
                    dataRow["FieldValue"] = DataConverter.CBoolean(collectionFieldRuleInfo.SpecialSetting).ToString();
                    break;

                case FieldType.ContentType:
                    if (!string.IsNullOrEmpty(collectionFieldRuleInfo.SpecialSetting))
                    {
                        break;
                    }
                    this.m_Result = false;
                    return dataRow;

                case FieldType.TitleType:
                    if (!string.IsNullOrEmpty(collectionFieldRuleInfo.SpecialSetting))
                    {
                        if (this.IsTitle && CollectionHistory.Exists(collectionFieldRuleInfo.SpecialSetting.Trim()))
                        {
                            this.m_IsTitleRepeat = true;
                            return dataRow;
                        }
                        dataRow["FieldValue"] = collectionFieldRuleInfo.SpecialSetting;
                        break;
                    }
                    this.m_Result = false;
                    return dataRow;

                default:
                    dataRow["FieldValue"] = collectionFieldRuleInfo.SpecialSetting;
                    break;
            }
            dataRow["FieldType"] = fieldType;
            dataRow["FieldLevel"] = fieldLevel;
            dataTable.Rows.Add(dataRow);
            if ((fieldType == FieldType.StatusType) && (DataConverter.CLng(collectionFieldRuleInfo.SpecialSetting) == 0x63))
            {
                dataRow = dataTable.NewRow();
                dataRow["FieldName"] = "Editor";
                dataRow["FieldValue"] = this.m_AdminName;
                dataRow["FieldType"] = FieldType.TextType;
                dataRow["FieldLevel"] = 0;
                dataTable.Rows.Add(dataRow);
                dataRow = dataTable.NewRow();
                dataRow["FieldName"] = "PassedTime";
                dataRow["FieldValue"] = DateTime.Now.ToString("yyyy-MM-dd");
                dataRow["FieldType"] = FieldType.DateTimeType;
                dataRow["FieldLevel"] = 0;
                dataTable.Rows.Add(dataRow);
            }
            if (fieldType == FieldType.TitleType)
            {
                FieldInfo fieldInfoByFieldName = Field.GetFieldInfoByFieldName(this.m_CollectionItemInfo.ModelId, collectionFieldRuleInfo.FieldName);
                if (fieldInfoByFieldName.IsNull)
                {
                    this.m_Result = true;
                    return dataRow;
                }
                Collection<string> settings = fieldInfoByFieldName.Settings;
                if ((fieldInfoByFieldName.Settings.Count > 3) && DataConverter.CBoolean(settings[3]))
                {
                    dataRow = dataTable.NewRow();
                    dataRow["FieldName"] = "PinyinTitle";
                    dataRow["FieldValue"] = ChineseSpell.MakeSpellCode(collectionFieldRuleInfo.SpecialSetting, SpellOptions.EnableUnicodeLetter | SpellOptions.TranslateUnknowWordToInterrogation);
                    dataRow["FieldType"] = FieldType.TextType;
                    dataRow["FieldLevel"] = 0;
                    dataTable.Rows.Add(dataRow);
                }
            }
            return dataRow;
        }

        public void Detection(int itemId)
        {
            this.m_IsInput = false;
            if (itemId < 1)
            {
                this.m_ErrorInfo = "<li>采集项目ID不对！</li>";
            }
            else
            {
                this.m_CollectionItemInfo = CollectionItem.GetInfoById(itemId);
                if (this.m_CollectionItemInfo.IsNull)
                {
                    this.m_ErrorInfo = "<li>采集项目规则不存在！</li>";
                }
                else
                {
                    string str = "<img src =\"../Images/loading.gif\" width='18' height='18' />";
                    this.m_CollectionMessage = "正在分析 <font color='blue'>" + this.m_CollectionItemInfo.ItemName + "</font> 列表规则  请稍候..." + str + "<br/>";
                    CollectionListRuleInfo infoById = CollectionListRules.GetInfoById(this.m_CollectionItemInfo.ItemId);
                    if (infoById.IsNull)
                    {
                        this.m_ErrorInfo = "<li>" + this.m_CollectionItemInfo.ItemName + "采集项目的列表规则不存在！</li>";
                    }
                    else
                    {
                        CollectionCommon collectionCommon = new CollectionCommon();
                        if (!DataValidator.IsUrl(this.m_CollectionItemInfo.Url))
                        {
                            this.m_ErrorInfo = "<li>" + this.m_CollectionItemInfo.ItemName + "采集项目的URL不是正确地址！</li>";
                        }
                        else
                        {
                            Uri url = new Uri(this.m_CollectionItemInfo.Url);
                            string httpPage = collectionCommon.GetHttpPage(url, this.m_CollectionItemInfo.CodeType);
                            if (string.IsNullOrEmpty(httpPage))
                            {
                                this.m_ErrorInfo = "<li>不能获得" + this.m_CollectionItemInfo.ItemName + "项目的" + this.m_CollectionItemInfo.Url + "源代码，有可能对方屏蔽了端口或本服务器屏蔽了端口！</li>";
                            }
                            else
                            {
                                string str3 = collectionCommon.GetInterceptionString(httpPage, infoById.ListBeginCode, infoById.ListEndCode);
                                if (string.IsNullOrEmpty(str3))
                                {
                                    this.m_ErrorInfo = "<li>" + this.m_CollectionItemInfo.ItemName + "项目列表设置错误！</li>";
                                }
                                else
                                {
                                    ArrayList list = collectionCommon.GetArray(str3, infoById.LinkBeginCode, infoById.LinkEndCode);
                                    if (list.Count < 1)
                                    {
                                        this.m_ErrorInfo = "<li>" + this.m_CollectionItemInfo.ItemName + "项目链接设置错误！</li>";
                                    }
                                    else
                                    {
                                        this.m_ListContentLinks = list;
                                        if (infoById.UsePaging)
                                        {
                                            this.m_CollectionMessage = "正在分析 <font color='blue'>" + this.m_CollectionItemInfo.ItemName + "</font> 列表分页规则  请稍候..." + str + "<br/>";
                                            this.DetectionPaging(httpPage, collectionCommon, infoById);
                                            if (!string.IsNullOrEmpty(this.ErrorInfo))
                                            {
                                                return;
                                            }
                                        }
                                        this.m_CollectionMessage = "正在分析 <font color='blue'>" + this.m_CollectionItemInfo.ItemName + "</font> 字段规则  请稍候..." + str + "<br/>";
                                        this.m_CollectionFieldRuleInfoList = CollectionFieldRules.GetList(this.m_CollectionItemInfo.ItemId);
                                        if (this.m_CollectionFieldRuleInfoList.Count < 1)
                                        {
                                            this.m_ErrorInfo = "<li>" + this.m_CollectionItemInfo.ItemName + "项目字段设置没有字段，请进行字段设置！</li>";
                                        }
                                        else
                                        {
                                            string fieldName = "";
                                            StringBuilder builder = new StringBuilder();
                                            foreach (CollectionFieldRuleInfo info2 in this.m_CollectionFieldRuleInfoList)
                                            {
                                                if (info2.RuleType != 2)
                                                {
                                                    continue;
                                                }
                                                fieldName = info2.FieldName;
                                                if (builder.Length > 0)
                                                {
                                                    if (builder.ToString().Contains("$" + info2.FieldName + "$"))
                                                    {
                                                        this.m_ErrorInfo = "<li>" + this.m_CollectionItemInfo.ItemName + "项目字段" + fieldName + "出现重复，请检查数据库删除多余数据！</li>";
                                                        return;
                                                    }
                                                    builder.Append(info2.FieldName + "$");
                                                }
                                                else
                                                {
                                                    builder.Append("$" + info2.FieldName + "$");
                                                }
                                                if (info2.IsNull)
                                                {
                                                    this.m_ErrorInfo = "<li>" + this.m_CollectionItemInfo.ItemName + "项目没有进行字段设置！</li>";
                                                    return;
                                                }
                                                url = new Uri(collectionCommon.ConvertToAbsluteUrl(list[0].ToString(), this.m_CollectionItemInfo.Url));
                                                httpPage = collectionCommon.GetHttpPage(url, this.m_CollectionItemInfo.CodeType);
                                                if (string.IsNullOrEmpty(collectionCommon.GetInterceptionString(httpPage, info2.BeginCode, info2.EndCode)))
                                                {
                                                    this.m_ErrorInfo = "<li>" + this.m_CollectionItemInfo.ItemName + "项目字段设置" + fieldName + "采集设置获取失败！</li>";
                                                    return;
                                                }
                                            }
                                            this.m_CollectionItemInfo.Detection = true;
                                            CollectionItem.Update(this.m_CollectionItemInfo);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void DetectionPaging(string constr, CollectionCommon collectionCommon, CollectionListRuleInfo collectionListRuleInfo)
        {
            CollectionPagingRuleInfo infoById = CollectionPagingRules.GetInfoById(this.m_CollectionItemInfo.ItemId, 0);
            switch (infoById.PagingType)
            {
                case 1:
                {
                    string str = collectionCommon.ConvertToAbsluteUrl(collectionCommon.GetPaing(constr, infoById.PagingBeginCode, infoById.PagingEndCode), this.m_CollectionItemInfo.Url);
                    if (!string.IsNullOrEmpty(str))
                    {
                        this.m_ArrPaging = "";
                        this.CountNextLinks(str, constr, collectionCommon, collectionListRuleInfo, infoById);
                        return;
                    }
                    return;
                }
                case 2:
                {
                    string str2 = infoById.DesignatedUrl.ToLower();
                    if (!string.IsNullOrEmpty(str2))
                    {
                        if (str2.IndexOf("{$id}", StringComparison.OrdinalIgnoreCase) < 0)
                        {
                            this.m_ErrorInfo = "<li>" + this.m_CollectionItemInfo.ItemName + "项目列表指定分页URL字符串没有标签{$ID}！</li>";
                            return;
                        }
                        int scopeBegin = infoById.ScopeBegin;
                        int scopeEnd = infoById.ScopeEnd;
                        List<string> list = new List<string>();
                        if (scopeEnd < scopeBegin)
                        {
                            for (int i = scopeBegin; i >= scopeEnd; i--)
                            {
                                list.Add(str2.Replace("{$id}", i.ToString()));
                            }
                        }
                        else
                        {
                            for (int j = scopeBegin; j <= scopeEnd; j++)
                            {
                                list.Add(str2.Replace("{$id}", j.ToString()));
                            }
                        }
                        foreach (string str3 in list)
                        {
                            this.AddListContentLinks(str3, collectionCommon, collectionListRuleInfo);
                        }
                        break;
                    }
                    this.m_ErrorInfo = "<li>" + this.m_CollectionItemInfo.ItemName + "项目列表指定分页没有填写地址！</li>";
                    return;
                }
                case 3:
                    if (string.IsNullOrEmpty(infoById.PagingUrlList))
                    {
                        this.m_ErrorInfo = "<li>" + this.m_CollectionItemInfo.ItemName + "项目手动分页不能为空！</li>";
                        return;
                    }
                    if (infoById.PagingUrlList.IndexOf("\r\n", StringComparison.Ordinal) > 0)
                    {
                        foreach (string str4 in infoById.PagingUrlList.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            this.AddListContentLinks(str4, collectionCommon, collectionListRuleInfo);
                        }
                        return;
                    }
                    this.AddListContentLinks(infoById.PagingUrlList, collectionCommon, collectionListRuleInfo);
                    return;

                case 4:
                {
                    string str5 = collectionCommon.GetInterceptionString(constr, infoById.PagingBeginCode, infoById.PagingEndCode);
                    if (!string.IsNullOrEmpty(str5))
                    {
                        ArrayList list2 = collectionCommon.GetArray(str5, infoById.LinkBeginCode, infoById.LinkEndCode);
                        StringBuilder builder = new StringBuilder();
                        for (int k = 0; k < list2.Count; k++)
                        {
                            if (builder.Length > 0)
                            {
                                builder.Append("," + collectionCommon.ConvertToAbsluteUrl(list2[k].ToString(), this.m_CollectionItemInfo.Url));
                            }
                            else
                            {
                                builder.Append(collectionCommon.ConvertToAbsluteUrl(list2[k].ToString(), this.m_CollectionItemInfo.Url));
                            }
                        }
                        if (string.IsNullOrEmpty(builder.ToString()))
                        {
                            this.m_ErrorInfo = "<li>" + this.m_CollectionItemInfo.ItemName + "没有截取到分页URL链接！</li>";
                            return;
                        }
                        foreach (string str6 in builder.ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            this.AddListContentLinks(str6, collectionCommon, collectionListRuleInfo);
                        }
                        break;
                    }
                    this.m_ErrorInfo = "<li>" + this.m_CollectionItemInfo.ItemName + "没有截取到分页URL列表！</li>";
                    return;
                }
                default:
                    return;
            }
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

        private string GetAllContent(string content, string interceptionContent, CollectionCommon collectionCommon, CollectionFieldRuleInfo collectionFieldRuleInfo)
        {
            CollectionPagingRuleInfo infoById = CollectionPagingRules.GetInfoById(this.m_CollectionItemInfo.ItemId, 1);
            if (!infoById.IsNull)
            {
                switch (infoById.PagingType)
                {
                    case 1:
                    {
                        string str = collectionCommon.GetPaing(content, infoById.PagingBeginCode, infoById.PagingEndCode);
                        if (!string.IsNullOrEmpty(str))
                        {
                            interceptionContent = this.AddPagingContent(collectionCommon.ConvertToAbsluteUrl(str, this.m_CollectionItemInfo.Url), interceptionContent, collectionCommon, collectionFieldRuleInfo, infoById);
                            return interceptionContent;
                        }
                        return interceptionContent;
                    }
                    case 2:
                    {
                        string str2 = infoById.DesignatedUrl.ToLower();
                        if (!string.IsNullOrEmpty(str2))
                        {
                            if (str2.IndexOf("{$id}", StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                int scopeBegin = infoById.ScopeBegin;
                                int scopeEnd = infoById.ScopeEnd;
                                List<string> list = new List<string>();
                                if (scopeEnd < scopeBegin)
                                {
                                    for (int i = scopeBegin; i >= scopeEnd; i--)
                                    {
                                        list.Add(str2.Replace("{$id}", i.ToString()));
                                    }
                                }
                                else
                                {
                                    for (int j = scopeBegin; j <= scopeEnd; j++)
                                    {
                                        list.Add(str2.Replace("{$id}", j.ToString()));
                                    }
                                }
                                foreach (string str3 in list)
                                {
                                    interceptionContent = this.AddPagingContent(str3, collectionCommon, collectionFieldRuleInfo, interceptionContent);
                                }
                            }
                            return interceptionContent;
                        }
                        return interceptionContent;
                    }
                    case 3:
                        if (!string.IsNullOrEmpty(infoById.PagingUrlList))
                        {
                            if (infoById.PagingUrlList.IndexOf("\r\n", StringComparison.Ordinal) > 0)
                            {
                                foreach (string str4 in infoById.PagingUrlList.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
                                {
                                    interceptionContent = this.AddPagingContent(str4, collectionCommon, collectionFieldRuleInfo, interceptionContent);
                                }
                                return interceptionContent;
                            }
                            interceptionContent = this.AddPagingContent(infoById.PagingUrlList, collectionCommon, collectionFieldRuleInfo, interceptionContent);
                        }
                        return interceptionContent;

                    case 4:
                    {
                        string str5 = collectionCommon.GetInterceptionString(content, infoById.PagingBeginCode, infoById.PagingEndCode);
                        if (!string.IsNullOrEmpty(str5))
                        {
                            ArrayList list2 = collectionCommon.GetArray(str5, infoById.LinkBeginCode, infoById.LinkEndCode);
                            StringBuilder builder = new StringBuilder();
                            for (int k = 0; k < list2.Count; k++)
                            {
                                if (builder.Length > 0)
                                {
                                    builder.Append("," + collectionCommon.ConvertToAbsluteUrl(list2[k].ToString(), this.m_CollectionItemInfo.Url));
                                }
                                else
                                {
                                    builder.Append(collectionCommon.ConvertToAbsluteUrl(list2[k].ToString(), this.m_CollectionItemInfo.Url));
                                }
                            }
                            if (!string.IsNullOrEmpty(builder.ToString()))
                            {
                                foreach (string str6 in builder.ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                                {
                                    interceptionContent = this.AddPagingContent(str6, collectionCommon, collectionFieldRuleInfo, interceptionContent);
                                }
                            }
                            return interceptionContent;
                        }
                        return interceptionContent;
                    }
                }
            }
            return interceptionContent;
        }

        private DataTable GetDataTableField(string link, NodeInfo nodeInfo)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("FieldName");
            dataTable.Columns.Add("FieldValue");
            dataTable.Columns.Add("FieldType");
            dataTable.Columns.Add("FieldLevel");
            DataRow dataRow = this.AddDataRow(dataTable, "NodeId", this.m_CollectionItemInfo.NodeId, FieldType.NodeType, 0);
            dataRow = this.AddDataRow(dataTable, "infoid", this.m_CollectionItemInfo.InfoNodeId, FieldType.InfoType, 0);
            dataRow = this.AddDataRow(dataTable, "SpecialId", this.m_CollectionItemInfo.SpecialId, FieldType.SpecialType, 0);
            dataRow = this.AddDataRow(dataTable, "Inputer", this.m_UserName, FieldType.TextType, 0);
            string checkStr = "Title,Status,EliteLevel,Priority,Hits,DayHits,WeekHits,MonthHits,UpdateTime,NodeId,TemplateFile,InfoId,SpecialId";
            FieldType none = FieldType.None;
            foreach (CollectionFieldRuleInfo info in this.m_CollectionFieldRuleInfoList)
            {
                CollectionCommon common;
                string httpPage;
                string str3;
                FieldInfo fieldInfoByFieldName;
                string str4;
                if (!this.m_Result)
                {
                    return dataTable;
                }
                if (this.m_IsTitleRepeat && this.IsTitle)
                {
                    return dataTable;
                }
                int fieldLevel = 1;
                if (StringHelper.FoundCharInArr(checkStr, info.FieldName))
                {
                    fieldLevel = 0;
                }
                if (System.Enum.IsDefined(typeof(FieldType), info.FieldType))
                {
                    none = (FieldType) System.Enum.Parse(typeof(FieldType), info.FieldType);
                }
                switch (info.RuleType)
                {
                    case 0:
                    case 1:
                    {
                        dataRow = this.DesignatedDataRow(dataTable, dataRow, none, info, fieldLevel);
                        continue;
                    }
                    case 2:
                    {
                        common = new CollectionCommon();
                        Uri url = new Uri(common.ConvertToAbsluteUrl(link, this.m_CollectionItemInfo.Url));
                        httpPage = common.GetHttpPage(url, this.m_CollectionItemInfo.CodeType);
                        str3 = common.GetInterceptionString(httpPage, info.BeginCode, info.EndCode);
                        if ((info.ExclosionId != 0) && (this.m_CollectionExclosionInfoList != null))
                        {
                            foreach (CollectionExclosionInfo info2 in this.m_CollectionExclosionInfoList)
                            {
                                if (info2.ExclosionId == info.ExclosionId)
                                {
                                    this.m_IsExclosion = CollectionExclosion.IsExclosion(info2, str3);
                                }
                            }
                        }
                        if (this.m_IsExclosion)
                        {
                            return dataTable;
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
                                fieldInfoByFieldName = Field.GetFieldInfoByFieldName(this.m_CollectionItemInfo.ModelId, info.FieldName);
                                if (!fieldInfoByFieldName.IsNull)
                                {
                                    break;
                                }
                                this.m_Result = true;
                                return dataTable;

                            case FieldType.MultipleTextType:
                            case FieldType.MultipleHtmlTextType:
                            case FieldType.LinkType:
                            case FieldType.PictureType:
                            case FieldType.NodeType:
                            case FieldType.InfoType:
                            case FieldType.SkinType:
                            case FieldType.DownServerType:
                            case FieldType.SpecialType:
                            case FieldType.ProductType:
                                goto Label_07B1;

                            case FieldType.NumberType:
                            case FieldType.StatusType:
                            {
                                dataRow = this.AddDataRow(dataTable, info.FieldName, DataConverter.CSingle(str3).ToString(), none, fieldLevel);
                                continue;
                            }
                            case FieldType.MoneyType:
                            {
                                dataRow = this.AddDataRow(dataTable, info.FieldName, DataConverter.CDecimal(str3).ToString(), none, fieldLevel);
                                continue;
                            }
                            case FieldType.DateTimeType:
                            {
                                dataRow = this.AddDataRow(dataTable, info.FieldName, DataConverter.CDate(str3).ToString(), none, fieldLevel);
                                continue;
                            }
                            case FieldType.BoolType:
                            {
                                dataRow = this.AddDataRow(dataTable, info.FieldName, DataConverter.CBoolean(str3).ToString(), none, fieldLevel);
                                continue;
                            }
                            case FieldType.FileType:
                            {
                                this.m_FileDown = "";
                                if (info.SpecialSetting.IndexOf("$$$", StringComparison.Ordinal) > 0)
                                {
                                    string[] strArray = info.SpecialSetting.Split(new string[] { "$$$" }, StringSplitOptions.RemoveEmptyEntries);
                                    bool isDown = DataConverter.CBoolean(strArray[1]);
                                    dataRow = dataTable.NewRow();
                                    dataRow["FieldName"] = info.FieldName;
                                    dataRow["FieldValue"] = this.SaveFile(str3, nodeInfo, common, isDown, strArray[0]);
                                    dataRow["FieldType"] = none;
                                    dataRow["FieldLevel"] = fieldLevel;
                                    dataTable.Rows.Add(dataRow);
                                    if (isDown)
                                    {
                                        dataRow = this.AddDataRow(dataTable, strArray[2], this.m_FileDown, none, fieldLevel);
                                    }
                                }
                                continue;
                            }
                            case FieldType.KeywordType:
                            {
                                dataRow = dataTable.NewRow();
                                dataRow["FieldName"] = info.FieldName;
                                dataRow["FieldValue"] = CollectionCommon.CreateKeyWord(CommonFilter(info.FilterRuleId, info.PrivateFilter, common, str3).Replace(",", "|").Replace("，", "|"), DataConverter.CLng(info.SpecialSetting));
                                dataRow["FieldType"] = none;
                                dataRow["FieldLevel"] = fieldLevel;
                                dataTable.Rows.Add(dataRow);
                                continue;
                            }
                            case FieldType.ContentType:
                                str4 = CommonFilter(info.FilterRuleId, info.PrivateFilter, common, str3);
                                if (!string.IsNullOrEmpty(str4))
                                {
                                    goto Label_053F;
                                }
                                this.m_Result = false;
                                return dataTable;

                            case FieldType.MultiplePhotoType:
                            {
                                this.m_ThumbPhoto = "";
                                dataRow = dataTable.NewRow();
                                dataRow["FieldName"] = info.FieldName;
                                dataRow["FieldValue"] = this.SaveMultiplePhoto(str3, nodeInfo, common, true);
                                dataRow["FieldType"] = none;
                                dataRow["FieldLevel"] = fieldLevel;
                                dataTable.Rows.Add(dataRow);
                                if (dataTable.Select("FieldName = 'DefaultPicUrl'").Length < 1)
                                {
                                    dataRow = this.AddDataRow(dataTable, "DefaultPicUrl", this.m_ThumbPhoto, none, 0);
                                }
                                continue;
                            }
                        }
                        goto Label_07B1;
                    }
                    default:
                    {
                        continue;
                    }
                }
                str3 = str3.Trim();
                if (string.IsNullOrEmpty(str3))
                {
                    this.m_Result = false;
                    return dataTable;
                }
                if (this.IsTitle && CollectionHistory.Exists(str3))
                {
                    this.m_IsTitleRepeat = true;
                    return dataTable;
                }
                if (str3.Length > 0xff)
                {
                    str3 = str3.Substring(0, 0xff);
                }
                str3 = CommonFilter(info.FilterRuleId, info.PrivateFilter, common, str3);
                dataRow = this.AddDataRow(dataTable, info.FieldName, str3, none, fieldLevel);
                Collection<string> settings = fieldInfoByFieldName.Settings;
                if ((fieldInfoByFieldName.Settings.Count > 3) && DataConverter.CBoolean(settings[3]))
                {
                    dataRow = this.AddDataRow(dataTable, "PinyinTitle", ChineseSpell.MakeSpellCode(str3, SpellOptions.EnableUnicodeLetter | SpellOptions.TranslateUnknowWordToInterrogation), FieldType.TextType, 0);
                }
                continue;
            Label_053F:
                if (info.UsePaging)
                {
                    this.m_ArrPaging = "";
                    str4 = CommonFilter(info.FilterRuleId, info.PrivateFilter, common, this.GetAllContent(httpPage, str3, common, info));
                }
                this.m_DefaultPicUrl = "";
                this.m_UploadFiles = "";
                dataRow = dataTable.NewRow();
                dataRow["FieldName"] = info.FieldName;
                if (DataConverter.CBoolean(info.SpecialSetting))
                {
                    str4 = this.SavePhoto(str4, nodeInfo, common);
                }
                dataRow["FieldValue"] = str4;
                dataRow["FieldType"] = none;
                dataRow["FieldLevel"] = fieldLevel;
                dataTable.Rows.Add(dataRow);
                if (dataTable.Select("FieldName = 'DefaultPicUrl'").Length < 1)
                {
                    dataRow = this.AddDataRow(dataTable, "DefaultPicUrl", this.m_DefaultPicUrl, none, 0);
                    dataRow = this.AddDataRow(dataTable, "UploadFiles", this.m_UploadFiles, none, 0);
                }
                continue;
            Label_07B1:
                dataRow = dataTable.NewRow();
                dataRow["FieldName"] = info.FieldName;
                dataRow["FieldValue"] = CommonFilter(info.FilterRuleId, info.PrivateFilter, common, str3);
                dataRow["FieldType"] = none;
                dataRow["FieldLevel"] = fieldLevel;
                dataTable.Rows.Add(dataRow);
            }
            return dataTable;
        }

        public string SaveFile(string content, NodeInfo nodeInfo, CollectionCommon collectionCommon, bool isDown, string fileType)
        {
            int num = 0;
            string str = VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.UploadDir);
            StringBuilder builder = new StringBuilder();
            WebClient client = new WebClient();
            MatchCollection matchs = new Regex("<a(?<Attributes1>[\\s\\S]*?)href=(\"{1}|'{1}|)(?<picture>[^\\[^>]*?(" + fileType + "))(\"{1}|'{1}|)(?<Attributes2>[\\s\\S]*?)>", RegexOptions.IgnoreCase).Matches(content);
            if (matchs.Count <= 0)
            {
                return "";
            }
            StringBuilder builder2 = new StringBuilder();
            foreach (Match match in matchs)
            {
                string str2 = collectionCommon.ConvertToAbsluteUrl(match.Groups["picture"].Value, this.m_CollectionItemInfo.Url);
                if (builder2.Length == 0)
                {
                    builder2.Append(str2);
                }
                else
                {
                    if (builder2.ToString().IndexOf(str2, StringComparison.Ordinal) > 0)
                    {
                        continue;
                    }
                    builder2.Append(str2);
                }
                num++;
                try
                {
                    string str3 = DataSecurity.MakeFileRndName();
                    string str4 = Path.GetExtension(str2).ToLower();
                    string str5 = EasyOne.Contents.Nodes.UploadPathParse(nodeInfo, str3 + str4).Replace("//", "/");
                    string str6 = str5 + str3 + str4;
                    StringBuilder builder3 = new StringBuilder();
                    string path = Path.Combine(this.PhysicalApplicationPath, str + str5);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    builder3.Append(path);
                    builder3.Append(str3 + str4);
                    client.DownloadFile(str2, builder3.ToString());
                    if (builder.Length > 0)
                    {
                        builder.Append("$$$下载地址").Append(num.ToString()).Append("|").Append(str6);
                    }
                    else
                    {
                        builder.Append("下载地址").Append(num.ToString()).Append("|").Append(str6);
                        if (isDown)
                        {
                            this.m_FileDown = str6;
                        }
                    }
                }
                catch (WebException)
                {
                    this.m_CollectionPhotoFailure++;
                }
                client.Dispose();
            }
            return builder.ToString();
        }

        public string SaveMultiplePhoto(string content, NodeInfo nodeInfo, CollectionCommon collectionCommon, bool isThumb)
        {
            int num = 0;
            string str = VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.UploadDir);
            StringBuilder builder = new StringBuilder();
            WebClient client = new WebClient();
            MatchCollection matchs = new Regex("<img(?<Attributes1>[\\s\\S]*?)src=(\"{1}|'{1}|)(?<picture>[^\\[^>]*?(gif|jpg|jpeg|bmp|png))(\"{1}|'{1}|)(?<Attributes2>[\\s\\S]*?)>", RegexOptions.IgnoreCase).Matches(content);
            if (matchs.Count <= 0)
            {
                return "";
            }
            foreach (Match match in matchs)
            {
                num++;
                string path = collectionCommon.ConvertToAbsluteUrl(match.Groups["picture"].Value, this.m_CollectionItemInfo.Url);
                try
                {
                    string str3 = DataSecurity.MakeFileRndName();
                    string str4 = Path.GetExtension(path).ToLower();
                    string str5 = EasyOne.Contents.Nodes.UploadPathParse(nodeInfo, str3 + str4).Replace("//", "/");
                    string originalImagePath = str5 + str3 + str4;
                    string thumbnailPath = str5 + str3 + "_S" + str4;
                    StringBuilder builder2 = new StringBuilder();
                    string str8 = Path.Combine(this.PhysicalApplicationPath, str + str5);
                    if (!Directory.Exists(str8))
                    {
                        Directory.CreateDirectory(str8);
                    }
                    builder2.Append(str8);
                    builder2.Append(str3 + str4);
                    client.DownloadFile(path, builder2.ToString());
                    if (isThumb)
                    {
                        Thumbs.GetThumbsPath(originalImagePath, thumbnailPath, this.m_PhysicalApplicationPath);
                    }
                    if (builder.Length > 0)
                    {
                        builder.Append("$$$图片地址" + num.ToString() + "|" + originalImagePath);
                    }
                    else
                    {
                        builder.Append(string.Concat(new object[] { "图片地址", num, "|", originalImagePath }));
                        if (isThumb)
                        {
                            this.m_ThumbPhoto = thumbnailPath;
                        }
                        else
                        {
                            this.m_ThumbPhoto = originalImagePath;
                        }
                    }
                }
                catch (WebException)
                {
                    this.m_CollectionPhotoFailure++;
                }
                client.Dispose();
            }
            return builder.ToString();
        }

        public string SavePhoto(string content, NodeInfo nodeInfo, CollectionCommon collectionCommon)
        {
            string str = VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.UploadDir);
            StringBuilder builder = new StringBuilder();
            WebClient client = new WebClient();
            MatchCollection matchs = new Regex("<img(?<Attributes1>[\\s\\S]*?)src=(\"{1}|'{1}|)(?<picture>[^\\[^>]*?(gif|jpg|jpeg|bmp|png))(\"{1}|'{1}|)(?<Attributes2>[\\s\\S]*?)>", RegexOptions.IgnoreCase).Matches(content);
            if (matchs.Count > 0)
            {
                foreach (Match match in matchs)
                {
                    string path = collectionCommon.ConvertToAbsluteUrl(match.Groups["picture"].Value, this.m_CollectionItemInfo.Url);
                    try
                    {
                        string str3 = DataSecurity.MakeFileRndName();
                        string str4 = Path.GetExtension(path).ToLower();
                        string str5 = EasyOne.Contents.Nodes.UploadPathParse(nodeInfo, str3 + str4).Replace("//", "/");
                        string str6 = str5 + str3 + str4;
                        StringBuilder builder2 = new StringBuilder();
                        string str7 = Path.Combine(this.PhysicalApplicationPath, str + str5);
                        if (!Directory.Exists(str7))
                        {
                            Directory.CreateDirectory(str7);
                        }
                        builder2.Append(str7);
                        builder2.Append(str3 + str4);
                        client.DownloadFile(path, builder2.ToString());
                        content = content.Replace(match.Groups["picture"].Value, "{PE.SiteConfig.ApplicationPath/}{PE.SiteConfig.uploaddir/}/" + str6);
                        if (builder.Length > 0)
                        {
                            builder.Append("|" + str6);
                        }
                        else
                        {
                            builder.Append(str6);
                        }
                        if (string.IsNullOrEmpty(this.m_DefaultPicUrl))
                        {
                            string thumbnailPath = str5 + str3 + "_S" + str4;
                            Thumbs.GetThumbsPath(str6, thumbnailPath, this.m_PhysicalApplicationPath);
                            this.m_DefaultPicUrl = thumbnailPath;
                        }
                        this.m_CollectionPhotoSuccess++;
                    }
                    catch (WebException)
                    {
                        this.m_CollectionPhotoFailure++;
                    }
                    client.Dispose();
                    this.m_UploadFiles = builder.ToString();
                }
            }
            return content;
        }

        private void StartCollection()
        {
            int num = DataConverter.CLng(this.m_CollectionItemInfo.MaxNum);
            int num2 = 0;
            NodeInfo nodeById = EasyOne.Contents.Nodes.GetNodeById(this.m_CollectionItemInfo.NodeId);
            this.m_CollectionCompleted = 0;
            if (this.m_CollectionItemInfo.OrderType == 1)
            {
                string str = "";
                int num3 = this.m_ListContentLinks.Count - 1;
                if (num3 > 0)
                {
                    for (int i = 0; i < DataConverter.CLng(num3 / 2); i++)
                    {
                        str = this.m_ListContentLinks[i].ToString();
                        this.m_ListContentLinks[i] = this.m_ListContentLinks[num3 - i];
                        this.m_ListContentLinks[num3 - i] = str;
                    }
                }
            }
            this.m_IsInput = true;
            if ((num > 0) && (num <= this.m_ListContentLinks.Count))
            {
                this.m_CollectionCount = num;
            }
            else
            {
                this.m_CollectionCount = this.m_ListContentLinks.Count;
            }
            CollectionItem.UpdateCollecDate(this.m_CollectionItemInfo.ItemId);
            this.m_CollectionStartTime = DateTime.Now;
            string str2 = "<br/><font color='blue'>" + this.m_CollectionItemInfo.ItemName + "</font>总共要采集 <font color='blue'><b>" + this.m_CollectionCount.ToString() + "</b></font> 个内容页面<br/><br/>";
            string str3 = "";
            int collectionSleep = SiteConfig.SiteOption.CollectionSleep;
            int num6 = 0;
            int num7 = 0;
            int num8 = 0;
            foreach (string str4 in this.m_ListContentLinks)
            {
                try
                {
                    if ((num > 0) && (num2 == num))
                    {
                        this.m_CollectionCompleted = num;
                        break;
                    }
                    this.m_Result = true;
                    this.m_IsExclosion = false;
                    this.m_IsTitleRepeat = false;
                    DataTable dataTableField = this.GetDataTableField(str4, nodeById);
                    DataRow[] rowArray = dataTableField.Select("FieldName = 'title'");
                    string str5 = "";
                    if ((rowArray != null) && (rowArray.Length > 0))
                    {
                        str5 = rowArray[0]["FieldValue"].ToString();
                    }
                    this.m_CollectionCompleted++;
                    if (this.IsTitle && this.m_IsTitleRepeat)
                    {
                        str3 = string.Concat(new object[] { "<li>没有采集第 <font color='red'><b>", this.m_CollectionCompleted, "</b></font>  个页面，因为重复标题采集历史记录已经记录。</li>", str3 });
                        this.m_CollectionMessage = str2 + str3;
                        num7++;
                    }
                    else if (this.m_IsExclosion)
                    {
                        str3 = string.Concat(new object[] { "<li>没有采集第 <font color='red'><b>", this.m_CollectionCompleted, "</b></font>  个页面，已被排除规则排除。</li>", str3 });
                        this.m_CollectionMessage = str2 + str3;
                        num7++;
                    }
                    else
                    {
                        CollectionHistoryInfo collectionHistoryInfo = new CollectionHistoryInfo();
                        CollectionCommon common = new CollectionCommon();
                        collectionHistoryInfo.ItemId = this.m_CollectionItemInfo.ItemId;
                        collectionHistoryInfo.ModelId = this.m_CollectionItemInfo.ModelId;
                        collectionHistoryInfo.NewsUrl = common.ConvertToAbsluteUrl(str4, this.m_CollectionItemInfo.Url);
                        collectionHistoryInfo.NodeId = this.m_CollectionItemInfo.NodeId;
                        collectionHistoryInfo.CollectionTime = DateTime.Now;
                        collectionHistoryInfo.Title = str5;
                        if (this.m_Result)
                        {
                            if (ContentManage.Add(this.m_CollectionItemInfo.ModelId, dataTableField))
                            {
                                collectionHistoryInfo.Result = true;
                                DataRow[] rowArray2 = dataTableField.Select("FieldName = 'generalId'");
                                collectionHistoryInfo.GeneralId = DataConverter.CLng(rowArray2[0]["FieldValue"].ToString());
                                str3 = string.Concat(new object[] { "<li>采集", str5, "成功，录入第 <font color='red'><b>", this.m_CollectionCompleted, "</b></font>  个页面</li>", str3 });
                                num2++;
                                int num9 = DataConverter.CLng(dataTableField.Select("FieldName = 'status'")[0]["FieldValue"].ToString());
                                if (this.m_CollectionItemInfo.AutoCreateHtml && (num9 == 0x63))
                                {
                                    this.m_IsCreateHtml = true;
                                }
                                if (collectionSleep != 0)
                                {
                                    if (num6 < 5)
                                    {
                                        num6++;
                                    }
                                    else
                                    {
                                        str3 = "<li>缓解服务器压力，停止采集" + collectionSleep.ToString() + "秒</li>" + str3;
                                        Thread.Sleep((int) (collectionSleep * 0x3e8));
                                        num6 = 0;
                                    }
                                }
                            }
                            else
                            {
                                str3 = string.Concat(new object[] { "<li>采集", str5, "失败，录入第 <font color='red'><b>", this.m_CollectionCompleted, "</b></font>  个页面</li>", str3 });
                                collectionHistoryInfo.Result = false;
                                collectionHistoryInfo.GeneralId = 0;
                            }
                        }
                        else
                        {
                            str3 = string.Concat(new object[] { "<li>采集", str5, "规则截取失败第 <font color='red'><b>", this.m_CollectionCompleted, "</b></font>  个页面</li>", str3 });
                            collectionHistoryInfo.Result = false;
                            collectionHistoryInfo.GeneralId = 0;
                        }
                        CollectionHistory.Add(collectionHistoryInfo);
                        this.m_CollectionMessage = str2 + str3;
                        if (num7 < 0x1d)
                        {
                            num7++;
                        }
                        else
                        {
                            num7 = 0;
                            str3 = "";
                        }
                    }
                    continue;
                }
                catch (Exception exception)
                {
                    str3 = "<li>采集错误： <font color='red'>" + exception.Message + "</font></li>";
                    this.m_CollectionMessage = str2 + str3;
                    num7++;
                    num8++;
                    if (num8 <= 10)
                    {
                        continue;
                    }
                    this.m_ErrorInfo = "<li>强制终止采集进程请检查数据库字段设置，采集规则设置是否正确！</li>";
                    return;
                }
            }
            this.m_CollectionMessage = str2 + this.m_CollectionItemInfo.ItemName + "全部采集完成！<br/><br/>" + str3;
            this.m_CollectionErrorTime = DateTime.Now;
        }

        private void StartCreate()
        {
            this.m_CreateThread = new Thread(new ThreadStart(this.Work));
            this.m_CreateThread.Start();
        }

        private void Work()
        {
            try
            {
                if (string.IsNullOrEmpty(this.ItemId))
                {
                    this.m_ErrorInfo = "<li>没有选择要采集的项目！</li>";
                }
                else
                {
                    this.m_CollectionExclosionInfoList = CollectionExclosion.GetList(0, 0);
                    if (this.ItemId.IndexOf(',') > 0)
                    {
                        string[] strArray = this.ItemId.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        this.m_ItemCount = strArray.Length;
                        foreach (string str in strArray)
                        {
                            this.Detection(DataConverter.CLng(str));
                            if (string.IsNullOrEmpty(this.ErrorInfo))
                            {
                                this.StartCollection();
                                this.m_ItemCompleted++;
                                this.m_CollectionMessage = "";
                            }
                        }
                    }
                    else
                    {
                        this.m_ItemCount = 1;
                        this.m_ItemCompleted = 1;
                        this.Detection(DataConverter.CLng(this.ItemId));
                        if (string.IsNullOrEmpty(this.ErrorInfo))
                        {
                            this.StartCollection();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                this.m_ErrorInfo = exception.Message;
            }
            finally
            {
                if (this.CreateThread != null)
                {
                    this.CreateThread.Abort();
                }
            }
        }

        public string AdminName
        {
            get
            {
                return this.m_AdminName;
            }
            set
            {
                this.m_AdminName = value;
            }
        }

        public int CollectionCompleted
        {
            get
            {
                return this.m_CollectionCompleted;
            }
            set
            {
                this.m_CollectionCompleted = value;
            }
        }

        public int CollectionCount
        {
            get
            {
                return this.m_CollectionCount;
            }
            set
            {
                this.m_CollectionCount = value;
            }
        }

        public DateTime CollectionErrorTime
        {
            get
            {
                return this.m_CollectionErrorTime;
            }
            set
            {
                this.m_CollectionErrorTime = value;
            }
        }

        public string CollectionMessage
        {
            get
            {
                return this.m_CollectionMessage;
            }
            set
            {
                this.m_CollectionMessage = value;
            }
        }

        public int CollectionPhotoFailure
        {
            get
            {
                return this.m_CollectionPhotoFailure;
            }
            set
            {
                this.m_CollectionPhotoFailure = value;
            }
        }

        public int CollectionPhotoSuccess
        {
            get
            {
                return this.m_CollectionPhotoSuccess;
            }
            set
            {
                this.m_CollectionPhotoSuccess = value;
            }
        }

        public DateTime CollectionStartTime
        {
            get
            {
                return this.m_CollectionStartTime;
            }
            set
            {
                this.m_CollectionStartTime = value;
            }
        }

        public string CreateId
        {
            get
            {
                return this.m_CreateId;
            }
            set
            {
                this.m_CreateId = value;
            }
        }

        public Thread CreateThread
        {
            get
            {
                return this.m_CreateThread;
            }
        }

        public string ErrorInfo
        {
            get
            {
                return this.m_ErrorInfo;
            }
            set
            {
                this.m_ErrorInfo = value;
            }
        }

        public bool IsCreateHtml
        {
            get
            {
                return this.m_IsCreateHtml;
            }
            set
            {
                this.m_IsCreateHtml = value;
            }
        }

        public bool IsInput
        {
            get
            {
                return this.m_IsInput;
            }
            set
            {
                this.m_IsInput = value;
            }
        }

        public bool IsTitle
        {
            get
            {
                return this.m_IsTitle;
            }
            set
            {
                this.m_IsTitle = value;
            }
        }

        public int ItemCompleted
        {
            get
            {
                return this.m_ItemCompleted;
            }
            set
            {
                this.m_ItemCompleted = value;
            }
        }

        public int ItemCount
        {
            get
            {
                return this.m_ItemCount;
            }
            set
            {
                this.m_ItemCount = value;
            }
        }

        public string ItemId
        {
            get
            {
                return this.m_ItemId;
            }
            set
            {
                this.m_ItemId = value;
            }
        }

        public string PhysicalApplicationPath
        {
            get
            {
                return this.m_PhysicalApplicationPath;
            }
            set
            {
                this.m_PhysicalApplicationPath = value;
            }
        }

        public string UserName
        {
            get
            {
                return this.m_UserName;
            }
            set
            {
                this.m_UserName = value;
            }
        }
    }
}

