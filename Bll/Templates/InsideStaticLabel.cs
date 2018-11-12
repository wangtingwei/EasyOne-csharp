namespace EasyOne.Templates
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.DalFactory;
    using EasyOne.Enumerations;
    using EasyOne.IDal.Contents;
    using EasyOne.Model.CommonModel;
    using EasyOne.Model.Contents;
    using EasyOne.Model.TemplateProc;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Xml;

    public class InsideStaticLabel : IInsideStaticLabel
    {
        private int ranNum;

        public string ADdir()
        {
            return SiteOptionInfo.AdvertisementDir;
        }

        public string Banner()
        {
            return SiteInfo.BannerUrl;
        }

        public static string ConversionCode(string content)
        {
            string input = PathReplaceLable(content);
            StringBuilder builder = new StringBuilder();
            string pattern = @"\<\!\-\-Code\-\-\>([\s\S]*?)\<\!\-\-Code\-\-\>";
            MatchCollection matchs = Regex.Matches(input, pattern, RegexOptions.IgnoreCase);
            if (matchs.Count > 0)
            {
                foreach (Match match in matchs)
                {
                    if (builder.Length > 0)
                    {
                        builder.Append("[PE:Code]" + match.Value);
                    }
                    else
                    {
                        builder.Append(match.Value);
                    }
                    input = input.Replace(match.Value, "[PE:Code]");
                }
                foreach (string str3 in builder.ToString().Split(new string[] { "[PE:Code]" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    input = input.Replace("[PE:Code]", str3);
                }
            }
            return input;
        }

        public string Convert2Char(string a1, string a2)
        {
            StringBuilder builder = new StringBuilder();
            try
            {
                int num = Convert.ToInt32(a1);
                for (int i = 0; i < num; i++)
                {
                    builder.Append(a2);
                }
            }
            catch (FormatException)
            {
                builder = new StringBuilder("参数应该为数字");
            }
            return builder.ToString();
        }

        public int Convert2Int(string a1)
        {
            return DataConverter.CLng(a1);
        }

        public string Convert2JS(string a1)
        {
            StringBuilder builder = new StringBuilder(string.Empty);
            if (!string.IsNullOrEmpty(a1))
            {
                string[] strArray = a1.Replace(@"\", @"\\").Replace("/", @"\/").Replace("'", @"\'").Replace("\"", "\\\"").Split(new char[] { '\n' });
                for (int i = 0; i < strArray.Rank; i++)
                {
                    builder.Append("document.writeln(\"" + strArray.GetValue(i).ToString() + "\");\n");
                }
            }
            return builder.ToString();
        }

        public string ConvertAbsolutePath(string path)
        {
            return Utility.ConvertAbsolutePath(this.UpLoadDir(), path);
        }

        public string ConvertAbsolutePath(string virtualPath, string path)
        {
            return Utility.ConvertAbsolutePath(virtualPath, path);
        }

        public string ConverToWeek(string a1)
        {
            string str = string.Empty;
            if (string.IsNullOrEmpty(a1))
            {
                return str;
            }
            try
            {
                return Convert.ToDateTime(a1).DayOfWeek.ToString();
            }
            catch (InvalidCastException)
            {
                return "日期格式错误";
            }
        }

        public string ConvertSizeToShow(string fileSize)
        {
            decimal num = DataConverter.CDecimal(fileSize) * 1024M;
            decimal num2 = num / 1024M;
            if (num2 < 1M)
            {
                return "B";
            }
            if (num2 < 1024M)
            {
                return "KB";
            }
            decimal num3 = num2 / 1024M;
            if (num3 < 1M)
            {
                return "KB";
            }
            if (num3 >= 1024M)
            {
                num3 /= 1024M;
                return "GB";
            }
            return "MB";
        }

        public string ConvertSoftSize(string fileSize)
        {
            decimal num = DataConverter.CDecimal(fileSize) * 1024M;
            decimal num2 = num / 1024M;
            if (num2 < 1M)
            {
                return num.ToString("0.00");
            }
            if (num2 < 1024M)
            {
                return num2.ToString("0.00");
            }
            decimal num3 = num2 / 1024M;
            if (num3 < 1M)
            {
                return num2.ToString("0.00");
            }
            if (num3 >= 1024M)
            {
                num3 /= 1024M;
                return num3.ToString("0.00");
            }
            return num3.ToString("0.00");
        }

        public string Copyright()
        {
            return SiteInfo.Copyright;
        }

        public string CreateHtmlPath()
        {
            return VirtualPathUtility.AppendTrailingSlash(SiteOptionInfo.CreateHtmlPath);
        }

        public string CutText(string oringstr, string len, string substr)
        {
            oringstr = DataSecurity.PELabelDecode(oringstr);
            int length = DataConverter.CLng(len);
            oringstr = StringHelper.SubString(oringstr, length, substr);
            return DataSecurity.PELabelEncode(oringstr);
        }

        public string EnableComment(int id)
        {
            if (EasyOne.Contents.Nodes.GetCacheNodeById(DataConverter.CLng(id)).Settings.EnableComment)
            {
                return "true";
            }
            return "false";
        }

        public string EnableTouristsComment(int id)
        {
            if (EasyOne.Contents.Nodes.GetCacheNodeById(DataConverter.CLng(id)).Settings.EnableTouristsComment)
            {
                return "true";
            }
            return "false";
        }

        public string EncodeText(string a1, string a2)
        {
            string str = string.Empty;
            a1 = DataSecurity.PELabelDecode(a1);
            if (!string.IsNullOrEmpty(a2))
            {
                switch (a2)
                {
                    case "md5_16":
                        str = StringHelper.MD5(a1).Substring(8, 0x10);
                        break;

                    case "md5_32":
                        str = StringHelper.MD5(a1);
                        break;

                    case "enbase64":
                        str = StringHelper.Base64StringEncode(a1);
                        break;

                    case "debase64":
                        str = StringHelper.Base64StringDecode(a1);
                        break;

                    case "htmlencode":
                        str = HttpUtility.HtmlEncode(a1);
                        break;

                    case "htmldecode":
                        a1 = a1.Replace("&amp;", "&");
                        str = HttpUtility.HtmlDecode(a1);
                        break;

                    case "urlencode":
                        str = HttpUtility.UrlEncode(a1);
                        break;
                }
            }
            return DataSecurity.PELabelEncode(str);
        }

        private static string ex34850value(string s)
        {
            switch (s)
            {
                case "0":
                    return "Not defined";

                case "1":
                    return "手动";

                case "2":
                    return "自动";

                case "3":
                    return "Aperture priority";

                case "4":
                    return "Shutter priority";

                case "5":
                    return "Creative program (biased toward depth of field)";

                case "6":
                    return "Action program (biased toward fast shutter speed)";

                case "7":
                    return "Portrait mode (for closeup photos with the background out of focus)";

                case "8":
                    return " Landscape mode (for landscape photos with the background in focus)";
            }
            return "其他模式";
        }

        private static string ex37384value(string s)
        {
            switch (s)
            {
                case "0":
                    return "未知";

                case "1":
                    return "日光";

                case "2":
                    return "日光灯";

                case "3":
                    return "白炽灯";

                case "4":
                    return "闪光灯";

                case "9":
                    return "晴天";

                case "10":
                    return "多云";

                case "11":
                    return "阴天";

                case "12":
                    return "Daylight fluorescent (D 5700 - 7100K)";

                case "13":
                    return "Day white fluorescent (N 4600 - 5400K)";

                case "14":
                    return "Cool white fluorescent (W 3900 - 4500K)";

                case "15":
                    return "White fluorescent (WW 3200 - 3700K)";

                case "17":
                    return "Standard light A";

                case "18":
                    return "Standard light B";

                case "19":
                    return "Standard light C";

                case "20":
                    return "D55";

                case "21":
                    return "D65";

                case "22":
                    return "D75";

                case "23":
                    return "D50";

                case "24":
                    return "ISO studio tungsten";
            }
            return "其他照明设备";
        }

        public string FiltInsideLink(string a1)
        {
            return WordReplace.ReplaceInsideLink(a1);
        }

        public string FiltText(string a1)
        {
            return WordReplace.ReplaceText(a1);
        }

        public string FormatDate(string a1, string a2)
        {
            if (string.IsNullOrEmpty(a1))
            {
                a2 = "日期格式错误";
                return a2;
            }
            try
            {
                DateTime time = Convert.ToDateTime(a1);
                char paddingChar = Convert.ToChar("0");
                a2 = a2.Replace("yyyy", time.Year.ToString());
                a2 = a2.Replace("YYYY", this.Int2Chinese(time.Year.ToString()));
                a2 = a2.Replace("yy", time.Year.ToString().PadLeft(2));
                a2 = a2.Replace("YY", this.Int2Chinese(time.Year.ToString().PadLeft(2)));
                a2 = a2.Replace("mm", time.Month.ToString().PadLeft(2, paddingChar));
                a2 = a2.Replace("MM", this.Int2Chinese(time.Month.ToString()));
                a2 = a2.Replace("dd", time.Day.ToString().PadLeft(2, paddingChar));
                a2 = a2.Replace("DD", this.Int2Chinese(time.Day.ToString()));
                a2 = a2.Replace("hh", time.Hour.ToString().PadLeft(2, paddingChar));
                a2 = a2.Replace("HH", this.Int2Chinese(time.Hour.ToString()));
                a2 = a2.Replace("ff", time.Minute.ToString().PadLeft(2, paddingChar));
                a2 = a2.Replace("FF", this.Int2Chinese(time.Minute.ToString()));
                a2 = a2.Replace("ss", time.Second.ToString().PadLeft(2, paddingChar));
                a2 = a2.Replace("SS", this.Int2Chinese(time.Second.ToString()));
            }
            catch (FormatException)
            {
                a2 = "日期格式错误";
            }
            catch (InvalidCastException)
            {
                a2 = "日期格式错误";
            }
            return a2;
        }

        public string GetCustomContent(int num, string content)
        {
            string[] strArray = content.Split(new string[] { "ζ#123;#$$$#ζ#125;" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < strArray.Length; i++)
            {
                if ((i + 1) == num)
                {
                    return strArray[i];
                }
            }
            return "";
        }

        public string GetFieldList(int modelId, int generalId, string cssTable, string cssKey, string cssValue)
        {
            IList<FieldInfo> fieldList = Field.GetFieldList(modelId, false);
            DataTable contentDataById = ContentManage.GetContentDataById(generalId);
            StringBuilder builder = new StringBuilder("<table class='" + cssTable + "'>");
            int num = 0;
            if ((fieldList.Count > 0) && (contentDataById != null))
            {
                foreach (FieldInfo info in fieldList)
                {
                    if (info.FieldType != FieldType.Property)
                    {
                        builder.Append("<tr><td style='white-space:nowrap;' class='" + cssKey + "'><b>").Append(info.FieldAlias).Append("：</b></td>");
                        string str = "";
                        if (contentDataById.Rows[0][info.FieldName] != null)
                        {
                            str = contentDataById.Rows[0][info.FieldName].ToString();
                            if (!string.IsNullOrEmpty(str))
                            {
                                str = DataSecurity.PELabelEncode(str);
                            }
                        }
                        builder.Append("<td class='" + cssValue + "'>").Append(str).Append("</td></tr>");
                        num++;
                    }
                }
            }
            if (num == 0)
            {
                builder.Append("<tr><td>没有相关内容！</td></tr>");
            }
            builder.Append("</table>");
            return builder.ToString();
        }

        private static string GetFirstPhotoUrl(string originPath, string uploadfiledir, int ltype)
        {
            string urlStr = string.Empty;
            foreach (string str2 in originPath.Split(new string[] { "$$$" }, StringSplitOptions.RemoveEmptyEntries))
            {
                string[] strArray2 = str2.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                if (!string.IsNullOrEmpty(strArray2[1]))
                {
                    if (ltype == 0)
                    {
                        urlStr = strArray2[1];
                    }
                    else
                    {
                        urlStr = SplitUrl(str2, uploadfiledir);
                    }
                    break;
                }
            }
            return DataSecurity.UrlEncode(urlStr);
        }

        public string GetGlobalResource(string classKey, string resourceKey)
        {
            string str = (string) HttpContext.GetGlobalResourceObject(classKey, resourceKey, CultureInfo.CurrentCulture);
            if (str == null)
            {
                str = string.Empty;
            }
            return str;
        }

        public string GetInfoPath(string id)
        {
            DataAccess.CreateNodes();
            IContentManage manage = DataAccess.CreateContentManage();
            int input = DataConverter.CLng(id, 0);
            if (input == 0)
            {
                return "GetInfoPath的参数id发生错误！";
            }
            CommonModelInfo commonModelInfoById = manage.GetCommonModelInfoById(DataConverter.CLng(input));
            NodeInfo cacheNodeById = EasyOne.Contents.Nodes.GetCacheNodeById(commonModelInfoById.NodeId);
            if (!cacheNodeById.IsCreateContentPage || (cacheNodeById.PurviewType > 0))
            {
                return (SiteConfig.SiteInfo.VirtualPath + "Item/" + id + ".aspx");
            }
            string str = SiteConfig.SiteInfo.VirtualPath + VirtualPathUtility.AppendTrailingSlash(SiteOptionInfo.CreateHtmlPath) + cacheNodeById.ContentPageHtmlRule;
            if (!string.IsNullOrEmpty(str))
            {
                str = str.ToLower().Replace("{$categorydir}", cacheNodeById.ParentDir + cacheNodeById.NodeDir).Replace("{$year}", commonModelInfoById.InputTime.Year.ToString("0000")).Replace("{$month}", commonModelInfoById.InputTime.Month.ToString("00")).Replace("{$day}", commonModelInfoById.InputTime.Day.ToString("00")).Replace("{$time}", commonModelInfoById.InputTime.Hour.ToString("00") + commonModelInfoById.InputTime.Minute.ToString("00") + commonModelInfoById.InputTime.Second.ToString("00")).Replace("{$infoid}", commonModelInfoById.GeneralId.ToString()).Replace("{$pinyinoftitle}", commonModelInfoById.PinyinTitle);
            }
            return str.Replace("//", "/");
        }

        public string GetInfoPath(string nodeId, string id, string inputTime, string pinyinTitle)
        {
            if ((string.IsNullOrEmpty(id) || string.IsNullOrEmpty(inputTime)) || string.IsNullOrEmpty(nodeId))
            {
                return "GetInfoPath的id和date参数不能为空！";
            }
            int num = DataConverter.CLng(id, 0);
            if (num == 0)
            {
                return "GetInfoPath的参数id发生错误！";
            }
            int num2 = DataConverter.CLng(nodeId, 0);
            DateTime? nullable = DataConverter.CDate(inputTime, null);
            if (!nullable.HasValue)
            {
                return "GetInfoPath的参数date发生错误！";
            }
            NodeInfo cacheNodeById = EasyOne.Contents.Nodes.GetCacheNodeById(num2);
            if (!cacheNodeById.IsCreateContentPage || (cacheNodeById.PurviewType > 0))
            {
                return string.Concat(new object[] { SiteConfig.SiteInfo.VirtualPath, "Item/", num, ".aspx" });
            }
            string str = SiteConfig.SiteInfo.VirtualPath + VirtualPathUtility.AppendTrailingSlash(SiteOptionInfo.CreateHtmlPath) + cacheNodeById.ContentPageHtmlRule;
            if (!string.IsNullOrEmpty(str))
            {
                str = str.ToLower().Replace("{$categorydir}", cacheNodeById.ParentDir + cacheNodeById.NodeDir).Replace("{$year}", nullable.Value.Year.ToString("0000")).Replace("{$month}", nullable.Value.Month.ToString("00")).Replace("{$day}", nullable.Value.Day.ToString("00")).Replace("{$time}", nullable.Value.Hour.ToString("00") + nullable.Value.Minute.ToString("00") + nullable.Value.Second.ToString("00")).Replace("{$infoid}", id.ToString()).Replace("{$pinyinoftitle}", pinyinTitle);
            }
            return str.Replace("//", "/");
        }

        public string GetLinkInfoPic(string infoPicPath, string title, string upLoadDir, string infoPath, int imgwidth, int imgheight)
        {
            string str = string.Empty;
            StringBuilder builder = new StringBuilder("");
            if (string.IsNullOrEmpty(infoPicPath))
            {
                builder.Append("<img src=\"");
                builder.Append(upLoadDir);
                builder.Append("nopic.gif\"");
                if (imgwidth > 0)
                {
                    builder.Append("width=\"" + imgwidth.ToString() + "\"");
                }
                if (imgheight > 0)
                {
                    builder.Append("height=\"" + imgheight.ToString() + "\"");
                }
                builder.Append(" alt=\"" + title + "\"");
                builder.Append("border=\"0\"");
                builder.Append(" />");
            }
            else
            {
                builder.Append("<a href=\"");
                builder.Append(infoPath);
                builder.Append("\">");
                if (infoPicPath.Contains("."))
                {
                    str = Path.GetExtension(infoPicPath).ToLower();
                }
                else
                {
                    str = "";
                }
                switch (str)
                {
                    case ".swf":
                        builder.Append("<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=5,0,0,0\" ");
                        if (imgwidth > 0)
                        {
                            builder.Append(" width=\"" + imgwidth.ToString() + "\"");
                        }
                        if (imgheight > 0)
                        {
                            builder.Append(" height=\"" + imgheight.ToString() + "\"");
                        }
                        builder.Append("><param name=\"movie\" value=\"");
                        builder.Append(infoPicPath);
                        builder.Append("\"><param name=\"quality\" value=\"high\"><embed src=\"");
                        builder.Append(infoPicPath);
                        builder.Append("\" pluginspage=\"http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash\" type=\"application/x-shockwave-flash\" ");
                        if (imgwidth > 0)
                        {
                            builder.Append(" width=\"" + imgwidth.ToString() + "\"");
                        }
                        if (imgheight > 0)
                        {
                            builder.Append(" height=\"" + imgheight.ToString() + "\"");
                        }
                        builder.Append("></embed></object>");
                        break;

                    case ".gif":
                    case ".jpg":
                    case ".jpeg":
                    case ".jpe":
                    case ".bmp":
                    case ".png":
                        builder.Append("<img class=\"pic2\" src=\"");
                        builder.Append(infoPicPath);
                        builder.Append("\" ");
                        if (imgwidth > 0)
                        {
                            builder.Append(" width=\"" + imgwidth.ToString() + "\"");
                        }
                        if (imgheight > 0)
                        {
                            builder.Append(" height=\"" + imgheight.ToString() + "\"");
                        }
                        builder.Append(" alt=\"" + title + "\"");
                        builder.Append(" border=\"0\" />");
                        break;

                    default:
                        builder.Append("<img class=\"pic2\" src=\"");
                        builder.Append(infoPicPath);
                        builder.Append("\" ");
                        if (imgwidth > 0)
                        {
                            builder.Append(" width=\"" + imgwidth.ToString() + "\"");
                        }
                        if (imgheight > 0)
                        {
                            builder.Append(" height=\"" + imgheight.ToString() + "\"");
                        }
                        builder.Append(" alt=\"" + title + "\"");
                        builder.Append(" border=\"0\" />");
                        break;
                }
                builder.Append("</a>");
            }
            return builder.ToString();
        }

        public string GetModelItemName(int id)
        {
            return ModelManager.GetCacheModelById(id).ItemName;
        }

        public string GetModelName(string tableName)
        {
            return ModelManager.GetCacheModelByTableName(tableName).ModelName;
        }

        public string GetNode(string id, string meth)
        {
            if (string.IsNullOrEmpty(id))
            {
                return string.Empty;
            }
            NodeInfo cacheNodeById = EasyOne.Contents.Nodes.GetCacheNodeById(DataConverter.CLng(id, 0));
            if (string.Compare(meth, "dir", StringComparison.OrdinalIgnoreCase) == 0)
            {
                return cacheNodeById.NodeDir;
            }
            if (string.Compare(meth, "aspxname", StringComparison.OrdinalIgnoreCase) == 0)
            {
                return cacheNodeById.ItemAspxFileName;
            }
            return cacheNodeById.NodeName;
        }

        public string GetNodeCommentNeedCheck(int id)
        {
            if (EasyOne.Contents.Nodes.GetCacheNodeById(DataConverter.CLng(id)).Settings.CommentNeedCheck)
            {
                return "true";
            }
            return "false";
        }

        public string GetNodeEnableComment(int id)
        {
            if (EasyOne.Contents.Nodes.GetCacheNodeById(DataConverter.CLng(id)).Settings.EnableComment)
            {
                return "true";
            }
            return "false";
        }

        public string GetNodeEnableProtect(int id)
        {
            NodeInfo cacheNodeById = EasyOne.Contents.Nodes.GetCacheNodeById(DataConverter.CLng(id));
            StringBuilder builder = new StringBuilder("");
            if (cacheNodeById.Settings.EnableProtect)
            {
                builder.Append(" oncontextmenu='return false'");
                builder.Append(" ondragstart='return false'");
                builder.Append(" onselectstart ='return false'");
                builder.Append(" onselect='document.selection.empty()'");
                builder.Append(" oncopy='document.selection.empty()'");
                builder.Append(" onbeforecopy='return false'");
            }
            return builder.ToString();
        }

        public string GetNodeFieldName(string id, string fieldName)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(fieldName))
            {
                return string.Empty;
            }
            NodeInfo cacheNodeById = EasyOne.Contents.Nodes.GetCacheNodeById(DataConverter.CLng(id));
            Type type = typeof(NodeInfo);
            return type.GetProperty(fieldName).GetValue(cacheNodeById, null).ToString();
        }

        public string GetNodePath(string type, string id)
        {
            NodeInfo cacheNodeById = EasyOne.Contents.Nodes.GetCacheNodeById(DataConverter.CLng(id));
            if (cacheNodeById.IsCreateListPage && (cacheNodeById.PurviewType == 0))
            {
                if (DataConverter.CBoolean(type))
                {
                    return (SiteConfig.SiteInfo.VirtualPath + VirtualPathUtility.AppendTrailingSlash(SiteOptionInfo.CreateHtmlPath) + cacheNodeById.ListHtmlPageName("1")).Replace("//", "/");
                }
                return (SiteConfig.SiteInfo.VirtualPath + VirtualPathUtility.AppendTrailingSlash(SiteOptionInfo.CreateHtmlPath) + cacheNodeById.ListHtmlPageName("0")).Replace("//", "/");
            }
            if (DataConverter.CBoolean(type))
            {
                return (SiteConfig.SiteInfo.VirtualPath + "Category_" + id + "/index_1.aspx");
            }
            return (SiteConfig.SiteInfo.VirtualPath + "Category_" + id + "/index.aspx");
        }

        private static string GetPathArray(string originPath, string uploadfiledir, int ltype)
        {
            StringBuilder builder = new StringBuilder();
            int num = 0;
            builder.Append("<script language='javascript'>\n");
            builder.Append("var arrUrlName=new Array();\n");
            builder.Append("var arrUrl=new Array();\n");
            foreach (string str in originPath.Split(new string[] { "$$$" }, StringSplitOptions.RemoveEmptyEntries))
            {
                string[] strArray2 = str.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                builder.Append("arrUrlName[");
                builder.Append(num);
                builder.Append("]='");
                builder.Append(strArray2[0]);
                builder.Append("';\n");
                builder.Append("arrUrl[");
                builder.Append(num);
                builder.Append("]='");
                if (ltype == 0)
                {
                    builder.Append(DataSecurity.UrlEncode(strArray2[1]));
                }
                else
                {
                    builder.Append(SplitUrl(str, uploadfiledir));
                }
                builder.Append("';\n");
                num++;
            }
            builder.Append("</script>\n");
            return builder.ToString();
        }

        public string GetPhotoPathList(int showType, int imgWidth, int imgHeight, int cols, int maxPerPage, string originPath, string uploadfiledir, int ltype)
        {
            StringBuilder builder = new StringBuilder();
            if (string.IsNullOrEmpty(originPath))
            {
                return string.Empty;
            }
            if (cols < 1)
            {
                cols = 1;
            }
            if (maxPerPage < 1)
            {
                maxPerPage = 1;
            }
            string str = string.Empty;
            if (ltype == 0)
            {
                str = GetPathArray(originPath, string.Empty, 0);
            }
            else
            {
                str = GetPathArray(originPath, uploadfiledir, 1);
            }
            builder.Append(str);
            builder.Append("<div id='PhotoUrlList'>");
            builder.Append("<script language='javascript'>\n");
            if (showType == 0)
            {
                builder.Append("for(var i=0;i<arrUrl.length;i++){\n");
                builder.Append("  document.write(\"<a href='#Title' onclick=ViewPhoto('\"+arrUrl[i]+\"')>\"+arrUrlName[i]+\"</a>&nbsp;&nbsp;\");\n");
                builder.Append("  if((i+1)%" + cols + "==0&&(i+1<arrUrl.length)){document.write('<br />');}\n");
                builder.Append("}\n");
            }
            else
            {
                string str2 = "";
                if (imgWidth > 0)
                {
                    object obj2 = str2;
                    str2 = string.Concat(new object[] { obj2, " width='", imgWidth, "'" });
                }
                if (imgHeight > 0)
                {
                    object obj3 = str2;
                    str2 = string.Concat(new object[] { obj3, " heigth='", imgHeight, "'" });
                }
                builder.Append("function ShowUrlList(page){\n");
                builder.Append("  if(arrUrl.length<=1) return '';\n");
                builder.Append("  var dTotalPage=arrUrl.length/" + maxPerPage + ";\n");
                builder.Append("  var TotalPage;\n");
                builder.Append("  var MaxPerPage=" + maxPerPage + ";\n");
                builder.Append("  if(arrUrl.length%MaxPerPage==0){TotalPage=Math.floor(dTotalPage);}else{TotalPage=Math.floor(dTotalPage)+1;}\n");
                builder.Append("  if(page<1) page=1;\n");
                builder.Append("  if(page>TotalPage) page=TotalPage;\n");
                builder.Append("  var strPage='<table border=0 id=\"photoUrl\"><tr>';\n");
                builder.Append("  for(var i=(page-1)*MaxPerPage;i<arrUrl.length&&i<page*MaxPerPage;i++){\n");
                builder.Append("    strPage+=\"<td><a href='#Title' onclick=ViewPhoto('\"+arrUrl[i]+\"')><img src='\"+arrUrl[i]+\"' border='0' " + str2 + "></a></td>\";\n");
                builder.Append("    if((i+1)%" + cols + "==0&&i+1<MaxPerPage){strPage+='</tr><tr>';}\n");
                builder.Append("  }\n");
                builder.Append("  strPage+=\"</tr></table>\";\n");
                builder.Append("  if(TotalPage>1){strPage+=\"<table><tr><td><a href='javascript:ShowUrlList(1)'>首页</a> <a href='javascript:ShowUrlList(\"+(page-1)+\")'>上一页</a> <a href='javascript:ShowUrlList(\"+(page+1)+\")'>下一页</a> <a href='javascript:ShowUrlList(\"+TotalPage+\")'>尾页</a></td></tr></table>\";}\n");
                builder.Append("  document.getElementById('PhotoUrlList').innerHTML=strPage;\n");
                builder.Append("}\n");
                builder.Append("ShowUrlList(1);\n");
            }
            builder.Append("</script>\n</div>");
            return builder.ToString();
        }

        public void GetRanNum()
        {
            this.ranNum = new Random().Next(0x270f);
        }

        public string GetSlidePic(int imgwidth, int imgheight, int titlelen, int timeout, int effectid, int picposition, int piclast, string imgPath, string linkPath, string title)
        {
            if ((imgwidth < 0) || (imgwidth > 0x3e8))
            {
                imgwidth = 150;
            }
            if ((imgheight < 0) || (imgheight > 0x3e8))
            {
                imgheight = 150;
            }
            if ((timeout < 0x3e8) || (timeout > 0x186a0))
            {
                timeout = 0x1388;
            }
            if ((effectid < 0) || (effectid > 0x17))
            {
                effectid = 0x17;
            }
            StringBuilder builder = new StringBuilder("");
            if (picposition == 1)
            {
                builder.Append("<script language='JavaScript'>\n");
                builder.Append("<!--\n");
                builder.Append(string.Concat(new object[] { "var SlidePic_", this.ranNum, " = new SlidePic_Info(\"SlidePic_", this.ranNum, "\");\n" }));
                builder.Append(string.Concat(new object[] { "SlidePic_", this.ranNum, ".Width    = ", imgwidth, ";\n" }));
                builder.Append(string.Concat(new object[] { "SlidePic_", this.ranNum, ".Height   = ", imgheight, ";\n" }));
                builder.Append(string.Concat(new object[] { "SlidePic_", this.ranNum, ".TimeOut  = ", timeout, ";\n" }));
                builder.Append(string.Concat(new object[] { "SlidePic_", this.ranNum, ".Effect   = ", effectid, ";\n" }));
                builder.Append(string.Concat(new object[] { "SlidePic_", this.ranNum, ".TitleLen = ", titlelen, ";\n" }));
            }
            builder.Append("var oSP = new objSP_Info();\n");
            builder.Append("oSP.ImgUrl         = \"" + imgPath + "\";\n");
            builder.Append("oSP.LinkUrl         = \"" + linkPath + "\";\n");
            builder.Append("oSP.Title         = \"" + title + "\";\n");
            builder.Append("SlidePic_" + this.ranNum + ".Add(oSP);\n");
            if (picposition == piclast)
            {
                builder.Append("SlidePic_" + this.ranNum + ".Show();\n");
                builder.Append("//-->\n");
                builder.Append("</script>\n");
            }
            return builder.ToString();
        }

        public string GetSpecial(string id, string meth)
        {
            if (string.IsNullOrEmpty(id))
            {
                return string.Empty;
            }
            ISpecial special = DataAccess.CreateSpecial();
            if (string.Compare(meth, "dir", StringComparison.OrdinalIgnoreCase) == 0)
            {
                return special.GetSpecialInfoById(DataConverter.CLng(id)).SpecialDir;
            }
            if (string.Compare(meth, "categoryid", StringComparison.OrdinalIgnoreCase) == 0)
            {
                return special.GetSpecialInfoById(DataConverter.CLng(id)).SpecialCategoryId.ToString();
            }
            return special.GetSpecialInfoById(DataConverter.CLng(id)).SpecialName;
        }

        public string GetSpecialCategoryPath(string id)
        {
            SpecialCategoryInfo specialCategoryInfoById = DataAccess.CreateSpecial().GetSpecialCategoryInfoById(DataConverter.CLng(id));
            if (!specialCategoryInfoById.IsCreateHtml)
            {
                return (SiteConfig.SiteInfo.VirtualPath + "Specialcategory_" + id + "/index.aspx");
            }
            return (SiteConfig.SiteInfo.VirtualPath + VirtualPathUtility.AppendTrailingSlash(SiteOptionInfo.CreateHtmlPath) + specialCategoryInfoById.CategoryHtmlPageName).Replace("//", "/");
        }

        public string GetSpecialPath(string id)
        {
            SpecialInfo specialInfoById = DataAccess.CreateSpecial().GetSpecialInfoById(DataConverter.CLng(id));
            if (!specialInfoById.IsCreateListPage)
            {
                return (SiteConfig.SiteInfo.VirtualPath + "Special_" + id + "/index.aspx");
            }
            return (SiteConfig.SiteInfo.VirtualPath + VirtualPathUtility.AppendTrailingSlash(SiteOptionInfo.CreateHtmlPath) + specialInfoById.ListHtmlPagePath("")).Replace("//", "/");
        }

        public string GetUserFace(string uname)
        {
            if (Users.Exists(uname))
            {
                UserInfo usersByUserName = Users.GetUsersByUserName(uname);
                if (!usersByUserName.IsNull && !string.IsNullOrEmpty(usersByUserName.UserFace))
                {
                    return usersByUserName.UserFace;
                }
            }
            return "err";
        }

        private static string GetValueOfType1(byte[] b)
        {
            return Encoding.ASCII.GetString(b);
        }

        private static string GetValueOfType10(byte[] b)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < b.Length; i++)
            {
                builder.Append(((char) b[i]).ToString());
            }
            return builder.ToString();
        }

        private static string GetValueOfType3(byte[] b)
        {
            if (b.Length != 2)
            {
                return string.Empty;
            }
            return Convert.ToUInt16((int) ((b[1] << 8) | b[0])).ToString();
        }

        private static string GetValueOfType4(byte[] b)
        {
            if (b.Length != 4)
            {
                return string.Empty;
            }
            return Convert.ToUInt32((int) ((((b[3] << 0x18) | (b[2] << 0x10)) | (b[1] << 8)) | b[0])).ToString();
        }

        private static string GetValueOfType5(byte[] b)
        {
            if (b.Length != 8)
            {
                return string.Empty;
            }
            uint num2 = 0;
            num2 = Convert.ToUInt32((int) ((((b[7] << 0x18) | (b[6] << 0x10)) | (b[5] << 8)) | b[4]));
            return Math.Round((double) (((float) Convert.ToUInt32((int) ((((b[3] << 0x18) | (b[2] << 0x10)) | (b[1] << 8)) | b[0]))) / ((float) num2)), 2).ToString();
        }

        private static string GetValueOfType7A(byte[] b)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < b.Length; i++)
            {
                builder.Append(((char) b[i]).ToString());
            }
            return builder.ToString();
        }

        private static string GetValueOfType7B(byte[] b)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < b.Length; i++)
            {
                builder.Append(b[i].ToString());
            }
            return builder.ToString();
        }

        public string GetVoteForm(int id)
        {
            if (id < 0)
            {
                return string.Empty;
            }
            return Votes.GetFormByGeneralId(id);
        }

        public string GetVoteNum(string ostr)
        {
            if (string.IsNullOrEmpty(ostr))
            {
                return string.Empty;
            }
            XmlDocument document = new XmlDocument();
            try
            {
                ostr = DataSecurity.HtmlDecode(ostr);
                document.LoadXml(ostr);
            }
            catch (XmlException exception)
            {
                return exception.Message;
            }
            int num = 0;
            foreach (XmlNode node in document.DocumentElement.ChildNodes)
            {
                XmlElement element = (XmlElement) node;
                if (!string.IsNullOrEmpty(element.GetAttribute("Title")))
                {
                    num += Convert.ToInt32(element.GetAttribute("VoteNumber"));
                }
            }
            return num.ToString();
        }

        public int HitsOfHot()
        {
            return SiteOptionInfo.HitsOfHot;
        }

        public string InstallDir()
        {
            return SiteInfo.VirtualPath;
        }

        public string Int2Chinese(string a1)
        {
            string str = string.Empty;
            if (!string.IsNullOrEmpty(a1))
            {
                for (int i = 0; i < a1.Length; i++)
                {
                    char ch = a1[i];
                    switch (ch.ToString())
                    {
                        case "0":
                            str = str + "零";
                            break;

                        case "1":
                            str = str + "一";
                            break;

                        case "2":
                            str = str + "二";
                            break;

                        case "3":
                            str = str + "三";
                            break;

                        case "4":
                            str = str + "四";
                            break;

                        case "5":
                            str = str + "五";
                            break;

                        case "6":
                            str = str + "六";
                            break;

                        case "7":
                            str = str + "七";
                            break;

                        case "8":
                            str = str + "八";
                            break;

                        case "9":
                            str = str + "九";
                            break;

                        case ".":
                            str = str + "点";
                            break;

                        default:
                            str = str + a1[i].ToString();
                            break;
                    }
                }
            }
            return str;
        }

        public string Int2CMoney(string numstr)
        {
            decimal num = 0M;
            try
            {
                num = Convert.ToDecimal(numstr);
            }
            catch (InvalidCastException)
            {
                return "参数应该为数字";
            }
            StringBuilder builder = new StringBuilder();
            if (num == 0M)
            {
                builder = new StringBuilder("零元整");
            }
            else
            {
                string str = "零壹贰叁肆伍陆柒捌玖";
                string str2 = "万仟佰拾亿仟佰拾万仟佰拾元角分";
                string str3 = string.Empty;
                string str4 = string.Empty;
                string str5 = string.Empty;
                string str6 = string.Empty;
                int num4 = 0;
                str4 = ((long) (Math.Round(Math.Abs(num), 2) * 100M)).ToString();
                int length = str4.Length;
                if (length > 15)
                {
                    return "溢出";
                }
                str2 = str2.Substring(15 - length);
                for (int i = 0; i < length; i++)
                {
                    str3 = str4.Substring(i, 1);
                    int startIndex = Convert.ToInt32(str3);
                    if (((i != (length - 3)) && (i != (length - 7))) && ((i != (length - 11)) && (i != (length - 15))))
                    {
                        if (str3 == "0")
                        {
                            str5 = string.Empty;
                            str6 = string.Empty;
                            num4++;
                        }
                        else if ((str3 != "0") && (num4 != 0))
                        {
                            str5 = "零" + str.Substring(startIndex, 1);
                            str6 = str2.Substring(i, 1);
                            num4 = 0;
                        }
                        else
                        {
                            str5 = str.Substring(startIndex, 1);
                            str6 = str2.Substring(i, 1);
                            num4 = 0;
                        }
                    }
                    else if ((str3 != "0") && (num4 != 0))
                    {
                        str5 = "零" + str.Substring(startIndex, 1);
                        str6 = str2.Substring(i, 1);
                        num4 = 0;
                    }
                    else if ((str3 != "0") && (num4 == 0))
                    {
                        str5 = str.Substring(startIndex, 1);
                        str6 = str2.Substring(i, 1);
                        num4 = 0;
                    }
                    else if ((str3 == "0") && (num4 >= 3))
                    {
                        str5 = string.Empty;
                        str6 = string.Empty;
                        num4++;
                    }
                    else if (length >= 11)
                    {
                        str5 = string.Empty;
                        num4++;
                    }
                    else
                    {
                        str5 = string.Empty;
                        str6 = str2.Substring(i, 1);
                        num4++;
                    }
                    if ((i == (length - 11)) || (i == (length - 3)))
                    {
                        str6 = str2.Substring(i, 1);
                    }
                    builder.Append(str5 + str6);
                    if ((i == (length - 1)) && (str3 == "0"))
                    {
                        builder.Append("整");
                    }
                }
            }
            return builder.ToString();
        }

        public string IsAdminLogined()
        {
            string str = "false";
            if ((HttpContext.Current != null) && PEContext.Current.Admin.Identity.IsAuthenticated)
            {
                str = "true";
            }
            return str;
        }

        public string IsLogined()
        {
            string str = "false";
            if ((HttpContext.Current != null) && PEContext.Current.User.Identity.IsAuthenticated)
            {
                str = "true";
            }
            return str;
        }

        public string IsShop(string tableName)
        {
            string str = "false";
            if (ModelManager.GetCacheModelByTableName(tableName).IsEshop)
            {
                str = "true";
            }
            return str;
        }

        public string IsStartWithhttp(string urlvalue)
        {
            string str = "false";
            if (string.IsNullOrEmpty(urlvalue))
            {
                return str;
            }
            if (urlvalue.Contains("://") || urlvalue.StartsWith("/", StringComparison.Ordinal))
            {
                return "true";
            }
            return "false";
        }

        public string JSSlidePic()
        {
            StringBuilder builder = new StringBuilder("");
            builder.Append("<script language=\"JavaScript\">\n");
            builder.Append("<!--\n");
            builder.Append("function objSP_Info() {this.ImgUrl=\"\"; this.LinkUrl=\"\"; this.Title=\"\";}\n");
            builder.Append("function SlidePic_Info(_id) {this.ID=_id; this.Width=0;this.Height=0; this.TimeOut=5000; this.Effect=23; this.TitleLen=0; this.PicNum=-1; this.Img=null; this.Url=null; this.Title=null; this.AllPic=new Array(); this.Add=SlidePic_Info_Add; this.Show=SlidePic_Info_Show; this.LoopShow=SlidePic_Info_LoopShow;}\n");
            builder.Append("function SlidePic_Info_Add(_SP) {this.AllPic[this.AllPic.length] = _SP;}\n");
            builder.Append("function SlidePic_Info_Show() {\n");
            builder.Append("  if(this.AllPic[0] == null) return false;\n");
            builder.Append("  document.write(\"<div align='center'><a id='Url_\" + this.ID + \"' href=''><img id='Img_\" + this.ID + \"' style='width:\" + this.Width + \"px; height:\" + this.Height + \"px; filter: revealTrans(duration=2,transition=23);' src='javascript:null' border='0'></a>\");\n");
            builder.Append("  if(this.TitleLen != 0) {document.write(\"<br><span id='Title_\" + this.ID + \"'></span></div>\");}\n");
            builder.Append("  else{document.write(\"</div>\");}\n");
            builder.Append("  this.Img = document.getElementById(\"Img_\" + this.ID);\n");
            builder.Append("  this.Url = document.getElementById(\"Url_\" + this.ID);\n");
            builder.Append("  this.Title = document.getElementById(\"Title_\" + this.ID);\n");
            builder.Append("  this.LoopShow();\n");
            builder.Append("}\n");
            builder.Append("function SlidePic_Info_LoopShow() {\n");
            builder.Append("  if(document.all) {\n");
            builder.Append("    if(this.PicNum<this.AllPic.length-1) this.PicNum++ ; \n");
            builder.Append("    else this.PicNum=0; \n");
            builder.Append("    this.Img.filters.revealTrans.Transition=this.Effect; \n");
            builder.Append("    this.Img.filters.revealTrans.apply(); \n");
            builder.Append("    this.Img.src=this.AllPic[this.PicNum].ImgUrl;\n");
            builder.Append("    this.Img.filters.revealTrans.play();\n");
            builder.Append("    this.Url.href=this.AllPic[this.PicNum].LinkUrl;\n");
            builder.Append("    if(this.Title) this.Title.innerHTML=\"<a href=\"+this.AllPic[this.PicNum].LinkUrl+\" target=_blank>\"+this.AllPic[this.PicNum].Title+\"</a>\";\n");
            builder.Append("    this.Img.timer=setTimeout(this.ID+\".LoopShow()\",this.TimeOut);\n");
            builder.Append("  } else {\n");
            builder.Append("    if(this.PicNum<this.AllPic.length-1) this.PicNum++ ; \n");
            builder.Append("    else this.PicNum=0; \n");
            builder.Append("    this.Img.src=this.AllPic[this.PicNum].ImgUrl;\n");
            builder.Append("    this.Url.href=this.AllPic[this.PicNum].LinkUrl;\n");
            builder.Append("    if(this.Title) this.Title.innerHTML=\"<a href=\"+this.AllPic[this.PicNum].LinkUrl+\" target=_blank>\"+this.AllPic[this.PicNum].Title+\"</a>\";\n");
            builder.Append("    this.Img.timer=setTimeout(this.ID+\".LoopShow()\",this.TimeOut);\n");
            builder.Append("  }\n");
            builder.Append("}\n");
            builder.Append("//-->\n");
            builder.Append("</script>\n");
            return builder.ToString();
        }

        public string LoginedUserEmail()
        {
            string email = "";
            if ((HttpContext.Current != null) && !string.IsNullOrEmpty(PEContext.Current.User.UserInfo.Email))
            {
                email = PEContext.Current.User.UserInfo.Email;
            }
            return email;
        }

        public string LoginedUserExp(string qstar)
        {
            string userTrueName = "";
            if (HttpContext.Current != null)
            {
                switch (qstar.ToLower())
                {
                    case "balane":
                        return PEContext.Current.User.UserInfo.Balance.ToString();

                    case "consumeexp":
                        return PEContext.Current.User.UserInfo.ConsumeExp.ToString();

                    case "consumemoney":
                        return PEContext.Current.User.UserInfo.ConsumeMoney.ToString();

                    case "consumepoint":
                        return PEContext.Current.User.UserInfo.ConsumePoint.ToString();

                    case "endtime":
                        return PEContext.Current.User.UserInfo.EndTime.ToString();

                    case "passeditems":
                        return PEContext.Current.User.UserInfo.PassedItems.ToString();

                    case "postitems":
                        return PEContext.Current.User.UserInfo.PostItems.ToString();

                    case "userexp":
                        return PEContext.Current.User.UserInfo.UserExp.ToString();

                    case "userpoint":
                        return PEContext.Current.User.UserInfo.UserPoint.ToString();

                    case "usertruename":
                        if (!string.IsNullOrEmpty(PEContext.Current.User.UserInfo.UserTrueName))
                        {
                            userTrueName = PEContext.Current.User.UserInfo.UserTrueName;
                        }
                        return userTrueName;

                    case "usertype":
                        return PEContext.Current.User.UserInfo.UserType.ToString();
                }
            }
            return userTrueName;
        }

        public string LoginedUserName()
        {
            string userName = "";
            if ((HttpContext.Current != null) && !string.IsNullOrEmpty(PEContext.Current.User.UserName))
            {
                userName = PEContext.Current.User.UserName;
            }
            return userName;
        }

        public string LoginedUserSet(string qstar)
        {
            string groupName = "";
            if (HttpContext.Current != null)
            {
                switch (qstar.ToLower())
                {
                    case "delitems":
                        return PEContext.Current.User.UserInfo.DelItems.ToString();

                    case "faceheight":
                        return PEContext.Current.User.UserInfo.FaceHeight.ToString();

                    case "facewidth":
                        return PEContext.Current.User.UserInfo.FaceWidth.ToString();

                    case "groupid":
                        return PEContext.Current.User.UserInfo.GroupId.ToString();

                    case "groupname":
                        if (!string.IsNullOrEmpty(PEContext.Current.User.UserInfo.GroupName))
                        {
                            groupName = PEContext.Current.User.UserInfo.GroupName;
                        }
                        return groupName;

                    case "lastloginip":
                        if (!string.IsNullOrEmpty(PEContext.Current.User.UserInfo.LastLogOnIP))
                        {
                            groupName = PEContext.Current.User.UserInfo.LastLogOnIP;
                        }
                        return groupName;

                    case "lastlogintime":
                        return PEContext.Current.User.UserInfo.LastLogOnTime.ToString();

                    case "logintimes":
                        return PEContext.Current.User.UserInfo.LogOnTimes.ToString();

                    case "regtime":
                        return PEContext.Current.User.UserInfo.RegTime.ToString();

                    case "sign":
                        if (!string.IsNullOrEmpty(PEContext.Current.User.UserInfo.Sign))
                        {
                            groupName = PEContext.Current.User.UserInfo.Sign;
                        }
                        return groupName;

                    case "status":
                        return PEContext.Current.User.UserInfo.Status.ToString();

                    case "userface":
                        if (!string.IsNullOrEmpty(PEContext.Current.User.UserInfo.UserFace))
                        {
                            groupName = PEContext.Current.User.UserInfo.UserFace;
                        }
                        return groupName;

                    case "userfriendgropu":
                        if (!string.IsNullOrEmpty(PEContext.Current.User.UserInfo.UserFriendGroup))
                        {
                            groupName = PEContext.Current.User.UserInfo.UserFriendGroup;
                        }
                        return groupName;

                    case "usersetting":
                        if (!string.IsNullOrEmpty(PEContext.Current.User.UserInfo.UserSetting))
                        {
                            groupName = PEContext.Current.User.UserInfo.GroupName;
                        }
                        return groupName;
                }
            }
            return groupName;
        }

        public string Logo()
        {
            return SiteInfo.LogoUrl;
        }

        public string ManageDir()
        {
            return SiteOptionInfo.ManageDir;
        }

        public string MetaDescription()
        {
            return SiteInfo.MetaDescription;
        }

        public string MetaKeywords()
        {
            return SiteInfo.MetaKeywords;
        }

        public static string PathReplaceLable(string content)
        {
            string input = content;
            string pattern = @"{PE\.SiteConfig\.ApplicationPath\/}";
            foreach (Match match in Regex.Matches(input, pattern, RegexOptions.IgnoreCase))
            {
                input = input.Replace(match.Value, SiteConfig.SiteInfo.VirtualPath);
            }
            pattern = @"{PE\.SiteConfig\.uploaddir\/}";
            foreach (Match match2 in Regex.Matches(input, pattern, RegexOptions.IgnoreCase))
            {
                input = input.Replace(match2.Value, SiteConfig.SiteOption.UploadDir);
            }
            return input;
        }

        public string ReadExif(string stext)
        {
            string str;
            StringBuilder builder = new StringBuilder();
            if (HttpContext.Current != null)
            {
                str = HttpContext.Current.Server.MapPath(stext);
            }
            else
            {
                str = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, stext);
            }
            if (File.Exists(str))
            {
                try
                {
                    foreach (PropertyItem item in Image.FromFile(str).PropertyItems)
                    {
                        switch (item.Id)
                        {
                            case 0x131:
                                builder.Append("<div>创建软件：" + value2str(item) + "</div>");
                                break;

                            case 0x132:
                                builder.Append("<div>拍摄时间：" + value2str(item) + "</div>");
                                break;

                            case 0x829a:
                                builder.Append("<div>暴光时间：" + value2str(item) + "秒</div>");
                                break;

                            case 0x829d:
                                builder.Append("<div>光圈大小：F/" + value2str(item) + "</div>");
                                break;

                            case 0x10f:
                                builder.Append("<div>设备制造商：" + value2str(item) + "</div>");
                                break;

                            case 0x110:
                                builder.Append("<div>摄影机型号：" + value2str(item) + "</div>");
                                break;

                            case 0x11a:
                                builder.Append("<div>水平分辨率：" + value2str(item) + "dpi</div>");
                                break;

                            case 0x11b:
                                builder.Append("<div>垂直分辨率：" + value2str(item) + "dpi</div>");
                                break;

                            case 0x8822:
                                builder.Append("<div>暴光模式：" + ex34850value(value2str(item)) + "</div>");
                                break;

                            case 0x8827:
                                builder.Append("<div>ISO速度：" + value2str(item) + "</div>");
                                break;

                            case 0x9000:
                                builder.Append("<div>EXIF版本：" + value2str(item) + "</div>");
                                break;

                            case 0x9208:
                                builder.Append("<div>照明模式：" + ex37384value(value2str(item)) + "</div>");
                                break;

                            case 0x920a:
                                builder.Append("<div>焦距：" + value2str(item) + "MM</div>");
                                break;

                            case 0xa001:
                                if (value2str(item) == "1")
                                {
                                    builder.Append("<div>色彩空间：sRGB</div>");
                                }
                                else
                                {
                                    builder.Append("<div>色彩空间：" + value2str(item) + "</div>");
                                }
                                break;

                            case 0x9205:
                                builder.Append("<div>最大光圈：" + value2str(item) + "</div>");
                                break;
                        }
                    }
                }
                catch (OutOfMemoryException)
                {
                }
            }
            return xmlsafestr(builder.ToString());
        }

        public string ReadId3(string stext)
        {
            string str;
            string str2 = string.Empty;
            if (HttpContext.Current != null)
            {
                str = HttpContext.Current.Server.MapPath(stext);
            }
            else
            {
                str = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, stext);
            }
            if (File.Exists(str) && (string.Compare(Path.GetExtension(str).ToLower(), ".mp3", StringComparison.OrdinalIgnoreCase) == 0))
            {
                byte[] buffer = new byte[0x80];
                FileStream stream = new FileStream(str, FileMode.Open);
                stream.Seek(-128L, SeekOrigin.End);
                stream.Read(buffer, 0, 0x80);
                if (string.Compare(Encoding.Default.GetString(buffer, 0, 3), "TAG", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    str2 = ((((str2 + "<div>标　题：" + Encoding.Default.GetString(buffer, 3, 30) + "</div>") + "<div>艺术家：" + Encoding.Default.GetString(buffer, 0x21, 30) + "</div>") + "<div>专　辑：" + Encoding.Default.GetString(buffer, 0x3f, 30) + "</div>") + "<div>年　代：" + Encoding.Default.GetString(buffer, 0x5d, 4) + "</div>") + "<div>备　注：" + Encoding.Default.GetString(buffer, 0x61, 30) + "</div>";
                }
            }
            return xmlsafestr(str2);
        }

        public string ReadTxtFile(string filepath)
        {
            if (HttpContext.Current != null)
            {
                filepath = HttpContext.Current.Server.MapPath(filepath);
            }
            else
            {
                filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filepath);
            }
            if (File.Exists(filepath))
            {
                return File.ReadAllText(filepath);
            }
            return string.Empty;
        }

        public string RebudList(string name, string num)
        {
            StringBuilder builder = new StringBuilder();
            int num2 = DataConverter.CLng(num);
            for (int i = 0; i < num2; i++)
            {
                if (i > 0)
                {
                    builder.Append("+\"|\"+");
                }
                builder.Append(name + ((i + 1)).ToString());
            }
            if (num2 == 0)
            {
                builder.Append("\"\"");
            }
            return builder.ToString();
        }

        public string RemoveHtml(string a1)
        {
            if (!string.IsNullOrEmpty(a1))
            {
                a1 = a1.Replace("&amp;nbsp;", "&nbsp;");
                a1 = a1.Replace("&amp;gt;", "&gt;");
                a1 = a1.Replace("&amp;lt;", "&lt;");
                a1 = a1.Replace("&amp;#39;", "&#39;");
                a1 = a1.Replace("&amp;quot;", "&quot;");
                a1 = DataSecurity.HtmlDecode(a1);
                a1 = StringHelper.StripTags(a1);
                a1 = DataSecurity.PELabelEncode(a1);
            }
            return a1;
        }

        public string ReplaceText(string a1, string a2, string a3)
        {
            a1 = DataSecurity.PELabelDecode(a1);
            a2 = DataSecurity.PELabelDecode(a2);
            return DataSecurity.PELabelEncode(Regex.Replace(a1, Regex.Escape(a2), a3, RegexOptions.IgnoreCase));
        }

        public string RssEnable()
        {
            return SiteConfig.SiteOption.RssEnable.ToString();
        }

        public string ShowDownloadPath(string id, string originPath, string installDir)
        {
            StringBuilder builder = new StringBuilder();
            string[] strArray = originPath.Split(new string[] { "$$$" }, StringSplitOptions.None);
            for (int i = 0; i < strArray.Length; i++)
            {
                string[] strArray2 = strArray[i].Split(new string[] { "|" }, StringSplitOptions.None);
                builder.Append(string.Concat(new object[] { "<a href=\"", installDir, "Common/ShowDownloadUrl.aspx?urlid=", i, "&id=", id, "\">", strArray2[0].ToString(), "</a>" }));
                builder.Append("<br />");
            }
            return builder.ToString();
        }

        public string ShowDownloadPathMore(string id, string originPath, string installDir, string arrServerName)
        {
            StringBuilder builder = new StringBuilder();
            string[] strArray = arrServerName.Split(new string[] { "$$$" }, StringSplitOptions.None);
            string[] strArray2 = originPath.Split(new string[] { "$$$" }, StringSplitOptions.None);
            for (int i = 0; i < strArray2.Length; i++)
            {
                for (int j = 1; j < strArray.Length; j++)
                {
                    string[] strArray3 = strArray[j].Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                    builder.Append(string.Concat(new object[] { "<a href=\"", installDir, "Common/ShowDownloadUrl.aspx?urlid=", i, "&id=", id, "&serverid=", strArray3[1].ToString(), "\">" }));
                    if ((strArray3[0].ToString().IndexOf("http://", StringComparison.OrdinalIgnoreCase) > -1) || strArray3[0].ToString().StartsWith("/", StringComparison.OrdinalIgnoreCase))
                    {
                        builder.Append("<img src=\"" + strArray3[0].ToString() + "\" border=\"0\" alt=\"下载服务器logo\"/>");
                    }
                    else
                    {
                        builder.Append(strArray3[0]);
                    }
                    builder.Append("</a>");
                    builder.Append("<br />");
                }
            }
            return builder.ToString();
        }

        public string ShowHeightLineText(string inputText, string keyword, string colorvalue)
        {
            string str = inputText;
            if (string.IsNullOrEmpty(inputText) || string.IsNullOrEmpty(keyword))
            {
                return str;
            }
            if (!string.IsNullOrEmpty(colorvalue))
            {
                return inputText.Replace(keyword, "<font color=\"" + colorvalue + "\">" + keyword + "</font>");
            }
            return inputText.Replace(keyword, "<font color=\"#ff0000\">" + keyword + "</font>");
        }

        public string ShowVoteImage(string ostr)
        {
            if (string.IsNullOrEmpty(ostr))
            {
                return string.Empty;
            }
            XmlDocument document = new XmlDocument();
            try
            {
                ostr = DataSecurity.HtmlDecode(ostr);
                document.LoadXml(ostr);
            }
            catch (XmlException exception)
            {
                return exception.Message;
            }
            int num = 1;
            decimal num2 = DataConverter.CDecimal(this.GetVoteNum(ostr));
            StringBuilder builder = new StringBuilder("<table cellSpacing=\"0\" cellPadding=\"3\" width=\"600\" align=\"center\" border=\"0\">");
            foreach (XmlNode node in document.DocumentElement.ChildNodes)
            {
                XmlElement element = (XmlElement) node;
                if ((element != null) && !string.IsNullOrEmpty(element.GetAttribute("Title")))
                {
                    decimal d = DataConverter.CDecimal(element.GetAttribute("VoteNumber"));
                    if (d > 0M)
                    {
                        d = (d / num2) * 100M;
                    }
                    int num4 = ((int) d) * 5;
                    builder.Append("<tr><td align=\"left\">");
                    builder.Append("<table style=\"width:559px\" cellSpacing=\"0\" cellPadding=\"0\" align=\"left\" border=\"0\">");
                    builder.Append("<tr><td height=\"20px\" style=\"background: url(" + SiteConfig.SiteInfo.VirtualPath + "Images/Vote/blue.gif); width:100%; color:#FFFFFF;\">　选项" + num.ToString() + "：<STRONG>" + element.GetAttribute("Title") + "</STRONG></td></tr></table></td></tr>");
                    builder.Append("<tr><td>");
                    builder.Append("<table height=\"20px\" cellSpacing=\"0\" cellPadding=\"0\" border=\"0\">");
                    builder.Append("<tr><td vAlign=top height=\"11px\" width=\"50px\">得票率：</td> <td vAlign=top style=\"width:500px; height:18px; background: #fff; border:1px solid black;\" >");
                    builder.Append(string.Concat(new object[] { "<div style=\"background:url(", SiteConfig.SiteInfo.VirtualPath, "Images/Vote/orange.gif); width:", num4, "px; height:18px;\"></div></td></tr>" }));
                    builder.Append(" <tr><td colspan=\"2\" align=\"center\">占：" + Math.Round(d, 2).ToString() + " [得：<FONT color=#ff0000>" + element.GetAttribute("VoteNumber") + "</FONT>票]</td></tr></table>");
                    builder.Append(" </td></tr>");
                    num++;
                }
            }
            builder.Append("</table>");
            return builder.ToString();
        }

        public string SiteName()
        {
            return SiteInfo.SiteName;
        }

        public string SitePath()
        {
            return SiteInfo.SiteUrl;
        }

        public string SiteTitle()
        {
            return SiteInfo.SiteTitle;
        }

        public string SlidePlay()
        {
            StringBuilder builder = new StringBuilder("");
            builder.Append("<script language='javascript'>\n");
            builder.Append("function resizepic(thispic)\n");
            builder.Append("{\n");
            builder.Append("if(thispic.width>700){thispic.height=thispic.height*700/thispic.width;thispic.width=700;}\n");
            builder.Append("}\n");
            builder.Append("function bbimg(o)\n");
            builder.Append("{\n");
            builder.Append("  var zoom=parseInt(o.style.zoom, 10)||100;\n");
            builder.Append("  zoom+=event.wheelDelta/12;\n");
            builder.Append("  if (zoom>0) o.style.zoom=zoom+'%';\n");
            builder.Append("  return false;\n");
            builder.Append("}\n");
            builder.Append("var IsPlaying=false;\n");
            builder.Append("var PhotoIndex=1;\n");
            builder.Append("function SlidePlay(){\n");
            builder.Append("  var sTimer,url;\n");
            builder.Append("  if(IsPlaying==false){\n");
            builder.Append("    IsPlaying=true;\n");
            builder.Append("    document.getElementById('SlideButton').value='停止播放';\n");
            builder.Append("    sTimer=setTimeout(\"ViewNext()\",2000);\n");
            builder.Append("  }else{\n");
            builder.Append("    clearTimeout(sTimer);\n");
            builder.Append("    IsPlaying=false;\n");
            builder.Append("    document.getElementById('SlideButton').value='幻灯放映';\n");
            builder.Append("  }\n");
            builder.Append("}\n");
            builder.Append("function ViewNext(){\n");
            builder.Append("  if(IsPlaying==false){return false;}\n");
            builder.Append("  if(PhotoIndex<arrUrl.length){\n");
            builder.Append("    ViewPhoto(arrUrl[PhotoIndex]);\n");
            builder.Append("    PhotoIndex+=1;\n");
            builder.Append("  }\n");
            builder.Append("  if(PhotoIndex>=arrUrl.length){\n");
            builder.Append("    PhotoIndex=0;\n");
            builder.Append("  }\n");
            builder.Append("  var iTimeout=parseInt(document.getElementById('interval').value * 1000);\n");
            builder.Append("  if (isNaN(iTimeout)){\n");
            builder.Append("      iTimeout = 3000;\n");
            builder.Append("  }\n");
            builder.Append("  if(iTimeout<1000){iTimeout=5000;}\n");
            builder.Append("  sTimer=setTimeout(\"ViewNext()\",iTimeout);\n");
            builder.Append("}\n");
            builder.Append("</script>\n");
            return builder.ToString();
        }

        private static string SplitUrl(string url, string uploadfiledir)
        {
            string[] strArray = url.Split(new string[] { "|" }, StringSplitOptions.None);
            string str = string.Empty;
            string str2 = string.Empty;
            if (strArray.Length > 1)
            {
                str = strArray[1];
            }
            if (!string.IsNullOrEmpty(str))
            {
                str2 = Utility.ConvertAbsolutePath(uploadfiledir, str);
            }
            return str2;
        }

        public string TimeNow()
        {
            return DateTime.Now.ToLocalTime().ToString();
        }

        public int TimeSpan(string a1, string a2)
        {
            if (!string.IsNullOrEmpty(a1))
            {
                try
                {
                    DateTime time2;
                    DateTime time = Convert.ToDateTime(a1);
                    if (string.IsNullOrEmpty(a2))
                    {
                        time2 = DateTime.Now.ToLocalTime();
                    }
                    else
                    {
                        time2 = Convert.ToDateTime(a2);
                    }
                    System.TimeSpan span = (System.TimeSpan) (time2 - time);
                    return Convert.ToInt32(span.TotalDays);
                }
                catch (InvalidCastException)
                {
                    return 0;
                }
            }
            return 0;
        }

        public string Txt2Img(string stext, string font, string hwsize, string fcolor, string gcolor, string showshadow, string outfile, string rtime)
        {
            if (string.IsNullOrEmpty(stext.Trim()))
            {
                return string.Empty;
            }
            stext = DataSecurity.PELabelDecode(stext);
            Txt2ImgInfo tinfo = new Txt2ImgInfo();
            tinfo.Text = stext;
            string[] strArray = font.Split(new char[] { ';' });
            if (strArray.Length == 4)
            {
                if (!string.IsNullOrEmpty(strArray[0]))
                {
                    tinfo.FontFamily = strArray[0];
                }
                tinfo.FontSize = DataConverter.CLng(strArray[1]);
                switch (DataConverter.CLng(strArray[2]))
                {
                    case 0:
                        tinfo.FontStyle = FontStyle.Regular;
                        break;

                    case 1:
                        tinfo.FontStyle = FontStyle.Italic;
                        break;

                    case 2:
                        tinfo.FontStyle = FontStyle.Bold;
                        break;

                    case 3:
                        tinfo.FontStyle = FontStyle.Strikeout;
                        break;

                    case 4:
                        tinfo.FontStyle = FontStyle.Underline;
                        break;
                }
                tinfo.Adaptable = DataConverter.CBoolean(strArray[3]);
            }
            strArray = hwsize.Split(new char[] { ';' });
            if (strArray.Length == 4)
            {
                tinfo.Height = DataConverter.CLng(strArray[0]);
                tinfo.Width = DataConverter.CLng(strArray[1]);
                tinfo.Left = DataConverter.CLng(strArray[2]);
                tinfo.Top = DataConverter.CLng(strArray[3]);
            }
            strArray = fcolor.Split(new char[] { ';' });
            if (strArray.Length == 4)
            {
                tinfo.Alpha = DataConverter.CLng(strArray[0]);
                tinfo.Red = DataConverter.CLng(strArray[1]);
                tinfo.Green = DataConverter.CLng(strArray[2]);
                tinfo.Blue = DataConverter.CLng(strArray[3]);
            }
            strArray = gcolor.Split(new char[] { ';' });
            if (strArray.Length == 3)
            {
                tinfo.BackgroundRed = DataConverter.CLng(strArray[0]);
                tinfo.BackgroundGreen = DataConverter.CLng(strArray[1]);
                tinfo.BackgroundBlue = DataConverter.CLng(strArray[2]);
            }
            else if (HttpContext.Current != null)
            {
                tinfo.BackgroundImage = HttpContext.Current.Server.MapPath("~/" + gcolor);
            }
            else
            {
                tinfo.BackgroundImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, gcolor);
            }
            tinfo.Shadow = DataConverter.CBoolean(showshadow);
            if (HttpContext.Current != null)
            {
                tinfo.ResultImage = HttpContext.Current.Server.MapPath("~/" + outfile);
            }
            else
            {
                tinfo.ResultImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, outfile);
            }
            string str = outfile;
            if (File.Exists(tinfo.ResultImage))
            {
                if (DateTime.Now.ToLocalTime() >= File.GetLastWriteTime(tinfo.ResultImage).AddMinutes(DataConverter.CDouble(rtime)))
                {
                    str = WriteToJpeg(tinfo, outfile);
                }
            }
            else
            {
                str = WriteToJpeg(tinfo, outfile);
            }
            return DataSecurity.PELabelEncode(SiteConfig.SiteInfo.VirtualPath + str);
        }

        public string UpLoadDir()
        {
            return (SiteConfig.SiteInfo.VirtualPath + VirtualPathUtility.AppendTrailingSlash(SiteOptionInfo.UploadDir));
        }

        public string UrlEncode(string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                content = DataSecurity.PELabelDecode(content);
                return DataSecurity.PELabelEncode(DataSecurity.UrlEncode(content));
            }
            return string.Empty;
        }

        public string UserPurview(string pname)
        {
            string str = "false";
            if ((HttpContext.Current != null) && PEContext.Current.User.Identity.IsAuthenticated)
            {
                UserPurviewInfo userPurview = PEContext.Current.User.UserInfo.UserPurview;
                string str2 = pname;
                if (str2 == null)
                {
                    return str;
                }
                if (!(str2 == "commentcheck"))
                {
                    if ((str2 == "manageselfpublic") && userPurview.ManageSelfPublicInfo)
                    {
                        str = "true";
                    }
                    return str;
                }
                if (userPurview.EnableComment)
                {
                    str = "true";
                }
            }
            return str;
        }

        private static string value2str(PropertyItem p)
        {
            switch (p.Type)
            {
                case 1:
                    return GetValueOfType1(p.Value);

                case 2:
                    return GetValueOfType1(p.Value);

                case 3:
                    return GetValueOfType3(p.Value);

                case 4:
                    return GetValueOfType4(p.Value);

                case 5:
                    return GetValueOfType5(p.Value);

                case 6:
                    return GetValueOfType1(p.Value);

                case 7:
                    if (string.Compare(p.Id.ToString(), "0x9101", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        return GetValueOfType7B(p.Value);
                    }
                    return GetValueOfType7A(p.Value);

                case 10:
                    return GetValueOfType10(p.Value);
            }
            return string.Empty;
        }

        public string ViewPhoto(int imgWidth, int imgHeight, string originPath, string uploadfiledir, int ltype)
        {
            string str = "";
            StringBuilder builder = new StringBuilder();
            string str2 = "";
            str = GetFirstPhotoUrl(originPath, uploadfiledir, ltype);
            if (string.IsNullOrEmpty(str))
            {
                str = SiteConfig.SiteInfo.VirtualPath + "Images/nopic.gif";
            }
            if (imgWidth > 0)
            {
                str2 = string.Concat(new object[] { " onload='if(this.width>", imgWidth, ") this.width=", imgWidth, "'" });
            }
            else
            {
                imgWidth = 550;
            }
            if (imgHeight <= 0)
            {
                imgHeight = 400;
            }
            builder.Append("<div id='imgBox'></div>");
            builder.Append("<script language='javascript'>\n");
            builder.Append("function ViewPhoto(PhotoUrl){\n");
            builder.Append("  var strHtml;\n");
            builder.Append("  var FileExt=PhotoUrl.substr(PhotoUrl.lastIndexOf('.')+1).toLowerCase();\n");
            builder.Append("  if(FileExt=='gif'||FileExt=='jpg'||FileExt=='png'||FileExt=='bmp'||FileExt=='jpeg'){\n");
            builder.Append("    strHtml=\"<a href='\"+PhotoUrl+\"' target='PhotoView'><img src='\"+PhotoUrl+\"' border='0'" + str2 + "></a>\";\n");
            builder.Append("  }else if(FileExt=='swf'){\n");
            builder.Append(string.Concat(new object[] { "    strHtml=\"<object classid='clsid:D27CDB6E-AE6D-11cf-96B8-444553540000' width='", imgWidth, "' height='", imgHeight, "' codebase='http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=5,0,0,0'><param name='movie' value='\"+PhotoUrl+\"'><param name='quality' value='high'><embed src='\"+PhotoUrl+\"' pluginspage='http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash' type='application/x-shockwave-flash' width='550' height='400'></embed></object>\";\n" }));
            builder.Append("  }else{\n");
            builder.Append("    strHtml=PhotoUrl;\n");
            builder.Append("  }\n");
            builder.Append("  document.getElementById('imgBox').innerHTML=strHtml;\n");
            builder.Append("}\n");
            builder.Append("ViewPhoto('");
            builder.Append(str);
            builder.Append("');\n");
            builder.Append("</script>\n");
            return builder.ToString();
        }

        public string WapEnable()
        {
            return SiteConfig.SiteOption.WapEnable.ToString();
        }

        public string Webmaster()
        {
            return SiteInfo.Webmaster;
        }

        public string WebmasterEmail()
        {
            return SiteInfo.WebmasterEmail;
        }

        private static string WriteToJpeg(Txt2ImgInfo tinfo, string outfile)
        {
            string str2;
            Bitmap image = new Bitmap(tinfo.Width, tinfo.Height, PixelFormat.Format64bppArgb);
            Graphics graphics = Graphics.FromImage(image);
            Font font = new Font(tinfo.FontFamily, (float) tinfo.FontSize, tinfo.FontStyle);
            if (string.IsNullOrEmpty(tinfo.BackgroundImage))
            {
                graphics.Clear(Color.FromArgb(0xff, tinfo.BackgroundRed, tinfo.BackgroundGreen, tinfo.BackgroundBlue));
            }
            else
            {
                try
                {
                    image = new Bitmap(Image.FromFile(tinfo.BackgroundImage));
                    graphics = Graphics.FromImage(image);
                }
                catch (FileNotFoundException)
                {
                    image = new Bitmap(tinfo.Width, tinfo.Height, PixelFormat.Format64bppArgb);
                    graphics = Graphics.FromImage(image);
                    graphics.Clear(Color.FromArgb(0xff, tinfo.BackgroundRed, tinfo.BackgroundGreen, tinfo.BackgroundBlue));
                }
            }
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            SizeF ef = graphics.MeasureString(tinfo.Text, font);
            if (tinfo.Adaptable)
            {
                int width = image.Width;
                int height = image.Height;
                width -= tinfo.Left;
                height -= tinfo.Top;
                while ((ef.Width > width) || (ef.Height > height))
                {
                    tinfo.FontSize--;
                    font = new Font(tinfo.FontFamily, (float) tinfo.FontSize, tinfo.FontStyle);
                    ef = graphics.MeasureString(tinfo.Text, font);
                }
            }
            try
            {
                Brush brush = new SolidBrush(Color.FromArgb(tinfo.Alpha, tinfo.Red, tinfo.Green, tinfo.Blue));
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Near;
                if (tinfo.Shadow)
                {
                    Brush brush2 = new SolidBrush(Color.FromArgb(90, 0, 0, 0));
                    graphics.DrawString(tinfo.Text, font, brush2, (float) (tinfo.Left + 2), (float) (tinfo.Top + 1));
                }
                graphics.DrawString(tinfo.Text, font, brush, new PointF((float) tinfo.Left, (float) tinfo.Top), format);
                string[] strArray = tinfo.ResultImage.Split(new char[] { '.' });
                string strA = strArray[strArray.Length - 1];
                if (string.Compare(strA, "gif", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    image.Save(tinfo.ResultImage, ImageFormat.Gif);
                }
                else if (string.Compare(strA, "tiff", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    image.Save(tinfo.ResultImage, ImageFormat.Tiff);
                }
                else
                {
                    image.Save(tinfo.ResultImage, ImageFormat.Jpeg);
                }
                str2 = outfile;
            }
            catch (Exception exception)
            {
                str2 = "[err:写入图片错，原因：" + exception.Message + "]";
            }
            finally
            {
                font.Dispose();
                image.Dispose();
                graphics.Dispose();
            }
            return str2;
        }

        public string XmlEncode(string inputStr)
        {
            return DataSecurity.XmlEncode(inputStr);
        }

        private static string xmlsafestr(string str)
        {
            return Regex.Replace(str, @"[\x00-\x08\x0b-\x0c\x0e-\x1f]", "");
        }

        public static EasyOne.Components.SiteInfo SiteInfo
        {
            get
            {
                return SiteConfig.SiteInfo;
            }
        }

        public static SiteOption SiteOptionInfo
        {
            get
            {
                return SiteConfig.SiteOption;
            }
        }
    }
}

