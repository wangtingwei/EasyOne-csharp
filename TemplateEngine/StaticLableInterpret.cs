using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace EasyOne.TemplateEngine
{
    /// <summary>
    /// 静态标签解析
    /// </summary>
    public static class StaticLableInterpret
    {
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
                        firstChild = (XmlElement)document.FirstChild;
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
                            firstChild = (XmlElement)document.FirstChild;
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

        /// <summary>
        /// 解析用户配置标签
        /// </summary>
        /// <param name="lableName"></param>
        /// <returns></returns>
        public static string GetUserConfigLable(string lableName)
        {
            return "";
        }
    }
}
