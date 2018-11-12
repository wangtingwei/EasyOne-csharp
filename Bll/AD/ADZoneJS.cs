namespace EasyOne.AD
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Enumerations;
    using EasyOne.Model.AD;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;

    public class ADZoneJS
    {
        private AdvertisementInfo advertisementInfo;
        private Dictionary<ADZoneType, string> zoneConfig = new Dictionary<ADZoneType, string>();
        private ADZoneInfo zoneInfo;

        public ADZoneJS()
        {
            this.zoneConfig.Add(ADZoneType.Banner, "Banner");
            this.zoneConfig.Add(ADZoneType.Pop, "Pop");
            this.zoneConfig.Add(ADZoneType.Move, "Move");
            this.zoneConfig.Add(ADZoneType.Fixed, "Fixed");
            this.zoneConfig.Add(ADZoneType.Float, "Float");
            this.zoneConfig.Add(ADZoneType.Code, "Code");
            this.zoneConfig.Add(ADZoneType.Couplet, "Couplet");
        }

        private static bool CheckJSName(string name)
        {
            Regex regex = new Regex(@"^[\w-]+/?\w+\.js$");
            bool flag = false;
            if (regex.IsMatch(name))
            {
                flag = true;
            }
            return flag;
        }

        private string CreatAdvertisementJS()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("var objAD = new ObjectAD();\n");
            builder.Append("objAD.ADID= " + this.advertisementInfo.ADId + ";");
            builder.Append("objAD.ADType= " + this.advertisementInfo.ADType + ";");
            builder.Append("objAD.ADName= \"" + this.advertisementInfo.ADName + "\";");
            string imgUrl = this.advertisementInfo.ImgUrl;
            builder.Append("objAD.ImgUrl= \"" + Utility.ConvertAbsolutePath(SiteConfig.SiteInfo.VirtualPath + VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.UploadDir) + VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.AdvertisementDir), imgUrl) + "\";");
            builder.Append("objAD.ImgWidth       = " + this.advertisementInfo.ImgWidth + ";");
            builder.Append("objAD.ImgHeight      = " + this.advertisementInfo.ImgHeight + ";");
            builder.Append("objAD.FlashWmode     = " + this.advertisementInfo.FlashWmode + ";");
            builder.Append("objAD.ADIntro =\"" + DataSecurity.ConvertToJavaScript(this.advertisementInfo.ADIntro) + "\";");
            builder.Append("objAD.LinkUrl        = \"" + this.advertisementInfo.LinkUrl + "\";");
            builder.Append("objAD.LinkTarget     = " + this.advertisementInfo.LinkTarget + ";");
            builder.Append("objAD.LinkAlt        = \"" + this.advertisementInfo.LinkAlt + "\";");
            builder.Append("objAD.Priority       = " + this.advertisementInfo.Priority + ";");
            builder.Append("objAD.CountView      = " + this.advertisementInfo.CountView.ToString().ToLower(CultureInfo.CurrentCulture) + ";");
            builder.Append("objAD.CountClick     = " + this.advertisementInfo.CountClick.ToString().ToLower(CultureInfo.CurrentCulture) + ";");
            builder.Append("objAD.OverdueDate    = \"" + this.advertisementInfo.OverdueDate.ToString("yyyy", CultureInfo.CurrentCulture) + "/" + this.advertisementInfo.OverdueDate.ToString("MM", CultureInfo.CurrentCulture) + "/" + this.advertisementInfo.OverdueDate.ToString("dd", CultureInfo.CurrentCulture) + "\";");
            builder.Append("objAD.InstallDir     = \"" + VirtualPathUtility.AppendTrailingSlash(HttpContext.Current.Request.ApplicationPath) + "\";");
            builder.Append("objAD.ADDIR= \"" + SiteConfig.SiteOption.AdvertisementDir + "\";");
            builder.Append("ZoneAD_" + this.advertisementInfo.ZoneId + ".AddAD(objAD);");
            return builder.ToString();
        }

        private string CreateADZoneJS()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneId, ".ZoneID=", this.zoneInfo.ZoneId, ";" }));
            builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneId, ".ZoneWidth=", this.zoneInfo.ZoneWidth, ";" }));
            builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneId, ".ZoneHeight=", this.zoneInfo.ZoneHeight, ";" }));
            builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneId, ".ShowType=", this.zoneInfo.ShowType, ";" }));
            builder.Append(this.GetZoneTypeJS());
            builder.Append("ZoneAD_" + this.zoneInfo.ZoneId + ".Show();");
            return builder.ToString();
        }

        public void CreateJS(ADZoneInfo adZoneInfo, IList<AdvertisementInfo> advertisementInfoList)
        {
            this.zoneInfo = adZoneInfo;
            StringBuilder builder = new StringBuilder(this.GetZoneJSTemplate());
            builder.Append("var ZoneAD_" + adZoneInfo.ZoneId + "=new ");
            builder.Append(string.Concat(new object[] { this.zoneConfig[adZoneInfo.ZoneType], "ZoneAD(\"ZoneAD_", adZoneInfo.ZoneId, "\");" }));
            for (int i = 0; i < advertisementInfoList.Count; i++)
            {
                this.advertisementInfo = advertisementInfoList[i];
                this.advertisementInfo.ZoneId = adZoneInfo.ZoneId.ToString(CultureInfo.CurrentCulture);
                if (this.advertisementInfo.Passed && (this.advertisementInfo.Days >= 0))
                {
                    builder.Append(this.CreatAdvertisementJS());
                }
            }
            builder.Append(this.CreateADZoneJS());
            FileSystemObject.WriteFile(VirtualPathUtility.AppendTrailingSlash(HttpContext.Current.Server.MapPath("~/" + SiteConfig.SiteOption.AdvertisementDir)) + adZoneInfo.ZoneJSName, builder.ToString());
        }

        public string GetADZoneJSTemplateContent(ADZoneType zoneType)
        {
            string templateName = this.GetTemplateName(zoneType);
            return FileSystemObject.ReadFile(GetJsTemplatePath() + templateName);
        }

        public string[] GetFileSize()
        {
            int count = this.zoneConfig.Count;
            string[] strArray = new string[count + 1];
            for (int i = 1; i < (count + 1); i++)
            {
                if (FileSystemObject.IsExist(GetJsTemplatePath() + this.GetTemplateName((ADZoneType) i), FsoMethod.File))
                {
                    strArray[i] = FileSystemObject.GetFileSize(GetJsTemplatePath() + this.GetTemplateName((ADZoneType) i));
                }
                else
                {
                    strArray[i] = "0.0KB";
                }
            }
            return strArray;
        }

        private static string GetJsTemplatePath()
        {
            string advertisementDir = SiteConfig.SiteOption.AdvertisementDir;
            HttpContext current = HttpContext.Current;
            if (current != null)
            {
                advertisementDir = current.Server.MapPath("~/" + advertisementDir);
            }
            else
            {
                advertisementDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, advertisementDir);
            }
            return (VirtualPathUtility.AppendTrailingSlash(advertisementDir) + "ADTemplate/");
        }

        public string GetTemplateName(ADZoneType zoneType)
        {
            return ("Template_" + this.zoneConfig[zoneType] + ".js");
        }

        public string GetZoneJSName()
        {
            string zoneJSName = this.zoneInfo.ZoneId + ".js";
            if (!string.IsNullOrEmpty(GetZoneJSName(this.zoneInfo.ZoneJSName)))
            {
                zoneJSName = GetZoneJSName(this.zoneInfo.ZoneJSName);
            }
            return zoneJSName;
        }

        public static string GetZoneJSName(string zoneJSName)
        {
            string str = null;
            if (!CheckJSName(zoneJSName))
            {
                return str;
            }
            string[] strArray = zoneJSName.Split(new char[] { '/' });
            if (strArray.Length == 2)
            {
                return strArray[1];
            }
            return zoneJSName;
        }

        public string GetZoneJSTemplate()
        {
            return this.GetADZoneJSTemplateContent(this.zoneInfo.ZoneType);
        }

        private string GetZoneTypeJS()
        {
            StringBuilder builder = new StringBuilder();
            string[] strArray = (this.zoneInfo.Setting + ",,,,,").Split(new char[] { ',' });
            switch (this.zoneInfo.ZoneType)
            {
                case ADZoneType.Pop:
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneId, ".PopType = ", strArray[0], ";" }));
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneId, ".Left= ", strArray[1], ";" }));
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneId, ".Top= ", strArray[2], ";" }));
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneId, ".CookieHour  = ", strArray[3], ";" }));
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneId, ".LocalityType = ", strArray[4], ";" }));
                    break;

                case ADZoneType.Move:
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneId, ".Left=", strArray[0], ";" }));
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneId, ".Top=", strArray[1], ";" }));
                    if (!string.IsNullOrEmpty(strArray[2]))
                    {
                        builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneId, ".Delta=", strArray[2], ";" }));
                    }
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneId, ".ShowCloseAD=", strArray[3], ";" }));
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneId, ".CloseFontColor=\"", strArray[4], "\";" }));
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneId, ".LocalityType = ", strArray[5], ";" }));
                    break;

                case ADZoneType.Fixed:
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneId, ".Left= ", strArray[0], ";" }));
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneId, ".Top= ", strArray[1], ";" }));
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneId, ".ShowCloseAD=", strArray[2], ";" }));
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneId, ".CloseFontColor=\"", strArray[3], "\";" }));
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneId, ".LocalityType = ", strArray[4], ";" }));
                    break;

                case ADZoneType.Float:
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneId, ".FloatType= ", strArray[0], ";" }));
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneId, ".Left= ", strArray[1], ";" }));
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneId, ".Top= ", strArray[2], ";" }));
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneId, ".ShowCloseAD=", strArray[3], ";" }));
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneId, ".CloseFontColor=\"", strArray[4], "\";" }));
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneId, ".LocalityType = ", strArray[5], ";" }));
                    break;

                case ADZoneType.Couplet:
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneId, ".Left=", strArray[0], ";" }));
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneId, ".Top=", strArray[1], ";" }));
                    if (!string.IsNullOrEmpty(strArray[2]))
                    {
                        builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneId, ".Delta=", strArray[2], ";" }));
                    }
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneId, ".ShowCloseAD=", strArray[3], ";" }));
                    builder.Append(string.Concat(new object[] { "ZoneAD_", this.zoneInfo.ZoneId, ".CloseFontColor=\"", strArray[4], "\";" }));
                    break;
            }
            return builder.ToString();
        }

        public bool SaveJSTemplate(string template, ADZoneType zoneType)
        {
            string templateName = this.GetTemplateName(zoneType);
            try
            {
                FileSystemObject.WriteFile(GetJsTemplatePath() + templateName, template);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

