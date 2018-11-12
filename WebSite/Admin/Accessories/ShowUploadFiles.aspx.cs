namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class ShowUploadFiles : AdminPage
    {
        protected string m_ConfigUploadDir;
        protected int m_ItemIndex;
        protected string m_ParentDir;

        protected void BindData()
        {
            string str2 = this.m_ConfigUploadDir + BasePage.RequestString("Dir");
            if (string.IsNullOrEmpty(this.TxtSearchKeyword.Text))
            {
                this.RptFiles.DataSource = FileSystemObject.GetDirectoryInfos(base.Request.PhysicalApplicationPath + str2, FsoMethod.All);
            }
            else
            {
                this.RptFiles.DataSource = FileSystemObject.SearchFiles(base.Request.PhysicalApplicationPath + str2, this.TxtSearchKeyword.Text);
            }
            this.RptFiles.DataBind();
        }

        protected string GetFileContent(string fileName, string extension)
        {
            string basePath = base.BasePath;
            string configUploadDir = this.m_ConfigUploadDir;
            string str3 = BasePage.RequestString("Dir");
            str3 = configUploadDir + str3;
            if (!string.IsNullOrEmpty(fileName))
            {
                fileName = fileName.Replace(@"\", "/");
                if (!fileName.StartsWith("/"))
                {
                    str3 = str3 + "/";
                }
            }
            string str4 = basePath + str3 + fileName;
            StringBuilder builder = new StringBuilder();
            switch (extension)
            {
                case "jpeg":
                case "jpe":
                case "bmp":
                case "png":
                case "jpg":
                case "gif":
                    builder.Append(@"<img src=\'" + str4 + @"\'");
                    builder.Append(@" width=\'200\'");
                    builder.Append(@" height=\'120\'");
                    builder.Append(@" border=\'0\' />");
                    break;

                case "wmv":
                case "avi":
                case "asf":
                case "mpg":
                case "rm":
                case "ra":
                case "ram":
                case "swf":
                    builder.Append(@"<object classid=\'clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\' codebase=\'http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0\'");
                    builder.Append(@" width=\'200\'");
                    builder.Append(@" height=\'120\'");
                    builder.Append(@"><param name=\'movie\' value=\'" + str4 + @"\'>");
                    builder.Append(@"<param name=\'wmode\' value=\'transparent\'>");
                    builder.Append(@"<param name=\'quality\' value=\'autohigh\'>");
                    builder.Append(@"<embed src=\'" + str4 + @"\' quality=\'autohigh\'");
                    builder.Append(@" pluginspage=\'http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash\' type=\'application/x-shockwave-flash\'");
                    builder.Append(@" wmode=\'transparent\'");
                    builder.Append(@" width=\'200\'");
                    builder.Append(@" height=\'120\'");
                    builder.Append("></embed></object>");
                    break;

                default:
                    builder.Append("&nbsp;此文件非图片或动画，无预览&nbsp;");
                    break;
            }
            return builder.ToString();
        }

        protected string GetShowExtension(string extension)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("jpeg", "<img src='../../admin/images/Folder/img.gif'>");
            dictionary.Add("jpe", "<img src='../../admin/images/Folder/img.gif'>");
            dictionary.Add("bmp", "<img src='../../admin/images/Folder/img.gif'>");
            dictionary.Add("png", "<img src='../../admin/images/Folder/img.gif'>");
            dictionary.Add("swf", "<img src='../../admin/images/Folder/Filetype_flash.gif'>");
            dictionary.Add("dll", "<img src='../../admin/images/Folder/img.gif'>");
            dictionary.Add("vbp", "<img src='../../admin/images/Folder/sys.gif'>");
            dictionary.Add("wmv", "<img src='../../admin/images/Folder/Filetype_media.gif'>");
            dictionary.Add("avi", "<img src='../../admin/images/Folder/Filetype_media.gif'>");
            dictionary.Add("asf", "<img src='../../admin/images/Folder/Filetype_media.gif'>");
            dictionary.Add("mpg", "<img src='../../admin/images/Folder/Filetype_media.gif'>");
            dictionary.Add("rm", "<img src='../../admin/images/Folder/Filetype_rm.gif'>");
            dictionary.Add("ra", "<img src='../../admin/images/Folder/Filetype_rm.gif'>");
            dictionary.Add("ram", "<img src='../../admin/images/Folder/Filetype_rm.gif'>");
            dictionary.Add("rar", "<img src='../../admin/images/Folder/zip.gif'>");
            dictionary.Add("zip", "<img src='../../admin/images/Folder/zip.gif'>");
            dictionary.Add("xml", "<img src='../../admin/images/Folder/xml.gif'>");
            dictionary.Add("txt", "<img src='../../admin/images/Folder/txt.gif'>");
            dictionary.Add("exe", "<img src='../../admin/images/Folder/exe.gif'>");
            dictionary.Add("doc", "<img src='../../admin/images/Folder/doc.gif'>");
            dictionary.Add("html", "<img src='../../admin/images/Folder/html.gif'>");
            dictionary.Add("htm", "<img src='../../admin/images/Folder/htm.gif'>");
            dictionary.Add("jpg", "<img src='../../admin/images/Folder/jpg.gif'>");
            dictionary.Add("gif", "<img src='../../admin/images/Folder/gif.gif'>");
            dictionary.Add("xls", "<img src='../../admin/images/Folder/xls.gif'>");
            dictionary.Add("asp", "<img src='../../admin/images/Folder/asp.gif'>");
            if (dictionary.ContainsKey(extension))
            {
                return dictionary[extension];
            }
            return "<img src='../../admin/images/Folder/other.gif'>";
        }

        protected string GetSize(string size)
        {
            if (string.IsNullOrEmpty(size))
            {
                return string.Empty;
            }
            long num = Convert.ToInt64(size);
            long num2 = Convert.ToInt64((long) (num / 0x400L));
            if (num2 < 1L)
            {
                return (num.ToString() + "B");
            }
            if (num2 < 0x400L)
            {
                return (num2.ToString() + "KB");
            }
            long num3 = num2 / 0x400L;
            if (num3 < 1L)
            {
                return (num2.ToString() + "KB");
            }
            if (num3 >= 0x400L)
            {
                num3 /= 0x400L;
                return (num3.ToString() + "GB");
            }
            return (num3.ToString() + "MB");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.m_ConfigUploadDir = SiteConfig.SiteOption.UploadDir;
            if (!string.IsNullOrEmpty(this.m_ConfigUploadDir))
            {
                string configUploadDir = this.m_ConfigUploadDir;
                string str2 = BasePage.RequestString("Dir").Replace("..", string.Empty);
                if (!string.IsNullOrEmpty(str2) && (str2.LastIndexOf("/", StringComparison.Ordinal) > 0))
                {
                    this.m_ParentDir = str2.Remove(str2.LastIndexOf("/", StringComparison.Ordinal), str2.Length - str2.LastIndexOf("/", StringComparison.Ordinal));
                }
                str2 = configUploadDir + str2 + "/";
                DirectoryInfo info = new DirectoryInfo(base.Request.PhysicalApplicationPath + str2);
                if (info.Exists)
                {
                    this.BindData();
                }
                this.LblCurrentDir.Text = str2;
            }
        }

        protected void RptFiles_ItemCommand(object source, CommandEventArgs e)
        {
            string str2 = this.m_ConfigUploadDir + BasePage.RequestString("Dir") + "/" + ((string) e.CommandArgument);
            if (e.CommandName == "DelFiles")
            {
                FileSystemObject.Delete(base.Request.PhysicalApplicationPath + str2.Replace("/", @"\"), FsoMethod.File);
                AdminPage.WriteSuccessMsg("删除成功！", "FileManage.aspx?Dir=" + base.Server.UrlEncode(BasePage.RequestString("Dir")));
            }
            if (e.CommandName == "DelDir")
            {
                base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
                FileSystemObject.Delete(base.Request.PhysicalApplicationPath + str2.Replace("/", @"\"), FsoMethod.Folder);
                AdminPage.WriteSuccessMsg("<li>删除目录成功。</li>", "FileManage.aspx?Dir=" + base.Server.UrlEncode(BasePage.RequestString("Dir")));
            }
        }
    }
}

